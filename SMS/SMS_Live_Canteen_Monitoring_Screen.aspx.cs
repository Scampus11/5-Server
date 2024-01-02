using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Web.Configuration;
using SMS.Class;

namespace SMS
{
    public partial class SMS_Live_Canteen_Monitoring_Screen : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!Page.IsPostBack)
            {
                //Session["Canteen_Flag"] = null;
                //Session["Session_Id"] = null;
                //Session["CanteenName"] = null;
                //Session["CanteenCount"] = null;
               txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
               txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Session["Sider"]= "Live SAG Monitoring Screen";
                if (Session["Session"] == null)
                {
                    BindSession();
                }
                else if (Session["Session"].ToString() == "0")
                {
                    BindSession();
                    FillGrid();
                }
                else if (Session["Session"] != null)
                {
                    BindSession();
                    ddlCanteen.ClearSelection();
                    //ddlCanteen.SelectedValue = Session["Session"].ToString();
                    ddlCanteen.Items.FindByValue(Session["Session"].ToString()).Selected = true;
                    FillGrid();
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
        protected void ddlCanteen_TextChanged(object sender, EventArgs e)
        {
            Session["Session"] = ddlCanteen.SelectedValue;
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
                ddlCanteen.DataSource = dtCollege;
                ddlCanteen.DataTextField = "Name";
                ddlCanteen.DataValueField = "Id";
                ddlCanteen.DataBind();
                ddlCanteen.Items.Insert(0, new ListItem("--Select Session--", "0"));
            }
            else
            {
                ddlCanteen.DataSource = null;
                ddlCanteen.DataBind();
                ddlCanteen.Items.Insert(0, new ListItem("--Select Session--", "0"));
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmployee.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        
        protected void FillGrid()
        {
            DataTable dtCanteen = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GetSession", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "GetCanteen");
            cmd.Parameters.AddWithValue("@Session", ddlCanteen.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@Date", txtDate.Text.Trim());
            cmd.Parameters.AddWithValue("@ToDate", txtDate.Text.Trim());


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

        protected void lnkCanteenName_Click(object sender, EventArgs e)
        {
            LinkButton rbAdmin = sender as LinkButton;
            GridViewRow gridrow = rbAdmin.NamingContainer as GridViewRow;
            LinkButton lnkCanteenName = gridrow.FindControl("lnkCanteenName") as LinkButton;
            LinkButton lnkAllowedmem = gridrow.FindControl("lnkAllowedmem") as LinkButton;
            LinkButton lnkAccessMem = gridrow.FindControl("lnkAccessMem") as LinkButton;
            LinkButton lnkPenMem = gridrow.FindControl("lnkPenMem") as LinkButton;
            LinkButton lnkDeniedMem = gridrow.FindControl("lnkDeniedMem") as LinkButton;
            LinkButton lblId = gridrow.FindControl("lblId") as LinkButton;

            Session["Session_Name"] = ddlCanteen.SelectedItem.Text.ToString();
            Session["Canteen"] = lblId.Text;

            Session["Session_Id"] = ddlCanteen.SelectedValue.ToString();
            Session["CanteenName"] = lnkCanteenName.Text;
            Session["AllowedmemCount"] = lnkAllowedmem.Text;
            Session["AccessMemCount"] = lnkAccessMem.Text;
            Session["PenMemCount"] = lnkPenMem.Text;
            Session["DeniedMemCount"] = lnkDeniedMem.Text;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(rbAdmin);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=AllowedMembers','_newtab');", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=AllowedMembers&FromDate=" + txtDate.Text.Trim() + "&ToDate=" + txtToDate.Text.Trim() + "','_newtab');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=AllowedMembers&Session_Id=" + ddlCanteen.SelectedValue + "&CanteenName=" + lnkCanteenName.Text + "&Count=" + lnkAllowedmem.Text + "','_newtab');", true);
        }

        protected void lnkAllowedmem_Click(object sender, EventArgs e)
        {
            LinkButton rbAdmin = sender as LinkButton;
            GridViewRow gridrow = rbAdmin.NamingContainer as GridViewRow;
            LinkButton lnkCanteenName = gridrow.FindControl("lnkCanteenName") as LinkButton;
            LinkButton lnkAllowedmem = gridrow.FindControl("lnkAllowedmem") as LinkButton;
            LinkButton lnkAccessMem = gridrow.FindControl("lnkAccessMem") as LinkButton;
            LinkButton lnkPenMem = gridrow.FindControl("lnkPenMem") as LinkButton;
            LinkButton lnkDeniedMem = gridrow.FindControl("lnkDeniedMem") as LinkButton;
            LinkButton lblId = gridrow.FindControl("lblId") as LinkButton;

            Session["Session_Name"] = ddlCanteen.SelectedItem.Text.ToString();
            Session["Canteen"] = lblId.Text;
            Session["Session_Id"] = ddlCanteen.SelectedValue.ToString();
            Session["CanteenName"] = lnkCanteenName.Text;
            Session["AllowedmemCount"] = lnkAllowedmem.Text;
            Session["AccessMemCount"] = lnkAccessMem.Text;
            Session["PenMemCount"] = lnkPenMem.Text;
            Session["DeniedMemCount"] = lnkDeniedMem.Text;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(rbAdmin);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=AllowedMembers&FromDate=" + txtDate.Text.Trim() + "&ToDate=" + txtToDate.Text.Trim() + "','_newtab');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=AllowedMembers&Session_Id=" + ddlCanteen.SelectedValue + "&CanteenName=" + lnkCanteenName.Text + "&Count=" + rbAdmin.Text + "','_newtab');", true);
        }

        protected void lnkPenMem_Click(object sender, EventArgs e)
        {
            LinkButton rbAdmin = sender as LinkButton;
            GridViewRow gridrow = rbAdmin.NamingContainer as GridViewRow;
            LinkButton lnkCanteenName = gridrow.FindControl("lnkCanteenName") as LinkButton;
            LinkButton lnkAllowedmem = gridrow.FindControl("lnkAllowedmem") as LinkButton;
            LinkButton lnkAccessMem = gridrow.FindControl("lnkAccessMem") as LinkButton;
            LinkButton lnkPenMem = gridrow.FindControl("lnkPenMem") as LinkButton;
            LinkButton lnkDeniedMem = gridrow.FindControl("lnkDeniedMem") as LinkButton;
            LinkButton lblId = gridrow.FindControl("lblId") as LinkButton;

            Session["Session_Name"] = ddlCanteen.SelectedItem.Text.ToString();
            Session["Canteen"] = lblId.Text;
            Session["Session_Id"] = ddlCanteen.SelectedValue.ToString();
            Session["CanteenName"] = lnkCanteenName.Text;
            Session["AllowedmemCount"] = lnkAllowedmem.Text;
            Session["AccessMemCount"] = lnkAccessMem.Text;
            Session["PenMemCount"] = lnkPenMem.Text;
            Session["DeniedMemCount"] = lnkDeniedMem.Text;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(rbAdmin);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=PendingMembers&FromDate=" + txtDate.Text.Trim() + "&ToDate=" + txtToDate.Text.Trim() + "','_newtab');", true);

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=PendingMembers&Session_Id=" + ddlCanteen.SelectedValue + "&CanteenName=" + lnkCanteenName.Text + "&Count=" + rbAdmin.Text + "','_newtab');", true);
        }

        protected void lnkAccessMem_Click(object sender, EventArgs e)
        {
            LinkButton rbAdmin = sender as LinkButton;
            GridViewRow gridrow = rbAdmin.NamingContainer as GridViewRow;
            LinkButton lnkCanteenName = gridrow.FindControl("lnkCanteenName") as LinkButton;
            LinkButton lnkAllowedmem = gridrow.FindControl("lnkAllowedmem") as LinkButton;
            LinkButton lnkAccessMem = gridrow.FindControl("lnkAccessMem") as LinkButton;
            LinkButton lnkPenMem = gridrow.FindControl("lnkPenMem") as LinkButton;
            LinkButton lnkDeniedMem = gridrow.FindControl("lnkDeniedMem") as LinkButton;
            LinkButton lblId = gridrow.FindControl("lblId") as LinkButton;

            Session["Session_Name"] = ddlCanteen.SelectedItem.Text.ToString();
            Session["Canteen"] = lblId.Text;
            Session["Session_Id"] = ddlCanteen.SelectedValue.ToString();
            Session["CanteenName"] = lnkCanteenName.Text;
            Session["AllowedmemCount"] = lnkAllowedmem.Text;
            Session["AccessMemCount"] = lnkAccessMem.Text;
            Session["PenMemCount"] = lnkPenMem.Text;
            Session["DeniedMemCount"] = lnkDeniedMem.Text;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(rbAdmin);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=AccessMembers&FromDate=" + txtDate.Text.Trim() + "&ToDate=" + txtToDate.Text.Trim() + "','_newtab');", true);

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=AccessMembers&Session_Id=" + ddlCanteen.SelectedValue + "&CanteenName=" + lnkCanteenName.Text + "&Count=" + rbAdmin.Text + "','_newtab');", true);
        }

        protected void lnkDeniedMem_Click(object sender, EventArgs e)
        {
            LinkButton rbAdmin = sender as LinkButton;
            GridViewRow gridrow = rbAdmin.NamingContainer as GridViewRow;
            LinkButton lnkCanteenName = gridrow.FindControl("lnkCanteenName") as LinkButton;
            LinkButton lnkAllowedmem = gridrow.FindControl("lnkAllowedmem") as LinkButton;
            LinkButton lnkAccessMem = gridrow.FindControl("lnkAccessMem") as LinkButton;
            LinkButton lnkPenMem = gridrow.FindControl("lnkPenMem") as LinkButton;
            LinkButton lnkDeniedMem = gridrow.FindControl("lnkDeniedMem") as LinkButton;
            LinkButton lblId = gridrow.FindControl("lblId") as LinkButton;

            Session["Session_Name"] = ddlCanteen.SelectedItem.Text.ToString();
            Session["Canteen"] = lblId.Text;
            Session["Session_Id"] = ddlCanteen.SelectedValue.ToString();
            Session["CanteenName"] = lnkCanteenName.Text;
            Session["AllowedmemCount"] = lnkAllowedmem.Text;
            Session["AccessMemCount"] = lnkAccessMem.Text;
            Session["PenMemCount"] = lnkPenMem.Text;
            Session["DeniedMemCount"] = lnkDeniedMem.Text;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(rbAdmin);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=DeniedMembers&FromDate=" + txtDate.Text.Trim() + "&ToDate=" + txtToDate.Text.Trim() + "','_newtab');", true);

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Canteen_Monitoring_Count.aspx?Flag=DeniedMembers&Session_Id=" + ddlCanteen.SelectedValue + "&CanteenName=" + lnkCanteenName.Text + "&Count=" + rbAdmin.Text + "','_newtab');", true);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            FillGrid();
            if(txtDate.Text=="")
            {
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
        }

        protected void lnkGo_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}