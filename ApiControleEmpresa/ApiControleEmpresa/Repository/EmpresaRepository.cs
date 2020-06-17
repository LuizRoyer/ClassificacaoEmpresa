using ApiControleEmpresa.Entities;
using ApiControleEmpresa.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiControleEmpresa.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        SqlConnection _conn;
        SqlTransaction _trans;
        public EmpresaRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _conn = connection;
            _trans = transaction;
        }
        List<Empresa> IEmpresaRepository.GetAll()
        {
            List<Empresa> empresas = new List<Empresa>();

            string sqlSelect = @"SELECT  IDEMPRESA
                                      ,NOME                                                                     
                                  FROM dbo.EMPRESAS
                                        WHERE 1=1                                            
                                   ORDER BY IDEMPRESA ASC";
            SqlCommand cmd = new SqlCommand(sqlSelect, _conn);
            cmd.Transaction = _trans;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    empresas.Add(new Empresa
                    {
                        IdEmpresa = Convert.ToInt32(reader["IDEMPRESA"].ToString()),
                        Nome = reader["NOME"].ToString(),
                    }
);
                }
            }

            return empresas;
        }


    }
}
