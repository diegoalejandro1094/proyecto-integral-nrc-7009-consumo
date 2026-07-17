using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmIngresoHilo : System.Web.UI.Page
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
                ddlCodTipoHilo.DataSource = servicio.MostrarTipoHiloTodo();
                ddlCodTipoHilo.DataValueField = "codigo";
                ddlCodTipoHilo.DataTextField = "nombre";
                ddlCodTipoHilo.DataBind();
                ddlCodAlmacen.DataSource = servicio.MostrarAlmacenTodo();
                ddlCodAlmacen.DataValueField = "codigo";
                ddlCodAlmacen.DataTextField = "nombre";
                ddlCodAlmacen.DataBind();
                ddlCodUnidad.DataSource = servicio.MostrarUnidadMedidaTodo();
                ddlCodUnidad.DataValueField = "codigo";
                ddlCodUnidad.DataTextField = "nombre";
                ddlCodUnidad.DataBind();
                ddlCodCompra.DataSource = servicio.MostrarCompraProveedorTodo();
                ddlCodCompra.DataValueField = "codigo";
                ddlCodCompra.DataTextField = "codigo";
                ddlCodCompra.DataBind();
                ddlCodCompra.Items.Insert(0, new ListItem("(Ninguno)", ""));
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvIngresoHilo.DataSource = servicio.MostrarIngresoHiloTodo(); gvIngresoHilo.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new IngresoHiloBO();
                obj.codTipoHilo = Convert.ToInt32(ddlCodTipoHilo.SelectedValue);
                obj.codAlmacen = Convert.ToInt32(ddlCodAlmacen.SelectedValue);
                obj.codUnidad = Convert.ToInt32(ddlCodUnidad.SelectedValue);
                obj.codCompra = string.IsNullOrEmpty(ddlCodCompra.SelectedValue) ? (int?)null : Convert.ToInt32(ddlCodCompra.SelectedValue);
                obj.cantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarIngresoHilo(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new IngresoHiloBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codTipoHilo = Convert.ToInt32(ddlCodTipoHilo.SelectedValue);
                obj.codAlmacen = Convert.ToInt32(ddlCodAlmacen.SelectedValue);
                obj.codUnidad = Convert.ToInt32(ddlCodUnidad.SelectedValue);
                obj.codCompra = string.IsNullOrEmpty(ddlCodCompra.SelectedValue) ? (int?)null : Convert.ToInt32(ddlCodCompra.SelectedValue);
                obj.cantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarIngresoHilo(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new IngresoHiloBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarIngresoHilo(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new IngresoHiloBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarIngresoHilo(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvIngresoHilo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvIngresoHilo.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodTipoHilo.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[3].Text.Trim())) { try { ddlCodAlmacen.SelectedValue = f.Cells[3].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[4].Text.Trim())) { try { ddlCodUnidad.SelectedValue = f.Cells[4].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[5].Text.Trim())) { try { ddlCodCompra.SelectedValue = f.Cells[5].Text.Trim(); } catch { } }
                txtCantidad.Text = f.Cells[6].Text.Trim();
                string est = ((f.Cells[7].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodTipoHilo.SelectedIndex = 0;
                ddlCodAlmacen.SelectedIndex = 0;
                ddlCodUnidad.SelectedIndex = 0;
                ddlCodCompra.SelectedIndex = 0;
                txtCantidad.Text = "";
            chkEstado.Checked = false;
        }
    }
}