using ApiControleEmpresa.IRepository;
using ApiControleEmpresa.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace ApiControleEmpresa.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        IEmpresaRepository empresaRepository;
        INotaDebitoRepository notaDebitoRepository;
        INotaFiscalRepository notaFiscalRepository;

        SqlConnection _sqlConn;
        SqlTransaction _sqlTrans;

        public UnitOfWork()
        {
            _sqlConn = new SqlConnection(ConnectionSql());
            _sqlConn.Open();
            _sqlTrans = _sqlConn.BeginTransaction();
        }

        public IEmpresaRepository EmpresaRepository()
        {
            if (empresaRepository == null)
                empresaRepository = new EmpresaRepository(_sqlConn, _sqlTrans);
            return empresaRepository;
        }
        public INotaDebitoRepository NotaDebitoRepository()
        {
            if (notaDebitoRepository == null)
                notaDebitoRepository = new NotaDebitoRepository(_sqlConn, _sqlTrans);
            return notaDebitoRepository;
        }

        public INotaFiscalRepository NotaFiscalRepository()
        {
            if (notaFiscalRepository == null)
                notaFiscalRepository = new NotaFiscalRepository(_sqlConn, _sqlTrans);
            return notaFiscalRepository;
        }

        public void Commit()
        {
            _sqlTrans.Commit();
            _sqlConn.Close();
        }
        public void Rollback()
        {
            try
            {
                _sqlTrans.Rollback();
                _sqlConn.Close();
            }
            catch
            { }
        }
        /// <summary>
        /// Metodo para Obter a Conecção no Appsettings
        /// </summary>
        /// <returns></returns>
        private string ConnectionSql()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

    }
}
