using AccesoDatos.Data;
using IngresosPlatformWebAPI.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Modelo;
using Negocio.Repositorios;
using System;
using System.Linq;

namespace IngresosPlatformWebAPI
{
    public class Startup
    {
        private readonly string _MyCors = "MyCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_MyCors, builder =>
                {
                    builder.WithOrigins("https://proingreso.azurewebsites.net", "http://localhost:31496")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddSignalR();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddDbContext<AplicacionDBContext>(options =>
                       options.UseSqlServer(Configuration.GetConnectionString("ProduccionConnection")));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IProveedorRepositorio, ProveedorRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IMailRepositorio, MailRepositorio>();
            services.AddScoped<IIngresoRepositorio, IngresoRepositorio>();
            services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
            services.AddScoped<IIngresoDiarioRepositorio, IngresoDiarioRepositorio>();
            services.AddScoped<ITerciarizacionRepositorio, TerciarizacionRepositorio>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //para traer los datos de configuracion de mail desde el appsettings.json
            services.Configure<ConfigMail>(Configuration.GetSection("MailConnection")); 

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IngresosPlatformWebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IngresosPlatformWebAPI v1"));
            }
            //Creacion de la base de datos
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AplicacionDBContext>();
                context.Database.EnsureCreated(); //para crear una base de datos pero no se pueden usar migraciones
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(_MyCors);
            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificacionesHub>("/notificacioneshub");
            });
        }
    }
}
