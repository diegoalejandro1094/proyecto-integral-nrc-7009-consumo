using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmCliente : System.Web.UI.Page
    {
        WCFHyBTextilClient servicio = new WCFHyBTextilClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { Mostrar(); }
        }

        private void Mostrar()
        {
            try { gvCliente.DataSource = servicio.MostrarClienteTodo(); gvCliente.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
            var obj = new ClienteBO();
                obj.ruc = txtRuc.Text;
                obj.razonSocial = txtRazonSocial.Text;
                obj.nombreContacto = txtNombreContacto.Text;
                obj.telefono = txtTelefono.Text;
                obj.email = txtEmail.Text;
                obj.direccion = txtDireccion.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarCliente(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
            var obj = new ClienteBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.ruc = txtRuc.Text;
                obj.razonSocial = txtRazonSocial.Text;
                obj.nombreContacto = txtNombreContacto.Text;
                obj.telefono = txtTelefono.Text;
                obj.email = txtEmail.Text;
                obj.direccion = txtDireccion.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarCliente(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new ClienteBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarCliente(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new ClienteBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarCliente(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvCliente.Rows[i];
                txtCod.Text = Server.HtmlDecode(f.Cells[0].Text);
                txtRuc.Text = Server.HtmlDecode(f.Cells[1].Text);
                txtRazonSocial.Text = Server.HtmlDecode(f.Cells[2].Text);
                txtNombreContacto.Text = Server.HtmlDecode(f.Cells[3].Text);
                txtTelefono.Text = Server.HtmlDecode(f.Cells[4].Text);
                txtEmail.Text = Server.HtmlDecode(f.Cells[5].Text);
                txtDireccion.Text = Server.HtmlDecode(f.Cells[6].Text);
                string est = ((f.Cells[7].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            txtRuc.Text = "";
            txtRazonSocial.Text = "";
            txtNombreContacto.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
            chkEstado.Checked = false;
        }
    }
}