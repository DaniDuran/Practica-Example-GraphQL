using GraphQLUtilitiesMicroServices;
using Laboratorio_graphql.domain.dto;
using Laboratorio_graphql.domain.QuerysDomain;

namespace LaboratorioGraphql.API.GraphQL
{
    public class QuerysAPI
    {
        private readonly IQueryMateriaAlumno _queryMateriaAlumno;

        public QuerysAPI(IQueryMateriaAlumno queryMateriaAlumno)
        {
            _queryMateriaAlumno = queryMateriaAlumno;
        }

        public ResultModel<PartialMateriaAlumno> GetListMateriaAlumnoModelDomain(int id = 0, int page = 1, int items = 0)
        {
            ResultModel<PartialMateriaAlumno> result;
            return result = _queryMateriaAlumno.GetListMateriaAlumnoModelDomain(id, page, items);
        }

        public ResultModel<PartialMateriaAlumno> GetListMateriaAlumnoModelDomainByArgs(MateriaAlumnoArsConsulta materiaAlumnoArsConsulta, int page = 1, int items = 0)
        {
            ResultModel<PartialMateriaAlumno> result;
            return result = _queryMateriaAlumno.GetListMateriaAlumnoModelDomainByArgs(materiaAlumnoArsConsulta, page, items);
        }
    }
}
