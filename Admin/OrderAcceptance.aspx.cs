using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Net.Mail;
using iTextSharp.text.pdf;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.Net;
using iTextSharp.tool.xml;

public partial class Admin_OrderAcceptance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    static int quotid; static string action; static int id; static string ccode; static string emailstatus;
    string qty, rate, discount, amt;

    //static int OAid;
    string OAid;

    int UpdatedQty; double UpdatedDiscount; double UpdatedAmt;
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
                GetTaxType();
                Autofill();
                if (Request.QueryString["cmd"] == null && Request.QueryString["cmded"] == null)
                {
                    Response.Redirect("QuotationList.aspx");
                }
                if (Request.QueryString["cmd"] != null && Request.QueryString["cmded"] == null)
                {
                    txtOAno.Text = GenerateCode();
                }
                if (Request.QueryString["cmd"] != null)
                {
                    OAid = Decrypt(Request.QueryString["cmd"].ToString());
                    HdnOAID.Value = OAid;
                    action = "insert";

                    // getQutationdts();
                    Getdata();
                    LoadQuotationNo();

                }
                if (Request.QueryString["cmded"] != null)
                {
                    OAid = Decrypt(Request.QueryString["cmded"].ToString());
                    HdnOAID.Value = OAid;
                    Filldata();
                    GetOAfileData();

                    getQutationdts();
                    action = "update";
                    //action = "insert";
                    Getdata();
                    LoadQuotationNo();
                }
            }
        }
    }


    protected string GenerateCode()
    {
        //string FinYear = null;
        //string FinFullYear = null;

        //if (DateTime.Today.Month > 3)
        //{
        //    FinYear = DateTime.Today.AddYears(1).ToString("yy");
        //    FinFullYear = DateTime.Today.AddYears(1).ToString("yyyy");
        //}
        //else
        //{
        //    var finYear = DateTime.Today.AddYears(1).ToString("yy");
        //    FinYear = (Convert.ToInt32(finYear) - 1).ToString();

        //    var finfYear = DateTime.Today.AddYears(1).ToString("yyyy");
        //    FinFullYear = (Convert.ToInt32(finfYear) - 1).ToString();
        //}
        //string previousyear = (Convert.ToDecimal(FinFullYear) - 1).ToString();
        //string strInvoiceNumber = "";
        //string OANO = "";
        //string fY = previousyear.ToString() + "-" + FinYear;
        //string strSelect = @"select ISNULL(MAX(OAno), '') AS maxno from OrderAccept where OAno like '%" + fY + "%'";
        ////string strSelect = @"SELECT TOP 1 OAno FROM [DB_ProcetechERP].OrderAccept where OAno like '%" + fY + "%' ORDER BY ID DESC";
        ////string strSelect = @"SELECT TOP 1 InvoiceNo FROM tblTaxInvoiceHdr where InvoiceNo like '%2022-23%' ORDER BY ID DESC";
        //SqlCommand cmd = new SqlCommand();
        //cmd.Connection = con;
        //cmd.CommandType = CommandType.Text;
        //cmd.CommandText = strSelect;
        //con.Open();
        //string result = cmd.ExecuteScalar().ToString();
        ////string result = "";
        //con.Close();
        //if (result != "")
        //{
        //    string str = result;
        //    int n = 7;

        //    str = str.Remove(0, n);

        //    int numbervalue = Convert.ToInt32(str.Substring(str.IndexOf("/") + 1, str.Length - (str.IndexOf("/") + 1)));
        //    numbervalue = numbervalue + 1;
        //    strInvoiceNumber = str.Substring(0, str.IndexOf("/") + 1) + "" + numbervalue.ToString("00");

        //    txtOAno.Text = "PA/OA/" + strInvoiceNumber;

        //    OANO = txtOAno.Text;
        //}
        //else
        //{
        //    OANO = "PA/OA/" + previousyear.ToString() + "-" + FinYear + "/" + "01";
        //}
        //return OANO;
        string OANO = "";
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [OrderAccept]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        int MxID = 0;
        if (dt.Rows.Count > 0)
        {
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? MxID + 1 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1;
            OANO = "OA24250000" + maxid.ToString();
            txtOAno.Text = "OA24250000" + maxid.ToString();
        }
        else
        {
            //ComCode = string.Empty;
        }
        return OANO;
    }


    private void Filldata()
    {
        if (Request.QueryString["cmded"] != null)
        {
            SqlCommand scmd = new SqlCommand("select oaid from OAList where oano='" + OAid + "'", con);
            con.Open();
            id = Convert.ToInt32(scmd.ExecuteScalar());
            con.Close();
        }
        SqlDataAdapter adpfill = new SqlDataAdapter(@"SELECT [id],[ccode],[quotationid],[OAno],convert(varchar(20),[currentdate],105) as currentdate,[customername],[address],
       [quotationno],convert(varchar(20),[quotationdate],105) as quotationdate,[pono],convert(varchar(20),[podate],105) as podate
      ,[contactpersonpurchase],[contpersonpurcontact],[contactpersontechnical],[contpersontechcontact],replace(description,'<br>',CHAR(13) + CHAR(10)) as description
      ,[qty],[drgref],[price],[totamt],convert(varchar(20),[deliverydatereqbycust],105) as deliverydatereqbycust,
       convert(varchar(20),[deliverydatecommbyus],105) as deliverydatecommbyus,[cgst],[sgst],[igst],[cgstamt]
      ,[sgstamt],[igstamt],[packing1],[packing2],[packing3],[note1],[note2],[note3],[note4],[note5],[note6],[note7]
      ,[note8],[note9],[DeliveryTransportation],[termaofpayment],[billingdetails],[buyer],[consignee]
      ,[instructionchk1],[instructionchk2],[instructionchk3],[instructionchk4],[instructionchk5],[instructionchk6],[instructionchk7],[instructionchk8],
       [specialinstruction1],[specialinstruction2],[specialinstruction3],[specialinstruction4],[specialinstruction5],[specialinstruction6]
      ,[specialinstruction7],[specialinstruction8],[sendtoemail1],[sendtoemail2],[sendtoemail3],[sendtoemail4]
      ,[sendtoemail5],[custemail],[status],[emailstatus],[IsDrawingcomplete] FROM [OrderAccept] where id=" + id, con);

        DataTable dtfill = new DataTable();
        adpfill.Fill(dtfill);

        if (dtfill.Rows.Count > 0)
        {
            txtOAno.Text = dtfill.Rows[0]["OAno"].ToString();
            txttodaysdate.Text = dtfill.Rows[0]["currentdate"].ToString();
            txtcustomername.Text = dtfill.Rows[0]["customername"].ToString();
            ccode = dtfill.Rows[0]["ccode"].ToString();

            SqlDataAdapter adp2 = new SqlDataAdapter("select top 1 oname1,oname2,oname3,oname4,oname5 from Company where ccode='" + dtfill.Rows[0]["ccode"].ToString() + "' and status=0 and isdeleted=0", con);
            DataTable dt2 = new DataTable();
            adp2.Fill(dt2);

            Hashtable ht = new Hashtable();
            if (dt2.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt2.Rows[0]["oname1"].ToString()))
                {
                    ht.Add(dt2.Rows[0]["oname1"].ToString(), dt2.Rows[0]["oname1"].ToString());
                }
                if (!string.IsNullOrEmpty(dt2.Rows[0]["oname2"].ToString()))
                {
                    ht.Add(dt2.Rows[0]["oname2"].ToString(), dt2.Rows[0]["oname2"].ToString());
                }
                if (!string.IsNullOrEmpty(dt2.Rows[0]["oname3"].ToString()))
                {
                    ht.Add(dt2.Rows[0]["oname3"].ToString(), dt2.Rows[0]["oname3"].ToString());
                }
                if (!string.IsNullOrEmpty(dt2.Rows[0]["oname4"].ToString()))
                {
                    ht.Add(dt2.Rows[0]["oname4"].ToString(), dt2.Rows[0]["oname3"].ToString());
                }
                if (!string.IsNullOrEmpty(dt2.Rows[0]["oname5"].ToString()))
                {
                    ht.Add(dt2.Rows[0]["oname5"].ToString(), dt2.Rows[0]["oname3"].ToString());
                }

                ddlcontactperson.DataSource = ht;
                ddlcontactperson.DataTextField = "value";
                ddlcontactperson.DataValueField = "key";
                ddlcontactperson.DataBind();
                ddlcontactperson.Items.Insert(0, "Select");
            }

            txtaddress.Text = dtfill.Rows[0]["address"].ToString();

            LoadQuotationNo();

            ddlQuotationNo.SelectedItem.Text = dtfill.Rows[0]["quotationno"].ToString();

            txtquotationdate.Text = dtfill.Rows[0]["quotationdate"].ToString();
            txtpono.Text = dtfill.Rows[0]["pono"].ToString();
            txtpodate.Text = dtfill.Rows[0]["podate"].ToString();

            ddlcontactperson.Text = dtfill.Rows[0]["contactpersonpurchase"].ToString();
            txtpurchasecontactno.Text = dtfill.Rows[0]["contpersonpurcontact"].ToString();
            txtcontactpersontechnical.Text = dtfill.Rows[0]["contactpersontechnical"].ToString();
            txttechnicalcontactno.Text = dtfill.Rows[0]["contpersontechcontact"].ToString();

            txtdeliverydatebycustomer.Text = dtfill.Rows[0]["deliverydatereqbycust"].ToString();
            txtdeliverydatebyus.Text = dtfill.Rows[0]["deliverydatecommbyus"].ToString();

            txtcgstper.Text = dtfill.Rows[0]["cgst"].ToString();
            txtsgstper.Text = dtfill.Rows[0]["sgst"].ToString();
            txtigstper.Text = dtfill.Rows[0]["igst"].ToString();
            txtcgstamt.Text = dtfill.Rows[0]["cgstamt"].ToString();
            txtsgstamt.Text = dtfill.Rows[0]["sgstamt"].ToString();
            txtigstamt.Text = dtfill.Rows[0]["igstamt"].ToString();

            ddlregular.Text = dtfill.Rows[0]["packing1"].ToString();
            ddlcostincluded.Text = dtfill.Rows[0]["packing2"].ToString();
            ddlAmount.Text = dtfill.Rows[0]["packing3"].ToString();

            txtnote1.Text = dtfill.Rows[0]["note1"].ToString();
            txtnote2.Text = dtfill.Rows[0]["note2"].ToString();
            txtnote3.Text = dtfill.Rows[0]["note3"].ToString();
            txtnote4.Text = dtfill.Rows[0]["note4"].ToString();
            txtnote5.Text = dtfill.Rows[0]["note5"].ToString();
            txtnote6.Text = dtfill.Rows[0]["note6"].ToString();
            txtnote7.Text = dtfill.Rows[0]["note7"].ToString();
            txtnote8.Text = dtfill.Rows[0]["note8"].ToString();
            txtnote9.Text = dtfill.Rows[0]["note9"].ToString();

            txtdeliverytransportaioncharge.Text = dtfill.Rows[0]["DeliveryTransportation"].ToString();
            txttermsofpayment.Text = dtfill.Rows[0]["termaofpayment"].ToString();
            txtbillingdetails.Text = dtfill.Rows[0]["billingdetails"].ToString();
            txtbuyer.Text = dtfill.Rows[0]["buyer"].ToString();
            txtConsignee.Text = dtfill.Rows[0]["consignee"].ToString();

            txtinstruction1.Text = dtfill.Rows[0]["specialinstruction1"].ToString();
            txtinstruction2.Text = dtfill.Rows[0]["specialinstruction2"].ToString();
            txtinstruction3.Text = dtfill.Rows[0]["specialinstruction3"].ToString();
            txtinstruction4.Text = dtfill.Rows[0]["specialinstruction4"].ToString();
            txtinstruction5.Text = dtfill.Rows[0]["specialinstruction5"].ToString();
            txtinstruction6.Text = dtfill.Rows[0]["specialinstruction6"].ToString();
            txtinstruction7.Text = dtfill.Rows[0]["specialinstruction7"].ToString();
            txtinstruction8.Text = dtfill.Rows[0]["specialinstruction8"].ToString();

            if (dtfill.Rows[0]["instructionchk1"].ToString() == "true")
            {
                Chkinstaruction1.Checked = true;
            }
            else
            {
                Chkinstaruction1.Checked = false;
                txtinstruction1.Text = "1.Inspection before powder coating is demanded by customer";
            }

            if (dtfill.Rows[0]["instructionchk2"].ToString() == "true")
            {
                Chkinstaruction2.Checked = true;
            }
            else
            {
                Chkinstaruction2.Checked = false;
                txtinstruction2.Text = "2.Inspection after powder coating before dispatch";
            }

            if (dtfill.Rows[0]["instructionchk3"].ToString() == "true")
            {
                Chkinstaruction3.Checked = true;
            }
            else
            {
                Chkinstaruction3.Checked = false;
                txtinstruction3.Text = "3.Send proforma 4 days before readiness";
            }

            if (dtfill.Rows[0]["instructionchk4"].ToString() == "true")
            {
                Chkinstaruction4.Checked = true;
            }
            else
            {
                Chkinstaruction4.Checked = false;
                txtinstruction4.Text = "4.Get full payment against proforma";
            }

            if (dtfill.Rows[0]["instructionchk5"].ToString() == "true")
            {
                Chkinstaruction5.Checked = true;
            }
            else
            {
                Chkinstaruction5.Checked = false;
                txtinstruction5.Text = "5.Get full payment against delivery";
            }

            if (dtfill.Rows[0]["instructionchk6"].ToString() == "true")
            {
                Chkinstaruction6.Checked = true;
            }
            else
            {
                Chkinstaruction6.Checked = false;
                txtinstruction6.Text = "6.xx days PDC against proforma";
            }

            if (dtfill.Rows[0]["instructionchk7"].ToString() == "true")
            {
                Chkinstaruction7.Checked = true;
            }
            else
            {
                Chkinstaruction7.Checked = false;
                txtinstruction7.Text = "7.xx days PDC against delivery";
            }

            if (dtfill.Rows[0]["instructionchk8"].ToString() == "true")
            {
                Chkinstaruction8.Checked = true;
            }
            else
            {
                Chkinstaruction8.Checked = false;
                txtinstruction8.Text = "8.xx days LC";
            }

            txtemail1.Text = dtfill.Rows[0]["sendtoemail1"].ToString();
            txtemail2.Text = dtfill.Rows[0]["sendtoemail2"].ToString();
            txtemail3.Text = dtfill.Rows[0]["sendtoemail3"].ToString();
            txtemail4.Text = dtfill.Rows[0]["sendtoemail4"].ToString();
            txtemail5.Text = dtfill.Rows[0]["sendtoemail5"].ToString();
            emailstatus = dtfill.Rows[0]["emailstatus"].ToString();
        }

        if (txtnote1.Text == "" && txtnote2.Text == "" && txtnote3.Text == "" && txtnote4.Text == "" && txtnote5.Text == "" && txtnote6.Text == "")
        {
            Autofill();
        }

        if (dtfill.Rows[0]["IsDrawingcomplete"].ToString() == "True")
        {
            if (Request.QueryString["cmded"] == null)
            {
                GenerateCode();
            }
            else
            {

            }

        }

    }

    private void LoadQuotationNo()
    {
        try
        {
            string com = "select * from QuotationMain where partyname='" + txtcustomername.Text + "' and IsRevise=0";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlQuotationNo.DataSource = dt;
            ddlQuotationNo.DataBind();
            ddlQuotationNo.DataTextField = "quotationno";
            ddlQuotationNo.DataValueField = "id";
            ddlQuotationNo.DataBind();
            ddlQuotationNo.Items.Insert(0, "Select");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void Getdata()
    {

        //string constructiontype = "";
        //SqlCommand scmdoa;
        //if (Request.QueryString["cmd"] != null)
        //{
        //    scmdoa = new SqlCommand("select top 1 constructiontype from OAList where id='" + OAid + "'", con);
        //}
        //else
        //{
        //    scmdoa = new SqlCommand("select top 1 constructiontype from OAList where oano='" + OAid + "'", con);
        //}

        //con.Open();
        //constructiontype = scmdoa.ExecuteScalar().ToString();
        //con.Close();

        string constructiontype = "";
        SqlCommand scmdoa;

        if (Request.QueryString["cmd"] != null)
        {
            scmdoa = new SqlCommand("select top 1 constructiontype from OAList where quotationid = @OAid", con);
            scmdoa.Parameters.AddWithValue("@OAid", OAid);
        }
        else
        {
            scmdoa = new SqlCommand("select top 1 constructiontype from OAList where oano = @OAid", con);
            scmdoa.Parameters.AddWithValue("@OAid", OAid);
        }

        con.Open();

        object result = scmdoa.ExecuteScalar();

        if (result != null && result != DBNull.Value)
        {
            constructiontype = result.ToString();
        }
        // If result is DBNull.Value or null, constructiontype remains an empty string

        con.Close();

        if (constructiontype == "")
        {
            constructiontype = "close";
        }




        if (constructiontype != "Open OA" && constructiontype != "")
        {
            SqlCommand scmd;
            if (Request.QueryString["cmd"] != null)
            {
                scmd = new SqlCommand("select top 1 quotationid from OAList where quotationid='" + OAid + "'", con);
            }
            else
            {
                scmd = new SqlCommand("select top 1 quotationid from OAList where oano='" + OAid + "'", con);
            }
            con.Open();
            quotid = Convert.ToInt32( OAid);
            //quotid = Convert.ToInt32(scmd.ExecuteScalar());
            con.Close();

            SqlDataAdapter adp = new SqlDataAdapter("select quotationno,ccode,partyname,kindatt,convert(varchar(20),date,105) as date,address,replace(descriptionall,'<br>',CHAR(13) + CHAR(10)) as descriptionall, Taxation from QuotationMain where id=" + OAid, con);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ccode = dt.Rows[0]["ccode"].ToString();
                //txtquotationno.Text = dt.Rows[0]["quotationno"].ToString();
                //ddlQuotationNo.Text = dt.Rows[0]["quotationno"].ToString();
                txtquotationdate.Text = dt.Rows[0]["date"].ToString();
                txtcustomername.Text = dt.Rows[0]["partyname"].ToString();
                txtaddress.Text = dt.Rows[0]["address"].ToString();
                txtdescription.Text = dt.Rows[0]["descriptionall"].ToString();
                ddltaxation.SelectedItem.Text = dt.Rows[0]["Taxation"].ToString();
                SqlDataAdapter adp2 = new SqlDataAdapter("select top 1 oname1,oname2,oname3,oname4,oname5,email1,mobile1 from Company where ccode='" + dt.Rows[0]["ccode"].ToString() + "' and status=0 and isdeleted=0", con);
                DataTable dt2 = new DataTable();
                adp2.Fill(dt2);

                Hashtable ht = new Hashtable();
                if (dt2.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname1"].ToString()))
                    {
                        ht.Add(dt2.Rows[0]["oname1"].ToString(), dt2.Rows[0]["oname1"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname2"].ToString()))
                    {
                        ht.Add(dt2.Rows[0]["oname2"].ToString(), dt2.Rows[0]["oname2"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname3"].ToString()))
                    {
                        ht.Add(dt2.Rows[0]["oname3"].ToString(), dt2.Rows[0]["oname3"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname4"].ToString()))
                    {
                        ht.Add(dt2.Rows[0]["oname4"].ToString(), dt2.Rows[0]["oname3"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname5"].ToString()))
                    {
                        ht.Add(dt2.Rows[0]["oname5"].ToString(), dt2.Rows[0]["oname3"].ToString());
                    }

                    ddlcontactperson.DataSource = ht;
                    ddlcontactperson.DataTextField = "value";
                    ddlcontactperson.DataValueField = "key";
                    ddlcontactperson.DataBind();
                    //ddlcontactperson.Items.Insert(0, "Select");

                    txtemail1.Text = dt2.Rows[0]["email1"].ToString() == "" ? "" : dt2.Rows[0]["email1"].ToString();
                    txtpurchasecontactno.Text = dt2.Rows[0]["mobile1"].ToString() == "" ? "" : dt2.Rows[0]["mobile1"].ToString();
                }
            }

            SqlDataAdapter adp1 = new SqlDataAdapter("SELECT [id],[quotationid],[quotationno],[sno],[description],[hsncode],[qty],[rate],[CGST],[SGST],[IGST],[CGSTamt],[SGSTamt],[IGSTamt],[totaltax],[discount],[amount] FROM QuotationData where quotationid=" + quotid, con);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                txtQty.Text = dt1.Rows[0]["qty"].ToString();
                txtprice.Text = dt1.Rows[0]["rate"].ToString();
                txtTotalamt.Text = dt1.Rows[0]["amount"].ToString();
                txtcgstper.Text = dt1.Rows[0]["CGST"].ToString();
                txtcgstamt.Text = dt1.Rows[0]["CGSTamt"].ToString();
                txtsgstper.Text = dt1.Rows[0]["SGST"].ToString();
                txtsgstamt.Text = dt1.Rows[0]["SGSTamt"].ToString();
                txtigstper.Text = dt1.Rows[0]["IGST"].ToString();
                txtigstamt.Text = dt1.Rows[0]["IGSTamt"].ToString();
            }
        }

        if (constructiontype == "Open OA" && constructiontype != "")
        {
            SqlDataAdapter adp = new SqlDataAdapter(@"SELECT [quotationid],[OAno],[ccode],[currentdate],[customername],[address],[quotationno]
      ,[quotationdate],[pono],[podate],[contactpersonpurchase],[contpersonpurcontact],[contactpersontechnical],[contpersontechcontact]
      ,[description],[qty],[drgref],[price],[totamt],[emailstatus] from [OrderAccept] where id=" + id, con);
            DataTable dtfill = new DataTable();

            adp.Fill(dtfill);

            if (dtfill.Rows.Count > 0)
            {
                txtOAno.Text = dtfill.Rows[0]["OAno"].ToString();
                txttodaysdate.Text = dtfill.Rows[0]["currentdate"].ToString();
                txtcustomername.Text = dtfill.Rows[0]["customername"].ToString();
                txtaddress.Text = dtfill.Rows[0]["address"].ToString();
                //ddlQuotationNo.Text = dtfill.Rows[0]["quotationno"].ToString();
                txtquotationdate.Text = dtfill.Rows[0]["quotationdate"].ToString();
                txtpono.Text = dtfill.Rows[0]["pono"].ToString();
                txtpodate.Text = dtfill.Rows[0]["podate"].ToString();

                ddlcontactperson.Text = dtfill.Rows[0]["contactpersonpurchase"].ToString();
                txtpurchasecontactno.Text = dtfill.Rows[0]["contpersonpurcontact"].ToString();
                txtcontactpersontechnical.Text = dtfill.Rows[0]["contactpersontechnical"].ToString();
                txttechnicalcontactno.Text = dtfill.Rows[0]["contpersontechcontact"].ToString();
                txtdescription.Text = dtfill.Rows[0]["description"].ToString();
                txtQty.Text = dtfill.Rows[0]["qty"].ToString();
                txtdrgref.Text = dtfill.Rows[0]["drgref"].ToString();
                txtprice.Text = dtfill.Rows[0]["price"].ToString();
                txtTotalamt.Text = dtfill.Rows[0]["totamt"].ToString();
                emailstatus = dtfill.Rows[0]["emailstatus"].ToString();

                //SqlDataAdapter adp2 = new SqlDataAdapter("select top 1 oname1,oname2,oname3,oname4,oname5 from Company where ccode='" + dt.Rows[0]["ccode"].ToString() + "' and status=0 and isdeleted=0", con);
                //DataTable dt2 = new DataTable();
                //adp2.Fill(dt2);

                //Hashtable ht = new Hashtable();
                //if (dt2.Rows.Count > 0)
                //{
                //    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname1"].ToString()))
                //    {
                //        ht.Add(dt2.Rows[0]["oname1"].ToString(), dt2.Rows[0]["oname1"].ToString());
                //    }
                //    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname2"].ToString()))
                //    {
                //        ht.Add(dt2.Rows[0]["oname2"].ToString(), dt2.Rows[0]["oname2"].ToString());
                //    }
                //    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname3"].ToString()))
                //    {
                //        ht.Add(dt2.Rows[0]["oname3"].ToString(), dt2.Rows[0]["oname3"].ToString());
                //    }
                //    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname4"].ToString()))
                //    {
                //        ht.Add(dt2.Rows[0]["oname4"].ToString(), dt2.Rows[0]["oname3"].ToString());
                //    }
                //    if (!string.IsNullOrEmpty(dt2.Rows[0]["oname5"].ToString()))
                //    {
                //        ht.Add(dt2.Rows[0]["oname5"].ToString(), dt2.Rows[0]["oname3"].ToString());
                //    }

                //    ddlcontactperson.DataSource = ht;
                //    ddlcontactperson.DataTextField = "value";
                //    ddlcontactperson.DataValueField = "key";
                //    ddlcontactperson.DataBind();
                //}
            }
        }
    }

    protected void getQutationdts()
    {

        if (Request.QueryString["cmd"] != null)
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM QuotationData where quotationno='" + ddlQuotationNo.SelectedItem.Text + "'", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgvOrderAcceptDtl.DataSource = dt;
                dgvOrderAcceptDtl.DataBind();
            }
            else
            {
                dgvOrderAcceptDtl.DataSource = null;
                dgvOrderAcceptDtl.DataBind();
            }
        }
        else
        {
            SqlDataAdapter ad = new SqlDataAdapter("select id,Description as description,HSN as hsncode,Qty as qty,Price as rate,CGST,SGST,IGST,Discount as discount,TotalAmount as amount,'true' as IsSelect from OrderAcceptDtls where OAno='" + txtOAno.Text + "'", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgvOrderAcceptDtl.DataSource = dt;
                dgvOrderAcceptDtl.DataBind();
            }
            else
            {
                dgvOrderAcceptDtl.DataSource = null;
                dgvOrderAcceptDtl.DataBind();
            }
        }


    }

    private void GetOAfileData()
    {
        try
        {
            string query1 = string.Empty;
            query1 = "SELECT TOP 1 [DocID],[OAid],[File1],[File2],[File3],[File4],[File5],[CeatedDate],[CreatedBy] FROM tblOAFiledata where OAid='" + id + "' ";
            SqlDataAdapter ad = new SqlDataAdapter(query1, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                HFfile1.Value = dt.Rows[0]["File1"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["File1"].ToString()))
                {
                    string a1 = dt.Rows[0]["File1"].ToString().Remove(0, 13);// "Has file";
                    lblfile1.Text = a1.Remove(a1.Length - 18, 18) + "...";
                    lblfile1.ForeColor = Color.Green;
                }
                else
                {
                    lblfile1.Text = "file not available";
                }


                HFfile2.Value = dt.Rows[0]["File2"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["File2"].ToString()))
                {
                    string a1 = dt.Rows[0]["File2"].ToString().Remove(0, 13);// "Has file";
                    lblfile2.Text = a1.Remove(a1.Length - 18, 18) + "...";
                    lblfile2.ForeColor = Color.Green;
                }
                else
                {
                    lblfile2.Text = "file not available";
                }
                HFfile3.Value = dt.Rows[0]["File3"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["File3"].ToString()))
                {
                    string a1 = dt.Rows[0]["File3"].ToString().Remove(0, 13);// "Has file";
                    lblfile3.Text = a1.Remove(a1.Length - 18, 18) + "...";
                    lblfile3.ForeColor = Color.Green;
                }
                else
                {
                    lblfile3.Text = "file not available";
                }
                HFfile4.Value = dt.Rows[0]["File4"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["File4"].ToString()))
                {
                    string a1 = dt.Rows[0]["File4"].ToString().Remove(0, 13);// "Has file";
                    lblfile4.Text = a1.Remove(a1.Length - 18, 18) + "...";
                    lblfile4.ForeColor = Color.Green;
                }
                else
                {
                    lblfile4.Text = "file not available";
                }
                HFfile5.Value = dt.Rows[0]["File5"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["File5"].ToString()))
                {
                    string a1 = dt.Rows[0]["File5"].ToString().Remove(0, 13);// "Has file";
                    lblfile5.Text = a1.Remove(a1.Length - 18, 18) + "...";
                    lblfile5.ForeColor = Color.Green;
                }
                else
                {
                    lblfile5.Text = "file not available";
                }
            }
            else
            {
                lblfile1.Text = lblfile2.Text = lblfile3.Text = lblfile4.Text = lblfile5.Text = "file not available";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void Autofill()
    {
        txttodaysdate.Text = DateTime.Now.ToString("dd-MM-yyyy");

        txtnote1.Text = "1. All doors & covers are in 2.00 thk.";
        txtnote2.Text = "2. Base is in ISMC";
        txtnote3.Text = "3. Powder coating is multicolour (Out side RAL-7032 & Inside White";
        txtnote4.Text = "4. MCCB Bracket to supply";
        txtnote5.Text = "5. 4 inch filters 4 Nos are in our scope &fan not in our scope";
        txtnote6.Text = "6. Anti vibration bed required";

        txtinstruction1.Text = "1.Inspection before powder coating is demanded by customer";
        txtinstruction2.Text = "2.Inspection after powder coating before dispatch";
        txtinstruction3.Text = "3.Send proforma 4 days before readiness";
        txtinstruction4.Text = "4.Get full payment against proforma";
        txtinstruction5.Text = "5.Get full payment against delivery";
        txtinstruction6.Text = "6.xx days PDC against proforma";
        txtinstruction7.Text = "7.xx days PDC against delivery";
        txtinstruction8.Text = "8.xx days LC";
    }

    protected string GeneratesubOACode()
    {
        string subOA = "";
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [OrderAcceptDtls]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        int MxID = 0;
        if (dt.Rows.Count > 0)
        {
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? MxID + 1 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1;
            subOA = "PA/SUBOA/" + maxid.ToString();
        }
        else
        {
            //ComCode = string.Empty;
        }
        return subOA;
    }

    protected void ddlcostincluded_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcostincluded.SelectedItem.Text == "Extra Billing")
        {
            ddlAmount.Visible = true; ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "sum()", true);
        }
        if (ddlcostincluded.SelectedItem.Text != "Extra Billing")
        {
            ddlAmount.Visible = false; ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "sum()", true);
        }
    }

    protected void ddlregular_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlregular.SelectedItem.Text == "Wooden")
        {
            ddlcostincluded.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "sum()", true);
        }
        if (ddlregular.SelectedItem.Text != "Wooden")
        {
            ddlcostincluded.Visible = false;
            ddlAmount.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "sum()", true);
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        InsertDeatils();
        //if (!string.IsNullOrEmpty(Request.Form[txtTotalamt.UniqueID].ToString()))
        //{
        SqlCommand cmd = new SqlCommand("SP_OrderAccept", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ccode", ccode);
        cmd.Parameters.AddWithValue("@customername", txtcustomername.Text);

        cmd.Parameters.AddWithValue("@address", txtaddress.Text);
        cmd.Parameters.AddWithValue("@quotationid", quotid);
        cmd.Parameters.AddWithValue("@quotationno", ddlQuotationNo.SelectedItem.Text);

        if (Request.Form[txtquotationdate.UniqueID].ToString() != "")
        {
            DateTime qdate = DateTime.ParseExact(Request.Form[txtquotationdate.UniqueID].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@quotationdate", qdate);
        }
        cmd.Parameters.AddWithValue("@pono", txtpono.Text);

        if (Request.Form[txtpodate.UniqueID].ToString() != "")
        {
            DateTime podate = DateTime.ParseExact(Request.Form[txtpodate.UniqueID].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@podate", podate);
        }

        cmd.Parameters.AddWithValue("@contactpersonpurchase", ddlcontactperson.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@contpersonpurcontact", txtpurchasecontactno.Text);
        cmd.Parameters.AddWithValue("@contactpersontechnical", txtcontactpersontechnical.Text);
        cmd.Parameters.AddWithValue("@contpersontechcontact", txttechnicalcontactno.Text);
        cmd.Parameters.AddWithValue("@description", txtdescription.Text);
        cmd.Parameters.AddWithValue("@qty", txtQty.Text);
        cmd.Parameters.AddWithValue("@drgref", txtdrgref.Text);
        cmd.Parameters.AddWithValue("@price", txtprice.Text);
        cmd.Parameters.AddWithValue("@totamt", Request.Form[txtTotalamt.UniqueID].ToString());
        cmd.Parameters.AddWithValue("@TaxType", ddltaxation.SelectedItem.Text);

        if (Request.Form[txtdeliverydatebycustomer.UniqueID].ToString() != "")
        {
            DateTime deliverydatereqbycust = DateTime.ParseExact(Request.Form[txtdeliverydatebycustomer.UniqueID].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@deliverydatereqbycust", deliverydatereqbycust);
        }
        if (Request.Form[txtdeliverydatebyus.UniqueID].ToString() != "")
        {
            DateTime deliverydatecommbyus = DateTime.ParseExact(Request.Form[txtdeliverydatebyus.UniqueID].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@deliverydatecommbyus", deliverydatecommbyus);
        }
        cmd.Parameters.AddWithValue("@cgst", txtcgstper.Text);
        cmd.Parameters.AddWithValue("@sgst", txtsgstper.Text);
        cmd.Parameters.AddWithValue("@igst", txtigstper.Text);
        cmd.Parameters.AddWithValue("@cgstamt", Request.Form[txtcgstamt.UniqueID].ToString());
        cmd.Parameters.AddWithValue("@sgstamt", Request.Form[txtsgstamt.UniqueID].ToString());
        cmd.Parameters.AddWithValue("@igstamt", Request.Form[txtigstamt.UniqueID].ToString());

        cmd.Parameters.AddWithValue("@packing1", ddlregular.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@packing2", ddlcostincluded.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@packing3", ddlAmount.SelectedItem.Text);

        cmd.Parameters.AddWithValue("@note1", txtnote1.Text);
        cmd.Parameters.AddWithValue("@note2", txtnote2.Text);
        cmd.Parameters.AddWithValue("@note3", txtnote3.Text);
        cmd.Parameters.AddWithValue("@note4", txtnote4.Text);
        cmd.Parameters.AddWithValue("@note5", txtnote5.Text);
        cmd.Parameters.AddWithValue("@note6", txtnote6.Text);
        cmd.Parameters.AddWithValue("@note7", txtnote7.Text);
        cmd.Parameters.AddWithValue("@note8", txtnote8.Text);
        cmd.Parameters.AddWithValue("@note9", txtnote9.Text);

        cmd.Parameters.AddWithValue("@DeliveryTransportation", txtdeliverytransportaioncharge.Text);
        cmd.Parameters.AddWithValue("@termaofpayment", txttermsofpayment.Text);
        cmd.Parameters.AddWithValue("@billingdetails", txtbillingdetails.Text);
        cmd.Parameters.AddWithValue("@buyer", txtbuyer.Text);
        cmd.Parameters.AddWithValue("@consignee", txtConsignee.Text);

        if (Chkinstaruction1.Checked == true)
        {
            cmd.Parameters.AddWithValue("@instructionchk1", "true");
            cmd.Parameters.AddWithValue("@specialinstruction1", txtinstruction1.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@instructionchk1", "false");
        }

        if (Chkinstaruction2.Checked == true)
        {
            cmd.Parameters.AddWithValue("@instructionchk2", "true");
            cmd.Parameters.AddWithValue("@specialinstruction2", txtinstruction2.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@instructionchk2", "false");
        }

        if (Chkinstaruction3.Checked == true)
        {
            cmd.Parameters.AddWithValue("@instructionchk3", "true");
            cmd.Parameters.AddWithValue("@specialinstruction3", txtinstruction3.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@instructionchk3", "false");
        }

        if (Chkinstaruction4.Checked == true)
        {
            cmd.Parameters.AddWithValue("@instructionchk4", "true");
            cmd.Parameters.AddWithValue("@specialinstruction4", txtinstruction4.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@instructionchk4", "false");
        }

        if (Chkinstaruction5.Checked == true)
        {
            cmd.Parameters.AddWithValue("@instructionchk5", "true");
            cmd.Parameters.AddWithValue("@specialinstruction5", txtinstruction5.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@instructionchk5", "false");
        }

        if (Chkinstaruction6.Checked == true)
        {
            cmd.Parameters.AddWithValue("@instructionchk6", "true");
            cmd.Parameters.AddWithValue("@specialinstruction6", txtinstruction6.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@instructionchk6", "false");
        }

        if (Chkinstaruction7.Checked == true)
        {
            cmd.Parameters.AddWithValue("@instructionchk7", "true");
            cmd.Parameters.AddWithValue("@specialinstruction7", txtinstruction7.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@instructionchk7", "false");
        }

        if (Chkinstaruction8.Checked == true)
        {
            cmd.Parameters.AddWithValue("@instructionchk8", "true");
            cmd.Parameters.AddWithValue("@specialinstruction8", txtinstruction8.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@instructionchk8", "false");
        }

        cmd.Parameters.AddWithValue("@sendtoemail1", txtemail1.Text);
        cmd.Parameters.AddWithValue("@sendtoemail2", txtemail2.Text);
        cmd.Parameters.AddWithValue("@sendtoemail3", txtemail3.Text);
        cmd.Parameters.AddWithValue("@sendtoemail4", txtemail4.Text);
        cmd.Parameters.AddWithValue("@sendtoemail5", txtemail5.Text);

        if (Request.QueryString["cmd"] != null && Request.QueryString["cmded"] == null)
        {
            GenerateCode();
        }
        cmd.Parameters.AddWithValue("@OAno", Request.Form[txtOAno.UniqueID].ToString());

        if (action == "update")
        {
            cmd.Parameters.AddWithValue("@id", id);
            DateTime currentdate = DateTime.ParseExact(Request.Form[txttodaysdate.UniqueID].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            cmd.Parameters.AddWithValue("@currentdate", currentdate);
        }

        if (ddloatocustomer.SelectedItem.Text == "Yes")
        {
            cmd.Parameters.AddWithValue("@emailstatus", "sent");
        }
        if (ddloatocustomer.SelectedItem.Text == "No")
        {
            cmd.Parameters.AddWithValue("@emailstatus", "Not Sent");
        }

        if (Request.QueryString["cmd"] != null)
        {
            action = "insert";
        }
        if (Request.QueryString["cmded"] != null)
        {
            action = "update";
            //action = "insert";
        }
        cmd.Parameters.AddWithValue("@action", action);

        cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
        cmd.Parameters.AddWithValue("@UpdatedBy", Session["name"].ToString());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        //Insert all Product-------------------------------------------------------------------------

        SqlCommand cmdPrDtl = new SqlCommand();
        con.Open();
        try
        {
            if (Request.QueryString["cmd"] != null)
            {
                foreach (GridViewRow g1 in dgvOrderAcceptDtl.Rows)
                {
                    CheckBox chk = (g1.Cells[0].FindControl("chkSelect") as CheckBox);
                    if (chk != null & chk.Checked)
                    {
                        string SubOA = GeneratesubOACode();
                        Label ID = (g1.Cells[1].FindControl("lblID") as Label);
                        Label HSN = (g1.Cells[1].FindControl("lblhsncode") as Label);
                        TextBox Desci = (g1.Cells[2].FindControl("txtdescription") as TextBox);
                        TextBox Qty = (g1.Cells[2].FindControl("txtQty") as TextBox);
                        TextBox Price = (g1.Cells[2].FindControl("txtprice") as TextBox);
                        TextBox Discount = (g1.Cells[2].FindControl("txtdiscount") as TextBox);
                        TextBox CGST = (g1.Cells[2].FindControl("txtCGST") as TextBox);
                        TextBox SGST = (g1.Cells[2].FindControl("txtSGST") as TextBox);
                        TextBox IGST = (g1.Cells[2].FindControl("txtIGST") as TextBox);
                        TextBox TotalAmount = (g1.Cells[2].FindControl("txtamount") as TextBox);
                        TextBox txtunit = (g1.Cells[2].FindControl("txtunit") as TextBox);
                        SqlDataAdapter adp = new SqlDataAdapter("select qty,rate,discount,amount from tblQuotationData where id='" + ID.Text + "'", con);
                        DataTable dtdtls = new DataTable();
                        adp.Fill(dtdtls);

                        if (dtdtls.Rows.Count > 0)
                        {
                            qty = dtdtls.Rows[0]["qty"].ToString();
                            rate = dtdtls.Rows[0]["rate"].ToString();
                            discount = dtdtls.Rows[0]["discount"].ToString();
                            amt = dtdtls.Rows[0]["amount"].ToString();
                        }
                        if (Convert.ToInt32(Qty.Text) > Convert.ToInt32(qty))
                        {
                            UpdatedQty = Convert.ToInt32(Qty.Text);
                            UpdatedDiscount = Convert.ToDouble(Discount.Text);
                            UpdatedAmt = Convert.ToDouble(TotalAmount.Text);
                        }
                        else if (Convert.ToInt32(Qty.Text) < Convert.ToInt32(qty))
                        {
                            UpdatedQty = Convert.ToInt32(Qty.Text);
                            UpdatedDiscount = Convert.ToDouble(Discount.Text);
                            UpdatedAmt = Convert.ToDouble(TotalAmount.Text);
                        }
                        else
                        {
                            UpdatedQty = Convert.ToInt32(qty);
                            if (discount == "")
                            {
                                discount = "0";
                            }
                            UpdatedDiscount = Convert.ToDouble(discount);
                            UpdatedAmt = Convert.ToDouble(amt);
                        }

                        if (Request.QueryString["cmded"] != null)
                        {

                        }
                        else
                        {
                            if (UpdatedQty == Convert.ToInt32(Qty.Text))
                            {
                                SqlCommand cmdupdate = new SqlCommand("update tblQuotationData set IsSelect=1, qty='" + UpdatedQty + "',discount='" + UpdatedDiscount + "',amount='" + UpdatedAmt + "' where id='" + ID.Text + "'", con);
                                cmdupdate.ExecuteNonQuery();

                                string query = "INSERT INTO OrderAcceptDtls ([OAno],[Description],[Qty],[Price],[Discount],[TotalAmount],[CGST],[SGST],[IGST],[SubOANumber],[UOM],[QuotationID],[IsComplete],[HSN])" +
                                "VALUES('" + txtOAno.Text + "','" + Desci.Text + "','" + Qty.Text + "','" + Price.Text + "','" + Discount.Text + "'," +
                                "'" + TotalAmount.Text + "','" + CGST.Text + "','" + SGST.Text + "','" + IGST.Text + "','" + SubOA + "','" + txtunit.Text + "','" + ID.Text + "',NULL,'" + HSN.Text + "')";
                                cmdPrDtl.CommandText = query;
                                cmdPrDtl.Connection = con;
                                cmdPrDtl.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmdupdate = new SqlCommand("update tblQuotationData set IsSelect=1, qty='" + UpdatedQty + "',discount='" + UpdatedDiscount + "',amount='" + UpdatedAmt + "' where id='" + ID.Text + "'", con);
                                cmdupdate.ExecuteNonQuery();

                                string query = "INSERT INTO OrderAcceptDtls ([OAno],[Description],[Qty],[Price],[Discount],[TotalAmount],[CGST],[SGST],[IGST],[SubOANumber], [UOM],[QuotationID],[IsComplete],[HSN])" +
                                "VALUES('" + txtOAno.Text + "','" + Desci.Text + "','" + Qty.Text + "','" + Price.Text + "','" + Discount.Text + "'," +
                                "'" + TotalAmount.Text + "','" + CGST.Text + "','" + SGST.Text + "','" + IGST.Text + "','"+ SubOA + "','" + txtunit.Text + "','" + ID.Text + "',NULL,'" + HSN.Text + "')";
                                cmdPrDtl.CommandText = query;
                                cmdPrDtl.Connection = con;
                                cmdPrDtl.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        if (Request.QueryString["cmded"] != null)
                        {
                            Label ID = (g1.Cells[1].FindControl("lblID") as Label);

                            SqlCommand cmdUpdate = new SqlCommand("update tblQuotationData set IsSelect=0 where id='" + ID.Text + "'", con);
                            cmdUpdate.ExecuteNonQuery();

                            SqlCommand cmdDelete = new SqlCommand("update tblOrderAcceptDtls set IsComplete=0 where QuotationID='" + ID.Text + "'", con);
                            cmdDelete.ExecuteNonQuery();
                        }
                    }
                }
            }
            if (Request.QueryString["cmded"] != null)
            {
                //SqlCommand cmddelete = new SqlCommand("delete from OrderAcceptDtls where OAno='" + txtOAno.Text + "'", con);
                //cmddelete.ExecuteNonQuery();

                foreach (GridViewRow g1 in dgvOrderAcceptDtl.Rows)
                {
                    CheckBox chk = (g1.Cells[0].FindControl("chkSelect") as CheckBox);
                    if (chk != null & chk.Checked)
                    {
                        string SubOA = GeneratesubOACode();
                        Label ID = (g1.Cells[1].FindControl("lblID") as Label);
                        Label HSN = (g1.Cells[1].FindControl("lblhsncode") as Label);
                        TextBox Desci = (g1.Cells[2].FindControl("txtdescription") as TextBox);
                        TextBox Qty = (g1.Cells[2].FindControl("txtQty") as TextBox);
                        TextBox Price = (g1.Cells[2].FindControl("txtprice") as TextBox);
                        TextBox Discount = (g1.Cells[2].FindControl("txtdiscount") as TextBox);
                        TextBox CGST = (g1.Cells[2].FindControl("txtCGST") as TextBox);
                        TextBox SGST = (g1.Cells[2].FindControl("txtSGST") as TextBox);
                        TextBox IGST = (g1.Cells[2].FindControl("txtIGST") as TextBox);
                        TextBox TotalAmount = (g1.Cells[2].FindControl("txtamount") as TextBox);
                        Label lblSuboa = (g1.Cells[2].FindControl("lblSuboa") as Label);
                        Label lblqoutationId = (g1.Cells[2].FindControl("lblqoutationId") as Label);
                        TextBox txtunit = (g1.Cells[2].FindControl("txtunit") as TextBox);
                        //string query = "INSERT INTO OrderAcceptDtls ([OAno],[Description],[Qty],[Price],[Discount],[TotalAmount],[CGST],[SGST],[IGST],[SubOANumber],[QuotationID],[IsComplete],[HSN])" +
                        //"VALUES('" + txtOAno.Text + "','" + Desci.Text + "','" + Qty.Text + "','" + Price.Text + "','" + Discount.Text + "'," +
                        //"'" + TotalAmount.Text + "','" + CGST.Text + "','" + SGST.Text + "','" + IGST.Text + "','" + SubOA + "','" + ID.Text + "',NULL,'" + HSN.Text + "')";

                        string query = "UPDATE OrderAcceptDtls SET [OAno] = '" + txtOAno.Text + "',[Description] = '" + Desci.Text + "',[Qty] = '" + Qty.Text + "',[Price] = '" + Price.Text + "'," +
                             "[Discount] = '" + Discount.Text + "',[TotalAmount] = '" + TotalAmount.Text + "',[CGST] = '" + CGST.Text + "',[SGST] = '" + SGST.Text + "',[IGST] = '" + IGST.Text + "',[IsComplete] = NULL," +
                             "[SubOANumber] = '" + lblSuboa.Text + "',[QuotationID] = '" + lblqoutationId.Text + "',[HSN] = '" + HSN.Text + "' ,[UOM] = '" + txtunit.Text + "' WHERE id='" + ID.Text + "'";
                        cmdPrDtl.CommandText = query;
                        cmdPrDtl.Connection = con;
                        cmdPrDtl.ExecuteNonQuery();
                    }
                    else
                    {

                    }
                }
            }


        }
        catch (Exception ex)
        {
            //tran.Rollback();
        }
        finally
        {
            con.Close();
        }
        // End Insert all Product-------------------------------------------------------------------------


        int MaxID = 0;
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [OrderAccept]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            MaxID = Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
        }
        else
        {
            //ComCode = string.Empty;
        }

        SqlCommand cmddata = new SqlCommand("SP_OAFileData", con);
        cmddata.CommandType = CommandType.StoredProcedure;
        cmddata.Parameters.AddWithValue("@OAid", MaxID);
        if (FileUpload1.HasFile)
        {
            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string[] OAfilename = filename.Split('.');
                string OAfilename1 = OAfilename[0];
                string filenameExt = OAfilename[1];
                string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                postedFile.SaveAs(Server.MapPath("~/OAFiles/") + OAfilename1 + time1 + "." + filenameExt);
                cmddata.Parameters.AddWithValue("@filepath1", "OAFiles/" + OAfilename1 + time1 + "." + filenameExt);
            }
        }
        else
        {
            cmddata.Parameters.AddWithValue("@filepath1", DBNull.Value);
        }
        if (FileUpload2.HasFile)
        {
            foreach (HttpPostedFile postedFile in FileUpload2.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string[] OAfilename = filename.Split('.');
                string OAfilename1 = OAfilename[0];
                string filenameExt = OAfilename[1];
                string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                postedFile.SaveAs(Server.MapPath("~/OAFiles/") + OAfilename1 + time1 + "." + filenameExt);
                cmddata.Parameters.AddWithValue("@filepath2", "OAFiles/" + OAfilename1 + time1 + "." + filenameExt);
            }
        }
        else
        {
            cmddata.Parameters.AddWithValue("@filepath2", DBNull.Value);
        }
        if (FileUpload3.HasFile)
        {
            foreach (HttpPostedFile postedFile in FileUpload3.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string[] OAfilename = filename.Split('.');
                string OAfilename1 = OAfilename[0];
                string filenameExt = OAfilename[1];
                string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                postedFile.SaveAs(Server.MapPath("~/OAFiles/") + OAfilename1 + time1 + "." + filenameExt);
                cmddata.Parameters.AddWithValue("@filepath3", "OAFiles/" + OAfilename1 + time1 + "." + filenameExt);
            }
        }
        else
        {
            cmddata.Parameters.AddWithValue("@filepath3", DBNull.Value);
        }
        if (FileUpload4.HasFile)
        {
            foreach (HttpPostedFile postedFile in FileUpload4.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string[] OAfilename = filename.Split('.');
                string OAfilename1 = OAfilename[0];
                string filenameExt = OAfilename[1];
                string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                postedFile.SaveAs(Server.MapPath("~/OAFiles/") + OAfilename1 + time1 + "." + filenameExt);
                cmddata.Parameters.AddWithValue("@filepath4", "OAFiles/" + OAfilename1 + time1 + "." + filenameExt);
            }
        }
        else
        {
            cmddata.Parameters.AddWithValue("@filepath4", DBNull.Value);
        }

        if (FileUpload5.HasFile)
        {
            foreach (HttpPostedFile postedFile in FileUpload5.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string[] OAfilename = filename.Split('.');
                string OAfilename1 = OAfilename[0];
                string filenameExt = OAfilename[1];
                string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                postedFile.SaveAs(Server.MapPath("~/OAFiles/") + OAfilename1 + time1 + "." + filenameExt);
                cmddata.Parameters.AddWithValue("@filepath5", "OAFiles/" + OAfilename1 + time1 + "." + filenameExt);
            }
        }
        else
        {
            cmddata.Parameters.AddWithValue("@filepath5", DBNull.Value);
        }
        //cmddata.Parameters.AddWithValue("@createdDate", DateTime.Now);
        cmddata.Parameters.AddWithValue("@Createdby", Session["name"].ToString());

        try
        {
            con.Open();
            cmddata.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error Generated. Details: " + ex.ToString());
        }

        if (ddloatocustomer.SelectedItem.Text == "Yes")
        {
            //Sendemail();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Order Details has been saved !!!');window.location='OrderAcceptanceList.aspx';", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill quantity and price !!!');", true);
        //}
    }

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

        SqlDataAdapter adpfill = new SqlDataAdapter(@"SELECT [id],[quotationid],[OAno],[currentdate],[customername],[address],
      [quotationno],convert(varchar(20),[quotationdate],105) as quotationdate,[pono],convert(varchar(20),[podate],105) as podate
      ,[contactpersonpurchase],[contpersonpurcontact],[contactpersontechnical],[contpersontechcontact],[description]
      ,[qty],[drgref],[price],[totamt],convert(varchar(20),[deliverydatereqbycust],105) as deliverydatereqbycust,
       convert(varchar(20),[deliverydatecommbyus],105) as deliverydatecommbyus,[cgst],[sgst],[igst],[cgstamt]
      ,[sgstamt],[igstamt],[packing1],[packing2],[packing3],[note1],[note2],[note3],[note4],[note5],[note6],[note7]
      ,[note8],[note9],[DeliveryTransportation],[termaofpayment],[billingdetails],[buyer],[consignee],[specialinstruction1]
      ,[specialinstruction2],[specialinstruction3],[specialinstruction4],[specialinstruction5],[specialinstruction6]
      ,[specialinstruction7],[specialinstruction8],[sendtoemail1],[sendtoemail2],[sendtoemail3],[sendtoemail4]
      ,[sendtoemail5],[custemail],[status]  FROM [OrderAccept] where quotationid=" + quotid, con);
        DataTable dtfill = new DataTable();
        adpfill.Fill(dtfill);

        //ReportDocument cryRpt = new ReportDocument();

        ////////New Quotation//////////
        //cryRpt.Load(Server.MapPath(string.Format("../OAReport.rpt", 1)));

        //crConnectionInfo.ServerName = servername;
        //crConnectionInfo.DatabaseName = dbname;
        //crConnectionInfo.UserID = userid;
        //crConnectionInfo.Password = pass;

        //if (dtfill.Rows.Count > 0)
        //{
        //    cryRpt.SetParameterValue("oano", dtfill.Rows[0]["OAno"].ToString());
        //    cryRpt.SetParameterValue("oadate", dtfill.Rows[0]["currentdate"].ToString());
        //    cryRpt.SetParameterValue("cutomername", dtfill.Rows[0]["customername"].ToString());
        //    cryRpt.SetParameterValue("address", dtfill.Rows[0]["address"].ToString());
        //    cryRpt.SetParameterValue("quotationno", dtfill.Rows[0]["quotationno"].ToString());

        //    cryRpt.SetParameterValue("quotationdate", dtfill.Rows[0]["quotationdate"].ToString());
        //    cryRpt.SetParameterValue("pono", dtfill.Rows[0]["pono"].ToString());
        //    cryRpt.SetParameterValue("podate", dtfill.Rows[0]["podate"].ToString());
        //    cryRpt.SetParameterValue("contpersonpurchase", dtfill.Rows[0]["contactpersonpurchase"].ToString());
        //    cryRpt.SetParameterValue("purcontactno", dtfill.Rows[0]["contpersonpurcontact"].ToString());

        //    cryRpt.SetParameterValue("contactpertechnical", dtfill.Rows[0]["contactpersontechnical"].ToString());
        //    cryRpt.SetParameterValue("technicalcontactno", dtfill.Rows[0]["contpersontechcontact"].ToString());
        //    cryRpt.SetParameterValue("description", dtfill.Rows[0]["description"].ToString());
        //    cryRpt.SetParameterValue("qty", dtfill.Rows[0]["qty"].ToString());
        //    cryRpt.SetParameterValue("drgref", dtfill.Rows[0]["drgref"].ToString());

        //    cryRpt.SetParameterValue("price", dtfill.Rows[0]["price"].ToString());
        //    cryRpt.SetParameterValue("specialnotes", dtfill.Rows[0]["totamt"].ToString());
        //    cryRpt.SetParameterValue("deliverydatebycust", dtfill.Rows[0]["deliverydatereqbycust"].ToString());
        //    cryRpt.SetParameterValue("deliverydatebyus", dtfill.Rows[0]["deliverydatecommbyus"].ToString());
        //    cryRpt.SetParameterValue("cgstper", dtfill.Rows[0]["cgst"].ToString());

        //    cryRpt.SetParameterValue("sgstper", dtfill.Rows[0]["sgst"].ToString());
        //    cryRpt.SetParameterValue("igstper", dtfill.Rows[0]["igst"].ToString());
        //    cryRpt.SetParameterValue("cgstamt", dtfill.Rows[0]["cgstamt"].ToString());
        //    cryRpt.SetParameterValue("sgstamt", dtfill.Rows[0]["sgstamt"].ToString());
        //    cryRpt.SetParameterValue("igstamt", dtfill.Rows[0]["igstamt"].ToString());
        //    cryRpt.SetParameterValue("amount", dtfill.Rows[0]["totamt"].ToString());

        //    cryRpt.SetParameterValue("deliverytransportcharge", dtfill.Rows[0]["DeliveryTransportation"].ToString());
        //    cryRpt.SetParameterValue("termaofpayment", dtfill.Rows[0]["termaofpayment"].ToString());
        //    cryRpt.SetParameterValue("billingdetails", dtfill.Rows[0]["billingdetails"].ToString());
        //    cryRpt.SetParameterValue("buyer", dtfill.Rows[0]["buyer"].ToString());
        //    cryRpt.SetParameterValue("consignee", dtfill.Rows[0]["consignee"].ToString());

        //    /////Packing
        //    StringBuilder sbpacking = new StringBuilder();
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["packing1"].ToString()))
        //    {
        //        sbpacking.Append(dtfill.Rows[0]["packing1"].ToString());
        //        sbpacking.Append(" ");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["packing2"].ToString()))
        //    {
        //        sbpacking.Append(dtfill.Rows[0]["packing2"].ToString());
        //        sbpacking.Append(" ");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["packing3"].ToString()))
        //    {
        //        sbpacking.Append(dtfill.Rows[0]["packing3"].ToString());
        //    }

        //    //Special Instructions
        //    StringBuilder sbspecialnotes = new StringBuilder();
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["specialinstruction1"].ToString()))
        //    {
        //        sbspecialnotes.Append(dtfill.Rows[0]["specialinstruction1"].ToString());
        //        sbspecialnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["specialinstruction2"].ToString()))
        //    {
        //        sbspecialnotes.Append(dtfill.Rows[0]["specialinstruction2"].ToString());
        //        sbspecialnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["specialinstruction3"].ToString()))
        //    {
        //        sbspecialnotes.Append(dtfill.Rows[0]["specialinstruction3"].ToString());
        //        sbspecialnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["specialinstruction4"].ToString()))
        //    {
        //        sbspecialnotes.Append(dtfill.Rows[0]["specialinstruction4"].ToString());
        //        sbspecialnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["specialinstruction5"].ToString()))
        //    {
        //        sbspecialnotes.Append(dtfill.Rows[0]["specialinstruction5"].ToString());
        //        sbspecialnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["specialinstruction6"].ToString()))
        //    {
        //        sbspecialnotes.Append(dtfill.Rows[0]["specialinstruction6"].ToString());
        //        sbspecialnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["specialinstruction7"].ToString()))
        //    {
        //        sbspecialnotes.Append(dtfill.Rows[0]["specialinstruction7"].ToString());
        //        sbspecialnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["specialinstruction8"].ToString()))
        //    {
        //        sbspecialnotes.Append(dtfill.Rows[0]["specialinstruction8"].ToString());
        //    }

        //    //Notes
        //    StringBuilder sbsnotes = new StringBuilder();
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note1"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note1"].ToString());
        //        sbsnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note2"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note2"].ToString());
        //        sbsnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note3"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note3"].ToString());
        //        sbsnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note4"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note4"].ToString());
        //        sbsnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note5"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note5"].ToString());
        //        sbsnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note6"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note6"].ToString());
        //        sbsnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note7"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note7"].ToString());
        //        sbsnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note8"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note8"].ToString());
        //        sbsnotes.Append("<br>");
        //    }
        //    if (!string.IsNullOrEmpty(dtfill.Rows[0]["note9"].ToString()))
        //    {
        //        sbsnotes.Append(dtfill.Rows[0]["note9"].ToString());
        //    }

        //    cryRpt.SetParameterValue("packing", sbpacking.ToString());
        //    cryRpt.SetParameterValue("specialnotes", sbspecialnotes.ToString());
        //    cryRpt.SetParameterValue("notes", sbsnotes.ToString());
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
        string ToMailID = "excelenclosures@gmail.com";
        //string ToMailID = "pushpendra@weblinkservices.net";

        MailMessage mm = new MailMessage();
        mm.From = new MailAddress(FromMailID);

        mm.Subject = "Order Acceptance Report from Excel Enclosures";
        mm.To.Add(ToMailID);

        if (!string.IsNullOrEmpty(txtemail1.Text))
        {
            mm.To.Add(txtemail1.Text);
        }
        if (!string.IsNullOrEmpty(txtemail2.Text))
        {
            mm.To.Add(txtemail2.Text);
        }
        if (!string.IsNullOrEmpty(txtemail3.Text))
        {
            mm.To.Add(txtemail3.Text);
        }
        if (!string.IsNullOrEmpty(txtemail4.Text))
        {
            mm.To.Add(txtemail4.Text);
        }
        if (!string.IsNullOrEmpty(txtemail5.Text))
        {
            mm.To.Add(txtemail5.Text);
        }

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
        //mm.Attachments.Add(new Attachment(new MemoryStream(attachment), "OA Report.pdf"));


        //string[] abc = filename.Split('/');
        //string abc2 = abc[0].ToString();
        //string newfilename = abc2 + "-" + party + ".pdf";
        // 

        //if (!string.IsNullOrEmpty(oldfile2))
        //{
        //    byte[] file = File.ReadAllBytes((Server.MapPath("~/RefDocument/")) + oldfile2);

        //    Stream stream = new MemoryStream(file);
        //    Attachment aa = new Attachment(stream, oldfile1);
        //    mm.Attachments.Add(aa);
        //}

        mm.Body = @"<html><body style='border:1px solid #148bc4;padding:10px;width:600px;'><b style='font-size:15px';><u>Order Acceptance Details</u></b><br><br><b>
            OA No : </b> " + txtOAno.Text + " <br> <b>Date : </b> " + txttodaysdate.Text +
        "<br> <b>Customer Name: </b> " + txtcustomername.Text + "  <br><b> Quotation No: </b> " + ddlQuotationNo.SelectedItem.Text + "<br><b> Quotation Date : </b> " + txtquotationdate.Text + "<br><b> PO No : </b> " + txtpono.Text + "<br><b> Billing Details : </b> " + txtbillingdetails.Text + " <br><b> Total Amount : " + txtTotalamt.Text + "/- Rs. </b> <br><b> Download Order Acceptance : <a href='http://www.weblinkservices.in/Reports/OARptPDF.aspx?ID=" + quotid + "'>Download Order Acceptance Invoice</a></b> </body></html> ";
        smtp.Send(mm);
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

    protected void btnreset_Click(object sender, EventArgs e)
    {

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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    public string Between(string STR, string FirstString, string LastString)
    {
        string FinalString = "";
        if (Request.QueryString["cmd"] != null)
        {

            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
        else
        {
            return FinalString = STR;
        }
    }

    protected void dgvOrderAcceptDtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Request.QueryString["cmd"] != null)
                {

                }
                else
                {
                    Label lblID = (Label)e.Row.FindControl("lblID");
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select * from OrderAcceptDtls where id='" + lblID.Text + "'", con);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Label lblSuboa = (Label)e.Row.FindControl("lblSuboa");
                        lblSuboa.Text = dt.Rows[0]["SubOANumber"].ToString();
                        Label lblqoutationId = (Label)e.Row.FindControl("lblqoutationId");
                        lblqoutationId.Text = dt.Rows[0]["QuotationID"].ToString();
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
    }

    private void calculationA(GridViewRow row)
    {
        TextBox txt_Qty = (TextBox)row.FindControl("txtQty");
        TextBox txt_price = (TextBox)row.FindControl("txtprice");
        TextBox txt_CGST = (TextBox)row.FindControl("txtCGST");
        TextBox txt_SGST = (TextBox)row.FindControl("txtSGST");
        TextBox txt_IGST = (TextBox)row.FindControl("txtIGST");
        TextBox txt_amount = (TextBox)row.FindControl("txtamount");
        TextBox txt_discount = (TextBox)row.FindControl("txtdiscount");

        var totalamt = Convert.ToDecimal(txt_Qty.Text.Trim()) * Convert.ToDecimal(txt_price.Text.Trim());

        var CGSTamt = totalamt * (Convert.ToDecimal(txt_CGST.Text.Trim())) / 100;
        var SGSTamt = totalamt * (Convert.ToDecimal(txt_SGST.Text.Trim())) / 100;
        var IGSTamt = totalamt * (Convert.ToDecimal(txt_IGST.Text.Trim())) / 100;

        var GSTtotal = SGSTamt + CGSTamt;

        //var NetAmt = totalamt + GSTtotal;
        var NetAmt = totalamt;

        decimal AmtWithDiscount;
        if (txt_discount.Text != "" || txt_discount.Text != null)
        {
            var disc = NetAmt * (Convert.ToDecimal(txt_discount.Text.Trim() == "" ? "0" : txt_discount.Text.Trim())) / 100;

            AmtWithDiscount = NetAmt - disc;
        }
        else
        {
            AmtWithDiscount = 0;
        }

        txt_amount.Text = AmtWithDiscount.ToString();
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
        TextBox txt_Qty = (TextBox)row.FindControl("txtQty");
        TextBox txt_discount = (TextBox)row.FindControl("txtdiscount");
        TextBox txtCGST = (TextBox)row.FindControl("txtCGST");
        TextBox txtSGST = (TextBox)row.FindControl("txtSGST");
        TextBox txtIGST = (TextBox)row.FindControl("txtIGST");
        CheckBox chk = (CheckBox)row.FindControl("chkSelect");

        if (chk != null & chk.Checked)
        {
            txt_Qty.Enabled = true;
            txt_discount.Enabled = true;
            txtCGST.ReadOnly = false;
            txtSGST.ReadOnly = false;
            txtIGST.ReadOnly = false;
        }
        else
        {
            txt_Qty.Enabled = false;
            txt_discount.Enabled = false;
            txtCGST.ReadOnly = true;
            txtSGST.ReadOnly = true;
            txtIGST.ReadOnly = true;
        }


    }

    protected void ddlQuotationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        getQutationdts();
    }

    protected void txtdiscount_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
    }
    protected void txtprice_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
    }


    protected void ddltaxation_TextChanged(object sender, EventArgs e)
    {

    }

    public void GetTaxType()
    {

        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("[SP_TaxMaster]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetTaxType"));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        ddltaxation.DataSource = Dt;
                        ddltaxation.DataValueField = "TaxCategory";
                        ddltaxation.DataTextField = "TaxCategory";
                        ddltaxation.DataBind();
                        ddltaxation.Items.Insert(0, "--Select Type--");

                    }

                }
            }
        }
        catch (Exception ex)
        {

            //throw;
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }

    public void GstForDirect()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("[SP_TaxMaster]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetGst"));
                    cmd.Parameters.Add(new SqlParameter("@TaxCategory", ddltaxation.SelectedItem.Text));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        txtcgstper.Text = Dt.Rows[0]["CGST"].ToString();
                        txtsgstper.Text = Dt.Rows[0]["SGST"].ToString();
                        txtigstper.Text = Dt.Rows[0]["IGST"].ToString();


                    }

                }
            }
        }
        catch (Exception ex)
        {

            //throw;
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }

    public void InsertDeatils()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("[SP_SendForOA]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Action", "InsertOAlistData"));
                cmd.Parameters.Add(new SqlParameter("@quotationid", HdnOAID.Value));
                SqlParameter messageParameter = new SqlParameter("@message", string.Empty) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(messageParameter);
                cmd.ExecuteNonQuery();
                string message = (string)messageParameter.Value;

            }
        }
    }
}