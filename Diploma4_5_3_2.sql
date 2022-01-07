use Edaibd;

/*IF OBJECT_ID('dbo.TestBlocks', 'U') IS NOT NULL
   DROP TABLE dbo.TestBlocks;
CREATE TABLE dbo.TestBlocks
(
   id INT NOT NULL PRIMARY KEY,
   id_page INT NOT NULL FOREIGN KEY REFERENCES dbo.Pages(id),
   name NVARCHAR(255) NOT NULL
);*/
INSERT INTO dbo.Pages(id, id_les, name, fname) VALUES
                     (32, 32, '', 'Astronavigation/App_Data/Lessons/Les32.pdf');
INSERT INTO dbo.TestBlocks(id, id_page, name) VALUES
                      (1, 32, '������ ����� � ����������� ����������')/*,
					  (2, 46, '��������������� � ������������� ������� � �� ����������'),
					  (3, 68, '������ ����������� ��������������� ����������� �� �������� ��������')*/;
/*IF OBJECT_ID('dbo.TestQuestions', 'U') IS NOT NULL
   DROP TABLE dbo.TestQuestions;
CREATE TABLE dbo.TestQuestions
(
   id INT NOT NULL PRIMARY KEY,
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   ques NVARCHAR(255) NOT NULL,
   im_file NVARCHAR(255) NULL,
   var1 NVARCHAR(255) NULL,
   var2 NVARCHAR(255) NULL,
   var3 NVARCHAR(255) NULL,
   var4 NVARCHAR(255) NULL,
   r_ans NVARCHAR(255) NOT NULL,
   balls INT NOT NULL
);*/
INSERT INTO dbo.TestQuestions(id, id_bl, ques, im_file, var1, var2, var3, var4, r_ans, balls) VALUES
       (1, 1, '������� 98 �������� 52,5 ������ � ������� ����: ', NULL, '6 � 48 ��� 35 �', '6 � 35 ��� 30 �', '6 � 33 ��� 35 �', '6 � 35 ��� 27,5 �', '2', 1),
	   (2, 1, '����: ������ 55 �������� 45,6 ����� N, ��������� ������� 10 �������� 13,4 ������ S, ������� ���� - 62 ������� 24,5 ������ W. ������� ������ �������.', NULL, '6 �������� 24,2 ������', '6 �������� 18,1 ������', '23 ������� 1 ������', '16 �������� 7,3 ������', '3', 2),
	   (3, 1, '�� ������ �� ������� 2 ������� ������ ������� � �������� �����, �������� �� ������� ����� ������.', NULL, NULL, NULL, NULL, NULL, '241.3', 2),
	   (4, 1, '��������� �������������� ������ 37 �������� 28,4 ������ S � ��������� ������� 12 �������� 13,8 ������ S, ���������� ������ �����.', NULL, '25 �������� 14,6 ������ S', '40 �������� 17,8 ������ N', '64 ������� 45,4 ������ S', '25 �������� 14,6 ������ N', '2', 1),
	   (5, 1, '��� ����� � ������� 14 �������� S ���������� ���� ����������� ������ ����� �����. ��� ����������� ������� 0,4 ������� � ����.', NULL, '30/X � 12/II', '16/VIII � 27/IV', '30/IX � 14/III', NULL, '1', 2),
	   (6, 1, '�� �������� ����� ����������� ���� 5 ��� 6 � 54 ���. ����� ����� ����������� ��� ����� � �������� 140 W.', NULL, '7 � 10 ���', '7 � 30 ���', '7 � 27 ���', '7 � 13 ���', '4', 1),
	   (7, 1, '�� ��������� 131 �������� 27,5 ������ Ost ������� ���� ������� 6 � 18 ��� 20 �. ���������� ������� ���� �� ��������� 61 ������ 43 ������ Ost.', NULL, '2 � 00 ��� 37 �', '1 � 39 ��� 22 �', '1 � 27 ��� 53 �', '1 � 43 ��� 48 �', '2', 2),
	   (8, 1, '������� ����� 5 � 38 ��� � ����� � �������� 82 ������� 10 ����� W; ���������� ������� �����: ', NULL, '5 � 38 ���', '5 � 6 ��� 40 �', '6 � 7 ���', NULL, '3', 2),
	   (9, 1, '����� ���������� 10 � 51 ��� 35 �, �������� ���������� -1 ��� 12 �, ����� ������� ����� 9 � 50 ��� 3/X (11 Ost). ���������� ������ ����������� ����� � ����.', NULL, '22 � 50 ��� 23 � 1/X', '10 � 50 ��� 23 � 3/X', '22 � 50 ��� 23 � 2/X', NULL, '3', 1);

/*IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
   DROP TABLE dbo.Users;
CREATE TABLE dbo.Users
(
   id INT NOT NULL PRIMARY KEY,
   name NVARCHAR(255) NOT NULL,
   cell_tel NVARCHAR(255) NULL,
   e_mail NVARCHAR(255) NULL,
   parole NVARCHAR(255) NOT NULL
);
INSERT INTO dbo.Users(id, name, cell_tel, e_mail, parole) VALUES
       (1, 'Tester1', '+7(917)777-33-55', 'tester@mail.ru', 'RQSP4511');

IF OBJECT_ID('dbo.ExamResults', 'U') IS NOT NULL
   DROP TABLE dbo.ExamResults;
CREATE TABLE dbo.ExamResults
(
   id INT NOT NULL PRIMARY KEY,
   id_us INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(id),
   id_bl INT NOT NULL FOREIGN KEY REFERENCES dbo.TestBlocks(id),
   mark INT NOT NULL,
   real_balls REAL NOT NULL,
   max_res REAL NOT NULL,
   dt DATETIME NOT NULL
);*/