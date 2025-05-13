using Mima.Domain.Model;

namespace Mima.Infrastructure.Repositories.Abstraction
{
    public interface ISaleRepository
    {
        Task CreateSale(Sale sale);
        Task<IEnumerable<Sale>> GetAllSales(string userId);
        Task<Sale> GetSaleById(int id);
    }
}
