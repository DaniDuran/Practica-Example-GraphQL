using Laboratorio_graphql.domain.contracts;
using Laboratorio_graphql.domain.dto;
using Laboratorio_graphql.domain.MutationsDomain;
using Laboratorio_graphql.domain.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace UnitTesting_ms_laboratorio_graphql
{
    public class MutationAlumnoShould
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public MutationAlumnoShould(ITestOutputHelper testOutputHelper)
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

            services.AddDbContext<Laboratorio_graphql.infraestructure.Entities.Database.Escuela_Laboratorio_GraphqlContext>(
            options => { options.UseNpgsql(connectionString); }, ServiceLifetime.Transient);
        }

        private MutationCrearAlumno BuildMutationCrearAlumno()
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
                services.AddTransient<IMutationAlumno, MutationCrearAlumno>();
            })
            .Build();
            MutationCrearAlumno repository = ActivatorUtilities.CreateInstance<MutationCrearAlumno>(host.Services);
            return repository;
        }

        [Fact]
        public void ValidateMutationCrearAlumnoModelDomain()
        {
            //Arrange
            MutationCrearAlumno mutation = BuildMutationCrearAlumno();

            CrearAlumnoArgs newAlumno = new()
            {
                DocumentoIdentidad = "1013661755",
                Nombre = "Luz Acevedo"
            };

            //Act
            var resultCrearAlumno = mutation.CrearAlumno(newAlumno);
            //Assert
            Assert.True(resultCrearAlumno.state == "200");
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultCrearAlumno));
        }

        [Fact]
        public void ValidateFailMutationCrearAlumnoModelDomain()
        {
            //Arrange
            MutationCrearAlumno mutation = BuildMutationCrearAlumno();

            CrearAlumnoArgs newAlumno = new()
            {
                DocumentoIdentidad = "1013661745",
                Nombre = "Luz Acevedo"
            };

            //Act
            var resultCrearAlumno = mutation.CrearAlumno(newAlumno);
            //Assert
            Assert.True(resultCrearAlumno.error =="true");
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultCrearAlumno));
        }
    }
}
