﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="_3DBag.Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div id="divBackup" class="container" runat="server">
        <h1>BackUp</h1>
        <br />
        <asp:Label ID="lblRuta" runat="server" Text="Ruta" SkinID="Ruta"></asp:Label>        
        <asp:TextBox ID="txtRuta" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label> 
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnBackup" runat="server" Text="Generar" CssClass="btn btn-success btn-block btn-lg" OnClick="Generar" SkinID="Realizar" />
        <br />
        <br />
        <asp:Label ID="lblResultado" runat="server" Visible="false"></asp:Label>    
    </div>
    <asp:Label ID="lblPermiso" runat="server" Visible="false"></asp:Label>   
</asp:Content>
