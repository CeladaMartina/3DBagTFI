<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Restore.aspx.cs" Inherits="_3DBag.Restore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="containerRestore">
        <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
        <br />
        <asp:TextBox ID="txtRuta" runat="server" Width="322px"></asp:TextBox>
        <br />
        <asp:Button ID="btnRestaurar" runat="server" Text="Restaurar" OnClick="btnRestaurar_Click" />
        <br />
        <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
