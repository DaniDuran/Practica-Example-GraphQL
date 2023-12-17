using GraphQLUtilitiesMicroServices;
using Laboratorio_graphql.domain.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.QuerysDomain
{
    public interface IQueryMateriaAlumno
    {
        // ResultModel<DatosSolicitudes> GetDatosSolictudesModelDomain(ArgsBusquedaDatosSolicitudes args);
        ResultModel<PartialMateriaAlumno> GetListMateriaAlumnoModelDomain(int id = 0, int page = 1, int items = 0);

        ResultModel<PartialMateriaAlumno> GetListMateriaAlumnoModelDomainByArgs(MateriaAlumnoArsConsulta materiaAlumnoArsConsulta, int page = 1, int items = 0);
    }
}
