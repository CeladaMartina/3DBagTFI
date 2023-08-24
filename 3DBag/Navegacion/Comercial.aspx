<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comercial.aspx.cs" Inherits="_3DBag.Comercial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>Modulo Comercial</h1>
        <br />
        <div>
            <%-- ABML productos --%>
            <asp:LinkButton ID="LinkProductos" runat="server" OnClick="LinkProductos_Click">Gestión de Productos.</asp:LinkButton>
            <br />
            <br />
            <%-- visualizar ventas --%>
            <asp:LinkButton ID="LinkVerVentas" runat="server" OnClick="LinkVerVentas_Click">Historial de Ventas.</asp:LinkButton>
            <br />
            <br />
            <%-- carrito de compras (venta) --%>
            <asp:LinkButton ID="LinkTienda" runat="server" OnClick="LinkTienda_Click">Tienda.</asp:LinkButton>
            <br />
            <br />
            <%-- detalle venta --%>
            <asp:LinkButton ID="LinkPedido" runat="server" OnClick="LinkPedido_Click">Pedido.</asp:LinkButton>
        </div>
    </div>
</asp:Content>
