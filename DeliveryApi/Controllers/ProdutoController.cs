﻿using Microsoft.AspNetCore.Mvc;
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
    public class ProdutoController : Controller
    {
        IProdutoRepository produtoRepository;

        Response response = new Response
        {
            ok = true,
            msg = ""
        };

        const string errmsg = "Não foi possível concluir a solicitação.";

        public ProdutoController(IProdutoRepository ProdutoRepository)
        {
            produtoRepository = ProdutoRepository;
        }

        [Route("/api/[controller]/Create")]
        [HttpPost]
        public Response Create(ProdutoModel produto)
        {
            try
            {
                produto.DtCadastro = DateTime.Now;
                produto.DtAtualizacao = DateTime.Now;
                produto.Ativo = true;

                var id = produtoRepository.Create(produto);

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
                return response;
            }
        }

        [Route("/api/[controller]/Update")]
        [HttpPatch]
        public Response Update(ProdutoModel produto)
        {
            try
            {
                var newProduto = produtoRepository.Get(produto.Id);

                newProduto.Nome = produto.Nome;
                newProduto.Descricao = produto.Descricao;
                newProduto.Qtd = produto.Qtd;
                newProduto.Valor = produto.Valor;
                newProduto.TipoMedidaId = produto.TipoMedidaId;
                newProduto.CategoriaProdutoId = produto.CategoriaProdutoId;
                newProduto.Ativo = produto.Ativo;
                newProduto.DtAtualizacao = DateTime.Now;

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
                    NomeFuncao = "Update",
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
                return response;
            }
        }

        [Route("/api/[controller]/Delete/{produtoId}")]
        [HttpDelete]
        public Response Delete(int produtoId)
        {
            try
            {
                var produto = produtoRepository.Get(produtoId);
                var result = produtoRepository.Delete(produto);

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
                return response;
            }
        }

        [Route("/api/[controller]/Get/{produtoId}")]
        [HttpGet]
        public Response Get(int produtoId)
        {
            try
            {
                var produto = produtoRepository.Get(produtoId);

                if (produto == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
                }

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
                return response;
            }
        }

        [Route("/api/[controller]/List")]
        [HttpGet]
        public Response List(int empresaId, int page, int pageSize)
        {
            try
            {
                var produtos = produtoRepository.List(empresaId)
                    .OrderByDescending(x => x.DtCadastro)
                    .ToList();

                if (produtos == null)
                {
                    response.ok = false;
                    response.msg = errmsg;
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
    }
}
