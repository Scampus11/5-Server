﻿using System;
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

namespace SMS
{
    public partial class SMS_Food_Video_Master : System.Web.UI.Page
    {
        string Image = "";
        string Photopath = "";
        string filePath = "";
        string format = "";
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
            Session["Sider"] = "Video Master";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    rbshow.Checked = true;
                    BindCanteen();
                    BindSession();
                    if (Request.QueryString["Id"] != null)
                    {
                        divgrid.Visible = false;
                        divView.Visible = true;
                        lnkupdate.Visible = true;
                        lnksave.Visible = false;
                        lnkAdd.Visible = false;
                        con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "select * from tbl_Food_Video_Master where VideoId= '" + Request.QueryString["Id"].ToString() + "'";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();


                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtVideoName.Text = ds.Tables[0].Rows[0]["VideoName"].ToString();
                            txtVideoDescription.Text = ds.Tables[0].Rows[0]["VideoDescription"].ToString();
                            if (ds.Tables[0].Rows[0]["VideoStatus"].ToString() == "1")
                            {
                                rbshow.Checked = true;
                            }
                            else
                            {
                                rbside.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["VideoPath"].ToString() == "")
                            {
                                VideoPlayer.Src = "~/Images/images1.jpg";
                            }
                            else
                            {
                                VideoPlayer.Src = ds.Tables[0].Rows[0]["VideoPath"].ToString();
                            }
                            ddlcanteen.ClearSelection();
                            ddlcanteen.Items.FindByValue(ds.Tables[0].Rows[0]["CanteenId"].ToString()).Selected = true;
                            ddlsession.ClearSelection();
                            ddlsession.Items.FindByValue(ds.Tables[0].Rows[0]["SessionId"].ToString()).Selected = true;
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
                SqlCommand cmd = new SqlCommand("SP_GetVideoData", con);
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
            Response.Redirect("SMS_Food_Video_Master.aspx?Insert=Insert Video");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;

            Response.Redirect("SMS_Food_Video_Master.aspx?Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    string FoodStatus = "0";
                    if (rbshow.Checked)
                    {
                        FoodStatus = "1";
                    }
                    else
                    {
                        FoodStatus = "0";
                    }
                    ImageUpload();
                    lblValidVideoname.Visible = false;
                    lblValidCanteenName.Visible = false;
                    SqlCommand cmd = new SqlCommand("SP_SMS_Video_Video_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Insert");
                    cmd.Parameters.AddWithValue("@VideoName", txtVideoName.Text);
                    cmd.Parameters.AddWithValue("@VideoDescription", txtVideoDescription.Text);
                    cmd.Parameters.AddWithValue("@VideoPath", Image);
                    cmd.Parameters.AddWithValue("@VideoStatus", FoodStatus);
                    cmd.Parameters.AddWithValue("@CanteenId", ddlcanteen.SelectedValue);
                    cmd.Parameters.AddWithValue("@SessionId", ddlsession.SelectedValue);
                    cmd.Parameters.AddWithValue("@Format", format);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect("SMS_Food_Video_Master.aspx");
                    con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }

        }
        protected void ImageUpload()
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string str = FileUpload1.FileName;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Video/" + str));
                    Image = "~/Video/" + str.ToString();
                    string ext = System.IO.Path.GetExtension(FileUpload1.FileName.ToString());
                    string FileName1 = Guid.NewGuid().ToString();
                    string FileName = System.IO.Path.GetFileName(FileUpload1.FileName.ToString());
                    string Imagepath1 = Server.MapPath("~\\" + "Video");
                    FileUpload1.SaveAs(Imagepath1 + "\\" + FileName1 + ext);
                    string pathForDB = "Video";
                    Photopath = "~\\" + pathForDB + "\\" + FileName1 + ext;
                    format = ext;
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    string FoodStatus = "0";
                    if (rbshow.Checked)
                    {
                        FoodStatus = "1";
                    }
                    else
                    {
                        FoodStatus = "0";
                    }
                    ImageUpload();
                    lblValidVideoname.Visible = false;
                    lblValidCanteenName.Visible = false;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_SMS_Video_Video_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Update");
                    cmd.Parameters.AddWithValue("@VideoId", Request.QueryString["Id"].ToString());
                    cmd.Parameters.AddWithValue("@VideoName", txtVideoName.Text);
                    cmd.Parameters.AddWithValue("@VideoDescription", txtVideoDescription.Text);
                    cmd.Parameters.AddWithValue("@VideoPath", Image);
                    cmd.Parameters.AddWithValue("@VideoStatus", FoodStatus);
                    cmd.Parameters.AddWithValue("@CanteenId", ddlcanteen.SelectedValue);
                    cmd.Parameters.AddWithValue("@SessionId", ddlsession.SelectedValue);
                    cmd.Parameters.AddWithValue("@Format", format);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Response.Redirect("SMS_Food_Video_Master.aspx");
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
                cmd.CommandText = "Delete from tbl_Food_Video_Master where VideoId= '" + Ids.Text + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                FillGrid();
                Response.Redirect("SMS_Food_Video_Master.aspx");
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected bool Validation()
        {

            if (ddlcanteen.SelectedValue == "0")
            {
                lblValidCanteenName.Visible = true;
                lblValidSessionName.Visible = false;
                lblValidVideoname.Visible = false;
                lblFoodImages.Visible = false;
                ddlcanteen.Focus();
                return false;
            }
            if (ddlsession.SelectedValue == "0")
            {
                lblValidCanteenName.Visible = false;
                lblValidSessionName.Visible = true;
                lblValidVideoname.Visible = false;
                lblFoodImages.Visible = false;
                ddlsession.Focus();
                return false;
            }
            if (txtVideoName.Text == "")
            {
                lblValidCanteenName.Visible = false;
                lblValidSessionName.Visible = false;
                lblValidVideoname.Visible = true;
                lblFoodImages.Visible = false;
                txtVideoName.Focus();
                return false;
            }
            if (FileUpload1.HasFile)
            {
                string ext = System.IO.Path.GetExtension(FileUpload1.FileName.ToString());
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext==".bmp")
                {
                    lblValidCanteenName.Visible = false;
                    lblValidSessionName.Visible = false;
                    lblValidVideoname.Visible = false;
                    lblFoodImages.Visible = true;
                    lblFoodImages.Focus();
                    return false;
                }
            }
            else
            {
                if (Request.QueryString["Id"] == null)
                {
                    lblValidCanteenName.Visible = false;
                    lblValidSessionName.Visible = false;
                    lblValidVideoname.Visible = false;
                    lblFoodImages.Visible = true;
                    lblFoodImages.Focus();
                    return false;
                }
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_Food_Video_Master.aspx");
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
        protected void BindSession()
        {
            DataTable dtCollege = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "GetSession");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCollege);
            if (dtCollege.Rows.Count > 0)
            {
                ddlsession.DataSource = dtCollege;
                ddlsession.DataTextField = "Name";
                ddlsession.DataValueField = "Id";
                ddlsession.DataBind();
                ddlsession.Items.Insert(0, new ListItem("--Select Session--", "0"));
            }
            else
            {
                ddlsession.DataSource = null;
                ddlsession.DataBind();
                ddlsession.Items.Insert(0, new ListItem("--Select Session--", "0"));
            }
        }

        protected void BindCanteen()
        {
            DataTable dtCollege = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "BindCanteen");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCollege);
            if (dtCollege.Rows.Count > 0)
            {
                ddlcanteen.DataSource = dtCollege;
                ddlcanteen.DataTextField = "Name";
                ddlcanteen.DataValueField = "Id";
                ddlcanteen.DataBind();
                ddlcanteen.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
            }
            else
            {
                ddlcanteen.DataSource = null;
                ddlcanteen.DataBind();
                ddlcanteen.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
            }
        }
    }
}