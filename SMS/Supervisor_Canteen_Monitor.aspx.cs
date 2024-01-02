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
using AjaxControlToolkit;

namespace SMS
{
    public partial class Supervisor_Canteen_Monitor : System.Web.UI.Page
    {
        DataTable dt_Grid = new DataTable();
        DataTable dt_Grid1 = new DataTable();
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        string FromDate = "", ToDate = "";
        //string SessionId = "", CanteenId = "", CurrentDate = "", StartTime = "", EndTime = "",CanteenName="",SessionName="";
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            Session["Sider"] = "SuperVisor Canteen Monitor";

            if (!Page.IsPostBack)
            {
                try
                {
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    // divgrid.Attributes.Add("style", "display:None");
                    if (Request.QueryString["Id"] != null)
                    {
                    }
                    else
                    {

                        //HttpContext.Current.Session["Height"] = height;
                        SetTimer();
                        ModalPopupExtender ModalPopupExtenderAPIs = (ModalPopupExtender)Page.Master.FindControl("ModalPopupExtenderAPIs");
                        ModalPopupExtenderAPIs.Hide();
                        ModalPopupExtender2.Show();
                        BindCanteen();
                        BindSession();

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    LogFile.LogError(ex);
                }
            }
        }
        protected void SetTimer()
        {
            SqlCommand cmd = new SqlCommand("SP_GetLogoData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                Timer1.Interval = Convert.ToInt32(ds.Rows[0]["timer_time"].ToString());
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
                
                TimeSpan start = DateTime.Parse(hidStartTime.Value).TimeOfDay;
                TimeSpan end = DateTime.Parse(hidEndTime.Value).TimeOfDay;
                FromDate = hidCurrentDate.Value + ' ' + start;
                ToDate = hidCurrentDate.Value + ' ' + end;
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get Canteen Details");
                cmd.Parameters.AddWithValue("@Session", hidSessionId.Value);
                cmd.Parameters.AddWithValue("@AG_Id", hidCanteenId.Value);
                cmd.Parameters.AddWithValue("@Date", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@Search", "");
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
                if (dt_Grid.Rows.Count > 0)
                {
                    lnkCopy.Visible = true;
                    divstudent.Visible = true;
                    Temp_Supervisor();
                    //Session["SuperVisorStudentId"] = dt_Grid.Rows[0]["Id"].ToString();
                    DefaultGrid(dt_Grid.Rows[0]["Id"].ToString());
                }
                else
                {
                    lnkCopy.Visible = false;
                    divstudent.Visible = false;
                }

            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void Temp_Supervisor()
        {
            SqlCommand cmd1 = new SqlCommand("SP_GetSession", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("@Flag", "Get Supervisor");
            cmd1.Parameters.AddWithValue("@Session", hidSessionId.Value);
            cmd1.Parameters.AddWithValue("@AG_Id", hidCanteenId.Value);
            cmd1.Parameters.AddWithValue("@Date", hidStartTime.Value);
            cmd1.Parameters.AddWithValue("@ToDate", hidEndTime.Value);
            cmd1.Parameters.AddWithValue("@Search", hidCurrentDate.Value);
            cmd1.Parameters.AddWithValue("@StudentId", "");
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            da.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "UpdateSupervisor");
                cmd.Parameters.AddWithValue("@Session", hidSessionId.Value);
                cmd.Parameters.AddWithValue("@AG_Id", hidCanteenId.Value);
                cmd.Parameters.AddWithValue("@Date", hidStartTime.Value);
                cmd.Parameters.AddWithValue("@ToDate", hidEndTime.Value);
                cmd.Parameters.AddWithValue("@Search", hidCurrentDate.Value);
                cmd.Parameters.AddWithValue("@StudentId", "");
                cmd.Parameters.AddWithValue("@UserId", Session["UserName"]);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                aPublic.HRef = WebConfigurationManager.AppSettings["HostName"].ToString() + "Canteen_Public_Monitor.aspx?Id=" + ds1.Rows[0]["GenerateId"].ToString();
                aPublic.Target = "_blank";
                aPublic.InnerText = WebConfigurationManager.AppSettings["HostName"].ToString() + "Canteen_Public_Monitor.aspx?Id=" + ds1.Rows[0]["GenerateId"].ToString();
            }
            else
            {
                //if (ddlCanteen.SelectedValue == Convert.ToString(ds1.Rows[0]["Canteen"]) && ddlsession.SelectedValue == Convert.ToString(ds1.Rows[0]["Session"])
                //    && txtStartTime.Text == ds1.Rows[0]["StartDate"].ToString() && txtEndTime.Text == ds1.Rows[0]["EndDate"].ToString()
                //    && txtDate.Text == ds1.Rows[0]["Date"].ToString())
                //{
                Random ran = new Random();
                String b = "abcdefghijklmnopqrstuvwxyz0123456789";
                int length = 6;
                String random = "";
                for (int i = 0; i < length; i++)
                {
                    int a = ran.Next(26);
                    random = random + b.ElementAt(a);
                }
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "InsertSupervisor");
                cmd.Parameters.AddWithValue("@Session", hidSessionId.Value);
                cmd.Parameters.AddWithValue("@AG_Id", hidCanteenId.Value);
                cmd.Parameters.AddWithValue("@Date", hidStartTime.Value);
                cmd.Parameters.AddWithValue("@ToDate", hidEndTime.Value);
                cmd.Parameters.AddWithValue("@Search", hidCurrentDate.Value);
                cmd.Parameters.AddWithValue("@StudentId", random);
                cmd.Parameters.AddWithValue("@UserId", Session["UserName"]);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                aPublic.HRef = WebConfigurationManager.AppSettings["HostName"].ToString() + "Canteen_Public_Monitor.aspx?Id=" + random;
                aPublic.Target = "_blank";
                aPublic.InnerText = WebConfigurationManager.AppSettings["HostName"].ToString() + "Canteen_Public_Monitor.aspx?Id=" + random;
                //}
            }
        }
        protected void DefaultGrid(string StudentID)
        {
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "Get Student Details");
            cmd.Parameters.AddWithValue("@Session", hidSessionId.Value);
            cmd.Parameters.AddWithValue("@AG_Id", hidCanteenId.Value);
            cmd.Parameters.AddWithValue("@Date", "");
            cmd.Parameters.AddWithValue("@ToDate", "");
            cmd.Parameters.AddWithValue("@Search", "");
            cmd.Parameters.AddWithValue("@StudentId", StudentID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                //Student Info
                lblStudentId_S.Text = ds.Rows[0]["StudentId"].ToString();
                lblStudent_Name_S.Text = ds.Rows[0]["Student_Name"].ToString();
                lblPunch_Datetime_S.Text = ds.Rows[0]["Punch_Datetime"].ToString();
                lblDept_S.Text = ds.Rows[0]["Department"].ToString();
                lblCardnumber_S.Text = ds.Rows[0]["Cardnumber"].ToString();
                if (ds.Rows[0]["Access_Code"].ToString() == "Access Granted")
                {
                    tmgstudent_S.Attributes.Add("Style", "width: 225px; height: 225px;border: 5px solid greenyellow !important");
                    lblgrantmsg.Text = "Access Granted";
                    lblgrantmsg.Attributes.Add("style", "color:green;font-size:25px;margin-left:30px");
                }
                else
                {
                    tmgstudent_S.Attributes.Add("Style", "width: 225px; height: 225px;border: 5px solid Red !important");
                    lblgrantmsg.Text = "Access Denied";
                    lblgrantmsg.Attributes.Add("style", "color:red;font-size:25px;margin-left:30px");
                }
                if (ds.Rows[0]["StudentImages"].ToString() != "" && ds.Rows[0]["StudentImages"].ToString() != null)
                {
                    tmgstudent_S.ImageUrl = ds.Rows[0]["StudentImages"].ToString();
                }
                else
                {
                    tmgstudent_S.ImageUrl = "~/Images/images1.jpg";
                }
            }

            //Canteen Info
            lblCanteenName.Text = hidCanteenName.Value;
            lblCanteenType.Text = hidSessionName.Value;
            lblCanteenFromTime.Text = hidStartTime.Value;
            lblCanteenToTime.Text = hidEndTime.Value;

            //Access Count Details
            TimeSpan start = DateTime.Parse(txtStartTime.Text).TimeOfDay;
            TimeSpan end = DateTime.Parse(txtEndTime.Text).TimeOfDay;
            FromDate = txtDate.Text + ' ' + start;
            ToDate = txtDate.Text + ' ' + end;
            SqlCommand cmdAccessCount = new SqlCommand("SP_GetSession", con);
            cmdAccessCount.CommandType = CommandType.StoredProcedure;
            cmdAccessCount.Connection = con;
            cmdAccessCount.Parameters.AddWithValue("@Flag", "Get AccessCount Details");
            cmdAccessCount.Parameters.AddWithValue("@Session", hidSessionId.Value);
            cmdAccessCount.Parameters.AddWithValue("@AG_Id", hidCanteenId.Value);
            cmdAccessCount.Parameters.AddWithValue("@Date", FromDate);
            cmdAccessCount.Parameters.AddWithValue("@ToDate", ToDate);
            cmdAccessCount.Parameters.AddWithValue("@Search", hidEndTime.Value);
            cmdAccessCount.Parameters.AddWithValue("@StudentId", StudentID);
            SqlDataAdapter daAccessCount = new SqlDataAdapter(cmdAccessCount);
            DataTable dsdaAccessCount = new DataTable();
            daAccessCount.Fill(dsdaAccessCount);
            if (dsdaAccessCount.Rows.Count > 0)
            {
                lblDenied.Text = dsdaAccessCount.Rows[0]["deniedMembers"].ToString();
                lblServed.Text = dsdaAccessCount.Rows[0]["AccessMembers"].ToString();
                lblPending.Text = dsdaAccessCount.Rows[0]["PendingMembers"].ToString();
                lblTotal.Text = dsdaAccessCount.Rows[0]["AllowedCount"].ToString();

            }

            divstudent.Visible = true;

        }

