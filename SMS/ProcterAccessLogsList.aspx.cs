using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.BussinessLayer;
using SMS.Class;
using SMS.CommonClass;

namespace SMS
{
    public partial class ProcterAccessLogsList : System.Web.UI.Page
    {
        #region Common Variable Declaration
        string Flag = "";
        int BGId = 0;
        string FromDate = "";
        string ToDate = "";
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
            Session["Sider"] = "PROCTER ACCESS LOGS LIST";
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Flag"] != null && Request.QueryString["BGId"] != null
                && Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null)
                {
                    Flag = Request.QueryString["Flag"].ToString();
                    BGId = Convert.ToInt32(Request.QueryString["BGId"].ToString());
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    FillGrid();
                }
                else if (Request.QueryString["Flag"] != null && Request.QueryString["BGId"] != null)
                {
                    Flag = Request.QueryString["Flag"].ToString();
                    BGId = Convert.ToInt32(Request.QueryString["BGId"].ToString());
                    FillGrid();
                }
            }
        }
        #endregion

        #region Grid List 
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                DataTable DS = BS.BGLogsList(Flag, BGId, FromDate, ToDate);
                if (DS.Rows.Count > 0)
                {
                    if (sortExpression != null)
                    {
                        DataView dv = DS.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gridEmployee.DataSource = dv;
                    }
                    else
                    {
                        gridEmployee.DataSource = DS;
                    }
                    gridEmployee.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    gridEmployee.DataSource = null;
                    gridEmployee.DataBind();
                    btnExport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
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
        #endregion

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