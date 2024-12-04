#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\TestCertificatePDF.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D14B28BBA194D849718FE37A9C658F98B056C95F"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\TestCertificatePDF.aspx.cs"
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

public partial class Admin_TestCertificatePDF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Pdf();
        }
    }

    private void Pdf()
    {
        if (Session["ID"] != null)
        {
            DataTable DtDtl = new DataTable();
            SqlDataAdapter DaDts = new SqlDataAdapter("select * from tblTestCertificateDtls where HeaderId = '" + Session["ID"].ToString() + "'", con);
            DaDts.Fill(DtDtl);


            var SubOANumberlist = DtDtl.AsEnumerable().Select(r => r["SubOANumber"].ToString());
            string SubOAvalue = string.Join(",\n                                              ", SubOANumberlist);

            List<string> myIds = new List<string>();
            foreach (DataRow dr in DtDtl.Rows)
            {
                myIds.Add(Convert.ToString(dr["EnclosureSize"]) + " [Qty- " + Convert.ToString(dr["Quantity"]) + " Nos.]");
            }
            string SizeAndQtyresult = string.Join(",\n                                            ", myIds);


            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("select * from tblTestCertificateHdr where id = '" + Session["ID"].ToString() + "'", con);
            Da.Fill(Dt);

            DataTable Dt2 = new DataTable();
            SqlDataAdapter Da2 = new SqlDataAdapter("select * from Company where cname='" + Dt.Rows[0]["CustomerName"].ToString() + "'", con);
            Da2.Fill(Dt2);
            string billingaddress = Dt2.Rows[0]["billingaddress"].ToString();
            string shippingaddress = Dt2.Rows[0]["shippingaddress"].ToString();

            DataTable Dt3 = new DataTable();
            SqlDataAdapter Da3 = new SqlDataAdapter("select * from employees where name='" + Dt.Rows[0]["CreatedBy"].ToString() + "'", con);
            Da3.Fill(Dt3);
            string mobile = Dt3.Rows[0]["mobile"].ToString();

            StringWriter sw = new StringWriter();
            StringReader sr = new StringReader(sw.ToString());

            Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "TestCertificate.pdf", FileMode.Create));
            //PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);

            doc.Open();

            string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";

            iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);

            //Resize image depend upon your need

            png.ScaleToFit(70, 100);

            //For Image Position
            png.SetAbsolutePosition(40, 720);
            //var document = new Document();

            //Give space before image
            //png.ScaleToFit(document.PageSize.Width - (document.RightMargin * 100), 50);
            png.SpacingBefore = 70f;

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
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Excel Enclosure", 280, 745, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Gat No. 1567, Shelar Vasti, Dehu-Alandi Road, Chikhali, Near INDTECH, Pune:411062 ", 130, 720, 0);
            //cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 300, 740, 0);
            cb.EndText();

            //PdfContentByte cbb = writer.DirectContent;
            //cbb.Rectangle(17f, 690f, 560f, 25f);
            //cbb.Stroke();
            // Header 
            //cbb.BeginText();
            //cbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            //cbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " CONTACT : 9225658662   Email ID : mktg@excelenclosures.com", 153, 722, 0);
            //cbb.EndText();

            PdfContentByte cbbb = writer.DirectContent;
            cbbb.Rectangle(17f, 690f, 560f, 20f);
            cbbb.Stroke();
            // Header 
            cbbb.BeginText();
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Ph:9822645948,9307634503", 50, 695, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tele:020-66308263-67", 192, 695, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Mail:mktg@excelenclosures.com, excelenclosures@gmail.com", 310, 695, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 480, 695, 0);
            cbbb.EndText();

            PdfContentByte cd = writer.DirectContent;
            cd.Rectangle(17f, 660f, 560f, 30f);
            cd.Stroke();
            // Header 



            cd.BeginText();
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 17);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TEST CERTIFICATE", 245, 667, 0);
            cd.EndText();

            if (Dt.Rows.Count > 0)
            {
                string CustomerName = Dt.Rows[0]["CustomerName"].ToString();
                string KindAtt = Dt.Rows[0]["KindAtt"].ToString();
                //string Address = Dt.Rows[0]["address"].ToString();
                string OANo = Dt.Rows[0]["OANo"].ToString();
                string Category = Dt.Rows[0]["Category"].ToString();
                string Shade = Dt.Rows[0]["Shade"].ToString();
                string Subject = Dt.Rows[0]["Subject"].ToString();
                string PONo = Dt.Rows[0]["PONo"].ToString();
                string CoatingThickness = Dt.Rows[0]["CoatingThickness"].ToString();
                string CreatedBy = Dt.Rows[0]["CreatedBy"].ToString();
                string IsStainlesssteel = Dt.Rows[0]["IsStainlesssteel"].ToString();
                string StainlessSteelShade = Dt.Rows[0]["StainlessSteelShade"].ToString();
                string BuffingFinish = Dt.Rows[0]["BuffingFinish"].ToString();
				string Remarks = Dt.Rows[0]["Remarks"].ToString();
				if (CoatingThickness == "Specify")
                {
                    CoatingThickness = Dt.Rows[0]["SpecifyThickness"].ToString();
                }
                else
                {
                    CoatingThickness = Dt.Rows[0]["CoatingThickness"].ToString();
                }
                Paragraph paragraphTable1 = new Paragraph();
                paragraphTable1.SpacingBefore = 120f;
                paragraphTable1.SpacingAfter = 8f;

                PdfPTable table = new PdfPTable(4);

                float[] widths2 = new float[] { 100, 180, 100, 180 };
                table.SetWidths(widths2);
                table.TotalWidth = 560f;
                table.LockedWidth = true;

                var date = DateTime.Now.ToString("yyyy-MM-dd");

                paragraphTable1.Add(table);
                doc.Add(paragraphTable1);

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

                table = new PdfPTable(9);
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 4f, 40f, 8f, 8f, 8f, 0f, 8f, 8f, 8f });
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

                Paragraph paragraphTable99 = new Paragraph();





                //Puja Enterprises Sign
                 string[] itemss = {
                "       To,                                                                                                                   Ref No : "+OANo+"\n",
                "      "+CustomerName+".,\n",
                "      "+billingaddress.Replace("\r\n"," ")+".,\n",
                "       Kind Att. : "+KindAtt+"\n\n",
                "       Subject : "+Subject+"\n",
                "       Dear Sir,    \n",
                "       A.   We hereby assure that the below mentioned enclosures can pass the category "+Category+" of ingress protection\n             standards as per IS:13947(Part-1)-1933/IEC pub 947-1(1988).These Encloures can pass \n              "+Category+" provided the instrument mounted on cutouts are "+Category+" Category  \n",
                "             * Enclosure Size :     "+SizeAndQtyresult.Replace("\n"," ")+"\n",
                "             * Enclosure Ref No :   "+SubOAvalue+"\n",
                "             * Your PO No :    "+PONo+"\n",
                "             The above enclosure is manufactured abd tested ourselves as per our procedure suitable for \n             "+Category+" During testing we have followed the similar procedure as per above standard. \n",
                "             Conclusion : It has passed the "+Category+" test\n\n",
                "       B.    Powder Coating Details:\n",
                "             7 Tank pretreatment with oven baking powder coating COLOUR "+Shade+" \n             Finish coating thickness "+CoatingThickness+" micron\n\n",
                "       C.    Above enclosure has been dimensionally checked and found as per drawing specification. We \n              hope this fulfills your requirements. \n\n",
                "       Remarks : "+Remarks+" \n\n\n",
                 "      Thanking You.\n      Yours faithfully,\n      Name: "+CreatedBy+"\n      Mobile :"+mobile+"\n"

                        };


                string[] itemssSS = {
                "       To,                                                                                                                   Ref No : "+OANo+"\n",
                "      "+CustomerName+".,\n",
                "      "+billingaddress.Replace("\r\n"," ")+".,\n",
                "       Kind Att. : "+KindAtt+"\n\n",
                "       Subject : "+Subject+"\n",
                "       Dear Sir,    \n",
                "       A.   We hereby assure that the below mentioned enclosures can pass the category "+Category+" of ingress protection\n             standards as per IS:13947(Part-1)-1933/IEC pub 947-1(1988).These Encloures can pass \n              "+Category+" provided the instrument mounted on cutouts are "+Category+" Category  \n",
                "             * Enclosure Size :     "+SizeAndQtyresult.Replace("\n"," ")+"\n",
                "             * Enclosure Ref No :   "+SubOAvalue+"\n",
                "             * Your PO No :    "+PONo+"\n",
                "             The above enclosure is manufactured abd tested ourselves as per our procedure suitable for \n             "+Category+" During testing we have folloewd the similar procedure as per above standard. \n",
                "             Conclusion : It has passed the "+Category+" test\n\n",
                "       B.    Powder Coating Details:\n",
                "             7 Tank pretreatment with oven baking powder coating COLOUR "+Shade+" \n             Finish coating thickness "+CoatingThickness+" micron\n\n",
                "       C.    Above enclosure has been dimensionally checked and found as per drawing specification. We \n              hope this fulfills your requirements. \n\n\n",
                "       D.    In case of stainless steel grade "+StainlessSteelShade+" with Buffing Finish "+BuffingFinish+" \n\n",
                 "       Remarks : "+Remarks+" \n\n\n",
                 "      Thanking You.\n      Yours faithfully,\n      Name: "+CreatedBy+"\n      Mobile :"+mobile+"\n"

                        };

                Font font14 = FontFactory.GetFont("Arial", 13);
                Font font15 = FontFactory.GetFont("Arial", 11);
                Paragraph paragraphhh = new Paragraph();

                if (IsStainlesssteel == "True")
                {
                    for (int i = 0; i < itemssSS.Length; i++)
                    {
                        paragraphhh.Add(new Phrase("" + itemssSS[i] + "\n", font15));
                    }
                }
                else
                {
                    for (int i = 0; i < itemss.Length; i++)
                    {
                        paragraphhh.Add(new Phrase("" + itemss[i] + "\n", font15));
                    }
                }


                table = new PdfPTable(1);
                table.TotalWidth = 559f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 560f });

                table.AddCell(paragraphhh);

                doc.Add(table);

                Paragraph paragraphTable10000 = new Paragraph();

                //Puja Enterprises Sign
                string[] itemss4 = {
                "   This is an electronically generated document and does not required signature.",

                        };

                Font font144 = FontFactory.GetFont("Arial", 12);
                Font font155 = FontFactory.GetFont("Arial", 11);
                Paragraph paragraphhhhhff = new Paragraph();


                for (int i = 0; i < itemss4.Length; i++)
                {
                    paragraphhhhhff.Add(new Phrase("" + itemss4[i] + "\n", font155));
                }

                table = new PdfPTable(1);
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 400f });

                //table.AddCell(paragraphhhhhff);
                //table.AddCell(new Phrase(" ", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("         For Excel Enclosures \n\n\n\n\n\n         Authorised Signature", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                doc.Add(paragraphhhhhff);

                doc.Close();


                Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + "TestCertificate.pdf");
                //string empFilename = "TestCertificate" + DateTime.Now.ToShortDateString() + ".pdf";

                //if (FileBuffer != null)
                //{
                //    Response.ContentType = "application/pdf";
                //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                //    Response.BinaryWrite(FileBuffer);
                //    Response.AddHeader("Content-Disposition", "attachment;filename=" + empFilename);
                //}
            }
            doc.Close();
            ifrRight6.Attributes["src"] = @"../files/" + "TestCertificate.pdf";
        }
        Session.Remove("ID");
    }
}

#line default
#line hidden
