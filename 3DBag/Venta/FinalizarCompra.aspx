<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinalizarCompra.aspx.cs" Inherits="_3DBag.FinalizarCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblCliente" runat="server">Cliente:</asp:Label><asp:Label ID="lblClienteResp" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblIdVenta" runat="server">N° Factura:</asp:Label><asp:Label ID="lblIdVentaResp" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblTipoFactura" runat="server">Tipo B</asp:Label>
    <br />
    <br />
    <iframe id="viewPDF" runat="server" height="500" width="300">

    </iframe>
    <br />
    <br />
    <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" OnClick="btnFinalizarCompra_Click"/>
</asp:Content>
