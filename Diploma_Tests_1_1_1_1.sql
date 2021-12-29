use Edaibd;

--INSERT INTO dbo.PromptCount VALUES (1, 1, 3);

--SELECT * FROM PromptCount;

/*INSERT INTO dbo.TestQuestions(id, id_bl, ques, im_file, var1, var2, var3, var4, r_ans, balls) VALUES
       (1, 1, '2', 1),
	   (2, 1, '3', 2),
	   (3, 1, '241.3', 2),
	   (4, 1, '2', 1),
	   (5, 1, '1', 2),
	   (6, 1, '4', 1),
	   (7, 1, '2', 2),
	   (8, 1, '3', 2),
	   (9, 1, '3', 1);*/

INSERT INTO dbo.ExamResults (id, id_us, id_bl, real_balls, max_res, procnt, mark, dt, trynum) VALUES
       (1, 1, 1, 8, 14, 57, 3, '20180401 15:19:47.244', 1);

INSERT INTO dbo.Answers (id, id_us, id_bl, id_ex, us_balls, us_ans, tr_ans) VALUES
       (1, 1, 1, 1, 0, '1', '2'),
	   (2, 1, 1, 1, 0, '2', '3'),
	   (3, 1, 1, 1, 0, '243.1', '241.3'),
	   (4, 1, 1, 1, 1, '2', '2'),
	   (5, 1, 1, 1, 2, '1', '1'),
	   (6, 1, 1, 1, 0, '1', '4'),
	   (7, 1, 1, 1, 2, '2', '2'),
	   (8, 1, 1, 1, 2, '3', '3'),
	   (9, 1, 1, 1, 1, '3', '3');