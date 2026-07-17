using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmMantenimientoMaquina : System.Web.UI.Page
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
                ddlCodMaquina.DataSource = servicio.MostrarMaquinaTodo();
                ddlCodMaquina.DataValueField = "codigo";
                ddlCodMaquina.DataTextField = "nombre";
                ddlCodMaquina.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvMantenimientoMaquina.DataSource = servicio.MostrarMantenimientoMaquinaTodo(); gvMantenimientoMaquina.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new MantenimientoMaquinaBO();
                obj.codMaquina = Convert.ToInt32(ddlCodMaquina.SelectedValue);
                obj.descripcion = txtDescripcion.Text;
                obj.costo = string.IsNullOrEmpty(txtCosto.Text) ? (decimal?)null : Convert.ToDecimal(txtCosto.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarMantenimientoMaquina(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new MantenimientoMaquinaBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codMaquina = Convert.ToInt32(ddlCodMaquina.SelectedValue);
                obj.descripcion = txtDescripcion.Text;
                obj.costo = string.IsNullOrEmpty(txtCosto.Text) ? (decimal?)null : Convert.ToDecimal(txtCosto.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarMantenimientoMaquina(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new MantenimientoMaquinaBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarMantenimientoMaquina(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new MantenimientoMaquinaBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarMantenimientoMaquina(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvMantenimientoMaquina_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvMantenimientoMaquina.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[1].Text.Trim())) { try { ddlCodMaquina.SelectedValue = f.Cells[1].Text.Trim(); } catch { } }
                txtDescripcion.Text = f.Cells[3].Text.Trim();
                txtCosto.Text = f.Cells[4].Text.Trim();
                string est = ((f.Cells[5].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodMaquina.SelectedIndex = 0;
                txtDescripcion.Text = "";
                txtCosto.Text = "";
            chkEstado.Checked = false;
        }
    }
}