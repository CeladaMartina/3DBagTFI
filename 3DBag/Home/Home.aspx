<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="_3DBag.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="containerHome" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Image runat="server" src="../Images/logo.png" width="170" height="170" style="display: flex; justify-content: left; align-items: center;"/>
                <asp:Label ID="lblUsuario" runat="server" CssClass="align-self-sm-end" Visible="false"></asp:Label>  
                <br />
                <br />
                <asp:Label ID="lblHome" runat="server">
                    3D- Bag es una empresa especializada en la venta y producción de bolsos impresos con la tecnologia 
                    3D tanto para empresas que requieren ser reconocidos mediante el uso de tecnología de última generación; 
                    permitiendo al cliente diseñar su propio modelo y vendiendo  a mercados de distribución, también ofrecemos nuestro producto a consumidores que quieran un cambio en su hogar actualizándose con tecnología de última generación.
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Table ID="containerError" runat="server" Visible="false">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblError" runat="server" CssClass="align-self-sm-end"></asp:Label>  
                <br />
                <br />
                <asp:LinkButton ID="btnRestore" runat="server">Restore</asp:LinkButton>
                <br />
                <br />
                <asp:LinkButton ID="btnRecalcularDV" runat="server">Recalcular DV</asp:LinkButton>
            </asp:TableCell>            
        </asp:TableRow>
    </asp:Table>
</asp:Content>
