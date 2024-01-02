using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System;

namespace SMS
{
    public partial class Canteen_Monitoring : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Canteen_Flag"] = Request.QueryString["Flag"].ToString().Trim();
                Session["Session_Id"] = Request.QueryString["Session_Id"].ToString().Trim();
                Session["CanteenName"] = Request.QueryString["CanteenName"].ToString().Trim();
                Session["CanteenCount"] = Request.QueryString["Count"].ToString().Trim();
                if (Request.QueryString["Flag"].ToString().Trim() == "AllowedMembers" || Request.QueryString["Flag"].ToString().Trim() == "PendingMembers")
                {
                    FillGrid();
                }
                else
                {
                    FillGrid2();
                }
            }
        }
        protected void FillGrid()
        {
            DataTable dtCanteen = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", Request.QueryString["Flag"].ToString().Trim());
            cmd.Parameters.AddWithValue("@Session", Request.QueryString["Session_Id"].ToString().Trim());
            cmd.Parameters.AddWithValue("@AG_Id", Request.QueryString["CanteenName"].ToString().Trim());
            

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
            cmd.Parameters.AddWithValue("@Session", Request.QueryString["Session_Id"].ToString().Trim());
            cmd.Parameters.AddWithValue("@AG_Id", Request.QueryString["CanteenName"].ToString().Trim());


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
    }
}