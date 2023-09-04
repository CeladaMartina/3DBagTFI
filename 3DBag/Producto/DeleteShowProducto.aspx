<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteShowProducto.aspx.cs" Inherits="_3DBag.DeleteShowProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblPregunta" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="lblCodProd" runat="server">Codigo Producto: </asp:Label><asp:Label ID="lblCodProdResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblNombre" runat="server">Nombre: </asp:Label><asp:Label ID="lblNombreResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblDescripcion" runat="server">Descripcion: </asp:Label><asp:Label ID="lblDescripcionResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblMaterial" runat="server">Material: </asp:Label><asp:Label ID="lblMaterialResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblStock" runat="server">Stock: </asp:Label><asp:Label ID="lblStockResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblPUnit" runat="server">Precio: </asp:Label><asp:Label ID="lblPUnitResp" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnBorrar" runat="server" Text="Borrar" OnClick="btnBorrar_Click" />
        <br />
        <br />
        <asp:LinkButton ID="LinkRedireccion" runat="server" OnClick="LinkRedireccion_Click"><< Volver atrás</asp:LinkButton>
    </div>
</asp:Content>
