using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.UI;
using AbpPractice.Products.Dto;
using AbpPractice.Products.Events;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AbpPractice.Products;

/// <summary>
/// Handles product-related application use cases.
/// </summary>
public class ProductAppService : ApplicationService, IProductAppService
{
    private readonly IRepository<Product, int> _productRepository;
    private readonly IEventBus _eventBus;

    public ProductAppService(
        IRepository<Product, int> productRepository,
        IEventBus eventBus)
    {
        _productRepository = productRepository;
        _eventBus = eventBus;
    }

    /// <summary>
    /// Returns a paged list of products using an efficient projected query.
    /// </summary>
    public async Task<PagedResultDto<ProductDto>> GetAllAsync(GetProductsInput input)
    {
        var query = _productRepository.GetAll();

        var totalCount = await query.CountAsync();

        var products = await query
            .OrderBy(product => product.Id)
            .PageBy(input)
            .Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive
            })
            .ToListAsync();

        return new PagedResultDto<ProductDto>(totalCount, products);
    }

    /// <summary>
    /// Creates a new product and raises a domain event after persistence.
    /// </summary>
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

        return MapToProductDto(product);
    }

    /// <summary>
    /// Updates an existing product while keeping domain rules inside the entity.
    /// </summary>
    [Audited]
    public async Task<ProductDto> UpdateAsync(UpdateProductInput input)
    {
        var product = await _productRepository.FirstOrDefaultAsync(input.Id);

        if (product is null)
        {
            throw new UserFriendlyException("Product not found.");
        }

        product.UpdateDetails(
            input.Name,
            input.Description,
            input.Price,
            input.IsActive
        );

        await _productRepository.UpdateAsync(product);
        await CurrentUnitOfWork.SaveChangesAsync();

        return MapToProductDto(product);
    }

    /// <summary>
    /// Deletes an existing product by id.
    /// </summary>
    [Audited]
    public async Task DeleteAsync(EntityDto<int> input)
    {
        var product = await _productRepository.FirstOrDefaultAsync(input.Id);

        if (product is null)
        {
            throw new UserFriendlyException("Product not found.");
        }

        await _productRepository.DeleteAsync(product);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Maps a product entity to its DTO representation.
    /// </summary>
    private static ProductDto MapToProductDto(Product product)
    {
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