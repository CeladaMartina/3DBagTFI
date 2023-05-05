<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="_3DBag.Formulario_web12" %>
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
                                     <h3>Registrarse</h3>
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
                                <label>Nick</label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtNick" CssClass="form-control" runat="server" placeholder="Nick"></asp:TextBox>
                                 </div>
                             </div>                                                        
                             <div class="col-md-6">
                                 <label>Contraseña</label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                                 </div>
                             </div>                             
                             <div class="col-md-6">
                                 <label>Nombre</label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" placeholder="Nombre" ></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <label>Apellido</label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" placeholder="Apellido" ></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <label>DNI</label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtDNI" CssClass="form-control" runat="server" placeholder="DNI" TextMode="Number"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <label>Email</label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <label>Telefono</label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" placeholder="Telefono" TextMode="Phone"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <label>Fecha de Nacimiento</label>
                                 <div class="form-group">
                                     <asp:TextBox ID="txtFecha" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-6">
                                 <label>Idioma</label>
                                 <div class="form-group">
                                     <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                         <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                         <asp:ListItem Text="Ingles" Value="Ingles"></asp:ListItem>
                                         <asp:ListItem Text="Español" Value="Español"></asp:ListItem>
                                     </asp:DropDownList>
                                 </div>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col">
                                 <center>
                                     <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn btn-success btn-block btn-lg" />
                                 </center>
                             </div>
                         </div>








                         <%--<div class="row">
                             <div class="col">
                                 
                                 <br />
                                 
                                 <br />
                                 <div class="form-group">
                                     <asp:Button ID="Button1" runat="server" Text="Login" CssClass="btn btn-success btn-block btn-lg" />
                                 </div>
                                 <div class="form-group">
                                     <input class="btn btn-info btn-block btn-lg" id="button2" type="button" value="Registrarse" />
                                 </div>
                             </div>
                         </div>--%>
                     </div>
                 </div>             
                <a href="Home/Home.aspx">
                    << Volver al Inicio
                </a>
                 <br />
             </div>
         </div>
    </div>
</asp:Content>
