<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateEditUsuario.aspx.cs" Inherits="_3DBag.CreateEditUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblUsuario" SkinID="Usuarios" runat="server">Usuarios</asp:Label>
        <br />          
        <br />
    <div id="divUsuarios" runat="server">
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
        <asp:TextBox ID="txtIdUsuario" runat="server" Visible="false"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblNick" runat="server" Text="Nick" SkinID="Nick"></asp:Label>
        <br />
        <asp:TextBox ID="txtNick" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
        <br />
        <asp:Label ID="lblContraseña" runat="server" Text="Contraseña" SkinID="Contraseña"></asp:Label>
        <br />
        <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre" SkinID="Nombre"></asp:Label>
        <br />
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
        <br />
        <asp:Label ID="lblMail" runat="server" Text="Mail" SkinID="Email"></asp:Label>
        <br />
        <asp:TextBox ID="txtMail" runat="server" Width="225px" CssClass="form-control" TextMode="Email"></asp:TextBox>        
        <br />
        <asp:Label ID="lblIdioma" runat="server" Text="Idioma" SkinID="Idioma"></asp:Label>
        <br />        
        <asp:TextBox ID="txtIdioma" runat="server" Width="170px" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>  
        <br />
        <asp:Label ID="lblBloqueado" runat="server" Text="Bloqueado" SkinID="Bloqueado"></asp:Label><asp:CheckBox ID="checkBloqueado" runat="server" />
        <br />
        <br />
        <asp:Label ID="lblBaja" runat="server" Text="Baja" SkinID="Baja"></asp:Label><asp:CheckBox ID="checkBaja" runat="server" />
        <br />
        <br />
        <br />
        <asp:Label ID="lblFamilia" runat="server" SkinID="Familias">Familias:</asp:Label>        
        <asp:Table ID="tituloFam" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell ID="tituloFamAsig" SkinID="Familias Asignadas">Familias Asignadas</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell ID="tituloNoFamAsig" SkinID="Familias No Asignadas">Familias No Asignadas</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:ListBox ID="FAsig" runat="server" Height="315px" Width="208px"></asp:ListBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnAsignarF" runat="server" Text="<-- Asignar" SkinID="Asignar" OnClick="btnAsignarF_Click"/>                     
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnNoAsignarF" runat="server" Text="Desasignar -->" SkinID="Desasignar" OnClick="btnNoAsignarF_Click"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ListBox ID="FNoAsig" runat="server" Height="315px" Width="208px"></asp:ListBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <asp:Label ID="lblPatente" runat="server" SkinID="Patentes">Patentes:</asp:Label>      
        <asp:Table ID="Permisos" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell ID="tituloPatAsig" SkinID="Patentes Asignadas">Patentes Asignadas</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell>-</asp:TableHeaderCell>
                <asp:TableHeaderCell ID="tituloPatNoAsig" SkinID="Patentes No Asignadas">Patentes No Asignadas</asp:TableHeaderCell>
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
        <asp:Button ID="btnFunction" runat="server" Text="Editar" SkinID="Editar" OnClick="btnFunction_Click"/>
        <br />
        <br />
        <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="LinkRedirect" runat="server" SkinID="Volver a la lista" OnClick="LinkRedirect_Click"><< Volver a la lista</asp:LinkButton>
    </div>
    <asp:Label ID="lblPermiso" runat="server" Visible="false"></asp:Label>
</asp:Content>
