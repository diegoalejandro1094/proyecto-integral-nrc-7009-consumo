using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmSalidaHilo : System.Web.UI.Page
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
                ddlCodProduccion.DataSource = servicio.MostrarProduccionTodo();
                ddlCodProduccion.DataValueField = "codigo";
                ddlCodProduccion.DataTextField = "codigo";
                ddlCodProduccion.DataBind();
                ddlCodTipoHilo.DataSource = servicio.MostrarTipoHiloTodo();
                ddlCodTipoHilo.DataValueField = "codigo";
                ddlCodTipoHilo.DataTextField = "nombre";
                ddlCodTipoHilo.DataBind();
                ddlCodUnidad.DataSource = servicio.MostrarUnidadMedidaTodo();
                ddlCodUnidad.DataValueField = "codigo";
                ddlCodUnidad.DataTextField = "nombre";
                ddlCodUnidad.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvSalidaHilo.DataSource = servicio.MostrarSalidaHiloTodo(); gvSalidaHilo.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new SalidaHiloBO();
                obj.codProduccion = Convert.ToInt32(ddlCodProduccion.SelectedValue);
                obj.codTipoHilo = Convert.ToInt32(ddlCodTipoHilo.SelectedValue);
                obj.codUnidad = Convert.ToInt32(ddlCodUnidad.SelectedValue);
                obj.cantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarSalidaHilo(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new SalidaHiloBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codProduccion = Convert.ToInt32(ddlCodProduccion.SelectedValue);
                obj.codTipoHilo = Convert.ToInt32(ddlCodTipoHilo.SelectedValue);
                obj.codUnidad = Convert.ToInt32(ddlCodUnidad.SelectedValue);
                obj.cantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarSalidaHilo(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new SalidaHiloBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarSalidaHilo(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new SalidaHiloBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarSalidaHilo(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvSalidaHilo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvSalidaHilo.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodProduccion.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[3].Text.Trim())) { try { ddlCodTipoHilo.SelectedValue = f.Cells[3].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[4].Text.Trim())) { try { ddlCodUnidad.SelectedValue = f.Cells[4].Text.Trim(); } catch { } }
                txtCantidad.Text = f.Cells[5].Text.Trim();
                string est = ((f.Cells[6].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodProduccion.SelectedIndex = 0;
                ddlCodTipoHilo.SelectedIndex = 0;
                ddlCodUnidad.SelectedIndex = 0;
                txtCantidad.Text = "";
            chkEstado.Checked = false;
        }
    }
}