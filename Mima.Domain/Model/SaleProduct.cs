using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mima.Domain.Model
{
    public class SaleProduct
    {
        public int Id { get; set; }
        public int SalesId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } 
        public Sale Sales { get; set; }
        public Product Product { get; set; }
    }
}
