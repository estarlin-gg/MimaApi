using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mima.Application.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPay { get; set; }
        public List<SaleProductDto> SalesProducts { get; set; }
    }
}
