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
using SMS.Class;

namespace SMS
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LogFile LogFile = new LogFile();
            string responsemessage = "";
            var url = WebConfigurationManager.AppSettings["APIURL"].ToString();
            //"https://api.beta.shipwire.com/exec/TrackingServices.php";
            var xml = @"  
            <NETBOX-API>
             <COMMAND name='Login' num='1' dateformat='tzoffset'>
             <PARAMS>
             <USERNAME>"+WebConfigurationManager.AppSettings["APIUserName"].ToString()+"</USERNAME>"
             +"<PASSWORD>"+WebConfigurationManager.AppSettings["APIPWD"].ToString()+"</PASSWORD>"
             +"</PARAMS>"
             + "</COMMAND>"
            + "</NETBOX-API> ";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Method = "POST";
                request.ContentType = "application/XML";
                //request.Headers.Add("Authorization",Autho);
                StreamWriter requestwriter = new StreamWriter(request.GetRequestStream());
                requestwriter.Write(xml);
                requestwriter.Close();
                StreamReader responsereader = new StreamReader(request.GetResponse().GetResponseStream());
                responsemessage = responsereader.ReadToEnd();
                responsereader.Close();
                request.GetResponse().Close();
                XmlDocument doc1 = new XmlDocument();
                doc1.LoadXml(responsemessage);
                var json = JsonConvert.SerializeXmlNode(doc1, Newtonsoft.Json.Formatting.None, true);
                lblmsg.Text = json;
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
    }
    
}