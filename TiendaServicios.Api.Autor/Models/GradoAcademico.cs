using System;

namespace TiendaServicios.Api.Autor.Models
{
    public class GradoAcademico
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }
        public string CentroAcademico { get; set; }
        public DateTime? FechaGrado { get; set; }
        public int AutorId { get; set; }
        public AutorLibro Autor { get; set; }
        public string GradoAcademicoGuid { get; set; }
    }
}
