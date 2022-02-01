using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Carrito.Models;

namespace TiendaServicios.Api.Carrito.Persistence
{
    public class CarritoContext : DbContext
    {
        public CarritoContext(DbContextOptions<CarritoContext> options): base(options)
        {
        }

        public DbSet<CarritoSesion> CarritoSesion { get;set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
