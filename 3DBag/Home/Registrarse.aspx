<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="_3DBag.Registrarse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
         <div class="row">
             <div class="col-md-6 mx-auto">
                 <div class="card">
                     <div class="card-body">
                         <div class="row">
                             <div class="col">
                                 <center>
                                     <img width="150px" src="../Images/persona.png"/>
                                 </center>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col">
                                 <center>
                                     <asp:Label ID="Registracion" runat="server" SkinID="Registracion">Registracion</asp:Label>    
                                 </center>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col">
                                 <hr />
                             </div>
                         </div>
                         <div class="row">
                             <div class="col-md-6">
                                 <asp:Label ID="lblNick" runat="server" SkinID="Nick">Nick</asp:Label>                                
                                 <div class="form-group">
                                     <asp:TextBox ID="txtNick" CssClass="form-control" runat="server" placeholder="Nick"></asp:TextBox>
                                 </div>
                             </div>                                                        
                             <div class="col-md-6">
                                 <asp:Label ID="Contraseña" runat="server" SkinID="Contraseña">Contraseña</asp:Label>                                 
                                 <div class="form-group">
                                     <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                 </div>
                             </div>                             
                             <div class="col-md-6">
                                 <asp:Label ID="Nombre" runat="server" SkinID="Nombre">Nombre</asp:Label>                                 
                                 <div class="form-group">
                                     <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" placeholder="Nombre" ></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <asp:Label ID="Apellido" runat="server" SkinID="Apellido">Apellido</asp:Label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" placeholder="Apellido" ></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <asp:Label ID="DNI" runat="server" SkinID="DNI">DNI</asp:Label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtDNI" CssClass="form-control" runat="server" placeholder="DNI" TextMode="Number"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <asp:Label ID="Email" runat="server" SkinID="Email">Email</asp:Label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <asp:Label ID="Telefono" runat="server" SkinID="Telefono">Telefono</asp:Label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" placeholder="Telefono" TextMode="Phone"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <asp:Label ID="Fecha_de_Nacimiento" runat="server" SkinID="Fecha de Nacimiento">Fecha de Nacimiento</asp:Label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtFecha" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <asp:Label ID="Idioma" runat="server" SkinID="Idioma">Idioma</asp:Label>
                                 <div class="form-group">
                                     <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                         <asp:ListItem  Text="Select" Value="Select"></asp:ListItem>
                                         <asp:ListItem Text="Ingles" Value="Ingles"></asp:ListItem>
                                         <asp:ListItem Text="Español" Value="Español"></asp:ListItem>
                                     </asp:DropDownList>
                                 </div>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col">
                                 <center>
                                     <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn btn-success btn-block btn-lg" OnClick="Registrar" />
                                 </center>
                             </div>
                         </div>
                          <asp:Label ID="lblError" runat="server" Text="Label" CssClass="alert alert-danger" Visible="false"></asp:Label>
                     </div>
                 </div>             
                <asp:LinkButton ID="linkVolver" SkinID="Volver atras" runat="server" OnClick="linkVolver_Click"><< Volver atras</asp:LinkButton>
                 <br />
             </div>
         </div>
    </div>
</asp:Content>
