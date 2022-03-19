<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Statistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0 - Обзор результатов тестирования</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 35px">
        <h1>Статистический обзор</h1>
    </div>
    <div style="margin-left: 20px">
        <h2>Выбраны данные: <asp:Label ID="itemname" runat="server"></asp:Label> </h2>
    </div>
    <div style="margin-left: 20px">
        <h3></h3>
    </div>
    <div style="margin-left: 20px">
        <h3>Список выбранных студентов: &nbsp <asp:DropDownList ID="Persons" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Persons_SelectedIndexChanged"/> </h3>
    </div>
    <div style="margin-left: 20px">
        <h3>Список выбранных тестов: &nbsp <asp:DropDownList ID="Tests" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Tests_SelectedIndexChanged"/> </h3>
    </div>
    <div style="margin-left: 20px">
        <h3>Попытки: &nbsp <asp:DropDownList ID="Attempts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Attempts_SelectedIndexChanged"/> &nbsp
        &nbsp Сдало более &nbsp <input type="number" min="0" max="100" step="any" value="0" name="st_per" id="st_per" form="form1" runat="server"/> % &nbsp
        &nbsp Набрали более &nbsp <input type="number" min="0" max="100" step="any" value="0" name="res_per" id="res_per" form="form1" runat="server"/> % &nbsp
        </h3>
    </div>
    <div style="margin-left: 20px">
        <h3>Выбрано тестовое задание: &nbsp <asp:DropDownList ID="TestTasks" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TestTasks_SelectedIndexChanged"/> </h3>
    </div>
    <div style="margin-left: 20px">
        <h3>Выберите время сдачи тестов: &nbsp </h3>
        с &nbsp <asp:TextBox ID="BeginTime" runat="server"></asp:TextBox> &nbsp по &nbsp <asp:TextBox ID="EndTime" runat="server"></asp:TextBox> &nbsp
        &nbsp <asp:Button id="EnterTime" runat="server" Text="Ввести время" OnClick="EnterTime_Click" />
    </div>
    <div style="margin-left: 20px"> <p></p> </div>
    <div style="margin-left: 20px"> <asp:Table ID="itemTable" runat="server"> <asp:TableHeaderRow></asp:TableHeaderRow> </asp:Table></div>
    <div style="margin-left: 20px"> <p></p> </div>
    <div>
        <table style="margin-left: 20px">
            <tr>
                <td> <b>Статистические параметры выборки: </b> </td>
                <td style="width: 150px"></td>
                <td> <asp:Button id="StatAnalyze" runat="server" Text="Подсчитать" OnClick="StatAnalyze_Click"/> </td>
            </tr>
            <tr>
                <td>  </td>
                <td style="width: 150px"></td>
                <td>  </td>
            </tr>
            <tr>
                <td> Среднее: </td>
                <td style="width: 150px"></td>
                <td> <asp:Label id="MeanValue" runat="server" Text=""></asp:Label> </td>
            </tr>
            <tr>
                <td> Дисперсия: </td>
                <td style="width: 150px"></td>
                <td> <asp:Label id="Disp" runat="server" Text=""></asp:Label> </td>
            </tr>
            <tr>
                <td> Среднее квадратичное отклонение: </td>
                <td style="width: 150px"></td>
                <td> <asp:Label id="StDev" runat="server" Text=""></asp:Label> </td>
            </tr>
        </table>
    </div>
    <aside>
        <ul>
            <li><a runat="server" href="~/Authorization.aspx?from=Statistics.aspx">Выйти</a></li>
        </ul>
    </aside>
    </form>
</body>
</html>
