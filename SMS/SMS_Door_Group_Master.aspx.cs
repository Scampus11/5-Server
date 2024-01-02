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
    public partial class SMS_Door_Group_Master : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Sider"] = "Door Group Master";
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
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

                    cmd.CommandText = "select * from tbl_Door_Group where Id= '" + Request.QueryString["Id"].ToString() + "'";

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtApplication_No.Text = ds.Tables[0].Rows[0]["Door_Name"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    }

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = "select * from tbl_Reader";
                    //con.Open();
                    cmd1.Connection = con;
                    cmd1.ExecuteNonQuery();


                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    DataSet ds1 = new DataSet();
                    String Door_Group_Id = ds.Tables[0].Rows[0]["Reader_Id"].ToString();
                    String[] Door_Group_Id_Array = Door_Group_Id.Split(',');
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        RepeatReader.DataSource = ds1;
                        RepeatReader.DataBind();
                        foreach (RepeaterItem item in RepeatReader.Items)
                        {
                            CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                            Label lblId = (Label)item.FindControl("lblId");
                            for (int i = 0; i < Door_Group_Id_Array.Length; i++)
                            {
                                if (lblId.Text == Door_Group_Id_Array[i].ToString())
                                {
                                    chkReader.Checked = true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                else if (Request.QueryString["Insert"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lnkupdate.Visible = false;
                    lnksave.Visible = true;
                    lnkAdd.Visible = false;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "select * from tbl_Reader";
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RepeatReader.DataSource = ds;
                        RepeatReader.DataBind();
                    }
                    con.Close();
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
                SqlCommand cmd = new SqlCommand("SP_GetDoorGroupData", con);
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
            Response.Redirect("SMS_Door_Group_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("SMS_Door_Group_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try { 
                lblValidoorgroupname.Visible = false;
                string ReaderId = string.Empty;
                List<string> Reader_List = new List<string>();
                foreach (RepeaterItem item in RepeatReader.Items)
                {
                    CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                    Label lblId = (Label)item.FindControl("lblId");
                    if (chkReader.Checked)
                    {
                        //ReaderId = ReaderId + lblId.Text + ",";
                        Reader_List.Add(lblId.Text);
                    }
                    ReaderId = String.Join(",", Reader_List.ToArray());

                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO [tbl_Door_Group] ([Door_Name] ,[Description],[Reader_Id]) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "','" + ReaderId.ToString() + "')";
                cmd.Connection = con;
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("SMS_Door_Group_Master.aspx");
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
                try { 
                lblValidoorgroupname.Visible = false;
                string ReaderId = string.Empty;
                List<string> Reader_List = new List<string>();
                foreach (RepeaterItem item in RepeatReader.Items)
                {
                    CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                    Label lblId = (Label)item.FindControl("lblId");
                    if (chkReader.Checked)
                    {
                        //ReaderId = ReaderId + lblId.Text + ",";
                        Reader_List.Add(lblId.Text);
                    }
                    ReaderId = String.Join(",", Reader_List.ToArray());

                }
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE [tbl_Door_Group] SET [Door_Name] = '" + txtApplication_No.Text + "',[Description] = '" + txtDescription.Text + "' ,[Reader_Id] = '" + ReaderId + "' WHERE Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                Response.Redirect("SMS_Door_Group_Master.aspx");
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
            cmd.CommandText = "Delete from tbl_Door_Group where Id= '" + Ids.Text + "'";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            FillGrid();
            Response.Redirect("SMS_Door_Group_Master.aspx");
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
                lblValidoorgroupname.Visible = true;
                txtApplication_No.Focus();
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Door_Group_Master.aspx");
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