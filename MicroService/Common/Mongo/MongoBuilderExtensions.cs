using Microsoft.Extensions.DependencyInjection;

namespace MicroService.Common.Mongo
{
  public static class MongoBuilderExtensions
  {
    public static IServiceCollection AddMongoDBConnection(this IServiceCollection services) =>
        services.AddSingleton<MongoConnection>();


  }
}
