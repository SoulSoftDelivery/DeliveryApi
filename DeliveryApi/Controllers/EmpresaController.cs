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
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class EmpresaController : Controller
    {
        IEmpresaRepository empresaRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public EmpresaController(IEmpresaRepository EmpresaRepository)
        {
            empresaRepository = EmpresaRepository;
        }

        [HttpPost]
        public ActionResult<Response> Post(EmpresaModel empresa)
        {
            try
            {
                empresa.DtCadastro = DateTime.Now;
                empresa.DtAtualizacao = DateTime.Now;
                empresa.Ativo = true;

                var id = empresaRepository.Create(empresa);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(empresa),
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

        [HttpPut]
        public ActionResult<Response> Put(EmpresaModel empresa)
        {
            try
            {
                var newEmpresa = empresaRepository.Get(empresa.Id);

                newEmpresa.Nome = empresa.Nome;
                newEmpresa.Telefone1 = empresa.Telefone1;
                newEmpresa.Telefone2 = empresa.Telefone2;
                newEmpresa.Email = empresa.Email;
                newEmpresa.Cidade = empresa.Cidade;
                newEmpresa.Uf = empresa.Uf;
                newEmpresa.Cep = empresa.Cep;
                newEmpresa.Bairro = empresa.Bairro;
                newEmpresa.Quadra = empresa.Quadra;
                newEmpresa.Rua = empresa.Rua;
                newEmpresa.Lote = empresa.Lote;
                newEmpresa.Numero = empresa.Numero;
                newEmpresa.Complemento = empresa.Complemento;
                newEmpresa.Ativo = empresa.Ativo;
                newEmpresa.DtAtualizacao = DateTime.Now;

                var result = empresaRepository.Update(newEmpresa);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(empresa),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = empresa.Id,
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

        [HttpDelete("{empresaId}")]
        public ActionResult<Response> Delete(int empresaId)
        {
            try
            {
                var empresa = empresaRepository.Get(empresaId);
                var result = empresaRepository.Delete(empresa);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(empresaId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = empresaId,
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

        [HttpGet("{empresaId}")]
        public ActionResult<Response> Get(int empresaId)
        {
            try
            {
                var empresa = empresaRepository.Get(empresaId);

                if (empresa == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                response.conteudo.Add(empresa);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(empresaId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = empresaId,
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
                var empresas = empresaRepository.List();

                if (empresas == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                response.conteudo.Add(empresas);
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
                return StatusCode(500, response);
            }
        }
    }
}
