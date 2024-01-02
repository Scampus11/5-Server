using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SMS.Class;
using System.Web.Configuration;
using System.IO;
using AjaxControlToolkit;

namespace SMS
{
    public partial class SMS_Staff_Access_List : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        string staffIds = string.Empty;
        public static List<string> StaffList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Sider"] = "Staff Access Master";
            if (!Page.IsPostBack)
            {
                try
                {
                    Session["Canteen_Flag"] = null;
                    Session["Session_Id"] = null;
                    Session["CanteenName"] = null;
                    Session["CanteenCount"] = null;
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
                            CheckAG(ds.Tables[0].Rows[0]["A_ID"].ToString());
                        }

                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandText = "select * from tbl_Access_Group where Is_Canteen='0'";
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
                        BindAG();
                        con.Open();
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.CommandText = "select * from tbl_Student_Session";
                        cmd3.Connection = con;
                        cmd3.ExecuteNonQuery();
                        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                        con.Close();
                        DataSet ds3 = new DataSet();
                        da3.Fill(ds3);
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            rptSession.DataSource = ds3;
                            rptSession.DataBind();
                        }
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
                SqlCommand cmd = new SqlCommand("SP_GetStaffAccessData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "GET");
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue.Trim());
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
                foreach (GridViewRow row in gridEmployee.Rows)
                {
                    //CheckBox chkALLStaff = row.FindControl("chkALLStaff") as CheckBox;
                    CheckBox chkStaff = row.FindControl("chkStaff") as CheckBox;
                    LinkButton linkDelete = row.FindControl("linkDelete") as LinkButton;
                    SqlCommand cmd1 = new SqlCommand("CheckBulkAG", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Flag", "Staff");
                    cmd1.Parameters.AddWithValue("@Role_Id", Session["Role_Id"].ToString().Trim());
                    cmd1.Parameters.AddWithValue("@Staff_Id", Session["Staff_Id"].ToString().Trim());
                    cmd1.Connection = con;
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable ds1 = new DataTable();
                    da1.Fill(ds1);
                    for (int i = 0; i < ds1.Rows.Count; i++)
                    {
                        if (ds1.Rows[i]["StaffAG"].ToString().Trim() == "1")
                        {
                            lnkStaffupdate.Visible = true;
                            lnkRemoveAll.Visible = true;
                      //      chkALLStaff.Enabled = false;
                            chkStaff.Enabled = true;
                            linkDelete.Visible = true;
                        }
                        else
                        {
                            lnkStaffupdate.Visible = false;
                            lnkRemoveAll.Visible = false;
                        //    chkALLStaff.Enabled = false;
                            chkStaff.Enabled = false;
                            linkDelete.Visible = false;
                        }
                    }

                }
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
            Response.Redirect("SMS_Staff_Access_List.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            //string ids = "";
            //ids = string.Empty;
            //ids = (sender as LinkButton).CommandArgument;
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
            Label Ids = (Label)gvr.FindControl("lblApplication_No");
            Response.Redirect("SMS_Staff_Access_List.aspx?Id=" + Ids.Text + "");
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
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

                    Response.Redirect("SMS_Staff_Access_List.aspx");
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
                try
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
                                cmd.CommandText = "Delete from [Staff_Access_List] WHERE StaffId='" + Request.QueryString["Id"].ToString() + "' and [Access_Group_ID] = '" + lblId.Text + "'Update officer_Master set Canteen_ID='' where Staff_Id='" + Request.QueryString["Id"].ToString().Trim() + "'";
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
                                cmd3.CommandText = "Delete from [Staff_Access_List] WHERE StaffId='" + Request.QueryString["Id"].ToString() + "' and [Session_ID] = '" + lblId.Text + "'Update officer_Master set Canteen_ID='' where Staff_Id='" + Request.QueryString["Id"].ToString().Trim() + "'";
                                cmd3.Connection = con;
                                cmd3.ExecuteNonQuery();
                            }


                        }
                    }
                    con.Close();
                    StaffList.Add(txtApplication_No.Text);
                    Response.Redirect("SMS_Staff_Access_List.aspx");
                    //myModal.Attributes.Add("style", "display:block;");
                    //Label1.Text = "Staff AGs updated !!";
                    //lnkOkk.Visible = false;
                    //lblS2.Visible = true;
                    //lnkGo.Visible = true;
                    //lnkpushCancel.Visible = true;
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
                Label Ids = (Label)gvr.FindControl("lblApplication_No");
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "Delete from Staff_Access_List where StaffId= '" + Ids.Text + "'Update officer_Master set Canteen_ID='' where Staff_Id='" + Ids.Text.Trim() + "'";

                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();


                FillGrid();
                Response.Redirect("SMS_Staff_Access_List.aspx");
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected bool Validation()
        {
            try
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
                    lblerrorAG.Visible = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Staff_Access_List.aspx");
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
        protected void lnkpushCancel_Click(object sender, EventArgs e)
        {
            myModal.Attributes.Add("style", "display:none;");
            Response.Redirect("SMS_Staff_Access_List.aspx");
        }
        protected void lnkGo_Click(object sender, EventArgs e)
        {
            foreach (var itemName in StaffList)
            {
                string staffIdName = itemName;
                SqlCommand cmd = new SqlCommand();
                string AccesslevelList = "";
                string uncheckedAccesslevelList = "";
                AccesslevelList += "<ACCESSLEVEL>Gate Barrier AL</ACCESSLEVEL>";
                AccesslevelList += "<ACCESSLEVEL>Natural Library AL</ACCESSLEVEL>";
                foreach (RepeaterItem item in RepeatReader.Items)
                {
                    CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                    Label lblId = (Label)item.FindControl("lblId");
                    if (chkReader.Checked)
                    {
                        AccesslevelList += "<ACCESSLEVEL>" + chkReader.Text + "</ACCESSLEVEL>";
                    }
                    else
                    {
                        uncheckedAccesslevelList += "<ACCESSLEVEL><ACCESSLEVELNAME>" + chkReader.Text + "</ACCESSLEVELNAME><DELETE>1</DELETE></ACCESSLEVEL>";
                    }
                }
                foreach (RepeaterItem item in RptAG.Items)
                {
                    CheckBox chkReader = (CheckBox)item.FindControl("chkAG");
                    Label lblId = (Label)item.FindControl("lblId1");
                    if (chkReader.Checked)
                    {
                        AccesslevelList += "<ACCESSLEVEL>" + chkReader.Text + "</ACCESSLEVEL>";
                    }
                    else
                    {
                        uncheckedAccesslevelList += "<ACCESSLEVEL><ACCESSLEVELNAME>" + chkReader.Text + "</ACCESSLEVELNAME><DELETE>1</DELETE></ACCESSLEVEL>";
                    }
                }

                if (Session["Sessionid"] == null)
                {
                    if (uncheckedAccesslevelList != "")
                    {
                        CheckCreditionals(staffIdName, uncheckedAccesslevelList);
                    }
                    CheckCreditionals(staffIdName, AccesslevelList);
                }
                else
                {
                    if (uncheckedAccesslevelList != "")
                    {
                        S2API.AddStaffAccesslevelAPI(staffIdName, uncheckedAccesslevelList);
                    }
                    string ResponseMsg = S2API.AddStaffAccesslevelAPI(staffIdName, AccesslevelList);
                    myModal.Attributes.Add("style", "display:block;");
                    Label1.Text = ResponseMsg;
                    lnkOkk.Visible = true;
                    lblS2.Visible = false;
                    lnkGo.Visible = false;
                    lnkpushCancel.Visible = false;
                }
            }
        }
        public void CheckCreditionals(string staffIdName, string AccesslevelList)
        {
            ModalPopupExtender ModalPopupExtenderAPIs = (ModalPopupExtender)Page.Master.FindControl("ModalPopupExtenderAPIs");
            SqlCommand cmd = new SqlCommand("SPS2loginAPI", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Get");
            cmd.Parameters.AddWithValue("@IpAddress", "");
            cmd.Parameters.AddWithValue("@Username", "");
            cmd.Parameters.AddWithValue("@password", "");
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count != 0)
            {
                string responsemessage = S2API.LoginAPI(ds.Rows[0]["IPAddress"].ToString(), ds.Rows[0]["Username"].ToString(), S2API.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", ds.Rows[0]["Password"].ToString()), "");
                if (responsemessage.ToUpper() == "SUCCESS")
                {
                    string ResponseMsg = S2API.AddStaffAccesslevelAPI(staffIdName, AccesslevelList);
                    myModal.Attributes.Add("style", "display:block;");
                    Label1.Text = ResponseMsg;
                    lnkOkk.Visible = true;
                    lblS2.Visible = false;
                    lnkGo.Visible = false;
                    lnkpushCancel.Visible = false;
                }
                else
                {
                    TextBox txtipaddress = (TextBox)Page.Master.FindControl("txtipaddress");
                    TextBox txtusername = (TextBox)Page.Master.FindControl("txtusername");
                    TextBox txtapipassword = (TextBox)Page.Master.FindControl("txtapipassword");
                    txtipaddress.Text = ds.Rows[0]["IPAddress"].ToString();
                    txtusername.Text = ds.Rows[0]["Username"].ToString();
                    txtapipassword.Text = "";
                    ModalPopupExtenderAPIs.Show();
                }
            }
            else
            {
                ModalPopupExtenderAPIs.Show();
            }
        }
        protected void lnkadvanceSearch_Click(object sender, EventArgs e)
        {
            if (divAdvanceSearch.Visible == false)
            {
                divAdvanceSearch.Visible = true;
                BindDepartment();
                BindBulkAG();
                //SqlCommand cmd1 = new SqlCommand("CheckBulkAG", con);
                //cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@Flag", "Staff");
                //cmd1.Parameters.AddWithValue("@Role_Id", Session["Role_Id"].ToString().Trim());
                //cmd1.Parameters.AddWithValue("@Staff_Id", Session["Staff_Id"].ToString().Trim());
                //cmd1.Connection = con;
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                //DataTable ds1 = new DataTable();
                //da1.Fill(ds1);
                //for (int i = 0; i < ds1.Rows.Count; i++)
                //{
                //    if (ds1.Rows[i]["StaffAG"].ToString().Trim() == "1")
                //    {
                //        lnkStaffupdate.Visible = true;
                //        lnkRemoveAll.Visible = true;
                //    }
                //    else
                //    {
                //        lnkStaffupdate.Visible = false;
                //        lnkRemoveAll.Visible = false;
                //    }
                //}
            }
            else
            {
                divAdvanceSearch.Visible = false;
            }
        }

        protected void ddlDepartment_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void BindDepartment()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BindEmpDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    ddlDepartment.DataSource = ds1;
                    ddlDepartment.DataTextField = "Name";
                    ddlDepartment.DataValueField = "Id";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));
                }
                else
                {
                    ddlDepartment.DataSource = null;
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void chkALLStaff_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkALLStaff = (CheckBox)gridEmployee.HeaderRow.FindControl("chkALLStaff");
                foreach (GridViewRow row in gridEmployee.Rows)
                {
                    CheckBox chkStaff = (CheckBox)row.FindControl("chkStaff");
                    if (chkALLStaff.Checked == true)
                    {
                        chkStaff.Checked = true;
                    }
                    else
                    {
                        chkStaff.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkStaffupdate_Click(object sender, EventArgs e)
        {
            if (Validation2())
            {
                try
                {
                    
                    foreach (GridViewRow row in gridEmployee.Rows)
                    {
                        con.Open();
                        Label lblApplication_No = (Label)row.FindControl("lblApplication_No");
                        Label lblStudent_Name = (Label)row.FindControl("lblStudent_Name");
                        CheckBox chkStaff = (CheckBox)row.FindControl("chkStaff");
                        if (chkStaff.Checked == true)
                        {
                            StaffList.Add(lblApplication_No.Text);
                            SqlCommand cmd = new SqlCommand();
                            string ReaderId = string.Empty;
                            List<string> Reader_List = new List<string>();
                            foreach (RepeaterItem item in RptAG.Items)
                            {

                                CheckBox chkReader = (CheckBox)item.FindControl("chkAG");
                                Label lblId = (Label)item.FindControl("lblId1");
                                if (chkReader.Checked)
                                {
                                    SqlCommand cmd1 = new SqlCommand();
                                    cmd1.CommandText = "select Count(*) A_Count from Staff_Access_List where StaffId='" + lblApplication_No.Text + "' and Access_Group_Id='" + lblId.Text + "'";
                                    cmd1.Connection = con;
                                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    if (ds.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                                    {
                                        cmd.CommandText = "INSERT INTO [Staff_Access_List] ([StaffId] ,[Staff_Name],[Access_Group_ID]) VALUES ('" + lblApplication_No.Text + "','" + txtDescription.Text + "','" + lblId.Text + "')";
                                        cmd.Connection = con;
                                        cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        cmd.CommandText = "UPDATE [Staff_Access_List] SET  [Access_Group_ID] = '" + lblId.Text + "' WHERE StaffId='" + lblApplication_No.Text + "' and [Access_Group_ID] = '" + lblId.Text + "'";
                                        cmd.Connection = con;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    SqlCommand cmd1 = new SqlCommand();
                                    cmd1.CommandText = "select Count(*) A_Count from Staff_Access_List where StaffId='" + lblApplication_No.Text + "' and Access_Group_Id='" + lblId.Text + "'";
                                    cmd1.Connection = con;
                                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    if (ds.Tables[0].Rows[0]["A_Count"].ToString() != "0")
                                    {
                                        cmd.CommandText = "Delete from [Staff_Access_List] WHERE StaffId='" + lblApplication_No.Text + "' and [Access_Group_ID] = '" + lblId.Text + "'Update officer_Master set Canteen_ID='' where Staff_Id='" + lblApplication_No.Text + "'";
                                        cmd.Connection = con;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            

                        }
                        else
                        {
                            staffIds = String.Join(",", StaffList.ToArray());
                        }
                        con.Close();
                    }
                    lblmsg.Visible = true;
                    Response.Redirect("SMS_Staff_Access_List.aspx");
                    //myModal.Attributes.Add("style", "display:block;");
                    //Label1.Text = "Staff AGs updated !!";
                    //lnkOkk.Visible = false;
                    //lblS2.Visible = true;
                    //lnkGo.Visible = true;
                    //lnkpushCancel.Visible = true;
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected void lnkRemoveAll_Click(object sender, EventArgs e)
        {
            if (Validation2())
            {
                try
                {
                    foreach (GridViewRow row in gridEmployee.Rows)
                    {
                        Label lblApplication_No = (Label)row.FindControl("lblApplication_No");
                        Label lblStaff_Id = (Label)row.FindControl("lblStaff_Id");
                        CheckBox chkStaff = (CheckBox)row.FindControl("chkStaff");
                        if (chkStaff.Checked == true)
                        {

                            SqlCommand cmd = new SqlCommand();
                            string ReaderId = string.Empty;
                            List<string> Reader_List = new List<string>();
                            foreach (RepeaterItem item in RptAG.Items)
                            {
                                con.Open();
                                CheckBox chkAccessGrp = (CheckBox)item.FindControl("chkAG");
                                Label lblId = (Label)item.FindControl("lblId1");
                                if (chkAccessGrp.Checked)
                                {
                                    cmd.CommandText = "Delete from Staff_Access_List where StaffId= '" + lblApplication_No.Text + "'Update officer_Master set Canteen_ID='' where Staff_Id='" + lblApplication_No.Text.Trim() + "'";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                }
                                con.Close();
                            }
                        }
                        else
                        {
                            chkStaff.Checked = false;
                        }

                    }
                    FillGrid();
                    lblmsg1.Visible = true;
                    lblmsg.Visible = false;
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }
        protected bool Validation2()
        {
            try
            {
                lblValchk.Visible = false;
                string chkstaffids = "";
                foreach (GridViewRow row in gridEmployee.Rows)
                {
                    CheckBox chkStaff = (CheckBox)row.FindControl("chkStaff");
                    if (chkStaff.Checked == true)
                    {
                        chkstaffids = "1";
                        string ReaderId = string.Empty;
                        List<string> Reader_List = new List<string>();
                        foreach (RepeaterItem item in RptAG.Items)
                        {
                            CheckBox chkReader = (CheckBox)item.FindControl("chkAG");
                            Label lblId = (Label)item.FindControl("lblId1");
                            if (chkReader.Checked)
                            {
                                ReaderId = ReaderId + lblId.Text + ",";
                                Reader_List.Add(lblId.Text);
                            }
                            ReaderId = String.Join(",", Reader_List.ToArray());

                        }
                        if (ReaderId == "")
                        {
                            lblAG.Visible = true;
                            return false;
                        }
                    }
                }
                if (chkstaffids == "")
                {
                    lblValchk.Visible = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return true;
        }
        protected void BindAG()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from tbl_Access_Group where Is_Canteen='0'";
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RepeatReader.DataSource = ds;
                RepeatReader.DataBind();
            }

        }
        protected void BindBulkAG()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from tbl_Access_Group where Is_Canteen='0'";
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RptAG.DataSource = ds;
                RptAG.DataBind();
            }

        }
        protected void CheckAG(string AD)
        {
            SqlCommand cmd1 = new SqlCommand("CheckBulkAG", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Flag", "Staff");
            cmd1.Parameters.AddWithValue("@Role_Id", Session["Role_Id"].ToString().Trim());
            cmd1.Parameters.AddWithValue("@Staff_Id", Session["Staff_Id"].ToString().Trim());
            cmd1.Connection = con;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            for (int i = 0; i < ds1.Rows.Count; i++)
            {
                if (ds1.Rows[i]["StaffAG"].ToString().Trim() == "1")
                {
                    if (AD == "" || AD == "null")
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
                else
                {
                    lnkupdate.Visible = false;
                    lnksave.Visible = false;
                }
            }
        }
    }
}