using FluentValidation;
using NLayer.Core.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("{PropertyName} can not be null.")
                                .NotEmpty().WithMessage("{PropertyName} can not be empty.");

            RuleFor(p => p.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(p => p.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(p => p.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
