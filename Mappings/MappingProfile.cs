namespace Prueba_Completa_NET.Mappings
{
    using AutoMapper;
    using Prueba_Completa_NET.Models;
    using Prueba_Completa_NET.DTOs;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteCreateDTO, Cliente>();
            CreateMap<ClienteUpdateDTO, Cliente>();
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoCreateDTO, Producto>();
            CreateMap<ProductoUpdateDTO, Producto>();
            CreateMap<DetalleOrden, DetalleOrdenDTO>();
            CreateMap<DetalleCreateDTO, DetalleOrden>();
            CreateMap<Orden, OrdenDTO>();
            CreateMap<OrdenCreateDTO, Orden>();

        }

    }
}
