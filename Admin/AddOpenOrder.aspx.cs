using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddOpenOrder : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    static string ccode;
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
                GenerateCode();
            }
        }            
    }
    protected void GenerateCode()
    {
        txttodaysdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        // txtOAno.Text = " EE/OA/2021-22/001";
        //     string finyear = "";
        //     int CurrentMonth = Convert.ToInt32(DateTime.Now.ToString("MM"));

        //     if (CurrentMonth < 4)
        //     {
        //         string year = DateTime.Now.ToString("yyyy");
        //         double PrevYear = Convert.ToDouble(year) - 1;

        //         string abc = year.ToString().Substring(2);
        //         finyear = PrevYear + "-" + abc;
        //     }
        //     else
        //     {
        //         string year = DateTime.Now.ToString("yyyy");
        //         double NextYear = Convert.ToDouble(year) + 1;
        //         string abc = NextYear.ToString().Substring(2);
        //         finyear = year + "-" + abc;
        //     }

        //     SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [OrderAccept]", con);
        //     DataTable dt = new DataTable();
        //     ad.Fill(dt);
        //     if (dt.Rows.Count > 0)
        //     {
        //int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 + 1 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1;
        //         txtOAno.Text = " EE/OA/" + finyear + "/" + maxid.ToString();
        //         //txtOAno.Text = " EE/OA/" + finyear + "/" + (Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1).ToString();
        //     }
        //     else
        //     {
        //         //ComCode = string.Empty;
        //     }

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
        //string strSelect = @"select ISNULL(MAX(InvoiceNo), '') AS maxno from tblTaxInvoiceHdr where InvoiceNo like '%" + fY + "%'";
        string strSelect = @"SELECT TOP 1 OAno FROM OrderAcceptDtls where OAno like '%" + fY + "%' ORDER BY ID DESC";
        //string strSelect = @"SELECT TOP 1 InvoiceNo FROM tblTaxInvoiceHdr where InvoiceNo like '%2022-23%' ORDER BY ID DESC";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSelect;
        con.Open();
        string result = cmd.ExecuteScalar().ToString();
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

            txtOAno.Text = "PA/OA/" + strInvoiceNumber;
            OANO = txtOAno.Text;
        }
        else
        {
            OANO = "PA/OA/" + previousyear.ToString() + "-" + FinYear + "/" + "01";
        }
        //return OANO;
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        PopupAddDetail.Visible = true;
        this.modelprofile.Show();
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "sum()", true);
    }

    protected void txtcustomername_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter adp2 = new SqlDataAdapter("select top 1 ccode, oname1,oname2,oname3,oname4,oname5,shippingaddress from Company where cname='" + txtcustomername.Text + "' and status=0 and isdeleted=0", con);
        DataTable dt2 = new DataTable();
        adp2.Fill(dt2);

        Hashtable ht = new Hashtable();
        if (dt2.Rows.Count > 0)
        {
            ccode = dt2.Rows[0]["ccode"].ToString();
            txtaddress.Text = dt2.Rows[0]["shippingaddress"].ToString();

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
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "sum()", true);
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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        DataTable dtConstructionType = new DataTable();
        dtConstructionType.Columns.AddRange(new DataColumn[6] { new DataColumn("category1", typeof(string)),new DataColumn("category2", typeof(string)),
                    new DataColumn("category3", typeof(string)),new DataColumn("category4",typeof(string)),new DataColumn("category5", typeof(string)),new DataColumn("category6", typeof(string)) });

        if (ChkSpecify1.Checked == false && ChkSpecify2.Checked == false
             && ChkSpecify3.Checked == false && ChkSpecify4.Checked == false
             && ChkSpecify5.Checked == false && ChkSpecify6.Checked == false
             && ChkSpecify7.Checked == false && ChkSpecify8.Checked == false
             && ChkSpecify9.Checked == false && ChkSpecify10.Checked == false)
        {
            validation = 1;
        }

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
            dtConstructionType.Rows.Add(txtspecify1cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify2cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify3cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify4cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify5cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify6cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify7cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify8cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify9cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify10cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify11cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify12cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify13cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify14cat1.Text, first, second, "", "", sentence);
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
            dtConstructionType.Rows.Add(txtspecify15cat1.Text, first, second, "", "", sentence);
        }
        StringBuilder sbdescription = new StringBuilder();
        int sno = 1;
        for (int i = 0; i < dtConstructionType.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(dtConstructionType.Rows[i]["category6"].ToString()) || dtConstructionType.Rows[i]["category6"].ToString() != "")
            {
                sbdescription.Append((sno) + ". " + dtConstructionType.Rows[i]["category6"].ToString() + "<br>");
                sno++;
            }
        }

        if (validation == 0 && dtConstructionType.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Request.Form[txtTotalamt.UniqueID].ToString()))
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "sum()", true);

                SqlCommand cmd = new SqlCommand("SP_OrderAccept", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customername", txtcustomername.Text);
                cmd.Parameters.AddWithValue("@ccode", ccode);

                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                cmd.Parameters.AddWithValue("@quotationno", txtquotationno.Text);

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
                cmd.Parameters.AddWithValue("@description", sbdescription.ToString());
                cmd.Parameters.AddWithValue("@qty", txtQty.Text);
                cmd.Parameters.AddWithValue("@drgref", txtdrgref.Text);
                cmd.Parameters.AddWithValue("@price", txtprice.Text);
                cmd.Parameters.AddWithValue("@totamt", Request.Form[txtTotalamt.UniqueID].ToString());
                cmd.Parameters.AddWithValue("@action", "AddOpenOA");

                GenerateCode();
                cmd.Parameters.AddWithValue("@OAno", Request.Form[txtOAno.UniqueID].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Open Order Details has been saved !!!');window.location='OrderAcceptanceList.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill quantity and price !!!');", true);
            }
        }
        if (validation == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Add the details  !!!');", true);
        }

    }
    static int validation;
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddOpenOrder.aspx");
    }

    protected void btnSubmitmaterial_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "sum()", true);
    }
}