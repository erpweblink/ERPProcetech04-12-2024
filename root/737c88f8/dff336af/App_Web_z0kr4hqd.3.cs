#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\DeliveryChallanPDF.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D189A3D7219F5ACBF9B7FE81BB80C632F253836F"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\DeliveryChallanPDF.aspx.cs"
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

public partial class Admin_DeliveryChallanPDF : System.Web.UI.Page
{
    string id;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                id = Decrypt(Request.QueryString["Id"].ToString());
                Pdf(id);
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

    protected void Pdf(string id)
    {
        DataTable Dt = new DataTable();
        SqlDataAdapter Da = new SqlDataAdapter("select * from vw_DeliveryChallanPDF where Id = '" + id + "'", con);

        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());
        string billingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
        string ChallanNO = Dt.Rows[0]["ChallanNO"].ToString();

        Document doc = new Document(PageSize.A4, 30f, 10f, 21f, 0f);
        //Document doc = new Document(PageSize.A4, 10f,);
        //string Path = ;
        string Docname = billingCustomer + "_DeliveryChallan.pdf";
        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + Docname, FileMode.Create));
        //PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);

        doc.Open();
        string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";

        //Price Format
        System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");

        iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);

        //Resize image depend upon your need

        png.ScaleToFit(70, 100);

        //For Image Position
        png.SetAbsolutePosition(40, 755);
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

            string ChallanDate = Dt.Rows[0]["Challandate"].ToString().TrimEnd("0:0".ToCharArray());
            string customerPoNo = Dt.Rows[0]["CustomerPONo"].ToString();
            string ChallanNo = Dt.Rows[0]["ChallanNO"].ToString();
            string PODate = Dt.Rows[0]["PODate"].ToString().TrimEnd("0:0".ToCharArray());
            string CustChallanDate = Dt.Rows[0]["CustChallanDate"].ToString().TrimEnd("0:0".ToCharArray());

            string transactionmode = Dt.Rows[0]["TransportMode"].ToString();
            string vehicalNo = Dt.Rows[0]["VehicalNo"].ToString();
            string placeOfSupply = Dt.Rows[0]["ShippingAddress"].ToString();
            string dateOfSupply = Dt.Rows[0]["Challandate"].ToString();

            string ShippingCustomer = Dt.Rows[0]["ShippingCustomer"].ToString();
            string ShippingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
            string BillingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
            string Remark = Dt.Rows[0]["Remark"].ToString();
            //string grandtotal = Dt.Rows[0]["GrandTotalFinal"].ToString();

            string GSTNo = "";


            DataTable dtgstno = new DataTable();
            SqlDataAdapter sadgst = new SqlDataAdapter("select * from Company where status='0' and cname='" + billingCustomer + "'", con);
            sadgst.Fill(dtgstno);
            if (dtgstno.Rows.Count > 0)
            {
                GSTNo = dtgstno.Rows[0]["gstno"].ToString();
            }

            string MyString = GSTNo;
            string res = MyString.Substring(0, 2);
            string word1 = res;
            string word2 = "1Z";
            string PanNo = stringBetween(MyString, word1, word2);

            PdfContentByte cb = writer.DirectContent;
            cb.Rectangle(28f, 710f, 560f, 100f);

            cb.Stroke();
            // Header 
            cb.BeginText();
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 25);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Excel Enclosures", 250, 778, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Gat No. 1567, Shelar Vasti, Dehu-Alandi Road, Chikhali, Pune - 411062", 150, 761, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "EMAIL : mktg@excelenclosures.com", 250, 748, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
            cb.EndText();


            PdfContentByte cbbb = writer.DirectContent;
            cbbb.Rectangle(28f, 710f, 560f, 25f);
            cbbb.Stroke();
            //Header
            cbbb.BeginText();
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "GSTIN :27ATFPS1959J1Z4" + "", 48, 720, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PAN NO: ATFPS1959J" + "", 170, 720, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "MAHARASHTRA STATE GST CODE : 27" + "", 280, 720, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CONTACT : 9225658662", 455, 720, 0);
            cbbb.EndText();

            PdfContentByte cd = writer.DirectContent;
            cd.Rectangle(28f, 685f, 560f, 25f);
            cd.Stroke();
            // Header 
            cd.BeginText();
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "DELIVERY CHALLAN", 270, 693, 0);
            cd.EndText();


            //DetailCustomer

            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingBefore = 120f;
            paragraphTable1.SpacingAfter = 0f;

            PdfPTable table = new PdfPTable(4);

            float[] widths2 = new float[] { 100, 180, 100, 180 };
            table.SetWidths(widths2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            //DateTime ffff1 = Convert.ToDateTime(Dt.Rows[0]["PODate"].ToString());
            //string datee = ffff1.ToString("yyyy-MM-dd");
            table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;

            var date = DateTime.Now.ToString("yyyy-MM-dd");

            table.AddCell(new Phrase("Our Challan Number : ", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(ChallanNo, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("Our Challan Date :", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(ChallanDate, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("PO No. : ", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(customerPoNo, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("PO Date :", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(PODate, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("Your Challan No : ", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(ChallanNo, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("Your Challan Date :", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(CustChallanDate, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("Transportation Mode", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(transactionmode, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("Vehical Number", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(vehicalNo, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("Place of Supply :", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase("Pune", FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            //table.AddCell(new Phrase("E-Bill No", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            //table.AddCell(new Phrase(EBillNo, FontFactory.GetFont("Arial", 9, Font.NORMAL)));

            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));


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
            table.AddCell(new Phrase(" Details of Buyer/Billed to: \n\n " + billingCustomer + "\n Address: " + ShippingAddress + " \n GSTIN: " + GSTNo + "      Pan No.: " + PanNo + " \n State Name:Maharashtra(27) \n  ", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            table.AddCell(new Phrase(" Details of Consignee/Shipped to: \n\n " + ShippingCustomer + ", \n Address: " + ShippingAddress + "\n GSTIN: " + GSTNo + "      Pan No.: " + PanNo + " \n State Name:Maharashtra(27) \n  ", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            doc.Add(table);
            // End




            ///Description Table

            Paragraph paragraphTable2 = new Paragraph();
            paragraphTable2.SpacingAfter = 0f;
            table = new PdfPTable(5);
            float[] widths3 = new float[] { 4f, 65f, 11f, 6f, 6f };
            table.SetWidths(widths3);

            double T_Qty = 0;
            if (Dt.Rows.Count > 0)
            {
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("Name Of Particulars", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("HSN Code", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("Qty", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                table.AddCell(new Phrase("UOM", FontFactory.GetFont("Arial", 9, Font.BOLD)));

                int rowid = 1;
                foreach (DataRow dr in Dt.Rows)
                {
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;

                    string Description = dr["Particular"].ToString() + "\n" + dr["Description"].ToString();

                    table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(Description, FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["HSN"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(Convert.ToDouble(dr["Qty"].ToString()).ToString("#0.00"), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["UOM"].ToString(), FontFactory.GetFont("Arial", 9)));
                    rowid++;
                    T_Qty += Convert.ToDouble(dr["Qty"].ToString());
                }

            }

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

            table = new PdfPTable(5);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.DefaultCell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;

            table.SetWidths(new float[] { 4f, 65f, 11f, 6f, 6f });
            table.AddCell(paragraph);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));

            if (Dt.Rows.Count == 1 || Dt.Rows.Count == 2)
            {
                table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            }
            else
            {
                table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            }


            doc.Add(table);
            //Space end

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

            table = new PdfPTable(5);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            paragraph.Alignment = Element.ALIGN_RIGHT;
            table.SetWidths(new float[] { 4f, 65f, 11f, 6f, 6f });
            table.AddCell(paragraph);
            PdfPCell cell = new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
            PdfPCell cell11 = new PdfPCell(new Phrase("Total", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            cell11.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell11);
            PdfPCell cell2 = new PdfPCell(new Phrase(T_Qty.ToString("#0.00"), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell2);
            PdfPCell cell113 = new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            cell113.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell113);
            PdfPCell cell4 = new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell4);
            PdfPCell cell115 = new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)));
            cell115.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell115);
            doc.Add(table);
            ///end calculation supply

            ///Sign Authorization

            // Bind stamp Image
            string imageStamp = Server.MapPath("~") + "/img/Account.png";
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
            table.AddCell(new Phrase("\bRemarks: " + Remark + "\n\n\bRECEIPT   \n\n\bName :\n\n\bSignature\n\n\n\n\bDate :\n\n", FontFactory.GetFont("Arial", 10)));
            table.AddCell(imageCell);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            doc.Add(table);
            //doc.Close();
            ///end Sign Authorization

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


        ifrRight6.Attributes["src"] = @"../files/" + Docname;
        doc.Close();
        //string filePath = @Server.MapPath("~/files/") + Docname;
        //Response.ContentType = Docname;


        //Response.WriteFile(filePath);
    }

    public static string ConvertNumbertoWords(int number)
    {
        if (number == 0)
            return "ZERO";
        if (number < 0)
            return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";
        if ((number / 1000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
            number %= 1000000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
            number %= 100;
        }
        if (number > 0)
        {
            if (words != "")
                words += "AND ";
            var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }
}

#line default
#line hidden
