#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\PaymentModule.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "76965DC3CBB7B72CC0D45CE3972186A5A5B439EA"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\PaymentModule.aspx.cs"
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
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

public partial class Admin_PaymentModule : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    List<string> Lstbillno = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Gvbind();
        }
    }

    private void Gvbind()
    {
        string query = string.Empty;
        query = @"select * from tblPurchaseBillHdr where IsPaid is null order by Id desc";
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvPurchaseBill.DataSource = dt;
            GvPurchaseBill.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvPurchaseBill.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            GvPurchaseBill.DataSource = null;
            GvPurchaseBill.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvPurchaseBill.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
    }

    protected void GvPurchaseBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ViewState["CompRowId"] = e.CommandArgument.ToString();
        if (e.CommandName == "RowEdit")
        {
            ViewState["id"] = e.CommandArgument.ToString();
            Response.Redirect("PurchaseBill.aspx?ID=" + encrypt(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "Suppliername")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                //GetSupplierDataPopup(e.CommandArgument.ToString());
                //this.modelprofile.Show();
            }
        }

        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();
                Response.Write("<script>window.open('PurchaseBillPDF.aspx','_blank');</script>");
            }
        }

        if (e.CommandName == "RowDelete")
        {
            con.Open();
            SqlCommand cmdget = new SqlCommand("select AgainstNumber from tblPurchaseBillHdr where Id=@Id", con);
            cmdget.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            string POno = cmdget.ExecuteScalar().ToString();

            SqlCommand cmdupdate = new SqlCommand("update tblPurchaseOrderHdr set IsClosed is null where PONo=@PONO", con);
            cmdupdate.Parameters.AddWithValue("@PONO", POno);
            cmdupdate.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand Cmd = new SqlCommand("Delete From [tblPurchaseBillHdr] WHERE Id=@Id", con);
            Cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmd.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand Cmdd = new SqlCommand("Delete From [tblPurchaseBillDtls] WHERE HeaderID=@HeaderID", con);
            Cmdd.Parameters.AddWithValue("@HeaderID", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmdd.ExecuteNonQuery();
            con.Close();


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Deleted Sucessfully');window.location.href='PurchaseBillList.aspx';", true);
        }


    }

    private void GetSupplierDataPopup(string id)
    {
        string query1 = string.Empty;
        query1 = @"select * from tblSupplierMaster where Id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblSname.Text = dt.Rows[0]["SupplierName"].ToString();
            lblemail.Text = dt.Rows[0]["EmailID"].ToString();
            // txtsupplierCode.Text = dt.Rows[0]["SupplierCode"].ToString();
            lblbillingaddress.Text = dt.Rows[0]["BillToAddress"].ToString();
            lblshipaddress.Text = dt.Rows[0]["ShipToAddress"].ToString();
            lblcountry.Text = dt.Rows[0]["Country"].ToString();
            lblState.Text = dt.Rows[0]["StateName"].ToString();
            //l.Text = dt.Rows[0]["RegistrationType"].ToString();
            lblgstno.Text = dt.Rows[0]["GSTNo"].ToString();
            lblPan.Text = dt.Rows[0]["PANNo"].ToString();
            //txtPaymentValidity.Text = dt.Rows[0]["PaymentValidity"].ToString();
            //txtSupplierTaxtype.Text = dt.Rows[0]["SupplierTaxType"].ToString();
            //txtSupplierCategory.Text = dt.Rows[0]["SupplierCategory"].ToString();
            //txtTradeName.Text = dt.Rows[0]["TradeName"].ToString();
            //txtOutstandingLimit.Text = dt.Rows[0]["OutstandingLimit"].ToString();
            //ddlPaymentTerm.Text = dt.Rows[0]["PaymentTerm"].ToString();
            //txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
            lblregBy.Text = dt.Rows[0]["CreatedBy"].ToString();

            //getConatctdts(id);

            //btnadd.Text = "Update Supplier";
        }

        string query = string.Empty;
        query = @"select * from tblSupplierContactDtls where HeaderID='" + id + "' ";
        SqlDataAdapter ad1 = new SqlDataAdapter(query, con);
        DataTable dt1 = new DataTable();
        ad1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            dgvContactDtls.DataSource = dt1;
            dgvContactDtls.DataBind();
        }
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
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

    protected void btnShowComDetail_Click(object sender, EventArgs e)
    {
        GetSupplierDataPopup(ViewState["CompRowId"].ToString());
        this.modelprofile.Show();
    }

    #region Filter

    protected void txtcnamefilter_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtcnamefilter.Text.Trim()))
        {
            query = "SELECT * FROM tblPurchaseBillHdr where SupplierName like '" + txtcnamefilter.Text.Trim() + "%' and IsPaid is null order by Id desc";
        }
        else
        {
            query = "select * from tblPurchaseBillHdr where IsPaid is null order by Id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvPurchaseBill.DataSource = dt;
            GvPurchaseBill.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvPurchaseBill.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            GvPurchaseBill.DataSource = null;
            GvPurchaseBill.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvPurchaseBill.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
    }

    protected void txtSupplierBill_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtSupplierBill.Text.Trim()))
        {
            query = "SELECT * FROM tblPurchaseBillHdr where SupplierBillNo='" + txtSupplierBill.Text.Trim() + "' order by Id desc";
        }
        else
        {
            query = "SELECT * FROM tblPurchaseBillHdr order by Id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvPurchaseBill.DataSource = dt;
            GvPurchaseBill.DataBind();
        }
        else
        {
            GvPurchaseBill.DataSource = null;
            GvPurchaseBill.DataBind();
        }
    }
    #endregion Filter

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentModule.aspx");
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSupplierList(string prefixText, int count)
    {
        return AutoFillSupplierName(prefixText);
    }

    public static List<string> AutoFillSupplierName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [SupplierName] from [tblSupplierMaster] where " + "SupplierName like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["SupplierName"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSupplierOwnerList(string prefixText, int count)
    {
        return AutoFillSupplierOwnerName(prefixText);
    }

    public static List<string> AutoFillSupplierOwnerName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [oname1] from [Supplier] where oname1 like @Search + '%' and status=0 and [isdeleted]=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["oname1"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    private string GetMailIdOfEmpl(string Empcode)
    {
        string query1 = "SELECT [email] FROM [employees] where [empcode]='" + Empcode + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        string email = string.Empty;
        if (dt.Rows.Count > 0)
        {
            email = dt.Rows[0]["email"].ToString();
        }
        return email;
    }

    protected void ddlsalesMainfilter_TextChanged(object sender, EventArgs e)
    {
        Gvbind();
    }

    protected void btnAddEnq_Click(object sender, EventArgs e)
    {
        string Cname = ((sender as Button).CommandArgument).ToString();
        Response.Redirect("Addenquiry.aspx?Cname=" + encrypt(Cname));
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid=" + id + "','','width=700px,height=600px');", true);
    }

    protected void linkbtnfile_Click(object sender, EventArgs e)
    {
        string id = encrypt(((sender as ImageButton).CommandArgument).ToString());

        string POID = Decrypt(id);

        string strQuery = "select SupplierName,RefDocument from tblPurchaseBillHdr where Id=@id";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(POID);
        DataTable dt = GetData(cmd);
        if (dt != null)
        {
            download(dt);
        }
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid=" + id + "&SN=1','','width=700px,height=600px');", true);
    }

    private DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlDataAdapter sda = new SqlDataAdapter();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }
        catch
        {
            return null;
        }
        finally
        {
            con.Close();
            sda.Dispose();
            con.Dispose();
        }
    }

    private void download(DataTable dt)
    {
        if (dt.Rows[0]["RefDocument"].ToString() != "")
        {
            Byte[] bytes = (Byte[])dt.Rows[0]["RefDocument"];
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = dt.Rows[0]["ContentType"].ToString();
            Response.AddHeader("content-disposition", "attachment;filename=" + dt.Rows[0]["SupplierName"].ToString());
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('File Not Found !!');", true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow row in GvPurchaseBill.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                if (chk != null & chk.Checked)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                int cnt = 0;
                foreach (GridViewRow g1 in GvPurchaseBill.Rows)
                {
                    bool chk = (g1.FindControl("chkSelect") as CheckBox).Checked;
                    string BillNo = (g1.FindControl("lblBillNo") as Label).Text;
                    string SupplierName = (g1.FindControl("linksname") as LinkButton).Text;
                    string SupBillNo = (g1.FindControl("lblSupBillNo") as Label).Text;
                    string BillDate = (g1.FindControl("lblBillDate") as Label).Text;
                    string GrandTotal = (g1.FindControl("lblGrandTotal") as Label).Text;
                    string RecvdValue = (g1.FindControl("txtRecvdValue") as TextBox).Text;
                    string PaidValue = (g1.FindControl("txtPaidValue") as TextBox).Text;

                    SqlCommand cmdexist = new SqlCommand("select BillNo from tblPaymentModule where BillNo='" + BillNo + "'", con);
                    con.Open();
                    string ExsitBillno = cmdexist.ExecuteScalar() == null ? "" : cmdexist.ExecuteScalar().ToString();
                    con.Close();

                    var totval = Math.Round(Convert.ToDecimal(GrandTotal));
                    var RecvdVal = Convert.ToDecimal(RecvdValue == "" ? "0" : RecvdValue);

                    bool flg = false;
                    if (totval == RecvdVal)
                    {
                        flg = true;
                    }
                    else
                    {
                        flg = false;
                    }

                    SqlCommand cmd1 = new SqlCommand("SP_PaymentModule", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@BillNo", BillNo);
                    cmd1.Parameters.AddWithValue("@SupplierName", SupplierName);
                    cmd1.Parameters.AddWithValue("@SupplierBillNo", SupBillNo);
                    cmd1.Parameters.AddWithValue("@BillDate", BillDate);
                    cmd1.Parameters.AddWithValue("@GrandTotal", GrandTotal);
                    cmd1.Parameters.AddWithValue("@PaidTotal", PaidValue);
                    cmd1.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                    con.Open();
                    if (flg == false)
                    {
                        if (chk == true)
                        {
                            cmd1.ExecuteNonQuery();
                            cnt++;
                        }
                    }
                    else
                    {
                        if (chk == true)
                        {
                            Lstbillno.Add(ExsitBillno);
                            var billn = string.Join(", ", Lstbillno);
                            lblmsg.Text = billn.ToString() + " - record is already exsist.";
                        }
                    }
                    con.Close();
                }
                if (cnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Submited Successfully...!');", true);
                    Gvbind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select at least one record...!');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    decimal TotalValue;
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GvPurchaseBill.Rows)
            {
                Label lblGrandTotal = (Label)row.FindControl("lblGrandTotal");
                TextBox txtPaidValue = (TextBox)row.FindControl("txtPaidValue");
                TextBox txtRecvdValue = (TextBox)row.FindControl("txtRecvdValue");

                CheckBox chk = (CheckBox)row.FindControl("chkSelect");

                if (chk != null & chk.Checked)
                {
                    var paidval = Math.Round(Convert.ToDecimal(lblGrandTotal.Text)) - Convert.ToDecimal(txtRecvdValue.Text == "" ? "0" : txtRecvdValue.Text);
                    TotalValue += paidval;
                    txtPaidValue.Text = Math.Round(paidval).ToString();
                    if (txtPaidValue.Text == "0")
                    {
                        txtPaidValue.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Already Paid');", true);
                    }
                    else
                    {
                        txtPaidValue.Enabled = true;
                        lblmsg.Text = "";
                    }
                }
                else
                {
                    txtPaidValue.Text = "0";
                    txtPaidValue.Enabled = false;
                }
            }
            txtGrandTotal.Text = Math.Round(TotalValue).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GvPurchaseBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
                int idd = Convert.ToInt32(GvPurchaseBill.DataKeys[e.Row.RowIndex].Values[0]);

                con.Open();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");
                Label lblBillNo = (Label)e.Row.FindControl("lblBillNo");
                TextBox txtRecvdValue = (TextBox)e.Row.FindControl("txtRecvdValue");

                SqlCommand cmdReceivedTotal = new SqlCommand("select SUM(CAST(ReceivedTotal as float)) as ReceivedTotal from tblPaymentModule where BillNo='" + lblBillNo.Text + "'", con);
                string ReceivedTotal = cmdReceivedTotal.ExecuteScalar() == null ? "0" : cmdReceivedTotal.ExecuteScalar().ToString();
                txtRecvdValue.Text = ReceivedTotal;

                con.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtPaidValue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GvPurchaseBill.Rows)
            {
                Label lblGrandTotal = (Label)row.FindControl("lblGrandTotal");
                TextBox txtPaidValue = (TextBox)row.FindControl("txtPaidValue");
                TextBox txtRecvdValue = (TextBox)row.FindControl("txtRecvdValue");

                CheckBox chk = (CheckBox)row.FindControl("chkSelect");

                if (chk != null & chk.Checked)
                {
                    var paidval = Math.Round(Convert.ToDecimal(txtPaidValue.Text));
                    TotalValue += paidval;
                    //txtPaidValue.Text = Math.Round(paidval).ToString();
                    txtPaidValue.Enabled = true;
                }
                else
                {
                    txtPaidValue.Text = "0";
                    txtPaidValue.Enabled = false;
                }
            }
            txtGrandTotal.Text = Math.Round(TotalValue).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

#line default
#line hidden
