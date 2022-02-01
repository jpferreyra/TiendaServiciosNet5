using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Aplication;
using TiendaServicios.Api.Libro.Aplication.Dtos;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator mediator;

        public LibroController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(CreateLibroCommand.Execute data) 
        {
            return await this.mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDto>>> GetAll()
        {
            return await this.mediator.Send(new GetAllLibroQuery.Execute());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDto>> GetById(Guid id)
        {
            return await mediator.Send(new GetLibroByIdQuery.Execute { LibroId = id });
        }
    }
}
