using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Laboratorio_graphql.domain.contracts;
using Laboratorio_graphql.domain.MutationsDomain;
using Laboratorio_graphql.domain.QuerysDomain;
using Laboratorio_graphql.domain.services;
using Laboratorio_graphql.infraestructure.Entities.Database;
using LaboratorioGraphql.API.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioGraphql.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string ambiente = Configuration.GetValue<string>("enviroment");
            string connectionString = Configuration.GetConnectionString(ambiente);

            services.AddDbContext<Escuela_Laboratorio_GraphqlContext>(options => {
                options.UseNpgsql(connectionString);
            }, ServiceLifetime.Transient);

            services.AddTransient<IMateriaAlumnoRepository, MateriaAlumnoRepository>();
            services.AddTransient<IQueryMateriaAlumno, QueryMateriaAlumno>();
            services.AddTransient<IAlumnoRepository, AlumnoRepository>();
            services.AddTransient<IMutationAlumno, MutationCrearAlumno>();

            services.AddGraphQL(provider => SchemaBuilder
               .New()
               .AddServices(provider)
               .AddMutationType<MutationsAPI>()
               .AddQueryType<QuerysAPI>()
               .Create());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/api/Laboratorio",
                    Path = "/playground"
                });
            }

            app.UseGraphQL("/api/Laboratorio");
        }
    }
}
