using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmTurno : System.Web.UI.Page
    {
        WCFHyBTextilClient servicio = new WCFHyBTextilClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { Mostrar(); }
        }

        private void Mostrar()
        {
            try { gvTurno.DataSource = servicio.MostrarTurnoTodo(); gvTurno.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
            var obj = new TurnoBO();
                obj.nombre = txtNombre.Text;
                obj.horaInicio = TimeSpan.Parse(txtHoraInicio.Text);
                obj.horaFin = TimeSpan.Parse(txtHoraFin.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarTurno(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
            var obj = new TurnoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.nombre = txtNombre.Text;
                obj.horaInicio = TimeSpan.Parse(txtHoraInicio.Text);
                obj.horaFin = TimeSpan.Parse(txtHoraFin.Text);
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarTurno(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new TurnoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarTurno(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new TurnoBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarTurno(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvTurno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvTurno.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                txtNombre.Text = f.Cells[1].Text;
                txtHoraInicio.Text = f.Cells[2].Text;
                txtHoraFin.Text = f.Cells[3].Text;
                string est = ((f.Cells[4].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            txtNombre.Text = "";
            txtHoraInicio.Text = "";
            txtHoraFin.Text = "";
            chkEstado.Checked = false;
        }
    }
}