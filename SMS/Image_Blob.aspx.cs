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

namespace SMS
{
    
    public partial class Image_Blob : System.Web.UI.Page
    {
        byte[] bytes = new byte[1];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["livedbconnection"].ConnectionString);
        SqlConnection conLocal = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from tblstudent";
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@Search", "");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                for(int i=0;i<ds.Rows.Count;i++)
                {
                    SqlCommand cmd1 = new SqlCommand("SP_EmpImageBlob_upset", conLocal);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    
                    cmd1.Parameters.AddWithValue("@StudentId", ds.Rows[i]["StudentId"].ToString());
                    if (ds.Rows[i]["StudentImages_Blob"].ToString() == "")
                    {
                        cmd1.Parameters.AddWithValue("@StudentImages_Blob", bytes);
                    }
                    else
                    {
                        byte[] hash1 = (byte[])ds.Rows[i]["StudentImages_Blob"];
                        cmd1.Parameters.AddWithValue("@StudentImages_Blob", hash1);
                    }
                    conLocal.Open();
                    cmd1.ExecuteNonQuery();
                    conLocal.Close();
                }
            }

            
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('SuccessFully')</script>");
        }
    }
}