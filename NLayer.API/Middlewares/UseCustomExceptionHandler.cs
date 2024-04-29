using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Service.Exceptions;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder application)
        {
            application.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json; // "application/json" yazmak yerine böyle tanımlayabiliriz.

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>(); // bu nesne hata ile ilgili bilgileri barındıracak

                    if(exceptionFeature != null)
                    {
                        context.Response.StatusCode = exceptionFeature.Error switch
                        {
                            ClientSideException => (int)HttpStatusCode.BadRequest, // 400 status code

                            NotFoundException => (int)HttpStatusCode.NotFound, // 404 status code

                            _ => (int)HttpStatusCode.InternalServerError // default hata -> 500 statusCode
                        };

                        var createdResponse = CustomResponseDto<NoResponseDto>.Fail(context.Response.StatusCode, exceptionFeature.Error.Message);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(createdResponse));
                    }
                });
            });
        }
    }
}
