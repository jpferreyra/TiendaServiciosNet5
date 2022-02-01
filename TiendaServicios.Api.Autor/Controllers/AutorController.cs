using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplication;
using TiendaServicios.Api.Autor.Aplication.Dtos;
using TiendaServicios.Api.Autor.Models;

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(CreateAutorCommand.AutorRequest data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorLibroDto>>> GetAutores()
        {
            return await _mediator.Send(new GetAllAutorQuery.GetAllAutoresQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorLibroDto>> GetAutor(string id)
        {
            return await _mediator.Send(new GetAutorByIdQuery.GetAutorQuery { AutorGuid = id });
        }
    }
}
