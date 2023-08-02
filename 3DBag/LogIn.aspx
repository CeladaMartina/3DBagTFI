<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="_3DBag.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
                                </div>
                                <div class="form-group">
                                    <input class="btn btn-info btn-block btn-lg" id="button2" type="button" value="Registrarse" />
                                </div>
                                <br />
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
    </form>
</body>
</html>
