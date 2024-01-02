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
    public partial class SMS_Visitor_Card_Master : System.Web.UI.Page
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
            Session["Sider"] = "Visitor Card Master";
            
            if (!Page.IsPostBack)
            {
                try { 
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

                    cmd.CommandText = "select * from SMS_Visitor_Card_Master where Id= '" + Request.QueryString["Id"].ToString() + "'";

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtApplication_No.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        if (ds.Tables[0].Rows[0]["CardStatusId"].ToString() == "2")
                        {
                            ddlCardstatus.ClearSelection();
                            ddlCardstatus.Items.FindByValue("0").Selected = true;
                        }
                        else
                        {
                            ddlCardstatus.ClearSelection();
                            ddlCardstatus.Items.FindByValue(ds.Tables[0].Rows[0]["CardStatusId"].ToString()).Selected = true;
                        }
                        txtCardNumber.Text = ds.Tables[0].Rows[0]["CardNumber"].ToString();
                        txtVisitorCardId.Text = ds.Tables[0].Rows[0]["visitorCardId"].ToString();
                        txtpremiesname.Text = ds.Tables[0].Rows[0]["PremissCode"].ToString();
                        
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
                SqlCommand cmd = new SqlCommand("SP_GetVisitorCardData", con);
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
            Response.Redirect("SMS_Visitor_Card_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Visitor_Card_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try {
                    SqlCommand cmd = new SqlCommand("SMS_SP_Get_Visitor_CardNumber", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Flag", "Get");
                    cmd.Parameters.AddWithValue("@CardNumber", txtCardNumber.Text);
                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows[0]["Count"].ToString() == "0")
                    {
                        lblCardName.Visible = false;
                        lblCardNumber.Visible = false;
                        lblstatus.Visible = false;
                        lblduplicatecardnumber.Visible = false;
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandText = "INSERT INTO [SMS_Visitor_Card_Master] ([Name] ,[CardNumber],CardStatusId,PremissCode,visitorCardId) VALUES ('" + txtApplication_No.Text + "','" + txtCardNumber.Text.Trim() + "','" + ddlCardstatus.SelectedValue + "','" + txtpremiesname.Text.Trim() + "','" + txtVisitorCardId.Text.Trim() + "')";
                        cmd1.Connection = con;
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        Response.Redirect("SMS_Visitor_Card_Master.aspx");
                        con.Close();
                    }
                    else
                    {
                        lblCardName.Visible = false;
                        lblCardNumber.Visible = false;
                        lblstatus.Visible = false;
                        lblduplicatecardnumber.Visible = true;
                    }
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
                lblCardName.Visible = false;
                lblCardNumber.Visible = false;
                lblstatus.Visible = false;
                con.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "UPDATE [SMS_Visitor_Card_Master] SET [Name] = '" + txtApplication_No.Text + "',[CardNumber] = '" + txtCardNumber.Text.Trim() + "',CardStatusId='" + ddlCardstatus.SelectedValue + "',PremissCode='" + txtpremiesname.Text.Trim() + "',visitorCardId='"+txtVisitorCardId.Text+"' WHERE Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";
                cmd1.Connection = con;
                cmd1.ExecuteNonQuery();
                Response.Redirect("SMS_Visitor_Card_Master.aspx");
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
            cmd.CommandText = "Delete from SMS_Visitor_Card_Master where Id= '" + Ids.Text + "'";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            FillGrid();
            Response.Redirect("SMS_Visitor_Card_Master.aspx");
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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Card Name')</script>");
                lblCardName.Visible = true;
                lblCardNumber.Visible = false;
                lblstatus.Visible = false;
                txtApplication_No.Focus();
                return false;
            }
            if (txtCardNumber.Text == "")
            {
                // ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Card Number')</script>");
                lblCardNumber.Visible = true;
                lblstatus.Visible = false;
                lblCardName.Visible = false;
                txtCardNumber.Focus();
                return false;
            }
            if (ddlCardstatus.SelectedValue == "0")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('--Select Status--')</script>");
                lblstatus.Visible = true;
                lblCardNumber.Visible = false;
                lblCardName.Visible = false;
                ddlCardstatus.Focus();
                return false;
            }
            //SqlCommand cmd = new SqlCommand("SP_GetVisitorCardData", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@Name", txtApplication_No.Text.Trim());
            //cmd.Parameters.AddWithValue("@CardNumber", txtCardNumber.Text.Trim());
            //cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //if (ds.Tables[0].Rows[0]["Count"].ToString() != "0")
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Card Number Already Exits')</script>");
            //    return false;
            //}
            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Visitor_Card_Master.aspx");
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