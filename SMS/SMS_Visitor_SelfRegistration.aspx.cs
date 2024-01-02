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
using SMS.Class;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using QRCoder;
using System.Text;
using System.Security.Cryptography;
namespace SMS
{
    public partial class SMS_Visitor_SelfRegistration : System.Web.UI.Page
    {
        LogFile LogFile = new LogFile();
        string Image = "";
        string VisitorImage = "";
        string Photopath = "";
        string filePath = "";
        byte[] bytes = new byte[1];
        string code = "";
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!IsPostBack)
            {
                try
                {
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    //ddlCountry_SelectedIndexChanged(sender, e);
                    BindVisitorType();
                    BindVisitorAccesslevel();
                    BindVisitorReason();
                    BindCountryCode();
                    BindServices();
                    //ddlCountry.SelectedValue = "+251";
                    //if (Session["FirstName"] != null)
                    //{
                    //    txtFirst_Name.Text = Session["FirstName"].ToString();
                    //}
                    //if (Session["LastName"] != null)
                    //{
                    //    txtLast_Name.Text = Session["LastName"].ToString();
                    //}
                    //if (Session["CompanyName"] != null)
                    //{
                    //    txtCompany.Text = Session["CompanyName"].ToString();
                    //}
                    //if (Session["VisitorType"] != null)
                    //{
                    //    ddlVisitor_Type.SelectedValue = Session["VisitorType"].ToString();
                    //}
                    //if (Session["visitorReason"] != null)
                    //{
                    //    ddlVisit_Reason.SelectedValue = Session["visitorReason"].ToString();
                    //}
                    //if (Session["Countrycode"] != null)
                    //{
                    //    ddlCountry.SelectedValue = Session["Countrycode"].ToString();
                    //}
                    //if (Session["phoneno"] != null)
                    //{
                    //    txtPhone_Number.Text = Session["phoneno"].ToString();
                    //}
                    //if (Session["EmailId"] != null)
                    //{
                    //    txtEmail_Id.Text = Session["EmailId"].ToString();
                    //}
                    //if (Session["NationalID"] != null)
                    //{
                    //    txtNational_ID.Text = Session["NationalID"].ToString();
                    //}
                    //if (Session["Accesslevel"] != null)
                    //{
                    //    ddlAccess_Level.SelectedValue = Session["Accesslevel"].ToString();
                    //}
                    //if (Session["ImgURL"] != null)
                    //{

                    //    imgStaff.ImageUrl = Session["ImgURL"].ToString();

                    //    Image = Session["ImgURL"].ToString();
                    //    bytes = Convert.FromBase64String(Session["imgbase64"].ToString());
                    //}
                    //else if (Session["ImgURL1"] != null)
                    //{

                    //    imgStaff.ImageUrl = Session["ImgURL1"].ToString();

                    //    Image = Session["ImgURL1"].ToString();
                    //    //bytes = Convert.FromBase64String(Session["imgbase64"].ToString());
                    //}
                    //else
                    //{
                    //    imgStaff.ImageUrl = "~/Images/student.jpeg";
                    //}
                    imgStaff.ImageUrl = "~/Images/student.jpeg";
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
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {

                    string listEmployess = "";
                    string ServiceId = "";
                    if (hidValue.Value != "")
                    {
                        drawimg(hidValue.Value.Replace("data:image/png;base64,", String.Empty), "");
                    }
                   
                    SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Insert");
                    cmd.Parameters.AddWithValue("@Visitor_RecId", "");
                    cmd.Parameters.AddWithValue("@Visitor_GUID", "");
                    cmd.Parameters.AddWithValue("@First_Name", txtFirst_Name.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", txtLast_Name.Text);
                    cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Type", ddlVisitor_Type.SelectedValue);
                    cmd.Parameters.AddWithValue("@Visit_Reason", ddlVisit_Reason.SelectedValue);
                    cmd.Parameters.AddWithValue("@Phone_Number", ddlCountry.SelectedValue + ' ' + txtPhone_Number.Text);
                    cmd.Parameters.AddWithValue("@Visitor_Photo", VisitorImage);
                    cmd.Parameters.AddWithValue("@ID_Number", "");
                    cmd.Parameters.AddWithValue("@National_ID", txtNational_ID.Text);
                    cmd.Parameters.AddWithValue("@Host_Employee_Code", Session["UserName"].ToString());
                    cmd.Parameters.AddWithValue("@Access_Card_Number", "");
                    cmd.Parameters.AddWithValue("@Access_Level", ddlAccess_Level.SelectedValue);
                    cmd.Parameters.AddWithValue("@Check_In", "");
                    cmd.Parameters.AddWithValue("@Valid_To", "");
                    cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                    cmd.Parameters.AddWithValue("@Valid_From", "");
                    cmd.Parameters.AddWithValue("@Check_Out", "");
                    cmd.Parameters.AddWithValue("@CreatedBy", "");
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", "");
                    cmd.Parameters.AddWithValue("@RecordStatus", "");
                    cmd.Parameters.AddWithValue("@Id", 0);
                    cmd.Parameters.AddWithValue("@hash", bytes);
                    cmd.Parameters.AddWithValue("@ServicesId", listEmployess);
                    cmd.Parameters.AddWithValue("@Visitor_PreReg_Status", "SelfRegistered");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Session["VisitorPhoto"] = null;
                    foreach (ListItem li in lstmoveemp.Items)
                    {
                        SqlCommand cmd1 = new SqlCommand("SP_Get_Last_Entry_Visitors", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;

                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable ds1 = new DataTable();
                        da.Fill(ds1);
                        if(ds1.Rows.Count>0)
                        {

                            listEmployess += li.Value + ",";
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
                    string URL = WebConfigurationManager.AppSettings["HostName"].ToString() + "QRImages/" + code + ".jpg";
                    //Email(txtFirst_Name.Text, txtLast_Name.Text, listEmployess, "",
                    //    ddlVisitor_Type.SelectedItem.Text, txtCompany.Text, ddlVisit_Reason.SelectedItem.Text, code,
                    //    txtEmail_Id.Text, ServiceId);
                    Response.Redirect("SelfRegistrationSuccessfully.aspx");
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected bool Validation()
        {
            if (txtFirst_Name.Text == "")
            {
                SpanFirstName.Attributes.Add("style", "display: normal; color: red;");
                SpanEmail.Attributes.Add("style", "display: none; color: red;");
                spnservices.Attributes.Add("style", "display: none; color: red;");
                return false;
            }

            if (txtEmail_Id.Text == "")
            {
                SpanEmail.Attributes.Add("style", "display: normal; color: red;");
                SpanFirstName.Attributes.Add("style", "display: none; color: red;");
                spnservices.Attributes.Add("style", "display: none; color: red;");
                return false;
            }
            string ServiceId = "";
            foreach (ListItem li in lstmoveemp.Items)
            {
                ServiceId += li.Value + ",";
            }
            if (ServiceId == "")
            {
                SpanFirstName.Attributes.Add("style", "display: none; color: red;");
                SpanEmail.Attributes.Add("style", "display: none; color: red;");
                spnservices.Attributes.Add("style", "display: normal; color: red;");
                return false;
            }
            //bool isValid = ucCaptcha.Validate(txtCaptcha.Text.Trim());
            //if (isValid)
            //{
            //    lblMessage.Text = "Valid!";
            //    lblMessage.ForeColor = Color.Green;
            //}
            //else
            //{
            //    lblMessage.Text = "Invalid!";
            //    lblMessage.ForeColor = Color.Red;
            //    return false;
            //}

            return true;
        }
        protected void ImageUpload()
        {

            if (FileUpload1.HasFile)
            {
                try
                {
                    string str = FileUpload1.FileName;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/VisitorImages/" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace(" ", "_").Replace(":", "_").Replace("/", "_") + "_" + str));
                    Image = "~/Images/VisitorImages/" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace(" ", "_").Replace(":", "_").Replace("/", "_") + "_" + str.ToString();
                    string ext = System.IO.Path.GetExtension(FileUpload1.FileName.ToString());
                    string FileName1 = Guid.NewGuid().ToString();
                    string FileName = System.IO.Path.GetFileName(FileUpload1.FileName.ToString());
                    string Imagepath1 = Server.MapPath("~\\" + "Images\\VisitorImages");
                    FileUpload1.SaveAs(Imagepath1 + "\\" + FileName1 + ext);
                    string pathForDB = "Images\\VisitorImages";
                    Photopath = "~\\" + pathForDB + "\\" + FileName1 + ext;

                    Stream fs = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);

                    //Save the Byte Array as File.
                    filePath = "/Images/VisitorImages/" + Path.GetFileName(FileUpload1.FileName);
                    File.WriteAllBytes(Server.MapPath(filePath), bytes);
                    
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue == "India")
            {
                txtPhone_Number.Text = "+91 - ";
            }
            else if (ddlCountry.SelectedValue == "UAE")
            {
                txtPhone_Number.Text = "+971 - ";
            }
            else if (ddlCountry.SelectedValue == "Israel")
            {
                txtPhone_Number.Text = "+972 - ";
            }
            else if (ddlCountry.SelectedValue == "SAU")
            {
                txtPhone_Number.Text = "+966 - ";
            }
            else if (ddlCountry.SelectedValue == "SA")
            {
                txtPhone_Number.Text = "+27 - ";
            }
        }
        protected void BindCountryCode()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Get_Country_Data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                ddlCountry.DataSource = ds;
                ddlCountry.DataValueField = "CodeId";
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataBind();
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

        protected void btncapture_Click(object sender, EventArgs e)
        {
            try
            {
                //Session["FirstName"] = txtFirst_Name.Text;
                //Session["LastName"] = txtLast_Name.Text;
                //Session["CompanyName"] = txtCompany.Text;
                //Session["VisitorType"] = ddlVisitor_Type.SelectedValue.Trim();
                //Session["visitorReason"] = ddlVisit_Reason.SelectedValue.Trim();
                //Session["Countrycode"] = ddlCountry.SelectedValue.Trim();
                //Session["phoneno"] = txtPhone_Number.Text;
                //Session["EmailId"] = txtEmail_Id.Text;
                //Session["NationalID"] = txtNational_ID.Text;
                //Session["Accesslevel"] = ddlAccess_Level.SelectedValue.Trim();

                Response.Redirect("Captureimage.aspx");
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            ImageUpload();
            Response.Redirect("SMS_Visitor_SelfRegistration.aspx");
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
                    lstEmp.DataSource = null;
                    lstEmp.DataBind();
                }

            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
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
            base64 = base64.Replace("data:image/jpeg;base64,","");
            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                DateTime nm = DateTime.Now;
                string date = nm.ToString("yyyymmddMMss");
                image = System.Drawing.Image.FromStream(ms);
                //image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                System.Drawing.Bitmap returnImage = new System.Drawing.Bitmap(System.Drawing.Image.FromStream(ms, true, true), 100, 100);
                VisitorImage = "~/Images/VisitorImages/" + date + ".jpg";
                returnImage.Save(Server.MapPath("~/Images/VisitorImages/" + date + ".jpg"));

            }
        }
        //public void Email(string FirstName, string LastName, string ServiceName, string ValidFrom, string VisitorType,
        //   string Company, string Reason, string Code, string Email, string serviceId)
        //{
        //    try
        //    {
        //        string Emails = Email + "," + Session["Email_Id"].ToString();
        //        string ServiceEmails = "";
        //        SqlCommand cmdservice = new SqlCommand("SP_SMS_GET_SERVICES", con);
        //        cmdservice.CommandType = CommandType.StoredProcedure;
        //        cmdservice.Connection = con;
        //        cmdservice.Parameters.AddWithValue("@Flag", "ServiceEmail");
        //        cmdservice.Parameters.AddWithValue("@Emails", Emails);
        //        cmdservice.Parameters.AddWithValue("@Employee", serviceId);
        //        cmdservice.Connection = con;
        //        SqlDataAdapter daservice = new SqlDataAdapter(cmdservice);
        //        DataSet dsService = new DataSet();
        //        daservice.Fill(dsService);
        //        if (dsService.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dsService.Tables[0].Rows.Count; i++)
        //            {
        //                if (dsService.Tables[0].Rows[i]["Employee"].ToString() != "")
        //                {
        //                    ServiceEmails += dsService.Tables[0].Rows[i]["Employee"].ToString() + ",";
        //                }
        //            }
        //        }
        //        ServiceEmails = ServiceEmails.TrimEnd(',');
        //        SqlCommand cmd = new SqlCommand("SMS_SP_Get_Email_Configuration", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = con;
        //        cmd.Parameters.AddWithValue("@Flag", "Get");
        //        cmd.Connection = con;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            ////Visitor Email
        //            string Host_Emp_EmailId = Session["Email_Id"].ToString();
        //            MailMessage msg = new MailMessage();
        //            msg.From = new MailAddress(ds.Tables[0].Rows[0]["Email_Address"].ToString());
        //            msg.To.Add(Email);
        //            msg.Subject = "You are scheduled for service " + ServiceName + " by " + Session["UserID"] + " on " + txtValidFromDatetime.Text + ".";
        //            //msg.Body = body;
        //            msg.Body = "You are scheduled for service " + ServiceName + " by " + Session["UserID"] + " on " + txtValidFromDatetime.Text + ".";
        //            msg.IsBodyHtml = true;
        //            if (ServiceEmails != "")
        //            {
        //                msg.CC.Add(ServiceEmails);
        //            }
        //            //msg.Priority = MailPriority.High;
        //            SmtpClient client = new SmtpClient();
        //            // decrypt parameters
        //            var key = "b14ca5898a4e4133bbce2ea2315a1916";
        //            var decrptedInput = DecryptString(key, ds.Tables[0].Rows[0]["password"].ToString());
        //            client.Credentials = new NetworkCredential(ds.Tables[0].Rows[0]["Email_Address"].ToString(), decrptedInput);
        //            client.Host = ds.Tables[0].Rows[0]["Outgoing_Services"].ToString();
        //            client.Port = Convert.ToInt32(ds.Tables[0].Rows[0]["SMTP"].ToString());
        //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            if (ds.Tables[0].Rows[0]["Connection"].ToString() == "1")
        //            {
        //                client.EnableSsl = true;
        //            }
        //            else
        //            {

        //            }
        //            if (ddlVisitor_Type.SelectedIndex == 0)
        //            {
        //                VisitorType = "";
        //            }
        //            if (ddlVisit_Reason.SelectedIndex == 0)
        //            {
        //                Reason = "";
        //            }
        //            Dictionary<String, String> replacements = new Dictionary<string, string>();
        //            replacements.Add("recipient", FirstName + " " + LastName);
        //            replacements.Add("ServiceName", ServiceName);
        //            replacements.Add("ValidFrom", ValidFrom);
        //            replacements.Add("UserId", Session["UserId"].ToString());
        //            replacements.Add("VisitDate", ValidFrom);
        //            replacements.Add("VisitType", VisitorType);
        //            replacements.Add("Company", Company);
        //            replacements.Add("VisitReason", Reason);
        //            replacements.Add("code", code);
        //            AlternateView html = CreateAlternateViewFromTemplate(Server.MapPath("EmailTemplate/Email.html"), "text/html", replacements);
        //            Byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage(QRimages));
        //            System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);

        //            LinkedResource img1 = new LinkedResource(streamBitmap, "image/jpeg");
        //            img1.ContentId = "img1";

        //            html.LinkedResources.Add(img1);
        //            //msg.AlternateViews.Add(plain);
        //            msg.AlternateViews.Add(html);
        //            //client.UseDefaultCredentials = true;
        //            client.Send(msg);
        //            //// login User Email
        //            MailMessage ml = new MailMessage();
        //            msg.From = new MailAddress(ds.Tables[0].Rows[0]["Email_Address"].ToString());
        //            ml.To.Add(Host_Emp_EmailId);
        //            if (ServiceEmails != "")
        //            {
        //                ml.CC.Add(ServiceEmails);
        //            }
        //            ml.Subject = "You have scheduled visit for service " + ServiceName + " for " + FirstName + " " + LastName + " on " + txtValidFromDatetime.Text + ".";
        //            //msg.Body = body;
        //            ml.Body = "You are scheduled for service " + ServiceName + " for " + Session["UserID"] + " on " + txtValidFromDatetime.Text + ".";
        //            ml.IsBodyHtml = true;
        //            // decrypt parameters
        //            var decrptedInput1 = DecryptString(key, ds.Tables[0].Rows[0]["password"].ToString());
        //            client.Credentials = new NetworkCredential(ds.Tables[0].Rows[0]["Email_Address"].ToString(), decrptedInput1);
        //            client.Host = ds.Tables[0].Rows[0]["Outgoing_Services"].ToString();
        //            client.Port = Convert.ToInt32(ds.Tables[0].Rows[0]["SMTP"].ToString());
        //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            if (ds.Tables[0].Rows[0]["Connection"].ToString() == "1")
        //            {
        //                client.EnableSsl = true;
        //            }
        //            else
        //            {

        //            }
        //            if (ddlVisitor_Type.SelectedIndex == 0)
        //            {
        //                VisitorType = "";
        //            }
        //            if (ddlVisit_Reason.SelectedIndex == 0)
        //            {
        //                Reason = "";
        //            }
        //            Dictionary<String, String> replacements1 = new Dictionary<string, string>();
        //            replacements1.Add("recipient", Session["UserID"].ToString());
        //            replacements1.Add("ServiceName", ServiceName);
        //            replacements1.Add("ValidFrom", ValidFrom);
        //            replacements1.Add("VisitorName", FirstName + " " + LastName);
        //            replacements1.Add("VisitDate", ValidFrom);
        //            replacements1.Add("VisitType", VisitorType);
        //            replacements1.Add("Company", Company);
        //            replacements1.Add("VisitReason", Reason);
        //            replacements1.Add("code", code);
        //            //AlternateView plain = CreateAlternateViewFromTemplate("template1.txt", "text/plain", replacements);
        //            AlternateView html1 = CreateAlternateViewFromTemplate(Server.MapPath("EmailTemplate/Employee_Email.html"), "text/html", replacements1);
        //            Byte[] bitmapData1 = Convert.FromBase64String(FixBase64ForImage(QRimages));
        //            System.IO.MemoryStream streamBitmap1 = new System.IO.MemoryStream(bitmapData1);

        //            LinkedResource img2 = new LinkedResource(streamBitmap1, "image/jpeg");
        //            img1.ContentId = "img2";

        //            html.LinkedResources.Add(img2);
        //            //msg.AlternateViews.Add(plain);
        //            ml.AlternateViews.Add(html1);
        //            //client.UseDefaultCredentials = true;
        //            client.Send(ml);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.LogError(ex);
        //    }
        //}
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
    }
}