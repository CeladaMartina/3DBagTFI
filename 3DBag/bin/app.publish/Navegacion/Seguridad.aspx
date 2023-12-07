<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Seguridad.aspx.cs" Inherits="_3DBag.Seguridad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <asp:Label ID="lblTitulo" SkinID="Seguridad" runat="server">Seguridad</asp:Label>
        <br />
        <br />
        <div>
            <asp:LinkButton ID="LinkBackup" SkinID="Generar copia de respaldo" runat="server" OnClick="LinkBackup_Click">Generar copia de respaldo.</asp:LinkButton> 
            <br />
            <br />
            <asp:LinkButton ID="LinkBitacora" SkinID="Bitacora" runat="server" OnClick="LinkBitacora_Click">Bitacora.</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="LinkRestore" SkinID="Restauracion" runat="server" OnClick="LinkRestore_Click">Restore.</asp:LinkButton>
        </div>        
    </div>
</asp:Content>
