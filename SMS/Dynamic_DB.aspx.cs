using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS
{
    public partial class Dynamic_DB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string name = "dbconnection";
                AddUpdateConnectionString(name);
            }
        }
        private void AddUpdateConnectionString(string name)
        {
            bool isNew = false;
            //string path = Server.MapPath("~/Web.Config");
            string path = Server.MapPath("~/Web.Config");
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList list = doc.DocumentElement.SelectNodes(string.Format("connectionStrings/add[@name='{0}']", name));
            XmlNode node;
            isNew = list.Count == 0;
            if (isNew)
            {
                node = doc.CreateNode(XmlNodeType.Element, "add", null);
                XmlAttribute attribute = doc.CreateAttribute("name");
                attribute.Value = name;
                node.Attributes.Append(attribute);

                attribute = doc.CreateAttribute("connectionString");
                attribute.Value = "";
                node.Attributes.Append(attribute);

                attribute = doc.CreateAttribute("providerName");
                attribute.Value = "System.Data.SqlClient";
                node.Attributes.Append(attribute);
            }
            else
            {
                node = list[0];
            }
            string conString = node.Attributes["connectionString"].Value;
            SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder(conString);
            conStringBuilder.InitialCatalog = txtDBNAme.Text.Trim();
            conStringBuilder.DataSource = txtServerName.Text.Trim();
            conStringBuilder.IntegratedSecurity = true;
            conStringBuilder.UserID = txtusername.Text.Trim();
            conStringBuilder.Password = txtPassword.Text.Trim();
            node.Attributes["connectionString"].Value = conStringBuilder.ConnectionString;
            if (isNew)
            {
                doc.DocumentElement.SelectNodes("connectionStrings")[0].AppendChild(node);
            }
            doc.Save(path);
            Response.Redirect("Login.aspx");
        }
        protected bool Validation()
        {
            if (txtDBNAme.Text=="" && txtServerName.Text=="" && txtPassword.Text == "" && txtusername.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Username and Password')</script>");
                Label2.Visible = true;
                Label1.Visible = false;
                lblerror.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrorusername.Visible = false;
                lblerrordbname.Visible = false;
                lblerrorServername.Visible = false;
                return false;
            }
            else if (txtServerName.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Username ')</script>");
                Label2.Visible = false;
                Label1.Visible = false;
                lblerror.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrorServername.Visible = true;
                lblerrordbname.Visible = false;
                lblerrorusername.Visible = false;
                return false;
            }
            else if (txtDBNAme.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Username ')</script>");
                Label2.Visible = false;
                Label1.Visible = false;
                lblerror.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrordbname.Visible = true;
                lblerrorServername.Visible = false;
                lblerrorusername.Visible = false;
                return false;
            }
            else if (txtusername.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Username ')</script>");
                Label2.Visible = false;
                Label1.Visible = false;
                lblerror.Visible = false;
                lblerrorpassword.Visible = false;
                lblerrorusername.Visible = true;
                lblerrordbname.Visible = false;
                lblerrorServername.Visible = false;
                return false;
            }
            else if (txtPassword.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Password')</script>");
                Label2.Visible = false;
                Label1.Visible = false;
                lblerror.Visible = false;
                lblerrorpassword.Visible = true;
                lblerrorusername.Visible = false;
                lblerrordbname.Visible = false;
                lblerrorServername.Visible = false;
                return false;
            }
            return true;
        }
    }
}