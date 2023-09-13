<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteShowUsuario.aspx.cs" Inherits="_3DBag.DeleteShowUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblPregunta" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="lblNick" runat="server">Nick: </asp:Label><asp:Label ID="lblNickResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblNombre" runat="server">Nombre: </asp:Label><asp:Label ID="lblNombreResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblMail" runat="server">Mail: </asp:Label><asp:Label ID="lblMailResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblIdioma" runat="server">Idioma: </asp:Label><asp:Label ID="lblIdiomaResp" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblPermisos" runat="server">Permisos Asignados:</asp:Label>
        <br />
        <asp:TreeView ID="TreeViewPermisos" runat="server"></asp:TreeView>
        <br />
        <br />
        <asp:Button ID="btnBorrar" runat="server" Text="Borrar" />
        <br />
        <br />
        <asp:LinkButton ID="LinkRedireccion" runat="server" OnClick="LinkRedireccion_Click"><< Volver atrás</asp:LinkButton>
    </div>
</asp:Content>
