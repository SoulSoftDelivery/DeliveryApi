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
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SituacaoPedidoController : Controller
    {
        ISituacaoPedidoRepository situacaoPedidoRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public SituacaoPedidoController(ISituacaoPedidoRepository SituacaoPedidoRepository)
        {
            situacaoPedidoRepository = SituacaoPedidoRepository;
        }

        [Route("/api/[controller]/Create")]
        [HttpPost]
        public Response Create(SituacaoPedidoModel situacaoPedido)
        {
            try
            {
                situacaoPedido.DtCadastro = DateTime.Now;
                situacaoPedido.DtAtualizacao = DateTime.Now;
                situacaoPedido.Ativo = true;

                var id = situacaoPedidoRepository.Create(situacaoPedido);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(situacaoPedido),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }

        [Route("/api/[controller]/Update")]
        [HttpPatch]
        public Response Update(SituacaoPedidoModel situacaoPedido)
        {
            try
            {
                var newSituacaoPedido = situacaoPedidoRepository.Get(situacaoPedido.Id);

                newSituacaoPedido.Nome = situacaoPedido.Nome;
                newSituacaoPedido.Ativo = situacaoPedido.Ativo;
                newSituacaoPedido.DtAtualizacao = DateTime.Now;

                var result = situacaoPedidoRepository.Update(newSituacaoPedido);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(situacaoPedido),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = situacaoPedido.Id,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }

        [Route("/api/[controller]/Delete/{situacaoPedidoId}")]
        [HttpDelete]
        public Response Delete(int situacaoPedidoId)
        {
            try
            {
                var situacaoPedido = situacaoPedidoRepository.Get(situacaoPedidoId);
                var result = situacaoPedidoRepository.Delete(situacaoPedido);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(situacaoPedidoId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = situacaoPedidoId,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }

        [Route("/api/[controller]/Get/{situacaoPedidoId}")]
        [HttpGet]
        public Response Get(int situacaoPedidoId)
        {
            try
            {
                var situacaoPedido = situacaoPedidoRepository.Get(situacaoPedidoId);

                if (situacaoPedido == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                response.conteudo.Add(situacaoPedido);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(situacaoPedidoId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = situacaoPedidoId,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
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
                var tipospedidos = situacaoPedidoRepository.List();

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
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return response;
            }
        }
    }
}
