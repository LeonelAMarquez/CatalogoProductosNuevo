using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace ConexionBD
{
    public class AccesoDatos
    {
        private readonly string _cs;

        public AccesoDatos(string connectionString) => _cs = connectionString;

        public Cliente GetByDocumento(string documento)
        {
            const string sql = @"SELECT Id,Documento,Nombre,Apellido,Email,Direccion,Ciudad,CP FROM Clientes WHERE Documento=@doc";
            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@doc", documento);
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Cliente
                    {
                        Id = rd.GetInt32(0),
                        Documento = rd.GetString(1),
                        Nombre = rd.GetString(2),
                        Apellido = rd.GetString(3),
                        Email = rd.GetString(4),
                        Direccion = rd.GetString(5),
                        Ciudad = rd.GetString(6),
                        CP = rd.GetInt32(7)
                    };
                }
            }
        }

        public int Insert(Cliente c)
        {
            const string sql = @"INSERT INTO Clientes (Documento,Nombre,Apellido,Email,Direccion,Ciudad,CP) VALUES (@Documento,@Nombre,@Apellido,@Email,@Direccion,@Ciudad,@CP); SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Documento", c.Documento);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@Ciudad", c.Ciudad);
                cmd.Parameters.AddWithValue("@CP", c.CP);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
