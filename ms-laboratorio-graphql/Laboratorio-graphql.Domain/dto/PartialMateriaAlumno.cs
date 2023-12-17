using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.dto
{
    public class PartialMateriaAlumno
    {
        //ma."ID", m.nombre materia, a."DocumentoIdentidad" , a."Nombre"
        public string NombreMateria { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string NombreAlumno { get; set; }

    }
}
