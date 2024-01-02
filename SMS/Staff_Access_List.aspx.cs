using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace SMS
{
    public partial class Staff_Access_List : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
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

                    lnkAdd.Visible = false;
                    con.Open();
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandText = "select distinct S.Id, S.StaffId, s.Full_Name Staff_Name, stuff((select ',' + Access_Group_ID from Staff_Access_List where StaffId=sm.StaffId for xml path('')),1,1,'') as Access_Group_Id , stuff((select ',' + Cast(ID as Nvarchar(50)) from Staff_Access_List where StaffId=sm.StaffId for xml path('')),1,1,'') as A_Id, stuff((select ',' + Session_ID from Staff_Access_List where StaffId=sm.StaffId for xml path('')),1,1,'') as Session_ID from Staff_Access_List SM Right join tblstudent S on SM.StaffId=S.StaffId where S.StaffId= '" + Request.QueryString["Id"].ToString() + "'";
                    SqlCommand cmd = new SqlCommand("SP_GetStaffAccessData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Flag", "Edit");
                    cmd.Parameters.AddWithValue("@Search", Request.QueryString["Id"].ToString().Trim());
                    cmd.ExecuteNonQuery();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtApplication_No.Text = ds.Tables[0].Rows[0]["Staff_Id"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["Staff_Name"].ToString();
                        if (ds.Tables[0].Rows[0]["A_ID"].ToString() == "" || ds.Tables[0].Rows[0]["A_ID"].ToString() == "null")
                        {
                            lnkupdate.Visible = false;
                            lnksave.Visible = true;
                        }
                        else
                        {
                            lnkupdate.Visible = true;
                            lnksave.Visible = false;
                        }


                    }

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = "select * from tbl_Access_Group";
                    //con.Open();
                    cmd1.Connection = con;
                    cmd1.ExecuteNonQuery();


                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    String Door_Group_Id = ds.Tables[0].Rows[0]["Access_Group_Id"].ToString();
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

                    SqlCommand cmdsession = new SqlCommand();
                    cmdsession.CommandText = "select * from tbl_Student_Session";
                    //con.Open();
                    cmdsession.Connection = con;
                    cmdsession.ExecuteNonQuery();


                    SqlDataAdapter dasession = new SqlDataAdapter(cmdsession);

                    DataSet dssession = new DataSet();
                    String session_Id = ds.Tables[0].Rows[0]["session_Id"].ToString();
                    String[] session_Id_Array = session_Id.Split(',');
                    dasession.Fill(dssession);
                    if (dssession.Tables[0].Rows.Count > 0)
                    {
                        rptSession.DataSource = dssession;
                        rptSession.DataBind();
                        foreach (RepeaterItem item in rptSession.Items)
                        {
                            CheckBox chkSession = (CheckBox)item.FindControl("chkSession");
                            Label lblSession_Id = (Label)item.FindControl("lblSession_Id");
                            for (int i = 0; i < session_Id_Array.Length; i++)
                            {
                                if (lblSession_Id.Text == session_Id_Array[i].ToString())
                                {
                                    chkSession.Checked = true;
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
                    cmd.CommandText = "select * from tbl_Access_Group";
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
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.CommandText = "select * from tbl_Student_Session";

                    cmd3.Connection = con;
                    cmd3.ExecuteNonQuery();


                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);

                    DataSet ds3 = new DataSet();

                    da3.Fill(ds3);
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        rptSession.DataSource = ds3;
                        rptSession.DataBind();
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

                //SqlCommand cmd = new SqlCommand();

                ////cmd.CommandText = "select Distinct S.ID,s.Staff_Id,(s.FirstName +'' +s.FatherName) Staff_Name ,stuff ( ( select ','+ Access_Group_Name from Access_List a inner join tbl_Access_Group b on b.Id = a.Access_Group_ID where Staff_Id=S.Staff_Id for xml path('') ),1,1,'' ) Access_Group_ID from tblstudent s left join Access_List ac on ac.Staff_Id=s.Staff_Id";
                //cmd.CommandText = "select Distinct S.ID,s.Staff_Id,(s.FirstName +'' +s.FatherName) Staff_Name , stuff ( ( select ','+ Access_Group_Name from Access_List a inner join tbl_Access_Group b on b.Id = a.Access_Group_ID where Staff_Id=S.Staff_Id for xml path('') ),1,1,'' ) Access_Group_ID ,stuff ( ( select ','+ Session_Name from Access_List a inner join tbl_Student_Session b on b.Session_Id = a.Session_Id where Staff_Id=S.Staff_Id for xml path('') ),1,1,'' ) Session_Name from tblstudent s left join Access_List ac on ac.Staff_Id=s.Staff_Id";

                //cmd.Connection = con;

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlCommand cmd = new SqlCommand("SP_GetStaffAccessData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "GET");
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gridEmployee.DataSource = dv;
                }
                else
                {
                    gridEmployee.DataSource = dt;
                }

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
            Response.Redirect("Staff_Access_List.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            //string ids = "";
            //ids = string.Empty;
            //ids = (sender as LinkButton).CommandArgument;
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
            Label Ids = (Label)gvr.FindControl("lblApplication_No");
            Response.Redirect("Staff_Access_List.aspx?Id=" + Ids.Text + "");
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                SqlCommand cmd = new SqlCommand();
                string ReaderId = string.Empty;
                List<string> Reader_List = new List<string>();
                foreach (RepeaterItem item in RepeatReader.Items)
                {
                    CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                    Label lblId = (Label)item.FindControl("lblId");
                    if (chkReader.Checked)
                    {
                        //ReaderId = ReaderId + lblId.Text + ",";
                        //Reader_List.Add(lblId.Text);
                        cmd.CommandText = "INSERT INTO [Staff_Access_List] ([StaffId] ,[Staff_Name],[Access_Group_ID]) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "','" + lblId.Text + "')";

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    //ReaderId = String.Join(",", Reader_List.ToArray());

                }

                string SessionId = string.Empty;
                List<string> Session_List = new List<string>();
                foreach (RepeaterItem item in rptSession.Items)
                {
                    CheckBox chkSession = (CheckBox)item.FindControl("chkSession");
                    Label lblSession_Id = (Label)item.FindControl("lblSession_Id");
                    if (chkSession.Checked)
                    {
                        //ReaderId = ReaderId + lblId.Text + ",";
                        //Reader_List.Add(lblId.Text);

                        cmd.CommandText = "INSERT INTO [Staff_Access_List] ([StaffId] ,[Staff_Name],[Session_ID]) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "','" + lblSession_Id.Text + "')";
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    //ReaderId = String.Join(",", Reader_List.ToArray());

                }




                //SqlDataAdapter da = new SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();

                //da.Fill(ds);

                //gridEmployee.DataSource = ds;

                //gridEmployee.DataBind();
                //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);

                Response.Redirect("Staff_Access_List.aspx");

            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                string ReaderId = string.Empty;
                List<string> Reader_List = new List<string>();
                foreach (RepeaterItem item in RepeatReader.Items)
                {

                    CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                    Label lblId = (Label)item.FindControl("lblId");
                    if (chkReader.Checked)
                    {
                        //ReaderId = ReaderId + lblId.Text + ",";
                        //Reader_List.Add(lblId.Text);

                        SqlCommand cmd1 = new SqlCommand();

                        cmd1.CommandText = "select Count(*) A_Count from Staff_Access_List where StaffId='" + txtApplication_No.Text + "' and Access_Group_Id='" + lblId.Text + "'";
                        cmd1.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        if (ds.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                        {
                            cmd.CommandText = "INSERT INTO [Staff_Access_List] ([StaffId] ,[Staff_Name],[Access_Group_ID]) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "','" + lblId.Text + "')";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "UPDATE [Staff_Access_List] SET  [Access_Group_ID] = '" + lblId.Text + "' WHERE StaffId='" + Request.QueryString["Id"].ToString() + "' and [Access_Group_ID] = '" + lblId.Text + "'";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                        }
                        //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                    }
                    //ReaderId = String.Join(",", Reader_List.ToArray());
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand();

                        cmd1.CommandText = "select Count(*) A_Count from Staff_Access_List where StaffId='" + txtApplication_No.Text + "' and Access_Group_Id='" + lblId.Text + "'";
                        cmd1.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        if (ds.Tables[0].Rows[0]["A_Count"].ToString() != "0")
                        {
                            cmd.CommandText = "Delete from [Staff_Access_List] WHERE StaffId='" + Request.QueryString["Id"].ToString() + "' and [Access_Group_ID] = '" + lblId.Text + "'";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                        }


                    }
                }
                SqlCommand cmd3 = new SqlCommand();
                string SessionId = string.Empty;
                List<string> Session_List = new List<string>();
                foreach (RepeaterItem item in rptSession.Items)
                {

                    CheckBox chkSession = (CheckBox)item.FindControl("chkSession");
                    Label lblId = (Label)item.FindControl("lblSession_Id");
                    if (chkSession.Checked)
                    {
                        //ReaderId = ReaderId + lblId.Text + ",";
                        //Reader_List.Add(lblId.Text);

                        SqlCommand cmd1 = new SqlCommand();

                        cmd1.CommandText = "select Count(*) A_Count from Staff_Access_List where StaffId='" + txtApplication_No.Text + "' and Session_Id='" + lblId.Text + "'";
                        cmd1.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        if (ds.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                        {
                            cmd3.CommandText = "INSERT INTO [Staff_Access_List] ([StaffId] ,[Staff_Name],[Session_ID]) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "','" + lblId.Text + "')";
                            cmd3.Connection = con;
                            cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd3.CommandText = "UPDATE [Staff_Access_List] SET  [Session_ID] = '" + lblId.Text + "' WHERE StaffId='" + Request.QueryString["Id"].ToString() + "' and [Session_ID] = '" + lblId.Text + "'";
                            cmd3.Connection = con;
                            cmd3.ExecuteNonQuery();
                        }
                        //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                    }
                    //ReaderId = String.Join(",", Reader_List.ToArray());
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand();

                        cmd1.CommandText = "select Count(*) A_Count from Staff_Access_List where StaffId='" + txtApplication_No.Text + "' and Session_Id='" + lblId.Text + "'";
                        cmd1.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        if (ds.Tables[0].Rows[0]["A_Count"].ToString() != "0")
                        {
                            cmd3.CommandText = "Delete from [Staff_Access_List] WHERE StaffId='" + Request.QueryString["Id"].ToString() + "' and [Session_ID] = '" + lblId.Text + "'";
                            cmd3.Connection = con;
                            cmd3.ExecuteNonQuery();
                        }


                    }
                }
                con.Close();
                Response.Redirect("Staff_Access_List.aspx");
            }
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            //string ids = "";
            //ids = string.Empty;
            //ids = (sender as LinkButton).CommandArgument;
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
            Label Ids = (Label)gvr.FindControl("lblApplication_No");
            con.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Delete from Staff_Access_List where StaffId= '" + Ids.Text + "'";

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();


            FillGrid();
            Response.Redirect("Staff_Access_List.aspx");
            con.Close();
        }
        protected bool Validation()
        {
            string ReaderId = string.Empty;
            List<string> Reader_List = new List<string>();
            foreach (RepeaterItem item in RepeatReader.Items)
            {
                CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                Label lblId = (Label)item.FindControl("lblId");
                if (chkReader.Checked)
                {
                    ReaderId = ReaderId + lblId.Text + ",";
                    Reader_List.Add(lblId.Text);
                }
                ReaderId = String.Join(",", Reader_List.ToArray());

            }
            if (ReaderId == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please select Atlist one Access Group')</script>");
                return false;
            }


            //string SessionId = string.Empty;
            //List<string> Session_List = new List<string>();
            //foreach (RepeaterItem item in rptSession.Items)
            //{
            //    CheckBox chkSession = (CheckBox)item.FindControl("chkSession");
            //    Label lblSession_Id = (Label)item.FindControl("lblSession_Id");
            //    if (chkSession.Checked)
            //    {
            //        SessionId = SessionId + lblSession_Id.Text + ",";
            //        Session_List.Add(lblSession_Id.Text);


            //    }
            //    SessionId = String.Join(",", Session_List.ToArray());

            //}
            //if (SessionId == "")
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please select Atlist one Session')</script>");
            //    return false;
            //}

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Staff_Access_List.aspx");
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
    }
}