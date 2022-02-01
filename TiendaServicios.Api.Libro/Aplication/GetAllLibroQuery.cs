using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplication.Dtos;
using TiendaServicios.Api.Libro.Persistence;

namespace TiendaServicios.Api.Libro.Aplication
{
    public class GetAllLibroQuery
    {
        public class Execute : IRequest<List<LibroMaterialDto>> { }

        public class Handler : IRequestHandler<Execute, List<LibroMaterialDto>>
        {
            private readonly LibroContext libroContext;
            private readonly IMapper mapper;

            public Handler(LibroContext libroContext, IMapper mapper)
            {
                this.libroContext = libroContext;
                this.mapper = mapper;
            }
            public async Task<List<LibroMaterialDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var entities = await libroContext.LibreriaMaterial.ToListAsync();

                return mapper.Map<List<LibroMaterialDto>>(entities);    
            }
        }
    }
}
