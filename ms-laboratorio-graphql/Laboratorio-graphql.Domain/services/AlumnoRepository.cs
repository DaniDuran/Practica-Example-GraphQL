using Example.Domain.toolkit;
using Laboratorio_graphql.domain.contracts;
using Laboratorio_graphql.domain.dto;
using Laboratorio_graphql.infraestructure.Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Laboratorio_graphql.domain.services
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly Escuela_Laboratorio_GraphqlContext _context;

        public AlumnoRepository(Escuela_Laboratorio_GraphqlContext context)
        {
            _context = context;
        }
        public CrearAlumnoResult AddAlumno(CrearAlumnoArgs alumnoArgs)
        {
            try
            {
                
                Alumno alumno = new Alumno();
                alumno.DocumentoIdentidad = alumnoArgs.DocumentoIdentidad;
                alumno.Nombre=alumnoArgs.Nombre;
                _context.Alumnos.Add(alumno);
                _context.SaveChanges();
                return new CrearAlumnoResult {Id= alumno.Id, codRet = "200", Sucess = true, ErrorMessage = "" };
            }
            catch (Exception ex)
            {
                return new CrearAlumnoResult { Id = 0, codRet = "99", Sucess = false, ErrorMessage = Exceptions.BuildMessage(ex) };
            }
        }
    }
}
