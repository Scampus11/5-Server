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
using System.Web.Configuration;
using SMS.BussinessLayer;
using SMS.CommonClass;

namespace SMS
{
    public partial class SMS_logo : System.Web.UI.Page
    {
        #region Common Variable Declaration
        string Image = "", DigitalIdImg = "";
        byte[] bytes = new byte[1];
        BS BS = new BS();
        DataTable DT = new DataTable();
        int Id = 0;

        #endregion

        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            Session["Sider"] = "logo";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    divgrid.Visible = false;
                    divView.Visible = true;
                    lnkupdate.Visible = true;
                    lnksave.Visible = false;
                    Id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                    GetCompanyInfo();
                }
                else if (Request.QueryString["Add"] != null)
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
        #endregion

        #region Get Company Info
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                DT = BS.SP_CompanyInfo("Get", 0, "", "", "", "", "","");
                if (sortExpression != null)
                {
                    DataView dv = DT.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gridEmployee.DataSource = dv;
                }
                else
                {
                    gridEmployee.DataSource = DT;
                }
                gridEmployee.DataBind();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        #endregion

        #region Edit | View Company Info
        protected void GetCompanyInfo()
        {

            DT = BS.SP_CompanyInfo("Edit", Id, "", "", "", "", "","");
            if (DT.Rows.Count > 0)
            {
                txtApplication_No.Text = DT.Rows[0]["timer_time"].ToString();
                txtpublic.Text = DT.Rows[0]["Public_timer"].ToString();
                txtMusteringTime.Text = DT.Rows[0]["MusteringTime"].ToString();
                txtName.Text = DT.Rows[0]["Name"].ToString();
                ImageDetails(DT.Rows[0]["Images"].ToString(), DT.Rows[0]["DigitalIdImages"].ToString());
            }
        }
        #endregion

        #region Image Details
        protected void ImageDetails(string LogoFilePath, string DigitalFilePath)
        {
            //Image For Logo
            if (LogoFilePath == "")
            {
                imgDigitalId.ImageUrl = "~/Images/images1.jpg";
            }
            else
            {
                string path = Server.MapPath(LogoFilePath);
                if (Checkpath.CheckPathExitsOrNot(path))
                {
                    imgStaff.ImageUrl = LogoFilePath;
                }
                else
                {
                    imgStaff.ImageUrl = "~/Images/images1.jpg";
                }
            }

            //Image For Digital Id
            if (DigitalFilePath == "")
            {
                imgDigitalId.ImageUrl = "~/Images/images1.jpg";
            }
            else
            {
                string path = Server.MapPath(DigitalFilePath);
                if (Checkpath.CheckPathExitsOrNot(path))
                {
                    imgDigitalId.ImageUrl = DigitalFilePath;
                }
                else
                {
                    imgDigitalId.ImageUrl = "~/Images/images1.jpg";
                }
            }
        }
        #endregion

