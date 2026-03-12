using Abp.Domain.Entities;
using Abp.UI;

namespace AbpPractice.Products;

/// <summary>
/// Represents a product in the domain.
/// Encapsulates business rules related to product state and updates.
/// </summary>
public class Product : Entity<int>
{
    public string Name { get; private set; }

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public bool IsActive { get; private set; }

    protected Product()
    {
    }

    public Product(string name, string? description, decimal price, bool isActive = true)
    {
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        SetActiveState(isActive);
    }

    public void UpdateDetails(string name, string? description, decimal price, bool isActive)
    {
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        SetActiveState(isActive);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new UserFriendlyException("Product name cannot be empty.");
        }

        if (name.Length > 100)
        {
            throw new UserFriendlyException("Product name cannot exceed 100 characters.");
        }

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (description != null && description.Length > 500)
        {
            throw new UserFriendlyException("Product description cannot exceed 500 characters.");
        }

        Description = description;
    }

    public void SetPrice(decimal price)
    {
        if (price < 0)
        {
            throw new UserFriendlyException("Product price cannot be negative.");
        }

        Price = price;
    }

    public void SetActiveState(bool isActive)
    {
        IsActive = isActive;
    }

}