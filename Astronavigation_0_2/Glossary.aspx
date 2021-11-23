<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Glossary.aspx.cs" Inherits="Glossary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>
        Электронный справочник
    </h1>
    </div>
    <div>
    <h2>
        <asp:Label ID="label1" runat="server" />
    </h2>
    </div>
    </form>
    <article>
        <iframe id="PageText" runat="server" class="file" style="border-style: none; border-color: inherit; border-width: medium; height: 382px;" width="1069" aria-readonly="True" aria-hidden="True">
            </iframe>
    </article>
    <asp:Panel ID="panel1" runat="server" />
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
