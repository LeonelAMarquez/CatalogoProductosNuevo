using System;
using System.Collections.Generic;
using System.Linq;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> ListarArticulos()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT  a.Id,
                            a.Codigo,
                            a.Nombre,
                            a.Descripcion,
                            a.Precio,
                            m.Descripcion AS Marca,
                            c.Descripcion AS Categoria,
                            i.ImagenUrl
                    FROM ARTICULOS a
                    INNER JOIN MARCAS m      ON a.IdMarca = m.Id
                    INNER JOIN CATEGORIAS c  ON a.IdCategoria = c.Id
                    LEFT JOIN IMAGENES i     ON a.Id = i.IdArticulo
                    ORDER BY a.Id, i.Id;");

                datos.ejecutarLectura();

                var diccionario = new Dictionary<int, Articulo>();

                while (datos.Lector.Read())
                {
                    int id = (int)datos.Lector["Id"];

                    if (!diccionario.ContainsKey(id))
                    {
                        diccionario[id] = new Articulo
                        {
                            Id = id,
                            Codigo = datos.Lector["Codigo"].ToString(),
                            Nombre = datos.Lector["Nombre"].ToString(),
                            Descripcion = datos.Lector["Descripcion"].ToString(),
                            Precio = (decimal)datos.Lector["Precio"],
                            Marca = datos.Lector["Marca"].ToString(),
                            Categoria = datos.Lector["Categoria"].ToString(),
                            Imagenes = new List<string>()
                        };
                    }

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        diccionario[id].Imagenes.Add(datos.Lector["ImagenUrl"].ToString());
                }

                return diccionario.Values.ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Articulo ObtenerArticuloPorId(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
            SELECT  a.Id,
                    a.Codigo,
                    a.Nombre,
                    a.Descripcion,
                    a.Precio,
                    m.Descripcion AS Marca,
                    c.Descripcion AS Categoria,
                    i.ImagenUrl
            FROM ARTICULOS a
            INNER JOIN MARCAS m     ON a.IdMarca = m.Id
            INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id
            LEFT JOIN IMAGENES i    ON a.Id = i.IdArticulo
            WHERE a.Id = @id
            ORDER BY a.Id, i.Id;");

                datos.setearParametro("@id", idArticulo);
                datos.ejecutarLectura();

                Articulo articulo = null;

                while (datos.Lector.Read())
                {
                    if (articulo == null)
                    {
                        articulo = new Articulo
                        {
                            Id = (int)datos.Lector["Id"],
                            Codigo = datos.Lector["Codigo"].ToString(),
                            Nombre = datos.Lector["Nombre"].ToString(),
                            Descripcion = datos.Lector["Descripcion"].ToString(),
                            Precio = (decimal)datos.Lector["Precio"],
                            Marca = datos.Lector["Marca"].ToString(),
                            Categoria = datos.Lector["Categoria"].ToString(),
                            Imagenes = new List<string>()
                        };
                    }

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        articulo.Imagenes.Add(datos.Lector["ImagenUrl"].ToString());
                }

                return articulo;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

