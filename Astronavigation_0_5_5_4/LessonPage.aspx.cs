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

public partial class LessonPage : System.Web.UI.Page
{
    int pagenum;
    bool req = true;
    List<HyperLink> hl = new List<HyperLink>();
    SqlConnection sc = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (req == true)
        {
            req = false;
            hl.Clear();
            pagenum = int.Parse(Request.QueryString["number"]);
            //ShowLesson("", pagenum);
            if (pagenum > 1)
            {
                button1.PostBackUrl = "LessonPage.aspx?number=" + (pagenum - 1).ToString();
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            if (pagenum < PageCount.GetCount())
            {
                button2.PostBackUrl = "LessonPage.aspx?number=" + (pagenum + 1).ToString();
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
            //button2.PostBackUrl = "LessonPage.aspx?number=" + (pagenum + 1).ToString();
            ShowPage("GetPage", pagenum);
        }
        /*try
        {
            if(req == true)
            {
                req = false;
                hl.Clear();
                pagenum = int.Parse(Request.QueryString["number"]);
                //ShowLesson("", pagenum);
                if(pagenum > 1)
                {
                    button1.PostBackUrl = "LessonPage.aspx?number=" + (pagenum - 1).ToString();
                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                }
                if (pagenum < PageCount.GetCount())
                {
                    button2.PostBackUrl = "LessonPage.aspx?number=" + (pagenum + 1).ToString();
                    button2.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                }
                //button2.PostBackUrl = "LessonPage.aspx?number=" + (pagenum + 1).ToString();
                ShowPage("GetPage", pagenum);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }*/
    }
    /*protected void ShowLesson(string proc, int lesnum)
    {
        int num;
        sc.Open();
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = lesnum;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                num = int.Parse(sdr["id"].ToString());
                ShowPage("", num);
            }
            else
            {
                Response.Write("Такого урока у нас нет. Просим извинения за неудобства!");
                //Console.WriteLine("Строки с искомым id не существует");
            }
        }
        sc.Close();
    }*/
    protected void ShowPage(string proc, int num)
    {
        string path;
        sc.Open();
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_page";
            param.Value = num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                label1.Text = ShowLessonName("GetPageLesson").ToString();
                label2.Text = sdr["name"].ToString();
                path = sdr["fname"].ToString();
                PageText.Src = path;
                ShowPageRefs("SetPageRefs", num);
            }
            else
            {
                Response.Write("Такой страницы у нас нет. Просим извинения за неудобства!");
            }
            sdr.Close();
        }
        sc.Close();
    }
    protected string ShowLessonName(string proc)
    {
        string name;
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
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
                name = sdr["name"].ToString();
                sdr.Close();
                return name;
            }
            else
            {
                sdr.Close();
                return "Астронавигация";
            }
        }
    }
    protected void ShowPageRefs(string proc, int num)
    {
        int n;
        HyperLink hl1;
        Label lb = new Label();
        lb.Text = "Дополнительные материалы:";
        panel1.Controls.Add(lb);
        panel1.Controls.Add(new LiteralControl("<br>"));
        panel1.Controls.Add(new LiteralControl("<br>"));
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_page";
            param.Value = num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                hl1 = new HyperLink();
                hl1.Text = sdr["name"].ToString() + "<br>";
                //hl1.Text = sdr["name"].ToString();
                n = int.Parse(sdr["id"].ToString());
                hl1.NavigateUrl = "Glossary.aspx?number=" + n.ToString();
                panel1.Controls.Add(hl1);
                //bl1.Items.Add(hl1.Text);
                hl.Add(hl1);
            }
        }
        
        using (SqlCommand command = new SqlCommand("FindTestBlock", sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_page";
            param.Value = num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                panel1.Controls.Add(new LiteralControl("<br>"));
                panel1.Controls.Add(new LiteralControl("<br>"));
                lb = new Label();
                lb.Text = "Контрольные задания:";
                panel1.Controls.Add(lb);
                panel1.Controls.Add(new LiteralControl("<br>"));
                panel1.Controls.Add(new LiteralControl("<br>"));
                
                hl1 = new HyperLink();
                //hl1.Text = "<br><br/><br>" + sdr["name"].ToString() + " (тест)<br/>";
                hl1.Text = sdr["name"].ToString() + " (тест)<br/>";
                //num = int.Parse(sdr["id"].ToString());
                hl1.NavigateUrl = "TestForm.aspx?number=" + num.ToString();
                panel1.Controls.Add(hl1);
                hl.Add(hl1);
            }
        }
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
}