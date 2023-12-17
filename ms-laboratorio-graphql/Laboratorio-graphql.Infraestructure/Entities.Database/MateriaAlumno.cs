using System;
using System.Collections.Generic;

#nullable disable

namespace Laboratorio_graphql.infraestructure.Entities.Database
{
    public partial class MateriaAlumno
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int MateriaId { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Alumno Alumno { get; set; }
        public virtual Materium Materia { get; set; }
    }
}
