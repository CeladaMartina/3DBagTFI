<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="_3DBag.Pedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>        
        <div class="form-group">
            <div class="table-responsive">
                <asp:GridView ID="gridDetalleVenta" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Descrip" HeaderText="Descrip" />
                        <asp:BoundField DataField="Cant" HeaderText="Cant" />
                        <asp:BoundField DataField="PUnit" HeaderText="PUnit" />                     
                       
                        <asp:ButtonField ButtonType="Button" CommandName="borrar" Text="Borrar">
                            <ControlStyle BackColor="#CC3300" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
