using SecretNight.Entities.Models;

namespace SecretNight.Repository
{
    public interface IProductoRepository
    {
        Task AddAsync(Producto producto);
        Task DeleteAsync(Producto producto);
        Task<List<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int? id);
        Task UpdateAsync(Producto producto);
    }
}