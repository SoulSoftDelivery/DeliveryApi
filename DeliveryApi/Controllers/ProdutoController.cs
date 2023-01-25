using Microsoft.AspNetCore.Mvc;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interface;
using System;
using System.Net;
using DeliveryApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using System.Drawing;

namespace DeliveryApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProdutoController : Controller
    {
        IProdutoRepository produtoRepository;
        public static IWebHostEnvironment _environment;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public ProdutoController(IProdutoRepository ProdutoRepository, IWebHostEnvironment environment)
        {
            produtoRepository = ProdutoRepository;
            _environment = environment;
        }

        [HttpPost]
        public ActionResult<Response> Post([FromForm] CadastrarProduto produto)
        {
            try
            {
                var newProduto = new ProdutoModel
                {
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Qtd = produto.Qtd,
                    Valor = produto.Valor,
                    Ativo = produto.Ativo,
                    CategoriaProdutoId = produto.CategoriaProdutoId,
                    TipoMedidaId = produto.TipoMedidaId,
                    EmpresaId = produto.EmpresaId,
                    DtCadastro = DateTime.Now,
                    DtAtualizacao = DateTime.Now
                };

                var id = produtoRepository.Create(newProduto);

                if (id == 0)
                {
                    response.ok = false;
                    response.msg = errmsg;

                    return response;
                }

                response.conteudo.Add(id);

                if (produto.ImgCapa != null && produto.ImgCapa.Length > 0)
                {
                    var fileName = produto.EmpresaId + "-" + id + ".jpg";
                    var filePath = Path.Combine("Uploads/Produtos", fileName);
                    using Stream fileStream = new FileStream(filePath, FileMode.Create);
                    produto.ImgCapa.CopyTo(fileStream);

                    string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                    newProduto.ImgCapaNome = fileName;
                    newProduto.ImgCapaUrl = domain + "/" + filePath;

                    produtoRepository.Update(newProduto);
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
                    NomeFuncao = "Post",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(produto),
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
        public ActionResult<Response> Patch([FromForm] CadastrarProduto produto)
        {
            try
            {
                var newProduto = produtoRepository.GetById(produto.Id);

                newProduto.Nome = produto.Nome;
                newProduto.Descricao = produto.Descricao;
                newProduto.Qtd = produto.Qtd;
                newProduto.Valor = produto.Valor;
                newProduto.TipoMedidaId = produto.TipoMedidaId;
                newProduto.CategoriaProdutoId = produto.CategoriaProdutoId;
                newProduto.Ativo = produto.Ativo;
                newProduto.DtAtualizacao = DateTime.Now;

                if (produto.ImgCapa != null && produto.ImgCapa.Length > 0)
                {
                    var fileName = produto.EmpresaId + "-" + produto.Id + ".jpg";
                    var filePath = Path.Combine("Uploads/Produtos", fileName);
                    using Stream fileStream = new FileStream(filePath, FileMode.Create);
                    produto.ImgCapa.CopyTo(fileStream);

                    if (String.IsNullOrEmpty(newProduto.ImgCapaUrl) && String.IsNullOrEmpty(newProduto.ImgCapaNome))
                    {
                        string domain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                        newProduto.ImgCapaNome = fileName;
                        newProduto.ImgCapaUrl = domain + "/" + filePath;
                    }
                }

                var result = produtoRepository.Update(newProduto);

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
                    NomeFuncao = "Patch",
                    Url = domain + "/api/" + ControllerContext.ActionDescriptor.ControllerName + "/" + ControllerContext.ActionDescriptor.ActionName,
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(produto),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = produto.Id,
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

        [HttpDelete("{produtoId:int:min(1)}")]
        public ActionResult<Response> Delete(int produtoId)
        {
            try
            {
                var produto = produtoRepository.GetById(produtoId);

                if (produto is null)
                {
                    response.ok = false;
                    response.msg = "Produto não encontrado.";

                    return NotFound(response);
                }

                var result = produtoRepository.Delete(produto);

                if (!String.IsNullOrEmpty(produto.ImgCapaNome))
                {
                    string filePath = Path.Combine("Uploads/Produtos", produto.ImgCapaNome);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(produtoId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = produtoId,
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

        [HttpGet("{produtoId}")]
        public ActionResult<Response> Get(int produtoId)
        {
            try
            {
                var produto = produtoRepository.GetById(produtoId);

                if (produto == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                //produto.CategoriaProduto = categoriaRepository.Get(produto.CategoriaProdutoId);

                response.conteudo.Add(produto);
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
                    ParametroEntrada = Newtonsoft.Json.JsonConvert.SerializeObject(produtoId),
                    Descricao = ex.Message,
                    DescricaoCompleta = ex.ToString(),
                    RegistroCorrenteId = produtoId,
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
        public ActionResult<Response> Get([FromQuery] int empresaId, string nome, int categoriaProdutoId, int tipoMedidaId, int situacao, int page, int pageSize)
        {
            try
            {
                var produtos = produtoRepository.GetListByEmpresaId(empresaId)
                    .OrderByDescending(x => x.DtCadastro)
                    .ToList();

                if (produtos is null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

                if (categoriaProdutoId != 0)
                {
                    produtos = produtos.Where(x => x.CategoriaProdutoId == categoriaProdutoId).ToList();
                }

                if (tipoMedidaId != 0)
                {
                    produtos = produtos.Where(x => x.TipoMedidaId == tipoMedidaId).ToList();
                }

                if (!string.IsNullOrEmpty(nome))
                {
                    produtos = produtos.Where(x => x.Nome.Contains(nome)).ToList();
                }

                if (situacao >= 1)
                {
                    if (situacao == 1)
                    {
                        produtos = produtos.Where(x => x.Ativo == true).ToList();
                    } else
                    {
                        produtos = produtos.Where(x => x.Ativo == false).ToList();
                    }
                }

                PaginationResponse paginationResponse = new PaginationResponse();

                var totalRows = produtos.Count;

                paginationResponse.TotalRows = totalRows;

                //Arredontando o total de páginas para cima
                decimal x = (decimal)totalRows;
                decimal y = (decimal)pageSize;

                decimal totalPages = (x / y);
                paginationResponse.TotalPages = (int)Math.Ceiling(totalPages);

                //Capturando os registros da páginação atual
                paginationResponse.Results.Add(produtos.Skip((page - 1) * pageSize).Take(pageSize).ToList());

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
