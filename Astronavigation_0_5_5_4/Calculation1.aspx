<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calculation1.aspx.cs" Inherits="Calculation1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Тренажёр № 2 расчётов координат места (с инструкцией)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1 style="margin-left: 20px">Метод высотных линий положения: </h1>
    </div>
    <div>
        <p style="text-indent: 20px; margin-left: 20px">
            Первый способ расчёта и прокладки высотных линий положения (называемый долготным) предложил американский капитан Томас Сомнер.
            Способ заключался в нахождении пересечения хорд кругов равных высот. Для построения хорд по измеренной высоте и координат светил
            задаётся начальное приближение широты φ. Затем по значению φ вычисляется часовой угол, а из часового угла - соответствующая долгота
            при вычитании гринвического часового угла. 
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            Второй способ был предложен штурманом черноморского флота М. А. Акимовым в 1849 году.
            Он отличается методом проведения хорды (перпендикулярно к линии азимута вместо отложения 10' по широте).
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            Третий способ, более универсальный и применяемый до сих пор, носит имя французского моряка Сент-Илера, предложившего его в 1875 году.
            Алгоритм следующий: через точку приблизительного значения проводят линию вдоль вертикала светила на разницу радиусов кругов приближённого и
            точного значений, а затем через полученную точку проводят перпендикуляр (касательную), и это будет высотная линия положения. 
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            Советский учёный В. В. Каврайский (1884 - 1954) распространил идею графоаналитической модификации метода, известного также как метод градиентов.
            Аналитический метод определения, по уравнениям высотных линий, был реализован на ЭВМ.
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            Для реализации алгоритма нужно знать примерное положение (счислимое место), применяемое как начальное приближение. 
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            Задача сводится к решению системы из двух уравнений высотных линий, получаемых из разложения в ряд Тейлора и отбрасывания членов второго
            и более порядков.
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>sin h<sub>1</sub> = sin φ sin δ<sub>1</sub> + cos φ cos δ<sub>1</sub> cos (t<sub>G1</sub> ± λ) </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>sin h<sub>2</sub> = sin φ sin δ<sub>2</sub> + cos φ cos δ<sub>2</sub> cos (t<sub>G2</sub> ± λ) </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            где <i>h<sub>1</sub></i>, <i>h<sub>2</sub></i> – высоты светил 1 и 2, <i>δ<sub>1</sub></i>, <i>δ<sub>2</sub></i>, <i>t<sub>G1</sub></i>, <i>t<sub>G2</sub></i> – их склонения и гринвические часовые углы, <i>φ</i> и <i>λ</i> – ширина и долгота 
            наблюдателя. </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>h<sub>0</sub> = arcsin [sin φ sin δ<sub>1</sub> + cos φ cos δ<sub>1</sub> cos (t<sub>G1</sub> ± λ)] </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>h<sub>0</sub> = arcsin [sin φ sin δ<sub>2</sub> + cos φ cos δ<sub>2</sub> cos (t<sub>G2</sub> ± λ)] </i></p>
        <p style="text-indent: 20px; margin-left: 20px"> </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>h<sub>0</sub> (φ<sub>0</sub>, λ<sub>0</sub>) = h (φ<sub>с1</sub>, λ<sub>с1</sub>) + (&part;h / &part;φ)<sub>с1</sub> &Delta;φ + (&part;h / &part;λ)<sub>с1</sub> &Delta;λ + ... </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>h<sub>0</sub> (φ<sub>0</sub>, λ<sub>0</sub>) = h (φ<sub>с2</sub>, λ<sub>с2</sub>) + (&part;h / &part;φ)<sub>с2</sub> &Delta;φ + (&part;h / &part;λ)<sub>с2</sub> &Delta;λ + ... </i></p>
        <p style="text-indent: 20px; margin-left: 20px"> </p>       
        <p style="text-indent: 20px; margin-left: 20px">
            Подставив значения производных <i>&part;h / &part;φ = cos A<sub>с</sub> и &part;h / &part;λ = – cos φ sin A<sub>с</sub></i>:
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>&Delta;h<sub>1</sub> = cos A<sub>с1</sub> &Delta;φ + cos φ sin A<sub>с1</sub> &Delta;λ </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>&Delta;h<sub>2</sub> = cos A<sub>с2</sub> &Delta;φ + cos φ sin A<sub>с2</sub> &Delta;λ </i></p>
        <p style="text-indent: 20px; margin-left: 20px"> </p>
        <p style="text-indent: 20px; margin-left: 20px">
            Решив их относительно <i>&Delta;φ</i> и <i>&Delta;λ</i> (умножив первое выражение на <i>sin A<sub>с2,1</sub></i>, второе на <i>cos A<sub>с2,1</sub></i>), получим:
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>&Delta;φ = (&Delta;h<sub>1</sub> sin A<sub>с2</sub> – &Delta;h<sub>2</sub> sin A<sub>с1</sub>) / sin (A<sub>с2</sub> – A<sub>с1</sub>) </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>&Delta;λ = (&Delta;h<sub>2</sub> cos A<sub>с1</sub> – &Delta;h<sub>1</sub> cos A<sub>с2</sub>) / (cos φ sin (A<sub>с2</sub> – A<sub>с1</sub>)) </i></p>
        <p style="text-indent: 20px; margin-left: 20px"> </p>
        <p style="text-indent: 20px; margin-left: 20px">
            Искомые координаты получаются по приближению и приращениям:
        </p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>φ<sub>0</sub> = φ<sub>с</sub> + &Delta;φ </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            <i>λ<sub>0</sub> = λ<sub>с</sub> + &Delta;λ </i></p>
        <p style="text-indent: 20px; margin-left: 20px">
            Далее, если нужно, делается второе приближение.
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
            <p style="margin-left: 23px"><b>Центр первого круга равных высот: </b><br />
                <output name="centr1" id="centr1" form="form1" runat="server"></output>
            </p>
            <p style="margin-left: 23px"><b>Радиус: </b><br />
                <output name="rad1" id="rad1" form="form1" runat="server"></output>
            </p>
            <p style="margin-left: 23px"><b>Центр второго круга равных высот: </b><br />
                <output name="centr2" id="centr2" form="form1" runat="server"></output>
            </p>
            <p style="margin-left: 23px"><b>Радиус: </b><br />
                <output name="rad2" id="rad2" form="form1" runat="server"></output>
            </p>
            <h2 style="margin-left: 20px">Начальное приближение: </h2>
            
            <table style="margin-left: 20px">
                <tr>
                    <td> <b>Широта: </b> </td><td style="width: 100px"> </td><td> <b>Долгота: </b> </td><td style="width: 100px"> </td><td> <b>Высота: </b> </td>
                </tr>
                <tr>
                    <td> <input type="number" min="0" step="any" max="90" name="lat_s" id="lat_s" form="form1" runat="server"/> </td>
                    <td style="width: 100px"> </td>
                    <td> <input type="number" min="-180" max="180" step="any" name="lon_s" id="lon_s" form="form1" runat="server"/> </td>
                    <td style="width: 100px"> </td>
                    <td> <input type="number" min="0" step="any" max="90" name="h_s" id="h_s" form="form1" runat="server"/> </td>
                </tr>
            </table>
            <p style="margin-left: 20px"> </p>
            <table style="margin-left: 20px">
                <tr>
                    <td> <b>Азимут первого светила: </b> </td><td style="width: 150px"> </td><td> <b>Азимут второго светила: </b> </td>
                </tr>
                <tr>
                    <td> <input type="number" min="0" step="any" max="360" name="as_1" id="as_1" form="form1" runat="server" required="required"/> </td>
                    <td style="width: 150px"> </td>
                    <td> <input type="number" min="0" max="360" step="any" name="as_2" id="as_2" form="form1" runat="server" required="required"/> </td>
                </tr>
            </table>
            
        </div>
        <div>
            <p style="margin-left: 23px">
                <asp:Button id="button2" text="Ввод" runat="server" OnClick="button2_Click"/>
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
            <li><a runat="server" href="~/Calculation.aspx">Тренажёр расчётов координат места (аналитическое решение Гаусса)</a></li>
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
