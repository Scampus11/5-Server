using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;

namespace SMS
{
    public partial class FileUpload : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //Upload and save the file
                string excelPath = Server.MapPath("~/Upload/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(excelPath);

                string conString = string.Empty;
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;

                }
                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();

                    //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                    dtExcelData.Columns.AddRange(new DataColumn[26] { new DataColumn("StudentID", typeof(string)),
                new DataColumn("FirstName", typeof(string)),
                new DataColumn("FatherName", typeof(string)),
                new DataColumn("GrandFatherName", typeof(string)),
                new DataColumn("Gender", typeof(string)),
                new DataColumn("DateOfBirth", typeof(DateTime)),
                new DataColumn("Signature", typeof(string)),
                new DataColumn("College", typeof(string)),
                new DataColumn("Department", typeof(string)),
                new DataColumn("Campus", typeof(string)),
                new DataColumn("Program", typeof(string)),
                new DataColumn("DegreeType", typeof(string)),
                new DataColumn("AdmissionType", typeof(string)),
                new DataColumn("AdmissionTypeShort", typeof(string)),
                new DataColumn("ValidDateUntil", typeof(float)),
                new DataColumn("IssueDate", typeof(string)),
                new DataColumn("MealNumber", typeof(string)),
                new DataColumn("UniqueNo", typeof(float)),
                new DataColumn("Status", typeof(string)),
                new DataColumn("Isactive", typeof(bool)),
                new DataColumn("UNIQUEID", typeof(string)),
                new DataColumn("cardstatus", typeof(string)),
                new DataColumn("cardid", typeof(string)),
                new DataColumn("Id", typeof(int)),
                new DataColumn("StudentImages", typeof(string)),
              //  new DataColumn("StudentImages_Blob", typeof(byte)),
                new DataColumn("StudentImages_Byte", typeof(string))
                });

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Distinct * FROM [" + sheet1 + "]", excel_con))
                    {
                        oda.Fill(dtExcelData);
                    }
                    excel_con.Close();


                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.tblstudent";

                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("StudentID", "StudentID");
                            sqlBulkCopy.ColumnMappings.Add("FirstName", "FirstName");
                            sqlBulkCopy.ColumnMappings.Add("FatherName", "FatherName");
                            sqlBulkCopy.ColumnMappings.Add("GrandFatherName", "GrandFatherName");
                            sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                            sqlBulkCopy.ColumnMappings.Add("DateOfBirth", "DateOfBirth");
                            sqlBulkCopy.ColumnMappings.Add("Signature", "Signature");
                            sqlBulkCopy.ColumnMappings.Add("College", "College");
                            sqlBulkCopy.ColumnMappings.Add("Department", "Department");
                            sqlBulkCopy.ColumnMappings.Add("Campus", "Campus");
                            sqlBulkCopy.ColumnMappings.Add("Program", "Program");
                            sqlBulkCopy.ColumnMappings.Add("DegreeType", "DegreeType");
                            sqlBulkCopy.ColumnMappings.Add("AdmissionType", "AdmissionType");
                            sqlBulkCopy.ColumnMappings.Add("AdmissionTypeShort", "AdmissionTypeShort");
                            sqlBulkCopy.ColumnMappings.Add("ValidDateUntil", "ValidDateUntil");
                            sqlBulkCopy.ColumnMappings.Add("IssueDate", "IssueDate");
                            sqlBulkCopy.ColumnMappings.Add("MealNumber", "MealNumber");
                            sqlBulkCopy.ColumnMappings.Add("UniqueNo", "UniqueNo");
                            sqlBulkCopy.ColumnMappings.Add("Status", "Status");
                            sqlBulkCopy.ColumnMappings.Add("Isactive", "Isactive");
                            sqlBulkCopy.ColumnMappings.Add("UNIQUEID", "UNIQUEID");
                            sqlBulkCopy.ColumnMappings.Add("cardstatus", "cardstatus");
                            sqlBulkCopy.ColumnMappings.Add("cardid", "cardid");
                            sqlBulkCopy.ColumnMappings.Add("Id", "Id");
                            sqlBulkCopy.ColumnMappings.Add("StudentImages", "StudentImages");
                            // sqlBulkCopy.ColumnMappings.Add("StudentImages_Blob", "StudentImages_Blob");
                            sqlBulkCopy.ColumnMappings.Add("StudentImages_Byte", "StudentImages_Byte");

                            con.Open();
                            sqlBulkCopy.WriteToServer(dtExcelData);
                            con.Close();
                            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}