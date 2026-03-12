using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AbpPractice.Products;

public class Product : Entity<int>
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    protected Product()
    {
    }

    public Product(string name, string? description, decimal price, bool isActive = true)
    {
        Name = name;
        Description = description;
        Price = price;
        IsActive = isActive;
    }
}