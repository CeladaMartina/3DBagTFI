<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerPatente.aspx.cs" Inherits="_3DBag.VerPatente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblTitulo" runat="server" Text="Detalles Patente"></asp:Label>
    <br />
    <br />    
    <asp:Label ID="lblNombre" runat="server" Text="Nombre: " SkinID="Nombre"></asp:Label><asp:Label ID="lblNombreResp" runat="server"></asp:Label>
    <br />
    <br />   
    <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion: "><asp:Label ID="lblDescripcionResp" runat="server"></asp:Label></asp:Label>
    <br />
    <br />  
    <asp:LinkButton ID="LinkRedireccion" runat="server" SkinID="Volver atras" OnClick="LinkRedireccion_Click"><< Volver atras</asp:LinkButton>
</asp:Content>
