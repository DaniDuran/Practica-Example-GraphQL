using GraphQLUtilitiesMicroServices;
using Laboratorio_graphql.domain.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.MutationsDomain
{
    public  interface IMutationAlumno
    {
        ResultModel<CrearAlumnoArgs> CrearAlumno(CrearAlumnoArgs crearAlumnoArgs);
    }
}
