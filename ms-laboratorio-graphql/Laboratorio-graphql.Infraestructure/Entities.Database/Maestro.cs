using System;
using System.Collections.Generic;

#nullable disable

namespace Laboratorio_graphql.infraestructure.Entities.Database
{
    public partial class Maestro
    {
        public int Id { get; set; }
        public long? Cedula { get; set; }
        public string Nombre { get; set; }
    }
}
