using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LojaEsportes.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app) {
            // Objeto gerenciador do usuário baseado no registro do serviço do Identity
            UserManager<IdentityUser> userManager =
                app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();

            // Busca o usuário espefício de acordo com a sua id de login
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if(user == null) {
                // Instanciando um novo usuário baseado no login informado
                user = new IdentityUser(adminUser);
                // Criando/Regisrtando o novo usuário com sua senha
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
