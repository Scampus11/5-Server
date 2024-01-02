using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SMS.Class;
using System.Web.Configuration;
using System.IO;

namespace SMS
{
    public partial class SMS_Change_password : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    Session["Sider"] = "Change PassWord";
                }
                if (Session["UserName"].ToString().ToUpper() == "SUPERADMIN")
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void Connection()
        {
            try
            {
                string filePath1 = Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
                StreamReader sr = new StreamReader(filePath1);
                string line = sr.ReadToEnd();
                con = new SqlConnection(line);
                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
                Response.Redirect("SMS_SQL_Connection.aspx");
            }

        }
        protected void lnksave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                lblerrorconfirmpwd.Visible = false;
                lblerrorpassword.Visible = false;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update officer_Master set password='" + EncryptionDecryption.GetEncrypt(txtNewpwd.Text.Trim())
                    + "' where Staff_Id='" + Request.QueryString["Id"].ToString().Trim()
                    + "'Update Users set password='" + EncryptionDecryption.GetEncrypt(txtNewpwd.Text.Trim()) 
                    + "' where StaffId='" + Request.QueryString["Id"].ToString().Trim() + "'";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("ChangePWDmsg.aspx");
            }
        }
        protected bool Validation()
        {
            if (txtpassword.Text == "")
            {
                lblcurrentpwd.Visible = true;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                lblerrorconfirmpwd.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrorsamepwd.Visible = false;
                return false;
            }
            if (txtNewpwd.Text == "")
            {
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = true;
                lblconfirmpwderror.Visible = false;
                lblerrorconfirmpwd.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrorsamepwd.Visible = false;
                return false;
            }
            if (txtconfirmpwd.Text == "")
            {
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = true;
                lblerrorconfirmpwd.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrorsamepwd.Visible = false;
                return false;
            }
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from officer_Master where Staff_Id= '" + Request.QueryString["Id"].ToString().Trim() + "'";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (txtpassword.Text.Trim() != EncryptionDecryption.GetDecrypt(ds.Tables[0].Rows[0]["password"].ToString()))
                {
                    lblerrorpassword.Visible = true;
                    lblerrorconfirmpwd.Visible = false;
                    lblcurrentpwd.Visible = false;
                    lblnewpwd.Visible = false;
                    lblconfirmpwderror.Visible = false;
                    lblerrorsamepwd.Visible = false;
                    txtNewpwd.Focus();
                    return false;
                }
                else
                {
                    lblerrorpassword.Visible = false;
                    lblcurrentpwd.Visible = false;
                    lblnewpwd.Visible = false;
                    lblconfirmpwderror.Visible = false;
                    lblerrorconfirmpwd.Visible = false;
                    lblerrorsamepwd.Visible = false;
                    txtNewpwd.Focus();
                }
            }
            con.Close();
            if (txtNewpwd.Text.Trim() != txtconfirmpwd.Text.Trim())
            {
                lblerrorconfirmpwd.Visible = true;
                lblerrorpassword.Visible = false;
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                lblerrorsamepwd.Visible = false;
                txtconfirmpwd.Focus();
                return false;
            }
            else
            {
                lblerrorconfirmpwd.Visible = false;
                lblerrorpassword.Visible = false;
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                lblerrorsamepwd.Visible = false;
                txtconfirmpwd.Focus();
            }
            if (txtpassword.Text.Trim() == txtNewpwd.Text.Trim())
            {
                lblerrorsamepwd.Visible = true;
                lblerrorpassword.Visible = false;
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                txtNewpwd.Focus();
                txtNewpwd.Text = "";
                txtconfirmpwd.Text = "";
                return false;
            }
            else
            {
                lblerrorsamepwd.Visible = false;
                lblerrorpassword.Visible = false;
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                txtNewpwd.Focus();
            }
            return true;
        }
        protected void txtpassword_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from officer_Master where Staff_Id= '" + Request.QueryString["Id"].ToString().Trim() + "'";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (txtpassword.Text.Trim() != ds.Tables[0].Rows[0]["password"].ToString().Trim())
                {
                    lblerrorpassword.Visible = true;
                    lblerrorpassword.Visible = false;
                    lblcurrentpwd.Visible = false;
                    lblnewpwd.Visible = false;
                    lblconfirmpwderror.Visible = false;
                    txtNewpwd.Focus();
                }
                else {
                    lblerrorsamepwd.Visible = false;
                    lblerrorpassword.Visible = false;
                    lblcurrentpwd.Visible = false;
                    lblnewpwd.Visible = false;
                    lblconfirmpwderror.Visible = false;
                    txtNewpwd.Focus();
                }
            }
            con.Close();
        }

        protected void txtconfirmpwd_TextChanged(object sender, EventArgs e)
        {
            if (txtNewpwd.Text.Trim() != txtconfirmpwd.Text.Trim())
            {
                lblerrorconfirmpwd.Visible = true;
                lblerrorsamepwd.Visible = false;
                lblerrorpassword.Visible = false;
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                txtconfirmpwd.Focus();
            }
            else
            {
                lblerrorconfirmpwd.Visible = false;
                lblerrorsamepwd.Visible = false;
                lblerrorpassword.Visible = false;
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                txtconfirmpwd.Focus();
            }
        }

        protected void txtNewpwd_TextChanged(object sender, EventArgs e)
        {
            if (txtpassword.Text.Trim() == txtNewpwd.Text.Trim())
            {
                lblerrorsamepwd.Visible = true;
                lblerrorpassword.Visible = false;
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                txtNewpwd.Focus();
                txtNewpwd.Text = "";
                txtconfirmpwd.Text = "";
            }
            else
            {
                lblerrorsamepwd.Visible = false;
                lblerrorpassword.Visible = false;
                lblcurrentpwd.Visible = false;
                lblnewpwd.Visible = false;
                lblconfirmpwderror.Visible = false;
                txtNewpwd.Focus();
            }
        }
    }
}