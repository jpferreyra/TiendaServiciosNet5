using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Models;
using TiendaServicios.Api.Libro.Persistence;

namespace TiendaServicios.Api.Libro.Aplication
{
    public class CreateLibroCommand
    {
        public class Execute : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly LibroContext libroContext;

            public Handler(LibroContext libroContext)
            {
                this.libroContext = libroContext;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var libroEntity = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro,
                };

                libroContext.Add(libroEntity);
                var returnValue = await this.libroContext.SaveChangesAsync();
                if (returnValue > 0)
                {
                    return Unit.Value;
                };

                throw new Exception("No se pudo insertar el libro");
            }
        }
    }
}
