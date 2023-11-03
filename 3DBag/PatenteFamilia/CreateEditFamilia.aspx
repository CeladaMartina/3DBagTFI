<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateEditFamilia.aspx.cs" Inherits="_3DBag.CreateEditFamilia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblNombre" runat="server" Text="Nombre" SkinID="Nombre"></asp:Label>
    <br />
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblPatente" runat="server" SkinID="Patentes">Patentes:</asp:Label>
    <asp:Table ID="Permisos" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell ID="patasig" SkinID="Patentes Asignadas">Patentes Asignadas</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell ID="patnoasig" SkinID="Patentes No Asignadas">Patentes No Asignadas</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:ListBox ID="PAsig" runat="server" Height="315px" Width="208px" DataValueField="permiso"></asp:ListBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnAsignar" runat="server" Text="<-- Asignar" SkinID="Asignar" OnClick="btnAsignar_Click"/>                     
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnNoAsignar" runat="server" Text="Desasignar -->"  SkinID="Desasignar" OnClick="btnNoAsignar_Click"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ListBox ID="PNoAsig" runat="server" Height="315px" Width="208px" DataValueField="permiso"></asp:ListBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    <br />
    <br />
    <asp:Button ID="btnFunction" runat="server" Text="Editar" SkinID="Editar" OnClick="btnFunction_Click" />
    <br />
    <br />
    <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>
    <br />
    <br />
    <asp:LinkButton ID="LinkRedirect" runat="server" SkinID="Volver a la lista" OnClick="LinkRedirect_Click" ><< Volver a la lista</asp:LinkButton>
</asp:Content>
