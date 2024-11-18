using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretNight.Entities.Models; // Asegúrate de que el espacio de nombres sea correcto
using Microsoft.EntityFrameworkCore;
using SecretNight.Entities.Data;

namespace SecretNight.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDBContext _context;

        public ProductoRepository(AppDBContext context)
        {
            _context = context;
        }

        // Método para obtener todos los registros de la entidad Producto
        public async Task<List<Producto>> GetAllAsync()
        {
            return await _context.Set<Producto>().ToListAsync(); // Usa Producto en lugar de T
        }

        // Método para obtener un registro de Producto por ID
        public async Task<Producto?> GetByIdAsync(int? id)
        {
            return await _context.Set<Producto>().FindAsync(id); // Usa Producto en lugar de T
        }

        // Método para agregar una nueva entidad Producto
        public async Task AddAsync(Producto producto) // Cambié T a Producto
        {
            await _context.Set<Producto>().AddAsync(producto); // Usa Producto en lugar de T
            await _context.SaveChangesAsync();
        }

        // Método para actualizar una entidad Producto existente
        public async Task UpdateAsync(Producto producto) // Cambié T a Producto
        {
            _context.Set<Producto>().Update(producto); // Usa Producto en lugar de T
            await _context.SaveChangesAsync();
        }

        // Método para eliminar una entidad Producto
        public async Task DeleteAsync(Producto producto) // Cambié T a Producto
        {
            _context.Set<Producto>().Remove(producto); // Usa Producto en lugar de T
            await _context.SaveChangesAsync();
        }
    }
}
