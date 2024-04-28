using AutoMapper;
using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.DTOs.ProductFeatureDTOs;
using NLayer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<CreateProductDto,Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>();

            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>();


            CreateMap<ProductFeature,ProductFeatureDto>().ReverseMap();
        }
    }
}
