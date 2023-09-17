<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Seguridad.aspx.cs" Inherits="_3DBag.Seguridad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>Sistema de Seguridad</h1>
        <br />
        <div>
            <asp:LinkButton ID="LinkBackup" runat="server" OnClick="LinkBackup_Click">Generar copia de respaldo.</asp:LinkButton> 
            <br />
            <br />
            <asp:LinkButton ID="LinkBitacora" runat="server" OnClick="LinkBitacora_Click">Bitacora.</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="LinkRestore" runat="server" OnClick="LinkRestore_Click">Restore.</asp:LinkButton>
            <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
