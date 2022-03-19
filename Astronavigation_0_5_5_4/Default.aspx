<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0 - Главная</title>
</head>
<body>
    <form id="form1" runat="server">
    <div><p>Учебно-справочное пособие</p></div>
    <div><p>Оглавление</p>
    </div>
       <article>
           <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">

           </asp:TreeView>
       </article> 
    </form>
    <aside style="text-indent: 20px; margin-left: 20px">
        <p>        
            Тренажёры:
        </p>
        <ul>
            <li><a runat="server" href="~/Calculation.aspx">Тренажёр расчётов координат места (аналитическое решение Гаусса)</a></li>
            <li><a runat="server" href="~/Calculation1.aspx">Тренажёр расчётов координат места (метод высотных линий положения)</a></li>
        </ul>
    </aside>
    <aside style="text-indent: 20px; margin-left: 20px">
        <p>        
            Переход на страницу:
        </p>
        <ul>
            <li><a runat="server" href="~/About.aspx">О программе</a></li>
            <li><a runat="server" href="~/Help.aspx">Справка</a></li>
        </ul>
    </aside>
</body>
</html>
