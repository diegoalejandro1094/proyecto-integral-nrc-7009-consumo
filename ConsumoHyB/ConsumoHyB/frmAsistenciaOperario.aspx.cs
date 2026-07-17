using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmAsistenciaOperario : System.Web.UI.Page
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
                ddlCodOperario.DataSource = servicio.MostrarOperarioTodo();
                ddlCodOperario.DataValueField = "codigo";
                ddlCodOperario.DataTextField = "nombre";
                ddlCodOperario.DataBind();
                ddlCodTurno.DataSource = servicio.MostrarTurnoTodo();
                ddlCodTurno.DataValueField = "codigo";
                ddlCodTurno.DataTextField = "nombre";
                ddlCodTurno.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private void Mostrar()
        {
            try { gvAsistenciaOperario.DataSource = servicio.MostrarAsistenciaOperarioTodo(); gvAsistenciaOperario.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new AsistenciaOperarioBO();
                obj.codOperario = Convert.ToInt32(ddlCodOperario.SelectedValue);
                obj.codTurno = Convert.ToInt32(ddlCodTurno.SelectedValue);
                obj.fecha = txtFecha.Text;
                obj.horaIngreso = txtHoraIngreso.Text;
                obj.horaSalida = txtHoraSalida.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarAsistenciaOperario(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new AsistenciaOperarioBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.codOperario = Convert.ToInt32(ddlCodOperario.SelectedValue);
                obj.codTurno = Convert.ToInt32(ddlCodTurno.SelectedValue);
                obj.fecha = txtFecha.Text;
                obj.horaIngreso = txtHoraIngreso.Text;
                obj.horaSalida = txtHoraSalida.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarAsistenciaOperario(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new AsistenciaOperarioBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarAsistenciaOperario(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new AsistenciaOperarioBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarAsistenciaOperario(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvAsistenciaOperario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvAsistenciaOperario.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                if (!string.IsNullOrEmpty(f.Cells[1].Text.Trim())) { try { ddlCodOperario.SelectedValue = f.Cells[1].Text.Trim(); } catch { } }
                if (!string.IsNullOrEmpty(f.Cells[2].Text.Trim())) { try { ddlCodTurno.SelectedValue = f.Cells[2].Text.Trim(); } catch { } }
                txtFecha.Text = f.Cells[3].Text.Trim();
                txtHoraIngreso.Text = f.Cells[4].Text.Trim();
                txtHoraSalida.Text = f.Cells[5].Text.Trim();
                string est = ((f.Cells[6].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
                ddlCodOperario.SelectedIndex = 0;
                ddlCodTurno.SelectedIndex = 0;
                txtFecha.Text = "";
                txtHoraIngreso.Text = "";
                txtHoraSalida.Text = "";
            chkEstado.Checked = false;
        }
    }
}