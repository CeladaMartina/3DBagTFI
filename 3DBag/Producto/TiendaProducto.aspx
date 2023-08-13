<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiendaProducto.aspx.cs" Inherits="_3DBag.TiendaProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Tienda</h1>       
        <br />
        <asp:DataList ID="dataListProducto" runat="server" DataKeyField="IdArticulo" RepeatColumns="4" RepeatDirection="Horizontal" Width="600px" OnItemCommand="dataListProducto_ItemCommand1">
            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%#Bind("Imagen") %>' Height="100px" Width="100px"/>                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNombre" runat="server" Text='<%#Bind("Nombre")%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Bind("Descripcion")%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCantidad" runat="server">Cantidad:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>                          
                            <%--<input id="btnAgregar" type="button" value="Agregar" contextmenu="Agregar" onclick="AlCarrito"/>--%>
                            <%-- <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CommandName="Agregar" CommandArgument='<%#Bind("IdArticulo")%>' />--%>
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CommandName="Agregar" />
                        </td>                        
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>        
    </div>
</asp:Content>
