using Microsoft.EntityFrameworkCore;
using Mima.Domain.Model;
using Mima.Infrastructure.Repositories.Abstraction;

namespace Mima.Infrastructure.Repositories.Implementation
{

    public class SaleRepository : ISaleRepository
    {
        private readonly MimaContext _context;

        public SaleRepository(MimaContext context)
        {
            _context = context;
        }
        public async Task CreateSale(Sale sale)
        {
            foreach (var s in sale.SalesProducts)
            {
                
            }
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetAllSales(string userId)
        {
            return await _context.Sales
                 .Where(s => s.UserId == userId)
                 .Include(s => s.SalesProducts)
                 .ThenInclude(sp => sp.Product)
                 .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetSalesByYears(string userId)
        {
            var result = await _context.Sales
                .Where(s => s.UserId == userId)
                .GroupBy(s => new { s.SaleDate.Year, s.SaleDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSales = g.Count(),
                    TotalPay = g.Sum(s => s.TotalPay)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync();

            var groupedByYear = result
                .GroupBy(r => r.Year)
                .Select(yearGroup => new
                {
                    Year = yearGroup.Key,
                    Months = yearGroup.Select(m => new
                    {
                        Month = m.Month,
                        TotalSales = m.TotalSales,
                        TotalPay = m.TotalPay
                    }).ToList()
                });

            return groupedByYear;
        }


        public async Task<Sale> GetSaleById(int id)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(p => p.Id == id);

            if (sale == null) throw new Exception("venta no encontrada");

            return sale;

        }
    }
}
