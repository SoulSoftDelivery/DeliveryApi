using System.Text;
using System.Net.Mail;
using System.Net;
using System;
using DeliveryApi.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryApi.Services
{
    public class PaginationService
    {
        public static PaginationResponse FazPaginacao(List<object> lista, int page, int pageSize)
        {
            PaginationResponse paginationResponse = new PaginationResponse();

            var totalRows = lista.Count;

            paginationResponse.TotalRows = totalRows;

            //Arredontando o total de páginas para cima
            decimal x = (decimal)totalRows;
            decimal y = (decimal)pageSize;

            decimal totalPages = (x / y);
            paginationResponse.TotalPages = (int)Math.Ceiling(totalPages);

            //Capturando os registros da páginação atual
            paginationResponse.Results.Add(lista.Skip(page * pageSize).Take(pageSize).ToList());

            return paginationResponse;
        }
    }
}
