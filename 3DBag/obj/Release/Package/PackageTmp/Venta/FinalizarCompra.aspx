<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinalizarCompra.aspx.cs" Inherits="_3DBag.FinalizarCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; margin-top: 60px;">
        <div style="width: 100%;">
            <asp:Label ID="lblIdVenta" runat="server" SkinID="N° Remito">N° Remito: </asp:Label><br /><asp:Label ID="lblIdVentaResp" runat="server"></asp:Label>            
            <br />
            <br />
            <asp:Label ID="lblCliente" runat="server" SkinID="Cliente">Cliente: </asp:Label><br /><asp:Label ID="lblClienteResp" runat="server"></asp:Label>            
        </div>
        <div style="width: 100%;">
            <iframe id="viewPDF" runat="server" height="500" width="300"></iframe>
        </div>
    </div>   
    <br />
    <br />
    <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" SkinID="Finalizar Compra" OnClick="btnFinalizarCompra_Click" CssClass="btn btn-primary" />
    <br />
    <br />
    <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>
</asp:Content>
