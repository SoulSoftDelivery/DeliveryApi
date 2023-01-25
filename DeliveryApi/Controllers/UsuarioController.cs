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

        [Route("/api/[controller]/Create")]
        [HttpPost]
        public Response Create(UsuarioModel usuario)
        {
            try
            {
                var exists = usuarioRepository.ConsultaPorEmail(usuario.Email);

                if (exists != null)
                {
                    response.ok = false;
                    response.msg = "Email já cadastrado.";

                    return response;
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
                return response;
            }
        }

        [Route("/api/[controller]/Update")]
        [HttpPatch]
        public Response Update(UsuarioModel usuario)
        {
            try
            {
                var newUsuario = usuarioRepository.Get(usuario.Id);

                newUsuario.Nome = usuario.Nome;
                newUsuario.Telefone = usuario.Telefone;
                newUsuario.Email = usuario.Email;
                newUsuario.Ativo = usuario.Ativo;
                newUsuario.DtAtualizacao = DateTime.Now;

                var result = usuarioRepository.Update(newUsuario);

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
                return response;
            }
        }

        [Route("/api/[controller]/Delete/{usuarioId}")]
        [HttpDelete]
        public Response Delete(int usuarioId)
        {
            try
            {
                var usuario = usuarioRepository.Get(usuarioId);
                var result = usuarioRepository.Delete(usuario);

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
                return response;
            }
        }

        [Route("/api/[controller]/Get/{usuarioId}")]
        [HttpGet]
        public Response Get(int usuarioId)
        {
            try
            {
                var usuario = usuarioRepository.Get(usuarioId);

                if (usuario == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                response.conteudo.Add(usuario);
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
                return response;
            }
        }

        [Route("/api/[controller]/List")]
        [HttpGet]
        public Response List(int empresaId, int page, int pageSize)
        {
            try
            {
                var usuarios = usuarioRepository.List(empresaId)
                    .OrderByDescending(x => x.DtCadastro)
                    .ToList();

                if (usuarios == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
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
                    RegistroCorrenteId = empresaId,
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

        [Route("/api/[controller]/EditPassword")]
        [HttpPost]
        public Response EditPassword(EditPassword editPassword)
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
                return response;
            }
        }
    }
}
