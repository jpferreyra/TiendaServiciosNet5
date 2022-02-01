using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplication.Dtos;
using TiendaServicios.Api.Libro.Persistence;

namespace TiendaServicios.Api.Libro.Aplication
{
    public class GetLibroByIdQuery
    {
        public class Execute : IRequest<LibroMaterialDto>{
            public Guid? LibroId { get; set; }
        }

        public class Handler : IRequestHandler<Execute, LibroMaterialDto>
        {
            private readonly IMapper mapper;
            private readonly LibroContext libroContext;

            public Handler(IMapper mapper, LibroContext libroContext)
            {
                this.mapper = mapper;
                this.libroContext = libroContext;
            }
            public async Task<LibroMaterialDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var libroEntity = await libroContext.LibreriaMaterial.FirstOrDefaultAsync(x => x.LibreriaMaterialId == request.LibroId);

                if (libroEntity == null)
                {
                    throw new Exception("No se encontro el libro");
                }

                return mapper.Map<LibroMaterialDto>(libroEntity);
            }
        }

    }
}
