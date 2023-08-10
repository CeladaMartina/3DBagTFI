<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndexProducto.aspx.cs" Inherits="_3DBag.IndexProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Productos</h1>
        <a class="nav-link" href="">Nuevo Producto</a>
        <br />
        <div class="form-group">
            <div class="table-responsive">
                <asp:GridView ID="gridProducto" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover" OnRowCommand="gridProducto_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="CodProd" HeaderText="CodProd" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                        <asp:BoundField DataField="Material" HeaderText="Material" />
                        <asp:BoundField DataField="Talle" HeaderText="Talle" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                        <asp:BoundField DataField="PUnit" HeaderText="PUnit" />
                        <%--<asp:BoundField DataField="Imagen" HeaderText="Imagen" HtmlEncode="false"/>--%>
                       <asp:TemplateField>
                           <ItemTemplate>
                               <asp:Image ID="Image1" runat="server" ImageUrl='<%#Bind("Imagen") %>' Height="100px" Width="100px"/> 
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
            </div>
        </div>
    </div>
</asp:Content>
