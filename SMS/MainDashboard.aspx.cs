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
using System.Drawing;
using ClosedXML.Excel;
using SMS.Class;
using System.Web.Configuration;
namespace SMS
{
    public partial class MainDashboard : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        LogFile logFile = new LogFile();
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Sider"] = "Dashboard";
            if (!Page.IsPostBack)
            {
                try { 
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindCount();
                BindVisitorsDetails();
                BindVisitorsDetails2();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
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
        protected void BindCount()
        {
            try { 
            SqlCommand cmd = new SqlCommand("SP_Dashboard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "AllCOUNT");
            cmd.Parameters.AddWithValue("@FromDate","");
            cmd.Parameters.AddWithValue("@Todate", "");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            lblStudent.Text = ds.Tables[0].Rows[0]["Student"].ToString();
            lblStaff.Text = ds.Tables[1].Rows[0]["Staff"].ToString();
            lblVisitor.Text = ds.Tables[2].Rows[0]["Visitor"].ToString();

            lblStudentAccess.Text = ds.Tables[3].Rows[0]["Student"].ToString();
            lblStaffAccess.Text = ds.Tables[4].Rows[0]["Staff"].ToString();
            lblSession.Text = ds.Tables[5].Rows[0]["Session"].ToString();
            lblCanteen.Text = ds.Tables[6].Rows[0]["Canteen"].ToString();
            lblAG.Text = ds.Tables[7].Rows[0]["AG"].ToString();
            lblReader.Text = ds.Tables[8].Rows[0]["Reader"].ToString();
            con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindVisitorsDetails()
        {
            try { 
            SqlCommand cmd = new SqlCommand("SP_Dashboard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "Visitors");
            cmd.Parameters.AddWithValue("@FromDate", txtDate.Text);
            cmd.Parameters.AddWithValue("@Todate", txtToDate.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            gridEmployee.DataSource = ds;
            gridEmployee.DataBind();
            con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindVisitorsDetails2()
        {
            try { 
            SqlCommand cmd = new SqlCommand("SP_Dashboard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "Visitors2");
            cmd.Parameters.AddWithValue("@FromDate", txtDate.Text);
            cmd.Parameters.AddWithValue("@Todate", txtToDate.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            Grdvisitors2.DataSource = ds;
            Grdvisitors2.DataBind();
            con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkGo_Click(object sender, EventArgs e)
        {
            BindCount();
            BindVisitorsDetails();
            BindVisitorsDetails2();
        }
    }
}