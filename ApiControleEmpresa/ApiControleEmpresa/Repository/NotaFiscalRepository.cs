using ApiControleEmpresa.Entities;
using ApiControleEmpresa.IRepository;
using System;
using System.Data.SqlClient;

namespace ApiControleEmpresa.Repository
{
    public class NotaFiscalRepository: INotaFiscalRepository
    {
        SqlConnection _conn;
        SqlTransaction _trans;
        public NotaFiscalRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _conn = connection;
            _trans = transaction;
        }
        public void Add(NotaFiscal obj)
        {

            string sqlInsert = @"INSERT INTO dbo.NotaFiscais
                                    (idnotafiscal, item, idempresa, nome, quantidade, preco, data)
                                VALUES
                                    (@idnotafiscal, @item, @idempresa ,@nome, @quantidade, @preco, @data)";

            SqlCommand cmd = new SqlCommand(sqlInsert, _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@idnotafiscal", obj.IdNotaFiscal));
            cmd.Parameters.Add(new SqlParameter("@item", obj.Item));
            cmd.Parameters.Add(new SqlParameter("@idempresa", obj.IdEmpresa));
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@quantidade", obj.Quantidade));
            cmd.Parameters.Add(new SqlParameter("@preco", obj.Preco));
            cmd.Parameters.Add(new SqlParameter("@data", obj.Data));

            cmd.ExecuteNonQuery();
        }

        public int GetValueNotaFiscal(int idEmpresa)
        {
            string sqlSelect = @"SELECT CASE
                                           WHEN COUNT(idNotaFiscal) >0 
                                                THEN CEILING((50 *CAST((COUNT( distinct idNotaFiscal)*2) AS numeric(20, 0))/100) +50)
                                       END AS somaNota
                                FROM NotaFiscais
                                WHERE idEmpresa =@empresa
                                GROUP BY idEmpresa";
            SqlCommand cmd = new SqlCommand(sqlSelect, _conn);
            cmd.Transaction = _trans;      
            cmd.Parameters.Add(new SqlParameter("@empresa", idEmpresa));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return Convert.ToInt32(reader["SOMANOTA"].ToString());
                }
                return 50;
            }
        }

        public int GetNextValue()
        {
            string sqlSelect = @"SELECT  COUNT(idNotaFiscal)+1 
                                        AS idNota
                                FROM NotaFiscais
                                WHERE 1=1";
            SqlCommand cmd = new SqlCommand(sqlSelect, _conn);
            cmd.Transaction = _trans;
         
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return Convert.ToInt32(reader["idNota"].ToString());
                }
                return 1;
            }
        }
    }
}
