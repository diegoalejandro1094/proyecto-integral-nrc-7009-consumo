<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPagoProveedor.aspx.cs" Inherits="ConsumoHyB.frmPagoProveedor" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mantenimiento de Cliente</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Mantenimiento de PagoProveedor</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txtCod" runat="server" Visible="false"></asp:TextBox>
                <div class="mb-2">
                    <asp:Label ID="lblCodCompra" runat="server" Text="Compra:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlCodCompra" runat="server" CssClass="form-select"></asp:DropDownList>
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
                    <asp:GridView ID="gvPagoProveedor" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnRowCommand="gvPagoProveedor_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="codCompra" HeaderText="Compra" />
                            <asp:BoundField DataField="codMoneda" HeaderText="Moneda" />
                            <asp:BoundField DataField="monto" HeaderText="Monto" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate><%# Convert.ToBoolean(Eval("estado")) ? "Habilitado" : "Deshabilitado" %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField Text="Seleccionar" CommandName="Seleccionar" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
