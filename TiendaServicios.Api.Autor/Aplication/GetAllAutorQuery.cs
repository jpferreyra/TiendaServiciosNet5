using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplication.Dtos;
using TiendaServicios.Api.Autor.Models;
using TiendaServicios.Api.Autor.Persistence;

namespace TiendaServicios.Api.Autor.Aplication
{
    public class GetAllAutorQuery
    {
        //IRequest recibe paramatros del controller
        //IRequest se utiliza para devolver datos al cliente
        public class GetAllAutoresQuery : IRequest<List<AutorLibroDto>>
        {
        }

        public class GetAllAutoresQueryCommandHandler : IRequestHandler<GetAllAutoresQuery, List<AutorLibroDto>>
        {
            public readonly AutorContext _context;
            private readonly IMapper _mapper;

            public GetAllAutoresQueryCommandHandler(AutorContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<List<AutorLibroDto>> Handle(GetAllAutoresQuery request, CancellationToken cancellationToken)
            {
                var autores = await this._context.Autor.ToListAsync();
                return _mapper.Map<List<AutorLibro>,List<AutorLibroDto>>(autores);
            }
        }
    }
}
