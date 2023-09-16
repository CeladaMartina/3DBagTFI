<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateEditUsuario.aspx.cs" Inherits="_3DBag.CreateEditUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
        <asp:TextBox ID="txtIdUsuario" runat="server" Visible="false"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblNick" runat="server" Text="Nick"></asp:Label>
        <br />
        <asp:TextBox ID="txtNick" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
        <br />
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblMail" runat="server" Text="Mail"></asp:Label>
        <br />
        <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>        
        <br />
        <asp:Label ID="lblIdioma" runat="server" Text="Idioma"></asp:Label>
        <br />        
        <asp:TextBox ID="txtIdioma" runat="server" Width="170px"></asp:TextBox>   
        <br />
        <br />
        <label id="lblFamilias" runat="server">Familias:</label>
        <asp:Table ID="Familias" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Familias Asignadas</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell>Familias No Asignadas</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:ListBox ID="FAsig" runat="server" Height="315px" Width="208px"></asp:ListBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnAsignarF" runat="server" Text="<-- Asignar" OnClick="btnAsignarF_Click"/>                     
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnNoAsignarF" runat="server" Text="Desasignar -->" OnClick="btnNoAsignarF_Click"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ListBox ID="FNoAsig" runat="server" Height="315px" Width="208px"></asp:ListBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <label id="lblPatentes" runat="server">Patentes:</label>
        <asp:Table ID="Permisos" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Patentes Asignadas</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell>Patentes No Asignadas</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:ListBox ID="PAsig" runat="server" Height="315px" Width="208px"></asp:ListBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnAsignar" runat="server" Text="<-- Asignar" OnClick="btnAsignar_Click"/>                     
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnNoAsignar" runat="server" Text="Desasignar -->" OnClick="btnNoAsignar_Click"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ListBox ID="PNoAsig" runat="server" Height="315px" Width="208px"></asp:ListBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <asp:Button ID="btnFunction" runat="server" Text="Editar" OnClick="btnFunction_Click"/>
        <br />
        <br />
        <asp:LinkButton ID="LinkRedirect" runat="server" OnClick="LinkRedirect_Click"><< Volver a la lista</asp:LinkButton>
    </div>
</asp:Content>
