using ConsumoHyB.ServicioHyB;   // <-- nombre de la Service Reference (debe llamarse ServicioHyB)
using System;
//using pe.com.hyb.bo;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmRol : System.Web.UI.Page
    {
        // Cliente del servicio WCF (declarado global)
        WCFHyBTextilClient servicio = new WCFHyBTextilClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarRol();
            }
        }

        private void MostrarRol()
        {
            try
            {
                var roles = servicio.MostrarRolTodo();
                gvRol.DataSource = roles;
                gvRol.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var objRol = new RolBO();
                objRol.nombre = txtNombre.Text;
                objRol.estado = chkEstado.Checked;

                bool res = servicio.RegistrarRol(objRol);
                if (res)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Se registro el Rol');", true);
                    MostrarRol();
                    Limpiar();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No se registro el Rol');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var objRol = new RolBO();
                objRol.codigo = Convert.ToInt32(txtCod.Text);
                objRol.nombre = txtNombre.Text;
                objRol.estado = chkEstado.Checked;

                bool res = servicio.ActualizarRol(objRol);
                if (res)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Se actualizo el Rol');", true);
                    MostrarRol();
                    Limpiar();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No se actualizo el Rol');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var objRol = new RolBO();
                objRol.codigo = Convert.ToInt32(txtCod.Text);

                bool res = servicio.EliminarRol(objRol);
                if (res)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Se elimino el Rol');", true);
                    MostrarRol();
                    Limpiar();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No se elimino el Rol');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var objRol = new RolBO();
                objRol.codigo = Convert.ToInt32(txtCod.Text);

                bool res = servicio.HabilitarRol(objRol);
                if (res)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Se habilito el Rol');", true);
                    MostrarRol();
                    Limpiar();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No se habilito el Rol');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void gvRol_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = gvRol.Rows[index];
                txtCod.Text = fila.Cells[0].Text;
                txtNombre.Text = fila.Cells[1].Text;
                string estadoTexto = ((fila.Cells[2].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = estadoTexto.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            txtNombre.Text = "";
            chkEstado.Checked = false;
        }
    }
}
