using DeliveryApi.Models;
using System.Text;
using System.Net.Http;

namespace DeliveryApi.Services
{
    public class ErroService
    {
        public static void NotifyError(ErroModel erro, string domain)
        {
            var httpClient = new HttpClient();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(erro);
            var bodyContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.PostAsync(domain + "/api/Erro/Notify", bodyContent);
        }

        //public static Response ReturnError()
        //{
        //    Response response = new Response
        //    {
        //        ok = false,
        //        msg = "Não foi possível concluir a solicitação."
        //    };

        //    return response;
        //}
    }
}
