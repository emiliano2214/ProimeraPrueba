using SecretNight.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretNight.Entities.Models;

namespace SecretNight.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        // Constructor para inyectar el repositorio de productos
        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        // Método para obtener todos los productos
        public async Task<ResultModel<List<Producto>>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync(); // Llamamos al repositorio para obtener todos los productos

            // Verificar si los datos se obtuvieron correctamente
            if (data == null || !data.Any())
            {
                return new ResultModel<List<Producto>>
                {
                    isSuccess = false,
                    Data = new List<Producto>(), // Retorna una lista vacía en lugar de null
                    ErrorMessage = "No se encontraron productos."
                };
            }

            return new ResultModel<List<Producto>>
            {
                isSuccess = true,
                Data = data, // Retorna la lista de productos
                ErrorMessage = null // No hay error
            };
        }

        // Método para obtener un producto por ID
        public async Task<ResultModel<Producto>> GetByIdAsync(int? id)
        {
            if (id == null || id <= 0)
            {
                return new ResultModel<Producto>
                {
                    isSuccess = false,
                    Data = null,
                    ErrorMessage = "ID de producto inválido."
                };
            }

            var entity = await _repository.GetByIdAsync(id); // Llamamos al repositorio para obtener el producto por ID

            if (entity == null)
            {
                return new ResultModel<Producto>
                {
                    isSuccess = false,
                    Data = null,
                    ErrorMessage = "Producto no encontrado."
                };
            }

            return new ResultModel<Producto>
            {
                isSuccess = true,
                Data = entity, // Retorna el producto encontrado
                ErrorMessage = null // No hay error
            };
        }

        // Método para agregar un nuevo producto
        public async Task<ResultModel<bool>> AddAsync(Producto producto)
        {
            try
            {
                // Verificamos si el producto es válido
                if (producto == null)
                {
                    return new ResultModel<bool>
                    {
                        isSuccess = false,
                        ErrorMessage = "El producto no puede ser nulo."
                    };
                }

                await _repository.AddAsync(producto); // Llamamos al repositorio para agregar el producto

                // Verifica si el ID se asignó correctamente después de guardar
                if (producto.IdProductos <= 0)
                {
                    return new ResultModel<bool>
                    {
                        isSuccess = false,
                        ErrorMessage = "El ID no fue asignado correctamente."
                    };
                }

                return new ResultModel<bool>
                {
                    isSuccess = true,
                    Data = true // Producto agregado correctamente
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<bool>
                {
                    isSuccess = false,
                    ErrorMessage = ex.Message // Si ocurre un error, lo capturamos
                };
            }
        }

        // Método para actualizar un producto
        public async Task<ResultModel<bool>> UpdateAsync(Producto producto)
        {
            try
            {
                // Verificamos si el producto es válido
                if (producto == null)
                {
                    return new ResultModel<bool>
                    {
                        isSuccess = false,
                        ErrorMessage = "El producto no puede ser nulo."
                    };
                }

                await _repository.UpdateAsync(producto); // Llamamos al repositorio para actualizar el producto

                return new ResultModel<bool>
                {
                    isSuccess = true,
                    Data = true // Producto actualizado correctamente
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<bool>
                {
                    isSuccess = false,
                    ErrorMessage = ex.Message // Si ocurre un error, lo capturamos
                };
            }
        }

        // Método para eliminar un producto
        public async Task<ResultModel<bool>> DeleteAsync(int? id)
        {
            if (id == null || id <= 0)
            {
                return new ResultModel<bool>
                {
                    isSuccess = false,
                    ErrorMessage = "ID de producto inválido."
                };
            }

            var entity = await _repository.GetByIdAsync(id); // Buscamos el producto por ID

            if (entity == null)
            {
                return new ResultModel<bool>
                {
                    isSuccess = false,
                    ErrorMessage = "Producto no encontrado."
                };
            }

            await _repository.DeleteAsync(entity); // Si el producto existe, lo eliminamos

            return new ResultModel<bool>
            {
                isSuccess = true,
                Data = true // Producto eliminado correctamente
            };
        }
    }
}
