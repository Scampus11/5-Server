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
    public partial class SMS_College_Master : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            Session["Sider"] = "College Master";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                try { 
                BindScampus();
                if (Request.QueryString["Id"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lnkupdate.Visible = true;
                    lnksave.Visible = false;
                    lnkAdd.Visible = false;
                    con.Open();
                    //txtStudentID.Text = Request.QueryString["Id"].ToString();
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "select * from tbl_College where Id= '" + Request.QueryString["Id"].ToString() + "'";

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtApplication_No.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        ddlType.ClearSelection();
                        ddlType.Items.FindByValue(ds.Tables[0].Rows[0]["ScampusId"].ToString()).Selected = true;
                        //ddlType.SelectedValue = ds.Tables[0].Rows[0]["ScampusId"].ToString();
                    }
                }
                else if (Request.QueryString["Insert"] != null)
                {
                    
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lnkupdate.Visible = false;
                    lnksave.Visible = true;
                    lnkAdd.Visible = false;
                }
                else
                {
                    divgrid.Visible = true;
                    divView.Visible = false;
                    FillGrid();
                }
                con.Close();
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
        protected void FillGrid(string sortExpression = null)
        {

            try
            {

                //SqlCommand cmd = new SqlCommand();

                //cmd.CommandText = "select * from tbl_College ";

                //cmd.Connection = con;
                SqlCommand cmd = new SqlCommand("SP_GetCollegeData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(dt_Grid);
                if (sortExpression != null)
                {
                    DataView dv = dt_Grid.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gridEmployee.DataSource = dv;
                }
                else
                {
                    gridEmployee.DataSource = dt_Grid;
                }
                //gridEmployee.DataSource = ds;

                gridEmployee.DataBind();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_College_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("SMS_College_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try { 
                lblCampus.Visible = false;
                lblValidCollegename.Visible = false;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO [tbl_College] ([Name] ,[ScampusId]) VALUES ('" + txtApplication_No.Text + "','" + ddlType.SelectedValue + "')";
                cmd.Connection = con;
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("SMS_College_Master.aspx");
                con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try { 
                lblCampus.Visible = false;
                lblValidCollegename.Visible = false;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE [tbl_College] SET [Name] = '" + txtApplication_No.Text + "',[ScampusId] = '" + ddlType.SelectedValue + "' WHERE Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                Response.Redirect("SMS_College_Master.aspx");
                con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            try { 
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
            Label Ids = (Label)gvr.FindControl("lblId");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Delete from tbl_College where Id= '" + Ids.Text + "'";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            FillGrid();
            Response.Redirect("SMS_College_Master.aspx");
            con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected bool Validation()
        {
            if (txtApplication_No.Text == "")
            {
                lblValidCollegename.Visible = true;
                lblCampus.Visible = false;
                txtApplication_No.Focus();
                return false;
            }
            if(ddlType.SelectedValue=="0")
            {
                lblValidCollegename.Visible = false;
                lblCampus.Visible = true;
                ddlType.Focus();
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_College_Master.aspx");
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmployee.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {
            FillGrid(e.SortExpression);
        }
        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void BindScampus()
        {
            try { 
            SqlCommand cmd = new SqlCommand("SP_SMS_BindScampus_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds1 = new DataTable();
            da.Fill(ds1);

            ddlType.DataSource = ds1;
            ddlType.DataTextField = "Name";
            ddlType.DataValueField = "Id";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("--Select Scampus Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void lnksyncjob_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SyncCollege", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            lblSyncmsg.Visible = true;
            FillGrid();
        }
    }
}