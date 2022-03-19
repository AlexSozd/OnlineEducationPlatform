<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonalData.aspx.cs" Inherits="PersonalData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0 - Личная страница просмотра результатов тестирования</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 35px">
        <h1>Результаты тестирования:</h1>
    </div>
    <div style="margin-left: 35px">
        <h2>пользователя <asp:Label ID="UserName" Text="" runat="server"></asp:Label></h2>
    </div>
    <div style="margin-left: 20px">
        <h3>Список выбранных тестов: &nbsp <asp:DropDownList ID="Tests" runat="server" OnSelectedIndexChanged="Tests_SelectedIndexChanged" AutoPostBack="True"/> </h3>
    </div>
    <div style="margin-left: 20px">
        <h3>Попытки: &nbsp <asp:DropDownList ID="Attempts" runat="server" OnSelectedIndexChanged="Attempts_SelectedIndexChanged" AutoPostBack="True"/> </h3>
    </div>
    <div style="margin-left: 20px">
        <h3>Выбрано тестовое задание: &nbsp <asp:DropDownList ID="TestTasks" runat="server" OnSelectedIndexChanged="TestTasks_SelectedIndexChanged" AutoPostBack="True"/> </h3>
    </div>
    <div style="margin-left: 20px">
        <h3>Выберите время сдачи тестов: &nbsp </h3> с &nbsp <asp:TextBox ID="BeginTime" runat="server"></asp:TextBox> &nbsp по &nbsp <asp:TextBox ID="EndTime" runat="server"></asp:TextBox>
    </div>
    <div style="margin-left: 20px"> <p></p> </div>
    <div style="margin-left: 20px; margin-top: 30px"> <asp:Table ID="itemTable" runat="server" style="margin-left: 20px"> <asp:TableHeaderRow></asp:TableHeaderRow> </asp:Table></div>
    <div style="margin-left: 20px"> <p></p> </div>
    <div style="margin-left: 20px; margin-top: 30px"> <asp:Button ID="DetailButton" text="Подробнее>>" runat="server" style="margin-left: 20px" OnClick="DetailButton_Click"/></div>
    <div style="margin-left: 20px"> <p></p> </div>
    <div style="margin-left: 20px; margin-top: 30px; margin-bottom: 30px"> <asp:Table ID="answerList" runat="server" style="margin-left: 20px"> <asp:TableHeaderRow></asp:TableHeaderRow> </asp:Table></div>
    <aside>
        <ul>
            <li><a runat="server" id="hyperlink1" name="hyperlink1">Вернуться обратно</a></li>
        </ul>
    </aside>
    </form>
</body>
</html>
