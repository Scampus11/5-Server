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
using SMS.Class;
using System.Web.Configuration;

namespace SMS
{
    public partial class SMS_Page_Right : System.Web.UI.Page
    {
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
                Session["Sider"] = "Page Right";
                BindRole();
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
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "select * from Officer_Master WHERE Staff_Id LIKE '%' + @Search + '%' or Id LIKE '%' + @Search + '%' OR Full_Name LIKE '%' + @Search + '%' OR Email_Id LIKE '%' + @Search + '%' OR Job_Title LIKE '%' + @Search + '%' OR Department  LIKE '%' + @Search + '%'";
                SqlCommand cmd = new SqlCommand("SP_SMS_Page_Name", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                
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

                if (ddlRole.SelectedValue != "0" && ddlStaff.SelectedValue != "")
                {
                    foreach (GridViewRow row in gridEmployee.Rows)
                    {
                       
                        CheckBox chkisactive = row.FindControl("chkisactive") as CheckBox;
                        Label lblPage_Name = row.FindControl("lblPage_Name") as Label;
                        CheckBox chkcanteens = (CheckBox)(row.FindControl("chkcanteens"));
                        CheckBox chkAccessgroup = (CheckBox)(row.FindControl("chkAccessgroup"));
                        CheckBox chkblockgroup = (CheckBox)(row.FindControl("chkblockgroup"));
                        CheckBox chkStaffAccessgroup = (CheckBox)(row.FindControl("chkStaffAccessgroup"));
                        SqlCommand cmd1 = new SqlCommand("SP_Get_Page_Right", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Role_Id", ddlRole.SelectedValue.Trim());
                        cmd1.Parameters.AddWithValue("@Staff_Id", ddlStaff.SelectedValue.Trim());
                        cmd1.Connection = con;
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable ds1 = new DataTable();
                        da1.Fill(ds1);
                        for (int i = 0; i < ds1.Rows.Count; i++)
                        {
                            if (lblPage_Name.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                            {
                                chkisactive.Checked = true;
                            }
                            
                            if (ds1.Rows[i]["Canteens"].ToString().Trim()=="1")
                            {
                                chkcanteens.Checked = true;
                            }
                            
                            if (ds1.Rows[i]["AG"].ToString().Trim() == "1")
                            {
                                chkAccessgroup.Checked = true;
                            }
                            
                            if (ds1.Rows[i]["BG"].ToString().Trim() == "1")
                            {
                                chkblockgroup.Checked = true;
                            }
                            
                            if (ds1.Rows[i]["StaffAG"].ToString().Trim() == "1")
                            {
                                chkStaffAccessgroup.Checked = true;
                            }
                            
                        }
                    }
                }
            }
            catch
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
        }

        protected void ddlStaff_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void BindRole()
        {
            SqlCommand cmd = new SqlCommand("SP_SMS_Role_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds1 = new DataTable();
            da.Fill(ds1);

            ddlRole.DataSource = ds1;
            ddlRole.DataTextField = "Role_Name";
            ddlRole.DataValueField = "Id";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("--Select Role--", "0"));
        }

        protected void ddlRole_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SP_SMS_Role_Master", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Role_Id", ddlRole.SelectedValue);
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds1 = new DataTable();
            da.Fill(ds1);

            ddlStaff.DataSource = ds1;
            ddlStaff.DataTextField = "StaffName";
            ddlStaff.DataValueField = "Staff_Id";
            ddlStaff.DataBind();
            lnkUpdate.Visible = true;
            FillGrid();
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridEmployee.Rows)
            {
                String Isactive = "0", Canteens = "0", AG = "0", BG = "0", StaffAG = "0";
                CheckBox chkisactive = row.FindControl("chkisactive") as CheckBox;
                Label lblPage_Name = row.FindControl("lblPage_Name") as Label;
                CheckBox chkcanteens = (CheckBox)(row.FindControl("chkcanteens"));
                CheckBox chkAccessgroup = (CheckBox)(row.FindControl("chkAccessgroup"));
                CheckBox chkblockgroup = (CheckBox)(row.FindControl("chkblockgroup"));
                CheckBox chkStaffAccessgroup = (CheckBox)(row.FindControl("chkStaffAccessgroup"));
                if (chkisactive.Checked)
                    Isactive = "1";
                if (lblPage_Name.Text.Trim() == "SMS_Student_Access_List.aspx")
                {
                    if (chkcanteens.Checked)
                        Canteens = "1";
                    if (chkAccessgroup.Checked)
                        AG = "1";
                    if (chkblockgroup.Checked)
                        BG = "1";
                }
                else if (lblPage_Name.Text.Trim() == "SMS_Staff_Access_List.aspx")
                { 
                    if (chkStaffAccessgroup.Checked)
                        StaffAG = "1";
            }
                SqlCommand cmd = new SqlCommand("SP_Page_Right_Upset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Isactive", Isactive);
                cmd.Parameters.AddWithValue("@Page_Name", lblPage_Name.Text);
                cmd.Parameters.AddWithValue("@Role_Id", ddlRole.SelectedValue);
                cmd.Parameters.AddWithValue("@Staff_Id", ddlStaff.SelectedValue);
                cmd.Parameters.AddWithValue("@Canteens", Canteens);
                cmd.Parameters.AddWithValue("@AG", AG);
                cmd.Parameters.AddWithValue("@BG", BG);
                cmd.Parameters.AddWithValue("@StaffAG", StaffAG);
                con.Open();
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!');", true);
                con.Close();


            }
        }
        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblPanelName = (Label)(e.Row.FindControl("lblPanelName"));
                    CheckBox chkcanteens = (CheckBox)(e.Row.FindControl("chkcanteens"));
                    CheckBox chkAccessgroup = (CheckBox)(e.Row.FindControl("chkAccessgroup"));
                    CheckBox chkblockgroup = (CheckBox)(e.Row.FindControl("chkblockgroup"));
                    CheckBox chkStaffAccessgroup = (CheckBox)(e.Row.FindControl("chkStaffAccessgroup"));
                    
                    if (lblPanelName.Text == "Student Access Master")
                    {
                        chkcanteens.Visible = true;
                        chkAccessgroup.Visible = true;
                        chkblockgroup.Visible = true;
                    }
                    else
                    {
                        chkcanteens.Visible = false;
                        chkAccessgroup.Visible = false;
                        chkblockgroup.Visible = false;
                    }
                    if (lblPanelName.Text == "Staff Access Master")
                    {
                        chkStaffAccessgroup.Visible = true;
                    }
                    else
                    {
                        chkStaffAccessgroup.Visible = false;
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