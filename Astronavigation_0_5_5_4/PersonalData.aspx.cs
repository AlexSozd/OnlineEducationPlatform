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

public partial class PersonalData : System.Web.UI.Page
{
    int /*bl_num,*/ us_id, id_ex;
    bool req = true;
    DateTime dt1, dt2;
    SqlConnection sc = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (req == true)
            {
                if (Request.QueryString.Count > 1)
                {
                    req = false;
                    us_id = int.Parse(Request.QueryString["UserId"]);
                    itemTable.GridLines = GridLines.Both;
                    itemTable.BorderWidth = 2;
                    DetailButton.Enabled = false;
                    DetailButton.Visible = false;
                    answerList.Enabled = false;
                    answerList.Visible = false;
                    hyperlink1.HRef = "~/" + Request.QueryString["from"];
                    FindUserName();
                    FillLists();
                }
                else
                {
                    Response.Redirect("Authorization.aspx?from=" + Page.Request.Url.Segments[1]);
                }
            }
        }
        catch (Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Source + ": " + ex.Message);
            Response.Write(ex.Message);
        }
    }
    protected void FindUserName()
    {
        sc.Open();
        using (SqlCommand command = new SqlCommand("FindUserName", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_us";
            param.Value = us_id;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);
            
            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                UserName.Text = sdr["name"].ToString();
            }
        }
        sc.Close();
    }
    protected void FillLists()
    {
        if (Tests.Items.Count == 0)
        {
            Tests.Items.Add("");
            Tests.Items.Add("Все");
            sc.Open();
            using (SqlCommand command = new SqlCommand("ShowTests", sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read() == true)
                {
                    Tests.Items.Add(sdr["name"].ToString());
                }
            }
            sc.Close();
        }

        if(Attempts.Items.Count == 0)
        {
            Attempts.Items.Add("Все");
            Attempts.Items.Add("Лучший результат");
            Attempts.Items.Add("Последний по времени");
        }
        
        if(TestTasks.Items.Count == 0)
        {
            TestTasks.Items.Add("Все");
        }
        //TestTasks.Items.Add("Все");
        //Fill in another function
    }
    protected void Tests_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Reload table
        if (Tests.SelectedItem.Text == "Все")
        {
            //ShowAllTestsResults(us_id);
        }
        else if (Tests.SelectedItem.Text == "")
        {
            //MessageBox.Show("Вы не выбрали категорию поиска!");
        }
        else
        {
            //ShowSelectedResults(Persons.SelectedItem.ToString(), Tests.SelectedItem.ToString());
            //Заполнить список заданий
            FillTestTaskList();
        }
        FillitemTable();
    }
    protected void FillTestTaskList()
    {
        TestTasks.Items.Clear();
        TestTasks.Items.Add("Все");
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowTestTaskList", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@name";
            param.Value = Tests.SelectedItem.Text;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TestTasks.Items.Add(sdr["ques"].ToString());
            }
        }
        sc.Close();
    }
    protected void Attempts_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillitemTable();
    }
    protected void TestTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillitemTable();
    }
    protected void FillitemTable()
    {
        if (Tests.SelectedItem.Text == "Все")
        {
            TestTasks.Items.Clear();
            TestTasks.Items.Add("Все");
            TestTasks.SelectedValue = "Все";
            TestTasks.Enabled = false;
            if (Attempts.SelectedItem.Text == "Все")
            {
                if ((BeginTime.Text == "") && (EndTime.Text == ""))
                {
                    //ShowAllTestsResults(UserName.Text);
                    ShowSelectedResults("ShowUserResults2", UserName.Text);
                }
                else if ((DateTime.TryParse(BeginTime.Text.Replace('.','/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.','/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                {
                    if (dt2 > dt1)
                    {
                        //ShowAllTestsResults(UserName.Text, dt1, dt2);
                        ShowSelectedResults("ShowUserResultsInTimePeriod", UserName.Text, dt1, dt2);
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                }
                else
                {
                    ShowErrorMessage(2);
                }
            }
            else if (Attempts.SelectedValue == "Лучший результат")
            {
                if ((BeginTime.Text == "") && (EndTime.Text == ""))
                {
                    //ShowBestTestsResults(UserName.Text);
                    ShowSelectedResults("ShowBestUserResults", UserName.Text);
                }
                else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                {
                    if (dt2 > dt1)
                    {
                        //ShowBestTestsResults(UserName.Text, dt1, dt2);
                        ShowSelectedResults("ShowBestUserResultsForPeriod", UserName.Text, dt1, dt2);
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                }
                else
                {
                    ShowErrorMessage(2);
                }
            }
            else
            {
                if ((BeginTime.Text == "") && (EndTime.Text == ""))
                {
                    //ShowLastTestsResults(UserName.Text);
                    ShowSelectedResults("ShowLastUserResults", UserName.Text);
                }
                else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                {
                    if (dt2 > dt1)
                    {
                        //ShowLastTestsResults(UserName.Text, dt1, dt2);
                        ShowSelectedResults("ShowLastUserResultsForPeriod", UserName.Text, dt1, dt2);
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                }
                else
                {
                    ShowErrorMessage(2);
                }
            }
        }
        else if (Tests.SelectedItem.Text == "")
        {
            TestTasks.Items.Clear();
            TestTasks.Items.Add("Все");
            ShowErrorMessage(1);
        }
        else
        {
            TestTasks.Enabled = true;
            if (Attempts.SelectedItem.Text == "Все")
            {
                if(TestTasks.SelectedItem.Text == "Все")
                {
                    if ((BeginTime.Text == "") && (EndTime.Text == ""))
                    {
                        ShowSelectedResults("ShowUserAnswersInTest", UserName.Text, Tests.SelectedValue);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            ShowSelectedResults("ShowUserAnswersInTestForTimePeriod", UserName.Text, Tests.SelectedValue, dt1, dt2);
                        }
                        else
                        {
                            ShowErrorMessage(2);
                        }
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                }
                else
                {
                    if ((BeginTime.Text == "") && (EndTime.Text == ""))
                    {
                        ShowUserAnswer("ShowUserAnswer", UserName.Text, TestTasks.SelectedValue);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            ShowUserAnswer("ShowUserAnswerForTimePeriod", UserName.Text, TestTasks.SelectedValue, dt1, dt2);
                        }
                        else
                        {
                            ShowErrorMessage(2);
                        }
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                }
            }
            else if (Attempts.SelectedValue == "Лучший результат")
            {
                if (TestTasks.SelectedItem.Text == "Все")
                {
                    if ((BeginTime.Text == "") && (EndTime.Text == ""))
                    {
                        //ShowBestTestsResults(UserName.Text, Tests.SelectedValue);
                        ShowSelectedResults("ShowBestUserAnswerInTest", UserName.Text, Tests.SelectedValue);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            //ShowBestTestsResults(UserName.Text, Tests.SelectedValue, dt1, dt2);
                            ShowSelectedResults("ShowBestUserAnswerInTestForPeriod", UserName.Text, Tests.SelectedValue, dt1, dt2);
                        }
                        else
                        {
                            ShowErrorMessage(2);
                        }
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                    if(itemTable.Rows.Count == 2)
                    {
                        DetailButton.Enabled = true;
                        DetailButton.Visible = true;
                    }
                }
                else
                {
                    if ((BeginTime.Text == "") && (EndTime.Text == ""))
                    {
                        //ShowUserBestAnswer(UserName.Text, TestTasks.SelectedValue);
                        ShowUserAnswer("ShowUserBestAnswerBall", UserName.Text, TestTasks.SelectedValue);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            //ShowUserBestAnswer(UserName.Text, TestTasks.SelectedValue, dt1, dt2);
                            ShowUserAnswer("ShowUserBestAnswerBallForTimePeriod", UserName.Text, TestTasks.SelectedValue, dt1, dt2);
                        }
                        else
                        {
                            ShowErrorMessage(2);
                        }
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                }
            }
            else
            {
                if (TestTasks.SelectedItem.Text == "Все")
                {
                    if ((BeginTime.Text == "") && (EndTime.Text == ""))
                    {
                        //ShowLastTestsResults(UserName.Text, Tests.SelectedValue);
                        ShowSelectedResults("ShowLastUserAnswerInTest", UserName.Text, Tests.SelectedValue);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            //ShowLastTestsResults(UserName.Text, Tests.SelectedValue, dt1, dt2);
                            ShowSelectedResults("ShowLastUserAnswerInTestForPeriod", UserName.Text, Tests.SelectedValue, dt1, dt2);
                        }
                        else
                        {
                            ShowErrorMessage(2);
                        }
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                    if (itemTable.Rows.Count == 2)
                    {
                        DetailButton.Enabled = true;
                        DetailButton.Visible = true;
                    }
                }
                else
                {
                    if ((BeginTime.Text == "") && (EndTime.Text == ""))
                    {
                        //ShowUserLastAnswer(UserName.Text, TestTasks.SelectedValue);
                        ShowUserAnswer("ShowUserLastAnswerBall", UserName.Text, TestTasks.SelectedValue);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            //ShowUserLastAnswer(UserName.Text, TestTasks.SelectedValue);
                            ShowUserAnswer("ShowUserLastAnswerBallForTimePeriod", UserName.Text, TestTasks.SelectedValue, dt1, dt2); 
                            
                        }
                        else
                        {
                            ShowErrorMessage(2);
                        }
                    }
                    else
                    {
                        ShowErrorMessage(2);
                    }
                }
            }
        }
    }
    protected void ShowErrorMessage(int fl)
    {
        itemTable.Rows.Clear();

        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        if(fl == 1)
        {
            tc.Text = "Вы не выбрали категорию поиска!";
        }
        if(fl == 2)
        {
            tc.Text = "Вы неправильно указали время! Проверьте введённое значение.";
        }
        tr.Cells.Add(tc);
    }
    protected void ShowSelectedResults(string proc, string Username)
    {
        itemTable.Rows.Clear();
        
        TableHeaderRow tr = new TableHeaderRow();
        
        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Test";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Max_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Procent";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Mark";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);
        
        itemTable.Rows.Add(tr);
        //Один студент и все тесты
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowUserResults2", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = Username;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();

                TableCell tc1 = new TableCell();
                tc1.Text = sdr["id"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["name"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["real_balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["max_res"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["procnt"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["mark"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["dt"].ToString();
                tr1.Cells.Add(tc1);
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
    protected void ShowSelectedResults(string proc, string Username, DateTime dt1, DateTime dt2)
    {
        itemTable.Rows.Clear();
        
        TableHeaderRow tr = new TableHeaderRow();

        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Test";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Max_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Procent";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Mark";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);
        itemTable.Rows.Add(tr);
        //Один студент и все тесты
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowUserResultsInTimePeriod", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = Username;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();

                TableCell tc1 = new TableCell();
                tc1.Text = sdr["id"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["name"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["real_balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["max_res"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["procnt"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["mark"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["dt"].ToString();
                tr1.Cells.Add(tc1);
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
    protected void ShowSelectedResults(string proc, string UserName, string TestName)
    {
        itemTable.Rows.Clear();
        TableHeaderRow tr = new TableHeaderRow();

        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Max_result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Procent";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Mark";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);
        itemTable.Rows.Add(tr);
        //Один студент, один тест
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowUserAnswersInTest", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@tname";
            param.Value = TestName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();

                TableCell tc1 = new TableCell();
                tc1.Text = sdr["id"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["real_balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["max_res"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["procnt"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["mark"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["dt"].ToString();
                tr1.Cells.Add(tc1);
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
    protected void ShowSelectedResults(string proc, string UserName, string TestName, DateTime dt1, DateTime dt2)
    {
        itemTable.Rows.Clear();
        TableHeaderRow tr = new TableHeaderRow();

        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Max_result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Procent";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Mark";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);
        itemTable.Rows.Add(tr);
        //Один студент, один тест
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowUserAnswersInTestForTimePeriod", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@tname";
            param.Value = TestName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();

                TableCell tc1 = new TableCell();
                tc1.Text = sdr["id"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["real_balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["max_res"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["procnt"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["mark"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["dt"].ToString();
                tr1.Cells.Add(tc1);
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
    /*protected void ShowBestTestsResults(string Username)
    {
        * * *
        //Один студент и все тесты
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowBestUserResults", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = Username;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                * * *
            }
        }
        sc.Close();
    }*/
    /*protected void ShowBestTestsResults(string Username, DateTime dt1, DateTime dt2)
    {
        * * *
        //Один студент и все тесты
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowBestUserResultsForPeriod", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = Username;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowBestTestsResults(string UserName, string TestName)
    {
        * * *
        //Один студент, один тест
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowBestUserAnswerInTest", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@tname";
            param.Value = TestName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowBestTestsResults(string UserName, string TestName, DateTime dt1, DateTime dt2)
    {
        * * *
        //Один студент, один тест
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowBestUserAnswerInTestForPeriod", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@tname";
            param.Value = TestName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowLastTestsResults(string Username)
    {
        * * *
        //Один студент и все тесты
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowLastUserResults", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = Username;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowLastTestsResults(string Username, DateTime dt1, DateTime dt2)
    {
        * * *
        //Один студент и все тесты
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowLastUserResultsForPeriod", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = Username;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowLastTestsResults(string UserName, string TestName)
    {
        * * *
        //Один студент, один тест
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowLastUserAnswerInTest", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@tname";
            param.Value = TestName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowLastTestsResults(string UserName, string TestName, DateTime dt1, DateTime dt2)
    {
        * * *
        //Один студент, один тест
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowLastUserAnswerInTestForPeriod", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@tname";
            param.Value = TestName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    protected void ShowUserAnswer(string proc, string UserName, string Ques)
    {
        itemTable.Rows.Clear();
        TableHeaderRow tr = new TableHeaderRow();

        TableHeaderCell tc = new TableHeaderCell();
        /*tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();*/
        
        tc.Text = "User's answer";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Right answer";
        tr.Cells.Add(tc);
        tc.Text = "User's ball";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Ball for right answer";
        tr.Cells.Add(tc);

        /*tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);*/

        itemTable.Rows.Add(tr);
        //Один студент, один тест
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowUserAnswer", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = Ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();

                TableCell tc1 = new TableCell();
                
                /*tc1.Text = sdr["id"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();*/

                tc1.Text = sdr["us_ans"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["tr_ans"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["us_balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["balls"].ToString();
                tr1.Cells.Add(tc1);
                /*tc1 = new TableCell();
                tc1.Text = sdr["dt"].ToString();
                tr1.Cells.Add(tc1);*/
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
    protected void ShowUserAnswer(string proc, string UserName, string Ques, DateTime dt1, DateTime dt2)
    {
        itemTable.Rows.Clear();
        TableHeaderRow tr = new TableHeaderRow();
        TableHeaderCell tc = new TableHeaderCell();
        /*tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();*/
        tc.Text = "User's answer";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Right answer";
        tr.Cells.Add(tc);
        tc.Text = "User's ball";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Ball for right answer";
        tr.Cells.Add(tc);
        /*tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);*/

        /*TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Max_result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Procent";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Mark";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);*/
        itemTable.Rows.Add(tr);
        //Один студент, один тест
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowUserAnswerForTimePeriod", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = Ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                TableCell tc1 = new TableCell();
                /*tc1.Text = sdr["id"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();*/
                tc1.Text = sdr["us_ans"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["tr_ans"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["us_balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["balls"].ToString();
                tr1.Cells.Add(tc1);
                /*tc1 = new TableCell();
                tc1.Text = sdr["dt"].ToString();
                tr1.Cells.Add(tc1);*/

                /*TableCell tc1 = new TableCell();
                tc1.Text = sdr["id"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["real_balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["max_res"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["procnt"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["mark"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["dt"].ToString();
                tr1.Cells.Add(tc1);*/
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
    /*protected void ShowUserBestAnswer(string UserName, string Ques)
    {
        * * *
        //Один студент, один тест
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowUserBestAnswerBall", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = Ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowUserBestAnswer(string UserName, string Ques, DateTime dt1, DateTime dt2)
    {
        * * *
        //Один студент, один тест
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowUserBestAnswerBallForTimePeriod", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = Ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowUserLastAnswer(string UserName, string Ques)
    {
        * * *
        //Один студент, один тест
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowUserLastAnswerBall", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = Ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    /*protected void ShowUserLastAnswer(string UserName, string Ques, DateTime dt1, DateTime dt2)
    {
        * * *
        //Один студент, один тест
        sc.Open();
        using (SqlCommand command = new SqlCommand("ShowUserLastAnswerBallForTimePeriod", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = Ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt1";
            param.Value = dt1;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@dt2";
            param.Value = dt2;
            param.DbType = DbType.Date;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                * * *
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }*/
    protected void DetailButton_Click(object sender, EventArgs e)
    {
        int i = 0;
        
        answerList.Enabled = true;
        answerList.Visible = true;
        answerList.Rows.Clear();

        TableHeaderRow tr = new TableHeaderRow();

        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "Number";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Question";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Max_result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Your answer";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Right answer";
        tr.Cells.Add(tc);
        
        answerList.Rows.Add(tr);

        sc.Open();
        using (SqlCommand command = new SqlCommand("PersonalTestAnswers", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_ex";
            param.Value = id_ex;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                i++;

                TableRow tr1 = new TableRow();

                TableCell tc1 = new TableCell();
                tc1.Text = i.ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["ques"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["us_balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["balls"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["us_ans"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                tc1.Text = sdr["tr_ans"].ToString();
                tr1.Cells.Add(tc1);
                
                answerList.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
}