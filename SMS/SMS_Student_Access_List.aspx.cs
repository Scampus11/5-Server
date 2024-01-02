using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using SMS.Class;
using System.Web.Configuration;
using AjaxControlToolkit;

namespace SMS
{
    public partial class SMS_Student_Access_List : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        string studentIds = string.Empty;
        public static List<string> StudentList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            Session["Sider"] = "Student Access Master";
            lblAAG.Visible = false;
            lblAC.Visible = false;
            lblVal.Visible = false;
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Id"] != null)
                    {
                        BindCanteen2();
                        EditData();
                    }
                    else if (Request.QueryString["Insert"] != null)
                    {
                        ViewMode();
                    }
                    else
                    {
                        divgrid.Visible = true;
                        divView.Visible = false;
                        //BindCollege();
                        //BindDepartment();
                        BindAdmissionType();
                        BindCampus();
                        BindBatchYear();
                        BindAccessGrp();
                        BindCanteen();
                        BindBlockGroup();
                        FillGrid();
                    }
                    //if (Session["Role_Id"].ToString() == "1" || Session["Role_Id"].ToString() == "3" || Session["Role_Id"].ToString() == "2")
                    //{
                    //    divblock.Visible = true;
                    //    divBg.Visible = true;
                    //    gridEmployee.Columns[11].Visible = true;
                    //}
                    //else
                    //{
                    //    divblock.Visible = false;
                    //    divBg.Visible = false;
                    //    gridEmployee.Columns[11].Visible = false;
                    //}
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
                SqlCommand cmd = new SqlCommand("SP_GetStudentAccessData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                cmd.Parameters.AddWithValue("@College", ddlCollege.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Campus", ddlCampus.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BatchYear", ddlBatchYear.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@AdmissionTye", ddlAdmissiontype.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Gender", drpGender.SelectedValue.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lnkStudent.Visible = true;
                    lnkRemoveAll.Visible = true;
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
                        CheckBox chkStudent = row.FindControl("chkStudent") as CheckBox;
                        LinkButton linkDelete = row.FindControl("linkDelete") as LinkButton;
                        SqlCommand cmd1 = new SqlCommand("CheckBulkAG", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Flag", "Student");
                        cmd1.Parameters.AddWithValue("@Role_Id", Session["Role_Id"].ToString().Trim());
                        cmd1.Parameters.AddWithValue("@Staff_Id", Session["Staff_Id"].ToString().Trim());
                        cmd1.Connection = con;
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable ds1 = new DataTable();
                        da1.Fill(ds1);
                        for (int i = 0; i < ds1.Rows.Count; i++)
                        {
                            if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
                            {
                                lnkStudent.Visible = true;
                                lnkRemoveAll.Visible = true;
                                chkStudent.Enabled = true;
                                linkDelete.Visible = true;
                                divStudentCanteens.Visible = true;
                                divStudentAG.Visible = true;
                                divStudentBG.Visible = true;
                            }
                            else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                            {
                                lnkStudent.Visible = false;
                                lnkRemoveAll.Visible = false;
                                chkStudent.Enabled = false;
                                linkDelete.Visible = false;
                                divStudentCanteens.Visible = true;
                                divStudentAG.Visible = true;
                                divStudentBG.Visible = true;
                            }
                            else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                            {
                                lnkStudent.Visible = true;
                                lnkRemoveAll.Visible = true;
                                chkStudent.Enabled = true;
                                linkDelete.Visible = true;
                                divStudentCanteens.Visible = true;
                                divStudentAG.Visible = true;
                                divStudentBG.Visible = false;
                            }
                            else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
                            {
                                lnkStudent.Visible = true;
                                lnkRemoveAll.Visible = true;
                                chkStudent.Enabled = true;
                                linkDelete.Visible = true;
                                divStudentCanteens.Visible = true;
                                divStudentAG.Visible = false;
                                divStudentBG.Visible = true;
                            }
                            else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
                            {
                                lnkStudent.Visible = true;
                                lnkRemoveAll.Visible = true;
                                chkStudent.Enabled = true;
                                linkDelete.Visible = true;
                                divStudentCanteens.Visible = false;
                                divStudentAG.Visible = true;
                                divStudentBG.Visible = true;
                            }
                            else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                            {
                                lnkStudent.Visible = true;
                                lnkRemoveAll.Visible = true;
                                chkStudent.Enabled = true;
                                linkDelete.Visible = true;
                                divStudentCanteens.Visible = true;
                                divStudentAG.Visible = false;
                                divStudentBG.Visible = false;
                            }
                            else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                            {
                                lnkStudent.Visible = true;
                                lnkRemoveAll.Visible = true;
                                chkStudent.Enabled = true;
                                linkDelete.Visible = true;
                                divStudentCanteens.Visible = false;
                                divStudentAG.Visible = true;
                                divStudentBG.Visible = false;
                            }
                            else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
                            {
                                lnkStudent.Visible = true;
                                lnkRemoveAll.Visible = true;
                                chkStudent.Enabled = true;
                                linkDelete.Visible = true;
                                divStudentCanteens.Visible = false;
                                divStudentAG.Visible = false;
                                divStudentBG.Visible = true;
                            }
                        }

                    }
                }
                else
                {
                    gridEmployee.DataSource = null;
                    gridEmployee.DataBind();
                    lnkStudent.Visible = false;
                    lnkRemoveAll.Visible = false;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Records not Founds!!')</script>");
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
            Response.Redirect("SMS_Student_Access_List.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
            Label Ids = (Label)gvr.FindControl("lblApplication_No");
            Response.Redirect("SMS_Student_Access_List.aspx?Id=" + Ids.Text + "");
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
                            cmd.CommandText = "INSERT INTO [Access_List] ([StudentId] ,[Student_Name],[Access_Group_ID]) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "','" + lblId.Text + "')";
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    string SessionId = string.Empty;
                    List<string> Session_List = new List<string>();
                    foreach (RepeaterItem item in rptSession.Items)
                    {
                        CheckBox chkSession = (CheckBox)item.FindControl("chkSession");
                        Label lblSession_Id = (Label)item.FindControl("lblSession_Id");
                        if (chkSession.Checked)
                        {
                            cmd.CommandText = "INSERT INTO [Access_List] ([StudentId] ,[Student_Name],[Session_ID]) VALUES ('" + txtApplication_No.Text + "','" + txtDescription.Text + "','" + lblSession_Id.Text + "')";
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    string BlockGroupId = string.Empty;
                    List<string> BlockGroup_List = new List<string>();
                    foreach (RepeaterItem item in RepeatBlock.Items)
                    {
                        CheckBox chkBlockGroup = (CheckBox)item.FindControl("chkBlockGroup");
                        Label lblId = (Label)item.FindControl("lblId");
                        if (chkBlockGroup.Checked)
                        {
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Connection = con;
                            cmd2.Parameters.AddWithValue("@Flag", "BlockGroupInsert");
                            cmd2.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd2.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                            cmd2.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    if (ddlcanteen2.SelectedValue != "0")
                    {
                        SqlCommand cmd5 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                        cmd5.CommandType = CommandType.StoredProcedure;
                        cmd5.Connection = con;
                        cmd5.Parameters.AddWithValue("@Flag", "Count");
                        cmd5.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                        cmd5.Parameters.AddWithValue("@AGID", ddlcanteen2.SelectedValue.Trim());
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd5);
                        DataSet ds1 = new DataSet();

                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                        {
                            SqlCommand cmd6 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                            cmd6.CommandType = CommandType.StoredProcedure;
                            cmd6.Connection = con;
                            cmd6.Parameters.AddWithValue("@Flag", "Insert");
                            cmd6.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd6.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                            cmd6.Parameters.AddWithValue("@AGID", ddlcanteen2.SelectedValue.Trim());
                            cmd6.Parameters.AddWithValue("@AssignBy", Session["UserName"].ToString());
                            con.Open();
                            cmd6.ExecuteNonQuery();
                            con.Close();

                        }
                        else
                        {
                            SqlCommand cmd7 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                            cmd7.CommandType = CommandType.StoredProcedure;
                            cmd7.Connection = con;
                            cmd7.Parameters.AddWithValue("@Flag", "Update");
                            cmd7.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd7.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                            cmd7.Parameters.AddWithValue("@AGID", ddlcanteen2.SelectedValue.Trim());
                            cmd7.Parameters.AddWithValue("@AssignBy", Session["UserName"].ToString());
                            con.Open();
                            cmd7.ExecuteNonQuery();
                            con.Close();

                        }
                    }

                    Response.Redirect("SMS_Student_Access_List.aspx");
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
                    string ReaderId = string.Empty;
                    List<string> Reader_List = new List<string>();
                    foreach (RepeaterItem item in RepeatReader.Items)
                    {
                        CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                        Label lblId = (Label)item.FindControl("lblId");
                        if (chkReader.Checked)
                        {
                            SqlCommand cmd1 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Connection = con;
                            cmd1.Parameters.AddWithValue("@Flag", "Count");
                            cmd1.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd1.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();

                            da1.Fill(ds1);
                            if (ds1.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                            {
                                SqlCommand cmd2 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Connection = con;
                                cmd2.Parameters.AddWithValue("@Flag", "Insert");
                                cmd2.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                                cmd2.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                                cmd2.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                cmd2.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmd3 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.Connection = con;
                                cmd3.Parameters.AddWithValue("@Flag", "Update");
                                cmd3.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                                cmd3.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                                cmd3.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                cmd3.ExecuteNonQuery();
                            }
                            ReaderId = String.Join(",", Reader_List.ToArray());
                        }
                        else
                        {
                            chkReader.Checked = false;
                            SqlCommand cmd8 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                            cmd8.CommandType = CommandType.StoredProcedure;
                            cmd8.Connection = con;
                            cmd8.Parameters.AddWithValue("@Flag", "Delete");
                            cmd8.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd8.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                            cmd8.ExecuteNonQuery();
                        }
                    }
                    if (ddlcanteen2.SelectedValue != "0")
                    {
                        SqlCommand cmd5 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                        cmd5.CommandType = CommandType.StoredProcedure;
                        cmd5.Connection = con;
                        cmd5.Parameters.AddWithValue("@Flag", "Count");
                        cmd5.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                        cmd5.Parameters.AddWithValue("@AGID", ddlcanteen2.SelectedValue.Trim());
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd5);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                        {
                            SqlCommand cmd6 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                            cmd6.CommandType = CommandType.StoredProcedure;
                            cmd6.Connection = con;
                            cmd6.Parameters.AddWithValue("@Flag", "Insert");
                            cmd6.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd6.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                            cmd6.Parameters.AddWithValue("@AGID", ddlcanteen2.SelectedValue.Trim());
                            cmd6.Parameters.AddWithValue("@AssignBy", Session["UserName"].ToString());
                            cmd6.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd7 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                            cmd7.CommandType = CommandType.StoredProcedure;
                            cmd7.Connection = con;
                            cmd7.Parameters.AddWithValue("@Flag", "Update");
                            cmd7.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd7.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                            cmd7.Parameters.AddWithValue("@AGID", ddlcanteen2.SelectedValue.Trim());
                            cmd7.Parameters.AddWithValue("@AssignBy", Session["UserName"].ToString());
                            cmd7.ExecuteNonQuery();
                        }
                    }
                    else
                    {

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "Delete AL from Access_List AL inner join tbl_Access_Group AG on AG.ID=AL.Access_Group_Id  where AL.StudentId= '" + Request.QueryString["Id"].ToString().Trim() + "' and AG.Is_Canteen=1";
                        cmd.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        cmd.ExecuteNonQuery();
                    }
                    string BlockGroupId = string.Empty;
                    List<string> BlockGroup_List = new List<string>();
                    foreach (RepeaterItem item in RepeatBlock.Items)
                    {
                        CheckBox chkBlockGroup = (CheckBox)item.FindControl("chkBlockGroup");
                        Label lblId = (Label)item.FindControl("lblId");
                        if (chkBlockGroup.Checked)
                        {
                            SqlCommand cmd1 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Connection = con;
                            cmd1.Parameters.AddWithValue("@Flag", "BlockGroupCount");
                            cmd1.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd1.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();

                            da1.Fill(ds1);
                            if (ds1.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                            {
                                SqlCommand cmd2 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Connection = con;
                                cmd2.Parameters.AddWithValue("@Flag", "BlockGroupInsert");
                                cmd2.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                                cmd2.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                                cmd2.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                cmd2.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmd3 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.Connection = con;
                                cmd3.Parameters.AddWithValue("@Flag", "BlockGroupUpdate");
                                cmd3.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                                cmd3.Parameters.AddWithValue("@StudentName", txtDescription.Text.Trim());
                                cmd3.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                cmd3.ExecuteNonQuery();
                            }
                            ReaderId = String.Join(",", Reader_List.ToArray());
                        }
                        else
                        {
                            chkBlockGroup.Checked = false;
                            SqlCommand cmd8 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                            cmd8.CommandType = CommandType.StoredProcedure;
                            cmd8.Connection = con;
                            cmd8.Parameters.AddWithValue("@Flag", "BlockGroupDelete");
                            cmd8.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                            cmd8.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                            cmd8.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                    StudentList.Add(txtApplication_No.Text);
                    Response.Redirect("SMS_Student_Access_List.aspx");
                    //myModal.Attributes.Add("style", "display:block;");
                    //Label1.Text = "Student AGs updated !!";
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
                cmd.CommandText = "Delete from Access_List where StudentId= '" + Ids.Text.Trim() + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                FillGrid();
                Response.Redirect("SMS_Student_Access_List.aspx");
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
                lblAAG.Visible = false;
                lblAC.Visible = false;
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
                if (diveditcanteens.Visible == true || diveditAG.Visible == true)
                {
                    if (ReaderId == "" && ddlcanteen2.SelectedValue == "0")
                    {
                        lblAAG.Visible = true;
                        lblAC.Visible = true;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return true;
        }
        protected bool Validation2()
        {
            try
            {
                //if (divStudentCanteens.Visible == true)
                //{
                lblValchk.Visible = false;
                string chkstudentID = "";
                foreach (GridViewRow row in gridEmployee.Rows)
                {
                    CheckBox chkStudent = (CheckBox)row.FindControl("chkStudent");
                    if (chkStudent.Checked == true)
                    {
                        chkstudentID = "1";

                        lblAAG.Visible = false;
                        lblAC.Visible = false;
                        string ReaderId = string.Empty;
                        List<string> Reader_List = new List<string>();
                        foreach (RepeaterItem item in rptAccessgrp.Items)
                        {
                            CheckBox chkReader = (CheckBox)item.FindControl("chkAccessGrp");
                            Label lblId = (Label)item.FindControl("lblAccessGrp");
                            if (chkReader.Checked)
                            {
                                ReaderId = ReaderId + lblId.Text + ",";
                                Reader_List.Add(lblId.Text);
                            }
                            ReaderId = String.Join(",", Reader_List.ToArray());

                        }
                        if (divStudentCanteens.Visible == true || divStudentAG.Visible == true)
                        {
                            if (ReaderId == "" && ddlcanteen.SelectedValue == "0")
                            {
                                lblVal.Visible = true;

                                return false;
                            }
                        }
                    }
                }
                if (chkstudentID == "")
                {
                    lblValchk.Visible = true;
                    return false;
                }
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Student_Access_List.aspx");
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
        protected void ddlCollege_TextChanged(object sender, EventArgs e)
        {
            BindDepartment();
            //FillGrid();
        }
        protected void College()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindCollegeData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Campus", ddlCampus.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlCollege.DataSource = ds1;
                ddlCollege.DataTextField = "Name";
                ddlCollege.DataValueField = "Id";
                ddlCollege.DataBind();
                ddlCollege.Items.Insert(0, new ListItem("--Select College--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindDepartment()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindDepartmentData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@College", ddlCollege.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlDepartment.DataSource = ds1;
                ddlDepartment.DataTextField = "Name";
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void EditData()
        {
            try
            {
                divgrid.Visible = false;
                divView.Visible = true;
                lnkAdd.Visible = false;
                //Access Group
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_GetAccess_List_Edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtApplication_No.Text = ds.Tables[0].Rows[0]["StudentId"].ToString();
                    txtDescription.Text = ds.Tables[0].Rows[0]["Student_Name"].ToString();
                    CheckCanteensAGBGAccessforUser(ds.Tables[0].Rows[0]["A_ID"].ToString());

                }
                //Non Canteen
                SqlCommand cmd1 = new SqlCommand("SP_GetAccess_List_nonCanteen_Edit", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Connection = con;
                cmd1.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
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
                //Is Canteen
                SqlCommand cmd3 = new SqlCommand("SP_GetAccess_List_IsCanteen_Edit", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Connection = con;
                cmd3.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                cmd3.ExecuteNonQuery();
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    ddlcanteen2.ClearSelection();
                    ddlcanteen2.Items.FindByValue(ds3.Tables[0].Rows[0]["Access_Group_Id"].ToString()).Selected = true;
                }

                //Block Group
                SqlCommand cmdBlock = new SqlCommand("Bind_BlockGroup", con);
                cmdBlock.CommandType = CommandType.StoredProcedure;
                cmdBlock.Connection = con;
                cmdBlock.ExecuteNonQuery();
                SqlDataAdapter daBlock = new SqlDataAdapter(cmdBlock);
                DataSet dsBlock = new DataSet();
                String BlockgroupId = ds.Tables[0].Rows[0]["BlockId"].ToString();
                String[] BlockgroupIdArray = BlockgroupId.Split(',');
                daBlock.Fill(dsBlock);
                if (dsBlock.Tables[0].Rows.Count > 0)
                {
                    RepeatBlock.DataSource = dsBlock;
                    RepeatBlock.DataBind();
                    foreach (RepeaterItem item in RepeatBlock.Items)
                    {
                        CheckBox chkBlockGroup = (CheckBox)item.FindControl("chkBlockGroup");
                        Label lblId = (Label)item.FindControl("lblId");
                        for (int i = 0; i < BlockgroupIdArray.Length; i++)
                        {
                            if (lblId.Text == BlockgroupIdArray[i].ToString())
                            {
                                chkBlockGroup.Checked = true;
                            }
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void ViewMode()
        {
            try
            {
                divgrid.Visible = false;
                divView.Visible = true;
                lnkupdate.Visible = false;
                lnksave.Visible = true;
                lnkAdd.Visible = false;

                BindAccessGrp();
                BindBlockGroup();
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
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void CheckCanteensAGBGAccessforUser(string AD)
        {
            SqlCommand cmd1 = new SqlCommand("CheckBulkAG", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Flag", "Student");
            cmd1.Parameters.AddWithValue("@Role_Id", Session["Role_Id"].ToString().Trim());
            cmd1.Parameters.AddWithValue("@Staff_Id", Session["Staff_Id"].ToString().Trim());
            cmd1.Connection = con;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            for (int i = 0; i < ds1.Rows.Count; i++)
            {
                if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
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
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = true;
                    diveditBG.Visible = true;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                {
                    lnkupdate.Visible = false;
                    lnksave.Visible = false;
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = true;
                    diveditBG.Visible = true;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
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
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = true;
                    diveditBG.Visible = false;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
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
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = false;
                    diveditBG.Visible = true;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
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
                    diveditcanteens.Visible = false;
                    diveditAG.Visible = true;
                    diveditBG.Visible = true;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
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
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = false;
                    diveditBG.Visible = false;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
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
                    diveditcanteens.Visible = false;
                    diveditAG.Visible = true;
                    diveditBG.Visible = false;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
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
                    diveditcanteens.Visible = false;
                    diveditAG.Visible = false;
                    diveditBG.Visible = true;
                }
            }

        }
        protected void ddlDepartment_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void BindAccessGrp()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from tbl_Access_Group where Is_Canteen!=1";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (divgrid.Visible == false)
                    {
                        RepeatReader.DataSource = ds;
                        RepeatReader.DataBind();
                    }
                    else
                    {
                        rptAccessgrp.DataSource = ds;
                        rptAccessgrp.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void BindBlockGroup()
        {
            try
            {
                con.Open();
                SqlCommand cmdBlock = new SqlCommand("Bind_BlockGroup", con);
                cmdBlock.CommandType = CommandType.StoredProcedure;
                cmdBlock.Connection = con;
                cmdBlock.ExecuteNonQuery();
                SqlDataAdapter daBlock = new SqlDataAdapter(cmdBlock);
                DataSet dsBlock = new DataSet();
                daBlock.Fill(dsBlock);
                if (dsBlock.Tables[0].Rows.Count > 0)
                {
                    if (divgrid.Visible == false)
                    {
                        RepeatBlock.DataSource = dsBlock;
                        RepeatBlock.DataBind();
                    }
                    else
                    {
                        rptBulkBlockGroup.DataSource = dsBlock;
                        rptBulkBlockGroup.DataBind();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindCanteen()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from tbl_Access_Group where Is_Canteen=1";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcanteen.DataSource = ds;
                    ddlcanteen.DataTextField = "Access_Group_Name";
                    ddlcanteen.DataValueField = "Id";
                    ddlcanteen.DataBind();
                    ddlcanteen.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
                }
                else
                {
                    ddlcanteen.DataSource = null;
                    ddlcanteen.DataBind();
                    ddlcanteen.Items.Insert(0, new ListItem("--Select Canteen--", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindCanteen2()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from tbl_Access_Group where Is_Canteen=1";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcanteen2.DataSource = ds;
                    ddlcanteen2.DataTextField = "Access_Group_Name";
                    ddlcanteen2.DataValueField = "Id";
                    ddlcanteen2.DataBind();
                    ddlcanteen2.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
                }
                else
                {
                    ddlcanteen2.DataSource = null;
                    ddlcanteen2.DataBind();
                    ddlcanteen2.Items.Insert(0, new ListItem("--Select Canteen--", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void chkALLStudent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkALLStudent = (CheckBox)gridEmployee.HeaderRow.FindControl("chkALLStudent");
                foreach (GridViewRow row in gridEmployee.Rows)
                {
                    CheckBox chkStudent = (CheckBox)row.FindControl("chkStudent");
                    if (chkALLStudent.Checked == true)
                    {
                        chkStudent.Checked = true;
                    }
                    else
                    {
                        chkStudent.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkStudent_Click(object sender, EventArgs e)
        {
            if (Validation2())
            {
                try
                {
                    con.Open();
                    foreach (GridViewRow row in gridEmployee.Rows)
                    {
                        Label lblApplication_No = (Label)row.FindControl("lblApplication_No");
                        Label lblStudent_Name = (Label)row.FindControl("lblStudent_Name");
                        CheckBox chkStudent = (CheckBox)row.FindControl("chkStudent");
                        if (chkStudent.Checked == true)
                        {
                            StudentList.Add(lblApplication_No.Text);
                            SqlCommand cmd = new SqlCommand();
                            string ReaderId = string.Empty;
                            List<string> Reader_List = new List<string>();
                            foreach (RepeaterItem item in rptAccessgrp.Items)
                            {

                                CheckBox chkAccessGrp = (CheckBox)item.FindControl("chkAccessGrp");
                                Label lblId = (Label)item.FindControl("lblAccessGrp");
                                if (chkAccessGrp.Checked)
                                {
                                    SqlCommand cmd1 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Connection = con;
                                    cmd1.Parameters.AddWithValue("@Flag", "Count");
                                    cmd1.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                    cmd1.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                    DataSet ds1 = new DataSet();

                                    da1.Fill(ds1);
                                    if (ds1.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                                    {
                                        SqlCommand cmd2 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                        cmd2.CommandType = CommandType.StoredProcedure;
                                        cmd2.Connection = con;
                                        cmd2.Parameters.AddWithValue("@Flag", "Insert");
                                        cmd2.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                        cmd2.Parameters.AddWithValue("@StudentName", lblStudent_Name.Text.Trim());
                                        cmd2.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                        cmd2.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        SqlCommand cmd3 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        cmd3.Connection = con;
                                        cmd3.Parameters.AddWithValue("@Flag", "Update");
                                        cmd3.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                        cmd3.Parameters.AddWithValue("@StudentName", lblStudent_Name.Text.Trim());
                                        cmd3.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                        cmd3.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    SqlCommand cmd8 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                    cmd8.CommandType = CommandType.StoredProcedure;
                                    cmd8.Connection = con;
                                    cmd8.Parameters.AddWithValue("@Flag", "Delete");
                                    cmd8.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                    cmd8.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                    cmd8.ExecuteNonQuery();

                                }
                            }
                            SqlCommand cmdBlockGp = new SqlCommand();
                            string BlockGpId = string.Empty;
                            List<string> BlockGp_List = new List<string>();
                            foreach (RepeaterItem item in rptBulkBlockGroup.Items)
                            {

                                CheckBox chkAccessGrp = (CheckBox)item.FindControl("chkBulkBlockGroup");
                                Label lblId = (Label)item.FindControl("lblBulkBlockGroup");
                                if (chkAccessGrp.Checked)
                                {
                                    SqlCommand cmd1 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Connection = con;
                                    cmd1.Parameters.AddWithValue("@Flag", "BlockGroupCount");
                                    cmd1.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                    cmd1.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                    DataSet ds1 = new DataSet();

                                    da1.Fill(ds1);
                                    if (ds1.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                                    {
                                        SqlCommand cmd2 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                        cmd2.CommandType = CommandType.StoredProcedure;
                                        cmd2.Connection = con;
                                        cmd2.Parameters.AddWithValue("@Flag", "BlockGroupInsert");
                                        cmd2.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                        cmd2.Parameters.AddWithValue("@StudentName", lblStudent_Name.Text.Trim());
                                        cmd2.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                        cmd2.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        SqlCommand cmd3 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        cmd3.Connection = con;
                                        cmd3.Parameters.AddWithValue("@Flag", "BlockGroupUpdate");
                                        cmd3.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                        cmd3.Parameters.AddWithValue("@StudentName", lblStudent_Name.Text.Trim());
                                        cmd3.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                        cmd3.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    SqlCommand cmd8 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                    cmd8.CommandType = CommandType.StoredProcedure;
                                    cmd8.Connection = con;
                                    cmd8.Parameters.AddWithValue("@Flag", "BlockGroupDelete");
                                    cmd8.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                    cmd8.Parameters.AddWithValue("@AGID", lblId.Text.Trim());
                                    cmd8.ExecuteNonQuery();

                                }
                            }
                            if (ddlcanteen.SelectedValue != "0")
                            {
                                SqlCommand cmd5 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                                cmd5.CommandType = CommandType.StoredProcedure;
                                cmd5.Connection = con;
                                cmd5.Parameters.AddWithValue("@Flag", "Count");
                                cmd5.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                cmd5.Parameters.AddWithValue("@AGID", ddlcanteen.SelectedValue.Trim());
                                SqlDataAdapter da1 = new SqlDataAdapter(cmd5);
                                DataSet ds1 = new DataSet();

                                da1.Fill(ds1);
                                if (ds1.Tables[0].Rows[0]["A_Count"].ToString() == "0")
                                {
                                    SqlCommand cmd6 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                                    cmd6.CommandType = CommandType.StoredProcedure;
                                    cmd6.Connection = con;
                                    cmd6.Parameters.AddWithValue("@Flag", "Insert");
                                    cmd6.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                    cmd6.Parameters.AddWithValue("@StudentName", lblStudent_Name.Text.Trim());
                                    cmd6.Parameters.AddWithValue("@AGID", ddlcanteen.SelectedValue.Trim());
                                    cmd6.Parameters.AddWithValue("@AssignBy", Session["UserName"].ToString());
                                    cmd6.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmd7 = new SqlCommand("SP_GetAccess_List_IsCanteen_Count", con);
                                    cmd7.CommandType = CommandType.StoredProcedure;
                                    cmd7.Connection = con;
                                    cmd7.Parameters.AddWithValue("@Flag", "Update");
                                    cmd7.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                    cmd7.Parameters.AddWithValue("@StudentName", lblStudent_Name.Text.Trim());
                                    cmd7.Parameters.AddWithValue("@AGID", ddlcanteen.SelectedValue.Trim());
                                    cmd7.Parameters.AddWithValue("@AssignBy", Session["UserName"].ToString());
                                    cmd7.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            studentIds = String.Join(",", StudentList.ToArray());
                            //chkStudent.Checked = false;
                        }
                    }
                    con.Close();
                    FillGrid();
                    lblmsg.Visible = true;
                    lblmsg1.Visible = false;
                    //myModal.Attributes.Add("style", "display:block;");
                    //Label1.Text = "Student AGs updated !!";
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

        protected void ddlCampus_TextChanged(object sender, EventArgs e)
        {
            College();
            //FillGrid();
        }

        protected void BindCampus()
        {
            try
            {
                DataTable dtCampus = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_GetStudentCollege", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "GetCampus");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtCampus);
                if (dtCampus.Rows.Count > 0)
                {
                    ddlCampus.DataSource = dtCampus;
                    ddlCampus.DataTextField = "Name";
                    ddlCampus.DataValueField = "Id";
                    ddlCampus.DataBind();
                    ddlCampus.Items.Insert(0, new ListItem("-- Select Campus --", "0"));
                }
                else
                {
                    ddlCampus.DataSource = null;
                    ddlCampus.DataBind();
                    ddlCampus.Items.Insert(0, new ListItem("-- Select Campus --", "0"));
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindBatchYear()
        {
            try
            {
                DataTable dtBatchYear = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_GetStudentCollege", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "GetBatchYear");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtBatchYear);
                if (dtBatchYear.Rows.Count > 0)
                {
                    ddlBatchYear.DataSource = dtBatchYear;
                    ddlBatchYear.DataTextField = "Name";
                    ddlBatchYear.DataValueField = "Id";
                    ddlBatchYear.DataBind();
                    ddlBatchYear.Items.Insert(0, new ListItem("-- Select BatchYear --", "0"));
                }
                else
                {
                    ddlBatchYear.DataSource = null;
                    ddlBatchYear.DataBind();
                    ddlBatchYear.Items.Insert(0, new ListItem("-- Select BatchYear --", "0"));
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindAdmissionType()
        {
            try
            {
                DataTable dtBatchYear = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_GetStudentCollege", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "GetAdmissionType");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtBatchYear);
                if (dtBatchYear.Rows.Count > 0)
                {
                    ddlAdmissiontype.DataSource = dtBatchYear;
                    ddlAdmissiontype.DataTextField = "Name";
                    ddlAdmissiontype.DataValueField = "Id";
                    ddlAdmissiontype.DataBind();
                    ddlAdmissiontype.Items.Insert(0, new ListItem("-- Select AdmissionType --", "0"));
                }
                else
                {
                    ddlAdmissiontype.DataSource = null;
                    ddlAdmissiontype.DataBind();
                    ddlAdmissiontype.Items.Insert(0, new ListItem("-- Select AdmissionType --", "0"));
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void ddlBatchYear_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
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
                        Label lblStudent_Name = (Label)row.FindControl("lblStudent_Name");
                        CheckBox chkStudent = (CheckBox)row.FindControl("chkStudent");
                        if (chkStudent.Checked == true)
                        {

                            SqlCommand cmd = new SqlCommand();
                            string ReaderId = string.Empty;
                            List<string> Reader_List = new List<string>();
                            foreach (RepeaterItem item in rptAccessgrp.Items)
                            {
                                con.Open();
                                CheckBox chkAccessGrp = (CheckBox)item.FindControl("chkAccessGrp");
                                Label lblId = (Label)item.FindControl("lblAccessGrp");
                                if (chkAccessGrp.Checked)
                                {
                                    cmd.CommandText = "Delete FROM [Access_List] WHERE [StudentId]='" + lblApplication_No.Text.Trim() + "' and Access_Group_ID='" + lblId.Text.Trim() + "'";
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                }
                                con.Close();
                            }
                            foreach (RepeaterItem item in rptBulkBlockGroup.Items)
                            {
                                CheckBox chkBulkBlockGroup = (CheckBox)item.FindControl("chkBulkBlockGroup");
                                Label lblBulkBlockGroup = (Label)item.FindControl("lblBulkBlockGroup");
                                if (chkBulkBlockGroup.Checked)
                                {
                                    con.Open();
                                    SqlCommand cmd8 = new SqlCommand("SP_GetAccess_List_NonCanteen_Count", con);
                                    cmd8.CommandType = CommandType.StoredProcedure;
                                    cmd8.Connection = con;
                                    cmd8.Parameters.AddWithValue("@Flag", "BlockGroupDelete");
                                    cmd8.Parameters.AddWithValue("@StudentId", lblApplication_No.Text.Trim());
                                    cmd8.Parameters.AddWithValue("@AGID", lblBulkBlockGroup.Text.Trim());
                                    cmd8.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            con.Open();
                            cmd.CommandText = "Delete FROM [Access_List] WHERE [StudentId]='" + lblApplication_No.Text + "' and Access_Group_ID='" + ddlcanteen.SelectedValue.Trim() + "'";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            chkStudent.Checked = false;
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
            //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Remove Successfully!');", true);

        }

        protected void lnkadvanceSearch_Click(object sender, EventArgs e)
        {
            if (divAdvanceSearch.Visible == false)
            {
                divAdvanceSearch.Visible = true;
                //FillGrid();
            }
            else
            {
                divAdvanceSearch.Visible = false;
            }
        }

        protected void ddlAdmissiontype_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void drpGender_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void lnkpushCancel_Click(object sender, EventArgs e)
        {
            myModal.Attributes.Add("style", "display:none;");
            Response.Redirect("SMS_Student_Access_List.aspx");
        }
        protected void lnkGo_Click(object sender, EventArgs e)
        {
            foreach (var itemName in StudentList)
            {

                string studentIdName = itemName;
                SqlCommand cmd = new SqlCommand();
                string AccesslevelList = "";
                string uncheckedAccesslevelList = "";
                AccesslevelList += "<ACCESSLEVEL>Gate Barrier AL</ACCESSLEVEL>";
                AccesslevelList += "<ACCESSLEVEL>Natural Library AL</ACCESSLEVEL>";
                foreach (RepeaterItem item in rptAccessgrp.Items)
                {
                    CheckBox chkAccessGrp = (CheckBox)item.FindControl("chkAccessGrp");
                    Label lblId = (Label)item.FindControl("lblAccessGrp");
                    if (chkAccessGrp.Checked)
                    {
                        AccesslevelList += "<ACCESSLEVEL>" + chkAccessGrp.Text + "</ACCESSLEVEL>";
                    }
                    else
                    {
                        uncheckedAccesslevelList += "<ACCESSLEVEL><ACCESSLEVELNAME>" + chkAccessGrp.Text + "</ACCESSLEVELNAME><DELETE>1</DELETE></ACCESSLEVEL>";
                    }
                }
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
                if (Session["Sessionid"] == null)
                {
                    if (uncheckedAccesslevelList != "")
                    {
                        CheckCreditionals(studentIdName, uncheckedAccesslevelList);
                    }
                    CheckCreditionals(studentIdName, AccesslevelList);
                }
                else
                {
                    if (uncheckedAccesslevelList != "")
                    {
                        S2API.AddStudentAccesslevelAPI(studentIdName, uncheckedAccesslevelList);
                    }
                    string ResponseMsg = S2API.AddStudentAccesslevelAPI(studentIdName, AccesslevelList);
                    myModal.Attributes.Add("style", "display:block;");
                    Label1.Text = ResponseMsg;
                    lnkOkk.Visible = true;
                    lblS2.Visible = false;
                    lnkGo.Visible = false;
                    lnkpushCancel.Visible = false;
                }
            }
        }
        public void CheckCreditionals(string studentIdName, string AccesslevelList)
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
                    string ResponseMsg = S2API.AddStudentAccesslevelAPI(studentIdName, AccesslevelList);
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
        #region Filter Drp List
        protected void lnkFilterlist_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
        #endregion

        #region Clear Filter
        protected void lnkClearFilter_Click(object sender, EventArgs e)
        {
            ddlCampus.SelectedValue = "0";
            ddlCollege.SelectedValue = "0";
            ddlDepartment.SelectedValue = "0";
            ddlBatchYear.SelectedValue = "0";
            ddlAdmissiontype.SelectedValue = "0";
            drpGender.SelectedValue = "0";
        }
        #endregion
    }
}