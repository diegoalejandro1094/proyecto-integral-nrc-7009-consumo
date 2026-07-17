using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmProduccion : System.Web.UI.Page
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
                ddlCodOperario.DataSource = servicio.MostrarOperarioTodo();
                ddlCodOperario.DataValueField = "codigo";
                ddlCodOperario.DataTextField = "nombre";
                ddlCodOperario.DataBind();
                ddlCodTipoTejido.DataSource = servicio.MostrarTipoTejidoTodo();
                ddlCodTipoTejido.DataValueField = "codigo";
                ddlCodTipoTejido.DataTextField = "nombre";
                ddlCodTipoTejido.DataBind();
                ddlCodPedido.DataSource = servicio.MostrarPedidoTodo();
                ddlCodPedido.DataValueField = "codigo";
                ddlCodPedido.DataTextField = "codigo";
                ddlCodPedido.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvProduccion.DataSource = servicio.MostrarProduccionTodo(); gvProduccion.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new ProduccionBO();
                obj.codMaquina = Convert.ToInt32(ddlCodMaquina.SelectedValue);
                obj.codOperario = Convert.ToInt32(ddlCodOperario.SelectedValue);
                obj.codTipoTejido = Convert.ToInt32(ddlCodTipoTejido.SelectedValue);
                obj.codPedido = Convert.ToInt32(ddlCodPedido.SelectedValue);
                obj.cantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarProduccion(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new ProduccionBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codMaquina = Convert.ToInt32(ddlCodMaquina.SelectedValue);
                obj.codOperario = Convert.ToInt32(ddlCodOperario.SelectedValue);
                obj.codTipoTejido = Convert.ToInt32(ddlCodTipoTejido.SelectedValue);
                obj.codPedido = Convert.ToInt32(ddlCodPedido.SelectedValue);
                obj.cantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarProduccion(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new ProduccionBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarProduccion(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new ProduccionBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarProduccion(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvProduccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvProduccion.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodMaquina.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[3].Text.Trim())) { try { ddlCodOperario.SelectedValue = f.Cells[3].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[4].Text.Trim())) { try { ddlCodTipoTejido.SelectedValue = f.Cells[4].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[5].Text.Trim())) { try { ddlCodPedido.SelectedValue = f.Cells[5].Text.Trim(); } catch { } }
                txtCantidad.Text = f.Cells[6].Text.Trim();
                string est = ((f.Cells[7].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodMaquina.SelectedIndex = 0;
                ddlCodOperario.SelectedIndex = 0;
                ddlCodTipoTejido.SelectedIndex = 0;
                ddlCodPedido.SelectedIndex = 0;
                txtCantidad.Text = "";
            chkEstado.Checked = false;
        }
    }
}