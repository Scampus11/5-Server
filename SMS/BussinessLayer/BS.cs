using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using SMS.Class;

namespace SMS.BussinessLayer
{
    public class BS
    {
        SqlConnection con = new SqlConnection();
        DataTable DT = new DataTable();
        #region Check Connection String
        protected void Connection()
        {
            try
            {
                string filePath1 = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ConPath"].ToString());
                StreamReader sr = new StreamReader(filePath1);
                String ConnectionString = sr.ReadToEnd();
                con = new SqlConnection(ConnectionString);
                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
                HttpContext.Current.Response.Redirect("SMS_SQL_Connection.aspx");
            }

        }
        #endregion

        #region Company info for setting
        public DataTable SP_CompanyInfo(string Flag, int Id, string timer_time, string Public_timer
        ,string MusteringTime, string Name, string Comapny_FilePath, string DigitalId_FilePath)
        {
            DataTable DT_Companyinfo = new DataTable();
            try
            {
                Connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SPCompanyInfo", con);
                cmd.Parameters.AddWithValue("@Flag", Flag);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@timer_time", timer_time);
                cmd.Parameters.AddWithValue("@Public_timer_time", Public_timer);
                cmd.Parameters.AddWithValue("@MusteringTime", MusteringTime);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Comapny_FilePath", Comapny_FilePath);
                cmd.Parameters.AddWithValue("@DigitalId_FilePath", DigitalId_FilePath);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                if (Flag == "Update" || Flag == "Delete")
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DT_Companyinfo);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return DT_Companyinfo;
        }
        #endregion

        #region Digital Id Info
        public DataTable SP_DigitalIdInfo(string Flag, string Id)
        {
            try
            {
                Connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_EditDigitalId", con);
                cmd.Parameters.AddWithValue("@Flag", Flag);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                if (Flag == "Update" || Flag == "Delete")
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DT);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return DT;
        }
        #endregion

        #region Bind PCR 
        public DataTable BindPCR(string Flag)
        {
            try
            {
                Connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_Bind_PCR", con);
                cmd.Parameters.AddWithValue("@Flag", Flag);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DT);
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return DT;
        }
        #endregion

        #region BG Logs Details
        public DataSet BGLogsDetails(string Flag, int BG,string FromDate,string ToDate)
        {
            DataSet DS = new DataSet();
            try
            {
                Connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SPBGLogsDetails", con);
                cmd.Parameters.AddWithValue("@Flag", Flag);
                cmd.Parameters.AddWithValue("@BG", BG);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DS);
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return DS;
        }
        #endregion

        #region Set Timer Time 
        public DataSet Timer()
        {
            DataSet DS = new DataSet();
            try
            {
                Connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_GetLogoData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DS);
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return DS;
        }
        #endregion

        #region BG Logs List
        public DataTable BGLogsList(string Flag, int BG, string FromDate, string ToDate)
        {
            DataTable DS = new DataTable();
            try
            {
                Connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SPBGLogsList", con);
                cmd.Parameters.AddWithValue("@Flag", Flag);
                cmd.Parameters.AddWithValue("@BG", BG);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DS);
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return DS;
        }
        #endregion

        #region Update Image Path
        public DataTable UpdateImagePath(string Flag)
        {
            DataTable DS = new DataTable();
            try
            {
                Connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SPImagePathUpdate", con);
                cmd.Parameters.AddWithValue("@Flag", Flag);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DS);
                con.Close();
            }
            catch (Exception ex)
            {
                LogFile.LogError(ex);
            }
            return DS;
        }
        #endregion
    }
}