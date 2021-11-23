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

public partial class _Default : System.Web.UI.Page
{
    SqlConnection sc = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
    protected void Page_Load(object sender, EventArgs e)
    {
        //PopulateTreeView();
        try
        {
            PopulateTreeView();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void PopulateTreeView()
    {
        string proc = "ShowFirstLevel";
        TreeNode rootNode;
        rootNode = new TreeNode("Астронавигация");
        //Считать разделы из базы данных
        sc.Open();
        PageCount.SetCount(SetBorder("PagesCount1"));
        TreeView1.ImageSet = TreeViewImageSet.BulletedList4;
        TreeView1.Nodes.Add(rootNode);
        ShowSections(proc, rootNode, 1);
        sc.Close();
    }
    protected void ShowSections(string proc, TreeNode root, int lev)
    {
        int num, lev1 = lev + 1;
        string[] subsec;
        TreeNode aNode;
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            if (proc == "ShowSections")
            {
                param.ParameterName = "@id_sec";
                param.Value = root.Text.Split(' ')[0];
                param.SqlDbType = SqlDbType.Int;
                command.Parameters.Add(param);
                param = new SqlParameter();
                param.ParameterName = "@lev";
                param.Value = lev;
                param.SqlDbType = SqlDbType.Int;
                command.Parameters.Add(param);
            }
            else
            {
                proc = "ShowSections";
            }
            
            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                aNode = new TreeNode(sdr["id"].ToString() + " " + sdr["name"].ToString());
                num = int.Parse(sdr["id"].ToString());
                subsec = GetSections(proc, num, lev1);
                if(subsec.Length != 0)
                {
                    //Считать разделы из базы данных
                    ShowSections(proc, aNode, lev1);
                }
                else
                {
                    //Считать список уроков
                    //proc = "ShowLessons";
                    ShowLessons("ShowLessons", aNode);
                }
                root.ChildNodes.Add(aNode);
            }
            sdr.Close();
        }
    }
    protected string[] GetSections(string proc, int id, int lev)
    {
        List<string> ans = new List<string>();
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_sec";
            param.Value = id;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            param = new SqlParameter();
            param.ParameterName = "@lev";
            param.Value = lev;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);
            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                ans.Add(sdr["id"].ToString() + sdr["name"].ToString());
            }
            sdr.Close();
            return ans.ToArray();
        }
    }
    protected string[] GetFirstSubSection(TreeNode root)
    {
        int num;
        string[] subsec;
        TreeNode aNode;
        aNode = root.ChildNodes[0];
        if (aNode.ChildNodes.Count != 0)
        {
            subsec = GetFirstSubSection(aNode);
            return subsec;
        }
        else
        {
            //Считать список страниц урока
            num = int.Parse(aNode.Text.Split(' ')[0]);
            subsec = GetLessonPages("GetLessonPage", num);
            return subsec;
        }
    }
    protected void ShowLessons(string proc, TreeNode root)
    {
        TreeNode aNode;
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_sec";
            param.Value = root.Text.Split(' ')[0];
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                aNode = new TreeNode(sdr["id"].ToString() + " " + sdr["name"].ToString());
                root.ChildNodes.Add(aNode);
            }
            sdr.Close();
        }
    }
    protected string[] GetLessonPages(string proc, int id)
    {
        List<string> ans = new List<string>();
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            //Добавление параметров
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id_lesson";
            param.Value = id;
            param.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(param);

            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read() == true)
            {
                ans.Add(sdr["id"].ToString() + " " + sdr["name"].ToString());
            }
            sdr.Close();
            return ans.ToArray();
        }
    }
    protected int SetBorder(string proc)
    {
        int count;
        using (SqlCommand command = new SqlCommand(proc, sc) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataReader sdr = command.ExecuteReader();
            sdr.Read();
            count = int.Parse(sdr[0].ToString());
            sdr.Close();
            return count;
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        int num;
        //string les;
        //string[] subsec;
        //SqlConnection sc1 = new SqlConnection("Data Source = LENOVO-PC; Initial Catalog = Edaibd; Integrated Security = SSPI; MultipleActiveResultSets = True");
        sc.Open();
        if(TreeView1.SelectedNode.ChildNodes.Count == 0)
        {
            TreeView1.SelectedNode.SelectAction = TreeNodeSelectAction.None;
            using (SqlCommand command = new SqlCommand("GetLessonPage", sc) { CommandType = CommandType.StoredProcedure })
            {
                //Добавление параметров
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id_lesson";
                param.Value = TreeView1.SelectedNode.Text.Split(' ')[0];
                param.SqlDbType = SqlDbType.Int;
                command.Parameters.Add(param);
                
                SqlDataReader sdr = command.ExecuteReader();
                if (sdr.Read() == true)
                {
                    num = int.Parse(sdr["id"].ToString());
                    //var t1 = HttpUtility.HtmlEncode(num);
                    TreeView1.SelectedNode.NavigateUrl = "LessonPage.aspx?number=" + num.ToString();
                    Response.Redirect(TreeView1.SelectedNode.NavigateUrl);
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("Запрашиваемая страница временно не доступна. Простите за неудобства.");
                }
                sdr.Close();
                sc.Close();
            }
        }
        else
        {
            TreeView1.SelectedNode.SelectAction = TreeNodeSelectAction.Expand;
            /*TreeNode aNode = TreeView1.SelectedNode.ChildNodes[0];
            if (aNode.ChildNodes.Count != 0)
            {
                //Считать первый дочерний узел рекурсивно
                subsec = GetFirstSubSection(aNode);
                num = int.Parse(subsec[0].Split(' ')[0]);
                //var t1 = HttpUtility.HtmlEncode(num);
                TreeView1.SelectedNode.NavigateUrl = "LessonPage.aspx?number=" + num.ToString();
            }
            else
            {
                //Считать список страниц урока
                num = int.Parse(aNode.Text.Split(' ')[0]);
                subsec = GetLessonPages("GetLessonPage", num);
                num = int.Parse(subsec[0].Split(' ')[0]);
                sc.Close();
                //var t1 = HttpUtility.HtmlEncode(num);
                TreeView1.SelectedNode.NavigateUrl = "LessonPage.aspx?number=" + num.ToString();
            }*/
        }
    }
}