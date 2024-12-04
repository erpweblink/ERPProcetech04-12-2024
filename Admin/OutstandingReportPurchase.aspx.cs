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
using iTextSharp.text.pdf.parser;

public partial class OutstandingReportPurchase : System.Web.UI.Page
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
                DivRoot1.Visible = true;
                btn.Visible = true;
                bindOutstandingData();
            }
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("OutstandingReportPurchase.aspx");
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
    {

        return AutoFillCustomerlist(prefixText);
    }

    public static List<string> AutoFillCustomerlist(string prefixText)
    {
        try
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "SELECT DISTINCT(tblPurchaseOrderHdr.SupplierName) FROM tblSupplierMaster INNER JOIN tblPurchaseOrderHdr ON tblSupplierMaster.SupplierName = tblPurchaseOrderHdr.SupplierName WHERE tblPurchaseOrderHdr.SupplierName LIKE @Search + '%'";
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
        catch (Exception ex)
        {
            throw ex;
        }
    }
    int count = 0;

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
                cmd.Parameters.AddWithValue("@Type", "PURCHASE");
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

        Pdf();
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
        DataTable dt = new DataTable("GridView_Data");
        GridView gvOrders = (GridView)dgvOutstanding.Rows[5].FindControl("dgvOutstandingDetails");
        foreach (TableCell cell in dgvOutstanding.HeaderRow.Cells)
        {
            dt.Columns.Add(cell.Text);
        }
        foreach (TableCell cell in gvOrders.HeaderRow.Cells)
        {
            dt.Columns.Add(cell.Text);
        }
        dt.Columns.RemoveAt(0);
        foreach (GridViewRow row in dgvOutstanding.Rows)
        {
            GridView gvOrderscell = (row.FindControl("dgvOutstandingDetails") as GridView);
            for (int j = 0; j < gvOrderscell.Rows.Count; j++)
            {
                dt.Rows.Add(row.Cells[1].Text, row.Cells[2].Text, row.Cells[3].Text, gvOrderscell.Rows[j].Cells[0].Text, gvOrderscell.Rows[j].Cells[1].Text, gvOrderscell.Rows[j].Cells[2].Text, gvOrderscell.Rows[j].Cells[3].Text, gvOrderscell.Rows[j].Cells[4].Text, gvOrderscell.Rows[j].Cells[5].Text, gvOrderscell.Rows[j].Cells[6].Text, gvOrderscell.Rows[j].Cells[7].Text);
            }
        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
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
            //Da = new SqlDataAdapter("select distinct(SupplierName) from tblPurchaseOrderHdr", con);
            Da = new SqlDataAdapter("	SELECT DISTINCT(tblPurchaseBillHdr.SupplierName) FROM tblSupplierMaster INNER JOIN tblPurchaseBillHdr ON tblSupplierMaster.SupplierName = tblPurchaseBillHdr.SupplierName", con);
        }
        else
        {
            //Da = new SqlDataAdapter("select distinct(BillingCustomer),ShippingAddress,ContactNo from tblTaxInvoiceHdr where BillingCustomer='" + txtPartyName.Text + "'", con);
            Da = new SqlDataAdapter("	SELECT DISTINCT(tblPurchaseBillHdr.SupplierName) FROM tblSupplierMaster INNER JOIN tblPurchaseBillHdr ON tblSupplierMaster.SupplierName = tblPurchaseBillHdr.SupplierName WHERE tblPurchaseBillHdr.SupplierName='" + txtPartyName.Text + "'", con);
        }

        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "OutstandingReportPurchase.pdf", FileMode.Create));
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
                string BillingCustomer = Dt.Rows[0]["SupplierName"].ToString();
                //SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (Mode),SupplierName from tblPurchaseOrderHdr where SupplierName='" + BillingCustomer + "'", con);
                SqlDataAdapter Daa = new SqlDataAdapter("select BillNo,SupplierName from tblPurchaseBillHdr where SupplierName='" + BillingCustomer + "'", con);
                Daa.Fill(Dttt);

                //string ShippingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
                //string ShippingAddress = Dttt.Rows[0]["Mode"].ToString();
                string ShippingAddress = Dttt.Rows[0]["BillNo"].ToString();
                //string Paid = Dttt.Rows[0]["ContactNo"].ToString();
            }
            else
            {
                //SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (Mode),SupplierName from tblPurchaseOrderHdr where SupplierName='" + txtPartyName.Text + "'", con);
                SqlDataAdapter Daa = new SqlDataAdapter("select BillNo,SupplierName from tblPurchaseBillHdr where SupplierName='" + txtPartyName.Text + "'", con);
                Daa.Fill(Dttt);

                string BillingCustomer = Dttt.Rows[0]["SupplierName"].ToString() == "" ? "" : Dttt.Rows[0]["SupplierName"].ToString();
                //string BillingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
                //string ShippingAddress = Dttt.Rows[0]["Mode"].ToString();
                string ShippingAddress = Dttt.Rows[0]["BillNo"].ToString();
                //string Paid = Dttt.Rows[0]["ContactNo"].ToString();
            }
            ////////////////


            // SqlDataAdapter Daa = new SqlDataAdapter("select TOP 1 (Mode),SupplierName from tblPurchaseOrderHdr where SupplierName='" + txtPartyName.Text + "'", con);
            // Daa.Fill(Dttt);

            // string BillingCustomer = Dttt.Rows[0]["SupplierName"].ToString() == "" ? "" : Dttt.Rows[0]["SupplierName"].ToString();
            // string ShippingAddress = Dttt.Rows[0]["Mode"].ToString();
            //// string Paid = Dttt.Rows[0]["ContactNo"].ToString();

            PdfContentByte cd = writer.DirectContent;
            cd.Rectangle(150f, 800f, 400f, 25f);
            cd.Stroke();

            // Header 
            cd.BeginText();
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "OUTSTANDING REPORT PURCHASE", 240, 808, 0);
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

                Dtt = GetData("SP_OutstandingPurchase", row["SupplierName"].ToString(), fdate, tdate, ddltype.Text);

                if (Dtt.Rows.Count > 0)
                {
                    string PartyName = Dtt.Rows[0]["SupplierName"].ToString();
                    //string ShippingAdd = Dtt.Rows[0]["BillNo"].ToString();
                    //string ShippingAdd = Dtt.Rows[0]["Mode"].ToString();

                    con.Open();
                    SqlCommand cmddueDt = new SqlCommand("select PaymentTerm from tblSupplierMaster where IsActive='1' and SupplierName='" + PartyName + "'", con);
                    //SqlCommand cmddueDt = new SqlCommand("select Top 1 (PaymentTerm) from tblpurchaseorderhdr where SupplierName='" + PartyName + "'", con);
                    //string paymentterm = cmddueDt.ExecuteScalar().ToString();
                    string paymentterm = cmddueDt.ExecuteScalar().ToString() == "" ? "0" : cmddueDt.ExecuteScalar().ToString();

                    SqlCommand cmdBillAddress = new SqlCommand("select BillToAddress from tblSupplierMaster where IsActive='1' and SupplierName='" + PartyName + "'", con);
                    string ShippingAdd = cmdBillAddress.ExecuteScalar().ToString();

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
                    table = new PdfPTable(8);
                    //float[] widths33349 = new float[] { 15f, 12f, 11f, 11f, 8f, 8f, 5f, 13f, 20f };
                    float[] widths33349 = new float[] { 15f, 12f, 11f, 8f, 8f, 5f, 13f, 20f };
                    table.SetWidths(widths33349);
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    //table.AddCell(new Phrase("Payment Validity:", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    //table.AddCell(new Phrase(DueDays.ToString() + " Days", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 12, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                    //table.AddCell(new Phrase("Payment Term:", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                    //table.AddCell(new Phrase(paymentterm, FontFactory.GetFont("Arial", 9)));
                    paragraphTable9.Add(table);
                    doc.Add(paragraphTable9);
                    ///////////////////


                    Paragraph paragraphTable2 = new Paragraph();
                    paragraphTable2.SpacingAfter = 0f;
                    table = new PdfPTable(8);
                    //float[] widths33 = new float[] { 13f, 12f, 11f, 11f, 10f, 10f, 12f, 10f, 8f };
                    float[] widths33 = new float[] { 13f, 12f, 11f, 10f, 10f, 12f, 10f, 8f };
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
                        //table.AddCell(new Phrase("Due", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Payable", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        //table.AddCell(new Phrase("Received", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Paid", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Balance", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Cum. Bal", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                        table.AddCell(new Phrase("Days", FontFactory.GetFont("Arial", 10, Font.BOLD)));

                        int rowid = 1;
                        decimal Balance = 0;
                        foreach (DataRow dr in Dtt.Rows)
                        {
                            //var Recevd = dr["Received"].ToString();
                            var payble = dr["Payable"].ToString() == "" ? "0" : dr["Payable"].ToString();
                            con.Open();
                            //string PONO = dr["PONo"].ToString();
                            string PONO = dr["SupplierBillNo"].ToString();
                            SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) as Paid from tblPaymentDtls where BillNo='" + dr["SupplierBillNo"].ToString() + "'", con);
                            // SqlCommand cmdpaid = new SqlCommand("select SUM(CAST(Paid as float)) from tblPaymentHdrs where BillNo ='" + dr["PONo"].ToString() + "'", con);
                            string paidval = cmdpaid.ExecuteScalar().ToString() == "" ? "0" : cmdpaid.ExecuteScalar().ToString();
                            con.Close();
                            var bal = Convert.ToDecimal(payble) - Convert.ToDecimal(paidval == "" ? "0" : paidval);
                            Balance += bal;

                            //Bind DueDate from Payment Term
                            //string str1 = "";
                            //if (dr["PODate"].ToString() != "")
                            //{
                            //    var today = DateTime.Parse(dr["PODate"].ToString(), new CultureInfo("en-GB", true));
                            //    DateTime DueDate = today.AddDays(DueDays);
                            //    str1 = DueDate.ToString();
                            //    str1 = str1.Replace("12:00:00 AM", "");
                            //    var time1 = str1;
                            //    DateTime Invoice = Convert.ToDateTime(str1);
                            //    str1 = Invoice.ToString("dd-MM-yyyy");
                            //}
                            //else
                            //{
                            //    str1 = "";
                            //}

                            table.TotalWidth = 560f;
                            table.LockedWidth = true;
                            //table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                            table.AddCell(new Phrase(dr["Type"].ToString(), FontFactory.GetFont("Arial", 9)));
                            //table.AddCell(new Phrase(dr["PONo"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["SupplierBillNo"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["BillDate"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
                            //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                            //table.AddCell(new Phrase(dr["Payable"].ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(payble, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(paidval == "" ? "0" : paidval, FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(bal).ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(Math.Round(Balance).ToString(), FontFactory.GetFont("Arial", 9)));
                            table.AddCell(new Phrase(dr["days"].ToString(), FontFactory.GetFont("Arial", 9)));
                            rowid++;
                            //SumOfTotal += Convert.ToDecimal(dr["Payable"].ToString());
                            SumOfTotal += Convert.ToDecimal(payble);
                            SumOfPayableTotal += Convert.ToDecimal(paidval == "" ? "0" : paidval);
                            SumOfbal += bal;
                        }
                    }
                    Dtt.Rows.Clear();
                    paragraphTable2.Add(table);
                    doc.Add(paragraphTable2);

                    con.Open();
                    //SqlCommand cmdAdv = new SqlCommand("select SUM(CAST(Amount as float)) from tblPaymentHdrs where Against='Advance' and Partyname='" + PartyName + "'", con);
                    SqlCommand cmdAdv = new SqlCommand("select SUM(CAST(Amount as float)) from tblPaymentHdrs where Against='Advance' and Partyname='" + PartyName + "'", con);
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
            ifrRight6.Attributes["src"] = @"../files/" + "OutstandingReportPurchase.pdf";
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