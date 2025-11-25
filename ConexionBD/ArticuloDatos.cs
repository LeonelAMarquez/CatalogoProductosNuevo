using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace ConexionBD
{
    public class ArticuloDatos
    {
        private string connectionString =
        ConfigurationManager.ConnectionStrings["PromosDB"].ConnectionString;

        public List<Articulo> ListarArticulos()
        {
            List<Articulo> lista = new List<Articulo>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT a.Id, a.Nombre, a.Descripcion, i.ImagenUrl
                FROM ARTICULOS a
                INNER JOIN IMAGENES i ON a.Id = i.IdArticulo
                ORDER BY a.Id, i.Id;";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                var dict = new Dictionary<int, Articulo>();

                while (reader.Read())
                {
                    int id = (int)reader["Id"];

                    if (!dict.ContainsKey(id))
                    {
                        dict[id] = new Articulo
                        {
                            Id = id,
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Imagenes = new List<string>()
                        };
                    }

                    dict[id].Imagenes.Add(reader["ImagenUrl"].ToString());
                }

                lista = dict.Values.ToList();
            }

            return lista;
        }
    }
}
