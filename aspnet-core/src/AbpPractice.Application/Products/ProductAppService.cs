using Abp.Application.Services;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using AbpPractice.Products.Dto;
using AbpPractice.Products.Events;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AbpPractice.Products;

public class ProductAppService : ApplicationService, IProductAppService
{
    private readonly IRepository<Product, int> _productRepository;
    private readonly IEventBus _eventBus;

    public ProductAppService(IRepository<Product, int> productRepository, IEventBus eventBus)
    {
        _productRepository = productRepository;
        _eventBus = eventBus;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
         return await _productRepository
            .GetAll()
            .Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive
            })
            .ToListAsync();
    }

    [Audited]
    public async Task<ProductDto> CreateAsync(CreateProductDto input)
    {
        var product = new Product(
            input.Name,
            input.Description,
            input.Price,
            input.IsActive
        );

        await _productRepository.InsertAsync(product);
        await CurrentUnitOfWork.SaveChangesAsync();

        await _eventBus.TriggerAsync(new ProductCreatedEventData(product));

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            IsActive = product.IsActive
        };
    }
}