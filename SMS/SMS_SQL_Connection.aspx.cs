using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Class;
using Microsoft.SqlServer.Management.Smo;
using System.Data;
using System.Data.Sql;
using System.Web.Configuration;
using System.IO;
//using Microsoft.SqlServer.Management.Smo.Wmi;
namespace SMS
{
    
    public partial class SMS_SQL_Connection : System.Web.UI.Page
    {
        string root = System.Web.Hosting.HostingEnvironment.MapPath(WebConfigurationManager.AppSettings["root"].ToString());
        string filePath1 = System.Web.Hosting.HostingEnvironment.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
        SMS.Class.LogFile logFile = new SMS.Class.LogFile();
        String line = "";
        string text2write = "";
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Canteen_Flag"] = null;
                Session["Session_Id"] = null;
                Session["CanteenName"] = null;
                Session["CanteenCount"] = null;
                Session["Sider"] = "DynamicDB";
                Authentication();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblsql.Visible = false;
                lbltestconnection.Visible = false;
                BtntestConnection_Click(sender, e);
                if (lbltestconnection.Visible == true)
                {
                   
                    // string root = System.Web.HttpContext.Current.Server.MapPath(@"~/Connection");
                    // string filePath1 = System.Web.HttpContext.Current.Server.MapPath(@"~/Connection/ConnectionString.txt");
                    if (!(Directory.Exists(root)))
                    {
                        Directory.CreateDirectory(root);
                    }
                    File.WriteAllText(Path.Combine(root, "ConnectionString.txt"), text2write);
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath1);
                    writer.Write(text2write);
                    writer.Close();
                    Response.Redirect("Login.aspx");
                }
            }
            catch (Exception ex)
            {
                SMS.Class.LogFile.LogError(ex);
                lblsql.Visible = true;
                lblsql.Text = ex.Message.ToString();
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
        protected bool Validation()
        {
            if (txtDBNAme.Text == "" && txtServerName.Text == "" && txtPassword.Text == "" && txtusername.Text == "")
            {

                if (ddlDBName.Visible == false && ddlServerName.Visible == false)
                {
                    lblerrorpassword.Visible = true;
                    lblerrorusername.Visible = true;
                    lblerrordbname.Visible = true;
                    lblerrorServername.Visible = true;
                    return false;
                }
                else if (ddlServerName.Visible == false)
                {
                    lblerrorpassword.Visible = false;
                    lblerrorServername.Visible = true;
                    lblerrordbname.Visible = false;
                    lblerrorusername.Visible = false;
                    return false;
                }
                else if (ddlDBName.Visible == false)
                {
                    lblerrorpassword.Visible = false;
                    lblerrordbname.Visible = true;
                    lblerrorServername.Visible = false;
                    lblerrorusername.Visible = false;
                    return false;
                }

            }

            else if (txtServerName.Text == "")
            {
                if (ddlServerName.Visible == false)
                {
                    lblerrorpassword.Visible = false;
                    lblerrorServername.Visible = true;
                    lblerrordbname.Visible = false;
                    lblerrorusername.Visible = false;
                    return false;
                }
            }
            else if (txtDBNAme.Text == "")
            {
                if (ddlDBName.Visible == false)
                {
                    lblerrorpassword.Visible = false;
                    lblerrordbname.Visible = true;
                    lblerrorServername.Visible = false;
                    lblerrorusername.Visible = false;
                    return false;
                }
            }
            else
            {
                if (ddlAuthentication.SelectedValue == "2")
                {
                    if (txtusername.Text == "")
                    {
                        lblerrorpassword.Visible = false;
                        lblerrorusername.Visible = true;
                        lblerrordbname.Visible = false;
                        lblerrorServername.Visible = false;
                        return false;
                    }
                    else if (txtPassword.Text == "")
                    {
                        lblerrorpassword.Visible = true;
                        lblerrorusername.Visible = false;
                        lblerrordbname.Visible = false;
                        lblerrorServername.Visible = false;
                        return false;
                    }
                }
            }
            return true;
        }

        protected void lnkrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                lblerrorpassword.Visible = false;
                lblerrordbname.Visible = true;
                lblerrorServername.Visible = false;
                lblerrorusername.Visible = false;
                txtServerName.Visible = false;
                txtDBNAme.Visible = true;
                ddlDBName.Visible = false;
                ddlServerName.Visible = true;
                lnkrefresh.Visible = false;
                lbltestconnection.Visible = false;
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                DataTable table = instance.GetDataSources();
                string ServerName = Environment.MachineName;
                foreach (DataRow row in table.Rows)
                {
                    ddlServerName.Items.Add(ServerName + "\\" + row["InstanceName"].ToString());
                    //txtServerName.Text = ServerName + "\\" + row["InstanceName"].ToString();
                }
                //string serverName = ddlServerName.SelectedValue;
                //Server server = new Server(serverName);

                //foreach (Database database in server.Databases)
                //{
                //    ddlDBName.Items.Add(database.Name);
                //}
            }
            catch (Exception ex)
            {
                SMS.Class.LogFile.LogError(ex);
                lblsql.Visible = true;
                lblsql.Text = ex.Message.ToString();
            }

        }
        protected void Authentication()
        {
            if (ddlAuthentication.SelectedValue == "2")
            {
                divUser.Visible = true;
                divPassword.Visible = true;
            }
            else if (ddlAuthentication.SelectedValue == "2")
            {
                divUser.Visible = false;
                divPassword.Visible = false;
            }
        }
        protected void ddlServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblerrorpassword.Visible = false;
                lblerrordbname.Visible = false;
                lblerrorServername.Visible = false;
                lblerrorusername.Visible = false;
                lblsql.Visible = false;
                lbltestconnection.Visible = false;
                string serverName = ddlServerName.SelectedValue;
                ddlDBName.Items.Clear();
                Server server = new Server(serverName);

                foreach (Database database in server.Databases)
                {
                    ddlDBName.Items.Add(database.Name);
                }

            }
            catch (Exception ex)
            {
                SMS.Class.LogFile.LogError(ex);
                lblsql.Visible = true;
                lblsql.Text = ex.Message.ToString();
            }
        }

        protected void ddlAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbltestconnection.Visible = false;
            Authentication();
        }

        protected void BtntestConnection_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {

                    lbltestconnection.Visible = false;
                    if (ddlAuthentication.SelectedValue == "1")
                    {
                        if (txtServerName.Visible == true && txtDBNAme.Visible == true)
                        {
                            text2write = "Data Source=" + txtServerName.Text.Trim() + ";Integrated Security=true;Initial Catalog=" + txtDBNAme.Text.Trim() + ";";
                        }
                        else if (txtServerName.Visible == true && txtDBNAme.Visible == false)
                        {
                            text2write = "Data Source=" + txtServerName.Text.Trim() + ";Integrated Security=true;Initial Catalog=" + ddlDBName.SelectedValue.Trim() + ";";
                        }
                        else if (txtServerName.Visible == false && txtDBNAme.Visible == true)
                        {
                            text2write = "Data Source=" + ddlServerName.SelectedValue.Trim() + ";Integrated Security=true;Initial Catalog=" + txtDBNAme.Text.Trim() + ";";
                        }
                        else if (txtServerName.Visible == false && txtDBNAme.Visible == false)
                        {
                            text2write = "Data Source=" + ddlServerName.SelectedValue.Trim() + ";Integrated Security=true;" +
                                "Initial Catalog=" + ddlDBName.SelectedValue.Trim() + ";";
                        }
                    }
                    else if (ddlAuthentication.SelectedValue == "2")
                    {
                        if (txtServerName.Visible == true && txtDBNAme.Visible == true)
                        {
                            text2write = "Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=" + txtDBNAme.Text.Trim()
                                + ";uid=" + txtusername.Text.Trim() + ";password=" + txtPassword.Text.Trim()
                                + ";multipleactiveresultsets=True;application name=EntityFramework";
                        }
                        else if (txtServerName.Visible == true && txtDBNAme.Visible == false)
                        {
                            text2write = "Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=" + ddlDBName.SelectedValue.Trim()
                                + ";uid=" + txtusername.Text.Trim() + ";password=" + txtPassword.Text.Trim()
                                + ";multipleactiveresultsets=True;application name=EntityFramework";
                        }
                        else if (txtServerName.Visible == false && txtDBNAme.Visible == true)
                        {
                            text2write = "Data Source=" + ddlServerName.SelectedValue.Trim() + ";Initial Catalog=" + txtDBNAme.Text.Trim()
                                + ";uid=" + txtusername.Text.Trim() + ";password=" + txtPassword.Text.Trim()
                                + ";multipleactiveresultsets=True;application name=EntityFramework";
                        }
                        else if (txtServerName.Visible == false && txtDBNAme.Visible == false)
                        {
                            text2write = "Data Source=" + ddlServerName.SelectedValue.Trim() + ";Initial Catalog=" + ddlDBName.SelectedValue.Trim()
                                + ";uid=" + txtusername.Text.Trim() + ";password=" + txtPassword.Text.Trim()
                                + ";multipleactiveresultsets=True;application name=EntityFramework";
                        }
                    }

                    con = new SqlConnection(text2write);
                    con.Open();
                    con.Close();
                    lblerrorpassword.Visible = false;
                    lblerrordbname.Visible = false;
                    lblerrorServername.Visible = false;
                    lblerrorusername.Visible = false;
                    lblsql.Visible = false;
                    lbltestconnection.Visible = true;
                }
                catch (Exception ex)
                {
                    SMS.Class.LogFile.LogError(ex);
                    lblsql.Visible = true;
                    lblsql.Text = ex.Message.ToString();
                }
            }
        }

        protected void lnkDBname_Click(object sender, EventArgs e)
        {
            try
            {
                string serverName = "";
                lblerrorpassword.Visible = false;
                lblerrordbname.Visible = false;
                lblerrorServername.Visible = false;
                lblerrorusername.Visible = false;
                lblsql.Visible = false;
                lbltestconnection.Visible = false;
                if (txtServerName.Visible == true)
                {
                    txtServerName.Visible = true;
                    txtDBNAme.Visible = false;
                    ddlDBName.Visible = true;
                    ddlServerName.Visible = false;
                    lnkrefresh.Visible = false;
                    lnkDBname.Visible = false;
                    serverName = txtServerName.Text.Trim();
                }
                else
                {
                    txtServerName.Visible = false;
                    txtDBNAme.Visible = false;
                    ddlDBName.Visible = true;
                    ddlServerName.Visible = true;
                    lnkrefresh.Visible = false;
                    lnkDBname.Visible = false;
                    //SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                    //DataTable table = instance.GetDataSources();
                    //string ServerName = Environment.MachineName;
                    //foreach (DataRow row in table.Rows)
                    //{
                    //    ddlServerName.Items.Add(ServerName + "\\" + row["InstanceName"].ToString());
                    //    //txtServerName.Text = ServerName + "\\" + row["InstanceName"].ToString();
                    //}
                    serverName = ddlServerName.SelectedValue;
                }

                Server server = new Server(serverName);

                foreach (Database database in server.Databases)
                {
                    ddlDBName.Items.Add(database.Name);
                }
            }
            catch (Exception ex)
            {
                SMS.Class.LogFile.LogError(ex);
                lblsql.Visible = true;
                lblsql.Text = ex.Message.ToString();
            }
        }
    }


}