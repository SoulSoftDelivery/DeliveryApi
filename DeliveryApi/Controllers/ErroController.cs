using Microsoft.AspNetCore.Mvc;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interface;
using DeliveryApi.Services;
using System;

namespace DeliveryApi.Controllers
{
    [ApiController]
    public class ErroController : Controller
    {
        IErroRepository erroRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public ErroController(IErroRepository ErroRepository)
        {
            erroRepository = ErroRepository;
        }

        [Route("/api/[controller]/Notify")]
        [HttpPost]
        public Response Notify(ErroModel erro)
        {
            try
            {
                erro.DtCadastro = DateTime.Now;
                erro.DtAtualizacao = DateTime.Now;
                erro.Situacao = 'A';

                var id = erroRepository.Notify(erro);

                if (id == 0)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }
    }
}
