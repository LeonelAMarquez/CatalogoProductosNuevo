using System;
using System.Web.UI;
using Dominio;
using Negocio;

namespace CatalogoProductosNuevo
{
    public partial class Voucher : Page
    {
        private readonly VoucherNegocio _voucherNegocio = new VoucherNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                lblMensaje.Text = "Ingrese un código de voucher.";
                return;
            }

            Dominio.Voucher miVoucher = _voucherNegocio.ObtenerPorCodigo(codigo);

            if (miVoucher == null)
            {
                lblMensaje.Text = "El código ingresado no existe.";
            }
            else if (!_voucherNegocio.EstaDisponible(codigo))
            {
                lblMensaje.Text = "El voucher ya fue utilizado.";
            }
            else
            {
                Response.Redirect("EleccionPremio.aspx?codigo=" + Server.UrlEncode(codigo));
            }
        }
    }
}
