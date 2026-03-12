using Abp.Application.Services;
using AbpPractice.Products.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbpPractice.Products;

public interface IProductAppService : IApplicationService
{
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto> CreateAsync(CreateProductDto input);
}