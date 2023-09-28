<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="_3DBag.Administracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <asp:Label ID="lblTitulo" SkinID="Sistema de Administración" runat="server">Sistema de Administración</asp:Label>
        <br />
        <br />
        <div>
            <asp:LinkButton ID="LinkGestionUsuarios" SkinID="Gestion de Usuarios" runat="server" OnClick="LinkGestionUsuarios_Click">Gestion de Usuarios.</asp:LinkButton>            
            <br />
            <br />
            <asp:LinkButton ID="LinkGestionFamilias" SkinID="Gestion de Familias" runat="server" OnClick="LinkGestionFamilias_Click">Gestion de Familias.</asp:LinkButton>
            <br />
             <br />
            <asp:LinkButton ID="LinkGestionPatentes" SkinID="Gestion de Patentes" runat="server" OnClick="LinkGestionPatentes_Click">Gestion de Patentes.</asp:LinkButton>            
        </div>
    </div>
</asp:Content>
