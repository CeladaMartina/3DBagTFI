<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="_3DBag.Pedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div> 
         <h1>Pedido</h1>       
        <br />
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
                                <asp:Label ID="IdDetalle" runat="server" Text='<%# Eval("IdDetalle") %>' Visible="false"></asp:Label>
                                <asp:Label ID="CodProd" runat="server" Text='<%# Eval("CodProd") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Descrip" HeaderText="Descrip" />
                        <asp:TemplateField HeaderText="Cant">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCant" runat="server" Text='<%# Eval("Cant") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:BoundField DataField="PUnit" HeaderText="PUnit" />                     
                       
                        <asp:ButtonField ButtonType="Button" CommandName="editar" Text="Editar">
                            <ControlStyle BackColor="#6699FF" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="borrar" Text="Borrar">
                            <ControlStyle BackColor="#CC3300" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
                <br />
                <asp:LinkButton ID="linkRedirect" runat="server" Visible="false" OnClick="VerTienda">-- Ver Tienda --</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
