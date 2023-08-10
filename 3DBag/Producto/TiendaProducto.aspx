<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiendaProducto.aspx.cs" Inherits="_3DBag.TiendaProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Tienda</h1>       
        <br />
        <asp:DataList ID="dataListProducto" runat="server" DataKeyField="IdArticulo" RepeatColumns="1" RepeatDirection="Horizontal" Width="600px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%#Bind("Imagen") %>' Height="100px" Width="100px"/>                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("Nombre")%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("Descripcion")%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server">Cantidad:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" />
                        </td>
                        
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        
    </div>
</asp:Content>
