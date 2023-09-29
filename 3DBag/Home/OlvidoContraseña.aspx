<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OlvidoContraseña.aspx.cs" Inherits="_3DBag.OlvidoContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblNick" runat="server" SkinID="Nick">Nick: </asp:Label><asp:TextBox ID="txtNick" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblMail" runat="server" SkinID="Email"> Mail: </asp:Label><asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblRespuesta" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnActualizarContra" runat="server" Text="Enviar Contraseña" OnClick="btnActualizarContra_Click"/>
</asp:Content>
