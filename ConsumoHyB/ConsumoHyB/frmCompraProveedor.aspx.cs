using ConsumoHyB.ServicioHyB;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumoHyB
{
    public partial class frmCompraProveedor : System.Web.UI.Page
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
                ddlCodProveedor.DataSource = servicio.MostrarProveedorTodo();
                ddlCodProveedor.DataValueField = "codigo"; ddlCodProveedor.DataTextField = "razonSocial"; ddlCodProveedor.DataBind();
                ddlCodMoneda.DataSource = servicio.MostrarMonedaTodo();
                ddlCodMoneda.DataValueField = "codigo"; ddlCodMoneda.DataTextField = "nombre"; ddlCodMoneda.DataBind();
                ddlCodUsuario.DataSource = servicio.MostrarUsuarioTodo();
                ddlCodUsuario.DataValueField = "codigo"; ddlCodUsuario.DataTextField = "nombre"; ddlCodUsuario.DataBind();
                ddlDCodTipoHilo.DataSource = servicio.MostrarTipoHiloTodo();
                ddlDCodTipoHilo.DataValueField = "codigo"; ddlDCodTipoHilo.DataTextField = "nombre"; ddlDCodTipoHilo.DataBind();
                ddlDCodUnidad.DataSource = servicio.MostrarUnidadMedidaTodo();
                ddlDCodUnidad.DataValueField = "codigo"; ddlDCodUnidad.DataTextField = "nombre"; ddlDCodUnidad.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        private DataTable GetDetalle()
        {
            if (ViewState["dt"] != null) return (DataTable)ViewState["dt"];
            DataTable dt = new DataTable();
            dt.Columns.Add("codTipoHilo");
            dt.Columns.Add("codTipoHiloTexto");
            dt.Columns.Add("codUnidad");
            dt.Columns.Add("codUnidadTexto");
            dt.Columns.Add("cantidad");
            dt.Columns.Add("precio");
            dt.Columns.Add("subtotal");
            ViewState["dt"] = dt;
            return dt;
        }

        private void BindDetalle()
        {
            DataTable dt = GetDetalle();
            gvDetalle.DataSource = dt; gvDetalle.DataBind();
            decimal total = 0;
            foreach (DataRow r in dt.Rows) total += Convert.ToDecimal(r["subtotal"]);
            lblTotal.Text = total.ToString("0.00");
        }

        protected void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetDetalle();
                DataRow r = dt.NewRow();
                r["codTipoHilo"] = ddlDCodTipoHilo.SelectedValue;
                r["codTipoHiloTexto"] = ddlDCodTipoHilo.SelectedItem.Text;
                r["codUnidad"] = ddlDCodUnidad.SelectedValue;
                r["codUnidadTexto"] = ddlDCodUnidad.SelectedItem.Text;
                r["cantidad"] = Convert.ToDecimal(txtDCantidad.Text);
                r["precio"] = Convert.ToDecimal(txtDPrecio.Text);
                r["subtotal"] = (Convert.ToDecimal(txtDCantidad.Text) * Convert.ToDecimal(txtDPrecio.Text)).ToString("0.00");
                dt.Rows.Add(r);
                ViewState["dt"] = dt;
                BindDetalle();
                txtDCantidad.Text = "";
                txtDPrecio.Text = "";
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                DataTable dt = GetDetalle();
                dt.Rows.RemoveAt(i);
                ViewState["dt"] = dt;
                BindDetalle();
            }
        }

        private void Mostrar()
        {
            try { gvCompraProveedor.DataSource = servicio.MostrarCompraProveedorTodo(); gvCompraProveedor.DataBind(); }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var cab = new CompraProveedorBO();
                cab.codProveedor = Convert.ToInt32(ddlCodProveedor.SelectedValue);
                cab.codMoneda = Convert.ToInt32(ddlCodMoneda.SelectedValue);
                cab.codUsuario = Convert.ToInt32(ddlCodUsuario.SelectedValue);
                cab.total = Convert.ToDecimal(lblTotal.Text);
                cab.estado = true;
                int id = servicio.RegistrarCompraProveedorRetornaId(cab);
                if (id > 0)
                {
                    foreach (DataRow r in GetDetalle().Rows)
                    {
                        var d = new DetalleCompraProveedorBO();
                        d.codCompra = id;
                        d.codTipoHilo = Convert.ToInt32(r["codTipoHilo"]);
                        d.codUnidad = Convert.ToInt32(r["codUnidad"]);
                        d.cantidad = Convert.ToDecimal(r["cantidad"]);
                        d.precio = Convert.ToDecimal(r["precio"]);
                        d.estado = true;
                        servicio.RegistrarDetalleCompraProveedor(d);
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (id > 0 ? "Registrado" : "No se pudo") + "');", true);
                Limpiar(); Mostrar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var cab = new CompraProveedorBO();
                cab.codigo = Convert.ToInt32(txtCod.Text);
                cab.codProveedor = Convert.ToInt32(ddlCodProveedor.SelectedValue);
                cab.codMoneda = Convert.ToInt32(ddlCodMoneda.SelectedValue);
                cab.codUsuario = Convert.ToInt32(ddlCodUsuario.SelectedValue);
                cab.total = Convert.ToDecimal(lblTotal.Text);
                cab.estado = true;
                bool res = servicio.ActualizarCompraProveedor(cab);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Actualizado" : "No se pudo") + "');", true);
                Limpiar(); Mostrar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var cab = new CompraProveedorBO();
                cab.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.EliminarCompraProveedor(cab);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Limpiar(); Mostrar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                var cab = new CompraProveedorBO();
                cab.codigo = Convert.ToInt32(txtCod.Text);
                bool res = servicio.HabilitarCompraProveedor(cab);
                ScriptManager.RegisterStartupScript(this, GetType(), "m", "alert('" + (res ? "Operacion correcta" : "No se pudo") + "');", true);
                Limpiar(); Mostrar();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }
        }

        protected void gvCompraProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow f = gvCompraProveedor.Rows[i];
                txtCod.Text = f.Cells[0].Text;
                try { ddlCodProveedor.SelectedValue = f.Cells[2].Text.Trim(); } catch { }
                try { ddlCodMoneda.SelectedValue = f.Cells[3].Text.Trim(); } catch { }
                try { ddlCodUsuario.SelectedValue = f.Cells[4].Text.Trim(); } catch { }
                int cod = Convert.ToInt32(txtCod.Text);
                DataTable dt = GetDetalle(); dt.Rows.Clear();
                foreach (var d in servicio.MostrarDetalleCompraProveedorXCompraProveedor(cod))
                {
                    DataRow r = dt.NewRow();
                    r["codTipoHilo"] = d.codTipoHilo; r["codTipoHiloTexto"] = d.codTipoHilo.ToString();
                    r["codUnidad"] = d.codUnidad; r["codUnidadTexto"] = d.codUnidad.ToString();
                    r["cantidad"] = d.cantidad;
                    r["precio"] = d.precio;
                    r["subtotal"] = (d.cantidad * d.precio).ToString("0.00");
                    dt.Rows.Add(r);
                }
                ViewState["dt"] = dt; BindDetalle();
            }
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            DataTable dt = GetDetalle(); dt.Rows.Clear(); ViewState["dt"] = dt; BindDetalle();
        }
    }
}