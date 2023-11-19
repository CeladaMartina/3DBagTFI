<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="_3DBag.Pedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="lblPedido" SkinID="Pedido" runat="server" Font-Size="30px">Pedido</asp:Label>
        <br />          
        <br />
    <div id="divPedido" runat="server">
        <div class="form-group">
            <div class="table-responsive">
                <asp:GridView ID="gridDetalleVenta" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover" OnRowCommand="gridDetalleVenta_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                    <Columns>  
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="IdVenta" runat="server" Text='<%# Eval("IdVenta") %>' Visible="false"></asp:Label>
                                <asp:Label ID="IdDetalle" runat="server" Text='<%# Eval("IdDetalle") %>' Visible="false"></asp:Label>
                                <asp:Label ID="CodProd" runat="server" Text='<%# Eval("CodProd") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Descrip" HeaderText="Descrip" />
                        <asp:BoundField DataField="PUnit" HeaderText="PUnit" />   
                       <%-- <asp:TemplateField HeaderText="Cant">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCant" runat="server" Text='<%# Eval("Cant") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>     --%>                  
                        <asp:BoundField DataField="Cant" HeaderText="Cant" />  

                        <asp:ButtonField ButtonType="Button" CommandName="borrar" Text="Borrar">
                            <ControlStyle BackColor="#CC3300" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>    
                <br />
                <br />
                <asp:LinkButton ID="linkRedirect" runat="server" OnClick="VerTienda"><< Agregar Productos al Carrito</asp:LinkButton>
                <br />
                <br />
                <asp:Button ID="btnComprarAhora" Text="Comprar" SkinID="Comprar" runat="server" OnClick="btnComprarAhora_Click" CssClass="btn btn-primary" />                
            </div>
        </div>
    </div>
     <asp:Label ID="lblPermiso" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>
</asp:Content>
