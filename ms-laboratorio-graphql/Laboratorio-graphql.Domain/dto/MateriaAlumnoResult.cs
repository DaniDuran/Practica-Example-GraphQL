using Laboratorio_graphql.infraestructure.Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.dto
{
    public class MateriaAlumnoResult : OperationResult
    {
        public List<MateriaAlumno> MateriaAlumno { get; set; }
    }
}
