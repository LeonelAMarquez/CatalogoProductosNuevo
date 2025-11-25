using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace CatalogoProductosNuevo
{
    public partial class EleccionPremio : Page
    {
        private readonly ArticuloNegocio _articuloNegocio = new ArticuloNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarArticulos();
            }
        }

        private void CargarArticulos()
        {
            List<Articulo> articulos = _articuloNegocio.ListarArticulos();

            RepeaterArticulos.ItemDataBound += RepeaterArticulos_ItemDataBound;

            RepeaterArticulos.DataSource = articulos;
            RepeaterArticulos.DataBind();
        }

        protected void RepeaterArticulos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var articulo = (Articulo)e.Item.DataItem;

                var repImagenes = (Repeater)e.Item.FindControl("RepeaterImagenes");
                if (repImagenes != null)
                {
                    repImagenes.DataSource = articulo.Imagenes;
                    repImagenes.DataBind();
                }
            }
        }
    }
}
