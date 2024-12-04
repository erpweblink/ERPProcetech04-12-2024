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

public partial class Admin_TestCertificate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
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
                BindCustomer();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("id"),
                 new DataColumn("SubOANumber"), new DataColumn("Size")
                , new DataColumn("Qty")
            });
                ViewState["SubOAData"] = dt;
                ViewState["RowNo"] = 0;
            }
        }
    }

    protected void BindCustomer()
    {
        using (con)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Distinct(cname) as companyName FROM Company "))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    ddlCustomerName.DataSource = ds.Tables[0];
                    ddlCustomerName.DataTextField = "companyName";
                    ddlCustomerName.DataValueField = "companyName";
                    ddlCustomerName.DataBind();
                }
            }
        }
        ddlCustomerName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", "0"));
        con.Close();
    }

    protected void BindOANumber()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string com = "select Distinct(OAno) as OANumber from OrderAccept where customername='" + ddlCustomerName.Text + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddlOANumbers.DataSource = dt;
        ddlOANumbers.DataBind();
        ddlOANumbers.DataTextField = "OANumber";
        ddlOANumbers.DataValueField = "OANumber";
        ddlOANumbers.DataBind();
        ddlOANumbers.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", "0"));
    }

    protected void BindSubOANumber()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cmdget = new SqlCommand("select pono from OrderAccept where OAno='" + ddlOANumbers.Text + "'", con);
        Object Pono = cmdget.ExecuteScalar();
        txtPoNo.Text = Pono.ToString();
        con.Close();
        string com = "select SubOA from tblDrawing where OANumber='" + ddlOANumbers.Text + "'";
        //string com = "select SubOA from tblDrawingOld where OANumber='" + ddlOANumbers.Text + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddlSubOanumber.DataSource = dt;
        ddlSubOanumber.DataBind();
        ddlSubOanumber.DataTextField = "SubOA";
        ddlSubOanumber.DataValueField = "SubOA";
        ddlSubOanumber.DataBind();
        ddlSubOanumber.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", "0"));
    }

    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCustomerName.Text == "0")
        {
            txtkindatt.Text = "NA";
        }
        else
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select oname1 from [Company] where " + "cname like @Search + '%' and status=0 and [isdeleted]=0";
                cmd.Parameters.AddWithValue("@Search", ddlCustomerName.Text);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        txtkindatt.Text = sdr["oname1"].ToString() == "" ? "Kind not found" : sdr["oname1"].ToString();
                    }
                }
                con.Close();

                BindOANumber();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    protected void ddlOANumbers_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubOANumber();
    }

    protected void ddlSubOanumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select Size,TotalQty from tblDrawing where " + "SubOA like @Search + '%'";
        //cmd.CommandText = "select Size,TotalQty from tblDrawingOld where " + "SubOA like @Search + '%'";            
        cmd.Parameters.AddWithValue("@Search", ddlSubOanumber.Text);
        cmd.Connection = con;
        con.Open();
        using (SqlDataReader sdr = cmd.ExecuteReader())
        {
            while (sdr.Read())
            {
                txtSize.Text = sdr["Size"].ToString() == "" ? "" : sdr["Size"].ToString();
                txtQty.Text = sdr["TotalQty"].ToString() == "" ? "" : sdr["TotalQty"].ToString();
            }
        }
        con.Close();
    }

    protected void Insert(object sender, EventArgs e)
    {
        if (txtSize.Text == "" || txtQty.Text == "" || ddlSubOanumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill All Fields !!!');", true);
        }
        else
        {
            Show_Grid();
        }
    }

    private void Show_Grid()
    {
        ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;
        DataTable dt = (DataTable)ViewState["SubOAData"];

        dt.Rows.Add(ViewState["RowNo"], ddlSubOanumber.Text, txtSize.Text, txtQty.Text);
        ViewState["SubOAData"] = dt;

        dgvSubOADtl.DataSource = (DataTable)ViewState["SubOAData"];
        dgvSubOADtl.DataBind();

        txtQty.Text = string.Empty;
        txtSize.Text = string.Empty;
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        DataTable dt = ViewState["SubOAData"] as DataTable;
        dt.Rows.Remove(dt.Rows[row.RowIndex]);
        ViewState["dt"] = dt;
        this.Show_Grid();
    }

    protected void Reset(object sender, EventArgs e)
    {
        Response.Redirect("TestCertificate.aspx");
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateData() == true)
            {
                bool IsStainlesssteel;
                if (chkIsStainlesssteel.Checked == true)
                    IsStainlesssteel = true;
                else
                    IsStainlesssteel = false;

                string SubjectLine = "Assurance of IP protection of enclosure & Surface treatment " + txtShade.Text;
                SqlCommand cmdInsert = new SqlCommand("INSERT INTO tblTestCertificateHdr([CustomerName],[OANo],[KindAtt],[Category],[Shade],[Subject],[PONo],[CoatingThickness],[CreatedBy],[CreatedOn],[IsStainlesssteel],[StainlessSteelShade],[BuffingFinish],[Remarks],[SpecifyThickness])VALUES(@CustomerName,@OANo,@KindAtt,@Category,@Shade,@Subject,@PONo,@CoatingThickness,@CreatedBy,GETDATE(),@IsStainlesssteel,@StainlessSteelShade,@BuffingFinish,@Remarks,@SpecifyThickness)", con);
                cmdInsert.Parameters.AddWithValue("@CustomerName", ddlCustomerName.Text);
                cmdInsert.Parameters.AddWithValue("@OANo", ddlOANumbers.Text);
                cmdInsert.Parameters.AddWithValue("@KindAtt", txtkindatt.Text);
                cmdInsert.Parameters.AddWithValue("@Category", ddlCategory.Text);
                cmdInsert.Parameters.AddWithValue("@Shade", txtShade.Text);
                cmdInsert.Parameters.AddWithValue("@Subject", SubjectLine);
                cmdInsert.Parameters.AddWithValue("@PONo", txtPoNo.Text);
                cmdInsert.Parameters.AddWithValue("@CoatingThickness", ddlcoatingThickness.Text);
                cmdInsert.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                cmdInsert.Parameters.AddWithValue("@IsStainlesssteel", IsStainlesssteel);
                cmdInsert.Parameters.AddWithValue("@StainlessSteelShade", txtStainlessSteelShade.Text);
                cmdInsert.Parameters.AddWithValue("@BuffingFinish", txtBuffingFinish.Text);
                cmdInsert.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                cmdInsert.Parameters.AddWithValue("@SpecifyThickness", txtSpecificthickness.Text);
                con.Open();
                cmdInsert.ExecuteNonQuery();
                con.Close();
                if (dgvSubOADtl.Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand("select MAX(id) as ID from tblTestCertificateHdr", con);
                    con.Open();
                    Object MxId = cmd.ExecuteScalar();
                    int HEaaderID = Convert.ToInt32(MxId.ToString());
                    con.Close();
                    Session["ID"] = MxId.ToString();

                    foreach (GridViewRow g1 in dgvSubOADtl.Rows)
                    {
                        Label lblsubOanumber = (Label)dgvSubOADtl.Rows[g1.RowIndex].FindControl("lblsubOanumber");
                        Label lblSize = (Label)dgvSubOADtl.Rows[g1.RowIndex].FindControl("lblSize");
                        Label lblQuantity = (Label)dgvSubOADtl.Rows[g1.RowIndex].FindControl("lblQuantity");

                        SqlCommand cmdSubOAdata = new SqlCommand(@"INSERT INTO tblTestCertificateDtls([HeaderId],[SubOANumber],[EnclosureSize],[Quantity])VALUES('" + HEaaderID + "','" + lblsubOanumber.Text + "','" + lblSize.Text + "','" + lblQuantity.Text + "')", con);
                        con.Open();
                        cmdSubOAdata.ExecuteNonQuery();
                        con.Close();
                        //Pdf();
                    }
                }
                //string modified_URL = "window.open('TestCertificate_PDF.aspx', '_blank');";
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", modified_URL, true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('SUCCESS- Test Certificate Generated Successfully...!');", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('SUCCESS- Test Certificate Generated Successfully...!');window.location.href='TestCertificateList.aspx';", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool ValidateData()
    {
        bool flg = true;
        if (ddlCustomerName.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select Customer !!!');", true);
            flg = false;
            ddlCustomerName.Focus();
        }
        else if (ddlOANumbers.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select OA Numbers !!!');", true);
            flg = false;
            ddlOANumbers.Focus();
        }
        else if (ddlCategory.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select Category !!!');", true);
            flg = false;
            ddlCategory.Focus();
        }
        else if (txtShade.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Enter Shade !!!');", true);
            flg = false; txtShade.Focus();
        }
        else if (ddlcoatingThickness.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select Thickness !!!');", true);
            flg = false; ddlcoatingThickness.Focus();
        }
        else if (dgvSubOADtl.Rows.Count < 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Add SubOA Details !!!');", true);
            flg = false;
        }
        else
        {
            flg = true;
        }
        return flg;
    }

    private void Pdf()
    {
        if (Session["ID"] != null)
        {
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("select * from tblTestCertificateHdr where id = '" + Session["ID"].ToString() + "'", con);
            Da.Fill(Dt);
            StringWriter sw = new StringWriter();
            StringReader sr = new StringReader(sw.ToString());

            Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/Files/") + "TestCertificate.pdf", FileMode.Create));
            //PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);

            doc.Open();

            string imageURL = Server.MapPath("~") + "/img/ExcelEncLogo.png";

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
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Excel Enclosure", 250, 745, 0);
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
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TEST CERTIFICATE", 260, 667, 0);
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

                Paragraph paragraphTable1 = new Paragraph();
                paragraphTable1.SpacingBefore = 120f;
                paragraphTable1.SpacingAfter = 10f;

                PdfPTable table = new PdfPTable(4);

                float[] widths2 = new float[] { 100, 180, 100, 180 };
                table.SetWidths(widths2);
                table.TotalWidth = 560f;
                table.LockedWidth = true;

                var date = DateTime.Now.ToString("yyyy-MM-dd");


                //table.AddCell(new Phrase("Party Name : ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase(PartyName, FontFactory.GetFont("Arial", 9, Font.BOLD)));

                //table.AddCell(new Phrase("Quatition Number :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase(QuatationNumber, FontFactory.GetFont("Arial", 9, Font.BOLD)));

                //table.AddCell(new Phrase("Address", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase(Address, FontFactory.GetFont("Arial", 9, Font.BOLD)));

                //table.AddCell(new Phrase("Quatition Date :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase(QuatationDate, FontFactory.GetFont("Arial", 9, Font.BOLD)));

                //table.AddCell(new Phrase("Kind Att :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase(KindAtt, FontFactory.GetFont("Arial", 9, Font.BOLD)));

                //table.AddCell(new Phrase(" ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase(GSTNo, FontFactory.GetFont("Arial", 9, Font.BOLD)));

                //table.AddCell(new Phrase("Created Date :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase(CreateDate, FontFactory.GetFont("Arial", 9, Font.BOLD)));

                //table.AddCell(new Phrase("Kind Attn. :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                //table.AddCell(new Phrase(KindAtt, FontFactory.GetFont("Arial", 9, Font.BOLD)));

                paragraphTable1.Add(table);
                doc.Add(paragraphTable1);

                //Paragraph paragraphTable2 = new Paragraph();
                //paragraphTable2.SpacingAfter = 0f;
                //table = new PdfPTable(9);
                //float[] widths3 = new float[] { 4f, 40f, 8f, 8f, 8f, 0f, 8f, 8f, 8f };
                //table.SetWidths(widths3);

                //double Ttotal_price = 0;
                //if (Dt.Rows.Count > 0)
                //{
                //    table.TotalWidth = 560f;
                //    table.LockedWidth = true;
                //    table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //    table.AddCell(new Phrase("Name Of Particulars", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //    table.AddCell(new Phrase("Hsn/Sac", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //    table.AddCell(new Phrase("Tax %", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //    table.AddCell(new Phrase("Quantity", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //    table.AddCell(new Phrase("Unit", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //    table.AddCell(new Phrase("Rate", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //    table.AddCell(new Phrase("Disc %", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //    table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));

                //    int rowid = 1;
                //    foreach (DataRow dr in Dt.Rows)
                //    {
                //        table.TotalWidth = 560f;
                //        table.LockedWidth = true;

                //        double Ftotal = Convert.ToDouble(dr["amount"].ToString());
                //        string _ftotal = Ftotal.ToString("##.00");
                //        table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 9)));
                //        table.AddCell(new Phrase(dr["description"].ToString().Replace("<br>", "\n"), FontFactory.GetFont("Arial", 9)));
                //        table.AddCell(new Phrase(dr["hsncode"].ToString(), FontFactory.GetFont("Arial", 9)));
                //        table.AddCell(new Phrase(dr["totaltax"].ToString(), FontFactory.GetFont("Arial", 9)));
                //        table.AddCell(new Phrase(dr["qty"].ToString(), FontFactory.GetFont("Arial", 9)));
                //        table.AddCell(new Phrase(dr["hsncode"].ToString(), FontFactory.GetFont("Arial", 9)));
                //        table.AddCell(new Phrase(dr["rate"].ToString(), FontFactory.GetFont("Arial", 9)));
                //        table.AddCell(new Phrase(dr["discount"].ToString(), FontFactory.GetFont("Arial", 9)));
                //        table.AddCell(new Phrase(_ftotal, FontFactory.GetFont("Arial", 9)));
                //        rowid++;

                //        Ttotal_price += Convert.ToDouble(dr["amount"].ToString());
                //    }

                //}
                //string amount = Ttotal_price.ToString();
                //paragraphTable2.Add(table);
                //doc.Add(paragraphTable2);

                //GST Calculation
                //double GSTamt = Convert.ToDouble(amount) * 9 / 100;

                //string lblCGSTamt = "";
                //string lblSGSTamt = "";
                //string lblIGSTamt = "";
                //string lblWithGSTAmount = "";

                //if (Dt.Rows[0]["Taxation"].ToString() == "outmah")
                //{
                //    lblCGSTamt = "Not Applicable";
                //    lblSGSTamt = "Not Applicable";

                //    double Igtamt = GSTamt + GSTamt;

                //    lblIGSTamt = "Extra as applicable (Presently 18 %)I Amount Rs." + Igtamt.ToString("##.##") + "";
                //    var tot = Convert.ToDouble(GSTamt) + Convert.ToDouble(GSTamt) + Convert.ToDouble(amount);
                //    lblWithGSTAmount = tot.ToString();
                //}
                //if (Dt.Rows[0]["Taxation"].ToString() == "inmah")
                //{
                //    lblCGSTamt = "Extra as applicable (Presently 9 %)I Amount Rs." + GSTamt.ToString("##.##") + "";
                //    lblSGSTamt = "Extra as applicable (Presently 9 %)I Amount Rs." + GSTamt.ToString("##.##") + "";
                //    lblIGSTamt = "Not Applicable";
                //    var tot = Convert.ToDouble(GSTamt) + Convert.ToDouble(GSTamt) + Convert.ToDouble(amount);
                //    lblWithGSTAmount = tot.ToString();
                //}
                //if (Dt.Rows[0]["Taxation"].ToString() == "outind")
                //{
                //    lblCGSTamt = "0.00";
                //    lblSGSTamt = "0.00";
                //    var tot = amount;
                //    lblWithGSTAmount = tot.ToString();
                //}


                //Space
                //Paragraph paragraphTable3 = new Paragraph();

                //string[] items = { "Goods once sold will not be taken back or exchange. \b",
                //        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                //        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                //        };

                //Font font12 = FontFactory.GetFont("Arial", 12, Font.BOLD);
                //Font font10 = FontFactory.GetFont("Arial", 10, Font.BOLD);
                //Paragraph paragraph = new Paragraph("", font12);

                //for (int i = 0; i < items.Length; i++)
                //{
                //    paragraph.Add(new Phrase("", font10));
                //}

                //table = new PdfPTable(9);
                //table.TotalWidth = 560f;
                //table.LockedWidth = true;
                //table.SetWidths(new float[] { 4f, 40f, 8f, 8f, 8f, 0f, 8f, 8f, 8f });
                //table.AddCell(paragraph);
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                ////table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));

                //doc.Add(table);

                //Add Total Row start
                //Paragraph paragraphTable5 = new Paragraph();

                //string[] itemsss = { "Goods once sold will not be taken back or exchange. \b",
                //        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                //        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                //        };

                //Font font13 = FontFactory.GetFont("Arial", 12, Font.BOLD);
                //Font font11 = FontFactory.GetFont("Arial", 10, Font.BOLD);
                //Paragraph paragraphh = new Paragraph("", font12);



                //for (int i = 0; i < items.Length; i++)
                //{
                //    paragraph.Add(new Phrase("", font10));
                //}

                //table = new PdfPTable(3);
                //table.TotalWidth = 560f;
                //table.LockedWidth = true;

                //paragraph.Alignment = Element.ALIGN_RIGHT;

                //table.SetWidths(new float[] { 0f, 76f, 12f });
                //table.AddCell(paragraph);
                //PdfPCell cell = new PdfPCell(new Phrase("Sub Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //table.AddCell(cell);
                //PdfPCell cell11 = new PdfPCell(new Phrase(amount, FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //cell11.HorizontalAlignment = Element.ALIGN_RIGHT;
                //table.AddCell(cell11);
                //doc.Add(table);



                //Grand total Row STart
                //Paragraph paragraphTable17 = new Paragraph();
                //paragraphTable5.SpacingAfter = 0f;

                //string[] itemm = { "Goods once sold will not be taken back or exchange. \b",
                //        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                //        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                //        };

                //Font font16 = FontFactory.GetFont("Arial", 12, Font.BOLD);
                //Font font17 = FontFactory.GetFont("Arial", 10, Font.BOLD);
                //Paragraph paragraphhhhh = new Paragraph("", font12);

                ////paragraphh.SpacingAfter = 10f;

                //for (int i = 0; i < items.Length; i++)
                //{
                //    paragraph.Add(new Phrase("", font10));
                //}

                //table = new PdfPTable(3);
                //table.TotalWidth = 560f;
                //table.LockedWidth = true;

                //table.SetWidths(new float[] { 0f, 76f, 12f });
                //table.AddCell(paragraph);
                //PdfPCell cell44 = new PdfPCell(new Phrase("Total Amount With Total Tax", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //cell44.HorizontalAlignment = Element.ALIGN_RIGHT;
                //table.AddCell(cell44);
                //PdfPCell cell55 = new PdfPCell(new Phrase(lblWithGSTAmount, FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //cell55.HorizontalAlignment = Element.ALIGN_RIGHT;
                //table.AddCell(cell55);
                //doc.Add(table);



                //Paragraph paragraphTable99 = new Paragraph();

                ////Puja Enterprises Sign
                //string[] itemss = {
                //"Payment Term                 :    100% Against Proforma Invoice Prior To Dispatch\n",
                //"CGST                                :    "+lblCGSTamt+"\n",
                //"SGST                                :    "+lblSGSTamt+" \n",
                //"IGST                                 :    "+lblIGSTamt+" \n",
                //"VALIDITY OF OFFER      :    30 Day Form The Date Of Quotation And Confirmation Therafter \n",
                //"DELIVERY PERIOD         :    2-3 Weeks From Date Of Receiving Approved GA Drg \n",
                //"TRANSFORMATION        :    Extra As Application \n",
                //"STANDARD PACKING    :    The Enclosures Will Be Packed In 2 Ply Corrugated Sheet The Charges For Teh Same Are Included In Above Price \n",
                //"SPECIAL PACKING         :    Any Special Will Be Supplied At Extra Cost \n",
                //"INSPECTION                    :    You/Your Representative Can Inspect The Enclosure At Our Factory\n",
                //"NOTE                                :    Price May Change If Any Addition Is Done In The Process Of Drawing Approval \n",



                //        };

                //Font font14 = FontFactory.GetFont("Arial", 11);
                //Font font15 = FontFactory.GetFont("Arial", 8);
                //Paragraph paragraphhh = new Paragraph(" Remarks :\n\n", font12);


                //for (int i = 0; i < itemss.Length; i++)
                //{
                //    paragraphhh.Add(new Phrase("\u2022 \u00a0" + itemss[i] + "\n", font15));
                //}

                //table = new PdfPTable(1);
                //table.TotalWidth = 560f;
                //table.LockedWidth = true;
                //table.SetWidths(new float[] { 560f });

                //table.AddCell(paragraphhh);
                ////table.AddCell(new Phrase("Puja Enterprises \n\n\n\n         Sign", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                ////table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //doc.Add(table);

                //Paragraph paragraphTable10000 = new Paragraph();

                ////Puja Enterprises Sign
                //string[] itemss4 = {
                //"Payment Term     ",

                //        };

                //Font font144 = FontFactory.GetFont("Arial", 11);
                //Font font155 = FontFactory.GetFont("Arial", 8);
                //Paragraph paragraphhhhhff = new Paragraph(" Remarks :\n\n", font12);


                //for (int i = 0; i < itemss4.Length; i++)
                //{
                //    paragraphhhhhff.Add(new Phrase("\u2022 \u00a0" + itemss4[i] + "\n", font155));
                //}

                table = new PdfPTable(2);
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 300f, 100f });

                //table.AddCell(paragraphhhhhff);
                table.AddCell(new Phrase(" ", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("         For Excel Enclosures \n\n\n\n\n\n         Authorised Signature", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                doc.Add(table);

                doc.Close();


                Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/Files/") + "TestCertificate.pdf");

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


                string empFilename = "TestCertificate" + DateTime.Now.ToShortDateString() + ".pdf";

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
        else
        {

        }
    }

    protected void chkIsStainlesssteel_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkIsStainlesssteel.Checked == true)
            {
                SS1.Visible = true;
                SS2.Visible = true;
            }
            else
            {
                SS1.Visible = false;
                SS2.Visible = false;

                txtStainlessSteelShade.Text = "";
                txtBuffingFinish.Text = "";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlcoatingThickness_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcoatingThickness.SelectedItem.Text == "Specify")
            {
                divSpecificThickness.Visible = true;
            }
            else
            {
                divSpecificThickness.Visible = false;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
}
