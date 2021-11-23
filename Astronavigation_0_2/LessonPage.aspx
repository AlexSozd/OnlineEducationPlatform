<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LessonPage.aspx.cs" Inherits="LessonPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0</title>
</head>
<body style="height: 339px">
    <form id="form1" runat="server">
    <div>
    <article>
        <h1>
            <asp:Label ID="label1" runat="server" />
        </h1>
        <h2>
            <asp:Label ID="label2" runat="server" />
        </h2>
    </article>
        <asp:Button ID="button1" runat="server" Text="Назад" OnClientClick="button1_Click" OnClick="button1_Click" />
            <asp:Button ID="button2" runat="server" Text="Вперёд" OnClientClick="button2_Click" OnClick="button2_Click" />
    </div>
        <div>
            <iframe id="PageText" runat="server" class="file" width="1066" style="border: none; border-color: inherit; border-width: medium; height: 277px;" aria-readonly="True" aria-hidden="True">
            </iframe> 
        </div>
          <asp:Panel ID="panel1" runat="server" />    
    </form>
    <aside>
        <p>        
            Переход на страницу:
        </p>
        <ul>
            <li><a runat="server" href="~/">Главная</a></li>
            <li><a runat="server" href="~/About.aspx">О программе</a></li>
            <li><a runat="server" href="~/Help.aspx">Справка</a></li>
        </ul>
    </aside>
</body>
</html>
