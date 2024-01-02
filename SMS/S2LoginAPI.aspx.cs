using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SMS.Class;
namespace SMS
{
    public partial class S2LoginAPI : System.Web.UI.Page
    {
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        String line = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!Page.IsPostBack)
            {
                try
                {
                    ModalPopupExtender2.Show();
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
        protected bool Validation()
        {
            if(txtipaddress.Text.Trim()=="")
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
            if (txtpassword.Text.Trim() == "")
            {
                lblipaddressvdn.Visible = false;
                lblpasswordvdn.Visible = true;
                lblusernamevdn.Visible = false;
                return false;
            }
            return true;
        }

        protected void lnklogin_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                SqlCommand cmd = new SqlCommand("SPS2loginAPI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Get");
                cmd.Parameters.AddWithValue("@IpAddress", txtipaddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtusername.Text.Trim());
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if(ds.Rows.Count>0)
                {
                    LoginApi();
                }
                else
                {
                    LoginApi();
                }
            }
        }
        protected void LoginApi()
        {
            string responsemessage = "";
            var url = "http://"+txtipaddress.Text.Trim()+"/goforms/nbapi";
            var xml = @"  
             <NETBOX-API>
             <COMMAND name='Login' num='1' dateformat='tzoffset'>
             <PARAMS>
             <USERNAME>"+txtusername.Text.Trim()+ "</USERNAME>"
             + "<PASSWORD>"+txtpassword.Text.Trim()+"</PASSWORD>"
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
                foreach (XmlNode childrenNode in parentNode)
                {
                    Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
                    
                }
                string msg = doc1.SelectSingleNode("//NETBOX/RESPONSE/CODE").InnerText;
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
        protected void btncancel_Click(object sender, EventArgs e)
        {

        }
    }
}