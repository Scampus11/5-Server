using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.BussinessLayer;
using SMS.Class;
using SMS.CommonClass;

namespace SMS
{
    public partial class ProcterAccessLogs : System.Web.UI.Page
    {
        #region Common Variable Declaration
        BS BS = new BS();
        DataTable DT = new DataTable();
        #endregion

        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Canteen_Flag"] = null;
            Session["Session_Id"] = null;
            Session["CanteenName"] = null;
            Session["CanteenCount"] = null;
            Session["Sider"] = "PROCTER ACCESS LOGS";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                SetTimer();
                BindBG();
                LastEntry();
            }
        }
        #endregion

        #region Last Entry
        protected void LastEntry()
        {
            DataSet DS = BS.BGLogsDetails("LastOne", Convert.ToInt32(ddlBGList2.SelectedValue), txtfromDate.Text.Trim(), txtToDate.Text.Trim());
            if (DS.Tables[0].Rows.Count > 0)
            {
                GrdBG.DataSource = DS;
                GrdBG.DataBind();
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            LastEntry();
            if (ddlBGList.SelectedValue != "0" && txtfromDate.Text!="" && txtToDate.Text!="")
            {
                lnkGo_Click(sender, e);
            }
            if (ddlBGList2.SelectedValue != "0")
            {
                lnkGo2_Click(sender, e);
            }
        }
        protected void SetTimer()
        {
            DataSet DS = BS.Timer();
            if (DS.Tables[0].Rows.Count > 0)
            {
                Timer1.Interval = Convert.ToInt32(DS.Tables[0].Rows[0]["MusteringTime"].ToString());
            }
        }
        #endregion

        #region Bind BG
        protected void BindBG()
        {
            try
            {
                DT = BS.BindPCR("BlockGroup");
                ddlBGList.DataSource = DT;
                ddlBGList.DataTextField = "Name";
                ddlBGList.DataValueField = "Id";
                ddlBGList.DataBind();
                ddlBGList.Items.Insert(0, new ListItem("Select Block Group", "0"));
                //Mustering
                ddlBGList2.DataSource = DT;
                ddlBGList2.DataTextField = "Name";
                ddlBGList2.DataValueField = "Id";
                ddlBGList2.DataBind();
                ddlBGList2.Items.Insert(0, new ListItem("Select Block Group", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        #endregion

        #region First Go Event
        protected void lnkGo_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                DataSet DS = BS.BGLogsDetails("CountDetails", Convert.ToInt32(ddlBGList.SelectedValue), txtfromDate.Text.Trim(), txtToDate.Text.Trim());
                lblCheckInCount.Text = DS.Tables[0].Rows[0]["CheckInCount"].ToString();
                lblCheckOutCount.Text = DS.Tables[1].Rows[0]["CheckOutCount"].ToString();
                aCheckIn.Attributes.Add("href", WebConfigurationManager.AppSettings["BGUrl"].ToString() 
                    + "?Flag=CheckIn&&BGId="+ddlBGList.SelectedValue+ "&&FromDate=" + txtfromDate.Text 
                    + "&&ToDate=" + txtToDate.Text);
                aCheckOut.Attributes.Add("href", WebConfigurationManager.AppSettings["BGUrl"].ToString()
                    + "?Flag=CheckOut&&BGId=" + ddlBGList.SelectedValue + "&&FromDate=" + txtfromDate.Text
                    + "&&ToDate=" + txtToDate.Text);
            }
        }
        protected bool Validation()
        {
            if (ddlBGList.SelectedValue == "0")
            {
                lblBGList.Visible = true;
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                return false;
            }
            if (txtfromDate.Text == "")
            {
                lblBGList.Visible = false;
                lblFromDate.Visible = true;
                lblToDate.Visible = false;
                return false;
            }
            if (txtToDate.Text == "")
            {
                lblBGList.Visible = false;
                lblFromDate.Visible = false;
                lblToDate.Visible = true;
                return false;
            }
            if (ddlBGList.SelectedValue != "0" && txtToDate.Text != "" && txtToDate.Text != "")
            {
                lblBGList.Visible = false;
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                return true;
            }
            return true;
        }
        #endregion

        #region First Go2 Event
        protected void lnkGo2_Click(object sender, EventArgs e)
        {
            if (Validation2())
            {
                DataSet DS = BS.BGLogsDetails("CountMustering", Convert.ToInt32(ddlBGList2.SelectedValue), txtfromDate.Text.Trim(), txtToDate.Text.Trim());
                lblMusteringCheckInCount.Text = DS.Tables[0].Rows[0]["CheckInCount"].ToString();
                lblMusteringCheckOutCount.Text = DS.Tables[1].Rows[0]["CheckOutCount"].ToString();
                lblMusteringCount.Text = DS.Tables[2].Rows[0]["MusteringCount"].ToString();
                lblTotal.Text = DS.Tables[3].Rows[0]["TotalCount"].ToString();
                aTotal.Attributes.Add("href", WebConfigurationManager.AppSettings["BGUrl"].ToString()
                   + "?Flag=MusteringTotal&&BGId=" + ddlBGList.SelectedValue);
                aMusteringCheckIn.Attributes.Add("href", WebConfigurationManager.AppSettings["BGUrl"].ToString()
                   + "?Flag=MusteringCheckIn&&BGId=" + ddlBGList.SelectedValue);
                aMusteringCheckOut.Attributes.Add("href", WebConfigurationManager.AppSettings["BGUrl"].ToString()
                   + "?Flag=MusteringCheckOut&&BGId=" + ddlBGList.SelectedValue);
                aMustering.Attributes.Add("href", WebConfigurationManager.AppSettings["BGUrl"].ToString()
                   + "?Flag=Mustering&&BGId=" + ddlBGList.SelectedValue);
            }
        }
        protected bool Validation2()
        {
            if (ddlBGList2.SelectedValue == "0")
            {
                lblBGList2.Visible = true;
                return false;
            }
            if (ddlBGList2.SelectedValue != "0")
            {
                lblBGList.Visible = false;
                return true;
            }
            return true;
        }
        #endregion

        #region RowDataBound Method
        protected void GrdBG_RowDataBound(object sender, GridViewRowEventArgs e)
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