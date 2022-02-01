using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Aplication;
using TiendaServicios.Api.Carrito.Aplication.Dtos;

namespace TiendaServicios.Api.Carrito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarritoComprasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(CreateCarritoCommand.Execute data)
        {
            return await mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id) 
        {
            return await mediator.Send(new GetCarritoCompraQuery.Execute { CarritoSesionId = id }); 
        }

    }
}
