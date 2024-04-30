using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;
using System.Net;

namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IGenericService<T> _service;
        public NotFoundFilter(IGenericService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments["id"]; // istekteki id parametresi

            if (idValue == null)
            {
                await next.Invoke(); // id null ise istek yoluna devam eder.
                return;
            }

            var id = (int)idValue; // idValue'nün türü object. Unboxing yapalım ve int'a çevirelim.

            //var entityExist = await _service.GetByIdAsync(id); 
            var entityExist = await _service.AnyAsync(x => x.Id == id); // Any kullanırsak daha performanslı bir filter yazmış oluruz.

            if (entityExist)
            {
                await next.Invoke(); // sorgulanan entity var ise istek yoluna devam eder.
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoResponseDto>.Fail((int)HttpStatusCode.NotFound,$"{id} numaralı {typeof(T).Name} bulunamadı"));
        }
    }
}
