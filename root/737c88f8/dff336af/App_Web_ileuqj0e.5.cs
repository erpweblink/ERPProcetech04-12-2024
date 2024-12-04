#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\CommercialReport.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5F00047EF3FD298C2D24D79C7E31B8B0B2272A6E"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\CommercialReport.aspx.cs"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CommercialReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    CommonCls objClass = new CommonCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //GetDrawingRptData();
        }
    }

    protected override void InitializeCulture()
    {
        CultureInfo ci = new CultureInfo("en-IN");
        ci.NumberFormat.CurrencySymbol = "₹";
        Thread.CurrentThread.CurrentCulture = ci;
        base.InitializeCulture();
    }
    protected void GetDrawingRptData()
    {
        try
        {
            DateTime date = Convert.ToDateTime(txtDate.Text.Trim());
            string dateString = date.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            string query = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CommercialReport";
            cmd.Parameters.AddWithValue("@Date", dateString);
            cmd.Connection = con;
            con.Open();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dgvCommercialRpt.EmptyDataText = "No Records Found";
                    dgvCommercialRpt.DataSource = reader;
                    dgvCommercialRpt.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        try
        {
            GetDrawingRptData();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CommercialReport.aspx");
    }
}

#line default
#line hidden
