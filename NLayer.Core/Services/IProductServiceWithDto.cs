using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductServiceWithDto : IServiceWithDto<Product, ProductDto>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync();

        Task<CustomResponseDto<NoResponseDto>> UpdateAsync(ProductUpdateDto dto); // Bizim IServiceWithDto'dan gelen Update(Dto dto) metodumuz var. Biz IProductService'de Dto'yu ProductDto olarak belirledik. Yani IServiceWithDto<Entity,Dto> arayüzüne Entity olarak Product sınıfını veriyoruz tamam güzel, fakat Dto olarak tek tür dto (ProductDto) veriyoruz. Ancak, bizim Controller'lardaki Update metodlarımız Parametre olarak ProductDto değil, ProductUpdateDto alıyor. Yani Biz eğer IServiceWithDto<> dan gelen Update() metodunu overload etmezsek ProductController'da update metodunda ProductUpdateDto alamayız.Bu yüzden ProductUpdateDto alan overload edilmiş Update metodunu tanımladık.

        Task<CustomResponseDto<ProductDto>> AddAsync(CreateProductDto dto);

        Task<CustomResponseDto<IEnumerable<ProductDto>>> AddRangeAsync(IEnumerable<CreateProductDto> dtos);

    }
}
