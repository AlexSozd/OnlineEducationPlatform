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

public partial class Authorization : System.Web.UI.Page
{
    string postpage;
    bool req = true;
    SqlConnection sc = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (req == true)
        {
            req = false;
            postpage = Request.QueryString["from"];
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
            using (SqlCommand command = new SqlCommand("FindLogin1", sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@login";
                param.Value = strIn;
                param.DbType = DbType.String;
                param.Direction = ParameterDirection.Input;
                command.Parameters.Add(param);

                SqlDataReader sdr = command.ExecuteReader();
                sdr.Read();

                var log2 = int.Parse(sdr[0].ToString());
                sdr.Close();
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
            
        }
        return res;
    }
    bool Authentication(string log1, string par1)
    {
        bool res = false;
        string log;
        try
        {
            //Looking for login and password in Users
            sc.Open();
            using (SqlCommand command = new SqlCommand("FindLoginAndPassword1", sc) { CommandType = CommandType.StoredProcedure })
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@login";
                param.Value = log1;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@pass";
                param.Value = par1;
                param.DbType = DbType.String;
                command.Parameters.Add(param);

                SqlDataReader sdr = command.ExecuteReader();
                sdr.Read();

                var log2 = int.Parse(sdr[0].ToString());
                sdr.Close();
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
            
        }
        return res;
    }
    protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
    {
        if (!IsValidLogin(Login1.UserName))
        {
            Login1.InstructionText = "Enter a valid login.";
            e.Cancel = true;
        }
        else
        {
            Login1.InstructionText = string.Empty;
        }
    }
    protected void Login1_LoginError(object sender, EventArgs e)
    {
        Login1.HelpPageText = "Help with logging in...";
        Login1.PasswordRecoveryText = "Forgot your password?";
        if (ViewState["LoginErrors"] == null)
        {
            ViewState["LoginErrors"] = 0;
        }
        int ErrorCount = (int)ViewState["LoginErrors"] + 1;
        ViewState["LoginErrors"] = ErrorCount;
        if ((ErrorCount > 3) && (Login1.PasswordRecoveryUrl != string.Empty))
        {
            Response.Redirect(Login1.PasswordRecoveryUrl);
        }
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool Authenticated = false;
        Authenticated = Authentication(Login1.UserName, Login1.Password);
        e.Authenticated = Authenticated;
        if (e.Authenticated == true)
        {
            /*try
            {*/
                sc.Open();
                using (SqlCommand command = new SqlCommand("FindID1", sc) { CommandType = CommandType.StoredProcedure })
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@login";
                    param.Value = Login1.UserName;
                    param.DbType = DbType.String;
                    command.Parameters.Add(param);

                    SqlDataReader sdr = command.ExecuteReader();
                    sdr.Read();
                    
                    var log2 = int.Parse(sdr[0].ToString());
                    sdr.Close();
                    int id_del = int.Parse(log2.ToString());
                    if(IsAvailable(id_del) == "1")
                    {
                        //Response.Redirect(postpage + "?id=" + id_del.ToString());
                        //Response.Redirect(postpage + "&UserId=" + id_del.ToString());
                        if (Page.Request.QueryString["from"].Contains('?'))
                        {
                            Response.Redirect(postpage + "&UserId=" + id_del.ToString());
                        }
                        else
                        {
                            Response.Redirect(postpage + "?UserId=" + id_del.ToString());
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                sc.Close();
            /*}
            catch (Exception)
            {
                
            }*/
        }
    }
    protected string IsAvailable(int us_id)
    {
        string res = "0";
        string[] data = postpage.Split('?');
        if(data[0] == "TestForm.aspx")
        {
            //Check Test Access
            res = TestFormAccess(us_id);
        }
        else if(data[0] == "Statistics.aspx")
        {
            //Check Statistics Access
            res = StatFormAccess(us_id);
        }
        else
        {
            //Check Content Access
            res = ContentChangeAccess(us_id);
        }
        return res;
    }
    protected string TestFormAccess(int us_id)
    {
        int ans;
        using (SqlCommand command = new SqlCommand("CheckTestAccess", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_us";
            param.Value = us_id;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            sdr.Read();

            ans = int.Parse(sdr[0].ToString());
            
            sdr.Close();
        }
        return ans.ToString();
    }
    protected string StatFormAccess(int us_id)
    {
        int ans;
        using (SqlCommand command = new SqlCommand("CheckStatAccess", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_us";
            param.Value = us_id;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            sdr.Read();

            ans = int.Parse(sdr[0].ToString());

            sdr.Close();
        }
        return ans.ToString();
    }
    protected string ContentChangeAccess(int us_id)
    {
        int ans;
        using (SqlCommand command = new SqlCommand("CheckContentAccess", sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_us";
            param.Value = us_id;
            param.DbType = DbType.Int32;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            sdr.Read();

            ans = int.Parse(sdr[0].ToString());

            sdr.Close();
        }
        return ans.ToString();
    }
}