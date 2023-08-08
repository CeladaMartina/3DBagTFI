<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialVenta.aspx.cs" Inherits="_3DBag.HistorialVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Historial Ventas</h1>        
        <br />
        <div class="form-group">
            <div class="table-responsive">
                <asp:GridView ID="gridVentas" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="NumVenta" HeaderText="NumVenta" />
                        <asp:BoundField DataField="DNIcliente" HeaderText="DNIcliente" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                         <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />  
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto" />                   
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
