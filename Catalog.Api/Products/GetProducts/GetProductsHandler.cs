// ReSharper disable ClassNeverInstantiated.Global

using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsQuery(int Page = 1, int PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.Page, query.PageSize, cancellationToken);

        return new GetProductsResult(products);
    }
}