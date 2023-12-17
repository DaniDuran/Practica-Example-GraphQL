using GraphQLUtilitiesMicroServices;
using Laboratorio_graphql.domain.contracts;
using Laboratorio_graphql.domain.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_graphql.domain.MutationsDomain
{
    public class MutationCrearAlumno : IMutationAlumno
    {

        private readonly IAlumnoRepository _alumnoRepository;

        public MutationCrearAlumno(IAlumnoRepository alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }
        public ResultModel<CrearAlumnoArgs> CrearAlumno(CrearAlumnoArgs crearAlumnoArgs)
        {
            ResultModel<CrearAlumnoArgs> result = new ResultModel<CrearAlumnoArgs>();
            try
            {
                var crearAlumno = _alumnoRepository.AddAlumno(crearAlumnoArgs);

                if (!crearAlumno.Sucess)
                {
                    result = new ResultModel<CrearAlumnoArgs>(new Exception(crearAlumno.ErrorMessage));                    
                }
                else
                {
                    crearAlumnoArgs.id =crearAlumno.Id;                    
                }
            }
            catch (Exception ex)
            {

                result = new ResultModel<CrearAlumnoArgs>(ex);                
            }

            result.custom2 = crearAlumnoArgs;
            return result;
        }
    }
}
