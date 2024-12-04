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
using System.Text.RegularExpressions;

public partial class Admin_AddCompany : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Getorderlist();
            //fillddlpaymentterm();
            UpdateHistorymsg = string.Empty; ComCode = string.Empty; ComCodeUpdate = string.Empty; visitingcardPath = string.Empty; regdate = string.Empty;
            if (Request.QueryString["code"] != null)
            {
                string ID = Decrypt(Request.QueryString["code"].ToString());
                hiddenid.Value = ID.ToString();
                ViewState["UpdateRowId"] = Decrypt(Request.QueryString["code"].ToString());
                GetCompanyData(ViewState["UpdateRowId"].ToString());
            }
        }
    }

    //private void fillddlpaymentterm()
    //{
    //    SqlDataAdapter adpt = new SqlDataAdapter("select distinct paymentterm from QuotationMainFooter", con);
    //    DataTable dtpt = new DataTable();
    //    adpt.Fill(dtpt);

    //    if (dtpt.Rows.Count > 0)
    //    {
    //        dtpt.Rows.Add("Specify");
    //        ddlpaymentterm.DataSource = dtpt;
    //        ddlpaymentterm.DataValueField = "paymentterm";
    //        ddlpaymentterm.DataTextField = "paymentterm";
    //        ddlpaymentterm.DataBind();
    //    }
    //}
    private void fillddlCountryCode()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select * from tblCountryCode", con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);

        if (dtpt.Rows.Count > 0)
        {
            ddlCountryCode.DataSource = dtpt;
            ddlCountryCode.DataValueField = "CountryCode";
            ddlCountryCode.DataTextField = "CountryName";
            ddlCountryCode.DataBind();
            ddlCountryCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
    }

    static string regdate = string.Empty;
    protected void GetCompanyData(string id)
    {
        string query1 = string.Empty;
        query1 = @"SELECT * FROM [Company] where id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ComCodeUpdate = dt.Rows[0]["ccode"].ToString();
            txtcname.Text = dt.Rows[0]["cname"].ToString();
            txtownname1.Text = dt.Rows[0]["oname1"].ToString();

            txtownname2.Text = dt.Rows[0]["oname2"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname2"].ToString()))
            {
                row2.Visible = true;
                btnadd2.Visible = false;
                btnadd3.Visible = true;
                btnadd4.Visible = false;
                btnadd5.Visible = false;
            }
            txtownname3.Text = dt.Rows[0]["oname3"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname3"].ToString()))
            {
                row3.Visible = true;
                btnadd2.Visible = false;
                btnadd3.Visible = false;
                btnadd4.Visible = true;
                btnadd5.Visible = false;
            }
            txtownname4.Text = dt.Rows[0]["oname4"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname4"].ToString()))
            {
                row4.Visible = true;
                btnadd2.Visible = false;
                btnadd3.Visible = false;
                btnadd4.Visible = false;
                btnadd5.Visible = true;
            }
            txtownname5.Text = dt.Rows[0]["oname5"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["oname5"].ToString()))
            {
                row5.Visible = true;
                btnadd2.Visible = false;
                btnadd3.Visible = false;
                btnadd4.Visible = false;
                btnadd5.Visible = false;
            }

            txtemail1.Text = dt.Rows[0]["email1"].ToString();
            txtemail2.Text = dt.Rows[0]["email2"].ToString();
            txtemail3.Text = dt.Rows[0]["email3"].ToString();
            txtemail4.Text = dt.Rows[0]["email4"].ToString();
            txtemail5.Text = dt.Rows[0]["email5"].ToString();

            txtmobile1.Text = dt.Rows[0]["mobile1"].ToString();
            txtmobile2.Text = dt.Rows[0]["mobile2"].ToString();
            txtmobile3.Text = dt.Rows[0]["mobile3"].ToString();
            txtmobile4.Text = dt.Rows[0]["mobile4"].ToString();
            txtmobile5.Text = dt.Rows[0]["mobile5"].ToString();

            txtdesig1.Text = dt.Rows[0]["desig1"].ToString();
            txtdesig2.Text = dt.Rows[0]["desig2"].ToString();
            txtdesig3.Text = dt.Rows[0]["desig3"].ToString();
            txtdesig4.Text = dt.Rows[0]["desig4"].ToString();
            txtdesig5.Text = dt.Rows[0]["desig5"].ToString();

            txtbillingaddress.Text = dt.Rows[0]["billingaddress"].ToString();
            txtshippingaddress.Text = dt.Rows[0]["shippingaddress"].ToString();

            DetailsBilling.Text = dt.Rows[0]["DetailsBillingAddress"].ToString();
            DetailsShipping.Text = dt.Rows[0]["DetailsShippingAddress"].ToString();


            txtgstno.Text = dt.Rows[0]["gstno"].ToString();
            ddlpaymentterm.Text = dt.Rows[0]["paymentterm1"].ToString();

            ddlTypeofSupply.SelectedItem.Text = dt.Rows[0]["E_inv_Typeof_supply"].ToString();
            fillddlCountryCode();
            if (dt.Rows[0]["E_inv_Typeof_supply"].ToString() == "EXPWOP")
            {
                DivCountryCode.Visible = true;
                Brtag.Visible = true;
                ddlCountryCode.SelectedValue = dt.Rows[0]["CountryCode"].ToString();
            }
            else
            {
                DivCountryCode.Visible = false;
                Brtag.Visible = false;
            }
            txtbillinglocation.Text = dt.Rows[0]["Billing_location"].ToString();
            txtbillingpin.Text = dt.Rows[0]["Billing_pincode"].ToString();
            txtbillingState.Text = dt.Rows[0]["Billing_statecode"].ToString();
            txtshippinglocation.Text = dt.Rows[0]["Shipping_location"].ToString();
            txtshippingpin.Text = dt.Rows[0]["Shipping_pincode"].ToString();
            txtshippingstate.Text = dt.Rows[0]["Shipping_statecode"].ToString();

            //txtpayment1.Text = dt.Rows[0]["paymentterm1"].ToString();
            //if (!string.IsNullOrEmpty(dt.Rows[0]["paymentterm1"].ToString()))
            //{
            //    paym2.Visible = false; paym3.Visible = false; paym4.Visible = false; paym5.Visible = false;
            //}
            //txtpayment2.Text = dt.Rows[0]["paymentterm2"].ToString();
            //if (!string.IsNullOrEmpty(dt.Rows[0]["paymentterm2"].ToString()))
            //{
            //    paym2.Visible = true; paym3.Visible = false; paym4.Visible = false; paym5.Visible = false;
            //    btnpayment2.Visible = false;
            //}
            //txtpayment3.Text = dt.Rows[0]["paymentterm3"].ToString();
            //if (!string.IsNullOrEmpty(dt.Rows[0]["paymentterm3"].ToString()))
            //{
            //    paym2.Visible = true; paym3.Visible = true; paym4.Visible = false;
            //    paym5.Visible = false; btnpayment3.Visible = false;
            //}
            //txtpayment4.Text = dt.Rows[0]["paymentterm4"].ToString();
            //if (!string.IsNullOrEmpty(dt.Rows[0]["paymentterm4"].ToString()))
            //{
            //    paym2.Visible = true; paym3.Visible = true; paym4.Visible = true;
            //    paym5.Visible = false; btnpayment4.Visible = false;
            //}
            //txtpayment5.Text = dt.Rows[0]["paymentterm5"].ToString();
            //if (!string.IsNullOrEmpty(dt.Rows[0]["paymentterm5"].ToString()))
            //{
            //    paym2.Visible = true; paym3.Visible = true; paym4.Visible = true;
            //    paym5.Visible = true; btnpayment5.Visible = false;
            //}

            txtcredit.Text = dt.Rows[0]["creditlimit"].ToString();

            btnadd.Text = "Update";

            /// Fill hidden fields for maintaining history
            ComCodeUpdate = dt.Rows[0]["ccode"].ToString();
            //HFcname.Value = dt.Rows[0]["cname"].ToString();
            //HFoname.Value = dt.Rows[0]["oname"].ToString();
            //HFemail.Value = dt.Rows[0]["email"].ToString();
            //HFmobile.Value = dt.Rows[0]["mobile"].ToString();
            //HFvisitingcard.Value = dt.Rows[0]["visitingcard"].ToString();
            //HFaddress.Value = dt.Rows[0]["address"].ToString();
            //HFvisitdate.Value = dt.Rows[0]["visitdate"].ToString();
            //HFbde.Value = dt.Rows[0]["BDE"].ToString();
            //hfwebsite.Value = dt.Rows[0]["website"].ToString();
            //HFclienttype.Value = dt.Rows[0]["type"].ToString();
            hfregby.Value = dt.Rows[0]["sessionname"].ToString();
        }
    }
    static string UpdateHistorymsg = string.Empty;
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
    protected void GenerateComCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [Company]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
            ComCode = "ECL/0" + (maxid + 1).ToString();
        }
        else
        {
            ComCode = string.Empty;
        }
    }

    static string ComCodeUpdate = string.Empty; static string visitingcardPath = string.Empty;
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string paymentterm = "";
        if (ddlpaymentterm.SelectedItem.Text != "Specify")
        {
            paymentterm = ddlpaymentterm.SelectedItem.Text;
        }
        if (ddlpaymentterm.SelectedItem.Text == "Specify")
        {
            paymentterm = txtpaymentterm.Text;
        }

        SqlCommand cmdfooter = new SqlCommand("SP_InsertQuotationMainFooter", con);
        cmdfooter.CommandType = CommandType.StoredProcedure;
        cmdfooter.Parameters.AddWithValue("@paymentterm", paymentterm);
        con.Open();
        cmdfooter.ExecuteNonQuery();
        con.Close();

        #region Insert
        if (btnadd.Text == "Add Customer")
        {
            GenerateComCode();
            if (!string.IsNullOrEmpty(ComCode))
            {
                if (ddlTypeofSupply.SelectedItem.Text == "" || ddlTypeofSupply.SelectedItem.Value == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Kindly Select Company Supply Type !!');", true);
                }
                else
                {
                    string input_billingaddress = txtbillingaddress.Text;
                    string input_Shippingaddress = txtshippingaddress.Text;
                    if (input_billingaddress.Length <= 100 && input_Shippingaddress.Length <= 100)
                    {
                        SqlCommand cmd = new SqlCommand("SP_Company", con);
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Action", "Insert");
                        cmd.Parameters.AddWithValue("@ccode", ComCode);
                        cmd.Parameters.AddWithValue("@cname", txtcname.Text.Trim());

                        cmd.Parameters.AddWithValue("@oname1", txtownname1.Text.Trim());
                        cmd.Parameters.AddWithValue("@oname2", txtownname2.Text.Trim());
                        cmd.Parameters.AddWithValue("@oname3", txtownname3.Text.Trim());
                        cmd.Parameters.AddWithValue("@oname4", txtownname4.Text.Trim());
                        cmd.Parameters.AddWithValue("@oname5", txtownname5.Text.Trim());

                        cmd.Parameters.AddWithValue("@email1", txtemail1.Text.Trim());
                        cmd.Parameters.AddWithValue("@email2", txtemail2.Text.Trim());
                        cmd.Parameters.AddWithValue("@email3", txtemail3.Text.Trim());
                        cmd.Parameters.AddWithValue("@email4", txtemail4.Text.Trim());
                        cmd.Parameters.AddWithValue("@email5", txtemail5.Text.Trim());

                        cmd.Parameters.AddWithValue("@mobile1", txtmobile1.Text.Trim());
                        cmd.Parameters.AddWithValue("@mobile2", txtmobile2.Text.Trim());
                        cmd.Parameters.AddWithValue("@mobile3", txtmobile3.Text.Trim());
                        cmd.Parameters.AddWithValue("@mobile4", txtmobile4.Text.Trim());
                        cmd.Parameters.AddWithValue("@mobile5", txtmobile5.Text.Trim());

                        cmd.Parameters.AddWithValue("@desig1", txtdesig1.Text.Trim());
                        cmd.Parameters.AddWithValue("@desig2", txtdesig2.Text.Trim());
                        cmd.Parameters.AddWithValue("@desig3", txtdesig3.Text.Trim());
                        cmd.Parameters.AddWithValue("@desig4", txtdesig4.Text.Trim());
                        cmd.Parameters.AddWithValue("@desig5", txtdesig5.Text.Trim());

                        cmd.Parameters.AddWithValue("@paymentterm1", paymentterm);
                        cmd.Parameters.AddWithValue("@creditlimit", txtcredit.Text.Trim());
                        cmd.Parameters.AddWithValue("@billingaddress", txtbillingaddress.Text);
                        cmd.Parameters.AddWithValue("@shippingaddress", txtshippingaddress.Text);

                        cmd.Parameters.AddWithValue("@Detaislbillingaddress", DetailsBilling.Text);
                        cmd.Parameters.AddWithValue("@Detailsshippingaddress", DetailsShipping.Text);

                        cmd.Parameters.AddWithValue("@sessionname", Session["name"].ToString());
                        cmd.Parameters.AddWithValue("@gstno", txtgstno.Text.Trim());
                        cmd.Parameters.AddWithValue("@updatedby", Session["name"].ToString());
                        //New Changes 12/8/23
                        cmd.Parameters.AddWithValue("@Billing_location", txtbillinglocation.Text);
                        cmd.Parameters.AddWithValue("@Billing_pincode", txtbillingpin.Text.Trim());
                        cmd.Parameters.AddWithValue("@Billing_statecode", txtbillingState.Text.Trim());
                        cmd.Parameters.AddWithValue("@Shipping_location", txtshippinglocation.Text);
                        cmd.Parameters.AddWithValue("@Shipping_pincode", txtshippingpin.Text.Trim());
                        cmd.Parameters.AddWithValue("@Shipping_statecode", txtshippingstate.Text.Trim());
                        cmd.Parameters.AddWithValue("@E_inv_Typeof_supply", ddlTypeofSupply.SelectedItem.Text);
                        if (ddlTypeofSupply.SelectedItem.Text == "EXPWOP")
                        {
                            cmd.Parameters.AddWithValue("@CountryCode", ddlCountryCode.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CountryCode", "");
                        }
                        int a = 0;
                        //cmd.Connection.Open();
                        con.Open();
                        a = cmd.ExecuteNonQuery();
                        con.Close();
                        //cmd.Connection.Close();

                        if (a > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Saved Sucessfully');window.location='AllCompanyList.aspx';", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Saved !!');", true);
                        }
                    }
                    else
                    {
                        if (input_billingaddress.Length >= 100)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Billing Address cannot exceed 100 characters. !!');", true);
                        }
                        else if (input_Shippingaddress.Length >= 100)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Shipping Address cannot exceed 100 characters. !!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Addresses cannot exceed 100 characters. !!');", true);
                        }
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Employee Code Generation Problem Please Try Again !!');", true);
            }
        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update")
        {

            string input_billingaddress = txtbillingaddress.Text;
            string input_Shippingaddress = txtshippingaddress.Text;
            if (input_billingaddress.Length <= 100 && input_Shippingaddress.Length <= 100)
            {
                SqlCommand cmd = new SqlCommand("SP_Company", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Update");
                //cmd.Parameters.AddWithValue("@id", ViewState["UpdateRowId"].ToString());
                cmd.Parameters.AddWithValue("@id", hiddenid.Value);
                cmd.Parameters.AddWithValue("@ccode", ComCodeUpdate);
                cmd.Parameters.AddWithValue("@cname", txtcname.Text.Trim());

                cmd.Parameters.AddWithValue("@oname1", txtownname1.Text.Trim());
                cmd.Parameters.AddWithValue("@oname2", txtownname2.Text.Trim());
                cmd.Parameters.AddWithValue("@oname3", txtownname3.Text.Trim());
                cmd.Parameters.AddWithValue("@oname4", txtownname4.Text.Trim());
                cmd.Parameters.AddWithValue("@oname5", txtownname5.Text.Trim());

                cmd.Parameters.AddWithValue("@email1", txtemail1.Text.Trim());
                cmd.Parameters.AddWithValue("@email2", txtemail2.Text.Trim());
                cmd.Parameters.AddWithValue("@email3", txtemail3.Text.Trim());
                cmd.Parameters.AddWithValue("@email4", txtemail4.Text.Trim());
                cmd.Parameters.AddWithValue("@email5", txtemail5.Text.Trim());

                cmd.Parameters.AddWithValue("@mobile1", txtmobile1.Text.Trim());
                cmd.Parameters.AddWithValue("@mobile2", txtmobile2.Text.Trim());
                cmd.Parameters.AddWithValue("@mobile3", txtmobile3.Text.Trim());
                cmd.Parameters.AddWithValue("@mobile4", txtmobile4.Text.Trim());
                cmd.Parameters.AddWithValue("@mobile5", txtmobile5.Text.Trim());

                cmd.Parameters.AddWithValue("@desig1", txtdesig1.Text.Trim());
                cmd.Parameters.AddWithValue("@desig2", txtdesig2.Text.Trim());
                cmd.Parameters.AddWithValue("@desig3", txtdesig3.Text.Trim());
                cmd.Parameters.AddWithValue("@desig4", txtdesig4.Text.Trim());
                cmd.Parameters.AddWithValue("@desig5", txtdesig5.Text.Trim());

                cmd.Parameters.AddWithValue("@paymentterm1", paymentterm);
                cmd.Parameters.AddWithValue("@creditlimit", txtcredit.Text.Trim());
                cmd.Parameters.AddWithValue("@billingaddress", txtbillingaddress.Text);
                cmd.Parameters.AddWithValue("@shippingaddress", txtshippingaddress.Text);

                cmd.Parameters.AddWithValue("@Detaislbillingaddress", DetailsBilling.Text);
                cmd.Parameters.AddWithValue("@Detailsshippingaddress", DetailsShipping.Text);


                cmd.Parameters.AddWithValue("@sessionname", Session["name"].ToString());
                cmd.Parameters.AddWithValue("@gstno", txtgstno.Text.Trim());
                cmd.Parameters.AddWithValue("@updatedby", Session["name"].ToString());

                //New Changes 12/8/23
                cmd.Parameters.AddWithValue("@Billing_location", txtbillinglocation.Text);
                cmd.Parameters.AddWithValue("@Billing_pincode", txtbillingpin.Text.Trim());
                cmd.Parameters.AddWithValue("@Billing_statecode", txtbillingState.Text.Trim());
                cmd.Parameters.AddWithValue("@Shipping_location", txtshippinglocation.Text);
                cmd.Parameters.AddWithValue("@Shipping_pincode", txtshippingpin.Text.Trim());
                cmd.Parameters.AddWithValue("@Shipping_statecode", txtshippingstate.Text.Trim());
                cmd.Parameters.AddWithValue("@E_inv_Typeof_supply", ddlTypeofSupply.SelectedItem.Text);

                if (ddlTypeofSupply.SelectedItem.Text == "EXPWOP")
                {
                    cmd.Parameters.AddWithValue("@CountryCode", ddlCountryCode.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CountryCode", "");
                }

                int a = 0;
                cmd.Connection.Open();
                a = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                if (a > 0)
                {
                    //CreateHistory();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Updated Sucessfully');window.location='AllCompanyList.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Updated !!');", true);
                }
            }
            else
            {
                if (input_billingaddress.Length >= 100)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Billing Address cannot exceed 100 characters. !!');", true);
                }
                else if (input_Shippingaddress.Length >= 100)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Shipping Address cannot exceed 100 characters. !!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Addresses cannot exceed 100 characters. !!');", true);
                }
            }
        }
        #endregion Update
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCompany.aspx");
    }
    protected void txtcname_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],[cname] FROM [Company] where [isdeleted]=0 and status=0 and cname='" + txtcname.Text.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "customer Already Existing !!";
            btnadd.Enabled = false;
        }
        else
        {
            btnadd.Enabled = true;
            lblmsg.Visible = false;
        }
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

    protected void btnadd2_Click(object sender, EventArgs e)
    {
        row2.Visible = true; btnadd2.Visible = false; btnadd3.Visible = true;
    }

    protected void btnadd3_Click(object sender, EventArgs e)
    {
        row3.Visible = true; btnadd3.Visible = false; btnadd4.Visible = true;
    }
    protected void btnadd4_Click(object sender, EventArgs e)
    {
        row4.Visible = true; btnadd4.Visible = false; btnadd5.Visible = true;
    }

    protected void btnadd5_Click(object sender, EventArgs e)
    {
        row5.Visible = true; btnadd5.Visible = false; //btnadd6.Visible = true;
    }

    //protected void btnpayment2_Click(object sender, EventArgs e)
    //{
    //    paym2.Visible = true;
    //    btnpayment2.Visible = false;
    //}
    //protected void btnpayment3_Click(object sender, EventArgs e)
    //{
    //    paym3.Visible = true;
    //    btnpayment3.Visible = false;
    //}
    //protected void btnpayment4_Click(object sender, EventArgs e)
    //{
    //    paym4.Visible = true;
    //    btnpayment4.Visible = false;
    //}
    //protected void btnpayment5_Click(object sender, EventArgs e)
    //{
    //    paym5.Visible = true;
    //    btnpayment5.Visible = false;
    //}

    protected void ddlpaymentterm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpaymentterm.SelectedItem.Text == "Specify")
        {
            txtpaymentterm.Visible = true;
        }
        if (ddlpaymentterm.SelectedItem.Text != "Specify")
        {
            txtpaymentterm.Visible = false;
        }
    }

    protected void ddlTypeofSupply_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTypeofSupply.SelectedItem.Text == "EXPWOP")
            {
                fillddlCountryCode();
                txtgstno.Text = "URP"; txtgstno.Enabled = false;
                txtbillingpin.Text = "999999"; txtbillingState.Text = "96"; txtbillingpin.Enabled = false; txtbillingState.Enabled = false;
                txtshippingpin.Text = "999999"; txtshippingstate.Text = "96"; txtshippingpin.Enabled = false; txtshippingstate.Enabled = false;
                DivCountryCode.Visible = true;
                Brtag.Visible = true;
            }
            else
            {
                txtgstno.Text = ""; txtgstno.Enabled = true;
                txtbillingpin.Text = ""; txtbillingState.Text = ""; txtbillingpin.Enabled = true; txtbillingState.Enabled = true;
                txtshippingpin.Text = ""; txtshippingstate.Text = ""; txtshippingpin.Enabled = true; txtshippingstate.Enabled = true;
                DivCountryCode.Visible = false;
                Brtag.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }

    protected void checkforcopy_CheckedChanged(object sender, EventArgs e)
    {
        DetailsBilling.Text = txtbillingaddress.Text;
        DetailsShipping.Text = DetailsBilling.Text;

        txtbillingaddress.Text = DetailsBilling.Text;
        txtshippingaddress.Text = DetailsShipping.Text;
         String A =  txtbillinglocation.Text;
         txtshippinglocation.Text = A;
        string B = txtbillingpin.Text;
        txtshippingpin.Text = B;
        string c = txtbillingState.Text;
        txtshippingstate.Text = c;
    }

    protected void txtgstno_TextChanged(object sender, EventArgs e)
    {
        //string GstNo = txtgstno.Text;

        //string firstTwoDigits = string.Empty;

        //if (!string.IsNullOrEmpty(GstNo))
        //{
        //    firstTwoDigits = GstNo.Substring(0, Math.Min(3, GstNo.Length));
        //}

        //// Later in your code...
        //txtbillingState.Text = firstTwoDigits;


        string GstNo = txtgstno.Text;

        if (ddlTypeofSupply.Text != "EXPWOP")
        {
            if (!string.IsNullOrEmpty(GstNo))
            {
                if (GstNo.Length < 15)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Invalid GST number. Please enter a 15-digit GST number. !!');", true);
                    txtgstno.Text = "";
                    return;
                }

                string firstTwoDigits = GstNo.Substring(0, 2);

                // Check if the first two digits are valid state codes
                string[] validStateCodes = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36" };
                if (!validStateCodes.Contains(firstTwoDigits))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Invalid GST number. Please enter a valid state code. !!');", true);
                    txtgstno.Text = "";
                    return;
                }

                txtbillingState.Text = firstTwoDigits;

                // Additional validation for GST number pattern
                string pattern = @"^\d{2}[A-Z]{5}\d{4}[A-Z]{1}\d[Z]{1}[A-Z\d]{1}$";
                Regex regex = new Regex(pattern);

                if (!regex.IsMatch(GstNo))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Invalid GST number. Please enter a valid GST number. !!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please enter a GST number. !!');", true);
                txtgstno.Text = "";
                return;
            }
        }



        //string GstNo = txtgstno.Text;

        //string firstTwoDigits = string.Empty;

        //if (!string.IsNullOrEmpty(GstNo))
        //{
        //    if (GstNo.Length < 15)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Invalid GST number. Please enter a 15-digit GST number. !!');", true);
        //        txtgstno.Text = "";

        //        return;
        //    }

        //    firstTwoDigits = GstNo.Substring(0, 2);

        //    // Check if the first two digits are valid state codes
        //    string[] validStateCodes = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36" };
        //    if (!validStateCodes.Contains(firstTwoDigits))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Invalid GST number. Please enter a valid state code. !!');", true);
        //        txtgstno.Text = "";
        //        return;
        //    }

        //    txtbillingState.Text = firstTwoDigits;
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please enter a GST number. !!');", true);
        //    txtgstno.Text = "";
        //    return;
        //}


    }

    public void Getorderlist()

    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("[SP_PaymnetTerms]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetList"));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        ddlpaymentterm.AppendDataBoundItems = true;
                        ddlpaymentterm.Items.Add(new ListItem("Select", ""));
                        ddlpaymentterm.DataSource = Dt;
                        ddlpaymentterm.DataValueField = "Terms";
                        ddlpaymentterm.DataTextField = "Terms";
                        ddlpaymentterm.DataBind();

                    }
                    else
                    {
                     
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