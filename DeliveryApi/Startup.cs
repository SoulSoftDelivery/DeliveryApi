using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interface;
using DeliveryApi.Repositories.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace DeliveryApi
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

            //Teste 1
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //services.AddDbContext<WebAppDbContext>(
            //    options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            var connection = Configuration.GetConnectionString(nameof(WebAppDbContext));
            services.AddDbContext<WebAppDbContext>(
                options => options.UseSqlServer(connection));

            services.AddTransient<ITipoUsuarioRepository, TipoUsuarioRepository>();
            services.AddTransient<ITipoEnderecoRepository, TipoEnderecoRepository>();
            services.AddTransient<ITipoMedidaRepository, TipoMedidaRepository>();
            services.AddTransient<ITipoPedidoRepository, TipoPedidoRepository>();
            services.AddTransient<ISituacaoPedidoRepository, SituacaoPedidoRepository>();
            services.AddTransient<ICategoriaProdutoRepository, CategoriaProdutoRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IErroRepository, ErroRepository>();

            //services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "DeliveryApi",
                    Description = "Documentação de Api's SoulSoft Delivery",
                    Contact = new OpenApiContact
                    {
                        Name = "Felipe Dutra",
                        Email = "servidor.soulsoft@gmail.com",
                        Url = new Uri("https://www.google.com.br/"),
                    },
                    Version = "v1" 
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Header de autorização JWT usando o esquema Bearer.\r\n\r\nInforme 'Bearer'[espaço] e o seu token.\r\n\r\nExamplo: \'Bearer 12345abcdef\'",
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
                        new string[] {}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeliveryApi v1"));
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "Uploads/Produtos")),
                RequestPath = "/uploads/produtos"
            });

            app.UseCors(c =>
                c.AllowAnyMethod()
                .AllowAnyHeader().AllowAnyOrigin());

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
