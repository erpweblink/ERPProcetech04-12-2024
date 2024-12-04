#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\PDFShow.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A251A1D8C2A192B7E116D61012C775226B629A0E"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\PDFShow.aspx.cs"
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Font = iTextSharp.text.Font;

public partial class PDFShow : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Name"] != null)
            {
                Pdf(Request.QueryString["Name"].ToString());
            }
        }
    }

    private void Pdf(string Name)
    {
        DataTable Dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        if (Name == "Drawing")
        {
            cmd.CommandText = "SELECT [OANumber],[Size],[TotalQty],[InwardQty],[deliverydatereqbycust],[customername] FROM vwDrawerCreation where IsComplete is null order by deliverydatereqbycust asc";
        }
        else if (Name == "Laser Programming")
        {
            cmd.CommandText = "SELECT [OANumber],[CustomerName] as customername,[Size],[TotalQty],[InwardQty],[DeliveryDate] as deliverydatereqbycust FROM tblLaserPrograming where IsApprove = 1 and IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";
        }
        else if (Name == "Laser Cutting")
        {
            cmd.CommandText = "SELECT [OANumber],[CustomerName] as customername,[Size],[TotalQty],[InwardQty],[DeliveryDate] as deliverydatereqbycust FROM tblLaserCutting where IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";
        }
        else if (Name == "CNC Bending")
        {
            cmd.CommandText = "SELECT [OANumber],[CustomerName] as customername,[Size],[TotalQty],[InwardQty],[DeliveryDate] as deliverydatereqbycust FROM tblCNCBending where IsApprove = 1 and IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";
        }
        else if (Name == "Welding")
        {
            cmd.CommandText = "SELECT [OANumber],[CustomerName] as customername,[Size],[TotalQty],[InwardQty],[DeliveryDate] as deliverydatereqbycust FROM tblWelding where IsApprove = 1 and IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";
        }
        else if (Name == "Powder Coating")
        {
            cmd.CommandText = "SELECT [OANumber],[CustomerName] as customername,[Size],[TotalQty],[InwardQty],[DeliveryDate] as deliverydatereqbycust FROM tblPowderCoating where IsApprove = 1 and IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";
        }
        else if (Name == "Final Assembly")
        {
            cmd.CommandText = "SELECT [OANumber],[CustomerName] as customername,[Size],[TotalQty],[InwardQty],[DeliveryDate] as deliverydatereqbycust FROM tblFinalAssembly where IsApprove = 1 and IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";
        }
        else if (Name == "Stock")
        {
            cmd.CommandText = "SELECT [OANumber],[CustomerName] as customername,[Size],[TotalQty],[InwardQty],[DeliveryDate] as deliverydatereqbycust FROM tblStock where IsApprove = 1 and IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";
        }
        SqlDataAdapter Da = new SqlDataAdapter();
        Da.SelectCommand = cmd;
        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        string empFilename = Name + ".pdf";

        Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);

        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + empFilename, FileMode.Create));
        //PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);

        doc.Open();

        //string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";
        string imageStamp = Server.MapPath("~") + "/img/AdminSign.png";

        //iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(imageURL);

        ////Resize image depend upon your need

        //png.ScaleToFit(70, 100);

        ////For Image Position
        //png.SetAbsolutePosition(40, 718);

        //png.SpacingBefore = 50f;

        ////Give some space after the image

        //png.SpacingAfter = 1f;

        //png.Alignment = Element.ALIGN_LEFT;


        //doc.Add(png);


        PdfContentByte cb = writer.DirectContent;
        cb.Rectangle(0f, 0f, 0f, 0f);
        cb.Stroke();
        // Header 
        cb.BeginText();
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 25);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Excel Enclosures", 200, 815, 0);
        //cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Gat No. 1567, Shelar Vasti, Dehu-Alandi Road, Chikhali, Pune - 411062", 145, 728, 0);
        //cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
        cb.EndText();

        PdfContentByte cbbb = writer.DirectContent;
        cbbb.Rectangle(0f, 0f, 0f, 0f);
        cbbb.Stroke();
        //// Header 
        //cbbb.BeginText();
        //cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        //cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "GSTIN : 27ATFPS1959J1Z4", 30, 695, 0);
        //cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        //cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PAN NO: ATFPS1959J", 160, 695, 0);
        //cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        //cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "EMAIL : mktg@excelenclosures.com", 270, 695, 0);
        //cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
        //cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CONTACT : 9225658662", 440, 695, 0);
        //cbbb.EndText();

        PdfContentByte cdd = writer.DirectContent;
        cdd.Rectangle(0f, 0f, 0f, 0f);
        cdd.Stroke();
        // Header 
        cdd.BeginText();
        cdd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 17);
        cdd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Name, 250, 795, 0);
        cdd.EndText();

        if (Dt.Rows.Count > 0)
        {
            PdfPTable table = new PdfPTable(1);

            Paragraph paragraphTable2 = new Paragraph();
            paragraphTable2.SpacingAfter = 0f;
            paragraphTable2.SpacingBefore = 111f;
            table = new PdfPTable(5);
            float[] widths3 = new float[] { 4f, 40f, 50f, 10f, 8f };
            table.SetWidths(widths3);
            PdfPCell cell = null;
            if (Dt.Rows.Count > 0)
            {
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                cell = new PdfPCell(new Phrase("SN", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Customer Name", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Size", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Delivery Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Qty", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                int rowid = 1;
                foreach (DataRow dr in Dt.Rows)
                {
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["customername"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["Size"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["deliverydatereqbycust"].ToString().TrimEnd("0:0".ToCharArray()), FontFactory.GetFont("Arial", 9)));
                    cell = new PdfPCell(new Phrase(dr["InwardQty"].ToString(), FontFactory.GetFont("Arial", 9)));
                    cell.HorizontalAlignment = 1;
                    table.AddCell(cell);
                    rowid++;
                }
            }

            paragraphTable2.Add(table);
            doc.Add(paragraphTable2);
            doc.Close();



            Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + empFilename);

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
                            //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageURL);
                            //image.ScaleToFit(70, 100);
                            //image.SetAbsolutePosition(40, 792);
                            //image.SpacingBefore = 50f;
                            //image.SpacingAfter = 1f;
                            //image.Alignment = Element.ALIGN_LEFT;
                            //pdfbyte.AddImage(image);
                        }
                        var PageName = "Page No. " + i.ToString();
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(PageName, blackFont), 568f, 820f, 0);
                    }
                }
                FileBuffer = stream.ToArray();
            }

            ifrRight6.Attributes["src"] = @"../files/" + empFilename;

            //Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + "Drawing.pdf");

            //string empFilename = "Drawing.pdf";

            //if (FileBuffer != null)
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //    Response.BinaryWrite(FileBuffer);
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + empFilename);
            //}

        }
        doc.Close();

    }
}

#line default
#line hidden
