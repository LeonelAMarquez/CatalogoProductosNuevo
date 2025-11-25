using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using Dominio;
using Negocio;

namespace CatalogoProductosNuevo
{
    public partial class Formulario : Page
    {
        private readonly ClienteNegocio _clienteNegocio = new ClienteNegocio();

        private static readonly Regex RxDni = new Regex(@"^\d{7,12}$");
        private static readonly Regex RxNombre = new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' -]{2,40}$");
        private static readonly Regex RxApellido = new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' -]{2,40}$");
        private static readonly Regex RxDir = new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ0-9°º\.\,#\- ]{5,80}$");
        private static readonly Regex RxCiudad = new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' -]{2,50}$");
        private static readonly Regex RxCp = new Regex(@"^\d{4,10}$");
        private static readonly Regex RxEmail = new Regex(@"^(?=.{1,254}$)(?=.{1,64}@)(?!.*\.\.)[A-Za-z0-9!#$%&'*+/=?^_{}|~\-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_{}|~\-]+)*@(?:(?=[A-Za-z0-9\-]{1,63}\.)[A-Za-z0-9](?:[A-Za-z0-9\-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,24}$");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void txtDni_TextChanged(object sender, EventArgs e)
        {
            litError.Text = string.Empty;

            string documento = txtDni.Text.Trim();
            if (string.IsNullOrWhiteSpace(documento))
                return;

            Cliente cli = _clienteNegocio.ObtenerPorDocumento(documento); 

            if (cli != null)
            {
                txtNombre.Text = cli.Nombre;
                txtApellido.Text = cli.Apellido;
                txtEmail.Text = cli.Email;
                txtDireccion.Text = cli.Direccion;
                txtCiudad.Text = cli.Ciudad;
                txtCp.Text = cli.CP.ToString();
            }
        }

        protected void btnParticipar_Click(object sender, EventArgs e)
        {
            litError.Text = string.Empty;

            if (!Page.IsValid)
                return;

            if (!CamposValidosServidor())
                return;

            string documento = txtDni.Text.Trim();
            Cliente existente = _clienteNegocio.ObtenerPorDocumento(documento);

            if (existente == null)
            {
                int cpVal;
                int.TryParse(txtCp.Text.Trim(), out cpVal);

                var c = new Cliente
                {
                    Documento = documento,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Ciudad = txtCiudad.Text.Trim(),
                    CP = cpVal
                };

                int id = _clienteNegocio.RegistrarCliente(c);  

                if (id <= 0)
                {
                    litError.Text = "<div class='alert alert-danger mt-3'>No pudimos registrar el cliente.</div>";
                    return;
                }

                try
                {
                    var mail = new ServicioEmailNegocio();
                    string html = $"<h2>¡Gracias, {Server.HtmlEncode(c.Nombre)}!</h2><p>Ya estás inscripto a la promo.</p>";
                    mail.Enviar(c.Email, "Registro exitoso - TuElectro", html);
                }
                catch
                {
                    
                }
            }

            Response.Redirect("~/Exito.aspx?nombre=" + Server.UrlEncode(txtNombre.Text.Trim()), false);
        }

        private bool CamposValidosServidor()
        {
            string dni = txtDni.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string email = txtEmail.Text.Trim();
            string dir = txtDireccion.Text.Trim();
            string ciudad = txtCiudad.Text.Trim();
            string cp = txtCp.Text.Trim();

            if (!RxDni.IsMatch(dni)) return ErrorOut("DNI inválido.");
            if (!RxNombre.IsMatch(nombre)) return ErrorOut("Nombre inválido.");
            if (!RxApellido.IsMatch(apellido)) return ErrorOut("Apellido inválido.");
            if (!RxDir.IsMatch(dir)) return ErrorOut("Dirección inválida.");
            if (!RxCiudad.IsMatch(ciudad)) return ErrorOut("Ciudad inválida.");
            if (!RxCp.IsMatch(cp)) return ErrorOut("CP inválido.");
            if (!EsEmailValido(email) || !RxEmail.IsMatch(email)) return ErrorOut("Email inválido.");

            return true;
        }

        private bool ErrorOut(string msg)
        {
            litError.Text = $"<div class='alert alert-danger mt-3'>{Server.HtmlEncode(msg)}</div>";
            return false;
        }

        private bool EsEmailValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;
            correo = correo.Trim();
            if (correo.EndsWith(".")) return false;
            if (correo.Length > 254) return false;

            var partes = correo.Split('@');
            if (partes.Length != 2) return false;
            if (partes[0].Length == 0 || partes[0].Length > 64) return false;
            if (!partes[1].Contains(".")) return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(correo);
                if (!string.Equals(addr.Address, correo, StringComparison.Ordinal)) return false;
            }
            catch
            {
                return false;
            }

            if (correo.Contains("..")) return false;

            foreach (var label in partes[1].Split('.'))
            {
                if (label.Length == 0) return false;
                if (label.StartsWith("-") || label.EndsWith("-")) return false;
                if (label.Length > 63) return false;
            }

            return true;
        }
    }
}
