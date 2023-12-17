using Laboratorio_graphql.domain.dto;
using Laboratorio_graphql.infraestructure.Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.contracts
{
    public interface IMateriaAlumnoRepository
    {
        OperationResult AddMateriaAlumno(CrearMateriaAlumnoArgs MaterialAlumnoArgs);

        OperationResult listMateriaAlumnos(int materiaId);

        OperationResult ListMateriasAlumnos();

        IQueryable<PartialMateriaAlumno> listMateriaAlumnosThomas (int materiaId = 0);

        IQueryable<PartialMateriaAlumno> listMateriaAlumnosFiltrada(MateriaAlumnoArsConsulta args);

    }
}
