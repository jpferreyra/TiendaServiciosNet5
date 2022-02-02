using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Models;

namespace TiendaServicios.Api.Libro.Persistence
{
    public class LibroContext : DbContext
    {
        public LibroContext()
        {

        }

        public LibroContext(DbContextOptions<LibroContext> options) : base(options)
        {
        }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
