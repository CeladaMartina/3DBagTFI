<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndexUsuarios.aspx.cs" Inherits="_3DBag.IndexUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Usuarios</h1>
        <a class="nav-link" href="">Nuevo Usuario</a>
        <br />
        <div class="form-group">
            <asp:GridView ID="gridUsuarios" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Nick" HeaderText="Nick"/>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre"/> 
                    <asp:BoundField DataField="Mail" HeaderText="Mail"/>                    
                    <asp:BoundField DataField="NombreIdioma" HeaderText="Idioma"/>
                    <asp:ButtonField ButtonType="Button" CommandName="editar" Text="Editar" >
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
</asp:Content>
