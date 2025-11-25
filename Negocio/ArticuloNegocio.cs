using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class ArticuloNegocio
    {
        private ArticuloDatos datos = new ArticuloDatos();

        public List<Articulo> ObtenerArticulos()
        {
            return datos.ListarArticulos();
        }
    }
}
