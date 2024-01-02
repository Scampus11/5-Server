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
    public partial class Access_Group_Master : System.Web.UI.Page
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
                    LoadTable();
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lnkupdate.Visible = true;
                    lnksave.Visible = false;
                    lnkAdd.Visible = false;
                    con.Open();
                    //txtStudentID.Text = Request.QueryString["Id"].ToString();
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "select * from tbl_Access_Group where Id= '" + Request.QueryString["Id"].ToString() + "';select Id, Door_Name Name from tbl_Door_Group;select Session_Id Id, Session_Name Name from tbl_Student_Session; ";

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtApplication_No.Text = ds.Tables[0].Rows[0]["Access_Group_Name"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                        ddlDoorgroup.DataSource = ds.Tables[1];
                        ddlDoorgroup.DataBind();
                        ddlDoorgroup.DataTextField = "Name";
                        ddlDoorgroup.DataValueField = "Id";
                        ddlDoorgroup.DataBind();
                        /////session dropdown
                        ddlSession.DataSource = ds.Tables[2];
                        ddlSession.DataBind();
                        ddlSession.DataTextField = "Name";
                        ddlSession.DataValueField = "Id";
                        ddlSession.DataBind();
                    }
                    cmd.CommandText = "select a.Access_Group_ID as Id,a.Id ChildId,a.Door_Group_ID as Door_GroupId,b.Door_Name as Door_Group,a.Session_Id as SessionId,c.Session_Name as Session from Access_Level a inner join tbl_Door_Group b on a.Door_Group_ID=b.Id inner join tbl_Student_Session c on a.Session_Id=c.Session_Id where a.Access_Group_ID='" + Request.QueryString["Id"].ToString() + "'";
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ViewState["AccessLevel"] = dt;
                    gvAccess.DataSource = dt;
                    gvAccess.DataBind();
                    //SqlCommand cmd1 = new SqlCommand();
                    //cmd1.CommandText = "select * from tbl_Door_Group";
                    ////con.Open();
                    //cmd1.Connection = con;
                    //cmd1.ExecuteNonQuery();


                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    //DataSet ds1 = new DataSet();
                    //String Door_Group_Id = ds.Tables[0].Rows[0]["Door_Group_Id"].ToString();
                    //String[] Door_Group_Id_Array = Door_Group_Id.Split(',');
                    //da1.Fill(ds1);
                    //if (ds1.Tables[0].Rows.Count > 0)
                    //{
                    //    RepeatReader.DataSource = ds1;
                    //    RepeatReader.DataBind();
                    //    foreach (RepeaterItem item in RepeatReader.Items)
                    //    {
                    //        CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                    //        Label lblId = (Label)item.FindControl("lblId");
                    //        for(int i=0;i<Door_Group_Id_Array.Length;i++)
                    //        {
                    //            if (lblId.Text == Door_Group_Id_Array[i].ToString())
                    //            {
                    //                chkReader.Checked = true;
                    //            }
                    //        }
                    //    }
                    //}
                    //con.Close();
                }
                else if (Request.QueryString["Insert"] != null)
                {
                    LoadTable();
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lnkupdate.Visible = false;
                    lnksave.Visible = true;
                    lnkAdd.Visible = false;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "select Id,Door_Name Name from tbl_Door_Group;select Session_Id Id, Session_Name Name from tbl_Student_Session; ";
                    con.Open();
                    cmd.Connection = con;
                    //cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDoorgroup.DataSource = ds.Tables[0];
                        ddlDoorgroup.DataBind();
                        ddlDoorgroup.DataTextField = "Name";
                        ddlDoorgroup.DataValueField = "Id";
                        ddlDoorgroup.DataBind();
                        /////session dropdown
                        ddlSession.DataSource = ds.Tables[1];
                        ddlSession.DataBind();
                        ddlSession.DataTextField = "Name";
                        ddlSession.DataValueField = "Id";
                        ddlSession.DataBind();
                    }
                    //con.Close();
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

                cmd.CommandText = "select Distinct SM.Id, SM.Access_Group_Name, SM.Description from dbo.tbl_Access_Group as SM left join Access_Level al on sm.Id=al.Access_Group_ID left join tbl_Door_Group dg on dg.Id=al.Door_Group_ID left join tbl_Student_Session ss on ss.Session_Id=al.Session_Id";

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
            Response.Redirect("Access_Group_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("Access_Group_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO [tbl_Access_Group] ([Access_Group_Name] ,[Description]) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "')";

                cmd.Connection = con;

                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select max(Id) from tbl_Access_Group";
                int Id = (Int32)cmd.ExecuteScalar();
                DataTable dt = (DataTable)ViewState["AccessLevel"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ChildId"].ToString() == "0")
                        {
                            cmd.CommandText = "insert into Access_Level(Access_Group_ID,Door_Group_ID,Session_Id)values('" + Id + "','" + dt.Rows[i]["Door_GroupId"].ToString() + "','" + dt.Rows[i]["SessionId"].ToString() + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                Response.Redirect("Access_Group_Master.aspx");
                ViewState["AccessLevel"] = null;
                con.Close();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE [tbl_Access_Group] SET [Access_Group_Name] = '" + txtApplication_No.Text + "',[Description] = '" + txtDescription.Text + "' WHERE Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";

                cmd.Connection = con;

                cmd.ExecuteNonQuery();
                int Id = Convert.ToInt32(Request.QueryString["Id"]);
                DataTable dt = (DataTable)ViewState["AccessLevel"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ChildId"].ToString() == "0")
                        {
                            cmd.CommandText = "insert into Access_Level(Access_Group_ID,Door_Group_ID,Session_Id)values('" + Id + "','" + dt.Rows[i]["Door_GroupId"].ToString() + "','" + dt.Rows[i]["SessionId"].ToString() + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                Response.Redirect("Access_Group_Master.aspx");
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

            cmd.CommandText = "Delete from tbl_Access_Group where Id= '" + Ids.Text + "'";

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();


            FillGrid();
            Response.Redirect("Access_Group_Master.aspx");
            con.Close();
        }
        protected bool Validation()
        {
            if (txtApplication_No.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Access Group Name')</script>");
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Access_Group_Master.aspx");
        }

        protected void LoadTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Id"), new DataColumn("ChildId"), new DataColumn("Door_GroupId"), new DataColumn("Door_Group"), new DataColumn("SessionId"), new DataColumn("Session") });
            ViewState["AccessLevel"] = dt;
            FillChildGrid();
        }

        protected void FillChildGrid()
        {
            gvAccess.DataSource = (DataTable)ViewState["AccessLevel"];
            gvAccess.DataBind();
        }

        protected bool ValidateChild()
        {
            DataTable dt = (DataTable)ViewState["AccessLevel"];
            int duplicate = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int DoorGroupId = Convert.ToInt32(dt.Rows[i]["Door_GroupId"].ToString());
                    int SessionId = Convert.ToInt32(dt.Rows[i]["SessionId"].ToString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        int DoorGroupIdC = Convert.ToInt32(dt.Rows[j]["Door_GroupId"].ToString());
                        int SessionIdC = Convert.ToInt32(dt.Rows[j]["SessionId"].ToString());
                        if (i != j && DoorGroupId == DoorGroupIdC && SessionId == SessionIdC)
                        {
                            duplicate = 1;
                        }
                        if (DoorGroupId == Convert.ToInt32(ddlDoorgroup.SelectedValue) && SessionId == Convert.ToInt32(ddlSession.SelectedValue))
                        {
                            duplicate = 1;
                        }
                    }
                }
            }
            if (duplicate == 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Same Record Is Already Available')</script>");
                return false;
            }
            if (txtApplication_No.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation2", "<script language='javascript'>alert('Enter Access Group Name')</script>");
                return false;
            }
            return true;
        }

        protected void LnkAdd1_Click(object sender, EventArgs e)
        {
            if (ValidateChild())
            {
                int Id = 0;
                if (Request.QueryString["Id"] != null)
                {
                    Id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                }
                DataTable dt = (DataTable)ViewState["AccessLevel"];
                dt.Rows.Add(Id, 0, ddlDoorgroup.SelectedValue, ddlDoorgroup.SelectedItem.Text, ddlSession.SelectedValue, ddlSession.SelectedItem.Text);
                ViewState["AccessLevel"] = dt;
                FillChildGrid();
                ddlDoorgroup.ClearSelection();
                ddlSession.ClearSelection();
            }
        }

        protected void linkChildDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
            Label Ids = (Label)gvr.FindControl("lblChildId");
            Label DoorGroupId = (Label)gvr.FindControl("lblDoorGroupId");
            Label SessionId = (Label)gvr.FindControl("lblSessionId");
            if (Convert.ToInt32(Ids.Text) > 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Delete from Access_Level where Id= '" + Ids.Text + "'";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            DataTable dt = (DataTable)ViewState["AccessLevel"];
            DataRow[] dr = dt.Select("Door_GroupId = " + DoorGroupId.Text + " and SessionId = " + SessionId.Text + "");
            dt.Rows.Remove(dr[0]);
            ViewState["AccessLevel"] = dt;
            FillChildGrid();
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