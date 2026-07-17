using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmHistorialEstadoPedido : System.Web.UI.Page
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
                ddlCodEstado.DataSource = servicio.MostrarEstadoPedidoTodo();
                ddlCodEstado.DataValueField = "codigo";
                ddlCodEstado.DataTextField = "nombre";
                ddlCodEstado.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvHistorialEstadoPedido.DataSource = servicio.MostrarHistorialEstadoPedidoTodo(); gvHistorialEstadoPedido.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new HistorialEstadoPedidoBO();
                obj.codPedido = Convert.ToInt32(ddlCodPedido.SelectedValue);
                obj.codEstado = Convert.ToInt32(ddlCodEstado.SelectedValue);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarHistorialEstadoPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new HistorialEstadoPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codPedido = Convert.ToInt32(ddlCodPedido.SelectedValue);
                obj.codEstado = Convert.ToInt32(ddlCodEstado.SelectedValue);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarHistorialEstadoPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new HistorialEstadoPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarHistorialEstadoPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new HistorialEstadoPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarHistorialEstadoPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvHistorialEstadoPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvHistorialEstadoPedido.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[1].Text.Trim())) { try { ddlCodPedido.SelectedValue = f.Cells[1].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodEstado.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                string est = ((f.Cells[4].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodPedido.SelectedIndex = 0;
                ddlCodEstado.SelectedIndex = 0;
            chkEstado.Checked = false;
        }
    }
}