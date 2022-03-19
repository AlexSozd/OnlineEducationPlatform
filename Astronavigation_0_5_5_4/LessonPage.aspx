<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LessonPage.aspx.cs" Inherits="LessonPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-indent: 20px; margin-left: 20px">
    <article>
        <h1>
            <asp:Label ID="label1" runat="server" />
        </h1>
        <h2>
            <asp:Label ID="label2" runat="server" />
        </h2>
    </article>
        <p>
            <asp:Button ID="button1" runat="server" Text="Назад" OnClientClick="button1_Click" OnClick="button1_Click" />
            <asp:Button ID="button2" runat="server" Text="Вперёд" OnClientClick="button2_Click" OnClick="button2_Click" />
        </p>
    </div>
        <div style="text-indent: 20px; margin-left: 20px">
            <iframe id="PageText" runat="server" class="file" height="1455" width="1066" style="border: 0; border-color: inherit; border-width: medium" aria-readonly="True" aria-hidden="True">
            </iframe> 
        </div>
        <asp:Panel ID="panel1" runat="server" style="margin-top: 20px; margin-left: 60px"></asp:Panel>    
    </form>
    <aside style="margin-left: 20px; margin-top: 70px">
        <p style="text-indent: 20px">        
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
