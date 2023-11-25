<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialVenta.aspx.cs" Inherits="_3DBag.HistorialVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblTitulo" SkinID="Historial de Ventas" runat="server">Historial de Ventas</asp:Label>
        <br />          
        <br />        
        <div id="divHistorialVentas" runat="server" class="form-group">
            <div class="table-responsive">
                <asp:GridView ID="gridVentas" PageSize="5"  AllowPaging="true" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover" OnDataBound="gridVentas_DataBound" OnPageIndexChanging="gridVentas_PageIndexChanging">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="NumVenta" HeaderText="NumVenta" />  
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />   
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" /> 
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto" />  
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />                                                            
                    </Columns>
                    <PagerTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="LinkAnterior" runat="server" Text="Anterior" SkinID="Anterior" CommandName="Page" CommandArgument="Prev" />
                                    <asp:LinkButton ID="LinkSiguiente" runat="server" Text="Siguiente" SkinID="Siguiente" CommandName="Page" CommandArgument="Next" />
                                </td>
                                <td>Pag N°
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    Total Pag.
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </PagerTemplate>
                    <PagerSettings Mode="NextPrevious" Position="Bottom" />
                </asp:GridView>
            </div>
            <br />
        <br />
        <asp:LinkButton ID="LinkRedireccion" runat="server" SkinID="Volver a la lista" OnClick="LinkRedireccion_Click"><< Volver atrás</asp:LinkButton>
        <br />
        <br />
        <asp:Button ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar" SkinID="Exportar" CssClass="btn btn-secondary" />
        <br />
        <br />
        <asp:Button ID="btnWebServiceCliente" runat="server" OnClick="btnWebServiceCliente_Click" Text="Consultar" SkinID="Consultar" CssClass="btn btn-info" />
        <br />
        <br />
        <asp:GridView ID="gridClientes" runat="server" AutoGenerateColumns="false" Visible="false">
            <AlternatingRowStyle BackColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
            <RowStyle BackColor="#f5f5f5" />
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Cliente" />
                <asp:BoundField DataField="NumVenta" HeaderText="Total de Compras realizadas" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
        </div>        
        <asp:Label ID="lblPermiso" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>  
    </div>
</asp:Content>
