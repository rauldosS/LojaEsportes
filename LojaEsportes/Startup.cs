using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LojaEsportes.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LojaEsportes
{
    public class Startup
    {

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Registra o serviço de conexão ao banco de dados da aplicação
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:LojaEsportesProducts:ConnectionString"]));

            // Registra o serviço de conexão ao banco de dados do identity
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:LojaEsportesIdentity:ConnectionString"]));

            // Registrando os serviços de acesso aos objetos do identity, tanto de usuário quanto de classes (função, papel)
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>().
                AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();            
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            
            // Associa as requisições de sessão do carrinho ao objmeto SessionCart
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            // Garante que as requisições serão sempre respondidas com o mesmo objeto
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            // Ativa o sistema de autenticação de usuário na aplicação
            app.UseAuthentication();
            app.UseMvc(routes => {

                routes.MapRoute(
                        name: null,
                        template: "{category}/Page{productPage}",
                        defaults:  new { controller = "Product", action = "List" }
                    );

                routes.MapRoute(
                        name: null,
                        template: "Page{productPage:int}",
                        defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );

                routes.MapRoute(
                        name: null,
                        template: "{category}",
                        defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );


                routes.MapRoute(
                        name: null,
                        template: "",
                        defaults: new {controller = "Product", action = "List", productPage = 1}
                    );


                routes.MapRoute(
                        name: null,
                        template: "{controller=Product}/{action}/{id?}"
                    );
            });

            SeedData.EnsurePopulated(app);

            // Criar dados do usuário para autenticação
            IdentitySeedData.EnsurePopulated(app);

            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            */            
        }
    }
}
