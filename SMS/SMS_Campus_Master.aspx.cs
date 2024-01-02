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
using System.Data.OleDb;
using System.Data.Common;

namespace SMS
{
    public partial class SMS_Campus_Master : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            Session["Sider"] = "Campus Master";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Id"] != null)
                    {
                        divgrid.Visible = false;
                        divView.Visible = true;
                        lnkupdate.Visible = true;
                        lnksave.Visible = false;
                        lnkAdd.Visible = false;
                        con.Open();
                        //txtStudentID.Text = Request.QueryString["Id"].ToString();
                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = "select * from tbl_Campus where Id= '" + Request.QueryString["Id"].ToString() + "'";

                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();


                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtApplication_No.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        }
                    }
                    else if (Request.QueryString["Insert"] != null)
                    {
                        divgrid.Visible = false;
                        divView.Visible = true;
                        lnkupdate.Visible = false;
                        lnksave.Visible = true;
                        lnkAdd.Visible = false;
                    }
                    else
                    {
                        divgrid.Visible = true;
                        divView.Visible = false;
                        FillGrid();
                    }
                    con.Close();
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
        protected void FillGrid(string sortExpression = null)
        {

            try
            {

                //SqlCommand cmd = new SqlCommand();

                //cmd.CommandText = "select * from tbl_Campus ";

                //cmd.Connection = con;
                SqlCommand cmd = new SqlCommand("SP_GetCampusData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(dt_Grid);
                if (sortExpression != null)
                {
                    DataView dv = dt_Grid.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gridEmployee.DataSource = dv;
                }
                else
                {
                    gridEmployee.DataSource = dt_Grid;
                }
                //gridEmployee.DataSource = ds;

                gridEmployee.DataBind();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Campus_Master.aspx?Insert=Insert Department");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("SMS_Campus_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    lblValidCampusname.Visible = false;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO [tbl_Campus] ([Name]) VALUES ('" + txtApplication_No.Text + "')";
                    cmd.Connection = con;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect("SMS_Campus_Master.aspx");
                    con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    lblValidCampusname.Visible = false;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE [tbl_Campus] SET [Name] = '" + txtApplication_No.Text + "' WHERE Id='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Response.Redirect("SMS_Campus_Master.aspx");
                    con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
                Label Ids = (Label)gvr.FindControl("lblId");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Delete from tbl_Campus where Id= '" + Ids.Text + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                FillGrid();
                Response.Redirect("SMS_Campus_Master.aspx");
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected bool Validation()
        {
            if (txtApplication_No.Text == "")
            {
                lblValidCampusname.Visible = true;
                txtApplication_No.Focus();
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Campus_Master.aspx");
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmployee.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {
            FillGrid(e.SortExpression);
        }
        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void lnksyncjob_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SyncCampus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            lblSyncmsg.Visible = true;
            FillGrid();
        }

        protected void lnkImportExcel_Click(object sender, EventArgs e)
        {
            myModal.Attributes.Add("style", "display:block;");
        }

        protected void lnkClose_Click(object sender, EventArgs e)
        {
            myModal.Attributes.Add("style", "display:None;");
            Response.Redirect("SMS_Campus_Master.aspx");
        }

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            if (FileUpload1.PostedFile != null)
            {
                try
                {
                    string path = Server.MapPath("~/ImportExcel/Campus/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(FileUpload1.FileName);
                    string extension = Path.GetExtension(FileUpload1.FileName);
                    FileUpload1.SaveAs(filePath);

                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    DataTable dt = new DataTable();
                    conString = string.Format(conString, filePath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();

                        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                        dtExcelData.Columns.AddRange(new DataColumn[1] {
                            //new DataColumn("Id", typeof(int)),
                new DataColumn("Name", typeof(string))
                //new DataColumn("Salary", typeof(decimal))
                        });

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();
                        string filePath1 = Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
                        StreamReader sr = new StreamReader(filePath1);
                        string consString = sr.ReadToEnd();
                        //con = new SqlConnection(line);
                        //string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name
                                sqlBulkCopy.DestinationTableName = "dbo.tbl_Campus";

                                //[OPTIONAL]: Map the Excel columns with that of the database table
                                //sqlBulkCopy.ColumnMappings.Add("Id", "PersonId");
                                sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                                //sqlBulkCopy.ColumnMappings.Add("Salary", "Salary");
                                con.Open();
                                sqlBulkCopy.WriteToServer(dtExcelData);
                                con.Close();
                                FillGrid();
                            }
                        }
                        lblMessage.Text = "Your file uploaded successfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Your file not uploaded !!!" + ex.Message;
                    //lblMessage.Text = "Your file not uploaded";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            DownloadFile();
        }
        protected void DownloadFile()
        {
            string filePath = Server.MapPath(@"\ImportExcel\Campus\Campus.xlsx");
            //string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
    }
}