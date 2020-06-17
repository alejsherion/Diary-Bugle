using ClarinDiary.ClientWS.DI;
using ClarinDiary.DataAccess.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ClarinDiary.ClientWS
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
            services.AddControllers();

            //Set Context
            services.AddDbContext<ClarinDiaryContext>(options => 
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );

            services.AddCors(opt =>
            {
                opt.AddPolicy("Default_CorsPolicy", o =>
                {
                    o.AllowAnyHeader();
                    o.AllowAnyMethod();
                    o.AllowAnyOrigin();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Clarin Diary API",
                    Version = "v1",
                    Description = "Clarin Diary API Documentation",
                    Contact = new OpenApiContact
                    {
                        Name = "Alejandro Vargas",
                        Email = "alejandro5504@hotmail.com"                        
                    }
                });
                ////Agregando comentarios Xml a la documentación
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            DependencyInjectionProfile.RegisterProfile(services);
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

            app.UseStaticFiles();
            app.UseCors("Default_CorsPolicy");

            //Obtenemos el nombre de la aplicación URL
            string appNameURI = Configuration.GetValue<string>("AppNameURI")?.Trim() ?? "/";
            appNameURI = (!appNameURI.StartsWith("/") ? ("/" + appNameURI) : appNameURI);
            appNameURI = (!appNameURI.EndsWith("/") ? (appNameURI + "/") : appNameURI);

            string swaggerEndPoint = $"{appNameURI}swagger/v1/swagger.json";

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.RoutePrefix = string.Empty;
                s.SwaggerEndpoint(swaggerEndPoint, "Clarin Diary API Documentation");
            });
        }
    }
}
