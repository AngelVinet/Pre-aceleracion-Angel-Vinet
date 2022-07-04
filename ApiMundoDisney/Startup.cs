using ApiMundoDisney.Models;
using ApiMundoDisney.Repositories;
using ApiMundoDisney.Repositories.Implements;
using ApiMundoDisney.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney
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
            //Servicio agregado necesario para conectar con las bases de datos
            //DefaultConnection = Cambiar por nombre dato a la conexi�n en el archivo appsettings.json
            services.AddDbContext<DisneyDbContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<UserContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("UserConnection")));

            //Servicios necesarios para configurar el registro y ingreso de usuarios al sistema
            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //Cambiar direcci�n usada dependiendo en la que se vaya autilizar
                    //Direcci�n en ValidAudience es la direcci�n del servidor
                    //Direcci�n en ValidIssuer es la direcci�n del usuario
                    ValidAudience = "https://localhost:44340",
                    ValidIssuer = "https://localhost:44340",
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaSuperLargaDeApiMundoDisney"))
                };
            });

            //Servicios de AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //Scoped de repositorios
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            //Servicio para el almacenamiento local de imagenes
            services.AddSingleton<IAlmacenador, Almacenador>();
            services.AddHttpContextAccessor();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiMundoDisney", Version = "v1" });

                //Configuraci�n para permitir autentificarse en Swagger
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Ingrese 'Bearer [token]' para poder autentificarse dentro de la aplicaci�n"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                  
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiMundoDisney v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
