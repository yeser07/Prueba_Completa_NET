namespace Prueba_Completa_NET.Services
{
    using Prueba_Completa_NET.Interfaces;
    using Prueba_Completa_NET.DTOs;
    using AutoMapper;
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IMapper _mapper;
        public OrdenService(IOrdenRepository ordenRepository, IMapper mapper)
        {
            _ordenRepository = ordenRepository;
            _mapper = mapper;
        }
        public async Task<OrdenDTO> CrearOrden(OrdenCreateDTO orden)
        {
            var ordenNueva = await _ordenRepository.CrearOrden(orden);

            return _mapper.Map<OrdenDTO>(ordenNueva);
        }
    }
}
