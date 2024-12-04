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

public partial class Admin_QuotationCat2 : System.Web.UI.Page
{
    static int quotationid; static string oldfile1, oldfile2, date;
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
            fillddlpaymentterm();
            ddlvalidityofoffer.Text = "30 days from the date of quotation and confirmation thereafter";
            ddldeliveryperiod.Text = "2-3 weeks from date of purchase order";
            ddltransportation.Text = "Extra as applicable";
            ddlStandardPacking.Text = "The enclosures will be packed in 2 ply corrugated sheet. The charges for the same are included in above price";
            ddlSpecialPacking.Text = "Any special packing will be supplied at extra cost";
            ddlinspection.Text = "You/Your representative can inspect the enclosure at our factory";

            if (Request.QueryString["Ccode"] != null)
            {
                GenerateCode();
                GetCompanyDataByName(Decrypt(Request.QueryString["Ccode"].ToString()));
            }

            if (Request.QueryString["cdd"] != null)
            {
                GetQuotationdata();
            }
            if (Request.QueryString["cdd"] == null)
            {
                oldfile1 = ""; oldfile2 = "";
            }
            Getemail();

            txtCGSTamt.Attributes.Add("readonly", "readonly");
            txtSGSTamt.Attributes.Add("readonly", "readonly");
            txtIGSTamt.Attributes.Add("readonly", "readonly");
            txtAmt1.Attributes.Add("readonly", "readonly");
        }
        }
    }

    static string myconstype; static int myquotationid;
    #region Fill Data for Update
    private void GetQuotationdata()
    {
        SqlCommand cmdall = new SqlCommand("SP_GetQuotationAll", con);
        cmdall.CommandType = CommandType.StoredProcedure;
        cmdall.Parameters.AddWithValue("@id", +Convert.ToInt32(Decrypt(Request.QueryString["cdd"].ToString())));
        cmdall.Parameters.AddWithValue("@constype", "PartofControlPanel");

        SqlDataAdapter adall = new SqlDataAdapter(cmdall);
        DataTable dtall = new DataTable();
        adall.Fill(dtall);

        if (dtall.Rows.Count > 0)
        {
            myquotationid = Convert.ToInt32(dtall.Rows[0]["id"].ToString());
            txtcname.Text = dtall.Rows[0]["partyname"].ToString();
            txQutno.Text = dtall.Rows[0]["quotationno"].ToString();
            GetCompanyDataByName(dtall.Rows[0]["ccode"].ToString());
            ddlkindatt.Text = dtall.Rows[0]["kindatt"].ToString();
            txtdate.Text = dtall.Rows[0]["date"].ToString();
            txtshippingaddress.Text = dtall.Rows[0]["address"].ToString();
            txtremark.Text = dtall.Rows[0]["remark"].ToString();

            txtsr1.Text = dtall.Rows[0]["sno"].ToString();
            txtHsn1.Text = dtall.Rows[0]["hsncode"].ToString();
            txtQty1.Text = dtall.Rows[0]["qty"].ToString();
            txtRate1.Text = dtall.Rows[0]["rate"].ToString();
            txtdisc1.Text = dtall.Rows[0]["discount"].ToString();

            txtCGST.Text = dtall.Rows[0]["CGST"].ToString();
            txtSGST.Text = dtall.Rows[0]["SGST"].ToString();
            txtIGST.Text = dtall.Rows[0]["IGST"].ToString();
            txtCGSTamt.Text = dtall.Rows[0]["CGSTamt"].ToString();
            txtSGSTamt.Text = dtall.Rows[0]["SGSTamt"].ToString();
            txtIGSTamt.Text = dtall.Rows[0]["IGSTamt"].ToString();

            txtAmt1.Text = dtall.Rows[0]["amount"].ToString();

            ddlpaymentterm.SelectedItem.Text = dtall.Rows[0]["paymentterm1"].ToString();

            ddlvalidityofoffer.Text = dtall.Rows[0]["validityofoffer"].ToString();
            ddldeliveryperiod.Text = dtall.Rows[0]["deliveryperiod"].ToString();
            ddltransportation.Text = dtall.Rows[0]["transportation"].ToString();
            ddlStandardPacking.Text = dtall.Rows[0]["standardpackaging"].ToString();
            ddlSpecialPacking.Text = dtall.Rows[0]["specialpackaging"].ToString();
            ddlinspection.Text = dtall.Rows[0]["inspection"].ToString();

            if (!string.IsNullOrEmpty(dtall.Rows[0]["filename1"].ToString()))
            {
                lblimg.Visible = true;
                lblimg.Text = "<u>Attachment </u>: " + dtall.Rows[0]["filename1"].ToString();
                oldfile1 = dtall.Rows[0]["filename1"].ToString();
                oldfile2 = dtall.Rows[0]["filename2"].ToString();
            }

            myconstype = dtall.Rows[0]["Constructiontype"].ToString();


            int temcount = 0;
            temcount = dtall.Rows.Count;

            //1     
            if (temcount > 2 || temcount == 2)
            {
                txtspecify1cat1.Text = dtall.Rows[1]["categoryname"].ToString();
                txtspecify1cat2.Text = dtall.Rows[1]["category1"].ToString();
                txtspecify1cat3.Text = dtall.Rows[1]["category2"].ToString();
                if (txtspecify1cat1.Text != "")
                {
                    ChkSpecify1.Checked = true;
                }
            }
            //2
            if (temcount > 3 || temcount == 3)
            {
                txtspecify2cat1.Text = dtall.Rows[2]["categoryname"].ToString();
                txtspecify2cat2.Text = dtall.Rows[2]["category1"].ToString();
                txtspecify2cat3.Text = dtall.Rows[2]["category2"].ToString();
                if (txtspecify2cat1.Text != "")
                {
                    ChkSpecify2.Checked = true;
                }
            }
            //3
            if (temcount > 4 || temcount == 4)
            {
                txtspecify3cat1.Text = dtall.Rows[3]["categoryname"].ToString();
                txtspecify3cat2.Text = dtall.Rows[3]["category1"].ToString();
                txtspecify3cat3.Text = dtall.Rows[3]["category2"].ToString();
                if (txtspecify3cat1.Text != "")
                {
                    ChkSpecify3.Checked = true;
                }
            }
            //4
            if (temcount > 5 || temcount == 5)
            {
                txtspecify4cat1.Text = dtall.Rows[4]["categoryname"].ToString();
                txtspecify4cat2.Text = dtall.Rows[4]["category1"].ToString();
                txtspecify4cat3.Text = dtall.Rows[4]["category2"].ToString();
                if (txtspecify4cat1.Text != "")
                {
                    ChkSpecify4.Checked = true;
                }
            }
            //5
            if (temcount > 6 || temcount == 6)
            {
                txtspecify5cat1.Text = dtall.Rows[5]["categoryname"].ToString();
                txtspecify5cat2.Text = dtall.Rows[5]["category1"].ToString();
                txtspecify5cat3.Text = dtall.Rows[5]["category2"].ToString();
                if (txtspecify5cat1.Text != "")
                {
                    ChkSpecify5.Checked = true;
                }
            }
            //6
            if (temcount > 7 || temcount == 7)
            {
                txtspecify6cat1.Text = dtall.Rows[6]["categoryname"].ToString();
                txtspecify6cat2.Text = dtall.Rows[6]["category1"].ToString();
                txtspecify6cat3.Text = dtall.Rows[6]["category2"].ToString();
                if (txtspecify6cat1.Text != "")
                {
                    ChkSpecify6.Checked = true;
                }
            }
            //7                 
            if (temcount > 8 || temcount == 8)
            {
                txtspecify7cat1.Text = dtall.Rows[7]["categoryname"].ToString();
                txtspecify7cat2.Text = dtall.Rows[7]["category1"].ToString();
                txtspecify7cat3.Text = dtall.Rows[7]["category2"].ToString();
                if (txtspecify7cat1.Text != "")
                {
                    ChkSpecify7.Checked = true;
                }
            }
            //8                 
            if (temcount > 9 || temcount == 9)
            {
                txtspecify8cat1.Text = dtall.Rows[8]["categoryname"].ToString();
                txtspecify8cat2.Text = dtall.Rows[8]["category1"].ToString();
                txtspecify8cat3.Text = dtall.Rows[8]["category2"].ToString();
                if (txtspecify8cat1.Text != "")
                {
                    ChkSpecify8.Checked = true;
                }
            }
            //9
            if (temcount > 10 || temcount == 10)
            {
                txtspecify9cat1.Text = dtall.Rows[9]["categoryname"].ToString();
                txtspecify9cat2.Text = dtall.Rows[9]["category1"].ToString();
                txtspecify9cat3.Text = dtall.Rows[9]["category2"].ToString();
                if (txtspecify9cat1.Text != "")
                {
                    ChkSpecify9.Checked = true;
                }
            }
            //10
            if (temcount > 11 || temcount == 11)
            {
                txtspecify10cat1.Text = dtall.Rows[10]["categoryname"].ToString();
                txtspecify10cat2.Text = dtall.Rows[10]["category1"].ToString();
                txtspecify10cat3.Text = dtall.Rows[10]["category2"].ToString();
                if (txtspecify10cat1.Text != "")
                {
                    ChkSpecify10.Checked = true;
                }
            }
            //11                    
            if (temcount > 12 || temcount == 12)
            {
                txtspecify11cat1.Text = dtall.Rows[11]["categoryname"].ToString();
                txtspecify11cat2.Text = dtall.Rows[11]["category1"].ToString();
                txtspecify11cat3.Text = dtall.Rows[11]["category2"].ToString();
                if (txtspecify11cat1.Text != "")
                {
                    ChkSpecify11.Checked = true;
                }
            }
            //12                   
            if (temcount > 13 || temcount == 13)
            {
                txtspecify12cat1.Text = dtall.Rows[12]["categoryname"].ToString();
                txtspecify12cat2.Text = dtall.Rows[12]["category1"].ToString();
                txtspecify12cat3.Text = dtall.Rows[12]["category2"].ToString();
                if (txtspecify12cat1.Text != "")
                {
                    ChkSpecify12.Checked = true;
                }
            }
            //13                    
            if (temcount > 14 || temcount == 14)
            {
                txtspecify13cat1.Text = dtall.Rows[13]["categoryname"].ToString();
                txtspecify13cat2.Text = dtall.Rows[13]["category1"].ToString();
                txtspecify13cat3.Text = dtall.Rows[13]["category2"].ToString();
                if (txtspecify13cat1.Text != "")
                {
                    ChkSpecify13.Checked = true;
                }
            }

            //14
            if (temcount > 15 || temcount == 15)
            {
                txtspecify14cat1.Text = dtall.Rows[14]["categoryname"].ToString();
                txtspecify14cat2.Text = dtall.Rows[14]["category1"].ToString();
                txtspecify14cat3.Text = dtall.Rows[14]["category2"].ToString();
                if (txtspecify14cat1.Text != "")
                {
                    ChkSpecify14.Checked = true;
                }
            }
            //15
            if (temcount > 15 || temcount == 16)
            {
                txtspecify15cat1.Text = dtall.Rows[15]["categoryname"].ToString();
                txtspecify15cat2.Text = dtall.Rows[15]["category1"].ToString();
                txtspecify15cat3.Text = dtall.Rows[15]["category2"].ToString();
                if (txtspecify15cat1.Text != "")
                {
                    ChkSpecify15.Checked = true;
                }
            }
        }

        /////////////
    }

    #endregion Fill Data

    private void Getemail()
    {
        SqlCommand cmdget = new SqlCommand("SP_GetEmail", con);
        cmdget.CommandType = CommandType.StoredProcedure;
        cmdget.Parameters.AddWithValue("@oname", ddlkindatt.SelectedItem.Text);

        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdget);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        if (dataTable.Rows.Count > 0)
        {
            lblemail.Text = dataTable.Rows[0]["Email"].ToString();
        }
    }

    private void fillddlpaymentterm()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select distinct paymentterm,transportation,standardpackaging,specialpackaging,deliveryperiod,validityofoffer,inspection from QuotationMainFooter", con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);

        if (dtpt.Rows.Count > 0)
        {
            ////////1
            List<string> lstpaytm = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["paymentterm"].ToString() != "")
                {
                    lstpaytm.Add(dtpt.Rows[i]["paymentterm"].ToString());
                }
            }
            ddlpaymentterm.DataSource = lstpaytm;
            ddlpaymentterm.DataBind();

            ///////////2
            List<string> lstdelp = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["deliveryperiod"].ToString() != "")
                {
                    lstdelp.Add(dtpt.Rows[i]["deliveryperiod"].ToString());
                }
            }
            ddldeliveryperiod.DataSource = lstdelp;
            ddldeliveryperiod.DataBind();

            ////3
            List<string> lstval = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["validityofoffer"].ToString() != "")
                {
                    lstval.Add(dtpt.Rows[i]["validityofoffer"].ToString());
                }
            }
            ddlvalidityofoffer.DataSource = lstval;
            ddlvalidityofoffer.DataBind();


            ////4
            List<string> lsttrans = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["transportation"].ToString() != "")
                {
                    lsttrans.Add(dtpt.Rows[i]["transportation"].ToString());
                }
            }
            ddltransportation.DataSource = lsttrans;
            ddltransportation.DataBind();

            ////5
            List<string> lststandpkg = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["standardpackaging"].ToString() != "")
                {
                    lststandpkg.Add(dtpt.Rows[i]["standardpackaging"].ToString());
                }
            }
            ddlStandardPacking.DataSource = lststandpkg;
            ddlStandardPacking.DataBind();

            ////6
            List<string> lstspecpack = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["specialpackaging"].ToString() != "")
                {
                    lstspecpack.Add(dtpt.Rows[i]["specialpackaging"].ToString());
                }
            }
            ddlSpecialPacking.DataSource = lstspecpack;
            ddlSpecialPacking.DataBind();

            ////7
            List<string> lstinspection = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["inspection"].ToString() != "")
                {
                    lstinspection.Add(dtpt.Rows[i]["inspection"].ToString());
                }
            }
            ddlinspection.DataSource = lstinspection;
            ddlinspection.DataBind();
        }
    }

    static string regdate = string.Empty;

    protected void GetCompanyDataByName(string ccode)
    {
        string query = "";
        query = "SELECT top 1 [id],[ccode],[cname],[oname1],[oname2],[oname3],[oname4],[oname5],[email1],[mobile1],[billingaddress],[shippingaddress],[gstno],[paymentterm1] FROM [Company] where [isdeleted]=0 and status=0 and ccode='" + ccode.Trim() + "' order by id desc ";

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            HFccode.Value = dt.Rows[0]["ccode"].ToString();
            txtcname.Text = dt.Rows[0]["cname"].ToString();
            List<string> kind = new List<string> { };
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname1"].ToString()))
            {
                kind.Add(dt.Rows[0]["oname1"].ToString());
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname2"].ToString()))
            {
                kind.Add(dt.Rows[0]["oname2"].ToString());
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname3"].ToString()))
            {
                kind.Add(dt.Rows[0]["oname3"].ToString());
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname4"].ToString()))
            {
                kind.Add(dt.Rows[0]["oname4"].ToString());
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname5"].ToString()))
            {
                kind.Add(dt.Rows[0]["oname5"].ToString());
            }
            ddlkindatt.DataSource = kind;
            ddlkindatt.DataBind();
            ddlkindatt.Items.Insert(0, "Select");
            ddlpaymentterm.Text = dt.Rows[0]["paymentterm1"].ToString();
            txtshippingaddress.Text = dt.Rows[0]["billingaddress"].ToString();
        }
        else
        {
            ddlkindatt.DataSource = null;
            ddlkindatt.DataBind();
            ddlkindatt.Items.Insert(0, "Select");
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

    static string ComCode = string.Empty;
    protected void GenerateCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [Quotation]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //ComCode = (Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1).ToString()+ DateTime.Now.ToString("MM");
            txQutno.Text = (Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1).ToString() + "-" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.AddYears(1).ToString("yy");
        }
        else
        {
            ComCode = string.Empty;
        }
    }

    static string ComCodeUpdate = string.Empty; static string visitingcardPath = string.Empty;
    protected void btnadd_Click(object sender, EventArgs e)
    {
    }

    protected void txtcname_TextChanged(object sender, EventArgs e)
    {
        GetCompanyDataByName(txtcname.Text);
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompanyList(string prefixText, int count)
    {
        return AutoFillCompanyName(prefixText);
    }

    public static List<string> AutoFillCompanyName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [cname] from [Company] where " + "cname like @Search + '%' and status=0 and [isdeleted]=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["cname"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void ddlkindatt_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getemail();
    }

    protected void btnaddperticular_Click(object sender, EventArgs e)
    {
        PopupAddDetail.Visible = true;
        this.modelprofile.Show();
    }

    static string constype = string.Empty; static int validation;
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string action = "", quotationno = "";
        DataTable dtConstructionType = new DataTable();
        dtConstructionType.Columns.AddRange(new DataColumn[9] { new DataColumn("quotationno", typeof(string)),new DataColumn("quotationid", typeof(Int32)),
                    new DataColumn("categoryname",typeof(string)),new DataColumn("category1", typeof(string)),new DataColumn("category2", typeof(string)),
                    new DataColumn("category3", typeof(string)),new DataColumn("category4",typeof(string)),new DataColumn("category5", typeof(string)),new DataColumn("category6", typeof(string)) });

        #region Insert into QuotationMain

        string filename1 = "", filename2 = "";
        if (FileUploadrefdoc.HasFile)
        {
            string filePath = FileUploadrefdoc.PostedFile.FileName;
            filename1 = Path.GetFileName(filePath);
            string[] avc = filename1.Split('.');
            string ext = Path.GetExtension(filename1);
            string contenttype = String.Empty;
            string timest = DateTime.Now.ToString("ddMMyyyyhhmmssfff");
            filename2 = avc[0] + timest + ext;
            FileUploadrefdoc.SaveAs(Server.MapPath("~/RefDocument/") + filename2);
        }

        if (FileUploadrefdoc.HasFile == false && oldfile1 != "" && oldfile2 != "")
        {
            filename1 = oldfile1; filename2 = oldfile2;
        }

        SqlCommand cmdquotation = new SqlCommand("SP_QuotationIns", con);
        cmdquotation.CommandType = CommandType.StoredProcedure;

        cmdquotation.Parameters.AddWithValue("@action", "insert");

        cmdquotation.Parameters.AddWithValue("@partyname", Request.Form[txtcname.UniqueID].ToString());
        cmdquotation.Parameters.AddWithValue("@ccode", HFccode.Value);
        cmdquotation.Parameters.AddWithValue("@kindatt", ddlkindatt.Text);
        cmdquotation.Parameters.AddWithValue("@date", Request.Form[txtdate.UniqueID].ToString());
        cmdquotation.Parameters.AddWithValue("@address", txtshippingaddress.Text);
        cmdquotation.Parameters.AddWithValue("@remark", txtremark.Text);
        cmdquotation.Parameters.AddWithValue("@Toatlamt", txtAmt1.Text);

        cmdquotation.Parameters.AddWithValue("@Maincat", "Enclosure For Control Panel");
        cmdquotation.Parameters.AddWithValue("@paymentterm1", ddlpaymentterm.SelectedItem.Text);
        cmdquotation.Parameters.AddWithValue("@validityofoffer", ddlvalidityofoffer.SelectedItem.Text);
        cmdquotation.Parameters.AddWithValue("@deliveryperiod", ddldeliveryperiod.SelectedItem.Text);
        cmdquotation.Parameters.AddWithValue("@standardpackaging", ddlStandardPacking.SelectedItem.Text);
        cmdquotation.Parameters.AddWithValue("@specialpackaging", ddlSpecialPacking.SelectedItem.Text);
        cmdquotation.Parameters.AddWithValue("@inspection", ddlinspection.SelectedItem.Text);
        cmdquotation.Parameters.AddWithValue("@transportation", ddltransportation.SelectedItem.Text);
        cmdquotation.Parameters.AddWithValue("@filename1", filename1);
        cmdquotation.Parameters.AddWithValue("@filename2", filename2);

        if (Request.QueryString["cdd"] == null)
        {
            quotationno = Request.Form[txQutno.UniqueID].ToString();
            cmdquotation.Parameters.AddWithValue("@quotationno", Request.Form[txQutno.UniqueID].ToString());
        }
        else
        {
            string quot11 = Request.Form[txQutno.UniqueID].ToString();
            if (!string.IsNullOrEmpty(quot11))
            {
                string[] qt22 = quot11.Split('/');
                string[] qt33 = qt22[0].Split('-');

                SqlDataAdapter quotad = new SqlDataAdapter("select top 1 quotationno from QuotationMain where quotationno like '" + qt33[0] + "-" + "%' order by id desc", con);
                DataTable quotdt = new DataTable();
                quotad.Fill(quotdt);

                if (quotdt.Rows.Count > 0)
                {
                    string quot = quotdt.Rows[0]["quotationno"].ToString();
                    string[] qt = quot.Split('/');
                    string[] qt1 = qt[0].Split('-');
                    int nextquot = (Convert.ToInt32(qt1[1])) + 1;
                    quotationno = qt1[0] + "-" + nextquot.ToString() + "/" + qt[1].ToString();
                }
                cmdquotation.Parameters.AddWithValue("@quotationno", quotationno);
            }
        }

        cmdquotation.Parameters.AddWithValue("@Constructiontype", "PartofControlPanel");
        cmdquotation.Parameters.AddWithValue("@sessionname", Session["name"].ToString());

        cmdquotation.Parameters.Add("@myquotationid", SqlDbType.Int).Direction = ParameterDirection.Output;

        con.Open();
        cmdquotation.ExecuteNonQuery();
        con.Close();

        quotationid = (int)cmdquotation.Parameters["@myquotationid"].Value;

        string totaltax = "";
        totaltax = ((Convert.ToDouble(Request.Form[txtCGSTamt.UniqueID].ToString())) + (Convert.ToDouble(Request.Form[txtSGSTamt.UniqueID].ToString())) + (Convert.ToDouble(Request.Form[txtIGSTamt.UniqueID].ToString()))).ToString();

        SqlCommand cmdquotdata = new SqlCommand(@"INSERT INTO [QuotationData]([quotationid],[quotationno],[sno]
           ,[description],[hsncode],[qty],[rate],[CGST],[SGST],[IGST],[CGSTamt],[SGSTamt],[IGSTamt],[totaltax],[discount],[amount]) VALUES (" + quotationid + ",'" + txQutno.Text + "','" + txtsr1.Text + "','','" + txtHsn1.Text + "','" + txtQty1.Text + "','" + txtRate1.Text + "','" + txtCGST.Text + "','" + txtSGST.Text + "','" + txtIGST.Text + "','" + Request.Form[txtCGSTamt.UniqueID].ToString() + "','" + Request.Form[txtSGSTamt.UniqueID].ToString() + "','" + Request.Form[txtIGSTamt.UniqueID].ToString() + "'," + totaltax + ",'" + txtdisc1.Text + "','" + Request.Form[txtAmt1.UniqueID].ToString() + "')", con);
        con.Open();
        cmdquotdata.ExecuteNonQuery();
        con.Close();

        #endregion

        #region Specify Material

        if (ChkSpecify1.Checked == false && ChkSpecify2.Checked == false
            && ChkSpecify3.Checked == false && ChkSpecify4.Checked == false
            && ChkSpecify5.Checked == false && ChkSpecify6.Checked == false
            && ChkSpecify7.Checked == false && ChkSpecify8.Checked == false
            && ChkSpecify9.Checked == false && ChkSpecify10.Checked == false)
        {
            validation = 1;
        }

        action = "insertPartofcontrolpanel";
        //1
        if (ChkSpecify1.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify1cat2.Text != "")
            {
                first = txtspecify1cat2.Text;
                sentence = txtspecify1cat1.Text + ": " + first + " ";
            }
            if (txtspecify1cat3.Text != "")
            {
                second = txtspecify1cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify1cat1.Text, first, second, "", "", "", sentence);
        }
        //2
        if (ChkSpecify2.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify2cat2.Text != "")
            {
                first = txtspecify2cat2.Text;
                sentence = txtspecify2cat1.Text + ": " + first + " ";
            }
            if (txtspecify2cat3.Text != "")
            {
                second = txtspecify2cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify2cat1.Text, first, second, "", "", "", sentence);
        }
        //3
        if (ChkSpecify3.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify3cat2.Text != "")
            {
                first = txtspecify3cat2.Text;
                sentence = txtspecify3cat1.Text + ": " + first + " ";
            }
            if (txtspecify3cat3.Text != "")
            {
                second = txtspecify3cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify3cat1.Text, first, second, "", "", "", sentence);
        }
        //4
        if (ChkSpecify4.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify4cat2.Text != "")
            {
                first = txtspecify4cat2.Text;
                sentence = txtspecify4cat1.Text + ": " + first + " ";
            }
            if (txtspecify4cat3.Text != "")
            {
                second = txtspecify4cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify4cat1.Text, first, second, "", "", "", sentence);
        }
        //5
        if (ChkSpecify5.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify5cat2.Text != "")
            {
                first = txtspecify5cat2.Text;
                sentence = txtspecify5cat1.Text + ": " + first + " ";
            }
            if (txtspecify5cat3.Text != "")
            {
                second = txtspecify5cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify5cat1.Text, first, second, "", "", "", sentence);
        }
        //6
        if (ChkSpecify6.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify6cat2.Text != "")
            {
                first = txtspecify6cat2.Text;
                sentence = txtspecify6cat1.Text + ": " + first + " ";
            }
            if (txtspecify6cat3.Text != "")
            {
                second = txtspecify6cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify6cat1.Text, first, second, "", "", "", sentence);
        }
        //7
        if (ChkSpecify7.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify7cat2.Text != "")
            {
                first = txtspecify7cat2.Text;
                sentence = txtspecify7cat1.Text + ": " + first + " ";
            }
            if (txtspecify7cat3.Text != "")
            {
                second = txtspecify7cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify7cat1.Text, first, second, "", "", "", sentence);
        }
        //8
        if (ChkSpecify8.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify8cat2.Text != "")
            {
                first = txtspecify8cat2.Text;
                sentence = txtspecify8cat1.Text + ": " + first + " ";
            }
            if (txtspecify8cat3.Text != "")
            {
                second = txtspecify8cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify8cat1.Text, first, second, "", "", "", sentence);
        }
        //9
        if (ChkSpecify9.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify9cat2.Text != "")
            {
                first = txtspecify9cat2.Text;
                sentence = txtspecify9cat1.Text + ": " + first + " ";
            }
            if (txtspecify9cat3.Text != "")
            {
                second = txtspecify9cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify9cat1.Text, first, second, "", "", "", sentence);
        }
        //10
        if (ChkSpecify10.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify10cat2.Text != "")
            {
                first = txtspecify10cat2.Text;
                sentence = txtspecify10cat1.Text + ": " + first + " ";
            }
            if (txtspecify10cat3.Text != "")
            {
                second = txtspecify10cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify10cat1.Text, first, second, "", "", "", sentence);
        }
        //11
        if (ChkSpecify11.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify11cat2.Text != "")
            {
                first = txtspecify11cat2.Text;
                sentence = txtspecify11cat1.Text + ": " + first + " ";
            }
            if (txtspecify11cat3.Text != "")
            {
                second = txtspecify11cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify11cat1.Text, first, second, "", "", "", sentence);
        }
        //12
        if (ChkSpecify12.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify12cat2.Text != "")
            {
                first = txtspecify12cat2.Text;
                sentence = txtspecify12cat1.Text + ": " + first + " ";
            }
            if (txtspecify12cat3.Text != "")
            {
                second = txtspecify12cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify12cat1.Text, first, second, "", "", "", sentence);
        }
        //13
        if (ChkSpecify13.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify13cat2.Text != "")
            {
                first = txtspecify13cat2.Text;
                sentence = txtspecify13cat1.Text + ": " + first + " ";
            }
            if (txtspecify13cat3.Text != "")
            {
                second = txtspecify13cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify13cat1.Text, first, second, "", "", "", sentence);
        }
        //14
        if (ChkSpecify2.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify14cat2.Text != "")
            {
                first = txtspecify14cat2.Text;
                sentence = txtspecify14cat1.Text + ": " + first + " ";
            }
            if (txtspecify14cat3.Text != "")
            {
                second = txtspecify14cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify14cat1.Text, first, second, "", "", "", sentence);
        }
        //15
        if (ChkSpecify15.Checked == true)
        {
            validation = 0;
            string first = "", second = "", sentence = "";

            if (txtspecify15cat2.Text != "")
            {
                first = txtspecify15cat2.Text;
                sentence = txtspecify15cat1.Text + ": " + first + " ";
            }
            if (txtspecify15cat3.Text != "")
            {
                second = txtspecify15cat3.Text;
                sentence += second;
            }
            dtConstructionType.Rows.Add(quotationno, quotationid, txtspecify15cat1.Text, first, second, "", "", "", sentence);
        }

        #endregion

        if (validation == 0)
        {
            StringBuilder sbdescription = new StringBuilder();

            int sno = 2;

            sbdescription.Append("1.Part of Control Panel <br>");

            for (int i = 0; i < dtConstructionType.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dtConstructionType.Rows[i]["category6"].ToString()) || dtConstructionType.Rows[i]["category6"].ToString() != "")
                {
                    sbdescription.Append((sno) + ". " + dtConstructionType.Rows[i]["category6"].ToString() + "<br>");
                    sno++;
                }
            }

            SqlCommand cmd = new SqlCommand("SP_ConstructionType", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tbljbbox", dtConstructionType);
            cmd.Parameters.AddWithValue("@action", action);
            cmd.Parameters.AddWithValue("@descriptionall", sbdescription.ToString());
            cmd.Parameters.AddWithValue("@quotationid", quotationid);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            if (Chkemail.Checked == true)
            {
                Sendemail();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", " alert('All details saved successfully !!!');window.location='QuotationList.aspx';", true);
            validation = 0;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Please Select atleast one field');", true);
            PopupAddDetail.Visible = true;
            this.modelprofile.Show();
        }
    }

    string filename = "", party = "";
    private void Sendemail()
    {
        string servername = "", dbname = "", userid = "", pass = "";
        byte[] attachment; StringBuilder sb = new StringBuilder();
        //TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        //TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        //ConnectionInfo crConnectionInfo = new ConnectionInfo();
        //Tables CrTables;
        DataTable dtt = new DataTable();
        List<byte[]> files = new List<byte[]>();

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        dbname = builder.InitialCatalog;
        servername = builder.DataSource;
        userid = builder.UserID;
        pass = builder.Password;

        SqlDataAdapter da2 = new SqlDataAdapter(@"SELECT QuotationData.description, QuotationData.hsncode, QuotationData.qty, QuotationData.rate,QuotationData.CGST
        ,QuotationData.SGST,QuotationData.IGST,QuotationData.CGSTamt,QuotationData.SGSTamt,QuotationData.IGSTamt,QuotationData.totaltax, QuotationData.discount, QuotationData.amount,
        QuotationMain.quotationno, QuotationMain.partyname, QuotationMain.ccode,QuotationMain.kindatt, QuotationMain.address,
        QuotationMain.paymentterm1,QuotationMain.paymentterm2,QuotationMain.paymentterm3,QuotationMain.paymentterm4,QuotationMain.paymentterm5,
		QuotationMain.validityofoffer,QuotationMain.deliveryperiod,QuotationMain.specialpackaging,QuotationMain.standardpackaging,QuotationMain.inspection,QuotationMain.transportation,
        Format(QuotationMain.date,'dd-MM-yyyy')as date, QuotationMain.remark, QuotationMain.width, QuotationMain.Toatlamt, QuotationMain.depth, 
        QuotationMain.height, QuotationMain.base, QuotationMain.canopy, QuotationMain.material,QuotationMain.specifymaterial, QuotationMain.Constructiontype,
        QuotationMain.descriptionall, QuotationMain.sessionname,QuotationMain.filename1,QuotationMain.filename2, QuotationMain.createddate
        FROM QuotationData INNER JOIN QuotationMain ON QuotationData.quotationid = QuotationMain.id where QuotationMain.id=" + quotationid + "", con);
        da2.Fill(dtt);

        //ReportDocument cryRpt = new ReportDocument();

        ////////New Quotation//////////
        //cryRpt.Load(Server.MapPath(string.Format("../SalesQuotationReport.rpt", 1)));

        //crConnectionInfo.ServerName = servername;
        //crConnectionInfo.DatabaseName = dbname;
        //crConnectionInfo.UserID = userid;
        //crConnectionInfo.Password = pass;

        //// CrTables = cryRpt.Database.Tables;
        //SqlDataAdapter adpp = new SqlDataAdapter("select cname,mobile1,mobile2 from Company where ccode='" + dtt.Rows[0]["ccode"].ToString() + "'", con);
        //DataTable data = new DataTable();
        //adpp.Fill(data);

        //if (data.Rows.Count > 0)
        //{
        //    if (string.IsNullOrEmpty(data.Rows[0]["mobile2"].ToString()))
        //    {
        //        cryRpt.SetParameterValue("contact", data.Rows[0]["mobile1"].ToString());
        //    }
        //    if (!string.IsNullOrEmpty(data.Rows[0]["mobile1"].ToString()) && !string.IsNullOrEmpty(data.Rows[0]["mobile2"].ToString()))
        //    {
        //        cryRpt.SetParameterValue("contact", data.Rows[0]["mobile1"].ToString() + " / " + data.Rows[0]["mobile2"].ToString());
        //    }
        //}
        //if (dtt.Rows.Count > 0)
        //{
        //    oldfile1 = dtt.Rows[0]["filename1"].ToString();
        //    oldfile2 = dtt.Rows[0]["filename2"].ToString();
        //    date = dtt.Rows[0]["date"].ToString();

        //    if (!string.IsNullOrEmpty(dtt.Rows[0]["paymentterm1"].ToString()))
        //    {
        //        sb.Append(dtt.Rows[0]["paymentterm1"].ToString() + "<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtt.Rows[0]["paymentterm2"].ToString()))
        //    {
        //        sb.Append(dtt.Rows[0]["paymentterm2"].ToString() + "<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtt.Rows[0]["paymentterm3"].ToString()))
        //    {
        //        sb.Append(dtt.Rows[0]["paymentterm3"].ToString() + "<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtt.Rows[0]["paymentterm4"].ToString()))
        //    {
        //        sb.Append(dtt.Rows[0]["paymentterm4"].ToString() + "<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtt.Rows[0]["paymentterm5"].ToString()))
        //    {
        //        sb.Append(dtt.Rows[0]["paymentterm5"].ToString() + "<br>");
        //    }

        //    cryRpt.SetParameterValue("descriptionall", dtt.Rows[0]["descriptionall"].ToString());
        //    cryRpt.SetParameterValue("partyname", dtt.Rows[0]["partyname"].ToString());
        //    cryRpt.SetParameterValue("address", dtt.Rows[0]["address"].ToString());
        //    cryRpt.SetParameterValue("qno", dtt.Rows[0]["quotationno"].ToString());
        //    cryRpt.SetParameterValue("qdate", dtt.Rows[0]["date"].ToString());
        //    cryRpt.SetParameterValue("hsn", dtt.Rows[0]["hsncode"].ToString());
        //    cryRpt.SetParameterValue("qty", dtt.Rows[0]["qty"].ToString());
        //    cryRpt.SetParameterValue("discount", dtt.Rows[0]["discount"].ToString());
        //    cryRpt.SetParameterValue("rate", dtt.Rows[0]["rate"].ToString());
        //    cryRpt.SetParameterValue("amount", dtt.Rows[0]["amount"].ToString());
        //    cryRpt.SetParameterValue("remark", dtt.Rows[0]["remark"].ToString());
        //    cryRpt.SetParameterValue("Paymentterm", sb.ToString());
        //    cryRpt.SetParameterValue("kindatt", dtt.Rows[0]["kindatt"].ToString());

        //    filename = dtt.Rows[0]["quotationno"].ToString();
        //    party = dtt.Rows[0]["partyname"].ToString();

        //    if (dtt.Rows[0]["CGST"].ToString() == "0" && dtt.Rows[0]["SGST"].ToString() == "0")
        //    {
        //        cryRpt.SetParameterValue("CGST", "Not Applicable");
        //        cryRpt.SetParameterValue("SGST", "Not Applicable");
        //        cryRpt.SetParameterValue("IGST", "Extra as applicable (Presently " + dtt.Rows[0]["IGST"].ToString() + "%)");
        //    }
        //    if (dtt.Rows[0]["CGST"].ToString() != "0" && dtt.Rows[0]["SGST"].ToString() != "0")
        //    {
        //        cryRpt.SetParameterValue("CGST", "Extra as applicable (Presently " + dtt.Rows[0]["CGST"].ToString() + "%)");
        //        cryRpt.SetParameterValue("SGST", "Extra as applicable (Presently " + dtt.Rows[0]["SGST"].ToString() + "%)");
        //        cryRpt.SetParameterValue("IGST", "Not Applicable");
        //    }

        //    cryRpt.SetParameterValue("validityofoffer", dtt.Rows[0]["validityofoffer"].ToString());
        //    cryRpt.SetParameterValue("deliveryperiod", dtt.Rows[0]["deliveryperiod"].ToString());
        //    cryRpt.SetParameterValue("transportation", dtt.Rows[0]["transportation"].ToString());
        //    cryRpt.SetParameterValue("standardpackaging", dtt.Rows[0]["standardpackaging"].ToString());
        //    cryRpt.SetParameterValue("specialpackaging", dtt.Rows[0]["specialpackaging"].ToString());
        //    cryRpt.SetParameterValue("inspection", dtt.Rows[0]["inspection"].ToString());
        //}
        //CrTables = cryRpt.Database.Tables;
        //foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        //{
        //    crtableLogoninfo = CrTable.LogOnInfo;
        //    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
        //    CrTable.ApplyLogOnInfo(crtableLogoninfo);
        //}

        //Stream stream1 = cryRpt.ExportToStream(ExportFormatType.PortableDocFormat);

        //byte[] attach = ReadFully(stream1);

        //using (MemoryStream stream = new MemoryStream())
        //{
        //    PdfReader reader = new PdfReader(attach);
        //    using (PdfStamper stamper = new PdfStamper(reader, stream))
        //    {
        //        int pages = reader.NumberOfPages;
        //    }
        //    attachment = stream.ToArray();
        //}
        //Stream stream2 = new MemoryStream(attachment);

        string FromMailID = ConfigurationManager.AppSettings["mailUserName"];
        string ToMailID = lblemail.Text;//"pushpendra@weblinkservices.net";

        MailMessage mm = new MailMessage();
        mm.From = new MailAddress(FromMailID);

        mm.Subject = "Quotation from Excel Enclosure";
        mm.To.Add(ToMailID);

        mm.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = ConfigurationManager.AppSettings["Host"];
        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
        NetworkCred.UserName = ConfigurationManager.AppSettings["mailUserName"];
        NetworkCred.Password = ConfigurationManager.AppSettings["mailUserPass"];
        smtp.UseDefaultCredentials = true;
        smtp.Credentials = NetworkCred;
        smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);

        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        string[] abc = filename.Split('/');
        string abc2 = abc[0].ToString();
        string newfilename = abc2 + "-" + party + ".pdf";
       // mm.Attachments.Add(new Attachment(new MemoryStream(attachment), newfilename));

        if (!string.IsNullOrEmpty(oldfile2))
        {
            byte[] file = File.ReadAllBytes((Server.MapPath("~/RefDocument/")) + oldfile2);

            Stream stream = new MemoryStream(file);
            Attachment aa = new Attachment(stream, oldfile1);
            mm.Attachments.Add(aa);
        }

        mm.Body = @"<html><body style='border:1px solid #148bc4;padding:10px;width:600px;'><b style='font-size:15px';><u>Quotation Details</u></b>&nbsp;&nbsp;(Part of Control Panel)<br><br><b>
            Quotation No : </b> " + txQutno.Text + " <br> <b>Quotation Date : </b> " + date +
        "<br> <b>Delivery Period: </b> " + ddldeliveryperiod.SelectedItem.Text + "  <br><b> Transportation: </b> " + ddltransportation.SelectedItem.Text + "<br><b> Statndard packaging: </b> " + ddlStandardPacking.SelectedItem.Text + "<br><b> Special packaging : </b> " + ddlSpecialPacking.SelectedItem.Text + "<br><b> Inspection : </b> " + ddlinspection.SelectedItem.Text + " <br><b> Total Amount : " + txtAmt1.Text + "/- Rs. </b> <br></body></html> ";
        smtp.Send(mm);
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuotationList.aspx");
    }

    protected void btnSubmitmaterial_Click(object sender, EventArgs e)
    {
        PopupAddDetail.Visible = false;
    }

    protected void btnCancelMaterial_Click(object sender, EventArgs e)
    {
        PopupAddDetail.Visible = false;
    }

    public static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms.ToArray();
        }
    }
}