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
using System.Web.UI.HtmlControls;

namespace SMS
{
    public partial class Canteen_Public_Monitor : System.Web.UI.Page
    {
        DataTable ds = new DataTable();
        DataTable dsFood = new DataTable();
        DataTable dsVideo = new DataTable();
        DataTable dsQuote = new DataTable();
        DataTable ds1 = new DataTable();
        DataTable dt_Grid = new DataTable();
        DataTable dt_Grid1 = new DataTable();
        LogFile LogFile = new LogFile();
        SqlConnection con = new SqlConnection();
        string SessionFlag = "";
        string FromDate = "", ToDate = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            Connection();
            //Session["Canteen_Flag"] = null;
            //Session["Session_Id"] = null;
            //Session["CanteenName"] = null;
            //Session["CanteenCount"] = null;
            //Session["Sider"] = "Public";

            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Id"] != null)
                    {

                        //string htmlForImages = String.Empty;

                        //for (int i = 0; i < dtImages.rows.Count; i++)
                        //    htmlForImages += "<img src="" + dtImages.Rows[i][0].ToString() + "" />";

                        //ImagesDiv.InnerHtml = htmlForImages;
                        SetTimer();
                        BindData();
                        BindVideo();
                        divView.Attributes.Add("style", "height: " + hidValue.Value + "px");
                    }
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
                Timer1.Interval = Convert.ToInt32(ds.Rows[0]["Public_timer"].ToString());
                Timer2.Interval = Convert.ToInt32(ds.Rows[0]["Public_timer"].ToString());
            }
        }
        protected void BindData()
        {
            SqlCommand cmd1 = new SqlCommand("SP_GetSession", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("@Flag", "Get Supervisor Records");
            cmd1.Parameters.AddWithValue("@Session", "");
            cmd1.Parameters.AddWithValue("@AG_Id", "");
            cmd1.Parameters.AddWithValue("@Date", "");
            cmd1.Parameters.AddWithValue("@ToDate", "");
            cmd1.Parameters.AddWithValue("@Search", "");
            cmd1.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd1);

            da.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {
                TimeSpan start = DateTime.Parse(ds1.Rows[0]["StartDate"].ToString()).TimeOfDay;
                TimeSpan end = DateTime.Parse(ds1.Rows[0]["EndDate"].ToString()).TimeOfDay;
                FromDate = ds1.Rows[0]["Date"].ToString() + ' ' + start;
                ToDate = ds1.Rows[0]["Date"].ToString() + ' ' + end;
                SqlCommand cmd = new SqlCommand("SP_GetSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Flag", "Get Canteen Details");
                cmd.Parameters.AddWithValue("@Session", Convert.ToString(ds1.Rows[0]["Session"]));
                cmd.Parameters.AddWithValue("@AG_Id", Convert.ToString(ds1.Rows[0]["Canteen"]));
                cmd.Parameters.AddWithValue("@Date", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@Search", "");
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);

                da1.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    
                        DefaultGrid(ds.Rows[0]["Id"].ToString(), Convert.ToString(ds1.Rows[0]["Session"]), Convert.ToString(ds1.Rows[0]["Canteen"])
                        , ds1.Rows[0]["StartDate"].ToString(), ds1.Rows[0]["EndDate"].ToString(),FromDate,ToDate);
                }
                
            }
        }
        protected void BindVideo()
        {
            //Food Images
            SqlCommand cmd2 = new SqlCommand("SP_GetSession", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Connection = con;
            cmd2.Parameters.AddWithValue("@Flag", "Get Food Details");
            cmd2.Parameters.AddWithValue("@Session", Convert.ToString(ds1.Rows[0]["Session"]));
            cmd2.Parameters.AddWithValue("@AG_Id", Convert.ToString(ds1.Rows[0]["Canteen"]));
            cmd2.Parameters.AddWithValue("@Date", FromDate);
            cmd2.Parameters.AddWithValue("@ToDate", ToDate);
            cmd2.Parameters.AddWithValue("@Search", "");
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            string htmlForImages = String.Empty;
            da2.Fill(dsFood);
            if (dsFood.Rows.Count > 0)
            {
                for (int i = 0; i < dsFood.Rows.Count; i++)
                    htmlForImages += "<div class='dashboard-stat gray' style='background-color:lightgray;border-radius: 20px;'><img src=" + dsFood.Rows[i]["FoodImages"]
                        + " width='20' height='20' class='circle1' />&nbsp;&nbsp;<span style='font-size:25px'>"
                        + dsFood.Rows[i]["FoodName"] + "</span></div>";

                ImagesDiv.InnerHtml = htmlForImages;
            }
            //Food Video
            SqlCommand cmd3 = new SqlCommand("SP_GetSession", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Connection = con;
            cmd3.Parameters.AddWithValue("@Flag", "Get Video Details");
            cmd3.Parameters.AddWithValue("@Session", Convert.ToString(ds1.Rows[0]["Session"]));
            cmd3.Parameters.AddWithValue("@AG_Id", Convert.ToString(ds1.Rows[0]["Canteen"]));
            cmd3.Parameters.AddWithValue("@Date", FromDate);
            cmd3.Parameters.AddWithValue("@ToDate", ToDate);
            cmd3.Parameters.AddWithValue("@Search", "");
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            string Video = String.Empty;
            da3.Fill(dsVideo);

            if (dsVideo.Rows.Count > 0)
            {
                for (int i = 0; i < dsVideo.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        Video += "<source type ='video/" + dsVideo.Rows[i]["Format"] + "' class='active' src='" + dsVideo.Rows[i]["VideoPath"] + "'/>";
                    }
                    else
                    {
                        Video += "<source type ='video/" + dsVideo.Rows[i]["Format"] + "'  src='" + dsVideo.Rows[i]["VideoPath"] + "'/>";
                    }


                }
                //Video += "<video id='vid'  autoplay muted> < source src = " + dsVideo.Rows[i]["VideoPath"] + " type = 'audio/ogg' /> < source src = 'movie.ogg' type = 'video/ogg' ></ video >";
                //"<img src=" + dsVideo.Rows[i]["VideoPath"] + " width='500' height='100' />";

                myvideo.InnerHtml = Video;
            }
            //Quote
            SqlCommand cmd4 = new SqlCommand("SP_GetSession", con);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Connection = con;
            cmd4.Parameters.AddWithValue("@Flag", "Get Quote Details");
            cmd4.Parameters.AddWithValue("@Session", Convert.ToString(ds1.Rows[0]["Session"]));
            cmd4.Parameters.AddWithValue("@AG_Id", Convert.ToString(ds1.Rows[0]["Canteen"]));
            cmd4.Parameters.AddWithValue("@Date", FromDate);
            cmd4.Parameters.AddWithValue("@ToDate", ToDate);
            cmd4.Parameters.AddWithValue("@Search", "");
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            string Quote = String.Empty;
            da4.Fill(dsQuote);
            if (dsQuote.Rows.Count > 0)
            {
                for (int i = 0; i < dsQuote.Rows.Count; i++)
                    Quote += "<b><span style='font-size:25px;color:##698869;'>* " + dsQuote.Rows[i]["QuoteName"] + "</span></b>&nbsp;&nbsp;&nbsp;";

                DivQuote.InnerHtml = Quote;
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
        protected void DefaultGrid(string StudentID,string Session,string Canteen,string StartTime,string EndTime,string FromDate,string ToDate)
        {
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "Get Student Details");
            cmd.Parameters.AddWithValue("@Session", Convert.ToString(ds1.Rows[0]["Session"]));
            cmd.Parameters.AddWithValue("@AG_Id", Convert.ToString(ds1.Rows[0]["Canteen"]));
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
                //Canteen Info
                lblCanteenName.Text = ds.Rows[0]["Canteen"].ToString();
                lblCanteenType.Text = ds.Rows[0]["Session_Name"].ToString();
                lblCanteenFromTime.Text = StartTime;
                lblCanteenToTime.Text = EndTime;
            }

            

            //Access Count Details
            
            SqlCommand cmdAccessCount = new SqlCommand("SP_GetSession", con);
            cmdAccessCount.CommandType = CommandType.StoredProcedure;
            cmdAccessCount.Connection = con;
            cmdAccessCount.Parameters.AddWithValue("@Flag", "Get AccessCount Details");
            cmdAccessCount.Parameters.AddWithValue("@Session", Session);
            cmdAccessCount.Parameters.AddWithValue("@AG_Id", Canteen);
            cmdAccessCount.Parameters.AddWithValue("@Date", FromDate);
            cmdAccessCount.Parameters.AddWithValue("@ToDate", ToDate);
            cmdAccessCount.Parameters.AddWithValue("@Search", "");
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
            TimeSpan start = DateTime.Parse(ds1.Rows[0]["StartDate"].ToString()).TimeOfDay;
            TimeSpan end = DateTime.Parse(ds1.Rows[0]["EndDate"].ToString()).TimeOfDay;
            string Date = DateTime.Now.ToString("HH:mm:ss tt"); 
            TimeSpan CurrentTime = DateTime.Parse(Date).TimeOfDay;
            string filterDate = DateTime.Now.ToString("dd/MM/yyyy");

            if (start <= CurrentTime && CurrentTime<=end && ds1.Rows[0]["Date"].ToString() == filterDate)
            {
                //divmain.Attributes.Add("style", "display:block");
                divmain.Visible = true;
                SessionFlag = "1";
                lblstatus.Text = "Session Started.";
                if(lblServed.Text == lblTotal.Text)
                {
                    lblallows.Text = lblServed.Text + "/" + lblTotal.Text + " All Members Served.";
                }
                else
                {
                    lblallows.Text = lblServed.Text + "/" + lblTotal.Text + " Members Served.";
                }
                
                lblallows.Attributes.Add("style", "color: green;font-size:medium;");
                lblstatus.Attributes.Add("style", "color: green;font-size:xx-large;");
            }
            else
            {
                //divmain.Attributes.Add("style", "display:none");
                divmain.Visible = false;
                ModalPopupExtender1.Show();
               
                lblstatus.Text = "Session Ended.";
                lblallows.Text = lblServed.Text + "/" + lblTotal.Text + " All Members Served.";
                lblallows.Attributes.Add("style", "color: red;font-size:medium;");
                lblstatus.Attributes.Add("style", "color: red;font-size:xx-large;");
            }
            divstudent.Visible = true;

        }

        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void ibtnclose_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.Hide();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //if (SessionFlag == "1")
            //{
                SetTimer();
                BindData();
                ModalPopupExtender1.Hide();
            //}
            //else
            //{
            //    ModalPopupExtender1.Hide();
            //    Response.Redirect("Canteen_Public_Monitor.aspx?Id=" + Request.QueryString["Id"].ToString() + "");
            //}
        }


    }
}