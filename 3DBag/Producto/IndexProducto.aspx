<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndexProducto.aspx.cs" Inherits="_3DBag.IndexProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblTitulo" SkinID="Productos" runat="server">Productos</asp:Label>
    <br />
    <br />
    <div id="divProductos" runat="server">         
        <asp:Button ID="btnAlta" runat="server" Text="Nuevo Producto" SkinID="Agregar" OnClick="btnAlta_Click"  CssClass="btn btn-primary" />
        <br />
        <br />
        <asp:GridView ID="gridProducto" runat="server" AutoGenerateColumns="false" Width="100%" OnRowCommand="gridProducto_RowCommand">
            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="CodProd" HeaderText="CodProd" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="Material" HeaderText="Material" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="PUnit" HeaderText="PUnit" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="Imagen" runat="server" ImageUrl='<%#Bind("Imagen") %>' Height="100px" Width="100px" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:ButtonField ButtonType="Button" CommandName="editar" Text="Editar">
                    <ControlStyle BackColor="#6699FF" />
                </asp:ButtonField>
                <asp:ButtonField ButtonType="Button" CommandName="select" Text="Ver">
                    <ControlStyle BackColor="Lime" />
                </asp:ButtonField>
                <asp:ButtonField ButtonType="Button" CommandName="borrar" Text="Borrar">
                    <ControlStyle BackColor="#CC3300" />
                </asp:ButtonField>
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <div id="divWebServiceProd" style="margin: 0 10px 0 auto;">
            <asp:Button ID="btnWebService" runat="server" Text="Consultar" SkinID="Consultar" OnClick="btnWebService_Click" CssClass="btn btn-secondary"/>            
            <asp:Button ID="Button1" runat="server" Text="Consultar" SkinID="Consultar" OnClick="btnWebService2_Click" CssClass="btn btn-secondary" />
        </div>        
        <br />
        <br />
        <asp:Label ID="lblRespuesta" runat="server" Visible="false"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="linkVolver" SkinID="Volver atras" runat="server" OnClick="linkVolver_Click"><< Volver atras</asp:LinkButton>
    </div>
     <asp:Label ID="lblPermiso" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>  
</asp:Content>
