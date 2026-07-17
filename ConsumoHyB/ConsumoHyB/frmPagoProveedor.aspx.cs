using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmPagoProveedor : System.Web.UI.Page
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
                ddlCodCompra.DataSource = servicio.MostrarCompraProveedorTodo();
                ddlCodCompra.DataValueField = "codigo";
                ddlCodCompra.DataTextField = "codigo";
                ddlCodCompra.DataBind();
                ddlCodMoneda.DataSource = servicio.MostrarMonedaTodo();
                ddlCodMoneda.DataValueField = "codigo";
                ddlCodMoneda.DataTextField = "nombre";
                ddlCodMoneda.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvPagoProveedor.DataSource = servicio.MostrarPagoProveedorTodo(); gvPagoProveedor.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new PagoProveedorBO();
                obj.codCompra = Convert.ToInt32(ddlCodCompra.SelectedValue);
                obj.codMoneda = Convert.ToInt32(ddlCodMoneda.SelectedValue);
                obj.monto = Convert.ToDecimal(txtMonto.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarPagoProveedor(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new PagoProveedorBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codCompra = Convert.ToInt32(ddlCodCompra.SelectedValue);
                obj.codMoneda = Convert.ToInt32(ddlCodMoneda.SelectedValue);
                obj.monto = Convert.ToDecimal(txtMonto.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarPagoProveedor(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new PagoProveedorBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarPagoProveedor(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new PagoProveedorBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarPagoProveedor(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvPagoProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvPagoProveedor.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodCompra.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[3].Text.Trim())) { try { ddlCodMoneda.SelectedValue = f.Cells[3].Text.Trim(); } catch { } }
                txtMonto.Text = f.Cells[4].Text.Trim();
                string est = ((f.Cells[5].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodCompra.SelectedIndex = 0;
                ddlCodMoneda.SelectedIndex = 0;
                txtMonto.Text = "";
            chkEstado.Checked = false;
        }
    }
}