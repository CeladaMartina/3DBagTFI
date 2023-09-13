<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="_3DBag.Administracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>Sistema de Administración</h1>
        <br />
        <div>
            <asp:LinkButton ID="LinkGestionUsuarios" runat="server" OnClick="LinkGestionUsuarios_Click">Gestion de Usuarios.</asp:LinkButton>            
            <br />
            <br />
            <asp:LinkButton ID="LinkGestionFamilias" runat="server" OnClick="LinkGestionFamilias_Click">Gestion de Familias.</asp:LinkButton>
            <br />
             <br />
            <asp:LinkButton ID="LinkGestionPatentes" runat="server" OnClick="LinkGestionPatentes_Click">Gestion de Patentes.</asp:LinkButton>            
        </div>
    </div>
</asp:Content>
