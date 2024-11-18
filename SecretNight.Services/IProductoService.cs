using SecretNight.Entities.Models;

namespace SecretNight.Services
{
    public interface IProductoService
    {
        Task<ResultModel<bool>> AddAsync(Producto producto);
        Task<ResultModel<bool>> DeleteAsync(int? id);
        Task<ResultModel<List<Producto>>> GetAllAsync();
        Task<ResultModel<Producto>> GetByIdAsync(int? id);
        Task<ResultModel<bool>> UpdateAsync(Producto producto);
    }
}