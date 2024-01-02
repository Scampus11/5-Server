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
using System.Security.Cryptography;
using System.Text;

namespace SMS
{
    public partial class SMS_Email_Configuration : System.Web.UI.Page
    {
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!IsPostBack)
            {
                try
                {
                    Email_Configuration();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }

            }
        }
        protected void Email_Configuration()
        {
            try
            {
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
                    txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtYourEmail.Text = ds.Tables[0].Rows[0]["Email_Address"].ToString();
                    ddlEncrypted.Items.FindByValue(ds.Tables[0].Rows[0]["Connection"].ToString()).Selected = true;
                    txtSMTP.Text = ds.Tables[0].Rows[0]["SMTP"].ToString();
                    txtOutgoingServerName.Text = ds.Tables[0].Rows[0]["Outgoing_Services"].ToString();
                    txtUserName.Text = ds.Tables[0].Rows[0]["User_Name"].ToString();
                    txtPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
                    txtpop3.Text = ds.Tables[0].Rows[0]["Pop3"].ToString();
                    txtincomingServer.Text = ds.Tables[0].Rows[0]["Incoming_Server"].ToString();
                    ddlEmailSender.Items.FindByValue(ds.Tables[0].Rows[0]["Email_Sender"].ToString()).Selected = true;
                    txtImapServerName.Text = ds.Tables[0].Rows[0]["Imap_Server_Name"].ToString();
                    txtimapAuthentication.Text = ds.Tables[0].Rows[0]["Imap_Authentication"].ToString();
                    txtImapPort.Text = ds.Tables[0].Rows[0]["imap_port"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
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
                Session["Sider"] = "Email configuration";
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
        protected bool Validation()
        {
            if (txtName.Text == "")
            {
                lblvalidname.Visible = true;
                lblEmailId.Visible = false;
                lblValidSMTP.Visible = false;
                lblValidOutgoingServerName.Visible = false;
                lblvalidpassword.Visible = false;
                return false;
            }
            if (txtYourEmail.Text == "")
            {
                lblvalidname.Visible = false;
                lblEmailId.Visible = true;
                lblValidSMTP.Visible = false;
                lblValidOutgoingServerName.Visible = false;
                lblvalidpassword.Visible = false;
                return false;
            }
            if (txtSMTP.Text == "")
            {
                lblvalidname.Visible = false;
                lblEmailId.Visible = false;
                lblValidSMTP.Visible = true;
                lblValidOutgoingServerName.Visible = false;
                lblvalidpassword.Visible = false;
                return false;
            }
            if (txtOutgoingServerName.Text == "")
            {
                lblvalidname.Visible = false;
                lblEmailId.Visible = false;
                lblValidSMTP.Visible = false;
                lblValidOutgoingServerName.Visible = true;
                lblvalidpassword.Visible = false;
                return false;
            }
            if (txtPassword.Text == "")
            {
                lblvalidname.Visible = false;
                lblvalidpassword.Visible = true;
                lblEmailId.Visible = false;
                lblValidSMTP.Visible = false;
                lblValidOutgoingServerName.Visible = false;
                return false;
            }

            return true;
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    // declaring key
                    var key = "b14ca5898a4e4133bbce2ea2315a1916";

                    // encrypt parameters
                    var input = string.Concat(EncryptString(key, txtPassword.Text));


                    SqlCommand cmd = new SqlCommand("SMS_SP_Get_Email_Configuration", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Update");
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email_Address", txtYourEmail.Text);
                    cmd.Parameters.AddWithValue("@Connection", ddlEncrypted.SelectedValue);
                    cmd.Parameters.AddWithValue("@SMTP", txtSMTP.Text);
                    cmd.Parameters.AddWithValue("@Outgoing_Services", txtOutgoingServerName.Text);
                    cmd.Parameters.AddWithValue("@User_Name", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@password", input);
                    cmd.Parameters.AddWithValue("@Pop3", txtpop3.Text);
                    cmd.Parameters.AddWithValue("@Incoming_Server", txtincomingServer.Text);
                    cmd.Parameters.AddWithValue("@Email_Sender", ddlEmailSender.SelectedValue);
                    cmd.Parameters.AddWithValue("@Imap_Server_Name", txtImapServerName.Text);
                    cmd.Parameters.AddWithValue("@Imap_Authentication", txtimapAuthentication.Text);
                    cmd.Parameters.AddWithValue("@imap_port", txtImapPort.Text);
                    cmd.Parameters.AddWithValue("@IsActive", 1);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //ModalPopupExtender2.Show();
                    divpopup.Attributes.Add("style", "display:block;");
                    lblmsg.Text = "Submit Successfully!!";
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        public static string EncryptString(string key, string plainInput)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainInput);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
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
        protected bool Validation1()
        {
            if (txttestEmail.Text == "")
            {
                lblvalidtestemail.Visible = true;
                return false;
            }

            return true;
        }
        protected void lnktest_Click(object sender, EventArgs e)
        {
            if (Validation1())
            {
                Email("Test Email", txttestEmail.Text);
            }
        }
        public void Email(string body, string Email)
        {
            try
            {
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
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(ds.Tables[0].Rows[0]["Email_Address"].ToString());
                    msg.To.Add(Email);
                    msg.Subject = "Test Email";
                    msg.Body = body;
                    msg.IsBodyHtml = true;
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
                    if (ds.Tables[0].Rows[0]["Outgoing_Services"].ToString() == "smtp.gmail.com")
                    { }
                    else {
                        //client.UseDefaultCredentials = false;
                    }

                    //var imageToInline = new LinkedResource("Your image full path", MediaTypeNames.Image.Jpeg);
                    //imageToInline.ContentId = "MyImage";
                    //msg.AlternateViews.Im
                    client.Send(msg);
                    //ModalPopupExtender2.Show();
                    divpopup.Attributes.Add("style", "display:block;");
                    lblmsg.Text = "Email sent Successfully!!";

                    //Byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage("Your base64 image string"));
                    //System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
                    //var mail = new MailMessage();
                    //var imageToInline = new LinkedResource(streamBitmap, MediaTypeNames.Image.Jpeg);
                    //FixBase64ForImage();
                    //imageToInline.ContentId = "MyImage";
                    //alternateView.LinkedResources.Add(imageToInline);
                    //mail.AlternateViews.Add(body);
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
                lblvalidtestemail.Visible = true;
                lblvalidtestemail.Text = ex.Message;
            }
        }
        public static string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }

        protected void lnkGo_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender2.Hide();
            divpopup.Attributes.Add("style", "display:none;");
            Response.Redirect("SMS_Email_Configuration.aspx");
        }
    }
}