using Laboratorio_graphql.infraestructure.Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.dto
{
    public class AlumnoResult
    {
        [JsonPropertyName("results")]
        public List<Alumno> Alumno { get; set; }
    }
}
