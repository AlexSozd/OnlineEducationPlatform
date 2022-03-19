<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetPassword.aspx.cs" Inherits="GetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Восстановление пароля</h2>
    </div>
        <div>
            <table style="margin-left: 20px">
            <tr>
                <td> <b>Логин: </b> </td><td> <asp:TextBox ID="textBox1" runat="server" OnTextChanged="textBox1_TextChanged"> </asp:TextBox> </td>
            </tr>
            <tr>
                <td> <b>Новый пароль: </b> </td>
                <td> <asp:TextBox ID="textBox2" runat="server" OnTextChanged="textBox2_TextChanged"> </asp:TextBox> </td>
            </tr>
            <tr>
                <td> <b>Введите новый пароль ещё раз: </b> </td>
                <td> <asp:TextBox ID="textBox3" runat="server" OnTextChanged="textBox3_TextChanged"> </asp:TextBox> </td>
            </tr>
        </table>
            <div>

            </div>
        </div>
        <div style="margin-left: 20px">
            <asp:Button ID="button1" runat="server" Text="Enter" OnClick="button1_Click"/>
        </div>
         <div>

            </div>
        <div> <asp:Label ID="label1" runat="server" Text="Note: Password must have 8 symbols - letters and numbers." Visible="False"></asp:Label> </div>
    </form>
</body>
</html>
