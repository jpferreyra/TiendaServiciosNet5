using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Aplication.Dtos;
using TiendaServicios.Api.Carrito.Persistence;
using TiendaServicios.Api.Carrito.RemoteInterface;

namespace TiendaServicios.Api.Carrito.Aplication
{
    public class GetCarritoCompraQuery
    {
        public class Execute : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Handler : IRequestHandler<Execute, CarritoDto>
        {
            private readonly CarritoContext context;
            private readonly ILibroService libroService;

            public Handler(CarritoContext carritoContext, ILibroService libroService)
            {
                this.context = carritoContext;
                this.libroService = libroService;
            }

            public async Task<CarritoDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var carritoSesion = await context.CarritoSesion
                        .FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await context.CarritoSesionDetalle
                        .Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDto>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await libroService.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.result)
                    {
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = response.libroRemote.Titulo,
                            FechaPublicacion = response.libroRemote.FechaPublicacion,
                            LibroId = (Guid)response.libroRemote.LibreriaMaterialId
                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    CarritoDetalle = listaCarritoDto
                };

                return carritoSesionDto;
            }
        }
    }
}
