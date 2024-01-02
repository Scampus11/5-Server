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
using System.Drawing;
using ClosedXML.Excel;
using SMS.Class;
using System.Web.Configuration;
using System.Globalization;
using AjaxControlToolkit;
using SMS.CommonClass;
using System.Data.OleDb;
using System.Data.Common;
using SMS.BussinessLayer;

namespace SMS
{
    public partial class Student_Master : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        LogFile logFile = new LogFile();
        BS BS = new BS();
        String line = "";
        string Image = "";
        string Photopath = "";
        string filePath = "";
        byte[] bytes = new byte[1];
        byte[] bytes2 = new byte[1];
        string base64string = "";
        string Signaturebase64string = "";
        string SignatureImage = "";
        string SignaturePhotopath = "";
        string SignaturefilePath = "";
        SqlConnection con = new SqlConnection();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!Page.IsPostBack)
            {
                pageload();
            }
        }
        protected void pageload()
        {
            try
            {

                Session["Canteen_Flag"] = null;
                Session["Session_Id"] = null;
                Session["CanteenName"] = null;
                Session["CanteenCount"] = null;
                Session["Sider"] = "Student Master";
                BindYear();
                BindScampus();
                BindLockerHostel();
                Program();
                DegreeType();
                AdmissionType();
                if (Request.QueryString["StudentID"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lblStudentID.Text = Request.QueryString["StudentID"].ToString();
                    ViewData();
                }
                else if (Request.QueryString["Edit"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = false;
                    DivEdit.Visible = true;
                    lnkupdate.Visible = true;
                    lnksave.Visible = false;
                    lblStudentID.Text = Request.QueryString["Edit"].ToString();
                    txtStudentId.Text = Request.QueryString["Edit"].ToString().Trim();
                    EditData();
                }
                else if (Request.QueryString["Add"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = false;
                    DivEdit.Visible = true;
                    lnkupdate.Visible = false;
                    lnksave.Visible = true;
                }
                else
                {
                    divgrid.Visible = true;
                    divView.Visible = false;
                    DivEdit.Visible = false;
                    FillGrid();
                }
                if (Session["Role_Id"].ToString() == "1" || Session["Role_Id"].ToString() == "3" || Session["Role_Id"].ToString() == "2")
                {
                    divHostel.Visible = true;
                    liHostel.Visible = true;
                    gridEmployee.Columns[9].Visible = true;
                }
                else
                {
                    divHostel.Visible = false;
                    liHostel.Visible = false;
                    gridEmployee.Columns[9].Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }

        }
        protected void BindYear()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindYear_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlYear.DataSource = ds1;
                ddlYear.DataTextField = "Name";
                ddlYear.DataValueField = "Id";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("--Select Year--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindScampus()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindScampus_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlCampus.DataSource = ds1;
                ddlCampus.DataTextField = "Name";
                ddlCampus.DataValueField = "Id";
                ddlCampus.DataBind();
                ddlCampus.Items.Insert(0, new ListItem("--Select Scampus Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void ViewData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_Student_Getdata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", Request.QueryString["StudentID"].ToString());

                //cmd.CommandText = "select * from tblstudent where StudentID= '" + lblStudentID.Text + "'";

                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblFirstName.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    lblFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    lblGrandFatherName.Text = ds.Tables[0].Rows[0]["GrandFatherName"].ToString();
                    lblGender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
                    lblDateOfBirth.Text = ds.Tables[0].Rows[0]["DateOfBirth"].ToString();
                    lblSignature.Text = ds.Tables[0].Rows[0]["Signature"].ToString();
                    lblCollege.Text = ds.Tables[0].Rows[0]["College"].ToString();
                    lblDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                    lblCampus.Text = ds.Tables[0].Rows[0]["Campus"].ToString();
                    lblProgram.Text = ds.Tables[0].Rows[0]["Program"].ToString();
                    lblDegreeType.Text = ds.Tables[0].Rows[0]["DegreeType"].ToString();
                    lblAdmissionType.Text = ds.Tables[0].Rows[0]["AdmissionType"].ToString();
                    lblAdmissionTypeShort.Text = ds.Tables[0].Rows[0]["AdmissionTypeShort"].ToString();
                    lblValidDateUntil.Text = ds.Tables[0].Rows[0]["ValidDateUntil"].ToString();
                    lblIssueDate.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();
                    lblMealNumber.Text = ds.Tables[0].Rows[0]["MealNumber"].ToString();
                    lblUniqueNo.Text = ds.Tables[0].Rows[0]["UniqueNo"].ToString();
                    lblStatus.Text = ds.Tables[0].Rows[0]["Status"].ToString();
                    lblIsactive.Text = ds.Tables[0].Rows[0]["Isactive"].ToString();
                    lblid.Text = ds.Tables[0].Rows[0]["id"].ToString();
                    lblUNIQUEID.Text = ds.Tables[0].Rows[0]["UNIQUEID"].ToString();
                    DivEdit.Visible = false;
                    if (ds.Tables[0].Rows[0]["StudentImages_Blob"].ToString() != "")
                    {
                        byte[] hash1 = (byte[])ds.Tables[0].Rows[0]["StudentImages_Blob"];
                        image1.ImageUrl = "data:image;base64," + Convert.ToBase64String(hash1);
                    }
                    if (ds.Tables[0].Rows[0]["StudentImages"].ToString() != "")
                    {
                        image2.ImageUrl = "~/Images/images1.jpg";
                    }
                    if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "1")
                    {
                        lblCardStatus.Text = "Active";
                    }
                    else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "2")
                    {
                        lblCardStatus.Text = "Revoked";
                    }
                    else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "3")
                    {
                        lblCardStatus.Text = "Lost";
                    }
                    else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "4")
                    {
                        lblCardStatus.Text = "Suspended";
                    }
                    else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "5")
                    {
                        lblCardStatus.Text = "Expired";
                    }
                    else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "6")
                    {
                        lblCardStatus.Text = "missing Active";
                    }
                    else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "7")
                    {
                        lblCardStatus.Text = "Graduate";
                    }
                    else
                    {
                        lblCardStatus.Text = "Active";
                    }
                    //image1.ImageUrl = ds.Tables[0].Rows[0]["StudentImages_Byte"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        #region Edit Method
        protected void EditData()
        {
            try
            {
                txtStudentId.Enabled = false;
                SqlCommand cmd = new SqlCommand("SP_SMS_Student_Getdata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", Request.QueryString["Edit"].ToString());
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtfirstname.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    txtFathername.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    txtGrandfathername.Text = ds.Tables[0].Rows[0]["GrandFatherName"].ToString();
                    drpGender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                    txtDateOfBirth.Text = ds.Tables[0].Rows[0]["DateOfBirth"].ToString();
                    ddlCampus.ClearSelection();
                    ddlCampus.Items.FindByValue(ds.Tables[0].Rows[0]["Campus"].ToString()).Selected = true;
                    College();
                    ddlCollege.ClearSelection();
                    ddlCollege.Items.FindByValue(ds.Tables[0].Rows[0]["College"].ToString()).Selected = true;
                    BindDepartment();
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["Department"].ToString()).Selected = true;
                    ddlYear.ClearSelection();
                    ddlYear.Items.FindByValue(ds.Tables[0].Rows[0]["Batch_Year"].ToString()).Selected = true;
                    ddlProgram.ClearSelection();
                    ddlProgram.Items.FindByValue(ds.Tables[0].Rows[0]["Program"].ToString()).Selected = true;
                    ddlDegreeType.ClearSelection();
                    ddlDegreeType.Items.FindByValue(ds.Tables[0].Rows[0]["DegreeType"].ToString()).Selected = true;
                    ddlAdmissionType.ClearSelection();
                    ddlAdmissionType.Items.FindByValue(ds.Tables[0].Rows[0]["AdmissionType"].ToString()).Selected = true;
                    AdmissionTypeShort();
                    ddlAdmissionTypeShort.ClearSelection();
                    ddlAdmissionTypeShort.Items.FindByValue(ds.Tables[0].Rows[0]["AdmissionTypeShort"].ToString()).Selected = true;
                    txtValidDateUntil.Text = ds.Tables[0].Rows[0]["ValidDateUntil"].ToString();
                    txtIssueDate.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();
                    txtMealNumber.Text = ds.Tables[0].Rows[0]["MealNumber"].ToString();

                    if (ds.Tables[0].Rows[0]["StudentImages"].ToString() == "")
                    {
                        image2.ImageUrl = "~/Images/images1.jpg";
                    }
                    else
                    {
                        hdnimage.Value = ds.Tables[0].Rows[0]["Image64byte"].ToString();
                        string path = Server.MapPath(ds.Tables[0].Rows[0]["StudentImages"].ToString());
                        if (Checkpath.CheckPathExitsOrNot(path))
                        {
                            image2.ImageUrl = ds.Tables[0].Rows[0]["StudentImages"].ToString();
                        }
                        else
                        {
                            image2.ImageUrl = "~/Images/images1.jpg";
                        }
                    }
                    if (ds.Tables[0].Rows[0]["Signature"].ToString() == "")
                    {
                        imgSignature.ImageUrl = "~/Images/images1.jpg";
                    }
                    else
                    {
                        imgSignature.ImageUrl = ds.Tables[0].Rows[0]["Signature"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DigitalStatus"].ToString() == "1")
                    {
                        chkdigitalStatus.Checked = true;
                    }
                    else
                    {
                        chkdigitalStatus.Checked = false;
                    }
                    txtUniqueNo.Text = ds.Tables[0].Rows[0]["UniqueNo"].ToString();
                    //DivEdit.Visible = false;
                    txtCardNumber.Text = ds.Tables[0].Rows[0]["Cardid"].ToString();
                    ddlCardstatus.ClearSelection();
                    ddlCardstatus.Items.FindByValue(ds.Tables[0].Rows[0]["Cardstatus"].ToString()).Selected = true;
                    if (ds.Tables[0].Rows[0]["HostelId"].ToString() == "0" || ds.Tables[0].Rows[0]["HostelId"].ToString() == "")
                    {
                        chkRoomLockerassgnment.Checked = false;
                        divMainHostel.Visible = false;
                    }
                    else
                    {
                        chkRoomLockerassgnment.Checked = true;
                        divMainHostel.Visible = true;
                        BindLockerHostel();
                        ddlLockerHostelName.ClearSelection();
                        ddlLockerHostelName.Items.FindByValue(ds.Tables[0].Rows[0]["HostelId"].ToString()).Selected = true;
                        BindLockerBlock();
                        ddlLockerBlockName.ClearSelection();
                        ddlLockerBlockName.Items.FindByValue(ds.Tables[0].Rows[0]["BlockId"].ToString()).Selected = true;
                        BindLockerFloor();
                        ddllockerFloorName.ClearSelection();
                        ddllockerFloorName.Items.FindByValue(ds.Tables[0].Rows[0]["FloorId"].ToString()).Selected = true;
                        BindlockerRoom();
                        ddlLockerRoomNumber.ClearSelection();
                        ddlLockerRoomNumber.Items.FindByValue(ds.Tables[0].Rows[0]["RoomId"].ToString()).Selected = true;
                        BindLockerBed();
                        ddlBedNumber.ClearSelection();
                        ddlBedNumber.Items.FindByValue(ds.Tables[0].Rows[0]["BedId"].ToString()).Selected = true;
                        BindLocker();
                        ddlLocker.ClearSelection();
                        ddlLocker.Items.FindByValue(ds.Tables[0].Rows[0]["LockerId"].ToString()).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        #endregion

        #region GetData
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                // SqlCommand cmd = new SqlCommand();
                SqlCommand cmd = new SqlCommand("SP_GetStudentData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select * from tblstudent WHERE StudentID LIKE '%' + @Search + '%' or FirstName LIKE '%' + @Search + '%' OR DateOfBirth LIKE '%' + @Search + '%' OR College LIKE '%' + @Search + '%' OR cardid LIKE '%' + @Search + '%' OR Department  LIKE '%' + @Search + '%'";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);

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
        #endregion
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string DigitalStatus = "0";
                if (chkdigitalStatus.Checked)
                {
                    DigitalStatus = "1";
                }
                else
                {
                    DigitalStatus = "0";
                }
                if (lnksave.Visible == true)
                {
                    if (Validation())
                    {
                        lblValidstudent.Visible = false;
                        lblvalidfirstname.Visible = false;
                        ImageUpload();
                        SqlCommand cmd = new SqlCommand("SP_SMS_Student_upset", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Insert");
                        cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);
                        cmd.Parameters.AddWithValue("@firstname", txtfirstname.Text);
                        cmd.Parameters.AddWithValue("@Fathername", txtFathername.Text);
                        cmd.Parameters.AddWithValue("@Grandfathername", txtGrandfathername.Text);
                        cmd.Parameters.AddWithValue("@Gender", drpGender.SelectedValue);
                        cmd.Parameters.AddWithValue("@DateOfBirth", txtDateOfBirth.Text);
                        cmd.Parameters.AddWithValue("@College", ddlCollege.SelectedValue);
                        cmd.Parameters.AddWithValue("@CollegeText", ddlCollege.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue);
                        cmd.Parameters.AddWithValue("@DepartmentText", ddlDepartment.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Campus", ddlCampus.SelectedValue);
                        cmd.Parameters.AddWithValue("@CampusText", ddlCampus.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Program", ddlProgram.SelectedValue);
                        cmd.Parameters.AddWithValue("@ProgramText", ddlProgram.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@DegreeType", ddlDegreeType.SelectedValue);
                        cmd.Parameters.AddWithValue("@DegreeTypeText", ddlDegreeType.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@AdmissionType", ddlAdmissionType.SelectedValue);
                        cmd.Parameters.AddWithValue("@AdmissionTypeText", ddlAdmissionType.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@AdmissionTypeShort", ddlAdmissionTypeShort.SelectedValue);
                        cmd.Parameters.AddWithValue("@AdmissionTypeShortText", ddlAdmissionTypeShort.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@ValidDateUntil", txtValidDateUntil.Text);
                        cmd.Parameters.AddWithValue("@IssueDate", txtIssueDate.Text);
                        cmd.Parameters.AddWithValue("@MealNumber", txtMealNumber.Text);
                        cmd.Parameters.AddWithValue("@UniqueNo", txtUniqueNo.Text);
                        cmd.Parameters.AddWithValue("@Cardstatus", ddlCardstatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@hash", bytes);
                        cmd.Parameters.AddWithValue("@CardNumber", txtCardNumber.Text);
                        cmd.Parameters.AddWithValue("@ImagePath", Image);
                        cmd.Parameters.AddWithValue("@Photopath", Photopath);
                        cmd.Parameters.AddWithValue("@base64", base64string);
                        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
                        cmd.Parameters.AddWithValue("@YearText", ddlYear.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Signaturebase64", Signaturebase64string);
                        cmd.Parameters.AddWithValue("@DigitalStatus", DigitalStatus);
                        cmd.Parameters.AddWithValue("@Signature", SignatureImage);
                        cmd.Parameters.AddWithValue("@AssignBy", Session["UserName"].ToString());
                        if (chkRoomLockerassgnment.Checked == true)
                        {
                            if (HostelValidation())
                            {
                                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                                cmd.Parameters.AddWithValue("@BlockId", ddlLockerBlockName.SelectedValue);
                                cmd.Parameters.AddWithValue("@FloorId", ddllockerFloorName.SelectedValue);
                                cmd.Parameters.AddWithValue("@RoomId", ddlLockerRoomNumber.SelectedValue);
                                cmd.Parameters.AddWithValue("@BedId", ddlBedNumber.SelectedValue);
                                cmd.Parameters.AddWithValue("@LockerId", ddlLocker.SelectedValue);
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                Response.Redirect("Student_Master.aspx");
                                con.Close();
                            }

                            if (ddlLockerHostelName.SelectedValue != "0" && ddlLockerBlockName.SelectedValue != "0" && ddllockerFloorName.SelectedValue != "0"
                                 && ddlLockerRoomNumber.SelectedValue != "0" && ddlBedNumber.SelectedValue != "0" && ddlLocker.SelectedValue != "0")
                            {
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                Response.Redirect("Student_Master.aspx");
                                con.Close();
                            }
                        }
                        else
                        {
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            Response.Redirect("Student_Master.aspx");
                            con.Close();
                        }
                    }
                }
                else
                {
                    if (Validation())
                    {
                        ImageUpload();
                        lblValidstudent.Visible = false;
                        lblvalidfirstname.Visible = false;
                        //cmd.CommandText = "UPDATE [tblStudent] SET [Cardid] = '" + txtCardNumber.Text + "',Cardstatus='" + ddlCardstatus.SelectedValue + "',StudentImages='" + Image + "',StudentImages_Blob=@hash,StudentImages_Byte='" + Photopath + "' WHERE StudentID='" + Request.QueryString["Edit"] + "'";
                        SqlCommand cmd = new SqlCommand("SP_SMS_Student_upset", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Update");
                        cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);
                        cmd.Parameters.AddWithValue("@firstname", txtfirstname.Text);
                        cmd.Parameters.AddWithValue("@Fathername", txtFathername.Text);
                        cmd.Parameters.AddWithValue("@Grandfathername", txtGrandfathername.Text);
                        cmd.Parameters.AddWithValue("@Gender", drpGender.SelectedValue);
                        cmd.Parameters.AddWithValue("@DateOfBirth", txtDateOfBirth.Text);
                        cmd.Parameters.AddWithValue("@College", ddlCollege.SelectedValue);
                        cmd.Parameters.AddWithValue("@CollegeText", ddlCollege.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue);
                        cmd.Parameters.AddWithValue("@DepartmentText", ddlDepartment.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Campus", ddlCampus.SelectedValue);
                        cmd.Parameters.AddWithValue("@CampusText", ddlCampus.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Program", ddlProgram.SelectedValue);
                        cmd.Parameters.AddWithValue("@ProgramText", ddlProgram.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@DegreeType", ddlDegreeType.SelectedValue);
                        cmd.Parameters.AddWithValue("@DegreeTypeText", ddlDegreeType.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@AdmissionType", ddlAdmissionType.SelectedValue);
                        cmd.Parameters.AddWithValue("@AdmissionTypeText", ddlAdmissionType.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@AdmissionTypeShort", ddlAdmissionTypeShort.SelectedValue);
                        cmd.Parameters.AddWithValue("@AdmissionTypeShortText", ddlAdmissionTypeShort.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@ValidDateUntil", txtValidDateUntil.Text);
                        cmd.Parameters.AddWithValue("@IssueDate", txtIssueDate.Text);
                        cmd.Parameters.AddWithValue("@MealNumber", txtMealNumber.Text);
                        cmd.Parameters.AddWithValue("@UniqueNo", txtUniqueNo.Text);
                        cmd.Parameters.AddWithValue("@Cardstatus", ddlCardstatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@hash", bytes);
                        cmd.Parameters.AddWithValue("@CardNumber", txtCardNumber.Text);
                        cmd.Parameters.AddWithValue("@ImagePath", Image);
                        cmd.Parameters.AddWithValue("@Photopath", Photopath);
                        cmd.Parameters.AddWithValue("@base64", base64string);
                        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
                        cmd.Parameters.AddWithValue("@YearText", ddlYear.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Signaturebase64", Signaturebase64string);
                        cmd.Parameters.AddWithValue("@DigitalStatus", DigitalStatus);
                        cmd.Parameters.AddWithValue("@Signature", SignatureImage);
                        cmd.Parameters.AddWithValue("@AssignBy", Session["UserName"].ToString());
                        if (chkRoomLockerassgnment.Checked == true)
                        {
                            if (HostelValidation())
                            {
                                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                                cmd.Parameters.AddWithValue("@BlockId", ddlLockerBlockName.SelectedValue);
                                cmd.Parameters.AddWithValue("@FloorId", ddllockerFloorName.SelectedValue);
                                cmd.Parameters.AddWithValue("@RoomId", ddlLockerRoomNumber.SelectedValue);
                                cmd.Parameters.AddWithValue("@BedId", ddlBedNumber.SelectedValue);
                                cmd.Parameters.AddWithValue("@LockerId", ddlLocker.SelectedValue);
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                myModal.Attributes.Add("style", "display:block;");
                                //lblmsg.Text = "Student record updated !!";
                                //lnkOkk.Visible = false;
                                //lblS2.Visible = true;
                                //lnkGo.Visible = true;
                                //lnkpushCancel.Visible = true;
                                Response.Redirect("Student_Master.aspx");
                                con.Close();
                            }

                            if (ddlLockerHostelName.SelectedValue != "0" && ddlLockerBlockName.SelectedValue != "0" && ddllockerFloorName.SelectedValue != "0"
                                 && ddlLockerRoomNumber.SelectedValue != "0" && ddlBedNumber.SelectedValue != "0" && ddlLocker.SelectedValue != "0")
                            {
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                //myModal.Attributes.Add("style", "display:block;");
                                //lblmsg.Text = "Student record updated !!";
                                //lnkOkk.Visible = false;
                                //lblS2.Visible = true;
                                //lnkGo.Visible = true;
                                //lnkpushCancel.Visible = true;
                                Response.Redirect("Student_Master.aspx");
                                con.Close();
                            }
                        }
                        else
                        {
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            Response.Redirect("Student_Master.aspx");
                            //myModal.Attributes.Add("style", "display:block;");
                            //lblmsg.Text = "Student record updated !!";
                            //lnkOkk.Visible = false;
                            //lblS2.Visible = true;
                            //lnkGo.Visible = true;
                            //lnkpushCancel.Visible = true;
                            con.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        #region Student Image
        protected void ImageUpload()
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string ext = Path.GetExtension(FileUpload1.FileName.ToString());
                    if ((ext.ToLower() == ".jpg") || (ext.ToLower() == ".eps") || (ext.ToLower() == ".jpeg") || (ext.ToLower() == ".gif") || (ext.ToLower() == ".png") || (ext.ToLower() == ".bmp"))
                    {
                        string str = FileUpload1.FileName;
                        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/StudentImages/" + txtStudentId.Text.Replace("/", "-").Replace("\\", "-") + ext));
                        Image = "~/Images/StudentImages/" + txtStudentId.Text.Replace("/", "-").Replace("\\", "-") + ext.ToString();
                        //string FileName1 = Guid.NewGuid().ToString();
                        //string FileName = System.IO.Path.GetFileName(FileUpload1.FileName.ToString());
                        //string Imagepath1 = Server.MapPath("~\\" + "Images\\StudentImages");
                        //FileUpload1.SaveAs(Imagepath1 + "\\" + FileName1 + ext);
                        //string pathForDB = "Images\\StudentImages";
                        //Photopath = "~\\" + pathForDB + "\\" + FileName1 + ext;
                        Stream fs = FileUpload1.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        bytes = br.ReadBytes((Int32)fs.Length);
                        base64string = Convert.ToBase64String(bytes, 0, bytes.Length);
                        hdnimage.Value = base64string;
                        //Save the Byte Array as File.
                        filePath = Image;
                        File.WriteAllBytes(Server.MapPath(filePath), bytes);
                    }
                }

                if (FileUpload2.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(FileUpload2.FileName.ToString());
                    if ((ext.ToLower() == ".jpg") || (ext.ToLower() == ".eps") || (ext.ToLower() == ".jpeg") || (ext.ToLower() == ".gif") || (ext.ToLower() == ".png") || (ext.ToLower() == ".bmp"))
                    {
                        string str = FileUpload2.FileName;
                        FileUpload2.PostedFile.SaveAs(Server.MapPath("~/Images/StudentSignature/" + txtStudentId.Text.Replace("/", "_") + "_" + str));
                        SignatureImage = "~/Images/StudentSignature/" + txtStudentId.Text.Replace("/", "_") + "_" + str.ToString();
                        string FileName1 = Guid.NewGuid().ToString();
                        string FileName = System.IO.Path.GetFileName(FileUpload2.FileName.ToString());
                        string Imagepath1 = Server.MapPath("~\\" + "Images\\StudentSignature");
                        FileUpload2.SaveAs(Imagepath1 + "\\" + FileName1 + ext);
                        string pathForDB = "Images\\StudentSignature";
                        SignaturePhotopath = "~\\" + pathForDB + "\\" + FileName1 + ext;

                        Stream fs = FileUpload2.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        bytes2 = br.ReadBytes((Int32)fs.Length);
                        Signaturebase64string = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                        //Save the Byte Array as File.
                        SignaturefilePath = "/Images/StudentSignature/" + Path.GetFileName(FileUpload2.FileName);
                        File.WriteAllBytes(Server.MapPath(SignaturefilePath), bytes2);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        #endregion
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
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = "";
                ids = string.Empty;
                ids = (sender as LinkButton).CommandArgument;
                Response.Redirect("Student_Master.aspx?StudentID=" + ids);
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void linkEdit_Click1(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Student_Master.aspx?Edit=" + ids);
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Student_Master.aspx");
        }
        protected void LnkExport_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //string FileName = "SMS" + DateTime.Now + ".xls";
            //StringWriter strwritter = new StringWriter();
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //gridEmployee.GridLines = GridLines.Both;
            //gridEmployee.HeaderStyle.Font.Bold = true;
            //gridEmployee.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            //Response.End(); 
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=SMS" + DateTime.Now + ".xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages
            //    gridEmployee.AllowPaging = false;
            //    FillGrid();

            //    gridEmployee.HeaderRow.BackColor = Color.White;
            //    foreach (TableCell cell in gridEmployee.HeaderRow.Cells)
            //    {
            //        cell.BackColor = gridEmployee.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in gridEmployee.Rows)
            //    {
            //        row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = gridEmployee.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = gridEmployee.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }

            //    gridEmployee.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();

            //}
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from tblstudent WHERE StudentID LIKE '%' + @Search + '%' or FirstName LIKE '%' + @Search + '%' OR DateOfBirth LIKE '%' + @Search + '%' OR College LIKE '%' + @Search + '%' OR cardid LIKE '%' + @Search + '%' OR Department  LIKE '%' + @Search + '%'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt_Grid);
            XLWorkbook wb = new XLWorkbook();

            wb.Worksheets.Add(dt_Grid, "tblstudent");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
            MemoryStream MyMemoryStream = new MemoryStream();

            wb.SaveAs(MyMemoryStream);
            MyMemoryStream.WriteTo(Response.OutputStream);
            Response.Flush();
            Response.End();


        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
        protected bool Validation()
        {
            if (txtStudentId.Text.Trim() == "")
            {
                lblvalidValidDateUntil2.Visible = false;
                lblValidstudent.Visible = true;
                lblvalidfirstname.Visible = false;
                lblcardnumber.Visible = false;
                lblvalidValidDateUntil.Visible = false;
                lbllastnamevalid.Visible = false;
                txtStudentId.Focus();
                aEducationinformation.Attributes.Add("class", "nav-link ");
                liEducationinformation.Attributes.Add("class", "nav-link");
                aPersonalDetails.Attributes.Add("class", "nav-link active");
                liPersonalDetails.Attributes.Add("class", "nav-link active");
                liPersonalDetails.Attributes.Add("aria - expanded", "true");
                divPersonalDetails.Attributes.Add("class", "tab-pane container active");
                divEducationinformation.Attributes.Add("class", "tab-pane container fade");
                liOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                liOtherinformaion.Attributes.Add("class", "nav-link");
                liHostel.Attributes.Add("class", "tab-pane container fade");
                liHostel.Attributes.Add("class", "nav-link");
                divHostel.Attributes.Add("class", "tab-pane container fade");
                return false;
            }
            if (txtfirstname.Text.Trim() == "")
            {
                lblvalidValidDateUntil2.Visible = false;
                lblvalidfirstname.Visible = true;
                lblValidstudent.Visible = false;
                lblcardnumber.Visible = false;
                lblvalidValidDateUntil.Visible = false;
                lbllastnamevalid.Visible = false;
                txtfirstname.Focus();
                aEducationinformation.Attributes.Add("class", "nav-link ");
                liEducationinformation.Attributes.Add("class", "nav-link");
                aPersonalDetails.Attributes.Add("class", "nav-link active");
                liPersonalDetails.Attributes.Add("class", "nav-link active");
                liPersonalDetails.Attributes.Add("aria - expanded", "true");
                // aPersonalDetails.HRef = "#Educationinformation";
                divPersonalDetails.Attributes.Add("class", "tab-pane container active");
                divEducationinformation.Attributes.Add("class", "tab-pane container fade");
                liOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                //aOtherinformaion.Attributes.Add("class", "nav-link");
                liOtherinformaion.Attributes.Add("class", "nav-link");
                liHostel.Attributes.Add("class", "tab-pane container fade");
                liHostel.Attributes.Add("class", "nav-link");
                divHostel.Attributes.Add("class", "tab-pane container fade");
                return false;
            }
            if (txtGrandfathername.Text.Trim() == "")
            {
                lblvalidValidDateUntil2.Visible = false;
                lblvalidfirstname.Visible = false;
                lblValidstudent.Visible = false;
                lblcardnumber.Visible = false;
                lblvalidValidDateUntil.Visible = false;
                lbllastnamevalid.Visible = true;
                txtfirstname.Focus();
                aEducationinformation.Attributes.Add("class", "nav-link ");
                liEducationinformation.Attributes.Add("class", "nav-link");
                aPersonalDetails.Attributes.Add("class", "nav-link active");
                liPersonalDetails.Attributes.Add("class", "nav-link active");
                liPersonalDetails.Attributes.Add("aria - expanded", "true");
                // aPersonalDetails.HRef = "#Educationinformation";
                divPersonalDetails.Attributes.Add("class", "tab-pane container active");
                divEducationinformation.Attributes.Add("class", "tab-pane container fade");
                liOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                //aOtherinformaion.Attributes.Add("class", "nav-link");
                liOtherinformaion.Attributes.Add("class", "nav-link");
                liHostel.Attributes.Add("class", "tab-pane container fade");
                liHostel.Attributes.Add("class", "nav-link");
                divHostel.Attributes.Add("class", "tab-pane container fade");
                return false;
            }
            if (txtCardNumber.Text.Trim() == "")
            {
                lblvalidValidDateUntil2.Visible = false;
                lblcardnumber.Visible = true;
                lblValidstudent.Visible = false;
                lblvalidValidDateUntil.Visible = false;
                lblvalidfirstname.Visible = false;
                lbllastnamevalid.Visible = false;
                txtCardNumber.Focus();
                aEducationinformation.Attributes.Add("class", "nav-link ");
                liEducationinformation.Attributes.Add("class", "nav-link");
                aPersonalDetails.Attributes.Add("class", "nav-link");
                liPersonalDetails.Attributes.Add("class", "nav-link");
                aOtherinformaion.Attributes.Add("aria - expanded", "true");
                // aPersonalDetails.HRef = "#Educationinformation";
                divPersonalDetails.Attributes.Add("class", "tab-pane container fade");
                divEducationinformation.Attributes.Add("class", "tab-pane container fade");
                divOtherinformaion.Attributes.Add("class", "tab-pane container active");
                //liOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                aOtherinformaion.Attributes.Add("class", "nav-link active");
                liOtherinformaion.Attributes.Add("class", "nav-link active");
                liHostel.Attributes.Add("class", "tab-pane container fade");
                liHostel.Attributes.Add("class", "nav-link");
                divHostel.Attributes.Add("class", "tab-pane container fade");
                return false;
            }
            if (txtValidDateUntil.Text.Trim() == "")
            {
                lblvalidValidDateUntil2.Visible = false;
                lblcardnumber.Visible = false;
                lblValidstudent.Visible = false;
                lblvalidValidDateUntil.Visible = true;
                lblvalidfirstname.Visible = false;
                lbllastnamevalid.Visible = false;
                txtValidDateUntil.Focus();
                aEducationinformation.Attributes.Add("class", "nav-link active");
                liEducationinformation.Attributes.Add("class", "nav-link active");
                aPersonalDetails.Attributes.Add("class", "nav-link ");
                liPersonalDetails.Attributes.Add("class", "nav-link");
                liEducationinformation.Attributes.Add("aria - expanded", "true");
                // aPersonalDetails.HRef = "#Educationinformation";
                divPersonalDetails.Attributes.Add("class", "tab-pane container fade");
                divEducationinformation.Attributes.Add("class", "tab-pane container active");
                divOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                //aOtherinformaion.Attributes.Add("class", "nav-link");
                liOtherinformaion.Attributes.Add("class", "nav-link");
                liHostel.Attributes.Add("class", "tab-pane container fade");
                liHostel.Attributes.Add("class", "nav-link");
                divHostel.Attributes.Add("class", "tab-pane container fade");
                return false;
            }
            if (txtValidDateUntil.Text.Trim() != "")
            {
                string Startdatetime = DateTime.Now.ToString("MM/dd/yyyy");
                DateTime fromdate = new DateTime(int.Parse(Startdatetime.Substring(6, 4)), int.Parse(Startdatetime.Substring(0, 2)), int.Parse(Startdatetime.Substring(3, 2)), 00, 00, 00);
                DateTime todate = new DateTime(int.Parse(txtValidDateUntil.Text.Substring(6, 4)), int.Parse(txtValidDateUntil.Text.Substring(0, 2)), int.Parse(txtValidDateUntil.Text.Substring(3, 2)), 00, 00, 00);
                int res = DateTime.Compare(fromdate, todate);
                if (res > 0)
                {
                    lblcardnumber.Visible = false;
                    lblValidstudent.Visible = false;
                    lblvalidValidDateUntil2.Visible = true;
                    lblvalidValidDateUntil.Visible = false;
                    lblvalidfirstname.Visible = false;
                    lbllastnamevalid.Visible = false;
                    txtValidDateUntil.Focus();
                    aEducationinformation.Attributes.Add("class", "nav-link active");
                    liEducationinformation.Attributes.Add("class", "nav-link active");
                    aPersonalDetails.Attributes.Add("class", "nav-link ");
                    liPersonalDetails.Attributes.Add("class", "nav-link");
                    liEducationinformation.Attributes.Add("aria - expanded", "true");
                    // aPersonalDetails.HRef = "#Educationinformation";
                    divPersonalDetails.Attributes.Add("class", "tab-pane container fade");
                    divEducationinformation.Attributes.Add("class", "tab-pane container active");
                    divOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                    //aOtherinformaion.Attributes.Add("class", "nav-link");
                    liOtherinformaion.Attributes.Add("class", "nav-link");
                    liHostel.Attributes.Add("class", "tab-pane container fade");
                    liHostel.Attributes.Add("class", "nav-link");
                    divHostel.Attributes.Add("class", "tab-pane container fade");
                    return false;
                }
                if (FileUpload1.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(FileUpload1.FileName.ToString());
                    if ((ext.ToLower() == ".jpg") || (ext.ToLower() == ".eps") || (ext.ToLower() == ".jpeg") || (ext.ToLower() == ".gif") || (ext.ToLower() == ".png") || (ext.ToLower() == ".bmp"))
                    {
                        lblf1.Visible = false;
                        lblf2.Visible = false;
                    }
                    else
                    {
                        lblf1.Visible = true;
                        lblf2.Visible = false;
                        return false;
                    }
                }

                if (FileUpload2.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(FileUpload2.FileName.ToString());
                    if ((ext.ToLower() == ".jpg") || (ext.ToLower() == ".eps") || (ext.ToLower() == ".jpeg") || (ext.ToLower() == ".gif") || (ext.ToLower() == ".png") || (ext.ToLower() == ".bmp"))
                    {
                        lblf1.Visible = false;
                        lblf2.Visible = false;
                    }
                    else
                    {
                        lblf1.Visible = false;
                        lblf2.Visible = true;
                        return false;
                    }
                }

            }

            return true;
        }
        protected bool HostelValidation()
        {
            if (ddlLockerHostelName.SelectedValue == "0")
            {
                lblValidLockerHostelName.Visible = true;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerNumber.Visible = false;
                HostelTab();
                return false;
            }
            if (ddlLockerBlockName.SelectedValue == "0")
            {
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = true;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerNumber.Visible = false;
                HostelTab();
                return false;
            }
            if (ddllockerFloorName.SelectedValue == "0")
            {
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = true;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerNumber.Visible = false;
                HostelTab();
                return false;
            }
            if (ddlLockerRoomNumber.SelectedValue == "0")
            {
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerRoomNumber.Visible = true;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerNumber.Visible = false;
                HostelTab();
                return false;
            }
            if (ddlBedNumber.SelectedValue == "0")
            {
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = true;
                lblValidLockerNumber.Visible = false;
                HostelTab();
                return false;
            }
            if (ddlLocker.SelectedValue == "0")
            {
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerNumber.Visible = true;
                HostelTab();
                return false;
            }
            return true;
        }
        protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
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
                aPersonalDetails.Attributes.Add("class", "nav-link");
                liPersonalDetails.Attributes.Add("class", "nav-link");

                aEducationinformation.Attributes.Add("class", "nav-link active");
                liEducationinformation.Attributes.Add("class", "nav-link active");

                aEducationinformation.HRef = "#ContentPlaceHolder1_divEducationinformation";
                divPersonalDetails.Attributes.Add("class", "tab-pane container fade");
                divEducationinformation.Attributes.Add("class", "tab-pane container active in");
                divOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                aOtherinformaion.Attributes.Add("class", "nav-link");
                liOtherinformaion.Attributes.Add("class", "nav-link");
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
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
                aEducationinformation.Attributes.Add("class", "nav-link active");
                liEducationinformation.Attributes.Add("class", "nav-link active");
                aPersonalDetails.Attributes.Add("class", "nav-link");
                liPersonalDetails.Attributes.Add("class", "nav-link");
                liEducationinformation.Attributes.Add("aria - expanded", "true");
                aEducationinformation.HRef = "#ContentPlaceHolder1_divEducationinformation";
                divPersonalDetails.Attributes.Add("class", "tab-pane container fade");
                divEducationinformation.Attributes.Add("class", "tab-pane container active in");
                liOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                aOtherinformaion.Attributes.Add("class", "nav-link");
                liOtherinformaion.Attributes.Add("class", "nav-link");
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
        protected void ddlAdmissionType_TextChanged(object sender, EventArgs e)
        {
            AdmissionTypeShort();
        }
        protected void Program()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BindProgramData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Program", ddlProgram.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlProgram.DataSource = ds1;
                ddlProgram.DataTextField = "Name";
                ddlProgram.DataValueField = "Id";
                ddlProgram.DataBind();
                ddlProgram.Items.Insert(0, new ListItem("--Select Program--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void DegreeType()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BindDegreeTypeData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@DegreeType", ddlDegreeType.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlDegreeType.DataSource = ds1;
                ddlDegreeType.DataTextField = "Name";
                ddlDegreeType.DataValueField = "Id";
                ddlDegreeType.DataBind();
                ddlDegreeType.Items.Insert(0, new ListItem("--Select Degree Type--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void AdmissionType()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BindAdmissionTypeData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@AdmissionType", ddlAdmissionType.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlAdmissionType.DataSource = ds1;
                ddlAdmissionType.DataTextField = "Adminssion";
                ddlAdmissionType.DataValueField = "Id";
                ddlAdmissionType.DataBind();
                ddlAdmissionType.Items.Insert(0, new ListItem("--Select Admission Type--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void AdmissionTypeShort()
        {
            try
            {
                if (ddlAdmissionType.SelectedValue == "0")
                {
                    ddlAdmissionTypeShort.Items.Insert(0, new ListItem("--Select Admission Type Short--", "0"));
                    ddlAdmissionTypeShort.SelectedValue = "0";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("BindAdmissionTypeShortData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@AdmissionType", ddlAdmissionType.SelectedValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable ds1 = new DataTable();
                    da.Fill(ds1);

                    ddlAdmissionTypeShort.DataSource = ds1;
                    ddlAdmissionTypeShort.DataTextField = "AdminssionType";
                    ddlAdmissionTypeShort.DataValueField = "Id";
                    ddlAdmissionTypeShort.DataBind();
                }
                //ddlAdmissionTypeShort.Items.Insert(0, new ListItem("--Select Admission Type Short--", "0"));
                aPersonalDetails.Attributes.Add("class", "nav-link");
                liPersonalDetails.Attributes.Add("class", "nav-link");

                aEducationinformation.Attributes.Add("class", "nav-link active");
                liEducationinformation.Attributes.Add("class", "nav-link active");

                aEducationinformation.HRef = "#ContentPlaceHolder1_divEducationinformation";
                divPersonalDetails.Attributes.Add("class", "tab-pane container fade");
                divEducationinformation.Attributes.Add("class", "tab-pane container active in");
                divOtherinformaion.Attributes.Add("class", "tab-pane container fade");
                aOtherinformaion.Attributes.Add("class", "nav-link");
                liOtherinformaion.Attributes.Add("class", "nav-link");
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void lnkClear_Click(object sender, EventArgs e)
        {
            ddlAdmissionType.SelectedValue = "";
            ddlAdmissionTypeShort.SelectedValue = "";
            txtCardNumber.Text = "";
            txtDateOfBirth.Text = "";
            ddlDegreeType.SelectedValue = "";
            txtFathername.Text = "";
            txtfirstname.Text = "";
            txtGrandfathername.Text = "";
            txtIssueDate.Text = "";
            txtMealNumber.Text = "";
            ddlProgram.SelectedValue = "0";
            txtUniqueNo.Text = "";
            txtValidDateUntil.Text = "";
            drpGender.SelectedValue = "0";
            ddlCampus.SelectedValue = "0";
            ddlCardstatus.SelectedValue = "0";
            ddlCollege.SelectedValue = "0";
            ddlDepartment.SelectedValue = "0";
            ddlYear.SelectedValue = "0";
            if (Request.QueryString["Edit"] == null)
            {
                txtStudentId.Text = "";
            }
        }
        protected void linkdigital_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Digital_Form.aspx?StudentID=" + ids);
        }

        #region RowDataBound Method
        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDigitalStatus = (Label)(e.Row.FindControl("lblDigitalStatus"));
                    Label lblDigitalRequest = (Label)(e.Row.FindControl("lblDigitalRequest"));
                    LinkButton linkdigital = (LinkButton)(e.Row.FindControl("linkdigital"));
                    System.Web.UI.WebControls.Image imgDigitalRequest = (System.Web.UI.WebControls.Image)(e.Row.FindControl("imgDigitalRequest"));
                    System.Web.UI.WebControls.Image imgDigitalStatus = (System.Web.UI.WebControls.Image)(e.Row.FindControl("imgDigitalStatus"));
                    System.Web.UI.WebControls.Image imgQRMain = (System.Web.UI.WebControls.Image)(e.Row.FindControl("imgQRMain"));
                    
                    if (lblDigitalStatus.Text == "1")
                    {
                        linkdigital.Visible = true;
                        imgDigitalStatus.Visible = true;
                        imgQRMain.Visible = true;
                    }
                    if (lblDigitalRequest.Text == "1")
                    {
                        imgQRMain.Visible = false;
                        imgDigitalRequest.Visible = true;
                    }
                    //check Student path Image
                    Label path = e.Row.FindControl("lblpath") as Label;
                    System.Web.UI.WebControls.Image Studentpath = e.Row.FindControl("imgStudent") as System.Web.UI.WebControls.Image;
                    System.Web.UI.WebControls.Image imgdefault = e.Row.FindControl("imgdefault") as System.Web.UI.WebControls.Image;
                    if (path.Text != "" && path.Text!="null")
                    {
                        if (Checkpath.CheckPathExitsOrNot(Server.MapPath(path.Text)))
                        {
                            Studentpath.Visible = true;
                            imgdefault.Visible = false;
                        }
                        else
                        {
                            Studentpath.Visible = false;
                            imgdefault.Visible = true;
                        }
                    }
                    else
                    {
                        Studentpath.Visible = false;
                        imgdefault.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        #endregion
        protected void Connection()
        {
            try
            {
                string filePath1 = Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
                StreamReader sr = new StreamReader(filePath1);
                line = sr.ReadToEnd();
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
        protected void BindLockerHostel()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Hostel");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlLockerHostelName.DataSource = ds1;
                ddlLockerHostelName.DataTextField = "Name";
                ddlLockerHostelName.DataValueField = "Id";
                ddlLockerHostelName.DataBind();
                ddlLockerHostelName.Items.Insert(0, new ListItem("--Select Hostel Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindLockerBlock()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Block");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlLockerBlockName.DataSource = ds1;
                ddlLockerBlockName.DataTextField = "Name";
                ddlLockerBlockName.DataValueField = "Id";
                ddlLockerBlockName.DataBind();
                ddlLockerBlockName.Items.Insert(0, new ListItem("--Select Block Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindLockerFloor()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Floor");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddllockerFloorName.DataSource = ds1;
                ddllockerFloorName.DataTextField = "Name";
                ddllockerFloorName.DataValueField = "Id";
                ddllockerFloorName.DataBind();
                ddllockerFloorName.Items.Insert(0, new ListItem("--Select Floor Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindlockerRoom()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Room");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                cmd.Parameters.AddWithValue("@FloorID", ddllockerFloorName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlLockerRoomNumber.DataSource = ds1;
                ddlLockerRoomNumber.DataTextField = "Name";
                ddlLockerRoomNumber.DataValueField = "Id";
                ddlLockerRoomNumber.DataBind();
                ddlLockerRoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindLockerBed()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "BedLocker");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                cmd.Parameters.AddWithValue("@FloorID", ddllockerFloorName.SelectedValue);
                cmd.Parameters.AddWithValue("@RoomId", ddlLockerRoomNumber.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlBedNumber.DataSource = ds1;
                ddlBedNumber.DataTextField = "Name";
                ddlBedNumber.DataValueField = "Id";
                ddlBedNumber.DataBind();
                ddlBedNumber.Items.Insert(0, new ListItem("--Select Bed Number--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindLocker()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Locker");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                cmd.Parameters.AddWithValue("@FloorID", ddllockerFloorName.SelectedValue);
                cmd.Parameters.AddWithValue("@RoomId", ddlLockerRoomNumber.SelectedValue);
                cmd.Parameters.AddWithValue("@BedId", ddlBedNumber.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlLocker.DataSource = ds1;
                ddlLocker.DataTextField = "Name";
                ddlLocker.DataValueField = "Id";
                ddlLocker.DataBind();
                ddlLocker.Items.Insert(0, new ListItem("--Select Locker Number--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void ddlLockerHostelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLockerBlock();
            ddllockerFloorName.Items.Clear();
            ddllockerFloorName.Items.Insert(0, new ListItem("--Select Floor Name--", "0"));
            ddlLockerRoomNumber.Items.Clear();
            ddlLockerRoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
            ddlBedNumber.Items.Clear();
            ddlBedNumber.Items.Insert(0, new ListItem("--Select Bed Number--", "0"));
            ddlLocker.Items.Clear();
            ddlLocker.Items.Insert(0, new ListItem("--Select Locker Number--", "0"));
            HostelTab();
        }
        protected void ddlLockerBlockName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLockerFloor();
            ddlLockerRoomNumber.Items.Clear();
            ddlLockerRoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
            ddlBedNumber.Items.Clear();
            ddlBedNumber.Items.Insert(0, new ListItem("--Select Bed Number--", "0"));
            ddlLocker.Items.Clear();
            ddlLocker.Items.Insert(0, new ListItem("--Select Locker Number--", "0"));
            HostelTab();
        }
        protected void ddllockerFloorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindlockerRoom();
            ddlBedNumber.Items.Clear();
            ddlBedNumber.Items.Insert(0, new ListItem("--Select Bed Number--", "0"));
            ddlLocker.Items.Clear();
            ddlLocker.Items.Insert(0, new ListItem("--Select Locker Number--", "0"));
            HostelTab();
        }
        protected void ddlLockerRoomNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLockerBed();
            ddlLocker.Items.Clear();
            ddlLocker.Items.Insert(0, new ListItem("--Select Locker Number--", "0"));
            HostelTab();
        }
        protected void chkRoomLockerassgnment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRoomLockerassgnment.Checked == true)
            {
                divMainHostel.Visible = true;
            }
            else
            {
                divMainHostel.Visible = false;
            }
            HostelTab();
        }
        protected void HostelTab()
        {
            aEducationinformation.Attributes.Add("class", "nav-link");
            liEducationinformation.Attributes.Add("class", "nav-link");
            aPersonalDetails.Attributes.Add("class", "nav-link ");
            liPersonalDetails.Attributes.Add("class", "nav-link");
            liEducationinformation.Attributes.Add("aria - expanded", "true");
            divPersonalDetails.Attributes.Add("class", "tab-pane container fade");
            divEducationinformation.Attributes.Add("class", "tab-pane container ");
            divOtherinformaion.Attributes.Add("class", "tab-pane container fade");
            liOtherinformaion.Attributes.Add("class", "nav-link");
            liHostel.Attributes.Add("class", "tab-pane container active");
            liHostel.Attributes.Add("class", "nav-link active");
            divHostel.Attributes.Add("class", "tab-pane container active");
            aHostel.Attributes.Add("class", "nav-link active");
        }
        protected void ddlBedNumber_TextChanged(object sender, EventArgs e)
        {
            BindLocker();
            HostelTab();
        }
        protected void lnksyncjob_Click(object sender, EventArgs e)
        {
            //DivSync.Style.Remove("display");
            //DivSync.Attributes.Add("style", "display:block;");
            //lnksyncyes.Visible = false;
            //lnksyncyes2.Visible = true;
            //Label2.Visible = false;
            //Label3.Visible = true;
            SqlCommand cmd = new SqlCommand("SyncStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            lblSyncmsg.Visible = true;
            FillGrid();
        }
        protected void lnkAllMasters_Click(object sender, EventArgs e)
        {
            //DivSync.Style.Remove("display");
            //DivSync.Attributes.Add("style", "display:block;");
            //lnksyncyes.Visible = true;
            //lnksyncyes2.Visible = false;
            //Label2.Visible = true;
            //Label3.Visible = false;
            SqlCommand cmd = new SqlCommand("SyncAllStudentMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            lblSyncmsg.Visible = true;
            FillGrid();
        }
        protected void lnkpushCancel_Click(object sender, EventArgs e)
        {
            myModal.Attributes.Add("style", "display:none;");
            Response.Redirect("Student_Master.aspx");
        }
        protected void lnkGo_Click(object sender, EventArgs e)
        {
            lnkpushtos2_Click(sender, e);
        }
        protected void lnkpushtos2_Click(object sender, EventArgs e)
        {
            if (Session["Sessionid"] == null)
            {
                CheckCreditionals();
            }
            else
            {
                Push();
            }
        }
        public void CheckCreditionals()
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
                    Push();
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
        public void Push()
        {
            string msg = S2API.GetStudentAPI(txtStudentId.Text);

            if (msg == "FAIL")
            {
                if (ddlCampus.SelectedIndex == 0) ddlCampus.SelectedItem.Text = "";
                if (ddlCollege.SelectedIndex == 0) ddlCollege.SelectedItem.Text = "";
                if (ddlDepartment.SelectedIndex == 0) ddlDepartment.SelectedItem.Text = "";
                if (ddlProgram.SelectedIndex == 0) ddlProgram.SelectedItem.Text = "";
                if (ddlAdmissionType.SelectedIndex == 0) ddlAdmissionType.SelectedItem.Text = "";
                if (ddlAdmissionTypeShort.SelectedItem.Text == "--Select Admission Type Short--") ddlAdmissionTypeShort.SelectedItem.Text = "";
                if (ddlDegreeType.SelectedIndex == 0) ddlDegreeType.SelectedItem.Text = "";

                string modifymsg = S2API.AddStudentAPI(txtStudentId.Text, txtfirstname.Text, txtFathername.Text, txtGrandfathername.Text,
                drpGender.SelectedItem.Text, txtDateOfBirth.Text, ddlCampus.SelectedItem.Text, ddlCollege.SelectedItem.Text,
                ddlDepartment.SelectedItem.Text, ddlProgram.SelectedItem.Text, ddlAdmissionType.SelectedItem.Text, ddlAdmissionTypeShort.SelectedItem.Text,
                txtValidDateUntil.Text, txtIssueDate.Text, txtMealNumber.Text, txtUniqueNo.Text, hdnimage.Value, txtCardNumber.Text, ddlDegreeType.SelectedItem.Text);
                
                if (modifymsg == "SUCCESS")
                {
                    string status = "";
                    if (ddlCardstatus.SelectedItem.Text.ToUpper() == "ACTIVE")
                    {
                        status = "Active";
                    }
                    else
                    {
                        status = "Disabled";
                    }
                    string Responsmsg =S2API.AddStudentCardnumberApi(txtStudentId.Text,txtCardNumber.Text,status);
                    Response.Redirect("Student_Master.aspx");
                }
                else
                {
                    lblmsg.Text = modifymsg;
                    myModal.Style.Remove("display");
                    myModal.Attributes.Add("style", "display:block;");
                    lnkOkk.Visible = true;
                    lblS2.Visible = false;
                    lnkGo.Visible = false;
                    lnkpushCancel.Visible = false;
                }
            }
            else
            {
                if (ddlCampus.SelectedIndex == 0) ddlCampus.SelectedItem.Text = "";
                if (ddlCollege.SelectedIndex == 0) ddlCollege.SelectedItem.Text = "";
                if (ddlDepartment.SelectedIndex == 0) ddlDepartment.SelectedItem.Text = "";
                if (ddlProgram.SelectedIndex == 0) ddlProgram.SelectedItem.Text = "";
                if (ddlAdmissionType.SelectedIndex == 0) ddlAdmissionType.SelectedItem.Text = "";
                if (ddlAdmissionTypeShort.SelectedItem.Text == "--Select Admission Type Short--") ddlAdmissionTypeShort.SelectedItem.Text="";
                if (ddlDegreeType.SelectedIndex == 0) ddlDegreeType.SelectedItem.Text = "";

                string modifymsg = S2API.UpdateStudentAPI(txtStudentId.Text, txtfirstname.Text, txtFathername.Text, txtGrandfathername.Text,
                drpGender.SelectedItem.Text, txtDateOfBirth.Text, ddlCampus.SelectedItem.Text, ddlCollege.SelectedItem.Text,
                ddlDepartment.SelectedItem.Text, ddlProgram.SelectedItem.Text, ddlAdmissionType.SelectedItem.Text, ddlAdmissionTypeShort.SelectedItem.Text,
                txtValidDateUntil.Text, txtIssueDate.Text, txtMealNumber.Text, txtUniqueNo.Text, hdnimage.Value, txtCardNumber.Text,ddlDegreeType.SelectedItem.Text);
                string status = "";
                if(ddlCardstatus.SelectedItem.Text.ToUpper()== "ACTIVE")
                {
                    status = "Active";
                }
                else
                {
                    status = "Disabled";
                }
                string Responsmsg = "";
                if (modifymsg == "SUCCESS")
                {
                    Responsmsg = S2API.AddStudentCardnumberApi(txtStudentId.Text, txtCardNumber.Text, status);
                }
                if(Responsmsg != "SUCCESS" && Responsmsg!="")
                {
                     Responsmsg = S2API.ModifyStudentCardnumberApi(txtStudentId.Text, txtCardNumber.Text, status);
                }
                lblmsg.Text = modifymsg;
                myModal.Style.Remove("display");
                myModal.Attributes.Add("style", "display:block;");
                lnkOkk.Visible = true;
                lblS2.Visible = false;
                lnkGo.Visible = false;
                lnkpushCancel.Visible = false;
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DivSync.Style.Remove("display");
            DivSync.Attributes.Add("style", "display:none;");
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            DivSync.Style.Remove("display");
            DivSync.Attributes.Add("style", "display:none;");
            SqlCommand cmd = new SqlCommand("SyncAllStudentMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            lblSyncmsg.Visible = true;
            FillGrid();
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            DivSync.Style.Remove("display");
            DivSync.Attributes.Add("style", "display:none;");
            SqlCommand cmd = new SqlCommand("SyncStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            lblSyncmsg.Visible = true;
            FillGrid();
        }
        #region Import Excel
        protected void lnkImportExcel_Click(object sender, EventArgs e)
        {
            DivModalImport.Attributes.Add("style", "display:block;");
        }
        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            DownloadFile();
        }
        protected void DownloadFile()
        {
            string filePath = Server.MapPath(@"\ImportExcel\Student\Student.xlsx");
            //string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            if (FileUpload3.PostedFile != null)
            {
                try
                {
                    string path = Server.MapPath("~/ImportExcel/Student/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(FileUpload3.FileName);
                    string extension = Path.GetExtension(FileUpload3.FileName);
                    FileUpload3.SaveAs(filePath);

                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    DataTable dt = new DataTable();
                    conString = string.Format(conString, filePath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();

                        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                        dtExcelData.Columns.AddRange(new DataColumn[21] {
                        new DataColumn("Student Id", typeof(string)),
                        new DataColumn("First Name", typeof(string)),
                        new DataColumn("Father's Name", typeof(string)),
                        new DataColumn("Grand Father's Name", typeof(string)),
                        new DataColumn("Gender", typeof(string)),
                        new DataColumn("Eng Full Name", typeof(string)),
                        new DataColumn("Campus", typeof(string)),
                        new DataColumn("College", typeof(string)),
                        new DataColumn("Department", typeof(string)),
                        new DataColumn("Year", typeof(string)),
                        new DataColumn("Program", typeof(string)),
                        new DataColumn("Degree Type", typeof(string)),
                        new DataColumn("Admission Type", typeof(string)),
                        new DataColumn("Admission Type Short", typeof(string)),
                        new DataColumn("Valid Date Until", typeof(string)),
                        new DataColumn("Issue Date", typeof(string)),
                        new DataColumn("Date Of Birth", typeof(string)),
                        new DataColumn("Email Id", typeof(string)),
                        new DataColumn("Card Status", typeof(string)),
                        new DataColumn("Card Number", typeof(string)),
                        new DataColumn("Digital Status", typeof(string))
                        });
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();
                        string filePath1 = Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
                        StreamReader sr = new StreamReader(filePath1);
                        string consString = sr.ReadToEnd();
                        //con = new SqlConnection(line);
                        //string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name
                                sqlBulkCopy.DestinationTableName = "dbo.tblstudent";
                                sqlBulkCopy.ColumnMappings.Add("Student Id", "StudentID");
                                sqlBulkCopy.ColumnMappings.Add("First Name", "FirstName");
                                sqlBulkCopy.ColumnMappings.Add("Father's Name", "FatherName");
                                sqlBulkCopy.ColumnMappings.Add("Grand Father's Name", "GrandFatherName");
                                sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                                sqlBulkCopy.ColumnMappings.Add("Eng Full Name", "MealNumber");
                                sqlBulkCopy.ColumnMappings.Add("Campus", "Campus");
                                sqlBulkCopy.ColumnMappings.Add("College", "College");
                                sqlBulkCopy.ColumnMappings.Add("Department", "Department");
                                sqlBulkCopy.ColumnMappings.Add("Year", "Batch_Year");
                                sqlBulkCopy.ColumnMappings.Add("Program", "Program");
                                sqlBulkCopy.ColumnMappings.Add("Degree Type", "DegreeType");
                                sqlBulkCopy.ColumnMappings.Add("Admission Type", "AdmissionType");
                                sqlBulkCopy.ColumnMappings.Add("Admission Type Short", "AdmissionTypeShort");
                                sqlBulkCopy.ColumnMappings.Add("Valid Date Until", "ValidDateUntil");
                                sqlBulkCopy.ColumnMappings.Add("Issue Date", "IssueDate");
                                sqlBulkCopy.ColumnMappings.Add("Date Of Birth", "DateOfBirth");
                                sqlBulkCopy.ColumnMappings.Add("Email Id", "UniqueNo");
                                sqlBulkCopy.ColumnMappings.Add("CardStatus", "cardstatus");
                                sqlBulkCopy.ColumnMappings.Add("Card Number", "cardid");
                                sqlBulkCopy.ColumnMappings.Add("Digital Status", "DigitalStatus");

                                con.Open();
                                sqlBulkCopy.WriteToServer(dtExcelData);
                                con.Close();
                                BS.UpdateImagePath("Student");
                                FillGrid();
                            }
                        }
                        lblMessage.Text = "Your file uploaded successfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Your file not uploaded !!!" + ex.Message;
                    //lblMessage.Text = "Your file not uploaded";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void lnkClose_Click(object sender, EventArgs e)
        {
            myModal.Attributes.Add("style", "display:None;");
            Response.Redirect("Student_Master.aspx");
        }
        #endregion
    }
}