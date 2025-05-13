using System.Text.Json.Serialization;

namespace Mima.Domain.Model
{
    public class Sale
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CustomerName { get; set; } 
        public DateTime SaleDate { get; set; } = DateTime.UtcNow; 
        public decimal TotalPay { get; set; } 
        [JsonIgnore]
        public User User { get; set; }
        public ICollection<SaleProduct> SalesProducts { get; set; }



    }
}
