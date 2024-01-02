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
using System.Web.Configuration;
using SMS.Class;

namespace SMS
{
    public partial class SMS_Role_Master : System.Web.UI.Page
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
            Session["Sider"] = "Role Master";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
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
                    cmd.CommandText = "select * from Role_Master where Id= '" + Request.QueryString["Id"].ToString() + "'";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtApplication_No.Text = ds.Tables[0].Rows[0]["Role_Name"].ToString();
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
                if (Session["Role_Id"].ToString() == "1" || Session["Role_Id"].ToString() == "2")
                {
                    gridEmployee.Columns[3].Visible = true;
                }
                else
                {
                    gridEmployee.Columns[3].Visible = false;
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
                SqlCommand cmd = new SqlCommand("SP_GetRole_NameData", con);
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
            Response.Redirect("SMS_Role_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Role_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO [Role_Master] ([Role_Name],IsActive ) VALUES ('" + txtApplication_No.Text + "',1)";
                cmd.Connection = con;
                lblmsg.Visible = false;
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("SMS_Role_Master.aspx");
                con.Close();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE [Role_Master] SET IsActive=1, [Role_Name] = '" + txtApplication_No.Text + "' WHERE Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                Response.Redirect("SMS_Role_Master.aspx");
                con.Close();
            }
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
            Label Ids = (Label)gvr.FindControl("lblId");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Role_Master set IsActive=0 where Id= '" + Ids.Text + "'";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            FillGrid();
            Response.Redirect("SMS_Role_Master.aspx");
            con.Close();
        }
        protected bool Validation()
        {
            if (txtApplication_No.Text == "")
            {
                lblmsg.Visible = true;
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Role_Master.aspx");
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

        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton linkDelete = (LinkButton)(e.Row.FindControl("linkDelete"));
                    Label lblId = (Label)(e.Row.FindControl("lblId"));
                    if(lblId.Text=="1" || lblId.Text == "2" || lblId.Text == "3")
                    {
                        linkDelete.Visible = false;
                    }
                    else
                    {
                        linkDelete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
    }
}