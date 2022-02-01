using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Persistence;
using TiendaServicios.Api.Carrito.Models;

namespace TiendaServicios.Api.Carrito.Aplication
{
    public class CreateCarritoCommand
    {
        public class Execute : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ItemCarrito { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly CarritoContext carritoContext;

            public Handler(CarritoContext carritoContext)
            {
                this.carritoContext = carritoContext;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                this.carritoContext.CarritoSesion.Add(carritoSesion);
                var value = await this.carritoContext.SaveChangesAsync();

                if (value == 0)
                {
                    throw new Exception("Errores en la insercion del carrito de compras");
                }

                var newId = carritoSesion.CarritoSesionId;

                foreach (var item in request.ItemCarrito)
                {
                    this.carritoContext.CarritoSesionDetalle.Add(new CarritoSesionDetalle
                    {
                        CarritoSesionId = newId,
                        FechaCreacion = DateTime.Now,
                        ProductoSeleccionado = item
                    });
                }

                value = await carritoContext.SaveChangesAsync();
                if (value == 0)
                {
                    throw new Exception("No se pudo insertar el detalle del carrito de Compras");
                }

                return Unit.Value;
            }
        }
    }
}
