<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteShowUsuario.aspx.cs" Inherits="_3DBag.DeleteShowUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="lblUsuario" SkinID="Usuarios" runat="server">Usuarios</asp:Label>
        <br />          
        <br />
    <div id="DivUsuarios" runat="server">
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblPregunta" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="lblNick" runat="server" SkinID="Nick">Nick: </asp:Label><asp:Label ID="lblNickResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblNombre" runat="server" SkinID="Nombre">Nombre: </asp:Label><asp:Label ID="lblNombreResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblMail" runat="server" SkinID="Email">Mail: </asp:Label><asp:Label ID="lblMailResp" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblIdioma" runat="server" SkinID="Idioma">Idioma: </asp:Label><asp:Label ID="lblIdiomaResp" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblPermisos" runat="server" SkinID="Permisos Asignados">Permisos Asignados:</asp:Label>
        <br />
        <asp:TreeView ID="TreeViewPermisos" runat="server"></asp:TreeView>
        <br />
        <br />
        <asp:Button ID="btnBorrar" runat="server" Text="Borrar" SkinID="Eliminar" OnClick="btnBorrar_Click" />
        <br />
        <br />
         <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="LinkRedireccion" runat="server" OnClick="LinkRedireccion_Click" SkinID="Volver atras"><< Volver atrás</asp:LinkButton>
    </div>
    <asp:Label ID="lblPermiso" runat="server" Visible="false"></asp:Label>
</asp:Content>
