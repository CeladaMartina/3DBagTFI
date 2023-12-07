<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndexUsuarios.aspx.cs" Inherits="_3DBag.IndexUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:Label ID="lblTitulo" SkinID="Usuarios" runat="server">Usuarios</asp:Label>
        <br />          
        <br />
    <div id="divUsuarios" runat="server">        
        <asp:Button ID="btnAltaUsuario" runat="server" Text="Nuevo Usuario" SkinID="Nuevo Usuario" OnClick="btnAltaUsuario_Click" CssClass="btn btn-primary" />
        <br />
        <br />
        <div class="form-group">
            <div class="table-responsive">
                <asp:GridView ID="gridUsuarios" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover" OnRowCommand="gridUsuarios_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                    <RowStyle BackColor="#f5f5f5" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Nick" HeaderText="Nick" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Mail" HeaderText="Mail" />
                        <asp:BoundField DataField="Idioma" HeaderText="Idioma" />

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
        <br />
        <br />
        <asp:LinkButton ID="linkVolver" SkinID="Volver atras" runat="server" OnClick="linkVolver_Click"><< Volver atras</asp:LinkButton>
        <br />
        <br />
        <asp:Button ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar" SkinID="Exportar" CssClass="btn btn-secondary" />
        <br />
        <br />        
    </div>
    <asp:Label ID="lblPermiso" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>  
</asp:Content>
