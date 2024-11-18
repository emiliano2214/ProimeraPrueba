using SecretNight.Entities.Models;
using SecretNight.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }
        //Peticion de lectura atraves de una lista
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetAll()
        {
            var result = await _productoService.GetAllAsync();
            if (result.isSuccess)
                return Ok(result.Data);
            else
                return NoContent();
        }
        //Peticion de lectura atraves de un ID(muestra uno solo)
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var result = await _productoService.GetByIdAsync(id);
            if (result.isSuccess)
                return Ok(result.Data);
            else
                return NotFound(result.ErrorMessage);
        }
        //Peticion Agregar producto
        [HttpPost]
        public async Task<ActionResult<Producto>> Add([FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest("Solicitud inválida");
            }

            var result = await _productoService.AddAsync(producto);

            if (result.isSuccess && producto.IdProductos > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = producto.IdProductos }, producto);
            }
            else
            {
                return BadRequest(result.ErrorMessage ?? "Error al crear el producto");
            }
        }


        //Peticion Actualizar/Modificar producto
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Producto producto)
        {
            if (id != producto.IdProductos)
                return BadRequest("ID del producto no coincide con el ID de la URL.");

            var result = await _productoService.UpdateAsync(producto);
            if (result.isSuccess)
                return NoContent();
            else
                return BadRequest(result.ErrorMessage);
        }
        //Peticion eliminar producto
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _productoService.DeleteAsync(id);
            if (result.isSuccess)
                return NoContent();
            else
                return NotFound(result.ErrorMessage);
        }
    }
}
