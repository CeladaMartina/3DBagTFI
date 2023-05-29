<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="_3DBag.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>Sistema de Seguridad</h1>
        <br />
        <div>
            <a class="nav-link" href="Backup.aspx">Generar copia de respaldo</a>
            <br />
            <a class="nav-link" href="">Bitacora</a>
            <br />
            <a class="nav-link" href="">Restaurar</a>
        </div>
    </div>
</asp:Content>
