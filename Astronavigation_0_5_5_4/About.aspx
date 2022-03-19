<%@ Page Language="C#" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0 - О программе</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 20px">
        <div>
            <h1 style="margin-left: 20px">О программе</h1>
            <p style="text-indent: 20px">
            "Астронавигация" - электронное учебно-справочное пособие по астрономии и наземной навигации, астрономическим методам навигации.
            В пособие входят уроки с изложением базовых основ этих областей знаний, теоретического обоснования
            и практического применения астрономических методов решения навигационных задач, справочник астрономических понятий и навигационных терминов.
            </p>
            <p style="text-indent: 20px; margin-top: -12px">
            Дополнительно в комплект включены наборы сгруппированных по тематике задач и тестовых заданий, тренажёры - симуляторы интерфейсов расчётных модулей
            навигационных программ.
            </p>
        </div>
    </div>
    <aside style="margin-left: 20px; margin-top: 70px">
        <p style="text-indent: 20px">        
            Переход на страницу:
        </p>
        <ul>
            <li><a runat="server" href="~/">Главная</a></li>
            <li><a runat="server" href="~/Help.aspx">Справка</a></li>
        </ul>
    </aside>
    </form>       
    </body>
</html>
