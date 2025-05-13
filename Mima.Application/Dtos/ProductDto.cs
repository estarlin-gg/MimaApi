namespace Mima.Application.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal? Discount { get; set; }


    }
}