        protected void FillGrid2(string sortExpression1 = null)
        {
            try
            {
                TimeSpan start = DateTime.Parse(hidStartTime.Value).TimeOfDay;
                TimeSpan end = DateTime.Parse(hidEndTime.Value).TimeOfDay;
                FromDate = hidCurrentDate.Value + ' ' + start;
                ToDate = hidCurrentDate.Value + ' ' + end;
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get DeniedMembers");
                cmd.Parameters.AddWithValue("@Session", hidSessionId.Value);
                cmd.Parameters.AddWithValue("@AG_Id", hidCanteenId.Value);
                cmd.Parameters.AddWithValue("@Date", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@Search", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(dt_Grid1);
                if (sortExpression1 != null)
                {
                    DataView dv = dt_Grid1.AsDataView();
                    this.SortDirection1 = this.SortDirection1 == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression1 + " " + this.SortDirection1;
                    GridView1.DataSource = dv;
                }
                else
                {
                    GridView1.DataSource = dt_Grid1;
                }
                GridView1.DataBind();
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
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmployee.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        protected void OnPageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid2();
        }
        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {
            FillGrid(e.SortExpression);
        }
        protected void OnSorting1(object sender, GridViewSortEventArgs e)
        {
            FillGrid2(e.SortExpression);
        }
        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }
        private string SortDirection1
        {
            get { return ViewState["SortDirection1"] != null ? ViewState["SortDirection1"].ToString() : "ASC"; }
            set { ViewState["SortDirection1"] = value; }
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
                ddlCanteen.DataSource = dtCollege;
                ddlCanteen.DataTextField = "Name";
                ddlCanteen.DataValueField = "Id";
                ddlCanteen.DataBind();
                ddlCanteen.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
            }
            else
            {
                ddlCanteen.DataSource = null;
                ddlCanteen.DataBind();
                ddlCanteen.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
            }
        }

