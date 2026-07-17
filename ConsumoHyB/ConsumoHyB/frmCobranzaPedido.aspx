<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCobranzaPedido.aspx.cs" Inherits="ConsumoHyB.frmCobranzaPedido" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Mantenimiento de CobranzaPedido</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" crossorigin="anonymous" />
</head>
<body>
    <div class="container">
        <h1>Mantenimiento de CobranzaPedido</h1>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtCod" runat="server" Visible="false"></asp:TextBox>
                    <div class="mb-2">
                        <asp:Label ID="lblCodPedido" runat="server" Text="Pedido:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlCodPedido" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblCodMoneda" runat="server" Text="Moneda:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlCodMoneda" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblMonto" runat="server" Text="Monto:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3 form-check">
                        <asp:CheckBox ID="chkEstado" runat="server" CssClass="form-check-input" />
                        <asp:Label ID="lblEstado" runat="server" Text="Habilitado" CssClass="form-check-label"></asp:Label>
                    </div>
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="btnActualizar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
                    <asp:Button ID="btnHabilitar" runat="server" Text="Habilitar" CssClass="btn btn-warning" OnClick="btnHabilitar_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="mb-3"></div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="table-responsive">
                    <asp:GridView ID="gvCobranzaPedido" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnRowCommand="gvCobranzaPedido_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="codPedido" HeaderText="Pedido" />
                            <asp:BoundField DataField="codMoneda" HeaderText="Moneda" />
                            <asp:BoundField DataField="monto" HeaderText="Monto" />
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