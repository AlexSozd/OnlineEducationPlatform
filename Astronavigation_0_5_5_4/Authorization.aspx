<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Authorization.aspx.cs" Inherits="Authorization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0 - Авторизация</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 20px">
        <h1 style="margin-left: 20px">Авторизация (вход в систему):</h1>
    </div>
    <div style="text-indent: 20px; margin-left: 20px">
        <asp:Login ID="Login1" runat="server" BorderStyle="Solid" BorderColor="Black" BackColor="WhiteSmoke" BorderWidth="2px" 
                UserNameLabelText="Login:" PasswordLabelText="Password:" PasswordRecoveryUrl="~/GetPassword.aspx" CreateUserText="Create a new user..." OnAuthenticate="Login1_Authenticate" OnLoggingIn="Login1_LoggingIn" OnLoginError="Login1_LoginError">
        </asp:Login>
    </div>
    </form>
</body>
</html>
