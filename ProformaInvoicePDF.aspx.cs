using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Net.Mail;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Threading.Tasks;
using iTextSharp.text;
using System.Collections;
using System.Net;
using iTextSharp.text.html.simpleparser;
using Image = iTextSharp.text.Image;
using iTextSharp.text.pdf.parser;

public partial class Admin_ProformaInvoicePDF : System.Web.UI.Page
{
    //string id;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                //id = Session["PDFID"].ToString();// Decrypt(Request.QueryString["Id"].ToString());
                Pdf("Original");
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public static string stringBetween(string Source, string Start, string End)
    {
        string result = "";
        if (Source.Contains(Start) && Source.Contains(End))
        {
            int StartIndex = Source.IndexOf(Start, 0) + Start.Length;
            int EndIndex = Source.IndexOf(End, StartIndex);
            result = Source.Substring(StartIndex, EndIndex - StartIndex);
            return result;
        }
        return result;
    }

    protected void Pdf(string flg)
    {
        string id = Session["PDFID"].ToString();

        DataTable Dt = new DataTable();
        SqlDataAdapter Da = new SqlDataAdapter("select * from vw_ProformaInvoicePDF where Id = '" + id + "'", con);

        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());
        string billingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
        string InvoiceNo = Dt.Rows[0]["InvoiceNo"].ToString();
        string Remark = Dt.Rows[0]["Remark"].ToString();

