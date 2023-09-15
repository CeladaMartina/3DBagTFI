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
        <asp:Table runat="server">
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
                    <asp:Button ID="btnAsignar" runat="server" Text="<-- Asignar"/>                     
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnNoAsignar" runat="server" Text="Desasignar -->"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ListBox ID="PNoAsig" runat="server" Height="315px" Width="208px"></asp:ListBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="btnFunction" runat="server" Text="Editar"/>
        <br />
        <br />
        <asp:LinkButton ID="LinkRedirect" runat="server"><< Volver a la lista</asp:LinkButton>
    </div>
</asp:Content>
