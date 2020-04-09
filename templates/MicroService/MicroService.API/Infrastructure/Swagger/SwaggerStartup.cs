using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MicroService.Infrastructure.Swagger
{
  public static class SwaggerStartup
  {
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
      return services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "API",
          Version = "v1",
          Description = "Service",
          Contact = new OpenApiContact
          {
            Name = "GitHub Repo",
            Email = ""
          }
        });
        var filePath = Path.Combine(System.AppContext.BaseDirectory, "MicroService.API.xml");
        c.IncludeXmlComments(filePath);
      });
    }
  }
}
