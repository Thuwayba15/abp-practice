using Abp.Events.Bus;

namespace AbpPractice.Products.Events;

public class ProductCreatedEventData : EventData
{
    public Product Product { get; }

    public ProductCreatedEventData(Product product)
    {
        Product = product;
    }
}