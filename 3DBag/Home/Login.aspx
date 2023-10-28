<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_3DBag.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblError" runat="server" Text="Label" Visible="false"></asp:Label>
    <br />
    <br />
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
                                    <asp:Label ID="MemberId" SkinID="Miembro" runat="server">Miembro</asp:Label>
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
                                <asp:Label ID="nickId" runat="server" SkinID="Nick">Nick</asp:Label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNick" CssClass="form-control" runat="server" placeholder="Nick" SkinID="Nick"></asp:TextBox>
                                </div>
                                <br />
                                <asp:Label ID="contraseñaId" runat="server" SkinID="Contraseña">Contraseña</asp:Label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtContraseña" CssClass="form-control" runat="server" placeholder="Contraseña" TextMode="Password" SkinID="Contraseña"></asp:TextBox>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success btn-block btn-lg" OnClick="logguear" SkinID="Ingresar" /> 
                                </div>                               
                                <br />
                                <div class="form-group">
                                    <asp:LinkButton runat="server" ID="Olvidaste" SkinID="¿Olvidaste tu contraseña?" OnClick="Olvidaste_Click">¿Olvidaste tu contraseña?</asp:LinkButton>
                                </div>                                
                            </div>
                        </div>
                    </div>
                </div>
                <br />             
                <asp:LinkButton ID="linkVolver" SkinID="Volver atras" runat="server" OnClick="linkVolver_Click"><< Volver atras</asp:LinkButton>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
