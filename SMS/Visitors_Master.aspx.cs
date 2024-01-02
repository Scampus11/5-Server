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
using SMS.Class;
using System.Net.Mail;
using System.Net;
using QRCoder;
using System.Drawing;

namespace SMS
{
    public partial class Visitors_Master : System.Web.UI.Page
    {
        string Image = "";
        byte[] bytes = new byte[1];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Insert"] == null)
                {
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                if (Request.QueryString["Id"] != null && Request.QueryString["Type"].ToString() == "Edit")
                {
                    EditMode();
                    lnkviewgrid.Visible = true;
                    lnkView_Menu.Visible = true;
                    lnkEdit_menu.Visible = false;
                    divSLN.Visible = true;
                    txtSLN_ACS_Visitor_Info.Enabled = false;
                }
                else if (Request.QueryString["Id"] != null && Request.QueryString["Type"].ToString() == "View")
                {
                    EditMode();
                    ViewMode();
                    lnkviewgrid.Visible = true;
                    lnkView_Menu.Visible = false;
                    lnkEdit_menu.Visible = true;
                    divSLN.Visible = true;
                    txtSLN_ACS_Visitor_Info.Enabled = false;
                }
                else if (Request.QueryString["Insert"] != null)
                {
                    divgrid.Visible = false;
                    lnkupdate.Visible = false;
                    lnksave.Visible = true;
                    divgrid.Visible = false;
                    divView.Visible = true;
                    divSLN.Visible = true;
                    lnkAdd.Visible = false;
                }
                else
                {
                    divgrid.Visible = true;
                    divView.Visible = false;
                    FillGrid();
                }
            }
        }
        //LogOut Logic
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        //Add New Staff Logic
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Visitors_Master.aspx?Insert=Insert Department");
        }
        //Save Staff Details Logic
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                ImageUpload();
                SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Insert");
                cmd.Parameters.AddWithValue("@Visitor_RecId", txtVisitor_RecId.Text);
                cmd.Parameters.AddWithValue("@Visitor_GUID", txtVisitor_GUID.Text);
                cmd.Parameters.AddWithValue("@First_Name", txtFirst_Name.Text);
                cmd.Parameters.AddWithValue("@Last_Name", txtLast_Name.Text);
                cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                cmd.Parameters.AddWithValue("@Visitor_Type", ddlVisitor_Type.SelectedValue);
                cmd.Parameters.AddWithValue("@Visit_Reason", ddlVisit_Reason.SelectedValue);
                cmd.Parameters.AddWithValue("@Phone_Number", txtPhone_Number.Text);
                cmd.Parameters.AddWithValue("@Visitor_Photo", Image);
                cmd.Parameters.AddWithValue("@ID_Number", txtID_Number.Text);
                cmd.Parameters.AddWithValue("@National_ID", txtNational_ID.Text);
                cmd.Parameters.AddWithValue("@Host_Employee_Code", txtHost_Employee_Code.Text);
                cmd.Parameters.AddWithValue("@Access_Card_Number", txtAccess_Card_Number.Text);
                cmd.Parameters.AddWithValue("@Access_Level", ddlAccess_Level.SelectedValue);
                cmd.Parameters.AddWithValue("@Check_In",txtCheck_In.Text);
                cmd.Parameters.AddWithValue("@Valid_To", txtValid_To.Text);
                cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                cmd.Parameters.AddWithValue("@Valid_From", txtValid_From.Text);
                cmd.Parameters.AddWithValue("@Check_Out", txtCheck_Out.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text);
                cmd.Parameters.AddWithValue("@LastUpdatedBy", txtLastUpdatedBy.Text);
                cmd.Parameters.AddWithValue("@RecordStatus", txtRecordStatus.Text);
                cmd.Parameters.AddWithValue("@Id", 0);
                cmd.Parameters.AddWithValue("@hash", bytes);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("Login.aspx");
                
            }
        }
        //Update Staff Details Logic
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                ImageUpload();
                SqlCommand cmd = new SqlCommand("SP_SMS_Visitors_upset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Update");
                cmd.Parameters.AddWithValue("@Visitor_RecId", txtVisitor_RecId.Text);
                cmd.Parameters.AddWithValue("@Visitor_GUID", txtVisitor_GUID.Text);
                cmd.Parameters.AddWithValue("@First_Name", txtFirst_Name.Text);
                cmd.Parameters.AddWithValue("@Last_Name", txtLast_Name.Text);
                cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                cmd.Parameters.AddWithValue("@Visitor_Type", ddlVisitor_Type.SelectedValue);
                cmd.Parameters.AddWithValue("@Visit_Reason", ddlVisit_Reason.SelectedValue);
                cmd.Parameters.AddWithValue("@Phone_Number", txtPhone_Number.Text);
                cmd.Parameters.AddWithValue("@Visitor_Photo", Image);
                cmd.Parameters.AddWithValue("@ID_Number", txtID_Number.Text);
                cmd.Parameters.AddWithValue("@National_ID", txtNational_ID.Text);
                cmd.Parameters.AddWithValue("@Host_Employee_Code", txtHost_Employee_Code.Text);
                cmd.Parameters.AddWithValue("@Access_Card_Number", txtAccess_Card_Number.Text);
                cmd.Parameters.AddWithValue("@Access_Level", ddlAccess_Level.SelectedValue);
                cmd.Parameters.AddWithValue("@Check_In", txtCheck_In.Text);
                cmd.Parameters.AddWithValue("@Valid_To", txtValid_To.Text);
                cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                cmd.Parameters.AddWithValue("@Valid_From", txtValid_From.Text);
                cmd.Parameters.AddWithValue("@Check_Out", txtCheck_Out.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text);
                cmd.Parameters.AddWithValue("@LastUpdatedBy", txtLastUpdatedBy.Text);
                cmd.Parameters.AddWithValue("@RecordStatus", txtRecordStatus.Text);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Request.QueryString["Id"]));
                cmd.Parameters.AddWithValue("@hash", bytes);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("Visitors_Master.aspx");
                con.Close();
            }
        }
        //Get Staff Details Logic
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from SMS_Visitors WHERE SLN_ACS_Visitor_Info LIKE '%' + @Search + '%' or Visitor_RecId LIKE '%' + @Search + '%' OR Visitor_GUID LIKE '%' + @Search + '%' OR Email_Id LIKE '%' + @Search + '%' OR First_Name LIKE '%' + @Search + '%' OR Company  LIKE '%' + @Search + '%'";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (sortExpression != null)
                {
                    DataView dv = ds.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gridEmployee.DataSource = dv;
                }
                else
                {
                    gridEmployee.DataSource = ds;
                }

                gridEmployee.DataBind();
                //cmd.Connection = con;
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //gridEmployee.DataSource = ds;
                //gridEmployee.DataBind();
            }
            catch
            {
            }

        }
        //Validation Logic
        protected bool Validation()
        {
            if (txtFirst_Name.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter First Name')</script>");
                return false;
            }
            //if (txtVisitor_RecId.Text == "")
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Visitor_RecId')</script>");
            //    return false;
            //}
            if (txtEmail_Id.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Email Id')</script>");
                return false;
            }
            //if (txtValid_From.Text == "")
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Valid From')</script>");
            //    return false;
            //}
            //if (lnksave.Visible == true)
            //{
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = "select Staff_Id from SMS_Visitors";
            //    cmd.Connection = con;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            if (ds.Tables[0].Rows[i]["Staff_Id"].ToString() == txtSLN_ACS_Visitor_Info.Text)
            //            {
            //                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Staff Id Already Exits')</script>");
            //                return false;
            //            }
            //        }
            //    }
            //}
            //else
            //{

            //}
            return true;
        }
        //Image upload Logic
        protected void ImageUpload()
        {

            if (FileUpload1.HasFile)
            {
                string str = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/StaffImages/" + txtSLN_ACS_Visitor_Info.Text.Replace("/", "_") + "_" + str));
                Image = "~/Images/StaffImages/" + txtSLN_ACS_Visitor_Info.Text.Replace("/", "_") + "_" + str.ToString();
                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);

                //Save the Byte Array as File.
                Image = "/Images/StudentImages/" + Path.GetFileName(FileUpload1.FileName);
                File.WriteAllBytes(Server.MapPath(Image), bytes);
            }
        }
        //Edit Staff Details Logic
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Visitors_Master.aspx?Id=" + ids + "&Type=Edit");
        }
        //View Staff Details Logic
        protected void linkView_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Visitors_Master.aspx?Id=" + ids + "&Type=View");
        }
        //Method for Edit Mode
        protected void EditMode()
        {
            divgrid.Visible = false;
            divView.Visible = true;
            lnkupdate.Visible = true;
            lnksave.Visible = false;
            lnkAdd.Visible = false;
            txtSLN_ACS_Visitor_Info.Enabled = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from SMS_Visitors where SLN_ACS_Visitor_Info= '" + Request.QueryString["Id"].ToString() + "'";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lblId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                txtSLN_ACS_Visitor_Info.Text = ds.Tables[0].Rows[0]["SLN_ACS_Visitor_Info"].ToString();
                txtVisitor_RecId.Text = ds.Tables[0].Rows[0]["Visitor_RecId"].ToString();
                txtVisitor_GUID.Text = ds.Tables[0].Rows[0]["Visitor_GUID"].ToString();
                txtFirst_Name.Text = ds.Tables[0].Rows[0]["First_Name"].ToString();
                //ddlVisitor_Type.SelectedValue = ds.Tables[0].Rows[0]["Visitor_Type"].ToString();
                txtLast_Name.Text = ds.Tables[0].Rows[0]["Last_Name"].ToString();
                txtCompany.Text = ds.Tables[0].Rows[0]["Company"].ToString();
               // ddlVisit_Reason.SelectedValue = ds.Tables[0].Rows[0]["Visit_Reason"].ToString();
                txtPhone_Number.Text = ds.Tables[0].Rows[0]["Phone_Number"].ToString();
                txtID_Number.Text = ds.Tables[0].Rows[0]["ID_Number"].ToString();
                txtNational_ID.Text = ds.Tables[0].Rows[0]["National_ID"].ToString();
                txtHost_Employee_Code.Text = ds.Tables[0].Rows[0]["Host_Employee_Code"].ToString();
                txtAccess_Card_Number.Text = ds.Tables[0].Rows[0]["Access_Card_Number"].ToString();
                txtEmail_Id.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();
                txtValid_From.Text = ds.Tables[0].Rows[0]["Valid_From"].ToString();
                txtValid_To.Text = ds.Tables[0].Rows[0]["Valid_To"].ToString();
                //txtAccess_Level.Text = ds.Tables[0].Rows[0]["Access_Level"].ToString();
                if (ds.Tables[0].Rows[0]["VisitorImage_Blob"].ToString() != "")
                {
                    imgStaff.Visible = true;
                    //imgStaff.ImageUrl = ds.Tables[0].Rows[0]["Emp_Photo"].ToString();
                    byte[] hash1 = (byte[])ds.Tables[0].Rows[0]["VisitorImage_Blob"];
                    imgStaff.ImageUrl = "data:image;base64," + Convert.ToBase64String(hash1);
                }
                txtCheck_In.Text = ds.Tables[0].Rows[0]["Check_In"].ToString();
                txtCheck_Out.Text = ds.Tables[0].Rows[0]["Check_Out"].ToString();
                txtCreatedBy.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtLastUpdatedBy.Text = ds.Tables[0].Rows[0]["LastUpdatedBy"].ToString();
                txtRecordStatus.Text = ds.Tables[0].Rows[0]["RecordStatus"].ToString();
            }
        }
        //Method for View Mode
        protected void ViewMode()
        {
            divgrid.Visible = false;
            divView.Visible = true;
            lnkupdate.Visible = false;
            lnksave.Visible = false;
            lnkAdd.Visible = false;
            txtSLN_ACS_Visitor_Info.Enabled = false;
            txtVisitor_RecId.Enabled = false;
            txtVisitor_GUID.Enabled = false;
            txtFirst_Name.Enabled = false;
            ddlVisitor_Type.Enabled = false;
            txtLast_Name.Enabled = false;
            txtCompany.Enabled = false;
            ddlVisit_Reason.Enabled = false;
            txtPhone_Number.Enabled = false;
            txtID_Number.Enabled = false;
            txtNational_ID.Enabled = false;
            txtHost_Employee_Code.Enabled = false;
            txtAccess_Card_Number.Enabled = false;
            txtEmail_Id.Enabled = false;
            txtValid_From.Enabled = false;
            txtValid_To.Enabled = false;
            ddlAccess_Level.Enabled = false;
            FileUpload1.Visible = false;
            txtCheck_In.Enabled = false;
            txtCheck_Out.Enabled = false;
            txtCreatedBy .Enabled = false;
            txtLastUpdatedBy.Enabled = false; 
            txtRecordStatus.Enabled = false;
        }

        protected void lnkEdit_menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("Visitors_Master.aspx?Id=" + txtSLN_ACS_Visitor_Info.Text + "&Type=Edit");
        }

        protected void lnkView_Menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("Visitors_Master.aspx?Id=" + txtSLN_ACS_Visitor_Info.Text + "&Type=View");
        }

        protected void lnkviewgrid_Click(object sender, EventArgs e)
        {
            Response.Redirect("Visitors_Master.aspx");
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
            Response.Redirect("Visitors_Master.aspx");
        }
       
    }
}