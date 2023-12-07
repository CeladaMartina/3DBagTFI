<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comercial.aspx.cs" Inherits="_3DBag.Comercial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <asp:Label ID="lblTitulo" SkinID="Modulo Comercial" runat="server">Modulo Comercial</asp:Label>
        <br />
        <br />
        <div>
            <%-- ABML productos --%>
            <asp:LinkButton ID="LinkProductos" SkinID="Gestión de Productos" runat="server" OnClick="LinkProductos_Click">Gestión de Productos.</asp:LinkButton>
            <br />
            <br />
            <%-- visualizar ventas --%>
            <asp:LinkButton ID="LinkVerVentas" SkinID="Historial de Ventas" runat="server" OnClick="LinkVerVentas_Click">Historial de Ventas.</asp:LinkButton>
            <br />
            <br />
            <%-- carrito de compras (venta) --%>
            <asp:LinkButton ID="LinkTienda" SkinID="Tienda" runat="server" OnClick="LinkTienda_Click">Tienda.</asp:LinkButton>
            <br />
            <br />
            <%-- detalle venta --%>
            <asp:LinkButton ID="LinkPedido" SkinID="Pedido" runat="server" OnClick="LinkPedido_Click">Pedido.</asp:LinkButton>
        </div>
    </div>
</asp:Content>
