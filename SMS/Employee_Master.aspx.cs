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

namespace SMS
{
    public partial class Employee_Master : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["StudentID"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lblStudentID.Text = Request.QueryString["StudentID"].ToString();
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "select * from tblstudent where StudentID= '" + lblStudentID.Text + "'";

                    cmd.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblFirstName.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        lblFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                        lblGrandFatherName.Text = ds.Tables[0].Rows[0]["GrandFatherName"].ToString();
                        lblGender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
                        lblDateOfBirth.Text = ds.Tables[0].Rows[0]["DateOfBirth"].ToString();
                        lblSignature.Text = ds.Tables[0].Rows[0]["Signature"].ToString();
                        lblCollege.Text = ds.Tables[0].Rows[0]["College"].ToString();
                        lblDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                        lblCampus.Text = ds.Tables[0].Rows[0]["Campus"].ToString();
                        lblProgram.Text = ds.Tables[0].Rows[0]["Program"].ToString();
                        lblDegreeType.Text = ds.Tables[0].Rows[0]["DegreeType"].ToString();
                        lblAdmissionType.Text = ds.Tables[0].Rows[0]["AdmissionType"].ToString();
                        lblAdmissionTypeShort.Text = ds.Tables[0].Rows[0]["AdmissionTypeShort"].ToString();
                        lblValidDateUntil.Text = ds.Tables[0].Rows[0]["ValidDateUntil"].ToString();
                        lblIssueDate.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();
                        lblMealNumber.Text = ds.Tables[0].Rows[0]["MealNumber"].ToString();
                        lblUniqueNo.Text = ds.Tables[0].Rows[0]["UniqueNo"].ToString();
                        lblStatus.Text = ds.Tables[0].Rows[0]["Status"].ToString();
                        lblIsactive.Text = ds.Tables[0].Rows[0]["Isactive"].ToString();
                        lblid.Text = ds.Tables[0].Rows[0]["id"].ToString();
                        lblUNIQUEID.Text = ds.Tables[0].Rows[0]["UNIQUEID"].ToString();
                        DivEdit.Visible = false;
                        if (ds.Tables[0].Rows[0]["StudentImages_Blob"].ToString() != "")
                        {
                            byte[] hash1 = (byte[])ds.Tables[0].Rows[0]["StudentImages_Blob"];
                            image1.ImageUrl = "data:image;base64," + Convert.ToBase64String(hash1);
                        }
                        if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "1")
                        {
                            lblCardStatus.Text = "Active";
                        }
                        else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "2")
                        {
                            lblCardStatus.Text = "Revoked";
                        }
                        else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "3")
                        {
                            lblCardStatus.Text = "Lost";
                        }
                        else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "4")
                        {
                            lblCardStatus.Text = "Suspended";
                        }
                        else if (ds.Tables[0].Rows[0]["Cardstatus"].ToString() == "5")
                        {
                            lblCardStatus.Text = "Expired";
                        }
                        else 
                        {
                            lblCardStatus.Text = "Active";
                        }
                        //image1.ImageUrl = ds.Tables[0].Rows[0]["StudentImages_Byte"].ToString();
                    }
                }
                else if (Request.QueryString["Edit"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = false;
                    DivEdit.Visible = true;
                    lblStudentID.Text = Request.QueryString["Edit"].ToString();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "select * from tblstudent where StudentID= '" + lblStudentID.Text + "'";
                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtCardNumber.Text = ds.Tables[0].Rows[0]["Cardid"].ToString();
                        ddlCardstatus.ClearSelection();
                        ddlCardstatus.Items.FindByValue(ds.Tables[0].Rows[0]["Cardstatus"].ToString()).Selected = true;
                    }
                }
                else
                {
                    divgrid.Visible = true;
                    divView.Visible = false;
                    DivEdit.Visible = false;
                    FillGrid();
                }
            }
        }
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
               // SqlCommand cmd = new SqlCommand();
                SqlCommand cmd = new SqlCommand("SP_GetStudentData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select * from tblstudent WHERE StudentID LIKE '%' + @Search + '%' or FirstName LIKE '%' + @Search + '%' OR DateOfBirth LIKE '%' + @Search + '%' OR College LIKE '%' + @Search + '%' OR cardid LIKE '%' + @Search + '%' OR Department  LIKE '%' + @Search + '%'";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt_Grid);
                if (sortExpression != null)
                {
                    DataView dv = dt_Grid.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gridEmployee.DataSource  = dv;
                }
                else
                {
                    gridEmployee.DataSource = dt_Grid;
                }
                
                gridEmployee.DataBind();
            }
            catch
            {
            }
        }

        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Employee_Master.aspx?StudentID=" + ids);
        }
        protected void linkEdit_Click1(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Employee_Master.aspx?Edit=" + ids);
        }
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string Image = "";
            string Photopath = "";
            string filePath = "";
            byte[] bytes = new byte[1];
            SqlCommand cmd = new SqlCommand();
            if (FileUpload1.HasFile)
            {
                string str = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/StudentImages/" + Request.QueryString["Edit"].Replace("/", "_") + "_" + str));
                Image = "~/Images/StudentImages/" + Request.QueryString["Edit"].Replace("/", "_") + "_" + str.ToString();
                string ext = System.IO.Path.GetExtension(FileUpload1.FileName.ToString());
                string FileName1 = Guid.NewGuid().ToString();
                string FileName = System.IO.Path.GetFileName(FileUpload1.FileName.ToString());
                string Imagepath1 = Server.MapPath("~\\" + "Images\\StudentImages");
                FileUpload1.SaveAs(Imagepath1 + "\\"+FileName1 +ext);
                string pathForDB = "Images\\StudentImages";
                Photopath = "~\\" + pathForDB + "\\" + FileName1 + ext;

                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                
                //Save the Byte Array as File.
                filePath = "/Images/StudentImages/" + Path.GetFileName(FileUpload1.FileName);
                File.WriteAllBytes(Server.MapPath(filePath), bytes);
                
                //string name = TextBox1.Text;
                //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;User Instance=True");
                //SqlCommand cmd = new SqlCommand("insert into tbl_data values(@name,@Image)", con);
                //cmd.Parameters.AddWithValue("@name", name);
                //cmd.Parameters.AddWithValue("Image", Image);
                //con.Open();
                //cmd.ExecuteNonQuery();
                //con.Close();
                //Label1.Text = "Image Uploaded";
                //Label1.ForeColor = System.Drawing.Color.ForestGreen;
                
                
            }
            cmd.CommandText = "UPDATE [tblStudent] SET [Cardid] = '" + txtCardNumber.Text + "',Cardstatus='" + ddlCardstatus.SelectedValue + "' WHERE StudentID='" + Request.QueryString["Edit"] + "'";//StudentImages='" + Image + "',StudentImages_Blob=@hash,StudentImages_Byte='" + Photopath + "',
            cmd.Parameters.Add("@hash", bytes);
            cmd.Connection = con;
            //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
            con.Open();
            cmd.ExecuteNonQuery();
            Response.Redirect("Employee_Master.aspx");
            con.Close();
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee_Master.aspx");
        }

         
        
        protected void LnkExport_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //string FileName = "SMS" + DateTime.Now + ".xls";
            //StringWriter strwritter = new StringWriter();
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //gridEmployee.GridLines = GridLines.Both;
            //gridEmployee.HeaderStyle.Font.Bold = true;
            //gridEmployee.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            //Response.End(); 
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=SMS" + DateTime.Now + ".xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages
            //    gridEmployee.AllowPaging = false;
            //    FillGrid();

            //    gridEmployee.HeaderRow.BackColor = Color.White;
            //    foreach (TableCell cell in gridEmployee.HeaderRow.Cells)
            //    {
            //        cell.BackColor = gridEmployee.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in gridEmployee.Rows)
            //    {
            //        row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = gridEmployee.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = gridEmployee.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }

            //    gridEmployee.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();

            //}
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from tblstudent WHERE StudentID LIKE '%' + @Search + '%' or FirstName LIKE '%' + @Search + '%' OR DateOfBirth LIKE '%' + @Search + '%' OR College LIKE '%' + @Search + '%' OR cardid LIKE '%' + @Search + '%' OR Department  LIKE '%' + @Search + '%'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt_Grid);
            XLWorkbook wb = new XLWorkbook();

            wb.Worksheets.Add(dt_Grid, "tblstudent");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                MemoryStream MyMemoryStream = new MemoryStream();
                
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                
            
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
    }
}