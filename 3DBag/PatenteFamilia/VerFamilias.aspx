<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerFamilias.aspx.cs" Inherits="_3DBag.VerFamilias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <asp:Label ID="lblTitulo" runat="server" Text="Detalles Familia" SkinID="Detalles Familia"></asp:Label>
    <br />
    <br />    
    <asp:Label ID="lblNombre" runat="server" Text="Nombre: " SkinID="Nombre"></asp:Label><asp:Label ID="lblNombreResp" runat="server"></asp:Label>
    <br />
    <br />   
    <asp:Label ID="lblDetalles" runat="server" Text="Patentes asignadas a la familia" SkinID="Patentes asignadas a la familia"></asp:Label>
    <br />
    <br />   
    <asp:TreeView ID="TreeView1" runat="server">
    </asp:TreeView>
    <br />
    <br />  
    <asp:LinkButton ID="LinkRedireccion" runat="server" OnClick="LinkRedireccion_Click" SkinID="Volver atras"><< Volver atras</asp:LinkButton>
</asp:Content>
