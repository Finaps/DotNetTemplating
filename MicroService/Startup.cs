using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using MicroService.Common.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MicroService.Common.Common;

namespace MicroService
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      // This code is used to configure what happens when the modelstate is invalid (validation errors)
      // Right now it just logs and then returns the errors to the client
      services.PostConfigure<ApiBehaviorOptions>(options =>
      {
        var builtInFactory = options.InvalidModelStateResponseFactory;

        options.InvalidModelStateResponseFactory = context =>
        {
          var loggerFactory = context.HttpContext.RequestServices
            .GetRequiredService<ILoggerFactory>();
          var logger = loggerFactory.CreateLogger(context.ActionDescriptor.DisplayName);
          var errorString = string.Join("\n", context.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage));
          logger.LogError("Validation error on incoming request:\n{0}", errorString);

          return builtInFactory(context);
        };
      });
      services.AddSingleton<IConfiguration>(Configuration);
      services.ConfigureDatabase(Configuration);
      services.ConfigureResolvers(Configuration);
      services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());
    }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      app.UseHealthChecks("/hc", new HealthCheckOptions()
      {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      });

      app.UseHealthChecks("/liveness", new HealthCheckOptions
      {
        Predicate = r => r.Name.Contains("self")
      });
      app.UseMvc();
    }
  }
  public static class CustomExtensionMethods
  {
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddSingleton<MongoConnection>();
      services.Configure<MongoOptions>(configuration.GetSection("Mongo"));


      return services;
    }
    public static IServiceCollection ConfigureResolvers(this IServiceCollection services, IConfiguration configuration)
    {
      return services;
    }
    private static IServiceCollection ConfigureMongo<T>(this IServiceCollection services) where T : IMongoModel
    {
      return services.AddSingleton<IDatabase<T>, MongoDatabase<T>>((ctx) =>
      {
        MongoConnection connection = ctx.GetRequiredService<MongoConnection>();
        return new MongoDatabase<T>(connection, typeof(T).Name);
      });
    }
  }
}
