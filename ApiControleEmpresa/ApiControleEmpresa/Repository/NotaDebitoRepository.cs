using ApiControleEmpresa.Entities;
using ApiControleEmpresa.IRepository;
using System;
using System.Data.SqlClient;

namespace ApiControleEmpresa.Repository
{
    public class NotaDebitoRepository : INotaDebitoRepository
    {
        SqlConnection _conn;
        SqlTransaction _trans;
        public NotaDebitoRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _conn = connection;
            _trans = transaction;
        }
        public void Add(Debito obj)
        {

            string sqlInsert = @"INSERT INTO dbo.Debito
                                    (iddebito, item, idempresa, nome, quantidade, preco, data)
                                VALUES
                                    (@iddebito, @item, @idempresa ,@nome, @quantidade, @preco, @data)";

            SqlCommand cmd = new SqlCommand(sqlInsert, _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@iddebito", obj.IdDebito));
            cmd.Parameters.Add(new SqlParameter("@item", obj.Item));
            cmd.Parameters.Add(new SqlParameter("@idempresa", obj.IdEmpresa));
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@quantidade", obj.Quantidade));
            cmd.Parameters.Add(new SqlParameter("@preco", obj.Preco));
            cmd.Parameters.Add(new SqlParameter("@data", obj.Data));

            cmd.ExecuteNonQuery();
        }

        public int GetValueDebito(int idEmpresa, int valor)
        {
            string sqlSelect = @"SELECT CASE                                         
                                           WHEN COUNT(idDebito) >0 THEN
                                                FLOOR (@valor -(@valor *CAST((COUNT( distinct idDebito)*4) AS NUMERIC(20, 0))/100))
                                       END AS somaNota
                                FROM debito
                                     WHERE idEmpresa =@empresa
                                GROUP BY idEmpresa;

";
            SqlCommand cmd = new SqlCommand(sqlSelect, _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@valor", valor));
            cmd.Parameters.Add(new SqlParameter("@empresa", idEmpresa));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return Convert.ToInt32(reader["SOMANOTA"].ToString());                     
                }
                return valor;
               
            }
        }

        public int GetNextValue()
        {
            string sqlSelect = @"SELECT  COUNT(idDebito)+1 
                                        AS idNota
                                FROM debito
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
