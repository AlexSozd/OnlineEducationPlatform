<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Учебно-справочное пособие  
    </div>
        <div>Оглавление </div>
       <article>
           <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">

           </asp:TreeView>
    </article> 
    </form>
    <aside>
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
