<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="_3DBag.Administracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>Sistema de Administración</h1>
        <br />
        <div>
            <a class="nav-link" href="../Usuarios/IndexUsuarios.aspx">Gestion de Usuarios.</a>
            <br />
            <a class="nav-link" href="../Home/Home.aspx">Gestion de Familias.</a>
            <br />
            <a class="nav-link" href="../Home/Home.aspx">Gestion de Patentes.</a>
        </div>
    </div>
</asp:Content>
