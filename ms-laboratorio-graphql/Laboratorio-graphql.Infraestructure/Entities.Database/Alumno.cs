using System;
using System.Collections.Generic;

#nullable disable

namespace Laboratorio_graphql.infraestructure.Entities.Database
{
    public partial class Alumno
    {
        public Alumno()
        {
            MateriaAlumnos = new HashSet<MateriaAlumno>();
        }

        public int Id { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<MateriaAlumno> MateriaAlumnos { get; set; }
    }
}
