use Edaibd;

INSERT INTO dbo.Pages(id, id_les, name, fname) VALUES
					 (11, 6, '', 'Eddata/Lessons/Les6.pdf'),
					 --(12, 7, '', 'Eddata/Lessons/Les7.pdf');
					 (12, 7, '', 'Eddata/Lessons/Les7_1.pdf'),
					 --(13, 7, '', 'Eddata/Lessons/Les7_2.pdf'),
					 (14, 7, 'Меркурий', 'Eddata/Lessons/Les7_3.pdf'),
					 (15, 7, 'Венера', 'Eddata/Lessons/Les7_4.pdf'),
					 (16, 7, 'Марс', 'Eddata/Lessons/Les7_5.pdf'),
					 (17, 7, 'Юпитер', 'Eddata/Lessons/Les7_6.pdf'),
					 (18, 7, 'Сатурн', 'Eddata/Lessons/Les7_7.pdf'),
					 (19, 7, 'Уран', 'Eddata/Lessons/Les7_8.pdf'),
					 (20, 7, 'Нептун', 'Eddata/Lessons/Les7_9.pdf'),
					 (21, 7, 'Плутон', 'Eddata/Lessons/Les7_10.pdf'),
					 (22, 7, 'Опроворжение теории о планете Х. Открытие пояса Койпера. Пересмотр статуса Плутона', 'Eddata/Lessons/Les7_11.pdf'),
					 (23, 7, 'Пояс Койпера', 'Eddata/Lessons/Les7_12.pdf');

UPDATE dbo.Pages
  SET id_les=7 WHERE id=13;

UPDATE dbo.Pages
  SET name='Малые тела' WHERE id=13;

UPDATE dbo.Pages
  SET fname='Eddata/Lessons/Les7_2.pdf' WHERE id=13;

SELECT * FROM dbo.Pages;
