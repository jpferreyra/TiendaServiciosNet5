using AutoMapper;
using TiendaServicios.Api.Libro.Aplication.Dtos;
using TiendaServicios.Api.Libro.Models;

namespace TiendaServicios.Api.Libro.XUnitTestProject
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
