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
using System.Threading;
using System.Activities.Expressions;

public partial class Admin_ManualOrderAcceptance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    string OAid = ""; static int quotid; static string QuotationCode; static string action; static int id; static string ccode; static string emailstatus;

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
                ViewState["RowNo"] = 0;
                Autofill();
                GetTaxType();
                if (Request.QueryString["cmded"] != null)
                {
                    OAid = Decrypt(Request.QueryString["cmded"].ToString());
                    Getdata();
                    GetOAfileData();
                    action = "update";
                    tdddlPerticular.Visible = true;
                    tdddlPerticular1.Visible = true;
                    divamt.Visible = true;
                    ddltaxation.Enabled = false;
                }
                else
                {
                    txtpodate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    divamt.Visible = false;
                    GenerateCode();
                    GenerateQuotationCode();
                    //txtOAno.Text = Code();
                }

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[12] { new DataColumn("id"), new DataColumn("Perticulars"),
                new DataColumn("Description"),  new DataColumn("HSN"),new DataColumn("Qty"), new DataColumn("UOM"), new DataColumn("Price")
                , new DataColumn("Discount"), new DataColumn("CGST"), new DataColumn("SGST")
                , new DataColumn("IGST"), new DataColumn("TotalAmount") });
                ViewState["Products"] = dt;
            }
        }
    }


         protected string Code()
    {
        string FinYear = null;
        string FinFullYear = null;
        if (DateTime.Today.Month > 3)
        {
            FinYear = DateTime.Today.AddYears(1).ToString("yy");
            FinFullYear = DateTime.Today.AddYears(1).ToString("yyyy");
        }
        else
        {
            var finYear = DateTime.Today.AddYears(1).ToString("yy");
            FinYear = (Convert.ToInt32(finYear) - 1).ToString();

            var finfYear = DateTime.Today.AddYears(1).ToString("yyyy");
            FinFullYear = (Convert.ToInt32(finfYear) - 1).ToString();
        }
        string previousyear = (Convert.ToDecimal(FinFullYear) - 1).ToString();
        string strInvoiceNumber = "";
        string OANO = "";
        string fY = previousyear.ToString() + "-" + FinYear;
        //string strSelect = @"select ISNULL(MAX(OAno), '') AS maxno from OrderAccept where OAno like '%" + fY + "%'";
        //string strSelect = @"SELECT TOP 1 OAno FROM OrderAcceptDtls where OAno like '%" + fY + "%' ORDER BY ID DESC";        //Old Query
        string strSelect = @"select ISNULL(MAX(id), '') AS maxno from OrderAccept where OAno like '%" + fY + "%' ";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSelect;
        con.Open();
        string resultID = cmd.ExecuteScalar().ToString();
        string strSelectOaNo = @"select OAno from OrderAccept where id='" + resultID + "' ";
        SqlCommand cmdd = new SqlCommand();
        cmdd.Connection = con;
        cmdd.CommandType = CommandType.Text;
        cmdd.CommandText = strSelectOaNo;

        string result = cmdd.ExecuteScalar().ToString();

        //string result = "";
        con.Close();
        if (result != "")
        {
            string str = result;
            int n = 7;

            str = str.Remove(0, n);

            int numbervalue = Convert.ToInt32(str.Substring(str.IndexOf("/") + 1, str.Length - (str.IndexOf("/") + 1)));
            numbervalue = numbervalue + 1;
            strInvoiceNumber = str.Substring(0, str.IndexOf("/") + 1) + "" + numbervalue.ToString("00");

            txtOAno.Text = " PA/OA/" + strInvoiceNumber;
            OANO = txtOAno.Text;
        }
        else
        {
            OANO = "PA/OA/" + previousyear.ToString() + "-" + FinYear + "/" + "01";
        }
        return OANO;

    }
	
	     //protected string Code()
    //{
    //    string FinYear = null;
    //    string FinFullYear = null;
    //    if (DateTime.Today.Month > 3)
    //    {
    //        FinYear = DateTime.Today.AddYears(1).ToString("yy");
    //        FinFullYear = DateTime.Today.AddYears(1).ToString("yyyy");
    //    }
    //    else
    //    {
    //        var finYear = DateTime.Today.AddYears(1).ToString("yy");
    //        FinYear = (Convert.ToInt32(finYear) - 1).ToString();

    //        var finfYear = DateTime.Today.AddYears(1).ToString("yyyy");
    //        FinFullYear = (Convert.ToInt32(finfYear) - 1).ToString();
    //    }
    //    string previousyear = (Convert.ToDecimal(FinFullYear) - 1).ToString();
    //    string strInvoiceNumber = "";
    //    string OANO = "";
    //    string fY = previousyear.ToString() + "-" + FinYear;
    //    // string strSelect = @"select ISNULL(MAX(OAno), '') AS maxno from OrderAccept where OAno like '%" + fY + "%'";
    //    //string strSelect = @"SELECT TOP 1 OAno FROM OrderAcceptDtls where OAno like '%" + fY + "%' ORDER BY ID DESC";        //Old Query
    //    string strSelect = @"select ISNULL(MAX(id), '') AS maxno from OrderAccept where OAno like '%" + fY + "%' ";
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = con;
    //    cmd.CommandType = CommandType.Text;
    //    cmd.CommandText = strSelect;
    //    con.Open();
    //    string result = cmd.ExecuteScalar().ToString();
    //    //string strSelectOaNo = @"select OAno from OrderAccept where id='" + resultID + "' ";
    //    //SqlCommand cmdd = new SqlCommand();
    //    //cmdd.Connection = con;
    //    //cmdd.CommandType = CommandType.Text;
    //    //cmdd.CommandText = strSelectOaNo;

    //    //string result = cmdd.ExecuteScalar().ToString();

    //    //string result = "";
    //    con.Close();
    //    if (result != "0")
    //    {
    //        string str = result;
    //        int n = 7;

    //        str = str.Remove(0, n);

    //        int numbervalue = Convert.ToInt32(str.Substring(str.IndexOf("/") + 1, str.Length - (str.IndexOf("/") + 1)));
    //        numbervalue = numbervalue + 1;
    //        strInvoiceNumber = str.Substring(0, str.IndexOf("/") + 1) + "" + numbervalue.ToString("00");

    //        txtOAno.Text = " EE/OA/" + strInvoiceNumber;
    //        OANO = txtOAno.Text;
    //    }
    //    else
    //    {
    //        OANO = " EE/OA/" + previousyear.ToString() + "-" + FinYear + "/" + "01";
    //    }
    //    return OANO;

    //}


    protected void Insert(object sender, EventArgs e)
    {
        if (txtQty.Text == "" || txtprice.Text == "" || txtTotalamt.Text == "" || txtdrgref.Text=="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill quantity ,price and Discount !!!');", true);
            txtQty.Focus();
        }
        else
        {
            ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;
            DataTable dt = (DataTable)ViewState["Products"];
            dt.Rows.Add(ViewState["RowNo"], ddlPerticuler.Text.Trim(), txtdescription.Text, txtHSN.Text, txtQty.Text, txtuom.Text, txtprice.Text, txtdrgref.Text, txtcgstper.Text, txtsgstper.Text, txtigstper.Text, txtTotalamt.Text);
            ViewState["Products"] = dt;
            ddlPerticuler.SelectedValue = "0";
            txtdescription.Text = string.Empty;
            txtHSN.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtuom.Text = string.Empty;
            txtprice.Text = string.Empty;
            txtdrgref.Text = string.Empty;
            txtcgstper.Text = string.Empty;
            txtsgstper.Text = string.Empty;
            txtigstper.Text = string.Empty;
            txtTotalamt.Text = string.Empty;

            dgvProductDtl.DataSource = (DataTable)ViewState["Products"];
            dgvProductDtl.DataBind();
            GstForDirect();
        }
    }

    protected void Getdata()
    {
        try
        {
            if (Request.QueryString["cmded"] != null)
            {
                SqlCommand scmd = new SqlCommand("select id from [DB_ProcetechTesting].[dbo].[OrderAccept] where OAno='" + OAid + "'", con);
                con.Open();
                id = Convert.ToInt32(scmd.ExecuteScalar());
                ViewState["OAID"] = id;
                con.Close();
            }
            // New Column Added in below code JobNo and table schema given 
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
      ,[sendtoemail5],[custemail],[status],[emailstatus],[TaxType],[JobNo] FROM [DB_ProcetechTesting].[dbo].[OrderAccept] where OAno='" + OAid + "'", con);

            DataTable dtfill = new DataTable();
            adpfill.Fill(dtfill);

            if (dtfill.Rows.Count > 0)
            {

                // New Field Add by Nikhil to get JobNo  06-12-2024
                txtJobNo.Text = dtfill.Rows[0]["JobNo"].ToString();
                txtJobNo.ReadOnly = true;
                
                HFQuotationid.Value = dtfill.Rows[0]["quotationid"].ToString();
                txtOAno.Text = dtfill.Rows[0]["OAno"].ToString();
                txtOAno.ReadOnly = true;
                txttodaysdate.Text = dtfill.Rows[0]["currentdate"].ToString();
                txttodaysdate.ReadOnly = true;
                txtcustomername.Text = dtfill.Rows[0]["customername"].ToString();

                divGstaddres.Visible = false;

                txtaddress.Text = dtfill.Rows[0]["address"].ToString();
                txtquotationno.Text = dtfill.Rows[0]["quotationno"].ToString();
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
                //lblsumaofmaterialAmt.Text = dtfill.Rows[0]["totamt"].ToString();
                //txtTotalamt.Text = dtfill.Rows[0]["totamt"].ToString();
                txtdeliverydatebycustomer.Text = dtfill.Rows[0]["deliverydatereqbycust"].ToString();
                txtdeliverydatebyus.Text = dtfill.Rows[0]["deliverydatecommbyus"].ToString();
                ddltaxation.Text = dtfill.Rows[0]["TaxType"].ToString();
                GstForDirect();
                //txtcgstper.Text = dtfill.Rows[0]["cgst"].ToString();
                //txtsgstper.Text = dtfill.Rows[0]["sgst"].ToString();
                //txtigstper.Text = dtfill.Rows[0]["igst"].ToString();
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

                GetOADetails(dtfill.Rows[0]["OAno"].ToString());
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void GetOADetails(string OANo)
    {

        //SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM OrderAcceptDtls where OAno='" + OANo + "'", con);
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM [DB_ProcetechTesting].[DB_ProcetechERP].[OrderAcceptDtls] where OAno='" + OANo + "'", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dgvOADetailsLoad.DataSource = dt;
            dgvOADetailsLoad.DataBind();
        }
        else
        {
            dgvOADetailsLoad.DataSource = null;
            dgvOADetailsLoad.DataBind();
        }
    }

    private void GetOAfileData()
    {
        try
        {
            string query1 = string.Empty;
            //query1 = "SELECT TOP 1 [DocID],[OAid],[File1],[File2],[File3],[File4],[File5],[CeatedDate],[CreatedBy] FROM tblOAFiledata where OAid='" + id + "' ";
            query1 = "SELECT TOP 1 [DocID],[OAid],[File1],[File2],[File3],[File4],[File5],[CeatedDate],[CreatedBy] FROM [DB_ProcetechTesting].[DB_ProcetechERP].[tblOAFiledata] where OAid='" + id + "' ";
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

    protected void GenerateCode()
    {
       
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid, max(JobNo) as JobNo FROM [OrderAccept]", con);
        //qlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid, max(JobNo) as JobNo FROM [DB_ProcetechTesting].[dbo].[OrderAccept]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        int MxID = 0;
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["JobNo"].ToString() == "")
            {
                SqlCommand scmds = new SqlCommand("SELECT dbo.GenerateAndIncrementCodeForJOBNO() ", con);
                con.Open();
                string jobNo = Convert.ToString(scmds.ExecuteScalar());
                txtJobNo.Text = jobNo;
            }
            else
            {
                SqlCommand scmds = new SqlCommand("SELECT dbo.GenerateAndIncrementCodeForJOBNO() ", con);
                con.Open();
                string jobNo = Convert.ToString(scmds.ExecuteScalar());
                txtJobNo.Text = jobNo;
            }
            //Old Code
            //int maxid = dt.Rows[0]["maxid"].ToString() == "" ? MxID + 1 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1;
            //txtOAno.Text = "OA24250000" + maxid.ToString();

            // New Code By Nikhil 16-12-2024
            SqlCommand cmd = new SqlCommand("SP_GenerateNewOaNumber", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NewOAno", SqlDbType.NVarChar, 20).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string OaNo = Convert.ToString(cmd.Parameters["@NewOAno"].Value);

            txtOAno.Text = OaNo;

        }
        else
        {
            //ComCode = string.Empty;
        }
    }

    protected string GeneratesubOACode()
    {
        string subOA = "";
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [DB_ProcetechTesting].[DB_ProcetechERP].[OrderAcceptDtls]", con);
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

    protected void GenerateQuotationCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [QuotationMain]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //txQutno.Text = (Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1).ToString() + "-" + DateTime.Now.ToString("MM")+"/"+ DateTime.Now.ToString("yy") + "-" + DateTime.Now.AddYears(1).ToString("yy");
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
            txtquotationno.Text = QuotationCode = (maxid + 1).ToString() + "-" + "0" + "/" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.AddYears(1).ToString("yy");
        }
        else
        {
            QuotationCode = string.Empty;
        }
    }

    private void Filldata()
    {

        txtinstruction1.Text = "1.Inspection before powder coating is demanded by customer";

        txtinstruction2.Text = "2.Inspection after powder coating before dispatch";

        txtinstruction3.Text = "3.Send proforma 4 days before readiness";

        txtinstruction4.Text = "4.Get full payment against proforma";

        txtinstruction5.Text = "5.Get full payment against delivery";

        txtinstruction6.Text = "6.xx days PDC against proforma";

        txtinstruction7.Text = "7.xx days PDC against delivery";

        txtinstruction8.Text = "8.xx days LC";
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

    bool flg = true;
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //SqlCommand scmd = new SqlCommand("select max([quotationid]) from OAList", con);
        //con.Open();
        //quotid = scmd.ExecuteScalar() == DBNull.Value ? 0 + 1 : Convert.ToInt32(scmd.ExecuteScalar()) + 1;
        //con.Close();

        if (Request.QueryString["cmded"] != null)
        {
            flg = true;
            ////Update mainData
            //SqlCommand cmdmain = new SqlCommand("Update QuotationMain SET [quotationno]=@quotationno,[partyname]=@partyname,[kindatt]=@kindatt,[Toatlamt]=@Toatlamt,[descriptionall]=@descriptionall,[sessionname]=@sessionname,[createddate]=@createddate where quotationno=@quotationno", con);
            //cmdmain.Parameters.AddWithValue("@quotationno", txtquotationno.Text);
            //cmdmain.Parameters.AddWithValue("@partyname", txtcustomername.Text);
            //cmdmain.Parameters.AddWithValue("@kindatt", ddlcontactperson.Text);
            //cmdmain.Parameters.AddWithValue("@Toatlamt", txtTotalamt.Text);
            //cmdmain.Parameters.AddWithValue("@descriptionall", txtdescription.Text);
            //cmdmain.Parameters.AddWithValue("@sessionname", Session["ProductionName"].ToString());
            //cmdmain.Parameters.AddWithValue("@createddate", DateTime.Now);
            //con.Open();
            //cmdmain.ExecuteNonQuery();
            //con.Close();

            //Update OAList
            SqlCommand cmdIns = new SqlCommand("Update OAList SET oano=@oano,partyname=@partyname,description=@description,status='edit',createddate=getdate() where oano=@oano", con);
            //cmdIns.Parameters.AddWithValue("@quotationid", txtOAno.Text);
            cmdIns.Parameters.AddWithValue("@oano", txtOAno.Text);
            cmdIns.Parameters.AddWithValue("@partyname", txtcustomername.Text);
            // cmdIns.Parameters.AddWithValue("@quotationno", txtquotationno.Text);
            cmdIns.Parameters.AddWithValue("@description", txtdescription.Text);
            con.Open();
            cmdIns.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            if (dgvProductDtl.Rows.Count > 0)
            {
                flg = true;

                //Added OAList
                SqlCommand cmdIns = new SqlCommand("insert into OAList (oano,partyname,description,status,createddate)values(@oano, @partyname,  @description, 'edit', getdate())", con);
                //cmdIns.Parameters.AddWithValue("@quotationid", quotid);
                cmdIns.Parameters.AddWithValue("@oano", txtOAno.Text);
                cmdIns.Parameters.AddWithValue("@partyname", txtcustomername.Text);
                // cmdIns.Parameters.AddWithValue("@quotationno", txtquotationno.Text);
                cmdIns.Parameters.AddWithValue("@description", txtdescription.Text);
                con.Open();
                Thread.Sleep(2000);
                cmdIns.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                flg = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill Products...!!!');", true);
            }
        }
        if (flg == true)
        {
            SqlCommand cmd = new SqlCommand("[SP_OrderAccept]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ccode", ccode);
            cmd.Parameters.AddWithValue("@customername", txtcustomername.Text);

            // New Parameter added by Nikhil To add job No and in SP as well  06-12-2024
            cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
            
            cmd.Parameters.AddWithValue("@address", txtaddress.Text);
            //if (Request.QueryString["cmded"] != null)
            //{
            //    cmd.Parameters.AddWithValue("@quotationid", HFQuotationid.Value);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@quotationid", quotid);
            //}

            //cmd.Parameters.AddWithValue("@quotationno", Request.Form[txtquotationno.UniqueID].ToString());

            //if (Request.Form[txtquotationdate.UniqueID].ToString() != "")
            //{
            //    DateTime qdate = DateTime.ParseExact(Request.Form[txtquotationdate.UniqueID].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //    cmd.Parameters.AddWithValue("@quotationdate", qdate);
            //}
            cmd.Parameters.AddWithValue("@pono", txtpono.Text);

            if (Request.Form[txtpodate.UniqueID].ToString() != "")
            {
                DateTime podate = DateTime.ParseExact(Request.Form[txtpodate.UniqueID].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("@podate", podate);
            }

            cmd.Parameters.AddWithValue("@contactpersonpurchase", ddlcontactperson.Text);
            cmd.Parameters.AddWithValue("@contpersonpurcontact", txtpurchasecontactno.Text);
            cmd.Parameters.AddWithValue("@contactpersontechnical", txtcontactpersontechnical.Text);
            cmd.Parameters.AddWithValue("@contpersontechcontact", txttechnicalcontactno.Text);
            cmd.Parameters.AddWithValue("@description", txtdescription.Text);
            cmd.Parameters.AddWithValue("@qty", txtQty.Text);
            cmd.Parameters.AddWithValue("@drgref", txtdrgref.Text);
            cmd.Parameters.AddWithValue("@price", txtprice.Text);
            cmd.Parameters.AddWithValue("@TaxType", ddltaxation.SelectedItem.Text);



            if (Request.QueryString["cmded"] != null)
            {
                if (lblsumaofmaterialAmtLoad.Text != "")
                {
                    decimal Total = Convert.ToDecimal(lblsumaofmaterialAmtLoad.Text) + Convert.ToDecimal(lblsumaofmaterialAmt.Text);

                    cmd.Parameters.AddWithValue("@totamt", Total.ToString());
                }
                else
                {
                    Decimal Ammount= 0; 
                    decimal Total = Convert.ToDecimal(Ammount) + Convert.ToDecimal(lblsumaofmaterialAmt.Text);

                    cmd.Parameters.AddWithValue("@totamt", Total.ToString());
                }

          
            }
            else
            {
                cmd.Parameters.AddWithValue("@totamt", lblsumaofmaterialAmt.Text);
            }

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
            cmd.Parameters.AddWithValue("@cgstamt", txtcgstamt.Text);
            cmd.Parameters.AddWithValue("@sgstamt", txtsgstamt.Text);
            cmd.Parameters.AddWithValue("@igstamt", txtigstamt.Text);

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

            //if (Request.QueryString["cmd"] != null && Request.QueryString["cmded"] == null)
            //{
            //    GenerateCode();
            //}
            cmd.Parameters.AddWithValue("@OAno", Request.Form[txtOAno.UniqueID].ToString());

            if (action == "update")
            {
                if (ViewState["OAID"] != null)
                {
                    cmd.Parameters.AddWithValue("@id", ViewState["OAID"].ToString());
                    DateTime currentdate = DateTime.ParseExact(Request.Form[txttodaysdate.UniqueID].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    cmd.Parameters.AddWithValue("@currentdate", currentdate);
                }
            }

            if (ddloatocustomer.SelectedItem.Text == "Yes")
            {
                cmd.Parameters.AddWithValue("@emailstatus", "sent");
            }
            if (ddloatocustomer.SelectedItem.Text == "No")
            {
                cmd.Parameters.AddWithValue("@emailstatus", "Not Sent");
            }
            if (Request.QueryString["cmded"] != null)
            {
                action = "update";
            }
            else
            {
                action = "insert";
            }

            cmd.Parameters.AddWithValue("@action", action);
            cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
            con.Open();
            Thread.Sleep(5000);
            cmd.ExecuteNonQuery();
            con.Close();

            //Insert all Product-------------------------------------------------------------------------

            SqlCommand cmdPrDtl = new SqlCommand();
            con.Open();
            //SqlTransaction tran;
            //tran = con.BeginTransaction();
            //cmdPrDtl.Transaction = tran;
            string slno = null;
            try
            {
                foreach (GridViewRow g1 in dgvProductDtl.Rows)
                {
                    string SubOA = GeneratesubOACode();
                    string Description = g1.Cells[2].Text;
                    string HSN = g1.Cells[3].Text;
                    string Qty = g1.Cells[4].Text;
                    string UOM = g1.Cells[5].Text;
                    string Price = g1.Cells[6].Text;
                    string Discount = g1.Cells[7].Text;
                    string CGST = g1.Cells[8].Text;
                    string SGST = g1.Cells[9].Text;
                    string IGST = g1.Cells[10].Text;
                    string TotalAmount = g1.Cells[11].Text;
                    string query = "INSERT INTO [DB_ProcetechTesting].[DB_ProcetechERP].[OrderAcceptDtls] ([OAno],[Description],[HSN],[Qty],[UOM],[Price],[Discount],[TotalAmount],[CGST],[SGST],[IGST],[SubOANumber])" +
                        "VALUES('" + txtOAno.Text + "','" + Description + "','" + HSN + "', '" + Qty + "','" + UOM + "','" + Price + "','" + Discount + "'," +
                        "'" + TotalAmount + "','" + CGST + "','" + SGST + "','" + IGST + "','" + SubOA + "')";
                    cmdPrDtl.CommandText = query;
                    cmdPrDtl.Connection = con;
                    cmdPrDtl.ExecuteNonQuery();
                }
                // tran.Commit();
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

            if (Request.QueryString["cmded"] != null)
            {
                SqlCommand cmddata = new SqlCommand("[DB_ProcetechERP].[SP_UpdateOAFileData]", con);
                cmddata.CommandType = CommandType.StoredProcedure;
                cmddata.Parameters.AddWithValue("@OAid", ViewState["OAID"].ToString());
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
                    cmddata.Parameters.AddWithValue("@filepath1", HFfile1.Value);
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
                    cmddata.Parameters.AddWithValue("@filepath2", HFfile2.Value);
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
                    cmddata.Parameters.AddWithValue("@filepath3", HFfile3.Value);
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
                    cmddata.Parameters.AddWithValue("@filepath4", HFfile4.Value);
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
                    cmddata.Parameters.AddWithValue("@filepath5", HFfile5.Value);
                }
                cmddata.Parameters.AddWithValue("@Createdby", Session["name"].ToString());
                try
                {
                    con.Open();
                    Thread.Sleep(6000);
                    cmddata.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }
            }
            else
            {
                SqlCommand cmddata = new SqlCommand("[DB_ProcetechERP].[SP_OAFileData]", con);
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

                cmddata.Parameters.AddWithValue("@Createdby", Session["name"].ToString());
                try
                {
                    con.Open();
                    Thread.Sleep(6000);
                    cmddata.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.InnerException.Message.ToString();
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }
            }

            if (ddloatocustomer.SelectedItem.Text == "Yes")
            {
                //Sendemail();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Order Details has been saved...!!!');window.location='ManualOrderAcceptance.aspx';", true);
        }
        else
        {

        }
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
        "<br> <b>Customer Name: </b> " + txtcustomername.Text + "  <br><b> Quotation No: </b> " + Request.Form[txtquotationno.UniqueID].ToString() + "<br><b> Quotation Date : </b> " + txtquotationdate.Text + "<br><b> PO No : </b> " + txtpono.Text + "<br><b> Billing Details : </b> " + txtbillingdetails.Text + " <br><b> Total Amount : " + txtTotalamt.Text + "/- Rs. </b> <br><b> Download Order Acceptance : <a href='http://www.weblinkservices.in/Reports/OARptPDF.aspx?ID=" + quotid + "'>Download Order Acceptance Invoice</a></b> </body></html> ";
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
        Response.Redirect("ManualOrderAcceptance.aspx");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void ddlPerticuler_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlPerticuler.SelectedValue == "MS fabricated Enclosure.")
        {
            
            txtdescription.Text = ddlPerticuler.SelectedValue;
            txtHSN.Text = "85381010";
        }
        else if (ddlPerticuler.SelectedValue == "Loose Parts for the Enclosure.")
        {
            txtdescription.Text = ddlPerticuler.SelectedValue;
            txtHSN.Text = "85381010";
        }
        else if (ddlPerticuler.SelectedValue == "0")
        {
            txtdescription.Text = "";
            txtHSN.Text = "0";
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


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
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

    protected void txtcustomername_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("select billingaddress,shippingaddress,gstno,paymentterm1 from Company where cname ='" + txtcustomername.Text + "' and [status]=0", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtaddress.Text = dt.Rows[0]["billingaddress"].ToString();
            txtShipingAddress.Text = dt.Rows[0]["shippingaddress"].ToString();
            txtGSTNO.Text = dt.Rows[0]["gstno"].ToString();
            txttermsofpayment.Text = dt.Rows[0]["paymentterm1"].ToString();
        }
        else
        {
        }
    }

    private decimal TotalPro = (decimal)0.0;
    protected void dgvProductDtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = e.Row.DataItem as DataRowView;
            TotalPro += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
            lblsumaofmaterialAmt.Text = string.Format("{0:N0}", TotalPro);
        }


    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cmded"] != null)
        {

        }
        else
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["Products"] != null)
            {

                DataTable dt = (DataTable)ViewState["Products"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count)
                    {
                        //Remove the Selected Row data and reset row number
                        dt.Rows.Remove(dt.Rows[rowID]);
                        ResetRowID(dt);
                    }
                }

                //Store the current data in ViewState for future reference
                ViewState["Products"] = dt;

                //Re bind the GridView for the updated data
                dgvProductDtl.DataSource = dt;
                dgvProductDtl.DataBind();
            }
        }
    }
    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row[0] = rowNumber;
                rowNumber++;
            }
        }
    }

    protected void dgvOADetailsLoad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Request.QueryString["cmded"] != null)
        {
            string id = e.CommandArgument.ToString();

            if (e.CommandName == "RowDelete")
            {
                SqlCommand cmd = new SqlCommand("delete from OrderAcceptDtls where id='" + id + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Products Deleted Successfully..!!');", true);
                GetOADetails(txtOAno.Text);
            }
        }
    }

    protected void dgvOADetailsLoad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = e.Row.DataItem as DataRowView;
            TotalPro += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
            lblsumaofmaterialAmtLoad.Text = string.Format("{0:N0}", TotalPro);
        }
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        dgvOADetailsLoad.EditIndex = -1;
        this.GetOADetails(txtOAno.Text);
    }

    protected void OnUpdate(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        Label id = (Label)row.FindControl("lblid");
        TextBox txtPerticular = (TextBox)row.FindControl("txtPerticular");
        Label HSN = (Label)row.FindControl("lblHSN");
        TextBox Qty = (TextBox)row.FindControl("txtQty1");
        TextBox Price = (TextBox)row.FindControl("txtPrice1");
        TextBox Discount = (TextBox)row.FindControl("txtDiscount1");
        Label CGSTPer = (Label)row.FindControl("lblCGST");
        Label SGSTPer = (Label)row.FindControl("lblSGST");
        Label IGSTPer = (Label)row.FindControl("lblIGST");
        TextBox TotalAmount = (TextBox)row.FindControl("txtTotalAmount");
        TextBox UOM = (TextBox)row.FindControl("txtuom");
        

        SqlCommand cmdupdate = new SqlCommand("update OrderAcceptDtls set Description='" + txtPerticular.Text + "',HSN='" + HSN.Text + "', Qty='" + Qty.Text + "', UOM = '" + UOM.Text + "', "  + 
            "Price='" + Price.Text + "', Discount='" + Discount.Text + "', TotalAmount='" + TotalAmount.Text + "', CGST='" + CGSTPer.Text + "', SGST='" + SGSTPer.Text + "', IGST='" + IGSTPer.Text + "'  where id='" + id.Text + "'", con);
        con.Open();
        cmdupdate.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", " alert('Product details Updated successfully !!!');window.location='ManualOrderAcceptance.aspx?cmded=" + Request.QueryString["cmded"].ToString() + "';", true);

    }

    protected void dgvOADetailsLoad_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dgvOADetailsLoad.EditIndex = e.NewEditIndex;
        this.GetOADetails(txtOAno.Text);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    private void GRID_GST_Calculation(GridViewRow row)
    {

        string Qty = ((TextBox)row.FindControl("txtQty1")).Text;
        string Price = ((TextBox)row.FindControl("txtPrice1")).Text;
        string discountText = ((TextBox)row.FindControl("txtDiscount1")).Text;

        // Check if the text is null or empty, and set the discount to 0 if true
        string Discount = string.IsNullOrEmpty(discountText) ? "0" : discountText;

        string CGSTPer = ((Label)row.FindControl("lblCGST")).Text;
        string SGSTPer = ((Label)row.FindControl("lblSGST")).Text;
        string IGSTPer = ((Label)row.FindControl("lblIGST")).Text;
        TextBox TotalAmount = (TextBox)row.FindControl("txtTotalAmount");

        var total = Convert.ToDecimal(Qty) * Convert.ToDecimal(Price);



        decimal disc; decimal Discamt;
        if (string.IsNullOrEmpty(Discount))
        {
            disc = 0;
            TotalAmount.Text = total.ToString();
        }
        else
        {
            decimal val1 = Convert.ToDecimal(total);
            decimal val2 = Convert.ToDecimal(Discount);

            disc = (val1 * val2 / 100);
            Discamt = val1 - disc;
            TotalAmount.Text = Discamt.ToString();
            alltotal.Value = Discamt.ToString();
        }
        var DiscAmt = alltotal.Value;

        decimal Vcgst;
        if (string.IsNullOrEmpty(CGSTPer))
        {
            Vcgst = 0;
        }
        else
        {
            decimal val1 = Convert.ToDecimal(DiscAmt);
            decimal val2 = Convert.ToDecimal(CGSTPer);

            Vcgst = (val1 * val2 / 100);
        }
        var CGSTAmt = Vcgst.ToString();

        decimal Vsgst;
        if (string.IsNullOrEmpty(SGSTPer))
        {
            Vsgst = 0;
        }
        else
        {
            decimal val1 = Convert.ToDecimal(DiscAmt);
            decimal val2 = Convert.ToDecimal(SGSTPer);

            Vsgst = (val1 * val2 / 100);
        }
        var SGSTAmt = Vsgst.ToString();

        decimal Vigst;
        if (string.IsNullOrEmpty(IGSTPer))
        {
            Vigst = 0;
        }
        else
        {
            decimal val1 = Convert.ToDecimal(DiscAmt);
            decimal val2 = Convert.ToDecimal(IGSTPer);

            Vigst = (val1 * val2 / 100);
        }
        var IGSTAmt = Vigst.ToString();

        var GSTTotal = Vcgst + Vsgst + Vigst;

       // var taxamt = Convert.ToDecimal(DiscAmt) + Convert.ToDecimal(GSTTotal);
        var taxamt = Convert.ToDecimal(DiscAmt);

        TotalAmount.Text = taxamt.ToString();
    }



    protected void dgvProductDtl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            double cgstamt; double sgstamt; double igstamt; double GrossTotal; Double discount=0;
            GridViewRow row = (GridViewRow)dgvProductDtl.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("lblsno");
            TextBox textDescription = (TextBox)row.Cells[2].Controls[0];
            TextBox textQty = (TextBox)row.Cells[4].Controls[0];
            TextBox txtunit = (TextBox)row.Cells[5].Controls[0];
            TextBox textPrice = (TextBox)row.Cells[6].Controls[0];
            TextBox textDiscount = (TextBox)row.Cells[7].Controls[0];
            string textCGST = row.Cells[8].Text;
            string textSGST = row.Cells[9].Text;
            string textIGST = row.Cells[10].Text;
            string textTotal = row.Cells[11].Text;

            var tot = Convert.ToDouble(textQty.Text) * Convert.ToDouble(textPrice.Text);

            if (textIGST == "0")
            {
                cgstamt = tot * Convert.ToDouble(textCGST) / 100;
                sgstamt = tot * Convert.ToDouble(textSGST) / 100;
                igstamt = 0;
               // GrossTotal = tot + cgstamt + sgstamt;
                GrossTotal = tot;
            }
            else
            {
                cgstamt = 0;
                sgstamt = 0;
                igstamt = tot * Convert.ToDouble(textIGST) / 100;
                // GrossTotal = tot + igstamt;
                GrossTotal = tot;
            }

            if (Convert.ToInt32(textDiscount.Text) > 0)
            {
                var doscountamt = tot * Convert.ToDouble(textDiscount.Text) / 100;

                //GrossTotal = GrossTotal - doscountamt;
                GrossTotal = tot- doscountamt;
            }


            dgvProductDtl.EditIndex = -1;
            DataTable dts = (DataTable)ViewState["Products"];
            int idd = Convert.ToInt32(lblID.Text);
            DataRow[] rows = dts.Select("id =" + idd);
            if (rows.Length > 0)
            {
                foreach (DataRow rowd in rows)
                {
                    rowd["Description"] = textDescription.Text;
                    rowd["Qty"] = textQty.Text;
                    rowd["UOM"] = txtunit.Text;
                    rowd["Price"] = textPrice.Text;
                    rowd["Discount"] = textDiscount.Text;
                    rowd["TotalAmount"] = GrossTotal.ToString();
                }
            }
            dgvProductDtl.DataSource = (DataTable)ViewState["Products"];
            dgvProductDtl.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void dgvProductDtl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dgvProductDtl.EditIndex = e.NewEditIndex;
        dgvProductDtl.DataSource = (DataTable)ViewState["Products"];
        dgvProductDtl.DataBind();
    }

    protected void dgvProductDtl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        dgvProductDtl.EditIndex = -1;
        dgvProductDtl.DataSource = (DataTable)ViewState["Products"];
        dgvProductDtl.DataBind();
    }

    protected void dgvProductDtl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dts = (DataTable)ViewState["Products"];
        dts.Rows.RemoveAt(e.RowIndex);
        dgvProductDtl.DataSource = (DataTable)ViewState["Products"];
        dgvProductDtl.DataBind();
    }


    protected void txtPrice_TextChanged1(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        GRID_GST_Calculation(row);
    }

    protected void txtQty_TextChanged1(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        GRID_GST_Calculation(row);
    }

    protected void txtDiscount_TextChanged1(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        GRID_GST_Calculation(row);
    }
    protected void txtGSTNO_TextChanged(object sender, EventArgs e)
    {
        string str = txtGSTNO.Text.Trim();
        string gststateCode = str.Substring(0, 2);

        if (gststateCode == "27")
        {
            ddltaxation.SelectedValue = "inmah";
        }
        else
        {
            ddltaxation.SelectedValue = "outmah";
        }

    }


    protected void ddltaxation_TextChanged(object sender, EventArgs e)
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

                        //CGSTPertcs.Text = Dt.Rows[0]["CGST"].ToString();
                        //SGSTPertcs.Text = Dt.Rows[0]["SGST"].ToString();
                        //IGSTPertcs.Text = Dt.Rows[0]["IGST"].ToString();



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


    public void GetTaxType()
    {

        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_TaxMaster]", connection))
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

                //using (SqlCommand cmd = new SqlCommand("[SP_TaxMaster]", connection))
                using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_TaxMaster]", connection))
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
}