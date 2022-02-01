using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Models;

namespace TiendaServicios.Api.Autor.Persistence
{
    public class AutorContext : DbContext
    {
        public AutorContext(DbContextOptions<AutorContext> options): base(options)
        {
        }

        public DbSet<AutorLibro> Autor { get; set; }
        public DbSet<GradoAcademico> GradosAcademico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
