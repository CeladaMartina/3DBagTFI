<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndexUsuarios.aspx.cs" Inherits="_3DBag.IndexUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Usuarios</h1>
        <a class="nav-link" href="">Nuevo Usuario</a>
        <br />
        <div class="form-group">
            <asp:GridView ID="gridUsuarios" runat="server" AutoGenerateColumns="false" DataSourceID="Usuarios">
                <Columns>
                    <asp:BoundField DataField="Nick" HeaderText="Nick"/>
                    <asp:BoundField DataField="Mail" HeaderText="Email"/>
                    <asp:BoundField DataField="Contraseña" HeaderText="Contraseña"/>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre"/>
                    <asp:BoundField DataField="IdIdioma" HeaderText="Idioma"/>
                </Columns>
            </asp:GridView>
        </div>

    </div>
</asp:Content>
