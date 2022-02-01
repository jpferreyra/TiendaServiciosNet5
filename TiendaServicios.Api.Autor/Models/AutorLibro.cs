using System;
using System.Collections.Generic;

namespace TiendaServicios.Api.Autor.Models
{
    public class AutorLibro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public ICollection<GradoAcademico> GradosAcademicos { get; set; }
        public string AutorGuid { get; set; }
    }
}