        protected void lnkGo_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                grid1.Attributes.Add("style", "overflow-x: auto;height: " + hidValue.Value + "px");
                grid2.Attributes.Add("style", "overflow-x: auto;height: " + hidValue.Value + "px");
                // divAdvanceSearch.Attributes.Add("style","display:None");
                //  divgrid.Attributes.Add("style", "display:inline");
                lblValidcanteen.Visible = false;
                lblValidsession.Visible = false;
                hidCanteenId.Value = ddlCanteen.SelectedValue;
                hidCurrentDate.Value = txtDate.Text;
                hidStartTime.Value = txtStartTime.Text;
                hidEndTime.Value = txtEndTime.Text;
                hidSessionId.Value = ddlsession.SelectedValue;
                hidCanteenName.Value= ddlCanteen.SelectedItem.Text;
                hidSessionName.Value = ddlsession.SelectedItem.Text;
                FillGrid();
                FillGrid2();
                ModalPopupExtender2.Hide();

            }
        }

        protected void lblStudentId_Click(object sender, EventArgs e)
        {
            LinkButton lnkStudentId = sender as LinkButton;
            GridViewRow gridrow = lnkStudentId.NamingContainer as GridViewRow;
            Label lblStudentId = gridrow.FindControl("lblID") as Label;
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get Student Details");
                cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@AG_Id", ddlCanteen.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", "");
                cmd.Parameters.AddWithValue("@ToDate", "");
                cmd.Parameters.AddWithValue("@Search", "");
                cmd.Parameters.AddWithValue("@StudentId", lblStudentId.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    lblStudentId_S.Text = ds.Rows[0]["StudentId"].ToString();
                    lblStudent_Name_S.Text = ds.Rows[0]["Student_Name"].ToString();
                    lblPunch_Datetime_S.Text = ds.Rows[0]["Punch_Datetime"].ToString();
                    lblCanteenName.Text = ddlCanteen.SelectedItem.Text;
                    lblCanteenType.Text = ddlsession.SelectedItem.Text;
                    lblDept_S.Text = ds.Rows[0]["Department"].ToString();
                    lblCardnumber_S.Text = ds.Rows[0]["Cardnumber"].ToString();
                    lblCanteenFromTime.Text = ds.Rows[0]["Start_Time"].ToString();
                    lblCanteenToTime.Text = ds.Rows[0]["End_Time"].ToString();
                    lblDenied.Text = ds.Rows[0]["deniedMembers"].ToString();
                    lblServed.Text = ds.Rows[0]["AccessMembers"].ToString();
                    lblPending.Text = ds.Rows[0]["PendingMembers"].ToString();
                    lblTotal.Text = ds.Rows[0]["AllowedCount"].ToString();

                    if (ds.Rows[0]["Access_Code"].ToString() == "Access Granted")
                    {
                        tmgstudent_S.Attributes.Add("Style", "width: 225px; height: 225px;border: 5px solid greenyellow !important");
                        lblgrantmsg.Text = "Access Granted";
                        lblgrantmsg.Attributes.Add("style", "color:green;font-size:25px;margin-left:30px");
                    }
                    else
                    {
                        tmgstudent_S.Attributes.Add("Style", "width: 225px; height: 225px;border: 5px solid Red !important");
                        lblgrantmsg.Text = "Access Denied";
                        lblgrantmsg.Attributes.Add("style", "color:red;font-size:25px;margin-left:30px");
                    }
                    if (ds.Rows[0]["StudentImages"].ToString() != "" && ds.Rows[0]["StudentImages"].ToString() != null)
                    {
                        tmgstudent_S.ImageUrl = ds.Rows[0]["StudentImages"].ToString();
                    }
                    else
                    {
                        tmgstudent_S.ImageUrl = "~/Images/images1.jpg";
                    }
                }
                divstudent.Visible = true;
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridEmployee, "Select$" + e.Row.RowIndex);

                if (e.Row.RowIndex == 0)
                {
                    e.Row.Attributes["style"] = "background-color:LightGrey;cursor:pointer";
                }
                else
                {
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
                //Find the DropDownList in the Row
                System.Web.UI.WebControls.Image studentimg = (e.Row.FindControl("tmgstudent") as System.Web.UI.WebControls.Image);
                string AccessCode = (e.Row.FindControl("lblAccess") as Label).Text;


                if (AccessCode == "Access Granted")
                {
                    studentimg.Attributes.Add("style", "width: 65px; height: 65px; border: 5px solid greenyellow !important");
                }
                else
                {
                    studentimg.Attributes.Add("style", "width: 65px; height: 65px; border: 5px solid Red !important");
                }
            }
        }
        protected bool Validation()
        {
            if (ddlCanteen.SelectedIndex == 0 && ddlsession.SelectedIndex == 0)
            {
                lblValidcanteen.Visible = true;
                lblValidsession.Visible = true;
                return false;
            }
            if (ddlCanteen.SelectedIndex == 0)
            {
                lblValidcanteen.Visible = true;
                lblValidsession.Visible = false;
                return false;
            }
            if (ddlsession.SelectedIndex == 0)
            {
                lblValidcanteen.Visible = false;
                lblValidsession.Visible = true;
                return false;
            }

            return true;
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //divAdvanceSearch.Attributes.Add("style", "display:None");
            //divgrid.Attributes.Add("style", "display:inline");
            lblValidcanteen.Visible = false;
            lblValidsession.Visible = false;
            FillGrid();
            FillGrid2();
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
            // divAdvanceSearch.Attributes.Add("style", "display:inline");
            //divgrid.Attributes.Add("style", "display:inline");
        }

        protected void gridEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index = GridView1.SelectedRow.RowIndex;
            //string name = GridView1.SelectedRow.Cells[0].Text;
            //string StudentId = GridView1.SelectedRow.Cells[1].Text;
            foreach (GridViewRow rowEmp in GridView1.Rows)
            {
                rowEmp.Attributes["style"] = "background-color:#FFFFFF !important";
                //rowEmp.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            foreach (GridViewRow row in gridEmployee.Rows)
            {
                if (row.RowIndex == gridEmployee.SelectedIndex)
                {
                    row.Attributes["style"] = "background-color:lightgray !important";
                    // row.BackColor = ColorTranslator.FromHtml("lightgray");
                }
                else
                {
                    row.Attributes["style"] = "background-color:#FFFFFF !important";
                    //row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
            GridViewRow row1 = gridEmployee.Rows[gridEmployee.SelectedIndex];
            Label lblStudentId = (Label)row1.FindControl("lblID"); // here link is always null.
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get Student Details");
                cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@AG_Id", ddlCanteen.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", "");
                cmd.Parameters.AddWithValue("@ToDate", "");
                cmd.Parameters.AddWithValue("@Search", "");
                cmd.Parameters.AddWithValue("@StudentId", lblStudentId.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    //Student Info
                    lblStudentId_S.Text = ds.Rows[0]["StudentId"].ToString();
                    lblStudent_Name_S.Text = ds.Rows[0]["Student_Name"].ToString();
                    lblPunch_Datetime_S.Text = ds.Rows[0]["Punch_Datetime"].ToString();
                    lblDept_S.Text = ds.Rows[0]["Department"].ToString();
                    lblCardnumber_S.Text = ds.Rows[0]["Cardnumber"].ToString();
                    if (ds.Rows[0]["Access_Code"].ToString() == "Access Granted")
                    {
                        tmgstudent_S.Attributes.Add("Style", "width: 225px; height: 225px;border: 5px solid greenyellow !important");
                        lblgrantmsg.Text = "Access Granted";
                        lblgrantmsg.Attributes.Add("style", "color:green;font-size:25px;margin-left:30px");
                    }
                    else
                    {
                        tmgstudent_S.Attributes.Add("Style", "width: 225px; height: 225px;border: 5px solid Red !important");
                        lblgrantmsg.Text = "Access Denied";
                        lblgrantmsg.Attributes.Add("style", "color:red;font-size:25px;margin-left:30px");
                    }
                    if (ds.Rows[0]["StudentImages"].ToString() != "" && ds.Rows[0]["StudentImages"].ToString() != null)
                    {
                        tmgstudent_S.ImageUrl = ds.Rows[0]["StudentImages"].ToString();
                    }
                    else
                    {
                        tmgstudent_S.ImageUrl = "~/Images/images1.jpg";
                    }
                }

                //Canteen Info
                lblCanteenName.Text = ddlCanteen.SelectedItem.Text;
                lblCanteenType.Text = ddlsession.SelectedItem.Text;
                lblCanteenFromTime.Text = txtStartTime.Text;
                lblCanteenToTime.Text = txtEndTime.Text;

                //Access Count Details
                TimeSpan start = DateTime.Parse(txtStartTime.Text).TimeOfDay;
                TimeSpan end = DateTime.Parse(txtEndTime.Text).TimeOfDay;
                FromDate = txtDate.Text + ' ' + start;
                ToDate = txtDate.Text + ' ' + end;
                SqlCommand cmdAccessCount = new SqlCommand("SP_GetSession", con);
                cmdAccessCount.CommandType = CommandType.StoredProcedure;
                cmdAccessCount.Connection = con;
                cmdAccessCount.Parameters.AddWithValue("@Flag", "Get AccessCount Details");
                cmdAccessCount.Parameters.AddWithValue("@Session", ddlsession.SelectedValue);
                cmdAccessCount.Parameters.AddWithValue("@AG_Id", ddlCanteen.SelectedValue);
                cmdAccessCount.Parameters.AddWithValue("@Date", FromDate);
                cmdAccessCount.Parameters.AddWithValue("@ToDate", ToDate);
                cmdAccessCount.Parameters.AddWithValue("@Search", txtEndTime.Text);
                cmdAccessCount.Parameters.AddWithValue("@StudentId", lblStudentId.Text);
                SqlDataAdapter daAccessCount = new SqlDataAdapter(cmdAccessCount);
                DataTable dsdaAccessCount = new DataTable();
                daAccessCount.Fill(dsdaAccessCount);
                if (dsdaAccessCount.Rows.Count > 0)
                {
                    lblDenied.Text = dsdaAccessCount.Rows[0]["deniedMembers"].ToString();
                    lblServed.Text = dsdaAccessCount.Rows[0]["AccessMembers"].ToString();
                    lblPending.Text = dsdaAccessCount.Rows[0]["PendingMembers"].ToString();
                    lblTotal.Text = dsdaAccessCount.Rows[0]["AllowedCount"].ToString();

                }
                divstudent.Visible = true;
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow rowEmp in gridEmployee.Rows)
            {
                rowEmp.Attributes["style"] = "background-color:#FFFFFF !important";
                //rowEmp.BackColor = ColorTranslator.FromHtml("");
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.Attributes["style"] = "background-color:lightgray !important";
                    //row.BackColor = ColorTranslator.FromHtml("lightgray");
                }
                else
                {
                    row.Attributes["style"] = "background-color:#FFFFFF !important";
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
            GridViewRow row1 = GridView1.Rows[GridView1.SelectedIndex];

            Label lblStudentId = (Label)row1.FindControl("lblID"); // here link is always null.
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get Student Details");
                cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@AG_Id", ddlCanteen.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", "");
                cmd.Parameters.AddWithValue("@ToDate", "");
                cmd.Parameters.AddWithValue("@Search", "");
                cmd.Parameters.AddWithValue("@StudentId", lblStudentId.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                da.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    //Student Info
                    lblStudentId_S.Text = ds.Rows[0]["StudentId"].ToString();
                    lblStudent_Name_S.Text = ds.Rows[0]["Student_Name"].ToString();
                    lblPunch_Datetime_S.Text = ds.Rows[0]["Punch_Datetime"].ToString();
                    lblDept_S.Text = ds.Rows[0]["Department"].ToString();
                    lblCardnumber_S.Text = ds.Rows[0]["Cardnumber"].ToString();
                    if (ds.Rows[0]["Access_Code"].ToString() == "Access Granted")
                    {
                        tmgstudent_S.Attributes.Add("Style", "width: 225px; height: 225px;border: 5px solid greenyellow !important");
                        lblgrantmsg.Text = "Access Granted";
                        lblgrantmsg.Attributes.Add("style", "color:green;font-size:25px;margin-left:30px");
                    }
                    else
                    {
                        tmgstudent_S.Attributes.Add("Style", "width: 225px; height: 225px;border: 5px solid Red !important");
                        lblgrantmsg.Text = "Access Denied";
                        lblgrantmsg.Attributes.Add("style", "color:red;font-size:25px;margin-left:30px");
                    }
                    if (ds.Rows[0]["StudentImages"].ToString() != "" && ds.Rows[0]["StudentImages"].ToString() != null)
                    {
                        tmgstudent_S.ImageUrl = ds.Rows[0]["StudentImages"].ToString();
                    }
                    else
                    {
                        tmgstudent_S.ImageUrl = "~/Images/images1.jpg";
                    }
                }

                //Canteen Info
                lblCanteenName.Text = ddlCanteen.SelectedItem.Text;
                lblCanteenType.Text = ddlsession.SelectedItem.Text;
                lblCanteenFromTime.Text = txtStartTime.Text;
                lblCanteenToTime.Text = txtEndTime.Text;

                //Access Count Details
                TimeSpan start = DateTime.Parse(txtStartTime.Text).TimeOfDay;
                TimeSpan end = DateTime.Parse(txtEndTime.Text).TimeOfDay;
                FromDate = txtDate.Text + ' ' + start;
                ToDate = txtDate.Text + ' ' + end;
                SqlCommand cmdAccessCount = new SqlCommand("SP_GetSession", con);
                cmdAccessCount.CommandType = CommandType.StoredProcedure;
                cmdAccessCount.Connection = con;
                cmdAccessCount.Parameters.AddWithValue("@Flag", "Get AccessCount Details");
                cmdAccessCount.Parameters.AddWithValue("@Session", ddlsession.SelectedValue);
                cmdAccessCount.Parameters.AddWithValue("@AG_Id", ddlCanteen.SelectedValue);
                cmdAccessCount.Parameters.AddWithValue("@Date", FromDate);
                cmdAccessCount.Parameters.AddWithValue("@ToDate", ToDate);
                cmdAccessCount.Parameters.AddWithValue("@Search", txtEndTime.Text);
                cmdAccessCount.Parameters.AddWithValue("@StudentId", lblStudentId.Text);
                SqlDataAdapter daAccessCount = new SqlDataAdapter(cmdAccessCount);
                DataTable dsdaAccessCount = new DataTable();
                daAccessCount.Fill(dsdaAccessCount);
                if (dsdaAccessCount.Rows.Count > 0)
                {
                    lblDenied.Text = dsdaAccessCount.Rows[0]["deniedMembers"].ToString();
                    lblServed.Text = dsdaAccessCount.Rows[0]["AccessMembers"].ToString();
                    lblPending.Text = dsdaAccessCount.Rows[0]["PendingMembers"].ToString();
                    lblTotal.Text = dsdaAccessCount.Rows[0]["AllowedCount"].ToString();

                }
                divstudent.Visible = true;
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }

        }

        protected void lnkCopy_Click(object sender, EventArgs e)
        {
            string url = String.Copy(aPublic.HRef);
            object.ReferenceEquals(url, url);
            txturl.Text = url;
            ModalPopupExtender1.Show();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }

        protected void lnkCloseicon_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "GetSession Details");
                cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@AG_Id", ddlCanteen.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", "");
                cmd.Parameters.AddWithValue("@ToDate", "");
                cmd.Parameters.AddWithValue("@Search", "");
                cmd.Parameters.AddWithValue("@StudentId", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    txtStartTime.Text = ds.Rows[0]["Start_Time"].ToString();
                    txtEndTime.Text = ds.Rows[0]["End_Time"].ToString();
                    //Session["SuperVisorCanteen"] = ddlCanteen.SelectedValue;
                    //Session["SuperVisorCanteenName"] = ddlCanteen.SelectedItem.Text;
                    //Session["SuperVisorSession"] = ddlsession.SelectedValue;
                    //Session["SuperVisorDate"] = txtDate.Text;
                    //Session["SuperVisorStartTime"] = ds.Rows[0]["Start_Time"].ToString();
                    //Session["SuperVisorEndTime"] = ds.Rows[0]["End_Time"].ToString();
                }

            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }

        }
    }
}