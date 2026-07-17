using ConsumoHyB.ServicioHyB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmProveedor : System.Web.UI.Page
    {
        WCFHyBTextilClient servicio = new WCFHyBTextilClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { Mostrar(); }
        }

        private void Mostrar()
        {
            try { gvProveedor.DataSource = servicio.MostrarProveedorTodo(); gvProveedor.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
            var obj = new ProveedorBO();
                obj.ruc = txtRuc.Text;
                obj.razonSocial = txtRazonSocial.Text;
                obj.nombreContacto = txtNombreContacto.Text;
                obj.telefono = txtTelefono.Text;
                obj.email = txtEmail.Text;
                obj.direccion = txtDireccion.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.RegistrarProveedor(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
            var obj = new ProveedorBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                obj.ruc = txtRuc.Text;
                obj.razonSocial = txtRazonSocial.Text;
                obj.nombreContacto = txtNombreContacto.Text;
                obj.telefono = txtTelefono.Text;
                obj.email = txtEmail.Text;
                obj.direccion = txtDireccion.Text;
                obj.estado = chkEstado.Checked;
                bool res = servicio.ActualizarProveedor(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new ProveedorBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarProveedor(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new ProveedorBO();
                obj.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarProveedor(obj);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Mostrar(); Limpiar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvProveedor.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                txtRuc.Text = f.Cells[1].Text;
                txtRazonSocial.Text = f.Cells[2].Text;
                txtNombreContacto.Text = f.Cells[3].Text;
                txtTelefono.Text = f.Cells[4].Text;
                txtEmail.Text = f.Cells[5].Text;
                txtDireccion.Text = f.Cells[6].Text;
                string est = ((f.Cells[7].Controls[0] as DataBoundLiteralControl).Text).Trim();
                chkEstado.Checked = est.Equals("Habilitado");
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            txtRuc.Text = "";
            txtRazonSocial.Text = "";
            txtNombreContacto.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
            chkEstado.Checked = false;
        }
    }
}