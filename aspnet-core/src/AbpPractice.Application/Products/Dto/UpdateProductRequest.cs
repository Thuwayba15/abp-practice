namespace AbpPractice.Products.Dto;

public class UpdateProductRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}