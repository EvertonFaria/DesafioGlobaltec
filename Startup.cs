using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using DesafioGlobaltec.Domain.Data;
using DesafioGlobaltec.Domain.Models;
using DesafioGlobaltec.Security;
using DesafioGlobaltec.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DesafioGlobaltec {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            // Configurando o acesso a dados de produtos
            services.AddDbContext<CatalogoDbContext>(
                options => options.UseInMemoryDatabase("InMemoryDatabase")
            );
            
            services.AddScoped<SPessoa>();

            // Configurando o uso da classe de contexto para
            // acesso às tabelas do ASP.NET Identity Core
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase("InMemoryDatabase")
            );

            // Ativando a utilização do ASP.NET Identity, a fim de
            // permitir a recuperação de seus objetos via injeção de
            // dependências
            services.AddIdentity<ApplicationUser, IdentityRole>(
                //
            ).AddEntityFrameworkStores<ApplicationDbContext>(
                //
            ).AddDefaultTokenProviders();

            // Configurando a dependência para a classe de validação
            // de credenciais e geração de tokens
            services.AddScoped<AccessManager>();

            var signingConfigurations = new SigningConfigurations();
            
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations")
            ).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            // Aciona a extensão que irá configurar o uso de
            // autenticação e autorização via tokens
            services.AddJwtSecurity(
                signingConfigurations, 
                tokenConfigurations
            );

            services.AddCors();
            services.AddMvc(
                x => x.EnableEndpointRouting = false
            ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers();
            services.AddSwaggerGen(
                c => {
                    c.SwaggerDoc(
                        "v1", 
                        new OpenApiInfo { 
                            Title = "DesafioGlobaltec", 
                            Version = "v1" 
                        }
                    );
                }
            );
        }

        public void Configure(
		    IApplicationBuilder app, 
			IHostEnvironment env,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        ) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json", 
                        "DesafioGlobaltec v1"
                    )
                );
            } else {
                app.UseHsts();
            }

            // Criação de estruturas, usuários e permissões
            // na base do ASP.NET Identity Core (caso ainda não existam)
            new IdentityInitializer(
                context, 
                userManager, 
                roleManager
            ).Initialize();

            app.UseHttpsRedirection();

            app.UseMvc();

            app.UseRouting();

            app.UseAuthorization();
        }
    }
}
