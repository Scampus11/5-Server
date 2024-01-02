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
    public partial class SMS_Service_Master : System.Web.UI.Page
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
            Session["Sider"] = "Service Master";

            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Id"] != null)
                    {
                        BindDepartment();
                        divgrid.Visible = false;
                        divView.Visible = true;
                        lnkupdate.Visible = true;
                        lnksave.Visible = false;
                        lnkAdd.Visible = false;
                        con.Open();
                        //txtStudentID.Text = Request.QueryString["Id"].ToString();
                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = "select * from SMS_Service_Master where Id= '" + Request.QueryString["Id"].ToString() + "'";

                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();


                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtService.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                            txtStartDate.Text = ds.Tables[0].Rows[0]["Startdatetime"].ToString();
                            txtEndDate.Text = ds.Tables[0].Rows[0]["EndDatetime"].ToString();
                            ddlDepartment.ClearSelection();
                            ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["DeptId"].ToString()).Selected = true;
                            Emplist(ds.Tables[0].Rows[0]["Employee"].ToString());
                            MoveEmp(ds.Tables[0].Rows[0]["Employee"].ToString());

                        }
                    }
                    else if (Request.QueryString["Insert"] != null)
                    {
                        BindDepartment();
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
        protected void BindDepartment()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_StaffDepartmentData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    ddlDepartment.DataSource = ds1;
                    ddlDepartment.DataTextField = "Name";
                    ddlDepartment.DataValueField = "Id";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));
                    lstEmp.DataSource = null;
                    lstEmp.DataTextField = "Name";
                    lstEmp.DataValueField = "Id";
                    lstEmp.DataBind();
                    lstmoveemp.DataSource = null;
                    lstmoveemp.DataTextField = "Name";
                    lstmoveemp.DataValueField = "Id";
                    lstmoveemp.DataBind();
                }
                else
                {
                    ddlDepartment.DataSource = null;
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void Emplist(string Emplist)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_Get_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Emplist");
                cmd.Parameters.AddWithValue("@Employee", Emplist);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    lstEmp.DataSource = ds1;
                    lstEmp.DataTextField = "Name";
                    lstEmp.DataValueField = "Id";
                    lstEmp.DataBind();
                    
                }
                else
                {
                    lstmoveemp.DataSource = null;
                    lstmoveemp.DataBind();

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void MoveEmp(string Emplist)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_Get_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "MoveEmp");
                cmd.Parameters.AddWithValue("@Employee", Emplist);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    lstmoveemp.DataSource = ds1;
                    lstmoveemp.DataTextField = "Name";
                    lstmoveemp.DataValueField = "Id";
                    lstmoveemp.DataBind();

                }
                else
                {
                    lstmoveemp.DataSource = null;
                    lstmoveemp.DataBind();

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
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
                SqlCommand cmd = new SqlCommand("SP_GetServiceData", con);
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
            Response.Redirect("SMS_Service_Master.aspx?Insert=Add Services");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Service_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    lblService.Visible = false;
                    lblStartDate.Visible = false;
                    lblEndDate.Visible = false;
                    lblDepartment.Visible = false;
                    string listEmployess = "";
                    foreach (ListItem li in lstmoveemp.Items)
                    {
                        listEmployess += li.Value + ",";
                    }
                    listEmployess = listEmployess.TrimEnd(',');
                    SqlCommand cmd = new SqlCommand("SMS_Service_Upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Insert");
                    cmd.Parameters.AddWithValue("@Service", txtService.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Startdatetime", txtStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDatetime", txtEndDate.Text);
                    cmd.Parameters.AddWithValue("@DeptId", ddlDepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@EmployeeId", listEmployess);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect("SMS_Service_Master.aspx");
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
                    lblService.Visible = false;
                    lblStartDate.Visible = false;
                    lblEndDate.Visible = false;
                    lblDepartment.Visible = false;
                    string listEmployess = "";
                    foreach (ListItem li in lstmoveemp.Items)
                    {
                        listEmployess += li.Value + ",";
                    }
                    listEmployess = listEmployess.TrimEnd(',');
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SMS_Service_Upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Update");
                    cmd.Parameters.AddWithValue("@Id", Request.QueryString["Id"].ToString());
                    cmd.Parameters.AddWithValue("@Service", txtService.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Startdatetime", txtStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDatetime", txtEndDate.Text);
                    cmd.Parameters.AddWithValue("@DeptId", ddlDepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@EmployeeId", listEmployess);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Response.Redirect("SMS_Service_Master.aspx");
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
                cmd.CommandText = "Delete from SMS_Service_Master where Id= '" + Ids.Text + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                FillGrid();
                Response.Redirect("SMS_Service_Master.aspx");
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected bool Validation()
        {
            if (txtService.Text == "")
            {
                lblService.Visible = true;
                lblStartDate.Visible = false;
                lblEndDate.Visible = false;
                lblDepartment.Visible = false;
                lblvalidValidDateUntil2.Visible = false;
                txtService.Focus();
                return false;
            }
            if (txtStartDate.Text == "")
            {
                lblStartDate.Visible = true;
                lblService.Visible = false;
                lblEndDate.Visible = false;
                lblDepartment.Visible = false;
                lblvalidValidDateUntil2.Visible = false;
                txtStartDate.Focus();
                return false;
            }
            if (txtEndDate.Text == "")
            {
                lblEndDate.Visible = true;
                lblService.Visible = false;
                lblStartDate.Visible = false;
                lblDepartment.Visible = false;
                lblvalidValidDateUntil2.Visible = false;
                txtEndDate.Focus();
                return false;
            }
            if (ddlDepartment.SelectedValue == "0")
            {
                lblDepartment.Visible = true;
                lblService.Visible = false;
                lblEndDate.Visible = false;
                lblEndDate.Visible = false;
                lblvalidValidDateUntil2.Visible = false;
                ddlDepartment.Focus();
                return false;
            }
            if (txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "")
            {
                //DateTime fromdate = new DateTime(int.Parse(txtStartDate.Text.Substring(6, 4)), int.Parse(txtStartDate.Text.Substring(3, 2)), int.Parse(txtStartDate.Text.Substring(0, 2)), 00, 00, 00);
                //DateTime todate = new DateTime(int.Parse(txtEndDate.Text.Substring(6, 4)), int.Parse(txtEndDate.Text.Substring(3, 2)), int.Parse(txtEndDate.Text.Substring(0, 2)), 00, 00, 00);
                DateTime fromdate = Convert.ToDateTime(txtStartDate.Text);
                DateTime todate = Convert.ToDateTime(txtEndDate.Text);
                int res = DateTime.Compare(fromdate, todate);
                if (res > 0)
                {
                    lblvalidValidDateUntil2.Visible = true;
                    lblDepartment.Visible = false;
                    lblService.Visible = false;
                    lblEndDate.Visible = false;
                    lblEndDate.Visible = false;
                    txtStartDate.Focus();

                    return false;
                }
                
            }
            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Service_Master.aspx");
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

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Request.QueryString["Id"] != null)
            //{
            //    if (ddlDepartment.SelectedValue != "0")
            //    {
            //        try
            //        {
            //            Emplist(ddlDepartment.SelectedValue);
            //            MoveEmp(ddlDepartment.SelectedValue);
            //        }
            //        catch (Exception ex)
            //        {
            //            LogFile.LogError(ex);
            //        }
            //    }
                
            //}
            //else 
            if (ddlDepartment.SelectedValue != "0")
            {
                try
                {
                    List<ListItem> removEditems = new List<ListItem>();
                    SqlCommand cmd = new SqlCommand("SP_SMS_StaffEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable ds1 = new DataTable();
                    da.Fill(ds1);
                    if (ds1.Rows.Count > 0)
                    {
                        lstEmp.DataSource = ds1;
                        lstEmp.DataTextField = "Name";
                        lstEmp.DataValueField = "Id";
                        lstEmp.DataBind();
                    }
                    else
                    {
                        lstEmp.DataSource = null;
                        lstEmp.DataBind();
                    }
                    
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
            lstmoveemp.Items.Clear();
        }
        protected void LeftClick(object sender, EventArgs e)
        {
            //List will hold items to be removed.
            List<ListItem> removEditems = new List<ListItem>();

            //Loop and transfer the Items to Destination ListBox.
            foreach (ListItem item in lstmoveemp.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    lstEmp.Items.Add(item);
                    removEditems.Add(item);
                }
            }

            //Loop and remove the Items from the Source ListBox.
            foreach (ListItem item in removEditems)
            {
                lstmoveemp.Items.Remove(item);
            }
        }

        protected void RightClick(object sender, EventArgs e)
        {
            //List will hold items to be removed.
            List<ListItem> removEditems = new List<ListItem>();

            //Loop and transfer the Items to Destination ListBox.
            foreach (ListItem item in lstEmp.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    lstmoveemp.Items.Add(item);
                    removEditems.Add(item);
                }
            }

            //Loop and remove the Items from the Source ListBox.
            foreach (ListItem item in removEditems)
            {
                lstEmp.Items.Remove(item);
            }
        }
    }
}