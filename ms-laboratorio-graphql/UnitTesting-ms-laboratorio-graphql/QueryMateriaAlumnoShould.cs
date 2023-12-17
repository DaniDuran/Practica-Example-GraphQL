using Laboratorio_graphql.domain.contracts;
using Laboratorio_graphql.domain.dto;
using Laboratorio_graphql.domain.QuerysDomain;
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
    public class QueryMateriaAlumnoShould
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public QueryMateriaAlumnoShould(ITestOutputHelper testOutputHelper)
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

        private QueryMateriaAlumno BuildQueryMateriaAlumno()
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
                services.AddTransient<IQueryMateriaAlumno,QueryMateriaAlumno>();
            })
            .Build();
            QueryMateriaAlumno repository = ActivatorUtilities.CreateInstance<QueryMateriaAlumno>(host.Services);
            return repository;
        }


        [Fact]
        public void ValidateGetListMateriaAlumnoModelDomain()
        {
            //Arrange
            QueryMateriaAlumno repository = BuildQueryMateriaAlumno();
            //Act
            var resultGetListMateriaAlumno = repository.GetListMateriaAlumnoModelDomain(id:2);
            //Assert
            if (resultGetListMateriaAlumno.custom is IQueryable<PartialMateriaAlumno>)
            {
                Assert.True(resultGetListMateriaAlumno.state == "200");
            }
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resultGetListMateriaAlumno));
        }
    }
}
