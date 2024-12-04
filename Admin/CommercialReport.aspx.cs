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
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
    DataTable dt = new DataTable();
    CommonCls objClass = new CommonCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!this.IsPostBack)
            {
                //GetDrawingRptData();
            }
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
            //DateTime date = Convert.ToDateTime(txtDate.Text.Trim());
            //string dateString = date.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            DateTime FromDate = Convert.ToDateTime(txtfromdate.Text.Trim());
            string Fromdatestring = FromDate.ToString("yyyy - MM - dd", CultureInfo.InvariantCulture);
            DateTime Todate = Convert.ToDateTime(txttodate.Text.Trim());
            string Todatestring = Todate.ToString("yyyy - MM - dd", CultureInfo.InvariantCulture);
            string query = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[DB_ProcetechERP].[SP_CommercialReport_NewDatewise]";
            cmd.Parameters.AddWithValue("@FromDate", Fromdatestring);
            cmd.Parameters.AddWithValue("@ToDate", Todatestring);
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
            btnexcell.Visible = true;
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


    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Commercial Reports" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        dgvCommercialRpt.GridLines = GridLines.Both;
        dgvCommercialRpt.HeaderStyle.Font.Bold = true;
        dgvCommercialRpt.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();


    }


    protected void btnexcell_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
}

