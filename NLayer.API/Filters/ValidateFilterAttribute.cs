using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs.ResponseDTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Model'de Hata varsa
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x=>x.Errors)
                                                      .Select(x=>x.ErrorMessage)
                                                      .ToList(); // error mesajlarını aldık

                context.Result = new BadRequestObjectResult(
                    CustomResponseDto<NoResponseDto>.Fail(400,errors));
            }
        }
    }
}
