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
using ClosedXML.Excel;
using System.Globalization;
using Spire.Pdf;
using Spire.Pdf.Texts;
using iTextSharp.text.pdf.parser;
using iTextSharp.tool.xml.html.table;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Font = iTextSharp.text.Font;
using DocumentFormat.OpenXml;
using iTextSharp.tool.xml;
using Spire.Pdf.Conversion;
using Microsoft.Reporting.WebForms;

public partial class Admin_OutstandingReport : System.Web.UI.Page
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
                DivRoot1.Visible = true;
                btn.Visible = true;
                bindOutstandingData();
            }
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("OutstandingReport.aspx");
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

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    //required to avoid the runtime error "  
    //    //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    //}

    //protected void btnexcel_Click(object sender, EventArgs e)
    //{
    //}

    protected void bindOutstandingData()
    {
        dgvOutstanding.DataSource = null;//GetData("SP_OutstandingR");
        dgvOutstanding.DataBind();
    }

    private static DataTable GetData(string SP)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand(SP, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", "SALE");
                cmd.Parameters.AddWithValue("@PartyName", "Radix Electrosystems Pvt Ltd");
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    decimal Balance = 0;
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string invoiceno = e.Row.Cells[1].Text;
            string payble = e.Row.Cells[4].Text;
            string Recevd = e.Row.Cells[5].Text;
            Label lblbalance = (Label)e.Row.FindControl("lblbalance");
            Label lblCum_Balance = (Label)e.Row.FindControl("lblCum_Balance");
        }

    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        //Pdf("PDF");
        //Pdf();
        GetPDF("PDF");
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {


        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void ExportExcel(object sender, EventArgs e)
    {
        // Pdf("Excel");
        //Excel("Excel");
        //Pdf1("Excel");
        GetOutstandingReports();
    }

    bool Show = false;
    protected void Pdf(string flg)
    {

        DataTable Dt = new DataTable();
        SqlDataAdapter Da;
        if (txtPartyName.Text == "")
        {
            //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr", con);
            Da = new SqlDataAdapter("select distinct Top 10  (BillingCustomer) from [DB_ProcetechERP].tblTaxInvoiceHdr", con);
        }
        else
        {
            //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
            Da = new SqlDataAdapter("select distinct(BillingCustomer) from [DB_ProcetechERP].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
        }

        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "OutstandingReport.pdf", FileMode.Create));
        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
        doc.Open();
        System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
        string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";
        iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);
        //Resize image depend upon your need
        png.ScaleToFit(70, 100);
        //For Image Position
        png.SetAbsolutePosition(40, 790);
        //var document = new Document();
        //Give space before image
        //png.ScaleToFit(document.PageSize.Width - (document.RightMargin * 100), 50);
        png.SpacingBefore = 50f;
        //Give some space after the image
        png.SpacingAfter = 1f;
        png.Alignment = Element.ALIGN_LEFT;
        doc.Add(png);
        if (Dt.Rows.Count > 0)
        {
            DataTable Dttt = new DataTable();
            if (txtPartyName.Text == "")
            {
                string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
                SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [DB_ProcetechERP].tblTaxInvoiceHdr where BillingCustomer='" + BillingCustomer + "'", con);
                Daa.Fill(Dttt);

                //string ShippingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
                string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
                string Paid = Dttt.Rows[0]["ContactNo"].ToString();
            }
            else
            {
                SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [DB_ProcetechERP].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
                Daa.Fill(Dttt);

                string BillingCustomer = Dttt.Rows[0]["BillingCustomer"].ToString() == "" ? "" : Dttt.Rows[0]["BillingCustomer"].ToString();
                //string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
                string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
                string Paid = Dttt.Rows[0]["ContactNo"].ToString();
            }


            PdfContentByte cd = writer.DirectContent;
            cd.Rectangle(150f, 800f, 400f, 25f);

            cd.Stroke();

            // Header 
            cd.BeginText();
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "OUTSTANDING REPORT", 280, 808, 0);
            cd.EndText();

            PdfPTable table = new PdfPTable(4);

            foreach (DataRow row in Dt.Rows)
            {
                DataTable Dtt = new DataTable();
                string fdate;
                string tdate;
                string ft = txtfromdate.Text;
                string tt = txttodate.Text;
                if (ft == "")
                {
                    fdate = "";
                }
                else
                {
                    fdate = ft;
                }

                if (tt == "")
                {
                    tdate = "";
                }
                else
                {
                    tdate = tt;
                }

                Dtt = GetData("[DB_ProcetechERP].[SP_OutstandingR]", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

                if (Dtt.Rows.Count > 0)
                {
                    string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
                    string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

                    con.Open();
                    SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
                    string paymentterm = cmddueDt.ExecuteScalar().ToString();
                    con.Close();

                    int DueDays = 0;

                    if (paymentterm == "30 Days credit")
                    {
                        DueDays = 30;
                    }
                    else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
                    {
                        DueDays = 60;
                    }
                    else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
                    {
                        DueDays = 45;
                    }
                    else if (paymentterm == "90 Days credit")
                    {
                        DueDays = 90;
                    }
                    else if (paymentterm == "75 Days credit")
                    {
                        DueDays = 75;
                    }
                    else
                    {
                        DueDays = 0;
                    }

                    PdfPCell Cell = new PdfPCell();

                    Paragraph paragraphTable1 = new Paragraph();
                    paragraphTable1.SpacingAfter = 10f;
                    paragraphTable1.SpacingBefore = 14f;

                    table = new PdfPTable(1);
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    float[] widths1 = new float[] { 100 };
                    table.SetWidths(widths1);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;

                    Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
                    Cell.HorizontalAlignment = 1;
                    Cell.Border = PdfPCell.NO_BORDER;

                    table.AddCell(Cell);

                    Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
                    Cell.HorizontalAlignment = 1;
                    Cell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(Cell);

                    paragraphTable1.Add(table);
                    doc.Add(paragraphTable1);

                    //Advance Table
                    Paragraph paragraphTable9 = new Paragraph();
                    paragraphTable9.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
                    table.SetWidths(widths33349);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
                    paragraphTable9.Add(table);
                    doc.Add(paragraphTable9);
                    ///////////////////


                    Paragraph paragraphTable2 = new Paragraph();
                    paragraphTable2.SpacingAfter = 0f;
                    table = new PdfPTable(9);
                    float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths33);
                    decimal SumOfTotal = 0;
                    decimal SumOfPayableTotal = 0;
                    decimal SumOfbal = 0;
                    if (Dtt.Rows.Count > 0)
                    {
                        table.TotalWidth = 560f;
                        table.LockedWidth = true;
                        //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                        table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

                        int rowid = 1;
                        decimal Balance = 0;

                        foreach (DataRow dr in Dtt.Rows)
                        {
                            //var Recevd = dr["Received"].ToString();
                            var payble = dr["Payable"].ToString();
                            con.Open();
                            //SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from  [ExcelEncLive].tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
                            SqlCommand cmdpaid = new SqlCommand("SELECT Max(CAST(Paid as float)) FROM [DB_ProcetechERP]. tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
                            string paidval = cmdpaid.ExecuteScalar().ToString();

                            SqlCommand cmdRecived = new SqlCommand("SELECT ISNULL(SUM(CAST(Grandtotal as float)), 0) as Recived FROM [DB_ProcetechERP].tblCreditDebitNoteHdr WHERE BillNumber = '" + dr["InvoiceNo"].ToString() + "' AND NoteType = 'Credit_Sale'", con);


                            //SqlCommand cmdRecived = new SqlCommand("select (CAST(Grandtotal as float)) as Recived  from  [ExcelEncLive].tblCreditDebitNoteHdr where  BillNumber='" + dr["InvoiceNo"].ToString() + "'", con);
                            string Recived = cmdRecived.ExecuteScalar().ToString();
                            decimal Recivedamt;
                            if (Recived == "")
                            {
                                Recivedamt = 0;

                            }

                            else
                            {
                                Recivedamt = Convert.ToDecimal(Recived);
                            }
                            con.Close();
                            //var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval) +  Recivedamt;
                            var bal = Convert.ToDecimal(payble) - (Convert.ToDecimal(paidval == "" ? "0" : paidval) + Recivedamt);
                            Balance += bal;



                            //Bind DueDate from Payment Term
                            string str1 = "";
                            if (dr["Invoicedate"].ToString() != "")
                            {
                                var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
                                DateTime DueDate = today.AddDays(DueDays);
                                str1 = DueDate.ToString();
                                str1 = str1.Replace("12:00:00 AM", "");
                                var time1 = str1;
                                DateTime Invoice = Convert.ToDateTime(str1);
                                str1 = Invoice.ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                str1 = "";
                            }

                            table.TotalWidth = 560f;
                            table.LockedWidth = true;
                            //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                            table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase((Convert.ToDecimal(paidval == "" ? "0" : paidval) + Recivedamt).ToString(), FontFactory.GetFont("Arial", 9)));
                            //table.AddCell(new Phrase(paidval == "" ? "0" : paidval, FontFactory.GetFont("Arial", 9)));
                            //table.AddCell(new Phrase(paidval =  paidval, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(bal).ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
                            rowid++;
                            SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
                            SumOfPayableTotal += Convert.ToDecimal(paidval == "" ? "0" : paidval);
                            SumOfbal += bal;
                        }
                    }
                    Dtt.Rows.Clear();
                    paragraphTable2.Add(table);
                    doc.Add(paragraphTable2);

                    con.Open();
                    SqlCommand cmdAdv = new SqlCommand("select SUM(CAST(Amount as float)) from  [DB_ProcetechERP].tblReceiptHdr where Against='Advance' and Partyname='" + PartyName + "'", con);
                    string Advre = cmdAdv.ExecuteScalar().ToString();
                    con.Close();

                    Paragraph paragraphTable3 = new Paragraph();
                    paragraphTable2.SpacingAfter = 10f;
                    table = new PdfPTable(9);
                    float[] widths333 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths333);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfPayableTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfbal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable3.Add(table);
                    doc.Add(paragraphTable3);

                    //Advance Table
                    Paragraph paragraphTable33 = new Paragraph();
                    paragraphTable33.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths3334 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths3334);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Advance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Convert.ToDouble(Advre == "" ? "0" : Advre).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable33.Add(table);
                    doc.Add(paragraphTable33);
                    ///////////////////

                    var fBalnace = Convert.ToDecimal(SumOfbal) - Convert.ToDecimal(Advre == "" ? "0" : Advre);

                    //Total Balance table 
                    Paragraph paragraphTable334 = new Paragraph();
                    paragraphTable334.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths33344 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths33344);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(fBalnace).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable334.Add(table);
                    doc.Add(paragraphTable334);
                    /////////////////////

                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    doc.Add(p);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
                    Show = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
                }
            }
            //ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
            //doc.Close();
        }
        if (Show == false)
        {
            if (flg == "Excel")
            {
                doc.Close();
                string pathd = Server.MapPath("~") + "/files/OutstandingReport.pdf";
                string pathsave = Server.MapPath("~") + "/files/OutstandingReport.xlsx";

                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                //Load the PDF file
                pdf.LoadFromFile(pathd);
                //Save to Excel
                pdf.SaveToFile(pathsave, FileFormat.XLSX);

                System.IO.FileInfo file = new System.IO.FileInfo(pathsave);
                string Outgoingfile = txtPartyName.Text + " OutstandingReport.xlsx";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.WriteFile(file.FullName);
            }
            else
            {
                ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
                doc.Close();
            }
        }
    }

    protected void Pdf()
    {
        DataTable Dt = new DataTable();
        SqlDataAdapter Da;
        if (txtPartyName.Text == "")
        {
            //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr", con);
            Da = new SqlDataAdapter("select distinct(BillingCustomer) from tblTaxInvoiceHdr", con);
        }
        else
        {
            //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
            Da = new SqlDataAdapter("select distinct(BillingCustomer) from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
        }

        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "OutstandingReport.pdf", FileMode.Create));
        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
        doc.Open();
        System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
        string imageURL = Server.MapPath("~") + "/img/ProcetechPDFLOGO.jpg"; 
        iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);
        //Resize image depend upon your need
        png.ScaleToFit(70, 100);
        //For Image Position
        png.SetAbsolutePosition(40, 790);
        //var document = new Document();
        //Give space before image
        //png.ScaleToFit(document.PageSize.Width - (document.RightMargin * 100), 50);
        png.SpacingBefore = 50f;
        //Give some space after the image
        png.SpacingAfter = 1f;
        png.Alignment = Element.ALIGN_LEFT;
        doc.Add(png);
        if (Dt.Rows.Count > 0)
        {
            DataTable Dttt = new DataTable();
            if (txtPartyName.Text == "")
            {
                string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
                SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + BillingCustomer + "'", con);
                Daa.Fill(Dttt);

                //string ShippingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
                string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
                string Paid = Dttt.Rows[0]["ContactNo"].ToString();
            }
            else
            {
                SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
                Daa.Fill(Dttt);

                string BillingCustomer = Dttt.Rows[0]["BillingCustomer"].ToString() == "" ? "" : Dttt.Rows[0]["BillingCustomer"].ToString();
                //string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
                string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
                string Paid = Dttt.Rows[0]["ContactNo"].ToString();
            }


            PdfContentByte cd = writer.DirectContent;
            cd.Rectangle(150f, 800f, 400f, 25f);
            cd.Stroke();

            // Header 
            cd.BeginText();
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "OUTSTANDING REPORT", 280, 808, 0);
            cd.EndText();

            PdfPTable table = new PdfPTable(4);

            foreach (DataRow row in Dt.Rows)
            {
                DataTable Dtt = new DataTable();
                string fdate;
                string tdate;
                string ft = txtfromdate.Text;
                string tt = txttodate.Text;
                if (ft == "")
                {
                    fdate = "";
                }
                else
                {
                    fdate = ft;
                }

                if (tt == "")
                {
                    tdate = "";
                }
                else
                {
                    tdate = tt;
                }

                Dtt = GetData("SP_OutstandingR", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

                if (Dtt.Rows.Count > 0)
                {
                    string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
                    string ShippingAdd = Dtt.Rows[0]["ShippingAddress"].ToString();

                    con.Open();
                    SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
                    string paymentterm = cmddueDt.ExecuteScalar().ToString();
                    con.Close();

                    int DueDays = 0;

                    if (paymentterm == "30 Days credit")
                    {
                        DueDays = 30;
                    }
                    else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
                    {
                        DueDays = 60;
                    }
                    else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
                    {
                        DueDays = 45;
                    }
                    else if (paymentterm == "90 Days credit")
                    {
                        DueDays = 90;
                    }
                    else if (paymentterm == "75 Days credit")
                    {
                        DueDays = 75;
                    }
                    else
                    {
                        DueDays = 0;
                    }

                    PdfPCell Cell = new PdfPCell();

                    Paragraph paragraphTable1 = new Paragraph();
                    paragraphTable1.SpacingAfter = 10f;
                    paragraphTable1.SpacingBefore = 14f;

                    table = new PdfPTable(1);
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    float[] widths1 = new float[] { 100 };
                    table.SetWidths(widths1);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;

                    Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
                    Cell.HorizontalAlignment = 1;
                    Cell.Border = PdfPCell.NO_BORDER;

                    table.AddCell(Cell);

                    Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
                    Cell.HorizontalAlignment = 1;
                    Cell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(Cell);

                    paragraphTable1.Add(table);
                    doc.Add(paragraphTable1);

                    //Advance Table
                    Paragraph paragraphTable9 = new Paragraph();
                    paragraphTable9.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
                    table.SetWidths(widths33349);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
                    paragraphTable9.Add(table);
                    doc.Add(paragraphTable9);
                    ///////////////////


                    Paragraph paragraphTable2 = new Paragraph();
                    paragraphTable2.SpacingAfter = 0f;
                    table = new PdfPTable(9);
                    float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths33);
                    decimal SumOfTotal = 0;
                    decimal SumOfPayableTotal = 0;
                    decimal SumOfbal = 0;
                    if (Dtt.Rows.Count > 0)
                    {
                        table.TotalWidth = 560f;
                        table.LockedWidth = true;
                        //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                        table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

                        int rowid = 1;
                        decimal Balance = 0;
                        foreach (DataRow dr in Dtt.Rows)
                        {
                            //var Recevd = dr["Received"].ToString();
                            var payble = dr["Payable"].ToString();
                            con.Open();
                            SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
                            string paidval = cmdpaid.ExecuteScalar().ToString();
                            con.Close();
                            var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval == "" ? "0" : paidval);
                            Balance += bal;

                            //Bind DueDate from Payment Term
                            string str1 = "";
                            if (dr["Invoicedate"].ToString() != "")
                            {
                                var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
                                DateTime DueDate = today.AddDays(DueDays);
                                str1 = DueDate.ToString();
                                str1 = str1.Replace("12:00:00 AM", "");
                                var time1 = str1;
                                DateTime Invoice = Convert.ToDateTime(str1);
                                str1 = Invoice.ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                str1 = "";
                            }

                            table.TotalWidth = 560f;
                            table.LockedWidth = true;
                            //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                            table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(paidval == "" ? "0" : paidval, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(bal).ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
                            rowid++;
                            SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
                            SumOfPayableTotal += Convert.ToDecimal(paidval == "" ? "0" : paidval);
                            SumOfbal += bal;
                        }
                    }
                    Dtt.Rows.Clear();
                    paragraphTable2.Add(table);
                    doc.Add(paragraphTable2);

                    con.Open();
                    SqlCommand cmdAdv = new SqlCommand("select SUM(CAST(Amount as float)) from tblReceiptHdr where Against='Advance' and Partyname='" + PartyName + "'", con);
                    string Advre = cmdAdv.ExecuteScalar().ToString();
                    con.Close();

                    Paragraph paragraphTable3 = new Paragraph();
                    paragraphTable2.SpacingAfter = 10f;
                    table = new PdfPTable(9);
                    float[] widths333 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths333);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfPayableTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfbal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable3.Add(table);
                    doc.Add(paragraphTable3);

                    //Advance Table
                    Paragraph paragraphTable33 = new Paragraph();
                    paragraphTable33.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths3334 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths3334);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Advance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Convert.ToDouble(Advre == "" ? "0" : Advre).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable33.Add(table);
                    doc.Add(paragraphTable33);
                    ///////////////////

                    var fBalnace = Convert.ToDecimal(SumOfbal) - Convert.ToDecimal(Advre == "" ? "0" : Advre);

                    //Total Balance table 
                    Paragraph paragraphTable334 = new Paragraph();
                    paragraphTable334.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths33344 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths33344);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(fBalnace).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable334.Add(table);
                    doc.Add(paragraphTable334);
                    /////////////////////

                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    doc.Add(p);
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
                }

            }
            ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
            doc.Close();
        }



        //string pathd= Server.MapPath("~") + "/files/OutstandingReport.pdf";
        //string pathsave= Server.MapPath("~") + "/files/OutstandingReport.xlsx";

        //Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
        ////Load the PDF file
        //pdf.LoadFromFile(pathd);
        ////Save to Excel
        //pdf.SaveToFile(pathsave, FileFormat.XLSX);
    }

    private static DataTable GetData(string SP, string PartyName, string FromDate, string ToDate, string Type)
    {
        //string strConnString = ConfigurationManager.ConnectionStrings["New_connectionString"].ConnectionString;
        //using (SqlConnection con = new SqlConnection(strConnString))
        //{
        //    using (SqlCommand cmd = new SqlCommand(SP, con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Type", Type);
        //        cmd.Parameters.AddWithValue("@PartyName", PartyName);
        //        if (FromDate == "")
        //            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
        //        else
        //            cmd.Parameters.AddWithValue("@FromDate", FromDate);
        //        if (ToDate == "")
        //            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
        //        else
        //            cmd.Parameters.AddWithValue("@ToDate", ToDate);
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            using (DataSet ds = new DataSet())
        //            {
        //                DataTable dt = new DataTable();
        //                sda.Fill(dt);
        //                return dt;
        //            }
        //        }
        //    }
        //}

        string strConnString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand(SP, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Explicitly specify parameter types
                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
                cmd.Parameters.Add("@PartyName", SqlDbType.NVarChar).Value = PartyName;

                // Repeat for other parameters...

                if (FromDate == "")
                    cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    //cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = DateTime.Parse(FromDate);
                    cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;

                if (ToDate == "")
                    cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    //cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = DateTime.Parse(ToDate);
                    cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

    }

    ////protected void GeneratePdfAndExcel(string flg)
    ////{
    ////    PdfPTable table;
    ////    DataTable Dt = new DataTable();
    ////    SqlDataAdapter Da;
    ////    System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
    ////    string connectionString = (ConfigurationManager.ConnectionStrings["New_connectionString"].ConnectionString);

    ////    using (SqlConnection con = new SqlConnection(connectionString))
    ////    {
    ////        if (txtPartyName.Text == "")
    ////        {
    ////            Da = new SqlDataAdapter("select distinct Top 10 (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr", con);
    ////        }
    ////        else
    ////        {
    ////            Da = new SqlDataAdapter("select distinct(BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    ////        }
    ////        Da.Fill(Dt);
    ////    }

    ////    Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
    ////    string pdfFilePath = Server.MapPath("~/files/OutstandingReport.pdf");
    ////    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfFilePath, FileMode.Create));
    ////    doc.Open();

    ////    // Add your PDF content here

    ////    // Create a new DataTable to store data for Excel
    ////    DataTable allData = new DataTable();
    ////    allData.Columns.Add("Type", typeof(string));
    ////    allData.Columns.Add("Doc No", typeof(string));
    ////    allData.Columns.Add("Date", typeof(string));
    ////    allData.Columns.Add("Due", typeof(string));
    ////    allData.Columns.Add("Payable", typeof(string));
    ////    allData.Columns.Add("Received", typeof(string));
    ////    allData.Columns.Add("Balance", typeof(string));
    ////    allData.Columns.Add("Cum. Bal", typeof(string));
    ////    allData.Columns.Add("Days", typeof(string));

    ////    if (Dt.Rows.Count > 0)
    ////    {
    ////        foreach (DataRow row in Dt.Rows)
    ////        {
    ////            DataTable Dtt = new DataTable();
    ////            string fdate;
    ////            string tdate;
    ////            string ft = txtfromdate.Text;
    ////            string tt = txttodate.Text;
    ////            if (ft == "")
    ////            {
    ////                fdate = "";
    ////            }
    ////            else
    ////            {
    ////                fdate = ft;
    ////            }

    ////            if (tt == "")
    ////            {
    ////                tdate = "";
    ////            }
    ////            else
    ////            {
    ////                tdate = tt;
    ////            }

    ////            Dtt = GetData("[ExcelEncLive].SP_OutstandingR", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

    ////            if (Dtt.Rows.Count > 0)
    ////            {
    ////                string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
    ////                string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

    ////                con.Open();
    ////                SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
    ////                string paymentterm = cmddueDt.ExecuteScalar().ToString();
    ////                con.Close();

    ////                int DueDays = 0;

    ////                if (paymentterm == "30 Days credit")
    ////                {
    ////                    DueDays = 30;
    ////                }
    ////                else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
    ////                {
    ////                    DueDays = 60;
    ////                }
    ////                else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
    ////                {
    ////                    DueDays = 45;
    ////                }
    ////                else if (paymentterm == "90 Days credit")
    ////                {
    ////                    DueDays = 90;
    ////                }
    ////                else if (paymentterm == "75 Days credit")
    ////                {
    ////                    DueDays = 75;
    ////                }
    ////                else
    ////                {
    ////                    DueDays = 0;
    ////                }

    ////                PdfPCell Cell = new PdfPCell();

    ////                Paragraph paragraphTable1 = new Paragraph();
    ////                paragraphTable1.SpacingAfter = 10f;
    ////                paragraphTable1.SpacingBefore = 14f;

    ////                table = new PdfPTable(1);
    ////                table.DefaultCell.Border = Rectangle.NO_BORDER;
    ////                float[] widths1 = new float[] { 100 };
    ////                table.SetWidths(widths1);
    ////                table.TotalWidth = 560f;
    ////                table.LockedWidth = true;

    ////                Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
    ////                Cell.HorizontalAlignment = 1;
    ////                Cell.Border = PdfPCell.NO_BORDER;

    ////                table.AddCell(Cell);

    ////                Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
    ////                Cell.HorizontalAlignment = 1;
    ////                Cell.Border = PdfPCell.NO_BORDER;
    ////                table.AddCell(Cell);

    ////                paragraphTable1.Add(table);
    ////                doc.Add(paragraphTable1);

    ////                // Advance Table
    ////                Paragraph paragraphTable9 = new Paragraph();
    ////                paragraphTable9.SpacingAfter = 2f;
    ////                table = new PdfPTable(9);
    ////                float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
    ////                table.SetWidths(widths33349);
    ////                table.TotalWidth = 560f;
    ////                table.LockedWidth = true;
    ////                table.DefaultCell.Border = Rectangle.NO_BORDER;
    ////                table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
    ////                paragraphTable9.Add(table);
    ////                doc.Add(paragraphTable9);

    ////                Paragraph paragraphTable2 = new Paragraph();
    ////                paragraphTable2.SpacingAfter = 0f;
    ////                table = new PdfPTable(9);
    ////                float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    ////                table.SetWidths(widths33);
    ////                decimal SumOfTotal = 0;
    ////                decimal SumOfPayableTotal = 0;
    ////                decimal SumOfbal = 0;

    ////                if (Dtt.Rows.Count > 0)
    ////                {
    ////                    table.TotalWidth = 560f;
    ////                    table.LockedWidth = true;
    ////                    table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                    table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                    table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                    table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                    table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                    table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                    table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                    table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                    table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

    ////                    int rowid = 1;
    ////                    decimal Balance = 0;
    ////                    foreach (DataRow dr in Dtt.Rows)
    ////                    {
    ////                        var payble = dr["Payable"].ToString();
    ////                        con.Open();
    ////                        SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from [ExcelEncLive].tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
    ////                        string paidval = cmdpaid.ExecuteScalar().ToString();
    ////                        con.Close();
    ////                        var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval == "" ? "0" : paidval);
    ////                        Balance += bal;

    ////                        string str1 = "";
    ////                        if (dr["Invoicedate"].ToString() != "")
    ////                        {
    ////                            var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
    ////                            DateTime DueDate = today.AddDays(DueDays);
    ////                            str1 = DueDate.ToString();
    ////                            str1 = str1.Replace("12:00:00 AM", "");
    ////                            var time1 = str1;
    ////                            DateTime Invoice = Convert.ToDateTime(str1);
    ////                            str1 = Invoice.ToString("dd-MM-yyyy");
    ////                        }
    ////                        else
    ////                        {
    ////                            str1 = "";
    ////                        }

    ////                        table.TotalWidth = 560f;
    ////                        table.LockedWidth = true;
    ////                        table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
    ////                        table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 9)));
    ////                        table.AddCell(new Phrase(dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
    ////                        table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
    ////                        table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
    ////                        table.AddCell(new Phrase(paidval == "" ? "0" : paidval, FontFactory.GetFont("Arial", 9)));
    ////                        table.AddCell(new Phrase(Math.Round(bal).ToString(), FontFactory.GetFont("Arial", 9)));
    ////                        table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
    ////                        table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
    ////                        rowid++;
    ////                        SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
    ////                        SumOfPayableTotal += Convert.ToDecimal(paidval == "" ? "0" : paidval);
    ////                        SumOfbal += bal;
    ////                    }
    ////                }

    ////                Dtt.Rows.Clear();
    ////                paragraphTable2.Add(table);
    ////                doc.Add(paragraphTable2);

    ////                con.Open();
    ////                SqlCommand cmdAdv = new SqlCommand("select SUM(CAST(Amount as float)) from [ExcelEncLive].tblReceiptHdr where Against='Advance' and Partyname='" + PartyName + "'", con);
    ////                string Advre = cmdAdv.ExecuteScalar().ToString();
    ////                con.Close();

    ////                Paragraph paragraphTable3 = new Paragraph();
    ////                paragraphTable2.SpacingAfter = 10f;
    ////                table = new PdfPTable(9);
    ////                float[] widths333 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    ////                table.SetWidths(widths333);
    ////                table.TotalWidth = 560f;
    ////                table.LockedWidth = true;
    ////                table.DefaultCell.Border = Rectangle.NO_BORDER;
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase("Advance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase(Convert.ToDouble(Advre == "" ? "0" : Advre).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    ////                paragraphTable3.Add(table);
    ////                doc.Add(paragraphTable3);

    ////                var fBalnace = Convert.ToDecimal(SumOfbal) - Convert.ToDecimal(Advre == "" ? "0" : Advre);

    ////                // Total Balance table 
    ////                Paragraph paragraphTable334 = new Paragraph();
    ////                paragraphTable334.SpacingAfter = 2f;
    ////                table = new PdfPTable(9);
    ////                float[] widths33344 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    ////                table.SetWidths(widths33344);
    ////                table.TotalWidth = 560f;
    ////                table.LockedWidth = true;
    ////                table.DefaultCell.Border = Rectangle.NO_BORDER;
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase(Math.Round(fBalnace).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    ////                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    ////                paragraphTable334.Add(table);
    ////                doc.Add(paragraphTable334);

    ////                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
    ////                doc.Add(p);
    ////            }
    ////            else
    ////            {
    ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
    ////                Show = false;
    ////            }
    ////        }
    ////    }

    ////    using (MemoryStream excelStream = new MemoryStream())
    ////    {
    ////        // Create a new Excel document
    ////        using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(excelStream, SpreadsheetDocumentType.Workbook))
    ////        {
    ////            // Add a WorkbookPart to the document
    ////            WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
    ////            workbookPart.Workbook = new Workbook();

    ////            // Add a WorksheetPart to the WorkbookPart
    ////            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
    ////            worksheetPart.Worksheet = new Worksheet(new SheetData());

    ////            // Add a new sheet to the workbook
    ////            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
    ////            Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
    ////            sheets.Append(sheet);

    ////            // Get the sheet data
    ////            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

    ////            // Create header row
    ////            Row headerRow = new Row();
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Type"), DataType = CellValues.String });
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Doc No"), DataType = CellValues.String });
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Date"), DataType = CellValues.String });
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Due"), DataType = CellValues.String });
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Payable"), DataType = CellValues.String });
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Received"), DataType = CellValues.String });
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Balance"), DataType = CellValues.String });
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Cum. Bal"), DataType = CellValues.String });
    ////            headerRow.Append(new Cell() { CellValue = new CellValue("Days"), DataType = CellValues.String });
    ////            sheetData.AppendChild(headerRow);

    ////            // Add data rows
    ////            foreach (DataRow dataRow in allData.Rows)
    ////            {
    ////                Row newRow = new Row();
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Type"].ToString()), DataType = CellValues.String });
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Doc No"].ToString()), DataType = CellValues.String });
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Date"].ToString()), DataType = CellValues.String });
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Due"].ToString()), DataType = CellValues.String });
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Payable"].ToString()), DataType = CellValues.String });
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Received"].ToString()), DataType = CellValues.String });
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Balance"].ToString()), DataType = CellValues.String });
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Cum. Bal"].ToString()), DataType = CellValues.String });
    ////                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Days"].ToString()), DataType = CellValues.String });
    ////                sheetData.AppendChild(newRow);
    ////            }
    ////        }

    ////        // Save the Excel data to a file or perform other actions as needed
    ////        string excelFilePath = Server.MapPath("~/files/OutstandingReport.xlx");
    ////        File.WriteAllBytes(excelFilePath, excelStream.ToArray());

    ////        // Now you have the Excel file in the excelFilePath.
    ////    }
    ////}
    //protected void GeneratePdfAndExcel(string flg)
    //{
    //    DataTable Dt = new DataTable();
    //    SqlDataAdapter Da;
    //    System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
    //    string connectionString = ConfigurationManager.ConnectionStrings["New_connectionString"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        Da = new SqlDataAdapter(txtPartyName.Text == ""
    //            ? "select distinct TOP 10 (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr"
    //            : "select distinct (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);

    //        Da.Fill(Dt);
    //    }

    //    if (Dt.Rows.Count > 0)
    //    {
    //        string pdfFilePath = Server.MapPath("~/files/OutstandingReport.pdf");

    //        using (Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f))
    //        {
    //            using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create))
    //            {
    //                using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
    //                {
    //                    doc.Open();

    //                    DataTable allData = new DataTable();
    //                    allData.Columns.Add("Type", typeof(string));
    //                    allData.Columns.Add("Doc No", typeof(string));
    //                    allData.Columns.Add("Date", typeof(string));
    //                    allData.Columns.Add("Due", typeof(string));
    //                    allData.Columns.Add("Payable", typeof(decimal));
    //                    allData.Columns.Add("Received", typeof(string));
    //                    allData.Columns.Add("Balance", typeof(decimal));
    //                    allData.Columns.Add("Cum. Bal", typeof(string));
    //                    allData.Columns.Add("Days", typeof(string));

    //                    foreach (DataRow row in Dt.Rows)
    //                    {
    //                        string fdate = string.IsNullOrEmpty(txtfromdate.Text) ? "" : txtfromdate.Text;
    //                        string tdate = string.IsNullOrEmpty(txttodate.Text) ? "" : txttodate.Text;

    //                        DataTable Dtt = GetData("[ExcelEncLive].SP_OutstandingR", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

    //                        if (Dtt.Rows.Count > 0)
    //                        {
    //                            string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
    //                            string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

    //                            con.Open();
    //                            SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
    //                            string paymentterm = cmddueDt.ExecuteScalar().ToString();
    //                            con.Close();

    //                            int DueDays = 0;

    //                            if (paymentterm == "30 Days credit")
    //                            {
    //                                DueDays = 30;
    //                            }
    //                            else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
    //                            {
    //                                DueDays = 60;
    //                            }
    //                            else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
    //                            {
    //                                DueDays = 45;
    //                            }
    //                            else if (paymentterm == "90 Days credit")
    //                            {
    //                                DueDays = 90;
    //                            }
    //                            else if (paymentterm == "75 Days credit")
    //                            {
    //                                DueDays = 75;
    //                            }
    //                            else
    //                            {
    //                                DueDays = 0;
    //                            }

    //                            PdfPCell Cell;

    //                            Paragraph paragraphTable1 = new Paragraph { SpacingAfter = 10f, SpacingBefore = 14f };

    //                            PdfPTable table = new PdfPTable(1)
    //                            {
    //                                DefaultCell = { Border = Rectangle.NO_BORDER },
    //                                TotalWidth = 560f,
    //                                LockedWidth = true
    //                            };

    //                            Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)))
    //                            {
    //                                HorizontalAlignment = 1,
    //                                Border = PdfPCell.NO_BORDER
    //                            };

    //                            table.AddCell(Cell);

    //                            Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)))
    //                            {
    //                                HorizontalAlignment = 1,
    //                                Border = PdfPCell.NO_BORDER
    //                            };

    //                            table.AddCell(Cell);

    //                            paragraphTable1.Add(table);
    //                            doc.Add(paragraphTable1);

    //                            Paragraph paragraphTable9 = new Paragraph { SpacingAfter = 2f };
    //                            table = new PdfPTable(9)
    //                            {
    //                                TotalWidth = 560f,
    //                                LockedWidth = true,
    //                                DefaultCell = { Border = Rectangle.NO_BORDER }
    //                            };

    //                            table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
    //                            paragraphTable9.Add(table);
    //                            doc.Add(paragraphTable9);

    //                            Paragraph paragraphTable2 = new Paragraph { SpacingAfter = 0f };
    //                            table = new PdfPTable(9)
    //                            {
    //                                TotalWidth = 560f,
    //                                LockedWidth = true
    //                            };

    //                            table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

    //                            decimal balanceTotal = 0;

    //                            foreach (DataRow dr in Dtt.Rows)
    //                            {
    //                                decimal Payable = Convert.ToDecimal(dr["Payable"].ToString());
    //                                decimal paidVal;

    //                                using (SqlCommand cmdPaid = new SqlCommand("SELECT SUM(CAST(Paid as float)) as Paid FROM [ExcelEncLive].tblReceiptDtls WHERE InvoiceNo = @InvoiceNo", con))
    //                                {
    //                                    con.Open();
    //                                    cmdPaid.Parameters.AddWithValue("@InvoiceNo", dr["InvoiceNo"].ToString());
    //                                    object paidValObj = cmdPaid.ExecuteScalar();
    //                                    con.Close();
    //                                    paidVal = paidValObj != DBNull.Value ? Convert.ToDecimal(paidValObj) : 0;
    //                                }

    //                                decimal bal = Payable - paidVal;

    //                                if (!Dtt.Columns.Contains("Balance"))
    //                                {
    //                                    Dtt.Columns.Add("Balance", typeof(decimal));
    //                                }

    //                                dr["Balance"] = Math.Round(bal, 2);
    //                                balanceTotal += bal;
    //                                allData.Merge(Dtt);
    //                            }

    //                            if (Show == false)
    //                            {
    //                                //if (flg == "Excel")
    //                                //{
    //                                //    string excelFilePath = Server.MapPath("~/files/OutstandingReport.xlsx");

    //                                //    // Your existing PDF creation code...
    //                                //    using (MemoryStream excelStream = new MemoryStream())
    //                                //    {

    //                                //        // Create a new Excel document
    //                                //        using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(excelStream, SpreadsheetDocumentType.Workbook))
    //                                //        {
    //                                //            // Add a WorkbookPart to the document
    //                                //            WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
    //                                //            workbookPart.Workbook = new Workbook();

    //                                //            // Add a WorksheetPart to the WorkbookPart
    //                                //            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
    //                                //            worksheetPart.Worksheet = new Worksheet(new SheetData());

    //                                //            // Add a new sheet to the workbook
    //                                //            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
    //                                //            Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
    //                                //            sheets.Append(sheet);

    //                                //            // Get the sheet data
    //                                //            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

    //                                //            // Create header row
    //                                //            Row headerRow = new Row();
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Type"), DataType = CellValues.String });
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Doc No"), DataType = CellValues.String });
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Date"), DataType = CellValues.String });
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Due"), DataType = CellValues.String });
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Payable"), DataType = CellValues.String });
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Received"), DataType = CellValues.String });
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Balance"), DataType = CellValues.String });
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Cum. Bal"), DataType = CellValues.String });
    //                                //            headerRow.Append(new Cell() { CellValue = new CellValue("Days"), DataType = CellValues.String });
    //                                //            sheetData.AppendChild(headerRow);

    //                                //            // Add data rows
    //                                //            foreach (DataRow dataRow in allData.Rows)
    //                                //            {
    //                                //                Row newRow = new Row();
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Type"].ToString()), DataType = CellValues.String });
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Doc No"].ToString()), DataType = CellValues.String });
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Date"].ToString()), DataType = CellValues.String });
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Due"].ToString()), DataType = CellValues.String });
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Payable"].ToString()), DataType = CellValues.String });
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Received"].ToString()), DataType = CellValues.String });
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Balance"].ToString()), DataType = CellValues.String });
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Cum. Bal"].ToString()), DataType = CellValues.String });
    //                                //                newRow.Append(new Cell() { CellValue = new CellValue(dataRow["Days"].ToString()), DataType = CellValues.String });
    //                                //                sheetData.AppendChild(newRow);
    //                                //            }
    //                                //        }

    //                                //        // Save the Excel data to a file or perform other actions as needed
    //                                //       // string excelFilePath = Server.MapPath("~/files/OutstandingReport.xlsx");
    //                                //        File.WriteAllBytes(excelFilePath, excelStream.ToArray());

    //                                //        // Now you have the Excel file in the excelFilePath.
    //                                //    }


    //                                //}


    //                                if (flg == "Excel")
    //                                {
    //                                    // Create Excel file
    //                                    string excelFilePath = Server.MapPath("~/files/OutstandingReport.xlsx");

    //                                    using (MemoryStream excelStream = new MemoryStream())
    //                                    {
    //                                        using (XLWorkbook workbook = new XLWorkbook())
    //                                        {
    //                                            var worksheet = workbook.Worksheets.Add("Sheet1");

    //                                            // Add header row
    //                                            foreach (DataColumn column in allData.Columns)
    //                                            {
    //                                                worksheet.Cell(1, column.Ordinal + 1).Value = column.ColumnName;
    //                                            }

    //                                            // Add data rows
    //                                            for (int i = 0; i < allData.Rows.Count; i++)
    //                                            {
    //                                                for (int j = 0; j < allData.Columns.Count; j++)
    //                                                {
    //                                                    worksheet.Cell(i + 2, j + 1).Value = allData.Rows[i][j];
    //                                                }
    //                                            }

    //                                            // Save the Excel file
    //                                            workbook.SaveAs(excelFilePath);
    //                                        }

    //                                        // Respond with the Excel file
    //                                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
    //                                        string Outgoingfile = txtPartyName.Text + " OutstandingReport.xlsx";
    //                                        Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
    //                                        Response.AddHeader("Content-Length", file.Length.ToString());
    //                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                                        Response.WriteFile(file.FullName);
    //                                    }
    //                                }

    //                                else
    //                                {
    //                                    ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
    //                                }
    //                            }
    //                        }
    //                    }

    //                    doc.Close();
    //                    Response.End();
    //                }
    //            }
    //        }
    //    }
    //}


    //protected void Excel(string flg)
    //{
    //    DataTable Dt = new DataTable();
    //    SqlDataAdapter Da;

    //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString)) ;
    //    {
    //        con.Open();

    //        if (string.IsNullOrEmpty(txtPartyName.Text))
    //        {
    //            Da = new SqlDataAdapter("select distinct Top 10  (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr", con);
    //        }
    //        else
    //        {
    //            Da = new SqlDataAdapter("select distinct(BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //        }

    //        Da.Fill(Dt);

    //        using (StringWriter sw = new StringWriter())
    //        {
    //            using (StringReader sr = new StringReader(sw.ToString()))
    //            {
    //                Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
    //                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/OutstandingReport.pdf"), FileMode.Create));
    //                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
    //                doc.Open();

    //                CultureInfo info = CultureInfo.GetCultureInfo("en-IN");
    //                string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";
    //                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);

    //                png.ScaleToFit(70, 100);
    //                png.SetAbsolutePosition(40, 790);
    //                png.SpacingBefore = 50f;
    //                png.SpacingAfter = 1f;
    //                png.Alignment = Element.ALIGN_LEFT;
    //                doc.Add(png);

    //                if (Dt.Rows.Count > 0)
    //                {
    //                    foreach (DataRow row in Dt.Rows)
    //                    {
    //                        DataTable Dttt = new DataTable();

    //                        if (string.IsNullOrEmpty(txtPartyName.Text))
    //                        {
    //                            string BillingCustomer = row["BillingCustomer"].ToString();
    //                            SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + BillingCustomer + "'", con);
    //                            Daa.Fill(Dttt);

    //                            string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
    //                            string Paid = Dttt.Rows[0]["ContactNo"].ToString();
    //                        }
    //                        else
    //                        {
    //                            SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //                            Daa.Fill(Dttt);

    //                            string BillingCustomer = Dttt.Rows[0]["BillingCustomer"].ToString() == "" ? "" : Dttt.Rows[0]["BillingCustomer"].ToString();
    //                            string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
    //                            string Paid = Dttt.Rows[0]["ContactNo"].ToString();
    //                        }

    //                        PdfContentByte cd = writer.DirectContent;
    //                        cd.Rectangle(150f, 800f, 400f, 25f);
    //                        cd.Stroke();

    //                        cd.BeginText();
    //                        cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
    //                        cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "OUTSTANDING REPORT", 280, 808, 0);
    //                        cd.EndText();

    //                        PdfPTable table = new PdfPTable(4);

    //                        DataTable Dtt = new DataTable();
    //                        string fdate;
    //                        string tdate;
    //                        string ft = txtfromdate.Text;
    //                        string tt = txttodate.Text;
    //                        if (ft == "")
    //                        {
    //                            fdate = "";
    //                        }
    //                        else
    //                        {
    //                            fdate = ft;
    //                        }

    //                        if (tt == "")
    //                        {
    //                            tdate = "";
    //                        }
    //                        else
    //                        {
    //                            tdate = tt;
    //                        }

    //                        Dtt = GetData("[ExcelEncLive].SP_OutstandingR", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

    //                        if (Dtt.Rows.Count > 0)
    //                        {
    //                            string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
    //                            string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

    //                            SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
    //                            string paymentterm = cmddueDt.ExecuteScalar().ToString();

    //                            int DueDays = 0;

    //                            if (paymentterm == "30 Days credit")
    //                            {
    //                                DueDays = 30;
    //                            }
    //                            else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
    //                            {
    //                                DueDays = 60;
    //                            }
    //                            else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
    //                            {
    //                                DueDays = 45;
    //                            }
    //                            else if (paymentterm == "90 Days credit")
    //                            {
    //                                DueDays = 90;
    //                            }
    //                            else if (paymentterm == "75 Days credit")
    //                            {
    //                                DueDays = 75;
    //                            }
    //                            else
    //                            {
    //                                DueDays = 0;
    //                            }

    //                            PdfPCell Cell = new PdfPCell();

    //                            Paragraph paragraphTable1 = new Paragraph();
    //                            paragraphTable1.SpacingAfter = 10f;
    //                            paragraphTable1.SpacingBefore = 14f;

    //                            table = new PdfPTable(1);
    //                            table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                            float[] widths1 = new float[] { 100 };
    //                            table.SetWidths(widths1);
    //                            table.TotalWidth = 560f;
    //                            table.LockedWidth = true;

    //                            Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
    //                            Cell.HorizontalAlignment = 1;
    //                            Cell.Border = PdfPCell.NO_BORDER;

    //                            table.AddCell(Cell);

    //                            Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
    //                            Cell.HorizontalAlignment = 1;
    //                            Cell.Border = PdfPCell.NO_BORDER;
    //                            table.AddCell(Cell);

    //                            paragraphTable1.Add(table);
    //                            doc.Add(paragraphTable1);

    //                            Paragraph paragraphTable9 = new Paragraph();
    //                            paragraphTable9.SpacingAfter = 2f;
    //                            table = new PdfPTable(9);
    //                            float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
    //                            table.SetWidths(widths33349);
    //                            table.TotalWidth = 560f;
    //                            table.LockedWidth = true;
    //                            table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                            table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
    //                            paragraphTable9.Add(table);
    //                            doc.Add(paragraphTable9);

    //                            Paragraph paragraphTable2 = new Paragraph();
    //                            paragraphTable2.SpacingAfter = 0f;
    //                            table = new PdfPTable(9);
    //                            float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                            table.SetWidths(widths33);
    //                            decimal SumOfTotal = 0;
    //                            decimal SumOfPayableTotal = 0;
    //                            decimal SumOfbal = 0;
    //                            if (Dtt.Rows.Count > 0)
    //                            {
    //                                table.TotalWidth = 560f;
    //                                table.LockedWidth = true;
    //                                table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

    //                                int rowid = 1;
    //                                decimal Balance = 0;
    //                                for (int rowIndex = Dtt.Rows.Count - 1; rowIndex >= 0; rowIndex--)
    //                                {
    //                                    DataRow dr = Dtt.Rows[rowIndex];

    //                                    var payble = dr["Payable"].ToString();
    //                                    SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from  [ExcelEncLive].tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
    //                                    string paidval = cmdpaid.ExecuteScalar().ToString();

    //                                    var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval == "" ? "0" : paidval);
    //                                    Balance += bal;

    //                                    string str1 = "";
    //                                    if (dr["Invoicedate"].ToString() != "")
    //                                    {
    //                                        var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
    //                                        DateTime DueDate = today.AddDays(DueDays);
    //                                        str1 = DueDate.ToString();
    //                                        str1 = str1.Replace("12:00:00 AM", "");
    //                                        var time1 = str1;
    //                                        DateTime Invoice = Convert.ToDateTime(str1);
    //                                        str1 = Invoice.ToString("dd-MM-yyyy");
    //                                    }
    //                                    else
    //                                    {
    //                                        str1 = "";
    //                                    }

    //                                    table.TotalWidth = 560f;
    //                                    table.LockedWidth = true;
    //                                    table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(paidval == "" ? "0" : paidval, FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(Math.Round(bal).ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    rowid++;
    //                                    SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
    //                                    SumOfPayableTotal += Convert.ToDecimal(paidval == "" ? "0" : paidval);
    //                                    SumOfbal += bal;
    //                                }
    //                            }
    //                            Paragraph paragraphTable33 = new Paragraph();
    //                            paragraphTable33.SpacingAfter = 2f;
    //                            table = new PdfPTable(9);
    //                            float[] widths3334 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                            table.SetWidths(widths3334);
    //                            table.TotalWidth = 560f;
    //                            table.LockedWidth = true;
    //                            table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                            // Add your additional code here
    //                            paragraphTable33.Add(table);
    //                            doc.Add(paragraphTable33);

    //                        }
    //                    }
    //                }

    //                if (flg == "Excel")
    //                {
    //                    doc.Close();
    //                    string pathd = Server.MapPath("~") + "/files/OutstandingReport.pdf";
    //                    string pathsave = Server.MapPath("~") + "/files/OutstandingReport.xlsx";

    //                    Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
    //                    //Load the PDF file
    //                    pdf.LoadFromFile(pathd);
    //                    //Save to Excel
    //                    pdf.SaveToFile(pathsave, FileFormat.XLSX);

    //                    System.IO.FileInfo file = new System.IO.FileInfo(pathsave);
    //                    string Outgoingfile = txtPartyName.Text + " OutstandingReport.xlsx";
    //                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
    //                    Response.AddHeader("Content-Length", file.Length.ToString());
    //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                    Response.WriteFile(file.FullName);
    //                }
    //            }
    //        }
    //    }
    //}



    //protected void Excel(string flg)
    //{
    //    DataTable Dt = new DataTable();
    //    SqlDataAdapter Da;

    //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString)) ;
    //    {
    //        con.Open();

    //        if (string.IsNullOrEmpty(txtPartyName.Text))
    //        {
    //            Da = new SqlDataAdapter("select distinct Top 10  (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr", con);
    //        }
    //        else
    //        {
    //            Da = new SqlDataAdapter("select distinct(BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //        }

    //        Da.Fill(Dt);

    //        using (StringWriter sw = new StringWriter())
    //        {
    //            using (StringReader sr = new StringReader(sw.ToString()))
    //            {
    //                Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
    //                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/OutstandingReport.pdf"), FileMode.Create));
    //                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
    //                doc.Open();

    //                CultureInfo info = CultureInfo.GetCultureInfo("en-IN");
    //                string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";
    //                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);

    //                png.ScaleToFit(70, 100);
    //                png.SetAbsolutePosition(40, 790);
    //                png.SpacingBefore = 50f;
    //                png.SpacingAfter = 1f;
    //                png.Alignment = Element.ALIGN_LEFT;
    //                doc.Add(png);

    //                if (Dt.Rows.Count > 0)
    //                {
    //                    foreach (DataRow row in Dt.Rows)
    //                    {
    //                        DataTable Dttt = new DataTable();

    //                        if (string.IsNullOrEmpty(txtPartyName.Text))
    //                        {
    //                            string BillingCustomer = row["BillingCustomer"].ToString();
    //                            SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + BillingCustomer + "'", con);
    //                            Daa.Fill(Dttt);

    //                            string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
    //                            string Paid = Dttt.Rows[0]["ContactNo"].ToString();
    //                        }
    //                        else
    //                        {
    //                            SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //                            Daa.Fill(Dttt);

    //                            string BillingCustomer = Dttt.Rows[0]["BillingCustomer"].ToString() == "" ? "" : Dttt.Rows[0]["BillingCustomer"].ToString();
    //                            string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
    //                            string Paid = Dttt.Rows[0]["ContactNo"].ToString();
    //                        }

    //                        PdfContentByte cd = writer.DirectContent;
    //                        cd.Rectangle(150f, 800f, 400f, 25f);
    //                        cd.Stroke();

    //                        cd.BeginText();
    //                        cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
    //                        cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "OUTSTANDING REPORT", 280, 808, 0);
    //                        cd.EndText();

    //                        PdfPTable table = new PdfPTable(4);

    //                        DataTable Dtt = new DataTable();
    //                        string fdate;
    //                        string tdate;
    //                        string ft = txtfromdate.Text;
    //                        string tt = txttodate.Text;
    //                        if (ft == "")
    //                        {
    //                            fdate = "";
    //                        }
    //                        else
    //                        {
    //                            fdate = ft;
    //                        }

    //                        if (tt == "")
    //                        {
    //                            tdate = "";
    //                        }
    //                        else
    //                        {
    //                            tdate = tt;
    //                        }

    //                        Dtt = GetData("[ExcelEncLive].SP_OutstandingR", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

    //                        if (Dtt.Rows.Count > 0)
    //                        {
    //                            string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
    //                            string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

    //                            SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
    //                            string paymentterm = cmddueDt.ExecuteScalar().ToString();

    //                            int DueDays = 0;

    //                            if (paymentterm == "30 Days credit")
    //                            {
    //                                DueDays = 30;
    //                            }
    //                            else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
    //                            {
    //                                DueDays = 60;
    //                            }
    //                            else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
    //                            {
    //                                DueDays = 45;
    //                            }
    //                            else if (paymentterm == "90 Days credit")
    //                            {
    //                                DueDays = 90;
    //                            }
    //                            else if (paymentterm == "75 Days credit")
    //                            {
    //                                DueDays = 75;
    //                            }
    //                            else
    //                            {
    //                                DueDays = 0;
    //                            }

    //                            PdfPCell Cell = new PdfPCell();

    //                            Paragraph paragraphTable1 = new Paragraph();
    //                            paragraphTable1.SpacingAfter = 10f;
    //                            paragraphTable1.SpacingBefore = 14f;

    //                            table = new PdfPTable(1);
    //                            table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                            float[] widths1 = new float[] { 100 };
    //                            table.SetWidths(widths1);
    //                            table.TotalWidth = 560f;
    //                            table.LockedWidth = true;

    //                            Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
    //                            Cell.HorizontalAlignment = 1;
    //                            Cell.Border = PdfPCell.NO_BORDER;

    //                            table.AddCell(Cell);

    //                            Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
    //                            Cell.HorizontalAlignment = 1;
    //                            Cell.Border = PdfPCell.NO_BORDER;
    //                            table.AddCell(Cell);

    //                            paragraphTable1.Add(table);
    //                            doc.Add(paragraphTable1);

    //                            Paragraph paragraphTable9 = new Paragraph();
    //                            paragraphTable9.SpacingAfter = 2f;
    //                            table = new PdfPTable(9);
    //                            float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
    //                            table.SetWidths(widths33349);
    //                            table.TotalWidth = 560f;
    //                            table.LockedWidth = true;
    //                            table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                            table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                            table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
    //                            paragraphTable9.Add(table);
    //                            doc.Add(paragraphTable9);

    //                            Paragraph paragraphTable2 = new Paragraph();
    //                            paragraphTable2.SpacingAfter = 0f;
    //                            table = new PdfPTable(9);
    //                            float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                            table.SetWidths(widths33);
    //                            decimal SumOfTotal = 0;
    //                            decimal SumOfPayableTotal = 0;
    //                            decimal SumOfbal = 0;
    //                            if (Dtt.Rows.Count > 0)
    //                            {
    //                                table.TotalWidth = 560f;
    //                                table.LockedWidth = true;
    //                                table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                                table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

    //                                int rowid = 1;
    //                                decimal Balance = 0;
    //                                for (int rowIndex = Dtt.Rows.Count - 1; rowIndex >= 0; rowIndex--)
    //                                {
    //                                    DataRow dr = Dtt.Rows[rowIndex];

    //                                    var payble = dr["Payable"].ToString();
    //                                    SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from  [ExcelEncLive].tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
    //                                    string paidval = cmdpaid.ExecuteScalar().ToString();

    //                                    var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval == "" ? "0" : paidval);
    //                                    Balance += bal;

    //                                    string str1 = "";
    //                                    if (dr["Invoicedate"].ToString() != "")
    //                                    {
    //                                        var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
    //                                        DateTime DueDate = today.AddDays(DueDays);
    //                                        str1 = DueDate.ToString();
    //                                        str1 = str1.Replace("12:00:00 AM", "");
    //                                        var time1 = str1;
    //                                        DateTime Invoice = Convert.ToDateTime(str1);
    //                                        str1 = Invoice.ToString("dd-MM-yyyy");
    //                                    }
    //                                    else
    //                                    {
    //                                        str1 = "";
    //                                    }

    //                                    table.TotalWidth = 560f;
    //                                    table.LockedWidth = true;
    //                                    table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(paidval == "" ? "0" : paidval, FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(Math.Round(bal).ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                                    rowid++;
    //                                    SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
    //                                    SumOfPayableTotal += Convert.ToDecimal(paidval == "" ? "0" : paidval);
    //                                    SumOfbal += bal;
    //                                }
    //                            }
    //                            Paragraph paragraphTable33 = new Paragraph();
    //                            paragraphTable33.SpacingAfter = 2f;
    //                            table = new PdfPTable(9);
    //                            float[] widths3334 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                            table.SetWidths(widths3334);
    //                            table.TotalWidth = 560f;
    //                            table.LockedWidth = true;
    //                            table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                            // Add your additional code here
    //                            paragraphTable33.Add(table);
    //                            doc.Add(paragraphTable33);

    //                        }
    //                    }
    //                }

    //                if (flg == "Excel")
    //                {
    //                    doc.Close();
    //                    string pathd = Server.MapPath("~") + "/files/OutstandingReport.pdf";
    //                    string pathsave = Server.MapPath("~") + "/files/OutstandingReport.xlsx";

    //                    Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
    //                    //Load the PDF file
    //                    pdf.LoadFromFile(pathd);
    //                    //Save to Excel
    //                    pdf.SaveToFile(pathsave, FileFormat.XLSX);

    //                    System.IO.FileInfo file = new System.IO.FileInfo(pathsave);
    //                    string Outgoingfile = txtPartyName.Text + " OutstandingReport.xlsx";
    //                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
    //                    Response.AddHeader("Content-Length", file.Length.ToString());
    //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                    Response.WriteFile(file.FullName);
    //                }
    //            }
    //        }
    //    }
    //}


    //protected void Pdf1(string flg)
    //{

    //    DataTable Dt = new DataTable();
    //    SqlDataAdapter Da;
    //    if (txtPartyName.Text == "")
    //    {
    //        //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr", con);
    //        Da = new SqlDataAdapter("select distinct Top 10  (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr", con);
    //    }
    //    else
    //    {
    //        //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //        Da = new SqlDataAdapter("select distinct(BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //    }

    //    Da.Fill(Dt);

    //    StringWriter sw = new StringWriter();
    //    StringReader sr = new StringReader(sw.ToString());

    //    Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
    //    iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "OutstandingReport.pdf", FileMode.Create));
    //    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
    //    doc.Open();
    //    System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
    //    string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";
    //    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);
    //    //Resize image depend upon your need
    //    png.ScaleToFit(70, 100);
    //    //For Image Position
    //    png.SetAbsolutePosition(40, 790);
    //    //var document = new Document();
    //    //Give space before image
    //    //png.ScaleToFit(document.PageSize.Width - (document.RightMargin * 100), 50);
    //    png.SpacingBefore = 50f;
    //    //Give some space after the image
    //    png.SpacingAfter = 1f;
    //    png.Alignment = Element.ALIGN_LEFT;
    //    doc.Add(png);
    //    if (Dt.Rows.Count > 0)
    //    {
    //        DataTable Dttt = new DataTable();
    //        if (txtPartyName.Text == "")
    //        {
    //            string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
    //            SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + BillingCustomer + "'", con);
    //            Daa.Fill(Dttt);

    //            //string ShippingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
    //            string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
    //            string Paid = Dttt.Rows[0]["ContactNo"].ToString();
    //        }
    //        else
    //        {
    //            SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //            Daa.Fill(Dttt);

    //            string BillingCustomer = Dttt.Rows[0]["BillingCustomer"].ToString() == "" ? "" : Dttt.Rows[0]["BillingCustomer"].ToString();
    //            //string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
    //            string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
    //            string Paid = Dttt.Rows[0]["ContactNo"].ToString();
    //        }


    //        PdfContentByte cd = writer.DirectContent;
    //        cd.Rectangle(150f, 800f, 400f, 25f);

    //        cd.Stroke();

    //        // Header 
    //        cd.BeginText();
    //        cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
    //        cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "OUTSTANDING REPORT", 280, 808, 0);
    //        cd.EndText();

    //        PdfPTable table = new PdfPTable(4);

    //        foreach (DataRow row in Dt.Rows)
    //        {
    //            DataTable Dtt = new DataTable();
    //            string fdate;
    //            string tdate;
    //            string ft = txtfromdate.Text;
    //            string tt = txttodate.Text;
    //            if (ft == "")
    //            {
    //                fdate = "";
    //            }
    //            else
    //            {
    //                fdate = ft;
    //            }

    //            if (tt == "")
    //            {
    //                tdate = "";
    //            }
    //            else
    //            {
    //                tdate = tt;
    //            }

    //            Dtt = GetData("[ExcelEncLive].[SP_OutstandingRNew]", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

    //            if (Dtt.Rows.Count > 0)
    //            {
    //                string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
    //                string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

    //                con.Open();
    //                SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
    //                string paymentterm = cmddueDt.ExecuteScalar().ToString();
    //                con.Close();

    //                int DueDays = 0;

    //                if (paymentterm == "30 Days credit")
    //                {
    //                    DueDays = 30;
    //                }
    //                else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
    //                {
    //                    DueDays = 60;
    //                }
    //                else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
    //                {
    //                    DueDays = 45;
    //                }
    //                else if (paymentterm == "90 Days credit")
    //                {
    //                    DueDays = 90;
    //                }
    //                else if (paymentterm == "75 Days credit")
    //                {
    //                    DueDays = 75;
    //                }
    //                else
    //                {
    //                    DueDays = 0;
    //                }

    //                PdfPCell Cell = new PdfPCell();

    //                Paragraph paragraphTable1 = new Paragraph();
    //                paragraphTable1.SpacingAfter = 10f;
    //                paragraphTable1.SpacingBefore = 14f;

    //                table = new PdfPTable(1);
    //                table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                float[] widths1 = new float[] { 100 };
    //                table.SetWidths(widths1);
    //                table.TotalWidth = 560f;
    //                table.LockedWidth = true;

    //                Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
    //                Cell.HorizontalAlignment = 1;
    //                Cell.Border = PdfPCell.NO_BORDER;

    //                table.AddCell(Cell);

    //                Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
    //                Cell.HorizontalAlignment = 1;
    //                Cell.Border = PdfPCell.NO_BORDER;
    //                table.AddCell(Cell);

    //                paragraphTable1.Add(table);
    //                doc.Add(paragraphTable1);

    //                //Advance Table
    //                Paragraph paragraphTable9 = new Paragraph();
    //                paragraphTable9.SpacingAfter = 2f;
    //                table = new PdfPTable(9);
    //                float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
    //                table.SetWidths(widths33349);
    //                table.TotalWidth = 560f;
    //                table.LockedWidth = true;
    //                table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
    //                paragraphTable9.Add(table);
    //                doc.Add(paragraphTable9);
    //                ///////////////////


    //                Paragraph paragraphTable2 = new Paragraph();
    //                paragraphTable2.SpacingAfter = 0f;
    //                table = new PdfPTable(9);
    //                float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                table.SetWidths(widths33);
    //                decimal SumOfTotal = 0;
    //                decimal SumOfPayableTotal = 0;
    //                decimal SumOfbal = 0;
    //                if (Dtt.Rows.Count > 0)
    //                {
    //                    table.TotalWidth = 560f;
    //                    table.LockedWidth = true;
    //                    //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
    //                    table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

    //                    int rowid = 1;
    //                    decimal Balance = 0;

    //                    foreach (DataRow dr in Dtt.Rows)
    //                    {
    //                        //var Recevd = dr["Received"].ToString();
    //                        var payble = dr["Payable"].ToString();
    //                        con.Open();
    //                        //SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from  [ExcelEncLive].tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
    //                        SqlCommand cmdpaid = new SqlCommand("SELECT Max(CAST(Paid as float)) FROM [ExcelEncLive]. tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
    //                        string paidval = cmdpaid.ExecuteScalar().ToString();

    //                        SqlCommand cmdRecived = new SqlCommand("SELECT ISNULL(SUM(CAST(Grandtotal as float)), 0) as Recived FROM [ExcelEncLive].tblCreditDebitNoteHdr WHERE BillNumber = '" + dr["InvoiceNo"].ToString() + "' AND NoteType = 'Credit_Sale'", con);


    //                        //SqlCommand cmdRecived = new SqlCommand("select (CAST(Grandtotal as float)) as Recived  from  [ExcelEncLive].tblCreditDebitNoteHdr where  BillNumber='" + dr["InvoiceNo"].ToString() + "'", con);
    //                        string Recived = cmdRecived.ExecuteScalar().ToString();
    //                        decimal Recivedamt;
    //                        if (Recived == "")
    //                        {
    //                            Recivedamt = 0;

    //                        }

    //                        else
    //                        {
    //                            Recivedamt = Convert.ToDecimal(Recived);
    //                        }
    //                        con.Close();
    //                        //var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval) +  Recivedamt;
    //                        var bal = Convert.ToDecimal(payble) - (Convert.ToDecimal(paidval == "" ? "0" : paidval) + Recivedamt);
    //                        Balance += bal;



    //                        //Bind DueDate from Payment Term
    //                        string str1 = "";
    //                        if (dr["Invoicedate"].ToString() != "")
    //                        {
    //                            var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
    //                            DateTime DueDate = today.AddDays(DueDays);
    //                            str1 = DueDate.ToString();
    //                            str1 = str1.Replace("12:00:00 AM", "");
    //                            var time1 = str1;
    //                            DateTime Invoice = Convert.ToDateTime(str1);
    //                            str1 = Invoice.ToString("dd-MM-yyyy");
    //                        }
    //                        else
    //                        {
    //                            str1 = "";
    //                        }

    //                        table.TotalWidth = 560f;
    //                        table.LockedWidth = true;
    //                        //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
    //                        table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase(dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase((Convert.ToDecimal(paidval == "" ? "0" : paidval) + Recivedamt).ToString(), FontFactory.GetFont("Arial", 9)));
    //                        //table.AddCell(new Phrase(paidval == "" ? "0" : paidval, FontFactory.GetFont("Arial", 9)));
    //                        //table.AddCell(new Phrase(paidval =  paidval, FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase(Math.Round(bal).ToString(), FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                        rowid++;
    //                        SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
    //                        SumOfPayableTotal += Convert.ToDecimal(paidval == "" ? "0" : paidval);
    //                        SumOfbal += bal;
    //                    }
    //                }
    //                Dtt.Rows.Clear();
    //                paragraphTable2.Add(table);
    //                doc.Add(paragraphTable2);

    //                con.Open();
    //                SqlCommand cmdAdv = new SqlCommand("select SUM(CAST(Amount as float)) from  [ExcelEncLive].tblReceiptHdr where Against='Advance' and Partyname='" + PartyName + "'", con);
    //                string Advre = cmdAdv.ExecuteScalar().ToString();
    //                con.Close();

    //                Paragraph paragraphTable3 = new Paragraph();
    //                paragraphTable2.SpacingAfter = 10f;
    //                table = new PdfPTable(9);
    //                float[] widths333 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                table.SetWidths(widths333);
    //                table.TotalWidth = 560f;
    //                table.LockedWidth = true;
    //                table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                table.AddCell(new Phrase(Math.Round(SumOfTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase(Math.Round(SumOfPayableTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase(Math.Round(SumOfbal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                paragraphTable3.Add(table);
    //                doc.Add(paragraphTable3);

    //                //Advance Table
    //                Paragraph paragraphTable33 = new Paragraph();
    //                paragraphTable33.SpacingAfter = 2f;
    //                table = new PdfPTable(9);
    //                float[] widths3334 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                table.SetWidths(widths3334);
    //                table.TotalWidth = 560f;
    //                table.LockedWidth = true;
    //                table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase("Advance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase(Convert.ToDouble(Advre == "" ? "0" : Advre).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                paragraphTable33.Add(table);
    //                doc.Add(paragraphTable33);
    //                ///////////////////

    //                var fBalnace = Convert.ToDecimal(SumOfbal) - Convert.ToDecimal(Advre == "" ? "0" : Advre);

    //                //Total Balance table 
    //                Paragraph paragraphTable334 = new Paragraph();
    //                paragraphTable334.SpacingAfter = 2f;
    //                table = new PdfPTable(9);
    //                float[] widths33344 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                table.SetWidths(widths33344);
    //                table.TotalWidth = 560f;
    //                table.LockedWidth = true;
    //                table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase(Math.Round(fBalnace).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                paragraphTable334.Add(table);
    //                doc.Add(paragraphTable334);
    //                /////////////////////

    //                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
    //                doc.Add(p);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
    //                Show = false;
    //                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
    //            }
    //        }
    //        //ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
    //        //doc.Close();
    //    }
    //    if (Show == false)
    //    {
    //        if (flg == "Excel")
    //        {
    //            doc.Close();
    //            string pathd = Server.MapPath("~") + "/files/OutstandingReport.pdf";
    //            string pathsave = Server.MapPath("~") + "/files/OutstandingReport.xlsx";

    //            Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
    //            Spire.Pdf.Conversion.XlsxLineLayoutOptions options = new XlsxLineLayoutOptions(false, true, true, true);
    //             pdf.ConvertOptions.SetPdfToXlsxOptions(options);



    //            //Load the PDF file
    //            pdf.LoadFromFile(pathd);
    //            //Save to Excel
    //            pdf.SaveToFile(pathsave, FileFormat.XLSX);

    //            System.IO.FileInfo file = new System.IO.FileInfo(pathsave);
    //            string Outgoingfile = txtPartyName.Text + " OutstandingReport.xlsx";
    //            Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
    //            Response.AddHeader("Content-Length", file.Length.ToString());
    //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //            Response.WriteFile(file.FullName);
    //        }
    //        else
    //        {
    //            ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
    //            doc.Close();
    //        }
    //    }
    //}


    //protected void Excel(string flg)
    //{
    //    DataTable Dt = new DataTable();
    //    SqlDataAdapter Da;

    //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
    //    {
    //        con.Open();

    //        if (string.IsNullOrEmpty(txtPartyName.Text))
    //        {
    //            Da = new SqlDataAdapter("select distinct Top 10  (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr", con);
    //        }
    //        else
    //        {
    //            Da = new SqlDataAdapter("select distinct(BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //        }

    //        Da.Fill(Dt);

    //        using (var workbook = new XLWorkbook())
    //        {
    //            var worksheet = workbook.Worksheets.Add("OutstandingReport"); // Create a single worksheet

    //            foreach (DataRow row in Dt.Rows)
    //            {
    //                DataTable Dtt = new DataTable();
    //                string fdate;
    //                string tdate;
    //                string ft = txtfromdate.Text;
    //                string tt = txttodate.Text;
    //                if (ft == "")
    //                {
    //                    fdate = "";
    //                }
    //                else
    //                {
    //                    fdate = ft;
    //                }

    //                if (tt == "")
    //                {
    //                    tdate = "";
    //                }
    //                else
    //                {
    //                    tdate = tt;
    //                }

    //                Dtt = GetData("[ExcelEncLive].SP_OutstandingR", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

    //                if (Dtt.Rows.Count > 0)
    //                {
    //                    string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
    //                    string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

    //                    SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
    //                    string paymentterm = cmddueDt.ExecuteScalar().ToString();

    //                    int DueDays = 0;

    //                    if (paymentterm == "30 Days credit")
    //                    {
    //                        DueDays = 30;
    //                    }
    //                    else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
    //                    {
    //                        DueDays = 60;
    //                    }
    //                    else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
    //                    {
    //                        DueDays = 45;
    //                    }
    //                    else if (paymentterm == "90 Days credit")
    //                    {
    //                        DueDays = 90;
    //                    }
    //                    else if (paymentterm == "75 Days credit")
    //                    {
    //                        DueDays = 75;
    //                    }
    //                    else
    //                    {
    //                        DueDays = 0;
    //                    }

    //                    int rowid = worksheet.LastRowUsed() != null ? worksheet.LastRowUsed().RowNumber() + 1 : 1;


    //                    for (int rowIndex = Dtt.Rows.Count - 1; rowIndex >= 0; rowIndex--)
    //                    {
    //                        DataRow dr = Dtt.Rows[rowIndex];

    //                        var payble = dr["Payable"].ToString();
    //                        SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from  [ExcelEncLive].tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
    //                        string paidval = cmdpaid.ExecuteScalar().ToString();

    //                        var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval == "" ? "0" : paidval);
    //                        decimal Balance = 0;

    //                        string str1 = "";
    //                        if (dr["Invoicedate"].ToString() != "")
    //                        {
    //                            var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
    //                            DateTime DueDate = today.AddDays(DueDays);
    //                            str1 = DueDate.ToString();
    //                            str1 = str1.Replace("12:00:00 AM", "");
    //                            var time1 = str1;
    //                            DateTime Invoice = Convert.ToDateTime(str1);
    //                            str1 = Invoice.ToString("dd-MM-yyyy");
    //                        }
    //                        else
    //                        {
    //                            str1 = "";
    //                        }

    //                        worksheet.Cell(rowid + 1, 1).Value = dr["Type"].ToString();
    //                        worksheet.Cell(rowid + 1, 2).Value = dr["InvoiceNo"].ToString();
    //                        worksheet.Cell(rowid + 1, 3).Value = dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray());
    //                        worksheet.Cell(rowid + 1, 4).Value = str1;
    //                        worksheet.Cell(rowid + 1, 5).Value = dr["Payable"].ToString();
    //                        worksheet.Cell(rowid + 1, 6).Value = paidval == "" ? "0" : paidval;
    //                        worksheet.Cell(rowid + 1, 7).Value = Math.Round(bal).ToString();
    //                        worksheet.Cell(rowid + 1, 8).Value = Math.Round(Balance).ToString();
    //                        worksheet.Cell(rowid + 1, 9).Value = dr["days"].ToString();

    //                        rowid++;
    //                    }
    //                }
    //            }

    //            if (flg == "Excel")
    //            {
    //                string pathsave = Server.MapPath("~") + "/files/OutstandingReport.xlsx";
    //                workbook.SaveAs(pathsave);

    //                // Send the Excel file to the client
    //                Response.Clear();
    //                Response.ClearHeaders();
    //                Response.ClearContent();
    //                Response.AddHeader("Content-Disposition", "attachment; filename=OutstandingReport.xlsx");
    //                Response.AddHeader("Content-Length", new FileInfo(pathsave).Length.ToString());
    //                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                Response.TransmitFile(pathsave);
    //                Response.Flush();
    //                Response.Close();
    //            }
    //            else
    //            {
    //                ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
    //            }
    //        }
    //    }
    //}

    //protected void Excel(string flg)
    //{
    //    DataTable Dt = new DataTable();
    //    SqlDataAdapter Da;

    //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
    //    {
    //        con.Open();

    //        if (string.IsNullOrEmpty(txtPartyName.Text))
    //        {
    //            Da = new SqlDataAdapter("select distinct Top 10  (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr", con);
    //        }
    //        else
    //        {
    //            Da = new SqlDataAdapter("select distinct(BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //        }

    //        Da.Fill(Dt);

    //        using (var workbook = new XLWorkbook())
    //        {
    //            var worksheet = workbook.Worksheets.Add("OutstandingReport"); // Create a single worksheet

    //            foreach (DataRow row in Dt.Rows)
    //            {
    //                DataTable Dtt = new DataTable();
    //                string fdate;
    //                string tdate;
    //                string ft = txtfromdate.Text;
    //                string tt = txttodate.Text;
    //                if (ft == "")
    //                {
    //                    fdate = "";
    //                }
    //                else
    //                {
    //                    fdate = ft;
    //                }

    //                if (tt == "")
    //                {
    //                    tdate = "";
    //                }
    //                else
    //                {
    //                    tdate = tt;
    //                }

    //                Dtt = GetData("[ExcelEncLive].SP_OutstandingR", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

    //                if (Dtt.Rows.Count > 0)
    //                {
    //                    string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
    //                    string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

    //                    SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
    //                    string paymentterm = cmddueDt.ExecuteScalar().ToString();

    //                    int DueDays = 0;

    //                    if (paymentterm == "30 Days credit")
    //                    {
    //                        DueDays = 30;
    //                    }
    //                    else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
    //                    {
    //                        DueDays = 60;
    //                    }
    //                    else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
    //                    {
    //                        DueDays = 45;
    //                    }
    //                    else if (paymentterm == "90 Days credit")
    //                    {
    //                        DueDays = 90;
    //                    }
    //                    else if (paymentterm == "75 Days credit")
    //                    {
    //                        DueDays = 75;
    //                    }
    //                    else
    //                    {
    //                        DueDays = 0;
    //                    }

    //                    int rowid = 1;

    //                    // Add header section to the worksheet
    //                    worksheet.Cell(rowid, 1).Value = "Type";
    //                    worksheet.Cell(rowid, 2).Value = "Doc No";
    //                    worksheet.Cell(rowid, 3).Value = "Date";
    //                    worksheet.Cell(rowid, 4).Value = "Due";
    //                    worksheet.Cell(rowid, 5).Value = "Payable";
    //                    worksheet.Cell(rowid, 6).Value = "Received";
    //                    worksheet.Cell(rowid, 7).Value = "Balance";
    //                    worksheet.Cell(rowid, 8).Value = "Cum. Bal";
    //                    worksheet.Cell(rowid, 9).Value = "Days";

    //                    rowid++;

    //                    for (int rowIndex = Dtt.Rows.Count - 1; rowIndex >= 0; rowIndex--)
    //                    {
    //                        DataRow dr = Dtt.Rows[rowIndex];

    //                        // Populate the worksheet with data
    //                        rowid++;
    //                        worksheet.Cell(rowid, 1).Value = dr["Type"].ToString();
    //                        worksheet.Cell(rowid, 2).Value = dr["InvoiceNo"].ToString();
    //                        worksheet.Cell(rowid, 3).Value = dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray());
    //                        worksheet.Cell(rowid, 4).Value = ""; // You may need to calculate and set the due date
    //                        worksheet.Cell(rowid, 5).Value = dr["Payable"].ToString();
    //                        worksheet.Cell(rowid, 6).Value = "0"; // Set received value
    //                        worksheet.Cell(rowid, 7).Value = "0"; // Set balance value
    //                        worksheet.Cell(rowid, 8).Value = "0"; // Set cumulative balance value
    //                        worksheet.Cell(rowid, 9).Value = dr["days"].ToString();
    //                    }
    //                }
    //            }

    //            if (flg == "Excel")
    //            {
    //                string pathsave = Server.MapPath("~") + "/files/OutstandingReport.xlsx";
    //                workbook.SaveAs(pathsave);

    //                // Send the Excel file to the client
    //                Response.Clear();
    //                Response.ClearHeaders();
    //                Response.ClearContent();
    //                Response.AddHeader("Content-Disposition", "attachment; filename=OutstandingReport.xlsx");
    //                Response.AddHeader("Content-Length", new FileInfo(pathsave).Length.ToString());
    //                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                Response.TransmitFile(pathsave);
    //                Response.Flush();
    //                Response.Close();
    //            }
    //            else
    //            {
    //                ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
    //            }
    //        }
    //    }
    //}



    //protected void Excel(string flg)
    //{
    //    DataTable Dt = new DataTable();
    //    SqlDataAdapter Da;

    //    if (txtPartyName.Text == "")
    //    {
    //        Da = new SqlDataAdapter("select distinct Top 10 (BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr", con);
    //    }
    //    else
    //    {
    //        Da = new SqlDataAdapter("select distinct(BillingCustomer) from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //    }

    //    Da.Fill(Dt);

    //    StringWriter sw = new StringWriter();
    //    StringReader sr = new StringReader(sw.ToString());

    //    Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
    //    iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "OutstandingReport.pdf", FileMode.Create));
    //    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
    //    doc.Open();

    //    string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";
    //    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);
    //    png.ScaleToFit(70, 100);
    //    png.SetAbsolutePosition(40, 790);
    //    png.SpacingBefore = 50f;
    //    png.SpacingAfter = 1f;
    //    png.Alignment = Element.ALIGN_LEFT;
    //    doc.Add(png);

    //    if (Dt.Rows.Count > 0)
    //    {
    //        foreach (DataRow row in Dt.Rows)
    //        {
    //            DataTable Dttt = new DataTable();

    //            if (txtPartyName.Text == "")
    //            {
    //                string BillingCustomer = row["BillingCustomer"].ToString();
    //                SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + BillingCustomer + "'", con);
    //                Daa.Fill(Dttt);

    //                string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
    //                string Paid = Dttt.Rows[0]["ContactNo"].ToString();
    //            }
    //            else
    //            {
    //                SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from [ExcelEncLive].tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
    //                Daa.Fill(Dttt);

    //                string BillingCustomer = Dttt.Rows[0]["BillingCustomer"].ToString() == "" ? "" : Dttt.Rows[0]["BillingCustomer"].ToString();
    //                string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
    //                string Paid = Dttt.Rows[0]["ContactNo"].ToString();
    //            }

    //            PdfContentByte cd = writer.DirectContent;
    //            cd.Rectangle(150f, 800f, 400f, 25f);
    //            cd.Stroke();

    //            cd.BeginText();
    //            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
    //            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "OUTSTANDING REPORT", 280, 808, 0);
    //            cd.EndText();

    //            PdfPTable table = new PdfPTable(4);

    //            foreach (DataRow dr in Dt.Rows)
    //            {
    //                DataTable Dtt = new DataTable();
    //                string fdate;
    //                string tdate;
    //                string ft = txtfromdate.Text;
    //                string tt = txttodate.Text;

    //                if (ft == "")
    //                {
    //                    fdate = "";
    //                }
    //                else
    //                {
    //                    fdate = ft;
    //                }

    //                if (tt == "")
    //                {
    //                    tdate = "";
    //                }
    //                else
    //                {
    //                    tdate = tt;
    //                }

    //                Dtt = GetData("[ExcelEncLive].[SP_OutstandingRNew]", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

    //                if (Dtt.Rows.Count > 0)
    //                {
    //                    string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
    //                    string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

    //                    con.Open();
    //                    SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
    //                    string paymentterm = cmddueDt.ExecuteScalar().ToString();
    //                    con.Close();

    //                    int DueDays = 0;

    //                    if (paymentterm == "30 Days credit")
    //                    {
    //                        DueDays = 30;
    //                    }
    //                    else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
    //                    {
    //                        DueDays = 60;
    //                    }
    //                    else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
    //                    {
    //                        DueDays = 45;
    //                    }
    //                    else if (paymentterm == "90 Days credit")
    //                    {
    //                        DueDays = 90;
    //                    }
    //                    else if (paymentterm == "75 Days credit")
    //                    {
    //                        DueDays = 75;
    //                    }
    //                    else
    //                    {
    //                        DueDays = 0;
    //                    }

    //                    PdfPCell Cell = new PdfPCell();

    //                    Paragraph paragraphTable1 = new Paragraph();
    //                    paragraphTable1.SpacingAfter = 10f;
    //                    paragraphTable1.SpacingBefore = 14f;

    //                    table = new PdfPTable(1);
    //                    table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                    float[] widths1 = new float[] { 100 };
    //                    table.SetWidths(widths1);
    //                    table.TotalWidth = 560f;
    //                    table.LockedWidth = true;

    //                    Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
    //                    Cell.HorizontalAlignment = 1;
    //                    Cell.Border = PdfPCell.NO_BORDER;

    //                    table.AddCell(Cell);

    //                    Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
    //                    Cell.HorizontalAlignment = 1;
    //                    Cell.Border = PdfPCell.NO_BORDER;
    //                    table.AddCell(Cell);

    //                    paragraphTable1.Add(table);
    //                    doc.Add(paragraphTable1);

    //                    Paragraph paragraphTable9 = new Paragraph();
    //                    paragraphTable9.SpacingAfter = 2f;
    //                    table = new PdfPTable(9);
    //                    float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
    //                    table.SetWidths(widths33349);
    //                    table.TotalWidth = 560f;
    //                    table.LockedWidth = true;
    //                    table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                    table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
    //                    table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
    //                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                    table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
    //                    table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
    //                    paragraphTable9.Add(table);
    //                    doc.Add(paragraphTable9);

    //                    Paragraph paragraphTable2 = new Paragraph();
    //                    paragraphTable2.SpacingAfter = 0f;
    //                    table = new PdfPTable(9);
    //                    float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                    table.SetWidths(widths33);
    //                    decimal SumOfTotal = 0;
    //                    decimal SumOfPayableTotal = 0;
    //                    decimal SumOfbal = 0;

    //                    if (Dtt.Rows.Count > 0)
    //                    {
    //                        table.TotalWidth = 560f;
    //                        table.LockedWidth = true;
    //                        table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

    //                        int rowid = 1;
    //                        decimal Balance = 0;

    //                        foreach (DataRow dr in Dtt.Rows)
    //                        {
    //                            var payable = dr["Payable"].ToString();
    //                            con.Open();
    //                            SqlCommand cmdPaid = new SqlCommand("SELECT Max(CAST(Paid as float)) FROM [ExcelEncLive].tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
    //                            string paidValue = cmdPaid.ExecuteScalar().ToString();

    //                            SqlCommand cmdReceived = new SqlCommand("SELECT ISNULL(SUM(CAST(Grandtotal as float)), 0) as Received FROM [ExcelEncLive].tblCreditDebitNoteHdr WHERE BillNumber = '" + dr["InvoiceNo"].ToString() + "' AND NoteType = 'Credit_Sale'", con);

    //                            string received = cmdReceived.ExecuteScalar().ToString();
    //                            decimal receivedAmt;

    //                            if (received == "")
    //                            {
    //                                receivedAmt = 0;
    //                            }
    //                            else
    //                            {
    //                                receivedAmt = Convert.ToDecimal(received);
    //                            }
    //                            con.Close();

    //                            var balance = Convert.ToDecimal(payable) - (Convert.ToDecimal(paidValue == "" ? "0" : paidValue) + receivedAmt);
    //                            Balance += balance;

    //                            string str1 = "";
    //                            if (dr["Invoicedate"].ToString() != "")
    //                            {
    //                                var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
    //                                DateTime dueDate = today.AddDays(DueDays);
    //                                str1 = dueDate.ToString();
    //                                str1 = str1.Replace("12:00:00 AM", "");
    //                                var time1 = str1;
    //                                DateTime invoiceDate = Convert.ToDateTime(str1);
    //                                str1 = invoiceDate.ToString("dd-MM-yyyy");
    //                            }
    //                            else
    //                            {
    //                                str1 = "";
    //                            }

    //                            table.TotalWidth = 560f;
    //                            table.LockedWidth = true;
    //                            table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase((Convert.ToDecimal(paidValue == "" ? "0" : paidValue) + receivedAmt).ToString(), FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(Math.Round(balance).ToString(), FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
    //                            table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
    //                            rowid++;
    //                            SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
    //                            SumOfPayableTotal += Convert.ToDecimal(paidValue == "" ? "0" : paidValue);
    //                            SumOfbal += balance;
    //                        }

    //                        Dtt.Rows.Clear();
    //                        paragraphTable2.Add(table);
    //                        doc.Add(paragraphTable2);

    //                        con.Open();
    //                        SqlCommand cmdAdv = new SqlCommand("select SUM(CAST(Amount as float)) from [ExcelEncLive].tblReceiptHdr where Against='Advance' and Partyname='" + PartyName + "'", con);
    //                        string advRe = cmdAdv.ExecuteScalar().ToString();
    //                        con.Close();

    //                        Paragraph paragraphTable3 = new Paragraph();
    //                        paragraphTable3.SpacingAfter = 10f;
    //                        table = new PdfPTable(9);
    //                        float[] widths333 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
    //                        table.SetWidths(widths333);
    //                        table.TotalWidth = 560f;
    //                        table.LockedWidth = true;
    //                        table.DefaultCell.Border = Rectangle.NO_BORDER;
    //                        table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 12, Font.BOLD)));
    //                        table.AddCell(new Phrase(Math.Round(SumOfTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase(Math.Round(SumOfPayableTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase(Math.Round(SumOfbal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
    //                        table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                        table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
    //                        paragraphTable3.Add(table);
    //                        doc.Add(paragraphTable3);

    //                        Paragraph paragraphTable33 = new Paragraph();
    //                        // Add the remaining code for paragraphTable33 here
    //                        // For example:
    //                        // paragraphTable33.Add(new Phrase("Your text goes here", FontFactory.GetFont("Arial", 10, Font.NORMAL)));
    //                        // Add any additional content or formatting for paragraphTable33

    //                        // Add paragraphTable33 to the document
    //                        doc.Add(paragraphTable33);
    //                    }

    //                }
    //                if (flg == "Excel")
    //                {
    //                    doc.Close();
    //                    string pathd = Server.MapPath("~") + "/files/OutstandingReport.pdf";
    //                    string pathsave = Server.MapPath("~") + "/files/OutstandingReport.xlsx";

    //                    Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
    //                    //Load the PDF file
    //                    pdf.LoadFromFile(pathd);
    //                    //Save to Excel
    //                    pdf.SaveToFile(pathsave, FileFormat.XLSX);

    //                    System.IO.FileInfo file = new System.IO.FileInfo(pathsave);
    //                    string Outgoingfile = txtPartyName.Text + " OutstandingReport.xlsx";
    //                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
    //                    Response.AddHeader("Content-Length", file.Length.ToString());
    //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                    Response.WriteFile(file.FullName);
    //                }
    //            }
    //        }
    //    }
    //}


    public void GetOutstandingReports()
    {
        string strConnString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(strConnString))
        {
            //string storeProcedure = "SP_OutstandingRForRDLC";
            string storeProcedure = "SP_OutstandingRForRDLReports";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(storeProcedure, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", "SALE");
                cmd.Parameters.AddWithValue("@PartyName", txtPartyName.Text);
                if (txtfromdate.Text != "")
                {
                    //cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(txtfromdate.Text));
                    cmd.Parameters.AddWithValue("@FromDate", txtfromdate.Text);
                }
                if (txttodate.Text != "")
                {
                    //cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(txttodate.Text));
                    cmd.Parameters.AddWithValue("@ToDate", txttodate.Text);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ReportDataSource obj1 = new ReportDataSource("DataSet1", ds.Tables[0]);
                        //ReportDataSource obj2 = new ReportDataSource("DataSet1", ds.Tables[1]);
                        // ReportDataSource obj3 = new ReportDataSource("DataSet1", ds.Tables[2]);

                        ReportViewer1.LocalReport.DataSources.Add(obj1);
                        // ReportViewer1.LocalReport.DataSources.Add(obj2);
                        // ReportViewer1.LocalReport.DataSources.Add(obj3);

                        ReportViewer1.LocalReport.ReportPath = "RdlcReports\\Outstandingrdlc.rdlc";
                        ReportViewer1.LocalReport.Refresh();

                        //-------- Print PDF directly without showing ReportViewer ----
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        byte[] bytePdfRep = ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Buffer = true;

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=\"" + "Outstandingreports" + ".XLS"); // Give file name here

                        Response.BinaryWrite(bytePdfRep);

                        ReportViewer1.LocalReport.DataSources.Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Found...........!')", true);
                    }
                }
            }
        }
    }

    public void GetPDF(string flg)
    {

        DataTable Dt = new DataTable();
        SqlDataAdapter Da;
        if (txtPartyName.Text == "")
        {
            //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr", con);
            Da = new SqlDataAdapter("select distinct(BillingCustomer) from tblTaxInvoiceHdr", con);
        }
        else
        {
            //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
            Da = new SqlDataAdapter("select distinct(BillingCustomer) from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
        }

        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "OutstandingReport.pdf", FileMode.Create));
        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
        doc.Open();
        System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
        string imageURL = Server.MapPath("~") + "/img/ProcetechPDFLOGO.jpg";   
        iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);
        //Resize image depend upon your need
        png.ScaleToFit(70, 100);
        //For Image Position
        png.SetAbsolutePosition(40, 790);
        //var document = new Document();
        //Give space before image
        //png.ScaleToFit(document.PageSize.Width - (document.RightMargin * 100), 50);
        png.SpacingBefore = 50f;
        //Give some space after the image
        png.SpacingAfter = 1f;
        png.Alignment = Element.ALIGN_LEFT;
        doc.Add(png);
        if (Dt.Rows.Count > 0)
        {
            DataTable Dttt = new DataTable();
            if (txtPartyName.Text == "")
            {
                string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
                SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + BillingCustomer + "'", con);
                Daa.Fill(Dttt);

                //string ShippingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
                string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
                string Paid = Dttt.Rows[0]["ContactNo"].ToString();
            }
            else
            {
                SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (ShippingAddress),BillingCustomer,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
                Daa.Fill(Dttt);

                string BillingCustomer = Dttt.Rows[0]["BillingCustomer"].ToString() == "" ? "" : Dttt.Rows[0]["BillingCustomer"].ToString();
                //string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
                string ShippingAddress = Dttt.Rows[0]["ShippingAddress"].ToString();
                string Paid = Dttt.Rows[0]["ContactNo"].ToString();
            }


            PdfContentByte cd = writer.DirectContent;
            cd.Rectangle(150f, 800f, 400f, 25f);

            cd.Stroke();

            // Header 
            cd.BeginText();
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "OUTSTANDING REPORT", 280, 808, 0);
            cd.EndText();

            PdfPTable table = new PdfPTable(4);

            foreach (DataRow row in Dt.Rows)
            {
                DataTable Dtt = new DataTable();
                string fdate;
                string tdate;
                string ft = txtfromdate.Text;
                string tt = txttodate.Text;
                if (ft == "")
                {
                    fdate = "";
                }
                else
                {
                    fdate = ft;
                }

                if (tt == "")
                {
                    tdate = "";
                }
                else
                {
                    tdate = tt;
                }

                Dtt = GetData("SP_OutstandingRNew", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

                if (Dtt.Rows.Count > 0)
                {
                    string PartyName = Dtt.Rows[0]["BillingCustomer"].ToString();
                    string ShippingAdd = Dtt.Rows[0]["ShippingCustomer"].ToString();

                    con.Open();
                    SqlCommand cmddueDt = new SqlCommand("select paymentterm1 from Company where status='0' and cname='" + PartyName + "'", con);
                    string paymentterm = cmddueDt.ExecuteScalar().ToString();
                    con.Close();

                    int DueDays = 0;

                    if (paymentterm == "30 Days credit")
                    {
                        DueDays = 30;
                    }
                    else if (paymentterm == "60 Days credit" || paymentterm == "45-60 Days Credit")
                    {
                        DueDays = 60;
                    }
                    else if (paymentterm == "45 Days credit" || paymentterm == "30-45 Days Credit")
                    {
                        DueDays = 45;
                    }
                    else if (paymentterm == "90 Days credit")
                    {
                        DueDays = 90;
                    }
                    else if (paymentterm == "75 Days credit")
                    {
                        DueDays = 75;
                    }
                    else
                    {
                        DueDays = 0;
                    }

                    PdfPCell Cell = new PdfPCell();

                    Paragraph paragraphTable1 = new Paragraph();
                    paragraphTable1.SpacingAfter = 10f;
                    paragraphTable1.SpacingBefore = 14f;

                    table = new PdfPTable(1);
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    float[] widths1 = new float[] { 100 };
                    table.SetWidths(widths1);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;

                    Cell = new PdfPCell(new Phrase(PartyName + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
                    Cell.HorizontalAlignment = 1;
                    Cell.Border = PdfPCell.NO_BORDER;

                    table.AddCell(Cell);

                    Cell = new PdfPCell(new Phrase(ShippingAdd, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
                    Cell.HorizontalAlignment = 1;
                    Cell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(Cell);

                    paragraphTable1.Add(table);
                    doc.Add(paragraphTable1);

                    //Advance Table
                    Paragraph paragraphTable9 = new Paragraph();
                    paragraphTable9.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
                    table.SetWidths(widths33349);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
                    paragraphTable9.Add(table);
                    doc.Add(paragraphTable9);
                    ///////////////////


                    Paragraph paragraphTable2 = new Paragraph();
                    paragraphTable2.SpacingAfter = 0f;
                    table = new PdfPTable(9);
                    float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths33);
                    decimal SumOfTotal = 0;
                    decimal SumOfPayableTotal = 0;
                    decimal SumOfbal = 0;
                    if (Dtt.Rows.Count > 0)
                    {
                        table.TotalWidth = 560f;
                        table.LockedWidth = true;
                        //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                        table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

                        int rowid = 1;
                        decimal Balance = 0;
                        foreach (DataRow dr in Dtt.Rows)
                        {
                            //var Recevd = dr["Received"].ToString();
                            var payble = dr["Payable"].ToString();
                            con.Open();
                            //SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from  [ExcelEncLive].tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
                            SqlCommand cmdpaid = new SqlCommand("SELECT Max(CAST(Paid as float)) FROM [DB_ProcetechERP]. tblReceiptDtls where InvoiceNo='" + dr["InvoiceNo"].ToString() + "'", con);
                            string paidval = cmdpaid.ExecuteScalar().ToString();
                            
                            SqlCommand cmdRecived = new SqlCommand("SELECT ISNULL(SUM(CAST(Grandtotal as float)), 0) as Recived FROM [DB_ProcetechERP].tblCreditDebitNoteHdr WHERE BillNumber = '" + dr["InvoiceNo"].ToString() + "' AND NoteType = 'Credit_Sale'", con);


                            //SqlCommand cmdRecived = new SqlCommand("select (CAST(Grandtotal as float)) as Recived  from  [ExcelEncLive].tblCreditDebitNoteHdr where  BillNumber='" + dr["InvoiceNo"].ToString() + "'", con);
                            string Recived = cmdRecived.ExecuteScalar().ToString();
                            decimal Recivedamt;
                            if (Recived == "")
                            {
                                Recivedamt = 0;

                            }

                            else
                            {
                                Recivedamt = Convert.ToDecimal(Recived);
                            }
                            con.Close();
                            //var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval) +  Recivedamt;
                            var bal = Convert.ToDecimal(payble) - (Convert.ToDecimal(paidval == "" ? "0" : paidval) + Recivedamt);
                            Balance += bal;




                            //Bind DueDate from Payment Term
                            string str1 = "";
                            if (dr["Invoicedate"].ToString() != "")
                            {
                                var today = DateTime.Parse(dr["Invoicedate"].ToString(), new CultureInfo("en-GB", true));
                                DateTime DueDate = today.AddDays(DueDays);
                                str1 = DueDate.ToString();
                                str1 = str1.Replace("12:00:00 AM", "");
                                var time1 = str1;
                                DateTime Invoice = Convert.ToDateTime(str1);
                                str1 = Invoice.ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                str1 = "";
                            }

                            table.TotalWidth = 560f;
                            table.LockedWidth = true;
                            //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                            table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["InvoiceNo"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase((Convert.ToDecimal(paidval == "" ? "0" : paidval) + Recivedamt).ToString(), FontFactory.GetFont("Arial", 9)));
                            //table.AddCell(new Phrase(paidval == "" ? "0" : paidval, FontFactory.GetFont("Arial", 9)));
                            //table.AddCell(new Phrase(paidval =  paidval, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(bal).ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
                            rowid++;
                            SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
                            SumOfPayableTotal += Convert.ToDecimal(paidval == "" ? "0" : paidval);
                            SumOfbal += bal;
                        }
                    }
                    Dtt.Rows.Clear();
                    paragraphTable2.Add(table);
                    doc.Add(paragraphTable2);

                    con.Open();
                    SqlCommand cmdAdv = new SqlCommand("select SUM(CAST(Amount as float)) from tblReceiptHdr where Against='Advance' and Partyname='" + PartyName + "'", con);
                    string Advre = cmdAdv.ExecuteScalar().ToString();
                    con.Close();

                    Paragraph paragraphTable3 = new Paragraph();
                    paragraphTable2.SpacingAfter = 10f;
                    table = new PdfPTable(9);
                    float[] widths333 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths333);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfPayableTotal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfbal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable3.Add(table);
                    doc.Add(paragraphTable3);

                    //Advance Table
                    Paragraph paragraphTable33 = new Paragraph();
                    paragraphTable33.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths3334 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths3334);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Advance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Convert.ToDouble(Advre == "" ? "0" : Advre).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable33.Add(table);
                    doc.Add(paragraphTable33);
                    ///////////////////

                    var fBalnace = Convert.ToDecimal(SumOfbal) - Convert.ToDecimal(Advre == "" ? "0" : Advre);

                    //Total Balance table 
                    Paragraph paragraphTable334 = new Paragraph();
                    paragraphTable334.SpacingAfter = 2f;
                    table = new PdfPTable(9);
                    float[] widths33344 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    table.SetWidths(widths33344);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(fBalnace).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable334.Add(table);
                    doc.Add(paragraphTable334);
                    /////////////////////

                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    doc.Add(p);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
                    Show = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
                }
            }
            //ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
            //doc.Close();

            if (Show == false)
            {
                if (flg == "Excel")
                {
                    doc.Close();
                    string pathd = Server.MapPath("~") + "/files/OutstandingReport.pdf";
                    string pathsave = Server.MapPath("~") + "/files/OutstandingReport.xlsx";

                    Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                    //Load the PDF file
                    pdf.LoadFromFile(pathd);
                    //Save to Excel
                    pdf.SaveToFile(pathsave, FileFormat.XLSX);

                    System.IO.FileInfo file = new System.IO.FileInfo(pathsave);
                    string Outgoingfile = txtPartyName.Text + " OutstandingReport.xlsx";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.WriteFile(file.FullName);
                }
                else
                {
                    ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReport.pdf";
                    doc.Close();
                }
            }
        }
    }
}
































