using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.dto
{
    public class CrearAlumnoArgs
    {
        public int? id { get;set; }
        public string DocumentoIdentidad { get; set; }
        public string Nombre { get; set; }
    }
}
