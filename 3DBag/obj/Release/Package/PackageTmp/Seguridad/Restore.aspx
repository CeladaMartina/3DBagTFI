<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Restore.aspx.cs" Inherits="_3DBag.Restore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="lblTitulo" SkinID="Restauracion" runat="server">Restauración</asp:Label>
        <br />
        <br />
    <div id="divGeneral" runat="server">        
        <asp:Label ID="lblRuta" runat="server" Text="Ruta" SkinID="Ruta"></asp:Label>     
        <br />
        <asp:TextBox ID="txtRuta" runat="server" Width="433px" CssClass="form-control"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnRestore" runat="server" Text="Generar" CssClass="btn btn-success btn-block btn-lg" OnClick="Generar" SkinID="Generar" />
        <br />
        <br />
        <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>  
        <br />
        <br />
        <asp:LinkButton ID="linkVolver" SkinID="Volver atras" runat="server" OnClick="linkVolver_Click"><< Volver atras</asp:LinkButton>
    </div>
     <asp:Label ID="lblPermiso" runat="server" Visible="false"></asp:Label>   
</asp:Content>
