using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using SMS.Class;
using System.Web.Configuration;
namespace SMS
{
    public partial class SMS_Access_Group_Master : System.Web.UI.Page
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
            Session["Sider"]= "Access Group Master";
            if (!Page.IsPostBack)
            {
                try { 
                //System.Threading.Thread.Sleep(5000);
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
                        if (ds.Tables[0].Rows[0]["Is_Canteen"].ToString() == "1")
                        {
                            chkcanteen.Checked = true;
                            DivCanteenType.Visible = true;
                            if (ds.Tables[0].Rows[0]["CanteenType"].ToString() == "In Kind")
                            {
                                
                                RdbInkind.Checked = true;
                            }
                            else
                            {
                                RdbIncash.Checked = true;
                            }
                        }
                        else
                        {
                            chkcanteen.Checked = false;
                            DivCanteenType.Visible = false;
                        }
                        
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
                SqlCommand cmd = new SqlCommand("SP_GetAccessGroupData", con);
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
            Response.Redirect("SMS_Access_Group_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("SMS_Access_Group_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try { 
                lblValiaccessgroupname.Visible = false;
                int canteen = 0;
                if (chkcanteen.Checked == true)
                {
                    canteen = 1;
                }
                string CanteenType="";
                if (RdbInkind.Checked)
                {
                    CanteenType = "InKind";
                }
                else
                {
                    CanteenType = "InCase";
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO [tbl_Access_Group] ([Access_Group_Name] ,[Description],Is_Canteen,CanteenType) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "','" + canteen + "','" + CanteenType + "')";
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
                Response.Redirect("SMS_Access_Group_Master.aspx");
                ViewState["AccessLevel"] = null;
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
                lblValiaccessgroupname.Visible = false;
                int canteen = 0;
                if (chkcanteen.Checked == true)
                {
                    canteen = 1;
                }
                string CanteenType = "";
                if (RdbInkind.Checked)
                {
                    CanteenType = "In Kind";
                }
                else
                {
                    CanteenType = "In Cash";
                }
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE [tbl_Access_Group] SET [Access_Group_Name] = '" + txtApplication_No.Text + "',[Description] = '" + txtDescription.Text + "',Is_Canteen='" + canteen + "',CanteenType='" + CanteenType + "' WHERE Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";

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
                Response.Redirect("SMS_Access_Group_Master.aspx");
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

            SqlCommand cmd = new SqlCommand("SP_Delete_AG", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ID", Ids.Text.Trim());
            //cmd.CommandText = "Delete from tbl_Access_Group where Id= '" + Ids.Text + "'";

            //cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();


            FillGrid();
            Response.Redirect("SMS_Access_Group_Master.aspx");
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
                lblValiaccessgroupname.Visible = true;
                txtApplication_No.Focus();
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Access_Group_Master.aspx");
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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Same Record Is Already Available')</script>");
                lblAlreadyValid.Visible = true;
                lblValiaccessgroupname.Visible = false;
                return false;
            }
            if (txtApplication_No.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation2", "<script language='javascript'>alert('Enter Access Group Name')</script>");
                lblValiaccessgroupname.Visible = true;
                lblAlreadyValid.Visible = false;
                return false;
            }
            return true;
        }

        protected void LnkAdd1_Click(object sender, EventArgs e)
        {
            if (ValidateChild())
            {
                try { 
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
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected void linkChildDelete_Click(object sender, EventArgs e)
        {
            try { 
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
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
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

        protected void chkcanteen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcanteen.Checked == true)
            {
                DivCanteenType.Visible = true;
                RdbInkind.Checked = true;
            }
            else
            {
                DivCanteenType.Visible = false;
            }
        }
    }
}