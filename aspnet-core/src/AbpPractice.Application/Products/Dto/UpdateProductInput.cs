using Abp.Application.Services.Dto;

namespace AbpPractice.Products.Dto;

/// <summary>
/// Request used to update an existing product.
/// </summary>
public class UpdateProductInput : EntityDto<int>
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}