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
using ClosedXML.Excel;
using SMS.Class;
using System.Web.Configuration;
using System.Globalization;


namespace SMS
{
    public partial class SMS_Upload_label_File : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        LogFile logFile = new LogFile();
        String line = "";
        SqlConnection con = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!Page.IsPostBack)
            {
                pageload();
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
        protected void pageload()
        {
            try
            {

                Session["Canteen_Flag"] = null;
                Session["Session_Id"] = null;
                Session["CanteenName"] = null;
                Session["CanteenCount"] = null;
                Session["Sider"] = "Upload Badge File";
                
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }

        }

        protected void lnkupload_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.Path.GetExtension(fstudent.FileName.ToString()) == ".label")
                {
                    string fileName = Path.GetFileName(fstudent.PostedFile.FileName);
                    string filePath = Server.MapPath("~/XML/Student_Badge.label");
                    fstudent.SaveAs(filePath);
                    lblsuccess.Visible = true;
                }
                else if (System.IO.Path.GetExtension(fstudent.FileName.ToString()) == "")
                {

                }
                else 
                {
                    lblstudenterror.Visible = true;
                    lblvisitorerror.Visible = false;
                }
                if (System.IO.Path.GetExtension(fvisitor.FileName.ToString()) == ".label")
                {
                    string fileName = Path.GetFileName(fvisitor.PostedFile.FileName);
                    string filePath = Server.MapPath("~/XML/Visitor_Badge.label");
                    fvisitor.SaveAs(filePath);
                    lblsuccess.Visible = true;
                }
                else if (System.IO.Path.GetExtension(fvisitor.FileName.ToString()) == "")
                {

                }
                else
                {
                    lblvisitorerror.Visible = true;
                    lblstudenterror.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
    }
}