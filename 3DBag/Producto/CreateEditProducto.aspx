<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateEditProducto.aspx.cs" Inherits="_3DBag.CreateEditProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblCodProd" runat="server" Text="CodProd"></asp:Label>
        <br />
        <asp:TextBox ID="txtCodProd" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
        <br />
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion"></asp:Label>
        <br />
        <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblMaterial" runat="server" Text="Material"></asp:Label>
        <br />
        <asp:TextBox ID="txtMaterial" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblStock" runat="server" Text="Stock"></asp:Label>
        <br />
        <asp:TextBox ID="txtStock" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblPUnit" runat="server" Text="Precio"></asp:Label>
        <br />
        <asp:TextBox ID="txtPUnit" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:FileUpload ID="FileUploadImagen" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnFunction" runat="server" OnClick="btnFunction_Click" />
        <br />
        <br />
        <asp:LinkButton ID="LinkRedirect" runat="server" OnClick="LinkRedirect_Click"><< Volver a la lista</asp:LinkButton>
    </div>
</asp:Content>
