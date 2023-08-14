<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiendaProducto.aspx.cs" Inherits="_3DBag.TiendaProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Tienda</h1>       
        <br />
        <asp:DataList ID="dataList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="600px" DataSourceID="SqlDataSource1" OnItemCommand="dataList_ItemCommand" DataKeyField="IdArticulo">   
            <ItemTemplate>
                <%--Imagen:
                <asp:Label ID="ImagenLabel" runat="server" Text='<%# Bind("Imagen") %>' />
                <br />--%>
                <asp:Label ID="IdArticuloLabel" runat="server" Text='<%# Eval("IdArticulo") %>'  Visible="False" />
                <br />
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
                <br />
            </ItemTemplate>
        </asp:DataList>   
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conexion %>" SelectCommand="SELECT [IdArticulo], [Nombre], [Descripcion], [PUnit] FROM [Articulo]"></asp:SqlDataSource>
    </div>
</asp:Content>
