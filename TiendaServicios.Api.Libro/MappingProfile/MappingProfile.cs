using AutoMapper;
using TiendaServicios.Api.Libro.Aplication.Dtos;
using TiendaServicios.Api.Libro.Models;

namespace TiendaServicios.Api.Libro.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
