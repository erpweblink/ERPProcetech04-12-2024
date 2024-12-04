#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\ReportAccount.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CB12DEA0B1343CB6008E583200666A7B5D5E0D5E"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\ReportAccount.aspx.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class Admin_ReportAccount : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DivRoot1.Visible = false;
            DivRoot.Visible = false;
            btn.Visible = false;
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportAccount.aspx");
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
    {

        return AutoFillCustomerlist(prefixText);
    }

    public static List<string> AutoFillCustomerlist(string prefixText)
    {

        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {

                com.CommandText = "select DISTINCT cname from Company where " + "cname like @Search + '%'  ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> cname = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        cname.Add(sdr["cname"].ToString());
                    }
                }
                con.Close();
                return cname;
            }

        }
    }
    int count = 0;
    protected void ddltype_TextChanged(object sender, EventArgs e)
    {
        if (ddltype.Text == "Sales")
        {
            AutoCompleteExtender1.Enabled = true;
            txtPartyName.Text = string.Empty;
            GetCustomerList(txtPartyName.Text, count);
            //AutoCompleteExtender3.Enabled = false;

        }

        else if (ddltype.Text == "Purchase")
        {
            //AutoCompleteExtender3.Enabled = true;
            txtPartyName.Text = string.Empty;
            GetSupplierList(txtPartyName.Text, count);
            AutoCompleteExtender1.Enabled = false;

        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSupplierList(string prefixText, int count)
    {

        return AutoFillSupplierlist(prefixText);
    }

    public static List<string> AutoFillSupplierlist(string prefixText)
    {

        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {

                com.CommandText = "select DISTINCT SupplierName from tblSupplierMaster where " + "SupplierName like @Search + '%'  ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> SupplierName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        SupplierName.Add(sdr["SupplierName"].ToString());
                    }
                }
                con.Close();
                return SupplierName;
            }

        }
    }

    protected void btndatewise_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(ddltype.Text))
            {
                Daily();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.');window.location.href='ReportAccount.aspx';", true);

            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void Monthly()
    {
        using (SqlCommand Cmd = new SqlCommand("SP_Report", con))
        {
            using (SqlDataAdapter Da = new SqlDataAdapter())
            {

                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@BillingCustomer", txtPartyName.Text);
                Cmd.Parameters.AddWithValue("@Type", ddltype.Text);
                if (ddltype.Text == "Sales")
                {
                    DivRoot.Visible = true;
                    Cmd.Parameters.AddWithValue("@Action", "MonthlySales");
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);

                        GvMonthReport.DataSource = Dt;
                        GvMonthReport.EmptyDataText = "Record Not Found";
                        GvMonthReport.DataBind();
                    }
                }
                else
                {
                    DivRoot1.Visible = true;
                    Cmd.Parameters.AddWithValue("@Action", "MonthlyPurchase");
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);

                        GvPurchase.DataSource = Dt;
                        GvPurchase.EmptyDataText = "Record Not Found";
                        GvPurchase.DataBind();
                    }
                }



            }
        }
    }

    protected void Daily()
    {
        using (SqlCommand Cmd = new SqlCommand("SP_Report", con))
        {
            using (SqlDataAdapter Da = new SqlDataAdapter())
            {
                var date = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@BillingCustomer", txtPartyName.Text);
                Cmd.Parameters.AddWithValue("@Type", ddltype.Text);
                Cmd.Parameters.AddWithValue("@Date", date);

                if (ddltype.Text == "Sales")
                {
                    DivRoot.Visible = true;
                    Cmd.Parameters.AddWithValue("@Action", "DailySales");
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);


                        GvMonthReport.DataSource = Dt;
                        GvMonthReport.EmptyDataText = "Record Not Found";
                        GvMonthReport.DataBind();
                    }
                }
                else
                {
                    DivRoot1.Visible = true;
                    Cmd.Parameters.AddWithValue("@Action", "DailyPurchase");
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);

                        GvPurchase.DataSource = Dt;
                        GvPurchase.EmptyDataText = "Record Not Found";
                        GvPurchase.DataBind();
                    }
                }
            }
        }
    }

    protected void Yearly()
    {
        using (SqlCommand Cmd = new SqlCommand("SP_Report", con))
        {
            using (SqlDataAdapter Da = new SqlDataAdapter())
            {

                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@BillingCustomer", txtPartyName.Text);
                Cmd.Parameters.AddWithValue("@Type", ddltype.Text);

                if (ddltype.Text == "Sales")
                {
                    DivRoot.Visible = true;
                    Cmd.Parameters.AddWithValue("@Action", "YearlySales");
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);


                        GvMonthReport.DataSource = Dt;
                        GvMonthReport.EmptyDataText = "Record Not Found";
                        GvMonthReport.DataBind();
                    }
                }
                else
                {
                    DivRoot1.Visible = true;
                    Cmd.Parameters.AddWithValue("@Action", "YearlyPurchase");
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);

                        GvPurchase.DataSource = Dt;
                        GvPurchase.EmptyDataText = "Record Not Found";
                        GvPurchase.DataBind();
                    }
                }



            }
        }
    }

    protected void btnmonthwise_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(ddltype.Text))
            {
                Monthly();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.');window.location.href='ReportAccount.aspx';", true);

            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void GvMonthReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();

                Response.Write("<script>window.open ('TaxInvoicePDF.aspx?Id=" + encrypt(e.CommandArgument.ToString()) + "','_blank');</script>");


            }
        }
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    protected void GvPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();
                Response.Write("<script>window.open('PurchaseBillPDF.aspx','_blank');</script>");

            }
        }
    }

    protected void btnyearwise_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddltype.Text))
        {
            Yearly();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.');window.location.href='ReportAccount.aspx';", true);

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    private void Export_Excelsales()
    {
        //string Export = "";
        DataTable dtexcel = new DataTable();
        dtexcel.Columns.AddRange(new DataColumn[7]
        { new DataColumn("Id"),
        new DataColumn("InvoiceNo"),
               new DataColumn("BillingCustomer"),
                new DataColumn("Invoicedate"),
                 new DataColumn("InvoiceAgainst"),
                  new DataColumn("CustomerPONo"),
                   new DataColumn("GrandTotalFinal")

        });
        foreach (GridViewRow row in GvMonthReport.Rows)
        {
            string SN = (row.Cells[1].FindControl("lblsno") as Label).Text;
            string InvoiceNo = (row.Cells[1].FindControl("lblinvoiceNo") as Label).Text;
            string BillingCustomer = (row.Cells[1].FindControl("lblcustomername") as Label).Text;
            string Invoicedate = (row.Cells[1].FindControl("lblInvoicedate") as Label).Text;
            string InvoiceAgainst = (row.Cells[1].FindControl("lblInvoiceAgainst") as Label).Text;
            string CustomerPONo = (row.Cells[1].FindControl("lblCustomerPO") as Label).Text;
            //string AgainstNumber = (row.Cells[1].FindControl("lblAgainstNumber") as Label).Text;
            string GrandTotalFinal = (row.Cells[1].FindControl("lblGrandTotal") as Label).Text;
            dtexcel.Rows.Add(SN, InvoiceNo, BillingCustomer, Invoicedate, InvoiceAgainst, CustomerPONo, GrandTotalFinal);

        }
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            GvMonthReport.AllowPaging = false;
            GvMonthReport.DataSource = dtexcel;
            GvMonthReport.DataBind();
            GvMonthReport.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddltype.Text))
        {
            if (GvMonthReport.Rows.Count > 0 || GvPurchase.Rows.Count > 0)
            {
                if (ddltype.Text == "Sales")
                {
                    Export_Excelsales();
                }
                else
                {
                    Export_ExcelPurchase();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Search Daily,Monthly,Yearly Report');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.');window.location.href='ReportAccount.aspx';", true);

        }

    }

    protected void Export_ExcelPurchase()
    {
        //string Export = "";
        DataTable dtexcelpurchase = new DataTable();
        dtexcelpurchase.Columns.AddRange(new DataColumn[7]
        { new DataColumn("Id"),

               new DataColumn("SupplierBillNo"),
                new DataColumn("SupplierName"),
                 new DataColumn("BillDate"),
                  new DataColumn("GrandTotal"),
                   new DataColumn("PaymentDueDate"),
                   new DataColumn("CreatedBy")


        });
        foreach (GridViewRow row in GvPurchase.Rows)
        {
            string SN = (row.Cells[1].FindControl("lblsno") as Label).Text;
            string BillNo = (row.Cells[1].FindControl("lblSupplierBillNo") as Label).Text;
            string SupplierName = (row.Cells[1].FindControl("lblSuppliername") as Label).Text;
            string BillDate = (row.Cells[1].FindControl("lblBillDate") as Label).Text;
            string GrandTotal = (row.Cells[1].FindControl("lblGrandTotal") as Label).Text;
            //string AgainstNumber = (row.Cells[1].FindControl("lblAgainstNumber") as Label).Text;
            string PaymentDate = (row.Cells[1].FindControl("lblPaymentDueDate") as Label).Text;
            string CreatedBy = (row.Cells[1].FindControl("lblCreatedBy") as Label).Text;
            dtexcelpurchase.Rows.Add(SN, BillNo, SupplierName, BillDate, GrandTotal, PaymentDate, CreatedBy);

        }
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            GvPurchase.AllowPaging = false;
            GvPurchase.DataSource = dtexcelpurchase;
            GvPurchase.DataBind();
            GvPurchase.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void GridViewBind()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter sad = new SqlDataAdapter("select * from tblTaxInvoiceHdr", con);
        sad.Fill(dt);
        GvMonthReport.DataSource = dt;
        GvMonthReport.DataBind();

    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddltype.Text))
        {
            if (GvMonthReport.Rows.Count > 0 || GvPurchase.Rows.Count > 0)
            {
                if (ddltype.Text == "Sales")
                {
                    Export_PDFSales();
                }
                else
                {
                    Export_PDFPurchase();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Search Daily,Monthly,Yearly Report');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.');window.location.href='ReportAccount.aspx';", true);

        }

    }

    protected void Export_PDFPurchase()
    {
        DataTable dtexcelpurchase = new DataTable();
        dtexcelpurchase.Columns.AddRange(new DataColumn[7]
        { new DataColumn("Id"),
               new DataColumn("SupplierBillNo"),
                new DataColumn("SupplierName"),
                 new DataColumn("BillDate"),
                  new DataColumn("GrandTotal"),
                   new DataColumn("PaymentDueDate"),
                   new DataColumn("CreatedBy")


        });
        foreach (GridViewRow row in GvPurchase.Rows)
        {
            string SN = (row.Cells[1].FindControl("lblsno") as Label).Text;
            string BillNo = (row.Cells[1].FindControl("lblSupplierBillNo") as Label).Text;
            string SupplierName = (row.Cells[1].FindControl("lblSuppliername") as Label).Text;
            string BillDate = (row.Cells[1].FindControl("lblBillDate") as Label).Text;
            string GrandTotal = (row.Cells[1].FindControl("lblGrandTotal") as Label).Text;
            //string AgainstNumber = (row.Cells[1].FindControl("lblAgainstNumber") as Label).Text;
            string PaymentDate = (row.Cells[1].FindControl("lblPaymentDueDate") as Label).Text;
            string CreatedBy = (row.Cells[1].FindControl("lblCreatedBy") as Label).Text;
            dtexcelpurchase.Rows.Add(SN, BillNo, SupplierName, BillDate, GrandTotal, PaymentDate, CreatedBy);

        }
        System.IO.StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);

        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "Report.pdf", FileMode.Create));

        doc.Open();


        PdfContentByte cb = writer.DirectContent;
        cb.Rectangle(15f, 800f, 561f, 35f);
        cb.Stroke();
        // Header 
        cb.BeginText();
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 20);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tax Invoice Purchase Report", 200, 812, 0);
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 145, 755, 0);
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
        cb.EndText();
        if (dtexcelpurchase.Rows.Count > 0)
        {
            string BillNo = dtexcelpurchase.Rows[0]["SupplierBillNo"].ToString();
            string SupplierName = dtexcelpurchase.Rows[0]["SupplierName"].ToString();
            string Billdate = dtexcelpurchase.Rows[0]["BillDate"].ToString().TrimEnd("0:0".ToCharArray());
            string GrandTotal = dtexcelpurchase.Rows[0]["GrandTotal"].ToString();
            string PaymentDate = dtexcelpurchase.Rows[0]["PaymentDueDate"].ToString();
            string CreatedBy = dtexcelpurchase.Rows[0]["CreatedBy"].ToString();
            //string IGST = Dt.Rows[0]["IGST"].ToString();

            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingBefore = 120f;
            paragraphTable1.SpacingAfter = 10f;

            PdfPTable table = new PdfPTable(1);

            float[] widths2 = new float[] { 560f };
            table.SetWidths(widths2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;


            paragraphTable1.Add(table);
            doc.Add(paragraphTable1);

            Paragraph paragraphTable2 = new Paragraph();
            paragraphTable2.SpacingAfter = 0f;
            paragraphTable2.SpacingBefore = 120f;

            table = new PdfPTable(7);
            float[] widths3 = new float[] { 4f, 10f, 19f, 10f, 10f, 10f, 10f };
            table.SetWidths(widths3);
            if (GvPurchase.Rows.Count > 0)
            {
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Bill Number", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Supplier Name", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Bill Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Grand Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Payment Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Created By", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("IGST", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                int rowid = 1;
                foreach (DataRow dr in dtexcelpurchase.Rows)
                {
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 13)));
                    table.AddCell(new Phrase(dr["SupplierBillNo"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["SupplierName"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["BillDate"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["GrandTotal"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["PaymentDueDate"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["CreatedBy"].ToString(), FontFactory.GetFont("Arial", 10)));
                    //table.AddCell(new Phrase(dr["IGST"].ToString(), FontFactory.GetFont("Arial", 10)));
                    rowid++;
                }
            }
            paragraphTable2.Add(table);
            doc.Add(paragraphTable2);

            //Space
            Paragraph paragraphTable3 = new Paragraph();

            string[] items = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

            Font font12 = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font font10 = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Paragraph paragraph = new Paragraph("", font12);

            for (int i = 0; i < items.Length; i++)
            {
                paragraph.Add(new Phrase("", font10));
            }

            table = new PdfPTable(7);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 4f, 10f, 19f, 10f, 10f, 10f, 10f });
            table.AddCell(paragraph);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("  \n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont("Arial", 10, Font.BOLD)));

            doc.Add(table);


            Paragraph paragraphTable4 = new Paragraph();

            paragraphTable4.SpacingBefore = 10f;

            table = new PdfPTable(2);
            table.TotalWidth = 560f;

            float[] widths = new float[] { 160f, 400f };
            table.SetWidths(widths);
            table.LockedWidth = true;

            doc.Close();
            Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + "Report.pdf");
            string empFilename = "Report" + DateTime.Now.ToShortDateString() + ".pdf";

            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
                Response.AddHeader("Content-Disposition", "attachment;filename=" + empFilename);
            }
        }
        doc.Close();
    }

    protected void Export_PDFSales()
    {
        DataTable dtexcel = new DataTable();
        dtexcel.Columns.AddRange(new DataColumn[7]
        { new DataColumn("Id"),
         new DataColumn("InvoiceNo"),
               new DataColumn("BillingCustomer"),
                new DataColumn("Invoicedate"),
                 new DataColumn("InvoiceAgainst"),
                  new DataColumn("CustomerPONo"),
                   new DataColumn("GrandTotalFinal")

        });
        foreach (GridViewRow row in GvMonthReport.Rows)
        {
            string SN = (row.Cells[1].FindControl("lblsno") as Label).Text;
            string InvoiceNo = (row.Cells[1].FindControl("lblinvoiceNo") as Label).Text;
            string BillingCustomer = (row.Cells[1].FindControl("lblcustomername") as Label).Text;
            string Invoicedate = (row.Cells[1].FindControl("lblInvoicedate") as Label).Text;
            string InvoiceAgainst = (row.Cells[1].FindControl("lblInvoiceAgainst") as Label).Text;
            string CustomerPONo = (row.Cells[1].FindControl("lblCustomerPO") as Label).Text;
            //string AgainstNumber = (row.Cells[1].FindControl("lblAgainstNumber") as Label).Text;
            string GrandTotalFinal = (row.Cells[1].FindControl("lblGrandTotal") as Label).Text;
            dtexcel.Rows.Add(SN, InvoiceNo, BillingCustomer, Invoicedate, InvoiceAgainst, CustomerPONo, GrandTotalFinal);

        }
        System.IO.StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);

        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "Report.pdf", FileMode.Create));

        doc.Open();


        PdfContentByte cb = writer.DirectContent;
        cb.Rectangle(15f, 800f, 561f, 35f);
        cb.Stroke();
        // Header 
        cb.BeginText();
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 20);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tax Invoice Sales Report", 200, 812, 0);
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 145, 755, 0);
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
        cb.EndText();
        if (dtexcel.Rows.Count > 0)
        {
            string InvoiceNo = dtexcel.Rows[0]["InvoiceNo"].ToString();
            string CustomerName = dtexcel.Rows[0]["BillingCustomer"].ToString();
            string Invoicedate = dtexcel.Rows[0]["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray());
            string InvoiceAgainst = dtexcel.Rows[0]["InvoiceAgainst"].ToString();
            string CustomerPO = dtexcel.Rows[0]["CustomerPONo"].ToString();
            string GrandTotal = dtexcel.Rows[0]["GrandTotalFinal"].ToString();
            //string IGST = Dt.Rows[0]["IGST"].ToString();

            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingBefore = 120f;
            paragraphTable1.SpacingAfter = 10f;

            PdfPTable table = new PdfPTable(1);

            float[] widths2 = new float[] { 560f };
            table.SetWidths(widths2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;


            paragraphTable1.Add(table);
            doc.Add(paragraphTable1);

            Paragraph paragraphTable2 = new Paragraph();
            paragraphTable2.SpacingAfter = 0f;
            paragraphTable2.SpacingBefore = 120f;

            table = new PdfPTable(7);
            float[] widths3 = new float[] { 4f, 10f, 19f, 10f, 10f, 10f, 10f };
            table.SetWidths(widths3);
            if (GvMonthReport.Rows.Count > 0)
            {
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Invoice Number", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Customer Name", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Invoice Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Invoice Against", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Customer PO No.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Grand Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("IGST", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                int rowid = 1;
                foreach (DataRow dr in dtexcel.Rows)
                {
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 13)));
                    table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["BillingCustomer"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["Invoicedate"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["InvoiceAgainst"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["CustomerPONo"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["GrandTotalFinal"].ToString(), FontFactory.GetFont("Arial", 10)));
                    //table.AddCell(new Phrase(dr["IGST"].ToString(), FontFactory.GetFont("Arial", 10)));
                    rowid++;
                }
            }
            paragraphTable2.Add(table);
            doc.Add(paragraphTable2);

            //Space
            Paragraph paragraphTable3 = new Paragraph();

            string[] items = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

            Font font12 = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font font10 = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Paragraph paragraph = new Paragraph("", font12);

            for (int i = 0; i < items.Length; i++)
            {
                paragraph.Add(new Phrase("", font10));
            }

            table = new PdfPTable(7);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 4f, 10f, 19f, 10f, 10f, 10f, 10f });
            table.AddCell(paragraph);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("  \n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont("Arial", 10, Font.BOLD)));

            doc.Add(table);


            Paragraph paragraphTable4 = new Paragraph();

            paragraphTable4.SpacingBefore = 10f;

            table = new PdfPTable(2);
            table.TotalWidth = 560f;

            float[] widths = new float[] { 160f, 400f };
            table.SetWidths(widths);
            table.LockedWidth = true;

            doc.Close();
            Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + "Report.pdf");
            string empFilename = "Report" + DateTime.Now.ToShortDateString() + ".pdf";

            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
                Response.AddHeader("Content-Disposition", "attachment;filename=" + empFilename);
            }
        }
        doc.Close();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(ddltype.Text))
            {
                if (ddltype.Text == "Sales")
                {
                    DivRoot.Visible = true;
                    btn.Visible = true;
                    DataTable dtsearch = new DataTable();
                    SqlDataAdapter sadsearch;
                    if (ddltype.Text == "Sales" && string.IsNullOrEmpty(txtPartyName.Text) && string.IsNullOrEmpty(txtfromdate.Text) && string.IsNullOrEmpty(txttodate.Text))
                    {
                        sadsearch = new SqlDataAdapter("select * from tblTaxInvoiceHdr", con);
                        sadsearch.Fill(dtsearch);
                    }
                    else if (ddltype.Text == "Sales" && !string.IsNullOrEmpty(txtPartyName.Text) && !string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                    {
                        txttodate.Text = Convert.ToDateTime(txttodate.Text).ToString("yyyy-MM-dd");
                        txtfromdate.Text = Convert.ToDateTime(txtfromdate.Text).ToString("yyyy-MM-dd");
                        sadsearch = new SqlDataAdapter("select * from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "' AND CreatedOn between '" + txtfromdate.Text + "' AND '" + txttodate.Text + "'", con);
                        sadsearch.Fill(dtsearch);
                    }
                    else if (ddltype.Text == "Sales" && !string.IsNullOrEmpty(txtPartyName.Text))
                    {
                        sadsearch = new SqlDataAdapter("select * from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
                        sadsearch.Fill(dtsearch);
                    }
                    else if (ddltype.Text == "Sales" && !string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                    {
                        txttodate.Text = Convert.ToDateTime(txttodate.Text).ToString("yyyy-MM-dd");
                        txtfromdate.Text = Convert.ToDateTime(txtfromdate.Text).ToString("yyyy-MM-dd");
                        sadsearch = new SqlDataAdapter("select * from tblTaxInvoiceHdr where  CreatedOn between '" + txtfromdate.Text + "' AND '" + txttodate.Text + "'", con);
                        sadsearch.Fill(dtsearch);
                    }
                    GvMonthReport.DataSource = dtsearch;
                    GvMonthReport.DataBind();
                    GvMonthReport.EmptyDataText = "Record Not Found";
                }
                else
                {
                    DivRoot1.Visible = true;
                    btn.Visible = true;
                    DataTable dtsearch = new DataTable();
                    SqlDataAdapter sadsearch;
                    if (ddltype.Text == "Purchase" && string.IsNullOrEmpty(txtPartyName.Text) && string.IsNullOrEmpty(txtfromdate.Text) && string.IsNullOrEmpty(txttodate.Text))
                    {
                        sadsearch = new SqlDataAdapter("select * from tblPurchaseBillHdr", con);
                        sadsearch.Fill(dtsearch);
                    }
                    else if (ddltype.Text == "Purchase" && !string.IsNullOrEmpty(txtPartyName.Text) && !string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                    {
                        txttodate.Text = Convert.ToDateTime(txttodate.Text).ToString("yyyy-MM-dd");
                        txtfromdate.Text = Convert.ToDateTime(txtfromdate.Text).ToString("yyyy-MM-dd");
                        sadsearch = new SqlDataAdapter("select * from tblPurchaseBillHdr where SupplierName='" + txtPartyName.Text + "' AND CreatedOn between '" + txtfromdate.Text + "' AND '" + txttodate.Text + "'", con);
                        sadsearch.Fill(dtsearch);
                    }
                    else if (ddltype.Text == "Purchase" && !string.IsNullOrEmpty(txtPartyName.Text))
                    {
                        sadsearch = new SqlDataAdapter("select * from tblPurchaseBillHdr where SupplierName='" + txtPartyName.Text + "'", con);
                        sadsearch.Fill(dtsearch);
                    }
                    else if (ddltype.Text == "Purchase" && !string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                    {
                        txttodate.Text = Convert.ToDateTime(txttodate.Text).ToString("yyyy-MM-dd");
                        txtfromdate.Text = Convert.ToDateTime(txtfromdate.Text).ToString("yyyy-MM-dd");
                        sadsearch = new SqlDataAdapter("select * from tblPurchaseBillHdr where  CreatedOn between '" + txtfromdate.Text + "' AND '" + txttodate.Text + "'", con);
                        sadsearch.Fill(dtsearch);
                    }
                    GvPurchase.DataSource = dtsearch;
                    GvPurchase.DataBind();
                    GvPurchase.EmptyDataText = "Record Not Found";
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.');window.location.href='ReportAccount.aspx';", true);
            }

        }
        catch (Exception)
        {

            throw;
        }

    }
    //protected void Datewise()
    // {
    //     if (!string.IsNullOrEmpty(ddltype.Text))
    //     {
    //         //if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
    //         //{
    //             using (SqlCommand Cmd = new SqlCommand("SP_Report", con))
    //             {
    //                 using (SqlDataAdapter Da = new SqlDataAdapter())
    //                 {

    //                     Cmd.Connection = con;
    //                     Cmd.CommandType = CommandType.StoredProcedure;
    //                     Cmd.Parameters.AddWithValue("@BillingCustomer", txtPartyName.Text);
    //                     Cmd.Parameters.AddWithValue("@Type", ddltype.Text);
    //                 //Cmd.Parameters.AddWithValue("@Todate", txttodate.Text);
    //                 if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
    //                 {
    //                     Cmd.Parameters.Add(new SqlParameter()
    //                     {
    //                         ParameterName = "@Todate",
    //                         DbType = System.Data.DbType.DateTime,
    //                         SqlDbType = System.Data.SqlDbType.DateTime,
    //                         Value = DateTime.Parse(txttodate.Text)
    //                     });
    //                     Cmd.Parameters.Add(new SqlParameter()
    //                     {
    //                         ParameterName = "@Fromdate",
    //                         DbType = System.Data.DbType.DateTime,
    //                         SqlDbType = System.Data.SqlDbType.DateTime,
    //                         Value = DateTime.Parse(txtfromdate.Text)
    //                     });
    //                 }
    //                     //Cmd.Parameters.AddWithValue("@Fromdate", txtfromdate.Text);

    //                     if (ddltype.Text == "Sales")
    //                     {
    //                         DivRoot.Visible = true;
    //                         Cmd.Parameters.AddWithValue("@Action", "dateWiseSales");
    //                         Da.SelectCommand = Cmd;
    //                         using (DataTable Dt = new DataTable())
    //                         {
    //                             Da.Fill(Dt);


    //                             GvMonthReport.DataSource = Dt;
    //                             GvMonthReport.EmptyDataText = "Record Not Found";
    //                             GvMonthReport.DataBind();
    //                         }
    //                     }
    //                     else
    //                     {
    //                         DivRoot1.Visible = true;
    //                         Cmd.Parameters.AddWithValue("@Action", "dateWisePurchase");
    //                         Da.SelectCommand = Cmd;
    //                         using (DataTable Dt = new DataTable())
    //                         {
    //                             Da.Fill(Dt);

    //                             GvPurchase.DataSource = Dt;
    //                             GvPurchase.EmptyDataText = "Record Not Found";
    //                             GvPurchase.DataBind();
    //                         }
    //                     }



    //                 }
    //             }
    //         //}
    //         //else
    //         //{
    //         //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Date.');", true);
    //         //}
    //     }
    //     else
    //     {
    //         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.');window.location.href='ReportAccount.aspx';", true);
    //     }
    // }
}


#line default
#line hidden
