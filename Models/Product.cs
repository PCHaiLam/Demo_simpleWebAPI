namespace Demo_simpleWebAPI.Models
{
    public class ProductFG
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class Product : ProductFG
    {
        public Guid Id { get; set; }
    }
}
