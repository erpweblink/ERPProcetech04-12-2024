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
using System.Net.Mime;
using iTextSharp.tool.xml;

public partial class Admin_ProformaInvoice : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dtparticular = new DataTable();
    DataTable dtparticularorder = new DataTable();
    string id;
    string invNo;

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
                GetTaxType();

                txtinvoicedate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                manuallytable.Visible = false;
                if (Request.QueryString["Id"] != null)
                {
                    id = Decrypt(Request.QueryString["Id"].ToString());

                    btnSubmit.Text = "Update";
                    hidden1.Value = id;

                    ViewState["RowNo"] = 0;
                    dtparticular.Columns.AddRange(new DataColumn[16] { new DataColumn("Id"),
                 new DataColumn("Particular"),new DataColumn("Description"), new DataColumn("HSN")
                , new DataColumn("Qty"),new DataColumn("UOM"), new DataColumn("Rate"),
                    new DataColumn("TaxableAmt"),new DataColumn("CGSTPer"),new DataColumn("CGSTAmt"),new DataColumn("SGSTPer"),new DataColumn("SGSTAmt")
                    ,new DataColumn("IGSTPer"),new DataColumn("IGSTAmt"),new DataColumn("Discount"),new DataColumn("GrandTotal") });

                    ViewState["ParticularDetails"] = dtparticular;
                    LoadData(id);
                }

                else
                {

                    ViewState["RowNo"] = 0;
                    dtparticular.Columns.AddRange(new DataColumn[16] { new DataColumn("id"),
                 new DataColumn("Particular"),new DataColumn("Description"), new DataColumn("HSN")
                , new DataColumn("Qty"),new DataColumn("UOM"), new DataColumn("Rate"),
                    new DataColumn("TaxableAmt"),new DataColumn("CGSTPer"),new DataColumn("CGSTAmt"),new DataColumn("SGSTPer"),new DataColumn("SGSTAmt")
                    ,new DataColumn("IGSTPer"),new DataColumn("IGSTAmt"),new DataColumn("Discount"),new DataColumn("GrandTotal")
            });
                    ViewState["ParticularDetails"] = dtparticular;
                }

            }
        }

    }

    protected void LoadData(string id)
    {
        DataTable dtload = new DataTable();
        SqlDataAdapter sad = new SqlDataAdapter("select * from tblProformaInvoiceHdr where Id='" + id + "'", con);
        sad.Fill(dtload);
        if (dtload.Rows.Count > 0)
        {
            hdnHdrId.Value = dtload.Rows[0]["id"].ToString();
            txtbillingcustomer.Text = dtload.Rows[0]["BillingCustomer"].ToString();
            txtBillingAddress.Text = dtload.Rows[0]["BillingAddress"].ToString();
            txtinvoicedate.Text = dtload.Rows[0]["Invoicedate"].ToString();
            txtinvoiceagainst.Text = dtload.Rows[0]["InvoiceAgainst"].ToString();
            txtagainstNumber.SelectedItem.Text = dtload.Rows[0]["AgainstNumber"].ToString();
            txtremark.Text = dtload.Rows[0]["Remark"].ToString();
            sumofAmount.Text = dtload.Rows[0]["SumOfProductAmt"].ToString();
            txtDescription.Text = dtload.Rows[0]["ChargesDescription"].ToString();
            txthsntcs.Text = dtload.Rows[0]["HSNTcs"].ToString();
            txtrateTcs.Text = dtload.Rows[0]["RateTcs"].ToString();
            txtBasic.Text = dtload.Rows[0]["Basic"].ToString();
            CGSTPertcs.Text = dtload.Rows[0]["CGST"].ToString();
            SGSTPertcs.Text = dtload.Rows[0]["SGST"].ToString();
            IGSTPertcs.Text = dtload.Rows[0]["IGST"].ToString();
            txtCost.Text = dtload.Rows[0]["Cost"].ToString();
            txtTCSPer.Text = dtload.Rows[0]["TCSPercent"].ToString();
            txtTCSAmt.Text = dtload.Rows[0]["TCSAmt"].ToString();
            txtGrandTot.Text = dtload.Rows[0]["GrandTotalFinal"].ToString();
            txtContactNo.Text = dtload.Rows[0]["ContactNo"].ToString();
            txtemail.Text = dtload.Rows[0]["Email"].ToString();
            txtinvoicedate.Text = dtload.Rows[0]["Invoicedate"].ToString();
            ddlCurrency.SelectedItem.Text = dtload.Rows[0]["CurrencyType"].ToString();
            txtSource.Text = dtload.Rows[0]["SourceName"].ToString();
            txtAdvance.Text = dtload.Rows[0]["AdvanceAmt"].ToString() == null || dtload.Rows[0]["AdvanceAmt"].ToString() == "" ? "0" : dtload.Rows[0]["AdvanceAmt"].ToString();
            
            ddltaxation.Text = dtload.Rows[0]["TaxType"].ToString();
            //string email = dtload.Rows[0]["IsEmail"].ToString();
            //IsSedndMail.Checked = email == "True" ? true : false;

            if (txtinvoiceagainst.Text == "Direct")
            {
                txtagainstNumber.Enabled = false;
            }
            ///BindDiscription

            DataTable dtt = new DataTable();
            SqlDataAdapter sad3 = new SqlDataAdapter("select Id,Particular,Description,HSN,Qty,UOM,Rate,TaxableAmt,CGSTPer,CGSTAmt,SGSTPer,SGSTAmt,IGSTPer,IGSTAmt,Discount,GrandTotal from tblProformaInvoiceDtls where HeaderID='" + id + "'", con);
            sad3.Fill(dtt);
            if (dtt.Rows.Count > 0)
            {
                ViewState["RowNo"] = 0;
                if (dtt.Rows.Count > 0)
                {
                    if (dtparticular.Columns.Count < 1)
                    {
                        Show_Grid();
                    }

                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        dtparticular.Rows.Add(dtt.Rows[i]["Id"].ToString(), dtt.Rows[i]["Particular"].ToString(), dtt.Rows[i]["Description"].ToString(), dtt.Rows[i]["HSN"].ToString(), dtt.Rows[i]["Qty"].ToString(), dtt.Rows[i]["UOM"].ToString(), dtt.Rows[i]["Rate"].ToString(), dtt.Rows[i]["TaxableAmt"].ToString(), dtt.Rows[i]["CGSTPer"].ToString(), dtt.Rows[i]["CGSTAmt"].ToString(), dtt.Rows[i]["SGSTPer"].ToString(), dtt.Rows[i]["SGSTAmt"].ToString(), dtt.Rows[i]["IGSTPer"].ToString(), dtt.Rows[i]["IGSTAmt"].ToString(), dtt.Rows[i]["Discount"].ToString(), dtt.Rows[i]["GrandTotal"].ToString());
                        // ViewState["RowNo"] = ViewState["RowNo"] + 1;
                    }
                }
                gvinvoiceParticular.DataSource = dtparticular;
                gvinvoiceParticular.DataBind();
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
    {
        return AutoFillCustomerName(prefixText);
    }

    public static List<string> AutoFillCustomerName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [cname] from Company where " + "cname like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> cname = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        cname.Add(sdr["cname"].ToString());
                    }
                }
                con.Close();
                return cname;
            }
        }
    }

    protected void txtbillingcustomer_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("select * from Company where cname='" + txtbillingcustomer.Text + "'", con);
            sad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtBillingAddress.Text = dt.Rows[0]["billingaddress"].ToString();
                txtContactNo.Text = dt.Rows[0]["mobile1"].ToString();
                txtemail.Text = dt.Rows[0]["email1"].ToString();

                txtagainstNumber.Items.Clear();

                divdtls1.Visible = true;
                DataTable dtorder = new DataTable();
                SqlDataAdapter sadorder = new SqlDataAdapter("select * from OrderAccept where status is null and customername='" + txtbillingcustomer.Text + "'", con);
                sadorder.Fill(dtorder);
                txtagainstNumber.DataValueField = "OAno";
                txtagainstNumber.DataTextField = "pono";
                txtagainstNumber.DataSource = dtorder;
                txtagainstNumber.DataBind();

                txtagainstNumber.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                manuallytable.Visible = false;
                txtagainstNumber.Enabled = true;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemList(string prefixText, int count)
    {
        return AutoFilItem(prefixText);
    }

    public static List<string> AutoFilItem(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [ItemName] from tblItemMaster where " + "ItemName like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> Items = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Items.Add(sdr["ItemName"].ToString());
                    }
                }
                con.Close();
                return Items;
            }
        }
    }

    protected void txtParticulars_TextChanged(object sender, EventArgs e)
    {
        txtHSN.Text = "85381010";
        txtdiscription.Text = txtParticulars.SelectedItem.Text;
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        //CGSTPer.Text = "0";
        //SGSTPer.Text = "0";
        //txtdiscount.Text = "0";
        //IGSTPer.Text = "0";
        GST_Calculation();


    }

    private void GST_Calculation()
    {
        var rate = txtRate.Text == "" ? "0" : txtRate.Text;

        var totalamt = Convert.ToDouble(txtQty.Text.Trim()) * Convert.ToDouble(rate);

        Double AmtWithDiscount;
        if (txtdiscount.Text != "")
        {
            var disc = totalamt * (Convert.ToDouble(txtdiscount.Text.Trim())) / 100;

            totalamt = totalamt - disc;
        }
        else
        {
            totalamt = totalamt + 0;
        }


        var CGSTamt = totalamt * (Convert.ToDouble(CGSTPer.Text.Trim())) / 100;
        var SGSTamt = totalamt * (Convert.ToDouble(SGSTPer.Text.Trim())) / 100;
        var IGSTamt = totalamt * (Convert.ToDouble(IGSTPer.Text.Trim())) / 100;

        double GSTtotal = 0;
        if (IGSTPer.Text == "0")
        {
            CGSTAmt.Text = CGSTamt.ToString();
            SGSTAmt.Text = SGSTamt.ToString();
            GSTtotal = SGSTamt + CGSTamt;
        }
        else
        {
            IGSTAmt.Text = IGSTamt.ToString() == "" ? "0" : IGSTamt.ToString();
            GSTtotal = IGSTamt;
        }

        txtAmountt.Text = totalamt.ToString();
        var NetAmt = totalamt + GSTtotal;
        //var NetAmt = totalamt;



        txtGrandtotal.Text = NetAmt.ToString("##.00");
    }

    protected void SGSTPer_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();

        if (SGSTPer.Text == "" || SGSTPer.Text == "0")
        {
            IGSTPer.Enabled = true;
            IGSTPer.Text = "0";
        }
        else
        {
            IGSTPer.Enabled = false;
            IGSTPer.Text = "0";
        }
    }

    private void GridOrderList(GridViewRow row)
    {
        TextBox txt_Qty = (TextBox)row.FindControl("lblQty");
        TextBox txt_price = (TextBox)row.FindControl("lblRate");
        TextBox txt_CGST = (TextBox)row.FindControl("lblCGSTPer");
        TextBox txt_SGST = (TextBox)row.FindControl("lblSGSTPer");
        TextBox txt_IGST = (TextBox)row.FindControl("lblIGSTPer");
        Label txt_amount = (Label)row.FindControl("txtGrandTotal");
        Label taxableamt = (Label)row.FindControl("lblAmount");
        TextBox txt_discount = (TextBox)row.FindControl("txtdiscount");

        var totalamt = Convert.ToDouble(txt_Qty.Text.Trim()) * Convert.ToDouble(txt_price.Text.Trim());
        taxableamt.Text = totalamt.ToString();
        var CGSTamt = totalamt * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
        var SGSTamt = totalamt * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
        var IGSTamt = totalamt * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;

        var GSTtotal = SGSTamt + CGSTamt;

        var NetAmt = totalamt + GSTtotal;
        //var NetAmt = totalamt;

        Double AmtWithDiscount;
        if (txt_discount.Text != "" || txt_discount.Text != null)
        {
            var disc = NetAmt * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;

            AmtWithDiscount = NetAmt - disc;
        }
        else
        {
            AmtWithDiscount = 0;
        }

        txt_amount.Text = AmtWithDiscount.ToString("##.00");
    }

    private void GRID_GST_Calculation(GridViewRow row)
    {
        TextBox txt_Qty = (TextBox)row.FindControl("txtQty");
        TextBox txt_price = (TextBox)row.FindControl("txtrate");
        TextBox txt_CGST = (TextBox)row.FindControl("txtCGSTPer");
        TextBox txt_SGST = (TextBox)row.FindControl("txtSGSTPer");
        TextBox txt_IGST = (TextBox)row.FindControl("txtIGSTPer");
        Label txt_amount = (Label)row.FindControl("txtGrandTotal");
        Label taxableamt = (Label)row.FindControl("lblAmount");
        TextBox txt_discount = (TextBox)row.FindControl("txtdiscountedit");

        var totalamt = Convert.ToDouble(txt_Qty.Text.Trim()) * Convert.ToDouble(txt_price.Text.Trim());
        taxableamt.Text = totalamt.ToString();
        var CGSTamt = totalamt * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
        var SGSTamt = totalamt * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
        var IGSTamt = totalamt * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;
        var GSTtotal = SGSTamt + CGSTamt;
        var NetAmt = totalamt + GSTtotal;
        //var NetAmt = totalamt;

        Double AmtWithDiscount;
        if (txt_discount.Text != "" || txt_discount.Text != null)
        {
            var disc = NetAmt * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;

            AmtWithDiscount = NetAmt - disc;
        }
        else
        {
            AmtWithDiscount = 0;
        }

        txt_amount.Text = AmtWithDiscount.ToString("##.00");


    }

    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        if (txtQty.Text == "" || txtParticulars.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill All Required fields !!!');", true);
        }
        else
        {
            Show_Grid();
        }
    }

    private void Show_Grid()
    {
        ViewState["RowNo"] = 0;
        ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;
        DataTable dt = (DataTable)ViewState["ParticularDetails"];

        dt.Rows.Add(ViewState["RowNo"], txtParticulars.SelectedItem.Text, txtdiscription.Text, txtHSN.Text, txtQty.Text, txtuom.Text, txtRate.Text, txtAmountt.Text, CGSTPer.Text, CGSTAmt.Text, SGSTPer.Text, SGSTAmt.Text, IGSTPer.Text, IGSTAmt.Text, txtdiscount.Text, txtGrandtotal.Text);
        ViewState["ParticularDetails"] = dt;

        gvinvoiceParticular.DataSource = (DataTable)ViewState["ParticularDetails"];
        gvinvoiceParticular.DataBind();

        //txtParticulars.SelectedItem.Text = string.Empty;
        txtdiscription.Text = string.Empty;
        txtQty.Text = string.Empty;
        //txtHSN.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtAmountt.Text = string.Empty;
        //CGSTPer.Text = string.Empty;
        //CGSTAmt.Text = string.Empty;
        txtdiscount.Text = "0";
        //SGSTPer.Text = "9";
        SGSTAmt.Text = "0";
       // CGSTPer.Text = "9";
        CGSTAmt.Text = "0";
       // IGSTPer.Text = "0";
        IGSTAmt.Text = "0";
        txtGrandtotal.Text = string.Empty;
        //txtuom.Text = string.Empty;

    }

    protected void gvinvoiceParticular_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvinvoiceParticular.EditIndex = e.NewEditIndex;
        gvinvoiceParticular.DataSource = (DataTable)ViewState["ParticularDetails"];
        gvinvoiceParticular.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void lnkbtnUpdate_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        string Particulars = ((Label)row.FindControl("lblParticulars")).Text;
        string Discription = ((TextBox)row.FindControl("txtDescription")).Text;
        string HSN = ((Label)row.FindControl("lblHSN")).Text;
        string UOM = ((Label)row.FindControl("txtUOM")).Text;
        string Qty = ((TextBox)row.FindControl("txtQty")).Text;
        string Rate = ((TextBox)row.FindControl("txtrate")).Text;

        string Amount = ((Label)row.FindControl("lblAmount")).Text;
        string CGSTPer = ((TextBox)row.FindControl("txtCGSTPer")).Text;
        string SGSTPer = ((TextBox)row.FindControl("txtSGSTPer")).Text;
        string IGSTPer = ((TextBox)row.FindControl("txtIGSTPer")).Text;
        string CGSAmt = ((Label)row.FindControl("lblCgstAmt")).Text;
        string SGSAmt = ((Label)row.FindControl("lblSgstAmt")).Text;
        string IGSTAmt = ((Label)row.FindControl("lblIGSTAmt")).Text;
        string Discount = ((TextBox)row.FindControl("txtdiscountedit")).Text;
        string grandtotal = ((Label)row.FindControl("txtGrandTotal")).Text;

        DataTable Dt = ViewState["ParticularDetails"] as DataTable;

        Dt.Rows[row.RowIndex]["Particular"] = Particulars;
        Dt.Rows[row.RowIndex]["Description"] = Discription;
        Dt.Rows[row.RowIndex]["HSN"] = HSN;
        Dt.Rows[row.RowIndex]["Qty"] = Qty;
        Dt.Rows[row.RowIndex]["UOM"] = UOM;
        Dt.Rows[row.RowIndex]["Rate"] = Rate;
        Dt.Rows[row.RowIndex]["TaxableAmt"] = Amount;
        Dt.Rows[row.RowIndex]["CGSTPer"] = CGSTPer;
        Dt.Rows[row.RowIndex]["SGSTPer"] = SGSTPer;
        Dt.Rows[row.RowIndex]["IGSTPer"] = IGSTPer;
        Dt.Rows[row.RowIndex]["Discount"] = Discount;
        Dt.Rows[row.RowIndex]["GrandTotal"] = grandtotal;
        Dt.Rows[row.RowIndex]["CGSTAmt"] = CGSAmt;
        Dt.Rows[row.RowIndex]["SGSTAmt"] = SGSAmt;
        Dt.Rows[row.RowIndex]["IGSTAmt"] = IGSTAmt;

        Dt.AcceptChanges();

        ViewState["ParticularDetails"] = Dt;
        gvinvoiceParticular.EditIndex = -1;

        gvinvoiceParticular.DataSource = (DataTable)ViewState["ParticularDetails"];
        gvinvoiceParticular.DataBind();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        DataTable Dt = ViewState["ParticularDetails"] as DataTable;
        gvinvoiceParticular.EditIndex = -1;

        ViewState["ParticularDetails"] = Dt;
        gvinvoiceParticular.EditIndex = -1;

        gvinvoiceParticular.DataSource = (DataTable)ViewState["ParticularDetails"];
        gvinvoiceParticular.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    Double Totalamt = 0;
    Double GrandTotalamt = 0;
    protected void gvinvoiceParticular_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Totalamt += Convert.ToDouble((e.Row.FindControl("lblAmount") as Label).Text);
            GrandTotalamt += Convert.ToDouble((e.Row.FindControl("txtGrandTotal") as Label).Text);
            hdnGrandtotal.Value = GrandTotalamt.ToString();
            sumofAmount.Text = Totalamt.ToString();

            var Total = Convert.ToDouble(txtCost.Text) + GrandTotalamt + Convert.ToDouble(txtTCSAmt.Text);
            txtGrandTot.Text = Total.ToString("##.00");
        }

    }

    protected void txtCGSTPer_TextChanged1(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        //GRID_GST_Calculation(row);
        GRID_GST_Calculation1(row);
        TextBox txt_CGST = (TextBox)row.FindControl("txtCGSTPer");
        Label lbl_CGSTAmt = (Label)row.FindControl("lblCgstAmt");
        Label Amount = (Label)row.FindControl("lblAmount");
        TextBox txt_IGST = (TextBox)row.FindControl("txtIGSTPer");
        if (txt_CGST.Text == "" || txt_CGST.Text == "0")
        {
            txt_IGST.Enabled = true;
            txt_IGST.Text = "0";
        }
        else
        {
            Double CGStAmt = Convert.ToDouble(Amount.Text) * Convert.ToDouble(txt_CGST.Text) / 100;
            lbl_CGSTAmt.Text = CGStAmt.ToString();
            txt_IGST.Enabled = false;
            txt_IGST.Text = "0";
        }

    }

    protected void CGSTPer_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();

        if (CGSTPer.Text == "" || CGSTPer.Text == "0")
        {

            IGSTPer.Enabled = true;
            IGSTPer.Text = "0";
        }
        else
        {
            Double CgstAmt = Convert.ToDouble(txtAmountt.Text) * Convert.ToDouble(CGSTPer.Text) / 100;
            CGSTAmt.Text = CgstAmt.ToString();
            IGSTPer.Enabled = false;
            IGSTPer.Text = "0";
        }
    }

    protected void SGSTPer_TextChanged1(object sender, EventArgs e)
    {
        GST_Calculation();

        if (SGSTPer.Text == "" || SGSTPer.Text == "0")
        {
            IGSTPer.Enabled = true;
            IGSTPer.Text = "0";
        }
        else
        {
            Double SgstAmt = Convert.ToDouble(txtAmountt.Text) * Convert.ToDouble(SGSTPer.Text) / 100;
            SGSTAmt.Text = SgstAmt.ToString();
            IGSTPer.Enabled = false;
            IGSTPer.Text = "0";
        }
    }

    protected void IGSTPer_TextChanged1(object sender, EventArgs e)
    {
        GST_Calculation();

        if (IGSTPer.Text == "" || IGSTPer.Text == "0")
        {
            // SGSTPer.Enabled = true;
            //CGSTPer.Enabled = true;
            SGSTPer.Text = "0";
            CGSTPer.Text = "0";
        }
        else
        {
            Double IgstAmt = Convert.ToDouble(txtAmountt.Text) * Convert.ToDouble(IGSTPer.Text) / 100;
            IGSTAmt.Text = IgstAmt.ToString();
            //SGSTPer.Enabled = false;
            // CGSTPer.Enabled = false;
            SGSTPer.Text = "0";
            CGSTPer.Text = "0";
        }
    }

    protected void txtdiscount_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();
    }

    protected void txtQty_TextChanged2(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        //GRID_GST_Calculation(row);
        GRID_GST_Calculation1(row);
    }

    protected void txtrate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        //GRID_GST_Calculation(row);
        GRID_GST_Calculation1(row);
    }

    protected void txtSGSTPer_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

        //GRID_GST_Calculation(row);
        GRID_GST_Calculation1(row);
        TextBox txt_SGST = (TextBox)row.FindControl("txtSGSTPer");
        TextBox txt_IGST = (TextBox)row.FindControl("txtIGSTPer");
        Label lbl_SGSTAmt = (Label)row.FindControl("lblSgstAmt");
        Label Amount = (Label)row.FindControl("lblAmount");

        if (txt_SGST.Text == "" || txt_SGST.Text == "0")
        {
            txt_IGST.Enabled = true;
            txt_IGST.Text = "0";
        }
        else
        {
            Double SGSTAmt = Convert.ToDouble(Amount.Text) * Convert.ToDouble(txt_SGST.Text) / 100;
            lbl_SGSTAmt.Text = SGSTAmt.ToString();
            txt_IGST.Enabled = false;
            txt_IGST.Text = "0";
        }
    }

    protected void txtIGSTPer_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        //GRID_GST_Calculation(row);
        GRID_GST_Calculation1(row);
        TextBox txt_CGST = (TextBox)row.FindControl("txtCGSTPer");
        TextBox txt_SGST = (TextBox)row.FindControl("txtSGSTPer");

        TextBox txt_IGST = (TextBox)row.FindControl("txtIGSTPer");
        Label lbl_SGSTAmt = (Label)row.FindControl("lblSgstAmt");
        Label Amount = (Label)row.FindControl("lblAmount");
        Label lblIGSTAmt = (Label)row.FindControl("lblIGSTAmt");

        if (txt_IGST.Text == "" || txt_IGST.Text == "0")
        {
            txt_CGST.Enabled = true;
            txt_SGST.Enabled = true;
            //txt_IGST.Enabled = true;
            txt_SGST.Text = "0";
            txt_CGST.Text = "0";
        }
        else
        {

            Double IGSTAmt = Convert.ToDouble(Amount.Text) * Convert.ToDouble(txt_IGST.Text) / 100;
            lblIGSTAmt.Text = IGSTAmt.ToString();
            txt_CGST.Enabled = false;
            txt_SGST.Enabled = false;
            txt_SGST.Text = "0";
            txt_CGST.Text = "0";
        }

    }

    protected void txtdiscountedit_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        // GRID_GST_Calculation(row);
        GRID_GST_Calculation1(row);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        DataTable dt = ViewState["ParticularDetails"] as DataTable;
        dt.Rows.Remove(dt.Rows[row.RowIndex]);
        ViewState["ParticularDetails"] = dt;
        gvinvoiceParticular.DataSource = (DataTable)ViewState["ParticularDetails"];
        gvinvoiceParticular.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Delete Succesfully !!!');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void txtrateTcs_TextChanged(object sender, EventArgs e)
    {
        string Amt = sumofAmount.Text;
        string Rate = txtrateTcs.Text;
        if (Rate == "0")
        {
            txtBasic.Text = "0";
            txtCost.Text = "0";
            CGSTPertcs.Text = "0";
            SGSTPertcs.Text = "0";
            IGSTPertcs.Text = "0";
        }
        else
        {
            var Basic = Convert.ToDouble(Amt) * Convert.ToDouble(Rate) / 100;
            txtBasic.Text = Basic.ToString("##.00");

            var grandtot = Convert.ToDouble(Basic) + Convert.ToDouble(hdnGrandtotal.Value);
            txtGrandTot.Text = grandtot.ToString("##.00");
        }
    }

    protected void CGSTPertcs_TextChanged(object sender, EventArgs e)
    {
        GstCalculationTcs();
    }

    protected void GstCalculationTcs()
    {
        string Basic = txtBasic.Text;
        string CGST = CGSTPertcs.Text;
        string SGST = SGSTPertcs.Text;
        if (CGST == "0" || SGST == "0")
        {
            if (CGST == "0" && SGST == "0" && IGSTPertcs.Text == "0")
            {
                IGSTPertcs.Enabled = true;
                CGSTPertcs.Enabled = true;
                SGSTPertcs.Enabled = true;
                txtCost.Text = Basic.ToString();
            }
            else
            {
                if (IGSTPertcs.Text == "0")
                {
                    IGSTPertcs.Enabled = false;
                    CGSTPertcs.Enabled = true;
                    SGSTPertcs.Enabled = true;
                    var CGSTAmt = Convert.ToDouble(Basic) * Convert.ToDouble(CGST) / 100;
                    var SGSTAmt = Convert.ToDouble(Basic) * Convert.ToDouble(SGST) / 100;
                    var GSTTaxTotal = Convert.ToDouble(Basic) + CGSTAmt + SGSTAmt;
                    txtCost.Text = GSTTaxTotal.ToString("##.00");

                    var grandtot = Convert.ToDouble(GSTTaxTotal) + Convert.ToDouble(hdnGrandtotal.Value);
                    txtGrandTot.Text = grandtot.ToString("##.00");
                }
                else
                {
                    IGSTPertcs.Enabled = true;
                    CGSTPertcs.Enabled = false;
                    SGSTPertcs.Enabled = false;
                    var IGSTAmt = Convert.ToDouble(Basic) * Convert.ToDouble(IGSTPertcs.Text) / 100;
                    var GSTTaxTotal = Convert.ToDouble(Basic) + IGSTAmt;
                    txtCost.Text = GSTTaxTotal.ToString("##.00");

                    var grandtot = Convert.ToDouble(GSTTaxTotal) + Convert.ToDouble(hdnGrandtotal.Value) + Convert.ToDouble(txtTCSAmt.Text);
                    txtGrandTot.Text = grandtot.ToString("##.00");
                }
            }
        }
        else
        {
            IGSTPertcs.Enabled = false;
            CGSTPertcs.Enabled = true;
            SGSTPertcs.Enabled = true;
            var CGSTAmt = Convert.ToDouble(Basic) * Convert.ToDouble(CGST) / 100;
            var SGSTAmt = Convert.ToDouble(Basic) * Convert.ToDouble(SGST) / 100;

            var GSTTaxTotal = Convert.ToDouble(Basic) + CGSTAmt + SGSTAmt;
            txtCost.Text = GSTTaxTotal.ToString("##.00");

            var grandtot = Convert.ToDouble(GSTTaxTotal) + Convert.ToDouble(hdnGrandtotal.Value);
            txtGrandTot.Text = grandtot.ToString("##.00");
        }
    }

    protected void SGSTPertcs_TextChanged(object sender, EventArgs e)
    {
        GstCalculationTcs();
        btnSubmit.Enabled = true;
    }

    protected void IGSTPertcs_TextChanged(object sender, EventArgs e)
    {
        GstCalculationTcs();
        btnSubmit.Enabled = true;
    }

    protected void txtTCSPer_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveRecord();
    }

    public string GenerateCode()
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
        string fY = previousyear.ToString() + "-" + FinYear;
        string strSelect = @"select ISNULL(MAX(InvoiceNo), '') AS maxno from [DB_ProcetechERP].tblProformaInvoiceHdr where InvoiceNo like '%" + fY + "%'";
        //string strSelect = @"SELECT TOP 1 InvoiceNo FROM  [DB_ProcetechERP].tblProformaInvoiceHdr where InvoiceNo like '%" + fY + "%' ORDER BY ID DESC";
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
            int numbervalue = Convert.ToInt32(result.Substring(result.IndexOf("/") + 1, result.Length - (result.IndexOf("/") + 1)));
            numbervalue = numbervalue + 1;
            strInvoiceNumber = result.Substring(0, result.IndexOf("/") + 1) + "" + numbervalue.ToString("00");
        }
        else
        {
            strInvoiceNumber = previousyear.ToString() + "-" + FinYear + "/" + "01";
        }
        return strInvoiceNumber;
    }

    protected void SaveRecord()
    {
        try
        {
            int id, idupdate;
            string invoiceno = GenerateCode();
            bool flgs = false;
            hiddeninvoiceno.Value = invoiceno;
            if (btnSubmit.Text == "Submit")
            {

                if (txtinvoiceagainst.Text == "Direct")
                {
                    if (gvinvoiceParticular.Rows.Count > 0)
                    {
                        flgs = true;
                    }
                    else
                    {
                        flgs = false;
                    }
                }
                else
                {
                    if (gvorder.Rows.Count > 0)
                    {
                        foreach (GridViewRow g2 in gvorder.Rows)
                        {
                            bool chk = (g2.FindControl("chkRow") as CheckBox).Checked;

                            while (chk == true)
                            {
                                flgs = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        flgs = false;
                    }
                }

                if (flgs == true)
                {
                    if (!string.IsNullOrEmpty(txtbillingcustomer.Text))
                    {
                        SqlCommand cmd = new SqlCommand("SP_ProformaInvoiceHdrs", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InvoiceNo", invoiceno);
                        cmd.Parameters.AddWithValue("@BillingCustomer", txtbillingcustomer.Text);
                        cmd.Parameters.AddWithValue("@BillingAddress", txtBillingAddress.Text);
                        cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                        cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                        cmd.Parameters.AddWithValue("@Invoicedate", txtinvoicedate.Text.Trim());
                        cmd.Parameters.AddWithValue("@InvoiceAgainst", txtinvoiceagainst.Text);
                        cmd.Parameters.AddWithValue("@AgainstNumber", txtagainstNumber.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                        cmd.Parameters.AddWithValue("@SumOfProductAmt", sumofAmount.Text);
                        cmd.Parameters.AddWithValue("@ChargesDescription", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@HSN", txthsntcs.Text);
                        cmd.Parameters.AddWithValue("@Rate", txtrateTcs.Text);
                        cmd.Parameters.AddWithValue("@Basic", txtBasic.Text);
                        cmd.Parameters.AddWithValue("@CGST", CGSTPertcs.Text);
                        cmd.Parameters.AddWithValue("@SGST", SGSTPertcs.Text);
                        cmd.Parameters.AddWithValue("@IGST", IGSTPertcs.Text);
                        cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                        cmd.Parameters.AddWithValue("@TCSPercent", txtTCSPer.Text);
                        cmd.Parameters.AddWithValue("@TCSAmt", txtTCSAmt.Text);
                        cmd.Parameters.AddWithValue("@GrandTotal", txtGrandTot.Text);
                        cmd.Parameters.AddWithValue("@CurrencyType", ddlCurrency.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@SourceName", txtSource.Text);
                        cmd.Parameters.AddWithValue("@IsEmail", IsSedndMail.Checked);
                        cmd.Parameters.Add("@Iddd", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //cmd.Parameters.AddWithValue("@CreatedBy", Session["ProductionName"].ToString());
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        cmd.Parameters.AddWithValue("@AdvanceAmt", txtAdvance.Text);
                        cmd.Parameters.AddWithValue("@TaxType", ddltaxation.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Action", "Insert");
                        //id8 = Convert.ToInt32(cmd.Parameters["@Iddd"].Value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        id = Convert.ToInt32(cmd.Parameters["@Iddd"].Value);
                        string idd = id.ToString();
                        if (txtinvoiceagainst.Text == "Direct")
                        {
                            foreach (GridViewRow g1 in gvinvoiceParticular.Rows)
                            {

                                string particular = (g1.FindControl("lblParticulars") as Label).Text;
                                string Description = (g1.FindControl("txtDescription") as Label).Text;
                                string HSN = (g1.FindControl("lblHSN") as Label).Text;
                                string QTY = (g1.FindControl("lblQty") as Label).Text;
                                string UOM = (g1.FindControl("txtUOM") as Label).Text;
                                string RATE = (g1.FindControl("lblRate") as Label).Text;
                                string AMOUNT = (g1.FindControl("lblAmount") as Label).Text;
                                string CGST = (g1.FindControl("lblCGSTPer") as Label).Text;
                                string SGST = (g1.FindControl("lblSGSTPer") as Label).Text;
                                string IGST = (g1.FindControl("lblIGSTPer") as Label).Text;
                                string DISCOUNT = (g1.FindControl("txtdiscount") as Label).Text;
                                string Grandtotal = (g1.FindControl("txtGrandTotal") as Label).Text;
                                string OAId = (g1.FindControl("lblid") as Label).Text;
                                string CGSTAmt = (g1.FindControl("lblCgstAmt") as Label).Text;
                                string SGSTAmt = (g1.FindControl("lblSgstAmt") as Label).Text;
                                string IGSTAmt = (g1.FindControl("lblIGSTAmt") as Label).Text;

                                SqlCommand cmd1 = new SqlCommand("SP_ProformaInvoiceDtls", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@HeaderID", id);
                                cmd1.Parameters.AddWithValue("@CustomerName", txtbillingcustomer.Text);
                                cmd1.Parameters.AddWithValue("@Particular", particular);
                                cmd1.Parameters.AddWithValue("@HSN", HSN);
                                cmd1.Parameters.AddWithValue("@Qty", QTY);
                                cmd1.Parameters.AddWithValue("@UOM", UOM);
                                cmd1.Parameters.AddWithValue("@Rate", RATE);
                                cmd1.Parameters.AddWithValue("@Discount", DISCOUNT);
                                cmd1.Parameters.AddWithValue("@TaxableAmt", AMOUNT);
                                cmd1.Parameters.AddWithValue("@CGSTPer", CGST);
                                cmd1.Parameters.AddWithValue("@SGSTPer", SGST);
                                cmd1.Parameters.AddWithValue("@IGSTPer", IGST);
                                cmd1.Parameters.AddWithValue("@CGSTAmt", CGSTAmt);
                                cmd1.Parameters.AddWithValue("@SGSTAmt", SGSTAmt);
                                cmd1.Parameters.AddWithValue("@IGSTAmt", IGSTAmt);
                                // cmd1.Parameters.Add("@Idddup", SqlDbType.Int).Direction = ParameterDirection.Output;
                                cmd1.Parameters.AddWithValue("@Description", Description);
                                cmd1.Parameters.AddWithValue("@GrandTotal", Grandtotal);
                                cmd1.Parameters.AddWithValue("@OAId", OAId);
                                //if (btnSubmit.Text == "Submit")
                                //{

                                cmd1.Parameters.AddWithValue("@Action", "Insert");

                                //    cmd1.Parameters.AddWithValue("@Action", "Update");

                                con.Open();
                                cmd1.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        else
                        {
                            foreach (GridViewRow g2 in gvorder.Rows)
                            {
                                string particular = (g2.FindControl("lblParticulars") as TextBox).Text;
                                string Description = (g2.FindControl("txtDescription") as TextBox).Text;
                                string HSN = (g2.FindControl("lblHSN") as TextBox).Text;
                                string QTY = (g2.FindControl("lblQty") as TextBox).Text;
                                string UOM = (g2.FindControl("txtUOM") as TextBox).Text;
                                string RATE = (g2.FindControl("lblRate") as TextBox).Text;
                                string AMOUNT = (g2.FindControl("lblAmount") as Label).Text;
                                string CGST = (g2.FindControl("lblCGSTPer") as TextBox).Text;
                                string SGST = (g2.FindControl("lblSGSTPer") as TextBox).Text;
                                string IGST = (g2.FindControl("lblIGSTPer") as TextBox).Text;
                                string DISCOUNT = (g2.FindControl("txtdiscount") as TextBox).Text;
                                string Grandtotal = (g2.FindControl("txtGrandTotal") as Label).Text;
                                string OAId = (g2.FindControl("lblid") as Label).Text;
                                bool chk = (g2.FindControl("chkRow") as CheckBox).Checked;
                                string CGSTAmt = (g2.FindControl("lblCGSTAmt") as Label).Text;
                                string SGSTAmt = (g2.FindControl("lblSGSTAmt") as Label).Text;
                                string IGSTAmt = (g2.FindControl("lblIGSTAmt") as Label).Text;

                                SqlCommand cmd1 = new SqlCommand("SP_ProformaInvoiceDtls", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@HeaderID", id);
                                cmd1.Parameters.AddWithValue("@CustomerName", txtbillingcustomer.Text);
                                cmd1.Parameters.AddWithValue("@Particular", particular);
                                cmd1.Parameters.AddWithValue("@HSN", HSN);
                                cmd1.Parameters.AddWithValue("@Qty", QTY);
                                cmd1.Parameters.AddWithValue("@UOM", UOM);
                                cmd1.Parameters.AddWithValue("@Rate", RATE);
                                cmd1.Parameters.AddWithValue("@Discount", DISCOUNT);
                                cmd1.Parameters.AddWithValue("@TaxableAmt", AMOUNT);
                                cmd1.Parameters.AddWithValue("@CGSTPer", CGST);
                                cmd1.Parameters.AddWithValue("@SGSTPer", SGST);
                                cmd1.Parameters.AddWithValue("@IGSTPer", IGST);
                                cmd1.Parameters.AddWithValue("@OAId", OAId);
                                cmd1.Parameters.AddWithValue("@CGSTAmt", CGSTAmt);
                                cmd1.Parameters.AddWithValue("@SGSTAmt", SGSTAmt);
                                cmd1.Parameters.AddWithValue("@IGSTAmt", IGSTAmt);
                                //cmd1.Parameters.Add("@Idddup", SqlDbType.Int).Direction = ParameterDirection.Output;
                                cmd1.Parameters.AddWithValue("@Description", Description);
                                cmd1.Parameters.AddWithValue("@GrandTotal", Grandtotal);

                                if (chk == true)
                                {
                                    cmd1.Parameters.AddWithValue("@Action", "Insert");
                                }
                                con.Open();
                                cmd1.ExecuteNonQuery();
                                con.Close();
                            }
                            //DataTable dt546665 = new DataTable();
                            //SqlDataAdapter sadparticular = new SqlDataAdapter("select * from tblProformaInvoiceDtls where HeaderID='" + id + "'", con);
                            //sadparticular.Fill(dt546665);
                            //if (dt546665.Rows.Count > 0)
                            //{

                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('At Least Select One Record.');", true);
                            //}
                        }

                        if (IsSedndMail.Checked == true)
                        {
                            Send_Mail(idd);
                        }

                        //if (Session["isclosed"] != null)
                        //{

                        //}
                        //else
                        //{
                        //    con.Open();
                        //    SqlCommand cmdupOA = new SqlCommand("update OrderAccept set status='close' where pono='" + txtagainstNumber.SelectedItem.Text + "'", con);
                        //    cmdupOA.ExecuteNonQuery();
                        //    con.Close();
                        //}

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Sucessfully');window.location.href='ProformaInvoiceList.aspx';", true);
                        Session["isclosed"] = null;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Party Name.');", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please add Particulars');", true);
                }
            }
            else if (btnSubmit.Text == "Update")
            {

                SqlCommand cmd = new SqlCommand("SP_ProformaInvoiceHdrs", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceNo", invoiceno);
                cmd.Parameters.AddWithValue("@BillingCustomer", txtbillingcustomer.Text);
                cmd.Parameters.AddWithValue("@BillingAddress", txtBillingAddress.Text);
                cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                cmd.Parameters.AddWithValue("@Invoicedate", txtinvoicedate.Text.Trim());
                cmd.Parameters.AddWithValue("@InvoiceAgainst", txtinvoiceagainst.Text);
                cmd.Parameters.AddWithValue("@AgainstNumber", txtagainstNumber.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                cmd.Parameters.AddWithValue("@SumOfProductAmt", sumofAmount.Text);
                cmd.Parameters.AddWithValue("@ChargesDescription", txtDescription.Text);
                cmd.Parameters.AddWithValue("@HSN", txthsntcs.Text);
                cmd.Parameters.AddWithValue("@Rate", txtrateTcs.Text);
                cmd.Parameters.AddWithValue("@Basic", txtBasic.Text);
                cmd.Parameters.AddWithValue("@CGST", CGSTPertcs.Text);
                cmd.Parameters.AddWithValue("@SGST", SGSTPertcs.Text);
                cmd.Parameters.AddWithValue("@IGST", IGSTPertcs.Text);
                cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                cmd.Parameters.AddWithValue("@TCSPercent", txtTCSPer.Text);
                cmd.Parameters.AddWithValue("@TCSAmt", txtTCSAmt.Text);
                cmd.Parameters.AddWithValue("@GrandTotal", txtGrandTot.Text);
                cmd.Parameters.AddWithValue("@CurrencyType", ddlCurrency.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@SourceName", txtSource.Text);
                cmd.Parameters.AddWithValue("@IsEmail", IsSedndMail.Checked);
                cmd.Parameters.AddWithValue("@Id", hdnHdrId.Value);
                cmd.Parameters.AddWithValue("@TaxType", ddltaxation.SelectedItem.Text);
                cmd.Parameters.Add("@Iddd", SqlDbType.Int).Direction = ParameterDirection.Output;
                //cmd.Parameters.AddWithValue("@Updatedby", Session["ProductionName"].ToString());
                cmd.Parameters.AddWithValue("@Updatedby", Session["name"].ToString());
                cmd.Parameters.AddWithValue("@AdvanceAmt", txtAdvance.Text);
                cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                cmd.Parameters.AddWithValue("@Action", "Update");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                SqlCommand CmdDelete = new SqlCommand("DELETE FROM tblProformaInvoiceDtls WHERE HeaderID=@HeaderID", con);
                CmdDelete.Parameters.AddWithValue("@HeaderID", hidden1.Value);
                con.Open();
                CmdDelete.ExecuteNonQuery();
                con.Close();

                if (gvorder.Rows.Count > 0)
                {
                    foreach (GridViewRow g2 in gvorder.Rows)
                    {
                        string particular = (g2.FindControl("lblParticulars") as TextBox).Text;
                        string Description = (g2.FindControl("txtDescription") as TextBox).Text;
                        string HSN = (g2.FindControl("lblHSN") as TextBox).Text;
                        string QTY = (g2.FindControl("lblQty") as TextBox).Text;
                        string UOM = (g2.FindControl("txtUOM") as TextBox).Text;
                        string RATE = (g2.FindControl("lblRate") as TextBox).Text;
                        string AMOUNT = (g2.FindControl("lblAmount") as Label).Text;
                        string CGST = (g2.FindControl("lblCGSTPer") as TextBox).Text;
                        string SGST = (g2.FindControl("lblSGSTPer") as TextBox).Text;
                        string IGST = (g2.FindControl("lblIGSTPer") as TextBox).Text;
                        string DISCOUNT = (g2.FindControl("txtdiscount") as TextBox).Text;
                        string Grandtotal = (g2.FindControl("txtGrandTotal") as Label).Text;
                        string OAId = (g2.FindControl("lblid") as Label).Text;
                        bool chk = (g2.FindControl("chkRow") as CheckBox).Checked;
                        string CGSTAmt = (g2.FindControl("lblCGSTAmt") as Label).Text;
                        string SGSTAmt = (g2.FindControl("lblSGSTAmt") as Label).Text;
                        string IGSTAmt = (g2.FindControl("lblIGSTAmt") as Label).Text;

                        SqlCommand cmd1 = new SqlCommand("SP_ProformaInvoiceDtls", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@HeaderID", hidden1.Value);
                        cmd1.Parameters.AddWithValue("@CustomerName", txtbillingcustomer.Text);
                        cmd1.Parameters.AddWithValue("@Particular", particular);
                        cmd1.Parameters.AddWithValue("@HSN", HSN);
                        cmd1.Parameters.AddWithValue("@Qty", QTY);
                        cmd1.Parameters.AddWithValue("@UOM", UOM);
                        cmd1.Parameters.AddWithValue("@Rate", RATE);
                        cmd1.Parameters.AddWithValue("@Discount", DISCOUNT);
                        cmd1.Parameters.AddWithValue("@TaxableAmt", AMOUNT);
                        cmd1.Parameters.AddWithValue("@CGSTPer", CGST);
                        cmd1.Parameters.AddWithValue("@SGSTPer", SGST);
                        cmd1.Parameters.AddWithValue("@IGSTPer", IGST);
                        cmd1.Parameters.AddWithValue("@OAId", OAId);
                        cmd1.Parameters.AddWithValue("@CGSTAmt", CGSTAmt);
                        cmd1.Parameters.AddWithValue("@SGSTAmt", SGSTAmt);
                        cmd1.Parameters.AddWithValue("@IGSTAmt", IGSTAmt);
                        //cmd1.Parameters.Add("@Idddup", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd1.Parameters.AddWithValue("@Description", Description);
                        cmd1.Parameters.AddWithValue("@GrandTotal", Grandtotal);

                        if (chk == true)
                        {
                            cmd1.Parameters.AddWithValue("@Action", "Insert");
                        }
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                foreach (GridViewRow g1 in gvinvoiceParticular.Rows)
                {
                    string particular = (g1.FindControl("lblParticulars") as Label).Text;
                    string Description = (g1.FindControl("txtDescription") as Label).Text;
                    string HSN = (g1.FindControl("lblHSN") as Label).Text;
                    string QTY = (g1.FindControl("lblQty") as Label).Text;
                    string UOM = (g1.FindControl("txtUOM") as Label).Text;
                    string RATE = (g1.FindControl("lblRate") as Label).Text;
                    string AMOUNT = (g1.FindControl("lblAmount") as Label).Text;
                    string CGST = (g1.FindControl("lblCGSTPer") as Label).Text;
                    string SGST = (g1.FindControl("lblSGSTPer") as Label).Text;
                    string IGST = (g1.FindControl("lblIGSTPer") as Label).Text;
                    string DISCOUNT = (g1.FindControl("txtdiscount") as Label).Text;
                    string Grandtotal = (g1.FindControl("txtGrandTotal") as Label).Text;
                    string CGSTAmt = (g1.FindControl("lblCgstAmt") as Label).Text;
                    string SGSTAmt = (g1.FindControl("lblSgstAmt") as Label).Text;
                    string IGSTAmt = (g1.FindControl("lblIGSTAmt") as Label).Text;

                    SqlCommand cmd1 = new SqlCommand("SP_ProformaInvoiceDtls", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@HeaderID", hidden1.Value);
                    cmd1.Parameters.AddWithValue("@CustomerName", txtbillingcustomer.Text);
                    cmd1.Parameters.AddWithValue("@Particular", particular);
                    cmd1.Parameters.AddWithValue("@HSN", HSN);
                    cmd1.Parameters.AddWithValue("@Qty", QTY);
                    cmd1.Parameters.AddWithValue("@UOM", UOM);
                    cmd1.Parameters.AddWithValue("@Rate", RATE);
                    cmd1.Parameters.AddWithValue("@Discount", DISCOUNT);
                    cmd1.Parameters.AddWithValue("@TaxableAmt", AMOUNT);
                    cmd1.Parameters.AddWithValue("@CGSTPer", CGST);
                    cmd1.Parameters.AddWithValue("@SGSTPer", SGST);
                    cmd1.Parameters.AddWithValue("@IGSTPer", IGST);
                    cmd1.Parameters.AddWithValue("@Description", Description);
                    cmd1.Parameters.AddWithValue("@GrandTotal", Grandtotal);
                    cmd1.Parameters.AddWithValue("@CGSTAmt", CGSTAmt);
                    cmd1.Parameters.AddWithValue("@SGSTAmt", SGSTAmt);
                    cmd1.Parameters.AddWithValue("@IGSTAmt", IGSTAmt);
                    // cmd1.Parameters.AddWithValue("@OAId", OAId);
                    cmd1.Parameters.AddWithValue("@Action", "Insert");

                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Updated Sucessfully');window.location.href='ProformaInvoiceList.aspx';", true);

                if (IsSedndMail.Checked == true)
                {
                    Send_Mail(hidden1.Value);
                }

                //if (Session["isclosed"] != null)
                //{

                //}
                //else
                //{
                //    con.Open();
                //    SqlCommand cmdupOA = new SqlCommand("update OrderAccept set status='close' where pono='" + txtagainstNumber.SelectedItem.Text + "'", con);
                //    cmdupOA.ExecuteNonQuery();
                //    con.Close();
                //}

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Updated Sucessfully');window.location.href='ProformaInvoiceList.aspx';", true);

            }

        }
        catch (Exception)
        {

            throw;
        }
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

    protected void Send_Mail(string Id)
    {
        string strMessage = "Hello " + txtbillingcustomer.Text.Trim() + "<br/>" +


                        "Greetings From " + "<strong>Excel Encloser<strong>" + "<br/>" +
                        "We sent you  Tax Invoice." + "Tax - " + hiddeninvoiceno.Value.Trim() + "/" + txtinvoicedate.Text.Trim() + ".pdf" + "<br/>" +

                         "We Look Foward to Conducting Future Business with you." + "<br/>" +

                        "Kind Regards," + "<br/>" +
                        "<strong>Excel Encloser<strong>";
        string pdfname = "TaxInv - " + hiddeninvoiceno.Value.Trim() + "/" + txtinvoicedate.Text.Trim() + ".pdf";

        MailMessage message = new MailMessage();
        DataTable dt666 = new DataTable();
        SqlDataAdapter sad = new SqlDataAdapter("select * from Company where cname='" + txtbillingcustomer.Text + "' ", con);
        sad.Fill(dt666);

        message.To.Add(dt666.Rows[0]["email1"].ToString());// Email-ID of Receiver  
        if (btnSubmit.Text == "Submit")
        {
            message.Subject = "Tax Invoice";// Subject of Email  
        }
        else
        {
            message.Subject = "Updated Tax Invoice";// Subject of Email  
        }
        message.Body = strMessage;
        message.From = new System.Net.Mail.MailAddress("enquiry@weblinkservices.net");// Email-ID of Sender  
        message.IsBodyHtml = true;



        MemoryStream file = new MemoryStream(PDF(Id).ToArray());

        file.Seek(0, SeekOrigin.Begin);
        Attachment data = new Attachment(file, pdfname, "application/pdf");
        ContentDisposition disposition = data.ContentDisposition;
        disposition.CreationDate = System.DateTime.Now;
        disposition.ModificationDate = System.DateTime.Now;
        disposition.DispositionType = DispositionTypeNames.Attachment;
        message.Attachments.Add(data);//Attach the file  


        //message.Body = txtmessagebody.Text;
        SmtpClient SmtpMail = new SmtpClient();
        SmtpMail.Host = "smtpout.secureserver.net";//name or IP-Address of Host used for SMTP transactions  
        SmtpMail.Port = 587;//Port for sending the mail  
        SmtpMail.Credentials = new System.Net.NetworkCredential("enquiry@weblinkservices.net", "wlspl@123");//username/password of network, if apply  
        SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpMail.EnableSsl = true;
        SmtpMail.ServicePoint.MaxIdleTime = 0;
        SmtpMail.ServicePoint.SetTcpKeepAlive(true, 2000, 2000);
        message.BodyEncoding = Encoding.Default;
        message.Priority = MailPriority.High;
        SmtpMail.Send(message); //Smtpclient to send the mail message  

        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };



    }

    protected MemoryStream PDF(string id)
    {
        MemoryStream pdf = new MemoryStream();
        DataTable Dt = new DataTable();
        SqlDataAdapter Da = new SqlDataAdapter("select * from vw_TaxInvoicePDF where Id = '" + id + "'", con);

        Da.Fill(Dt);

        StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);
        PdfWriter pdfWriter = PdfWriter.GetInstance(doc, pdf);
        //string Path = ;
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "TaxInvoice.pdf", FileMode.Create));

        //iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "TaxInvoice.pdf", FileMode.Create));
        //PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        //iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
        XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
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


        doc.Add(png);

        //PdfContentByte cb = pdfWriter.DirectContent;
        //cb.Rectangle(17f, 735f, 560f, 60f);
        //cb.Stroke();
        if (Dt.Rows.Count > 0)
        {
            var CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
            string InvoiceNo = Dt.Rows[0]["InvoiceNo"].ToString();
            string invoicedate = Dt.Rows[0]["Invoicedate"].ToString().TrimEnd("0:0".ToCharArray());
            string customerPoNo = Dt.Rows[0]["CustomerPONo"].ToString();
            string PODate = Dt.Rows[0]["PODate"].ToString().TrimEnd("0:0".ToCharArray());
            string EBillNo = Dt.Rows[0]["E_BillNo"].ToString();
            string transactionmode = Dt.Rows[0]["TransportMode"].ToString();
            string vehicalNo = Dt.Rows[0]["VehicalNo"].ToString();
            string placeOfSupply = Dt.Rows[0]["ShippingAddress"].ToString();
            string dateOfSupply = Dt.Rows[0]["Invoicedate"].ToString();
            string billingCustomer = Dt.Rows[0]["BillingCustomer"].ToString();
            string ShippingCustomer = Dt.Rows[0]["ShippingCustomer"].ToString();
            string ShippingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
            string BillingAddress = Dt.Rows[0]["ShippingAddress"].ToString();
            string grandtotal = Dt.Rows[0]["GrandTotalFinal"].ToString();
            string GSTNo = "";


            DataTable dtgstno = new DataTable();
            SqlDataAdapter sadgst = new SqlDataAdapter("select * from Company where cname='" + billingCustomer + "'", con);
            sadgst.Fill(dtgstno);
            if (dtgstno.Rows.Count > 0)
            {
                GSTNo = dtgstno.Rows[0]["gstno"].ToString();
            }

            PdfContentByte cb = pdfWriter.DirectContent;
            cb.Rectangle(17f, 710f, 560f, 60f);
            cb.Stroke();
            // Header 
            cb.BeginText();
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 25);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Excel Enclosure", 250, 745, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Gat No. 1567, Shelar Vasti, Dehu-Alandi Road, Chikhali, Pune - 411062", 145, 728, 0);
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
            cb.EndText();


            PdfContentByte cbbb = pdfWriter.DirectContent;
            cbbb.Rectangle(17f, 685f, 560f, 25f);
            cbbb.Stroke();
            //Header
            cbbb.BeginText();
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "GSTIN :27ATFPS1959J1Z4" + "", 30, 695, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PAN NO: ATFPS1959J" + "", 160, 695, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "EMAIL : mktg@excelenclosures.com" + "", 270, 695, 0);
            cbbb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 10);
            cbbb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CONTACT : 9225658662", 440, 695, 0);
            cbbb.EndText();

            PdfContentByte cd = pdfWriter.DirectContent;
            cd.Rectangle(17f, 660f, 560f, 25f);
            cd.Stroke();
            // Header 
            cd.BeginText();
            cd.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 14);
            cd.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TAX INVOICE", 240, 667, 0);
            cd.EndText();
            //DetailCustomer
            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingBefore = 120f;
            paragraphTable1.SpacingAfter = 1f;

            PdfPTable mtable = new PdfPTable(2);
            mtable.WidthPercentage = 98;
            mtable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = 280f;
            table.LockedWidth = true;
            table.HorizontalAlignment = 1;
            table.SetWidths(new float[] { 180f });
            table.AddCell(new Phrase(" Details of Receiver/Billed to: \n\n " + billingCustomer + "\n Address: " + ShippingAddress + " \n GSTIN: " + GSTNo + " \n State Name:Maharashtra(27) \n  ", FontFactory.GetFont("Arial", 10, Font.NORMAL)));
            table.AddCell(new Phrase(" Details of Consignee/Shipped to: \n\n " + ShippingCustomer + ", \n Address: " + ShippingAddress + "\n GSTIN: " + GSTNo + " \n State Name:Maharashtra(27) \n  ", FontFactory.GetFont("Arial", 10, Font.NORMAL)));

            mtable.AddCell(table);

            table = new PdfPTable(2);
            float[] widths2 = new float[] { 100, 180 };
            table.SetWidths(widths2);
            table.TotalWidth = 280f;
            table.HorizontalAlignment = 2;
            table.LockedWidth = true;
            ///end Customer details
            ///invoice details
            var date = DateTime.Now.ToString("yyyy-MM-dd");

            table.AddCell(new Phrase("Invoice Number : ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(InvoiceNo, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Invoice Date :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(invoicedate, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("PO. No : ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(customerPoNo, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("PO Date :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(PODate, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Transportation Mode", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(transactionmode, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Vehical Number", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(vehicalNo, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Place of Supply :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(ShippingAddress, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Date of Supply :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(invoicedate, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("E-Bill No", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase(EBillNo, FontFactory.GetFont("Arial", 9, Font.BOLD)));

            table.AddCell(new Phrase("Reverse Charge :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            table.AddCell(new Phrase("No", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            mtable.AddCell(table);


            paragraphTable1.Add(mtable);
            doc.Add(paragraphTable1);
            ///end invoice details

            ///Description Table

            Paragraph paragraphTable2 = new Paragraph();
            paragraphTable2.SpacingAfter = 0f;
            table = new PdfPTable(11);
            float[] widths3 = new float[] { 4f, 40f, 11f, 6f, 10f, 12f, 8f, 10f, 8f, 10f, 15f };
            table.SetWidths(widths3);

            Double Ttotal_price = 0;
            Double CGST_price = 0;
            Double SGST_price = 0;
            Double GrandTotal1 = 0;
            string CGSTPer = "";
            string SGSTPer = "";
            if (Dt.Rows.Count > 0)
            {
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Name Of Particulars", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Hsn/Sac", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Qty", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Rate", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Amount", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("CGST(%)", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("CGST Amt", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("SGST(%)", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("SGST Amt", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));

                int rowid = 1;
                foreach (DataRow dr in Dt.Rows)
                {
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;

                    Double Ftotal = Convert.ToDouble(dr["GrandTotal"].ToString());
                    string _ftotal = Ftotal.ToString("##.00");

                    string Description = dr["Particular"].ToString() + "\n" + dr["Description"].ToString();

                    var amt = dr["TaxableAmt"].ToString();
                    var cgstper = dr["CGSTPer"].ToString();
                    var sgstper = dr["SGSTPer"].ToString();

                    var cgstamt = Convert.ToDouble(amt) * Convert.ToDouble(cgstper) / 100;
                    var sgstamt = Convert.ToDouble(amt) * Convert.ToDouble(sgstper) / 100;

                    table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(Description, FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["HSN"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["Qty"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["Rate"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["TaxableAmt"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["CGSTPer"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(cgstamt.ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(dr["SGSTPer"].ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(sgstamt.ToString(), FontFactory.GetFont("Arial", 9)));
                    table.AddCell(new Phrase(_ftotal, FontFactory.GetFont("Arial", 9)));
                    rowid++;
                    CGSTPer = dr["CGSTPer"].ToString();
                    SGSTPer = dr["SGSTPer"].ToString();
                    Ttotal_price += Convert.ToDouble(dr["TaxableAmt"].ToString());
                    GrandTotal1 += Convert.ToDouble(dr["GrandTotal"].ToString());
                    CGST_price += Convert.ToDouble(cgstamt);
                    SGST_price += Convert.ToDouble(sgstamt);
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

            Font font12 = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font font10 = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Paragraph paragraph = new Paragraph("", font12);

            for (int i = 0; i < items.Length; i++)
            {
                paragraph.Add(new Phrase("", font10));
            }

            table = new PdfPTable(11);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 4f, 40f, 11f, 6f, 10f, 12f, 8f, 10f, 8f, 10f, 15f });
            table.AddCell(paragraph);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            // table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n ", FontFactory.GetFont("Arial", 10, Font.BOLD)));

            doc.Add(table);
            //Space end
            //change description
            Paragraph paragraphTable4 = new Paragraph();
            paragraphTable4.SpacingAfter = 0f;
            table = new PdfPTable(11);
            float[] widths33 = new float[] { 4f, 40f, 11f, 6f, 10f, 12f, 8f, 10f, 8f, 10f, 15f };
            table.SetWidths(widths3);

            Double Ttotal_price1 = 0;
            Double CGST_price1 = 0;
            Double SGST_price1 = 0;
            int rowidd = 1;
            Double ValueOFSupply = 0;
            Double CGStTotal = 0;
            Double SGStTotal = 0;
            Double TotalAmount = 0;
            SqlCommand cmd = new SqlCommand("select * from vw_TaxInvoicePDF where Id='" + id + "'", con);
            con.Open();
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {

                table.TotalWidth = 560f;
                table.LockedWidth = true;

                Double Ftotal = Convert.ToDouble(dr1["Cost"].ToString());
                string _ftotal = Ftotal.ToString("##.00");

                string Description1 = dr1["ChargesDescription"].ToString();

                var amt = dr1["TaxableAmt"].ToString();
                var cgstper1 = dr1["CGST"].ToString();
                var sgstper1 = dr1["SGST"].ToString();

                var cgstamt1 = Convert.ToDouble(amt) * Convert.ToDouble(cgstper1) / 100;
                var sgstamt1 = Convert.ToDouble(amt) * Convert.ToDouble(sgstper1) / 100;

                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(Description1, FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(dr1["HSNTcs"].ToString(), FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(dr1["RateTcs"].ToString(), FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(dr1["Basic"].ToString(), FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(dr1["CGST"].ToString(), FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(cgstamt1.ToString(), FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(dr1["SGST"].ToString(), FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(sgstamt1.ToString(), FontFactory.GetFont("Arial", 9)));
                table.AddCell(new Phrase(_ftotal, FontFactory.GetFont("Arial", 9)));
                //rowidd++;

                Ttotal_price1 += Convert.ToDouble(dr1["TaxableAmt"].ToString());
                CGST_price1 += Convert.ToDouble(cgstamt1);
                SGST_price1 += Convert.ToDouble(sgstamt1);

                ValueOFSupply = Convert.ToDouble(Ttotal_price) + Convert.ToDouble(dr1["Basic"]);
                CGStTotal = Convert.ToDouble(CGST_price) + Convert.ToDouble(cgstamt1);
                SGStTotal = Convert.ToDouble(SGST_price) + Convert.ToDouble(sgstamt1);
                TotalAmount = Convert.ToDouble(Ftotal) + Convert.ToDouble(GrandTotal1);
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

            table.SetWidths(new float[] { 0f, 119f, 15f });
            table.AddCell(paragraph);
            PdfPCell cell = new PdfPCell(new Phrase("Value of Supply", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
            PdfPCell cell11 = new PdfPCell(new Phrase(ValueOFSupply.ToString("#.00"), FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell11.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell11);
            doc.Add(table);
            ///end calculation supply
            var CGSTAmount = ValueOFSupply * (Convert.ToDouble(CGSTPer)) / 100;
            var SGSTAmount = ValueOFSupply * (Convert.ToDouble(SGSTPer)) / 100;
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
            PdfPCell cell6 = new PdfPCell(new Phrase("Add CGST(" + CGSTPer + "%)", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell6.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell6);
            PdfPCell cell116 = new PdfPCell(new Phrase(CGSTAmount.ToString(), FontFactory.GetFont("Arial", 10, Font.BOLD)));
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
            PdfPCell cell67 = new PdfPCell(new Phrase("Add SGST(" + SGSTPer + "%)", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell67.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell67);
            PdfPCell cell1167 = new PdfPCell(new Phrase(SGSTAmount.ToString("#.00"), FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell1167.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell1167);
            doc.Add(table);
            ///end SGST

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

            Double TaxAmount = CGSTAmount + SGSTAmount;

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
            PdfPCell cell678 = new PdfPCell(new Phrase("Tax Amount", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell678.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell678);
            PdfPCell cell11678 = new PdfPCell(new Phrase(TaxAmount.ToString("#.00"), FontFactory.GetFont("Arial", 10, Font.BOLD)));
            cell11678.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell11678);
            doc.Add(table);
            ///end Tax Amount
            ///total Amount 
            /// 

            var totalgrandAmount = ValueOFSupply + TaxAmount;
            Double grandtotal1 = Convert.ToDouble(totalgrandAmount);
            string Amtinword = ConvertNumbertoWords(Convert.ToInt32(grandtotal1));

            //Total amount InNumber
            table = new PdfPTable(3);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            paragraph.Alignment = Element.ALIGN_RIGHT;

            table.SetWidths(new float[] { 0f, 199f, 25f });
            table.AddCell(paragraph);

            PdfPCell cell443458 = new PdfPCell(new Phrase("Total Amount(Rs) ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell443458.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell443458);
            //PdfPCell cell443457 = new PdfPCell(new Phrase("Total Amount(Rs): " , FontFactory.GetFont("Arial", 9, Font.BOLD)));
            ////cell443457.HorizontalAlignment = Element.ALIGN_LEFT;
            //table.AddCell(cell443457);
            PdfPCell cell440448 = new PdfPCell(new Phrase(Math.Round(totalgrandAmount).ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
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

            PdfPCell cell44345 = new PdfPCell(new Phrase("Total Amount(Rs): " + Amtinword + "", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell44345.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell44345);
            //PdfPCell cell443457 = new PdfPCell(new Phrase("Total Amount(Rs): " , FontFactory.GetFont("Arial", 9, Font.BOLD)));
            ////cell443457.HorizontalAlignment = Element.ALIGN_LEFT;
            //table.AddCell(cell443457);
            PdfPCell cell44044 = new PdfPCell(new Phrase(grandtotal1.ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell44044.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell44044);
            doc.Add(table);
            ///end Total Amount

            //Declaration
            Paragraph paragraphTable99 = new Paragraph(" Remarks :\n\n", font12);

            //Puja Enterprises Sign
            string[] itemss = {
                "Declaration  : I/We hereby certify that my/our registration certificate under the GST Act, 2017 is in force on the",
                "date on which the supply of the goods specified in this tax invoice is made by me/us and that the transaction ",
                "of supplies covered by this tax invoice has been effected by me/us and it shall be accounted for in the ",
                "turnover of supplies while filing of return and the due tax, if any, payable on the supplies has been paid or ",
                "shall be paid. \n",
                        };

            Font font14 = FontFactory.GetFont("Arial", 11);
            Font font15 = FontFactory.GetFont("Arial", 10);
            Paragraph paragraphhh = new Paragraph(" Terms & Condition :\n\n", font10);


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
            doc.Add(table);
            ///end declaration
            ///

            ///Sign Authorization
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
            table.AddCell(new Phrase("\bBANK: HDFC BANK LTD(THERMAX CHOWK)\n\bAcc No.:17958970000057\n\bIFSC:HDFC0001795\n\bCHINCHWAD(Branch Code(1795)\n\bMICR No.:411240031\n\bSWIFT No.:002205 ", FontFactory.GetFont("Arial", 10)));
            table.AddCell(new Phrase("         For Excel Enclosures \n\n\n\n\n\n         Authorised Signature", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            doc.Add(table);
            doc.Close();
            ///end Sign Authorization

        }

        //doc.Close();

        // Byte [] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + "TaxInvoice.pdf");

        //Font blackFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
        //using (MemoryStream stream = new MemoryStream())
        //{
        //    PdfReader reader = new PdfReader(Server.MapPath("~/files/") + "TaxInvoice.pdf");
        //    using (PdfStamper stamper = new PdfStamper(reader, stream))
        //    {

        //int pages = 5;
        //for (int j = 1; j <= pages; j++)
        //{
        //    if (j == 1)
        //    {

        //    }
        //    else
        //    {
        //        Image image = Image.GetInstance(Server.MapPath("~/img/ExcelEncLogo.png"));

        //        image.ScaleToFit(70, 100);
        //        image.SetAbsolutePosition(40, 792);
        //        image.SpacingBefore = 50f;
        //        image.SpacingAfter = 1f;
        //        image.Alignment = Image.ALIGN_LEFT;
        //        doc.Add(image);
        //    }
        //    var PageName = "Page No. " + j.ToString();
        //    //    }
        //    //}
        //}


        string filePath = @Server.MapPath("~/files/") + "TaxInvoice.pdf";
        //Response.ContentType = "TaxInvoice.pdf";
        //Response.WriteFile(filePath);
        pdfWriter.CloseStream = false;
        //doc.Close();
        //pdf.Position = 0;
        return pdf;
    }

    protected void txtinvoiceagainst_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(txtbillingcustomer.Text))
            {
                if (txtinvoiceagainst.Text == "Direct")
                {
                    txtagainstNumber.Enabled = false;
                    manuallytable.Visible = true;
                    divdtls1.Visible = false;
                    sumofAmount.Text = string.Empty;
                }
                else if (txtinvoiceagainst.Text == "Order")
                {
                    divdtls1.Visible = true;
                    manuallytable.Visible = false;
                    txtagainstNumber.Enabled = true;
                }
                else
                {
                    txtagainstNumber.SelectedValue = "0";
                }
            }
            else
            {
 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter Customer Name First');window.location.href='ProformaInvoice.aspx';", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void txtagainstNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtorderno = new DataTable();
            SqlDataAdapter sadorderno = new SqlDataAdapter("select * from OrderAcceptDtls where OAno='" + txtagainstNumber.SelectedValue + "'", con);
            sadorderno.Fill(dtorderno);

            gvorder.DataSource = dtorderno;
            gvorder.DataBind();

            //DataTable dtPODetails = new DataTable();
            //SqlDataAdapter saPODetails = new SqlDataAdapter("select * from OrderAccept where OAno='" + txtagainstNumber.SelectedValue + "'", con);
            //saPODetails.Fill(dtPODetails);
            //if (dtPODetails.Rows.Count > 0)
            //{
            //    txtcustomerPoNo.Text = dtPODetails.Rows[0]["pono"].ToString();

            //    string str1 = dtPODetails.Rows[0]["podate"].ToString();
            //    str1 = str1.Replace("12:00:00 AM", "");
            //    var time1 = str1;
            //    DateTime Invoice = Convert.ToDateTime(str1);
            //    str1 = Invoice.ToString("dd-MM-yyyy");

            //    txtpodate.Text = str1; //dtPODetails.Rows[0]["podate"].ToString().Replace("/", "-").TrimEnd("0:0".ToCharArray());
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    Double qty = 0, rate = 0, Amount = 0, Totalamt1 = 0, GrandTotalamt1 = 0;
    protected void gvorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txt_Qty = (TextBox)e.Row.FindControl("lblQty");
            CheckBox chk = (CheckBox)e.Row.FindControl("chkRow");
            Label id = (Label)e.Row.FindControl("lblid");
            Label Headerid = (Label)e.Row.FindControl("lblid");
            int OAId = Convert.ToInt32(id.Text);
            int sumofqty;

            SqlCommand cmdmax = new SqlCommand("SELECT Max(Id) FROM tblProformaInvoiceDtls where OAId='" + OAId + "'", con);
            con.Open();
            Object mxid = cmdmax.ExecuteScalar();
            con.Close();

            SqlCommand cmdsumQty = new SqlCommand("SELECT SUM(CAST(Qty as int)) FROM tblProformaInvoiceDtls where OAId='" + OAId + "'", con);
            con.Open();
            Object smQty = cmdsumQty.ExecuteScalar();
            //sumofqty = Convert.ToInt32(smQty);
            con.Close();

            var mxiddd = mxid.ToString() == "" ? null : mxid.ToString();
            var smquantity = smQty.ToString() == "" ? "0" : smQty.ToString();

            DataTable dtTIDetails = new DataTable();
            SqlDataAdapter saTIDetails = new SqlDataAdapter("select * from tblProformaInvoiceDtls where OAId='" + OAId + "' and Id='" + Convert.ToInt32(mxiddd) + "'", con);
            saTIDetails.Fill(dtTIDetails);
            if (dtTIDetails.Rows.Count > 0)
            {
                //var ExistQty = dtTIDetails.Rows[0]["Qty"].ToString();
                var Qty = txt_Qty.Text;
                //var minusQty = Convert.ToInt32(Qty) - Convert.ToInt32(smquantity);     //original row 8-6-23
                var minusQty = Convert.ToInt32(Qty);                                     // Duplocate row 8 - 6 - 23
                txt_Qty.Text = minusQty.ToString();
                if (minusQty == 0)
                {
                    chk.Enabled = false;
                }

            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAmount1 = (Label)e.Row.FindControl("lblAmount");

            qty = Convert.ToDouble((e.Row.FindControl("lblQty") as TextBox).Text);
            rate = Convert.ToDouble((e.Row.FindControl("lblRate") as TextBox).Text);
            Amount = qty * rate;
            lblAmount1.Text = Amount.ToString();
            TextBox txt_Qty = (TextBox)e.Row.FindControl("lblQty");
            TextBox txt_price = (TextBox)e.Row.FindControl("lblRate");
            TextBox txt_CGST = (TextBox)e.Row.FindControl("lblCGSTPer");
            TextBox txt_SGST = (TextBox)e.Row.FindControl("lblSGSTPer");
            TextBox txt_IGST = (TextBox)e.Row.FindControl("lblIGSTPer");
            Label txt_amount = (Label)e.Row.FindControl("txtGrandTotal");
            Label taxableamt = (Label)e.Row.FindControl("lblAmount");
            Label lblCGST = (Label)e.Row.FindControl("lblCGSTAmt");
            Label lblSGST = (Label)e.Row.FindControl("lblSGSTAmt");
            Label LblIGST = (Label)e.Row.FindControl("lblIGSTAmt");
            TextBox txt_discount = (TextBox)e.Row.FindControl("txtdiscount");
			   if (txt_discount != null)
            {
                // Get the text from the TextBox and trim it
                string discountText = txt_discount.Text.Trim();

                // Set the text to "0" if the TextBox is empty or null
                if (string.IsNullOrEmpty(discountText))
                {
                    txt_discount.Text = "0";
                }
            }

            //var totalamt = Convert.ToDouble(txt_Qty.Text.Trim()) * Convert.ToDouble(txt_price.Text.Trim());
            //taxableamt.Text = totalamt.ToString();
            //var CGSTamt = totalamt * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
            //var SGSTamt = totalamt * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
            //var IGSTamt = totalamt * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;
            //lblCGST.Text = CGSTamt.ToString();
            //lblSGST.Text = SGSTamt.ToString();
            //LblIGST.Text = IGSTamt.ToString();

            //double GSTtotal;
            //if (LblIGST.Text == "0")
            //{
            //    GSTtotal = SGSTamt + CGSTamt;

            //    CGSTPertcs.Text = txt_CGST.Text;
            //    SGSTPertcs.Text = txt_SGST.Text;
            //}
            //else
            //{
            //    GSTtotal = IGSTamt;
            //    IGSTPertcs.Text = txt_IGST.Text;
            //}


            //var NetAmt = totalamt + GSTtotal;
            ////var NetAmt = totalamt;

            //Double AmtWithDiscount;
            //if (txt_discount.Text != "" || txt_discount.Text != null)
            //{
            //    var disc = NetAmt * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;

            //    AmtWithDiscount = NetAmt - disc;
            //}
            //else
            //{
            //    AmtWithDiscount = 0;
            //}

            //txt_amount.Text = AmtWithDiscount.ToString("##.00");

            //Totalamt1 += Convert.ToDouble(Amount);
            //GrandTotalamt1 += Convert.ToDouble((e.Row.FindControl("txtGrandTotal") as Label).Text);
            //hdnGrandtotal.Value = GrandTotalamt1.ToString();
            ////sumofAmount.Text = Totalamt1.ToString();
            //var Total = Convert.ToDouble(txtCost.Text) + GrandTotalamt1 + Convert.ToDouble(txtTCSAmt.Text);
            //txtGrandTot.Text = Total.ToString("##.00");

            // changes on  23-02-2024 by shubham 
            var totalamt = Convert.ToDouble(txt_Qty.Text.Trim()) * Convert.ToDouble(txt_price.Text.Trim());
            var NetAmt = totalamt;
            Double CGSTamt = 0;
            Double SGSTamt = 0;
            Double IGSTamt = 0;
            Double GSTtotal = 0;

            Double AmtWithDiscount;
            if (txt_discount.Text != "0")
            {
                var disc = NetAmt * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;

                AmtWithDiscount = NetAmt - disc;
                var TotalamoountafterDisc = (totalamt - disc);

                if (txt_IGST.Text == "18")
                {
                    IGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;
                    GSTtotal = IGSTamt;
                    NetAmt = TotalamoountafterDisc + GSTtotal;
                    txt_amount.Text = NetAmt.ToString("##.00");
                    taxableamt.Text = TotalamoountafterDisc.ToString();
                    string IGSTamt1 = Convert.ToString(IGSTamt);
                    LblIGST.Text = IGSTamt1; // Formatting IGSTamt to display with two decimal places

                    return;
                }

                else
                {
                    CGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
                    SGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
                    GSTtotal = CGSTamt + SGSTamt;
                    NetAmt = TotalamoountafterDisc + GSTtotal;
                    txt_amount.Text = NetAmt.ToString("##.00");
                    taxableamt.Text = TotalamoountafterDisc.ToString();
                    string CGSTamt1 = Convert.ToString(CGSTamt);
                    lblSGST.Text = CGSTamt1;
                    lblCGST.Text = CGSTamt1;
                    return;
                }
            }
            else
            {
                AmtWithDiscount = 0;
                var disc = NetAmt * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;

                AmtWithDiscount = NetAmt - disc;
                var TotalamoountafterDisc = (totalamt - disc);

                if (txt_IGST.Text != "0")
                {
                    IGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;
                    GSTtotal = IGSTamt;
                    NetAmt = TotalamoountafterDisc + GSTtotal;
                    txt_amount.Text = NetAmt.ToString("##.00");
                    taxableamt.Text = TotalamoountafterDisc.ToString();
                    string IGSTamt1 = Convert.ToString(IGSTamt);
                    LblIGST.Text = IGSTamt1; // Formatting IGSTamt to display with two decimal places

                    return;
                }

                else
                {
                    CGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
                    SGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
                    GSTtotal = CGSTamt + SGSTamt;
                    NetAmt = TotalamoountafterDisc + GSTtotal;
                    txt_amount.Text = NetAmt.ToString("##.00");
                    taxableamt.Text = TotalamoountafterDisc.ToString();
                    string CGSTamt1 = Convert.ToString(CGSTamt);
                    lblSGST.Text = CGSTamt1;
                    lblCGST.Text = CGSTamt1;
                    return;
                }


            }



        }
    }

    Double GrandTotal111 = 0, grandtotalt = 0, FinalGrandtotalTcs = 0;
    Double total = 0;
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        //GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
        //foreach (GridViewRow row in gvorder.Rows)
        //{
        //    TextBox discription = (TextBox)row.FindControl("txtDescription");
        //    TextBox HSN = (TextBox)row.FindControl("lblHSN");
        //    TextBox txtqty = (TextBox)row.FindControl("lblQty");
        //    TextBox UOM = (TextBox)row.FindControl("txtUOM");
        //    TextBox rate = (TextBox)row.FindControl("lblRate");
        //    Label Amount = (Label)row.FindControl("lblAmount");
        //    TextBox cgst = (TextBox)row.FindControl("lblCGSTPer");
        //    TextBox sgst = (TextBox)row.FindControl("lblSGSTPer");
        //    TextBox Igst = (TextBox)row.FindControl("lblIGSTPer");
        //    TextBox discount = (TextBox)row.FindControl("txtdiscount");
        //    Label grandtotal = (Label)row.FindControl("txtGrandTotal");
        //    CheckBox chk = (CheckBox)row.FindControl("chkRow");
        //    CheckBox chkheader = (CheckBox)gvorder.HeaderRow.FindControl("chkHeader");


        //    if (chk != null & chk.Checked)
        //    {


        //        Double qty1 = Convert.ToDouble(txtqty.Text);

        //        if (qty1 == 0)
        //        {
        //            discription.Enabled = false;
        //            HSN.Enabled = false;
        //            txtqty.Enabled = false;
        //            UOM.Enabled = false;
        //            rate.Enabled = false;
        //            Amount.Enabled = false;
        //            cgst.Enabled = false;
        //            sgst.Enabled = false;
        //            Igst.Enabled = false;
        //            discount.Enabled = false;
        //            chkheader.Checked = false;
        //            chk.Enabled = false;
        //        }
        //        else
        //        {
        //            discription.Enabled = true;
        //            HSN.Enabled = true;
        //            txtqty.Enabled = true;
        //            UOM.Enabled = true;
        //            rate.Enabled = true;
        //            Amount.Enabled = true;
        //            cgst.Enabled = true;
        //            sgst.Enabled = true;
        //            Igst.Enabled = true;
        //            discount.Enabled = true;
        //            chk.Enabled = true;
        //            Double price1 = Convert.ToDouble(rate.Text);
        //            total = (qty1 * price1);
        //            Amount.Text = total.ToString();
        //            Totalamt += total;
        //            sumofAmount.Text = Totalamt.ToString();
        //            grandtotalt = Convert.ToDouble(grandtotal.Text);
        //            FinalGrandtotalTcs += grandtotalt;
        //            //txtGrandTot.Text = FinalGrandtotalTcs.ToString();
        //            hdnGrandtotal.Value = FinalGrandtotalTcs.ToString();
        //            var Total = Convert.ToDouble(txtCost.Text) + FinalGrandtotalTcs + Convert.ToDouble(txtTCSAmt.Text);
        //            txtGrandTot.Text = Total.ToString("##.00");
        //        }
        //    }
        //    else
        //    {
        //        discription.Enabled = false;
        //        HSN.Enabled = false;
        //        txtqty.Enabled = false;
        //        UOM.Enabled = false;
        //        rate.Enabled = false;
        //        Amount.Enabled = false;
        //        cgst.Enabled = false;
        //        sgst.Enabled = false;
        //        Igst.Enabled = false;
        //        discount.Enabled = false;
        //        chkheader.Checked = false;


        //    }

        //    //GrandTotalamt = Convert.ToDouble(grandtotal.Text);
        //}


        // GrandTotal111 += grandtotalt;


        double totalAmount = 0;
        double Grandtotal = 0;

        foreach (GridViewRow row in gvorder.Rows)
        {
            CheckBox chckheader = (CheckBox)gvorder.HeaderRow.FindControl("chkHeader");
            CheckBox chk = (CheckBox)row.FindControl("chkRow");
            TextBox discription = (TextBox)row.FindControl("txtDescription");
            TextBox HSN = (TextBox)row.FindControl("lblHSN");
            TextBox txtqty = (TextBox)row.FindControl("lblQty");
            TextBox UOM = (TextBox)row.FindControl("txtUOM");
            TextBox rate = (TextBox)row.FindControl("lblRate");
            Label Amount = (Label)row.FindControl("lblAmount");
            TextBox cgst = (TextBox)row.FindControl("lblCGSTPer");
            TextBox sgst = (TextBox)row.FindControl("lblSGSTPer");
            TextBox Igst = (TextBox)row.FindControl("lblIGSTPer");
            TextBox discount = (TextBox)row.FindControl("txtdiscount");
            Label grandtotal = (Label)row.FindControl("txtGrandTotal");
            CheckBox chkrow = (CheckBox)row.FindControl("chkRow");
            Label IGlblSgstAmt = (Label)row.FindControl("lblSgstAmt");
            Label IGlblcgstAmt = (Label)row.FindControl("lblCgstAmt");
            Label IGstammount = (Label)row.FindControl("lblIGSTAmt");

            if (chk != null & chk.Checked)
             {
                chkrow.Checked = true;
                discription.Enabled = true;
                HSN.Enabled = true;
                txtqty.Enabled = true;
                UOM.Enabled = true;
                rate.Enabled = true;
                Amount.Enabled = true;
                cgst.Enabled = true;
                sgst.Enabled = true;
                Igst.Enabled = true;
                discount.Enabled = true;

                Double qty1 = Convert.ToDouble(txtqty.Text);
                Double price1 = Convert.ToDouble(rate.Text);
                Double CGSTamt = 0;
                Double SGSTamt = 0;
                Double IGSTamt = 0;
                Double GSTtotal = 0;

                if (cgst.Text == "0" && Igst.Text == "0")
                {
                    // No GST
                    Amount.Text = (qty1 * price1).ToString();
                    totalAmount += qty1 * price1;
                    Grandtotal += qty1 * price1;
                }
                else
                {
                    if (discount.Text != "0")
                    {
                        double discountValue = Convert.ToDouble(discount.Text.Trim());
                        double total = qty1 * price1;
                        var disc = total * (Convert.ToDouble(discount.Text.Trim())) / 100;
                        var TotalamoountafterDisc = (total - disc);

                        if (Igst.Text != "0")
                        {
                            IGSTamt = TotalamoountafterDisc * (Convert.ToDouble(Igst.Text.Trim())) / 100;
                            total -= total * (discountValue / 100);
                            Amount.Text = total.ToString();
                            totalAmount += total;
                            Grandtotal += total + IGSTamt;
                        }
                        if (cgst.Text != "")
                        {
                            CGSTamt = TotalamoountafterDisc * (Convert.ToDouble(cgst.Text.Trim())) / 100;
                            total -= total * (discountValue / 100);
                            Amount.Text = total.ToString();
                            totalAmount += total;
                            Grandtotal += total + CGSTamt + CGSTamt;
                        }
                    }
                    else
                    {
                        double total = qty1 * price1;

                        if (Igst.Text != "0")
                        {
                            IGSTamt = total * (Convert.ToDouble(Igst.Text.Trim())) / 100;
                            Amount.Text = total.ToString();
                            totalAmount += total;
                            Grandtotal += total + IGSTamt;
                        }
                        if (cgst.Text != "0")
                        {
                            CGSTamt = total * (Convert.ToDouble(cgst.Text.Trim())) / 100;
                            Amount.Text = total.ToString();
                            totalAmount += total;
                            Grandtotal += total + CGSTamt + CGSTamt;
                        }
                    }
                }
            }
        }

        txtGrandTot.Text = Grandtotal.ToString();
        sumofAmount.Text = totalAmount.ToString();
	    hdnGrandtotal.Value = Grandtotal.ToString();
    }

    Double Totalamt111 = 0, GrandTotalamt11 = 0;
    private void calculationA(GridViewRow row)
    {
        TextBox txt_Qty = (TextBox)row.FindControl("lblQty");
        TextBox txt_price = (TextBox)row.FindControl("lblRate");
        TextBox txt_CGST = (TextBox)row.FindControl("lblCGSTPer");
        TextBox txt_SGST = (TextBox)row.FindControl("lblSGSTPer");
        TextBox txt_IGST = (TextBox)row.FindControl("lblIGSTPer");
        Label lblCGSTAmt = (Label)row.FindControl("lblCGSTAmt");
        Label lblSGSTAmt = (Label)row.FindControl("lblSGSTAmt");
        Label lblIGSTAmt = (Label)row.FindControl("lblIGSTAmt");
        Label txt_amount = (Label)row.FindControl("txtGrandTotal");
        TextBox txt_discount = (TextBox)row.FindControl("txtdiscount");
        Label Amount = (Label)row.FindControl("lblAmount");



        var totalamt = Convert.ToDouble(txt_Qty.Text.Trim()) * Convert.ToDouble(txt_price.Text.Trim());
        Amount.Text = totalamt.ToString("#0.00");

        Double AmtWithDiscount;
        if (txt_discount.Text != "" || txt_discount.Text != null)
        {
            var disc = Convert.ToDouble(Amount.Text) * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;

            AmtWithDiscount = Convert.ToDouble(Amount.Text) - disc;
        }
        else
        {
            AmtWithDiscount = 0;
        }
        Amount.Text = AmtWithDiscount.ToString("#0.00");

        var CGSTamt = Convert.ToDouble(Amount.Text) * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
        var SGSTamt = Convert.ToDouble(Amount.Text) * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
        var IGSTamt = Convert.ToDouble(Amount.Text) * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;

        lblCGSTAmt.Text = CGSTamt.ToString("#0.00");
        lblSGSTAmt.Text = SGSTamt.ToString("#0.00");
        lblIGSTAmt.Text = IGSTamt.ToString("#0.00");

        double GSTtotal;
        if (lblIGSTAmt.Text != "0.00")
        {
            GSTtotal = IGSTamt;
        }
        else
        {
            GSTtotal = SGSTamt + CGSTamt;
        }

        var NetAmt = Convert.ToDouble(Amount.Text) + GSTtotal;
        txt_amount.Text = NetAmt.ToString();


        foreach (GridViewRow g1 in gvorder.Rows)
        {
            Label Amount1 = (Label)g1.FindControl("lblAmount");
            Label txtGrandTotal = (Label)g1.FindControl("txtGrandTotal");
            CheckBox chkrow = (CheckBox)g1.FindControl("chkRow");
            if (chkrow.Checked == true)
            {
                Totalamt111 += Convert.ToDouble(Amount1.Text);
                GrandTotalamt11 += Convert.ToDouble(txtGrandTotal.Text);
            }

        }

        hdnGrandtotal.Value = GrandTotalamt11.ToString();
        sumofAmount.Text = Totalamt111.ToString();
        var total = Convert.ToDouble(GrandTotalamt11) + Convert.ToDouble(txtCost.Text) + Convert.ToDouble(txtTCSAmt.Text);
        txtGrandTot.Text = total.ToString("#0.00");
    }

    protected void lblQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        Label lblAmount1 = (Label)row.FindControl("lblAmount");
        calculationA(row);

        Session["isclosed"] = "open";
    }

    protected void lblRate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
    }

    protected void lblCGSTPer_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        TextBox txt_CGST = (TextBox)row.FindControl("lblCGSTPer");
        TextBox txt_SGST = (TextBox)row.FindControl("lblSGSTPer");
        TextBox txt_IGST = (TextBox)row.FindControl("lblIGSTPer");
        calculationA(row);
        if (txt_CGST.Text == "" || txt_CGST.Text == "0")
        {
            txt_IGST.Enabled = true;
            txt_IGST.Text = "0";
        }
        else
        {
            txt_IGST.Enabled = false;
            txt_IGST.Text = "0";
        }
    }

    protected void lblSGSTPer_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        TextBox txt_CGST = (TextBox)row.FindControl("lblCGSTPer");
        TextBox txt_SGST = (TextBox)row.FindControl("lblSGSTPer");
        TextBox txt_IGST = (TextBox)row.FindControl("lblIGSTPer");
        calculationA(row);
        if (txt_SGST.Text == "" || txt_SGST.Text == "0")
        {
            txt_IGST.Enabled = true;
            txt_IGST.Text = "0";
        }
        else
        {
            txt_IGST.Enabled = false;
            txt_IGST.Text = "0";
        }
    }

    protected void lblIGSTPer_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        TextBox txt_CGST = (TextBox)row.FindControl("lblCGSTPer");
        TextBox txt_SGST = (TextBox)row.FindControl("lblSGSTPer");
        TextBox txt_IGST = (TextBox)row.FindControl("lblIGSTPer");
        calculationA(row);
        if (txt_IGST.Text == "" || txt_IGST.Text == "0")
        {
            txt_SGST.Enabled = true;
            txt_CGST.Enabled = true;
            txt_SGST.Text = "0";
            txt_CGST.Text = "0";
        }
        else
        {
            txt_SGST.Enabled = false;
            txt_CGST.Enabled = false;
            txt_SGST.Text = "0";
            txt_CGST.Text = "0";
        }
    }

    protected void txtdiscount_TextChanged1(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
    }

    protected void gvorder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvinvoiceParticular.EditIndex = e.NewEditIndex;
        gvinvoiceParticular.DataSource = (DataTable)ViewState["ParticularDetails"];
        gvinvoiceParticular.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void txtBasic_TextChanged(object sender, EventArgs e)
    {
        if (txtinvoiceagainst.Text == "Direct")
        {
            if (txtBasic.Text != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter GST');", true);
                btnSubmit.Enabled = false;
            }
        }

        string Amt = sumofAmount.Text;
        string Basic = txtBasic.Text;
        if (Basic == "0")
        {
            txtBasic.Text = "0";
            txtCost.Text = "0";
            CGSTPertcs.Text = "0";
            SGSTPertcs.Text = "0";
            IGSTPertcs.Text = "0";
        }
        else
        {
            var Per = Convert.ToDouble(Basic) / Convert.ToDouble(Amt) * 100;
            txtrateTcs.Text = Per.ToString("##.00");

            if (IGSTPertcs.Text == "0")
            {
                IGSTPertcs.Enabled = true;
                CGSTPertcs.Enabled = true;
                SGSTPertcs.Enabled = true;
                var CGSTAmt = Convert.ToDouble(Basic) * Convert.ToDouble(CGSTPertcs.Text) / 100;
                var SGSTAmt = Convert.ToDouble(Basic) * Convert.ToDouble(SGSTPertcs.Text) / 100;

                var GSTTaxTotal = Convert.ToDouble(Basic) + CGSTAmt + SGSTAmt;
                txtCost.Text = GSTTaxTotal.ToString("##.00");

                var grandtot = Convert.ToDouble(GSTTaxTotal) + Convert.ToDouble(hdnGrandtotal.Value) + Convert.ToDouble(txtTCSAmt.Text);
                txtGrandTot.Text = grandtot.ToString("##.00");
            }
            else
            {
                IGSTPertcs.Enabled = true;
                CGSTPertcs.Enabled = false;
                SGSTPertcs.Enabled = false;
                var IGSTAmt = Convert.ToDouble(Basic) * Convert.ToDouble(IGSTPertcs.Text) / 100;
                var GSTTaxTotal = Convert.ToDouble(Basic) + IGSTAmt;
                txtCost.Text = GSTTaxTotal.ToString("##.00");

                var grandtot = Convert.ToDouble(GSTTaxTotal) + Convert.ToDouble(hdnGrandtotal.Value) + Convert.ToDouble(txtTCSAmt.Text);
                txtGrandTot.Text = grandtot.ToString("##.00");
            }

            //var grandtot = Convert.ToDouble(Basic) + Convert.ToDouble(hdnGrandtotal.Value);
            //txtGrandTot.Text = grandtot.ToString("##.00");
        }
    }

    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {

        double totalAmount = 0;
        double Grandtotal = 0;

        foreach (GridViewRow row in gvorder.Rows)
        {
            CheckBox chckheader = (CheckBox)gvorder.HeaderRow.FindControl("chkHeader");
            TextBox discription = (TextBox)row.FindControl("txtDescription");
            TextBox HSN = (TextBox)row.FindControl("lblHSN");
            TextBox txtqty = (TextBox)row.FindControl("lblQty");
            TextBox UOM = (TextBox)row.FindControl("txtUOM");
            TextBox rate = (TextBox)row.FindControl("lblRate");
            Label Amount = (Label)row.FindControl("lblAmount");
            TextBox cgst = (TextBox)row.FindControl("lblCGSTPer");
            TextBox sgst = (TextBox)row.FindControl("lblSGSTPer");
            TextBox Igst = (TextBox)row.FindControl("lblIGSTPer");
            TextBox discount = (TextBox)row.FindControl("txtdiscount");
            Label grandtotal = (Label)row.FindControl("txtGrandTotal");
            CheckBox chkrow = (CheckBox)row.FindControl("chkRow");
            Label IGlblSgstAmt = (Label)row.FindControl("lblSgstAmt");
            Label IGlblcgstAmt = (Label)row.FindControl("lblCgstAmt");
            Label IGstammount = (Label)row.FindControl("lblIGSTAmt");

            if (chckheader.Checked == true)
            {
                chkrow.Checked = true;
                discription.Enabled = true;
                HSN.Enabled = true;
                txtqty.Enabled = true;
                UOM.Enabled = true;
                rate.Enabled = true;
                Amount.Enabled = true;
                cgst.Enabled = true;
                sgst.Enabled = true;
                Igst.Enabled = true;
                discount.Enabled = true;

                Double qty1 = Convert.ToDouble(txtqty.Text);
                Double price1 = Convert.ToDouble(rate.Text);
                Double CGSTamt = 0;
                Double SGSTamt = 0;
                Double IGSTamt = 0;
                Double GSTtotal = 0;

                if (cgst.Text != "" && Igst.Text == "0")
                {
                    // No GST
                    Amount.Text = (qty1 * price1).ToString();
                    totalAmount += qty1 * price1;
                    Grandtotal += qty1 * price1;
                }
                else
                {
                    if (discount.Text != "0")
                    {
                        double discountValue = Convert.ToDouble(discount.Text.Trim());
                        double total = qty1 * price1;
                        var disc = total * (Convert.ToDouble(discount.Text.Trim())) / 100;
                        var TotalamoountafterDisc = (total - disc);

                        if (Igst.Text != "0")
                        {
                            IGSTamt = TotalamoountafterDisc * (Convert.ToDouble(Igst.Text.Trim())) / 100;
                            total -= total * (discountValue / 100);
                            Amount.Text = total.ToString();
                            totalAmount += total;
                            Grandtotal += total + IGSTamt;
                        }
                        if (cgst.Text == "0")
                        {
                            CGSTamt = TotalamoountafterDisc * (Convert.ToDouble(cgst.Text.Trim())) / 100;
                            total -= total * (discountValue / 100);
                            Amount.Text = total.ToString();
                            totalAmount += total;
                            Grandtotal += total + CGSTamt+ CGSTamt;
                        }
                    }
                    else
                    {
                        double total = qty1 * price1;

                        if (Igst.Text != "0")
                        {
                            IGSTamt = total * (Convert.ToDouble(Igst.Text.Trim())) / 100;
                            Amount.Text = total.ToString();
                            totalAmount += total;
                            Grandtotal += total + IGSTamt;
                        }
                        if (cgst.Text == "0")
                        {
                            CGSTamt = total * (Convert.ToDouble(cgst.Text.Trim())) / 100;
                            Amount.Text = total.ToString();
                            totalAmount += total;
                            Grandtotal += total + CGSTamt+ CGSTamt;
                        }
                    }
                }
            }
        }

        txtGrandTot.Text = Grandtotal.ToString();
        sumofAmount.Text = totalAmount.ToString();



    }



    protected void txtTCSPer_TextChanged1(object sender, EventArgs e)
    {
        if (txtTCSPer.Text == "0" || txtTCSPer.Text == "")
        {
            var tot = Convert.ToDouble(sumofAmount.Text) + Convert.ToDouble(txtCost.Text);
            var TcsAmt = Convert.ToDouble(txtTCSPer.Text) * tot / 100;
            txtTCSAmt.Text = TcsAmt.ToString("##.00");

            var grandtot = Convert.ToDouble(txtTCSAmt.Text) + Convert.ToDouble(hdnGrandtotal.Value) + Convert.ToDouble(txtCost.Text);
            txtGrandTot.Text = grandtot.ToString("##.00");
            txtTCSAmt.Text = "0";
        }
        else
        {
            var tot = Convert.ToDouble(sumofAmount.Text) + Convert.ToDouble(txtCost.Text);
            var TcsAmt = Convert.ToDouble(txtTCSPer.Text) * tot / 100;
            txtTCSAmt.Text = TcsAmt.ToString("##.00");

            var grandtot = Convert.ToDouble(txtTCSAmt.Text) + Convert.ToDouble(hdnGrandtotal.Value) + Convert.ToDouble(txtCost.Text);
            txtGrandTot.Text = grandtot.ToString("##.00");
        }
    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        SaveRecord();
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProformaInvoice.aspx");
    }
    protected void ddlType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string type = ddlType.SelectedItem.Text;
            if (type == "To pay")
            {
                txtDescription.Text = "Freight- To pay";
            }
            else if (type == "Paid")
            {
                txtDescription.Text = "Freight- Paid";
            }
            else if (type == "Included In Price")
            {
                txtDescription.Text = "Freight- Included In Price";
            }
            else if (type == "Specify")
            {
                txtDescription.Text = "Freight";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void GRID_GST_Calculation1(GridViewRow row)
    {
        try
        {
            TextBox txt_Qty = (TextBox)row.FindControl("txtQty");
            TextBox txt_price = (TextBox)row.FindControl("txtrate");
            TextBox txt_CGST = (TextBox)row.FindControl("txtCGSTPer");
            TextBox txt_SGST = (TextBox)row.FindControl("txtSGSTPer");
            TextBox txt_IGST = (TextBox)row.FindControl("txtIGSTPer");
            Label txt_amount = (Label)row.FindControl("txtGrandTotal");
            Label taxableamt = (Label)row.FindControl("lblAmount");
            Label IGstammount = (Label)row.FindControl("lblIGSTAmt");
            TextBox txt_discount = (TextBox)row.FindControl("txtdiscountedit");
            Label IGlblSgstAmt = (Label)row.FindControl("lblSgstAmt");
            Label IGlblcgstAmt = (Label)row.FindControl("lblCgstAmt");

            var totalamt = Convert.ToDouble(txt_Qty.Text.Trim()) * Convert.ToDouble(txt_price.Text.Trim());
            var NetAmt = totalamt;
            Double CGSTamt = 0;
            Double SGSTamt = 0;
            Double IGSTamt = 0;
            Double GSTtotal = 0;

            Double AmtWithDiscount;
            if (txt_discount.Text != "0")
            {
                var disc = NetAmt * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;

                AmtWithDiscount = NetAmt - disc;
                var TotalamoountafterDisc = (totalamt - disc);

                if (txt_IGST.Text == "18")
                {
                    IGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;
                    GSTtotal = IGSTamt;
                    NetAmt = TotalamoountafterDisc + GSTtotal;
                    txt_amount.Text = NetAmt.ToString("##.00");
                    taxableamt.Text = TotalamoountafterDisc.ToString();
                    string IGSTamt1 = Convert.ToString(IGSTamt);
                    IGstammount.Text = IGSTamt1; // Formatting IGSTamt to display with two decimal places

                    return;
                }

                else
                {
                    CGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
                    SGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
                    GSTtotal = CGSTamt + SGSTamt;
                    NetAmt = TotalamoountafterDisc + GSTtotal;
                    txt_amount.Text = NetAmt.ToString("##.00");
                    taxableamt.Text = TotalamoountafterDisc.ToString();
                    string CGSTamt1 = Convert.ToString(CGSTamt);
                    IGlblSgstAmt.Text = CGSTamt1;
                    IGlblcgstAmt.Text = CGSTamt1;
                    return;
                }
            }
            else
            {
                AmtWithDiscount = 0;
                var disc = NetAmt * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;

                AmtWithDiscount = NetAmt - disc;
                var TotalamoountafterDisc = (totalamt - disc);

                if (txt_IGST.Text == "18")
                {
                    IGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;
                    GSTtotal = IGSTamt;
                    NetAmt = TotalamoountafterDisc + GSTtotal;
                    txt_amount.Text = NetAmt.ToString("##.00");
                    taxableamt.Text = TotalamoountafterDisc.ToString();
                    string IGSTamt1 = Convert.ToString(IGSTamt);
                    IGstammount.Text = IGSTamt1; // Formatting IGSTamt to display with two decimal places

                    return;
                }

                else
                {
                    CGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
                    SGSTamt = TotalamoountafterDisc * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
                    GSTtotal = CGSTamt + SGSTamt;
                    NetAmt = TotalamoountafterDisc + GSTtotal;
                    txt_amount.Text = NetAmt.ToString("##.00");
                    taxableamt.Text = TotalamoountafterDisc.ToString();
                    string CGSTamt1 = Convert.ToString(CGSTamt);
                    IGlblSgstAmt.Text = CGSTamt1;
                    IGlblcgstAmt.Text = CGSTamt1;
                    return;
                }


            }




        }
        catch (Exception ex)
        {

            throw;
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
                        CGSTPer.Text = Dt.Rows[0]["CGST"].ToString();
                        SGSTPer.Text = Dt.Rows[0]["SGST"].ToString();
                        IGSTPer.Text = Dt.Rows[0]["IGST"].ToString();

                       // CGSTPertcs.Text = Dt.Rows[0]["CGST"].ToString();
                        //SGSTPertcs.Text = Dt.Rows[0]["SGST"].ToString();
                        //IGSTPertcs.Text = Dt.Rows[0]["IGST"].ToString();
                        CGSTPertcs.Text = "0";
                        SGSTPertcs.Text = "0";
                        IGSTPertcs.Text = "0";


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


}
