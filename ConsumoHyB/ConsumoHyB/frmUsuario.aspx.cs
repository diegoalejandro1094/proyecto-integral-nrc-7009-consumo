using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmUsuario : System.Web.UI.Page
    {
        WCFHyBTextilClient servicio = new WCFHyBTextilClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { CargarCombos(); Mostrar(); }
        }

        private void CargarCombos()
        {
            try
            {
                ddlCodRol.DataSource = servicio.MostrarRolTodo();
                ddlCodRol.DataValueField = "codigo";
                ddlCodRol.DataTextField = "nombre";
                ddlCodRol.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvUsuario.DataSource = servicio.MostrarUsuarioTodo(); gvUsuario.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new UsuarioBO();
                obj.nombre = txtNombre.Text;
                obj.usuario = txtUsuario.Text;
                obj.clave = txtClave.Text;
                obj.codRol = Convert.ToInt32(ddlCodRol.SelectedValue);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarUsuario(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new UsuarioBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.nombre = txtNombre.Text;
                obj.usuario = txtUsuario.Text;
                obj.clave = txtClave.Text;
                obj.codRol = Convert.ToInt32(ddlCodRol.SelectedValue);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarUsuario(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new UsuarioBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarUsuario(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new UsuarioBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarUsuario(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvUsuario.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                txtNombre.Text = f.Cells[1].Text.Trim();
                txtUsuario.Text = f.Cells[2].Text.Trim();
                txtClave.Text = f.Cells[3].Text.Trim();
                if (!string.IsNullOrEmpty(f.Cells[4].Text.Trim())) { try { ddlCodRol.SelectedValue = f.Cells[4].Text.Trim(); } catch { } }
                string est = ((f.Cells[5].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtClave.Text = "";
                ddlCodRol.SelectedIndex = 0;
            chkEstado.Checked = false;
        }
    }
}