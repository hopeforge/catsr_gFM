using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using cpj.DAO.Context;
using cpj.DAO.Repositories;
using cpj.DAO.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace cpj
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
            services.AddMvc();

            //Registro de serviços IoC
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IContaRepository, ContaRepository>();
            services.AddTransient<IPortalRepository, PortalRepository>();
            services.AddTransient<IPeticaoRepository, PeticaoRepository>();
            services.AddTransient<IProcessoRepository, ProcessoRepository>();
            services.AddTransient<IMovimentacaoRepository, MovimentacaoRepository>();
            var connection = Configuration["ConexaoMySql:MySqlConnectionString"];
            services.AddDbContext<CPJContext>(options =>
                options.UseMySql(connection)
            );

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CPJ", Version = "v1" });
                c.IncludeXmlComments(xmlPath);
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "API",
            //        Description = "Test API with ASP.NET Core 3.0",
            //        TermsOfService = "None",
            //        Contact = new Contact()
            //        {
            //            Name = "Dotnet Detail",
            //            Email = "dotnetdetail@gmail.com",
            //            Url = "www.dotnetdetail.net"
            //        },
            //        License = new License
            //        {
            //            Name = "ABC",
            //            Url = "www.dotnetdetail.net"
            //        },
            //    });
            //    c.IncludeXmlComments(xmlPath);

            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CPJ API V1");
            });
        }
    }
}
