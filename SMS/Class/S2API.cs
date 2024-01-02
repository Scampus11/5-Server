using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml;

namespace SMS.Class
{
    public class S2API
    {
        #region Login API
        public static string LoginAPI(string IpAddress, string UserName, string Password, string Action)
        {
            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + IpAddress.Trim() + "/goforms/nbapi";
            var xml = @"  
             <NETBOX-API>
             <COMMAND name='Login' num='1' dateformat='tzoffset'>
             <PARAMS>
             <USERNAME>" + UserName.Trim() + "</USERNAME>"
             + "<PASSWORD>" + Password.Trim() + "</PASSWORD>"
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
                foreach (XmlNode childrenNode in parentNode)
                {
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
                    System.Web.HttpContext.Current.Session["ipaddress"] = IpAddress;
                }

                responsemessage = msg;

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
        #endregion

        #region Student API
        public static string GetStudentAPI(string StudentId)
        {
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='GetPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StudentId.Trim() + "</PERSONID>"
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
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
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
        public static string AddStudentAPI(string StudentId, string FirstName, string MiddleName, string LastName,
        string Gender, string DateofBirth, string Campus, string collage, string Department, string Program, string AdmissionType,
        string AdmissionShort, string ValidDate, string IssueDate, string MealNumber, string EmailId, string Images,
        string CardNumbers, string DegreeType)
        {
            string XmlString = "";
            //if (Gender.Trim() != "")
            //{
            //    XmlString += "<UDF1>" + Gender.Trim() + "</UDF1>";
            //}
            //if (MealNumber.Trim() != "")
            //{
            //    XmlString += "<UDF2>" + MealNumber.Trim() + "</UDF2>";
            //}
            //if (Department.Trim() != "")
            //{
            //    XmlString += "<UDF3>" + Department.Trim() + "</UDF3>";
            //}
            //if (collage.Trim() != "")
            //{
            //    XmlString += "<UDF4>" + collage.Trim() + "</UDF4>";
            //}
            //if (DegreeType.Trim() != "")
            //{
            //    XmlString += "<UDF5>" + DegreeType.Trim() + "</UDF5>";
            //}
            //if (AdmissionShort.Trim() != "")
            //{
            //    XmlString += "<UDF6>" + AdmissionShort.Trim() + "</UDF6>";
            //}
            if (IssueDate.Trim() != "")
            {
                XmlString += "<ACTDATE>" + Convert.ToDateTime(IssueDate).ToString("yyyy-MM-dd") + "</ACTDATE>";
            }
            if (Images.Trim() != "")
            {
                XmlString += "<PICTURE>" + Images.Replace("data:image/png;base64,", "") + "</PICTURE>";
            }
            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='AddPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StudentId.Trim() + "</PERSONID>"
             + "<FIRSTNAME>" + FirstName.Trim() + " " + MiddleName.Trim() + "</FIRSTNAME>"
             + "<LASTNAME>" + LastName.Trim() + "</LASTNAME>"
             + "<EXPDATE>" + Convert.ToDateTime(ValidDate.Trim()).ToString("yyyy-MM-dd") + "</EXPDATE>"
             + "<UDF1>" + Gender.Trim() + "</UDF1>"
             + "<UDF2>" + MealNumber.Trim() + "</UDF2>"
             + "<UDF3>" + Department.Trim() + "</UDF3>"
             + "<UDF4>" + collage.Trim() + "</UDF4>"
             + "<UDF5>" + DegreeType.Trim() + "</UDF5>"
             + "<UDF6>" + AdmissionShort.Trim() + "</UDF6>"
             //+ "<ACCESSLEVELS><ACCESSLEVEL>Visitors Access AL</ACCESSLEVEL></ACCESSLEVELS>"
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
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
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

        public static string UpdateStudentAPI(string StudentId, string FirstName, string MiddleName, string LastName,
        string Gender, string DateofBirth, string Campus, string collage, string Department, string Program, string AdmissionType,
        string AdmissionShort, string ValidDate, string IssueDate, string MealNumber, string EmailId, string Images,
        string CardNumbers, string DegreeType)
        {
            string XmlString = "";
            //if (Gender.Trim() != "")
            //{
            //    XmlString += "<UDF1>" + Gender.Trim() + "</UDF1>";
            //}
            //if (MealNumber.Trim() != "")
            //{
            //    XmlString += "<UDF2>" + MealNumber.Trim() + "</UDF2>";
            //}
            //if (Department.Trim() != "")
            //{
            //    XmlString += "<UDF3>" + Department.Trim() + "</UDF3>";
            //}
            //if (collage.Trim() != "")
            //{
            //    XmlString += "<UDF4>" + collage.Trim() + "</UDF4>";
            //}
            //if (DegreeType.Trim() != "")
            //{
            //    XmlString += "<UDF5>" + DegreeType.Trim() + "</UDF5>";
            //}
            //if (AdmissionShort.Trim() != "")
            //{
            //    XmlString += "<UDF6>" + AdmissionShort.Trim() + "</UDF6>";
            //}
            if (IssueDate.Trim() != "")
            {
                XmlString += "<ACTDATE>" + Convert.ToDateTime(IssueDate).ToString("yyyy-MM-dd") + "</ACTDATE>";
            }
            if (Images.Trim() != "")
            {
                XmlString += "<PICTURE>" + Images.Replace("data:image/png;base64,", "") + "</PICTURE>";
            }
            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='ModifyPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StudentId.Trim() + "</PERSONID>"
             + "<FIRSTNAME>" + FirstName.Trim() + " " + MiddleName.Trim() + "</FIRSTNAME>"
             + "<LASTNAME>" + LastName.Trim() + "</LASTNAME>"
             + "<EXPDATE>" + Convert.ToDateTime(ValidDate.Trim()).ToString("yyyy-MM-dd") + "</EXPDATE>"
             + "<UDF1>" + Gender.Trim() + "</UDF1>"
             + "<UDF2>" + MealNumber.Trim() + "</UDF2>"
             + "<UDF3>" + Department.Trim() + "</UDF3>"
             + "<UDF4>" + collage.Trim() + "</UDF4>"
             + "<UDF5>" + DegreeType.Trim() + "</UDF5>"
             + "<UDF6>" + AdmissionShort.Trim() + "</UDF6>"
             //+ "<ACCESSLEVELS><ACCESSLEVEL>Visitors Access AL</ACCESSLEVEL></ACCESSLEVELS>"
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
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
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

        public static string AddStudentCardnumberApi(string StudentId, string Cardnumber, string Status)
        {

            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='AddCredential' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StudentId.Trim() + "</PERSONID>"
             + "<CARDFORMAT>Mifare ASU</CARDFORMAT>"
             + "<HOTSTAMP>1111</HOTSTAMP>"
             + "<ENCODEDNUM>" + Cardnumber.Trim() + "</ENCODEDNUM>"
             + "<WANTCREDENTIALID>1</WANTCREDENTIALID>"
             + "<CARDSTATUS>" + Status.Trim() + "</CARDSTATUS>"
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
        public static string ModifyStudentCardnumberApi(string StudentId, string Cardnumber, string Status)
        {
            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='ModifyCredential' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StudentId.Trim() + "</PERSONID>"
             + "<CARDFORMAT>Mifare ASU</CARDFORMAT>"
             + "<HOTSTAMP>1111</HOTSTAMP>"
             + "<ENCODEDNUM>" + Cardnumber.Trim() + "</ENCODEDNUM>"
             + "<WANTCREDENTIALID>1</WANTCREDENTIALID>"
             + "<CARDSTATUS>" + Status.Trim() + "</CARDSTATUS>"
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
        #endregion

        #region Staff API
        public static string GetStaffAPI(string StaffId)
        {
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='GetPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StaffId.Trim() + "</PERSONID>"
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
                //Select the book node with the matching attribute value.
                XmlNode nodeToFind;
                XmlElement root = doc1.DocumentElement;


                // Selects all the title elements that have an attribute named lang
                nodeToFind = root.SelectSingleNode("//NETBOX/RESPONSE/CODE");

                if (nodeToFind != null)
                {
                    responsemessage = doc1.SelectSingleNode("//NETBOX/RESPONSE/CODE").InnerText;
                }
                else
                {
                    nodeToFind = root.SelectSingleNode("//NETBOX/RESPONSE/APIERROR");
                    if(nodeToFind!=null) responsemessage = doc1.SelectSingleNode("//NETBOX/RESPONSE/APIERROR").InnerText;
                }
                
                foreach (XmlNode childrenNode in parentNode)
                {
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
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
        public static string AddStaffAPI(string StaffId, string FullName, string Gender,
        string Department, string Phoneno, string EmailId, string Address, string UID,
        string PlateNo, string ValidDate, string IssueDate, string Images)
        {
            string XmlString = "";
            if (IssueDate.Trim() != "")
            {
                XmlString += "<ACTDATE>" + Convert.ToDateTime(IssueDate).ToString("yyyy-MM-dd") + "</ACTDATE>";
            }
            if (Images.Trim() != "")
            {
                XmlString += "<PICTURE>" + Images.Replace("data:image/png;base64,", "") + "</PICTURE>";
            }
            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='AddPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StaffId.Trim() + "</PERSONID>"
             //+ "<FIRSTNAME>" + FullName.Trim() + "</FIRSTNAME>"
             + "<LASTNAME>" + FullName.Trim() + "</LASTNAME>"
             //+ "<EXPDATE>" + Convert.ToDateTime(ValidDate.Trim()).ToString("yyyy-MM-dd") + "</EXPDATE>"
             + "<UDF1>" + Gender.Trim() + "</UDF1>"
             + "<CONTACTPHONE>" + Phoneno.Trim() + "</CONTACTPHONE>"
             + "<UDF3>" + Department.Trim() + "</UDF3>"
             + "<CONTACTSMSEMAIL>" + EmailId.Trim() + "</CONTACTSMSEMAIL>"
             + "<CONTACTLOCATION>" + Address.Trim() + "</CONTACTLOCATION>"
             + "<VEHICLES><VEHICLE><VEHICLETAGNUM>" + UID.Trim() + "</VEHICLETAGNUM>"
             + "<VEHICLELICNUM>" + PlateNo.Trim() + "</VEHICLELICNUM></VEHICLE></VEHICLES>"
             //+ "<ACCESSLEVELS><ACCESSLEVEL>Visitors Access AL</ACCESSLEVEL></ACCESSLEVELS>"
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
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
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
        public static string UpdateStaffAPI(string StaffId, string FullName, string Gender,
        string Department, string Phoneno, string EmailId, string Address, string UID,
        string PlateNo, string ValidDate, string IssueDate, string Images)
        {
            string XmlString = "";
            if (IssueDate.Trim() != "")
            {
                XmlString += "<ACTDATE>" + Convert.ToDateTime(IssueDate).ToString("yyyy-MM-dd") + "</ACTDATE>";
            }
            if (Images.Trim() != "")
            {
                XmlString += "<PICTURE>" + Images.Replace("data:image/png;base64,", "") + "</PICTURE>";
            }
            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='ModifyPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StaffId.Trim() + "</PERSONID>"
             //+ "<FIRSTNAME>" + FullName.Trim() + "</FIRSTNAME>"
             + "<LASTNAME>" + FullName.Trim() + "</LASTNAME>"
             //+ "<EXPDATE>" + Convert.ToDateTime(ValidDate.Trim()).ToString("yyyy-MM-dd") + "</EXPDATE>"
             + "<UDF1>" + Gender.Trim() + "</UDF1>"
             + "<CONTACTPHONE>" + Phoneno.Trim() + "</CONTACTPHONE>"
             + "<UDF3>" + Department.Trim() + "</UDF3>"
             + "<CONTACTSMSEMAIL>" + EmailId.Trim() + "</CONTACTSMSEMAIL>"
             + "<CONTACTLOCATION>" + Address.Trim() + "</CONTACTLOCATION>"
             + "<VEHICLES><VEHICLE><VEHICLETAGNUM>" + UID.Trim() + "</VEHICLETAGNUM>"
             + "<VEHICLELICNUM>" + PlateNo.Trim() + "</VEHICLELICNUM></VEHICLE></VEHICLES>"
             //+ "<ACCESSLEVELS><ACCESSLEVEL>Visitors Access AL</ACCESSLEVEL></ACCESSLEVELS>"
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
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
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
        public static string AddStaffCardnumberApi(string StaffId, string Cardnumber, string Status)
        {

            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='AddCredential' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StaffId.Trim() + "</PERSONID>"
             + "<CARDFORMAT>Mifare ASU</CARDFORMAT>"
             + "<HOTSTAMP>1111</HOTSTAMP>"
             + "<ENCODEDNUM>" + Cardnumber.Trim() + "</ENCODEDNUM>"
             + "<WANTCREDENTIALID>1</WANTCREDENTIALID>"
             + "<CARDSTATUS>" + Status.Trim() + "</CARDSTATUS>"
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
        public static string ModifyStaffCardnumberApi(string StaffId, string Cardnumber, string Status)
        {

            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='ModifyCredential' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StaffId.Trim() + "</PERSONID>"
             + "<CARDFORMAT>Mifare ASU</CARDFORMAT>"
             + "<HOTSTAMP>1111</HOTSTAMP>"
             + "<ENCODEDNUM>" + Cardnumber.Trim() + "</ENCODEDNUM>"
             + "<WANTCREDENTIALID>1</WANTCREDENTIALID>"
             + "<CARDSTATUS>" + Status.Trim() + "</CARDSTATUS>"
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

        public static string AddStaffUHFApi(string StaffId, string UHF, string Status)
        {

            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='AddCredential' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StaffId.Trim() + "</PERSONID>"
             + "<CARDFORMAT>26 bit Wiegand</CARDFORMAT>"
             + "<HOTSTAMP>1111</HOTSTAMP>"
             + "<ENCODEDNUM>" + UHF.Trim() + "</ENCODEDNUM>"
             + "<WANTCREDENTIALID>1</WANTCREDENTIALID>"
             + "<CARDSTATUS>" + Status.Trim() + "</CARDSTATUS>"
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
        public static string ModifyStaffUHFApi(string StaffId, string UHF, string Status)
        {

            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='ModifyCredential' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StaffId.Trim() + "</PERSONID>"
             + "<CARDFORMAT>26 bit Wiegand</CARDFORMAT>"
             + "<HOTSTAMP>1111</HOTSTAMP>"
             + "<ENCODEDNUM>" + UHF.Trim() + "</ENCODEDNUM>"
             + "<WANTCREDENTIALID>1</WANTCREDENTIALID>"
             + "<CARDSTATUS>" + Status.Trim() + "</CARDSTATUS>"
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
        #endregion

        #region Student Access List
        public static string AddStudentAccesslevelAPI(string StudentId,string Acceslist)
        {
            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='ModifyPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StudentId.Trim() + "</PERSONID>"
             + "<ACCESSLEVELS>" + Acceslist + "</ACCESSLEVELS>"
             //+ "<ACCESSLEVELS><ACCESSLEVEL>Visitors Access AL</ACCESSLEVEL></ACCESSLEVELS>"
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
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
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
        #endregion

        #region Student Access List
        public static string AddStaffAccesslevelAPI(string StaffId, string Acceslist)
        {
            SqlConnection con = new SqlConnection();
            string SQLString = "";
            string filePath1 = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
            StreamReader sr = new StreamReader(filePath1);
            SQLString = sr.ReadToEnd();
            con = new SqlConnection(SQLString);
            string responsemessage = "";
            var url = "http://" + System.Web.HttpContext.Current.Session["ipaddress"].ToString().Trim() + "/goforms/nbapi";
            string xml = @"<NETBOX-API sessionid='" + System.Web.HttpContext.Current.Session["sessionid"] + "'>"
             + "<COMMAND name='ModifyPerson' num='1'>"
             + "<PARAMS>"
             + "<PERSONID>" + StaffId.Trim() + "</PERSONID>"
             + "<ACCESSLEVELS>" + Acceslist + "</ACCESSLEVELS>"
             //+ "<ACCESSLEVELS><ACCESSLEVEL>Visitors Access AL</ACCESSLEVEL></ACCESSLEVELS>"
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
                    System.Web.HttpContext.Current.Session["Sessionid"] = childrenNode.Attributes["sessionid"].Value;
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
        #endregion

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
    }
}