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
using SMS.Class;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using QRCoder;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using AjaxControlToolkit;
using System.Xml;

namespace SMS
{
    public partial class SMS_Visitor_Master : System.Web.UI.Page
    {
        Admin_Master Master = new Admin_Master();
        LogFile LogFile = new LogFile();
        string Image = "";
        byte[] bytes = new byte[1];
        string QRimages = "";
        string listServicesName = "";
        string code = "";
        string ImgPath = "";
        string VisitorImage = "";
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!IsPostBack)
            {
                try
                {
                    BindDropdown();
                    if (Request.QueryString["Add"] == "Add pre-Reg")
                    {
                        PreReg();
                    }
                    else if (Request.QueryString["Id"] != null && Request.QueryString["Type"].ToString() == "Edit")
                    {
                        EditMode();
                    }
                    //else if (Request.QueryString["Search_Registration_Number"] == "Search Registration Number")
                    //{
                    //    SearchRegNumber();
                    //}
                    else if (Request.QueryString["QRCode"] == "Scan QR")
                    {
                        SearchQRCode();
                    }
                    else if (Request.QueryString["CopyVisitor"] != null)
                    {
                        CopyQR();
                    }

                    else if (Request.QueryString["Id"] != null && Request.QueryString["Type"].ToString() == "View")
                    {
                        EditMode();
                        ViewMode();
                        lnkviewgrid.Visible = true;
                        lnkView_Menu.Visible = false;
                        lnkEdit_menu.Visible = true;
                        divSLN.Visible = true;
                        txtSLN_ACS_Visitor_Info.Enabled = false;
                        divScanQR.Visible = false;
                        lnkCopy.Visible = false;
                    }

                    else if (Request.QueryString["Type"] == "Asign")
                    {
                        divgrid.Visible = false;
                        divView.Visible = false;
                        lnkupdate.Visible = false;
                        lnksave.Visible = false;
                        lnkAdd.Visible = false;
                        txtSLN_ACS_Visitor_Info.Enabled = false;
                        divasignCarsnumber.Visible = true;
                        divServedBy.Visible = false;
                        divScanQR.Visible = false;
                        lnkCopy.Visible = false;
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "select * from SMS_Visitors where SLN_ACS_Visitor_Info= '" + Request.QueryString["Id"].ToString() + "'";
                        cmd.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblAsignFirstName.Text = ds.Tables[0].Rows[0]["First_Name"].ToString();
                            lblAsignLastName.Text = ds.Tables[0].Rows[0]["Last_Name"].ToString();
                            //txtCompany.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                        }
                        lnkviewgrid.Visible = true;
                        lnkView_Menu.Visible = true;
                        lnkEdit_menu.Visible = false;
                        divSLN.Visible = true;
                        txtSLN_ACS_Visitor_Info.Enabled = false;
                    }
                    else if (Request.QueryString["Type"] == "Servedby")
                    {
                        divgrid.Visible = false;
                        divView.Visible = false;
                        lnkupdate.Visible = false;
                        lnksave.Visible = false;
                        lnkAdd.Visible = false;
                        divScanQR.Visible = false;
                        lnkCopy.Visible = false;
                        txtSLN_ACS_Visitor_Info.Enabled = false;
                        divasignCarsnumber.Visible = false;
                        divServedBy.Visible = true;
                        GetServicesDropdown();

                    }
                    else if (Request.QueryString["Type"] == "View_Served")
                    {
                        divgrid.Visible = false;
                        divView.Visible = false;
                        lnkupdate.Visible = false;
                        lnksave.Visible = false;
                        lnkAdd.Visible = false;
                        txtSLN_ACS_Visitor_Info.Enabled = false;
                        divasignCarsnumber.Visible = false;
                        divServedBy.Visible = false;
                        DivViewServedby.Visible = true;
                        divScanQR.Visible = false;
                        lnkCopy.Visible = false;
                        string htmlForImages = String.Empty;
                        SqlCommand cmd2 = new SqlCommand("SP_Get_Service_Notification", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@Flag", "GetServedDetailsServices");
                        cmd2.Parameters.AddWithValue("@EmpID", "");
                        cmd2.Parameters.AddWithValue("@VisitorId", Request.QueryString["Id"].ToString());
                        cmd2.Connection = con;
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
                        DataTable ds2 = new DataTable();
                        da1.Fill(ds2);
                        if (ds2.Rows.Count > 0)
                        {
                            for (int i = 0; i < ds2.Rows.Count; i++)
                                htmlForImages += "<span style='font-weight:bold;color:greenyellow;font-size:medium;'> " +
                                    ds2.Rows[i]["Name"].ToString() + " Served by " +
                                    ds2.Rows[i]["Full_Name"].ToString() + " at Datetime " + ds2.Rows[i]["ServedDatetime"].ToString()
                                    + ".</span><br><br>";

                            divServed.InnerHtml = htmlForImages;
                        }
                    }
                    else
                    {
                        divgrid.Visible = true;
                        divView.Visible = false;
                        lnkCopy.Visible = false;
                        divScanQR.Visible = false;
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
                Session["Canteen_Flag"] = null;
                Session["Session_Id"] = null;
                Session["CanteenName"] = null;
                Session["CanteenCount"] = null;
                Session["Sider"] = "Visitor Master";
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
        protected void BindDropdown()
        {
            if (Request.QueryString["Add"] == null)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            BindVisitorType();
            BindVisitorReason();
            BindVisitorAccesslevel();
            BindServices();
        }
        protected void PreReg()
        {
            txtValidFromDatetime.Text = DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 AM";
            txtValidToDatetime.Text = DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 PM";
            divgrid.Visible = false;
            lnkupdate.Visible = false;
            lnksave.Visible = true;
            divgrid.Visible = false;
            divView.Visible = true;
            divSLN.Visible = true;
            lnkAdd.Visible = false;
            divScanQR.Visible = false;
            lnkCopy.Visible = false;
            lnkCheckIn.Visible = false;
            lnkCheckOut.Visible = false;
            //lnkmodifys2.Visible = false;
            h1selfvisitor.Visible = false;
            h3visitor.Visible = false;
            h1previsitor.Visible = true;
            h1checkinout.Visible = false;
            lnkpushtos2.Visible = false;
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
                lnkviewgrid.Visible = true;
                lnkView_Menu.Visible = true;
                lnkEdit_menu.Visible = false;
                divSLN.Visible = true;
                txtSLN_ACS_Visitor_Info.Enabled = false;
                divScanQR.Visible = false;
                lnkCopy.Visible = false;
                lnkCheckIn.Visible = false;
                lnkCheckOut.Visible = false;
                //lnkmodifys2.Visible = false;
                lnkpushtos2.Visible = false;
                SqlCommand cmd = new SqlCommand("SP_SMS_Edit_VisitorMastor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Id");
                cmd.Parameters.AddWithValue("@SLN_ACS_Visitor_Info", Request.QueryString["Id"].ToString());
                //cmd.CommandText = "select * from SMS_Visitors where SLN_ACS_Visitor_Info= '" + Request.QueryString["Id"].ToString() + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSLN_ACS_Visitor_Info.Text = ds.Tables[0].Rows[0]["SLN_ACS_Visitor_Info"].ToString();
                    txtVisitor_RecId.Text = ds.Tables[0].Rows[0]["Visitor_RecId"].ToString();
                    txtVisitor_GUID.Text = ds.Tables[0].Rows[0]["Visitor_GUID"].ToString();
                    txtFirst_Name.Text = ds.Tables[0].Rows[0]["First_Name"].ToString();
                    ddlVisitor_Type.ClearSelection();
                    ddlVisitor_Type.Items.FindByValue(ds.Tables[0].Rows[0]["Visitor_Type"].ToString()).Selected = true;
                    txtLast_Name.Text = ds.Tables[0].Rows[0]["Last_Name"].ToString();
                    txtCompany.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                    ddlVisit_Reason.ClearSelection();
                    ddlVisit_Reason.Items.FindByValue(ds.Tables[0].Rows[0]["Visit_Reason"].ToString()).Selected = true;
                    txtPhone_Number.Text = ds.Tables[0].Rows[0]["Phone_Number"].ToString();
                    txtID_Number.Text = ds.Tables[0].Rows[0]["ID_Number"].ToString();
                    txtNational_ID.Text = ds.Tables[0].Rows[0]["National_ID"].ToString();
                    txtHost_Employee_Code.Text = Session["UserName"].ToString();
                    txtAccess_Card_Number.Text = ds.Tables[0].Rows[0]["Access_Card_Number"].ToString();
                    txtEmail_Id.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();
                    txtValid_From.Text = ds.Tables[0].Rows[0]["Valid_From"].ToString();
                    txtValid_To.Text = ds.Tables[0].Rows[0]["Valid_To"].ToString();
                    ddlAccess_Level.ClearSelection();
                    ddlAccess_Level.Items.FindByValue(ds.Tables[0].Rows[0]["Access_Level"].ToString()).Selected = true;
                    if (ds.Tables[0].Rows[0]["Visitor_Photo"].ToString() == "")
                    {
                        imgStaff.ImageUrl = "~/Images/visitors.png";
                        ImgPath = "~/Images/visitors.png";
                    }
                    else
                    {
                        imgStaff.ImageUrl = ds.Tables[0].Rows[0]["Visitor_Photo"].ToString();
                        ImgPath = ds.Tables[0].Rows[0]["Visitor_Photo"].ToString();
                    }
                    lblVisitorRegno.Text = ds.Tables[0].Rows[0]["Visitor_Reg_No"].ToString();
                    txtCheck_In.Text = ds.Tables[0].Rows[0]["Check_In"].ToString();
                    txtCheck_Out.Text = ds.Tables[0].Rows[0]["Check_Out"].ToString();
                    txtCreatedBy.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                    txtLastUpdatedBy.Text = ds.Tables[0].Rows[0]["LastUpdatedBy"].ToString();
                    ddlCardstatus.ClearSelection();
                    ddlCardstatus.Items.FindByValue(ds.Tables[0].Rows[0]["RecordStatus"].ToString()).Selected = true;
                    Emplist(ds.Tables[0].Rows[0]["EmpServiceId"].ToString());
                    MoveEmp(ds.Tables[0].Rows[0]["EmpServiceId"].ToString());
                    txtValidFromDatetime.Text = ds.Tables[0].Rows[0]["Valid_From_Datetime"].ToString();
                    txtValidToDatetime.Text = ds.Tables[0].Rows[0]["Valid_To_Datetime"].ToString();
                    lblstatusNone.Text = ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString();
                    txtAsignCardNumber.Text = ds.Tables[0].Rows[0]["Access_Card_Number"].ToString();
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = "select * from logo";
                    cmd1.Connection = con;
                    cmd1.ExecuteNonQuery();
                    SqlDataAdapter dalogo = new SqlDataAdapter(cmd1);
                    DataSet dslogo = new DataSet();
                    dalogo.Fill(dslogo);
                    if (dslogo.Tables[0].Rows.Count > 0)
                    {
                        txtlogoname.Text = dslogo.Tables[0].Rows[0]["Name"].ToString();
                        string ImgPath = dslogo.Tables[0].Rows[0]["Images"].ToString().Replace("~/Images/ICON", "Images/ICON");
                        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(ImgPath));

                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            string base64String = Convert.ToBase64String(imageBytes);
                            imglogo.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }

                    con.Close();
                    if (ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString() == "SelfRegistered")
                    {
                        h3visitor.Visible = false;
                        h1previsitor.Visible = false;
                        h1selfvisitor.Visible = true;
                        h1checkinout.Visible = false;
                        divValidFromDatetime.Visible = false;
                        divValidToDatetime.Visible = false;
                        divCardNumber_Status.Visible = false;
                        divAccess.Visible = false;
                        lblStatus.Text = "[ Status : Self-Registered ]";
                        lblStatus.Attributes.Add("style", "color:Blue;");
                        lnkpushtos2.Visible = false;
                    }
                    else if (ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString() == "PreRegistered")
                    {
                        h3visitor.Visible = false;
                        h1previsitor.Visible = true;
                        h1selfvisitor.Visible = false;
                        h1checkinout.Visible = false;
                        divValidFromDatetime.Visible = true;
                        divValidToDatetime.Visible = true;
                        divCardNumber_Status.Visible = false;
                        divAccess.Visible = false;
                        lblStatus.Text = "[ Status : Pre-Registered ]";
                        lblStatus.Attributes.Add("style", "color:Blue;");
                        lnkpushtos2.Visible = false;
                    }
                    else if (ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString() == "CheckedIn")
                    {
                        Disable();
                        divValidFromDatetime.Visible = true;
                        divValidToDatetime.Visible = true;
                        //divCardNumber_Status.Visible = true;
                        divAccess.Visible = true;
                        txtValidFromDatetime.Enabled = false;
                        txtValidToDatetime.Enabled = false;
                        txtAsignCardNumber.Enabled = false;
                        lblStatus.Text = "[ Status : Checked-In ]";
                        lblStatus.Attributes.Add("style", "color:Green;");
                        h3visitor.Visible = false;
                        h1previsitor.Visible = false;
                        h1selfvisitor.Visible = false;
                        h1checkinout.Visible = true;
                        lnkpushtos2.Visible = false;
                        lnkmodifys2.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString() == "CheckedOut")
                    {
                        Disable();
                        lnkupdate.Visible = false;
                        divValidFromDatetime.Visible = true;
                        divValidToDatetime.Visible = true;
                        //divCardNumber_Status.Visible = true;
                        divAccess.Visible = true;
                        txtValidFromDatetime.Enabled = false;
                        txtValidToDatetime.Enabled = false;
                        txtAsignCardNumber.Enabled = false;
                        lblStatus.Text = "[ Status : Checked-Out ]";
                        lblStatus.Attributes.Add("style", "color:Red;");
                        h3visitor.Visible = false;
                        h1previsitor.Visible = false;
                        h1selfvisitor.Visible = false;
                        h1checkinout.Visible = true;
                        lnkpushtos2.Visible = false;
                        lnkmodifys2.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void CheckIn()
        {
            if (Validation1())
            {
                try
                {
                    ImageUpload();
                    SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AsignCardNumber");
                    cmd.Parameters.AddWithValue("@Visitor_RecId", txtVisitor_RecId.Text);
                    cmd.Parameters.AddWithValue("@Visitor_GUID", txtVisitor_GUID.Text);
                    cmd.Parameters.AddWithValue("@First_Name", txtFirst_Name.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", txtLast_Name.Text);
                    cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Type", ddlVisitor_Type.SelectedValue);
                    cmd.Parameters.AddWithValue("@Visit_Reason", ddlVisit_Reason.SelectedValue);
                    cmd.Parameters.AddWithValue("@Phone_Number", txtPhone_Number.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Photo", Image);
                    cmd.Parameters.AddWithValue("@ID_Number", txtID_Number.Text);
                    cmd.Parameters.AddWithValue("@National_ID", txtNational_ID.Text);
                    cmd.Parameters.AddWithValue("@Host_Employee_Code", Session["UserName"].ToString());
                    cmd.Parameters.AddWithValue("@Access_Card_Number", txtAsignCardNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Access_Level", ddlAccess_Level.SelectedValue);
                    cmd.Parameters.AddWithValue("@Check_In", txtCheck_In.Text);
                    cmd.Parameters.AddWithValue("@Valid_To", txtValid_To.Text);
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                    cmd.Parameters.AddWithValue("@Valid_From", txtValid_From.Text);
                    cmd.Parameters.AddWithValue("@Check_Out", txtCheck_Out.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text);
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", txtLastUpdatedBy.Text);
                    cmd.Parameters.AddWithValue("@RecordStatus", ddlCardstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@Valid_From_Datetime", txtValidFromDatetime.Text);
                    cmd.Parameters.AddWithValue("@Valid_To_Datetime", txtValidToDatetime.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Reg_No", Request.QueryString["CopyVisitor"].ToString());
                    cmd.Parameters.AddWithValue("@hash", bytes);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ModalPopupExtender1.Attributes.Add("style", "display:none;");
                    ModalPopupExtender3.Attributes.Add("style", "display:none;");
                    //ModalPopupExtender2.Show();
                    // div3.Visible = true;
                    lblmsg.Text = " Visitor checked-In successfully!!<br>Registration number : " + Request.QueryString["CopyVisitor"].ToString();
                    //Response.Redirect("SMS_Visitor_Master.aspx?CopyVisitor=" + Request.QueryString["CopyVisitor"].ToString());
                    myModal.Attributes.Add("style", "display:block;");
                    mymodal2.Attributes.Add("style", "display:none;");
                    lnkOkk.Visible = true;
                    lblS2.Visible = false;
                    lnkGo.Visible = false;
                    lnkpushCancel.Visible = false;
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }
        protected void CheckOut()
        {
            try
            {
                string ids = "";
                ids = string.Empty;
                //   ids = (sender as LinkButton).CommandArgument;
                SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "RemoveCardNumber");
                cmd.Parameters.AddWithValue("@Valid_From_Datetime", txtValidFromDatetime.Text);
                cmd.Parameters.AddWithValue("@Valid_To_Datetime", txtValidToDatetime.Text);
                cmd.Parameters.AddWithValue("@Access_Card_Number", Request.QueryString["CopyVisitor"].ToString());
                cmd.Parameters.AddWithValue("@hash", bytes);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ModalPopupExtender1.Attributes.Add("style", "display:none;");
                ModalPopupExtender3.Attributes.Add("style", "display:none;");
                //ModalPopupExtender2.Show();
                //div3.Visible = true;
                lblmsg.Text = "Visitor checked-Out successfully!!<br/> Registration number : " + Request.QueryString["CopyVisitor"].ToString();
                //Response.Redirect("SMS_Visitor_Master.aspx");
                myModal.Attributes.Add("style", "display:block;");
                mymodal2.Attributes.Add("style", "display:none;");
                lnkOkk.Visible = true;
                lblS2.Visible = false;
                lnkGo.Visible = false;
                lnkpushCancel.Visible = false;
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void SearchRegNumber()
        {
            divgrid.Visible = false;
            lnkupdate.Visible = false;
            lnksave.Visible = false;
            divgrid.Visible = false;
            divView.Visible = false;
            divSLN.Visible = false;
            lnkAdd.Visible = false;
            divScanQR.Visible = false;
            lnkCopy.Visible = false;
            divSearchRegistrationNumber.Visible = true;
        }
        protected void SearchQRCode()
        {
            divgrid.Visible = false;
            lnkupdate.Visible = false;
            lnksave.Visible = false;
            divgrid.Visible = false;
            divView.Visible = false;
            divSLN.Visible = false;
            lnkAdd.Visible = false;
            divScanQR.Visible = true;
            lnkCopy.Visible = false;
            divSearchRegistrationNumber.Visible = false;
        }
        protected void Disable()
        {
            txtFirst_Name.Enabled = false;
            txtLast_Name.Enabled = false;
            txtCompany.Enabled = false;
            txtEmail_Id.Enabled = false;
            txtPhone_Number.Enabled = false;
            txtNational_ID.Enabled = false;
            ddlAccess_Level.Enabled = false;
            ddlservices.Enabled = false;
            ddlCardstatus.Enabled = false;
            ddlVisitor_Type.Enabled = false;
            ddlVisit_Reason.Enabled = false;
            lstEmp.Enabled = false;
            lstmoveemp.Enabled = false;
        }
        protected void Enable()
        {
            txtFirst_Name.Enabled = true;
            txtLast_Name.Enabled = true;
            txtCompany.Enabled = true;
            txtEmail_Id.Enabled = true;
            txtPhone_Number.Enabled = true;
            txtNational_ID.Enabled = true;
            ddlAccess_Level.Enabled = true;
            ddlservices.Enabled = true;
            ddlCardstatus.Enabled = true;
            ddlVisitor_Type.Enabled = true;
            ddlVisit_Reason.Enabled = true;
            lstEmp.Enabled = true;
            lstmoveemp.Enabled = true;
        }
        protected void CopyQR()
        {
            try
            {
                if (File.Exists(Server.MapPath(@"\XML\Visitor_Badge.label")))
                {
                    txtxml.Text = File.ReadAllText(Server.MapPath(@"\XML\Visitor_Badge.label"));
                }
                else
                {
                    txtxml.Text = "";
                }
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "select * from SMS_Visitors where Visitor_Reg_No= '" + Request.QueryString["CopyVisitor"].ToString() + "'";
                cmd1.Connection = con;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lnkupdate.Visible = false;
                    lnkAdd.Visible = false;
                    lnkviewgrid.Visible = true;
                    lnkView_Menu.Visible = false;
                    lnkEdit_menu.Visible = true;
                    divSLN.Visible = true;
                    txtSLN_ACS_Visitor_Info.Enabled = false;
                    divScanQR.Visible = false;
                    lnkCopy.Visible = true;
                    SqlCommand cmd = new SqlCommand("SP_SMS_Edit_VisitorMastor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Flag", "VRN");
                    cmd.Parameters.AddWithValue("@SLN_ACS_Visitor_Info", Request.QueryString["CopyVisitor"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtSLN_ACS_Visitor_Info.Text = ds.Tables[0].Rows[0]["SLN_ACS_Visitor_Info"].ToString();
                        txtvisitorregno.Text = Request.QueryString["CopyVisitor"].ToString();
                        txtVisitor_RecId.Text = ds.Tables[0].Rows[0]["Visitor_RecId"].ToString();
                        txtVisitor_GUID.Text = ds.Tables[0].Rows[0]["Visitor_GUID"].ToString();
                        txtFirst_Name.Text = ds.Tables[0].Rows[0]["First_Name"].ToString();
                        ddlVisitor_Type.ClearSelection();
                        ddlVisitor_Type.Items.FindByValue(ds.Tables[0].Rows[0]["Visitor_Type"].ToString()).Selected = true;
                        txtLast_Name.Text = ds.Tables[0].Rows[0]["Last_Name"].ToString();
                        txtCompany.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                        ddlVisit_Reason.ClearSelection();
                        ddlVisit_Reason.Items.FindByValue(ds.Tables[0].Rows[0]["Visit_Reason"].ToString()).Selected = true;
                        txtPhone_Number.Text = ds.Tables[0].Rows[0]["Phone_Number"].ToString();
                        txtID_Number.Text = ds.Tables[0].Rows[0]["ID_Number"].ToString();
                        txtNational_ID.Text = ds.Tables[0].Rows[0]["National_ID"].ToString();
                        txtHost_Employee_Code.Text = Session["UserName"].ToString();
                        txtAccess_Card_Number.Text = ds.Tables[0].Rows[0]["Access_Card_Number"].ToString();
                        txtEmail_Id.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();
                        txtValid_From.Text = ds.Tables[0].Rows[0]["Valid_From"].ToString();
                        txtValid_To.Text = ds.Tables[0].Rows[0]["Valid_To"].ToString();
                        ddlAccess_Level.ClearSelection();
                        ddlAccess_Level.Items.FindByValue(ds.Tables[0].Rows[0]["Access_Level"].ToString()).Selected = true;
                        con.Open();
                        SqlCommand cmdlogo = new SqlCommand();
                        cmdlogo.CommandText = "select * from logo";
                        cmdlogo.Connection = con;
                        cmdlogo.ExecuteNonQuery();
                        SqlDataAdapter dalogo = new SqlDataAdapter(cmdlogo);
                        DataSet dslogo = new DataSet();
                        dalogo.Fill(dslogo);
                        if (dslogo.Tables[0].Rows.Count > 0)
                        {
                            txtlogoname.Text = dslogo.Tables[0].Rows[0]["Name"].ToString();
                            string ImgPath = dslogo.Tables[0].Rows[0]["Images"].ToString().Replace("~/Images/ICON", "Images/ICON");
                            if (File.Exists(Server.MapPath(@ImgPath)))
                            {
                                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(ImgPath));
                                using (MemoryStream m = new MemoryStream())
                                {
                                    image.Save(m, image.RawFormat);
                                    byte[] imageBytes = m.ToArray();

                                    // Convert byte[] to Base64 String
                                    string base64String = Convert.ToBase64String(imageBytes);
                                    imglogo.ImageUrl = "data:image/png;base64," + base64String;
                                }
                            }
                            else
                            {
                                imgStaff.ImageUrl = "~/Images/visitors.png";
                                ImgPath = "~/Images/visitors.png";
                                Session["VisitorPhoto"] = null;
                                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(ImgPath));
                                using (MemoryStream m = new MemoryStream())
                                {
                                    image.Save(m, image.RawFormat);
                                    byte[] imageBytes = m.ToArray();

                                    // Convert byte[] to Base64 String
                                    string base64String = Convert.ToBase64String(imageBytes);
                                    imglogo.ImageUrl = "data:image/png;base64," + base64String;
                                }
                            }
                        }
                        con.Close();

                        if (ds.Tables[0].Rows[0]["Visitor_Photo"].ToString() == "")
                        {
                            imgStaff.ImageUrl = "~/Images/visitors.png";
                            ImgPath = "~/Images/visitors.png";
                            Session["VisitorPhoto"] = null;
                        }
                        else
                        {
                            imgStaff.ImageUrl = ds.Tables[0].Rows[0]["Visitor_Photo"].ToString();
                            ImgPath = ds.Tables[0].Rows[0]["Visitor_Photo"].ToString();
                            Session["VisitorPhoto"] = ds.Tables[0].Rows[0]["Visitor_Photo"].ToString();
                            if (File.Exists(Server.MapPath(@ImgPath)))
                            {
                                System.Drawing.Image image2 = System.Drawing.Image.FromFile(Server.MapPath(ImgPath));
                                lblvisitor.Text = ds.Tables[0].Rows[0]["Visitor_Photo"].ToString();
                                using (MemoryStream m = new MemoryStream())
                                {
                                    image2.Save(m, image2.RawFormat);
                                    byte[] imageBytes = m.ToArray();

                                    // Convert byte[] to Base64 String
                                    string base64String = Convert.ToBase64String(imageBytes);
                                    imgvisitor.ImageUrl = "data:image/png;base64," + base64String;
                                }
                            }
                            else
                            {
                                imgStaff.ImageUrl = "~/Images/visitors.png";
                                ImgPath = "~/Images/visitors.png";
                                Session["VisitorPhoto"] = null;
                                System.Drawing.Image image2 = System.Drawing.Image.FromFile(Server.MapPath(ImgPath));
                                lblvisitor.Text = ds.Tables[0].Rows[0]["Visitor_Photo"].ToString();
                                using (MemoryStream m = new MemoryStream())
                                {
                                    image2.Save(m, image2.RawFormat);
                                    byte[] imageBytes = m.ToArray();

                                    // Convert byte[] to Base64 String
                                    string base64String = Convert.ToBase64String(imageBytes);
                                    imgvisitor.ImageUrl = "data:image/png;base64," + base64String;
                                }
                            }
                        }
                        lblVisitorRegno.Text = ds.Tables[0].Rows[0]["Visitor_Reg_No"].ToString();
                        txtCheck_In.Text = ds.Tables[0].Rows[0]["Check_In"].ToString();
                        txtCheck_Out.Text = ds.Tables[0].Rows[0]["Check_Out"].ToString();
                        txtCreatedBy.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                        txtLastUpdatedBy.Text = ds.Tables[0].Rows[0]["LastUpdatedBy"].ToString();
                        ddlCardstatus.ClearSelection();
                        ddlCardstatus.Items.FindByValue(ds.Tables[0].Rows[0]["RecordStatus"].ToString()).Selected = true;
                        Emplist(ds.Tables[0].Rows[0]["EmpServiceId"].ToString());
                        MoveEmp(ds.Tables[0].Rows[0]["EmpServiceId"].ToString());
                        Disable();
                        if (ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString() == "SelfRegistered")
                        {
                            h1selfvisitor.Visible = true;
                            h1previsitor.Visible = false;
                            h3visitor.Visible = false;
                            h1checkinout.Visible = false;
                            lnkCheckIn.Visible = true;
                            lnkpushtos2.Visible = false;
                            lnkCheckOut.Visible = false;
                            //lnkmodifys2.Visible = false;
                            lnksave.Visible = false;
                            divregno.Visible = true;
                            divCardNumber_Status.Visible = true;
                            lnkprint.Attributes.Add("style", "display:none");
                            preview.Attributes.Add("style", "display:none");
                            divAccess.Visible = true;
                            lblStatus.Text = "[ Status : Self-Registered ]";
                            lblStatus.Attributes.Add("style", "color:Blue;");

                        }
                        else if (ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString() == "PreRegistered")
                        {
                            h1selfvisitor.Visible = false;
                            h1previsitor.Visible = true;
                            h3visitor.Visible = false;
                            h1checkinout.Visible = false;
                            lnkCheckIn.Visible = true;
                            //lnkpushtos2.Visible = true;
                            lnkCheckOut.Visible = false;
                            //lnkmodifys2.Visible = false;
                            lnksave.Visible = false;
                            divregno.Visible = true;
                            divCardNumber_Status.Visible = true;
                            lnkprint.Attributes.Add("style", "display:none");
                            preview.Attributes.Add("style", "display:none");
                            divAccess.Visible = true;
                            lblStatus.Text = "[ Status : Pre-Registered ]";
                            lblStatus.Attributes.Add("style", "color:Blue;");
                        }
                        else if (ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString() == "CheckedIn")
                        {
                            h1checkinout.Visible = true;
                            h1selfvisitor.Visible = false;
                            h1previsitor.Visible = false;
                            h3visitor.Visible = false;
                            lnkCheckIn.Visible = false;
                            lnkpushtos2.Visible = true;
                            lnkCheckOut.Visible = true;
                            //lnkmodifys2.Visible = true;
                            lnksave.Visible = false;
                            //divCardNumber_Status.Visible = true;
                            //ddlbadgeaccess.Enabled = false;
                            txtValidFromDatetime.Enabled = false;
                            txtValidToDatetime.Enabled = false;
                            txtAsignCardNumber.Enabled = false;
                            lblStatus.Text = "[ Status : Checked-In ]";
                            lblStatus.Attributes.Add("style", "color:Green;");

                        }
                        else if (ds.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString() == "CheckedOut")
                        {
                            h1selfvisitor.Visible = false;
                            h1previsitor.Visible = false;
                            h3visitor.Visible = false;
                            h1checkinout.Visible = true;
                            lnkCheckIn.Visible = false;
                            lnkpushtos2.Visible = true;
                            lnkCheckOut.Visible = false;
                            // lnkmodifys2.Visible = false;
                            lnksave.Visible = false;
                            //divCardNumber_Status.Visible = true;
                            divAccess.Visible = true;
                            txtValidFromDatetime.Enabled = false;
                            txtValidToDatetime.Enabled = false;
                            txtAsignCardNumber.Enabled = false;
                            lblStatus.Text = "[ Status : Checked-Out ]";
                            lblStatus.Attributes.Add("style", "color:Red;");
                        }
                        if (lnksave.Visible == true)
                        {
                            txtValidFromDatetime.Text = DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 AM";
                            txtValidToDatetime.Text = DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 PM";
                            txtValidFromDatetime.Enabled = true;
                            txtValidToDatetime.Enabled = true;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[0]["Valid_From_Datetime"].ToString() == "" && ds.Tables[0].Rows[0]["Valid_To_Datetime"].ToString() == "")
                            {
                                txtValidFromDatetime.Text = DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 AM";
                                txtValidToDatetime.Text = DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 PM";
                            }
                            else
                            {
                                txtValidFromDatetime.Text = ds.Tables[0].Rows[0]["Valid_From_Datetime"].ToString();
                                txtValidToDatetime.Text = ds.Tables[0].Rows[0]["Valid_To_Datetime"].ToString();
                            }
                            txtAsignCardNumber.Text = ds.Tables[0].Rows[0]["Access_Card_Number"].ToString();
                        }
                    }
                }
                else
                {
                    lblqr.Text = "This visitor registration number is not valid.please enter valid visitor registration number";
                    mymodal2.Attributes.Add("style", "display:block;");
                    myModal.Attributes.Add("style", "display:none;");
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void QR_Code()
        {

        }


        //LogOut Logic
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        //Add New Staff Logic
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Visitor_Master.aspx?Add=Add pre-Reg");
        }
        //Save Staff Details Logic
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    if (hidValue.Value != "")
                    {
                        drawimg(hidValue.Value.Replace("data:image/png;base64,", String.Empty), "");
                    }
                    else
                    {
                        ImageUpload();
                    }
                    
                    string listEmployess = "";
                    string ServiceId = "";
                    //foreach (ListItem li in lstmoveemp.Items)
                    //{
                    //    listEmployess += li.Value + ",";
                    //    listServicesName += li.Text + ",";
                    //}
                    //listServicesName = listServicesName.TrimEnd(',');
                    //listEmployess = listEmployess.TrimEnd(',');
                    SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Insert");
                    cmd.Parameters.AddWithValue("@Visitor_RecId", txtVisitor_RecId.Text);
                    cmd.Parameters.AddWithValue("@Visitor_GUID", txtVisitor_GUID.Text);
                    cmd.Parameters.AddWithValue("@First_Name", txtFirst_Name.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", txtLast_Name.Text);
                    cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Type", ddlVisitor_Type.SelectedValue);
                    cmd.Parameters.AddWithValue("@Visit_Reason", ddlVisit_Reason.SelectedValue);
                    cmd.Parameters.AddWithValue("@Phone_Number", txtPhone_Number.Text);
                    if (Session["VisitorPhoto"] == null)
                    {
                        cmd.Parameters.AddWithValue("@Visitor_Photo", Image);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Visitor_Photo", Session["VisitorPhoto"]);
                    }
                    cmd.Parameters.AddWithValue("@ID_Number", txtID_Number.Text);
                    cmd.Parameters.AddWithValue("@National_ID", txtNational_ID.Text);
                    cmd.Parameters.AddWithValue("@Host_Employee_Code", Session["UserName"].ToString());
                    cmd.Parameters.AddWithValue("@Access_Card_Number", txtAccess_Card_Number.Text);
                    cmd.Parameters.AddWithValue("@Access_Level", ddlAccess_Level.SelectedValue);
                    cmd.Parameters.AddWithValue("@Check_In", txtCheck_In.Text);
                    cmd.Parameters.AddWithValue("@Valid_To", txtValid_To.Text);
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                    cmd.Parameters.AddWithValue("@Valid_From", txtValid_From.Text);
                    cmd.Parameters.AddWithValue("@Check_Out", txtCheck_Out.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text);
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", txtLastUpdatedBy.Text);
                    cmd.Parameters.AddWithValue("@RecordStatus", ddlCardstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@Id", 0);
                    cmd.Parameters.AddWithValue("@hash", bytes);
                    cmd.Parameters.AddWithValue("@ServicesId", "");
                    cmd.Parameters.AddWithValue("@Valid_From_Datetime", txtValidFromDatetime.Text);
                    cmd.Parameters.AddWithValue("@Valid_To_Datetime", txtValidToDatetime.Text);
                    cmd.Parameters.AddWithValue("@Visitor_PreReg_Status", "PreRegistered");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    foreach (ListItem li in lstmoveemp.Items)
                    {
                        SqlCommand cmd1 = new SqlCommand("SP_Get_Last_Entry_Visitors", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;

                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable ds1 = new DataTable();
                        da.Fill(ds1);
                        if (ds1.Rows.Count > 0)
                        {
                            ServiceId += li.Value + ",";
                            listEmployess += li.Text + ",";
                            SqlCommand cmd2 = new SqlCommand("SP_Insert_Visitor_assign_Served_Services", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@Flag", "Insert");
                            cmd2.Parameters.AddWithValue("@VisitorId", ds1.Rows[0]["SLN_ACS_Visitor_Info"].ToString());
                            cmd2.Parameters.AddWithValue("@ServiceId", li.Value);
                            cmd2.Parameters.AddWithValue("@EmpId", "");
                            con.Open();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    listEmployess = listEmployess.TrimEnd(',');
                    ServiceId = ServiceId.TrimEnd(',');
                    GenerateQR();
                    string URL = WebConfigurationManager.AppSettings["HostName"].ToString() + "QRImages/" + code + ".jpg";
                    //string bodystring = "<!DOCTYPE html><html><body style='height:auto;min-height:auto;'><br />"
                    //       + "Hello " + txtFirst_Name.Text + " " + txtLast_Name.Text + ",<br /><br />"
                    //       + "Your visit for " + listServicesName + " arrange by " + Session["UserID"] + " in visitor pre-registration system on " + txtValidFromDatetime.Text + ".<br /><br />"
                    //       + "<b>Visit Date : </b>" + txtValidFromDatetime.Text + "<br />"
                    //       + "<b>Visitor Type : </b>" + ddlVisitor_Type.SelectedItem.Text + "<br />"
                    //       + "<b>Company Name : </b>" + txtCompany.Text + "<br />"
                    //       + "<b>Visit Reason : </b>" + ddlVisit_Reason.SelectedItem.Text + "<br />"
                    //       + "<b>Service  : </b>" + listServicesName + "<br /><br />"
                    //       + "Your registration number is <b>" + code + "</b><br /><br />"
                    //       + "Please keep this email with you when you" + "<br />"
                    //       + "visit us at Security desk, you will be able to scan the" + "<br />"
                    //       + "qrcode below for rapid check in." + "<br /><br />"
                    //       + "<img src='" + URL + "' target='_blank'/>" + "<br /><br />"
                    //       + "<h3><b>" + code + "</b></h3>" + "<br /><br />"
                    //       + "IF YOU DON'T SEE QRCODE IMAGE, OPEN THIS URL IN BROWSER<br />"
                    //       + "<a href='" + URL + "' target='_blank' > Click here</a><br /><br />"
                    //       + "*** Message automatically generated by sCampus System ***<br />"
                    //       + "*** Please do not reply ***"
                    //       + "</body></html>";
                    Email(txtFirst_Name.Text, txtLast_Name.Text, listEmployess, txtValidFromDatetime.Text,
                        ddlVisitor_Type.SelectedItem.Text, txtCompany.Text, ddlVisit_Reason.SelectedItem.Text, code,
                        txtEmail_Id.Text, ServiceId);

                    ModalPopupExtender1.Attributes.Add("style", "display:none;");
                    ModalPopupExtender3.Attributes.Add("style", "display:none;");
                    //ModalPopupExtender2.Show();
                    lnkOkk.Visible = true;
                    lblS2.Visible = false;
                    lnkGo.Visible = false;
                    lnkpushCancel.Visible = false;
                    lblmsg.Text = "Visitor pre-registered successfully!! <br>Registration number : " + code;
                    myModal.Attributes.Add("style", "display:block;");
                    mymodal2.Attributes.Add("style", "display:none;");
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                    lblmsg.Text = "Visitor pre-registered successfully!! <br>Registration number : " + code;
                    myModal.Attributes.Add("style", "display:block;");
                    mymodal2.Attributes.Add("style", "display:none;");
                    lnkOkk.Visible = true;
                    lblS2.Visible = false;
                    lnkGo.Visible = false;
                    lnkpushCancel.Visible = false;
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
                    if (hidValue.Value != "")
                    {
                        drawimg(hidValue.Value.Replace("data:image/png;base64,", String.Empty), "");
                    }
                    else
                    {
                        ImageUpload();
                    }
                    string listEmployess = "";
                    //foreach (ListItem li in lstmoveemp.Items)
                    //{
                    //    listEmployess += li.Value + ",";
                    //    listServicesName += li.Text + ",";
                    //}
                    //listServicesName = listServicesName.TrimEnd(',');
                    //listEmployess = listEmployess.TrimEnd(',');
                    SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Update");
                    cmd.Parameters.AddWithValue("@Visitor_RecId", txtVisitor_RecId.Text);
                    cmd.Parameters.AddWithValue("@Visitor_GUID", txtVisitor_GUID.Text);
                    cmd.Parameters.AddWithValue("@First_Name", txtFirst_Name.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", txtLast_Name.Text);
                    cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Type", ddlVisitor_Type.SelectedValue);
                    cmd.Parameters.AddWithValue("@Visit_Reason", ddlVisit_Reason.SelectedValue);
                    cmd.Parameters.AddWithValue("@Phone_Number", txtPhone_Number.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Photo", Image);
                    cmd.Parameters.AddWithValue("@ID_Number", txtID_Number.Text);
                    cmd.Parameters.AddWithValue("@National_ID", txtNational_ID.Text);
                    cmd.Parameters.AddWithValue("@Host_Employee_Code", Session["UserName"].ToString());
                    cmd.Parameters.AddWithValue("@Access_Card_Number", txtAccess_Card_Number.Text);
                    cmd.Parameters.AddWithValue("@Access_Level", ddlAccess_Level.SelectedValue);
                    cmd.Parameters.AddWithValue("@Check_In", txtCheck_In.Text);
                    cmd.Parameters.AddWithValue("@Valid_To", txtValid_To.Text);
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                    cmd.Parameters.AddWithValue("@Valid_From", txtValid_From.Text);
                    cmd.Parameters.AddWithValue("@Check_Out", txtCheck_Out.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text);
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", txtLastUpdatedBy.Text);
                    cmd.Parameters.AddWithValue("@RecordStatus", ddlCardstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Request.QueryString["Id"]));
                    cmd.Parameters.AddWithValue("@hash", bytes);
                    cmd.Parameters.AddWithValue("@ServicesId", listEmployess);
                    if (lblstatusNone.Text == "SelfRegistered")
                    {
                        cmd.Parameters.AddWithValue("@Valid_From_Datetime", "");
                        cmd.Parameters.AddWithValue("@Valid_To_Datetime", "");
                        cmd.Parameters.AddWithValue("@Visitor_PreReg_Status", "SelfRegistered");
                    }
                    else if (lblstatusNone.Text == "PreRegistered")
                    {
                        cmd.Parameters.AddWithValue("@Valid_From_Datetime", txtValidFromDatetime.Text);
                        cmd.Parameters.AddWithValue("@Valid_To_Datetime", txtValidToDatetime.Text);
                        cmd.Parameters.AddWithValue("@Visitor_PreReg_Status", "PreRegistered");
                    }
                    else if (lblstatusNone.Text == "CheckedIn")
                    {
                        cmd.Parameters.AddWithValue("@Valid_From_Datetime", txtValidFromDatetime.Text);
                        cmd.Parameters.AddWithValue("@Valid_To_Datetime", txtValidToDatetime.Text);
                        cmd.Parameters.AddWithValue("@Visitor_PreReg_Status", "CheckedIn");
                    }
                    else if (lblstatusNone.Text == "CheckedOut")
                    {
                        cmd.Parameters.AddWithValue("@Valid_From_Datetime", txtValidFromDatetime.Text);
                        cmd.Parameters.AddWithValue("@Valid_To_Datetime", txtValidToDatetime.Text);
                        cmd.Parameters.AddWithValue("@Visitor_PreReg_Status", "CheckedOut");
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    foreach (ListItem li in lstmoveemp.Items)
                    {
                        SqlCommand cmd1 = new SqlCommand("SP_Insert_Visitor_assign_Served_Services", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;
                        cmd1.Parameters.AddWithValue("@Flag", "GetServises");
                        cmd1.Parameters.AddWithValue("@VisitorId", Request.QueryString["Id"]);
                        cmd1.Parameters.AddWithValue("@ServiceId", li.Value);
                        cmd1.Parameters.AddWithValue("@EmpId", "");
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable ds1 = new DataTable();
                        da.Fill(ds1);
                        if (ds1.Rows.Count > 0)
                        {

                            listEmployess += li.Value + ",";
                            SqlCommand cmd2 = new SqlCommand("SP_Insert_Visitor_assign_Served_Services", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@Flag", "UpdateServises");
                            cmd2.Parameters.AddWithValue("@VisitorId", Request.QueryString["Id"]);
                            cmd2.Parameters.AddWithValue("@ServiceId", li.Value);
                            cmd2.Parameters.AddWithValue("@EmpId", "");
                            con.Open();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            listEmployess += li.Value + ",";
                            SqlCommand cmd2 = new SqlCommand("SP_Insert_Visitor_assign_Served_Services", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@Flag", "Insert");
                            cmd2.Parameters.AddWithValue("@VisitorId", Request.QueryString["Id"]);
                            cmd2.Parameters.AddWithValue("@ServiceId", li.Value);
                            cmd2.Parameters.AddWithValue("@EmpId", "");
                            con.Open();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    listEmployess = listEmployess.TrimEnd(',');
                    SqlCommand cmd3 = new SqlCommand("SP_Insert_Visitor_assign_Served_Services", con);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@Flag", "DeleteServises");
                    cmd3.Parameters.AddWithValue("@VisitorId", Request.QueryString["Id"]);
                    cmd3.Parameters.AddWithValue("@ServiceId", 0);
                    cmd3.Parameters.AddWithValue("@EmpId", listEmployess);
                    con.Open();
                    cmd3.ExecuteNonQuery();
                    con.Close();
                    ModalPopupExtender1.Attributes.Add("style", "display:none;");
                    ModalPopupExtender3.Attributes.Add("style", "display:none;");
                    lblmsg.Text = "Visitor record update successfully!!<br/>Registration number : " + lblVisitorRegno.Text;
                    myModal.Attributes.Add("style", "display:block;");
                    mymodal2.Attributes.Add("style", "display:none;");
                    lnkOkk.Visible = true;
                    lblS2.Visible = false;
                    lnkGo.Visible = false;
                    lnkpushCancel.Visible = false;
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
                SqlCommand cmd = new SqlCommand("SP_SMS_GridVisitor_Reason_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select * from SMS_Visitors WHERE Visitor_Reg_No LIKE '%' + @Search + '%' or Visitor_RecId LIKE '%' + @Search + '%' OR Visitor_GUID LIKE '%' + @Search + '%' OR Email_Id LIKE '%' + @Search + '%' OR First_Name LIKE '%' + @Search + '%' OR Company  LIKE '%' + @Search + '%'  OR Phone_Number  LIKE '%' + @Search + '%' order by SLN_ACS_Visitor_Info Desc";
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
            if (txtFirst_Name.Text == "")
            {
                lblvalidValidDateUntil2.Visible = false;
                lblEmailId.Visible = false;
                lblvalidfirstname.Visible = true;
                lblvalidmove.Visible = false;
                return false;
            }
            if (txtEmail_Id.Text == "")
            {
                lblvalidValidDateUntil2.Visible = false;
                lblvalidfirstname.Visible = false;
                lblEmailId.Visible = true;
                lblvalidmove.Visible = false;
                return false;
            }
            if (txtValidFromDatetime.Text.Trim() != "" && txtValidToDatetime.Text.Trim() != "")
            {
                DateTime fromdate = Convert.ToDateTime(txtValidFromDatetime.Text);
                DateTime todate = Convert.ToDateTime(txtValidToDatetime.Text);
                int res = DateTime.Compare(fromdate, todate);
                if (res > 0)
                {
                    lblvalidValidDateUntil2.Visible = true;
                    txtValidFromDatetime.Focus();
                    lblvalidmove.Visible = false;
                    return false;
                }

            }
            string ServiceId = "";
            foreach (ListItem li in lstmoveemp.Items)
            {
                ServiceId += li.Value + ",";
            }
            if (ServiceId == "")
            {
                lblvalidValidDateUntil2.Visible = false;
                lblvalidfirstname.Visible = false;
                lblEmailId.Visible = false;
                lblvalidmove.Visible = true;
                return false;
            }
            if (FileUpload1.HasFile)
            {
                string ext = System.IO.Path.GetExtension(FileUpload1.FileName.ToString());
                if ((ext.ToLower() == ".jpg") || (ext.ToLower() == ".eps") || (ext.ToLower() == ".jpeg") || (ext.ToLower() == ".gif") || (ext.ToLower() == ".png") || (ext.ToLower() == ".bmp"))
                {
                    lblf1.Visible = false;
                }
                else
                {
                    lblf1.Visible = true;
                    return false;
                }
            }

            return true;
        }

        protected bool Validation1()
        {
            if (ddlbadgeaccess.SelectedValue == "1")
            {

                if (txtAsignCardNumber.Text == "")
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Card Number')</script>");
                    lblvalidationcard.Visible = true;
                    lblAlreadyAssign.Visible = false;
                    lblLost.Visible = false;
                    lblNotAvailable.Visible = false;
                    return false;
                }
                else
                {
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandText = "select * from SMS_Visitors where Access_Card_Number= '" + txtAsignCardNumber.Text.Trim() + "'";
                    //cmd.Connection = con;
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds);
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    if (ds.Tables[0].Rows[0]["Check_In"].ToString() != "" && ds.Tables[0].Rows[0]["Check_Out"].ToString() != "")
                    //    {
                    //        //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Card Number already Asign Visitor')</script>");
                    //        lblvalidationcard.Visible = false;
                    //        lblAlreadyAssign.Visible = true;
                    //        lblLost.Visible = false;
                    //        lblNotAvailable.Visible = false;
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = "select * from SMS_Visitor_Card_Master where CardNumber= '" + txtAsignCardNumber.Text.Trim() + "'";
                    cmd1.Connection = con;
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["CardStatusId"].ToString() == "3" || ds1.Tables[0].Rows[0]["CardStatusId"].ToString() == "4")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Card Number Is Expired or  Lost/ Demanged ! Please select Another Card Number')</script>");
                            lblvalidationcard.Visible = false;
                            lblAlreadyAssign.Visible = false;
                            lblLost.Visible = true;
                            lblNotAvailable.Visible = false;
                            return false;
                        }
                        else if (ds1.Tables[0].Rows[0]["CardStatusId"].ToString() == "2")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Card Number Already Asigned ! Please select Another Card Number')</script>");
                            lblvalidationcard.Visible = false;
                            lblAlreadyAssign.Visible = true;
                            lblLost.Visible = false;
                            lblNotAvailable.Visible = false;
                            return false;
                        }
                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Card Number Not Available')</script>");
                        lblvalidationcard.Visible = false;
                        lblAlreadyAssign.Visible = false;
                        lblLost.Visible = false;
                        lblNotAvailable.Visible = true;
                        return false;
                    }
                }  //}
            }
            return true;
        }
        protected bool ValidationforServedby()
        {

            SqlCommand cmd2 = new SqlCommand("SP_Get_Service_Notification", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@Flag", "GetServedbyServices");
            cmd2.Parameters.AddWithValue("@EmpID", ddlservices.SelectedValue);
            cmd2.Parameters.AddWithValue("@VisitorId", Request.QueryString["Id"].ToString());
            cmd2.Connection = con;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            DataTable ds2 = new DataTable();
            da1.Fill(ds2);
            if (ds2.Rows.Count > 0)
            {
                lblservedbyemp.Text = "Already served by " + ds2.Rows[0]["Full_Name"].ToString() + " at Datetime " + ds2.Rows[0]["ServedDatetime"].ToString() + ".";
                lblservedbyemp.Visible = true;
                return false;
            }
            else
            {
                lblservedbyemp.Visible = false;
            }
            return true;
        }
        //Image upload Logic
        protected void ImageUpload()
        {

            if (FileUpload1.HasFile)
            {
                string Image1 = "";
                string str = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/VisitorImages/" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace(" ", "_").Replace(":", "_").Replace("/", "_") + "_" + str));
                Image = "~/Images/VisitorImages/" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace(" ", "_").Replace(":", "_").Replace("/", "_") + "_" + str.ToString();
                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);

                //Save the Byte Array as File.
                Image1 = "/Images/VisitorImages/" + Path.GetFileName(FileUpload1.FileName);
                File.WriteAllBytes(Server.MapPath(Image1), bytes);

                //string str = FileUpload1.FileName;
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/VisitorImages/" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace(" ", "_").Replace(":", "_").Replace("/", "_") + "_" + str));
                //Image = "~/Images/VisitorImages/" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace(" ", "_").Replace(":", "_").Replace("/", "_") + "_" + str.ToString();
                //string ext = System.IO.Path.GetExtension(FileUpload1.FileName.ToString());
                //string FileName1 = Guid.NewGuid().ToString();
                //string FileName = System.IO.Path.GetFileName(FileUpload1.FileName.ToString());
                //string Imagepath1 = Server.MapPath("~\\" + "Images\\VisitorImages");
                //FileUpload1.SaveAs(Imagepath1 + "\\" + FileName1 + ext);
                //string pathForDB = "Images\\VisitorImages";
                //Photopath = "~\\" + pathForDB + "\\" + FileName1 + ext;

                //Stream fs = FileUpload1.PostedFile.InputStream;
                //BinaryReader br = new BinaryReader(fs);
                //bytes = br.ReadBytes((Int32)fs.Length);

                ////Save the Byte Array as File.
                //filePath = "/Images/VisitorImages/" + Path.GetFileName(FileUpload1.FileName);
                //File.WriteAllBytes(Server.MapPath(filePath), bytes);
            }
        }
        //Edit Staff Details Logic
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Visitor_Master.aspx?Id=" + ids + "&Type=Edit");
        }
        //View Staff Details Logic
        protected void linkView_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Visitor_Master.aspx?Id=" + ids + "&Type=View");
        }


        protected void Emplist(string Emplist)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_GET_SERVICES", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Emplist");
                cmd.Parameters.AddWithValue("@Employee", Emplist);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    lstEmp.DataSource = ds1;
                    lstEmp.DataTextField = "Name";
                    lstEmp.DataValueField = "Id";
                    lstEmp.DataBind();

                }
                else
                {
                    lstEmp.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void MoveEmp(string Emplist)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_GET_SERVICES", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "MoveEmp");
                cmd.Parameters.AddWithValue("@Employee", Emplist);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    lstmoveemp.DataSource = ds1;
                    lstmoveemp.DataTextField = "Name";
                    lstmoveemp.DataValueField = "Id";
                    lstmoveemp.DataBind();

                }
                else
                {
                    lstmoveemp.Items.Clear();

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
                txtSLN_ACS_Visitor_Info.Enabled = false;
                txtVisitor_RecId.Enabled = false;
                txtVisitor_GUID.Enabled = false;
                txtFirst_Name.Enabled = false;
                ddlVisitor_Type.Enabled = false;
                txtLast_Name.Enabled = false;
                txtCompany.Enabled = false;
                ddlVisit_Reason.Enabled = false;
                txtPhone_Number.Enabled = false;
                txtID_Number.Enabled = false;
                txtNational_ID.Enabled = false;
                txtHost_Employee_Code.Enabled = false;
                txtAccess_Card_Number.Enabled = false;
                txtEmail_Id.Enabled = false;
                txtValid_From.Enabled = false;
                txtValid_To.Enabled = false;
                ddlAccess_Level.Enabled = false;
                FileUpload1.Visible = false;
                txtCheck_In.Enabled = false;
                txtCheck_Out.Enabled = false;
                txtCreatedBy.Enabled = false;
                txtLastUpdatedBy.Enabled = false;
                ddlCardstatus.Enabled = false;
                divCardNumber.Visible = true;
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkEdit_menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Visitor_Master.aspx?Id=" + txtSLN_ACS_Visitor_Info.Text + "&Type=Edit");
        }

        protected void lnkView_Menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Visitor_Master.aspx?Id=" + txtSLN_ACS_Visitor_Info.Text + "&Type=View");
        }

        protected void lnkviewgrid_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Visitor_Master.aspx");
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
            Response.Redirect("SMS_Visitor_Master.aspx");
        }

        protected void lnkCardNumber_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Visitor_Master.aspx?Id=" + ids + "&Type=Asign");
        }

        protected void LnkAsignUpdate_Click(object sender, EventArgs e)
        {
            if (Validation1())
            {
                try
                {
                    ImageUpload();
                    SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AsignCardNumber");
                    cmd.Parameters.AddWithValue("@Visitor_RecId", txtVisitor_RecId.Text);
                    cmd.Parameters.AddWithValue("@Visitor_GUID", txtVisitor_GUID.Text);
                    cmd.Parameters.AddWithValue("@First_Name", txtFirst_Name.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", txtLast_Name.Text);
                    cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Type", ddlVisitor_Type.SelectedValue);
                    cmd.Parameters.AddWithValue("@Visit_Reason", ddlVisit_Reason.SelectedValue);
                    cmd.Parameters.AddWithValue("@Phone_Number", txtPhone_Number.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Photo", Image);
                    cmd.Parameters.AddWithValue("@ID_Number", txtID_Number.Text);
                    cmd.Parameters.AddWithValue("@National_ID", txtNational_ID.Text);
                    cmd.Parameters.AddWithValue("@Host_Employee_Code", Session["UserName"].ToString());
                    cmd.Parameters.AddWithValue("@Access_Card_Number", txtAsignCardNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Access_Level", ddlAccess_Level.SelectedValue);
                    cmd.Parameters.AddWithValue("@Check_In", txtCheck_In.Text);
                    cmd.Parameters.AddWithValue("@Valid_To", txtValid_To.Text);
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                    cmd.Parameters.AddWithValue("@Valid_From", txtValid_From.Text);
                    cmd.Parameters.AddWithValue("@Check_Out", txtCheck_Out.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text);
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", txtLastUpdatedBy.Text);
                    cmd.Parameters.AddWithValue("@RecordStatus", ddlCardstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Request.QueryString["Id"]));
                    cmd.Parameters.AddWithValue("@hash", bytes);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("SMS_Visitor_Master.aspx");
                    con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblAccess_CardNumber = e.Row.FindControl("lblAccess_CardNumber") as Label;
                    LinkButton lnkCardNumber = e.Row.FindControl("lnkCardNumber") as LinkButton;
                    LinkButton lnkRemoveCardNo = e.Row.FindControl("lnkRemoveCardNo") as LinkButton;
                    LinkButton lnkServedby = e.Row.FindControl("lnkServedby") as LinkButton;
                    LinkButton lnkClosed = e.Row.FindControl("lnkClosed") as LinkButton;
                    Label lblCheckin = e.Row.FindControl("lblCheckin") as Label;
                    Label lblCheckout = e.Row.FindControl("lblCheckout") as Label;
                    Label lblcompleted = e.Row.FindControl("lblcompleted") as Label;
                    Label lblid = e.Row.FindControl("lblid") as Label;

                    if (lblCheckin.Text != "" && lblCheckout.Text != "")
                    {
                        lnkCardNumber.Visible = false;
                        lnkRemoveCardNo.Visible = false;
                        lblcompleted.Visible = true;
                    }
                    else if (lblAccess_CardNumber.Text != "")
                    {
                        lnkCardNumber.Visible = false;
                        lnkRemoveCardNo.Visible = true;
                        lblcompleted.Visible = false;
                    }
                    else
                    {
                        lnkCardNumber.Visible = true;
                        lnkRemoveCardNo.Visible = false;
                        lblcompleted.Visible = false;
                    }
                    string listServices = "";
                    SqlCommand cmd = new SqlCommand("SP_Get_Service_Notification", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "GetServices");
                    cmd.Parameters.AddWithValue("@EmpID", Session["Id"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@VisitorId", lblid.Text);
                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable ds1 = new DataTable();
                    da.Fill(ds1);
                    if (ds1.Rows.Count > 0)
                    {
                        for (int i = 0; i < ds1.Rows.Count; i++)
                        {
                            listServices += ds1.Rows[i]["Id"].ToString() + ",";
                        }
                        listServices = listServices.TrimEnd(',');
                        SqlCommand cmd2 = new SqlCommand("SP_Get_Service_Notification", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@Flag", "GetAssignServices");
                        cmd2.Parameters.AddWithValue("@EmpID", listServices);
                        cmd2.Parameters.AddWithValue("@VisitorId", lblid.Text);
                        cmd2.Connection = con;
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
                        DataTable ds2 = new DataTable();
                        da1.Fill(ds2);
                        if (ds2.Rows.Count > 0)
                        {
                            lnkServedby.Visible = true;
                        }
                        else
                        {
                            lnkServedby.Visible = false;
                            lnkClosed.Visible = true;
                        }
                    }
                    else
                    {
                        lnkServedby.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkRemoveCardNo_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = "";
                ids = string.Empty;
                ids = (sender as LinkButton).CommandArgument;
                SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "RemoveCardNumber");
                cmd.Parameters.AddWithValue("@Access_Card_Number", ids.ToString());
                cmd.Parameters.AddWithValue("@hash", bytes);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("SMS_Visitor_Master.aspx");
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindVisitorReason()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindVisitor_Reason_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlVisit_Reason.DataSource = ds1;
                ddlVisit_Reason.DataTextField = "VisitorReason";
                ddlVisit_Reason.DataValueField = "Id";
                ddlVisit_Reason.DataBind();
                ddlVisit_Reason.Items.Insert(0, new ListItem("--Select Visit Reason--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindServices()
        {
            try
            {
                List<ListItem> removEditems = new List<ListItem>();
                SqlCommand cmd = new SqlCommand("SP_SMS_GET_SERVICES", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "GetServices");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    lstEmp.DataSource = ds1;
                    lstEmp.DataTextField = "Name";
                    lstEmp.DataValueField = "Id";
                    lstEmp.DataBind();
                }
                else
                {
                    lstEmp.Items.Clear();
                }

            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void GetServicesDropdown()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Get_Service_Notification", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "GetServicesDropdown");
                cmd.Parameters.AddWithValue("@VisitorId", Request.QueryString["Id"].ToString());
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlservices.DataSource = ds1;
                ddlservices.DataTextField = "Name";
                ddlservices.DataValueField = "Id";
                ddlservices.DataBind();
                //ddlAccess_Level.Items.Insert(0, new ListItem("--Select Visitor Access level--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindVisitorAccesslevel()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindVisitor_Accesslevel_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlAccess_Level.DataSource = ds1;
                ddlAccess_Level.DataTextField = "VisitorAccesslevel";
                ddlAccess_Level.DataValueField = "Id";
                ddlAccess_Level.DataBind();
                ddlAccess_Level.Items.Insert(0, new ListItem("--Select Visitor Access level--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindVisitorType()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SMS_BindVisitor_Type_Master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlVisitor_Type.DataSource = ds1;
                ddlVisitor_Type.DataTextField = "VisitorType";
                ddlVisitor_Type.DataValueField = "Id";
                ddlVisitor_Type.DataBind();
                ddlVisitor_Type.Items.Insert(0, new ListItem("--Select Visitor Type--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void btnsave_Click1(object sender, EventArgs e)
        {
            ImageUpload();
        }
        protected void LeftClick(object sender, EventArgs e)
        {
            //List will hold items to be removed.
            List<ListItem> removEditems = new List<ListItem>();

            //Loop and transfer the Items to Destination ListBox.
            foreach (ListItem item in lstmoveemp.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    lstEmp.Items.Add(item);
                    removEditems.Add(item);
                }
            }

            //Loop and remove the Items from the Source ListBox.
            foreach (ListItem item in removEditems)
            {
                lstmoveemp.Items.Remove(item);
            }
        }

        protected void RightClick(object sender, EventArgs e)
        {
            //List will hold items to be removed.
            List<ListItem> removEditems = new List<ListItem>();

            //Loop and transfer the Items to Destination ListBox.
            foreach (ListItem item in lstEmp.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    lstmoveemp.Items.Add(item);
                    removEditems.Add(item);
                }
            }

            //Loop and remove the Items from the Source ListBox.
            foreach (ListItem item in removEditems)
            {
                lstEmp.Items.Remove(item);
            }
        }

        protected void lnkServedby_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Visitor_Master.aspx?Id=" + ids + "&Type=Servedby");
        }

        protected void lnkServedBy_Click1(object sender, EventArgs e)
        {

            try
            {
                if (ValidationforServedby())
                {
                    SqlCommand cmd2 = new SqlCommand("SP_Insert_Visitor_assign_Served_Services", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@Flag", "Update");
                    cmd2.Parameters.AddWithValue("@VisitorId", Request.QueryString["Id"].ToString());
                    cmd2.Parameters.AddWithValue("@ServiceId", ddlservices.SelectedValue);
                    cmd2.Parameters.AddWithValue("@EmpId", Session["Id"].ToString());
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("SMS_Visitor_Master.aspx");
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkClosed_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Visitor_Master.aspx?Id=" + ids + "&Type=View_Served");
        }

        protected void lnkCopy_Click(object sender, EventArgs e)
        {
            h1selfvisitor.Visible = false;
            h3visitor.Visible = false;
            h1previsitor.Visible = true;
            h1checkinout.Visible = false;
            lnksave.Visible = true;
            lnkCopy.Visible = false;
            lnkCheckIn.Visible = false;
            lnkpushtos2.Visible = false;
            lnkCheckOut.Visible = false;
            //lnkmodifys2.Visible = false;
            txtValidFromDatetime.Text = DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 AM";
            txtValidToDatetime.Text = DateTime.Now.ToString("MM/dd/yyyy") + " 12:00 PM";
            txtAsignCardNumber.Text = "";
            divCardNumber_Status.Visible = false;
            divAccess.Visible = false;
            Enable();
            lstEmp.Items.Clear();
            lstmoveemp.Items.Clear();
            BindServices();
            if (Session["VisitorPhoto"] == null)
            {
                imgStaff.ImageUrl = "";
                Image = "";
            }
            else
            {
                imgStaff.ImageUrl = Session["VisitorPhoto"].ToString();
                ImgPath = Session["VisitorPhoto"].ToString();
                Image = Session["VisitorPhoto"].ToString();
            }

            lblStatus.Text = "";
        }
        public void Email(string FirstName, string LastName, string ServiceName, string ValidFrom, string VisitorType,
            string Company, string Reason, string Code, string Email, string serviceId)
        {
            try
            {
                string Emails = Email + "," + Session["Email_Id"].ToString();
                string ServiceEmails = "";
                SqlCommand cmdservice = new SqlCommand("SP_SMS_GET_SERVICES", con);
                cmdservice.CommandType = CommandType.StoredProcedure;
                cmdservice.Connection = con;
                cmdservice.Parameters.AddWithValue("@Flag", "ServiceEmail");
                cmdservice.Parameters.AddWithValue("@Emails", Emails);
                cmdservice.Parameters.AddWithValue("@Employee", serviceId);
                cmdservice.Connection = con;
                SqlDataAdapter daservice = new SqlDataAdapter(cmdservice);
                DataSet dsService = new DataSet();
                daservice.Fill(dsService);
                if (dsService.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsService.Tables[0].Rows.Count; i++)
                    {
                        if (dsService.Tables[0].Rows[i]["Employee"].ToString() != "")
                        {
                            ServiceEmails += dsService.Tables[0].Rows[i]["Employee"].ToString() + ",";
                        }
                    }
                }
                ServiceEmails = ServiceEmails.TrimEnd(',');
                SqlCommand cmd = new SqlCommand("SMS_SP_Get_Email_Configuration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get");
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ////Visitor Email
                    string Host_Emp_EmailId = Session["Email_Id"].ToString();
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(ds.Tables[0].Rows[0]["Email_Address"].ToString());
                    msg.To.Add(Email);
                    msg.Subject = "You are scheduled for service " + ServiceName + " by " + Session["UserID"] + " on " + txtValidFromDatetime.Text + ".";
                    //msg.Body = body;
                    msg.Body = "You are scheduled for service " + ServiceName + " by " + Session["UserID"] + " on " + txtValidFromDatetime.Text + ".";
                    msg.IsBodyHtml = true;
                    //if (ServiceEmails != "")
                    //{
                    //    msg.CC.Add(ServiceEmails);
                    //}
                    //msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    // decrypt parameters
                    var key = "b14ca5898a4e4133bbce2ea2315a1916";
                    var decrptedInput = DecryptString(key, ds.Tables[0].Rows[0]["password"].ToString());
                    client.Credentials = new NetworkCredential(ds.Tables[0].Rows[0]["Email_Address"].ToString(), decrptedInput);
                    client.Host = ds.Tables[0].Rows[0]["Outgoing_Services"].ToString();
                    client.Port = Convert.ToInt32(ds.Tables[0].Rows[0]["SMTP"].ToString());
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    if (ds.Tables[0].Rows[0]["Connection"].ToString() == "1")
                    {
                        client.EnableSsl = true;
                    }
                    else
                    {

                    }
                    if (ddlVisitor_Type.SelectedIndex == 0)
                    {
                        VisitorType = "";
                    }
                    if (ddlVisit_Reason.SelectedIndex == 0)
                    {
                        Reason = "";
                    }
                    Dictionary<String, String> replacements = new Dictionary<string, string>();
                    replacements.Add("recipient", FirstName + " " + LastName);
                    replacements.Add("ServiceName", ServiceName);
                    replacements.Add("ValidFrom", ValidFrom);
                    replacements.Add("UserId", Session["UserId"].ToString());
                    replacements.Add("VisitDate", ValidFrom);
                    replacements.Add("VisitType", VisitorType);
                    replacements.Add("Company", Company);
                    replacements.Add("VisitReason", Reason);
                    replacements.Add("code", code);
                    //AlternateView plain = CreateAlternateViewFromTemplate("template1.txt", "text/plain", replacements);
                    AlternateView html = CreateAlternateViewFromTemplate(Server.MapPath("EmailTemplate/Email.html"), "text/html", replacements);
                    Byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage(QRimages));
                    System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);

                    LinkedResource img1 = new LinkedResource(streamBitmap, "image/jpeg");
                    img1.ContentId = "img1";

                    html.LinkedResources.Add(img1);
                    //msg.AlternateViews.Add(plain);
                    msg.AlternateViews.Add(html);
                    //client.UseDefaultCredentials = true;
                    client.Send(msg);
                    ///////////////////////////// login User 2 Email
                    MailMessage ml = new MailMessage();
                    ml.From = new MailAddress(ds.Tables[0].Rows[0]["Email_Address"].ToString());
                    ml.To.Add(Host_Emp_EmailId);
                    if (ServiceEmails != "")
                    {
                        ml.CC.Add(ServiceEmails);
                    }
                    ml.Subject = "You have scheduled visit for service " + ServiceName + " for " + FirstName + " " + LastName + " on " + txtValidFromDatetime.Text + ".";
                    //msg.Body = body;
                    ml.Body = "You are scheduled for service " + ServiceName + " for " + Session["UserID"] + " on " + txtValidFromDatetime.Text + ".";
                    ml.IsBodyHtml = true;
                    // decrypt parameters
                    var decrptedInput1 = DecryptString(key, ds.Tables[0].Rows[0]["password"].ToString());
                    client.Credentials = new NetworkCredential(ds.Tables[0].Rows[0]["Email_Address"].ToString(), decrptedInput1);
                    client.Host = ds.Tables[0].Rows[0]["Outgoing_Services"].ToString();
                    client.Port = Convert.ToInt32(ds.Tables[0].Rows[0]["SMTP"].ToString());
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    if (ds.Tables[0].Rows[0]["Connection"].ToString() == "1")
                    {
                        client.EnableSsl = true;
                    }
                    else
                    {

                    }
                    if (ddlVisitor_Type.SelectedIndex == 0)
                    {
                        VisitorType = "";
                    }
                    if (ddlVisit_Reason.SelectedIndex == 0)
                    {
                        Reason = "";
                    }
                    Dictionary<String, String> replacements1 = new Dictionary<string, string>();
                    replacements1.Add("recipient", Session["UserID"].ToString());
                    replacements1.Add("ServiceName", ServiceName);
                    replacements1.Add("ValidFrom", ValidFrom);
                    replacements1.Add("VisitorName", FirstName + " " + LastName);
                    replacements1.Add("VisitDate", ValidFrom);
                    replacements1.Add("VisitType", VisitorType);
                    replacements1.Add("Company", Company);
                    replacements1.Add("VisitReason", Reason);
                    replacements1.Add("code", code);
                    //AlternateView plain = CreateAlternateViewFromTemplate("template1.txt", "text/plain", replacements);
                    AlternateView html1 = CreateAlternateViewFromTemplate(Server.MapPath("EmailTemplate/Employee_Email.html"), "text/html", replacements1);
                    Byte[] bitmapData1 = Convert.FromBase64String(FixBase64ForImage(QRimages));
                    System.IO.MemoryStream streamBitmap1 = new System.IO.MemoryStream(bitmapData1);

                    LinkedResource img2 = new LinkedResource(streamBitmap1, "image/jpeg");
                    img2.ContentId = "img2";

                    html1.LinkedResources.Add(img2);
                    //msg.AlternateViews.Add(plain);
                    ml.AlternateViews.Add(html1);
                    //client.UseDefaultCredentials = true;
                    client.Send(ml);
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
                lblmsg.Text = "Visitor pre-registered successfully!! <br>Registration number : " + code;
                myModal.Attributes.Add("style", "display:block;");
                mymodal2.Attributes.Add("style", "display:none;");
            }
        }
        public static string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }
        static AlternateView CreateAlternateViewFromTemplate(String path, String mediaType, IDictionary<String, String> replacements)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                String content = sr.ReadToEnd();

                if (replacements != null)
                {
                    foreach (KeyValuePair<String, String> replacement in replacements)
                    {
                        content = content.Replace(String.Concat("{", replacement.Key, "}"), replacement.Value);
                    }
                }

                AlternateView view = AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, mediaType);
                view.ContentType.CharSet = Encoding.UTF8.BodyName;
                if (mediaType == "text/html")
                {
                    view.TransferEncoding = TransferEncoding.QuotedPrintable;
                }
                return view;
            }
        }

        protected void GenerateQR()
        {
            SqlCommand cmd = new SqlCommand("SP_Visitor_Reg_No", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            if (ds.Rows.Count > 0)
            {
                code = ds.Rows[0]["Visitor_Reg_No"].ToString();
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 150;
            imgBarCode.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    //bitMap.Save(Server.MapPath("~/QRImages/" + code + ".jpg"));
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    QRimages = Convert.ToBase64String(byteImage);
                }
                //plBarCode.Controls.Add(imgBarCode);
            }
        }

        protected void lnkSearchRegNum_Click(object sender, EventArgs e)
        {
            if (SearchRegNumberValidation())
            {
                //ModalPopupExtender2.Hide();
                //div3.Visible = false;
                myModal.Attributes.Add("style", "display:none;");
                mymodal2.Attributes.Add("style", "display:none;");
                ModalPopupExtender1.Attributes.Add("style", "display:none;");
                ModalPopupExtender3.Attributes.Add("style", "display:none;");
                Response.Redirect("SMS_Visitor_Master.aspx?CopyVisitor=" + txtSearchRegNumber.Text);
            }
        }
        protected bool SearchRegNumberValidation()
        {
            if (txtSearchRegNumber.Text == "")
            {
                lblValidationSearchRegNumber.Visible = true;
                lblValidationSRNNotValid.Visible = false;
                return false;
            }
            else
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "select * from SMS_Visitors where Visitor_Reg_No= '" + txtSearchRegNumber.Text.Trim() + "'";
                cmd1.Connection = con;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {

                }
                else
                {
                    lblValidationSearchRegNumber.Visible = false;
                    lblValidationSRNNotValid.Visible = true;
                    return false;
                }
            }
            return true;
        }

        protected void lnkCheckIn_Click(object sender, EventArgs e)
        {
            CheckIn();
        }

        protected void lnkCheckOut_Click(object sender, EventArgs e)
        {
            CheckOut();
        }
        protected void lnkGo_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Visitor_Master.aspx");
            //lnkpushtos2_Click(sender, e);
        }

        protected void lnkregnum_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender2.Hide();
            //div3.Visible = false;
            lblValidationSRNNotValid.Visible = false;
            txtSearchRegNumber.Text = "";
            myModal.Attributes.Add("style", "display:none;");
            mymodal2.Attributes.Add("style", "display:none;");
            ModalPopupExtender1.Attributes.Add("style", "display:block;");
            ModalPopupExtender3.Attributes.Add("style", "display:none;");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Attributes.Add("style", "display:none;");
            ModalPopupExtender3.Attributes.Add("style", "display:none;");
            //ModalPopupExtender2.Hide();
            //div3.Visible = false;
            myModal.Attributes.Add("style", "display:none;");
            mymodal2.Attributes.Add("style", "display:none;");
            Response.Redirect("SMS_Visitor_Master.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Attributes.Add("style", "display:none;");
            ModalPopupExtender3.Attributes.Add("style", "display:none;");
            //ModalPopupExtender2.Hide();
            //div3.Visible = false;
            lblValidationSRNNotValid.Visible = false;
            txtSearchRegNumber.Text = "";
            myModal.Attributes.Add("style", "display:none;");
            mymodal2.Attributes.Add("style", "display:none;");
            
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Attributes.Add("style", "display:none;");
            ModalPopupExtender3.Attributes.Add("style", "display:none;");
            myModal.Attributes.Add("style", "display:none;");
            mymodal2.Attributes.Add("style", "display:none;");
            Response.Redirect("SMS_Visitor_Master.aspx");
        }
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        protected void ddlbadgeaccess_TextChanged(object sender, EventArgs e)
        {
            if (ddlbadgeaccess.SelectedValue == "1")
            {
                divCardNumber_Status.Visible = true;
                lnkprint.Attributes.Add("style", "display:none");
                preview.Attributes.Add("style", "display:none");
            }
            else
            {
                ImgPath = imgStaff.ImageUrl.Replace("~/Images", "Images");
                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(ImgPath));

                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    Image1.ImageUrl = "data:image/png;base64," + base64String;
                    //"data:image/png;base64," + base64String;
                    //return base64String;
                }


                divCardNumber_Status.Visible = false;
                lnkprint.Attributes.Add("style", "display:block");
                preview.Attributes.Add("style", "display:block");
            }
        }
        protected void lnkcardnumber_Click1(object sender, EventArgs e)
        {
            try
            {
                if (txtAsignCardNumber.Text != "")
                {
                    //if (txtAsignCardNumber.Text.Length > 8)
                    //{
                    //string CardNumber = Converter(txtAsignCardNumber.Text.Substring(0, 8));
                    string originalString = txtAsignCardNumber.Text.Trim();
                    string reverseString = string.Empty;
                    for (int i = originalString.Length - 1; i >= 0; i--)
                    {
                        reverseString += originalString[i];
                    }
                    string CardNumber = Convert.ToInt64(reverseString, 16).ToString();

                    txtAsignCardNumber.Text = CardNumber;
                    //}
                }
                else
                {
                    lblvalidationcard.Visible = true;
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected string Converter(String CardNumber)
        {
            string A1 = CardNumber.Substring(0, 2);
            string A2 = CardNumber.Substring(2, 2);
            string A3 = CardNumber.Substring(4, 2);
            string A4 = CardNumber.Substring(6, 2);
            string Card = A4 + A3 + A2 + A1;
            return Card;
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
        
        protected string GetPersionApi()
        {
            string responsemessage = "";
            var url = "http://" + Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + Session["sessionid"] + "'>"
             + "<COMMAND name='GetPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + lblVisitorRegno.Text.Trim() + "</PERSONID>"
             + "</PARAMS>"
             + "</COMMAND>"
             + "</NETBOX-API> ";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.ContentType = "application/XML";
                StreamWriter requestwriter = new StreamWriter(request.GetRequestStream());
                requestwriter.Write(xml);
                requestwriter.Close();
                StreamReader responsereader = new StreamReader(request.GetResponse().GetResponseStream());
                responsemessage = responsereader.ReadToEnd();
                responsereader.Close();
                request.GetResponse().Close();
                XmlDocument doc1 = new XmlDocument();
                doc1.LoadXml(responsemessage);
                XmlNodeList parentNode = doc1.GetElementsByTagName("NETBOX");

                responsemessage = doc1.SelectSingleNode("//NETBOX/RESPONSE/CODE").InnerText;
                foreach (XmlNode childrenNode in parentNode)
                {
                    Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
                }

            }
            catch (WebException ex)
            {
                WebResponse errormessage = ex.Response;
                using (Stream responsestream = errormessage.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responsestream, Encoding.GetEncoding("utf-8"));
                    responsemessage = reader.ReadToEnd();
                }
            }
            return responsemessage;
        }
        protected string AddPersionApi()
        {
            string XmlString = "";
            if (txtLast_Name.Text.Trim() != "")
            {
                XmlString += "<LASTNAME>" + txtLast_Name.Text.Trim() + "</LASTNAME>";
            }
            if (txtPhone_Number.Text.Trim() != "")
            {
                XmlString += "<CONTACTPHONE>" + txtPhone_Number.Text.Trim() + "</CONTACTPHONE>";
            }
            if (lblvisitor.Text.Trim() != "")
            {
                XmlString += "<PICTURE>" + imgvisitor.ImageUrl.Replace("data:image/png;base64,", "") + "</PICTURE>";
            }
            string responsemessage = "";
            var url = "http://" + Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + Session["sessionid"] + "'>"
             + "<COMMAND name='AddPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + lblVisitorRegno.Text.Trim() + "</PERSONID>"
             + "<FIRSTNAME>" + txtFirst_Name.Text.Trim() + "</FIRSTNAME>"
             + "<EXPDATE>" + Convert.ToDateTime(txtValidToDatetime.Text.Trim()).ToString("yyyy-MM-dd") + "</EXPDATE>"
             + "<ACTDATE>" + Convert.ToDateTime(txtValidFromDatetime.Text).ToString("yyyy-MM-dd") + "</ACTDATE>"
             + "<CONTACTSMSEMAIL>" + txtEmail_Id.Text.Trim() + "</CONTACTSMSEMAIL>"
             + "<ENCODEDNUM>" + txtAsignCardNumber.Text.Trim() + "</ENCODEDNUM>"
             + "<ACCESSLEVELS><ACCESSLEVEL>Visitors Access AL</ACCESSLEVEL></ACCESSLEVELS>"
             + XmlString.Trim()
             + "</PARAMS>"
             + "</COMMAND>"
             + "</NETBOX-API>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.ContentType = "application/XML";
                StreamWriter requestwriter = new StreamWriter(request.GetRequestStream());
                requestwriter.Write(xml);
                requestwriter.Close();
                StreamReader responsereader = new StreamReader(request.GetResponse().GetResponseStream());
                responsemessage = responsereader.ReadToEnd();
                responsereader.Close();
                request.GetResponse().Close();
                XmlDocument doc1 = new XmlDocument();
                doc1.LoadXml(responsemessage);
                XmlNodeList parentNode = doc1.GetElementsByTagName("NETBOX");

                responsemessage = doc1.SelectSingleNode("//NETBOX/RESPONSE/CODE").InnerText;
                if (responsemessage.ToUpper() == "FAIL")
                {
                    string responsemessage2 = doc1.SelectSingleNode("//NETBOX/RESPONSE/DETAILS/ERRMSG").InnerText;
                    responsemessage = responsemessage + ", " + responsemessage2;
                }
                foreach (XmlNode childrenNode in parentNode)
                {
                    Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
                }
                
            }
            catch (WebException ex)
            {
                WebResponse errormessage = ex.Response;
                using (Stream responsestream = errormessage.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responsestream, Encoding.GetEncoding("utf-8"));
                    responsemessage = reader.ReadToEnd();
                }
            }
            return responsemessage;
        }
        protected void AddCardnumberApi()
        {
            string responsemessage = "";
            var url = "http://" + Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + Session["sessionid"] + "'>"
             + "<COMMAND name='AddCredential' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + lblVisitorRegno.Text.Trim() + "</PERSONID>"
             + "<CARDFORMAT>Mifare ASU</CARDFORMAT>"
             + "<HOTSTAMP>1111</HOTSTAMP>"
             + "<ENCODEDNUM>" + txtAsignCardNumber.Text + "</ENCODEDNUM>"
             + "<WANTCREDENTIALID>1</WANTCREDENTIALID>"
             + "<CARDSTATUS>Active</CARDSTATUS>"
             + "</PARAMS>"
             + "</COMMAND>"
             + "</NETBOX-API> ";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.ContentType = "application/XML";
                StreamWriter requestwriter = new StreamWriter(request.GetRequestStream());
                requestwriter.Write(xml);
                requestwriter.Close();
                StreamReader responsereader = new StreamReader(request.GetResponse().GetResponseStream());
                responsemessage = responsereader.ReadToEnd();
                responsereader.Close();
                request.GetResponse().Close();
                XmlDocument doc1 = new XmlDocument();
                doc1.LoadXml(responsemessage);
                XmlNodeList parentNode = doc1.GetElementsByTagName("NETBOX");

                string msg = doc1.SelectSingleNode("//NETBOX/RESPONSE/CODE").InnerText;
                //foreach (XmlNode childrenNode in parentNode)
                //{
                //    Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
                //}

            }
            catch (WebException ex)
            {
                WebResponse errormessage = ex.Response;
                using (Stream responsestream = errormessage.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responsestream, Encoding.GetEncoding("utf-8"));
                    responsemessage = reader.ReadToEnd();
                }
            }
        }
        protected void lnkmodifys2_Click(object sender, EventArgs e)
        {
            if (Session["Sessionid"] == null)
            {
                CheckCreditionals2();
            }
            else
            {
                Push2();
            }
        }
        protected string ModifyPersionApi()
        {
            string XmlString = "";
            if (txtLast_Name.Text.Trim() != "")
            {
                XmlString += "<LASTNAME>" + txtLast_Name.Text.Trim() + "</LASTNAME>";
            }
            if (txtPhone_Number.Text.Trim() != "")
            {
                XmlString += "<CONTACTPHONE>" + txtPhone_Number.Text.Trim() + "</CONTACTPHONE>";
            }
            if (lblvisitor.Text.Trim() != "")
            {
                XmlString += "<PICTURE>" + imgvisitor.ImageUrl.Replace("data:image/png;base64,", "") + "</PICTURE>";
            }
            string responsemessage = "";
            var url = "http://" + Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + Session["sessionid"] + "'>"
             + "<COMMAND name='ModifyPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + lblVisitorRegno.Text.Trim() + "</PERSONID>"
             + "<FIRSTNAME>" + txtFirst_Name.Text.Trim() + "</FIRSTNAME>"
             + "<EXPDATE>" + Convert.ToDateTime(txtValidToDatetime.Text.Trim()).ToString("yyyy-MM-dd") + "</EXPDATE>"
             + "<ACTDATE>" + Convert.ToDateTime(txtValidFromDatetime.Text).ToString("yyyy-MM-dd") + "</ACTDATE>"
             + "<CONTACTSMSEMAIL>" + txtEmail_Id.Text.Trim() + "</CONTACTSMSEMAIL>"
             + "<ENCODEDNUM>" + txtAsignCardNumber.Text.Trim() + "</ENCODEDNUM>"
             + "<ACCESSLEVELS><ACCESSLEVEL>Visitors Access AL</ACCESSLEVEL></ACCESSLEVELS>"
             + XmlString.Trim()
             + "</PARAMS>"
             + "</COMMAND>"
             + "</NETBOX-API> ";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.ContentType = "application/XML";
                StreamWriter requestwriter = new StreamWriter(request.GetRequestStream());
                requestwriter.Write(xml);
                requestwriter.Close();
                StreamReader responsereader = new StreamReader(request.GetResponse().GetResponseStream());
                responsemessage = responsereader.ReadToEnd();
                responsereader.Close();
                request.GetResponse().Close();
                XmlDocument doc1 = new XmlDocument();
                doc1.LoadXml(responsemessage);
                XmlNodeList parentNode = doc1.GetElementsByTagName("NETBOX");

                responsemessage = doc1.SelectSingleNode("//NETBOX/RESPONSE/CODE").InnerText;
                if (responsemessage.ToUpper() == "FAIL")
                {
                    string responsemessage2 = doc1.SelectSingleNode("//NETBOX/RESPONSE/DETAILS/ERRMSG").InnerText;
                    responsemessage = responsemessage + ", " + responsemessage2;
                }
                foreach (XmlNode childrenNode in parentNode)
                {
                    Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
                }

            }
            catch (WebException ex)
            {
                WebResponse errormessage = ex.Response;
                using (Stream responsestream = errormessage.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responsestream, Encoding.GetEncoding("utf-8"));
                    responsemessage = reader.ReadToEnd();
                }
            }
            return responsemessage;
        }
        protected void modifyCardnumberApi()
        {
            string responsemessage = "";
            var url = "http://" + Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + Session["sessionid"] + "'>"
             //+ "<COMMAND name='ModifyCredential' num='1'>"
             + "<COMMAND name='RemoveCredential' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + lblVisitorRegno.Text.Trim() + "</PERSONID>"
             + "<CARDFORMAT>Mifare ASU</CARDFORMAT>"
             + "<HOTSTAMP>1111</HOTSTAMP>"
             + "<ENCODEDNUM>" + txtAsignCardNumber.Text + "</ENCODEDNUM>"
             //+ "<WANTCREDENTIALID>1</WANTCREDENTIALID>"
             //+ "<CARDSTATUS>Disabled</CARDSTATUS>"
             + "</PARAMS>"
             + "</COMMAND>"
             + "</NETBOX-API> ";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.ContentType = "application/XML";
                StreamWriter requestwriter = new StreamWriter(request.GetRequestStream());
                requestwriter.Write(xml);
                requestwriter.Close();
                StreamReader responsereader = new StreamReader(request.GetResponse().GetResponseStream());
                responsemessage = responsereader.ReadToEnd();
                responsereader.Close();
                request.GetResponse().Close();
                XmlDocument doc1 = new XmlDocument();
                doc1.LoadXml(responsemessage);
                XmlNodeList parentNode = doc1.GetElementsByTagName("NETBOX");

                string msg = doc1.SelectSingleNode("//NETBOX/RESPONSE/CODE").InnerText;
                //foreach (XmlNode childrenNode in parentNode)
                //{
                //    Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
                //}

            }
            catch (WebException ex)
            {
                WebResponse errormessage = ex.Response;
                using (Stream responsestream = errormessage.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responsestream, Encoding.GetEncoding("utf-8"));
                    responsemessage = reader.ReadToEnd();
                }
            }
        }
        protected void lnkpushCancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Attributes.Add("style", "display:none;");
            ModalPopupExtender3.Attributes.Add("style", "display:none;");
            myModal.Attributes.Add("style", "display:none;");
            mymodal2.Attributes.Add("style", "display:none;");
            //ModalPopupExtender2.Hide();
            //div3.Visible = false;
            //FillGrid();
            if (lnkCheckIn.Visible == true)
            {
                Response.Redirect("SMS_Visitor_Master.aspx?CopyVisitor=" + Request.QueryString["CopyVisitor"].ToString());
            }
            else
            {
                Response.Redirect("SMS_Visitor_Master.aspx");
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
        public void CheckCreditionals2()
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
                    Push2();
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
            string msg = GetPersionApi().ToUpper();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "select * from SMS_Visitors where Visitor_Reg_No= '" + Request.QueryString["CopyVisitor"].ToString() + "'";
            cmd1.Connection = con;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString().ToLower() == "checkedin")
                {
                    if (msg == "FAIL")
                    {
                        string modifymsg = AddPersionApi();
                        if (modifymsg == "SUCCESS")
                        {
                            AddCardnumberApi();
                            Response.Redirect("SMS_Visitor_Master.aspx?CopyVisitor=" + Request.QueryString["CopyVisitor"].ToString());
                        }
                        else
                        {
                            lblmsg.Text = modifymsg;
                            myModal.Style.Remove("display");
                            myModal.Attributes.Add("style", "display:block;");
                            mymodal2.Attributes.Add("style", "display:none;");
                            lnkOkk.Visible = true;
                            lblS2.Visible = false;
                            lnkGo.Visible = false;
                            lnkpushCancel.Visible = false;
                        }
                    }
                    else
                    {
                        string modifymsg = AddPersionApi();
                        lblmsg.Text = modifymsg;
                        myModal.Style.Remove("display");
                        myModal.Attributes.Add("style", "display:block;");
                        mymodal2.Attributes.Add("style", "display:none;");
                        lnkOkk.Visible = true;
                        lblS2.Visible = false;
                        lnkGo.Visible = false;
                        lnkpushCancel.Visible = false;
                    }
                }
                if (ds1.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString().ToLower() == "checkedout")
                {
                    if (msg == "SUCCESS")
                    {
                        string modifymsg = ModifyPersionApi();
                        if (modifymsg == "SUCCESS")
                        {
                            modifyCardnumberApi();
                            Response.Redirect("SMS_Visitor_Master.aspx");
                        }
                        else
                        {
                            lblmsg.Text = modifymsg;
                            myModal.Style.Remove("display");
                            myModal.Attributes.Add("style", "display:block;");

                            mymodal2.Attributes.Add("style", "display:none;");
                            lnkOkk.Visible = true;
                            lblS2.Visible = false;
                            lnkGo.Visible = false;
                            lnkpushCancel.Visible = false;
                        }
                    }
                    else
                    {
                        string modifymsg = ModifyPersionApi();
                        lblmsg.Text = modifymsg;
                        myModal.Style.Remove("display");
                        myModal.Attributes.Add("style", "display:block;");
                        mymodal2.Attributes.Add("style", "display:none;");
                        lnkOkk.Visible = true;
                        lblS2.Visible = false;
                        lnkGo.Visible = false;
                        lnkpushCancel.Visible = false;
                    }
                }
            }
        }
        public  void Push2()
        {
            string msg = GetPersionApi().ToUpper();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "select * from SMS_Visitors where Visitor_Reg_No= '" + lblVisitorRegno.Text.Trim() + "'";
            cmd1.Connection = con;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString().ToLower() == "checkedin")
                {
                    if (msg == "FAIL")
                    {
                        string modifymsg = AddPersionApi();
                        if (modifymsg == "SUCCESS")
                        {
                            AddCardnumberApi();
                            Response.Redirect("SMS_Visitor_Master.aspx?CopyVisitor=" + Request.QueryString["CopyVisitor"].ToString());
                        }
                        else
                        {
                            lblmsg.Text = modifymsg;
                            myModal.Style.Remove("display");
                            myModal.Attributes.Add("style", "display:block;");
                            mymodal2.Attributes.Add("style", "display:none;");
                            lnkOkk.Visible = true;
                            lblS2.Visible = false;
                            lnkGo.Visible = false;
                            lnkpushCancel.Visible = false;
                        }
                    }
                    else
                    {
                        string modifymsg = AddPersionApi();
                        lblmsg.Text = modifymsg;
                        myModal.Style.Remove("display");
                        myModal.Attributes.Add("style", "display:block;");
                        mymodal2.Attributes.Add("style", "display:none;");
                        lnkOkk.Visible = true;
                        lblS2.Visible = false;
                        lnkGo.Visible = false;
                        lnkpushCancel.Visible = false;
                    }
                }
                if (ds1.Tables[0].Rows[0]["Visitor_PreReg_Status"].ToString().ToLower() == "checkedout")
                {
                    string addmsg = AddPersionApi();
                    AddCardnumberApi();
                    if (msg == "SUCCESS")
                    {
                        string modifymsg = ModifyPersionApi();
                        if (modifymsg == "SUCCESS")
                        {
                            modifyCardnumberApi();
                            Response.Redirect("SMS_Visitor_Master.aspx");
                        }
                        else
                        {
                            lblmsg.Text = modifymsg;
                            myModal.Style.Remove("display");
                            myModal.Attributes.Add("style", "display:block;");

                            mymodal2.Attributes.Add("style", "display:none;");
                            lnkOkk.Visible = true;
                            lblS2.Visible = false;
                            lnkGo.Visible = false;
                            lnkpushCancel.Visible = false;
                        }
                    }
                    else
                    {
                        string modifymsg = ModifyPersionApi();
                        lblmsg.Text = modifymsg;
                        myModal.Style.Remove("display");
                        myModal.Attributes.Add("style", "display:block;");
                        mymodal2.Attributes.Add("style", "display:none;");
                        lnkOkk.Visible = true;
                        lblS2.Visible = false;
                        lnkGo.Visible = false;
                        lnkpushCancel.Visible = false;
                    }
                }
            }

        }
        public void drawimg(string base64, string filename)  // Drawing image from Base64 string.
        {
            SqlCommand cmd = new SqlCommand("SP_Visitor_Reg_No", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            if (ds.Rows.Count > 0)
            {
                code = ds.Rows[0]["Visitor_Reg_No"].ToString();
            }
            base64 = base64.Replace("data:image/jpeg;base64,", "");
            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                DateTime nm = DateTime.Now;
                string date = nm.ToString("yyyymmddMMss");
                image = System.Drawing.Image.FromStream(ms);
                //image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                System.Drawing.Bitmap returnImage = new System.Drawing.Bitmap(System.Drawing.Image.FromStream(ms, true, true), 100, 100);
                Image = "~/Images/VisitorImages/" + date + ".jpg";
                returnImage.Save(Server.MapPath("~/Images/VisitorImages/" + date + ".jpg"));

            }
        }
        protected void DivUpload_Click(object sender, EventArgs e)
        {
            ModalWebcamPopup.Attributes.Add("style", "display:block;");
        }
    }

}