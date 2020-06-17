using ApiControleEmpresa.Entities;
using ApiControleEmpresa.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiControleEmpresa.Service
{
    public class EmpresaService
    {
        public IActionResult Save(Notas notas,
            [FromServices] IUnitOfWork unitOfWork)
        {
            try
            {
                ValidarParametros(notas);

                if (notas.TipoNota == 2)
                {
                    GravarNotasDebitos(notas, unitOfWork);
                }
                else
                {
                    GravarNotasFiscais(notas, unitOfWork);
                }

                unitOfWork.Commit();

                return new OkObjectResult("Salvo com Sucesso");
            }
            catch (Exception e)
            {
                unitOfWork.Rollback();
                return new BadRequestObjectResult(e.Message);
            }
        }

        private void GravarNotasFiscais(Notas notas, IUnitOfWork unitOfWork)
        {
            for (int i = 0; i < notas.ListaNotas.Count(); i++)
            {
                NotaFiscal nota = new NotaFiscal();
                nota.IdEmpresa = notas.IdEmpresa;
                nota.IdNotaFiscal = unitOfWork.NotaFiscalRepository().GetNextValue() + i;
                nota.Data = Convert.ToDateTime(notas.ListaNotas[0].Data);

                for (int j = 0; j < notas.ListaNotas[i].Item.Count(); j++)
                {
                    nota.Item = Convert.ToInt32(notas.ListaNotas[i].Item[j]);
                    nota.Nome = notas.ListaNotas[i].Nome[j];
                    nota.Quantidade = Convert.ToInt32(notas.ListaNotas[i].Quantidade[j]);
                    nota.Preco = Convert.ToInt32(notas.ListaNotas[i].Preco[j]);

                    unitOfWork.NotaFiscalRepository().Add(nota);
                }
            }
        }

        private void GravarNotasDebitos(Notas notas, IUnitOfWork unitOfWork)
        {
            for (int i = 0; i < notas.ListaNotas.Count(); i++)
            {
                Debito nota = new Debito();
                nota.IdEmpresa = notas.IdEmpresa;
                nota.IdDebito = unitOfWork.NotaDebitoRepository().GetNextValue() + i;
                nota.Data = Convert.ToDateTime(notas.ListaNotas[0].Data);

                for (int j = 0; j < notas.ListaNotas[i].Item.Count(); j++)
                {
                    nota.Item = Convert.ToInt32(notas.ListaNotas[i].Item[j]);
                    nota.Nome = notas.ListaNotas[i].Nome[j];
                    nota.Quantidade = Convert.ToInt32(notas.ListaNotas[i].Quantidade[j]);
                    nota.Preco = Convert.ToInt32(notas.ListaNotas[i].Preco[j]);

                    unitOfWork.NotaDebitoRepository().Add(nota);
                }
            }
        }

        public List<Empresa> SelecionarEmpresas([FromServices] IUnitOfWork unitOfWork)
        {
            try
            {// pega a lista das empresas
                List<Empresa> lista = unitOfWork.EmpresaRepository().GetAll();

                for (int i = 0; i < lista.Count; i++)
                {
                    lista[i].classificacao = CalcularClassificacao(lista[i].IdEmpresa, unitOfWork);
                }
                return lista;
            }
            catch (Exception e)
            {
                unitOfWork.Rollback();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Metodo para calcular o porcentual da classificação
        /// </summary>
        private int CalcularClassificacao(int idEmpresa, IUnitOfWork unitOfWork)
        {
            int pontuacao = unitOfWork.NotaFiscalRepository().GetValueNotaFiscal(idEmpresa);
            pontuacao = unitOfWork.NotaDebitoRepository().GetValueDebito(idEmpresa, pontuacao);

            if (pontuacao < 1)
            {
                return 1;
            }
            else if (pontuacao > 100)
            {
                return 100;
            }
            else
            {
                return pontuacao;
            }
        }
        private void ValidarParametros(Notas notas)
        {
            if (notas.IdEmpresa < 1)
                throw new ArgumentException("Campo Identificador de Empresa Inválido");

            if (notas.TipoNota != 1 && notas.TipoNota != 2)
                throw new ArgumentException("Campo Tipo Nota é Inválido");

            if (notas.ListaNotas.Count() < 1)
                throw new ArgumentException("Empresa precisa possuir Uma nota  ou mais");

            if (string.IsNullOrWhiteSpace(notas.ListaNotas[0].Data))
                throw new ArgumentException("Campo Data é Obrigatorio");

            for (int i = 0; i < notas.ListaNotas.Count(); i++)
            {
                if (notas.ListaNotas[i].Item.Count() != notas.ListaNotas[i].Nome.Count()
               || notas.ListaNotas[i].Item.Count() != notas.ListaNotas[i].Preco.Count()
               || notas.ListaNotas[i].Item.Count() != notas.ListaNotas[i].Quantidade.Count())
                {
                    throw new ArgumentException($"{i} Nota Inválido, Valide os Dados.");
                }
            }           
        }
    }
}
