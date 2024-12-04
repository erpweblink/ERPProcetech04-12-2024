using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OutstandingReport_List : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

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
                //BindGV();
            }
        }

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSupplierList(string prefixText, int count)
    {
        return AutoFillSupplierName(prefixText);
    }

    public static List<string> AutoFillSupplierName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "Select DISTINCT [SupplierName] from [tblSupplierMaster] where " + "SupplierName like @Search + '%'";
                com.CommandText = "Select DISTINCT [cname] from [Company] where " + "cname like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["cname"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void BindGV()
    {
        string query1 = string.Empty;
        query1 = @"select BillingCustomer,InvoiceNo as DocNo,Invoicedate,'' as DueDate,SumOfProductAmt As Basic,ROUND(GrandTotalFinal, 2, 1) as Payable FROM tblTaxInvoiceHdr where BillingCustomer='" + txtcnamefilter.Text + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        GV_PDCReport.DataSource = dt;
        GV_PDCReport.DataBind();
        GV_PDCReport.EmptyDataText = "Record Not Found";
    }

    protected void txtcnamefilter_TextChanged(object sender, EventArgs e)
    {
        BindGV();
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDC_Report.aspx");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        DateTime now = DateTime.Today;
        string filename = "PDCREPORT_" + now.ToString("dd/MM/yyyy");
        Response.AddHeader("content-disposition", "attachment; filename = '" + filename + "'.xls");
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite =
        new HtmlTextWriter(stringWrite);
        GV_PDCReport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindOutstandingReportData();
    }

    protected void bindOutstandingReportData()
    {
        DataTable dt = GetData("SP_PDCReport", txtcnamefilter.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);

        if (dt.Rows.Count > 0)
        {
            btnexcel.Visible = true;
        }
        else
        {
            btnexcel.Visible = false;
        }
        GV_PDCReport.DataSource = dt;
        GV_PDCReport.DataBind();
        GV_PDCReport.EmptyDataText = "Record Not Found";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GV_PDCReport.ClientID + "',500, 1020 , 40 ,true); </script>", false);
    }

    private static DataTable GetData(string SP, string PartyName, string FromDate, string ToDate, string Type)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand(SP, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", Type);
                // if (PartyName == "")
                // cmd.Parameters.AddWithValue("@PartyName", DBNull.Value);
                // else
                cmd.Parameters.AddWithValue("@PartyName", PartyName);

                if (FromDate == "")
                    cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                if (ToDate == "")
                    cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        //sda.Fill(ds);
                        return dt;
                    }
                }
            }
        }
    }



    protected void GV_PDCReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string BillNo = GV_PDCReport.DataKeys[e.Row.RowIndex].Values[0].ToString();
                con.Open();

                Label linksname = (Label)e.Row.FindControl("linksname");
                Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
                Label lblPDCAmt = (Label)e.Row.FindControl("lblPDCAmt");

                SqlCommand cmdHeaderID = new SqlCommand("select id from tblReceiptHdr where Against='Invoice' and Amount='" + lblPDCAmt.Text + "' and Partyname='" + linksname.Text + "'", con);
                string HeaderID = cmdHeaderID.ExecuteScalar().ToString();

                SqlCommand cmdInvoiceNo = new SqlCommand("SELECT InvoiceNo = STUFF ((SELECT ','+InvoiceNo FROM tblReceiptDtls where HeaderID='" + HeaderID + "' FOR XML PATH('')),1,1,'')	FROM tblReceiptDtls SMS WITH (NOLOCK) where HeaderID='" + HeaderID + "'", con);
                string InvoiceNo = cmdInvoiceNo.ExecuteScalar().ToString();

                Label lblPDC_Against = (Label)e.Row.FindControl("lblPDC_Against");
                lblPDC_Against.Text = InvoiceNo;

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}