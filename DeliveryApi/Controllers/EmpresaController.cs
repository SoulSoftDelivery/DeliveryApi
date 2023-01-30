using Microsoft.AspNetCore.Mvc;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interface;
using System;
using System.Net;
using DeliveryApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

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
        IUsuarioRepository usuarioRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public EmpresaController(IEmpresaRepository EmpresaRepository, IUsuarioRepository UsuarioRepository)
        {
            empresaRepository = EmpresaRepository;
            usuarioRepository = UsuarioRepository;
        }

        [AllowAnonymous]
        [HttpPost("CadastrarEmpresaUsuario")]
        public ActionResult<Response> PostCadastrarEmpresaUsuario(CadastrarEmpresaUsuario cadastrarEmpresaUsuario)
        {
            try
            {
                var exists = usuarioRepository.ConsultaPorEmail(cadastrarEmpresaUsuario.EmailUsuario);

                if (exists != null)
                {
                    response.ok = false;
                    response.msg = "Email já cadastrado.";

                    return BadRequest(response);
                }

                var empresa = new EmpresaModel
                {
                    Nome = cadastrarEmpresaUsuario.NomeEmpresa,
                    Email = cadastrarEmpresaUsuario.EmailEmpresa,
                    Telefone1 = cadastrarEmpresaUsuario.Telefone1Empresa,
                    Telefone2 = cadastrarEmpresaUsuario.Telefone2Empresa,
                    Cnpj = cadastrarEmpresaUsuario.CnpjEmpresa,
                    Cep = cadastrarEmpresaUsuario.CepEmpresa,
                    Cidade = cadastrarEmpresaUsuario.CidadeEmpresa,
                    Uf = cadastrarEmpresaUsuario.UfEmpresa,
                    Bairro = cadastrarEmpresaUsuario.BairroEmpresa,
                    Rua = cadastrarEmpresaUsuario.RuaEmpresa,
                    Quadra = cadastrarEmpresaUsuario.QuadraEmpresa,
                    Lote = cadastrarEmpresaUsuario.LoteEmpresa,
                    Numero = cadastrarEmpresaUsuario.NumeroEmpresa,
                    Complemento = cadastrarEmpresaUsuario.ComplementoEmpresa,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                var empresaId = empresaRepository.Create(empresa);

                if (empresaId > 0)
                {
                    var usuario = new UsuarioModel
                    {
                        Nome = cadastrarEmpresaUsuario.NomeUsuario,
                        Telefone = cadastrarEmpresaUsuario.TelefoneUsuario,
                        Email = cadastrarEmpresaUsuario.EmailUsuario,
                        Senha = cadastrarEmpresaUsuario.SenhaUsuario.Encrypt(),
                        EmpresaId = empresaId,
                        TipoUsuarioId = 1,
                        DtCadastro = DateTime.Now,
                        DtAtualizacao = DateTime.Now,
                        Ativo = true
                    };

                    var usuarioId = usuarioRepository.Create(usuario);

                    if (usuarioId > 0)
                    {
                        response.conteudo.Add(usuarioId);

                        return Ok(response);
                    }
                }

                response.ok = false;
                response.msg = errmsg;

                return StatusCode(500, response); ;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "CadastrarEmpresaUsuario",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(cadastrarEmpresaUsuario),
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

    public class CadastrarEmpresaUsuario
    {
        //Dados da Empresa
        [StringLength(100)]
        [Required]
        public string NomeEmpresa { get; set; }
        [StringLength(18)]
        public string CnpjEmpresa { get; set; }
        [StringLength(16)]
        [Required]
        public string Telefone1Empresa { get; set; }
        [StringLength(16)]
        public string Telefone2Empresa { get; set; }
        [StringLength(100)]
        public string EmailEmpresa { get; set; }
        [StringLength(2)]
        [Required]
        public string UfEmpresa { get; set; }
        [StringLength(100)]
        [Required]
        public string CidadeEmpresa { get; set; }
        [StringLength(10)]
        public string CepEmpresa { get; set; }
        [StringLength(100)]
        public string BairroEmpresa { get; set; }
        [StringLength(100)]
        public string RuaEmpresa { get; set; }
        [StringLength(10)]
        public string QuadraEmpresa { get; set; }
        [StringLength(10)]
        public string LoteEmpresa { get; set; }
        [StringLength(10)]
        public string NumeroEmpresa { get; set; }
        [StringLength(100)]
        public string ComplementoEmpresa { get; set; }

        //Dados do Usuário
        [StringLength(100)]
        [Required]
        public string NomeUsuario { get; set; }
        [StringLength(16)]
        [Required]
        public string TelefoneUsuario { get; set; }
        [StringLength(100)]
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
    }
}
