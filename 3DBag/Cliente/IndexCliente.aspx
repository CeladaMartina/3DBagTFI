<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndexCliente.aspx.cs" Inherits="_3DBag.IndexCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
        <h1>Usuarios</h1>
        <a class="nav-link" href="">Nuevo Usuario</a>
        <br />
        <div class="form-group">
            <div class="table-responsive">
                <asp:GridView ID="gridCliente" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover" OnRowCommand="gridCliente_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="DNI" HeaderText="DNI" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="FechaNac" HeaderText="FechaNac" />
                        <asp:BoundField DataField="Tel" HeaderText="Tel" />
                        <asp:BoundField DataField="Mail" HeaderText="Mail" />
                        

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
