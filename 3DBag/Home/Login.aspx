<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_3DBag.Login" %>

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
                                    <img width="150px" src="../Images/persona.png" />
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Member Login</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Nick</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNick" CssClass="form-control" runat="server" placeholder="Nick"></asp:TextBox>
                                </div>
                                <br />
                                <label>Contraseña</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtContraseña" CssClass="form-control" runat="server" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success btn-block btn-lg" OnClick="logguear" />
                                    <%--<input class="btn btn-info btn-block btn-lg" id="button2" type="button" value="Registrarse" />--%>
                                </div>                               
                                <br />
                                <div class="form-group">
                                    <asp:LinkButton runat="server">¿Olvidaste tu contraseña?</asp:LinkButton>
                                </div>
                                <asp:Label ID="lblError" runat="server" Text="Label" CssClass="alert alert-danger" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="../Home/Home.aspx"><< Volver al Inicio
                </a>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
