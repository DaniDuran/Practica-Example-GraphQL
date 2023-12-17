using Laboratorio_graphql.domain.contracts;
using Laboratorio_graphql.domain.dto;
using Laboratorio_graphql.domain.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace UnitTesting_ms_laboratorio_graphql
{
    public class MateriaAlumnoRepositoryShould
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public MateriaAlumnoRepositoryShould(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        public void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables();
        }

        public static void ConfigureDB(IServiceCollection services, string connectionString)
        {
            
            services.AddDbContext< Laboratorio_graphql.infraestructure.Entities.Database.Escuela_Laboratorio_GraphqlContext>(
            options => { options.UseNpgsql(connectionString); }, ServiceLifetime.Transient);
        }


        private MateriaAlumnoRepository BuildMateriaAlumnoRepository()
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
            var config = builder.Build();
            string enviroment = config.GetValue<string>("enviroment");
            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                string connectionString = config.GetConnectionString(enviroment);
                ConfigureDB(services, connectionString);
                services.AddTransient<IMateriaAlumnoRepository, MateriaAlumnoRepository>();
            })
            .Build();
            MateriaAlumnoRepository repository = ActivatorUtilities.CreateInstance<MateriaAlumnoRepository>(host.Services);
            return repository;
        }

        [Fact]
        public void ValidateListMateriaAlumno()
        {
            // Arrange
            CrearMateriaAlumnoArgs listMateriaAlumno = new();
            MateriaAlumnoRepository repository = BuildMateriaAlumnoRepository();

            // Act
            var resultListMateriaAlumno = repository.ListMateriasAlumnos();

            //Assert
            if (resultListMateriaAlumno is MateriaAlumnoResult)
            {
                MateriaAlumnoResult materiaAlumnoResult = (MateriaAlumnoResult)resultListMateriaAlumno;
                Assert.NotNull(materiaAlumnoResult);
            }
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultListMateriaAlumno));

        }
        [Fact]
        public void ValidateListPartialMateriaAlumno()
        {
            // Arrange
            PartialMateriaAlumno listMateriaAlumno = new();
            MateriaAlumnoRepository repository = BuildMateriaAlumnoRepository();
            // Act
            var resultPartialListMateriaAlumno = repository.listMateriaAlumnos(2);
            //Assert
            if (resultPartialListMateriaAlumno is PartialMateriaAlumnoResult)
            {
                PartialMateriaAlumnoResult materiaAlumnosResult = (PartialMateriaAlumnoResult)resultPartialListMateriaAlumno;
                Assert.NotNull(materiaAlumnosResult);
                Assert.True(materiaAlumnosResult.Sucess);
            }
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultPartialListMateriaAlumno));
        }
        [Fact]
        public void ValidatelistMateriaAlumnosThomas()
        {
            // Arrange
            PartialMateriaAlumno listMateriaAlumno = new();
            MateriaAlumnoRepository repository = BuildMateriaAlumnoRepository();
            // Act
            var resultPartialListMateriaAlumnoThomas = repository.listMateriaAlumnosThomas(2);
            //Assert
            if (resultPartialListMateriaAlumnoThomas is PartialMateriaAlumno)
            {
                PartialMateriaAlumno materiaAlumnosResult = (PartialMateriaAlumno)resultPartialListMateriaAlumnoThomas;
                Assert.NotNull(materiaAlumnosResult);

            }
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultPartialListMateriaAlumnoThomas));
        }

        [Fact]
        public void ValidatelistMateriasAlumnosThomas()
        {
            // Arrange
            PartialMateriaAlumno listMateriaAlumno = new();
            MateriaAlumnoRepository repository = BuildMateriaAlumnoRepository();
            // Act
            var resultPartialListMateriaAlumnoThomas = repository.listMateriaAlumnosThomas();
            //Assert
            if (resultPartialListMateriaAlumnoThomas is PartialMateriaAlumno)
            {
                PartialMateriaAlumno materiaAlumnosResult = (PartialMateriaAlumno)resultPartialListMateriaAlumnoThomas;
                Assert.NotNull(materiaAlumnosResult);
            }
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultPartialListMateriaAlumnoThomas));
        }

        [Fact]
        public void ValidatelistMateriasAlumnosFiltradaErrorDosCampos()
        {
            // Arrange
            PartialMateriaAlumno listMateriaAlumno = new();
            MateriaAlumnoRepository repository = BuildMateriaAlumnoRepository();
            MateriaAlumnoArsConsulta args = new MateriaAlumnoArsConsulta();
            args.CodigoMateria = 2;
            args.DocumentoIdentidad = "1013";

            // Act
            Assert.Throws<Exception>(() => repository.listMateriaAlumnosFiltrada(args));
        }

        [Fact]
        public void ValidatelistMateriasAlumnosFiltra_Materia()
        {
            // Arrange
            PartialMateriaAlumno listMateriaAlumno = new();
            MateriaAlumnoRepository repository = BuildMateriaAlumnoRepository();
            MateriaAlumnoArsConsulta args = new MateriaAlumnoArsConsulta();
            args.CodigoMateria = 2;

            var resultValidatelistMateriasAlumnosFiltra_Materia = repository.listMateriaAlumnosFiltrada(args);
            //Assert
            if (resultValidatelistMateriasAlumnosFiltra_Materia is PartialMateriaAlumno)
            {
                PartialMateriaAlumno materiaAlumnosResult = (PartialMateriaAlumno)resultValidatelistMateriasAlumnosFiltra_Materia;
                Assert.NotNull(materiaAlumnosResult);
            }
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultValidatelistMateriasAlumnosFiltra_Materia));
        }

        [Fact]
        public void ValidatelistMateriasAlumnosFiltra_Alumno()
        {
            // Arrange
            PartialMateriaAlumno listMateriaAlumno = new();
            MateriaAlumnoRepository repository = BuildMateriaAlumnoRepository();
            MateriaAlumnoArsConsulta args = new MateriaAlumnoArsConsulta();
            args.DocumentoIdentidad = "1013661746";

            var resultValidatelistMateriasAlumnosFiltra_Materia = repository.listMateriaAlumnosFiltrada(args);
            //Assert
            if (resultValidatelistMateriasAlumnosFiltra_Materia is PartialMateriaAlumno)
            {
                PartialMateriaAlumno materiaAlumnosResult = (PartialMateriaAlumno)resultValidatelistMateriasAlumnosFiltra_Materia;
                Assert.NotNull(materiaAlumnosResult);
            }
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultValidatelistMateriasAlumnosFiltra_Materia));
        }

        [Fact]
        public void ValidatelistMateriasAlumnosFiltro_nulo()
        {
            // Arrange
            PartialMateriaAlumno listMateriaAlumno = new();
            MateriaAlumnoRepository repository = BuildMateriaAlumnoRepository();
            MateriaAlumnoArsConsulta args = new MateriaAlumnoArsConsulta();            

            var resultValidatelistMateriasAlumnosFiltra_Materia = repository.listMateriaAlumnosFiltrada(args);
            //Assert
            if (resultValidatelistMateriasAlumnosFiltra_Materia is PartialMateriaAlumno)
            {
                PartialMateriaAlumno materiaAlumnosResult = (PartialMateriaAlumno)resultValidatelistMateriasAlumnosFiltra_Materia;
                Assert.NotNull(materiaAlumnosResult);
            }
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultValidatelistMateriasAlumnosFiltra_Materia));
        }
    }
}
