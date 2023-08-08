<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comercial.aspx.cs" Inherits="_3DBag.Comercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h1>Modulo Comercial</h1>
        <br />
        <div>
            <%-- ABML productos --%>
            <a class="nav-link" href="../Producto/IndexProducto.aspx">Gestión de Productos</a>
             <br />
            <%-- carrito de compras --%>
            <a class="nav-link" href="">Tienda</a>
            <br />
            <%-- visualizar ventas --%>
            <a class="nav-link" href="">Historial de Ventas</a>
             <br />
            <%-- venta --%>
            <a class="nav-link" href="">Pedido</a>
           
            
        </div>
    </div>
</asp:Content>
