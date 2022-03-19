<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calculation.aspx.cs" Inherits="Calculation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Тренажёр № 1 расчётов координат места (с инструкцией)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1 style="margin-left: 20px">Аналитическое решение Гаусса: </h1>
    </div>
    <div>
        <p style="text-indent: 20px; margin-left: 20px">
            Прямое аналитическое решение расчёта координат места по координатам светил на небесной сфере было написано Карлом Фридрихом Гауссом
            в 1808 году по заказу венского генерального штаба. Оно состояло в решении системы из двух уравнений:
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>sin h<sub>1</sub> = sin φ sin δ<sub>1</sub> + cos φ cos δ<sub>1</sub> cos (t<sub>G1</sub> ± λ) </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>sin h<sub>2</sub> = sin φ sin δ<sub>2</sub> + cos φ cos δ<sub>2</sub> cos (t<sub>G2</sub> ± λ) </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            где <i>h<sub>1</sub></i>, <i>h<sub>2</sub></i> – высоты светил 1 и 2, <i>δ<sub>1</sub></i>, <i>δ<sub>2</sub></i>, <i>t<sub>G1</sub></i>, <i>t<sub>G2</sub></i> – их склонения и гринвические часовые углы, <i>φ</i> и <i>λ</i> – ширина и долгота 
            наблюдателя. </p>
        <p style="text-indent: 20px; margin-left: 20px">
            Решение: 1.) разделить первое выражение на <i>cos δ<sub>1</sub></i>, второе на <i>cos δ<sub>2</sub></i>.
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>tg δ<sub>1</sub> sin φ + cos φ cos (t<sub>G1</sub> ± λ) – sin h<sub>1</sub> sec δ<sub>1</sub> = 0 </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>tg δ<sub>2</sub> sin φ + cos φ cos (t<sub>G2</sub> ± λ) – sin h<sub>2</sub> sec δ<sub>2</sub> = 0 </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            2.) ввести замену переменных: <i>a<sub>1</sub> = tg δ<sub>1</sub></i>, <i>b<sub>1</sub> = – sin h<sub>1</sub> sec δ<sub>1</sub></i>, <i>c = cos(t<sub>G1</sub> – t<sub>G2</sub>)</i>, <i>a<sub>2</sub> = tg δ<sub>2</sub></i>, <i>b<sub>2</sub> = – sin h<sub>2</sub> sec δ<sub>2</sub></i>. </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>a<sub>1</sub> sin φ + cos φ cos (t<sub>G1</sub> – λ) + b<sub>1</sub> = 0 </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>a<sub>2</sub> sin φ + cos φ cos (t<sub>G2</sub> – λ) + b<sub>2</sub> = 0 </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            3.) ввести вторую замену: <i>x = sin φ</i>, <i>y = cos(t<sub>G1</sub> – λ)</i>.</p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>a<sub>1</sub> x + <span>&radic;</span><span style="border-top:1px solid">1 – x<sup>2</sup></span> + b<sub>1</sub> = 0 </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>a<sub>2</sub> x + (cy + &radic;<span style="border-top:1px solid">(1 – c<sup>2</sup>)(1 – x<sup>2</sup>)</span> ) &radic;<span style="border-top:1px solid">1 – x<sup>2</sup></span> + b<sub>2</sub> = 0 </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            4.) выразить, используя первое ур-е, <i>y</i> через <i>x</i>: <i>y = – (a<sub>1</sub>x + b<sub>1</sub>) / &radic;<span style="border-top:1px solid">1 – x<sup>2</sup></span></i>.
            Из второго получатся квадратное ур-е вида <i>Ax<sup>2</sup> + 2Bx + C = 0</i>, где:
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>A = 1 + a<sub>1</sub><sup>2</sup> + a<sub>2</sub><sup>2</sup> – 2a<sub>1</sub>a<sub>2</sub>c – c<sup>2</sup> </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>B = a<sub>1</sub>b<sub>1</sub> + a<sub>2</sub>b<sub>2</sub> – (a<sub>1</sub>b<sub>2</sub> + a<sub>2</sub>b<sub>2</sub> + a<sub>2</sub>b<sub>1</sub>)c </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>C = b<sub>1</sub><sup>2</sup> + b<sub>2</sub><sup>2</sup> – 2b<sub>1</sub>b<sub>2</sub>c – 1 </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            Получаем два решения: <i>x<sub>1,2</sub> = ( – B ± &radic;<span style="border-top:1px solid">B<sup>2</sup> – AC</span> ) / A </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            Подставим их и находим искомые значения: <i>φ = arcsin x</i>, <i>y = – (a<sub>1</sub>x + b<sub>1</sub>) / &radic;<span style="border-top:1px solid">1 – x<sup>2</sup></span></i>, <i>λ = t<sub>G1</sub> – arccos y</i>.
        </p>
    </div>
    <div>
    <p style="margin-left: 23px"><b>Дата: </b><br />
        <input type="date" name="cur_date" id="cur_date" form="form1" runat="server"/>
    </p>
    </div>
        <div>
            <h2 style="margin-left: 20px">Первое наблюдение: </h2>
        </div>
    <div>
        <table style="margin-left: 20px">
            <tr>
                <td> <b>Тело: </b> </td><td style="width: 150px"></td><td> <b>Время: </b> </td>
            </tr>
            <tr>
                <td> <asp:DropDownList id="fir_body1" runat="server" OnSelectedIndexChanged="fir_body1_SelectedIndexChanged"> </asp:DropDownList> </td>
                <td style="width: 150px"></td>
                <td> <input type="datetime" name="fir_time" id="fir_time" form="form1" runat="server" required="required"/> </td>
            </tr>
        </table>
        
        <p style="margin-left: 23px"><b>Измеренная высота светила: </b><br />
        <input type="number" min="0" max="90" step="any" name="fir_ht" id="fir_ht" form="form1" runat="server" required="required"/>
        </p>

        <table style="margin-left: 20px">
            <tr>
                <td> <b>Высота над морем *: </b> </td><td style="width: 100px"></td><td> <b>Температура **: </b> </td><td style="width: 100px"></td><td> <b>Атмосферное давление **: </b> </td>
            </tr>
            <tr>
                <td> <input type="number" min="0" step="any" name="fir_ht1" id="fir_ht1" form="form1" runat="server" required="required"/> </td>
                <td style="width: 100px"></td>
                <td> <input type="number" min="-100" max="100" step="any" name="fir_t" id="fir_t" form="form1" runat="server" required="required"/> </td>
                <td style="width: 100px"></td>
                <td> <input type="number" min="0" step="any" value="760" name="fir_p" id="fir_p" form="form1" runat="server" required="required"/> </td>
            </tr>
        </table>

    </div>
        <div>
            <h2 style="margin-left: 20px">Второе наблюдение: </h2>
        </div>
    <div>
        <table style="margin-left: 20px">
            <tr>
                <td> <b>Тело: </b> </td><td style="width: 150px"></td><td> <b>Время: </b> </td>
            </tr>
            <tr>
                <td> <asp:DropDownList id="sec_body1" runat="server" OnSelectedIndexChanged="sec_body1_SelectedIndexChanged"> </asp:DropDownList> </td>
                <td style="width: 150px"></td>
                <td> <input type="datetime" name="sec_time" id="sec_time" form="form1" runat="server" required="required"/> </td>
            </tr>
        </table>
        
        <p style="margin-left: 23px"><b>Измеренная высота светила: </b><br />
        <input type="number" min="0" max="90" step="any" name="sec_ht" id="sec_ht" form="form1" runat="server" required="required"/>
        </p>

        <table style="margin-left: 20px">
            <tr>
                <td> <b>Высота над морем *: </b> </td><td style="width: 100px"></td><td> <b>Температура **: </b> </td><td style="width: 100px"></td><td> <b>Атмосферное давление **: </b> </td>
            </tr>
            <tr>
                <td> <input type="number" min="0" step="any" name="sec_ht1" id="sec_ht1" form="form1" runat="server" required="required"/> </td>
                <td style="width: 100px"></td>
                <td> <input type="number" min="-100" max="100" step="any" name="sec_t" id="sec_t" form="form1" runat="server" required="required"/> </td>
                <td style="width: 100px"></td>
                <td> <input type="number" min="0" step="any" value="760" name="sec_p" id="sec_p" form="form1" runat="server" required="required"/> </td>
            </tr>
        </table>

    </div>
        <div>
            <p style="text-indent: 20px; margin-left: 20px">
                * Значение высоты наблюдателя над морем (в метрах) нужно для определения истинного горизонта и исправления измеренной высоты. </p>
            <p style="text-indent: 20px; margin-left: 20px">
                ** Значения температуры и давления необходимы для поправки высоты за атмосферную рефракцию. </p>
            <p style="text-indent: 20px; margin-left: 20px">
                Примечание: при измерении высоты звезды нужно отсчитывать от видимого горизонта. Для Солнца и Луны в качестве высоты указывать высоту
                нижнего края диска. Высоту указывать в градусной мере.
            </p>
        </div>
        <div>
            <p style="margin-left: 23px">
                <asp:Button id="button1" text="Ввод" runat="server" OnClick="button1_Click"/>
            </p>
        </div>
        <div>
            <h2 style="margin-left: 23px">Ответ: </h2>
        </div>
    <div>
        <p style="margin-left: 23px"><b>Широта: </b><br />
        <output name="lat" id="lat" form="form1" runat="server"></output>
        </p>
        <p style="margin-left: 23px"><b>Долгота: </b><br />
        <output name="lon" id="lon" form="form1" runat="server"></output>
        </p>
    </div>
        <aside>
        <p style="margin-left: 23px">        
            Другие тренажёры:
        </p>
        <ul>
            <li><a runat="server" href="~/Calculation1.aspx">Тренажёр расчётов координат места (метод высотных линий положения)</a></li>
        </ul>
    </aside>
        <aside>
        <p style="margin-left: 23px">        
            Переход на страницу:
        </p>
        <ul>
            <li><a runat="server" href="~/About.aspx">О программе</a></li>
            <li><a runat="server" href="~/Help.aspx">Справка</a></li>
        </ul>
    </aside>
    </form>
</body>
</html>
