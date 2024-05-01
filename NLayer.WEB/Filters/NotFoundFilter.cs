using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;
using NLayer.WEB.Models;

namespace NLayer.WEB.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IGenericService<T> _genericService;
        public NotFoundFilter(IGenericService<T> genericService)
        {
            _genericService = genericService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments["id"];

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            int id = (int)idValue;

            var entityExist = await _genericService.AnyAsync(entity => entity.Id == id);

            if (entityExist)
            {
                await next.Invoke();
                return;
            }

            var errorViewModel = new ErrorViewModel();
            errorViewModel.Errors.Add($"{typeof(T).Name}({id}) not found");

            context.Result = new RedirectToActionResult("Error", "Home",errorViewModel);
        }
    }
}
