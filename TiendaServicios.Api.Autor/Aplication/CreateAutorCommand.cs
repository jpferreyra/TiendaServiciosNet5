using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Models;
using TiendaServicios.Api.Autor.Persistence;

namespace TiendaServicios.Api.Autor.Aplication
{
    public class CreateAutorCommand
    {
        //recibe paramatros del controller
        public class AutorRequest : IRequest
        { 
            public string Nombre { get; set; }  
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class AutorRequestValidation : AbstractValidator<AutorRequest>
        {
            public AutorRequestValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();    
            }
        }

        //logica de insert
        //se crea la instancia de EF
        public class CreateAutorCommandHandler : IRequestHandler<AutorRequest>
        {
            public readonly AutorContext _context;

            public CreateAutorCommandHandler(AutorContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(AutorRequest request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Apellido = request.Apellido,
                    Nombre = request.Nombre,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorGuid = Convert.ToString(Guid.NewGuid())
                };

                _context.Autor.Add(autorLibro);
                var returnValue = await _context.SaveChangesAsync();                
                if (returnValue > 0) {
                    return Unit.Value;
                };

                throw new Exception("No se pudo insertar el autor");
            }
        }
    }
}
