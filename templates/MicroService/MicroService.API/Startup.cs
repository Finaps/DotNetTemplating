using MicroService.Infrastructure.ErrorHandling;
using MicroService.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace MicroService
{
  public class Startup
  {
    private readonly ILogger<Startup> _logger;
    public Startup(
      ILogger<Startup> logger,
      IConfiguration configuration)
    {
      _logger = logger;
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.ConfigureErrorHandling();
      services.ConfigureSwagger();
      services.AddSingleton<IConfiguration>(Configuration);
      services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());
      services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        Predicate = _ => true
      });

      app.UseHealthChecks("/liveness", new HealthCheckOptions
      {
        Predicate = r => r.Name.Contains("self")
      });

      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });

      app.UseRouting();
      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
