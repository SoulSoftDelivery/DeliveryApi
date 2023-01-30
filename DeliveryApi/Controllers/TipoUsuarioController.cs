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
    [Route("/api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TipoUsuarioController : Controller
    {
        ITipoUsuarioRepository tipoUsuarioRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public TipoUsuarioController(ITipoUsuarioRepository TipoUsuarioRepository)
        {
            tipoUsuarioRepository = TipoUsuarioRepository;
        }

        [HttpPost]
        public ActionResult<Response> Post(TipoUsuarioModel tipoUsuario)
        {
            try
            {
                tipoUsuario.DtCadastro = DateTime.Now;
                tipoUsuario.DtAtualizacao = DateTime.Now;
                tipoUsuario.Ativo = true;

                var id = tipoUsuarioRepository.Create(tipoUsuario);

                if (id == 0)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return response;
                }

                response.conteudo.Add(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "Post",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(tipoUsuario),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return StatusCode(500, response);
            }
        }

        [HttpPatch]
        public ActionResult<Response> Patch(TipoUsuarioModel tipoUsuario)
        {
            try
            {
                var newTipoUsuario = tipoUsuarioRepository.Get(tipoUsuario.Id);

                newTipoUsuario.Nome = tipoUsuario.Nome;
                newTipoUsuario.Ativo = tipoUsuario.Ativo;
                newTipoUsuario.DtAtualizacao = DateTime.Now;

                var result = tipoUsuarioRepository.Update(newTipoUsuario);

                if (!result)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return StatusCode(500, response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "Patch",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(tipoUsuario),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = tipoUsuario.Id,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{tipoUsuarioId}")]
        public ActionResult<Response> Delete(int tipoUsuarioId)
        {
            try
            {
                var tipoUsuario = tipoUsuarioRepository.Get(tipoUsuarioId);

                if (tipoUsuario == null)
                {
                    response.ok = false;
                    response.msg = "Não foi possível encontrar o registro.";

                    return NotFound(response);
                }

                var result = tipoUsuarioRepository.Delete(tipoUsuario);

                if (!result)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return StatusCode(500, response);
                }

                return Ok(response);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(tipoUsuarioId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = tipoUsuarioId,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{tipoUsuarioId}")]
        public ActionResult<Response> Get(int tipoUsuarioId)
        {
            try
            {
                var tipoUsuario = tipoUsuarioRepository.Get(tipoUsuarioId);

                if (tipoUsuario == null)
                {
                    response.ok = false;
                    response.msg = "Não foi possível encontrar o registro.";

                    return NotFound(response);
                }

                response.conteudo.Add(tipoUsuario);
                return Ok(response);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(tipoUsuarioId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = tipoUsuarioId,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        public ActionResult<Response> Get()
        {
            try
            {
                var tiposUsuarios = tipoUsuarioRepository.List();

                if (tiposUsuarios == null)
                {
                    response.ok = false;
                    response.msg = "Não foi possível encontrar o registro.";

                    return NotFound(response);
                }

                response.conteudo.Add(tiposUsuarios);
                return Ok(response);
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
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                ErroService.NotifyError(erro, domain);

                response.ok = false;
                response.msg = errmsg;
                return StatusCode(500, response);
            }
        }
    }
}
