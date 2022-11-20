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
    [Route("/api/[controller]")]
    public class ClienteController : Controller
    {
        IClienteRepository clienteRepository;
        IEnderecoRepository enderecoRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public ClienteController(IClienteRepository ClienteRepository, IEnderecoRepository EnderecoRepository)
        {
            clienteRepository = ClienteRepository;
            enderecoRepository = EnderecoRepository;
        }

        [HttpPost]
        public ActionResult<Response> Post(ClienteEndereco clienteEndereco)
        {
            try
            {
                //Cadastro de Cliente
                ClienteModel newCliente = new ClienteModel
                {
                    EmpresaId = clienteEndereco.EmpresaId,
                    Nome = clienteEndereco.Nome,
                    Telefone = clienteEndereco.Telefone,
                    Email = clienteEndereco.Email,
                    Sexo = clienteEndereco.Sexo,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                var clienteId = clienteRepository.Create(newCliente);

                if (clienteId == 0)
                {
                    response.ok = false;
                    response.msg = "Não foi possível cadastrar o Cliente";

                    return StatusCode(500, response);
                }

                //Cadastro de Endereco
                EnderecoModel newEndereco = new EnderecoModel
                {
                    Uf = clienteEndereco.Uf,
                    Bairro = clienteEndereco.Bairro,
                    Rua = clienteEndereco.Rua,
                    Cep = clienteEndereco.Cep,
                    Cidade = clienteEndereco.Cidade,
                    Quadra = clienteEndereco.Quadra,
                    Lote = clienteEndereco.Lote,
                    Numero = clienteEndereco.Numero,
                    Complemento = clienteEndereco.Complemento,
                    ClienteId = clienteId,
                    TipoEnderecoId = 2,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Ativo = true
                };

                var enderecoId = enderecoRepository.Create(newEndereco);

                if (enderecoId == 0)
                {
                    response.ok = false;
                    response.msg = "Não foi possível cadastrar o Endereço do Cliente";

                    return StatusCode(500, response);
                }

                response.conteudo.Add(clienteId);

                return Ok(response);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(clienteEndereco),
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
        public ActionResult<Response> Put(ClienteEndereco clienteEndereco)
        {
            try
            {
                //Alteração de cliente
                var newCliente = clienteRepository.Get(clienteEndereco.ClienteId);

                newCliente.Nome = clienteEndereco.Nome;
                newCliente.Telefone = clienteEndereco.Telefone;
                newCliente.Email = clienteEndereco.Email;
                newCliente.Sexo = clienteEndereco.Sexo;
                newCliente.Ativo = clienteEndereco.Ativo;
                newCliente.DtAtualizacao = DateTime.Now;

                var result = clienteRepository.Update(newCliente);

                if (!result)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return NotFound(response);
                }

                var newEndereco = enderecoRepository.Get(clienteEndereco.EnderecoId);

                newEndereco.Cidade = clienteEndereco.Cidade;
                newEndereco.Cep = clienteEndereco.Cep;
                newEndereco.Rua = clienteEndereco.Rua;
                newEndereco.Quadra = clienteEndereco.Quadra;
                newEndereco.Bairro = clienteEndereco.Bairro;
                newEndereco.Numero = clienteEndereco.Numero;
                newEndereco.Lote = clienteEndereco.Lote;
                newEndereco.Complemento = clienteEndereco.Complemento;
                newEndereco.Ativo = clienteEndereco.Ativo;

                enderecoRepository.Update(newEndereco);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(clienteEndereco),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = clienteEndereco.ClienteId,
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

        [HttpDelete]
        public ActionResult<Response> Delete(int clienteId)
        {
            try
            {
                //Deletando o Cliente
                var cliente = clienteRepository.Get(clienteId);
                var result = clienteRepository.Delete(cliente);

                if (!result)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    StatusCode(500, response);
                }

                //Deletando o Endereço do Cliente
                var endereco = enderecoRepository.EnderecoByClienteId(clienteId);

                if (endereco != null)
                {
                    enderecoRepository.Delete(endereco);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(clienteId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = clienteId,
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

        [HttpGet("{clienteId}")]
        public ActionResult<Response> Get(int clienteId)
        {
            try
            {
                var cliente = clienteRepository.Get(clienteId);

                ClienteEndereco clienteEndereco = new ClienteEndereco
                {
                    ClienteId = clienteId,
                    EmpresaId = cliente.EmpresaId,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Email = cliente.Email,
                    Sexo = cliente.Sexo,
                    Ativo = cliente.Ativo
                };

                var endereco = enderecoRepository.EnderecoByClienteId(clienteId);

                if (endereco != null)
                {
                    clienteEndereco.EnderecoId = endereco.Id;
                    clienteEndereco.Bairro = endereco.Bairro;
                    clienteEndereco.Rua = endereco.Rua;
                    clienteEndereco.Quadra = endereco.Quadra;
                    clienteEndereco.Lote = endereco.Lote;
                    clienteEndereco.Numero = endereco.Numero;
                    clienteEndereco.Complemento = endereco.Complemento;
                    clienteEndereco.Cep = endereco.Cep;
                    clienteEndereco.Cidade = endereco.Cidade;
                }

                if (cliente == null)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return response;
                }

                response.conteudo.Add(clienteEndereco);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(clienteId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = clienteId,
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
        public ActionResult<Response> Get(int empresaId, string searchText, int page, int pageSize)
        {
            try
            {
                var clientes = clienteRepository.List(empresaId)
                    .OrderByDescending(x => x.DtCadastro)
                    .ToList();

                if (clientes == null)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return NotFound(response);
                }

                //Filtro da pesquisa
                if (!string.IsNullOrEmpty(searchText))
                {
                    clientes = clientes.Where(x =>
                        (!string.IsNullOrEmpty(x.Nome) && x.Nome.Contains(searchText)) ||
                        (!string.IsNullOrEmpty(x.Telefone) && x.Telefone.Contains(searchText)) ||
                        (!string.IsNullOrEmpty(x.Email) && x.Email.Contains(searchText))
                    )
                    .OrderByDescending(x => x.DtCadastro)
                    .ToList();
                }

                PaginationResponse paginationResponse = new PaginationResponse();

                var totalRows = clientes.Count;

                paginationResponse.TotalRows = totalRows;

                //Arredontando o total de páginas para cima
                decimal x = (decimal)totalRows;
                decimal y = (decimal)pageSize;

                decimal totalPages = (x / y);
                paginationResponse.TotalPages = (int)Math.Ceiling(totalPages);

                //Capturando os registros da páginação atual
                paginationResponse.Results.Add(clientes.Skip(page * pageSize).Take(pageSize).ToList());

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
                return StatusCode(500, response);
            }
        }
    }
}
