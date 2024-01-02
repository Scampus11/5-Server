using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using SMS.Class;
using System.Web.Configuration;
namespace SMS
{
    public partial class SelfRegistrationSuccessfully : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!Page.IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("SP_GetVisitorRegNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                lblId.Text = ds.Rows[0]["RegNo"].ToString();
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
    }
}