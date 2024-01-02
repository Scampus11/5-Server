using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System;
using System.Drawing;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.Configuration;
using SMS.Class;

namespace SMS
{
    public partial class Canteen_Monitoring_Count : System.Web.UI.Page
    {
        // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Connection();
                if (Session["UserName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                //Session["Canteen_Flag"] = Request.QueryString["Flag"].ToString().Trim();
                //Session["Session_Id"] = Request.QueryString["Session_Id"].ToString().Trim();
                //Session["CanteenName"] = Request.QueryString["CanteenName"].ToString().Trim();
                //Session["CanteenCount"] = Request.QueryString["Count"].ToString().Trim();
                
                if (Session["Session_Name"]!=null)
                {
                    lblSessionName.Text = Session["Session_Name"].ToString().Trim();
                    lblcanteen.Text = Session["Canteen"].ToString().Trim();
                 lblallowedcount.Text = Session["AllowedmemCount"].ToString();
                lblaccesscount.Text = Session["AccessMemCount"].ToString();
                lblPendingCount.Text = Session["PenMemCount"].ToString();
                lbldeniedCount.Text = Session["DeniedMemCount"].ToString();
                }
                
                if (Request.QueryString["Flag"].ToString().Trim() == "AllowedMembers")
                {
                    liAlloedMem.Visible = true;
                    liAlloedMem.Attributes.Add("class", "nav-item start active");

                }
                if (Request.QueryString["Flag"].ToString().Trim() == "AccessMembers")
                {
                    liAccessMem.Visible = true;
                    liAccessMem.Attributes.Add("class", "nav-item start active");
                    
                }
                if (Request.QueryString["Flag"].ToString().Trim() == "PendingMembers")
                {
                    liPendingMem.Visible = true;
                    liPendingMem.Attributes.Add("class", "nav-item start active");
                    
                }
                if (Request.QueryString["Flag"].ToString().Trim() == "DeniedMembers")
                {
                    lideniedMem.Visible = true;
                    lideniedMem.Attributes.Add("class", "nav-item start active");
                    
                }
                if (Request.QueryString["Flag"].ToString().Trim() == "AllowedMembers" || Request.QueryString["Flag"].ToString().Trim() == "PendingMembers")
                {
                    FillGrid();
                    divsearch.Visible = true;
                    divsearch2.Visible = false;
                    lnkExportToExcel.Visible = true;
                    lnkExportToExcel.Focus();
                }
                else
                {
                    FillGrid2();
                    divsearch2.Visible = true;
                    divsearch.Visible = false;
                    lnkExportToExcel2.Visible = true;
                    lnkExportToExcel2.Focus();
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
        protected void FillGrid()
        {
            DataTable dtCanteen = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", Request.QueryString["Flag"].ToString().Trim());
            cmd.Parameters.AddWithValue("@Session", Session["Session_Id"].ToString().Trim());
            cmd.Parameters.AddWithValue("@AG_Id", Session["CanteenName"].ToString().Trim());
            cmd.Parameters.AddWithValue("@Date", Request.QueryString["FromDate"].ToString().Trim());
            cmd.Parameters.AddWithValue("@ToDate", Request.QueryString["ToDate"].ToString().Trim());
            cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCanteen);
            if (dtCanteen.Rows.Count > 0)
            {
                gridEmployee.DataSource = dtCanteen;
                gridEmployee.DataBind();
            }
            else
            {
                gridEmployee.DataSource = null;
                gridEmployee.DataBind();
            }
        }
        protected void FillGrid2()
        {
            DataTable dtCanteen = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", Request.QueryString["Flag"].ToString().Trim());
            cmd.Parameters.AddWithValue("@Session", Session["Session_Id"].ToString().Trim());
            cmd.Parameters.AddWithValue("@AG_Id", Session["CanteenName"].ToString().Trim());
            cmd.Parameters.AddWithValue("@Date", Request.QueryString["FromDate"].ToString().Trim());
            cmd.Parameters.AddWithValue("@ToDate", Request.QueryString["ToDate"].ToString().Trim());
            cmd.Parameters.AddWithValue("@Search", txtSearch2.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCanteen);
            if (dtCanteen.Rows.Count > 0)
            {
                GridMemaccess.DataSource = dtCanteen;
                GridMemaccess.DataBind();
            }
            else
            {
                GridMemaccess.DataSource = null;
                GridMemaccess.DataBind();
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Request.QueryString["Flag"].ToString().Trim() == "AllowedMembers" || Request.QueryString["Flag"].ToString().Trim() == "PendingMembers")
            {
                gridEmployee.PageIndex = e.NewPageIndex;
                FillGrid();
            }
            else
            {
                GridMemaccess.PageIndex = e.NewPageIndex;
                FillGrid2();
            }
        }
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Session["Sider"] = null;
            Response.Redirect("Login.aspx");

        }
        protected void lnkalloedmem_Click(object sender, EventArgs e)
        {
            Response.Redirect("Canteen_Monitoring_Count.aspx?Flag=AllowedMembers&FromDate=" + Request.QueryString["FromDate"].ToString().Trim() + "&ToDate=" + Request.QueryString["ToDate"].ToString().Trim() + "");
        }

        protected void lnkdeniedMem_Click(object sender, EventArgs e)
        {
            Response.Redirect("Canteen_Monitoring_Count.aspx?Flag=DeniedMembers&FromDate=" + Request.QueryString["FromDate"].ToString().Trim() + "&ToDate=" + Request.QueryString["ToDate"].ToString().Trim() + "");
        }

        protected void lnkPendingMem_Click(object sender, EventArgs e)
        {
            Response.Redirect("Canteen_Monitoring_Count.aspx?Flag=PendingMembers&FromDate=" + Request.QueryString["FromDate"].ToString().Trim() + "&ToDate=" + Request.QueryString["ToDate"].ToString().Trim() + "");
        }

        protected void lnkAccessMem_Click(object sender, EventArgs e)
        {
            Response.Redirect("Canteen_Monitoring_Count.aspx?Flag=AccessMembers&FromDate=" + Request.QueryString["FromDate"].ToString().Trim() + "&ToDate=" + Request.QueryString["ToDate"].ToString().Trim() + "");
        }
        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + Session["Canteen"].ToString().Trim() + "_" + Session["Session_Name"].ToString().Trim() + "_" + Request.QueryString["Flag"].ToString().Trim() + "_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gridEmployee.AllowPaging = false;
                this.FillGrid();

                gridEmployee.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gridEmployee.HeaderRow.Cells)
                {
                    cell.BackColor = gridEmployee.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gridEmployee.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gridEmployee.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gridEmployee.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gridEmployee.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        protected void ExportToExcel2(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + Session["Canteen"].ToString().Trim() + "_" + Session["Session_Name"].ToString().Trim() + "_" + Request.QueryString["Flag"].ToString().Trim() + "_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridMemaccess.AllowPaging = false;
                this.FillGrid2();

                GridMemaccess.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridMemaccess.HeaderRow.Cells)
                {
                    cell.BackColor = GridMemaccess.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridMemaccess.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridMemaccess.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridMemaccess.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridMemaccess.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        protected void ExportToPDF(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    gridEmployee.AllowPaging = false;
                    this.FillGrid();

                    gridEmployee.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + Session["Canteen"].ToString().Trim() + "_" + Session["Session_Name"].ToString().Trim() + "_" + Request.QueryString["Flag"].ToString().Trim() + "_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void lblSearch2_Click(object sender, EventArgs e)
        {
            divsearch.Visible = false;
            divsearch2.Visible = true;
            FillGrid2();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            divsearch.Visible = true;
            divsearch2.Visible = false;
            FillGrid();
        }
    }
}