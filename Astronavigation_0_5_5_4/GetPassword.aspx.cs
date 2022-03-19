using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetPassword : System.Web.UI.Page
{
    //string postpage;
    bool req = true;
    SqlConnection sc = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (req == true)
        {
            req = false;
            //postpage = Request.QueryString["from"];
        }
    }
    bool IsValidLogin(string strIn)
    {
        bool res = false;
        string log;
        try
        {
            //Looking for login in Users
            sc.Open();
            using (SqlCommand command = new SqlCommand("FindLogin", sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@login";
                param.DbType = DbType.String;
                param.Value = strIn;
                command.Parameters.Add(param);

                param = new SqlParameter("@i", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();

                var log2 = command.Parameters["@i"].Value;
                log = log2.ToString();
                if (log == "1")
                {
                    res = true;
                }
            }
            sc.Close();
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
        return res;
    }
    bool IsValidPassword(string str)
    {
        return Regex.IsMatch(str, @"([0-9 A-Z a-z]{8})$");
    }
    protected void button1_Click(object sender, EventArgs e)
    {
        if (!IsValidLogin(textBox1.Text))
        {
            textBox1.Text = "Enter a valid login.";
            textBox1.BackColor = System.Drawing.Color.DarkRed;
        }
        else
        {
            textBox1.Enabled = false;
            textBox1.BackColor = System.Drawing.Color.DarkGreen;
            if (textBox2.Text == textBox3.Text)
            {
                if (!IsValidPassword(textBox2.Text))
                {
                    label1.Visible = true;
                    textBox2.BackColor = System.Drawing.Color.DarkRed;
                }
                else
                {
                    label1.Visible = false;
                    textBox1.BackColor = System.Drawing.Color.White;
                    textBox2.BackColor = System.Drawing.Color.White;
                    try
                    {
                        ChangePassword();
                    }
                    catch (Exception)
                    {
                        //Response.Redirect("Default.aspx");
                    }
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
    private void ChangePassword()
    {
        sc.Open();
        using (SqlCommand command = new SqlCommand("UpdatePassword", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@log";
            param.DbType = DbType.String;
            param.Value = textBox1.Text;
            command.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@pass";
            param.DbType = DbType.String;
            param.Value = textBox2.Text;
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }
        sc.Close();
    }
    protected void textBox1_TextChanged(object sender, EventArgs e)
    {
        textBox1.Enabled = true;
        textBox1.BackColor = System.Drawing.Color.White;
    }
    protected void textBox2_TextChanged(object sender, EventArgs e)
    {
        textBox2.BackColor = System.Drawing.Color.White;
    }
    protected void textBox3_TextChanged(object sender, EventArgs e)
    {
        textBox3.BackColor = System.Drawing.Color.White;
    }
}