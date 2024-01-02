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

namespace SMS
{
    public partial class Canteen_Report : System.Web.UI.Page
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
            Session["Sider"] = "Canteen Report";
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
                    ModalPopupExtender ModalPopupExtenderAPIs = (ModalPopupExtender)Page.Master.FindControl("ModalPopupExtenderAPIs");
                    ModalPopupExtenderAPIs.Hide();
                    ModalPopupExtender2.Show();
                    BindCanteen();
                    BindSession();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }
        protected void BindSession()
        {
            DataTable dtCollege = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "GetSession");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCollege);

            if (dtCollege.Rows.Count > 0)
            {

                ddlsession.DataSource = dtCollege;
                ddlsession.DataTextField = "Name";
                ddlsession.DataValueField = "Id";
                ddlsession.DataBind();
                ddlsession.Items.Insert(0, new ListItem("--Select Session--", "0"));
            }
            else
            {
                ddlsession.DataSource = null;
                ddlsession.DataBind();
                ddlsession.Items.Insert(0, new ListItem("--Select Session--", "0"));
            }
        }

        protected void BindCanteen()
        {
            DataTable dtCollege = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "BindCanteen");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCollege);
            if (dtCollege.Rows.Count > 0)
            {
                ddlCanteen.DataSource = dtCollege;
                ddlCanteen.DataTextField = "Name";
                ddlCanteen.DataValueField = "Id";
                ddlCanteen.DataBind();
                ddlCanteen.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
            }
            else
            {
                ddlCanteen.DataSource = null;
                ddlCanteen.DataBind();
                ddlCanteen.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
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
            Response.Redirect("Canteen_Report.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get Canteen info");
                cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@AG_Id", ddlCanteen.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text);
                cmd.Parameters.AddWithValue("@StudentId", txtStudent.Text.Trim());
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
            using (XLWorkbook wb = new XLWorkbook())
            {
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get Canteen info");
                cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@AG_Id", ddlCanteen.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text);
                cmd.Parameters.AddWithValue("@StudentId", txtStudent.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt_Grid);

                string attachment = "attachment; filename=CanteenReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt_Grid.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt_Grid.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt_Grid.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
        [WebMethod]
        public static List<string> GetStudentInfo(string StudentName)
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
                cmd.CommandText = "select Distinct Student_Id from tblMemAccLog where Student_Id!='' and Student_Id is not null and Student_Id  LIKE '%'+@SearchEmpName+'%'";
                //select top 10 StudentId from tblstudent where StudentId!='' and StudentId is not null and StudentId like
                cmd.Connection = con;
                con.Open();
                cmd.Parameters.AddWithValue("@SearchEmpName", StudentName);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    empResult.Add(dr["Student_Id"].ToString());
                }
                con.Close();
                
                return empResult;
            }

        }
    }
}