<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiendaProducto.aspx.cs" Inherits="_3DBag.TiendaProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblTitulo" runat="server" SkinID="Tienda">Tienda</asp:Label>
        <br />
        <br />
        <asp:DataList ID="dataList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="800px"  OnItemCommand="dataList_ItemCommand" OnItemDataBound="dataList_ItemDataBound">   
            <ItemTemplate>
                <asp:Label ID="IdVentaUsuario" runat="server" Text="" Visible="False" />
                <%--Imagen:
                <asp:Label ID="ImagenLabel" runat="server" Text='<%# Bind("Imagen") %>' />
                <br />--%>
                 <asp:Image ID="Imagen" runat="server" ImageUrl='<%#Bind("Imagen") %>' Height="150px" Width="150px"/>
                <br />                
                <asp:Label ID="IdArticuloLabel" runat="server" Text='<%# Eval("IdArticulo") %>'  Visible="False" />
                <br />
               <asp:Label runat="server" ID="lblNombre" SkinID="Nombre">Nombre: </asp:Label>
                <asp:Label ID="NombreLabel" runat="server" Text='<%# Eval("Nombre") %>' />
                <br />  
                <asp:Label runat="server" ID="lblDescripcion" SkinID="Descripcion">Descripcion: </asp:Label>                
                <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>' />
                <br />
                <asp:Label runat="server" ID="lblPrecio" SkinID="Precio">Precio: </asp:Label>  
                <asp:Label ID="PUnitLabel" runat="server" Text='<%# Eval("PUnit") %>' />
                <br />  
                <asp:Label runat="server" ID="lblCantidad" SkinID="Cantidad">Cantidad: </asp:Label>  
                 <asp:TextBox ID="TxtCantidad" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" SkinID="Agregar" CommandName="AgregarCarrito" CssClass="btn btn-primary" />
                <br />
            </ItemTemplate>
        </asp:DataList>   
        <br />         
        <br />
        <asp:LinkButton ID="LinkRedireccion" runat="server" SkinID="Volver a la lista" OnClick="LinkRedireccion_Click"><< Volver atrás</asp:LinkButton>
        <br />
        <br />
        <asp:Label ID="lblRespuesta" runat="server" Visible="false"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="VerCarrito" runat="server" OnClick="VerCarrito_Click" Visible="False" SkinID="Ver Carrito de Compras">-- Ver Carrito de Compras --</asp:LinkButton>
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conexion %>" SelectCommand="SELECT [IdArticulo], [Nombre], [Descripcion], [PUnit], [Imagen] FROM [Articulo]"></asp:SqlDataSource>--%>
    </div>
</asp:Content>
