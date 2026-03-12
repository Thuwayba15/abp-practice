using Abp.Application.Services;
using AbpPractice.Products.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace AbpPractice.Products;

public interface IProductAppService : IApplicationService
{
    Task<PagedResultDto<ProductDto>> GetAllAsync(GetProductsInput input);
    Task<ProductDto> CreateAsync(CreateProductDto input);

    Task<ProductDto> UpdateAsync(UpdateProductInput input);

    Task DeleteAsync(EntityDto<int> input);
}