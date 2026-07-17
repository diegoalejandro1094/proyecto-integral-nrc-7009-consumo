using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmMoneda : System.Web.UI.Page
    {
        WCFHyBTextilClient servicio = new WCFHyBTextilClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { Mostrar(); }
        }

        private void Mostrar()
        {
            try { gvMoneda.DataSource = servicio.MostrarMonedaTodo(); gvMoneda.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
            var obj = new MonedaBO();
                obj.nombre = txtNombre.Text;
                obj.simbolo = txtSimbolo.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarMoneda(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
            var obj = new MonedaBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.nombre = txtNombre.Text;
                obj.simbolo = txtSimbolo.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarMoneda(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new MonedaBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarMoneda(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new MonedaBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarMoneda(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvMoneda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvMoneda.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                txtNombre.Text = f.Cells[1].Text;
                txtSimbolo.Text = f.Cells[2].Text;
                string est = ((f.Cells[3].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            txtNombre.Text = "";
            txtSimbolo.Text = "";
            chkEstado.Checked = false;
        }
    }
}