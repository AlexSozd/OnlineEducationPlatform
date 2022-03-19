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

public partial class Statistics : System.Web.UI.Page
{
    bool req = true;
    int us_id;
    //List<TableRow> trc = new List<TableRow>();
    //List<DataRow> trc=new List<DataRow>();
    DataTable dt;
    SqlConnection sc = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (req == true)
            {
                /*if (Request.QueryString.Count > 1)
                {
                    req = false;
                    us_id = int.Parse(Request.QueryString["UserId"]);
                    itemTable.GridLines = GridLines.Both;
                    itemTable.BorderWidth = 2;
                    st_per.Disabled = false;
                    StatAnalyze.Enabled = false;
                    StatAnalyze.Visible = false;
                    FillLists();
                }*/
                if (Request.QueryString["UserId"] != null)
                {
                    req = false;
                    us_id = int.Parse(Request.QueryString["UserId"]);
                    itemTable.GridLines = GridLines.Both;
                    itemTable.BorderWidth = 2;
                    st_per.Disabled = false;
                    StatAnalyze.Enabled = false;
                    StatAnalyze.Visible = false;
                    FillLists();
                }
                else
                {
                    //Response.Redirect("Authorization.aspx?from=TestForm.aspx?number=" + pagenum.ToString());
                    //Response.Redirect(Page.PreviousPage.Request.Url.AbsoluteUri);
                    Response.Redirect("Authorization.aspx?from=" + Page.Request.Url.Segments[1]);
                }
            }
        }
        catch (Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Source + ": " + ex.Message);
            Response.Write(ex.Message);
        }
        //Fill dropDownLists
        //FillLists();
    }
    protected void Persons_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Reload table
        Attempts.SelectedValue = "Все";
        TestTasks.SelectedValue = "Все";
        st_per.Disabled = false;
        st_per.Value = "0";
        res_per.Value = "0";
        BeginTime.Text = "";
        EndTime.Text = "";
        if (Persons.SelectedValue == "Все")
        {
            itemname.Text = "По всем студентам";
            if(Tests.SelectedValue == "Все")
            {
                TestTasks.Items.Clear();
                TestTasks.Items.Add("Все");
                TestTasks.SelectedValue = "Все";
                TestTasks.Enabled = false;
                ShowSelectedResults("ShowPassedProcentTestResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
            }
            else if (Tests.SelectedValue == "")
            {
                TestTasks.Items.Clear();
                TestTasks.Items.Add("Все");
                TestTasks.SelectedValue = "Все";
                ShowErrorMessage(1);
            }
            else
            {
                st_per.Disabled = true;
                ShowSelectedResults("ShowResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
            }
        }
        else if (Persons.SelectedValue == "")
        {
            ShowErrorMessage(1);
        }
        else
        {
            itemname.Text = Persons.SelectedItem.ToString();
            st_per.Disabled = true;
            if (Tests.SelectedValue == "Все")
            {
                TestTasks.Items.Clear();
                TestTasks.Items.Add("Все");
                TestTasks.SelectedValue = "Все";
                TestTasks.Enabled = false;
                ShowSelectedResults("ShowUserResults3", int.Parse(res_per.Value), Persons.SelectedValue, 0);
            }
            else if (Tests.SelectedValue == "")
            {
                ShowErrorMessage(1);
            }
            else
            {
                ShowSelectedResults("ShowUserAnswersInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
            }
        }
    }
    protected void Tests_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Reload table
        Attempts.SelectedValue = "Все";
        TestTasks.SelectedValue = "Все";
        st_per.Disabled = false;
        st_per.Value = "0";
        res_per.Value = "0";
        BeginTime.Text = "";
        EndTime.Text = "";
        if (Tests.SelectedValue == "Все")
        {
            itemname.Text = "По всем тестам";
            Attempts.SelectedValue = "Все";
            TestTasks.Items.Clear();
            TestTasks.Items.Add("Все");
            TestTasks.SelectedValue = "Все";
            TestTasks.Enabled = false;
            if (Persons.SelectedValue == "Все")
            {
                ShowSelectedResults("ShowPassedProcentTestResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
            }
            else if (Persons.SelectedValue == "")
            {
                ShowErrorMessage(1);
            }
            else
            {
                st_per.Disabled = true;
                ShowSelectedResults("ShowUserResults3", int.Parse(res_per.Value), Persons.SelectedValue, 0);
            }
        }
        else if (Tests.SelectedValue == "")
        {
            TestTasks.Items.Clear();
            TestTasks.Items.Add("Все");
            TestTasks.SelectedValue = "Все";
            ShowErrorMessage(1);
        }
        else
        {
            st_per.Disabled = true;
            TestTasks.Enabled = true;
            FillTestTaskList();
            itemname.Text = Tests.SelectedValue;
            if (Persons.SelectedValue == "Все")
            {
                ShowSelectedResults("ShowResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
            }
            else if (Persons.SelectedValue == "")
            {
                ShowErrorMessage(1);
            }
            else
            {
                ShowSelectedResults("ShowUserAnswersInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
            }
        }
    }
    protected void FillLists()
    {
        if(Attempts.Items.Count == 0)
        {
            Attempts.Items.Add("Все");
            Attempts.Items.Add("Лучший результат");
            Attempts.Items.Add("Последний по времени");
        }
        if(Persons.Items.Count == 0)
        {
            Persons.Items.Add("");
            Persons.Items.Add("Все");
            sc.Open();
            using (SqlCommand command = new SqlCommand("ShowUsers", sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read() == true)
                {
                    Persons.Items.Add(sdr["name"].ToString());
                }
            }
            sc.Close();
        }
        if(Tests.Items.Count == 0)
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
        if(TestTasks.Items.Count == 0)
        {
            TestTasks.Items.Add("Все");
        }
        
        /*sc.Open();
        using (SqlCommand command = new SqlCommand("ShowUsers", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                Persons.Items.Add(sdr["name"].ToString());
            }
        }
        using (SqlCommand command = new SqlCommand("ShowTests", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                Tests.Items.Add(sdr["name"].ToString());
            }
        }
        sc.Close();*/
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
    protected void ShowErrorMessage(int fl)
    {
        itemTable.Rows.Clear();

        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        if (fl == 1)
        {
            tc.Text = "Вы не выбрали категорию поиска!";
        }
        if (fl == 2)
        {
            tc.Text = "Вы неправильно указали время! Проверьте введённое значение.";
        }
        tr.Cells.Add(tc);

        itemTable.Rows.Add(tr);
    }
    protected void ShowSelectedResults(string proc, int st_proc, int ex_proc)
    {
        itemTable.Rows.Clear();
        
        TableHeaderRow tr = new TableHeaderRow();

        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "User";
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
        tc.Text = "Percent";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Mark";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Number of try";
        tr.Cells.Add(tc);
        itemTable.Rows.Add(tr);
        //Все студенты, все тесты
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowPassedProcentTestResults", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@stud_proc";
            param.Value = st_proc;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);
            
            param = new SqlParameter();
            param.ParameterName = "@ex_proc";
            param.Value = ex_proc;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);
            
            SqlDataReader sdr = command.ExecuteReader();
            
            while (sdr.Read() == true)
            {
                TableRow tr1 = new TableRow();
                
                TableCell tc1 = new TableCell();
                tc1.Text = sdr["id"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                //tc1.Text = sdr["id_us"].ToString();
                tc1.Text = sdr["UserName"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                //tc1.Text = sdr["id_bl"].ToString();
                tc1.Text = sdr["TestName"].ToString();
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
                tc1 = new TableCell();
                tc1.Text = sdr["trynum"].ToString();
                tr1.Cells.Add(tc1);
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
    protected void ShowSelectedResults(string proc, int st_proc, int ex_proc, DateTime dt1, DateTime dt2)
    {
        itemTable.Rows.Clear();

        TableHeaderRow tr = new TableHeaderRow();

        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "ID_Result";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "User";
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
        tc.Text = "Percent";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Mark";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = "Number of try";
        tr.Cells.Add(tc);
        itemTable.Rows.Add(tr);
        //Все студенты, все тесты
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowPassedProcentTestResultsForTimePeriod", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@stud_proc";
            param.Value = st_proc;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@ex_proc";
            param.Value = ex_proc;
            param.DbType = DbType.Int32;
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
                //tc1.Text = sdr["id_us"].ToString();
                tc1.Text = sdr["UserName"].ToString();
                tr1.Cells.Add(tc1);
                tc1 = new TableCell();
                //tc1.Text = sdr["id_bl"].ToString();
                tc1.Text = sdr["TestName"].ToString();
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
                tc1 = new TableCell();
                tc1.Text = sdr["trynum"].ToString();
                tr1.Cells.Add(tc1);
                itemTable.Rows.Add(tr1);
            }
        }
        sc.Close();
    }
    protected void ShowSelectedResults(string proc, int per, string name, int fl)
    {
        itemTable.Rows.Clear();
        //Один тест и все студенты, или один студент и все тесты
        if(fl == 0)
        {
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

            sc.Open();
            //using (SqlCommand command = new SqlCommand("ShowUserResults3", sc) { CommandType = CommandType.StoredProcedure })
            using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@usname";
                param.Value = name;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@procnt";
                param.Value = per;
                param.DbType = DbType.Int32;
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
        else
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableHeaderCell tc = new TableHeaderCell();
            tc.Text = "ID_Result";
            tr.Cells.Add(tc);
            tc = new TableHeaderCell();
            tc.Text = "User";
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
            //По одному тесту
            sc.Open();
            //using (SqlCommand command = new SqlCommand("ShowResultsByTest1", sc) { CommandType = CommandType.StoredProcedure })
            using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@tname";
                param.Value = name;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@procnt";
                param.Value = per;
                param.DbType = DbType.Int32;
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
    }
    protected void ShowSelectedResults(string proc, int per, string UserName, string TestName)
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
        //using (SqlCommand command = new SqlCommand("ShowUserAnswersInTest1", sc) { CommandType = CommandType.StoredProcedure })
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
            param.ParameterName = "@procnt";
            param.Value = per;
            param.DbType = DbType.Int32;
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
    protected void ShowSelectedResults(string proc, int per, string name, int fl, DateTime dt1, DateTime dt2)
    {
        itemTable.Rows.Clear();
        //Один тест и все студенты, или один студент и все тесты
        if (fl == 0)
        {
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

            sc.Open();
            //using (SqlCommand command = new SqlCommand("ShowUserResultsInTimePeriod1", sc) { CommandType = CommandType.StoredProcedure })
            using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@usname";
                param.Value = name;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@procnt";
                param.Value = per;
                param.DbType = DbType.Int32;
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
        else
        {
            TableHeaderRow tr = new TableHeaderRow();

            TableHeaderCell tc = new TableHeaderCell();
            tc.Text = "ID_Result";
            tr.Cells.Add(tc);
            tc = new TableHeaderCell();
            tc.Text = "User";
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
            //По одному тесту
            sc.Open();
            //using (SqlCommand command = new SqlCommand("ShowResultsByTestForTimePeriod1", sc) { CommandType = CommandType.StoredProcedure })
            using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@tname";
                param.Value = name;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@procnt";
                param.Value = per;
                param.DbType = DbType.Int32;
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
                    tr1.Cells.Add(tc);
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
    }
    protected void ShowSelectedResults(string proc, int per, string UserName, string TestName, DateTime dt1, DateTime dt2)
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
        //using (SqlCommand command = new SqlCommand("ShowUserAnswersInTestForTimePeriod1", sc) { CommandType = CommandType.StoredProcedure })
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
            param.ParameterName = "@procnt";
            param.Value = per;
            param.DbType = DbType.Int32;
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
    protected void Attempts_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Reload table
        st_per.Disabled = false;
        st_per.Value = "0";
        res_per.Value = "0";
        TestTasks.SelectedValue = "Все";
        BeginTime.Text = "";
        EndTime.Text = "";
        if (Tests.SelectedValue == "Все")
        {
            itemname.Text = "По всем тестам";
            //TestTasks.SelectedValue = "Все";
            TestTasks.Items.Clear();
            TestTasks.Items.Add("Все");
            TestTasks.SelectedValue = "Все";
            TestTasks.Enabled = false;
            if (Persons.SelectedValue == "Все")
            {
                if(Attempts.SelectedValue == "Все")
                {
                    ShowSelectedResults("ShowPassedProcentTestResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                }
                else if (Attempts.SelectedValue == "Лучший результат")
                {
                    ShowSelectedResults("ShowProcentedBestResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                }
                else
                {
                    ShowSelectedResults("ShowProcentedLastResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                }
            }
            else if (Persons.SelectedValue == "")
            {
                ShowErrorMessage(1);
            }
            else
            {
                st_per.Disabled = true;
                if (Attempts.SelectedValue == "Все")
                {
                    ShowSelectedResults("ShowUserResults3", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                }
                else if (Attempts.SelectedValue == "Лучший результат")
                {
                    ShowSelectedResults("ShowBestUserResults1", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                }
                else
                {
                    ShowSelectedResults("ShowLastUserResults1", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                }
            }
        }
        else if (Tests.SelectedValue == "")
        {
            TestTasks.Items.Clear();
            TestTasks.Items.Add("Все");
            TestTasks.SelectedValue = "Все";
            ShowErrorMessage(1);
        }
        else
        {
            itemname.Text = Tests.SelectedValue;
            st_per.Disabled = true;
            if (Persons.SelectedValue == "Все")
            {
                if(Attempts.SelectedValue == "Все")
                {
                    ShowSelectedResults("ShowResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                }
                else if (Attempts.SelectedValue == "Лучший результат")
                {
                    ShowSelectedResults("ShowBestResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                }
                else
                {
                    ShowSelectedResults("ShowLastResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                }
            }
            else if (Persons.SelectedValue == "")
            {
                ShowErrorMessage(1);
            }
            else
            {
                if (Attempts.SelectedValue == "Все")
                {
                    ShowSelectedResults("ShowUserAnswersInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                }
                else if (Attempts.SelectedValue == "Лучший результат")
                {
                    ShowSelectedResults("ShowBestUserAnswerInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                }
                else
                {
                    ShowSelectedResults("ShowLastUserAnswerInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                }
            }
        }
    }
    protected void TestTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        BeginTime.Text = "";
        EndTime.Text = "";
        itemname.Text = Tests.SelectedValue;
        if (Tests.SelectedValue == "Все")
        {
            if (Persons.SelectedValue == "Все")
            {
                if (Attempts.SelectedValue == "Все")
                {
                    ShowSelectedResults("ShowPassedProcentTestResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                }
                else if (Attempts.SelectedValue == "Лучший результат")
                {
                    ShowSelectedResults("ShowProcentedBestResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                }
                else
                {
                    ShowSelectedResults("ShowProcentedLastResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                }
            }
            else if (Persons.SelectedValue == "")
            {
                ShowErrorMessage(1);
            }
            else
            {
                if (Attempts.SelectedValue == "Все")
                {
                    ShowSelectedResults("ShowUserResults3", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                }
                else if (Attempts.SelectedValue == "Лучший результат")
                {
                    ShowSelectedResults("ShowBestUserResults1", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                }
                else
                {
                    ShowSelectedResults("ShowLastUserResults1", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                }
            }
        }
        else if (Tests.SelectedValue == "")
        {
            ShowErrorMessage(1);
        }
        else
        {
            if(TestTasks.SelectedValue == "Все")
            {
                if (Persons.SelectedValue == "Все")
                {
                    if (Attempts.SelectedValue == "Все")
                    {
                        ShowSelectedResults("ShowResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                    }
                    else if (Attempts.SelectedValue == "Лучший результат")
                    {
                        ShowSelectedResults("ShowBestResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                    }
                    else
                    {
                        ShowSelectedResults("ShowLastResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                    }
                }
                else if (Persons.SelectedValue == "")
                {
                    ShowErrorMessage(1);
                }
                else
                {
                    if (Attempts.SelectedValue == "Все")
                    {
                        ShowSelectedResults("ShowUserAnswersInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                    }
                    else if (Attempts.SelectedValue == "Лучший результат")
                    {
                        ShowSelectedResults("ShowBestUserAnswerInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                    }
                    else
                    {
                        ShowSelectedResults("ShowLastUserAnswerInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                    }
                }
            }
            else
            {
                if (Persons.SelectedValue == "Все")
                {
                    if (Attempts.SelectedValue == "Все")
                    {
                        ShowTaskAnswers("ShowPeopleAnswers1", int.Parse(res_per.Value), TestTasks.SelectedValue);
                        StatAnalyze.Visible = true;
                        StatAnalyze.Enabled = true;
                    }
                    else if (Attempts.SelectedValue == "Лучший результат")
                    {
                        ShowTaskAnswers("ShowPeopleBestAnswerBalls1", int.Parse(res_per.Value), TestTasks.SelectedValue);
                        StatAnalyze.Visible = true;
                        StatAnalyze.Enabled = true;
                    }
                    else
                    {
                        ShowTaskAnswers("ShowPeopleLastAnswerBalls1", int.Parse(res_per.Value), TestTasks.SelectedValue);
                        StatAnalyze.Visible = true;
                        StatAnalyze.Enabled = true;
                    }
                }
                else if (Persons.SelectedValue == "")
                {
                    ShowErrorMessage(1);
                }
                else
                {
                    if (Attempts.SelectedValue == "Все")
                    {
                        ShowTaskAnswers("ShowUserAnswer1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue);
                        StatAnalyze.Visible = true;
                        StatAnalyze.Enabled = true;
                    }
                    else if (Attempts.SelectedValue == "Лучший результат")
                    {
                        ShowTaskAnswers("ShowUserBestAnswerBall1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue);
                        StatAnalyze.Visible = true;
                        StatAnalyze.Enabled = true;
                    }
                    else
                    {
                        ShowTaskAnswers("ShowUserLastAnswerBall1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue);
                        StatAnalyze.Visible = true;
                        StatAnalyze.Enabled = true;
                    }
                }
            }
        }
    }
    protected void ShowTaskAnswers(string proc, int per, string ques)
    {
        int i = 0;
        dt = MakeDataTableForSaving();
        itemTable.Rows.Clear();
        //trc.Clear();
        dt.Clear();
        TableHeaderRow tr = new TableHeaderRow();
        DataRow dr = dt.NewRow();
        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "Number";
        tr.Cells.Add(tc);
        dr[0] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        dr[1] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Max_result";
        tr.Cells.Add(tc);
        dr[2] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Person's answer";
        tr.Cells.Add(tc);
        dr[3] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Right answer";
        tr.Cells.Add(tc);
        dr[4] = tc.Text;
        itemTable.Rows.Add(tr);
        //trc.Add(tr);
        dt.Rows.Add(dr);
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowPeopleAnswers1", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@procnt";
            param.Value = per;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                i++;

                TableRow tr1 = new TableRow();
                dr = dt.NewRow();
                TableCell tc1 = new TableCell();
                tc1.Text = i.ToString();
                tr1.Cells.Add(tc1);
                dr[0] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["us_balls"].ToString();
                tr1.Cells.Add(tc1);
                dr[1] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["balls"].ToString();
                tr1.Cells.Add(tc1);
                dr[2] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["us_ans"].ToString();
                tr1.Cells.Add(tc1);
                dr[3] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["tr_ans"].ToString();
                tr1.Cells.Add(tc1);
                dr[4] = tc1.Text;
                itemTable.Rows.Add(tr1);
                //trc.Add(tr1);
                dt.Rows.Add(dr);
            }
        }
        sc.Close();
        //ViewState["Table"] = trc;
        ViewState["Table"] = dt;
    }
    protected void ShowTaskAnswers(string proc, int per, string ques, DateTime dt1, DateTime dt2)
    {
        int i = 0;
        dt = MakeDataTableForSaving();
        itemTable.Rows.Clear();
        //trc.Clear();
        dt.Clear();
        TableHeaderRow tr = new TableHeaderRow();
        DataRow dr = dt.NewRow();
        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "Number";
        tr.Cells.Add(tc);
        dr[0] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        dr[1] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Max_result";
        tr.Cells.Add(tc);
        dr[2] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Person's answer";
        tr.Cells.Add(tc);
        dr[3] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Right answer";
        tr.Cells.Add(tc);
        dr[4] = tc.Text;
        itemTable.Rows.Add(tr);
        //trc.Add(tr);
        dt.Rows.Add(dr);
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowPeopleAnswersForTimePeriod1", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@procnt";
            param.Value = per;
            param.DbType = DbType.Int32;
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
                i++;

                TableRow tr1 = new TableRow();
                dr = dt.NewRow();
                TableCell tc1 = new TableCell();
                tc1.Text = i.ToString();
                tr1.Cells.Add(tc1);
                dr[0] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["us_balls"].ToString();
                tr1.Cells.Add(tc1);
                dr[1] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["balls"].ToString();
                tr1.Cells.Add(tc1);
                dr[2] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["us_ans"].ToString();
                tr1.Cells.Add(tc1);
                dr[3] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["tr_ans"].ToString();
                tr1.Cells.Add(tc1);
                dr[4] = tc1.Text;
                itemTable.Rows.Add(tr1);
                //trc.Add(tr1);
                dt.Rows.Add(dr);
            }
        }
        sc.Close();
        //ViewState["Table"] = trc;
        ViewState["Table"] = dt;
    }
    protected void ShowTaskAnswers(string proc, int per, string UserName, string ques)
    {
        int i = 0;
        dt = MakeDataTableForSaving();
        itemTable.Rows.Clear();
        //trc.Clear();
        dt.Clear();
        TableHeaderRow tr = new TableHeaderRow();
        DataRow dr = dt.NewRow();
        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "Number";
        tr.Cells.Add(tc);
        dr[0] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        dr[1] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Max_result";
        tr.Cells.Add(tc);
        dr[2] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Person's answer";
        tr.Cells.Add(tc);
        dr[3] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Right answer";
        tr.Cells.Add(tc);
        dr[4] = tc.Text;
        itemTable.Rows.Add(tr);
        //trc.Add(tr);
        dt.Rows.Add(dr);
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
            param.Value = ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@procnt";
            param.Value = per;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                i++;

                TableRow tr1 = new TableRow();
                dr = dt.NewRow();
                TableCell tc1 = new TableCell();
                tc1.Text = i.ToString();
                tr1.Cells.Add(tc1);
                dr[0] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["us_balls"].ToString();
                tr1.Cells.Add(tc1);
                dr[1] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["balls"].ToString();
                tr1.Cells.Add(tc1);
                dr[2] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["us_ans"].ToString();
                tr1.Cells.Add(tc1);
                dr[3] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["tr_ans"].ToString();
                tr1.Cells.Add(tc1);
                dr[4] = tc1.Text;
                itemTable.Rows.Add(tr1);
                //trc.Add(tr1);
                dt.Rows.Add(dr);
            }
        }
        sc.Close();
        //ViewState["Table"] = trc;
        ViewState["Table"] = dt;
    }
    protected void ShowTaskAnswers(string proc, int per, string UserName, string ques, DateTime dt1, DateTime dt2)
    {
        int i = 0;
        dt = MakeDataTableForSaving();
        itemTable.Rows.Clear();
        //trc.Clear();
        dt.Clear();
        DataRow dr = dt.NewRow();
        TableHeaderRow tr = new TableHeaderRow();

        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = "Number";
        tr.Cells.Add(tc);
        dr[0] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Result_balls";
        tr.Cells.Add(tc);
        dr[1] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Max_result";
        tr.Cells.Add(tc);
        dr[2] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Person's answer";
        tr.Cells.Add(tc);
        dr[3] = tc.Text;
        tc = new TableHeaderCell();
        tc.Text = "Right answer";
        tr.Cells.Add(tc);
        dr[4] = tc.Text;
        itemTable.Rows.Add(tr);
        //trc.Add(tr);
        dt.Rows.Add(dr);
        sc.Open();
        //using (SqlCommand command = new SqlCommand("ShowUserAnswerForTimePeriod1", sc) { CommandType = CommandType.StoredProcedure })
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@usname";
            param.Value = UserName;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@ques";
            param.Value = ques;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@procnt";
            param.Value = per;
            param.DbType = DbType.Int32;
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
                i++;

                TableRow tr1 = new TableRow();
                dr = dt.NewRow();
                TableCell tc1 = new TableCell();
                tc1.Text = i.ToString();
                tr1.Cells.Add(tc1);
                dr[0] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["us_balls"].ToString();
                tr1.Cells.Add(tc1);
                dr[1] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["balls"].ToString();
                tr1.Cells.Add(tc1);
                dr[2] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["us_ans"].ToString();
                tr1.Cells.Add(tc1);
                dr[3] = tc1.Text;
                tc1 = new TableCell();
                tc1.Text = sdr["tr_ans"].ToString();
                tr1.Cells.Add(tc1);
                dr[4] = tc1.Text;
                itemTable.Rows.Add(tr1);
                //trc.Add(tr1);
                dt.Rows.Add(dr);
            }
        }
        sc.Close();
        //ViewState["Table"] = trc;
        ViewState["Table"] = dt;
    }
    protected void EnterTime_Click(object sender, EventArgs e)
    {
        DateTime dt1, dt2;

        if (Tests.SelectedValue == "Все")
        {
            if (Persons.SelectedValue == "Все")
            {
                if (Attempts.SelectedValue == "Все")
                {
                    if ((BeginTime.Text == "") && (EndTime.Text == ""))
                    {
                        ShowSelectedResults("ShowPassedProcentTestResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            ShowSelectedResults("ShowPassedProcentTestResultsForTimePeriod", int.Parse(st_per.Value), int.Parse(res_per.Value), dt1, dt2);
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
                        ShowSelectedResults("ShowProcentedBestResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            ShowSelectedResults("ShowProcentedBestResultsForPeriod", int.Parse(st_per.Value), int.Parse(res_per.Value), dt1, dt2);
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
                        ShowSelectedResults("ShowProcentedLastResults", int.Parse(st_per.Value), int.Parse(res_per.Value));
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            ShowSelectedResults("ShowProcentedLastResultsForPeriod", int.Parse(st_per.Value), int.Parse(res_per.Value), dt1, dt2);
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
            else if (Persons.SelectedValue == "")
            {
                ShowErrorMessage(1);
            }
            else
            {
                if (Attempts.SelectedValue == "Все")
                {
                    if ((BeginTime.Text == "") && (EndTime.Text == ""))
                    {
                        ShowSelectedResults("ShowUserResults3", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            ShowSelectedResults("ShowUserResultsInTimePeriod1", int.Parse(res_per.Value), Persons.SelectedValue, 0, dt1, dt2);
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
                        ShowSelectedResults("ShowBestUserResults1", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            ShowSelectedResults("ShowBestUserResultsForPeriod1", int.Parse(res_per.Value), Persons.SelectedValue, 0, dt1, dt2);
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
                        ShowSelectedResults("ShowLastUserResults1", int.Parse(res_per.Value), Persons.SelectedValue, 0);
                    }
                    else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                    //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                    {
                        if (dt2 > dt1)
                        {
                            ShowSelectedResults("ShowLastUserResultsForPeriod1", int.Parse(res_per.Value), Persons.SelectedValue, 0, dt1, dt2);
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
        else if (Tests.SelectedValue == "")
        {
            ShowErrorMessage(1);
        }
        else
        {
            if (TestTasks.SelectedValue == "Все")
            {
                if (Persons.SelectedValue == "Все")
                {
                    if (Attempts.SelectedValue == "Все")
                    {
                        if ((BeginTime.Text == "") && (EndTime.Text == ""))
                        {
                            ShowSelectedResults("ShowResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowSelectedResults("ShowResultsByTestForTimePeriod1", int.Parse(res_per.Value), Tests.SelectedValue, 1, dt1, dt2);
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
                            ShowSelectedResults("ShowBestResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowSelectedResults("ShowBestResultsByTestForPeriod1", int.Parse(res_per.Value), Tests.SelectedValue, 1, dt1, dt2);
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
                            ShowSelectedResults("ShowLastResultsByTest1", int.Parse(res_per.Value), Tests.SelectedValue, 1);
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowSelectedResults("ShowLastResultsByTestForPeriod1", int.Parse(res_per.Value), Tests.SelectedValue, 1, dt1, dt2);
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
                else if (Persons.SelectedValue == "")
                {
                    ShowErrorMessage(1);
                }
                else
                {
                    if (Attempts.SelectedValue == "Все")
                    {
                        if ((BeginTime.Text == "") && (EndTime.Text == ""))
                        {
                            ShowSelectedResults("ShowUserAnswersInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowSelectedResults("ShowUserAnswersInTestForTimePeriod1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue, dt1, dt2);
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
                            ShowSelectedResults("ShowBestUserAnswerInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowSelectedResults("ShowBestUserAnswerInTestForPeriod1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue, dt1, dt2);
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
                            ShowSelectedResults("ShowLastUserAnswerInTest1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue);
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowSelectedResults("ShowLastUserAnswerInTestForPeriod1", int.Parse(res_per.Value), Persons.SelectedValue, Tests.SelectedValue, dt1, dt2);
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
            else
            {
                if (Persons.SelectedValue == "Все")
                {
                    if (Attempts.SelectedValue == "Все")
                    {
                        if ((BeginTime.Text == "") && (EndTime.Text == ""))
                        {
                            ShowTaskAnswers("ShowPeopleAnswers1", int.Parse(res_per.Value), TestTasks.SelectedValue);
                            StatAnalyze.Visible = true;
                            StatAnalyze.Enabled = true;
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowTaskAnswers("ShowPeopleAnswersForTimePeriod1", int.Parse(res_per.Value), TestTasks.SelectedValue, dt1, dt2);
                                StatAnalyze.Visible = true;
                                StatAnalyze.Enabled = true;
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
                            ShowTaskAnswers("ShowPeopleBestAnswerBalls1", int.Parse(res_per.Value), TestTasks.SelectedValue);
                            StatAnalyze.Visible = true;
                            StatAnalyze.Enabled = true;
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowTaskAnswers("ShowPeopleBestAnswerBallsForTimePeriod1", int.Parse(res_per.Value), TestTasks.SelectedValue, dt1, dt2);
                                StatAnalyze.Visible = true;
                                StatAnalyze.Enabled = true;
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
                            ShowTaskAnswers("ShowPeopleLastAnswerBalls1", int.Parse(res_per.Value), TestTasks.SelectedValue);
                            StatAnalyze.Visible = true;
                            StatAnalyze.Enabled = true;
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowTaskAnswers("ShowPeopleLastAnswerBallsForTimePeriod1", int.Parse(res_per.Value), TestTasks.SelectedValue, dt1, dt2);
                                StatAnalyze.Visible = true;
                                StatAnalyze.Enabled = true;
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
                else if (Persons.SelectedValue == "")
                {
                    ShowErrorMessage(1);
                }
                else
                {
                    if (Attempts.SelectedValue == "Все")
                    {
                        if ((BeginTime.Text == "") && (EndTime.Text == ""))
                        {
                            ShowTaskAnswers("ShowUserAnswer1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue);
                            StatAnalyze.Visible = true;
                            StatAnalyze.Enabled = true;
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowTaskAnswers("ShowUserAnswerForTimePeriod1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue, dt1, dt2);
                                StatAnalyze.Visible = true;
                                StatAnalyze.Enabled = true;
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
                            ShowTaskAnswers("ShowUserBestAnswerBall1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue);
                            StatAnalyze.Visible = true;
                            StatAnalyze.Enabled = true;
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowTaskAnswers("ShowUserBestAnswerBallForTimePeriod1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue, dt1, dt2);
                                StatAnalyze.Visible = true;
                                StatAnalyze.Enabled = true;
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
                            ShowTaskAnswers("ShowUserLastAnswerBall1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue);
                            StatAnalyze.Visible = true;
                            StatAnalyze.Enabled = true;
                        }
                        else if ((DateTime.TryParse(BeginTime.Text.Replace('.', '/'), out dt1)) && (DateTime.TryParse(EndTime.Text.Replace('.', '/'), out dt2)))
                        //(((object)BeginTime.Text is DateTime) && ((object)EndTime.Text is DateTime))
                        {
                            if (dt2 > dt1)
                            {
                                ShowTaskAnswers("ShowUserLastAnswerBallForTimePeriod1", int.Parse(res_per.Value), Persons.SelectedValue, TestTasks.SelectedValue, dt1, dt2);
                                StatAnalyze.Visible = true;
                                StatAnalyze.Enabled = true;
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
    }
    protected void StatAnalyze_Click(object sender, EventArgs e)
    {
        double sr_ar = 0.000, disp = 0.000, sq_quad;
        StatAnalyze.Enabled = false;
        StatAnalyze.Visible = false;
        //trc = (List<TableRow>)ViewState["Table"];
        dt = (DataTable)ViewState["Table"];
        DataRow dr = dt.Rows[0];
        TableHeaderRow tr = new TableHeaderRow();
        TableHeaderCell tc = new TableHeaderCell();
        tc.Text = dr[0].ToString();
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = dr[1].ToString();
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = dr[2].ToString();
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = dr[3].ToString();
        tr.Cells.Add(tc);
        tc = new TableHeaderCell();
        tc.Text = dr[4].ToString();
        tr.Cells.Add(tc);
        itemTable.Rows.Clear();
        itemTable.Rows.Add(tr);
        //itemTable.Rows.Add(trc[0]);
        //itemTableCells
        for(int i = 1; i < /*itemTable.Rows.Count*/ dt.Rows.Count; i++)
        {
            dr = dt.Rows[i];
            TableRow tr1 = new TableRow();
            TableCell tc1 = new TableCell();
            tc1.Text = dr.ItemArray[0].ToString();
            tr1.Cells.Add(tc1);
            tc1 = new TableCell();
            tc1.Text = dr.ItemArray[1].ToString();
            tr1.Cells.Add(tc1);
            tc1 = new TableCell();
            tc1.Text = dr.ItemArray[2].ToString();
            tr1.Cells.Add(tc1);
            tc1 = new TableCell();
            tc1.Text = dr.ItemArray[3].ToString();
            tr1.Cells.Add(tc1);
            tc1 = new TableCell();
            tc1.Text = dr.ItemArray[4].ToString();
            tr1.Cells.Add(tc1);
            itemTable.Rows.Add(tr1);
            //itemTable.Rows.Add(trc[i]);
            sr_ar += double.Parse(itemTable.Rows[i].Cells[1].Text);
        }
        //StatCalculation();
        sr_ar = sr_ar / (itemTable.Rows.Count - 1);
        MeanValue.Text = sr_ar.ToString();
        for (int i = 1; i < itemTable.Rows.Count; i++)
        {
            disp += Math.Pow(double.Parse(itemTable.Rows[i].Cells[1].Text) - sr_ar, 2);
        }
        disp = disp / (itemTable.Rows.Count - 2);
        Disp.Text = disp.ToString();
        sq_quad = Math.Sqrt(disp);
        StDev.Text = sq_quad.ToString();
    }
    /*protected void StatCalculation()
    {
        double sr_ar = 0.000, disp = 0.000, sq_quad;
        
        for (int i = 1; i < itemTable.Rows.Count; i++)
        {
            sr_ar += double.Parse(itemTable.Rows[i].Cells[1].Text);
        }
        sr_ar = sr_ar / (itemTable.Rows.Count - 1);
        MeanValue.Text = sr_ar.ToString();
        for (int i = 1; i < itemTable.Rows.Count; i++)
        {
            disp += Math.Pow(double.Parse(itemTable.Rows[i].Cells[1].Text) - sr_ar, 2);
        }
        disp = disp / (itemTable.Rows.Count - 2);
        Disp.Text = disp.ToString();
        sq_quad = Math.Sqrt(disp);
        StDev.Text = sq_quad.ToString();
    }*/
    protected DataTable MakeDataTableForSaving()
    {
        DataTable dt = new DataTable("Answers");

        DataColumn NumColumn = new DataColumn();
        //NumColumn.DataType = System.Type.GetType("System.Int32");
        NumColumn.DataType = System.Type.GetType("System.String");
        NumColumn.ColumnName = "Number";
        dt.Columns.Add(NumColumn);

        DataColumn ResballColumn = new DataColumn();
        //ResballColumn.DataType = System.Type.GetType("System.Double");
        ResballColumn.DataType = System.Type.GetType("System.String");
        ResballColumn.ColumnName = "Result_balls";
        dt.Columns.Add(ResballColumn);

        DataColumn BallColumn = new DataColumn();
        //BallColumn.DataType = System.Type.GetType("System.Int32");
        BallColumn.DataType = System.Type.GetType("System.String");
        BallColumn.ColumnName = "Max_result";
        dt.Columns.Add(BallColumn);

        DataColumn PansColumn = new DataColumn();
        PansColumn.DataType = System.Type.GetType("System.String");
        PansColumn.ColumnName = "Person's answer";
        dt.Columns.Add(PansColumn);

        DataColumn AnsColumn = new DataColumn();
        AnsColumn.DataType = System.Type.GetType("System.String");
        AnsColumn.ColumnName = "Right answer";
        dt.Columns.Add(AnsColumn);
        
        return dt;
    }
}