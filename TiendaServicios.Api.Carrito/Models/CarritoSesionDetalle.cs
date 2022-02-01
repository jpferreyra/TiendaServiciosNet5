using System;

namespace TiendaServicios.Api.Carrito.Models
{
    public class CarritoSesionDetalle
    {
        public int CarritoSesionDetalleId { get; set; }
        public int CarritoSesionId { get; set; }
        public CarritoSesion CarritoSesion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ProductoSeleccionado { get; set; }
    }
}
