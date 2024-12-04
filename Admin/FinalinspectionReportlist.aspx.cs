using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FinalinspectionReportlist : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                GVBinddata();
            }
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {

    }

    protected void GvInvoiceList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                String ID1 = e.CommandArgument.ToString();

                int ID = Convert.ToInt32(ID1);

                Report(ID);

                
            }
        }
    }

    protected void GvInvoiceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    protected void GVBinddata()
    {
        try
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("select Id,CustomerName,Project,Jo,PoNo from TblFinalinspectionHDr ", con);
            sad.Fill(dt);
            GvInvoiceList.DataSource = dt;
            GvInvoiceList.DataBind();
            GvInvoiceList.EmptyDataText = "Record Not Found";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvInvoiceList.ClientID + "', 400, 1020 , 40 ,true); </script>", false);
            con.Close();
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void Report(int ID)
    {

        //String flg = "Excel";
        String flg = "PDF";
        String filename = "A";
        DataSet Dtt = new DataSet();
        string strConnString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_FinalInspection]", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Getinspectionrecords");
                cmd.Parameters.AddWithValue("@HeaderID", ID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(Dtt);


                    }
                }
            }
        }

        if (Dtt.Tables.Count > 0)
        {

            if (Dtt.Tables[0].Rows.Count > 0)
            {
                ReportDataSource obj1 = new ReportDataSource("DataSet1", Dtt.Tables[0]);
                ReportDataSource obj2 = new ReportDataSource("DataSet2", Dtt.Tables[1]);
                //ReportDataSource obj3 = new ReportDataSource("DataSet3", Dtt.Tables[2]);
                ReportViewer1.LocalReport.DataSources.Add(obj1);
                ReportViewer1.LocalReport.DataSources.Add(obj2);
                //ReportViewer1.LocalReport.DataSources.Add(obj3);
                ReportViewer1.LocalReport.ReportPath = "RdlcReports\\Finalinspection.rdlc";
                ReportViewer1.LocalReport.Refresh();
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                byte[] bytePdfRep = ReportViewer1.LocalReport.Render(flg, null, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;

                if (flg == "pdf")
                {
                    Response.ContentType = "application/vnd.pdf";
                }
                else if (flg == "Excel")
                {
                    Response.ContentType = "application/vnd.xls";
                }

                //Response.AddHeader("content-disposition", "attachment; filename=SalesSummaryReports." +(flg == "pdf" ? "pdf" : "xls"));
                //Response.AddHeader("content-disposition", "attachment; filename = '" + filename + "'.xls");
                Response.AddHeader("content-disposition", "attachment; filename = '" + filename + "'.PDF");



                Response.BinaryWrite(bytePdfRep);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.Reset();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Found...........!')", true);
            }
        }
    }
}