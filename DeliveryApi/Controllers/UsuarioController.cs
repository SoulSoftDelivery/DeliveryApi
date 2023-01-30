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
    public class UsuarioController : Controller
    {
        IUsuarioRepository usuarioRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public UsuarioController(IUsuarioRepository UsuarioRepository)
        {
            usuarioRepository = UsuarioRepository;
        }

        [HttpPost]
        public ActionResult<Response> Post(UsuarioModel usuario)
        {
            try
            {
                var exists = usuarioRepository.ConsultaPorEmail(usuario.Email);

                if (exists != null)
                {
                    response.ok = false;
                    response.msg = "Email já cadastrado.";

                    return BadRequest(response);
                }

                usuario.DtCadastro = DateTime.Now;
                usuario.DtAtualizacao = DateTime.Now;
                usuario.Ativo = true;
                usuario.Senha = usuario.Senha.Encrypt();

                var id = usuarioRepository.Create(usuario);

                if (id == 0)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return Ok(response);
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
                    NomeFuncao = "Post",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(usuario),
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
        public ActionResult<Response> Patch(UsuarioModel usuario)
        {
            try
            {
                var newUsuario = usuarioRepository.Get(usuario.Id);

                if (newUsuario == null)
                {
                    response.ok = false;
                    response.msg = "Não foi possível encontrar o registro.";

                    return NotFound(response);
                }

                newUsuario.Nome = usuario.Nome;
                newUsuario.Telefone = usuario.Telefone;
                newUsuario.Email = usuario.Email;
                newUsuario.Senha = usuario.Senha.Encrypt();
                newUsuario.Ativo = usuario.Ativo;
                newUsuario.DtAtualizacao = DateTime.Now;

                var result = usuarioRepository.Update(newUsuario);

                if (!result)
                {
                    response.ok = false;
                    response.msg = errmsg;
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(usuario),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = usuario.Id,
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

        [HttpDelete("{usuarioId}")]
        public ActionResult<Response> Delete(int usuarioId)
        {
            try
            {
                var usuario = usuarioRepository.Get(usuarioId);

                if (usuario == null)
                {
                    response.ok = false;
                    response.msg = "Não foi possível encontrar o registro.";

                    return NotFound(response);
                }

                var result = usuarioRepository.Delete(usuario);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(usuarioId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = usuarioId,
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

        [HttpGet("{usuarioId}")]
        public ActionResult<Response> Get(int usuarioId)
        {
            try
            {
                var usuario = usuarioRepository.Get(usuarioId);

                if (usuario == null)
                {
                    response.ok = false;
                    response.msg = "Não foi possível encontrar o registro.";

                    return NotFound(response);
                }

                usuario.Senha = usuario.Senha.DecryptStringAES();

                response.conteudo.Add(usuario);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(usuarioId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = usuarioId,
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
        public ActionResult<Response> Get([FromQuery] int empresaId, string nome, int situacao, int page, int pageSize)
        {
            try
            {
                var usuarios = usuarioRepository.List(empresaId)
                    .OrderByDescending(x => x.DtCadastro)
                    .ToList();

                if (usuarios == null)
                {
                    response.ok = false;
                    response.msg = "Não foi possível encontrar o registro.";

                    return NotFound(response);
                }

                //Filtro da pesquisa
                if (!string.IsNullOrEmpty(nome))
                {
                    usuarios = usuarios.Where(x => x.Nome.Contains(nome)).ToList();
                }

                if (situacao >= 1)
                {
                    if (situacao == 1)
                    {
                        usuarios = usuarios.Where(x => x.Ativo == true).ToList();
                    }
                    else
                    {
                        usuarios = usuarios.Where(x => x.Ativo == false).ToList();
                    }
                }

                PaginationResponse paginationResponse = new PaginationResponse();

                var totalRows = usuarios.Count;

                paginationResponse.TotalRows = totalRows;

                //Arredontando o total de páginas para cima
                decimal x = (decimal)totalRows;
                decimal y = (decimal)pageSize;

                decimal totalPages = (x / y);
                paginationResponse.TotalPages = (int)Math.Ceiling(totalPages);

                //Capturando os registros da páginação atual
                paginationResponse.Results.Add(usuarios.Skip((page - 1) * pageSize).Take(pageSize).ToList());

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
                    NomeFuncao = "List",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
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

        [HttpPost("EditPassword")]
        public ActionResult<Response> EditPassword(EditPassword editPassword)
        {
            try
            {
                var usuario = usuarioRepository.Get(editPassword.UsuarioId);

                editPassword.SenhaAtual = editPassword.SenhaAtual.Encrypt();

                if (usuario.Senha != editPassword.SenhaAtual)
                {
                    response.ok = false;
                    response.msg = "Senha Atual incorreta.";

                    return response;
                }

                usuario.Senha = editPassword.NovaSenha.Encrypt();
                usuario.DtAtualizacao = DateTime.Now;

                var result = usuarioRepository.Update(usuario);

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
                    NomeFuncao = "EditPassword",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(editPassword),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = editPassword.UsuarioId,
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
