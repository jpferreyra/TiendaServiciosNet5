using System;
using System.Collections.Generic;

namespace TiendaServicios.Api.Carrito.Models
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public List<CarritoSesionDetalle> listaDetalle { get; set; }
    }
}
