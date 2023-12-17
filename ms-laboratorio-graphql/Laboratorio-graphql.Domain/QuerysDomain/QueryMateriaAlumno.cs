using GraphQLUtilitiesMicroServices;
using Laboratorio_graphql.domain.contracts;
using Laboratorio_graphql.domain.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.QuerysDomain
{
    public class QueryMateriaAlumno : IQueryMateriaAlumno
    {
        
        private readonly IMateriaAlumnoRepository _materiaAlumnoRepository;

        public QueryMateriaAlumno(IMateriaAlumnoRepository materiaAlumnoRepository)
        {
            _materiaAlumnoRepository = materiaAlumnoRepository;
        }

        public ResultModel<PartialMateriaAlumno> GetListMateriaAlumnoModelDomain(int id = 0, int page = 1, int items = 0)
        {
            ResultModel<PartialMateriaAlumno> result = new ResultModel<PartialMateriaAlumno>();

            try
            {
                if (id < 0)
                {
                    throw new Exception("El identificador debe ser positivo");
                }

                GraphQLUtilitiesMicroServices.PageInfo pageInfo = null;
                result.custom = _materiaAlumnoRepository.listMateriaAlumnosThomas(id)
                .OrderBy(x => x.DocumentoIdentidad)
                .Pagination(page, items, ref pageInfo);
                result.PagesInfo = pageInfo;
            }
            catch (Exception ex)
            {
                result = new ResultModel<PartialMateriaAlumno>(ex);
            }
            return result;
        }

        public ResultModel<PartialMateriaAlumno> GetListMateriaAlumnoModelDomainByArgs(MateriaAlumnoArsConsulta materiaAlumnoArsConsulta, int page = 1, int items = 0)
        {
            ResultModel<PartialMateriaAlumno> result = new ResultModel<PartialMateriaAlumno>();

            try
            {
                GraphQLUtilitiesMicroServices.PageInfo pageInfo = null;
                result.custom = _materiaAlumnoRepository.listMateriaAlumnosFiltrada(materiaAlumnoArsConsulta)
                .OrderBy(x => x.DocumentoIdentidad)
                .Pagination(page, items, ref pageInfo);
                result.PagesInfo = pageInfo;
            }
            catch (Exception ex)
            {
                result = new ResultModel<PartialMateriaAlumno>(ex);
            }
            return result;
        }
    }
}
