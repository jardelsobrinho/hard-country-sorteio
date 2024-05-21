using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ServerSorterio.Api.Configs;

public static class ErrorHandleExtensions
{
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        _ = app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {

                IExceptionHandlerFeature? exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature != null)
                {
                    Exception exception = exceptionHandlerFeature.Error;

                    ProblemDetails problemDetails = new()
                    {
                        Instance = context.Request.HttpContext.Request.Path
                    };

                    if (exception.Message.IndexOf("http://") > 0 || exception.Message.IndexOf("localhost") > 0)
                    {
                        problemDetails.Title = "Falha no servidor da Bless Sistemas";
                        problemDetails.Instance = "";
                    }
                    else
                    {
                        problemDetails.Title = exception.Message;
                    }

                    if (exception is not null || exception is BadHttpRequestException)
                    {
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                    }
                    else
                    {
                        problemDetails.Status = StatusCodes.Status500InternalServerError;
                    }

                    problemDetails.Detail = exception!.InnerException != null ? exception.InnerException.ToString() : "";

                    context.Response.StatusCode = problemDetails.Status.Value;
                    context.Response.ContentType = "application/problem+json";

                    string json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    await context.Response.WriteAsync(json);
                }
            });
        });
    }
}
