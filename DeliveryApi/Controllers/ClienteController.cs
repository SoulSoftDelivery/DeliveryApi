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

        [Route("/api/[controller]/Create")]
        [HttpPost]
        public Response Create(ClienteEndereco clienteEndereco)
        {
            try
            {
                //Cadastro de Cliente
                ClienteModel newCliente = new ClienteModel
                {
                    Nome = clienteEndereco.Nome,
                    Telefone = clienteEndereco.Telefone,
                    Email = clienteEndereco.Email,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Situacao = 'A'
                };

                var clienteId = clienteRepository.Create(newCliente);

                if (clienteId == 0)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return response;
                }

                //Cadastro de Endereco
                EnderecoModel newEndereco = new EnderecoModel
                {
                    Bairro = clienteEndereco.Bairro,
                    Rua = clienteEndereco.Rua,
                    Cep = clienteEndereco.Cep,
                    Cidade = clienteEndereco.Cidade,
                    Quadra = clienteEndereco.Quadra,
                    Lote = clienteEndereco.Lote,
                    Numero = clienteEndereco.Numero,
                    Complemento = clienteEndereco.Complemento,
                    ClienteId = clienteId,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now,
                    Situacao = 'A'
                };

                var enderecoId = enderecoRepository.Create(newEndereco);

                if (enderecoId == 0)
                {
                    response.ok = false;
                    response.msg = "Erro no cadastro de endereço";

                    return response;
                }

                response.conteudo.Add(clienteId);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(clienteEndereco),
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
        public Response Update(ClienteModel cliente)
        {
            try
            {
                var newcliente = clienteRepository.Get(cliente.Id);

                newcliente.Nome = cliente.Nome;
                newcliente.Telefone = cliente.Telefone;
                newcliente.Email = cliente.Email;
                newcliente.Situacao = cliente.Situacao;
                newcliente.DtAtualizacao = DateTime.Now;

                var result = clienteRepository.Update(newcliente);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(cliente),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = cliente.Id,
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

        [Route("/api/[controller]/Delete/{clienteId}")]
        [HttpDelete]
        public Response Delete(int clienteId)
        {
            try
            {
                var cliente = clienteRepository.Get(clienteId);
                var result = clienteRepository.Delete(cliente);

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(clienteId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = clienteId,
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

        [Route("/api/[controller]/Get/{clienteId}")]
        [HttpGet]
        public Response Get(int clienteId)
        {
            try
            {
                var cliente = clienteRepository.Get(clienteId);
                var endereco = enderecoRepository.EnderecoByClienteId(clienteId);

                ClienteEndereco clienteEndereco = new ClienteEndereco
                {
                    ClienteId = clienteId,
                    EnderecoId = endereco.Id,
                    EmpresaId = cliente.EmpresaId,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Email = cliente.Email,
                    //Sexo = cliente.Sexo,
                    Bairro = endereco.Bairro,
                    Rua = endereco.Rua,
                    Quadra = endereco.Quadra,
                    Lote = endereco.Lote,
                    Numero = endereco.Numero,
                    Complemento = endereco.Complemento,
                    Cep = endereco.Cep,
                    Cidade = endereco.Cidade
                };

                if (cliente == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
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
        public Response List(int empresaId, string searchText, int page, int pageSize)
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

                    return response;
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
