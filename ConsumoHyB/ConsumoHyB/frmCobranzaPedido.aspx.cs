using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmCobranzaPedido : System.Web.UI.Page
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
                ddlCodPedido.DataSource = servicio.MostrarPedidoTodo();
                ddlCodPedido.DataValueField = "codigo";
                ddlCodPedido.DataTextField = "codigo";
                ddlCodPedido.DataBind();
                ddlCodMoneda.DataSource = servicio.MostrarMonedaTodo();
                ddlCodMoneda.DataValueField = "codigo";
                ddlCodMoneda.DataTextField = "nombre";
                ddlCodMoneda.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvCobranzaPedido.DataSource = servicio.MostrarCobranzaPedidoTodo(); gvCobranzaPedido.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new CobranzaPedidoBO();
                obj.codPedido = Convert.ToInt32(ddlCodPedido.SelectedValue);
                obj.codMoneda = Convert.ToInt32(ddlCodMoneda.SelectedValue);
                obj.monto = Convert.ToDecimal(txtMonto.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarCobranzaPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new CobranzaPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codPedido = Convert.ToInt32(ddlCodPedido.SelectedValue);
                obj.codMoneda = Convert.ToInt32(ddlCodMoneda.SelectedValue);
                obj.monto = Convert.ToDecimal(txtMonto.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarCobranzaPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new CobranzaPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarCobranzaPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new CobranzaPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarCobranzaPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvCobranzaPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvCobranzaPedido.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodPedido.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[3].Text.Trim())) { try { ddlCodMoneda.SelectedValue = f.Cells[3].Text.Trim(); } catch { } }
                txtMonto.Text = f.Cells[4].Text.Trim();
                string est = ((f.Cells[5].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodPedido.SelectedIndex = 0;
                ddlCodMoneda.SelectedIndex = 0;
                txtMonto.Text = "";
            chkEstado.Checked = false;
        }
    }
}