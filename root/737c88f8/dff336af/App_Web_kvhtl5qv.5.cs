#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Reports\SalesQuotationRptPDF.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D96EC537BCF5226C25822DCA2A8DA098AD4CFAE5"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Reports\SalesQuotationRptPDF.aspx.cs"

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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class Admin_SalesQuotationRptPDF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            GetQuotationData();
        }
    }

    protected void GetQuotationData()
    {
        try
        {
            string quotationid = Request.QueryString["ID"].ToString();
			ViewState["QuotationNo"] = quotationid;
            titlename.Text = quotationid + "Sales Quatation Report";

            SqlCommand cmdexsit = new SqlCommand("select IsRevise from QuotationMain where quotationno='" + quotationid + "'", con);
            con.Open();
            string Isrevised = cmdexsit.ExecuteScalar().ToString();
            con.Close();

            SqlDataAdapter ad;

            if (Isrevised == "True")
            {
                ad = new SqlDataAdapter(@"SELECT TQ.description, TQ.hsncode, TQ.qty, TQ.rate,TQ.CGST
              ,TQ.SGST,TQ.IGST,TQ.CGSTamt,TQ.SGSTamt,TQ.IGSTamt,TQ.totaltax, TQ.discount, TQ.amount,
              QuotationMain.quotationno, QuotationMain.partyname, QuotationMain.ccode,QuotationMain.kindatt, QuotationMain.address,
              QuotationMain.paymentterm1,QuotationMain.paymentterm2,QuotationMain.paymentterm3,QuotationMain.paymentterm4,QuotationMain.paymentterm5,
        QuotationMain.validityofoffer,QuotationMain.deliveryperiod,QuotationMain.specialpackaging,QuotationMain.standardpackaging,QuotationMain.inspection,QuotationMain.transportation,
              Format(QuotationMain.date,'dd-MM-yyyy')as date, QuotationMain.remark, QuotationMain.width, QuotationMain.Toatlamt, QuotationMain.depth, 
              QuotationMain.height, QuotationMain.base, QuotationMain.canopy, QuotationMain.material,QuotationMain.specifymaterial, QuotationMain.Constructiontype,
              QuotationMain.descriptionall, QuotationMain.sessionname, QuotationMain.createddate, QuotationMain.Taxation,QuotationMain.Currency	
              FROM TempQuotationData TQ INNER JOIN QuotationMain ON TQ.quotationno = QuotationMain.quotationno where QuotationMain.quotationno='" + quotationid + "'", con);
            }
            else
            {
                ad = new SqlDataAdapter(@"SELECT QuotationData.description, QuotationData.hsncode, QuotationData.qty, QuotationData.rate,QuotationData.CGST
              ,QuotationData.SGST,QuotationData.IGST,QuotationData.CGSTamt,QuotationData.SGSTamt,QuotationData.IGSTamt,QuotationData.totaltax, QuotationData.discount, QuotationData.amount,
              QuotationMain.quotationno, QuotationMain.partyname, QuotationMain.ccode,QuotationMain.kindatt, QuotationMain.address,
              QuotationMain.paymentterm1,QuotationMain.paymentterm2,QuotationMain.paymentterm3,QuotationMain.paymentterm4,QuotationMain.paymentterm5,
        QuotationMain.validityofoffer,QuotationMain.deliveryperiod,QuotationMain.specialpackaging,QuotationMain.standardpackaging,QuotationMain.inspection,QuotationMain.transportation,
              Format(QuotationMain.date,'dd-MM-yyyy')as date, QuotationMain.remark, QuotationMain.width, QuotationMain.Toatlamt, QuotationMain.depth, 
              QuotationMain.height, QuotationMain.base, QuotationMain.canopy, QuotationMain.material,QuotationMain.specifymaterial, QuotationMain.Constructiontype,
              QuotationMain.descriptionall, QuotationMain.sessionname, QuotationMain.createddate, QuotationMain.Taxation,QuotationMain.Currency
              FROM QuotationData INNER JOIN QuotationMain ON QuotationData.quotationno = QuotationMain.quotationno where QuotationMain.quotationno='" + quotationid + "'", con);
            }
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //Bind dgv
                dgvSalesQuatationRpt.DataSource = dt;
                dgvSalesQuatationRpt.DataBind();


                //Bind Lable
                lblPartyName.Text = dt.Rows[0]["partyname"].ToString();
                lblQuotationNo.Text = dt.Rows[0]["quotationno"].ToString();
                lblKindatt.Text = dt.Rows[0]["kindatt"].ToString();
                lblQuotationDate.Text = dt.Rows[0]["date"].ToString();
                lblAddress.Text = dt.Rows[0]["address"].ToString();
                lblRemark.Text = dt.Rows[0]["remark"].ToString();
                lblPaymentTerm.Text = dt.Rows[0]["paymentterm1"].ToString() == "Specify" ? dt.Rows[0]["paymentterm2"].ToString() : dt.Rows[0]["paymentterm1"].ToString();

                if (dt.Rows[0]["Taxation"].ToString() == "outmah")
                {
                    lblCGST.Text = "Not Applicable";
                    lblSGST.Text = "Not Applicable";
                    lblIGST.Text = dt.Rows[0]["IGST"].ToString() == "" ? "Not Applicable" : "Extra as applicable (Presently 18 %)";
                }
                if (dt.Rows[0]["Taxation"].ToString() == "inmah")
                {
                    lblCGST.Text = "Extra as applicable (Presently 9 %)";
                    lblSGST.Text = "Extra as applicable (Presently 9 %)";
                    lblIGST.Text = "Not Applicable";
                }
                if (dt.Rows[0]["Taxation"].ToString() == "outind")
                {
                    lblCGST.Text = "Not Applicable";
                    lblSGST.Text = "Not Applicable";
                    lblIGST.Text = "Not Applicable";
                }

                lblValidityOfOffer.Text = dt.Rows[0]["validityofoffer"].ToString();
                lblDeliveryPeriod.Text = dt.Rows[0]["deliveryperiod"].ToString();
                lblTransportation.Text = dt.Rows[0]["transportation"].ToString();
                lblStandardpacking.Text = dt.Rows[0]["standardpackaging"].ToString();
                lblSpecialpacking.Text = dt.Rows[0]["specialpackaging"].ToString();
                lblInspection.Text = dt.Rows[0]["inspection"].ToString();
                lblNote.Text = "Price may change if any addition is done in the process of Drawing Approval.";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    decimal amount = 0;
    int Qty = 0;
    protected void OnDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            amount += Convert.ToDecimal((e.Row.FindControl("txtAmount") as Label).Text);
            Qty += Convert.ToInt32((e.Row.FindControl("lblQty") as Label).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblAmount") as Label).Text = dt.Rows[0]["Currency"].ToString() + " " + amount.ToString();

            var cgstamt = amount * 9 / 100;

            if (dt.Rows[0]["Taxation"].ToString() == "outmah")
            {
                lblCGSTamt.Text = "0.00";
                lblSGSTamt.Text = "0.00";
                var tot = cgstamt + cgstamt + amount;
                lblWithGSTAmount.InnerText = dt.Rows[0]["Currency"].ToString() + " " + tot.ToString();
            }
            if (dt.Rows[0]["Taxation"].ToString() == "inmah")
            {
                lblCGSTamt.Text = dt.Rows[0]["Currency"].ToString() + " " + cgstamt.ToString("##.##");
                lblSGSTamt.Text = dt.Rows[0]["Currency"].ToString() + " " + cgstamt.ToString("##.##");
                var tot = cgstamt + cgstamt + amount;
                lblWithGSTAmount.InnerText = dt.Rows[0]["Currency"].ToString() + " " + tot.ToString();
            }
            if (dt.Rows[0]["Taxation"].ToString() == "outind")
            {
                lblCGSTamt.Text = "0.00";
                lblSGSTamt.Text = "0.00";
                var tot = amount;
                lblWithGSTAmount.InnerText = dt.Rows[0]["Currency"].ToString() + " " + tot.ToString();
            }

            (e.Row.FindControl("lblRate") as Label).Text = Qty.ToString();
        }
    }

	private void Pdf()
    {
        DataTable Dt = new DataTable();
        SqlDataAdapter Da = new SqlDataAdapter("SELECT QuotationMain.id,IsRevise,TQ.description, TQ.hsncode, TQ.qty, TQ.rate,TQ.CGST, TQ.SGST, TQ.IGST, TQ.CGSTamt, TQ.SGSTamt, TQ.IGSTamt, TQ.totaltax, TQ.discount, TQ.amount, QuotationMain.quotationno, QuotationMain.partyname," +
            " QuotationMain.ccode, QuotationMain.kindatt, QuotationMain.address, QuotationMain.paymentterm1, QuotationMain.paymentterm2, " +
            "QuotationMain.paymentterm3, QuotationMain.paymentterm4, QuotationMain.paymentterm5,  QuotationMain.validityofoffer, QuotationMain.deliveryperiod, QuotationMain.specialpackaging," +
            " QuotationMain.standardpackaging, QuotationMain.inspection, QuotationMain.transportation, Format(QuotationMain.date, 'dd-MM-yyyy') as date, QuotationMain.remark, QuotationMain.width, QuotationMain.Toatlamt," +
            " QuotationMain.depth,QuotationMain.height, QuotationMain.base, QuotationMain.canopy, QuotationMain.material, QuotationMain.specifymaterial, QuotationMain.Constructiontype, " +
            " QuotationMain.descriptionall, QuotationMain.sessionname, QuotationMain.createddate, QuotationMain." +
            "Taxation,Currency FROM QuotationData TQ INNER JOIN QuotationMain ON TQ.quotationno = QuotationMain.quotationno where QuotationMain.quotationno = '" + ViewState["QuotationNo"].ToString() + "'", con);

        Da.Fill(Dt);
        //GvQuotation.DataSource = Dt;
        //GvQuotation.DataBind();

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);

        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/Files/") + "SalesQuatition.pdf", FileMode.Create));
        //PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);

        doc.Open();

        string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";
		 string imageStamp = Server.MapPath("~") + "/img/AdminSign.png";
		
        iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);

        //Resize image depend upon your need

        png.ScaleToFit(70, 100);

        //For Image Position
        png.SetAbsolutePosition(40, 718);
        //var document = new Document();

        //Give space before image
        //png.ScaleToFit(document.PageSize.Width - (document.RightMargin * 100), 50);
        png.SpacingBefore = 50f;

        //Give some space after the image

        png.SpacingAfter = 1f;

        png.Alignment = Element.ALIGN_LEFT;

        //paragraphimage.Add(png);
        //doc.Add(paragraphimage);
        doc.Add(png);


        PdfContentByte cb = writer.DirectContent;
        cb.Rectangle(17f, 710f, 560f, 60f);
        cb.Stroke();
        // Header 
        cb.BeginText();
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 20);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Excel Enclosures", 250, 745, 0);
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Gat No. 1567, Shelar Vasti, Dehu-Alandi Road, Chikhali, Pune - 411062", 145, 728, 0);
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
        cb.EndText();

        //PdfContentByte cbb = writer.DirectContent;
        //cbb.Rectangle(17f, 710f, 560f, 25f);
        //cbb.Stroke();
        //// Header 
        //cbb.BeginText();
        //cbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        //cbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " CONTACT : 9225658662   Email ID : mktg@excelenclosures.com", 153, 722, 0);
        //cbb.EndText();

        PdfContentByte cbbb = writer.DirectContent;
        cbbb.Rectangle(17f, 685f, 560f, 25f);
        cbbb.Stroke();
        // Header 
        cbbb.BeginText();
        cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "GSTIN : 27ATFPS1959J1Z4", 30, 695, 0);
        cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PAN NO: ATFPS1959J", 160, 695, 0);
        cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "EMAIL : mktg@excelenclosures.com", 270, 695, 0);
        cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CONTACT : 9225658662", 440, 695, 0);
        cbbb.EndText();

        PdfContentByte cd = writer.DirectContent;
        cd.Rectangle(17f, 660f, 560f, 25f);
        cd.Stroke();
        // Header 
        cd.BeginText();
        cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 17);
        cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Sales Quotation", 260, 667, 0);
        cd.EndText();

        if (Dt.Rows.Count > 0)
        {
            var CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
            string InvoiceNo = Dt.Rows[0]["ccode"].ToString();
            string KindAtt = Dt.Rows[0]["kindatt"].ToString();
            string Address = Dt.Rows[0]["address"].ToString();
            string QuatationDate = Dt.Rows[0]["date"].ToString().TrimEnd("0:0".ToCharArray());
             string paymentterm2 = Dt.Rows[0]["paymentterm2"].ToString();
            string paymentterm = Dt.Rows[0]["paymentterm1"].ToString() == "Specify" ? paymentterm2 : Dt.Rows[0]["paymentterm1"].ToString();
            string TotalInWord = Dt.Rows[0]["paymentterm2"].ToString();
            string GrandTotal = Dt.Rows[0]["paymentterm2"].ToString();
            string CGST = Dt.Rows[0]["CGST"].ToString();
            string SGST = Dt.Rows[0]["SGST"].ToString();
            string Total = Dt.Rows[0]["amount"].ToString();
            string QuatationNumber = Dt.Rows[0]["quotationno"].ToString();
            string PartyName = Dt.Rows[0]["partyname"].ToString();
            string Taxation = Dt.Rows[0]["Taxation"].ToString();
			string Remarkd = Dt.Rows[0]["remark"].ToString();
			 string validityofoffer = Dt.Rows[0]["validityofoffer"].ToString();
			  string deliveryperiod = Dt.Rows[0]["deliveryperiod"].ToString();
            string transportation = Dt.Rows[0]["transportation"].ToString();
			string Currency = Dt.Rows[0]["Currency"].ToString();
			
            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingBefore = 120f;
            paragraphTable1.SpacingAfter = 10f;

            PdfPTable table = new PdfPTable(4);

            float[] widths2 = new float[] { 100, 180, 100, 180 };
            table.SetWidths(widths2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            var date = DateTime.Now.ToString("yyyy-MM-dd");


            table.AddCell(new Phrase("Party Name : ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(PartyName, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Quotation Number :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(QuatationNumber, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Address", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(Address, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Quotation Date :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(QuatationDate, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Kind Att :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(KindAtt, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase(" ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(" ", FontFactory.GetFont("Arial", 9, Font.BOLD)));

            //table.AddCell(new Phrase("Created Date :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //table.AddCell(new Phrase(CreateDate, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            //table.AddCell(new Phrase("Kind Attn. :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //table.AddCell(new Phrase(KindAtt, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            paragraphTable1.Add(table);
            doc.Add(paragraphTable1);

            Paragraph paragraphTable2 = new Paragraph();
            paragraphTable2.SpacingAfter = 0f;
            table = new PdfPTable(8);
            float[] widths3 = new float[] { 4f, 40f, 8f, 8f, 0f, 8f, 8f, 10f };
            table.SetWidths(widths3);

            double Ttotal_price = 0;
            if (Dt.Rows.Count > 0)
            {
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Name Of Particulars", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Hsn/Sac", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("Tax %", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Quantity", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Unit", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Rate", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Disc %", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));

                int rowid = 1;
                foreach (DataRow dr in Dt.Rows)
                {
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;

                    double Ftotal = Convert.ToDouble(dr["amount"].ToString());
                    string _ftotal = Ftotal.ToString("##.00");
                    table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["description"].ToString().Replace("<br>", "\n"), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["hsncode"].ToString(), FontFactory.GetFont("Arial", 9)));
                    //table.AddCell(new Phrase(dr["totaltax"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["qty"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["hsncode"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["Currency"].ToString() + " " + dr["rate"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["discount"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["Currency"].ToString() + " " + _ftotal, FontFactory.GetFont("Arial", 9)));
                    rowid++;

                    Ttotal_price += Convert.ToDouble(dr["amount"].ToString());
                }

            }
            string amount = Ttotal_price.ToString();
            paragraphTable2.Add(table);
            doc.Add(paragraphTable2);

            //GST Calculation
            double GSTamt = Convert.ToDouble(amount) * 9 / 100;

            string lblCGSTamt = "";
            string lblSGSTamt = "";
            string lblIGSTamt = "";
            string lblWithGSTAmount = "";

            if (Dt.Rows[0]["Taxation"].ToString() == "outmah")
            {
                lblCGSTamt = "Not Applicable";
                lblSGSTamt = "Not Applicable";

                double Igtamt = GSTamt + GSTamt;

                lblIGSTamt = "Extra as applicable (Presently 18 %)I Amount" + Dt.Rows[0]["Currency"].ToString() + " " + Igtamt.ToString("##.##") + "";
                var tot = Convert.ToDouble(GSTamt) + Convert.ToDouble(GSTamt) + Convert.ToDouble(amount);
                lblWithGSTAmount = tot.ToString();
            }
            if (Dt.Rows[0]["Taxation"].ToString() == "inmah")
            {
                lblCGSTamt = "Extra as applicable (Presently 9 %)I Amount " + Dt.Rows[0]["Currency"].ToString() + " " + GSTamt.ToString("##.##") + "";
                lblSGSTamt = "Extra as applicable (Presently 9 %)I Amount " + Dt.Rows[0]["Currency"].ToString() + " " + GSTamt.ToString("##.##") + "";
                lblIGSTamt = "Not Applicable";
                var tot = Convert.ToDouble(GSTamt) + Convert.ToDouble(GSTamt) + Convert.ToDouble(amount);
                lblWithGSTAmount = tot.ToString();
            }
            if (Dt.Rows[0]["Taxation"].ToString() == "outind")
            {
                lblCGSTamt = "0.00";
                lblSGSTamt = "0.00";
				lblIGSTamt = "Not Applicable";
                var tot = amount;
                lblWithGSTAmount = tot.ToString();
            }


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

            table = new PdfPTable(8);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 4f, 40f, 8f, 8f, 0f, 8f, 8f, 10f });
            table.AddCell(paragraph);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));

            doc.Add(table);

            //Add Total Row start
            Paragraph paragraphTable5 = new Paragraph();

            string[] itemsss = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

            Font font13 = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font font11 = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Paragraph paragraphh = new Paragraph("", font12);



            for (int i = 0; i < items.Length; i++)
            {
                paragraph.Add(new Phrase("", font10));
            }

            table = new PdfPTable(3);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            paragraph.Alignment = Element.ALIGN_RIGHT;

            table.SetWidths(new float[] { 0f, 76f, 20f });
            table.AddCell(paragraph);
            PdfPCell cell = new PdfPCell(new Phrase("Sub Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
			var amtf = Convert.ToDouble(amount);
            System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
            string StrAmt = amtf.ToString("N2", info);
            PdfPCell cell11 = new PdfPCell(new Phrase(Dt.Rows[0]["Currency"].ToString() + " " + StrAmt, FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell11.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell11);
            doc.Add(table);

           

            //Grand total Row STart
            Paragraph paragraphTable17 = new Paragraph();
            paragraphTable5.SpacingAfter = 0f;

            string[] itemm = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

            Font font16 = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font font17 = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Paragraph paragraphhhhh = new Paragraph("", font12);

            //paragraphh.SpacingAfter = 10f;

            for (int i = 0; i < items.Length; i++)
            {
                paragraph.Add(new Phrase("", font10));
            }

            table = new PdfPTable(3);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            table.SetWidths(new float[] { 0f, 76f, 20f });
            table.AddCell(paragraph);
            PdfPCell cell44 = new PdfPCell(new Phrase("Total Amount With Total Tax", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell44.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell44);
			 var WithGSTAm = Convert.ToDouble(lblWithGSTAmount);
            string StrWithGSTAm = WithGSTAm.ToString("N2", info);
            PdfPCell cell55 = new PdfPCell(new Phrase(Dt.Rows[0]["Currency"].ToString() + " " + StrWithGSTAm, FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell55.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell55);
            doc.Add(table);



            Paragraph paragraphTable99 = new Paragraph();

            //Puja Enterprises Sign
            string[] itemss = {
				"Remarks                       :    "+Remarkd+"\n\n",
                "Payment Term                 :    "+paymentterm+"\n",
                "CGST                                :    "+lblCGSTamt+"\n",
                "SGST                                :    "+lblSGSTamt+" \n",
                "IGST                                 :    "+lblIGSTamt+" \n",
                "VALIDITY OF OFFER      :    "+validityofoffer+" \n",
                "DELIVERY PERIOD         :    "+deliveryperiod+" \n",
                "TRANSPORTATION        :    "+transportation+" \n",
                "STANDARD PACKING    :    The Enclosures Will Be Packed In 2 Ply Corrugated Sheet The Charges For Teh Same Are Included In Above Price \n",
                "SPECIAL PACKING         :    Any Special Will Be Supplied At Extra Cost \n",
                "INSPECTION                    :    You/Your Representative Can Inspect The Enclosure At Our Factory\n",
                "NOTE                                :    Price May Change If Any Addition Is Done In The Process Of Drawing Approval \n",



                        };

            Font font14 = FontFactory.GetFont("Arial", 11);
            Font font15 = FontFactory.GetFont("Arial", 8);
            Paragraph paragraphhh = new Paragraph(" Remarks :\n\n", font12);


            for (int i = 0; i < itemss.Length; i++)
            {
                paragraphhh.Add(new Phrase("\u2022 \u00a0" + itemss[i] + "\n", font15));
            }

            table = new PdfPTable(1);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 560f });

            table.AddCell(paragraphhh);
            //table.AddCell(new Phrase("Puja Enterprises \n\n\n\n         Sign", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            doc.Add(table);

            Paragraph paragraphTable10000 = new Paragraph();

            //Puja Enterprises Sign
            string[] itemss4 = {
                "Payment Term     ",

                        };

            Font font144 = FontFactory.GetFont("Arial", 11);
            Font font155 = FontFactory.GetFont("Arial", 8);
            Paragraph paragraphhhhhff = new Paragraph(" Remarks :\n\n", font12);


            //for (int i = 0; i < itemss4.Length; i++)
            //{
            //    paragraphhhhhff.Add(new Phrase("\u2022 \u00a0" + itemss4[i] + "\n", font155));
            //}

            table = new PdfPTable(2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 300f,100f });
			
			
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(imageStamp);
            image1.ScaleToFit(500,140);

            PdfPCell imageCell = new PdfPCell(image1);		

            //table.AddCell(paragraphhhhhff);
            table.AddCell(new Phrase(" ", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(imageCell);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            doc.Add(table);
            doc.Close();


            Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/Files/") + "SalesQuatition.pdf");
            
            Font blackFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(FileBuffer);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        if (i == 1)
                        {

                        }
                        else
                        {
                            var pdfbyte = stamper.GetOverContent(i);
                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageURL);
                            image.ScaleToFit(70, 100);
                            image.SetAbsolutePosition(40, 792);
                            image.SpacingBefore = 50f;
                            image.SpacingAfter = 1f;
                            image.Alignment = Element.ALIGN_LEFT;
                            pdfbyte.AddImage(image);
                        }
                        var PageName = "Page No. " + i.ToString();
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(PageName, blackFont), 568f, 820f, 0);
                    }
                }
                FileBuffer = stream.ToArray();
            }


            //string empFilename = "SalesQuatition" + DateTime.Now.ToShortDateString() + ".pdf";
			string empFilename = QuatationNumber + " " + PartyName + ".pdf";

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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Pdf();
    }
}

#line default
#line hidden
