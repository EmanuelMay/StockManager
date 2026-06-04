using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using StockManager.Domain.Entities;
using StockManager.Domain.Exceptions;

namespace StockManager.Presentation.Middlewares;

public static class ApiExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    var exception = contextFeature.Error;

                    context.Response.StatusCode = exception switch
                    {
                        UserNotFoundException =>
                            StatusCodes.Status404NotFound,
                        
                        EmailAlreadyExistsException =>
                            StatusCodes.Status409Conflict,
                        
                        CategoryAlreadyExistsException =>
                            StatusCodes.Status409Conflict,
                        
                        CategoryNotFoundException =>
                            StatusCodes.Status404NotFound,
                        
                        ProductAlreadyExistsException =>
                            StatusCodes.Status409Conflict,
                        
                        ArgumentException =>
                            StatusCodes.Status400BadRequest,
                        
                        ProductNotFoundException =>
                            StatusCodes.Status404NotFound,
                        
                        _ =>
                            StatusCodes.Status500InternalServerError
                    };

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
                        Trace = env.IsDevelopment()
                            ? contextFeature.Error.StackTrace
                            : null
                    }.ToString());
                }
            });
        });
    }
}
