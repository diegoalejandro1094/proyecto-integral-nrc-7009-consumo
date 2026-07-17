<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPedido.aspx.cs" Inherits="ConsumoHyB.frmPedido" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Mantenimiento de Pedido</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" crossorigin="anonymous" />
</head>
<body>
    <div class="container">
        <h1>Registro de Pedido (Maestro-Detalle)</h1>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtCod" runat="server" Visible="false"></asp:TextBox>
                    <h4>Cabecera</h4>
                    <div class="mb-2">
                        <asp:Label ID="lblCodCliente" runat="server" Text="Cliente:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlCodCliente" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblCodEstado" runat="server" Text="Estado:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlCodEstado" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblCodUsuario" runat="server" Text="Usuario:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlCodUsuario" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblCodMoneda" runat="server" Text="Moneda:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlCodMoneda" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <hr />
                    <h4>Agregar detalle</h4>
                    <div class="mb-2">
                        <asp:Label ID="lblDCodTipoTejido" runat="server" Text="Tipo Tejido:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlDCodTipoTejido" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblDCodUnidad" runat="server" Text="Unidad:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlDCodUnidad" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblDCantidad" runat="server" Text="Cantidad:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtDCantidad" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblDPrecio" runat="server" Text="Precio:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtDPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnAgregarDetalle" runat="server" Text="Agregar detalle" CssClass="btn btn-secondary" OnClick="btnAgregarDetalle_Click" />
                    <div class="mb-2"></div>
                    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="false" CssClass="table table-sm table-bordered" OnRowCommand="gvDetalle_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="codTipoTejidoTexto" HeaderText="Tipo Tejido" />
                            <asp:BoundField DataField="codUnidadTexto" HeaderText="Unidad" />
                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" />
                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal" />
                            <asp:ButtonField Text="Quitar" CommandName="Quitar" />
                        </Columns>
                    </asp:GridView>
                    <h5>Total: <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label></h5>
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Pedido" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar cabecera" CssClass="btn btn-success" OnClick="btnActualizar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
                    <asp:Button ID="btnHabilitar" runat="server" Text="Habilitar" CssClass="btn btn-warning" OnClick="btnHabilitar_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <hr />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <h4>Pedidos registrados</h4>
                    <div class="table-responsive">
                    <asp:GridView ID="gvPedido" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnRowCommand="gvPedido_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="codCliente" HeaderText="Cliente" />
                            <asp:BoundField DataField="codEstado" HeaderText="Estado" />
                            <asp:BoundField DataField="codUsuario" HeaderText="Usuario" />
                            <asp:BoundField DataField="codMoneda" HeaderText="Moneda" />
                            <asp:BoundField DataField="total" HeaderText="Total" />
                            <asp:TemplateField HeaderText="Estado"><ItemTemplate><%# Convert.ToBoolean(Eval("estado")) ? "Habilitado" : "Deshabilitado" %></ItemTemplate></asp:TemplateField>
                            <asp:ButtonField Text="Seleccionar" CommandName="Seleccionar" />
                        </Columns>
                    </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>
</body>
</html>