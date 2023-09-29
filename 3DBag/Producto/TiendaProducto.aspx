<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiendaProducto.aspx.cs" Inherits="_3DBag.TiendaProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblTitulo" runat="server" SkinID="Tienda">Tienda</asp:Label>
        <br />
        <br />
        <asp:DataList ID="dataList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="600px" DataSourceID="SqlDataSource1" OnItemCommand="dataList_ItemCommand" DataKeyField="IdArticulo">   
            <ItemTemplate>
                <%--Imagen:
                <asp:Label ID="ImagenLabel" runat="server" Text='<%# Bind("Imagen") %>' />
                <br />--%>
                <asp:Label ID="IdVentaUsuario" runat="server" Text="" Visible="False" />
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
                <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" SkinID="Agregar" CommandName="AgregarCarrito" />
                <br />
            </ItemTemplate>
        </asp:DataList>   
        <br />
        <asp:Label ID="lblRespuesta" runat="server" Visible="false"></asp:Label>
        <asp:LinkButton ID="VerCarrito" runat="server" OnClick="VerCarrito_Click" Visible="False" SkinID="Ver Carrito de Compras">-- Ver Carrito de Compras --</asp:LinkButton>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conexion %>" SelectCommand="SELECT [IdArticulo], [Nombre], [Descripcion], [PUnit] FROM [Articulo]"></asp:SqlDataSource>
    </div>
</asp:Content>
