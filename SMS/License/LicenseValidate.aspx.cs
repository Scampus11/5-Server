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
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace SMS
{
    public partial class LicenseValidate : System.Web.UI.Page
    {
        LogFile logFile = new LogFile();
        String line = "";
        string Endpoint;
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (Session["UserName1"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    Endpoint = EncryptionDecryption.GetDecrypt(File.ReadAllText(Server.MapPath(@"\License\Server.txt")));

                    UpdateMAC();
                    string MACAddress = MAC();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("checklicense", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MAC", EncryptionDecryption.GetEncrypt(MACAddress));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Close();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        //txtCustomerId.Text = EncryptionDecryption.GetDecrypt(dt.Rows[0]["CustomerId"].ToString());
                    }
                    txtMACAddress.Text = MAC();
                    pnlMessage.Visible = false;
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    AdminCustomerMaster master = new AdminCustomerMaster()
                    {
                        //CustomerId = EncryptionDecryption.GetEncrypt(txtCustomerId.Text.Trim()),
                        LicenseKey = EncryptionDecryption.GetEncrypt(txtLicenseKey.Text.Trim()),
                        MAC = EncryptionDecryption.GetEncrypt(MAC()),
                        IsApproved = true
                    };

                    APIRequest aPIRequest = new APIRequest()
                    {
                        Data = master,
                        Token = "admin",
                        NextPageIndex = 1,
                    };
                    Endpoint = EncryptionDecryption.GetDecrypt(File.ReadAllText(Server.MapPath(@"\License\Server.txt")));

                    string jsonString = JsonConvert.SerializeObject(aPIRequest);
                    var jsoncontent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    string responseBodyAsText = string.Empty;
                    HttpMethod methodType = HttpMethod.Post;
                    string url = Endpoint + "/ValidateLicenseUserByKey";
                    string contents = string.Empty;
                    HttpClientHandler handler = new HttpClientHandler();
                    using (HttpClient httpClient = new HttpClient(handler))
                    {

                        HttpRequestMessage message = new HttpRequestMessage(methodType, url);
                        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", ApplicationConfiguration.Token.Replace("bearer", "").Trim());
                        httpClient.Timeout = new TimeSpan(0, 0, 10, 0, 0);
                        HttpResponseMessage responce = httpClient.PostAsync(url, jsoncontent).Result;
                        responce.EnsureSuccessStatusCode();
                        responseBodyAsText = responce.Content.ReadAsStringAsync().Result;
                    }
                    var result = JsonConvert.DeserializeObject<APIResponseData>(responseBodyAsText);
                    AdminCustomerMaster adminCustomerMaster = (AdminCustomerMaster)Newtonsoft.Json.JsonConvert.DeserializeObject(ConvertTo.String(result.Data), typeof(AdminCustomerMaster));
                    if (adminCustomerMaster != null)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UpdateRegistarionDetail", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerId", adminCustomerMaster.CustomerId);
                        cmd.Parameters.AddWithValue("@Name", adminCustomerMaster.Name);
                        cmd.Parameters.AddWithValue("@Organization", adminCustomerMaster.Organization);
                        cmd.Parameters.AddWithValue("@Email", adminCustomerMaster.Email);
                        cmd.Parameters.AddWithValue("@Contact", adminCustomerMaster.Contact);
                        cmd.Parameters.AddWithValue("@ContryCode", adminCustomerMaster.ContryCode);
                        cmd.Parameters.AddWithValue("@ProductName", adminCustomerMaster.ProductName);
                        cmd.Parameters.AddWithValue("@LicenseCreationDate", adminCustomerMaster.LicenseCreationDate);
                        cmd.Parameters.AddWithValue("@LicenseExprieyDate", adminCustomerMaster.LicenseExprieyDate);
                        cmd.Parameters.AddWithValue("@LicenseKey", adminCustomerMaster.LicenseKey);
                        cmd.Parameters.AddWithValue("@MAC", adminCustomerMaster.MAC);
                        cmd.Parameters.AddWithValue("@SoftwareRegistrationDate", adminCustomerMaster.SoftwareRegistrationDate);
                        cmd.Parameters.AddWithValue("@IsApproved", adminCustomerMaster.IsApproved);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        lblLicenseMessage.Visible = true;
                    }
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('Your license request is forwarded to the admin, Please wait for futher communication.')", true);

                    ValidateMAC();
                }

            }
            catch (Exception ex)
            {

            }

        }

        protected bool Validation()
        {
            if (txtLicenseKey.Text == "")
            {
                lblerrorusername.Visible = true;
                lblerrorpassword.Visible = false;
                return false;
            }
            else if (txtMACAddress.Text == "")
            {
                lblerrorusername.Visible = false;
                lblerrorpassword.Visible = true;
                return false;
            }

            return true;
        }

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
        private void DeleteDirectory(string path)
        {
            // Delete all files from the Directory  
            foreach (string filename in Directory.GetFiles(path))
            {
                File.Delete(filename);
            }
            // Check all child Directories and Delete files  
            foreach (string subfolder in Directory.GetDirectories(path))
            {
                DeleteDirectory(subfolder);
            }
            Directory.Delete(path);
            //   Deletelbl.Text = "Directory Deleted successfully";
        }


        private bool ValidateMAC()
        {
            try
            {
                string MACAddress = MAC();
                con.Open();

                SqlCommand cmd = new SqlCommand("checklicense", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MAC", EncryptionDecryption.GetEncrypt(MACAddress));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    //txtCustomerId.Text = EncryptionDecryption.GetDecrypt(dt.Rows[0]["CustomerId"].ToString());
                    if (dt.Rows[0]["IsApproved"].ToString().ToLower() == "false")
                    {
                        lblLicenseMessage.Visible = true;
                        return false;
                    }
                    if (dt.Rows[0]["MAC"].ToString() == "0")
                    {
                        lblLicenseMessage.Visible = true;
                    }
                    else
                    {
                        string Mac = EncryptionDecryption.GetDecrypt(dt.Rows[0]["MAC"].ToString());
                        string[] ArrMacDB = Mac.Split(',');
                        string[] ArrMacLive = MACAddress.Split(',');

                        string[] result = ArrMacDB.Intersect(ArrMacLive).ToArray();
                        string expiryDate = EncryptionDecryption.GetDecrypt(dt.Rows[0]["LicenseExprieyDate"].ToString());


                        if (expiryDate != "")
                        {
                            if (result.Count() > 0 && Convert.ToDateTime(expiryDate) >= System.DateTime.Today.Date)
                            {
                                Session["UserName"] = Session["UserName1"];
                                Session["Pwd"] = Session["Pwd1"];

                                if ((Convert.ToDateTime(expiryDate).Year - (System.DateTime.Today.Date.Year)) > 90)
                                {
                                    lblfinalMessage.Text = "Thank you for registering with us, your license subscribed with life time.";
                                }
                                else
                                {
                                    lblfinalMessage.Text = "Thank you for registering with us. Your license will expire on " + expiryDate;
                                }
                                pnlMessage.Visible = true;

                                btnSubmit.Visible = false;
                                btnBack.Visible = false;
                                btnLicenseKey.Visible = false;



                                //Response.Redirect("~/MainDashboard.aspx");
                                //lblLicenseMessage.Visible = false;
                            }
                            else
                            {
                                lblLicenseMessage.Visible = true;
                            }
                        }
                    }
                    //if (EncryptionDecryption.GetDecrypt(dt.Rows[0]["LicenseKey"].ToString()) != "" && EncryptionDecryption.GetDecrypt(dt.Rows[0]["CustomerId"].ToString()) != "")
                    //{
                    //    Response.Redirect("~/License/LicenseValidate.aspx");
                    //}
                    //else
                    //{
                    //    lblLicenseMessage.Visible = true;

                    //}
                }
                else
                {
                    lblLicenseMessage.Visible = true;

                }

            }
            catch (Exception ex)
            {

            }


            return false;
        }

        public string MAC()
        {
            string macAddresses = string.Empty;
            try
            {
                try
                {
                    foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                    {
                        if (nic.GetPhysicalAddress().ToString() != string.Empty)
                        {
                            if (macAddresses != string.Empty)
                            {
                                macAddresses = macAddresses + "," + nic.GetPhysicalAddress().ToString();
                            }
                            else
                            {
                                macAddresses = nic.GetPhysicalAddress().ToString();
                            }

                        }
                    }
                }
                catch
                {

                }
                if (macAddresses != string.Empty)
                {
                    if(getCustomMAC()!=string.Empty)
                    {
                        macAddresses = macAddresses + "," + getCustomMAC();
                    }
                    
                }
                else
                {
                    macAddresses = getCustomMAC();
                }

                if (macAddresses == string.Empty)
                {
                    UpdateCustomMAC();
                    MAC();
                }
                return macAddresses;
            }
            catch (Exception ex)
            {               
                return macAddresses;
            }
        }

        private string getCustomMAC()
        {
            string mac = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("getCustomMAC", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    mac = EncryptionDecryption.GetDecrypt(dt.Rows[0]["MAC"].ToString());
                }
                con.Close();
            }
            catch(Exception ex)
            {

            }
            return mac;

        }

        private void UpdateCustomMAC()
        {
            try
            {
                string MACAddress = LicenseKeyGenerator.GenerateCustomMAC(System.DateTime.Now.Ticks.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCustomMACAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MAC", EncryptionDecryption.GetEncrypt(MACAddress));
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateMAC()
        {
            try
            {
                string MACAddress = MAC();

                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateMACAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MAC", MACAddress);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btnLicenseKey_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/License/License.aspx");
        }
        protected void btnDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MainDashboard.aspx");
        }
    }
}