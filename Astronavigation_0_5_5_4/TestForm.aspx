<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestForm.aspx.cs" Inherits="TestForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0 - Тест</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 20px">
    <article>
        <h1>
            <asp:Label ID="label1" runat="server" text="Тестовый блок по теме: "/>
        </h1>
        <h2>
            <asp:Label ID="label2" runat="server"/>
        </h2>
    </article>
        <asp:Button ID="button1" runat="server" Text="Назад" OnClientClick="button1_Click" OnClick="button1_Click" />
            <asp:Button ID="button2" runat="server" Text="Вперёд" OnClientClick="button2_Click" OnClick="button2_Click" />
    </div>
    <div style="margin-left: 20px">
        <asp:Table ID="Table1" runat="server"> </asp:Table>
    </div>
    <div>
        <p style="margin-left: 23px">
            <asp:Button id="button3" text="Отправить" runat="server" OnClick="button3_Click"/>
        </p>
    </div>
    <aside>
        <ul>
            <li><a runat="server" id="hyperlink1" name="hyperlink1">Посмотреть результаты прохождения других тестов и попыток</a></li>
        </ul>
    </aside>
    </form>
</body>
</html>