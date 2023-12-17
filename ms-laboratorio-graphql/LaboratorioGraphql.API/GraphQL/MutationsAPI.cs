using GraphQLUtilitiesMicroServices;
using Laboratorio_graphql.domain.dto;
using Laboratorio_graphql.domain.MutationsDomain;

namespace LaboratorioGraphql.API.GraphQL
{
    public class MutationsAPI
    {
        private readonly IMutationAlumno _mutationAlumno;

        public MutationsAPI(IMutationAlumno mutationAlumno)
        {
            _mutationAlumno = mutationAlumno;
        }

        public ResultModel<CrearAlumnoArgs> CrearAlumno(CrearAlumnoArgs crearAlumnoArgs)
        {
            ResultModel<CrearAlumnoArgs> result;
            return result = _mutationAlumno.CrearAlumno(crearAlumnoArgs);
        }
    }
}
