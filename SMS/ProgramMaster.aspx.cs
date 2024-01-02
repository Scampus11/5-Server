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
    public partial class ProgramMaster : System.Web.UI.Page
    {
        #region Declare Variable
        DataTable dt_Grid = new DataTable();
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            Session["Sider"] = "Program Master";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
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
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "select * from ProgramMaster where Id= '" + Request.QueryString["Id"].ToString() + "'";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtProgram.Text = ds.Tables[0].Rows[0]["Name"].ToString();
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
                SqlCommand cmd = new SqlCommand("GetProgramData", con);
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
            Response.Redirect("ProgramMaster.aspx?Insert=Add");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("ProgramMaster.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    lblValidProgramname.Visible = false;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO [ProgramMaster] ([Name]) VALUES ('" + txtProgram.Text + "')";
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect("ProgramMaster.aspx");
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
                    lblValidProgramname.Visible = false;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE [ProgramMaster] SET [Name] = '" + txtProgram.Text + "' WHERE Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Response.Redirect("ProgramMaster.aspx");
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
                cmd.CommandText = "Delete from ProgramMaster where Id= '" + Ids.Text + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                FillGrid();
                Response.Redirect("ProgramMaster.aspx");
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected bool Validation()
        {
            if (txtProgram.Text == "")
            {
                lblValidProgramname.Visible = true;
                txtProgram.Focus();
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProgramMaster.aspx");
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
        protected void lnksyncjob_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SyncProgram", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            lblSyncmsg.Visible = true;
            FillGrid();
        }
    }
}