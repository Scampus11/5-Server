using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SMS
{
    public partial class Session_Master : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
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
        }
        protected void FillGrid(string sortExpression = null)
        {

            try
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "select * from tbl_Student_Session";

                cmd.Connection = con;

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

                //da.Fill(ds);

                //gridEmployee.DataSource = ds;

                gridEmployee.DataBind();

            }

            catch
            {



            }

        }
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Session_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("Session_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO [tbl_Student_Session] ([Session_Name] ,[Session_Description],[Start_Time],[End_Time]) VALUES ('" + txtApplication_No.Text + "','" + txtSessionDescription.Text + "','" + txtStartTime.Text + "','" + txtEndTime.Text + "')";

                cmd.Connection = con;

                //SqlDataAdapter da = new SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();

                //da.Fill(ds);

                //gridEmployee.DataSource = ds;

                //gridEmployee.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("Session_Master.aspx");
                con.Close();
            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE [tbl_Student_Session] SET [Session_Name] = '" + txtApplication_No.Text + "',[Session_Description] = '" + txtSessionDescription.Text + "',[Start_Time] = '" + txtStartTime.Text + "',[End_Time] = '" + txtEndTime.Text + "' WHERE Session_Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";

                cmd.Connection = con;

                //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);

                cmd.ExecuteNonQuery();
                Response.Redirect("Session_Master.aspx");
                con.Close();
            }
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            //string ids = "";
            //ids = string.Empty;
            //ids = (sender as LinkButton).CommandArgument;
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
            Response.Redirect("Session_Master.aspx");
            con.Close();
        }
        protected bool Validation()
        {
            if (txtApplication_No.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Reader Name')</script>");
                return false;
            }
            if (txtStartTime.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Start Time')</script>");
                return false;
            }
            if (txtEndTime.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter End Time')</script>");
                return false;
            }
            if (Convert.ToDateTime(txtStartTime.Text.Substring(0, 5)) > Convert.ToDateTime(txtEndTime.Text.Substring(0, 5)))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('end time can not be exceed than start time of session')</script>");
                return false;
            }
            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Session_Master.aspx");
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
    }
}