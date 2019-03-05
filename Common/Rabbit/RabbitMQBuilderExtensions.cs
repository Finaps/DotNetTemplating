using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroService.Common.Rabbit
{
  public static class RabbitMQBuilderExtensions
  {
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddSingleton<RabbitConnection>()
            .AddSingleton<RabbitManager>()
            .Configure<RabbitOptions>(configuration.GetSection("Rabbit"));
  }
}
