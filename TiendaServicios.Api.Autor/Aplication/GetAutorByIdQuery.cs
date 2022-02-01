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
    public class GetAutorByIdQuery
    {
        public class GetAutorQuery : IRequest<AutorLibroDto>
        {
            public string AutorGuid { get; set; }
        }

        public class GetAutorByIdCommandHandler : IRequestHandler<GetAutorQuery, AutorLibroDto>
        {
            public readonly AutorContext _context;
            private readonly IMapper _mapper;

            public GetAutorByIdCommandHandler(AutorContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<AutorLibroDto> Handle(GetAutorQuery request, CancellationToken cancellationToken)
            {
                var autor =  await _context.Autor.FirstOrDefaultAsync(x => x.AutorGuid == request.AutorGuid, cancellationToken: cancellationToken);
                                                
                if (autor == null) 
                {
                    throw new Exception("No se encontro el autor");
                }

                return _mapper.Map<AutorLibro, AutorLibroDto>(autor);              
            }
        }
    }
}
