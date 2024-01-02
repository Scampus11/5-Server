using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Drawing;
using ClosedXML.Excel;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.Net.Mail;
using QRCoder;
using System.Globalization;
using SMS.Class;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Net.Mime;
using SMS.BussinessLayer;
using SMS.CommonClass;

namespace SMS
{
    public partial class SMS_Digital_Form : System.Web.UI.Page
    {
        static int RoleCheck = 0;
        string QRimages = "";
        DataTable dt_Grid = new DataTable();
        LogFile logFile = new LogFile();
        DataTable ds1 = new DataTable();
        SqlConnection con = new SqlConnection();
        BS BS = new BS();
        DataTable DT = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    CheckCanteensAGBGAccessforUser();
                    if (Request.QueryString["Id"] != null)
                    {
                        if (File.Exists(Server.MapPath(@"\XML\Student_Badge.label")))
                        {
                            txtxml.Text = File.ReadAllText(Server.MapPath(@"\XML\Student_Badge.label"));
                        }
                        else
                        {
                            txtxml.Text = "";
                        }
                        EditData();
                    }
                    else if (Request.QueryString["ADD"] != null)
                    {
                        AG("");
                        BindCanteen2();
                        Blockgroups("");
                        AddDigitalId();
                        lnkGenerate_Click(sender, e);
                    }
                    else if (Request.QueryString["StudentId"] != null)
                    {
                        divgrid.Visible = true;
                        DivEdit.Visible = false;
                        FillGrid();
                    }
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
        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
        protected void AddDigitalId()
        {
            try
            {
                lnkupdate.Visible = false;
                lnkEmail.Visible = false;
                lnksave.Visible = true;
                divgrid.Visible = false;
                DivEdit.Visible = true;
                TxtAssignDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                SqlCommand cmd = new SqlCommand("SP_EditDigitalId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Get");
                cmd.Parameters.AddWithValue("@Id", Request.QueryString["StudentId"].ToString().Trim());
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblStudentId.Text = ds.Tables[0].Rows[0]["StudentId"].ToString();
                    lblStudentName.Text = ds.Tables[0].Rows[0]["StudentName"].ToString();
                    lblDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                    imgbarcode2.Visible = true;
                    if (ds.Tables[0].Rows[0]["StudentImages"].ToString() == "")
                    {
                        image2.ImageUrl = "~/Images/images1.jpg";
                    }
                    else
                    {
                        image2.ImageUrl = ds.Tables[0].Rows[0]["StudentImages"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void FillGrid(string sortExpression = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetDigitalId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                cmd.Parameters.AddWithValue("@StudentId", Request.QueryString["StudentId"].ToString().Trim());

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
                    SqlCommand cmd1 = new SqlCommand("SP_EditDigitalId", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Flag", "StudentData");
                    cmd1.Parameters.AddWithValue("@Id", Request.QueryString["StudentId"].ToString().Trim());
                    cmd1.Connection = con;
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);

                    lblStudentName2.Text = ds1.Tables[0].Rows[0]["StudentName"].ToString();
                    lblStudentId2.Text = Request.QueryString["StudentId"].ToString().Trim();
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
            Response.Redirect("SMS_Digital_Form.aspx?StudentId=" + Request.QueryString["StudentId"].ToString().Trim() + "&ADD=ADD");
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            string ids = "";
            ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            Response.Redirect("SMS_Digital_Form.aspx?StudentId=" + Request.QueryString["StudentId"].ToString().Trim() + "&Id=" + ids);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    try
                    {
                        string DIgitalToken = "";
                        lblCompare.Visible = false;
                        lblValidExpireDate.Visible = false;
                        lblGenerate.Visible = false;
                        //AGs
                        string ReaderId = string.Empty;
                        List<string> Reader_List = new List<string>();
                        foreach (RepeaterItem item in RepeatReader.Items)
                        {
                            CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                            Label lblId = (Label)item.FindControl("lblId");
                            if (chkReader.Checked)
                            {
                                Reader_List.Add(lblId.Text);
                            }
                            ReaderId = String.Join(",", Reader_List.ToArray());

                        }
                        //BGs
                        string BGs = string.Empty;
                        List<string> BGs_List = new List<string>();
                        foreach (RepeaterItem item in RepeatBlock.Items)
                        {
                            CheckBox chkBlockGroup = (CheckBox)item.FindControl("chkBlockGroup");
                            Label lblId = (Label)item.FindControl("lblId");
                            if (chkBlockGroup.Checked)
                            {
                                BGs_List.Add(lblId.Text);
                            }
                            BGs = String.Join(",", BGs_List.ToArray());
                        }

                        SqlCommand cmd = new SqlCommand("SP_SMS_DigitalId_upset", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", "Insert");
                        cmd.Parameters.AddWithValue("@StudentId", Request.QueryString["StudentId"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@Exp_Time", txtExpireDate.Text);
                        cmd.Parameters.AddWithValue("@AsignDate", TxtAssignDate.Text);
                        cmd.Parameters.AddWithValue("@Generate_D_Id", Session["DigitalId"].ToString());
                        cmd.Parameters.AddWithValue("@Digital_Status", ddlCardstatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@Title", txttitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@AGS", ReaderId);
                        cmd.Parameters.AddWithValue("@Canteens", ddlcanteen2.SelectedValue);
                        cmd.Parameters.AddWithValue("@BGS", BGs);
                        con.Open();
                        cmd.ExecuteNonQuery();


                        DIgitalToken = GetToken();
                        PushNotification(DIgitalToken, Request.QueryString["StudentId"].ToString());
                        SqlCommand cmd1 = new SqlCommand("SP_EditDigitalId", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Flag", "getDigitalid");
                        cmd1.Parameters.AddWithValue("@Id", Session["DigitalId"].ToString());
                        cmd1.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["DigitalId"] = null;
                            Response.Redirect("SMS_Digital_Form.aspx?StudentId=" + Request.QueryString["StudentId"].ToString().Trim() + "&Id=" + ds.Tables[0].Rows[0]["Id"].ToString());
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        LogFile.LogError(ex);
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        protected string GetToken()
        {
            string Token = "";
            SqlCommand cmd1 = new SqlCommand("SP_GetDigitalToken", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@StudentId", Request.QueryString["StudentId"].ToString().Trim());
            cmd1.Connection = con;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Token = ds1.Tables[0].Rows[0]["Token"].ToString();
            }
            else
            {
                Token = "";
            }
            return Token;
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    string DIgitalToken = "";
                    lblCompare.Visible = false;
                    lblValidExpireDate.Visible = false;
                    lblGenerate.Visible = false;
                    //AGs
                    string ReaderId = string.Empty;
                    List<string> Reader_List = new List<string>();
                    foreach (RepeaterItem item in RepeatReader.Items)
                    {
                        CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                        Label lblId = (Label)item.FindControl("lblId");
                        if (chkReader.Checked)
                        {
                            Reader_List.Add(lblId.Text);
                        }
                        ReaderId = String.Join(",", Reader_List.ToArray());

                    }
                    //BGs
                    string BGs = string.Empty;
                    List<string> BGs_List = new List<string>();
                    foreach (RepeaterItem item in RepeatBlock.Items)
                    {
                        CheckBox chkBlockGroup = (CheckBox)item.FindControl("chkBlockGroup");
                        Label lblId = (Label)item.FindControl("lblId");
                        if (chkBlockGroup.Checked)
                        {
                            BGs_List.Add(lblId.Text);
                        }
                        BGs = String.Join(",", BGs_List.ToArray());
                    }
                    SqlCommand cmd = new SqlCommand("SP_SMS_DigitalId_upset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "Update");
                    cmd.Parameters.AddWithValue("@StudentId", Request.QueryString["Id"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Exp_Time", txtExpireDate.Text);
                    cmd.Parameters.AddWithValue("@AsignDate", TxtAssignDate.Text);
                    cmd.Parameters.AddWithValue("@Generate_D_Id", Session["DigitalId"].ToString());
                    cmd.Parameters.AddWithValue("@Digital_Status", ddlCardstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@Title", txttitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@AGS", ReaderId);
                    cmd.Parameters.AddWithValue("@Canteens", ddlcanteen2.SelectedValue);
                    cmd.Parameters.AddWithValue("@BGS", BGs);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Session["DigitalId"] = null;
                    //DIgitalToken = GetToken();
                    //PushNotification(DIgitalToken, Request.QueryString["StudentId"].ToString());
                    Response.Redirect("SMS_Digital_Form.aspx?StudentId=" + Request.QueryString["StudentId"].ToString().Trim());
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
                //string ids = "";
                //ids = string.Empty;
                //ids = (sender as LinkButton).CommandArgument;
                LinkButton lnk = sender as LinkButton;
                GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
                Label Ids = (Label)gvr.FindControl("lblId_no");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update SMS_Digital_Form set AGS='',Canteens='',BGS='' where Id= '" + Ids.Text + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                FillGrid();
                Response.Redirect("SMS_Digital_Form.aspx?StudentId=" + Request.QueryString["StudentId"]);
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected bool Validation()
        {
            if (txtExpireDate.Text == "")
            {
                lblCompare.Visible = false;
                lblValidExpireDate.Visible = true;
                lblGenerate.Visible = false;
                imgbarcode2.Visible = true;
                //imgbarcode2.ImageUrl = "~/Images/Staticbarcode.jpg";
                txtExpireDate.Focus();
                return false;
            }
            string assigndate = TxtAssignDate.Text;
            string expdate = txtExpireDate.Text;
            string[] formats = { "dd/MM/yyyy" };
            var Date1 = DateTime.ParseExact(assigndate, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            var Date2 = DateTime.ParseExact(expdate, formats, new CultureInfo("en-US"), DateTimeStyles.None);

            int value = DateTime.Compare(Date1, Date2);
            if (value > 0)
            {
                lblCompare.Visible = true;
                lblValidExpireDate.Visible = false;
                lblGenerate.Visible = false;
                imgbarcode2.Visible = true;
                //imgbarcode2.ImageUrl = "~/Images/Staticbarcode.jpg";
                txtExpireDate.Focus();
                return false;
            }

            if (Session["DigitalId"] == null)
            {
                lblCompare.Visible = false;
                lblValidExpireDate.Visible = false;
                lblGenerate.Visible = true;
                lblGenerate.Focus();
                return false;
            }

            return true;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentId"] == null)
            {
                Response.Redirect("SMS_Digital_Form.aspx?StudentId=" + lblStudentId.Text);
            }
            else
            {
                Response.Redirect("SMS_Digital_Form.aspx?StudentId=" + Request.QueryString["StudentId"].ToString().Trim());
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

        protected void PushNotification(string DeviceToken, string Massage)
        {
            try
            {
                string ServerKey = WebConfigurationManager.AppSettings["ServerKey"].ToString();
                var SenderId = WebConfigurationManager.AppSettings["SenderId"].ToString();

                WebRequest Trequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                Trequest.Method = "post";
                Trequest.ContentType = "application/json";
                var objnotification = new
                {
                    //to = "/topics/DigitalID",
                    to = DeviceToken,
                    //to = "cICQqzGEvPk:APA91bE3M37d7oX1VuJRtslRs1A3bsUG9k_tHePszsnN_YI33gwdhJhYlJ2knaAcsEQU3Pe8q1zJ3hEONvZuY_o6iLom-GRllqwESq9iBPsAba43AEEte92H2v4_Z7gRpaStpMcAhxMH",
                    notification = new
                    {
                        Title = "Welcome to StudentID App",
                        body = "Your request for Student ID : " + Request.QueryString["StudentId"].ToString() + "has been approved. Enjoy!"
                        //click_action="TOP_STORY ACTIVITY"
                    }
                    //    data=new {
                    //        //story_id="story_12345"
                    //}

                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(objnotification);

                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                Trequest.Headers.Add(string.Format("Authorization: Key={0}", ServerKey));
                Trequest.Headers.Add(string.Format("Sender: Id={0}", SenderId));

                Trequest.ContentLength = byteArray.Length;
                Trequest.ContentType = "application/json";
                using (Stream dataStream = Trequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse Tresponse = Trequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = Tresponse.GetResponseStream())
                        {
                            using (StreamReader Treader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFireBaseServer = Treader.ReadToEnd();
                                FCMResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFireBaseServer);
                                if (response.success == 1)
                                {
                                    Console.WriteLine("succeeded");
                                }
                                else
                                {
                                    Console.WriteLine("failed");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                imgbarcode2.Visible = false;
                Random randomAccessToken = new Random();
                DateTime currentDateTime = DateTime.Now;
                string Encrypt = randomAccessToken.Next(0, 100000) + Request.QueryString["StudentId"].Trim() + currentDateTime.ToString("yyyyMMddHHmmssfff");
                Session["DigitalId"] = Encrypt;
                string code = EnryptString(Encrypt);
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                //QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                // Bitmap qrCodeImage = qrCode.GetGraphic(20);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 200;
                imgBarCode.Width = 200;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        imgbarcode2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        imgbarcode3.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    plBarCode.Controls.Add(imgBarCode);
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void EditData()
        {
            try
            {
                divgrid.Visible = false;
                DivEdit.Visible = true;
                lnkupdate.Visible = true;
                lnkEmail.Visible = true;
                lnksave.Visible = false;
                lnkAdd.Visible = false;
                lnkGenerate.Visible = false;
                preview.Attributes.Add("style", "display:block");
                lnkprint.Attributes.Add("style", "display:block");
                DT = BS.SP_DigitalIdInfo("Edit", Request.QueryString["Id"].ToString().Trim());
                if (DT.Rows.Count > 0)
                {
                    lblStudentName2.Text = DT.Rows[0]["StudentName"].ToString();
                    lblStudentId.Text = DT.Rows[0]["StudentId"].ToString();
                    txtStudentName.Text = DT.Rows[0]["StudentName"].ToString();
                    txtStudentId.Text = DT.Rows[0]["StudentId"].ToString();
                    lblStudentName.Text = DT.Rows[0]["StudentName"].ToString();
                    lblDepartment.Text = DT.Rows[0]["Department"].ToString();
                    txtdepartment.Text = DT.Rows[0]["Department"].ToString();
                    txtAG.Text = DT.Rows[0]["Access_Group_ID"].ToString();
                    txtvalidate.Text = DT.Rows[0]["validate"].ToString();
                    txtbatchyear.Text = DT.Rows[0]["Batch_Year"].ToString();
                    txtExpireDate.Text = DT.Rows[0]["Exp_Time"].ToString();
                    TxtAssignDate.Text = DT.Rows[0]["AsignDate"].ToString();
                    txttitle.Text = DT.Rows[0]["Title"].ToString();
                    lblEmailId.Text = DT.Rows[0]["EmailId"].ToString();
                    lblImage64byte.Text = DT.Rows[0]["Image64byte"].ToString();
                    ddlCardstatus.ClearSelection();
                    ddlCardstatus.Items.FindByValue(DT.Rows[0]["Digital_Status"].ToString()).Selected = true;

                    if (DT.Rows[0]["StudentImages"].ToString() == "")
                    {
                        image2.ImageUrl = "~/Images/images1.jpg";
                    }
                    else
                    {
                        image2.ImageUrl = DT.Rows[0]["StudentImages"].ToString();
                    }
                    string ImgPath = image2.ImageUrl.Replace("~/Images/StudentImages", "Images/StudentImages");
                    if (File.Exists(Server.MapPath(ImgPath)))
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(ImgPath));
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            string base64String = Convert.ToBase64String(imageBytes);
                            Image1.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }
                    else
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("Images/images1.jpg"));
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            string base64String = Convert.ToBase64String(imageBytes);
                            Image1.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }

                    if (DT.Rows[0]["Generate_D_Id"].ToString() != "")
                    {
                        imgbarcode2.Visible = false;
                        Session["DigitalId"] = DT.Rows[0]["Generate_D_Id"].ToString();
                        string code = EnryptString(DT.Rows[0]["Generate_D_Id"].ToString());
                        txtQrCode.Text = code;
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        //QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        // Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                        imgBarCode.Height = 200;
                        imgBarCode.Width = 200;
                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms.ToArray();
                                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                lblqrcode.Text = Convert.ToBase64String(byteImage);
                                imgbarcode2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                imgbarcode3.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            }
                            plBarCode.Controls.Add(imgBarCode);
                        }
                    }
                    else
                    {
                        imgbarcode2.Visible = true;
                    }
                    AG(DT.Rows[0]["AGS"].ToString());
                    BindCanteen2();
                    Blockgroups(DT.Rows[0]["BGS"].ToString());
                    ddlcanteen2.ClearSelection();
                    ddlcanteen2.Items.FindByValue(DT.Rows[0]["Canteens"].ToString()).Selected = true;
                    con.Open();
                    SqlCommand cmdlogo = new SqlCommand();
                    cmdlogo.CommandText = "select * from logo";
                    cmdlogo.Connection = con;
                    cmdlogo.ExecuteNonQuery();
                    SqlDataAdapter dalogo = new SqlDataAdapter(cmdlogo);
                    DataSet dslogo = new DataSet();
                    dalogo.Fill(dslogo);
                    if (dslogo.Tables[0].Rows.Count > 0)
                    {
                        txtlogoname.Text = dslogo.Tables[0].Rows[0]["Name"].ToString();
                        string ImgPath1 = dslogo.Tables[0].Rows[0]["Images"].ToString().Replace("~/Images/ICON", "Images/ICON");
                        System.Drawing.Image image1 = System.Drawing.Image.FromFile(Server.MapPath(ImgPath1));

                        using (MemoryStream m = new MemoryStream())
                        {
                            image1.Save(m, image1.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            string base64String = Convert.ToBase64String(imageBytes);
                            imglogo.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            txtExpireDate.Text = "";
            txttitle.Text = "";
            Session["DigitalId"] = null;
            imgbarcode2.Visible = true;
            //imgbarcode2.ImageUrl = "~/Images/Staticbarcode.jpg";
        }
        protected void BindCanteen2()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from tbl_Access_Group where Is_Canteen=1";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcanteen2.DataSource = ds;
                    ddlcanteen2.DataTextField = "Access_Group_Name";
                    ddlcanteen2.DataValueField = "Id";
                    ddlcanteen2.DataBind();
                    ddlcanteen2.Items.Insert(0, new ListItem("--Select Canteen--", "0"));
                }
                else
                {
                    ddlcanteen2.DataSource = null;
                    ddlcanteen2.DataBind();
                    ddlcanteen2.Items.Insert(0, new ListItem("--Select Canteen--", "0"));

                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
        }
        protected void Blockgroups(string BGs)
        {
            //Block Group
            con.Open();
            SqlCommand cmdBlock = new SqlCommand("Bind_BlockGroup", con);
            cmdBlock.CommandType = CommandType.StoredProcedure;
            cmdBlock.Connection = con;
            cmdBlock.ExecuteNonQuery();
            SqlDataAdapter daBlock = new SqlDataAdapter(cmdBlock);
            DataSet dsBlock = new DataSet();
            String BlockgroupId = BGs;
            String[] BlockgroupIdArray = BlockgroupId.Split(',');
            daBlock.Fill(dsBlock);
            if (dsBlock.Tables[0].Rows.Count > 0)
            {
                RepeatBlock.DataSource = dsBlock;
                RepeatBlock.DataBind();
                foreach (RepeaterItem item in RepeatBlock.Items)
                {
                    CheckBox chkBlockGroup = (CheckBox)item.FindControl("chkBlockGroup");
                    Label lblId = (Label)item.FindControl("lblId");
                    for (int i = 0; i < BlockgroupIdArray.Length; i++)
                    {
                        if (lblId.Text == BlockgroupIdArray[i].ToString())
                        {
                            chkBlockGroup.Checked = true;
                        }
                        if (RoleCheck == 1)
                        {
                            chkBlockGroup.Enabled = false;
                        }
                        else
                        {
                            chkBlockGroup.Enabled = true;
                        }
                    }
                }
            }
            con.Close();
        }
        protected void AG(string AGs)
        {
            //Non Canteen
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SP_GetAccess_List_nonCanteen_Edit", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("@StudentId", Request.QueryString["StudentId"].ToString().Trim());
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            String Door_Group_Id = AGs;
            String[] Door_Group_Id_Array = Door_Group_Id.Split(',');
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                RepeatReader.DataSource = ds1;
                RepeatReader.DataBind();
                foreach (RepeaterItem item in RepeatReader.Items)
                {
                    CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                    Label lblId = (Label)item.FindControl("lblId");
                    for (int i = 0; i < Door_Group_Id_Array.Length; i++)
                    {
                        if (lblId.Text == Door_Group_Id_Array[i].ToString())
                        {
                            chkReader.Checked = true;
                        }
                        if (RoleCheck == 1)
                        {
                            chkReader.Enabled = false;
                        }
                        else
                        {
                            chkReader.Enabled = true;
                        }
                    }
                }
            }
            con.Close();
        }
        public void Email()
        {
            try
            {
                if (lblEmailId.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("SMS_SP_Get_Email_Configuration", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Flag", "Get");
                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ////Digital Id Email
                        string Host_Emp_EmailId = Session["Email_Id"].ToString();
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(ds.Tables[0].Rows[0]["Email_Address"].ToString());
                        msg.To.Add(lblEmailId.Text);
                        msg.Subject = "Digital ID for " + lblStudentName.Text + "-" + lblStudentId.Text;
                        //msg.Body = body;
                        msg.Body = "Digital ID for " + lblStudentName.Text + "-" + lblStudentId.Text;
                        msg.IsBodyHtml = true;
                        //if (ServiceEmails != "")
                        //{
                        //    msg.CC.Add(ServiceEmails);
                        //}
                        //msg.Priority = MailPriority.High;
                        SmtpClient client = new SmtpClient();
                        // decrypt parameters
                        var key = "b14ca5898a4e4133bbce2ea2315a1916";
                        var decrptedInput = DecryptString(key, ds.Tables[0].Rows[0]["password"].ToString());
                        client.Credentials = new NetworkCredential(ds.Tables[0].Rows[0]["Email_Address"].ToString(), decrptedInput);
                        client.Host = ds.Tables[0].Rows[0]["Outgoing_Services"].ToString();
                        client.Port = Convert.ToInt32(ds.Tables[0].Rows[0]["SMTP"].ToString());
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        if (ds.Tables[0].Rows[0]["Connection"].ToString() == "1")
                        {
                            client.EnableSsl = true;
                        }
                        else
                        {

                        }
                        string CanteensList = "";
                        string BGsList = "";
                        string AGsList = "";
                        if (ddlcanteen2.SelectedItem.Text != "--Select Canteen--")
                        {
                            CanteensList = ddlcanteen2.SelectedItem.Text;
                        }
                        //AGs
                        List<string> Reader_List = new List<string>();
                        foreach (RepeaterItem item in RepeatReader.Items)
                        {
                            CheckBox chkReader = (CheckBox)item.FindControl("chkReader");
                            Label lblId = (Label)item.FindControl("lblId");
                            if (chkReader.Checked)
                            {
                                Reader_List.Add(chkReader.Text);
                            }
                            AGsList = String.Join(",", Reader_List.ToArray());

                        }
                        //BGs
                        List<string> BGs_List = new List<string>();
                        foreach (RepeaterItem item in RepeatBlock.Items)
                        {
                            CheckBox chkBlockGroup = (CheckBox)item.FindControl("chkBlockGroup");
                            Label lblId = (Label)item.FindControl("lblId");
                            if (chkBlockGroup.Checked)
                            {
                                BGs_List.Add(chkBlockGroup.Text);
                            }
                            BGsList = String.Join(",", BGs_List.ToArray());
                        }
                        #region Company Info Details
                        string LogoFilePath = "";
                        string CampusName = "";
                        DT = BS.SP_CompanyInfo("Get", 0, "", "", "", "", "","");
                        if (DT.Rows.Count > 0)
                        {
                            //Image For Logo
                            if (DT.Rows[0]["DigitalIdImages"].ToString() == "")
                            {
                                LogoFilePath = "~/Images/images1.jpg";
                            }
                            else
                            {
                                string path = Server.MapPath(LogoFilePath);
                                if (Checkpath.CheckPathExitsOrNot(path))
                                {
                                    LogoFilePath = DT.Rows[0]["DigitalIdImages"].ToString();
                                }
                                else
                                {
                                    LogoFilePath = "~/Images/images1.jpg";
                                }
                            }
                            CampusName = DT.Rows[0]["Name"].ToString();
                        }
                        #endregion

                        #region Student DG Details
                        string StudentImages = "";
                        DT = BS.SP_DigitalIdInfo("Edit", Request.QueryString["Id"].ToString().Trim());
                        if (DT.Rows.Count > 0)
                        {
                            //Image For Student Image
                            if (DT.Rows[0]["StudentImages"].ToString() == "")
                            {
                                StudentImages = "~/Images/images1.jpg";
                            }
                            else
                            {
                                string path = Server.MapPath(StudentImages);
                                if (Checkpath.CheckPathExitsOrNot(path))
                                {
                                    StudentImages = DT.Rows[0]["StudentImages"].ToString();
                                }
                                else
                                {
                                    StudentImages = "~/Images/images1.jpg";
                                }
                            }
                        }
                        #endregion

                        Dictionary<String, String> replacements = new Dictionary<string, string>();
                        replacements.Add("StudentId", lblStudentId.Text);
                        replacements.Add("StudentName", lblStudentName.Text);
                        replacements.Add("DepartmentName", lblDepartment.Text);
                        replacements.Add("ValidFrom", TxtAssignDate.Text);
                        replacements.Add("ValidTo", txtExpireDate.Text);
                        replacements.Add("CanteenList", CanteensList);
                        replacements.Add("BGList", BGsList);
                        replacements.Add("AGList", AGsList);
                        replacements.Add("CampusName", CampusName);

                        AlternateView html = CreateAlternateViewFromTemplate(Server.MapPath("EmailTemplate/DigitalIdEmail.html"), "text/html", replacements);

                        #region Barcode Image
                        Byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage(lblqrcode.Text));
                        System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
                        LinkedResource img1 = new LinkedResource(streamBitmap, "image/jpeg");
                        img1.ContentId = "imgBarcode";
                        html.LinkedResources.Add(img1);
                        #endregion

                        #region Student Image
                        System.Drawing.Image image1 = System.Drawing.Image.FromFile(Server.MapPath(StudentImages));
                        using (MemoryStream m = new MemoryStream())
                        {
                            image1.Save(m, image1.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            // Convert byte[] to Base64 String
                            StudentImages = Convert.ToBase64String(imageBytes);
                        }
                        Byte[] bitmapData2 = Convert.FromBase64String(FixBase64ForImage(StudentImages));
                        System.IO.MemoryStream streamBitmap2 = new System.IO.MemoryStream(bitmapData2);
                        LinkedResource img2 = new LinkedResource(streamBitmap2, "image/jpeg");
                        img2.ContentId = "imgStudent";
                        html.LinkedResources.Add(img2);
                        #endregion

                        #region Campus Logo 
                        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(LogoFilePath));
                        string Visitorimage = "";
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            // Convert byte[] to Base64 String
                            Visitorimage = Convert.ToBase64String(imageBytes);
                        }
                        Byte[] bitmapData3 = Convert.FromBase64String(FixBase64ForImage(Visitorimage));
                        System.IO.MemoryStream streamBitmap3 = new System.IO.MemoryStream(bitmapData3);
                        LinkedResource img3 = new LinkedResource(streamBitmap3, "image/jpeg");
                        img3.ContentId = "imgLogo";
                        html.LinkedResources.Add(img3);
                        #endregion

                        msg.AlternateViews.Add(html);
                        //client.UseDefaultCredentials = true;
                        client.Send(msg);
                        lblEmailmsg.Visible = true;
                        imgbarcode2.Visible = true;
                    }

                }
                else
                {
                    lblEmailError.Visible = true;
                    imgbarcode2.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
                lblEmailError.Visible = true;
                lblEmailError.Text = ex.ToString();
            }
        }
        public static string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }
        static AlternateView CreateAlternateViewFromTemplate(String path, String mediaType, IDictionary<String, String> replacements)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                String content = sr.ReadToEnd();

                if (replacements != null)
                {
                    foreach (KeyValuePair<String, String> replacement in replacements)
                    {
                        content = content.Replace(String.Concat("{", replacement.Key, "}"), replacement.Value);
                    }
                }

                AlternateView view = AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, mediaType);
                view.ContentType.CharSet = Encoding.UTF8.BodyName;
                if (mediaType == "text/html")
                {
                    view.TransferEncoding = TransferEncoding.QuotedPrintable;
                }
                return view;
            }
        }
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        protected void lnkEmail_Click(object sender, EventArgs e)
        {
            Email();
        }
        protected void CheckCanteensAGBGAccessforUser()
        {
            SqlCommand cmd1 = new SqlCommand("CheckBulkAG", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Flag", "Student");
            cmd1.Parameters.AddWithValue("@Role_Id", Session["Role_Id"].ToString().Trim());
            cmd1.Parameters.AddWithValue("@Staff_Id", Session["Staff_Id"].ToString().Trim());
            cmd1.Connection = con;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            for (int i = 0; i < ds1.Rows.Count; i++)
            {
                if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
                {
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = true;
                    diveditBG.Visible = true;
                    RoleCheck = 0;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                {
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = true;
                    diveditBG.Visible = true;
                    RoleCheck = 1;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                {
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = true;
                    diveditBG.Visible = false;
                    RoleCheck = 0;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
                {
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = false;
                    diveditBG.Visible = true;
                    RoleCheck = 0;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
                {
                    diveditcanteens.Visible = false;
                    diveditAG.Visible = true;
                    diveditBG.Visible = true;
                    RoleCheck = 0;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "1" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                {
                    diveditcanteens.Visible = true;
                    diveditAG.Visible = false;
                    diveditBG.Visible = false;
                    RoleCheck = 0;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "1" && ds1.Rows[i]["BG"].ToString().Trim() == "0")
                {
                    diveditcanteens.Visible = false;
                    diveditAG.Visible = true;
                    diveditBG.Visible = false;
                    RoleCheck = 0;
                }
                else if (ds1.Rows[i]["Canteens"].ToString().Trim() == "0" && ds1.Rows[i]["AG"].ToString().Trim() == "0" && ds1.Rows[i]["BG"].ToString().Trim() == "1")
                {
                    diveditcanteens.Visible = false;
                    diveditAG.Visible = false;
                    diveditBG.Visible = true;
                    RoleCheck = 0;
                }
            }
        }
    }
}