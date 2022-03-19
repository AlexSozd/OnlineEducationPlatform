using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestForm : System.Web.UI.Page
{
    int pagenum, bl_num, us_id, id_ex, try_num;
    bool req = true;
    
    List<int> b = new List<int>();
    List<string> ans = new List<string>();
    SqlConnection sc = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (req == true)
            {
                pagenum = int.Parse(Request.QueryString["number"]);
                if(Request.QueryString.Count > 1)
                {
                    req = false;
                    us_id = int.Parse(Request.QueryString["UserId"]);
                    button1.PostBackUrl = "LessonPage.aspx?number=" + pagenum.ToString();
                    button1.Enabled = true;
                    if (pagenum < PageCount.GetCount())
                    {
                        button2.PostBackUrl = "LessonPage.aspx?number=" + (pagenum + 1).ToString();
                        button2.Enabled = true;
                    }
                    else
                    {
                        button2.Enabled = false;
                    }
                    //hyperlink1.HRef = "~/PersonalData.aspx?from=" + Page.Request.Url.Segments[1];
                    //hyperlink1.HRef = "~/PersonalData.aspx?from=" + Page.PreviousPage.Request.Url.Segments[1] + "&UserId=" + us_id.ToString();
                    hyperlink1.HRef = "~/PersonalData.aspx?from=LessonPage.aspx?number=" + pagenum.ToString() + "&UserId=" + us_id.ToString();
                    PromptCount(us_id);
                    if(try_num <= 3)
                    {
                        FillForm("GetTestBlock", pagenum);
                    }
                    else
                    {
                        //Access denied + watch previous tests' results
                        Response.Redirect("PersonalData.aspx?from=LessonPage.aspx?number=" + pagenum.ToString() + "&UserId=" + us_id);
                        //hyperlink1.HRef = "~/PersonalData.aspx?from=" + Page.PreviousPage.Request.Url.Segments[1] + "&UserId=" + us_id.ToString();
                        /*if(Page.PreviousPage.Request.QueryString["UserId"] != null)
                        {
                            Response.Redirect("PersonalData.aspx?from=" + Page.PreviousPage.Request.Url.Segments[1]);
                        }
                        else
                        {
                            Response.Redirect("PersonalData.aspx?from=" + Page.PreviousPage.Request.Url.Segments[1] + "&UserId=" + us_id);
                        }*/
                    }
                }
                else
                {
                    Response.Redirect("Authorization.aspx?from=TestForm.aspx?number=" + pagenum.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Source + ": " + ex.Message);
            Response.Write(ex.Message);
        }
    }
    protected void PromptCount(int us_id)
    {
        //Количество попыток - создать процедуру
        sc.Open();
        using (SqlCommand command = new SqlCommand("GetTryNum", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_bl";
            param.Value = bl_num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@id_us";
            param.Value = us_id;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                try_num = int.Parse(sdr[0].ToString());
            }
            sdr.Close();
        }
        sc.Close();
    }
    protected void FillForm(string proc, int pagenum)
    {
        //int bl_num = 1, 
        int ans_t = 0;
        
        Table1.CellSpacing = 0;
        //Table1.BorderWidth = 2;
        //Table1.GridLines = GridLines.Both;
        Table1.GridLines = GridLines.None;

        sc.Open();
        //Заполнить таблицу вопросами теста из базы данных
        /*using (SqlCommand command = new SqlCommand("GetPage", sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_page";
            param.Value = pagenum;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                label2.Text = ShowLessonName("GetPageLesson").ToString();
                //label2.Text = sdr["name"].ToString();
            }
            else
            {
                Response.Write("Такой страницы у нас нет. Просим извинения за неудобства!");
            }
            sdr.Close();
        }*/
        using (SqlCommand command = new SqlCommand("FindTestBlock", sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_page";
            param.Value = pagenum;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                bl_num = int.Parse(sdr["id"].ToString());
                label2.Text = sdr["name"].ToString();
            }
            else
            {
                Response.Write("Такой страницы у нас нет. Просим извинения за неудобства!");
            }
            sdr.Close();
        }
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_bl";
            param.Value = bl_num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            int i = 0;
            while (sdr.Read() == true)
            {
                TableRow tr = new TableRow();

                TableCell c1 = new TableCell();
                c1.Text = sdr["id"].ToString() + ". " + sdr["ques"].ToString();
                tr.Cells.Add(c1);

                Table1.Rows.Add(tr);
                
                tr = new TableRow();

                c1 = new TableCell(); 
                c1.Text = sdr["im_file"].ToString();
                if((c1.Text != "") && (c1.Text != null))
                {
                    Image im1 = new Image();
                    im1.ImageUrl = sdr["im_file"].ToString();
                    c1.Controls.Add(im1);
                }
                tr.Cells.Add(c1);

                Table1.Rows.Add(tr);

                if(AnswerFormat(sdr["r_ans"].ToString()) == "Only one right answer")
                {
                    ans_t = 1;
                }
                if (AnswerFormat(sdr["r_ans"].ToString()) == "Several right answers")
                {
                    ans_t = 2;
                }
                if (AnswerFormat(sdr["r_ans"].ToString()) == "Without variants")
                {
                    ans_t = 3;
                }

                tr = new TableRow();

                c1 = new TableCell();
                c1.Text = sdr["var1"].ToString();
                tr.Cells.Add(c1);

                if((sdr["var1"].ToString() != "") && (sdr["var1"].ToString() != null))
                {
                    if(ans_t == 1)
                    {
                        RadioButton rb = new RadioButton();
                        rb.Text = sdr["var1"].ToString();
                        rb.GroupName = "RadioGroup" + (i + 1);
                        c1.Controls.Add(rb);
                    }
                    if (ans_t == 2)
                    {
                        CheckBox cb = new CheckBox();
                        cb.Text = sdr["var1"].ToString();
                        c1.Controls.Add(cb);
                    }

                    c1 = new TableCell();
                    c1.Style["width"] = "20px";
                    tr.Cells.Add(c1);

                    c1 = new TableCell();
                    c1.Text = sdr["var2"].ToString();
                    if (ans_t == 1)
                    {
                        RadioButton rb = new RadioButton();
                        rb.Text = sdr["var2"].ToString();
                        rb.GroupName = "RadioGroup" + (i + 1);
                        c1.Controls.Add(rb);
                    }
                    if (ans_t == 2)
                    {
                        CheckBox cb = new CheckBox();
                        cb.Text = sdr["var2"].ToString();
                        c1.Controls.Add(cb);
                    }
                    tr.Cells.Add(c1);
                }
                Table1.Rows.Add(tr);
                tr = new TableRow();

                c1 = new TableCell();
                c1.Text = sdr["var3"].ToString();
                tr.Cells.Add(c1);
                
                if ((sdr["var3"].ToString() != "") && (sdr["var3"].ToString() != null))
                {
                    if (ans_t == 1)
                    {
                        RadioButton rb = new RadioButton();
                        rb.Text = sdr["var3"].ToString();
                        rb.GroupName = "RadioGroup" + (i + 1);
                        c1.Controls.Add(rb);
                    }
                    if (ans_t == 2)
                    {
                        CheckBox cb = new CheckBox();
                        cb.Text = sdr["var3"].ToString();
                        c1.Controls.Add(cb);
                    }

                    if ((sdr["var4"].ToString() != "") && (sdr["var4"].ToString() != null))
                    {
                        c1 = new TableCell();
                        c1.Style["width"] = "20px";
                        tr.Cells.Add(c1);

                        c1 = new TableCell();
                        c1.Text = sdr["var4"].ToString();
                        if (ans_t == 1)
                        {
                            RadioButton rb = new RadioButton();
                            rb.Text = sdr["var4"].ToString();
                            rb.GroupName = "RadioGroup" + (i + 1);
                            c1.Controls.Add(rb);
                        }
                        if (ans_t == 2)
                        {
                            CheckBox cb = new CheckBox();
                            cb.Text = sdr["var4"].ToString();
                            c1.Controls.Add(cb);
                        }
                        tr.Cells.Add(c1);
                    }
                }
                Table1.Rows.Add(tr);

                tr = new TableRow();
                c1 = new TableCell();
                tr.Cells.Add(c1);
                if(ans_t == 3)
                {
                    c1.Controls.Add(new TextBox());
                    c1 = new TableCell();
                    c1.Style["width"] = "20px";
                    tr.Cells.Add(c1);
                    c1 = new TableCell();
                    tr.Cells.Add(c1);
                }
                c1 = new TableCell();
                c1.Style["width"] = "20px";
                tr.Cells.Add(c1);
                c1 = new TableCell();
                c1.Text = "Баллов: ";
                tr.Cells.Add(c1);
                
                Table1.Rows.Add(tr);
                ans.Add(sdr["r_ans"].ToString());
                b.Add(int.Parse(sdr["balls"].ToString()));

                i++;
            }
        }
        sc.Close();

        TableRow tr1 = new TableRow();
        TableCell cl = new TableCell();
        cl.Text = "Оценка: ";
        tr1.Cells.Add(cl);

        Table1.Rows.Add(tr1);
    }
    protected void button1_Click(object sender, EventArgs e)
    {
        req = true;
        Response.Redirect(button1.PostBackUrl);
    }
    protected void button2_Click(object sender, EventArgs e)
    {
        req = true;
        Response.Redirect(button2.PostBackUrl);
    }
    protected void button3_Click(object sender, EventArgs e)
    {
        double b_count = 0.00, sum_ball = 0.00;
        button1.Enabled = false;
        button2.Enabled = true;
        button3.Enabled = false;
        DateTime t_dt = DateTime.Now;
        GetExamResultNumber();
        //Check answers
        List<string> us_ans = new List<string>();
        List<double> us_b = new List<double>();

        for(int i = 0; i < ans.Count; i++)
        {
            sum_ball += b[i];
            if(AnswerFormat(ans[i]) == "Without variants")
            {
                if((Table1.Rows[5 * i + 4].Cells[0].Controls[0] as TextBox).Text == ans[i])
                {
                    Table1.Rows[5 * i + 4].Cells[2].Text = "Правильно.";
                    Table1.Rows[5 * i + 4].Cells[4].Text += b[i] + "/" + b[i];
                    b_count += b[i];
                    us_ans.Add(ans[i]);
                    us_b.Add(b[i]);
                }
                else
                {
                    Table1.Rows[5 * i + 4].Cells[2].Text = "Неправильно. Правильный ответ: " + ans[i];
                    Table1.Rows[5 * i + 4].Cells[4].Text += 0 + "/" + b[i];
                    us_ans.Add((Table1.Rows[5 * i + 4].Cells[0].Controls[0] as TextBox).Text);
                    us_b.Add(0.000);
                }
                (Table1.Rows[5 * i + 4].Cells[0].Controls[0] as TextBox).Enabled = false;
            }
            if (AnswerFormat(ans[i]) == "Only one right answer")
            {
                if(ans[i] == "1")
                {
                    if((Table1.Rows[5 * i + 2].Cells[0].Controls[0] as RadioButton).Checked == true)
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Правильно.";
                        Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                        b_count += b[i];
                        us_ans.Add(ans[i]);
                        us_b.Add(b[i]);
                    }
                    else
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Неправильно. Правильный ответ: " + ans[i] + "-й";
                        Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                        us_ans.Add(GetUserAnswer(i));
                        us_b.Add(0.000);
                    }
                }
                else if(ans[i] == "2")
                {
                    if ((Table1.Rows[5 * i + 2].Cells[2].Controls[0] as RadioButton).Checked == true)
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Правильно.";
                        Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                        b_count += b[i];
                        us_ans.Add(ans[i]);
                        us_b.Add(b[i]);
                    }
                    else
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Неправильно. Правильный ответ: " + ans[i] + "-й";
                        Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                        us_ans.Add(GetUserAnswer(i));
                        us_b.Add(0.000);
                    }
                }
                else if(ans[i] == "3")
                {
                    if ((Table1.Rows[5 * i + 3].Cells[0].Controls[0] as RadioButton).Checked == true)
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Правильно.";
                        Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                        b_count += b[i];
                        us_ans.Add(ans[i]);
                        us_b.Add(b[i]);
                    }
                    else
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Неправильно. Правильный ответ: " + ans[i] + "-й";
                        Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                        us_ans.Add(GetUserAnswer(i));
                        us_b.Add(0.000);
                    }
                }
                else
                {
                    if ((Table1.Rows[5 * i + 3].Cells[2].Controls[0] as RadioButton).Checked == true)
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Правильно.";
                        Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                        b_count += b[i];
                        us_ans.Add(ans[i]);
                        us_b.Add(b[i]);
                    }
                    else
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Неправильно. Правильный ответ: " + ans[i] + "-й";
                        Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                        us_ans.Add(GetUserAnswer(i));
                        us_b.Add(0.000);
                    }
                }
                (Table1.Rows[5 * i + 2].Cells[0].Controls[0] as RadioButton).Enabled = false;
                (Table1.Rows[5 * i + 2].Cells[2].Controls[0] as RadioButton).Enabled = false;
                if(Table1.Rows[5 * i + 3].Cells[0].Controls.Count != 0)
                {
                    (Table1.Rows[5 * i + 3].Cells[0].Controls[0] as RadioButton).Enabled = false;
                }
                if(Table1.Rows[5 * i + 3].Cells.Count > 1)
                {
                    (Table1.Rows[5 * i + 3].Cells[2].Controls[0] as RadioButton).Enabled = false;
                }
            }
            if(AnswerFormat(ans[i]) == "Several right answers")
            {
                int k = 0;
                string[] m_ans = ans[i].Split(' ');
                for(int j = 0; j < m_ans.Length; j++)
                {
                    if(IsRight(m_ans[j], i) == true)
                    {
                        k++;
                    }
                }
                if(k == 0)
                {
                    Table1.Rows[5 * i + 4].Cells[0].Text = "Всё неправильно. Правильный ответ: " + ans[i] + "-й";
                    Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                    us_ans.Add(GetUserAnswer(i));
                    us_b.Add(0.000);
                }
                else if(k < m_ans.Length)
                {
                    Table1.Rows[5 * i + 4].Cells[0].Text = "Частично правильно. Правильный ответ: " + ans[i] + "-й";
                    double buf = (double)b[i];
                    buf = k * buf;
                    buf = buf / m_ans.Length;
                    //b_count += (k * b[i]) / m_ans.Length;
                    b_count += buf;
                    Table1.Rows[5 * i + 4].Cells[2].Text += buf + "/" + b[i];
                    us_ans.Add(GetUserAnswer(i));
                    us_b.Add(buf);
                }
                else
                {
                    Table1.Rows[5 * i + 4].Cells[0].Text = "Всё правильно.";
                    Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                    b_count += b[i];
                    us_ans.Add(ans[i]);
                    us_b.Add(b[i]);
                }
                (Table1.Rows[5 * i + 2].Cells[0].Controls[0] as CheckBox).Enabled = false;
                (Table1.Rows[5 * i + 2].Cells[2].Controls[0] as CheckBox).Enabled = false;
                if(Table1.Rows[5 * i + 3].Cells[0].Controls.Count != 0)
                {
                    (Table1.Rows[5 * i + 3].Cells[0].Controls[0] as CheckBox).Enabled = false;
                }
                if(Table1.Rows[5 * i + 3].Cells.Count > 1)
                {
                    (Table1.Rows[5 * i + 3].Cells[2].Controls[0] as CheckBox).Enabled = false;
                }
            }
        }
        /*for(int i = 0; i < ans.Count; i++)
        {
            sum_ball += b[i];
            if(AnswerFormat(ans[i]) == "Without variants")
            {
                if((Table1.Rows[5 * i + 4].Cells[0].Controls[0] as TextBox).Text == ans[i])
                {
                    Table1.Rows[5 * i + 4].Cells[2].Text = "Правильно.";
                    Table1.Rows[5 * i + 4].Cells[4].Text += b[i] + "/" + b[i];
                    b_count += b[i];
                    SaveUserBalls(b[i], (Table1.Rows[5 * i + 4].Cells[0].Controls[0] as TextBox).Text, ans[i]);
                }
                else
                {
                    Table1.Rows[5 * i + 4].Cells[2].Text = "Неправильно. Правильный ответ: " + ans[i];
                    Table1.Rows[5 * i + 4].Cells[4].Text += 0 + "/" + b[i];
                    SaveUserBalls(0.000, (Table1.Rows[5 * i + 4].Cells[0].Controls[0] as TextBox).Text, ans[i]);
                }
                (Table1.Rows[5 * i + 4].Cells[0].Controls[0] as TextBox).Enabled = false;
            }
            if (AnswerFormat(ans[i]) == "Only one right answer")
            {
                if(ans[i] == "1")
                {
                    if((Table1.Rows[5 * i + 2].Cells[0].Controls[0] as RadioButton).Checked == true)
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Правильно.";
                        Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                        b_count += b[i];
                        SaveUserBalls(b[i], ans[i], ans[i]);
                    }
                    else
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Неправильно. Правильный ответ: " + ans[i] + "-й";
                        Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                        SaveUserBalls(0.000, GetUserAnswer(i), ans[i]);
                    }
                }
                else if(ans[i] == "2")
                {
                    if ((Table1.Rows[5 * i + 2].Cells[2].Controls[0] as RadioButton).Checked == true)
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Правильно.";
                        Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                        b_count += b[i];
                        SaveUserBalls(b[i], ans[i], ans[i]);
                    }
                    else
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Неправильно. Правильный ответ: " + ans[i] + "-й";
                        Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                        SaveUserBalls(0.000, GetUserAnswer(i), ans[i]);
                    }
                }
                else if(ans[i] == "3")
                {
                    if ((Table1.Rows[5 * i + 3].Cells[0].Controls[0] as RadioButton).Checked == true)
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Правильно.";
                        Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                        b_count += b[i];
                        SaveUserBalls(b[i], ans[i], ans[i]);
                    }
                    else
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Неправильно. Правильный ответ: " + ans[i] + "-й";
                        Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                        SaveUserBalls(0.000, GetUserAnswer(i), ans[i]);
                    }
                }
                else
                {
                    if ((Table1.Rows[5 * i + 3].Cells[2].Controls[0] as RadioButton).Checked == true)
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Правильно.";
                        Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                        b_count += b[i];
                        SaveUserBalls(b[i], ans[i], ans[i]);
                    }
                    else
                    {
                        Table1.Rows[5 * i + 4].Cells[0].Text = "Неправильно. Правильный ответ: " + ans[i] + "-й";
                        Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                        SaveUserBalls(0.000, GetUserAnswer(i), ans[i]);
                    }
                }
                (Table1.Rows[5 * i + 2].Cells[0].Controls[0] as RadioButton).Enabled = false;
                (Table1.Rows[5 * i + 2].Cells[2].Controls[0] as RadioButton).Enabled = false;
                if(Table1.Rows[5 * i + 3].Cells[0].Controls.Count != 0)
                {
                    (Table1.Rows[5 * i + 3].Cells[0].Controls[0] as RadioButton).Enabled = false;
                }
                if(Table1.Rows[5 * i + 3].Cells.Count > 1)
                {
                    (Table1.Rows[5 * i + 3].Cells[2].Controls[0] as RadioButton).Enabled = false;
                }
            }
            if(AnswerFormat(ans[i]) == "Several right answers")
            {
                int k = 0;
                string[] m_ans = ans[i].Split(' ');
                for(int j = 0; j < m_ans.Length; j++)
                {
                    if(IsRight(m_ans[j], i) == true)
                    {
                        k++;
                    }
                }
                if(k == 0)
                {
                    Table1.Rows[5 * i + 4].Cells[0].Text = "Всё неправильно. Правильный ответ: " + ans[i] + "-й";
                    Table1.Rows[5 * i + 4].Cells[2].Text += 0 + "/" + b[i];
                    SaveUserBalls(0.000, GetUserAnswer(i), ans[i]);
                }
                else if(k < m_ans.Length)
                {
                    Table1.Rows[5 * i + 4].Cells[0].Text = "Частично правильно. Правильный ответ: " + ans[i] + "-й";
                    double buf = (double)b[i];
                    buf = k * buf;
                    buf = buf / m_ans.Length;
                    //b_count += (k * b[i]) / m_ans.Length;
                    b_count += buf;
                    Table1.Rows[5 * i + 4].Cells[2].Text += buf + "/" + b[i];
                    SaveUserBalls(buf, GetUserAnswer(i), ans[i]);
                }
                else
                {
                    Table1.Rows[5 * i + 4].Cells[0].Text = "Всё правильно.";
                    Table1.Rows[5 * i + 4].Cells[2].Text += b[i] + "/" + b[i];
                    b_count += b[i];
                    SaveUserBalls(b[i], ans[i], ans[i]);
                }
                (Table1.Rows[5 * i + 2].Cells[0].Controls[0] as CheckBox).Enabled = false;
                (Table1.Rows[5 * i + 2].Cells[2].Controls[0] as CheckBox).Enabled = false;
                if(Table1.Rows[5 * i + 3].Cells[0].Controls.Count != 0)
                {
                    (Table1.Rows[5 * i + 3].Cells[0].Controls[0] as CheckBox).Enabled = false;
                }
                if(Table1.Rows[5 * i + 3].Cells.Count > 1)
                {
                    (Table1.Rows[5 * i + 3].Cells[2].Controls[0] as CheckBox).Enabled = false;
                }
            }
        }*/
        Table1.Rows[Table1.Rows.Count - 1].Cells[0].Text += Math.Ceiling(5 * (b_count / sum_ball)) + "(" + b_count + "/" + sum_ball + ")";
        SaveResult((int)Math.Ceiling(5 * (b_count / sum_ball)), b_count, (int)sum_ball, (b_count / sum_ball) * 100, t_dt);
        for (int i = 0; i < ans.Count; i++)
        {
            SaveUserBalls(us_b[i], us_ans[i], ans[i]);
        }
    }
    protected string AnswerFormat(string ans)
    {
        string format = "";
        //
        string[] s = ans.Split(' ', ',');
        if(s.Length == 1)
        {
            if(s[0].Length == 1)
            {
                if(Char.IsDigit(s[0][0]) == true)
                {
                    format = "Only one right answer";
                }
                else
                {
                    format = "Without variants";
                }
            }
            else
            {
                format = "Without variants";
            }
        }
        else
        {
            if(s.Length <= 3)
            {
                format = "Several right answers";
                for(int i = 0; i < s.Length; i++)
                {
                    if (s[i].Length == 1)
                    {
                        if (Char.IsDigit(s[0][0]) == false)
                        {
                            format = "Without variants";
                        }
                    }
                    else
                    {
                        format = "Without variants";
                    }
                }
            }
            else
            {
                format = "Without variants";
            }
        }
        return format;
    }
    protected bool IsRight(string ans, int i)
    {
        bool res = false;
        if (ans == "1")
        {
            if ((Table1.Rows[5 * i + 2].Cells[0].Controls[0] as CheckBox).Checked == true)
            {
                res = true;
            }
        }
        else if (ans == "2")
        {
            if ((Table1.Rows[5 * i + 2].Cells[2].Controls[0] as CheckBox).Checked == true)
            {
                res = true;
            }
        }
        else if (ans == "3")
        {
            if ((Table1.Rows[5 * i + 3].Cells[0].Controls[0] as CheckBox).Checked == true)
            {
                res = true;
            }
        }
        else
        {
            if ((Table1.Rows[5 * i + 3].Cells[2].Controls[0] as CheckBox).Checked == true)
            {
                res = true;
            }
        }
        return res;
    }
    protected string GetUserAnswer(int i)
    {
        string res = null;
        if(AnswerFormat(ans[i]) == "Several right answers")
        {
            if ((Table1.Rows[5 * i + 2].Cells[0].Controls[0] as CheckBox).Checked == true)
            {
                res += "1";
            }
            if ((Table1.Rows[5 * i + 2].Cells[2].Controls[0] as CheckBox).Checked == true)
            {
                if (res != null)
                {
                    res += ", ";
                }
                res += "2";
            }
            if (Table1.Rows[5 * i + 3].Cells[0].Controls.Count != 0)
            {
                if ((Table1.Rows[5 * i + 3].Cells[0].Controls[0] as CheckBox).Checked == true)
                {
                    if (res != null)
                    {
                        res += ", ";
                    }
                    res += "3";
                }
                if (Table1.Rows[5 * i + 3].Cells.Count > 1)
                {
                    if ((Table1.Rows[5 * i + 3].Cells[2].Controls[0] as CheckBox).Checked == true)
                    {
                        if (res != null)
                        {
                            res += ", ";
                        }
                        res += "4";
                    }
                }
            }
        }
        if (AnswerFormat(ans[i]) == "Only one right answer")
        {
            if ((Table1.Rows[5 * i + 2].Cells[0].Controls[0] as RadioButton).Checked == true)
            {
                res = "1";
            }
            else if ((Table1.Rows[5 * i + 2].Cells[2].Controls[0] as RadioButton).Checked == true)
            {
                res = "2";
            }
            else
            {
                if (Table1.Rows[5 * i + 3].Cells[0].Controls.Count != 0)
                {
                    if ((Table1.Rows[5 * i + 3].Cells[0].Controls[0] as RadioButton).Checked == true)
                    {
                        res = "3";
                    }
                    if (Table1.Rows[5 * i + 3].Cells.Count > 1)
                    {
                        if ((Table1.Rows[5 * i + 3].Cells[2].Controls[0] as RadioButton).Checked == true)
                        {
                            res = "4";
                        }
                    }
                }
            }
        }
        return res;
    }
    protected void GetExamResultNumber()
    {
        sc.Open();
        using (SqlCommand command = new SqlCommand("GetExamID1", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                id_ex = int.Parse(sdr[0].ToString());
            }
            sdr.Close();
        }
        sc.Close();
    }
    protected void SaveResult(int mark, double r_b, int b, double proc, DateTime dt)
    {
        sc.Open();
        /*int id = 0, p_num = 1;
        using (SqlCommand command = new SqlCommand("GetExamID1", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                id = int.Parse(sdr[0].ToString());
            }
            sdr.Close();
        }
        id_ex = id;*/
        //Количество попыток - создать процедуру
        /*using (SqlCommand command = new SqlCommand("GetTryNum", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_bl";
            param.Value = bl_num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            
            param = new SqlParameter();
            param.ParameterName = "@id_us";
            param.Value = us_id;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            
            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                p_num = int.Parse(sdr[0].ToString());
            }
            sdr.Close();
        }*/
        using (SqlCommand command = new SqlCommand("AddExResult", sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_num";
            param.Value = id_ex;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            
            param = new SqlParameter();
            param.ParameterName = "@id_us";
            param.Value = us_id;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            
            param = new SqlParameter();
            param.ParameterName = "@id_bl";
            param.Value = bl_num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@m";
            param.Value = mark;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@b";
            param.Value = r_b;
            param.SqlDbType = SqlDbType.Real;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@max_res";
            param.Value = b;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@pc";
            param.Value = proc;
            param.SqlDbType = SqlDbType.Real;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt";
            param.Value = dt;
            param.SqlDbType = SqlDbType.DateTime;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@p_num";
            //param.Value = p_num;
            param.Value = try_num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }
        sc.Close();
    }
    protected void SaveUserBalls(double r_b, string us_ans, string r_ans)
    {
        int id = 0;
        sc.Open();
        using (SqlCommand command = new SqlCommand("GetAnswerID1", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                id = int.Parse(sdr[0].ToString());
            }
            sdr.Close();
        }
        //Номера + реальные результаты
        using (SqlCommand command = new SqlCommand("AddUserAnswers", sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_num";
            param.Value = id;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@id_us";
            param.Value = us_id;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@id_bl";
            param.Value = bl_num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@id_ex";
            param.Value = id_ex;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            
            param = new SqlParameter();
            param.ParameterName = "@b";
            param.Value = r_b;
            param.SqlDbType = SqlDbType.Real;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@us_ans";
            param.Value = us_ans;
            param.SqlDbType = SqlDbType.NText;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@tr_ans";
            param.Value = r_ans;
            param.SqlDbType = SqlDbType.NText;
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }
        sc.Close();
    }
}