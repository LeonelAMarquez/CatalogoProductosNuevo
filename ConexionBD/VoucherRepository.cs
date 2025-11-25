using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBD
{
    public class VoucherRepository
    {
        private readonly string _cs;

        public VoucherRepository(string connectionString)
        {
            _cs = connectionString;
        }

        public Voucher GetByCodigo(string codigo)
        {
            const string sql = @"SELECT CodigoVoucher, IdCliente, FechaCanje, IdArticulo 
                                 FROM Vouchers 
                                 WHERE CodigoVoucher=@codigo";

            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;

                    return new Voucher
                    {
                        CodigoVoucher = rd.GetString(0),
                        IdCliente = rd.IsDBNull(1) ? (int?)null : rd.GetInt32(1),
                        FechaCanje = rd.IsDBNull(2) ? (DateTime?)null : rd.GetDateTime(2),
                        IdArticulo = rd.IsDBNull(3) ? (int?)null : rd.GetInt32(3)
                    };
                }
            }
        }

        public bool EstaDisponible(string codigo)
        {
            const string sql = @"SELECT COUNT(*) 
                                 FROM Vouchers 
                                 WHERE CodigoVoucher=@codigo AND IdCliente IS NULL";

            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public void CanjearVoucher(string codigo, int idCliente, int idArticulo)
        {
            const string sql = @"UPDATE Vouchers
                                 SET IdCliente=@idCliente, IdArticulo=@idArticulo, FechaCanje=GETDATE()
                                 WHERE CodigoVoucher=@codigo";

            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@idArticulo", idArticulo);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
