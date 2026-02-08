using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.Models;
using Prueba_Completa_NET.DTOs;
using Prueba_Completa_NET.Services;

namespace Prueba_Completa_NET.Repositories
{
    public class OrdenRepository
    {
        private readonly AppDbContext _context;
        private readonly ProductoService _productoService;

        public OrdenRepository( AppDbContext context, ProductoService productoService)
        {
            _context = context;
            _productoService = productoService;
        }

        public async Task<OrdenDTO> CrearOrden(OrdenCreateDTO ordenCreateDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var orden = new Orden
                {
                    ClienteId = ordenCreateDTO.ClienteId,
                    FechaCreacion = DateTime.UtcNow
                };
                _context.Ordenes.Add(orden);
                await _context.SaveChangesAsync();

                var detallesDTO = new List<DetalleOrdenDTO>();
                decimal subtotalOrden = 0;
                decimal impuestoOrden = 0;

                foreach (var detalle in ordenCreateDTO.Detalle)
                {
                    var producto = await _productoService.ObtenerProductoPorIdAsync(detalle.ProductoId);
                    if (producto == null)
                        throw new Exception($"Producto con ID {detalle.ProductoId} no encontrado.");

                    // Validar existencia y restar stock
                    await _productoService.ActualizarExistenciaAsync(producto, detalle.Cantidad);

                    // Calcular subtotal, impuesto y total del detalle
                    var subtotal = producto.Precio * detalle.Cantidad;
                    var impuesto = subtotal * 0.15m; // ISV 15%
                    var totalDetalle = subtotal + impuesto;

                    // Crear detalle de orden
                    var detalleOrden = new DetalleOrden
                    {
                        OrdenId = orden.OrdenId,
                        ProductoId = detalle.ProductoId,
                        Cantidad = detalle.Cantidad,
                        Subtotal = subtotal,
                        Impuesto = impuesto,
                        Total = totalDetalle
                    };
                    _context.DetallesOrden.Add(detalleOrden);
                    await _context.SaveChangesAsync();

                    // Acumular para la orden
                    subtotalOrden += subtotal;
                    impuestoOrden += impuesto;

                    detallesDTO.Add(new DetalleOrdenDTO
                    {
                        DetalleOrdenId = detalleOrden.DetalleOrdenId,
                        OrdenId = orden.OrdenId,
                        ProductoId = detalle.ProductoId,
                        Cantidad = detalle.Cantidad,
                        Subtotal = subtotal,
                        Impuesto = impuesto,
                        Total = totalDetalle
                    });
                }


                // Actualizar totales de la orden
                orden.Subtotal = subtotalOrden;
                orden.Impuesto = impuestoOrden;
                orden.Total = subtotalOrden + impuestoOrden;
                _context.Ordenes.Update(orden);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new OrdenDTO
                {
                    OrdenId = orden.OrdenId,
                    ClienteId = orden.ClienteId,
                    FechaCreacion = orden.FechaCreacion,
                    Subtotal = orden.Subtotal,
                    Impuesto = orden.Impuesto,
                    Total = orden.Total,
                    Detalles = detallesDTO
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}
