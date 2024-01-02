using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using SMS.Class;
using System.Web.Configuration;

namespace SMS
{
    public partial class Procter_System : System.Web.UI.Page
    {
        LogFile logFile = new LogFile();
        String line = "";
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (!Page.IsPostBack)
            {
                pageload();
            }
        }
        protected void pageload()
        {
            try
            {

                Session["Canteen_Flag"] = null;
                Session["Session_Id"] = null;
                Session["CanteenName"] = null;
                Session["CanteenCount"] = null;
                //Hostel
                if (Request.QueryString["HostelGrid"] == "Grid")
                {
                    Session["Sider"] = "Hostel";
                    divHostel.Visible = false;
                    divgridHostel.Visible = true;
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    HostelGrid();
                }
                else if (Request.QueryString["HostelAdd"] == "Add")
                {
                    Session["Sider"] = "Hostel";
                    divHostel.Visible = true;
                    divgridHostel.Visible = false;
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    lnkhostelsave.Visible = true;
                    lnkhostelupdate.Visible = false;
                }
                else if (Request.QueryString["HostelEdit"] == "Edit" && Request.QueryString["HostelId"] != null)
                {
                    Session["Sider"] = "Hostel";
                    divHostel.Visible = true;
                    divgridHostel.Visible = false;
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    EditHostelData();
                    lnkhostelsave.Visible = false;
                    lnkhostelupdate.Visible = true;
                }
                //Block
                if (Request.QueryString["BlockGrid"] == "Grid")
                {
                    Session["Sider"] = "Block";
                    divBlock.Visible = false;
                    divgridBlock.Visible = true;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item active");
                    BlockGrid();
                }
                else if (Request.QueryString["BlockAdd"] == "Add")
                {
                    Session["Sider"] = "Block";
                    divBlock.Visible = true;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    lnkBlocksave.Visible = true;
                    lnkBlockupdate.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item active");
                    BindHostel();
                }
                else if (Request.QueryString["BlockEdit"] == "Edit" && Request.QueryString["BlockId"] != null)
                {
                    Session["Sider"] = "Block";
                    divBlock.Visible = true;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    EditBlockData();
                    lnkBlocksave.Visible = false;
                    lnkBlockupdate.Visible = true;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item active");
                }
                //Floor
                if (Request.QueryString["FloorGrid"] == "Grid")
                {
                    Session["Sider"] = "Floor";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = true;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item active");
                    FloorGrid();
                }
                else if (Request.QueryString["FloorAdd"] == "Add")
                {
                    Session["Sider"] = "Floor";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = true;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    lnkFloorsave.Visible = true;
                    lnkFloorupdate.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item active");
                    BindHostelForFloor();
                    //BindBlock();
                }
                else if (Request.QueryString["FloorEdit"] == "Edit" && Request.QueryString["FloorId"] != null)
                {
                    Session["Sider"] = "Floor";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = true;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    EditFloorData();
                    lnkFloorsave.Visible = false;
                    lnkFloorupdate.Visible = true;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item active");
                }
                //Floor
                if (Request.QueryString["RoomGrid"] == "Grid")
                {
                    Session["Sider"] = "Room";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = true;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item active");
                    RoomGrid();
                }
                else if (Request.QueryString["RoomAdd"] == "Add")
                {
                    Session["Sider"] = "Room";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = true;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    lnkRoomSave.Visible = true;
                    lnkRoomupdate.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item active");
                    BindRoomHostel();

                }
                else if (Request.QueryString["RoomEdit"] == "Edit" && Request.QueryString["RoomId"] != null)
                {
                    Session["Sider"] = "Room";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = true;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    EditRoomData();
                    lnkRoomSave.Visible = false;
                    lnkRoomupdate.Visible = true;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item active");
                }
                //BED
                if (Request.QueryString["BedGrid"] == "Grid")
                {
                    Session["Sider"] = "Bed";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = true;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item");
                    liBed.Attributes.Add("class", "nav-item active");
                    BedGrid();
                }
                else if (Request.QueryString["BedAdd"] == "Add")
                {
                    Session["Sider"] = "Bed";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = true;
                    divgridBED.Visible = false;
                    lnkBedSave.Visible = true;
                    lnkBedUpdate.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item");
                    liBed.Attributes.Add("class", "nav-item active");
                    BindBedHostel();
                }
                else if (Request.QueryString["BedEdit"] == "Edit" && Request.QueryString["BedId"] != null)
                {
                    Session["Sider"] = "Bed";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = true;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = false;
                    EditBedData();
                    lnkBedSave.Visible = false;
                    lnkBedUpdate.Visible = true;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item");
                    liBed.Attributes.Add("class", "nav-item active");
                }
                //Locker
                if (Request.QueryString["LockerGrid"] == "Grid")
                {
                    Session["Sider"] = "Locker";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = false;
                    divgridLocker.Visible = true;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item");
                    liBed.Attributes.Add("class", "nav-item");
                    liLocker.Attributes.Add("class", "nav-item active");
                    LockerGrid();
                }
                else if (Request.QueryString["LockerAdd"] == "Add")
                {
                    Session["Sider"] = "Locker";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = true;
                    divgridLocker.Visible = false;
                    lnkLockerSave.Visible = true;
                    lnkLockerUpdate.Visible = false;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item");
                    liBed.Attributes.Add("class", "nav-item");
                    liLocker.Attributes.Add("class", "nav-item active");
                    BindLockerHostel();
                }
                else if (Request.QueryString["LockerEdit"] == "Edit" && Request.QueryString["LockerId"] != null)
                {
                    Session["Sider"] = "Locker";
                    divBlock.Visible = false;
                    divgridBlock.Visible = false;
                    divHostel.Visible = false;
                    divgridHostel.Visible = false;
                    divFloor.Visible = false;
                    divgridFloor.Visible = false;
                    divRoom.Visible = false;
                    divgridRoom.Visible = false;
                    divBed.Visible = false;
                    divgridBED.Visible = false;
                    divLocker.Visible = true;
                    divgridLocker.Visible = false;
                    EditLockerData();
                    lnkLockerSave.Visible = false;
                    lnkLockerUpdate.Visible = true;
                    liHostel.Attributes.Add("class", "nav-item");
                    liBlock.Attributes.Add("class", "nav-item");
                    liFloor.Attributes.Add("class", "nav-item");
                    liRoom.Attributes.Add("class", "nav-item");
                    liBed.Attributes.Add("class", "nav-item");
                    liLocker.Attributes.Add("class", "nav-item active");
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }

        }
        protected void Connection()
        {
            try
            {
                string filePath1 = Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
                StreamReader sr = new StreamReader(filePath1);
                line = sr.ReadToEnd();
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
        //Hostel
        protected void EditHostelData()
        {
            SqlCommand cmd = new SqlCommand("SP_EditHostelData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", Request.QueryString["HostelId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable DtEditHostel = new DataTable();
            da.Fill(DtEditHostel);
            if (DtEditHostel.Rows.Count > 0)
            {
                txtHostelId.Text = DtEditHostel.Rows[0]["HostelId"].ToString();
                txtHostelName.Text = DtEditHostel.Rows[0]["Name"].ToString();
                ddlHostelType.SelectedValue = DtEditHostel.Rows[0]["Type"].ToString();
                txtTotalBlock.Text = DtEditHostel.Rows[0]["TotalBlocks"].ToString();
                ddlHostelStatus.SelectedValue = DtEditHostel.Rows[0]["Status"].ToString();
            }

        }
        protected void HostelGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetHostelData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearchHostel.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt_Hostel = new DataTable();
                da.Fill(dt_Hostel);
                if (sortExpression != null)
                {
                    DataView dv = dt_Hostel.AsDataView();
                    this.HostelSortDirection = this.HostelSortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.HostelSortDirection;
                    gridHostel.DataSource = dv;
                }
                if (dt_Hostel.Rows.Count > 0)
                {
                    gridHostel.DataSource = dt_Hostel;
                    gridHostel.DataBind();
                }
                else
                {
                    gridHostel.DataSource = null;
                    gridHostel.DataBind();
                }

            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        private string HostelSortDirection
        {
            get { return ViewState["HostelSortDirection"] != null ? ViewState["HostelSortDirection"].ToString() : "ASC"; }
            set { ViewState["HostelSortDirection"] = value; }
        }
        protected void HostelOnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridHostel.PageIndex = e.NewPageIndex;
            HostelGrid();
        }
        protected void HostelOnSorting(object sender, GridViewSortEventArgs e)
        {
            HostelGrid(e.SortExpression);
        }

        protected bool HostelValidation()
        {
            if (txtHostelId.Text.Trim() == "")
            {
                lblvalidHostelId.Visible = true;
                lblvalidHostelName.Visible = false;
                lblvalidHostelType.Visible = false;
                lblValidTotalBlock.Visible = false;
                lblValidHostelStatus.Visible = false;
                return false;
            }
            if (txtHostelName.Text.Trim() == "")
            {
                lblvalidHostelId.Visible = false;
                lblvalidHostelName.Visible = true;
                lblvalidHostelType.Visible = false;
                lblValidTotalBlock.Visible = false;
                lblValidHostelStatus.Visible = false;
                return false;
            }
            if (ddlHostelType.SelectedValue == "0")
            {
                lblvalidHostelId.Visible = false;
                lblvalidHostelName.Visible = false;
                lblvalidHostelType.Visible = true;
                lblValidTotalBlock.Visible = false;
                lblValidHostelStatus.Visible = false;
                return false;
            }
            if (txtTotalBlock.Text.Trim() == "" || txtTotalBlock.Text.Trim() == "0")
            {
                lblvalidHostelId.Visible = false;
                lblvalidHostelName.Visible = false;
                lblvalidHostelType.Visible = false;
                lblValidTotalBlock.Visible = true;
                lblValidHostelStatus.Visible = false;
                return false;
            }
            if (ddlHostelStatus.SelectedValue == "0")
            {
                lblvalidHostelId.Visible = false;
                lblvalidHostelName.Visible = false;
                lblvalidHostelType.Visible = false;
                lblValidTotalBlock.Visible = false;
                lblValidHostelStatus.Visible = true;
                return false;
            }
            return true;
        }

        protected void lnkhostelsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (HostelValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "HostelDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtHostelId.Text);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Hostel", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Insert");
                        cmd.Parameters.AddWithValue("@Id", 0);
                        cmd.Parameters.AddWithValue("@HostelId", txtHostelId.Text);
                        cmd.Parameters.AddWithValue("@Name", txtHostelName.Text);
                        cmd.Parameters.AddWithValue("@Type", ddlHostelType.SelectedValue);
                        cmd.Parameters.AddWithValue("@TotalBlocks", txtTotalBlock.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlHostelStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?HostelGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidDuplicateHostelId.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkhostelupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (HostelValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "UpdateHostelDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtHostelId.Text);
                    cmdDuplicate.Parameters.AddWithValue("@RoomID", Request.QueryString["HostelId"].ToString());
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da1 = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da1.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Hostel", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Update");
                        cmd.Parameters.AddWithValue("@Id", Request.QueryString["HostelId"].ToString());
                        cmd.Parameters.AddWithValue("@HostelId", txtHostelId.Text);
                        cmd.Parameters.AddWithValue("@Name", txtHostelName.Text);
                        cmd.Parameters.AddWithValue("@Type", ddlHostelType.SelectedValue);
                        cmd.Parameters.AddWithValue("@TotalBlocks", txtTotalBlock.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlHostelStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?HostelGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidDuplicateHostelId.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkhostelBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procter_System.aspx?HostelGrid=Grid");
        }

        protected void lnkhostelClear_Click(object sender, EventArgs e)
        {
            txtHostelId.Text = "";
            txtHostelName.Text = "";
            ddlHostelType.SelectedValue = "0";
            txtTotalBlock.Text = "1";
            ddlHostelStatus.SelectedValue = "0";
        }

        protected void lnkHostelEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Procter_System.aspx?HostelEdit=Edit&HostelId=" + ids);
        }

        protected void btnsearchHostel_Click(object sender, EventArgs e)
        {
            HostelGrid();
        }
        //Block
        protected void BlockGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetBlockData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtBlockSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt_Block = new DataTable();
                da.Fill(dt_Block);
                if (sortExpression != null)
                {
                    DataView dv = dt_Block.AsDataView();
                    this.BlockSortDirection = this.BlockSortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.BlockSortDirection;
                    GridBlock.DataSource = dv;
                }

                if (dt_Block.Rows.Count > 0)
                {
                    GridBlock.DataSource = dt_Block;
                    GridBlock.DataBind();
                }
                else
                {
                    GridBlock.DataSource = null;
                    GridBlock.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void EditBlockData()
        {
            SqlCommand cmd = new SqlCommand("SP_EditBlockData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", Request.QueryString["BlockId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable DtEditBlock = new DataTable();
            da.Fill(DtEditBlock);
            if (DtEditBlock.Rows.Count > 0)
            {
                txtBlockNumber.Text = DtEditBlock.Rows[0]["Number"].ToString();
                txtBlockName.Text = DtEditBlock.Rows[0]["Name"].ToString();
                txtBlockDescription.Text = DtEditBlock.Rows[0]["Description"].ToString();
                txtTotalOccupancy.Text = DtEditBlock.Rows[0]["Total_Occupancy"].ToString();
                txtAssignOccupancy.Text = DtEditBlock.Rows[0]["Assign_Occupancy"].ToString();
                txtAvailableOccupancy.Text = DtEditBlock.Rows[0]["Available_Occupancy"].ToString();
                ddlBlockStatus.SelectedValue = DtEditBlock.Rows[0]["Status"].ToString();
                BindHostel();
                ddlHostelName.ClearSelection();
                ddlHostelName.Items.FindByValue(DtEditBlock.Rows[0]["HostelId"].ToString()).Selected = true;
            }

        }
        private string BlockSortDirection
        {
            get { return ViewState["BlockSortDirection"] != null ? ViewState["BlockSortDirection"].ToString() : "ASC"; }
            set { ViewState["BlockSortDirection"] = value; }
        }
        protected void GridBlock_Sorting(object sender, GridViewSortEventArgs e)
        {
            BlockGrid(e.SortExpression);
        }

        protected void GridBlock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridBlock.PageIndex = e.NewPageIndex;
            BlockGrid();
        }

        protected void lnkBlockSearch_Click(object sender, EventArgs e)
        {
            BlockGrid();
        }
        protected bool BlockValidation()
        {
            if (txtBlockNumber.Text.Trim() == "")
            {
                lblValidBlockNumber.Visible = true;
                lblValidBlockName.Visible = false;
                lblValidBlockHostelName.Visible = false;
                lblValidBlockhostelStatus.Visible = false;
                return false;
            }
            if (txtBlockName.Text.Trim() == "")
            {
                lblValidBlockNumber.Visible = false;
                lblValidBlockName.Visible = true;
                lblValidBlockHostelName.Visible = false;
                lblValidBlockhostelStatus.Visible = false;
                return false;
            }
            if (ddlHostelName.SelectedValue == "0")
            {
                lblValidBlockNumber.Visible = false;
                lblValidBlockName.Visible = false;
                lblValidBlockHostelName.Visible = true;
                lblValidBlockhostelStatus.Visible = false;
                return false;
            }
            if (ddlBlockStatus.SelectedValue == "0")
            {
                lblValidBlockNumber.Visible = false;
                lblValidBlockName.Visible = false;
                lblValidBlockHostelName.Visible = false;
                lblValidBlockhostelStatus.Visible = true;
                return false;
            }
            return true;
        }

        protected void lnkBlocksave_Click(object sender, EventArgs e)
        {
            try
            {
                if (BlockValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "BlockDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtBlockNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlHostelName.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Block", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Insert");
                        cmd.Parameters.AddWithValue("@Id", 0);
                        cmd.Parameters.AddWithValue("@Number", txtBlockNumber.Text);
                        cmd.Parameters.AddWithValue("@Name", txtBlockName.Text);
                        cmd.Parameters.AddWithValue("@Description", txtBlockDescription.Text);
                        cmd.Parameters.AddWithValue("@Total_Occupancy", txtTotalOccupancy.Text);
                        cmd.Parameters.AddWithValue("@Assign_Occupancy", txtAssignOccupancy.Text);
                        cmd.Parameters.AddWithValue("@Available_Occupancy", txtAvailableOccupancy.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlBlockStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@HostelId", ddlHostelName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?BlockGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidDuplicateBlockNumber.Visible = true;
                        lblValidBlockNumber.Visible = false;
                        lblValidBlockName.Visible = false;
                        lblValidBlockHostelName.Visible = false;
                        lblValidBlockhostelStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkBlockupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (BlockValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "UpdateBlockDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtBlockNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@RoomID", Request.QueryString["BlockId"].ToString());
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlHostelName.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Block", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Update");
                        cmd.Parameters.AddWithValue("@Id", Request.QueryString["BlockId"].ToString());
                        cmd.Parameters.AddWithValue("@Number", txtBlockNumber.Text);
                        cmd.Parameters.AddWithValue("@Name", txtBlockName.Text);
                        cmd.Parameters.AddWithValue("@Description", txtBlockDescription.Text);
                        cmd.Parameters.AddWithValue("@Total_Occupancy", txtTotalOccupancy.Text);
                        cmd.Parameters.AddWithValue("@Assign_Occupancy", txtAssignOccupancy.Text);
                        cmd.Parameters.AddWithValue("@Available_Occupancy", txtAvailableOccupancy.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlBlockStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@HostelId", ddlHostelName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?BlockGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidDuplicateBlockNumber.Visible = true;
                        lblValidBlockNumber.Visible = false;
                        lblValidBlockName.Visible = false;
                        lblValidBlockHostelName.Visible = false;
                        lblValidBlockhostelStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkBlockback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procter_System.aspx?BlockGrid=Grid");
        }

        protected void lnkBlockclear_Click(object sender, EventArgs e)
        {
            txtBlockNumber.Text = "";
            txtBlockName.Text = "";
            txtBlockDescription.Text = "";
            txtTotalOccupancy.Text = "0";
            txtAssignOccupancy.Text = "0";
            txtAvailableOccupancy.Text = "0";
            ddlHostelName.SelectedValue = "0";
            ddlBlockStatus.SelectedValue = "0";
        }

        protected void lnkBlockEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Procter_System.aspx?BlockEdit=Edit&BlockId=" + ids);
        }
        protected void BindHostel()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Hostel");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlHostelName.DataSource = ds1;
                ddlHostelName.DataTextField = "Name";
                ddlHostelName.DataValueField = "Id";
                ddlHostelName.DataBind();
                ddlHostelName.Items.Insert(0, new ListItem("--Select Hostel Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindHostelForFloor()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Hostel");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlFloorHostelName.DataSource = ds1;
                ddlFloorHostelName.DataTextField = "Name";
                ddlFloorHostelName.DataValueField = "Id";
                ddlFloorHostelName.DataBind();
                ddlFloorHostelName.Items.Insert(0, new ListItem("--Select Hostel Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        //Floor
        protected void FloorGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetFloorData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtFloorSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt_Floor = new DataTable();
                da.Fill(dt_Floor);
                if (sortExpression != null)
                {
                    DataView dv = dt_Floor.AsDataView();
                    this.FloorSortDirection = this.FloorSortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.FloorSortDirection;
                    GridFloor.DataSource = dv;
                }

                if (dt_Floor.Rows.Count > 0)
                {
                    GridFloor.DataSource = dt_Floor;
                    GridFloor.DataBind();
                }
                else
                {
                    GridFloor.DataSource = null;
                    GridFloor.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void EditFloorData()
        {
            SqlCommand cmd = new SqlCommand("SP_EditFloorData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", Request.QueryString["FloorId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable DtEditFloor = new DataTable();
            da.Fill(DtEditFloor);
            if (DtEditFloor.Rows.Count > 0)
            {
                txtFloorNumber.Text = DtEditFloor.Rows[0]["Number"].ToString();
                txtFloorName.Text = DtEditFloor.Rows[0]["Name"].ToString();
                txtFloorDescription.Text = DtEditFloor.Rows[0]["Description"].ToString();
                txtRoomCapacity.Text = DtEditFloor.Rows[0]["Room_Capacity"].ToString();
                ddlFloorStatus.SelectedValue = DtEditFloor.Rows[0]["Status"].ToString();
                BindHostelForFloor();
                ddlFloorHostelName.ClearSelection();
                ddlFloorHostelName.Items.FindByValue(DtEditFloor.Rows[0]["HostelId"].ToString()).Selected = true;
                BindBlock();
                ddlBlockName.ClearSelection();
                ddlBlockName.Items.FindByValue(DtEditFloor.Rows[0]["BlockId"].ToString()).Selected = true;
            }

        }
        private string FloorSortDirection
        {
            get { return ViewState["FloorSortDirection"] != null ? ViewState["FloorSortDirection"].ToString() : "ASC"; }
            set { ViewState["FloorSortDirection"] = value; }
        }
        protected bool FloorValidation()
        {
            if (txtFloorNumber.Text.Trim() == "")
            {
                lblValidFloorNumber.Visible = true;
                lblValidFloorName.Visible = false;
                lblValidFloorhostelName.Visible = false;
                lblValidFloorBlockName.Visible = false;
                lblValidFloorStatus.Visible = false;
                return false;
            }
            if (txtFloorName.Text.Trim() == "")
            {
                lblValidFloorNumber.Visible = false;
                lblValidFloorName.Visible = true;
                lblValidFloorhostelName.Visible = false;
                lblValidFloorBlockName.Visible = false;
                lblValidFloorStatus.Visible = false;
                return false;
            }
            if (ddlFloorHostelName.SelectedValue == "0")
            {
                lblValidFloorNumber.Visible = false;
                lblValidFloorName.Visible = false;
                lblValidFloorhostelName.Visible = true;
                lblValidFloorBlockName.Visible = false;
                lblValidFloorStatus.Visible = false;
                return false;
            }
            if (ddlBlockName.SelectedValue == "0")
            {
                lblValidFloorNumber.Visible = false;
                lblValidFloorName.Visible = false;
                lblValidFloorhostelName.Visible = false;
                lblValidFloorBlockName.Visible = true;
                lblValidFloorStatus.Visible = false;
                return false;
            }
            if (ddlFloorStatus.SelectedValue == "0")
            {
                lblValidFloorNumber.Visible = false;
                lblValidFloorName.Visible = false;
                lblValidFloorhostelName.Visible = false;
                lblValidFloorBlockName.Visible = false;
                lblValidFloorStatus.Visible = true;
                return false;
            }

            return true;
        }
        protected void lnkFloorSearch_Click(object sender, EventArgs e)
        {
            FloorGrid();
        }

        protected void GridFloor_Sorting(object sender, GridViewSortEventArgs e)
        {
            FloorGrid(e.SortExpression);
        }

        protected void GridFloor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridFloor.PageIndex = e.NewPageIndex;
            FloorGrid();
        }

        protected void lnkFloorsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (FloorValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "FloorDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtFloorNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlFloorHostelName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@BlockNumber", ddlBlockName.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Floor", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Insert");
                        cmd.Parameters.AddWithValue("@Id", 0);
                        cmd.Parameters.AddWithValue("@Number", txtFloorNumber.Text);
                        cmd.Parameters.AddWithValue("@Name", txtFloorName.Text);
                        cmd.Parameters.AddWithValue("@Description", txtFloorDescription.Text);
                        cmd.Parameters.AddWithValue("@Room_Capacity", txtRoomCapacity.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlFloorStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@BlockID", ddlBlockName.SelectedValue);
                        cmd.Parameters.AddWithValue("@HostelId", ddlFloorHostelName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?FloorGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValiddFlooruplicate.Visible = true;
                        lblValidFloorNumber.Visible = false;
                        lblValidFloorName.Visible = false;
                        lblValidFloorhostelName.Visible = false;
                        lblValidFloorBlockName.Visible = false;
                        lblValidFloorStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkFloorupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (FloorValidation())
                {

                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "UpdateFloorDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtFloorNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@RoomID", Request.QueryString["FloorId"].ToString());
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlFloorHostelName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@BlockNumber", ddlBlockName.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Floor", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Update");
                        cmd.Parameters.AddWithValue("@Id", Request.QueryString["FloorId"].ToString());
                        cmd.Parameters.AddWithValue("@Number", txtFloorNumber.Text);
                        cmd.Parameters.AddWithValue("@Name", txtFloorName.Text);
                        cmd.Parameters.AddWithValue("@Description", txtFloorDescription.Text);
                        cmd.Parameters.AddWithValue("@Room_Capacity", txtRoomCapacity.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlFloorStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@BlockID", ddlBlockName.SelectedValue);
                        cmd.Parameters.AddWithValue("@HostelId", ddlFloorHostelName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?FloorGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValiddFlooruplicate.Visible = true;
                        lblValidFloorNumber.Visible = false;
                        lblValidFloorName.Visible = false;
                        lblValidFloorhostelName.Visible = false;
                        lblValidFloorBlockName.Visible = false;
                        lblValidFloorStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkFloorback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procter_System.aspx?FloorGrid=Grid");
        }

        protected void lnkFloorclear_Click(object sender, EventArgs e)
        {
            txtFloorNumber.Text = "";
            txtFloorName.Text = "";
            txtFloorDescription.Text = "";
            txtRoomCapacity.Text = "";
            ddlFloorStatus.SelectedValue = "0";
            ddlBlockName.SelectedValue = "0";
            ddlFloorHostelName.SelectedValue = "0";
        }

        protected void lnkFloorEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Procter_System.aspx?FloorEdit=Edit&FloorId=" + ids);
        }
        protected void BindBlock()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Block");
                cmd.Parameters.AddWithValue("@HostelId", ddlFloorHostelName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlBlockName.DataSource = ds1;
                ddlBlockName.DataTextField = "Name";
                ddlBlockName.DataValueField = "Id";
                ddlBlockName.DataBind();
                ddlBlockName.Items.Insert(0, new ListItem("--Select Block Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        //Room
        protected void RoomGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetRoomData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtRoomSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt_Room = new DataTable();
                da.Fill(dt_Room);
                if (sortExpression != null)
                {
                    DataView dv = dt_Room.AsDataView();
                    this.RoomSortDirection = this.RoomSortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.RoomSortDirection;
                    GridRoom.DataSource = dv;
                }

                if (dt_Room.Rows.Count > 0)
                {
                    GridRoom.DataSource = dt_Room;
                    GridRoom.DataBind();
                }
                else
                {
                    GridRoom.DataSource = null;
                    GridRoom.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void EditRoomData()
        {
            SqlCommand cmd = new SqlCommand("SP_EditRoomData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", Request.QueryString["RoomId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable DtEditRoom = new DataTable();
            da.Fill(DtEditRoom);
            if (DtEditRoom.Rows.Count > 0)
            {
                txtRoomNumber.Text = DtEditRoom.Rows[0]["Number"].ToString();
                ddlRoomStatus.SelectedValue = DtEditRoom.Rows[0]["Status"].ToString();
                BindRoomHostel();
                ddlRoomHostelName.ClearSelection();
                ddlRoomHostelName.Items.FindByValue(DtEditRoom.Rows[0]["HostelId"].ToString()).Selected = true;
                BindRoomBlock();
                ddlRoomBlockName.ClearSelection();
                ddlRoomBlockName.Items.FindByValue(DtEditRoom.Rows[0]["BlockID"].ToString()).Selected = true;
                BindFloor();
                ddlFloorName.ClearSelection();
                ddlFloorName.Items.FindByValue(DtEditRoom.Rows[0]["FloorId"].ToString()).Selected = true;
            }

        }
        private string RoomSortDirection
        {
            get { return ViewState["RoomSortDirection"] != null ? ViewState["RoomSortDirection"].ToString() : "ASC"; }
            set { ViewState["RoomSortDirection"] = value; }
        }
        protected bool RoomValidation()
        {
            if (txtRoomNumber.Text.Trim() == "")
            {
                lblValidRoomNumber.Visible = true;
                lblValidFloorId.Visible = false;
                lblValidRoomHostelName.Visible = false;
                lblValidRoomBlockName.Visible = false;
                lblValidRoomStatus.Visible = false;
                return false;
            }
            if (ddlRoomHostelName.SelectedValue == "0")
            {
                lblValidRoomNumber.Visible = false;
                lblValidFloorId.Visible = false;
                lblValidRoomHostelName.Visible = true;
                lblValidRoomBlockName.Visible = false;
                lblValidRoomStatus.Visible = false;
                return false;
            }
            if (ddlRoomBlockName.SelectedValue == "0")
            {
                lblValidRoomNumber.Visible = false;
                lblValidFloorId.Visible = false;
                lblValidRoomHostelName.Visible = false;
                lblValidRoomBlockName.Visible = true;
                lblValidRoomStatus.Visible = false;
                return false;
            }
            if (ddlFloorName.SelectedValue == "0")
            {
                lblValidRoomNumber.Visible = false;
                lblValidFloorId.Visible = true;
                lblValidRoomHostelName.Visible = false;
                lblValidRoomBlockName.Visible = false;
                lblValidRoomStatus.Visible = false;
                return false;
            }
            if (ddlRoomStatus.SelectedValue == "0")
            {
                lblValidRoomNumber.Visible = false;
                lblValidFloorId.Visible = false;
                lblValidRoomHostelName.Visible = false;
                lblValidRoomBlockName.Visible = false;
                lblValidRoomStatus.Visible = true;
                return false;
            }
            return true;
        }

        protected void lnkRoomSearch_Click(object sender, EventArgs e)
        {
            RoomGrid();
        }

        protected void GridRoom_Sorting(object sender, GridViewSortEventArgs e)
        {
            RoomGrid(e.SortExpression);
        }

        protected void GridRoom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridRoom.PageIndex = e.NewPageIndex;
            RoomGrid();
        }

        protected void lnkRoomEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Procter_System.aspx?RoomEdit=Edit&RoomId=" + ids);
        }

        protected void lnkRoomSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (RoomValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "RoomDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtRoomNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@BlockNumber", ddlRoomBlockName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlRoomHostelName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@FloorID", ddlFloorName.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Room", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Insert");
                        cmd.Parameters.AddWithValue("@Id", 0);
                        cmd.Parameters.AddWithValue("@Number", txtRoomNumber.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlRoomStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@FloorID", ddlFloorName.SelectedValue);
                        cmd.Parameters.AddWithValue("@BlockID", ddlRoomBlockName.SelectedValue);
                        cmd.Parameters.AddWithValue("@HostelId", ddlRoomHostelName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?RoomGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidduplicateRoomNumber.Visible = true;
                        lblValidRoomNumber.Visible = false;
                        lblValidFloorId.Visible = false;
                        lblValidRoomHostelName.Visible = false;
                        lblValidRoomBlockName.Visible = false;
                        lblValidRoomStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkRoomupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (RoomValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "UpdateRoomDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtRoomNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@RoomID", Request.QueryString["RoomId"].ToString());
                    cmdDuplicate.Parameters.AddWithValue("@BlockNumber", ddlRoomBlockName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlRoomHostelName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@FloorID", ddlFloorName.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Room", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Update");
                        cmd.Parameters.AddWithValue("@Id", Request.QueryString["RoomId"].ToString());
                        cmd.Parameters.AddWithValue("@Number", txtRoomNumber.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlRoomStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@FloorID", ddlFloorName.SelectedValue);
                        cmd.Parameters.AddWithValue("@BlockID", ddlRoomBlockName.SelectedValue);
                        cmd.Parameters.AddWithValue("@HostelId", ddlRoomHostelName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?RoomGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidduplicateRoomNumber.Visible = true;
                        lblValidRoomNumber.Visible = false;
                        lblValidFloorId.Visible = false;
                        lblValidRoomHostelName.Visible = false;
                        lblValidRoomBlockName.Visible = false;
                        lblValidRoomStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkRoomBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procter_System.aspx?RoomGrid=Grid");
        }

        protected void lnkRoomClear_Click(object sender, EventArgs e)
        {
            txtRoomNumber.Text = "";
            ddlRoomHostelName.SelectedValue = "0";
            ddlRoomBlockName.SelectedValue = "0";
            ddlFloorName.SelectedValue = "0";
            ddlRoomStatus.SelectedValue = "0";
        }
        protected void BindFloor()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Floor");
                cmd.Parameters.AddWithValue("@HostelId", ddlRoomHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlRoomBlockName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlFloorName.DataSource = ds1;
                ddlFloorName.DataTextField = "Name";
                ddlFloorName.DataValueField = "Id";
                ddlFloorName.DataBind();
                ddlFloorName.Items.Insert(0, new ListItem("--Select Floor Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        //BED
        protected void BedGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetBedData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtBEDSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt_Bed = new DataTable();
                da.Fill(dt_Bed);
                if (sortExpression != null)
                {
                    DataView dv = dt_Bed.AsDataView();
                    this.BedSortDirection = this.BedSortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.BedSortDirection;
                    GridBED.DataSource = dv;
                }

                if (dt_Bed.Rows.Count > 0)
                {
                    GridBED.DataSource = dt_Bed;
                    GridBED.DataBind();
                }
                else
                {
                    GridBED.DataSource = null;
                    GridBED.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void EditBedData()
        {
            SqlCommand cmd = new SqlCommand("SP_EditBedData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", Request.QueryString["BedId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable DtEditBed = new DataTable();
            da.Fill(DtEditBed);
            if (DtEditBed.Rows.Count > 0)
            {
                txtBedNumber.Text = DtEditBed.Rows[0]["Number"].ToString();
                ddlBedStatus.SelectedValue = DtEditBed.Rows[0]["Status"].ToString();
                BindBedHostel();
                ddlBedHostelName.ClearSelection();
                ddlBedHostelName.Items.FindByValue(DtEditBed.Rows[0]["HostelId"].ToString()).Selected = true;
                BindBedBlock();
                ddlBedBlockName.ClearSelection();
                ddlBedBlockName.Items.FindByValue(DtEditBed.Rows[0]["BlockID"].ToString()).Selected = true;
                BindBedFloor();
                ddlBedFloorName.ClearSelection();
                ddlBedFloorName.Items.FindByValue(DtEditBed.Rows[0]["FloorID"].ToString()).Selected = true;
                BindRoom();
                ddlBED_RoomNumber.ClearSelection();
                ddlBED_RoomNumber.Items.FindByValue(DtEditBed.Rows[0]["RoomId"].ToString()).Selected = true;
            }

        }
        private string BedSortDirection
        {
            get { return ViewState["BedSortDirection"] != null ? ViewState["BedSortDirection"].ToString() : "ASC"; }
            set { ViewState["BedSortDirection"] = value; }
        }
        protected bool BedValidation()
        {
            if (txtBedNumber.Text.Trim() == "")
            {
                lblValidBedNumber.Visible = true;
                lblBED_RoomNumber.Visible = false;
                lblValidBedHostelName.Visible = false;
                lblValidBedBlockName.Visible = false;
                lblValidBedFloorName.Visible = false;
                lblValidBedStatus.Visible = false;
                return false;
            }
            if (ddlBED_RoomNumber.SelectedIndex == 0)
            {
                lblValidBedNumber.Visible = false;
                lblBED_RoomNumber.Visible = true;
                lblValidBedHostelName.Visible = false;
                lblValidBedBlockName.Visible = false;
                lblValidBedFloorName.Visible = false;
                lblValidBedStatus.Visible = false;
                return false;
            }
            if (ddlBedHostelName.SelectedIndex == 0)
            {
                lblValidBedNumber.Visible = false;
                lblBED_RoomNumber.Visible = false;
                lblValidBedHostelName.Visible = true;
                lblValidBedBlockName.Visible = false;
                lblValidBedFloorName.Visible = false;
                lblValidBedStatus.Visible = false;
                return false;
            }
            if (ddlBedBlockName.SelectedIndex == 0)
            {
                lblValidBedNumber.Visible = false;
                lblBED_RoomNumber.Visible = false;
                lblValidBedHostelName.Visible = false;
                lblValidBedBlockName.Visible = true;
                lblValidBedFloorName.Visible = false;
                lblValidBedStatus.Visible = false;
                return false;
            }
            if (ddlBedFloorName.SelectedIndex == 0)
            {
                lblValidBedNumber.Visible = false;
                lblBED_RoomNumber.Visible = false;
                lblValidBedHostelName.Visible = false;
                lblValidBedBlockName.Visible = false;
                lblValidBedFloorName.Visible = true;
                lblValidBedStatus.Visible = false;
                return false;
            }
            if (ddlBedStatus.SelectedIndex == 0)
            {
                lblValidBedNumber.Visible = false;
                lblBED_RoomNumber.Visible = false;
                lblValidBedHostelName.Visible = false;
                lblValidBedBlockName.Visible = false;
                lblValidBedFloorName.Visible = false;
                lblValidBedStatus.Visible = true;
                return false;
            }

            return true;
        }

        protected void lnkBEDEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Procter_System.aspx?BedEdit=Edit&BedId=" + ids);
        }

        protected void GridBED_Sorting(object sender, GridViewSortEventArgs e)
        {
            BedGrid(e.SortExpression);
        }

        protected void GridBED_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridBED.PageIndex = e.NewPageIndex;
            BedGrid();
        }

        protected void lnkBEDSearch_Click(object sender, EventArgs e)
        {
            BedGrid();
        }

        protected void lnkBedSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (BedValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "BedDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtBedNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlBedHostelName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@BlockNumber", ddlBedBlockName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@FloorId", ddlBedFloorName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@RoomNumber", ddlBED_RoomNumber.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Bed", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Insert");
                        cmd.Parameters.AddWithValue("@Id", 0);
                        cmd.Parameters.AddWithValue("@Number", txtBedNumber.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlBedStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@RoomID", ddlBED_RoomNumber.SelectedValue);
                        cmd.Parameters.AddWithValue("@HostelId", ddlBedHostelName.SelectedValue);
                        cmd.Parameters.AddWithValue("@BlockId", ddlBedBlockName.SelectedValue);
                        cmd.Parameters.AddWithValue("@FloorId", ddlBedFloorName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?BedGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidduplicateBedNumber.Visible = true;
                        lblValidBedNumber.Visible = false;
                        lblBED_RoomNumber.Visible = false;
                        lblValidBedHostelName.Visible = false;
                        lblValidBedBlockName.Visible = false;
                        lblValidBedFloorName.Visible = false;
                        lblValidBedStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkBedUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (BedValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "UpdateBedDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtBedNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@RoomID", Request.QueryString["BedId"].ToString());
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlBedHostelName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@BlockNumber", ddlBedBlockName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@FloorId", ddlBedFloorName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@RoomNumber", ddlBED_RoomNumber.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Bed", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Update");
                        cmd.Parameters.AddWithValue("@Id", Request.QueryString["BedId"].ToString());
                        cmd.Parameters.AddWithValue("@Number", txtBedNumber.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlBedStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@RoomID", ddlBED_RoomNumber.SelectedValue);
                        cmd.Parameters.AddWithValue("@HostelId", ddlBedHostelName.SelectedValue);
                        cmd.Parameters.AddWithValue("@BlockId", ddlBedBlockName.SelectedValue);
                        cmd.Parameters.AddWithValue("@FloorId", ddlBedFloorName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?BedGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidduplicateBedNumber.Visible = true;
                        lblValidBedNumber.Visible = false;
                        lblBED_RoomNumber.Visible = false;
                        lblValidBedHostelName.Visible = false;
                        lblValidBedBlockName.Visible = false;
                        lblValidBedFloorName.Visible = false;
                        lblValidBedStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkBedBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procter_System.aspx?BedGrid=Grid");
        }

        protected void lnkBedClear_Click(object sender, EventArgs e)
        {
            txtBedNumber.Text = "";
            ddlBedHostelName.SelectedValue = "0";
            ddlBedBlockName.SelectedValue = "0";
            ddlBedFloorName.SelectedValue = "0";
            ddlBED_RoomNumber.SelectedValue = "0";
            ddlBedStatus.SelectedValue = "0";
        }
        protected void BindRoom()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Room");
                cmd.Parameters.AddWithValue("@HostelId", ddlBedHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlBedBlockName.SelectedValue);
                cmd.Parameters.AddWithValue("@FloorID", ddlBedFloorName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlBED_RoomNumber.DataSource = ds1;
                ddlBED_RoomNumber.DataTextField = "Name";
                ddlBED_RoomNumber.DataValueField = "Id";
                ddlBED_RoomNumber.DataBind();
                ddlBED_RoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        //Locker
        protected void LockerGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetLockerData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtLockerSearch.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt_Locker = new DataTable();
                da.Fill(dt_Locker);
                if (sortExpression != null)
                {
                    DataView dv = dt_Locker.AsDataView();
                    this.LockerSortDirection = this.LockerSortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.LockerSortDirection;
                    GridLocker.DataSource = dv;
                }

                if (dt_Locker.Rows.Count > 0)
                {
                    GridLocker.DataSource = dt_Locker;
                    GridLocker.DataBind();
                }
                else
                {
                    GridLocker.DataSource = null;
                    GridLocker.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void EditLockerData()
        {
            SqlCommand cmd = new SqlCommand("SP_EditLockerData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", Request.QueryString["LockerId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable DtEditLocker = new DataTable();
            da.Fill(DtEditLocker);
            if (DtEditLocker.Rows.Count > 0)
            {
                txtLockerNumber.Text = DtEditLocker.Rows[0]["Number"].ToString();
                txtLockerName.Text = DtEditLocker.Rows[0]["Name"].ToString();
                ddlLockerStatus.SelectedValue = DtEditLocker.Rows[0]["Status"].ToString();
                BindLockerHostel();
                ddlLockerHostelName.ClearSelection();
                ddlLockerHostelName.Items.FindByValue(DtEditLocker.Rows[0]["hostelId"].ToString()).Selected = true;
                BindLockerBlock();
                ddlLockerBlockName.ClearSelection();
                ddlLockerBlockName.Items.FindByValue(DtEditLocker.Rows[0]["BlockId"].ToString()).Selected = true;
                BindLockerFloor();
                ddllockerFloorName.ClearSelection();
                ddllockerFloorName.Items.FindByValue(DtEditLocker.Rows[0]["FloorId"].ToString()).Selected = true;
                BindlockerRoom();
                ddlLockerRoomNumber.ClearSelection();
                ddlLockerRoomNumber.Items.FindByValue(DtEditLocker.Rows[0]["RoomId"].ToString()).Selected = true;
                BindLockerBed();
                ddlBedNumber.ClearSelection();
                ddlBedNumber.Items.FindByValue(DtEditLocker.Rows[0]["BedId"].ToString()).Selected = true;
            }

        }
        private string LockerSortDirection
        {
            get { return ViewState["LockerSortDirection"] != null ? ViewState["LockerSortDirection"].ToString() : "ASC"; }
            set { ViewState["LockerSortDirection"] = value; }
        }
        protected bool LockerValidation()
        {
            if (txtLockerNumber.Text.Trim() == "")
            {
                lblValidLockerNumber.Visible = true;
                lblValidLockerName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerStatus.Visible = false;
                return false;
            }
            if (txtLockerName.Text.Trim() == "")
            {
                lblValidLockerNumber.Visible = false;
                lblValidLockerName.Visible = true;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerStatus.Visible = false;
                return false;
            }
            if (ddlLockerHostelName.SelectedValue == "0")
            {
                lblValidLockerNumber.Visible = false;
                lblValidLockerName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerHostelName.Visible = true;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerStatus.Visible = false;
                return false;
            }
            if (ddlLockerBlockName.SelectedValue == "0")
            {
                lblValidLockerNumber.Visible = false;
                lblValidLockerName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = true;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerStatus.Visible = false;
                return false;
            }
            if (ddllockerFloorName.SelectedValue == "0")
            {
                lblValidLockerNumber.Visible = false;
                lblValidLockerName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = true;
                lblValidLockerStatus.Visible = false;
                return false;
            }
            
            if (ddlLockerRoomNumber.SelectedValue == "0")
            {
                lblValidLockerNumber.Visible = false;
                lblValidLockerName.Visible = false;
                lblValidLockerRoomNumber.Visible = true;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                return false;
            }
            if (ddlBedNumber.SelectedValue == "0")
            {
                lblValidLockerNumber.Visible = false;
                lblValidLockerName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = true;
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = false;
                lblValidLockerStatus.Visible = false;
                return false;
            }
            if (ddlLockerStatus.SelectedValue == "0")
            {
                lblValidLockerNumber.Visible = false;
                lblValidLockerName.Visible = false;
                lblValidLockerRoomNumber.Visible = false;
                lblValidLockerBedNumber.Visible = false;
                lblValidLockerHostelName.Visible = false;
                lblValidLockerBlockName.Visible = false;
                lblValidlockerFloorName.Visible = true;
                return false;
            }
            return true;
        }
        protected void lnkLockerSearch_Click(object sender, EventArgs e)
        {
            LockerGrid();
        }

        protected void GridLocker_Sorting(object sender, GridViewSortEventArgs e)
        {
            LockerGrid(e.SortExpression);
        }

        protected void GridLocker_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridLocker.PageIndex = e.NewPageIndex;
            LockerGrid();
        }

        protected void lnkLockerEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("Procter_System.aspx?LockerEdit=Edit&LockerId=" + ids);
        }

        protected void lnkLockerSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LockerValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "LockerDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtLockerNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlLockerHostelName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@FloorID", ddllockerFloorName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@RoomNumber", ddlLockerRoomNumber.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@BedId", ddlBedNumber.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Locker", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Insert");
                        cmd.Parameters.AddWithValue("@Id", 0);
                        cmd.Parameters.AddWithValue("@Number", txtLockerNumber.Text);
                        cmd.Parameters.AddWithValue("@Name", txtLockerName.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlLockerStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@RoomID", ddlLockerRoomNumber.SelectedValue);
                        cmd.Parameters.AddWithValue("@BedID", ddlBedNumber.SelectedValue);
                        cmd.Parameters.AddWithValue("@hostelId", ddlLockerHostelName.SelectedValue);
                        cmd.Parameters.AddWithValue("@BlockId", ddlLockerBlockName.SelectedValue);
                        cmd.Parameters.AddWithValue("@FloorId", ddllockerFloorName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?LockerGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidduplicateLockerNumber.Visible = true;
                        lblValidLockerNumber.Visible = false;
                        lblValidLockerName.Visible = false;
                        lblValidLockerRoomNumber.Visible = false;
                        lblValidLockerBedNumber.Visible = false;
                        lblValidLockerHostelName.Visible = false;
                        lblValidLockerBlockName.Visible = false;
                        lblValidlockerFloorName.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkLockerUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (LockerValidation())
                {
                    SqlCommand cmdDuplicate = new SqlCommand("SP_Bind_PCR", con);
                    cmdDuplicate.Parameters.AddWithValue("@Flag", "UpdateLockerDuplicate");
                    cmdDuplicate.Parameters.AddWithValue("@HostelId", txtLockerNumber.Text);
                    cmdDuplicate.Parameters.AddWithValue("@RoomID", Request.QueryString["LockerId"].ToString());
                    cmdDuplicate.Parameters.AddWithValue("@HostelNumber", ddlLockerHostelName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@FloorID", ddllockerFloorName.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@RoomNumber", ddlLockerRoomNumber.SelectedValue);
                    cmdDuplicate.Parameters.AddWithValue("@BedId", ddlBedNumber.SelectedValue);
                    cmdDuplicate.CommandType = CommandType.StoredProcedure;
                    cmdDuplicate.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmdDuplicate);
                    DataTable dsdulicate = new DataTable();
                    da.Fill(dsdulicate);
                    if (dsdulicate.Rows[0]["Count"].ToString() == "0")
                    {
                        SqlCommand cmd = new SqlCommand("SP_PCR_Upset_Locker", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "update");
                        cmd.Parameters.AddWithValue("@Id", Request.QueryString["LockerId"].ToString());
                        cmd.Parameters.AddWithValue("@Number", txtLockerNumber.Text);
                        cmd.Parameters.AddWithValue("@Name", txtLockerName.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlLockerStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@UserId", Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@RoomID", ddlLockerRoomNumber.SelectedValue);
                        cmd.Parameters.AddWithValue("@BedID", ddlBedNumber.SelectedValue);
                        cmd.Parameters.AddWithValue("@hostelId", ddlLockerHostelName.SelectedValue);
                        cmd.Parameters.AddWithValue("@BlockId", ddlLockerBlockName.SelectedValue);
                        cmd.Parameters.AddWithValue("@FloorId", ddllockerFloorName.SelectedValue);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Procter_System.aspx?LockerGrid=Grid");
                        con.Close();
                    }
                    else
                    {
                        lblValidduplicateLockerNumber.Visible = true;
                        lblValidLockerNumber.Visible = false;
                        lblValidLockerName.Visible = false;
                        lblValidLockerRoomNumber.Visible = false;
                        lblValidLockerBedNumber.Visible = false;
                        lblValidLockerHostelName.Visible = false;
                        lblValidLockerBlockName.Visible = false;
                        lblValidlockerFloorName.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkLockerBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procter_System.aspx?LockerGrid=Grid");
        }

        protected void lnkLockerClear_Click(object sender, EventArgs e)
        {
            txtLockerNumber.Text = "";
            txtLockerName.Text = "";
            ddlLockerHostelName.SelectedValue = "0";
            ddlLockerBlockName.SelectedValue = "0";
            ddllockerFloorName.SelectedValue = "0";
            ddlLockerRoomNumber.SelectedValue = "0";
            ddlBedNumber.SelectedValue = "0";
            ddlLockerStatus.SelectedValue = "0";
        }
       
        protected void ddlFloorHostelName_TextChanged(object sender, EventArgs e)
        {
            BindBlock();
        }


        protected void ddlRoomBlockName_TextChanged(object sender, EventArgs e)
        {
            BindFloor();
        }
        protected void BindRoomHostel()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Hostel");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlRoomHostelName.DataSource = ds1;
                ddlRoomHostelName.DataTextField = "Name";
                ddlRoomHostelName.DataValueField = "Id";
                ddlRoomHostelName.DataBind();
                ddlRoomHostelName.Items.Insert(0, new ListItem("--Select Hostel Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindRoomBlock()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Block");
                cmd.Parameters.AddWithValue("@HostelId", ddlRoomHostelName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlRoomBlockName.DataSource = ds1;
                ddlRoomBlockName.DataTextField = "Name";
                ddlRoomBlockName.DataValueField = "Id";
                ddlRoomBlockName.DataBind();
                ddlRoomBlockName.Items.Insert(0, new ListItem("--Select Block Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void ddlRoomHostelName_TextChanged(object sender, EventArgs e)
        {
            BindRoomBlock();
            ddlFloorName.Items.Clear();
            ddlFloorName.Items.Insert(0, new ListItem("--Select Floor Name--", "0"));
        }
        protected void BindBedHostel()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Hostel");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlBedHostelName.DataSource = ds1;
                ddlBedHostelName.DataTextField = "Name";
                ddlBedHostelName.DataValueField = "Id";
                ddlBedHostelName.DataBind();
                ddlBedHostelName.Items.Insert(0, new ListItem("--Select Hostel Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindBedBlock()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Block");
                cmd.Parameters.AddWithValue("@HostelId", ddlBedHostelName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlBedBlockName.DataSource = ds1;
                ddlBedBlockName.DataTextField = "Name";
                ddlBedBlockName.DataValueField = "Id";
                ddlBedBlockName.DataBind();
                ddlBedBlockName.Items.Insert(0, new ListItem("--Select Block Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindBedFloor()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Floor");
                cmd.Parameters.AddWithValue("@HostelId", ddlBedHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlBedBlockName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlBedFloorName.DataSource = ds1;
                ddlBedFloorName.DataTextField = "Name";
                ddlBedFloorName.DataValueField = "Id";
                ddlBedFloorName.DataBind();
                ddlBedFloorName.Items.Insert(0, new ListItem("--Select Floor Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void ddlBedHostelName_TextChanged(object sender, EventArgs e)
        {
            BindBedBlock();
            ddlBedFloorName.Items.Clear();
            ddlBedFloorName.Items.Insert(0, new ListItem("--Select Floor Name--", "0"));
            ddlBED_RoomNumber.Items.Clear();
            ddlBED_RoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
            
        }

        protected void ddlBedBlockName_TextChanged(object sender, EventArgs e)
        {
            BindBedFloor();
            ddlBED_RoomNumber.Items.Clear();
            ddlBED_RoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
        }

        protected void ddlBedFloorName_TextChanged(object sender, EventArgs e)
        {
            BindRoom();
        }
        protected void BindLockerHostel()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Hostel");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlLockerHostelName.DataSource = ds1;
                ddlLockerHostelName.DataTextField = "Name";
                ddlLockerHostelName.DataValueField = "Id";
                ddlLockerHostelName.DataBind();
                ddlLockerHostelName.Items.Insert(0, new ListItem("--Select Hostel Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindLockerBlock()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Block");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlLockerBlockName.DataSource = ds1;
                ddlLockerBlockName.DataTextField = "Name";
                ddlLockerBlockName.DataValueField = "Id";
                ddlLockerBlockName.DataBind();
                ddlLockerBlockName.Items.Insert(0, new ListItem("--Select Block Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindLockerFloor()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Floor");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddllockerFloorName.DataSource = ds1;
                ddllockerFloorName.DataTextField = "Name";
                ddllockerFloorName.DataValueField = "Id";
                ddllockerFloorName.DataBind();
                ddllockerFloorName.Items.Insert(0, new ListItem("--Select Floor Name--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void BindlockerRoom()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "Room");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                cmd.Parameters.AddWithValue("@FloorID", ddllockerFloorName.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlLockerRoomNumber.DataSource = ds1;
                ddlLockerRoomNumber.DataTextField = "Name";
                ddlLockerRoomNumber.DataValueField = "Id";
                ddlLockerRoomNumber.DataBind();
                ddlLockerRoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void BindLockerBed()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", "BedLocker");
                cmd.Parameters.AddWithValue("@HostelId", ddlLockerHostelName.SelectedValue);
                cmd.Parameters.AddWithValue("@BlockNumber", ddlLockerBlockName.SelectedValue);
                cmd.Parameters.AddWithValue("@FloorID", ddllockerFloorName.SelectedValue);
                cmd.Parameters.AddWithValue("@RoomId", ddlLockerRoomNumber.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                da.Fill(ds1);

                ddlBedNumber.DataSource = ds1;
                ddlBedNumber.DataTextField = "Name";
                ddlBedNumber.DataValueField = "Id";
                ddlBedNumber.DataBind();
                ddlBedNumber.Items.Insert(0, new ListItem("--Select Bed Number--", "0"));
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void ddlLockerHostelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLockerBlock();
            ddllockerFloorName.Items.Clear();
            ddllockerFloorName.Items.Insert(0, new ListItem("--Select Floor Name--", "0"));
            ddlLockerRoomNumber.Items.Clear();
            ddlLockerRoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
            ddlBedNumber.Items.Clear();
            ddlBedNumber.Items.Insert(0, new ListItem("--Select Bed Number--", "0"));
        }

        protected void ddlLockerBlockName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLockerFloor();
            ddlLockerRoomNumber.Items.Clear();
            ddlLockerRoomNumber.Items.Insert(0, new ListItem("--Select Room Number--", "0"));
            ddlBedNumber.Items.Clear();
            ddlBedNumber.Items.Insert(0, new ListItem("--Select Bed Number--", "0"));
        }

        protected void ddllockerFloorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindlockerRoom();
            ddlBedNumber.Items.Clear();
            ddlBedNumber.Items.Insert(0, new ListItem("--Select Bed Number--", "0"));
        }

        protected void ddlLockerRoomNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLockerBed();
        }

    }
}