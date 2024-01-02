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
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace SMS
{
    public partial class License : System.Web.UI.Page
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

                    CheckDBObject();
                    UpdateMAC();
                    BindDropdowncounty();
                    txtMACAddress.Text = MAC();
                    bool isConnected = IsConnectedToInternet();
                    if (isConnected)
                    {
                        if (CheckLiceseWithLive())
                        {
                            ValidateMAC();
                        }
                        else
                        {
                            lblLicenseMessage.Text = "Your license is not avaiable! Please Register here.";
                            lblLicenseMessage.Visible = true;
                        }
                    }
                    else
                    {
                        ValidateMAC();
                    }
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
                    if (txtCustomerId.Text == string.Empty)
                    {
                        txtCustomerId.Text = "0";
                    }

                    AdminCustomerMaster master = new AdminCustomerMaster()
                    {
                        CustomerId = EncryptionDecryption.GetEncrypt(txtCustomerId.Text.Trim()),
                        Name = EncryptionDecryption.GetEncrypt(txtName.Text.Trim()),
                        Organization = EncryptionDecryption.GetEncrypt(txtOrganization.Text.Trim()),
                        Email = EncryptionDecryption.GetEncrypt(txtEmail.Text.Trim()),
                        Contact = EncryptionDecryption.GetEncrypt(txtContact.Text.Trim()),
                        ContryCode = EncryptionDecryption.GetEncrypt(ddlcountry.SelectedValue.Trim()),
                        MAC = EncryptionDecryption.GetEncrypt(MAC()),
                        IsApproved = true,
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
                    string url = Endpoint + "/RegisterNewUser";
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
                        SqlCommand cmd = new SqlCommand("UpdateRegistarionDetailNew", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerId", adminCustomerMaster.CustomerId);
                        cmd.Parameters.AddWithValue("@Name", adminCustomerMaster.Name);
                        cmd.Parameters.AddWithValue("@Organization", adminCustomerMaster.Organization);
                        cmd.Parameters.AddWithValue("@Email", adminCustomerMaster.Email);
                        cmd.Parameters.AddWithValue("@Contact", adminCustomerMaster.Contact);
                        cmd.Parameters.AddWithValue("@ContryCode", adminCustomerMaster.ContryCode);
                        cmd.Parameters.AddWithValue("@ProductName", adminCustomerMaster.ProductName);
                        cmd.Parameters.AddWithValue("@LicenseCreationDate", adminCustomerMaster.LicenseCreationDate);
                        //cmd.Parameters.AddWithValue("@LicenseExprieyDate", adminCustomerMaster.LicenseExprieyDate);
                        //cmd.Parameters.AddWithValue("@LicenseKey", adminCustomerMaster.LicenseKey);
                        //cmd.Parameters.AddWithValue("@LicenseExprieyDate", "");
                        //cmd.Parameters.AddWithValue("@LicenseKey", "");
                        cmd.Parameters.AddWithValue("@MAC", adminCustomerMaster.MAC);
                        cmd.Parameters.AddWithValue("@SoftwareRegistrationDate", adminCustomerMaster.SoftwareRegistrationDate);
                        cmd.Parameters.AddWithValue("@IsApproved", adminCustomerMaster.IsApproved);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('Your license request is forwarded to the admin, Please wait for futher communication.')", true);
                    txtCustomerId.Text = EncryptionDecryption.GetDecrypt(adminCustomerMaster.CustomerId);
                    lblEnterName.Visible = false;
                    lblOrganization.Visible = false;
                    lblEmail.Visible = false;
                    lblContact.Visible = false;
                    lblLicenseMessage.Visible = false;


                    //ValidateMAC();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected bool Validation()
        {
            lblLicenseMessage.Visible = false;
            if (txtName.Text == "")
            {
                lblEnterName.Visible = true;
                lblOrganization.Visible = false;
                lblEmail.Visible = false;
                lblContact.Visible = false;
                return false;
            }
            else if (txtOrganization.Text == "")
            {
                lblEnterName.Visible = false;
                lblOrganization.Visible = true;
                lblEmail.Visible = false;
                lblContact.Visible = false;
                return false;
            }
            else if (!ValidateEmail())
            {
                lblEnterName.Visible = false;
                lblOrganization.Visible = false;
                lblEmail.Visible = true;
                lblContact.Visible = false;
                lblEmail.Text = "* Please enter Valid Email Address";
                return false;
            }
            else if (txtEmail.Text == "")
            {

                lblEnterName.Visible = false;
                lblOrganization.Visible = false;
                lblEmail.Text = "* Enter Email Id";
                lblEmail.Visible = true;
                lblContact.Visible = false;
                return false;
            }
            else if (txtContact.Text == "")
            {
                lblEnterName.Visible = false;
                lblOrganization.Visible = false;
                lblEmail.Visible = false;
                lblContact.Visible = true;
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
                Response.Redirect("~/SMS_SQL_Connection.aspx");
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

                    txtCustomerId.Text = EncryptionDecryption.GetDecrypt(dt.Rows[0]["CustomerId"].ToString());
                    txtName.Text = EncryptionDecryption.GetDecrypt(dt.Rows[0]["Name"].ToString());
                    txtOrganization.Text = EncryptionDecryption.GetDecrypt(dt.Rows[0]["Organization"].ToString());
                    txtEmail.Text = EncryptionDecryption.GetDecrypt(dt.Rows[0]["Email"].ToString());

                    //ddlcountry.ClearSelection();
                    //ddlcountry.Items.FindByValue(EncryptionDecryption.GetDecrypt(dt.Rows[0]["ContryCode"].ToString())).Selected = true;
                    ddlcountry.SelectedValue = EncryptionDecryption.GetDecrypt(dt.Rows[0]["ContryCode"].ToString());

                    txtContact.Text = EncryptionDecryption.GetDecrypt(dt.Rows[0]["Contact"].ToString());

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
                            if (result.Count() > 0 && Convert.ToDateTime(expiryDate) >= System.DateTime.Today.Date &&  EncryptionDecryption.GetDecrypt(dt.Rows[0]["CustomerId"].ToString()) != "")
                            {
                                if(EncryptionDecryption.GetDecrypt(dt.Rows[0]["LicenseKey"].ToString()) == "" )
                                {
                                    lblLicenseMessage.Text = "Your license key is not avaiable! Please contact to administrator.";
                                    lblLicenseMessage.Visible = true;
                                    return false;
                                }
                                Session["UserName"] = Session["UserName1"];
                                Session["Pwd"] = Session["Pwd1"];
                                lblLicenseMessage.Visible = false;
                                Response.Redirect("~/MainDashboard.aspx");
                            }
                            else
                            {
                                lblLicenseMessage.Text = "Your license has been expired! Please contact to administrator.";
                                lblLicenseMessage.Visible = true;
                            }
                        }
                        else
                        {
                            lblLicenseMessage.Text = "Your license key is not avaiable! Please contact to administrator.";
                            lblLicenseMessage.Visible = true;
                        }

                    }

                    //if (EncryptionDecryption.GetDecrypt(dt.Rows[0]["LicenseKey"].ToString()) != "" && EncryptionDecryption.GetDecrypt(dt.Rows[0]["CustomerId"].ToString()) != "")
                    //{
                    //    Response.Redirect("~/License/LicenseValidate.aspx");
                    //}
                }
                else
                {
                    lblLicenseMessage.Text = "Your license is not avaiable! Please contact to administrator.";
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
                    if (getCustomMAC() != string.Empty)
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
            catch
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
            catch
            {

            }
        }

        private void UpdateMAC()
        {
            try
            {
                string MACAddress = EncryptionDecryption.GetEncrypt(MAC());

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
            Response.Redirect("~/License/LicenseValidate.aspx");
        }

        public bool IsConnectedToInternet()
        {
            try
            {
                int timeoutMs = 3000;

                string url = "http://www.gstatic.com/generate_204";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckLiceseWithLive()
        {
            try
            {
                string MACAddress = MAC();
                con.Open();

                SqlCommand cmd1 = new SqlCommand("checklicense", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@MAC", EncryptionDecryption.GetEncrypt(MACAddress));
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    txtCustomerId.Text = EncryptionDecryption.GetDecrypt(dt.Rows[0]["CustomerId"].ToString());
                    AdminCustomerMaster master = new AdminCustomerMaster()
                    {
                        LicenseKey = dt.Rows[0]["LicenseKey"].ToString(),
                        CustomerId = dt.Rows[0]["CustomerId"].ToString(),
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
                    string url = Endpoint + "/ValidateLicenseUser";
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
                        SqlCommand cmd = new SqlCommand("UpdateRegistarionDetailCheckWithLive", con);
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
                        //cmd.Parameters.AddWithValue("@LicenseKey", adminCustomerMaster.LicenseKey);
                        cmd.Parameters.AddWithValue("@MAC", adminCustomerMaster.MAC);
                        cmd.Parameters.AddWithValue("@SoftwareRegistrationDate", adminCustomerMaster.SoftwareRegistrationDate);
                        cmd.Parameters.AddWithValue("@IsApproved", adminCustomerMaster.IsApproved);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public void BindDropdowncounty()
        {
            List<ListItem> CountryList = new List<ListItem>();
            CountryList.Add(new ListItem { Text = "United Arab Emirates(ARE) +971", Value = "+971" });
            CountryList.Add(new ListItem { Text = "Afghanistan(AFG) +93", Value = "+93" });
            CountryList.Add(new ListItem { Text = "Albania(ALB) +355", Value = "+355" });
            CountryList.Add(new ListItem { Text = "Algeria(DZA) +213", Value = "+213" });
            CountryList.Add(new ListItem { Text = "American Samoa(ASM) +684", Value = "+684" });
            CountryList.Add(new ListItem { Text = "Andorra(AND) +376", Value = "+376" });
            CountryList.Add(new ListItem { Text = "Angola(AGO) +244", Value = "+244" });
            CountryList.Add(new ListItem { Text = "Anguilla(AIA) +1-264", Value = "+1-264" });
            CountryList.Add(new ListItem { Text = "Antarctica(ATA) +672", Value = "+672" });
            CountryList.Add(new ListItem { Text = "Antigua and Barbuda(ATG) +1-268", Value = "+1-268" });
            CountryList.Add(new ListItem { Text = "Argentina(ARG) +54", Value = "+54" });
            CountryList.Add(new ListItem { Text = "Armenia(ARM) +374", Value = "+374" });
            CountryList.Add(new ListItem { Text = "Aruba(ABW) +297", Value = "+297" });
            CountryList.Add(new ListItem { Text = "Australia(AUS) +61", Value = "+61" });
            CountryList.Add(new ListItem { Text = "Austria(AUT) +43", Value = "+43" });
            CountryList.Add(new ListItem { Text = "Azerbaijan(AZE) +994", Value = "+994" });
            CountryList.Add(new ListItem { Text = "Bahamas(BHS) +1-242", Value = "+1-242" });
            CountryList.Add(new ListItem { Text = "Bahrain(BHR) +973", Value = "+973" });
            CountryList.Add(new ListItem { Text = "Bangladesh(BGD) +880", Value = "+880" });
            CountryList.Add(new ListItem { Text = "Barbados(BRB) +1-246", Value = "+1-246" });
            CountryList.Add(new ListItem { Text = "Belarus(BLR) +375", Value = "+375" });
            CountryList.Add(new ListItem { Text = "Belgium(BEL) +32", Value = "+32" });
            CountryList.Add(new ListItem { Text = "Belize(BLZ) +501", Value = "+501" });
            CountryList.Add(new ListItem { Text = "Benin(BEN) +229", Value = "+229" });
            CountryList.Add(new ListItem { Text = "Bermuda(BMU) +1-441", Value = "+1-441" });
            CountryList.Add(new ListItem { Text = "Bhutan(BTN) +975", Value = "+975" });
            CountryList.Add(new ListItem { Text = "Bolivia(BOL) +591", Value = "+591" });
            CountryList.Add(new ListItem { Text = "Bosnia-Herzegovina(BIH) +387", Value = "+387" });
            CountryList.Add(new ListItem { Text = "Botswana(BWA) +267", Value = "+267" });
            CountryList.Add(new ListItem { Text = "Brazil(BRA) +55", Value = "+55" });
            CountryList.Add(new ListItem { Text = "Brunei Darussalam(BRN) +673", Value = "+673" });
            CountryList.Add(new ListItem { Text = "Bulgaria(BGR) +359", Value = "+359" });
            CountryList.Add(new ListItem { Text = "Burkina Faso(BFA) +226", Value = "+226" });
            CountryList.Add(new ListItem { Text = "Burundi(BDI) +257", Value = "+257" });
            CountryList.Add(new ListItem { Text = "Cabo Verde(CPV) +238", Value = "+238" });
            CountryList.Add(new ListItem { Text = "Cambodia(KHM) +855", Value = "+855" });
            CountryList.Add(new ListItem { Text = "Cameroon(CMR) +237", Value = "+237" });
            CountryList.Add(new ListItem { Text = "Canada(CAN) +1", Value = "+1" });
            CountryList.Add(new ListItem { Text = "Cayman Islands(CYM) +1-345", Value = "+1-345" });
            CountryList.Add(new ListItem { Text = "Central African Republic(CAF) +236", Value = "+236" });
            CountryList.Add(new ListItem { Text = "Chad(TCD) +235", Value = "+235" });
            CountryList.Add(new ListItem { Text = "Chile(CHL) +56", Value = "+56" });
            CountryList.Add(new ListItem { Text = "China(CHN) +86", Value = "+86" });
            CountryList.Add(new ListItem { Text = "Christmas Island(CXR) +61", Value = "+61" });
            CountryList.Add(new ListItem { Text = "Cocos (Keeling) Islands(CCK) +61", Value = "+61" });
            CountryList.Add(new ListItem { Text = "Colombia(COL) +57", Value = "+57" });
            CountryList.Add(new ListItem { Text = "Comoros(COM) +269", Value = "+269" });
            CountryList.Add(new ListItem { Text = "Congo(COG) +242", Value = "+242" });
            CountryList.Add(new ListItem { Text = "Congo, Dem. Republic(COD) +243", Value = "+243" });
            CountryList.Add(new ListItem { Text = "Cook Islands(COK) +682", Value = "+682" });
            CountryList.Add(new ListItem { Text = "Costa Rica(CRI) +506", Value = "+506" });
            CountryList.Add(new ListItem { Text = "Croatia(HRV) +385", Value = "+385" });
            CountryList.Add(new ListItem { Text = "Cuba(CUB) +53", Value = "+53" });
            CountryList.Add(new ListItem { Text = "Curaçao(CUW) +5999", Value = "+5999" });
            CountryList.Add(new ListItem { Text = "Cyprus(CYP) +357", Value = "+357" });
            CountryList.Add(new ListItem { Text = "Czechia(CZE) +420", Value = "+420" });
            CountryList.Add(new ListItem { Text = "Côte d'Ivoire(CIV) +225", Value = "+225" });
            CountryList.Add(new ListItem { Text = "Denmark(DNK) +45", Value = "+45" });
            CountryList.Add(new ListItem { Text = "Djibouti(DJI) +253", Value = "+253" });
            CountryList.Add(new ListItem { Text = "Dominica(DMA) +1-767", Value = "+1-767" });
            CountryList.Add(new ListItem { Text = "Dominican Republic(DOM) +809", Value = "+809" });
            CountryList.Add(new ListItem { Text = "Ecuador(ECU) +593", Value = "+593" });
            CountryList.Add(new ListItem { Text = "Egypt(EGY) +20", Value = "+20" });
            CountryList.Add(new ListItem { Text = "El Salvador(SLV) +503", Value = "+503" });
            CountryList.Add(new ListItem { Text = "Equatorial Guinea(GNQ) +240", Value = "+240" });
            CountryList.Add(new ListItem { Text = "Eritrea(ERI) +291", Value = "+291" });
            CountryList.Add(new ListItem { Text = "Estonia(EST) +372", Value = "+372" });
            CountryList.Add(new ListItem { Text = "Eswatini(SWZ) +268", Value = "+268" });
            CountryList.Add(new ListItem { Text = "Ethiopia(ETH) +251", Value = "+251" });
            CountryList.Add(new ListItem { Text = "Falkland Islands (Malvinas)(FLK) +500", Value = "+500" });
            CountryList.Add(new ListItem { Text = "Faroe Islands(FRO) +298", Value = "+298" });
            CountryList.Add(new ListItem { Text = "Fiji(FJI) +679", Value = "+679" });
            CountryList.Add(new ListItem { Text = "Finland(FIN) +358", Value = "+358" });
            CountryList.Add(new ListItem { Text = "France(FRA) +33", Value = "+33" });
            CountryList.Add(new ListItem { Text = "French Guiana(GUF) +594", Value = "+594" });
            CountryList.Add(new ListItem { Text = "French Polynesia(PYF) +689", Value = "+689" });
            CountryList.Add(new ListItem { Text = "Gabon(GAB) +241", Value = "+241" });
            CountryList.Add(new ListItem { Text = "Gambia(GMB) +220", Value = "+220" });
            CountryList.Add(new ListItem { Text = "Georgia(GEO) +995", Value = "+995" });
            CountryList.Add(new ListItem { Text = "Germany(DEU) +49", Value = "+49" });
            CountryList.Add(new ListItem { Text = "Ghana(GHA) +233", Value = "+233" });
            CountryList.Add(new ListItem { Text = "Gibraltar(GIB) +350", Value = "+350" });
            CountryList.Add(new ListItem { Text = "Greece(GRC) +30", Value = "+30" });
            CountryList.Add(new ListItem { Text = "Greenland(GRL) +299", Value = "+299" });
            CountryList.Add(new ListItem { Text = "Grenada(GRD) +1-473", Value = "+1-473" });
            CountryList.Add(new ListItem { Text = "Guadeloupe (French)(GLP) +590", Value = "+590" });
            CountryList.Add(new ListItem { Text = "Guam (USA)(GUM) +1-671", Value = "+1-671" });
            CountryList.Add(new ListItem { Text = "Guatemala(GTM) +502", Value = "+502" });
            CountryList.Add(new ListItem { Text = "Guinea(GIN) +224", Value = "+224" });
            CountryList.Add(new ListItem { Text = "Guinea Bissau(GNB) +245", Value = "+245" });
            CountryList.Add(new ListItem { Text = "Guyana(GUY) +592", Value = "+592" });
            CountryList.Add(new ListItem { Text = "Haiti(HTI) +509", Value = "+509" });
            CountryList.Add(new ListItem { Text = "Holy See(VAT) +39", Value = "+39" });
            CountryList.Add(new ListItem { Text = "Honduras(HND) +504", Value = "+504" });
            CountryList.Add(new ListItem { Text = "Hong Kong(HKG) +852", Value = "+852" });
            CountryList.Add(new ListItem { Text = "Hungary(HUN) +36", Value = "+36" });
            CountryList.Add(new ListItem { Text = "Iceland(ISL) +354", Value = "+354" });
            CountryList.Add(new ListItem { Text = "India(IND) +91", Value = "+91" });
            CountryList.Add(new ListItem { Text = "Indonesia(IDN) +62", Value = "+62" });
            CountryList.Add(new ListItem { Text = "Iran(IRN) +98", Value = "+98" });
            CountryList.Add(new ListItem { Text = "Iraq(IRQ) +964", Value = "+964" });
            CountryList.Add(new ListItem { Text = "Ireland(IRL) +353", Value = "+353" });
            CountryList.Add(new ListItem { Text = "Israel(ISR) +972", Value = "+972" });
            CountryList.Add(new ListItem { Text = "Italy(ITA) +39", Value = "+39" });
            CountryList.Add(new ListItem { Text = "Jamaica(JAM) +1-876", Value = "+1-876" });
            CountryList.Add(new ListItem { Text = "Japan(JPN) +81", Value = "+81" });
            CountryList.Add(new ListItem { Text = "Jordan(JOR) +962", Value = "+962" });
            CountryList.Add(new ListItem { Text = "Kazakhstan(KAZ) +7", Value = "+7" });
            CountryList.Add(new ListItem { Text = "Kenya(KEN) +254", Value = "+254" });
            CountryList.Add(new ListItem { Text = "Kiribati(KIR) +686", Value = "+686" });
            CountryList.Add(new ListItem { Text = "Korea-North(PRK) +850", Value = "+850" });
            CountryList.Add(new ListItem { Text = "Korea-South(KOR) +82", Value = "+82" });
            CountryList.Add(new ListItem { Text = "Kuwait(KWT) +965", Value = "+965" });
            CountryList.Add(new ListItem { Text = "Kyrgyzstan(KGZ) +996", Value = "+996" });
            CountryList.Add(new ListItem { Text = "Laos(LAO) +856", Value = "+856" });
            CountryList.Add(new ListItem { Text = "Latvia(LVA) +371", Value = "+371" });
            CountryList.Add(new ListItem { Text = "Lebanon(LBN) +961", Value = "+961" });
            CountryList.Add(new ListItem { Text = "Lesotho(LSO) +266", Value = "+266" });
            CountryList.Add(new ListItem { Text = "Liberia(LBR) +231", Value = "+231" });
            CountryList.Add(new ListItem { Text = "Libya(LBY) +218", Value = "+218" });
            CountryList.Add(new ListItem { Text = "Liechtenstein(LIE) +423", Value = "+423" });
            CountryList.Add(new ListItem { Text = "Lithuania(LTU) +370", Value = "+370" });
            CountryList.Add(new ListItem { Text = "Luxembourg(LUX) +352", Value = "+352" });
            CountryList.Add(new ListItem { Text = "Macao(MAC) +853", Value = "+853" });
            CountryList.Add(new ListItem { Text = "Madagascar(MDG) +261", Value = "+261" });
            CountryList.Add(new ListItem { Text = "Malawi(MWI) +265", Value = "+265" });
            CountryList.Add(new ListItem { Text = "Malaysia(MYS) +60", Value = "+60" });
            CountryList.Add(new ListItem { Text = "Maldives(MDV) +960", Value = "+960" });
            CountryList.Add(new ListItem { Text = "Mali(MLI) +223", Value = "+223" });
            CountryList.Add(new ListItem { Text = "Malta(MLT) +356", Value = "+356" });
            CountryList.Add(new ListItem { Text = "Marshall Islands(MHL) +692", Value = "+692" });
            CountryList.Add(new ListItem { Text = "Martinique (French)(MTQ) +596", Value = "+596" });
            CountryList.Add(new ListItem { Text = "Mauritania(MRT) +222", Value = "+222" });
            CountryList.Add(new ListItem { Text = "Mauritius(MUS) +230", Value = "+230" });
            CountryList.Add(new ListItem { Text = "Mayotte(MYT) +269", Value = "+269" });
            CountryList.Add(new ListItem { Text = "Mexico(MEX) +52", Value = "+52" });
            CountryList.Add(new ListItem { Text = "Micronesia(FSM) +691", Value = "+691" });
            CountryList.Add(new ListItem { Text = "Moldova(MDA) +373", Value = "+373" });
            CountryList.Add(new ListItem { Text = "Monaco(MCO) +377", Value = "+377" });
            CountryList.Add(new ListItem { Text = "Mongolia(MNG) +976", Value = "+976" });
            CountryList.Add(new ListItem { Text = "Montenegro(MNE) +382", Value = "+382" });
            CountryList.Add(new ListItem { Text = "Montserrat(MSR) +1-664", Value = "+1-664" });
            CountryList.Add(new ListItem { Text = "Morocco(MAR) +212", Value = "+212" });
            CountryList.Add(new ListItem { Text = "Mozambique(MOZ) +258", Value = "+258" });
            CountryList.Add(new ListItem { Text = "Myanmar(MMR) +95", Value = "+95" });
            CountryList.Add(new ListItem { Text = "Namibia(NAM) +264", Value = "+264" });
            CountryList.Add(new ListItem { Text = "Nauru(NRU) +674", Value = "+674" });
            CountryList.Add(new ListItem { Text = "Nepal(NPL) +977", Value = "+977" });
            CountryList.Add(new ListItem { Text = "Netherlands(NLD) +31", Value = "+31" });
            CountryList.Add(new ListItem { Text = "New Caledonia (French)(NCL) +687", Value = "+687" });
            CountryList.Add(new ListItem { Text = "New Zealand(NZL) +64", Value = "+64" });
            CountryList.Add(new ListItem { Text = "Nicaragua(NIC) +505", Value = "+505" });
            CountryList.Add(new ListItem { Text = "Niger(NER) +227", Value = "+227" });
            CountryList.Add(new ListItem { Text = "Nigeria(NGA) +234", Value = "+234" });
            CountryList.Add(new ListItem { Text = "Niue(NIU) +683", Value = "+683" });
            CountryList.Add(new ListItem { Text = "Norfolk Island(NFK) +672", Value = "+672" });
            CountryList.Add(new ListItem { Text = "North Macedonia(MKD) +389", Value = "+389" });
            CountryList.Add(new ListItem { Text = "Northern Mariana Islands(MNP) +670", Value = "+670" });
            CountryList.Add(new ListItem { Text = "Norway(NOR) +47", Value = "+47" });
            CountryList.Add(new ListItem { Text = "Oman(OMN) +968", Value = "+968" });
            CountryList.Add(new ListItem { Text = "Pakistan(PAK) +92", Value = "+92" });
            CountryList.Add(new ListItem { Text = "Palau(PLW) +680", Value = "+680" });
            CountryList.Add(new ListItem { Text = "Panama(PAN) +507", Value = "+507" });
            CountryList.Add(new ListItem { Text = "Papua New Guinea(PNG) +675", Value = "+675" });
            CountryList.Add(new ListItem { Text = "Paraguay(PRY) +595", Value = "+595" });
            CountryList.Add(new ListItem { Text = "Peru(PER) +51", Value = "+51" });
            CountryList.Add(new ListItem { Text = "Philippines(PHL) +63", Value = "+63" });
            CountryList.Add(new ListItem { Text = "Poland(POL) +48", Value = "+48" });
            CountryList.Add(new ListItem { Text = "Portugal(PRT) +351", Value = "+351" });
            CountryList.Add(new ListItem { Text = "Puerto Rico(PRI) +1-787", Value = "+1-787" });
            CountryList.Add(new ListItem { Text = "Qatar(QAT) +974", Value = "+974" });
            CountryList.Add(new ListItem { Text = "Reunion (French)(REU) +262", Value = "+262" });
            CountryList.Add(new ListItem { Text = "Romania(ROU) +40", Value = "+40" });
            CountryList.Add(new ListItem { Text = "Russia(RUS) +7", Value = "+7" });
            CountryList.Add(new ListItem { Text = "Rwanda(RWA) +250", Value = "+250" });
            CountryList.Add(new ListItem { Text = "Saint Helena(SHN) +290", Value = "+290" });
            CountryList.Add(new ListItem { Text = "Saint Kitts & Nevis Anguilla(KNA) +1-869", Value = "+1-869" });
            CountryList.Add(new ListItem { Text = "Saint Lucia(LCA) +1-758", Value = "+1-758" });
            CountryList.Add(new ListItem { Text = "Saint Pierre and Miquelon(SPM) +508", Value = "+508" });
            CountryList.Add(new ListItem { Text = "Saint Vincent & Grenadines(VCT) +1-784", Value = "+1-784" });
            CountryList.Add(new ListItem { Text = "Samoa(WSM) +684", Value = "+684" });
            CountryList.Add(new ListItem { Text = "San Marino(SMR) +378", Value = "+378" });
            CountryList.Add(new ListItem { Text = "Sao Tome and Principe(STP) +239", Value = "+239" });
            CountryList.Add(new ListItem { Text = "Saudi Arabia(SAU) +966", Value = "+966" });
            CountryList.Add(new ListItem { Text = "Senegal(SEN) +221", Value = "+221" });
            CountryList.Add(new ListItem { Text = "Serbia(SRB) +381", Value = "+381" });
            CountryList.Add(new ListItem { Text = "Seychelles(SYC) +248", Value = "+248" });
            CountryList.Add(new ListItem { Text = "Sierra Leone(SLE) +232", Value = "+232" });
            CountryList.Add(new ListItem { Text = "Singapore(SGP) +65", Value = "+65" });
            CountryList.Add(new ListItem { Text = "Slovakia(SVK) +421", Value = "+421" });
            CountryList.Add(new ListItem { Text = "Slovenia(SVN) +386", Value = "+386" });
            CountryList.Add(new ListItem { Text = "Solomon Islands(SLB) +677", Value = "+677" });
            CountryList.Add(new ListItem { Text = "Somalia(SOM) +252", Value = "+252" });
            CountryList.Add(new ListItem { Text = "South Africa(ZAF) +27", Value = "+27" });
            CountryList.Add(new ListItem { Text = "Spain(ESP) +34", Value = "+34" });
            CountryList.Add(new ListItem { Text = "Sri Lanka(LKA) +94", Value = "+94" });
            CountryList.Add(new ListItem { Text = "Sudan(SDN) +249", Value = "+249" });
            CountryList.Add(new ListItem { Text = "Suriname(SUR) +597", Value = "+597" });
            CountryList.Add(new ListItem { Text = "Sweden(SWE) +46", Value = "+46" });
            CountryList.Add(new ListItem { Text = "Switzerland(CHE) +41", Value = "+41" });
            CountryList.Add(new ListItem { Text = "Syria(SYR) +963", Value = "+963" });
            CountryList.Add(new ListItem { Text = "Taiwan(TWN) +886", Value = "+886" });
            CountryList.Add(new ListItem { Text = "Tajikistan(TJK) +992", Value = "+992" });
            CountryList.Add(new ListItem { Text = "Tanzania(TZA) +255", Value = "+255" });
            CountryList.Add(new ListItem { Text = "Thailand(THA) +66", Value = "+66" });
            CountryList.Add(new ListItem { Text = "Togo(TGO) +228", Value = "+228" });
            CountryList.Add(new ListItem { Text = "Tokelau(TKL) +690", Value = "+690" });
            CountryList.Add(new ListItem { Text = "Tonga(TON) +676", Value = "+676" });
            CountryList.Add(new ListItem { Text = "Trinidad and Tobago(TTO) +1-868", Value = "+1-868" });
            CountryList.Add(new ListItem { Text = "Tunisia(TUN) +216", Value = "+216" });
            CountryList.Add(new ListItem { Text = "Turkey(TUR) +90", Value = "+90" });
            CountryList.Add(new ListItem { Text = "Turkmenistan(TKM) +993", Value = "+993" });
            CountryList.Add(new ListItem { Text = "Turks and Caicos Islands(TCA) +1-649", Value = "+1-649" });
            CountryList.Add(new ListItem { Text = "Tuvalu(TUV) +688", Value = "+688" });
            CountryList.Add(new ListItem { Text = "USA(USA) +1", Value = "+1" });
            CountryList.Add(new ListItem { Text = "Uganda(UGA) +256", Value = "+256" });
            CountryList.Add(new ListItem { Text = "Ukraine(UKR) +380", Value = "+380" });

            CountryList.Add(new ListItem { Text = "United Kingdom(GBR) +44", Value = "+44" });
            CountryList.Add(new ListItem { Text = "Uruguay(URY) +598", Value = "+598" });
            CountryList.Add(new ListItem { Text = "Uzbekistan(UZB) +998", Value = "+998" });
            CountryList.Add(new ListItem { Text = "Vanuatu(VUT) +678", Value = "+678" });
            CountryList.Add(new ListItem { Text = "Venezuela(VEN) +58", Value = "+58" });
            CountryList.Add(new ListItem { Text = "Vietnam(VNM) +84", Value = "+84" });
            CountryList.Add(new ListItem { Text = "Virgin Islands (British)(VGB) +1-284", Value = "+1-284" });
            CountryList.Add(new ListItem { Text = "Virgin Islands (USA)(VIR) +1-340", Value = "+1-340" });
            CountryList.Add(new ListItem { Text = "Wallis and Futuna Islands(WLF) +681", Value = "+681" });
            CountryList.Add(new ListItem { Text = "Yemen(YEM) +967", Value = "+967" });
            CountryList.Add(new ListItem { Text = "Zambia(ZMB) +260", Value = "+260" });
            CountryList.Add(new ListItem { Text = "Zimbabwe(ZWE) +263", Value = "+263" });

            ddlcountry.DataSource = CountryList;
            ddlcountry.DataTextField = "Text";
            ddlcountry.DataValueField = "Value";
            ddlcountry.DataBind();


        }

        private bool ValidateEmail()
        {
            string email = txtEmail.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                return false;
            }
            return true;
        }

        protected void CheckDBObject()
        {
            try
            {
                if (!CheckTableExist())
                {
                    string path = Server.MapPath(@"\License\script.sql");

                    con.Open();
                    string script = File.ReadAllText(path);

                    //split the script on "GO" commands
                    string[] splitter = new string[] { "\r\nGO\r\n" };
                    string[] commandTexts = script.Split(splitter,
                      StringSplitOptions.RemoveEmptyEntries);
                    foreach (string commandText in commandTexts)
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand(commandText, con);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {

                        }
                    }
                    con.Close();
                }
            }
            catch
            {

            }
        }
        protected bool CheckTableExist()
        {

            string tableName = "AdminCustomerMaster";
            try
            {
                con.Open();
                string script = "select case when exists((select * from information_schema.tables where table_name = '" + tableName + "')) then 1 else 0 end";
                SqlCommand cmd = new SqlCommand(script, con);
                int x = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (x == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                con.Close();
                return false;

            }
        }
    }
}