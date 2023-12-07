<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndexFamilias.aspx.cs" Inherits="_3DBag.IndexFamilias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblTitulo" SkinID="Familias" runat="server">Familias</asp:Label>
        <br />          
        <br />    
    <div id="divFamilias" runat="server">
        <asp:Button  ID="btnAltaFamilia" runat="server" OnClick="btnAltaFamilia_Click" Text="Alta Familia" SkinID="Agregar" CssClass="btn btn-primary" />
    <br />          
    <br />
    <asp:GridView ID="gridFamilias" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover" OnRowCommand="gridFamilias_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
        <RowStyle BackColor="#f5f5f5" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />

            <asp:ButtonField ButtonType="Button" CommandName="editar" Text="Editar">
                <ControlStyle BackColor="#6699FF" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Button" CommandName="select" Text="Ver">
                <ControlStyle BackColor="Lime" />
            </asp:ButtonField>            
        </Columns>
    </asp:GridView>
        <br />
        <br />
        <asp:LinkButton ID="linkVolver" SkinID="Volver atras" runat="server" OnClick="linkVolver_Click"><< Volver atras</asp:LinkButton>
    </div>
    <asp:Label ID="lblPermiso" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>  
</asp:Content>
