using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.dto
{
    public class CrearMateriaAlumnoArgs
    {
        public int alumnoId { get; set; }
        public int materiaId { get; set; }
        public DateTime fecha { get; set; }
    }
}
