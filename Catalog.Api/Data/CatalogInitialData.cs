using Marten.Schema;

namespace Catalog.Api.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
            return;
        
        session.Store(GetProducts());
        await session.SaveChangesAsync(cancellation);
    }
    
    private static IEnumerable<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 1",
                Categories = ["Category 1", "Category 2"],
                Description = "Description 1",
                Price = 10.00m,
                ImageFile = "product1.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 2",
                Categories = ["Category 1", "Category 3"],
                Description = "Description 2",
                Price = 20.00m,
                ImageFile = "product2.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 3",
                Categories = ["Category 2", "Category 3"],
                Description = "Description 3",
                Price = 30.00m,
                ImageFile = "product3.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 4",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 4",
                Price = 40.00m,
                ImageFile = "product4.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 5",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 5",
                Price = 50.00m,
                ImageFile = "product5.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 6",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 6",
                Price = 60.00m,
                ImageFile = "product6.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 7",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 7",
                Price = 70.00m,
                ImageFile = "product7.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 8",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 8",
                Price = 80.00m,
                ImageFile = "product8.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 9",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 9",
                Price = 90.00m,
                ImageFile = "product9.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 10",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 10",
                Price = 100.00m,
                ImageFile = "product10.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 11",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 11",
                Price = 110.00m,
                ImageFile = "product11.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 12",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 12",
                Price = 120.00m,
                ImageFile = "product12.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 13",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 13",
                Price = 130.00m,
                ImageFile = "product13.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 14",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 14",
                Price = 140.00m,
                ImageFile = "product14.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 15",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 15",
                Price = 150.00m,
                ImageFile = "product15.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 16",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 16",
                Price = 160.00m,
                ImageFile = "product16.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 17",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 17",
                Price = 170.00m,
                ImageFile = "product17.jpg"
            },
            new Product
            {
                Id = Guid.CreateVersion7(),
                Name = "Product 18",
                Categories = ["Category 1", "Category 2", "Category 3"],
                Description = "Description 18",
                Price = 180.00m,
                ImageFile = "product18.jpg"
            }
        };
    }
}