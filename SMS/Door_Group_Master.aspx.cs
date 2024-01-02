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
    public partial class Door_Group_Master : System.Web.UI.Page
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
        }
        protected void FillGrid(string sortExpression = null)
        {

            try
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "select SM.Id, SM.Door_Name, SM.Description, ( select ','+CM.Name from dbo.tbl_Reader as CM where ','+SM.Reader_Id+',' like '%,'+Cast(CM.Id as nvarchar(50))+',%' for xml path(''), type ).value('substring(text()[1], 2)', 'varchar(max)') as Reader_Id from dbo.tbl_Door_Group as SM";

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
            Response.Redirect("Door_Group_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("Door_Group_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
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

                //SqlDataAdapter da = new SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();

                //da.Fill(ds);

                //gridEmployee.DataSource = ds;

                //gridEmployee.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("Door_Group_Master.aspx");
                con.Close();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
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

                //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);

                cmd.ExecuteNonQuery();
                Response.Redirect("Door_Group_Master.aspx");
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

            cmd.CommandText = "Delete from tbl_Door_Group where Id= '" + Ids.Text + "'";

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();

           
            FillGrid();
            Response.Redirect("Door_Group_Master.aspx");
            con.Close();
        }
        protected bool Validation()
        {
            if (txtApplication_No.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Door Name')</script>");
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Door_Group_Master.aspx");
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