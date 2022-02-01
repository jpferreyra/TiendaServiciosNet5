using System;
using System.Collections.Generic;

namespace TiendaServicios.Api.Carrito.Aplication.Dtos
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDto> CarritoDetalle { get; set; }

    }
}
