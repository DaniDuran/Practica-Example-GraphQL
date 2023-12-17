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
    public class MateriaAlumnoRepository : IMateriaAlumnoRepository
    {
        private readonly Escuela_Laboratorio_GraphqlContext _context;

        public MateriaAlumnoRepository(Escuela_Laboratorio_GraphqlContext context)
        {
            _context = context;
        }

        
        public OperationResult listMateriaAlumnos(int materiaId)
        {
            try
            {
                var query =
                from ma in _context.MateriaAlumnos
                join m in _context.Materia on ma.MateriaId equals m.Id
                join a in _context.Alumnos on ma.AlumnoId equals a.Id
                where m.Id == materiaId
                select 
                new PartialMateriaAlumno
                {
                    NombreMateria =m.Nombre,
                    DocumentoIdentidad= a.DocumentoIdentidad,
                    NombreAlumno= a.Nombre
                };
                return new PartialMateriaAlumnoResult { listaMateriaAlumnos = query.ToList(), codRet = "200", Sucess = true, ErrorMessage = null};             
            }
            catch (Exception ex)
            {
                return new PartialMateriaAlumnoResult { listaMateriaAlumnos = null, codRet = "99", Sucess = false, ErrorMessage = Exceptions.BuildMessage(ex) };
            }
        }

        public IQueryable<PartialMateriaAlumno> listMateriaAlumnosFiltrada(MateriaAlumnoArsConsulta args)
        {

            var query =
            from ma in _context.MateriaAlumnos
            join m in _context.Materia on ma.MateriaId equals m.Id
            join a in _context.Alumnos on ma.AlumnoId equals a.Id
            select
            new
            { m, a };

            if (!string.IsNullOrEmpty(args.DocumentoIdentidad) && args.CodigoMateria.HasValue) {
                throw new Exception("No se permite el ingreso de los valores");
            }

            if (args.CodigoMateria.HasValue)
            {
                query = query.Where(x => x.m.Id == args.CodigoMateria);
            }

            if (!string.IsNullOrEmpty(args.DocumentoIdentidad))
            {
                query = query.Where(x => x.a.DocumentoIdentidad == args.DocumentoIdentidad);
            }

            return query.Select(x => new PartialMateriaAlumno
            {
                NombreMateria = x.m.Nombre,
                DocumentoIdentidad = x.a.DocumentoIdentidad,
                NombreAlumno = x.a.Nombre
            });


        }    

        public IQueryable<PartialMateriaAlumno> listMateriaAlumnosThomas(int materiaId = 0)
        {
            var query = 
                from ma in _context.MateriaAlumnos
                join m in _context.Materia on ma.MateriaId equals m.Id
                join a in _context.Alumnos on ma.AlumnoId equals a.Id                    
                select
                new 
                {m,a};
                
            if (materiaId > 0) { query = query.Where(x => x.m.Id ==materiaId); }

            return query.Select(x => new PartialMateriaAlumno
            {
                NombreMateria = x.m.Nombre,
                DocumentoIdentidad = x.a.DocumentoIdentidad,
                NombreAlumno = x.a.Nombre
            });


        }

        public OperationResult ListMateriasAlumnos()
        {
            try
            {

                return new MateriaAlumnoResult
                {
                    MateriaAlumno = _context.MateriaAlumnos.OrderBy(x => x.Id).ToList(),
                    codRet = null,
                    Sucess = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new MateriaAlumnoResult { MateriaAlumno = null, codRet = "99", Sucess = false, ErrorMessage = Exceptions.BuildMessage(ex) };
            }
        }

        OperationResult IMateriaAlumnoRepository.AddMateriaAlumno(CrearMateriaAlumnoArgs materiaAlumnoArgs)
        {
            try
            {

                MateriaAlumno materiaAlumno = new MateriaAlumno();
                materiaAlumno.AlumnoId = materiaAlumnoArgs.alumnoId;
                materiaAlumno.MateriaId = materiaAlumnoArgs.materiaId;
                materiaAlumno.Fecha = materiaAlumnoArgs.fecha;
                _context.MateriaAlumnos.Add(materiaAlumno);
                _context.SaveChanges();
                return new OperationResult { codRet = "200", Sucess = true, ErrorMessage = "" };
            }
            catch (Exception ex)
            {
                return new OperationResult { codRet = "99", Sucess = false, ErrorMessage = Exceptions.BuildMessage(ex) };
            }
        }
    }
}
