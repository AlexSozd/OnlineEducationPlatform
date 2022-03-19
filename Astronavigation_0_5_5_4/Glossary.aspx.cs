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

public partial class Glossary : System.Web.UI.Page
{
    int termnum;
    bool req = true;
    List<HyperLink> hl = new List<HyperLink>();
    SqlConnection sc = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (req == true)
        {
            req = false;
            termnum = int.Parse(Request.QueryString["number"]);
            ShowTerm("GetTerm", termnum);
        }
        /*try
        {
            if (req == true)
            {
                req = false;
                termnum = int.Parse(Request.QueryString["number"]);
                ShowTerm("GetTerm", termnum);
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }*/
    }
    protected void ShowTerm(string proc, int num)
    {
        string path;
        sc.Open();
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_term";
            param.Value = num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            SqlDataReader sdr = command.ExecuteReader();
            if (sdr.Read() == true)
            {
                label1.Text = sdr["name"].ToString();
                path = sdr["fname"].ToString();
                PageText.Src = path;
                ShowPageRefs("SetInRefs", num);
            }
            else
            {
                Response.Write("Такой страницы у нас нет. Просим извинения за неудобства!");
            }
            sdr.Close();
        }
        
        sc.Close();
    }
    protected void ShowPageRefs(string proc, int num)
    {
        Label lb = new Label();
        lb.Text = "Дополнительные материалы:";
        panel1.Controls.Add(lb);
        panel1.Controls.Add(new LiteralControl("<br>"));
        panel1.Controls.Add(new LiteralControl("<br>"));
        HyperLink hl1;
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_cur";
            param.Value = num;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                hl1 = new HyperLink();
                hl1.Text = sdr["name"].ToString() + "<br>";
                num = int.Parse(sdr["id"].ToString());
                hl1.NavigateUrl = "Glossary.aspx?number=" + num.ToString();
                panel1.Controls.Add(hl1);
                hl.Add(hl1);
            }
            sdr.Close();
        }
    }
}