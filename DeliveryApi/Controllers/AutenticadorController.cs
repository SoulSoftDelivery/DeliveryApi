using Microsoft.AspNetCore.Mvc;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interface;
using System;
using System.Net;
using DeliveryApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Web;

namespace DeliveryApi.Controllers
{
    [Authorize]
    [ApiController]
    public class AutenticadorController : Controller
    {
        IUsuarioRepository usuarioRepository;
        private IConfiguration _config { get; }

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public AutenticadorController(
            IUsuarioRepository UsuarioRepository,
            IConfiguration Configuration
        )
        {
            usuarioRepository = UsuarioRepository;
            _config = Configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/[controller]/Login")]
        public Response Login([FromBody] Login login)
        {
            try
            {
                var usuario = usuarioRepository.ConsultaPorEmail(login.Email);

                if (usuario == null)
                {
                    response.ok = false;
                    response.msg = "Email não encontrado.";

                    return response;
                }

                login.Senha = SecurityService.Criptografar(login.Senha);

                if (login.Senha != usuario.Senha)
                {
                    response.ok = false;
                    response.msg = "Senha incorreta.";

                    return response;
                }

                var token = TokenService.GenerateToken(usuario, _config["Jwt:Key"]);
                var refreshToken = TokenService.GenerateRefreshToken();

                TokenService.DeleteRefreshToken(login.Email);
                TokenService.SaveRefreshToken(login.Email, refreshToken);

                var responseLogin = new ResponseLogin
                {
                    UsuarioId = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Telefone = usuario.Telefone,
                    EmpresaId = usuario.EmpresaId,
                    TipoUsuarioId = usuario.TipoUsuarioId,
                    Token = token.Trim(),
                    RefreshToken = refreshToken.Trim()
                };

                response.conteudo.Add(responseLogin);

                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "Login",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(login),
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

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/[controller]/LoginWithToken")]
        public Response LoginWithToken([FromBody] LoginWithToken loginWithToken)
        {
            try
            {
                var principal = TokenService.GetPrincipalFromExpiredToken(loginWithToken.Token, _config["Jwt:Key"]);
                var usuarioEmail = principal.Identity.Name;
                var savedRefreshToken = TokenService.GetRefreshToken(usuarioEmail);
                if (savedRefreshToken != loginWithToken.RefreshToken)
                    throw new SecurityTokenException("Refresh Token Invalido");

                var newJwtToken = TokenService.GenerateToken(principal.Claims, _config["Jwt:Key"]);
                var newRefreshToken = TokenService.GenerateRefreshToken();
                TokenService.DeleteRefreshToken(usuarioEmail);
                TokenService.SaveRefreshToken(usuarioEmail, newRefreshToken);

                var usuario = usuarioRepository.ConsultaPorEmail(usuarioEmail);

                var responseLogin = new ResponseLogin
                {
                    UsuarioId = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Telefone = usuario.Telefone,
                    EmpresaId = usuario.EmpresaId,
                    TipoUsuarioId = usuario.TipoUsuarioId,
                    Token = newJwtToken,
                    RefreshToken = newRefreshToken
                };

                response.conteudo.Add(responseLogin);

                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "LoginWithToken",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(loginWithToken),
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

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/[controller]/EnviarLinkRedefinicaoSenha/{email}")]
        public Response EnviarLinkRedefinicaoSenha(string email)
        {
            try
            {
                var usuario = usuarioRepository.ConsultaPorEmail(email);

                if (usuario == null)
                {
                    response.ok = false;
                    response.msg = "Email não encontrado.";

                    return response;
                }

                string emailCifrado = SecurityService.Cifrar(usuario.Email);
                emailCifrado = HttpUtility.UrlEncode(emailCifrado);
                emailCifrado = emailCifrado.Replace('-', '+').Replace('_', '/');

                var url = "https://soulsoft.com.br/reset-password";

                string hostName = (this.Request.Host).ToString();

                if (hostName.Contains("localhost"))
                {
                    url = "http://localhost:3000/reset-password";
                }

                var link = $"{url}?ref={emailCifrado}&email={usuario.Email}&id={usuario.Id}";

                string texto = $"Olá {System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(usuario.Nome)}, <br><br> Acesse o seguinte <a href=\"{link}\">link</a> para redefinir sua senha. Ou copie o link abaixo e cole no seu navegador: <br><br>{link} <br> <br>Atenciosamente,<br><br> Segurança SoulSoft <br>soulsoft.com.br";
                EmailService.EnviarEmail("SoulSoft - Redefinição de Senha", usuario.Nome, usuario.Email, texto);

                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "LoginWithToken",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(email),
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

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/[controller]/ChecarResetSenha")]
        public Response ChecarResetSenha([FromBody] RedefinirSenha redefinirSenha)
        {
            try
            {
                var usuario = usuarioRepository.Get(redefinirSenha.Id);

                if (usuario == null)
                {
                    response.ok = false;
                    response.msg = "Usuário não encontrado.";
                }


                bool resultado = SecurityService.Decifrar2(redefinirSenha.Token, redefinirSenha.Email);

                if (!(usuario.Email == redefinirSenha.Email && resultado == true && usuario.Id == redefinirSenha.Id))
                {
                    response.ok = false;
                    response.msg = "Ocorreu erro no link de reset enviado por email. Por solicite outro reset novamente";

                    return response;
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
                    NomeFuncao = "LoginWithToken",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(redefinirSenha),
                    RegistroCorrenteId = redefinirSenha.Id,
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

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/[controller]/ResetarSenha")]
        public Response ResetarSenha([FromBody] RedefinirSenha redefinirSenha)
        {
            try
            {
                var usuario = usuarioRepository.Get(redefinirSenha.Id);

                if (usuario == null)
                {
                    response.ok = false;
                    response.msg = "Usuário não encontrado.";

                    return response;
                }

                usuario.Senha = SecurityService.Criptografar(redefinirSenha.NovaSenha);
                usuario.DtAtualizacao = DateTime.Now;

                usuarioRepository.Update(usuario);

                return response;
            }
            catch (Exception ex)
            {
                string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                ErroModel erro = new ErroModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    NomeAplicacao = "DeliveryApi",
                    NomeFuncao = "LoginWithToken",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(redefinirSenha),
                    RegistroCorrenteId = redefinirSenha.Id,
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
