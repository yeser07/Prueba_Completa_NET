using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.Models;
using Prueba_Completa_NET.DTOs;
using AutoMapper;
using Prueba_Completa_NET.Interfaces.IRepository;

namespace Prueba_Completa_NET.Repositories
{
    public class OrdenRepository: IOrdenRepository
    {
        private readonly AppDbContext _context;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public OrdenRepository( AppDbContext context, IProductoRepository productoRepository,IMapper mapper)
        {
            _context = context;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<Orden> CrearOrden(OrdenCreateDTO ordenCreateDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var orden = _mapper.Map<Orden>(ordenCreateDTO);
                orden.FechaCreacion = DateTime.Now;
                _context.Ordenes.Add(orden);

                await _context.SaveChangesAsync();

                decimal subtotalOrden = 0;
                decimal impuestoOrden = 0;


                foreach (var detalle in ordenCreateDTO.Detalle)
                {
                    var producto = await _productoRepository.ObtenerProductoPorId(detalle.ProductoId);
                    if (producto == null)
                        throw new Exception($"Producto con ID {detalle.ProductoId} no encontrado.");

                    // Validar existencia y restar stock
                    await _productoRepository.ActualizarExistenciaAsync(producto, detalle.Cantidad);

                    // Calcular subtotal, impuesto y total del detalle
                    var subtotal = producto.Precio * detalle.Cantidad;
                    var impuesto = subtotal * 0.15m; // ISV 15%
                    var totalDetalle = subtotal + impuesto;

                    // Crear detalle de orden
                   var detalleOrden = _mapper.Map<DetalleOrden>(detalle);
                    detalleOrden.OrdenId = orden.OrdenId;
                    detalleOrden.Subtotal = subtotal;
                    detalleOrden.Impuesto = impuesto;
                    detalleOrden.Total = totalDetalle;

                    _context.DetallesOrden.Add(detalleOrden);

                    // Acumular para la orden
                    subtotalOrden += subtotal;
                    impuestoOrden += impuesto;
                }


                // Actualizar totales de la orden
                orden.Subtotal = subtotalOrden;
                orden.Impuesto = impuestoOrden;
                orden.Total = subtotalOrden + impuestoOrden;

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return orden;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}
