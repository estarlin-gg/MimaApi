using System.Text.Json.Serialization;

namespace Mima.Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]

        public string UserId { get; set; }
        public int Stock { get; set; } 
        [JsonIgnore]
        public User User { get; set; }
        public decimal? Discount { get; set; }
        public decimal FinalPrice => Discount.HasValue ? Price - (Price * (Discount.Value / 100)) : Price;

    }
}
