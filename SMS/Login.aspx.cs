using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using SMS.Class;
using System.Web.Configuration;

namespace SMS
{
    public partial class Login : System.Web.UI.Page
    {
        LogFile logFile = new LogFile();
        String line = "";
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!Page.IsPostBack)
            {
                try
                {
                   
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            con.Open();
            if (Validation())
            {
                SqlCommand cmd = new SqlCommand("SP_User_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", EncryptionDecryption.GetEncrypt(txtPassword.Text.Trim()));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                if (dt.Rows.Count > 0)
                {
                    Session["UserID"] = dt.Rows[0]["Full_Name"].ToString();
                    Session["Images"] = dt.Rows[0]["Emp_photo"].ToString();
                    Session["Id"] = dt.Rows[0]["Id"].ToString();
                    Session["Email_Id"] = dt.Rows[0]["Email_Id"].ToString();
                    Session["Role_Id"] = dt.Rows[0]["Role_Id"].ToString();
                    Session["Staff_Id"] = dt.Rows[0]["Staff_Id"].ToString();
                    if (dt.Rows[0]["cardstatus"].ToString() != "5")
                    {
                        Session["UserName"] = txtusername.Text;
                        Session["Pwd"] = txtPassword.Text;
                        //Session["UserName1"] = txtusername.Text;
                        //Session["Pwd1"] = EncryptionDecryption.GetEncrypt(txtPassword.Text.Trim());

                        SqlCommand cmd1 = new SqlCommand("SP_Get_Page_Right_Active", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Flag", "PageActive");
                        cmd1.Parameters.AddWithValue("@Staff_Id", txtusername.Text);
                        cmd1.Connection = con;

                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable ds1 = new DataTable();
                        da1.Fill(ds1);
                        if (ds1.Rows.Count > 0)
                        {
                            //Response.Redirect("Student_Master.aspx");
                            //Response.Redirect("~/License/License.aspx");
                            Response.Redirect("MainDashboard.aspx");

                        }
                        else
                        {
                            Session["notaccess"] = "1";
                            //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Sorry!!! You do not have access to this portal,kindly contact to administrator.');", true);
                            Label1.Visible = true;
                            lblerror.Visible = false;
                            // Response.Redirect("Login.aspx");
                            //Response.Redirect("Student_Master.aspx");
                        }
                    }
                    else
                    {
                        Label1.Visible = true;
                        lblerror.Visible = false;
                    }
                }
                else
                {
                    // ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Username and Password')</script>");
                    lblerror.Visible = true;
                    Label1.Visible = false;
                }
            }
        }

        protected bool Validation()
        {
            if (txtPassword.Text == "" && txtusername.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Username and Password')</script>");
                Label2.Visible = true;
                Label1.Visible = false;
                lblerror.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrorusername.Visible = false;
                return false;
            }
            else if (txtusername.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Username ')</script>");
                Label2.Visible = false;
                Label1.Visible = false;
                lblerror.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrorusername.Visible = true;
                return false;
            }
            else if (txtPassword.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Password')</script>");
                Label2.Visible = false;
                Label1.Visible = false;
                lblerror.Visible = false;
                lblerrorpassword.Visible = true;
                lblerrorusername.Visible = false;
                return false;
            }
            return true;
        }

        //Add New Staff Logic
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Visitor_Master.aspx?Insert=Insert Department");
        }
        protected void Connection()
        
{
            try
            {
                string filePath1 = Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
                StreamReader sr = new StreamReader(filePath1);
                
                line = sr.ReadToEnd();
                sr.Close();
                con = new SqlConnection(line);
                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                //con.Close();
                //string root = Server.MapPath(WebConfigurationManager.AppSettings["root"].ToString());
                //if (Directory.Exists(root))
                //{
                //    DeleteDirectory(root);
                //}
                LogFile.LogError(ex);
                Response.Redirect("SMS_SQL_Connection.aspx");
            }

        }
        private void DeleteDirectory(string path)
        {
            // Delete all files from the Directory  
            foreach (string filename in Directory.GetFiles(path))
            {
                File.Delete(filename);
            }
            // Check all child Directories and Delete files  
            foreach (string subfolder in Directory.GetDirectories(path))
            {
                DeleteDirectory(subfolder);
            }
            Directory.Delete(path);
            //   Deletelbl.Text = "Directory Deleted successfully";
        }
    }
}