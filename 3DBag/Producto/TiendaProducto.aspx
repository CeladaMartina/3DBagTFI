<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiendaProducto.aspx.cs" Inherits="_3DBag.TiendaProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Tienda</h1>       
        <br />
        <asp:DataList ID="dataList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="600px" DataSourceID="SqlDataSource1" OnItemCommand="dataList_ItemCommand">   
            <ItemTemplate>
                <%--Imagen:
                <asp:Label ID="ImagenLabel" runat="server" Text='<%# Bind("Imagen") %>' />
                <br />--%>
                Nombre:
                <asp:Label ID="NombreLabel" runat="server" Text='<%# Eval("Nombre") %>' />
                <br />
                Descripcion:
                <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>' />
                <br />                
                Precio:
                <asp:Label ID="PUnitLabel" runat="server" Text='<%# Eval("PUnit") %>' />
                <br />
                Cantidad:
                <asp:TextBox ID="TxtCantidad" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CommandName="AgregarCarrito" />
            </ItemTemplate>
        </asp:DataList>   
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conexion %>" SelectCommand="SELECT [Nombre], [Descripcion], [PUnit] FROM [Articulo]"></asp:SqlDataSource>
    </div>
</asp:Content>
