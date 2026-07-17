using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmDocumentoPedido : System.Web.UI.Page
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
                ddlCodTipoDocumento.DataSource = servicio.MostrarTipoDocumentoTodo();
                ddlCodTipoDocumento.DataValueField = "codigo";
                ddlCodTipoDocumento.DataTextField = "nombre";
                ddlCodTipoDocumento.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvDocumentoPedido.DataSource = servicio.MostrarDocumentoPedidoTodo(); gvDocumentoPedido.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new DocumentoPedidoBO();
                obj.codPedido = Convert.ToInt32(ddlCodPedido.SelectedValue);
                obj.codTipoDocumento = Convert.ToInt32(ddlCodTipoDocumento.SelectedValue);
                obj.numero = txtNumero.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarDocumentoPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new DocumentoPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codPedido = Convert.ToInt32(ddlCodPedido.SelectedValue);
                obj.codTipoDocumento = Convert.ToInt32(ddlCodTipoDocumento.SelectedValue);
                obj.numero = txtNumero.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarDocumentoPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new DocumentoPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarDocumentoPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new DocumentoPedidoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarDocumentoPedido(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvDocumentoPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvDocumentoPedido.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[1].Text.Trim())) { try { ddlCodPedido.SelectedValue = f.Cells[1].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodTipoDocumento.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                txtNumero.Text = f.Cells[3].Text.Trim();
                string est = ((f.Cells[5].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodPedido.SelectedIndex = 0;
                ddlCodTipoDocumento.SelectedIndex = 0;
                txtNumero.Text = "";
            chkEstado.Checked = false;
        }
    }
}