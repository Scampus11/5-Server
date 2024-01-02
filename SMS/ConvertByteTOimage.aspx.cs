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

namespace SMS
{
    public partial class ConvertByteTOimage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();
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
        protected void btnConvert_Click(object sender, EventArgs e)
        {
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "select * from Officer_Master where Id=6";
            SqlCommand cmd = new SqlCommand("SP_Insert_ByteToImageFolader_Student", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    byte[] jpegByteArray = (byte[])ds.Tables[0].Rows[i]["StudentImages_Blob"];
                    //byte[] jpegByteArray = Convert.FromBase64String("" + ds.Tables[0].Rows[0]["StaffImage_Blob"] + "");
                    System.Drawing.Image image;
                    using (MemoryStream ms = new MemoryStream(jpegByteArray))
                    {
                        image = System.Drawing.Image.FromStream(ms);
                        //image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        System.Drawing.Bitmap returnImage = new System.Drawing.Bitmap(System.Drawing.Image.FromStream(ms, true, true), 100, 100);
                        returnImage.Save(Server.MapPath("~/Images/StudentImages/" + ds.Tables[0].Rows[i]["StudentId"].ToString().Replace("/", "_") + ".jpg"));
                        string FilePath = "~/Images/StudentImages/" + ds.Tables[0].Rows[i]["StudentId"].ToString().Replace("/", "_") + ".jpg";
                        SqlCommand cmd1 = new SqlCommand("SP_Image_Save_Student_Folder", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@StudentId", ds.Tables[0].Rows[i]["StudentId"].ToString());
                        cmd1.Parameters.AddWithValue("@FilePath", FilePath.ToString());
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('1000 Records Insert Successfully!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('All Records Inserted!!');", true);
            }
            
            
        }

        protected void BtnEmployee_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SP_Insert_ByteToImageFolader_Staff", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    byte[] jpegByteArray1 = (byte[])ds1.Tables[0].Rows[i]["StaffImage_Blob"];
                    //byte[] jpegByteArray = Convert.FromBase64String("" + ds.Tables[0].Rows[0]["StaffImage_Blob"] + "");
                    System.Drawing.Image image1;
                    using (MemoryStream ms = new MemoryStream(jpegByteArray1))
                    {
                        image1 = System.Drawing.Image.FromStream(ms);
                        //image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        System.Drawing.Bitmap returnImage = new System.Drawing.Bitmap(System.Drawing.Image.FromStream(ms, true, true), 100, 100);
                        returnImage.Save(Server.MapPath("~/Images/StaffImages/" + ds1.Tables[0].Rows[i]["Staff_Id"].ToString().Replace("/", "_") + ".jpg"));
                        string FilePath = "~/Images/StaffImages/" + ds1.Tables[0].Rows[i]["Staff_Id"].ToString().Replace("/", "_") + ".jpg";
                        SqlCommand cmd1 = new SqlCommand("SP_Image_Save_Staff_Folder", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Staff_Id", ds1.Tables[0].Rows[i]["Staff_Id"].ToString());
                        cmd1.Parameters.AddWithValue("@FilePath", FilePath.ToString());
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('1000 Records Insert Successfully!!');", true);
        }
    }
}