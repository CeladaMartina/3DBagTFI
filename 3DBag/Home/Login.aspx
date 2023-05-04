<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_3DBag.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>3D-Bag</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" rel="stylesheet" />
    <link href="../css/site.css" rel="stylesheet" />
</head>
<body>
    <form id="formLogin" runat="server">       
        <div class="container-fluid">
            <main role="main">
                <div class="row">
                    <div class="col">
                        <div class="wrapper">
                            <div class="formContent">
                                <h2>Ingresar</h2>
                                <div class="form-group">
                                    <asp:Label ID="lblUsuario" runat="server" Text="Nick:"></asp:Label>
                                    <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" placeholder="Nick"></asp:TextBox>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Label ID="lblPassword" runat="server" Text="Contraseña:"></asp:Label>
                                    <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server" placeholder="Contraseña"></asp:TextBox>
                                </div>
                                <br />
                                <div class="row">
                                    <asp:Label ID="lblError" CssClass="alert-danger" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <div class="mt-4">
                                        <asp:Button ID="btnIngresar" CssClass="btn btn-primary" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
