using Microsoft.AspNetCore.Mvc;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interface;
using System;
using System.Net;
using DeliveryApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace DeliveryApi.Controllers
{
    [Authorize]
    [ApiController]
    public class TipoPedidoController : Controller
    {
        ITipoPedidoRepository tipoPedidoRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public TipoPedidoController(ITipoPedidoRepository TipoPedidoRepository)
        {
            tipoPedidoRepository = TipoPedidoRepository;
        }

        [Route("/api/[controller]/Create")]
        [HttpPost]
        public Response Create(TipoPedidoModel tipoPedido)
        {
            try
            {
                tipoPedido.DtCadastro = DateTime.Now;
                tipoPedido.DtAtualizacao = DateTime.Now;
                tipoPedido.Situacao = 'A';

                var id = tipoPedidoRepository.Create(tipoPedido);

                if (id == 0)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return response;
                }

                response.conteudo.Add(id);

                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "Create",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(tipoPedido),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Situacao = 'A'
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }

        [Route("/api/[controller]/Update")]
        [HttpPatch]
        public Response Update(TipoPedidoModel tipoPedido)
        {
            try
            {
                var newtipoPedido = tipoPedidoRepository.Get(tipoPedido.Id);

                newtipoPedido.Descricao = tipoPedido.Descricao;
                newtipoPedido.Situacao = tipoPedido.Situacao;
                newtipoPedido.DtAtualizacao = DateTime.Now;

                var result = tipoPedidoRepository.Update(newtipoPedido);

                if (!result)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "Update",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(tipoPedido),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = tipoPedido.Id,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Situacao = 'A'
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }

        [Route("/api/[controller]/Delete/{tipoPedidoId}")]
        [HttpDelete]
        public Response Delete(int tipoPedidoId)
        {
            try
            {
                var tipoPedido = tipoPedidoRepository.Get(tipoPedidoId);
                var result = tipoPedidoRepository.Delete(tipoPedido);

                if (!result)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "Delete",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(tipoPedidoId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = tipoPedidoId,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Situacao = 'A'
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }

        [Route("/api/[controller]/Get/{tipoPedidoId}")]
        [HttpGet]
        public Response Get(int tipoPedidoId)
        {
            try
            {
                var tipoPedido = tipoPedidoRepository.Get(tipoPedidoId);

                if (tipoPedido == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                response.conteudo.Add(tipoPedido);
                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "Get",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(tipoPedidoId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = tipoPedidoId,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Situacao = 'A'
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }

        [Route("/api/[controller]/List")]
        [HttpGet]
        public Response List()
        {
            try
            {
                var tipospedidos = tipoPedidoRepository.List();

                if (tipospedidos == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                response.conteudo.Add(tipospedidos);
                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "List",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Situacao = 'A'
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }
    }
}
