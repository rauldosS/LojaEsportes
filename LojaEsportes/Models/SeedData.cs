using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace LojaEsportes.Models
{
    public class SeedData
    {
        public static void EnsurePopulated( IApplicationBuilder app ) {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any()) {
                context.Products.AddRange(
                    new Product {
                        Name = "Jurupinga",
                        Description = "Cachaça de Vinho",
                        Category = "Bebidas",
                        Price = 20
                    },
                    new Product
                    {
                        Name = "Notebook",
                        Description = "Computador",
                        Category = "Eletronico",
                        Price = 3000
                    },
                    new Product
                    {
                        Name = "Bola",
                        Description = "Bola de futebol",
                        Category = "Esportes",
                        Price = 20
                    },
                    new Product
                    {
                        Name = "Arroz",
                        Description = "Comida de japonês",
                        Category = "Alimento",
                        Price = 15
                    },
                    new Product
                    {
                        Name = "Coca-Cola",
                        Description = "Bebida de burguês",
                        Category = "Bebidas",
                        Price = 5
                    },
                    new Product
                    {
                        Name = "Jaqueta",
                        Description = "Roupa de frio",
                        Category = "Vestuário",
                        Price = 250
                    },
                    new Product
                    {
                        Name = "Pneu",
                        Description = "Pneu de carro",
                        Category = "Automóveis",
                        Price = 150
                    },
                    new Product
                    {
                        Name = "Relógio",
                        Description = "Relógio marca djabo",
                        Category = "Acessórios",
                        Price = 75
                    },
                    new Product
                    {
                        Name = "Adaptador de tomada",
                        Description = "Padrão brasil para japonês",
                        Category = "Eletrônico",
                        Price = 5
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
