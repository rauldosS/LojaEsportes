using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace LojaEsportes.Infrastructure
{
    public static class SessionExtensions
    {
        // Método para armazenar conteúdo do carrinho de compras
        // em formato string json na sessão da aplicação
        public static void SetJson(
            this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Método para acessar conteúdo do carrinho
        public static T GetJson<T>(this ISession session, string key) {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T)
                    : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
