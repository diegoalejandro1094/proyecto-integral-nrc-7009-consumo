using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmRolPermiso : System.Web.UI.Page
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
                ddlCodPermiso.DataSource = servicio.MostrarPermisoTodo();
                ddlCodPermiso.DataValueField = "codigo";
                ddlCodPermiso.DataTextField = "nombre";
                ddlCodPermiso.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvRolPermiso.DataSource = servicio.MostrarRolPermisoTodo(); gvRolPermiso.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new RolPermisoBO();
                obj.codRol = Convert.ToInt32(ddlCodRol.SelectedValue);
                obj.codPermiso = Convert.ToInt32(ddlCodPermiso.SelectedValue);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarRolPermiso(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new RolPermisoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codRol = Convert.ToInt32(ddlCodRol.SelectedValue);
                obj.codPermiso = Convert.ToInt32(ddlCodPermiso.SelectedValue);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarRolPermiso(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new RolPermisoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarRolPermiso(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new RolPermisoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarRolPermiso(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvRolPermiso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvRolPermiso.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[1].Text.Trim())) { try { ddlCodRol.SelectedValue = f.Cells[1].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodPermiso.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                string est = ((f.Cells[3].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodRol.SelectedIndex = 0;
                ddlCodPermiso.SelectedIndex = 0;
            chkEstado.Checked = false;
        }
    }
}