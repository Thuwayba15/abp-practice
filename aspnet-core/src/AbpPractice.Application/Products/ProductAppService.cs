using Abp.Application.Services;
using Abp.Domain.Repositories;
using AbpPractice.Products.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpPractice.Products;

public class ProductAppService : ApplicationService, IProductAppService
{
    private readonly IRepository<Product, int> _productRepository;

    public ProductAppService(IRepository<Product, int> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllListAsync();

        return products
            .Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive
            })
            .ToList();
    }
}