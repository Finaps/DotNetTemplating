using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MicroService.Infrastructure.ErrorHandling
{
  public static class ErrorHandlingStartup
  {
    public static IServiceCollection ConfigureErrorHandling(this IServiceCollection services)
    {
      // This code is used to configure what happens when the modelstate is invalid (validation errors)
      // Right now it just logs and then returns the errors to the client
      return services.PostConfigure<ApiBehaviorOptions>(options =>
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
    }
  }
}