        #region Edit Company Info
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_logo.aspx?Id=" + ids);
        }
        #endregion

        #region upset Company Info
        protected void btnsave_Click(object sender, EventArgs e)
        {
            //if (Validation())
            //{
            //    SqlCommand cmd = new SqlCommand();

            //    cmd.CommandText = "INSERT INTO [logo] ([Name] ,[Reader_Type]) VALUES ('" + txtApplication_No.Text + "','" + ddlType.SelectedItem.Text + "')";

            //    cmd.Connection = con;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Insert Successfully!!');", true);
            //    con.Open();
            //    cmd.ExecuteNonQuery();
            //    Response.Redirect("SMS_logo.aspx");
            //    con.Close();
            //}

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (lblimg.Text != "")
                {
                    if (txtApplication_No.Text == "")
                    {
                        txtApplication_No.Text = "0";
                    }
                    if (txtpublic.Text == "")
                    {
                        txtpublic.Text = "0";
                    }
                    if (txtMusteringTime.Text == "")
                    {
                        txtMusteringTime.Text = "0";
                    }
                    ImageUpload();
                    if (Request.QueryString["Id"] != null)
                    {
                        Id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                    }
                    BS.SP_CompanyInfo("Update", Id, txtApplication_No.Text, txtpublic.Text, txtMusteringTime.Text, txtName.Text, Image, DigitalIdImg);
                    Response.Redirect("SMS_logo.aspx");
                }
                else
                {
                    lblimg.Visible = true;
                }
            }
        }
        #endregion

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
            Label Ids = (Label)gvr.FindControl("lblId");
            DT = BS.SP_CompanyInfo("Delete", 0, txtApplication_No.Text, txtpublic.Text,txtMusteringTime.Text, txtName.Text, Image, DigitalIdImg);
            Response.Redirect("SMS_logo.aspx");
        }
        protected bool Validation()
        {
            if (FileUpload1.HasFile)
            {
                string ext = System.IO.Path.GetExtension(FileUpload1.FileName.ToString());
                if ((ext.ToLower() == ".jpg") || (ext.ToLower() == ".eps") || (ext.ToLower() == ".jpeg") || (ext.ToLower() == ".gif") || (ext.ToLower() == ".png") || (ext.ToLower() == ".bmp"))
                {
                    lblf1.Visible = false;
                }
                else
                {
                    lblf1.Visible = true;
                    return false;
                }
            }
            if (FileDigitalIdUpload.HasFile)
            {
                string ext = System.IO.Path.GetExtension(FileDigitalIdUpload.FileName.ToString());
                if ((ext.ToLower() == ".jpg") || (ext.ToLower() == ".eps") || (ext.ToLower() == ".jpeg") || (ext.ToLower() == ".gif") || (ext.ToLower() == ".png") || (ext.ToLower() == ".bmp"))
                {
                    lblDigitalIdimgnotsuppoted.Visible = false;
                }
                else
                {
                    lblDigitalIdimgnotsuppoted.Visible = true;
                    return false;
                }
            }
            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_logo.aspx");
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
        protected void ImageUpload()
        {
            if (FileUpload1.HasFile)
            {
                string str = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/ICON/" + str));
                Image = "~/Images/ICON/" + str.ToString();
                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                //Save the Byte Array as File.
                //Image = "~/Images/ICON/" + Path.GetFileName(FileUpload1.FileName);
                File.WriteAllBytes(Server.MapPath(Image), bytes);
            }
            if (FileDigitalIdUpload.HasFile)
            {
                string str = FileDigitalIdUpload.FileName;
                FileDigitalIdUpload.PostedFile.SaveAs(Server.MapPath("~/Images/ICON/" + str));
                DigitalIdImg = "~/Images/ICON/" + str.ToString();
                Stream fs = FileDigitalIdUpload.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                //Save the Byte Array as File.
                //Image = "~/Images/ICON/" + Path.GetFileName(FileDigitalIdUpload.FileName);
                File.WriteAllBytes(Server.MapPath(DigitalIdImg), bytes);
            }
        }
        #region RowDataBound Method
        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //check Student path Image
                    Label path = e.Row.FindControl("lblpath") as Label;
                    Image Studentpath = e.Row.FindControl("imgStudent") as Image;
                    Image imgdefault = e.Row.FindControl("imgdefault") as Image;
                    if (path.Text != "" && path.Text != "null")
                    {
                        if (Checkpath.CheckPathExitsOrNot(Server.MapPath(path.Text)))
                        {
                            Studentpath.Visible = true;
                            imgdefault.Visible = false;
                        }
                        else
                        {
                            Studentpath.Visible = false;
                            imgdefault.Visible = true;
                        }
                    }
                    else
                    {
                        Studentpath.Visible = false;
                        imgdefault.Visible = true;
                    }
                    //check Digital Id path Image
                    Label DGIpath = e.Row.FindControl("lblDGIpath") as Label;
                    Image DGIpathMain = e.Row.FindControl("imgDGIpathMain") as Image;
                    Image imgDGIdefault = e.Row.FindControl("imgDGIdefault") as Image;
                    if (DGIpath.Text != "" && DGIpath.Text != "null")
                    {
                        if (Checkpath.CheckPathExitsOrNot(Server.MapPath(DGIpath.Text)))
                        {
                            DGIpathMain.Visible = true;
                            imgDGIdefault.Visible = false;
                        }
                        else
                        {
                            DGIpathMain.Visible = false;
                            imgDGIdefault.Visible = true;
                        }
                    }
                    else
                    {
                        DGIpathMain.Visible = false;
                        imgDGIdefault.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        #endregion
    }
}