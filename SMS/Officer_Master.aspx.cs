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

namespace SMS
{
    public partial class Officer_Master : System.Web.UI.Page
    {
        string Image = "";
        byte[] bytes = new byte[1];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Sider"] = "Officer";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null && Request.QueryString["Type"].ToString() == "Edit")
                {
                    EditMode();
                    lnkviewgrid.Visible = true;
                    lnkView_Menu.Visible = true;
                    lnkEdit_menu.Visible = false;
                }
                else if (Request.QueryString["Id"] != null && Request.QueryString["Type"].ToString() == "View")
                {
                    EditMode();
                    ViewMode();
                    lnkviewgrid.Visible = true;
                    lnkView_Menu.Visible = false;
                    lnkEdit_menu.Visible = true;
                }
                else if (Request.QueryString["Insert"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lnkupdate.Visible = false;
                    lnksave.Visible = true;
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
            Response.Redirect("Officer_Master.aspx?Insert=Insert Department");
        }
        //Save Staff Details Logic
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                ImageUpload();
                SqlCommand cmd = new SqlCommand("SP_SMS_Officer_upset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Insert");
                cmd.Parameters.AddWithValue("@Application_No", txtApplication_No.Text);
                cmd.Parameters.AddWithValue("@SL_No", txtSL_No.Text);
                cmd.Parameters.AddWithValue("@Staff_Id", txtStudentID.Text);
                cmd.Parameters.AddWithValue("@UID", txtUID.Text);
                cmd.Parameters.AddWithValue("@Full_Name", txtFull_Name.Text);
                cmd.Parameters.AddWithValue("@Gender", txtgender.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
                cmd.Parameters.AddWithValue("@Job_Title", txtJob_Title.Text);
                cmd.Parameters.AddWithValue("@Emp_Photo", Image);
                //cmd.Parameters.AddWithValue("@Signature", txtSignature.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@ID_no", txtID_no.Text);
                cmd.Parameters.AddWithValue("@Issue_Date", txtIssue_Date.Text);
                cmd.Parameters.AddWithValue("@Isactive", txtIsactive.Text);
                cmd.Parameters.AddWithValue("@cardstatus", ddlCardstatus.SelectedValue);
                cmd.Parameters.AddWithValue("@cardnumber", txtCard_no.Text);
                cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                cmd.Parameters.AddWithValue("@Id", 0);
                cmd.Parameters.AddWithValue("@hash", bytes);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("Officer_Master.aspx");
                con.Close();
            }
        }
        //Update Staff Details Logic
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                ImageUpload();
                SqlCommand cmd = new SqlCommand("SP_SMS_Officer_upset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Update");
                cmd.Parameters.AddWithValue("@Application_No", txtApplication_No.Text);
                cmd.Parameters.AddWithValue("@SL_No", txtSL_No.Text);
                cmd.Parameters.AddWithValue("@Staff_Id", txtStudentID.Text);
                cmd.Parameters.AddWithValue("@UID", txtUID.Text);
                cmd.Parameters.AddWithValue("@Full_Name", txtFull_Name.Text);
                cmd.Parameters.AddWithValue("@Gender", txtgender.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
                cmd.Parameters.AddWithValue("@Job_Title", txtJob_Title.Text);
                cmd.Parameters.AddWithValue("@Emp_Photo", Image);
                //cmd.Parameters.AddWithValue("@Signature", txtSignature.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@ID_no", txtID_no.Text);
                cmd.Parameters.AddWithValue("@Issue_Date", txtIssue_Date.Text);
                cmd.Parameters.AddWithValue("@Isactive", txtIsactive.Text);
                cmd.Parameters.AddWithValue("@cardstatus", ddlCardstatus.SelectedValue);
                cmd.Parameters.AddWithValue("@cardnumber", txtCard_no.Text);
                cmd.Parameters.AddWithValue("@Email_Id", txtEmail_Id.Text);
                cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Request.QueryString["Id"]));
                cmd.Parameters.AddWithValue("@hash", bytes);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("Officer_Master.aspx");
                con.Close();
            }
        }
        //Get Staff Details Logic
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "select * from Officer_Master WHERE Staff_Id LIKE '%' + @Search + '%' or Id LIKE '%' + @Search + '%' OR Full_Name LIKE '%' + @Search + '%' OR Email_Id LIKE '%' + @Search + '%' OR Job_Title LIKE '%' + @Search + '%' OR Department  LIKE '%' + @Search + '%'";
                SqlCommand cmd = new SqlCommand("SP_GetStaffData", con);
                cmd.CommandType = CommandType.StoredProcedure;
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
            if (txtStudentID.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Staff Id')</script>");
                return false;
            }
            if (txtFull_Name.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Full Name')</script>");
                return false;
            }
            if (txtEmail_Id.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter Email Id')</script>");
                return false;
            }
            if (txtpassword.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter PassWord')</script>");
                return false;
            }
            if (lnksave.Visible == true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select Staff_Id from Officer_Master";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Staff_Id"].ToString() == txtStudentID.Text)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Staff Id Already Exits')</script>");
                            return false;
                        }
                    }
                }
            }
            else
            {

            }
            return true;
        }
        //Image upload Logic
        protected void ImageUpload()
        {

            if (FileUpload1.HasFile)
            {
                string str = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/StaffImages/" + txtStudentID.Text.Replace("/", "_") + "_" + str));
                Image = "~/Images/StaffImages/" + txtStudentID.Text.Replace("/", "_") + "_" + str.ToString();
                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);

                //Save the Byte Array as File.
                Image = "/Images/StaffImages/" + Path.GetFileName(FileUpload1.FileName);
                File.WriteAllBytes(Server.MapPath(Image), bytes);
            }
        }
        //Edit Staff Details Logic
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Officer_Master.aspx?Id=" + ids + "&Type=Edit");
        }
        //View Staff Details Logic
        protected void linkView_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Officer_Master.aspx?Id=" + ids + "&Type=View");
        }
        //Method for Edit Mode
        protected void EditMode()
        {
            divgrid.Visible = false;
            divView.Visible = true;
            lnkupdate.Visible = true;
            lnksave.Visible = false;
            lnkAdd.Visible = false;
            txtStudentID.Enabled = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Officer_Master where Id= '" + Request.QueryString["Id"].ToString() + "'";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                txtStudentID.Text = ds.Tables[0].Rows[0]["Staff_Id"].ToString();
                txtApplication_No.Text = ds.Tables[0].Rows[0]["Application_No"].ToString();
                txtSL_No.Text = ds.Tables[0].Rows[0]["SL_No"].ToString();
                txtUID.Text = ds.Tables[0].Rows[0]["UID"].ToString();
                txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                txtFull_Name.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
                txtgender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
                txtDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                txtJob_Title.Text = ds.Tables[0].Rows[0]["Job_Title"].ToString();
                //txtSignature.Text = ds.Tables[0].Rows[0]["Signature"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtID_no.Text = ds.Tables[0].Rows[0]["ID_no"].ToString();
                txtIssue_Date.Text = ds.Tables[0].Rows[0]["Issue_Date"].ToString();
                txtEmail_Id.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();
                txtpassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
                txtCard_no.Text = ds.Tables[0].Rows[0]["cardnumber"].ToString();
                txtIsactive.Text = ds.Tables[0].Rows[0]["Isactive"].ToString();
                if (ds.Tables[0].Rows[0]["StaffImage_Blob"].ToString() != "")
                {
                    imgStaff.Visible = true;
                    //imgStaff.ImageUrl = ds.Tables[0].Rows[0]["Emp_Photo"].ToString();
                    byte[] hash1 = (byte[])ds.Tables[0].Rows[0]["StaffImage_Blob"];
                    imgStaff.ImageUrl = "data:image;base64," + Convert.ToBase64String(hash1);
                }
                ddlCardstatus.ClearSelection();
                ddlCardstatus.Items.FindByValue(ds.Tables[0].Rows[0]["Cardstatus"].ToString()).Selected = true;
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
            txtStudentID.Enabled = false;
            txtApplication_No.Enabled = false;
            txtSL_No.Enabled = false;
            txtUID.Enabled = false;
            txtDOB.Enabled = false;
            txtFull_Name.Enabled = false;
            txtgender.Enabled = false;
            txtDepartment.Enabled = false;
            txtJob_Title.Enabled = false;
            txtSignature.Enabled = false;
            txtAddress.Enabled = false;
            txtID_no.Enabled = false;
            txtIssue_Date.Enabled = false;
            txtEmail_Id.Enabled = false;
            txtpassword.Enabled = false;
            txtCard_no.Enabled = false;
            txtIsactive.Enabled = false;
            FileUpload1.Visible = false;
        }

        protected void lnkEdit_menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("Officer_Master.aspx?Id=" + lblId.Text + "&Type=Edit");
        }

        protected void lnkView_Menu_Click(object sender, EventArgs e)
        {
            Response.Redirect("Officer_Master.aspx?Id=" + lblId.Text + "&Type=View");
        }

        protected void lnkviewgrid_Click(object sender, EventArgs e)
        {
            Response.Redirect("Officer_Master.aspx");
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
            Response.Redirect("Officer_Master.aspx");
        }
    }
}