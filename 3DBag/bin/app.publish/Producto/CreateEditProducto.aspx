﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateEditProducto.aspx.cs" Inherits="_3DBag.CreateEditProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblCodProd" runat="server" SkinID="Codigo Producto" Text="CodProd"></asp:Label>
        <br />
        <asp:TextBox ID="txtCodProd" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre" SkinID="Nombre"></asp:Label>
        <br />
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
        <br />
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion" SkinID="Descripcion"></asp:Label>
        <br />
        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="lblMaterial" runat="server" Text="Material" SkinID="Material"></asp:Label>
        <br />
        <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="lblStock" runat="server" Text="Stock" SkinID="Stock"></asp:Label>
        <br />
        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        <br />
        <asp:Label ID="lblPUnit" runat="server" Text="Precio" SkinID="Precio" ></asp:Label>
        <br />
        <asp:TextBox ID="txtPUnit" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        <br />
        <br />        
        <asp:Image ID="Imagen" runat="server" ImageUrl='<%#Bind("Imagen") %>' Height="100px" Width="100px" Visible="false"/>
        <br />
        <br />
        <asp:FileUpload ID="FileUploadImagen" runat="server"/>
        <br />
        <br />
        <asp:Button ID="btnFunction" runat="server" OnClick="btnFunction_Click" />
        <br />
        <br />
        <asp:Label ID="lblRespuesta" runat="server"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="LinkRedirect" runat="server" OnClick="LinkRedirect_Click" SkinID="Volver a la lista"><< Volver a la lista</asp:LinkButton>
    </div>
</asp:Content>
