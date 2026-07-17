<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRolPermiso.aspx.cs" Inherits="ConsumoHyB.frmRolPermiso" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Mantenimiento de RolPermiso</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" crossorigin="anonymous" />
</head>
<body>
    <div class="container">
        <h1>Mantenimiento de RolPermiso</h1>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtCod" runat="server" Visible="false"></asp:TextBox>
                    <div class="mb-2">
                        <asp:Label ID="lblCodRol" runat="server" Text="Rol:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlCodRol" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblCodPermiso" runat="server" Text="Permiso:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlCodPermiso" runat="server" CssClass="form-select"></asp:DropDownList>
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
                    <asp:GridView ID="gvRolPermiso" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnRowCommand="gvRolPermiso_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                            <asp:BoundField DataField="codRol" HeaderText="Rol" />
                            <asp:BoundField DataField="codPermiso" HeaderText="Permiso" />
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