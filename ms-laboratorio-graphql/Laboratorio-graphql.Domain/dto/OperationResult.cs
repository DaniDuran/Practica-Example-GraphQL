using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using TP_Movil.Infraestructure.Entities.NegocioSQL;

namespace Laboratorio_graphql.domain.dto
{
    public class OperationResult
    {

        public string codRet { get; set; }

        public bool Sucess { get; set; }

        [JsonPropertyName("msg")]
        public string ErrorMessage { get; set; }

    }

}
