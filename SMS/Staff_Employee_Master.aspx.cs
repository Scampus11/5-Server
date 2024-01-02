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
using System.Data.Sql;
using System.Web.Configuration;
using SMS.Class;
using SMS.CommonClass;
using System.Net.Mail;
using System.Net;
using AjaxControlToolkit;
using System.Data.OleDb;
using SMS.BussinessLayer;

namespace SMS
{
    public partial class Staff_Employee_Master : System.Web.UI.Page
    {
        string Image = "";
        string Imagesignature = "";
        byte[] bytes = new byte[1];
        string base64string = "";
        byte[] bytes2 = new byte[1];
        string base64string2 = "";
        Checkpath Checkpath = new Checkpath();
        SqlConnection con = new SqlConnection();
        LogFile logFile = new LogFile();
        BS BS = new BS();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            Session["Sider"] = "Staff Master";
            if (!Page.IsPostBack)
            {

                try
                {
                    BindDepartment();
                    Bindfacility();
                    BindCompany();
                    BindCountry();
                    BindJobTitle();
                    if (Request.QueryString["Id"] != null && Request.QueryString["Type"].ToString() == "Edit")
                    {
                        BindCanteen2();
                        EditMode();
                        lnkviewgrid.Visible = true;
                        lnkView_Menu.Visible = true;
                        lnkEdit_menu.Visible = false;
                    }
                    else if (Request.QueryString["Id"] != null && Request.QueryString["Type"].ToString() == "View")
                    {
                        BindCanteen2();
                        EditMode();
                        ViewMode();
                        lnkviewgrid.Visible = true;
                        lnkView_Menu.Visible = false;
                        lnkEdit_menu.Visible = true;
                    }
                    else if (Request.QueryString["Insert"] != null)
                    {
                        BindCanteen2();
                        divgrid.Visible = false;
                        divView.Visible = true;
                        lnkupdate.Visible = false;
                        lnksave.Visible = true;
                        imgStaff.Visible = true;
                        imgSignature.Visible = true;
                    }
                    else
                    {
                        divgrid.Visible = true;
                        divView.Visible = false;
                        FillGrid();
                    }
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
        //Add New Staff Logic
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Staff_Employee_Master.aspx?Insert=Insert Department");
        }
        //Save Staff Details Logic
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    lblValidstudent.Visible = false;
                    lblvalidFullname.Visible = false;
                    lblValidEmail.Visible = false;
                    lblvalidPassword.Visible = false;
                    lblValidstudentalready.Visible = false;
                    ImageUpload();
                    ImageUpload2();
                    SqlCommand cmd = new SqlCommand("SP_SMS_Officer_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Insert");
                    cmd.Parameters.AddWithValue("@Application_No", txtApplication_No.Text);
                    cmd.Parameters.AddWithValue("@SL_No", txtSL_No.Text);
                    cmd.Parameters.AddWithValue("@Staff_Id", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@UID", txtUID.Text);
                    cmd.Parameters.AddWithValue("@Full_Name", txtFull_Name.Text);
                    cmd.Parameters.AddWithValue("@Gender", drpGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                    cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@DepartmentText", ddlDepartment.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Facility", ddlFacility.SelectedValue);
                    cmd.Parameters.AddWithValue("@FacilityText", ddlFacility.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Job_Title", ddljobtitle.SelectedValue);
                    cmd.Parameters.AddWithValue("@Job_TitleText", ddljobtitle.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Emp_Photo", Image);
                    cmd.Parameters.AddWithValue("@Signature", Imagesignature);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@ID_no", txtID_no.Text);
                    cmd.Parameters.AddWithValue("@Issue_Date", txtIssue_Date.Text);
                    cmd.Parameters.AddWithValue("@Isactive", "");
                    cmd.Parameters.AddWithValue("@cardstatus", ddlCardstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@cardnumber", txtCard_no.Text);
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                    cmd.Parameters.AddWithValue("@password", EncryptionDecryption.GetEncrypt(txtpassword.Text));
                    cmd.Parameters.AddWithValue("@Id", 0);
                    cmd.Parameters.AddWithValue("@hash", bytes);
                    cmd.Parameters.AddWithValue("@CompanyName", ddlCompany.SelectedValue);
                    cmd.Parameters.AddWithValue("@BloodGroup", ddlBloodgroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@PersonalPhone", txtPersonalPhone.Text);
                    cmd.Parameters.AddWithValue("@VehicleNumber", txtVehicleNumber.Text);
                    cmd.Parameters.AddWithValue("@CountryName", ddlCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@Canteen", ddlcanteen2.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@base64", base64string);
                    cmd.Parameters.AddWithValue("@base642", base64string2);
                    cmd.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                    cmd.Parameters.AddWithValue("@amharicFullName", txtamharicFullName.Text);
                    cmd.Parameters.AddWithValue("@Woreda", txtWoreda.Text);
                    cmd.Parameters.AddWithValue("@Subcity", txtSubcity.Text);
                    cmd.Parameters.AddWithValue("@HouseNumber", txtHouseNumber.Text);
                    cmd.Parameters.AddWithValue("@Nationality", ddlCountry.SelectedItem.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("Staff_Employee_Master.aspx");
                    con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }
        //Update Staff Details Logic
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    if (txtpassword.Text == "")
                    {
                        txtpassword.Text = hdnpassword.Value;
                    }
                    else
                    {
                        txtpassword.Text = EncryptionDecryption.GetEncrypt(txtpassword.Text);
                    }

                    lblValidstudent.Visible = false;
                    lblvalidFullname.Visible = false;
                    lblValidEmail.Visible = false;
                    lblvalidPassword.Visible = false;
                    lblValidstudentalready.Visible = false;
                    ImageUpload();
                    ImageUpload2();
                    SqlCommand cmd = new SqlCommand("SP_SMS_Officer_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Update");
                    cmd.Parameters.AddWithValue("@Application_No", txtApplication_No.Text);
                    cmd.Parameters.AddWithValue("@SL_No", txtSL_No.Text);
                    cmd.Parameters.AddWithValue("@Staff_Id", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@UID", txtUID.Text);
                    cmd.Parameters.AddWithValue("@Full_Name", txtFull_Name.Text);
                    cmd.Parameters.AddWithValue("@Gender", drpGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                    cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@DepartmentText", ddlDepartment.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Facility", ddlFacility.SelectedValue);
                    cmd.Parameters.AddWithValue("@FacilityText", ddlFacility.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Job_Title", ddljobtitle.SelectedValue);
                    cmd.Parameters.AddWithValue("@Job_TitleText", ddljobtitle.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Emp_Photo", Image);
                    cmd.Parameters.AddWithValue("@Signature", Imagesignature);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@ID_no", txtID_no.Text);
                    cmd.Parameters.AddWithValue("@Issue_Date", txtIssue_Date.Text);
                    cmd.Parameters.AddWithValue("@Isactive", "");
                    cmd.Parameters.AddWithValue("@cardstatus", ddlCardstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@cardnumber", txtCard_no.Text);
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                    cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Request.QueryString["Id"]));
                    cmd.Parameters.AddWithValue("@hash", bytes);
                    cmd.Parameters.AddWithValue("@CompanyName", ddlCompany.SelectedValue);
                    cmd.Parameters.AddWithValue("@BloodGroup", ddlBloodgroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@PersonalPhone", txtPersonalPhone.Text);
                    cmd.Parameters.AddWithValue("@VehicleNumber", txtVehicleNumber.Text);
                    cmd.Parameters.AddWithValue("@CountryName", ddlCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@Canteen", ddlcanteen2.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@base64", base64string);
                    cmd.Parameters.AddWithValue("@base642", base64string2);
                    cmd.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                    cmd.Parameters.AddWithValue("@amharicFullName", txtamharicFullName.Text);
                    cmd.Parameters.AddWithValue("@Woreda", txtWoreda.Text);
                    cmd.Parameters.AddWithValue("@Subcity", txtSubcity.Text);
                    cmd.Parameters.AddWithValue("@HouseNumber", txtHouseNumber.Text);
                    cmd.Parameters.AddWithValue("@Nationality", ddlCountry.SelectedItem.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //myModal.Attributes.Add("style", "display:block;");
                    //lblmsg.Text = "Staff record updated !!";
                    //lnkOkk.Visible = false;
                    //lblS2.Visible = true;
                    //lnkGo.Visible = true;
                    //lnkpushCancel.Visible = true;
                    Response.Redirect("Staff_Employee_Master.aspx");
                    con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }
        //Get Staff Details Logic
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "select * from Officer_Master WHERE Staff_Id LIKE '%' + @Search + '%' or Id LIKE '%' + @Search + '%' OR Full_Name LIKE '%' + @Search + '%' OR Email_Id LIKE '%' + @Search + '%' OR Job_Title LIKE '%' + @Search + '%' OR Department  LIKE '%' + @Search + '%'";
                SqlCommand cmd = new SqlCommand("SP_GetStaffData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
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
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }

        }
        //Validation Logic
        protected bool Validation()
        {
            if (txtStudentID.Text == "")
            {
                lblValidstudent.Visible = true;
                lblvalidFullname.Visible = false;
                lblValidEmail.Visible = false;
                lblvalidPassword.Visible = false;
                lblValidstudentalready.Visible = false;
                txtStudentID.Focus();
                return false;
            }
            if (txtFull_Name.Text == "")
            {
                lblValidstudent.Visible = false;
                lblvalidFullname.Visible = true;
                lblValidEmail.Visible = false;
                lblvalidPassword.Visible = false;
                lblValidstudentalready.Visible = false;
                txtFull_Name.Focus();
                return false;
            }
            if (txtEmail_Id.Text == "")
            {
                lblValidstudent.Visible = false;
                lblvalidFullname.Visible = false;
                lblValidEmail.Visible = true;
                lblvalidPassword.Visible = false;
                lblValidstudentalready.Visible = false;
                txtEmail_Id.Focus();
                return false;
            }
            if (txtpassword.Text == "")
            {
                if (lnksave.Visible == true)
                {
                    lblValidstudent.Visible = false;
                    lblvalidFullname.Visible = false;
                    lblValidEmail.Visible = false;
                    lblvalidPassword.Visible = true;
                    lblValidstudentalready.Visible = false;
                    txtpassword.Focus();
                    return false;
                }
                else if (txtpassword.Text != "")
                {
                    lblValidstudent.Visible = false;
                    lblvalidFullname.Visible = false;
                    lblValidEmail.Visible = false;
                    lblvalidPassword.Visible = true;
                    lblValidstudentalready.Visible = false;
                    txtpassword.Focus();
                    return false;
                }
            }
            if (lnksave.Visible == true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select Staff_Id from Officer_Master";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Staff_Id"].ToString() == txtStudentID.Text)
                        {
                            lblValidstudent.Visible = false;
                            lblvalidFullname.Visible = false;
                            lblValidEmail.Visible = false;
                            lblvalidPassword.Visible = false;
                            lblValidstudentalready.Visible = true;
                            txtStudentID.Focus();
                            return false;
                        }
                    }
                }
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
            return true;
        }
        //Image upload Logic
        protected void ImageUpload()
        {

            if (FileUpload1.HasFile)
            {
                try
                {
                    string str = FileUpload1.FileName;
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string extension = Path.GetExtension(filename);
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/StaffImages/" + txtStudentID.Text.Replace("/", "-").Replace("\\","-")  + extension));
                    Image = "~/Images/StaffImages/" + txtStudentID.Text.Replace("/", "-").Replace("\\", "-") + extension.ToString();
                    Stream fs = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    base64string = Convert.ToBase64String(bytes, 0, bytes.Length);
                    hdnimage.Value = base64string;
                    //Save the Byte Array as File.
                    string Imagesaveasbytyte = Image;
                    File.WriteAllBytes(Server.MapPath(Imagesaveasbytyte), bytes);
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }
        protected void ImageUpload2()
        {

            if (FileUpload2.HasFile)
            {
                try
                {
                    string str = FileUpload2.FileName;
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/Images/StaffImages/" + txtStudentID.Text.Replace("/", "_") + "_" + str));
                    Imagesignature = "~/Images/StaffImages/" + txtStudentID.Text.Replace("/", "_") + "_" + str.ToString();
                    Stream fs = FileUpload2.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes2 = br.ReadBytes((Int32)fs.Length);
                    base64string2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                    //Save the Byte Array as File.
                    Imagesignature = "~/Images/StaffSignatureImages/" + Path.GetFileName(FileUpload2.FileName);
                    File.WriteAllBytes(Server.MapPath(Imagesignature), bytes2);
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }
        //Edit Staff Details Logic
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Staff_Employee_Master.aspx?Id=" + ids + "&Type=Edit");
        }
        //View Staff Details Logic
        protected void linkView_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Staff_Employee_Master.aspx?Id=" + ids + "&Type=View");
        }
        //Method for Edit Mode
        protected void EditMode()
        {
            try
            {
                divgrid.Visible = false;
                divView.Visible = true;
                lnkupdate.Visible = true;
                lnksave.Visible = false;
                lnkAdd.Visible = false;
                txtStudentID.Enabled = false;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from Officer_Master where Id= '" + Request.QueryString["Id"].ToString() + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                    txtStudentID.Text = ds.Tables[0].Rows[0]["Staff_Id"].ToString();
                    txtApplication_No.Text = ds.Tables[0].Rows[0]["Application_No"].ToString();
                    txtSL_No.Text = ds.Tables[0].Rows[0]["SL_No"].ToString();
                    txtUID.Text = ds.Tables[0].Rows[0]["UID"].ToString();
                    txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    txtFull_Name.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
                    drpGender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                    BindDepartment();
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["Department"].ToString()).Selected = true;
                    
                    BindJobTitle();
                    ddljobtitle.ClearSelection();
                    ddljobtitle.Items.FindByValue(ds.Tables[0].Rows[0]["Job_Title"].ToString()).Selected = true;
                    Bindfacility();
                    ddlFacility.ClearSelection();
                    ddlFacility.Items.FindByValue(ds.Tables[0].Rows[0]["dept_name"].ToString().Trim()).Selected = true;
                    //txtSignature.Text = ds.Tables[0].Rows[0]["Signature"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    txtID_no.Text = ds.Tables[0].Rows[0]["ID_no"].ToString();
                    txtIssue_Date.Text = ds.Tables[0].Rows[0]["Issue_Date"].ToString();
                    txtEmail_Id.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();
                    hdnpassword.Value = ds.Tables[0].Rows[0]["password"].ToString();
                    HiddenField1.Value = EncryptionDecryption.GetDecrypt(ds.Tables[0].Rows[0]["password"].ToString());
                    txtCard_no.Text = ds.Tables[0].Rows[0]["cardnumber"].ToString();
                    //txtIsactive.Text = ds.Tables[0].Rows[0]["Isactive"].ToString();
                    ddlCompany.SelectedValue = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                    ddlBloodgroup.SelectedValue = ds.Tables[0].Rows[0]["BloodGroup"].ToString();
                    txtPersonalPhone.Text = ds.Tables[0].Rows[0]["PersonalPhone"].ToString();
                    txtVehicleNumber.Text = ds.Tables[0].Rows[0]["VehicleNumber"].ToString();
                    txtEmployeeCode.Text = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                    txtamharicFullName.Text = ds.Tables[0].Rows[0]["amharicFullName"].ToString();
                    ddlCountry.ClearSelection();
                    ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["country_name"].ToString()).Selected = true;


                    if (ds.Tables[0].Rows[0]["StaffImage_Blob"].ToString() != "")
                    {
                        imgStaff.Visible = true;
                        imgStaff.ImageUrl = ds.Tables[0].Rows[0]["Emp_Photo"].ToString();
                        //byte[] hash1 = (byte[])ds.Tables[0].Rows[0]["StaffImage_Blob"];
                        //imgStaff.ImageUrl = "data:image;base64," + Convert.ToBase64String(hash1);
                        hdnimage.Value= ds.Tables[0].Rows[0]["Image64byte"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Emp_Photo"].ToString() == "")
                    {
                        imgStaff.ImageUrl = "~/Images/student.jpeg";
                    }
                    else
                    {
                        string path = Server.MapPath(ds.Tables[0].Rows[0]["Emp_Photo"].ToString());
                        if(Checkpath.CheckPathExitsOrNot(path))
                        {
                            imgStaff.ImageUrl = ds.Tables[0].Rows[0]["Emp_Photo"].ToString();
                        }
                        else
                        {
                            imgStaff.ImageUrl = "~/Images/student.jpeg";
                        }
                    }
                    if (ds.Tables[0].Rows[0]["Signature"].ToString() != "")
                    {
                        imgSignature.Visible = true;
                        imgSignature.ImageUrl = ds.Tables[0].Rows[0]["Signature"].ToString();
                        //byte[] hash1 = (byte[])ds.Tables[0].Rows[0]["StaffImage_Blob"];
                        //imgStaff.ImageUrl = "data:image;base64," + Convert.ToBase64String(hash1);
                    }
                    else if (ds.Tables[0].Rows[0]["Signature"].ToString() == "")
                    {
                        imgSignature.Visible = true;
                        imgSignature.ImageUrl = "~/Images/student.jpeg";
                    }
                    if (ds.Tables[0].Rows[0]["Canteen_ID"].ToString() != "")
                    {
                        ddlcanteen2.ClearSelection();
                        ddlcanteen2.Items.FindByValue(ds.Tables[0].Rows[0]["Canteen_ID"].ToString()).Selected = true;
                    }
                    ddlCardstatus.ClearSelection();
                    ddlCardstatus.Items.FindByValue(ds.Tables[0].Rows[0]["Cardstatus"].ToString()).Selected = true;
                    txtWoreda.Text = ds.Tables[0].Rows[0]["Woreda"].ToString();
                    txtSubcity.Text = ds.Tables[0].Rows[0]["sub_city"].ToString();
                    txtHouseNumber.Text = ds.Tables[0].Rows[0]["house_number"].ToString();
                    //txtNationality.Text = ds.Tables[0].Rows[0]["Nationality"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        //Method for View Mode
        protected void ViewMode()
        {
            try
            {
                divgrid.Visible = false;
                divView.Visible = true;
                lnkupdate.Visible = false;
                lnksave.Visible = false;
                lnkAdd.Visible = false;
                txtStudentID.Enabled = false;
                txtApplication_No.Enabled = false;
                txtSL_No.Enabled = false;
                txtUID.Enabled = false;
                txtDOB.Enabled = false;
                txtFull_Name.Enabled = false;
                drpGender.Enabled = false;
                ddlDepartment.Enabled = false;
                ddljobtitle.Enabled = false;
                ddlFacility.Enabled = false;
                txtAddress.Enabled = false;
                txtID_no.Enabled = false;
                txtIssue_Date.Enabled = false;
                txtEmail_Id.Enabled = false;
                txtpassword.Enabled = false;
                txtCard_no.Enabled = false;
                ddlcanteen2.Enabled = false;
                //txtIsactive.Enabled = false;
                ddlCompany.Enabled = false;
                ddlBloodgroup.Enabled = false;
                txtPersonalPhone.Enabled = false;
                txtVehicleNumber.Enabled = false;
                ddlCountry.Enabled = false;
                txtUID.Enabled = false;
                FileUpload1.Visible = false;
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkEdit_menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("Staff_Employee_Master.aspx?Id=" + lblId.Text + "&Type=Edit");
        }

        protected void lnkView_Menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("Staff_Employee_Master.aspx?Id=" + lblId.Text + "&Type=View");
        }

        protected void lnkviewgrid_Click(object sender, EventArgs e)
        {
            Response.Redirect("Staff_Employee_Master.aspx");
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

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Staff_Employee_Master.aspx");
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
                    ddlcanteen2.Items.Insert(0, new ListItem("Select Canteen", "0"));
                }
                else
                {
                    ddlcanteen2.DataSource = null;
                    ddlcanteen2.DataBind();
                    ddlcanteen2.Items.Insert(0, new ListItem("Select Canteen", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void Bindfacility()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BindEmpFacility", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    ddlFacility.DataSource = ds1;
                    ddlFacility.DataTextField = "Name";
                    ddlFacility.DataValueField = "Id";
                    ddlFacility.DataBind();
                    ddlFacility.Items.Insert(0, new ListItem("Select Facility", "0"));
                }
                else
                {
                    ddlFacility.DataSource = null;
                    ddlFacility.DataBind();
                    ddlFacility.Items.Insert(0, new ListItem("Select Facility", "0"));

                }
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
        protected void BindCompany()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindCompany_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    ddlCompany.DataSource = ds1;
                    ddlCompany.DataTextField = "Name";
                    ddlCompany.DataValueField = "Id";
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));
                }
                else
                {
                    ddlCompany.DataSource = null;
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindCountry()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindCountry_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    ddlCountry.DataSource = ds1;
                    ddlCountry.DataTextField = "Name";
                    ddlCountry.DataValueField = "Id";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, new ListItem("Select Country", "0"));
                }
                else
                {
                    ddlCountry.DataSource = null;
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, new ListItem("Select Country", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindJobTitle()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindJobTitle_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    ddljobtitle.DataSource = ds1;
                    ddljobtitle.DataTextField = "Name";
                    ddljobtitle.DataValueField = "Id";
                    ddljobtitle.DataBind();
                    ddljobtitle.Items.Insert(0, new ListItem("Select Job Title", "0"));
                }
                else
                {
                    ddljobtitle.DataSource = null;
                    ddljobtitle.DataBind();
                    ddljobtitle.Items.Insert(0, new ListItem("Select Job Title", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void lnkClear_Click(object sender, EventArgs e)
        {
            txtAddress.Text = "";
            txtApplication_No.Text = "";
            txtCard_no.Text = "";
            txtDOB.Text = "";
            txtEmail_Id.Text = "";
            txtFull_Name.Text = "";
            txtID_no.Text = "";
            txtIssue_Date.Text = "";
            txtpassword.Text = "";
            txtPersonalPhone.Text = "";
            txtSL_No.Text = "";
            txtUID.Text = "";
            txtVehicleNumber.Text = "";
            drpGender.SelectedValue = "0";
            ddlBloodgroup.SelectedValue = "0";
            ddlCardstatus.SelectedValue = "0";
            ddlcanteen2.SelectedValue = "0";
            ddlDepartment.SelectedValue = "0";
            ddlFacility.SelectedValue = "0";
            ddlCountry.SelectedValue = "0";
            ddlCompany.SelectedValue = "0";
            ddljobtitle.SelectedValue = "0";
            if (Request.QueryString["Type"] == null)
            {
                txtStudentID.Text = "";
            }
        }

        protected void lnksyncjob_Click(object sender, EventArgs e)
        {
            //DivSync.Style.Remove("display");
            //DivSync.Attributes.Add("style", "display:block;");
            //lnksyncyes.Visible = false;
            //lnksyncyes2.Visible = true;
            //Label2.Visible = false;
            //Label3.Visible = true;
            SqlCommand cmd = new SqlCommand("SyncStaff", con);
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
            SqlCommand cmd = new SqlCommand("SyncAllStaffMasters", con);
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
            Response.Redirect("Staff_Employee_Master.aspx");
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
            string msg = S2API.GetStaffAPI(txtStudentID.Text);

            if (msg == "FAIL")
            {
                if (ddlDepartment.SelectedIndex == 0) ddlDepartment.SelectedItem.Text = "";

                string modifymsg = S2API.AddStaffAPI(txtStudentID.Text, txtFull_Name.Text,drpGender.SelectedItem.Text,
                ddlDepartment.SelectedItem.Text, txtPersonalPhone.Text,txtEmail_Id.Text,txtAddress.Text,txtUID.Text,
                txtVehicleNumber.Text,"",txtIssue_Date.Text,hdnimage.Value);

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
                    string Responsmsg = S2API.AddStaffCardnumberApi(txtStudentID.Text, txtCard_no.Text, status);
                    Responsmsg = S2API.AddStaffUHFApi(txtStudentID.Text, txtUID.Text, status);
                    Response.Redirect("Staff_Employee_Master.aspx");
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
                if (ddlDepartment.SelectedIndex == 0) ddlDepartment.SelectedItem.Text = "";

                string modifymsg = S2API.UpdateStaffAPI(txtStudentID.Text, txtFull_Name.Text, drpGender.SelectedItem.Text,
                ddlDepartment.SelectedItem.Text, txtPersonalPhone.Text, txtEmail_Id.Text, txtAddress.Text, txtUID.Text,
                txtVehicleNumber.Text, "", txtIssue_Date.Text, hdnimage.Value);

                string status = "";
                if (ddlCardstatus.SelectedItem.Text.ToUpper() == "ACTIVE")
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
                    Responsmsg = S2API.AddStaffCardnumberApi(txtStudentID.Text, txtCard_no.Text, status);
                    Responsmsg = S2API.AddStaffUHFApi(txtStudentID.Text, txtUID.Text, status);
                }
                if (Responsmsg != "SUCCESS" && Responsmsg != "")
                {
                    Responsmsg = S2API.ModifyStaffCardnumberApi(txtStudentID.Text, txtCard_no.Text, status);
                    Responsmsg = S2API.ModifyStaffUHFApi(txtStudentID.Text, txtUID.Text, status);
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
            SqlCommand cmd = new SqlCommand("SyncAllStaffMasters", con);
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
            SqlCommand cmd = new SqlCommand("SyncStaff", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            lblSyncmsg.Visible = true;
            FillGrid();
        }

        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label path = e.Row.FindControl("lblpath") as Label;
                    Image Staffpath = e.Row.FindControl("imgStaff") as Image;
                    Image imgdefault = e.Row.FindControl("imgdefault") as Image;
                    if(Checkpath.CheckPathExitsOrNot(Server.MapPath(path.Text)))
                    {
                        Staffpath.Visible = true;
                        imgdefault.Visible = false;
                    }
                    else
                    {
                        Staffpath.Visible = false;
                        imgdefault.Visible = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
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
            string filePath = Server.MapPath(@"\ImportExcel\Staff\Staff.xlsx");
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
                    string path = Server.MapPath("~/ImportExcel/Staff/");
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
                        dtExcelData.Columns.AddRange(new DataColumn[25] {
                        new DataColumn("Staff  Id", typeof(string)),
                        new DataColumn("Full Name", typeof(string)),
                        new DataColumn("Employee Code", typeof(string)),
                        new DataColumn("amharic Name", typeof(string)),
                        new DataColumn("Date Of Birth", typeof(string)),
                        new DataColumn("Gender", typeof(string)),
                        new DataColumn("Blood Group", typeof(string)),
                        new DataColumn("Personal Phone", typeof(string)),
                        new DataColumn("Email Address", typeof(string)),
                        new DataColumn("password", typeof(string)),
                        new DataColumn("Address", typeof(string)),
                        new DataColumn("Country Name", typeof(string)),
                        new DataColumn("Department", typeof(string)),
                        new DataColumn("Job Title", typeof(string)),
                        new DataColumn("Company Name", typeof(string)),
                        new DataColumn("Issue Date", typeof(string)),
                        new DataColumn("Facility", typeof(string)),
                        new DataColumn("UHF", typeof(string)),
                        new DataColumn("Plate No", typeof(string)),
                        new DataColumn("Card Status", typeof(string)),
                        new DataColumn("Card Number", typeof(string)),
                        new DataColumn("Canteen", typeof(string)),
                        new DataColumn("Woreda", typeof(string)),
                        new DataColumn("Sub City", typeof(string)),
                        new DataColumn("House Number", typeof(string))
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
                                //Set the database table name Staff
                                sqlBulkCopy.DestinationTableName = "dbo.officer_master";
                                sqlBulkCopy.ColumnMappings.Add("Staff  Id", "Staff_Id");
                                sqlBulkCopy.ColumnMappings.Add("Full Name", "Full_Name");
                                sqlBulkCopy.ColumnMappings.Add("Employee Code", "EmployeeCode");
                                sqlBulkCopy.ColumnMappings.Add("amharic Name", "amharicFullName");
                                sqlBulkCopy.ColumnMappings.Add("Date Of Birth", "DOB");
                                sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                                sqlBulkCopy.ColumnMappings.Add("Blood Group", "BloodGroup");
                                sqlBulkCopy.ColumnMappings.Add("Personal Phone", "PersonalPhone");
                                sqlBulkCopy.ColumnMappings.Add("Email Address", "Email_Id");
                                sqlBulkCopy.ColumnMappings.Add("password", "password");
                                sqlBulkCopy.ColumnMappings.Add("Address", "Address");
                                sqlBulkCopy.ColumnMappings.Add("Country Name", "country_name");
                                sqlBulkCopy.ColumnMappings.Add("Department", "Department");
                                sqlBulkCopy.ColumnMappings.Add("Job Title", "Job_Title");
                                sqlBulkCopy.ColumnMappings.Add("Company Name", "CompanyName");
                                sqlBulkCopy.ColumnMappings.Add("Issue Date", "Issue_Date");
                                sqlBulkCopy.ColumnMappings.Add("Facility", "dept_name");
                                sqlBulkCopy.ColumnMappings.Add("UHF", "UID");
                                sqlBulkCopy.ColumnMappings.Add("Plate No", "VehicleNumber");
                                sqlBulkCopy.ColumnMappings.Add("Card Status", "cardstatus");
                                sqlBulkCopy.ColumnMappings.Add("Card Number", "cardnumber");
                                sqlBulkCopy.ColumnMappings.Add("Canteen", "Canteen_ID");
                                sqlBulkCopy.ColumnMappings.Add("Woreda", "woreda");
                                sqlBulkCopy.ColumnMappings.Add("Sub City", "sub_city");
                                sqlBulkCopy.ColumnMappings.Add("House Number", "house_number");
                                con.Open();
                                sqlBulkCopy.WriteToServer(dtExcelData);
                                con.Close();
                            }
                        }
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name Users
                                sqlBulkCopy.DestinationTableName = "dbo.Users";
                                sqlBulkCopy.ColumnMappings.Add("Staff  Id", "staffid");
                                sqlBulkCopy.ColumnMappings.Add("Full Name", "Name");
                                sqlBulkCopy.ColumnMappings.Add("Email Address", "Email");
                                sqlBulkCopy.ColumnMappings.Add("password", "Password");
                                con.Open();
                                sqlBulkCopy.WriteToServer(dtExcelData);
                                con.Close();
                            }
                            BS.UpdateImagePath("Staff");
                            FillGrid();
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
            Response.Redirect("Staff_Employee_Master.aspx");
        }
        #endregion
    }
}