        Document doc = new Document(PageSize.A4, 30f, 10f, -25f, 0f);
        //Document doc = new Document(PageSize.A4, 10f,);
        //string Path = ;
        //string Docname = billingCustomer + "_ProformaInvoice.pdf";
        string DocNo = InvoiceNo.Replace("/", "_");
        string Docname = "Proforma Invoice No. " + DocNo + ".pdf";
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + Docname, FileMode.Create));
        //PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);

        doc.Open();
        string imageURL = Server.MapPath("~") + "/img/ProcetechPDFLOGO.jpg";
        //Price Format
        System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");

        iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);

        //Resize image depend upon your need

        png.ScaleToFit(80, 100);

        //For Image Position
        png.SetAbsolutePosition(25, 785);
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
            var CreateDate = DateTime.Now.ToString("yyyy-MM-dd");

            string invoicedate = Dt.Rows[0]["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray());
            string BillingAddress = Dt.Rows[0]["BillingAddress"].ToString();
            string grandtotal = Dt.Rows[0]["GrandTotalFinal"].ToString();
            string AgainstNumber = Dt.Rows[0]["AgainstNumber"].ToString();

            string ContactNo = Dt.Rows[0]["ContactNo"].ToString();
            string EmailID = Dt.Rows[0]["Email"].ToString();
            string CurrencyType = Dt.Rows[0]["CurrencyType"].ToString();

            string GSTNo = "";
            string PONo = "";
            string PODate = "";
            string OrderNo = "";
            string OrderDate = "";
            string StateName = "";
            string quotationno = "";
            string quotationdate = "";
            string PanNo = "";

            DataTable dtgstno = new DataTable();
            SqlDataAdapter sadgst = new SqlDataAdapter("select * from Company where status='0' and cname='" + billingCustomer + "'", con);
            sadgst.Fill(dtgstno);
            if (dtgstno.Rows.Count > 0)
            {
                GSTNo = dtgstno.Rows[0]["gstno"].ToString();
                //if (GSTNo == "")
				if (GSTNo == "URP")
                {
                    PanNo = "NA";
                    GSTNo = "";
                }
                else
                {
                    string result = GSTNo.Substring(0, 2);

                    SqlCommand cmdcheck = new SqlCommand("select StateName from tblStateNameCode where Code='" + result + "'", con);
                    con.Open();
                    string stateName = cmdcheck.ExecuteScalar().ToString();
                    StateName = stateName + "(" + result + ")";
                    con.Close();
                }
            }

            DataTable dtpodt = new DataTable();
            SqlDataAdapter sadpodtls = new SqlDataAdapter("select * from OrderAccept where pono='" + AgainstNumber + "'", con);
            sadpodtls.Fill(dtpodt);
            if (dtpodt.Rows.Count > 0)
            {
                PONo = dtpodt.Rows[0]["pono"].ToString();

                // string str = dtpodt.Rows[0]["podate"].ToString();
                // str = str.Replace("00:00:00 AM", "");
                // var time = Convert.ToDateTime(str);
                //OrderNo = dtpodt.Rows[0]["OAno"].ToString();
                quotationno = dtpodt.Rows[0]["quotationno"].ToString();

                if (quotationno == "")
                {
                    quotationno = "                ";
                }
                else
                {
                    quotationno = dtpodt.Rows[0]["quotationno"].ToString();
                }
                //quotationdate = dtpodt.Rows[0]["quotationdate"].ToString();

                string str1 = dtpodt.Rows[0]["quotationdate"].ToString();
                if (str1 == "")
                {
                    quotationdate = "";
                }
                else
                {
                    str1 = str1.Replace("00:00:00 AM", "");
                    var time1 = Convert.ToDateTime(str1);
                    quotationdate = time1.ToString("dd-MM-yyyy");
                }
            }


			  if (GSTNo == "")
           // if (GSTNo == "URP")
            {
                PanNo = "NA";
                GSTNo = "NA";
            }
            else
            {
                string MyString = GSTNo;
                string res = MyString.Substring(0, 2);
                string word1 = res;
                string word2 = "1Z";
                PanNo = stringBetween(MyString, word1, word2);
            }


            PdfContentByte cb = writer.DirectContent;
            cb.Rectangle(28f, 756f, 560f, 80f);

            cb.Stroke();
            // Header 
            cb.BeginText();

            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 25);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PROCETECH AUTOMATION PVT. LTD.", 150, 815, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PL LC 03, FLAT NO B-506, POLITE HERMITAGE, SECTOR NO 18, CHIKHALI PRADHIKARN CHINCHWAD, Pune. 411019 Maharashtra", 98, 800, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "EMAIL : info@procetech.in", 250, 785, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
            cb.EndText();


            PdfContentByte cbbb = writer.DirectContent;
            cbbb.Rectangle(28f, 756f, 560f, 25f);
            cbbb.Stroke();
            //Header
            cbbb.BeginText();
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "GSTIN :27AAHCP6642H1Z6" + "", 48, 765, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PAN NO: AAHCP6642H1" + "", 170, 765, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "MAHARASHTRA STATE GST CODE : 27" + "", 280, 765, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CONTACT : 9373654308", 455, 765, 0);
            cbbb.EndText();

            PdfContentByte cd = writer.DirectContent;
            cd.Rectangle(28f, 731f, 560f, 25f);
            cd.Stroke();
            // Header 

            cd.BeginText();

            if (flg == "Original")
            {
                //cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "ORIGINAL FOR BUYER", 480, 739, 0);
            }
            else if (flg == "Duplicate")
            {
                cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "DUPLICATE FOR TRANSPORTER", 450, 739, 0);
            }
            else if (flg == "Triplicate")
            {
                cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TRIPLICATE FOR SUPPLIER", 470, 739, 0);
            }
            else if (flg == "Extra")
            {
                cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "EXTRA COPY", 480, 739, 0);
            }
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PROFORMA INVOICE", 270, 739, 0);
            cd.EndText();


            //DetailCustomer

            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingBefore = 120f;
            paragraphTable1.SpacingAfter = 0f;

            PdfPTable table = new PdfPTable(2);

            float[] widths2 = new float[] { 100, 100 };
            table.SetWidths(widths2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            //DateTime ffff1 = Convert.ToDateTime(Dt.Rows[0]["PODate"].ToString());
            //string datee = ffff1.ToString("yyyy-MM-dd");
            table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;

            var date = DateTime.Now.ToString("yyyy-MM-dd");

            table.AddCell(new Phrase("Details Of Receiver/Billed to :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            //table.AddCell(new Phrase("PO. No : ", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            //table.AddCell(new Phrase(customerPoNo, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            //table.AddCell(new Phrase("PO Date :", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            //table.AddCell(new Phrase(PODate, FontFactory.GetFont("Arial", 9, Font.NORMAL)));


            paragraphTable1.Add(table);
            doc.Add(paragraphTable1);

            //Bill To
            Paragraph paragraphTable44 = new Paragraph();
            paragraphTable44.SpacingAfter = 0f;

            Font font141 = FontFactory.GetFont("Arial", 9);
            Font font151 = FontFactory.GetFont("Arial", 9);

            table = new PdfPTable(2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 280f, 280f });

            //table.AddCell(paragraphTable4);
            table.AddCell(new Phrase("" + billingCustomer + "\n" + BillingAddress + " \nGSTIN: " + GSTNo + "      Pan No.: " + PanNo + " \nState Name:" + StateName + "     Contact No.: " + ContactNo + "\nEmail ID.: " + EmailID + "\n", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase("Proforma No: " + InvoiceNo + "                   Proforma Date: " + invoicedate + "\n\nQuotation No: " + quotationno + "           Quotation Date: " + quotationdate + " \n\nPO No: " + PONo + "                   PO Date: " + PODate + "\n", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            doc.Add(table);
            // End

            ///Description Table

            Paragraph paragraphTable2 = new Paragraph();
            paragraphTable2.SpacingAfter = 0f;

            if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
            {
                table = new PdfPTable(12);
                float[] widths3 = new float[] { 4f, 39f, 12f, 6f, 12f, 10f, 12f, 8f, 10f, 8f, 10f, 15f };
                table.SetWidths(widths3);
            }
            else
            {
                table = new PdfPTable(10);
                float[] widths3 = new float[] { 4f, 39f, 12f, 6f, 12f, 10f, 12f, 8f, 10f, 15f };
                table.SetWidths(widths3);
            }


            double Ttotal_price = 0;
            double CGST_price = 0;
            double SGST_price = 0;
            double IGST_price = 0;
            double GrandTotal1 = 0;
            string CGSTPer = "";
            string SGSTPer = "";
            string IGSTPer = "";


            if (Dt.Rows.Count > 0)
            {
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("Name Of Particulars", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("HSN Code", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("Qty", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("Rate", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("Disc(%)", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("Amount", FontFactory.GetFont("Arial", 9, Font.BOLD)));

                if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
                {
                    table.AddCell(new Phrase("CGST(%)", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                    table.AddCell(new Phrase("CGST Amt", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                    table.AddCell(new Phrase("SGST(%)", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                    table.AddCell(new Phrase("SGST Amt", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                else
                {
                    table.AddCell(new Phrase("IGST(%)", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                    table.AddCell(new Phrase("IGST Amt", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }


                table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 9, Font.BOLD)));

                int rowid = 1;
                foreach (DataRow dr in Dt.Rows)
                {
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;

                    string Rate = dr["Rate"].ToString();
                    var ConvRate = Convert.ToDouble(Rate);
                    string FinaleRate = ConvRate.ToString("N2", info);

                    string TaxableAmt = dr["TaxableAmt"].ToString();
                    var ConvTaxableAmt = Convert.ToDouble(TaxableAmt);
                    string FinaleTaxableAmt = ConvTaxableAmt.ToString("N2", info);

                    double Ftotal = Convert.ToDouble(dr["GrandTotal"].ToString());
                    string _ftotal = Ftotal.ToString("##.00");

                    string partic = dr["Particular"].ToString().Replace("Enclosure For Control Panel.", "");

                    string Description = partic + "\n" + dr["Description"].ToString();

                    var amt = dr["TaxableAmt"].ToString();

                    decimal cgstamt = 0;
                    decimal sgstamt = 0;
                    decimal igstamt = 0;
                    if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
                    {
                        var cgstper = dr["CGSTPer"].ToString();
                        var sgstper = dr["SGSTPer"].ToString();

                        cgstamt = Convert.ToDecimal(amt) * Convert.ToDecimal(cgstper) / 100;
                        sgstamt = Convert.ToDecimal(amt) * Convert.ToDecimal(sgstper) / 100;
                    }
                    else
                    {
                        var igstper = dr["IGSTPer"].ToString();

                        igstamt = Convert.ToDecimal(amt) * Convert.ToDecimal(igstper) / 100;
                    }


                    table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["Description"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["HSN"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["Qty"].ToString() + " Nos", FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(FinaleRate, FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["Discount"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(FinaleTaxableAmt, FontFactory.GetFont("Arial", 9)));

                    if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
                    {
                        table.AddCell(new Phrase(dr["CGSTPer"].ToString(), FontFactory.GetFont("Arial", 9)));
                        table.AddCell(new Phrase(cgstamt.ToString(), FontFactory.GetFont("Arial", 9)));
                        table.AddCell(new Phrase(dr["SGSTPer"].ToString(), FontFactory.GetFont("Arial", 9)));
                        table.AddCell(new Phrase(sgstamt.ToString(), FontFactory.GetFont("Arial", 9)));
                    }
                    else
                    {
                        table.AddCell(new Phrase(dr["IGSTPer"].ToString(), FontFactory.GetFont("Arial", 9)));
                        table.AddCell(new Phrase(igstamt.ToString(), FontFactory.GetFont("Arial", 9)));
                    }
                    table.AddCell(new Phrase(_ftotal, FontFactory.GetFont("Arial", 9)));
                    rowid++;
                    CGSTPer = dr["CGSTPer"].ToString();
                    SGSTPer = dr["SGSTPer"].ToString();
                    IGSTPer = dr["IGSTPer"].ToString();
                    Ttotal_price += Convert.ToDouble(dr["TaxableAmt"].ToString());
                    GrandTotal1 += Convert.ToDouble(dr["GrandTotal"].ToString());
                    CGST_price += Convert.ToDouble(cgstamt);
                    SGST_price += Convert.ToDouble(sgstamt);
                    IGST_price += Convert.ToDouble(igstamt);
                }

            }
            string amount = Ttotal_price.ToString();
            paragraphTable2.Add(table);
            doc.Add(paragraphTable2);
            ///End Description table
            //Space
            Paragraph paragraphTable3 = new Paragraph();

            string[] items = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

            Font font12 = FontFactory.GetFont("Arial", 9, Font.BOLD);
            Font font10 = FontFactory.GetFont("Arial", 9, Font.BOLD);
            Paragraph paragraph = new Paragraph("", font12);

            for (int i = 0; i < items.Length; i++)
            {
                paragraph.Add(new Phrase("", font10));
            }

            if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
            {
                table = new PdfPTable(12);
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                table.SetWidths(new float[] { 4f, 39f, 12f, 6f, 12f, 10f, 12f, 8f, 10f, 8f, 10f, 15f });
                table.AddCell(paragraph);
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                // table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                if (Dt.Rows.Count == 1 || Dt.Rows.Count == 2)
                {
                    table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                else if (Dt.Rows.Count == 2)
                {
                    table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                else if (Dt.Rows.Count == 3)
                {
                    table.AddCell(new Phrase("\n\n\n\n\n", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                else
                {
                    table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                doc.Add(table);
                //Space end
            }
            else
            {
                table = new PdfPTable(10);
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
                table.SetWidths(new float[] { 4f, 39f, 12f, 6f, 12f, 10f, 12f, 8f, 10f, 15f });
                table.AddCell(paragraph);
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));m
                // table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                if (Dt.Rows.Count == 1 || Dt.Rows.Count == 2)
                {
                    table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                else if (Dt.Rows.Count == 2)
                {
                    table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                else if (Dt.Rows.Count == 3)
                {
                    table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                else
                {
                    table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                }
                doc.Add(table);
            }

            //charge description
            Paragraph paragraphTable4 = new Paragraph();
            paragraphTable4.SpacingAfter = 0f;
            if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
            {
                table = new PdfPTable(12);
                float[] widths33 = new float[] { 4f, 39f, 12f, 6f, 12f, 10f, 12f, 8f, 10f, 8f, 10f, 15f };
                table.SetWidths(widths33);
            }
            else
            {

                table = new PdfPTable(10);
                float[] widths33 = new float[] { 4f, 39f, 12f, 6f, 12f, 10f, 12f, 8f, 10f, 15f };
                table.SetWidths(widths33);
            }
            double Ttotal_price1 = 0;
            double CGST_price1 = 0;
            double SGST_price1 = 0;
            double IGST_price1 = 0;
            int rowidd = 1;
            decimal ValueOFSupply = 0;
            decimal CGStTotal = 0;
            decimal SGStTotal = 0;
            decimal IGStTotal = 0;
            decimal TotalAmount = 0;
            SqlCommand cmd = new SqlCommand("select * from vw_ProformaInvoicePDF where Id='" + id + "'", con);
            con.Open();
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {

                table.TotalWidth = 560f;
                table.LockedWidth = true;

                double Ftotal = Convert.ToDouble(dr1["Cost"].ToString());
                string _ftotal = Ftotal.ToString("##.00");

                string Description1 = dr1["ChargesDescription"].ToString();
                

                var amt = dr1["Basic"].ToString();

                decimal cgstamt1 = 0;
                decimal sgstamt1 = 0;
                decimal igstamt1 = 0;
                if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
                {
                    var cgstper1 = dr1["CGST"].ToString();
                    var sgstper1 = dr1["SGST"].ToString();

                    cgstamt1 = Convert.ToDecimal(amt) * Convert.ToDecimal(cgstper1) / 100;
                    sgstamt1 = Convert.ToDecimal(amt) * Convert.ToDecimal(sgstper1) / 100;
                }
                else
                {
                    var igstper1 = dr1["IGST"].ToString();

                    igstamt1 = Convert.ToDecimal(amt) * Convert.ToDecimal(igstper1) / 100;
                }
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase("              Fright Charges", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(dr1["HSNTcs"].ToString(), FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(dr1["Basic"].ToString(), FontFactory.GetFont("Arial", 9)));
                if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
                {
                    table.AddCell(new Phrase(cgstamt1 == 0 ? "0" : dr1["CGST"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(cgstamt1.ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(sgstamt1 == 0 ? "0" : dr1["SGST"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(sgstamt1.ToString(), FontFactory.GetFont("Arial", 9)));
                }
                else
                {
                    table.AddCell(new Phrase(igstamt1 == 0 ? "0" : dr1["IGST"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(igstamt1.ToString(), FontFactory.GetFont("Arial", 9)));
                }
                table.AddCell(new Phrase(_ftotal, FontFactory.GetFont("Arial", 9)));
                //rowidd++;

                Ttotal_price1 += Convert.ToDouble(dr1["TaxableAmt"].ToString());
                CGST_price1 += Convert.ToDouble(cgstamt1);
                SGST_price1 += Convert.ToDouble(sgstamt1);
                IGST_price1 += Convert.ToDouble(sgstamt1);

                ValueOFSupply = Convert.ToDecimal(Ttotal_price) + Convert.ToDecimal(dr1["Basic"]);
                CGStTotal = Convert.ToDecimal(CGST_price) + Convert.ToDecimal(cgstamt1);
                SGStTotal = Convert.ToDecimal(SGST_price) + Convert.ToDecimal(sgstamt1);
                IGStTotal = Convert.ToDecimal(IGST_price) + Convert.ToDecimal(igstamt1);
                TotalAmount = Convert.ToDecimal(Ftotal) + Convert.ToDecimal(GrandTotal1);
                dr1.Close();
                con.Close();
            }

            string amount1 = Ttotal_price1.ToString();
            paragraphTable4.Add(table);
            doc.Add(paragraphTable4);

            //end change description
            ////calculation supply
            ///value of supply
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

            var ConvValueOFSupply = Convert.ToDouble(ValueOFSupply);
            string FinaleValueOFSupply = ConvValueOFSupply.ToString("N2", info);

            table.SetWidths(new float[] { 0f, 119f, 15f });
            table.AddCell(paragraph);
            PdfPCell cell = new PdfPCell(new Phrase("Value of Supply", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
            PdfPCell cell11 = new PdfPCell(new Phrase(FinaleValueOFSupply.ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            cell11.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell11);
            doc.Add(table);
            ///end calculation supply

            decimal CGSTAmount = 0;
            decimal SGSTAmount = 0;
            decimal IGSTAmount = 0;
            if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
            {
                CGSTAmount = ValueOFSupply * (Convert.ToDecimal(CGSTPer.Trim())) / 100;
                SGSTAmount = ValueOFSupply * (Convert.ToDecimal(SGSTPer.Trim())) / 100;
            }
            else
            {
                IGSTAmount = ValueOFSupply * (Convert.ToDecimal(IGSTPer.Trim())) / 100;
            }

            if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
            {
                //Add CGST
                Paragraph paragraphTable6 = new Paragraph();

                string[] itemsss6 = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

                Font font136 = FontFactory.GetFont("Arial", 12, Font.BOLD);
                Font font116 = FontFactory.GetFont("Arial", 10, Font.BOLD);
                Paragraph paragraphh6 = new Paragraph("", font12);
                for (int i = 0; i < items.Length; i++)
                {
                    paragraph.Add(new Phrase("", font10));
                }

                table = new PdfPTable(3);
                table.TotalWidth = 560f;
                table.LockedWidth = true;

                paragraph.Alignment = Element.ALIGN_RIGHT;

                table.SetWidths(new float[] { 0f, 119f, 15f });
                table.AddCell(paragraph);
                PdfPCell cell6 = new PdfPCell(new Phrase("Add CGST(" + CGSTPer + "%)", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell6.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell6);
                PdfPCell cell116 = new PdfPCell(new Phrase(CGSTAmount.ToString("#.00"), FontFactory.GetFont("Arial", 9, Font.NORMAL)));
                cell116.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell116);
                doc.Add(table);
                ///end CGST

                //Add SGST
                Paragraph paragraphTable67 = new Paragraph();

                string[] itemsss67 = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

                Font font1367 = FontFactory.GetFont("Arial", 12, Font.BOLD);
                Font font1167 = FontFactory.GetFont("Arial", 10, Font.BOLD);
                Paragraph paragraphh67 = new Paragraph("", font12);



                for (int i = 0; i < items.Length; i++)
                {
                    paragraph.Add(new Phrase("", font10));
                }

                table = new PdfPTable(3);
                table.TotalWidth = 560f;
                table.LockedWidth = true;

                paragraph.Alignment = Element.ALIGN_RIGHT;

                table.SetWidths(new float[] { 0f, 119f, 15f });
                table.AddCell(paragraph);
                PdfPCell cell67 = new PdfPCell(new Phrase("Add SGST(" + SGSTPer + "%)", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell67.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell67);
                PdfPCell cell1167 = new PdfPCell(new Phrase(SGSTAmount.ToString("#.00"), FontFactory.GetFont("Arial", 9, Font.NORMAL)));
                cell1167.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell1167);
                doc.Add(table);
                ///end SGST
            }

            else
            {
                //Add IGST
                Paragraph paragraphTable6778 = new Paragraph();

                string[] itemsss6778 = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

                //Font font1367 = FontFactory.GetFont("Arial", 12, Font.BOLD);
                //Font font1167 = FontFactory.GetFont("Arial", 10, Font.BOLD);
                Paragraph paragraphh67777 = new Paragraph("", font12);



                for (int i = 0; i < items.Length; i++)
                {
                    paragraph.Add(new Phrase("", font10));
                }

                table = new PdfPTable(3);
                table.TotalWidth = 560f;
                table.LockedWidth = true;

                paragraph.Alignment = Element.ALIGN_RIGHT;

                table.SetWidths(new float[] { 0f, 119f, 15f });
                table.AddCell(paragraph);
                PdfPCell cell6777 = new PdfPCell(new Phrase("Add IGST(" + IGSTPer + "%)", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell6777.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell6777);
                PdfPCell cell1167777 = new PdfPCell(new Phrase(IGSTAmount.ToString("#.00"), FontFactory.GetFont("Arial", 9, Font.NORMAL)));
                cell1167777.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell1167777);
                doc.Add(table);
                ///end SGST
            }


            ///Add Tax Amount
            //Add SGST
            Paragraph paragraphTable678 = new Paragraph();

            string[] itemsss678 = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

            Font font13678 = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font font11678 = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Paragraph paragraphh678 = new Paragraph("", font12);

            Decimal TaxAmount = 0;
            if (Dt.Rows[0]["IGSTPer"].ToString() == "0")
            {
                TaxAmount = CGSTAmount + SGSTAmount;
            }
            else
            {
                TaxAmount = IGSTAmount;
            }


            for (int i = 0; i < items.Length; i++)
            {
                paragraph.Add(new Phrase("", font10));
            }

            table = new PdfPTable(3);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            paragraph.Alignment = Element.ALIGN_RIGHT;

            table.SetWidths(new float[] { 0f, 119f, 15f });
            table.AddCell(paragraph);
            PdfPCell cell678 = new PdfPCell(new Phrase("Tax Amount", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell678.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell678);
            PdfPCell cell11678 = new PdfPCell(new Phrase(TaxAmount.ToString("#.00"), FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            cell11678.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell11678);
            doc.Add(table);
            ///end Tax Amount

            var totalgrandAmount = ValueOFSupply + TaxAmount;
            decimal grandtotal1 = Convert.ToDecimal(totalgrandAmount);

            var Totalamtfff = Math.Round(grandtotal1);
            string FinaleTotalamt = Totalamtfff.ToString("N2", info);
            //double GrandroudedVal = Math.Round(Convert.ToDouble(grandtotal1), MidpointRounding.AwayFromZero); // rounded value
            double GetVal = 0;  // to know rounded value
            GetVal -= Convert.ToDouble(grandtotal1) - Convert.ToDouble(Totalamtfff);
            GetVal = Math.Round(GetVal, 2);

            Decimal AdvanceAmt = Convert.ToDecimal(Dt.Rows[0]["AdvanceAmt"].ToString() == null || Dt.Rows[0]["AdvanceAmt"].ToString() == "" ? "0" : Dt.Rows[0]["AdvanceAmt"].ToString());
            Decimal FinalGrandTotal = 0;
            //FinalGrandTotal = Convert.ToDecimal(FinaleValueOFSupply) - Convert.ToDecimal(AdvanceAmt);
            FinalGrandTotal = Convert.ToDecimal(Dt.Rows[0]["GrandTotalFinal"].ToString()) - Convert.ToDecimal(AdvanceAmt);

            string Amtinword = ConvertNumbertoWords(Convert.ToInt32(FinalGrandTotal), CurrencyType);

            ///total Amount


            //

            //For Advance Received Section
            table = new PdfPTable(3);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            paragraph.Alignment = Element.ALIGN_RIGHT;
            

            table.SetWidths(new float[] { 0f, 199f, 25f });
            table.AddCell(paragraph);

            PdfPCell cell4434589 = new PdfPCell(new Phrase("Advance Received (" + CurrencyType + ") ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell4434589.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell4434589);
            PdfPCell cell4404489 = new PdfPCell(new Phrase(AdvanceAmt.ToString("#.00"), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell4404489.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell4404489);
            doc.Add(table);
            ///end Advance Received
            //


            //Total amount InNumber
            table = new PdfPTable(3);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            paragraph.Alignment = Element.ALIGN_RIGHT;

            table.SetWidths(new float[] { 0f, 199f, 25f });
            table.AddCell(paragraph);

           

            //var ConvtotalgrandAmount = Convert.ToDouble(totalgrandAmount);
            //string FinaletotalgrandAmount = ConvtotalgrandAmount.ToString("N2", info);

            PdfPCell cell443458 = new PdfPCell(new Phrase("Total Amount (" + CurrencyType + ") ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell443458.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell443458);
            //PdfPCell cell443457 = new PdfPCell(new Phrase("Total Amount(Rs): " , FontFactory.GetFont("Arial", 9, Font.BOLD)));
            ////cell443457.HorizontalAlignment = Element.ALIGN_LEFT;
            //table.AddCell(cell443457);
            PdfPCell cell440448 = new PdfPCell(new Phrase(FinalGrandTotal.ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //PdfPCell cell440448 = new PdfPCell(new Phrase("12205.19", FontFactory.GetFont("Arial", 9, Font.BOLD)));    //For Advance Purpose
            cell440448.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell440448);
            doc.Add(table);


            ///end Total InNumber

            //Total amount In word
            table = new PdfPTable(3);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            paragraph.Alignment = Element.ALIGN_RIGHT;

            table.SetWidths(new float[] { 0f, 199f, 0f });
            table.AddCell(paragraph);


            PdfPCell cell44345 = new PdfPCell(new Phrase("Total Amount (" + CurrencyType + "): " + Amtinword + " Only", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //PdfPCell cell44345 = new PdfPCell(new Phrase("Total Amount(" + CurrencyType + "): Twelve Thousand Two Hundred Five USD And Nineteen Cent Only", FontFactory.GetFont("Arial", 9, Font.BOLD)));    //For Advance Purpose
            cell44345.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell44345);
            PdfPCell cell443457 = new PdfPCell(new Phrase("Total Amount (" + CurrencyType + "): ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //cell443457.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell443457);
            PdfPCell cell44044 = new PdfPCell(new Phrase(FinaleTotalamt.ToString(), FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell44044.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell44044);
            doc.Add(table);
            ///end Total Amount
            ///

            // Remark Start
            table = new PdfPTable(3);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            paragraph.Alignment = Element.ALIGN_RIGHT;

            table.SetWidths(new float[] { 0f, 199f, 0f });
            table.AddCell(paragraph);

            PdfPCell cell443455 = new PdfPCell(new Phrase("Remark : " + Remark, FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell443455.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell443455);
            //PdfPCell cell4434575 = new PdfPCell(new Phrase("Remark : "+ Remark, FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //cell443457.HorizontalAlignment = Element.ALIGN_LEFT;
            //table.AddCell(cell4434575);
            PdfPCell cell440445 = new PdfPCell(new Phrase(Remark, FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell440445.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell440445);
            doc.Add(table);
            // Remark End

            //Declaration
            Paragraph paragraphTable99 = new Paragraph(" Remarks :\n\n", font12);

            //Puja Enterprises Sign
            string[] itemss = {
                "Declaration  : I/We hereby certify that my/our registration certificate under the GST Act, 2017 is in force on the date on which the supply of",
                "the goods specified in this tax invoice is made by me/us and that the transaction of supplies covered by this tax invoice has been effected",
                "by me/us and it shall be accounted for in the turnover of supplies while filing of return and the due tax, if any, payable on the supplies",
                "has been paid or shall be paid. \n",

                "Subject To Pune Jurisdiction Only.",

                        };

            Font font14 = FontFactory.GetFont("Arial", 9);
            Font font15 = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            Paragraph paragraphhh = new Paragraph(" Terms & Condition :\n\n", font15);


            for (int i = 0; i < itemss.Length; i++)
            {
                //paragraphhh.Add(new Phrase("\u2022 \u00a0" + itemss[i] + "\n", font15));
                paragraphhh.Add(new Phrase(itemss[i] + "\n", font15));
            }

            table = new PdfPTable(1);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 560f });

            table.AddCell(paragraphhh);
            //table.AddCell(new Phrase("Puja Enterprises \n\n\n\n         Sign", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            //doc.Add(table);
            ///end declaration
            ///

            ///Sign Authorization
            ///

            // Bind stamp Image
            string imageStamp = Server.MapPath("~") + "/img/Marketing.png";
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(imageStamp);
            image1.ScaleToFit(600, 120);
            PdfPCell imageCell = new PdfPCell(image1);
            imageCell.PaddingLeft = 10f;
            imageCell.PaddingTop = 0f;
            /////////////////

            Paragraph paragraphTable10000 = new Paragraph();


            string[] itemss4 = {
                "Payment Term     ",

                        };

            Font font144 = FontFactory.GetFont("Arial", 11);
            Font font155 = FontFactory.GetFont("Arial", 8);
            Paragraph paragraphhhhhff = new Paragraph();
            table = new PdfPTable(2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 300f, 100f });

            //table.AddCell(paragraphhhhhff);
            if (CurrencyType == "INR")
            {
                table.AddCell(new Phrase("Terms & Condition :\n\n Declaration  : I/We hereby certify that my/our registration certificate under the GST Act, 2017 is in force on the date on which the supply of the goods specified in this tax invoice is made by me/us and that the transaction of supplies covered by this tax invoice has been effected by me/us and it shall be accounted for in the turnover of supplies while filing of return and the due tax, if any, payable on the supplies has been paid or shall be paid. \n\n Subject To Pune Jurisdiction Only. \n\n\bBANK: HDFC BANK LTD(THERMAX CHOWK)   \bACC No.:17958970000057   \bIFSC:HDFC0001795\n\bCHINCHWAD (Branch Code:-1795)   \bMICR No.:411240031   \bSWIFT No.:002205\n ", FontFactory.GetFont("Arial", 9)));
            }
            else
            {
                table.AddCell(new Phrase("Terms & Condition :\n\n Declaration  : I/We hereby certify that my/our registration certificate under the GST Act, 2017 is in force on the date on which the supply of the goods specified in this tax invoice is made by me/us and that the transaction of supplies covered by this tax invoice has been effected by me/us and it shall be accounted for in the turnover of supplies while filing of return and the due tax, if any, payable on the supplies has been paid or shall be paid. \n\n Subject To Pune Jurisdiction Only. \n\n\bBANK: HDFC BANK LTD(THERMAX CHOWK)   \bACC No.:50200080399811   \bIFSC:HDFC0001795\n\bCHINCHWAD (Branch Code:-1795)   \bMICR No.:411240031   \bSWIFT No.:HDFCINBB\n ", FontFactory.GetFont("Arial", 9)));
            }
            table.AddCell(imageCell);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            doc.Add(table);
            //doc.Close();
            ///end Sign Authorization


            //doc.NewPage();

            //doc.Add(table);//Add the paragarh to the document  
        }

        //Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + "TaxInvoice.pdf");

        //Font blackFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
        //using (MemoryStream stream = new MemoryStream())
        //{
        //    PdfReader reader = new PdfReader(FileBuffer);
        //    using (PdfStamper stamper = new PdfStamper(reader, stream))
        //    {
        //        int pages = reader.NumberOfPages;
        //        for (int i = 1; i <= pages; i++)
        //        {
        //            if (i == 1)
        //            {

        //            }
        //            else
        //            {
        //                var pdfbyte = stamper.GetOverContent(i);
        //                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageURL);
        //                image.ScaleToFit(70, 100);
        //                image.SetAbsolutePosition(40, 792);
        //                image.SpacingBefore = 50f;
        //                image.SpacingAfter = 1f;
        //                image.Alignment = Element.ALIGN_LEFT;
        //                pdfbyte.AddImage(image);
        //            }
        //            var PageName = "Page No. " + i.ToString();
        //            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(PageName, blackFont), 568f, 820f, 0);
        //        }
        //    }
        //    FileBuffer = stream.ToArray();
        //}
        doc.Close();

        ifrRight6.Attributes["src"] = @"../files/" + Docname;
    }

    public static string ConvertNumbertoWords(int numbers, string currency)
    {
        Boolean paisaconversion = false;
        var pointindex = numbers.ToString().IndexOf(".");
        var paisaamt = 0;
        if (pointindex > 0)
            paisaamt = Convert.ToInt32(numbers.ToString().Substring(pointindex + 1, 2));

        int number = Convert.ToInt32(numbers);

        if (number == 0) return "Zero";
        if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
        int[] num = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (number < 0)
        {
            sb.Append("Minus ");
            number = -number;
        }
        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
        string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
        string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };
        num[0] = number % 1000; // units
        num[1] = number / 1000;
        num[2] = number / 100000;
        num[1] = num[1] - 100 * num[2]; // thousands
        num[3] = number / 10000000; // crores
        num[2] = num[2] - 100 * num[3]; // lakhs
        for (int i = 3; i > 0; i--)
        {
            if (num[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (num[i] == 0) continue;
            u = num[i] % 10; // ones
            t = num[i] / 10;
            h = num[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }

        if (paisaamt == 0 && paisaconversion == false)
        {
            if (currency == "INR")
            {
                sb.Append("Rupees ");
            }
            else
            {
                sb.Append("Dollar ");
            }
        }
        else if (paisaamt > 0)
        {
            var paisatext = ConvertNumbertoWords(paisaamt, "");
            if (currency == "INR")
            {
                sb.AppendFormat("Rupees {0} paise", paisatext);
            }
            else
            {
                sb.AppendFormat("Dollar {0}", paisatext);
            }
        }
        return sb.ToString().TrimEnd();
    }

    protected void btnOriginal_Click(object sender, EventArgs e)
    {
        Pdf("Original");
    }

    protected void btnDuplicate_Click(object sender, EventArgs e)
    {
        Pdf("Duplicate");
    }

    protected void btnTriplicate_Click(object sender, EventArgs e)
    {
        Pdf("Triplicate");
    }

    protected void btnExtra_Click(object sender, EventArgs e)
    {
        Pdf("Extra");
    }
}