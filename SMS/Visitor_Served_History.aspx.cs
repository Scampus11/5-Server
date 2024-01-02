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
using System.Web.Services;
using AjaxControlToolkit;

namespace SMS.Visitor
{
    public partial class Visitor_Served_History : System.Web.UI.Page
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
            Session["Sider"] = "Employee Served Visitors";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEmployee.Enabled = true;
                    lblStartServed.Text = "Start Served date";
                    lblEndServed.Text = "End Served date";
                    ModalPopupExtender ModalPopupExtenderAPIs = (ModalPopupExtender)Page.Master.FindControl("ModalPopupExtenderAPIs");
                    ModalPopupExtenderAPIs.Hide();
                    ModalPopupExtender2.Show();

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
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Visitor_Served_History.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                if(txtServices.Text=="")
                {
                    txtServicesId.Text = "";
                }
                if(txtEmployee.Text=="")
                {
                    txtEmployeeId.Text = "";
                }
                if(txtVisitor.Text=="")
                {
                    txtVisitorId.Text = "";
                }
                SqlCommand cmd = new SqlCommand("SP_SMS_Visitor_Serve_Assignment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                if(ddlServeFlag.SelectedValue=="2")
                {
                    cmd.Parameters.AddWithValue("@Flag", "Assign");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Flag", "Served");
                }
                
                cmd.Parameters.AddWithValue("@Service", txtServicesId.Text.Trim());
                cmd.Parameters.AddWithValue("@Employee", txtEmployeeId.Text.Trim());
                cmd.Parameters.AddWithValue("@Visitor", txtVisitorId.Text.Trim());
                cmd.Parameters.AddWithValue("@ServedFlag", ddlServeFlag.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt_Grid);
                lblCount.Text = Convert.ToString(dt_Grid.Rows.Count);
                if (dt_Grid.Rows.Count > 0)
                {
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
                    btnExport.Visible = true;
                }
                else
                {
                    gridEmployee.DataSource = null;
                    gridEmployee.DataBind();
                    btnExport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void lnkGo_Click(object sender, EventArgs e)
        {
            grid1.Attributes.Add("style", "overflow-x: auto;height: " + hidValue.Value + "px");
            FillGrid();
            ModalPopupExtender2.Hide();
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
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
        }

        protected void lnkExcel_Click(object sender, EventArgs e)
        {
           
        }
        [WebMethod]
        public static string[] GetVisitorInfo(string Name)
        {
            string line = "";
            SqlConnection con = new SqlConnection();
            string filePath1 = System.Web.Hosting.HostingEnvironment.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            line = sr.ReadToEnd();
            con = new SqlConnection(line);
            con.Open();
            con.Close();
            List<string> empResult = new List<string>();


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Distinct A.VisitorId Id,First_Name+' '+Last_Name+'( '+cast(A.VisitorId as varchar(50))+' )' Name from [dbo].[tbl_SMS_Visitor_assign_Served_Services] A"
                                    + " Inner Join SMS_Visitors B on A.VisitorId = B.SLN_ACS_Visitor_Info"
                                    +" where(A.VisitorId  LIKE '%' + @SearchEmpName + '%' or First_Name  LIKE '%' + @SearchEmpName + '%'"
                                    +" or Last_Name  LIKE '%' + @SearchEmpName + '%')";
                cmd.Connection = con;
                con.Open();
                cmd.Parameters.AddWithValue("@SearchEmpName", Name);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //empResult.Add(dr["Name"].ToString());
                    empResult.Add(string.Format("{0}-{1}", dr["Name"], dr["Id"]));
                }
                con.Close();

                return empResult.ToArray();
            }

        }
        [WebMethod]
        public static string[] GetEmployeesInfo(string Name)
        {
            string line = "";
            SqlConnection con = new SqlConnection();
            string filePath1 = System.Web.Hosting.HostingEnvironment.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            line = sr.ReadToEnd();
            con = new SqlConnection(line);
            con.Open();
            con.Close();
            List<string> empResult = new List<string>();


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Distinct A.EmpId Id,B.Full_Name+'( '+cast(B.Staff_Id as varchar(50))+' )' Name from [dbo].[tbl_SMS_Visitor_assign_Served_Services] A"
                                    + " Inner Join officer_master B on A.empId = B.Id"
                                    + " where(A.ServiceId  LIKE '%' + @SearchEmpName + '%' or B.Full_Name  LIKE '%' + @SearchEmpName + '%')";
                cmd.Connection = con;
                con.Open();
                cmd.Parameters.AddWithValue("@SearchEmpName", Name);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // empResult.Add(dr["Name"].ToString());
                    empResult.Add(string.Format("{0}-{1}", dr["Name"], dr["Id"]));
                }
                con.Close();

                return empResult.ToArray();
            }

        }
        [WebMethod]
        public static string[] GetServicesInfo(string Name)
        {
            string line = "";
            SqlConnection con = new SqlConnection();
            string filePath1 = System.Web.Hosting.HostingEnvironment.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            line = sr.ReadToEnd();
            con = new SqlConnection(line);
            con.Open();
            con.Close();
            List<string> empResult = new List<string>();
            List<string> terms = Name.Split(',').ToList();
            terms = terms.Select(s => s.Trim()).ToList();

            //Extract the term to be searched from the list
            string searchTerm = terms.LastOrDefault().ToString().Trim();

            //Return if Search Term is empty
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new string[0];
            }

            //Populate the terms that need to be filtered out
            List<string> excludeTerms = new List<string>();
            if (terms.Count > 1)
            {
                terms.RemoveAt(terms.Count - 1);
                excludeTerms = terms;
            }


            using (SqlCommand cmd = new SqlCommand())
            {
                string query = "select Distinct A.ServiceId Id,B.Name Name from [dbo].[tbl_SMS_Visitor_assign_Served_Services] A"
                                    + " Inner Join SMS_Service_Master B on A.ServiceId = B.Id"
                                    + " where(A.ServiceId  LIKE '%' + @SearchEmpName + '%' or B.Name  LIKE '%' + @SearchEmpName + '%')";
                //Filter out the existing searched items
                if (excludeTerms.Count > 0)
                {
                    query += string.Format(" and B.Name not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                }
                cmd.CommandText = query;
                cmd.Connection = con;
                con.Open();
                cmd.Parameters.AddWithValue("@SearchEmpName", searchTerm);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //empResult.Add(dr["Name"].ToString());
                    empResult.Add(string.Format("{0}-{1}", dr["Name"], dr["Id"]));
                }
                con.Close();

                return empResult.ToArray();
            }

        }

        protected void ddlServeFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlServeFlag.SelectedValue=="2")
            {
                txtEmployee.Enabled = false;
                lblStartServed.Text = "Start Assign date";
                lblEndServed.Text = "End Assign date";
            }
            else
            {
                txtEmployee.Enabled = true;
                lblStartServed.Text = "Start Served date";
                lblEndServed.Text = "End Served date";
            }
        }
    }
}