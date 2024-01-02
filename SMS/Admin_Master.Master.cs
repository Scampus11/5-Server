using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using SMS.Class;
using System.Web.Configuration;
using System.Xml;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace SMS
{
    public partial class Admin_Master : System.Web.UI.MasterPage
    {
        LogFile logFile = new LogFile();
        String SQLString = "";
        S2API S2API = new S2API();
        string key = "b14ca5898a4e4133bbce2ea2315a1916";
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                if (Session["Sessionid"] == null)
                {
                    lblmsgsuccessred.Visible = true;
                    lblmsgsuccessgreen.Visible = false;
                }
                else
                {
                    lblmsgsuccessgreen.Visible = true;
                    lblmsgsuccessred.Visible = false;
                }
                Connection();
                //lnkpopupApi_Click(sender, e);
                SqlCommand cmd1 = new SqlCommand("SP_User_Login", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@username", Session["UserName"].ToString().Trim());
                cmd1.Parameters.AddWithValue("@password", Session["Pwd"].ToString().Trim());
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da1.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["cardstatus"].ToString() == "5")
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                if (Session["Sider"] != null)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "select * from logo";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        imglogo.ImageUrl = ds.Tables[0].Rows[0]["Images"].ToString();
                    }
                    con.Close();
                    if (Session["Sider"].ToString() == "Student Master")
                    {
                        liStudent.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Dashboard")
                    {
                        liMainDashboard.Attributes.Add("class", "nav-item start active");
                        liMainDashboard.Visible = true;
                    }
                    else if (Session["Sider"].ToString() == "Staff Master")
                    {
                        liOfficer.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Reader Master")
                    {
                        liReader.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Door Group Master")
                    {
                        liDoorGroup.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Access Group Master")
                    {
                        liAccessGroup.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Student Access Master")
                    {
                        liStudentAccess.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Staff Access Master")
                    {
                        liStaffAccess.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Session Master")
                    {
                        liSession.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Visitor Master")
                    {
                        liVisitor.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Visitor Card Master")
                    {
                        liVisitorCard.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Visitor Type Master")
                    {
                        liVisitorType.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Visitor Reason Master")
                    {
                        liVisitorReason.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Visitor Access level")
                    {
                        liVisitorAccessLevel.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Service Master")
                    {
                        liService.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Live SAG Monitoring Screen")
                    {
                        liCanteen.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "SuperVisor Canteen Monitor")
                    {
                        liSuperVisor.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Canteen Public Monitor")
                    {
                        liPublic.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Canteen Report")
                    {
                        liCanteen_Report.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Food Master")
                    {
                        liFood.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Video Master")
                    {
                        liVideo.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Quote Master")
                    {
                        liQuote.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Role")
                    {
                        liRole.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Role Master")
                    {
                        liRoleMaster.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "PageRight")
                    {
                        lipage_Right.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "logo")
                    {
                        lilogo.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Campus Master")
                    {
                        liCampus.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "College Master")
                    {
                        liCollege.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Department Master")
                    {
                        liDepartment.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Staff Department")
                    {
                        liEmpDepartment.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Staff Facility")
                    {
                        liEmpFacility.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Year Master")
                    {
                        liYear.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Job Title Master")
                    {
                        lijobtitle.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Company Master")
                    {
                        liCompany.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Country Master")
                    {
                        liCountry.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Program Master")
                    {
                        liProgramMaster.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Degree Type Master")
                    {
                        liDegreeType.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Admission Type Master")
                    {
                        liAdminssion.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Student Canteen Assignment")
                    {
                        liCanteen_Assigned_History.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Student Card Assignment")
                    {
                        liStudent_Card_Assignment.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Employee Served Visitor")
                    {
                        liVisitor_Served_History.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Block Groups Access Logs")
                    {
                        liBlock_Groups_Access_Logs.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "DynamicDB")
                    {
                        liDynamicDB.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Email configuration")
                    {
                        liSMS_Email_Configuration.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Upload Badge File")
                    {
                        liSMS_Upload_label_File.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Hostel")
                    {
                        liHostel.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Block")
                    {
                        liBlock.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Room")
                    {
                        liRoom.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Bed")
                    {
                        liBed.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Locker")
                    {
                        liLocker.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "Block Group")
                    {
                        liProcter_Block_Group.Attributes.Add("class", "nav-item start active");
                    }
                    else if (Session["Sider"].ToString() == "PROCTER ACCESS LOGS")
                    {
                        liProcterAccessLogs.Attributes.Add("class", "nav-item start active");
                    }
                    divSider.Visible = true;

                }
                else
                {
                    body.Attributes.Add("class", "page-sidebar-closed");
                    divSider.Visible = false;
                }
                PageRight();

                lblUserName.Text = Session["UserName"].ToString();
                lbluserid.Text = Session["UserID"].ToString();
                imgstaff.ImageUrl = Session["Images"].ToString();
                if (Session["UserName"].ToString().ToUpper() == "SUPERADMIN")
                {
                    txtpassword.Visible = false;
                }
                if (Session["Role_Id"].ToString() == "1" || Session["Role_Id"].ToString() == "3" || Session["Role_Id"].ToString() == "2")
                {
                    liproctordisable.Visible = true;
                    liBlock_Groups_Access_Logs.Visible = true;
                }
                else
                {
                    liproctordisable.Visible = false;
                    liBlock_Groups_Access_Logs.Visible = false;
                }
                //Check page Rights
                SqlCommand cmdpgrgtchk = new SqlCommand("SP_Page_Right_Check", con);
                cmdpgrgtchk.CommandType = CommandType.StoredProcedure;
                cmdpgrgtchk.Parameters.AddWithValue("@StaffId", Session["UserName"].ToString().Trim());
                cmdpgrgtchk.Parameters.AddWithValue("@panelName", Session["Sider"].ToString().Trim());
                SqlDataAdapter dapgrgtchk = new SqlDataAdapter(cmdpgrgtchk);
                DataTable dtpgrgtchk = new DataTable();
                dapgrgtchk.Fill(dtpgrgtchk);

                if (dtpgrgtchk.Rows.Count > 0)
                {
                    //myModal.Attributes.Add("style", "display:None;");
                }
                else
                {
                    if (Session["Sider"].ToString().Trim() == "Dashboard")
                    {
                        //  myModal.Attributes.Add("style", "display:None;");
                    }
                    else if (Session["Sider"].ToString().Trim() == "PROCTER ACCESS LOGS LIST")
                    {
                        //  myModal.Attributes.Add("style", "display:None;");
                    }
                    else
                    {
                        //myModal.Attributes.Add("style", "display:block;");
                        Response.Redirect("Page_Not_Access.aspx");
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
            Session["UserName"] = null;
            Session["Pwd"] = null;
            Session["Sider"] = null;
            Session["Sessionid"] = null;
            Response.Redirect("Login.aspx");

        }
        protected void PageRight()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Get_Page_Right_Active", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Officer");
                cmd.Parameters.AddWithValue("@Staff_Id", Session["UserName"].ToString().Trim());
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    if (ds.Rows[0]["Role_Id"].ToString().Trim() == "1")
                    {
                        liRole.Visible = true;
                        lipage_Right.Visible = true;
                        liRoleMaster.Visible = true;
                    }
                }



                SqlCommand cmd1 = new SqlCommand("SP_Get_Page_Right_Active", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Flag", "PageActive");
                cmd1.Parameters.AddWithValue("@Staff_Id", Session["UserName"].ToString().Trim());
                cmd1.Connection = con;

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Rows.Count; i++)
                    {
                        if (lblStudent_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liStudent.Visible = true;
                        }
                        else if (lblStaff_Employee_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liOfficer.Visible = true;
                        }
                        else if (lblSMS_Reader_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liReader.Visible = true;
                        }
                        else if (lblSMS_Door_Group_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liDoorGroup.Visible = true;
                        }
                        else if (lblSMS_Access_Group_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liAccessGroup.Visible = true;
                        }
                        else if (lblSMS_Student_Access_List.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liStudentAccess.Visible = true;
                        }
                        else if (lblSMS_Staff_Access_List.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liStaffAccess.Visible = true;
                        }
                        else if (lblSMS_Session_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liSession.Visible = true;
                        }
                        else if (lblSMS_Visitor_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liVisitor.Visible = true;
                        }
                        else if (lblSMS_Visitor_Card_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liVisitorCard.Visible = true;
                        }
                        else if (lblSMS_Visitor_Type_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liVisitorType.Visible = true;
                        }
                        else if (lblSMS_Visitor_Reason_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liVisitorReason.Visible = true;
                        }
                        else if (lblSMS_Visitor_Access_Level.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liVisitorAccessLevel.Visible = true;
                        }
                        else if (lblSMS_Service_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liService.Visible = true;
                        }
                        else if (lblSMS_Live_Canteen_Monitoring_Screen.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liCanteen.Visible = true;
                        }
                        else if (lblSupervisor_Canteen_Monitor.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liSuperVisor.Visible = true;
                        }
                        else if (lblCanteen_Public_Monitor.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liPublic.Visible = true;
                        }
                        else if (lblCanteen_Report.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liCanteen_Report.Visible = true;
                        }
                        else if (lblSMS_Food_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liFood.Visible = true;
                        }
                        else if (lblSMS_Food_Video_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liVideo.Visible = true;
                        }
                        else if (lblSMS_Quote_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liQuote.Visible = true;
                        }
                        else if (lblFileUpload.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liFileUpload.Visible = true;
                        }
                        else if (lblimage_Blob.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liimage_Blob.Visible = true;
                        }
                        else if (lblSMS_logo.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            lilogo.Visible = true;
                        }
                        else if (lblSMS_Campus_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liCampus.Visible = true;
                        }
                        else if (lblSMS_College_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liCollege.Visible = true;
                        }
                        else if (lblSMS_Department_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liDepartment.Visible = true;
                        }
                        else if (lblEmpDepartment.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liEmpDepartment.Visible = true;
                        }
                        else if (lblEmpFacility.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liEmpFacility.Visible = true;
                        }
                        else if (lblYear_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liYear.Visible = true;
                        }
                        else if (lblSMS_Job_Title.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            lijobtitle.Visible = true;
                        }
                        else if (lblSMS_Company_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liCompany.Visible = true;
                        }
                        else if (lblSMS_Country_Master.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liCountry.Visible = true;
                        }
                        else if (lblProgramMaster.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liProgramMaster.Visible = true;
                        }
                        else if (lblDegreeTypeMaster.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liDegreeType.Visible = true;
                        }
                        else if (lblAdminssionMaster.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liAdminssion.Visible = true;
                        }
                        else if (lblCanteen_Assigned_History.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liCanteen_Assigned_History.Visible = true;
                        }
                        else if (lblStudent_Card_Assignment.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liStudent_Card_Assignment.Visible = true;
                        }
                        else if (lblVisitor_Served_History.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liVisitor_Served_History.Visible = true;
                        }
                        else if (lblBlock_Groups_Access_Logs.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liBlock_Groups_Access_Logs.Visible = true;
                        }
                        else if (lblSMS_SQL_Connection_Dynamic.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liDynamicDB.Visible = true;
                        }
                        else if (lblSMS_Email_Configuration.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liSMS_Email_Configuration.Visible = true;
                        }
                        else if (lblSMS_Upload_label_File.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liSMS_Upload_label_File.Visible = true;
                        }
                        else if (lblS2LoginAPI.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liS2LoginAPI.Visible = true;
                        }
                        else if (lblHostel.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liHostel.Visible = true;
                        }
                        else if (lblBlock.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liBlock.Visible = true;
                        }
                        else if (lblFloor.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liFloor.Visible = true;
                        }
                        else if (lblRoom.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liRoom.Visible = true;
                        }
                        else if (lblBed.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liBed.Visible = true;
                        }
                        else if (lblLocker.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liLocker.Visible = true;
                        }
                        else if (lblProcter_Block_Group.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liProcter_Block_Group.Visible = true;
                        }
                        else if (lblProcterAccessLogs.Text.Trim() == ds1.Rows[i]["Page_Name"].ToString().Trim())
                        {
                            liProcterAccessLogs.Visible = true;
                        }
                    }
                }
                else
                {
                    Session["notaccess"] = "1";
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Sorry!!! You do not have access to this portal,kindly contact to administrator.');", true);
                    // Response.Redirect("Login.aspx");
                    //Response.Redirect("Student_Master.aspx");
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkchangepwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Change_password.aspx?Id=" + Session["UserName"].ToString() + "");
        }

        //protected void lnkalloedmem_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Canteen_Monitoring.aspx?Flag=" + Session["Canteen_Flag"].ToString() + "&Session_Id=" + Session["Session_Id"].ToString() + "&CanteenName=" + Session["CanteenName"].ToString() + "&Count=" + Session["CanteenCount"].ToString() + "");
        //}
        protected void Connection()
        {
            try
            {
                string filePath1 = Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
                StreamReader sr = new StreamReader(filePath1);
                SQLString = sr.ReadToEnd();
                con = new SqlConnection(SQLString);
                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
                Response.Redirect("SMS_SQL_Connection.aspx");
            }

        }

        protected void lnkGo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        protected void lnklogin_Click(object sender, EventArgs e)
        {
            string responsemessage = "";
            if (Validation())
            {
                SqlCommand cmd = new SqlCommand("SPS2loginAPI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Get");
                cmd.Parameters.AddWithValue("@IpAddress", txtipaddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtusername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtapipassword.Text.Trim());
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (ds.Rows.Count == 0)
                {
                    try
                    {
                        responsemessage = LoginApi("Save");
                        if (responsemessage.ToUpper() == "SUCCESS")
                        {
                            lblmsgsuccessred.Visible = false;
                            lblmsgsuccessgreen.Visible = true;
                            ModalPopupExtenderAPIs.Hide();
                        }
                        else
                        {
                            lblmsgsuccessred.Visible = true;
                            lblmsgsuccessgreen.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblAPISuccess.Text = "Invalid Credentials !!!";
                        lblAPISuccess.Visible = true;
                    }
                }
                else
                {
                    try
                    {
                        responsemessage = LoginApi("Update");
                        if (responsemessage.ToUpper() == "SUCCESS")
                        {
                            lblmsgsuccessred.Visible = false;
                            lblmsgsuccessgreen.Visible = true;
                            ModalPopupExtenderAPIs.Hide();
                        }
                        else
                        {
                            lblmsgsuccessred.Visible = true;
                            lblmsgsuccessgreen.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblAPISuccess.Text = "Invalid Credentials !!!";
                        lblAPISuccess.Visible = true;
                    }
                }
            }
        }
        protected string LoginApi(string Action)
        {
            string responsemessage = "";
            responsemessage = S2API.LoginAPI(txtipaddress.Text.Trim(), txtusername.Text.Trim(), txtapipassword.Text.Trim(), Action);
            lblAPISuccess.Text = responsemessage;
            lblAPISuccess.Visible = true;
            if (responsemessage == "SUCCESS" && Action == "Save")
            {
                SqlCommand cmd = new SqlCommand("SPS2loginAPI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Save");
                cmd.Parameters.AddWithValue("@IpAddress", txtipaddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtusername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", S2API.EncryptString(key, txtapipassword.Text.Trim()));
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else if (responsemessage == "SUCCESS" && Action == "Update")
            {
                SqlCommand cmd = new SqlCommand("SPS2loginAPI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Update");
                cmd.Parameters.AddWithValue("@IpAddress", txtipaddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtusername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", S2API.EncryptString(key, txtapipassword.Text.Trim()));
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return responsemessage;
        }
        protected bool Validation()
        {
            if (txtipaddress.Text.Trim() == "")
            {
                lblipaddressvdn.Visible = true;
                lblpasswordvdn.Visible = false;
                lblusernamevdn.Visible = false;
                return false;
            }
            if (txtusername.Text.Trim() == "")
            {
                lblipaddressvdn.Visible = false;
                lblpasswordvdn.Visible = false;
                lblusernamevdn.Visible = true;
                return false;
            }
            if (txtapipassword.Text.Trim() == "")
            {
                lblipaddressvdn.Visible = false;
                lblpasswordvdn.Visible = true;
                lblusernamevdn.Visible = false;
                return false;
            }
            return true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderAPIs.Hide();
        }

        protected void lnkpopupApi_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SPS2loginAPI", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Get");
            cmd.Parameters.AddWithValue("@IpAddress", txtipaddress.Text.Trim());
            cmd.Parameters.AddWithValue("@Username", txtusername.Text.Trim());
            cmd.Parameters.AddWithValue("@password", txtapipassword.Text.Trim());
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count == 0)
            {
                txtipaddress.Text = "";
                txtusername.Text = "";
                txtapipassword.Text = "";
            }
            else
            {
                txtipaddress.Text = ds.Rows[0]["IPAddress"].ToString();
                txtusername.Text = ds.Rows[0]["Username"].ToString();
                txtapipassword.Text = ""; 
                //S2API.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", ds.Rows[0]["Password"].ToString());
            }
            ModalPopupExtenderAPIs.Show();
        }

    }
}