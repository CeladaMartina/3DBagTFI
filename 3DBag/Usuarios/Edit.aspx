<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="_3DBag.Edit" %>
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
                                     <h3>Editar Usuario</h3>
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
                                    <asp:TextBox ID="txtIdUsuario" runat="server" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Nombre</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" placeholder="Nombre"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Mail</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtMail" CssClass="form-control" runat="server" placeholder="Mail"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Estado Bloqueado</label>
                                <div class="form-group">
                                    <asp:RadioButton ID="rdbBloqueado" runat="server" />                                                                    
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Idioma</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtIdioma" CssClass="form-control" runat="server" placeholder="Idioma"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Estado de Baja</label>
                                <div class="form-group">
                                    <asp:RadioButton ID="rdbBaja" runat="server" />                                                                    
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-success btn-block btn-lg" OnClick="ModificarUsuario" />
                        <br />
                        <br />
                        <a href="../Usuarios/IndexUsuarios.aspx"><< Volver</a>
                    </div>
                </div>
            </div>
        </div>        
    </div>
</asp:Content>
