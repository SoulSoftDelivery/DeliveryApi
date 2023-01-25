using Microsoft.AspNetCore.Mvc;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interface;
using System;
using System.Net;
using DeliveryApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace DeliveryApi.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("/api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MesaController : Controller
    {
        IMesaRepository mesaRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public MesaController(IMesaRepository MesaRepository)
        {
            mesaRepository = MesaRepository;
        }

        [HttpPost]
        public ActionResult<Response> Post(MesaModel mesa)
        {
            try
            {
                mesa.DtCadastro = DateTime.Now;
                mesa.DtAtualizacao = DateTime.Now;
                mesa.Ativo = mesa.Ativo;

                var id = mesaRepository.Create(mesa);

                if (id == 0)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return StatusCode(500, response);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(mesa),
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
        public ActionResult<Response> Patch(MesaModel mesa)
        {
            try
            {
                var newMesa = mesaRepository.Get(mesa.Id);

                newMesa.Ativo = mesa.Ativo;
                newMesa.DtAtualizacao = DateTime.Now;
                newMesa.Numero = mesa.Numero;

                var result = mesaRepository.Update(newMesa);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(mesa),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = mesa.Id,
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

        [HttpDelete("{mesaId}")]
        public ActionResult<Response> Delete(int mesaId)
        {
            try
            {
                var mesa = mesaRepository.Get(mesaId);
                var result = mesaRepository.Delete(mesa);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(mesaId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = mesaId,
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

        [HttpGet("{mesaId}")]
        public ActionResult<Response> Get(int mesaId)
        {
            try
            {
                var mesa = mesaRepository.Get(mesaId);

                if (mesa == null)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return StatusCode(500, response);
                }

                response.conteudo.Add(mesa);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(mesaId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = mesaId,
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
        public ActionResult<Response> Get([FromQuery] int empresaId, int situacao, int page, int pageSize)
        {
            try
            {
                var mesas = mesaRepository.List(empresaId)
                    .OrderByDescending(x => x.DtCadastro)
                    .ToList();

                if (mesas == null)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return NotFound(response);
                }

                if (situacao >= 1)
                {
                    if (situacao == 1)
                    {
                        mesas = mesas.Where(x => x.Ativo == true).ToList();
                    }
                    else
                    {
                        mesas = mesas.Where(x => x.Ativo == false).ToList();
                    }
                }

                PaginationResponse paginationResponse = new PaginationResponse();

                var totalRows = mesas.Count;

                paginationResponse.TotalRows = totalRows;

                //Arredontando o total de páginas para cima
                decimal x = (decimal)totalRows;
                decimal y = (decimal)pageSize;

                decimal totalPages = (x / y);
                paginationResponse.TotalPages = (int)Math.Ceiling(totalPages);

                //Capturando os registros da páginação atual
                paginationResponse.Results.Add(mesas.Skip(page * pageSize).Take(pageSize).ToList());

                response.conteudo.Add(paginationResponse);
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
