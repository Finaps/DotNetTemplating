using Microsoft.Extensions.DependencyInjection;

namespace MicroService.Common.Rabbit
{
    public static class RabbitMQBuilderExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services) =>
            services
                .AddSingleton<RabbitConnection>()
                .AddSingleton<RabbitManager>();
    }
}