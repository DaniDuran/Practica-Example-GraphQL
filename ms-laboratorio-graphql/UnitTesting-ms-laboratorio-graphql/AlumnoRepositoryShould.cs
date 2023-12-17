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
    public class AlumnoRepositoryShould
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public AlumnoRepositoryShould(ITestOutputHelper testOutputHelper)
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


        private AlumnoRepository BuildAlumnoRepository()
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
                services.AddTransient<IAlumnoRepository, AlumnoRepository>();
            })
            .Build();
            AlumnoRepository repository = ActivatorUtilities.CreateInstance<AlumnoRepository>(host.Services);
            return repository;
        }

        [Fact]
        public void ValidateAddAlumno()
        {
            //Arrange            
            CrearAlumnoArgs newAlumno = new()
            {
                DocumentoIdentidad = "52115233",
                Nombre = "Mary Luz Acevedo"
            };
            AlumnoRepository repository = BuildAlumnoRepository();
            //Act            
            var alumno = repository.AddAlumno(newAlumno);
            //Assert
            Assert.NotNull(alumno);
            Assert.Equal(alumno.Sucess = true, alumno.Sucess);
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(alumno));

        }
    }
}
