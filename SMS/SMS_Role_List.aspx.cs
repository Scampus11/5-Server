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
using System.Web.Configuration;
using SMS.Class;

namespace SMS
{
    public partial class SMS_Role_List : System.Web.UI.Page
    {
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            if (!Page.IsPostBack)
            {
                Session["Sider"]= "Role List";
                FillGrid();
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
                SqlCommand cmd = new SqlCommand("SP_GetStaffData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (sortExpression != null)
                {
                    DataView dv = ds.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gridEmployee.DataSource = dv;
                }
                else
                {
                    gridEmployee.DataSource = ds;
                }

                gridEmployee.DataBind();
                
            }
            catch(Exception ex)
            {
            }

        }
        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            FillGrid();
            lblmsg.Visible = false;
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridEmployee.Rows)
            {
                DropDownList drpRole= row.FindControl("drpRole") as DropDownList;
                Label lblStaff_Id = row.FindControl("lblStaff_Id") as Label;
                string query = "";
               if (drpRole.SelectedValue== "Select Role")
                {
                     query = " Update Officer_Master set Role_Id='0' where staff_id='" + lblStaff_Id.Text + "'";
                }
               else
                {
                     query = " Update Officer_Master set Role_Id='" + drpRole.SelectedValue + "' where staff_id='" + lblStaff_Id.Text + "'";
                }
                
                SqlCommand cmd = new SqlCommand(query, con);
                
                con.Open();
                cmd.ExecuteNonQuery();
                //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Update Successfully!');", true);
                lblmsg.Visible = true;
                con.Close();
                
               
            }
        }

        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList drpRole = (e.Row.FindControl("drpRole") as DropDownList);
                drpRole.DataSource = GetData("SELECT DISTINCT Id,Role_Name FROM Role_Master");
                drpRole.DataTextField = "Role_Name";
                drpRole.DataValueField = "Id";
                drpRole.DataBind();

                //Add Default Item in the DropDownList
                drpRole.Items.Insert(0, new ListItem("Select Role"));

                //Select the Country of Customer in DropDownList
                string Role = (e.Row.FindControl("lblRole_Id") as Label).Text;
                if(Role!="" && Role!="0")
                {
                    drpRole.Items.FindByValue(Role).Selected = true;
                }
                
            }
        }
        private DataSet GetData(string query)
        {
            //string conString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            //using (SqlConnection con = new SqlConnection(conString))
            //{
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        return ds;
                    }
                }
            //}
        }
    }
}