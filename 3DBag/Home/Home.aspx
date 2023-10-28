<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="_3DBag.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/site.css" rel="stylesheet" />
    <div id="containerHome" runat="server" style="display: flex;margin-top:100px;">
        <asp:Label ID="lblUsuario" runat="server" CssClass="align-self-sm-end" Visible="false"></asp:Label>
        <br />
        <br />
        <div style="width: 100%">
            <asp:Label ID="lblHome" runat="server"
                SkinID="3D- Bag es una empresa especializada en la venta y producción de bolsos impresos con la tecnologia 3D para empresas que requieren ser reconocidos mediante el uso de tecnología de última generación.">
                    <br />
                    3D- Bag es una empresa especializada en la venta y producción de bolsos impresos con la tecnologia 3D para empresas que requieren ser reconocidos mediante el uso de tecnología de última generación.
                    <br />                    
            </asp:Label>
            <asp:Label ID="lblHome2" runat="server"
                SkinID="Permite al cliente diseñar su propio modelo y vendiendo  a mercados de distribución, también ofrecemos nuestro producto a consumidores que quieran un cambio en su hogar.">
                    <br />
                    Permite al cliente diseñar su propio modelo y vendiendo  a mercados de distribución, también ofrecemos nuestro producto a consumidores que quieran un cambio en su hogar.
                    <br />
            </asp:Label>
            <asp:Label ID="lblHome3" runat="server"
                SkinID="Nuestro proyecto se basa en marcar diferencias a la hora de calidad e incorporar elementos que no se utilizan y también utilizar la mejor tecnología como es la impresión 3D.">
                    <br />
                    Nuestro proyecto se basa en marcar diferencias a la hora de calidad e incorporar elementos que no se utilizan y también utilizar la mejor tecnología como es la impresión 3D.<br />
            </asp:Label>
            <asp:Label ID="lblHome4" runat="server"
                SkinID="En cuanto al cliente queremos que ellos tengan una forma de comprar el producto de manera efectiva, simple y sencilla y que cuenten con la mejor calidad.">
                    <br />
                    En cuanto al cliente queremos que ellos tengan una forma de comprar el producto de manera efectiva, simple y sencilla y que cuenten con la mejor calidad.
                    <br />
            </asp:Label>
        </div>
        <div style="width: 100%; display:flex; justify-content: center; align-self: center">
            <asp:Image runat="server" src="../Images/logo.png" Width="350" Height="350" />
        </div>
    </div>
    <br />
    <asp:Table ID="containerError" runat="server" Visible="false">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblError" runat="server" CssClass="align-self-sm-end"></asp:Label>
                <br />
                <br />
                <asp:LinkButton ID="btnRestore" runat="server" OnClick="hrefRestaurar">Restore</asp:LinkButton>
                <br />
                <br />
                <asp:LinkButton ID="btnRecalcularDV" runat="server" OnClick="hrefRecalcular">Recalcular DV</asp:LinkButton>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
