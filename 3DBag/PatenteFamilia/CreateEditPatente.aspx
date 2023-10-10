<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateEditPatente.aspx.cs" Inherits="_3DBag.CreateEditPatente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblPatente" SkinID="Patente" runat="server">Patente</asp:Label>
    <br />
    <br />
    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
    <br />
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion" SkinID="Descripcion"></asp:Label>
    <br />
    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnFunction" runat="server" Text="Editar" SkinID="Editar" OnClick="btnFunction_Click" />
    <br />
    <br />
    <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>
    <br />
    <br />
    <asp:LinkButton ID="LinkRedirect" runat="server" SkinID="Volver a la lista" OnClick="LinkRedirect_Click"><< Volver a la lista</asp:LinkButton>
</asp:Content>
