using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;

namespace SMS
{
    public partial class Page_Not_Access : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //            var xml = @"  
            //<NETBOX-API>
            // <COMMAND name='Login' num='1' dateformat='tzoffset'>
            // <PARAMS>
            // <USERNAME> admin </USERNAME>
            // <PASSWORD> admin </PASSWORD>
            // </PARAMS>
            // </COMMAND>
            //</NETBOX-API> ";
            //            XmlDocument doc = new XmlDocument();
            //            doc.LoadXml(xml);
            //            var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
            //var url = "https://api.beta.shipwire.com/exec/TrackingServices.php";

            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            //    HttpResponseMessage response = client.GetAsync(url).Result;
            //    //HttpRequestMessage requestmsg = client.GetStreamAsync(url).ToString();
            //    if (response.StatusCode == HttpStatusCode.OK)
            //    {
            //        XDocument xdoc = XDocument.Parse(response.Content.ReadAsStringAsync().Result);

            //        StringReader sr = new StringReader(xdoc.ToString());

            //        //DataSet ds = new DataSet();

            //        //ds.ReadXml(sr);

            //        //GridView1.DataSource = ds.Tables[0];
            //        //GridView1.DataBind();
            //    }

            //}
            //    string responsemessage = "";
            //    var url = WebConfigurationManager.AppSettings["APIURL"].ToString();
            //    //"https://api.beta.shipwire.com/exec/TrackingServices.php";
            //    var xml = @"  
            //    <NETBOX-API>
            //     <COMMAND name='Login' num='1' dateformat='tzoffset'>
            //     <PARAMS>
            //     <USERNAME> '+WebConfigurationManager.AppSettings['APIURL'].ToString()+' </USERNAME>
            //     < PASSWORD> '+WebConfigurationManager.AppSettings['APIURL'].ToString()+' </PASSWORD>
            //     </PARAMS>
            //     </COMMAND>
            //    </NETBOX-API> ";
            //    XmlDocument doc = new XmlDocument();
            //    doc.LoadXml(xml);
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //    try
            //    {
            //        request.Method = "POST";
            //        request.ContentType = "application/XML";
            //        //request.Headers.Add("Authorization",Autho);
            //        StreamWriter requestwriter = new StreamWriter(request.GetRequestStream());
            //        requestwriter.Write(xml);
            //        requestwriter.Close();
            //        StreamReader responsereader = new StreamReader(request.GetResponse().GetResponseStream());
            //        responsemessage = responsereader.ReadToEnd();
            //        responsereader.Close();
            //        request.GetResponse().Close();
            //    }
            //    catch(WebException ex)
            //    {
            //        WebResponse errormessage = ex.Response;
            //        using (Stream responsestream=errormessage.GetResponseStream())
            //        {
            //            StreamReader reader = new StreamReader(responsestream,Encoding.GetEncoding("utf-8"));
            //            responsemessage = reader.ReadToEnd();
            //        }
            //    }

            //}
            //String service = "http://server:81/SearchSvc/CVWebService.svc/";
            //public void CVRESTAPISample()
            //{
            //    //1. Login
            //    string user = "username";
            //    string pwd = "plainpassword";
            //    string token = GetSessionToken(user, pwd);
            //    if (string.IsNullOrEmpty(token))
            //    {
            //        Debug.WriteLine("Login Failed");
            //    }
            //    else
            //    {
            //        Debug.WriteLine("Login Successful");
            //    }
            //    //Login successful.
            //}
            //private string GetSessionToken(string userName, string password)
            //{
            //    string token = string.Empty;
            //    string loginService = service + "Login";
            //    byte[] pwd = System.Text.Encoding.UTF8.GetBytes(password);
            //    String encodedPassword = Convert.ToBase64String(pwd, 0, pwd.Length, Base64FormattingOptions.None);
            //    string loginReq = string.Format("<DM2ContentIndexing_CheckCredentialReq username=\"{0}\" password=\"{1}\" />", userName, encodedPassword);

            //    HttpWebResponse resp = SendRequest(loginService, "POST", null, loginReq);
            //    //Check response code and check if the response has an attribute "token" set
            //    if (resp.StatusCode == HttpStatusCode.OK)
            //    {
            //        XmlDocument xmlDoc = new XmlDocument();
            //        xmlDoc.Load(resp.GetResponseStream());
            //        token = xmlDoc.SelectSingleNode("/DM2ContentIndexing_CheckCredentialResp/@token").Value;
            //    }
            //    else
            //    {
            //        Debug.WriteLine(string.Format("Login Failed. Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
            //    }
            //    return token;
            //}
        }
        protected void lnkGo_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}