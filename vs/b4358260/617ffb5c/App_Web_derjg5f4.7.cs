#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\PartyLedgerReport.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8558413D0875C4F74C84A6E20A7E14D962DB4CB0"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\PartyLedgerReport.aspx.cs"
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
using Spire.Pdf;

public partial class Admin_PartyLedgerReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DivRoot1.Visible = true;
            btn.Visible = true;
            bindOutstandingData();
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("PartyLedgerReport.aspx");
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
        if (ddltype.Text == "SALE")
        {
            AutoCompleteExtender1.Enabled = true;
            txtPartyName.Text = string.Empty;
            GetCustomerList(txtPartyName.Text, count);
            AutoCompleteExtender2.Enabled = false;

        }

        else if (ddltype.Text == "PURCHASE")
        {
            AutoCompleteExtender2.Enabled = true;
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {


    }

    protected void bindOutstandingData()
    {
        dgvOutstanding.DataSource = null;//GetData("SP_OutstandingR");
        dgvOutstanding.DataBind();
    }

    private static DataTable GetData(string SP)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
        Pdf("PDF");
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
        Pdf("Excel");
    }

    bool Show = false;
    protected void Pdf(string flg)
    {
        DataTable Dt = new DataTable();
        SqlDataAdapter Da;

        if (ddltype.Text == "SALE")
        {
            Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
        }
        else
        {
            Da = new SqlDataAdapter("SELECT distinct(a.SupplierName) as BillingCustomer,ShipToAddress as ShippingAddress,c.ContactNo as ContactNo FROM ((tblPurchaseBillHdr a INNER JOIN tblSupplierMaster b ON a.SupplierName=b.SupplierName) INNER JOIN tblSupplierContactDtls c ON b.Id = c.HeaderID) where a.SupplierName='" + txtPartyName.Text + "'", con);
        }
        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "PartyLedgerReport.pdf", FileMode.Create));
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
            string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
            string ShippingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
            string Paid = Dt.Rows[0]["ContactNo"].ToString();

            PdfContentByte cd = writer.DirectContent;
            cd.Rectangle(150f, 800f, 400f, 25f);
            cd.Stroke();

            // Header 
            cd.BeginText();
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PARTY TRANSACTION LEDGER", 280, 808, 0);
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
					 DateTime ftdate = Convert.ToDateTime(ft, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                     fdate = ftdate.ToString("yyyy-MM-dd");
					
                    //var fttime = Convert.ToDateTime(ft);
                    //fdate = fttime.ToString("yyyy-MM-dd");
                }

                if (tt == "")
                {
                    tdate = "";
                }
                else
                {

                     DateTime date = Convert.ToDateTime(tt, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                     tdate = date.ToString("yyyy-MM-dd");
                    //var tttime = Convert.ToDateTime(tt);
                    //tdate = tttime.ToString("yyyy-MM-dd");
                }

                Dtt = GetData("SP_PartyLedgerR", row["BillingCustomer"].ToString(), fdate, tdate, ddltype.Text);

                if (Dtt.Rows.Count > 0)
                {
                    Show = true;
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

                    Cell = new PdfPCell(new Phrase(BillingCustomer + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
                    Cell.HorizontalAlignment = 1;
                    Cell.Border = PdfPCell.NO_BORDER;

                    table.AddCell(Cell);

                    Cell = new PdfPCell(new Phrase(ShippingAddress, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
                    Cell.HorizontalAlignment = 1;
                    Cell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(Cell);

                    paragraphTable1.Add(table);
                    doc.Add(paragraphTable1);

                    Paragraph paragraphTable2 = new Paragraph();
                    paragraphTable2.SpacingAfter = 0f;
                    table = new PdfPTable(7);
                    float[] widths33 = new float[] { 13f, 12f, 11f, 15f, 12f, 12f, 12f };
                    table.SetWidths(widths33);
                    decimal SumOfDebit = 0;
                    decimal SumOfCredit = 0;
                    decimal SumOfbal = 0;
                    if (Dtt.Rows.Count > 0)
                    {
                        table.TotalWidth = 560f;
                        table.LockedWidth = true;

                        if (ddltype.Text == "SALE")
                        {
                            table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Particulars", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Debit", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Credit", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        }
                        else
                        {
                            table.AddCell(new Phrase("Type", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Doc No", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Particulars", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Credit", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Debit", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                            table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        }
                        //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;

                        int rowid = 1;
                        decimal Balance = 0;
                        foreach (DataRow dr in Dtt.Rows)
                        {
                            if (dr["Type"].ToString() == "Sale Invoice")
                            {
                                if (dr["Type"].ToString() == "Sale Invoice")
                                {
                                    var bal = Convert.ToDecimal(dr["Debit"].ToString());
                                    Balance += bal;
                                }
                                else
                                {
                                    var bal = Convert.ToDecimal(dr["Credit"].ToString());
                                    Balance -= bal;
                                }
                            }
                            else
                            {
                                if (dr["Type"].ToString() == "Purchase Invoice")
                                {
                                    var bal = Convert.ToDecimal(dr["Debit"].ToString());
                                    Balance += bal;
                                }
                                else
                                {
                                    var bal = Convert.ToDecimal(dr["Credit"].ToString());
                                    Balance -= bal;
                                }
                            }

                            string str1 = dr["CDate"].ToString();
                            str1 = str1.Replace("12:00:00 AM", "");
                            var time1 = str1;
                            DateTime Invoice = Convert.ToDateTime(str1);
                            str1 = Invoice.ToString("dd-MM-yyyy");


                            table.TotalWidth = 560f;
                            table.LockedWidth = true;
                            //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                            table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["DocNo"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(str1, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Particulars"].ToString() + " " + dr["chekNo"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Debit"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["Credit"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
                            rowid++;
                            SumOfDebit += Convert.ToDecimal(dr["Debit"].ToString() == "" ? "0" : dr["Debit"].ToString());
                            SumOfCredit += Convert.ToDecimal(dr["Credit"].ToString() == "" ? "0" : dr["Credit"].ToString());
                            SumOfbal = Balance;
                        }
                    }
                    Dtt.Rows.Clear();
                    paragraphTable2.Add(table);
                    doc.Add(paragraphTable2);

                    Paragraph paragraphTable3 = new Paragraph();
                    paragraphTable2.SpacingAfter = 10f;
                    table = new PdfPTable(7);
                    float[] widths333 = new float[] { 13f, 12f, 11f, 15f, 12f, 12f, 12f };
                    table.SetWidths(widths333);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 11, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfDebit).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfCredit).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase(Math.Round(SumOfbal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    paragraphTable3.Add(table);
                    doc.Add(paragraphTable3);

                    //Total Receivable Table
                    Paragraph paragraphTable33 = new Paragraph();
                    paragraphTable33.SpacingAfter = 2f;
                    table = new PdfPTable(7);
                    float[] widths3334 = new float[] { 13f, 12f, 11f, 15f, 12f, 12f, 12f };
                    table.SetWidths(widths3334);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    if (ddltype.Text == "SALE")
                    {
                        table.AddCell(new Phrase("Total Receivable", FontFactory.GetFont("Arial", 11, Font.BOLD)));
                        table.AddCell(new Phrase(Math.Round(SumOfbal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    }
                    else
                    {
                        table.AddCell(new Phrase("Total Payable", FontFactory.GetFont("Arial", 11, Font.BOLD)));
                        table.AddCell(new Phrase(Math.Round(SumOfbal).ToString("N2", info), FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    }
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    paragraphTable33.Add(table);
                    doc.Add(paragraphTable33);
                    /////////////////

                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    doc.Add(p);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found !!!');", true);
                    Show = false;
                }
            }
        }
        if (Show == true)
        {
            if (flg == "Excel")
            {
                doc.Close();
                string pathd = Server.MapPath("~") + "/files/PartyLedgerReport.pdf";
                string pathsave = Server.MapPath("~") + "/files/PartyLedgerReport.xlsx";

                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                //Load the PDF file
                pdf.LoadFromFile(pathd);
                //Save to Excel
                pdf.SaveToFile(pathsave, FileFormat.XLSX);

                System.IO.FileInfo file = new System.IO.FileInfo(pathsave);
                string Outgoingfile = txtPartyName.Text + " PartyLedgerReport.xlsx";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.WriteFile(file.FullName);
            }
            else
            {

                ifrRight6.Attributes["src"] = @"../files/" + "PartyLedgerReport.pdf";
                doc.Close();
            }
        }


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
                        return dt;
                    }
                }
            }
        }
    }
}


#line default
#line hidden
