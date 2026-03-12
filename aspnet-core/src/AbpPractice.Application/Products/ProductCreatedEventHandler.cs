using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Abp.Logging;
using AbpPractice.Products.Events;
using Castle.Core.Logging;

namespace AbpPractice.Products;

public class ProductCreatedEventHandler : IEventHandler<ProductCreatedEventData>, ITransientDependency
{
    private readonly ILogger _logger;

    public ProductCreatedEventHandler(ILogger logger)
    {
        _logger = logger;
    }

    public void HandleEvent(ProductCreatedEventData eventData)
    {
        _logger.Info($"Product created: {eventData.Product.Name} (Id: {eventData.Product.Id})");
    }
}