using Demo_simpleWebAPI.Models;

namespace Demo_simpleWebAPI.Data
{
    public static class FakeData
    {
        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 100
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Price = 200
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 3",
                Price = 300
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 4",
                Price = 400
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 5",
                Price = 500
            }
        };
    }
}
