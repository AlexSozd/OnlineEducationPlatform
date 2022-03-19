﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Help" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Астронавигация 1.0 - Справка</title>
</head>
<body>
    <form id="form1" runat="server">
    
    </form>
    <article style="text-indent: 20px; margin-left: 20px">
        <h1 style="margin-top: -3px">
            Справка
        </h1>
        <h3 style="margin-top: -3px">
            Навигация по страницам
        </h3>
        <p style="margin-top: -12px">
            При входе в программу вы попадаете на главную страницу, на которой в оглавлении в виде выпадающего списка содержатся гиперссылки на странице.
            Чтобы найти страницу с искомой информацией, просто выберете ссылку по названию. Для перехода между уроками и по страницам урока
            нажмите расположенные на странице сверху кнопки "Назад" и "Вперёд".
        </p>
        <h3 style="margin-top: -3px">
            Получение статьи из справочника
        </h3>
        <p style="margin-top: -12px">
            Нажмите на соответствующую гиперссылку на странице пособия. Тогда появится новая вкладка с определением термина.
        </p>
        <h3 style="margin-top: -3px">
            Тренажёры-симуляторы
        </h3>
        <p style="margin-top: -12px">
            Гипертекстовые ссылки для перехода на данные страницы расположены отдельным списком в нижней части главной страницы.
        </p>
        <p style="margin-top: -12px">
            Выбор светил, подходящих для проведения наблюдения и расчёта места, в программе ограничен Солнцем и тремя десятками ярчайших звёзд небосвода.
        </p>
        <p style="margin-top: -12px">
            Для получения ответа просто введите в поля соответствующие числа (проверьте правильность перед запуском!) и 
            нажмите кнопку "Ввод" в нижней части страницы, под заполняемой формой.
            Для корректной работы симулятора при вводе дробную часть следует отделять запятой (не точкой!).
        </p>
        <h3 style="margin-top: -3px">
            Прохождение тестов и просмотр результатов
        </h3>
        <p style="margin-top: -12px">
            Ссылки на проверочные работы находятся на последней странице соответствующего теме теста раздела. Для прохождения теста и просмотра результатов
            необходимо авторизоваться.
        </p>
        <p style="margin-top: -12px">
            Чтобы ответить на задание с вариантами ответа достаточно навести мышь на соответствующий (лежащий слева) переключатель или флажок
            и нажать левую кнопку мыши. Для отправки заполненной формы нужно нажать на кнопку "Отправить";
            прямо на странице будет выведен результат с указанием правильных и ошибочных ответов, полученных баллов и соответствующей им оценки.
        </p>
        <p style="margin-top: -12px">
            Ссылка для перехода на личную страницу просмотра результатов прохождения тестов находится у нижнего края страницы, под кнопкой отправки
            тестовой формы. Для запуска поиска записей необходимо задать категории отбора (изменив значение поля вариантом, выбранным из выпадающего списка)
            - тестовый блок, попытку (все, лучшая, последняя по времени), конкретный вопрос теста или время сдачи (определяемое как интервал между двумя введёнными датами).
            Страница для преподавателей отличается от студенческой большим выбором критериев сортировки и запуском поиска записей нажатием кнопки ввода времени
            прохождения.
        </p>
    </article>

    <aside style="margin-left: 20px; margin-top: 30px">
        <p style="text-indent: 20px">        
            Переход на страницу:
        </p>
        <ul>
            <li><a runat="server" href="~/">Главная</a></li>
            <li><a runat="server" href="~/About.aspx">О программе</a></li>
        </ul>
    </aside>
</body>
</html>
