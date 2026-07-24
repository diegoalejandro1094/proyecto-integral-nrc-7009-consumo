using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;

namespace ConsumoHyB
{
    public partial class frmLogin : System.Web.UI.Page
    {
        WCFHyBTextilClient servicio = new WCFHyBTextilClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Si ya hay sesión activa, redirigir directo al inicio
            if (Session["usuario"] != null)
            {
                Response.Redirect("Inicio.aspx");
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave   = txtClave.Text.Trim();

            // Validación básica de campos vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MostrarError("Por favor, ingresa tu usuario y contraseña.");
                return;
            }

            try
            {
                // Llamar al servicio WCF para autenticar
                UsuarioBO param = new UsuarioBO
                {
                    usuario = usuario,
                    clave   = clave
                };

                UsuarioBO resultado = servicio.LoginUsuario(param);

                if (resultado != null)
                {
                    // Guardar datos del usuario en sesión
                    Session["usuario"]    = resultado;
                    Session["nomUsuario"] = resultado.nombre;
                    Session["codRol"]     = resultado.codRol;

                    // Redirigir al inicio
                    Response.Redirect("Inicio.aspx");
                }
                else
                {
                    MostrarError("Usuario o contraseña incorrectos. Verifica tus credenciales.");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al conectar con el servicio. Intenta de nuevo más tarde.");
                System.Diagnostics.Debug.WriteLine("Error login: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            pnlError.Visible = true;
            lblError.Text    = mensaje;
        }
    }
}
