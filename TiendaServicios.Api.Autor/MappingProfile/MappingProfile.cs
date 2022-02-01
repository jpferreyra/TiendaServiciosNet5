using AutoMapper;
using TiendaServicios.Api.Autor.Aplication.Dtos;
using TiendaServicios.Api.Autor.Models;

namespace TiendaServicios.Api.Autor.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorLibroDto>();
        }
    }
}
