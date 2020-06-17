using ApiControleEmpresa.Controllers;
using ApiControleEmpresa.Entities;
using ApiControleEmpresa.UnitOfWorks;
using ApiControleEmpresaTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace ApiControleEmpresaTests.Cenarios
{
    public class EmpresaTests
    {
        private readonly EmpresaController _controller;
        private readonly TestContext _testContext;
        private HttpClient client;
        private Uri urlBase;


        public EmpresaTests()
        {
            _controller = new EmpresaController();
            _testContext = new TestContext();
            client = new HttpClient();
            urlBase = new Uri($"https://localhost:44331/api");
        }

        [Fact]
        public async Task<object> ValidarIntegracao()
        {
            HttpResponseMessage response = client.GetAsync($"{urlBase}/Empresa/SelectEmpresas").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            else
            {
                throw new Exception($"ERRO-{response.Content.ReadAsStringAsync().Result}");
            }
        }

        [Fact]
        public async Task CapturarRetornoResponse()
        {
            var response = await _testContext.Client.GetAsync($"{urlBase}/Empresa/SelectEmpresas");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CapturarErroFormatacapDados()
        {
            var response = await _testContext.Client.GetAsync($"{urlBase}/Empresa/SaveNotas" + new Notas());
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString().Should().Be("text/plain; charset=utf-8");
        }

        [Fact]
        public void ValidarListaVazia()
        {//Teste Com Objeto Nulo

            // act
            var lista = _controller.SelectEmpresas(new UnitOfWork());

            // assert
            Assert.NotNull(lista);
        }

        [Fact]
        public void SalvarEmpresaSemDados()
        {//Teste Com Objeto Nulo

            // act
            var result = _controller.SaveNotas(new Notas(), new UnitOfWork());
            var okResult = result as ObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
        }
        [Fact]
        public void SalvarEmpresaSemIdentificador()
        {
            Notas nota = new Notas();

            nota.IdEmpresa = 0;
            nota.TipoNota = 1;

            NotaItem itemNota = new NotaItem();

            itemNota.Item[0] = 1;
            itemNota.Nome[0] = "Teste";
            itemNota.Preco[0] = 2.5;
            itemNota.Quantidade[0] = 2;

            nota.ListaNotas[0] = itemNota;

            // act
            var result = _controller.SaveNotas(nota, new UnitOfWork());
            var okResult = result as ObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
        }
        [Fact]
        public void SalvarEmpresaSemNotas()
        {
            Notas nota = new Notas();

            nota.IdEmpresa = 2;
            nota.TipoNota = 1;

            // act
            var result = _controller.SaveNotas(nota, new UnitOfWork());
            var okResult = result as ObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
        }
        [Fact]
        public void SalvarEmpresaNotaErrada()
        {
            Notas nota = new Notas();

            nota.IdEmpresa = 0;
            nota.TipoNota = 1;

            NotaItem itemNota = new NotaItem();

            itemNota.Item[0] = 1;
            itemNota.Item[1] = 2;
            itemNota.Nome[0] = "Teste";
            itemNota.Preco[0] = 2.5;
            itemNota.Quantidade[0] = 2;

            nota.ListaNotas[0] = itemNota;

            // act
            var result = _controller.SaveNotas(nota, new UnitOfWork());
            var okResult = result as ObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
        }
        [Fact]
        public void SalvarEmpresaDuasNotas()
        {
            Notas nota = new Notas();

            nota.IdEmpresa = 0;
            nota.TipoNota = 1;

            NotaItem itemNota = new NotaItem();

            itemNota.Item[0] = 1;
            itemNota.Item[1] = 2;
            itemNota.Nome[0] = "Teste";
            itemNota.Nome[1] = "Teste 2";
            itemNota.Preco[0] = 2.5;
            itemNota.Preco[0] = 1.5;
            itemNota.Quantidade[0] = 2;
            itemNota.Quantidade[1] = 1;

            nota.ListaNotas[0] = itemNota;

            NotaItem itemNota1 = new NotaItem();

            itemNota.Item[0] = 1;
            itemNota.Nome[0] = "Teste 4";
            itemNota.Preco[0] = 6.5;
            itemNota.Quantidade[0] = 10;

            nota.ListaNotas[0] = itemNota1;

            // act
            var result = _controller.SaveNotas(nota, new UnitOfWork());
            var okResult = result as ObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
        }
    }
}
