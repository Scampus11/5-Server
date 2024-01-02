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
    public partial class SMS_Session_Master : System.Web.UI.Page
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
            Session["Sider"] = "Session Master";
            if (!Page.IsPostBack)
            {
                try
                {
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

                        cmd.CommandText = "select * from tbl_Student_Session where Session_Id= '" + Request.QueryString["Id"].ToString() + "'";

                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();


                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtApplication_No.Text = ds.Tables[0].Rows[0]["Session_Name"].ToString();
                            txtSessionDescription.Text = ds.Tables[0].Rows[0]["Session_Description"].ToString();
                            txtStartTime.Text = ds.Tables[0].Rows[0]["Start_Time"].ToString();
                            txtEndTime.Text = ds.Tables[0].Rows[0]["End_Time"].ToString();
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
                SqlCommand cmd = new SqlCommand("SP_GetSessionData", con);
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
            Response.Redirect("SMS_Session_Master.aspx?Insert=Insert Session");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("SMS_Session_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    lblValisessionname.Visible = false;
                    lblValistarttime.Visible = false;
                    lblValiendtime.Visible = false;
                    lblvalistartend.Visible = false;
                    SqlCommand cmd = new SqlCommand("SP_Upset_Session", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Insert");
                    cmd.Parameters.AddWithValue("@SessionName", txtApplication_No.Text);
                    cmd.Parameters.AddWithValue("@Session_Description", txtSessionDescription.Text);
                    cmd.Parameters.AddWithValue("@Start_Time", txtStartTime.Text);
                    cmd.Parameters.AddWithValue("@End_Time", txtEndTime.Text);
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandText = "INSERT INTO [tbl_Student_Session] ([Session_Name] ,[Session_Description],[Start_Time],[End_Time]) VALUES ('" + txtApplication_No.Text + "','" + txtSessionDescription.Text + "','" + txtStartTime.Text + "','" + txtEndTime.Text + "')";
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect("SMS_Session_Master.aspx");
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
                try
                {
                    lblValisessionname.Visible = false;
                    lblValistarttime.Visible = false;
                    lblValiendtime.Visible = false;
                    lblvalistartend.Visible = false;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_Upset_Session", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Update");
                    cmd.Parameters.AddWithValue("@SessionName", txtApplication_No.Text);
                    cmd.Parameters.AddWithValue("@Session_Description", txtSessionDescription.Text);
                    cmd.Parameters.AddWithValue("@Start_Time", txtStartTime.Text);
                    cmd.Parameters.AddWithValue("@End_Time", txtEndTime.Text);
                    cmd.Parameters.AddWithValue("@Session_Id", Convert.ToInt32(Request.QueryString["Id"]));
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandText = "UPDATE [tbl_Student_Session] SET [Session_Name] = '" + txtApplication_No.Text + "',[Session_Description] = '" + txtSessionDescription.Text + "',[Start_Time] = '" + txtStartTime.Text + "',[End_Time] = '" + txtEndTime.Text + "' WHERE Session_Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    
                    Response.Redirect("SMS_Session_Master.aspx");
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
            try
            {
                LinkButton lnk = sender as LinkButton;
                GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
                Label Ids = (Label)gvr.FindControl("lblId");
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "Delete from tbl_Student_Session where Session_Id= '" + Ids.Text + "'";

                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();


                FillGrid();
                Response.Redirect("SMS_Session_Master.aspx");
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
                lblValisessionname.Visible = true;
                lblValistarttime.Visible = false;
                lblValiendtime.Visible = false;
                lblvalistartend.Visible = false;
                txtApplication_No.Focus();
                return false;
            }
            if (txtStartTime.Text == "")
            {
                lblValisessionname.Visible = false;
                lblValistarttime.Visible = true;
                lblValiendtime.Visible = false;
                lblvalistartend.Visible = false;
                txtStartTime.Focus();
                return false;
            }
            if (txtEndTime.Text == "")
            {
                lblValisessionname.Visible = false;
                lblValistarttime.Visible = false;
                lblValiendtime.Visible = true;
                lblvalistartend.Visible = false;
                txtEndTime.Focus();
                return false;
            }
            TimeSpan start = DateTime.Parse(txtStartTime.Text).TimeOfDay;
            TimeSpan end = DateTime.Parse(txtEndTime.Text).TimeOfDay;
            if (start > end)
            {
                lblValisessionname.Visible = false;
                lblValistarttime.Visible = false;
                lblValiendtime.Visible = false;
                lblvalistartend.Visible = true;
                txtStartTime.Focus();
                return false;
            }
            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Session_Master.aspx");
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
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           