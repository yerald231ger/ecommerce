// ReSharper disable ClassNeverInstantiated.Global

namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string Category)
    : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductsByCategoryQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting products by category {@Category}", query.Category);
        
        var products  = await session.Query<Product>()
            .Where(p => p.Categories.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}