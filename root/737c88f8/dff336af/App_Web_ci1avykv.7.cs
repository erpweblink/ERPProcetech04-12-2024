#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\Payment.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CA84825053F2038E0FDE7F4E77E56A902E635922"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\Payment.aspx.cs"
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
public partial class Admin_Payment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    string id;
    string checkinvooice;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                id = Decrypt(Request.QueryString["Id"].ToString());
                Session["UPID"] = id;
                btnsubmit.Text = "Update";
                LoadData(id);

                hidden1.Value = id;
            }
            else
            {
                txtdate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            }

        }
    }

    protected void LoadData(string id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sad1 = new SqlDataAdapter("select * from tblPaymentHdrs where Id='" + id + "'", con);
            sad1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPartyName.Text = dt.Rows[0]["PartyName"].ToString();
                ddlAgainst.Text = dt.Rows[0]["Against"].ToString();

                Session["AgainstVal"] = dt.Rows[0]["Against"].ToString();

                ddltoaccountName.Text = dt.Rows[0]["FromAccountName"].ToString();
                txtbankname.Text = dt.Rows[0]["BankName"].ToString();
                ddltransactionmode.Text = dt.Rows[0]["TransactionMode"].ToString();
                txtmodedescription.Text = dt.Rows[0]["ModeDescription"].ToString();
                txtdate.Text = dt.Rows[0]["PostDate"].ToString();
                txtamount.Text = dt.Rows[0]["Amount"].ToString();
                txtremark.Text = dt.Rows[0]["TransactionRemark"].ToString();
                txttds.Text = dt.Rows[0]["ApplyTDS"].ToString();
                txtbasic.Text = dt.Rows[0]["Basic"].ToString();
                if (ddlAgainst.Text == "Invoice Bill")
                {
                    BillDetailsload();
                    Session["Adv"] = "0";
                }
                else
                {
                    Gvpayment.Visible = false;
                }

            }

        }
        catch (Exception)
        {

            throw;
        }


    }

    protected void BillDetailsload()
    {
        try
        {
            //Bind GridView
            DataTable dtbindgv = new DataTable();
            //SqlDataAdapter sad2 = new SqlDataAdapter("select * from tblPaymentDtls where HeaderId='" + id + "'", con);
            SqlDataAdapter sad2 = new SqlDataAdapter("SELECT [Id],[HeaderId],[BillNo] as SupplierBillNo,[BillDate],[Basic],[GrandTotal],[Recvd],[WO],[Paid],[TDS],[Adjust],[Excess],[Pending],[Total],[Note],[Chk],[SupplierName] FROM tblPaymentDtls where HeaderId='" + id + "'", con);
            sad2.Fill(dtbindgv);
            Gvpayment.DataSource = dtbindgv;
            Gvpayment.DataBind();
            Gvpayment.EmptyDataText = "Record Not Show";
        }
        catch (Exception)
        {

            throw;
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
    public static List<string> GetSupplierList(string prefixText, int count)
    {
        return AutoFillSupplierlist(prefixText);
    }

    public static List<string> AutoFillSupplierlist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT SupplierName from tblSupplierMaster where " + "SupplierName like @Search + '%'  ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> SupplierName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        SupplierName.Add(sdr["SupplierName"].ToString());
                    }
                }
                con.Close();
                return SupplierName;
            }

        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetBankList(string prefixText, int count)
    {
        return AutoFillbanklist(prefixText);
    }

    public static List<string> AutoFillbanklist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT BankName from tblBankMaster where " + "BankName like @Search + '%' AND isdeleted='0' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> BankName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        BankName.Add(sdr["BankName"].ToString());
                    }
                }
                con.Close();
                return BankName;
            }

        }
    }

    protected void ddlAgainst_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPartyName.Text))
            {
                if (ddlAgainst.Text == "Invoice Bill")
                {
                    Gvpayment.Visible = true;
                    DataTable dtagainst = new DataTable();
                    SqlDataAdapter sad = new SqlDataAdapter("select * from tblPurchaseBillHdr where IsPaid is null and SupplierName='" + txtPartyName.Text + "'", con);//
                    sad.Fill(dtagainst);
                    if (dtagainst.Rows.Count > 0)
                    {
                        Gvpayment.DataSource = dtagainst;
                        Gvpayment.DataBind();
                        Gvpayment.EmptyDataText = "Record Not Found";
                        //txtamount.Text = dtagainst.Rows[0]["GrandTotal"].ToString();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bill Record Not Found...Already Paid');window.location.href='Payment.aspx';", true);

                    }
                    lblshowmsg.Visible = false;
                    Session["Adv"] = "1";
                }
                else
                {
                    lblshowmsg.Visible = true;
                    Gvpayment.Visible = false;
                    lblshowmsg.Text = "No Information Found";

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Enter Party Name !!!');", true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gvpayment.Rows)
        {
            TextBox paid = (TextBox)row.FindControl("txtgvpaid");
            TextBox TDS = (TextBox)row.FindControl("txtgvTDS");
            TextBox Adjust = (TextBox)row.FindControl("txtgvadjust");
            TextBox Excess = (TextBox)row.FindControl("txtgvExcess");
            TextBox Pending = (TextBox)row.FindControl("txtgvpending");
            TextBox Note = (TextBox)row.FindControl("txtgvNote");
            Label payable = (Label)row.FindControl("lblpayable");
            Label Totalamount = (Label)row.FindControl("lbltotal");
            Label totalfooter = (Label)Gvpayment.FooterRow.FindControl("footertotal");
            Label footerpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");
            CheckBox chk = (CheckBox)row.FindControl("chkRow");

            TextBox Reced = (TextBox)row.FindControl("lblrate");

            Label lblfooterpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");

            var tdsval = TDS.Text == "" ? "0" : TDS.Text;

            var paidval = Convert.ToDouble(payable.Text) - Convert.ToDouble(Reced.Text) - Convert.ToDouble(tdsval);

            //var pendingval = Convert.ToDouble(payable.Text) - Convert.ToDouble(Reced.Text) - Convert.ToDouble(paidval);

            //var pendn = Math.Round(pendingval).ToString();

            //var finalpend = Convert.ToDouble(tdsval) - Convert.ToDouble(pendn);


            if (chk != null & chk.Checked)
            {

                paid.Enabled = true;
                TDS.Enabled = true;
                Adjust.Enabled = true;
                Excess.Enabled = true;
                Pending.Enabled = true;
                Note.Enabled = true;
                paid.Text = paidval.ToString();
                Totalamount.Text = (Convert.ToDecimal(payable.Text) - Convert.ToDecimal(TDS.Text == "" ? "0" : TDS.Text)).ToString();
                Calculation(row);
                //Pending.Text = finalpend.ToString();
                SumOfTotalFooter += Convert.ToDouble(Totalamount.Text);
                SumOfPaidFooter += Convert.ToDouble(paid.Text);

            }
            else
            {
                paid.Enabled = false;
                TDS.Enabled = false;
                Adjust.Enabled = false;
                Excess.Enabled = false;
                Pending.Enabled = false;
                Note.Enabled = false;
                paid.Text = "0";
                TDS.Text = "0";
                Adjust.Text = "0";
                Excess.Text = "0";
                Pending.Text = "0";
                Note.Text = string.Empty;
                Totalamount.Text = "0";
            }
            totalfooter.Text = SumOfTotalFooter.ToString();
            footerpaid.Text = SumOfPaidFooter.ToString();
        }
    }

    Double SumOfTotalFooter;
    Double SumOfPaidFooter;
    protected void Calculation(GridViewRow row)
    {
        Label FinalBasic = (Label)row.FindControl("lblfinalbasic");
        TextBox TDS = (TextBox)row.FindControl("txtgvTDS");
        CheckBox Chkrow = (CheckBox)row.FindControl("chkRow");
        Double TDSs = Convert.ToDouble(FinalBasic.Text) * Convert.ToDouble(txttds.Text) / 100;
        TDS.Text = TDSs.ToString();

        Label lblPayable = (Label)row.FindControl("lblpayable");

        Label lbltotal = (Label)row.FindControl("lbltotal");
        TextBox txtTDS = (TextBox)row.FindControl("txtgvTDS");
        TextBox txtadjust = (TextBox)row.FindControl("txtgvadjust");
        TextBox txtexcess = (TextBox)row.FindControl("txtgvExcess");
        TextBox txtpending = (TextBox)row.FindControl("txtgvpending");
        TextBox txtpaid = (TextBox)row.FindControl("txtgvpaid");
        TextBox Reced = (TextBox)row.FindControl("lblrate");

        txtpending.Text = lblPayable.ToString();

        var paidtotal = Convert.ToDouble(txtpaid.Text);
        double payAmt = Convert.ToDouble(txtpaid.Text) + Convert.ToDouble(txtTDS.Text);
        double Paid = Convert.ToDouble(paidtotal);
        if (txtpaid.Text == lblPayable.Text)
        {
            Double lbltotal1 = Paid - Convert.ToDouble(txtTDS.Text);
            txtpaid.Text = Math.Round(lbltotal1).ToString();
            lbltotal.Text = Math.Round(lbltotal1).ToString();
            txtpending.Text = (Convert.ToDouble(lblPayable.Text) - Paid).ToString("#0.00");
        }
        else
        {
            lbltotal.Text = Math.Round(paidtotal).ToString();
            txtpending.Text = (Convert.ToDouble(lblPayable.Text) - Convert.ToDouble(Reced.Text) - payAmt).ToString("#0.00");
        }

    }

    decimal paidval = 0;
    decimal totval = 0;
    protected void txtgvpaid_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        Calculation(row);

        foreach (GridViewRow g1 in Gvpayment.Rows)
        {
            string paid = (g1.FindControl("txtgvpaid") as TextBox).Text;
            paidval += Convert.ToDecimal(paid);

            string tot = (g1.FindControl("lbltotal") as Label).Text;
            totval += Convert.ToDecimal(tot);
        }
        Label lblfinalbasic = (Label)Gvpayment.FooterRow.FindControl("footerpaid");
        lblfinalbasic.Text = paidval.ToString();

        Label footertotal = (Label)Gvpayment.FooterRow.FindControl("footertotal");
        footertotal.Text = totval.ToString();
    }

    protected void txtgvTDS_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        Calculation(row);
    }

    protected void txtgvadjust_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        Calculation(row);
    }

    protected void txtgvExcess_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        Calculation(row);
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Payment.aspx");
    }

    DateTime date = DateTime.Now;
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int id;
            if (btnsubmit.Text == "Submit")
            {
                fnInsert();
            }
            else if (btnsubmit.Text == "Update")
            {
                if (Session["AgainstVal"] != "")
                {
                    if (Session["Adv"].ToString() == "1")
                    {
                        fnInsert();

                        string Advanceamt = "";
                        DataTable dttupdateadv = new DataTable();
                        SqlDataAdapter sadupds = new SqlDataAdapter("select * from tblPaymentHdrs where Id='" + Session["UPID"].ToString() + "'", con);
                        sadupds.Fill(dttupdateadv);

                        if (dttupdateadv.Rows.Count > 0)
                        {
                            Advanceamt = dttupdateadv.Rows[0]["Amount"].ToString();
                        }

                        var MinusPlusAdvn = Convert.ToDouble(Advanceamt) - Convert.ToDouble(txtamount.Text);
                        SqlCommand cmduptt = new SqlCommand("update tblPaymentHdrs set Amount='" + Math.Round(MinusPlusAdvn).ToString() + "' where Id='" + Session["UPID"].ToString() + "'", con);
                        con.Open();
                        cmduptt.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        DateTime date = DateTime.Now;
                        SqlCommand cmd = new SqlCommand("SP_PaymentHdrs", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PartyName", txtPartyName.Text);
                        cmd.Parameters.AddWithValue("@FromAccountName", ddltoaccountName.Text);
                        cmd.Parameters.AddWithValue("@BankName", txtbankname.Text);
                        cmd.Parameters.AddWithValue("@TransactionMode", ddltransactionmode.Text);
                        cmd.Parameters.AddWithValue("@ModeDescription", txtmodedescription.Text);
                        cmd.Parameters.AddWithValue("@PostDate", txtdate.Text);

                        cmd.Parameters.AddWithValue("@Against", ddlAgainst.Text);
                        cmd.Parameters.AddWithValue("@Id", hidden1.Value);
                        cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
                        cmd.Parameters.AddWithValue("@TransactionRemark", txtremark.Text);
                        cmd.Parameters.AddWithValue("@ApplyTDS", txttds.Text);
                        cmd.Parameters.AddWithValue("@Basic", txtbasic.Text);
                        cmd.Parameters.AddWithValue("@UpdatedBy", Session["name"].ToString());
                        cmd.Parameters.AddWithValue("@UpdatedOn", date);

                        cmd.Parameters.Add("@Idd", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@Action", "Update");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //id = Convert.ToInt32(cmd.Parameters["@Idd"].Value);

                        SqlCommand CmdDelete = new SqlCommand("DELETE FROM tblPaymentDtls WHERE HeaderID=@HeaderID", con);
                        CmdDelete.Parameters.AddWithValue("@HeaderID", hidden1.Value);
                        con.Open();
                        CmdDelete.ExecuteNonQuery();
                        con.Close();

                        if (ddlAgainst.Text == "Invoice Bill")
                        {
                            foreach (GridViewRow g1 in Gvpayment.Rows)
                            {
                                bool chk = (g1.FindControl("chkRow") as CheckBox).Checked;
                                string Billno = (g1.FindControl("lblBillno") as Label).Text;
                                string Billdate = (g1.FindControl("lblBillDate") as Label).Text;
                                string payable = (g1.FindControl("lblpayable") as Label).Text;
                                string Recvd = (g1.FindControl("lblrate") as TextBox).Text;
                                string WO = (g1.FindControl("lblWO") as TextBox).Text;
                                string paid = (g1.FindControl("txtgvpaid") as TextBox).Text;
                                string TDS = (g1.FindControl("txtgvTDS") as TextBox).Text;
                                string Adjust = (g1.FindControl("txtgvadjust") as TextBox).Text;
                                string Excess = (g1.FindControl("txtgvExcess") as TextBox).Text;
                                string Pending = (g1.FindControl("txtgvpending") as TextBox).Text;
                                string Total = (g1.FindControl("lbltotal") as Label).Text;
                                string Notes = (g1.FindControl("txtgvNote") as TextBox).Text;
                                string lblbasic = (g1.FindControl("lblfinalbasic") as Label).Text;
                                decimal Paid1 = Convert.ToDecimal(paid.ToString());
                                decimal TDS1 = Convert.ToDecimal(TDS.ToString());

                                SqlCommand cmd1 = new SqlCommand("SP_PaymentDtls", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@HeaderId", hidden1.Value);
                                cmd1.Parameters.AddWithValue("@BillNo", Billno);
                                cmd1.Parameters.AddWithValue("@Billdate", Billdate);
                                cmd1.Parameters.AddWithValue("@Basic", lblbasic);
                                cmd1.Parameters.AddWithValue("@GrandTotal", payable);

                                var Recd = Recvd == "" ? "0" : Recvd;
                                var totRece = Convert.ToDouble(Recd) + Convert.ToDouble(paid);
                                cmd1.Parameters.AddWithValue("@Recvd", totRece);
                                cmd1.Parameters.AddWithValue("@WO", WO);
                                cmd1.Parameters.AddWithValue("@Paid", Paid1);
                                cmd1.Parameters.AddWithValue("@TDS", TDS1);
                                cmd1.Parameters.AddWithValue("@Adjust", Adjust);
                                cmd1.Parameters.AddWithValue("@Excess", Excess);
                                cmd1.Parameters.AddWithValue("@Pending", Math.Round(Convert.ToDouble(Pending)).ToString());
                                cmd1.Parameters.AddWithValue("@Total", Total);
                                cmd1.Parameters.AddWithValue("@Note", Notes);
                                cmd1.Parameters.AddWithValue("@Chk", chk);
                                cmd1.Parameters.AddWithValue("@SupplierName", txtPartyName.Text);
                                if (chk == true)
                                {
                                    cmd1.Parameters.AddWithValue("@Action", "Insert");
                                }
                                con.Open();
                                cmd1.ExecuteNonQuery();
                                con.Close();

                                if (chk == true)
                                {
                                    Label lblfooterpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");

                                    con.Open();
                                    SqlCommand cmddtt = new SqlCommand("select MAX(Id) from tblPaymentDtls where BillNo='" + Billno + "'", con);
                                    Object mxid = cmddtt.ExecuteScalar();

                                    SqlCommand cmddtot = new SqlCommand("select sum(cast(Total as float)) from tblPaymentDtls where BillNo='" + Billno + "'", con);
                                    Object TOTalamt = cmddtot.ExecuteScalar();

                                    SqlCommand cmddgtot = new SqlCommand("select GrandTotal from tblPaymentDtls where Id='" + mxid.ToString() + "'", con);
                                    Object GTOTalamt = cmddgtot.ExecuteScalar();

                                    var Payble = payable;//lblfooterpaid.Text;
                                    var tot = TOTalamt == null ? "0" : TOTalamt.ToString();
                                    var Gtot = GTOTalamt == null ? "0" : GTOTalamt.ToString();

                                   var totminus = Convert.ToDecimal(Gtot) - (Math.Round(Paid1 + TDS1));

                                    if (totminus == 0)
                                    {
                                        SqlCommand cmdpaid = new SqlCommand("update tblPurchaseBillHdr set IsPaid=1 where SupplierBillNo='" + Billno + "'", con);
                                        cmdpaid.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        SqlCommand cmdpaid = new SqlCommand("update tblPurchaseBillHdr set IsPaid=null where SupplierBillNo='" + Billno + "'", con);
                                        cmdpaid.ExecuteNonQuery();
                                    }
                                    con.Close();
                                }
                            }


                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data updated Sucessfully');window.location.href='Payment.aspx';", true);
                        Session["AgainstVal"] = null;
                    }
                }
                else
                {

                    DateTime date = DateTime.Now;
                    SqlCommand cmd = new SqlCommand("SP_PaymentHdrs", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PartyName", txtPartyName.Text);
                    cmd.Parameters.AddWithValue("@FromAccountName", ddltoaccountName.Text);
                    cmd.Parameters.AddWithValue("@BankName", txtbankname.Text);
                    cmd.Parameters.AddWithValue("@TransactionMode", ddltransactionmode.Text);
                    cmd.Parameters.AddWithValue("@ModeDescription", txtmodedescription.Text);
                    cmd.Parameters.AddWithValue("@PostDate", txtdate.Text);

                    cmd.Parameters.AddWithValue("@Against", ddlAgainst.Text);
                    cmd.Parameters.AddWithValue("@Id", hidden1.Value);
                    cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
                    cmd.Parameters.AddWithValue("@TransactionRemark", txtremark.Text);
                    cmd.Parameters.AddWithValue("@ApplyTDS", txttds.Text);
                    cmd.Parameters.AddWithValue("@Basic", txtbasic.Text);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["name"].ToString());
                    cmd.Parameters.AddWithValue("@UpdatedOn", date);

                    cmd.Parameters.Add("@Idd", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@Action", "Update");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //id = Convert.ToInt32(cmd.Parameters["@Idd"].Value);
                    if (ddlAgainst.Text == "Invoice Bill")
                    {
                        foreach (GridViewRow g1 in Gvpayment.Rows)
                        {
                            bool chk = (g1.FindControl("chkRow") as CheckBox).Checked;
                            string Billno = (g1.FindControl("lblBillno") as Label).Text;
                            string Billdate = (g1.FindControl("lblBillDate") as Label).Text;
                            string payable = (g1.FindControl("lblpayable") as Label).Text;
                            string Recvd = (g1.FindControl("lblrate") as TextBox).Text;
                            string WO = (g1.FindControl("lblWO") as TextBox).Text;
                            string paid = (g1.FindControl("txtgvpaid") as TextBox).Text;
                            string TDS = (g1.FindControl("txtgvTDS") as TextBox).Text;
                            string Adjust = (g1.FindControl("txtgvadjust") as TextBox).Text;
                            string Excess = (g1.FindControl("txtgvExcess") as TextBox).Text;
                            string Pending = (g1.FindControl("txtgvpending") as TextBox).Text;
                            string Total = (g1.FindControl("lbltotal") as Label).Text;
                            string Notes = (g1.FindControl("txtgvNote") as TextBox).Text;
                            string lblbasic = (g1.FindControl("lblfinalbasic") as Label).Text;
                            decimal Paid1 = Convert.ToDecimal(paid.ToString());
                            decimal TDS1 = Convert.ToDecimal(TDS.ToString());

                            SqlCommand cmd1 = new SqlCommand("SP_PaymentDtls", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@HeaderId", hidden1.Value);
                            cmd1.Parameters.AddWithValue("@BillNo", Billno);
                            cmd1.Parameters.AddWithValue("@Billdate", Billdate);
                            cmd1.Parameters.AddWithValue("@Basic", lblbasic);
                            cmd1.Parameters.AddWithValue("@GrandTotal", payable);

                            var Recd = Recvd == "" ? "0" : Recvd;
                            var totRece = Convert.ToDouble(Recd) + Convert.ToDouble(paid);
                            cmd1.Parameters.AddWithValue("@Recvd", totRece);
                            cmd1.Parameters.AddWithValue("@WO", WO);
                            cmd1.Parameters.AddWithValue("@Paid", Paid1);
                            cmd1.Parameters.AddWithValue("@TDS", TDS1);
                            cmd1.Parameters.AddWithValue("@Adjust", Adjust);
                            cmd1.Parameters.AddWithValue("@Excess", Excess);
                            cmd1.Parameters.AddWithValue("@Pending", Math.Round(Convert.ToDouble(Pending)).ToString());
                            cmd1.Parameters.AddWithValue("@Total", Total);
                            cmd1.Parameters.AddWithValue("@Note", Notes);
                            cmd1.Parameters.AddWithValue("@Chk", chk);
                            cmd1.Parameters.AddWithValue("@SupplierName", txtPartyName.Text);
                            if (chk == true)
                            {
                                cmd1.Parameters.AddWithValue("@Action", "Insert");
                            }
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();

                            if (chk == true)
                            {
                                Label lblfooterpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");

                                con.Open();
                                SqlCommand cmddtt = new SqlCommand("select MAX(Id) from tblPaymentDtls where BillNo='" + Billno + "'", con);
                                Object mxid = cmddtt.ExecuteScalar();

                                SqlCommand cmddtot = new SqlCommand("select sum(cast(Total as float)) from tblPaymentDtls where BillNo='" + Billno + "'", con);
                                Object TOTalamt = cmddtot.ExecuteScalar();

                                SqlCommand cmddgtot = new SqlCommand("select GrandTotal from tblPaymentDtls where Id='" + mxid.ToString() + "'", con);
                                Object GTOTalamt = cmddgtot.ExecuteScalar();

                                var Payble = payable;//lblfooterpaid.Text;
                                var tot = TOTalamt == null ? "0" : TOTalamt.ToString();
                                var Gtot = GTOTalamt == null ? "0" : GTOTalamt.ToString();

                                var totminus = Convert.ToDecimal(Gtot) - (Math.Round(Paid1 + TDS1));

                                if (totminus == 0)
                                {
                                    SqlCommand cmdpaid = new SqlCommand("update tblPurchaseBillHdr set IsPaid=1 where SupplierBillNo='" + Billno + "'", con);
                                    cmdpaid.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmdpaid = new SqlCommand("update tblPurchaseBillHdr set IsPaid=null where SupplierBillNo='" + Billno + "'", con);
                                    cmdpaid.ExecuteNonQuery();
                                }
                                con.Close();
                            }
                        }


                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data updated Sucessfully');window.location.href='Payment.aspx';", true);
                    Session["AgainstVal"] = null;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void fnInsert()
    {
        try
        {
            int id;
            SqlCommand cmd = new SqlCommand("SP_PaymentHdrs", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PartyName", txtPartyName.Text);
            cmd.Parameters.AddWithValue("@FromAccountName", ddltoaccountName.Text);
            cmd.Parameters.AddWithValue("@BankName", txtbankname.Text);
            cmd.Parameters.AddWithValue("@TransactionMode", ddltransactionmode.Text);
            cmd.Parameters.AddWithValue("@ModeDescription", txtmodedescription.Text);
            cmd.Parameters.AddWithValue("@PostDate", txtdate.Text.Trim());
            cmd.Parameters.AddWithValue("@Against", ddlAgainst.Text);
            cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
            cmd.Parameters.AddWithValue("@TransactionRemark", txtremark.Text);
            cmd.Parameters.AddWithValue("@ApplyTDS", txttds.Text);
            cmd.Parameters.AddWithValue("@Basic", txtbasic.Text);
            cmd.Parameters.AddWithValue("@CreatedBy", Session["ProductionName"].ToString());
            cmd.Parameters.AddWithValue("@CreatedOn", date);
            cmd.Parameters.Add("@Idd", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Action", "Insert");
            cmd.Parameters.AddWithValue("@isdeleted", '0');
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            id = Convert.ToInt32(cmd.Parameters["@Idd"].Value);
            if (ddlAgainst.Text == "Invoice Bill")
            {
                foreach (GridViewRow g1 in Gvpayment.Rows)
                {
                    bool chk = (g1.FindControl("chkRow") as CheckBox).Checked;
                    string Billno = (g1.FindControl("lblBillno") as Label).Text;
                    string Billdate = (g1.FindControl("lblBillDate") as Label).Text;
                    string payable = (g1.FindControl("lblpayable") as Label).Text;
                    string Recvd = (g1.FindControl("lblrate") as TextBox).Text;
                    string WO = (g1.FindControl("lblWO") as TextBox).Text;
                    string paid = (g1.FindControl("txtgvpaid") as TextBox).Text;
                    string TDS = (g1.FindControl("txtgvTDS") as TextBox).Text;
                    string Adjust = (g1.FindControl("txtgvadjust") as TextBox).Text;
                    string Excess = (g1.FindControl("txtgvExcess") as TextBox).Text;
                    string Pending = (g1.FindControl("txtgvpending") as TextBox).Text;
                    string Total = (g1.FindControl("lbltotal") as Label).Text;
                    string Notes = (g1.FindControl("txtgvNote") as TextBox).Text;
                    string lblbasic = (g1.FindControl("lblfinalbasic") as Label).Text;
                    decimal Paid1 = Convert.ToDecimal(paid.ToString());
                    decimal TDS1 = Convert.ToDecimal(TDS.ToString());

                    SqlCommand cmd1 = new SqlCommand("SP_PaymentDtls", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@HeaderId", id);
                    cmd1.Parameters.AddWithValue("@BillNo", Billno);
                    cmd1.Parameters.AddWithValue("@Billdate", Billdate);
                    cmd1.Parameters.AddWithValue("@Basic", lblbasic);
                    cmd1.Parameters.AddWithValue("@GrandTotal", payable);

                    var Recd = Recvd == "" ? "0" : Recvd;
                    var totRece = Convert.ToDouble(Recd) + Convert.ToDouble(paid);
                    cmd1.Parameters.AddWithValue("@Recvd", totRece);
                    cmd1.Parameters.AddWithValue("@WO", WO);
                    cmd1.Parameters.AddWithValue("@Paid", Paid1);
                    cmd1.Parameters.AddWithValue("@TDS", TDS1);
                    cmd1.Parameters.AddWithValue("@Adjust", Adjust);
                    cmd1.Parameters.AddWithValue("@Excess", Excess);
                    cmd1.Parameters.AddWithValue("@Pending", Math.Round(Convert.ToDouble(Pending)).ToString());
                    cmd1.Parameters.AddWithValue("@Total", Total);
                    cmd1.Parameters.AddWithValue("@Note", Notes);
                    cmd1.Parameters.AddWithValue("@Chk", chk);
                    cmd1.Parameters.AddWithValue("@SupplierName", txtPartyName.Text);
                    if (chk == true)
                    {
                        cmd1.Parameters.AddWithValue("@Action", "Insert");
                    }
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    if (chk == true)
                    {
                        Label lblfooterpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");

                        con.Open();
                        SqlCommand cmddtt = new SqlCommand("select MAX(Id) from tblPaymentDtls where BillNo='" + Billno + "'", con);
                        Object mxid = cmddtt.ExecuteScalar();

                        SqlCommand cmddtot = new SqlCommand("select sum(cast(Total as float)) from tblPaymentDtls where BillNo='" + Billno + "'", con);
                        Object TOTalamt = cmddtot.ExecuteScalar();

                        SqlCommand cmddgtot = new SqlCommand("select GrandTotal from tblPaymentDtls where Id='" + mxid.ToString() + "'", con);
                        Object GTOTalamt = cmddgtot.ExecuteScalar();

                        var Payble = payable;//lblfooterpaid.Text;
                        var tot = TOTalamt == null ? "0" : TOTalamt.ToString();
                        var Gtot = GTOTalamt == null ? "0" : GTOTalamt.ToString();

                         var totminus = Convert.ToDecimal(Gtot) - (Math.Round(Paid1 + TDS1));

                        if (totminus == 0)
                        {
                            SqlCommand cmdpaid = new SqlCommand("update tblPurchaseBillHdr set IsPaid=1 where SupplierBillNo='" + Billno + "'", con);
                            cmdpaid.ExecuteNonQuery();
                        }
                        else
                        {
                        }
                        con.Close();
                    }
                }

                DataTable dt546665 = new DataTable();
                SqlDataAdapter sadparticular = new SqlDataAdapter("select * from tblPaymentDtls where HeaderId='" + id + "'", con);
                sadparticular.Fill(dt546665);
                if (dt546665.Rows.Count > 0)
                {

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('At Least Select One Record.');", true);
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Sucessfully');window.location.href='Payment.aspx';", true);
            Session["AgainstVal"] = null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    decimal payable, paid, TDS, Excess, Basic, Adjust, Total, pending = 0; decimal footer = 0;
    protected void Gvpayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnsubmit.Text == "Update")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (Session["AgainstVal"].ToString() == "Advance")
                //{

                int id = Convert.ToInt32(Gvpayment.DataKeys[e.Row.RowIndex].Values[0]);
                TextBox paid = (TextBox)e.Row.FindControl("txtgvpaid");
                TextBox TDS = (TextBox)e.Row.FindControl("txtgvTDS");
                TextBox Adjust = (TextBox)e.Row.FindControl("txtgvadjust");
                TextBox Excess = (TextBox)e.Row.FindControl("txtgvExcess");
                TextBox Pending = (TextBox)e.Row.FindControl("txtgvpending");
                Label Total = (Label)e.Row.FindControl("lbltotal");
                Label lblbasic = (Label)e.Row.FindControl("lblfinalbasic");
                TextBox Notes = (TextBox)e.Row.FindControl("txtgvNote");
                CheckBox chk = (CheckBox)e.Row.FindControl("chkRow");
                con.Open();
                SqlCommand cmd4525 = new SqlCommand("select * from tblPaymentDtls where Id='" + id + "'", con);
                SqlDataReader dr = cmd4525.ExecuteReader();
                if (dr.Read())
                {

                    paid.Text = dr["Paid"].ToString();
                    lblbasic.Text = dr["Basic"].ToString();
                    TDS.Text = dr["TDS"].ToString();
                    Adjust.Text = dr["Adjust"].ToString();
                    Excess.Text = dr["Excess"].ToString();
                    Pending.Text = dr["Pending"].ToString();
                    Total.Text = dr["Total"].ToString();
                    Notes.Text = dr["Note"].ToString();
                    checkinvooice = dr["Chk"].ToString();
                    dr.Close();
                }
                chk.Checked = checkinvooice == "True" ? true : false;
                con.Close();

                if (chk != null & chk.Checked)
                {
                    paid.Enabled = true;
                    TDS.Enabled = true;
                    Adjust.Enabled = true;
                    Excess.Enabled = true;
                    Pending.Enabled = true;
                    Notes.Enabled = true;

                }
                else
                {
                    paid.Enabled = false;
                    TDS.Enabled = false;
                    Adjust.Enabled = false;
                    Excess.Enabled = false;
                    Pending.Enabled = false;
                    Notes.Enabled = false;

                }
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
            int id = Convert.ToInt32(Gvpayment.DataKeys[e.Row.RowIndex].Values[0]);
            Label payableAmt = (Label)e.Row.FindControl("lblpayable");

            var pbalevalue = Convert.ToDouble(payableAmt.Text);
            payableAmt.Text = Math.Round(pbalevalue).ToString();

            Label BillNo = (Label)e.Row.FindControl("lblBillno");
            string billno = BillNo.Text;
            CheckBox chk = (CheckBox)e.Row.FindControl("chkRow");
            CheckBox chkheader = (CheckBox)Gvpayment.HeaderRow.FindControl("chkheader");

            if (Session["UPID"] != "")
            {
                con.Open();
                SqlCommand cmdmaxid = new SqlCommand("select Id from tblPurchaseBillHdr where SupplierBillNo='" + billno + "' AND SupplierName='" + txtPartyName.Text + "'", con);
                string idd = cmdmaxid.ExecuteScalar().ToString();

                SqlCommand cmddtl = new SqlCommand("select SUM(CAST(Amount as float)) from tblPurchaseBillDtls where HeaderID='" + idd + "'", con);
                Object TaxAmt = cmddtl.ExecuteScalar() == null ? "0" : cmddtl.ExecuteScalar();
                SqlCommand cmdTc = new SqlCommand("select TransportationCharges from tblPurchaseBillHdr where SupplierBillNo='" + billno + "' AND SupplierName='" + txtPartyName.Text + "'", con);
                Object cmdTcval = cmdTc.ExecuteScalar() == null ? "0" : cmdTc.ExecuteScalar();

                con.Close();
                Label lblBasic = (Label)e.Row.FindControl("lblfinalbasic");

                //var Amt = Convert.ToDecimal(TaxAmt.ToString()) + Convert.ToDecimal(lblBasic.Text==null?"0": lblBasic.Text) + Convert.ToDecimal(cmdTcval.ToString());
                var Amt = Convert.ToDecimal(TaxAmt.ToString()) + Convert.ToDecimal(cmdTcval.ToString());
                var basicvalue = Math.Round(Amt);
                Label lblfinalbasic = (Label)e.Row.FindControl("lblfinalbasic");
                lblfinalbasic.Text = basicvalue.ToString("N2", info);

                Label lblBillno = (Label)e.Row.FindControl("lblBillno");
                SqlCommand cmdmax = new SqlCommand("SELECT Recvd FROM tblPaymentDtls where BillNo='" + lblBillno.Text + "' AND SupplierName='" + txtPartyName.Text + "'", con);
                con.Open();
                Object Recvdval = cmdmax.ExecuteScalar();
                con.Close();

                TextBox lblRecvdd = (TextBox)e.Row.FindControl("lblrate");

                if (Recvdval == null)
                {
                    lblRecvdd.Text = "0";
                }
                else
                {
                    lblRecvdd.Text = Recvdval.ToString();
                }
            }
            else
            {
                SqlCommand cmdmax = new SqlCommand("SELECT min(Pending) FROM tblPaymentDtls  where SupplierName='" + txtPartyName.Text + "' AND BillNo='" + billno + "'", con);
                con.Open();
                Object smpayable = cmdmax.ExecuteScalar();

                SqlCommand cmddtl = new SqlCommand("select SUM(CAST(Amount as float)) from tblPurchaseBillDtls where HeaderID='" + id + "'", con);
                Object TaxAmt = cmddtl.ExecuteScalar();

                SqlCommand cmdTc = new SqlCommand("select TransportationCharges from tblPurchaseBillHdr where Id='" + id + "'", con);
                Object cmdTcval = cmdTc.ExecuteScalar();
                con.Close();

                Label lblBasic = (Label)e.Row.FindControl("lblfinalbasic");

                var bval = lblBasic.Text.Replace(",", "");

                var txval = TaxAmt.ToString() == "" ? "0" : TaxAmt.ToString();

                var Amt = Convert.ToDecimal(txval) + Convert.ToDecimal(bval) + Convert.ToDecimal(cmdTcval.ToString());
                var basicvalue = Math.Round(Amt);
                Label lblfinalbasic = (Label)e.Row.FindControl("lblfinalbasic");
                lblfinalbasic.Text = basicvalue.ToString("N2", info);
                Basic += Decimal.Parse(lblfinalbasic.Text);

                if (smpayable.ToString() == "0.00")
                {
                    payableAmt.Text = "0";
                    chk.Enabled = false;
                    chkheader.Enabled = false;
                    e.Row.Visible = false;
                }

                Label lblBillno = (Label)e.Row.FindControl("lblBillno");
                SqlCommand cmdmaxdd = new SqlCommand("SELECT Recvd FROM tblPaymentDtls where BillNo='" + lblBillno.Text + "'", con);
                con.Open();
                Object Recvdval = cmdmaxdd.ExecuteScalar();
                con.Close();

                TextBox lblRecvdd = (TextBox)e.Row.FindControl("lblrate");

                if (Recvdval == null)
                {
                    lblRecvdd.Text = "0";
                }
                else
                {
                    lblRecvdd.Text = Recvdval.ToString();
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPgTotal = (Label)e.Row.FindControl("lblpayable");
            payable += Decimal.Parse(lblPgTotal.Text);
        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    Label lblpayablefooter = (Label)e.Row.FindControl("footerpayble");
        //    lblpayablefooter.Text = payable.ToString();
        //    if (payable.ToString() == "0")
        //    {
        //        //CheckBox chk = Gvpayment.FindControl("chkRow") as CheckBox;
        //        //CheckBox chk = (CheckBox)Gvpayment.Rows.FindControl("chkRow");
        //        lblmsg.Text = "Payment Already Paid";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Payment Already Paid');", true);
        //        btnsubmit.Enabled = false;
        //        //chk.Enabled = false;
        //    }
        //}

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblpayablefooter = (Label)e.Row.FindControl("footerpayble");
            lblpayablefooter.Text = payable.ToString();
            Label lblfooterpaid = (Label)e.Row.FindControl("footerpaid");
            lblfooterpaid.Text = TDS.ToString();
            Label lblfootertds = (Label)e.Row.FindControl("footertds");
            lblfootertds.Text = TDS.ToString();
            Label lblfooteradjust = (Label)e.Row.FindControl("footeradjust");
            lblfooteradjust.Text = Adjust.ToString();
            Label lblfooterexcess = (Label)e.Row.FindControl("footerexcess");
            lblfooterexcess.Text = Excess.ToString();
            Label lblfootertotal = (Label)e.Row.FindControl("footertotal");
            lblfootertotal.Text = Total.ToString();
            Label lblfooterpending = (Label)e.Row.FindControl("footerpending");
            lblfooterpending.Text = pending.ToString();

        }
    }

    protected void chkheader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gvpayment.Rows)
        {
            CheckBox chckheader = (CheckBox)Gvpayment.HeaderRow.FindControl("chkheader");
            CheckBox chckrow = (CheckBox)row.FindControl("chkRow");
            TextBox paid = (TextBox)row.FindControl("txtgvpaid");
            TextBox TDS = (TextBox)row.FindControl("txtgvTDS");
            TextBox Adjust = (TextBox)row.FindControl("txtgvadjust");
            TextBox Excess = (TextBox)row.FindControl("txtgvExcess");
            TextBox Pending = (TextBox)row.FindControl("txtgvpending");
            TextBox Note = (TextBox)row.FindControl("txtgvNote");
            Label payable = (Label)row.FindControl("lblpayable");
            Label Totalamount = (Label)row.FindControl("lbltotal");
            Label totalfooter = (Label)Gvpayment.FooterRow.FindControl("footertotal");
            Label footerpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");
            TextBox Reced = (TextBox)row.FindControl("lblrate");

            Label lblfooterpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");

            var paidval = Convert.ToDouble(payable.Text) - Convert.ToDouble(Reced.Text);

            var pendingval = Convert.ToDouble(payable.Text) - Convert.ToDouble(Reced.Text) - Convert.ToDouble(paidval);

            if (chckheader.Checked == true)
            {
                chckrow.Checked = true;
                paid.Enabled = true;
                TDS.Enabled = true;
                Adjust.Enabled = true;
                Excess.Enabled = true;
                Pending.Enabled = true;
                Note.Enabled = true;
                paid.Text = Math.Round(paidval).ToString();
                Totalamount.Text = payable.Text;
                Pending.Text = Math.Round(pendingval).ToString();
                Calculation(row);
                SumOfTotalFooter += Convert.ToDouble(Totalamount.Text);
                SumOfPaidFooter += Convert.ToDouble(paid.Text);

            }
            else
            {
                chckrow.Checked = false;
                paid.Enabled = false;
                TDS.Enabled = false;
                Adjust.Enabled = false;
                Excess.Enabled = false;
                Pending.Enabled = false;
                Note.Enabled = false;
                paid.Text = "0";
                TDS.Text = "0";
                Adjust.Text = "0";
                Excess.Text = "0";
                Pending.Text = "0";
                Note.Text = string.Empty;
                Totalamount.Text = "0";
            }
            totalfooter.Text = Math.Round(SumOfTotalFooter).ToString();
            footerpaid.Text = Math.Round(SumOfPaidFooter).ToString();
        }

    }

    protected void ddltoaccountName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltoaccountName.Text == "Bank" || ddltoaccountName.Text == "CC" || ddltoaccountName.Text == "OD" || ddltoaccountName.Text == "OD")
        {
            txtbankname.Text = "HDFC Bank";
        }
        else
        {
            txtbankname.Text = "Shirke Sir";
        }
    }

    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        if (ddlAgainst.Text == "Invoice Bill")
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            Label footertotal = (Label)Gvpayment.FooterRow.FindControl("footerpaid");
            if (Convert.ToDouble(txtamount.Text) <= Convert.ToDouble(footertotal.Text))
            {
                btnsubmit.Enabled = true;
            }
            else
            {
                btnsubmit.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Amount is not match');", true);
                txtamount.Focus();

            }
        }
    }

    protected void txttds_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in Gvpayment.Rows)
            {
                Label FinalBasic = (Label)row.FindControl("lblfinalbasic");
                TextBox TDS = (TextBox)row.FindControl("txtgvTDS");
                TextBox txtgvpaid = (TextBox)row.FindControl("txtgvpaid");
                Label lbltotal = (Label)row.FindControl("lbltotal");

                CheckBox Chkrow = (CheckBox)row.FindControl("chkRow");
                if (Chkrow.Checked)
                {
                    Double TDSs = Convert.ToDouble(FinalBasic.Text) * Convert.ToDouble(txttds.Text) / 100;
                    TDS.Text = TDSs.ToString("#.00");

                    var tot = Convert.ToDecimal(txtgvpaid.Text) - Convert.ToDecimal(TDS.Text);

                    txtgvpaid.Text = tot.ToString();
                    lbltotal.Text = tot.ToString();

                }
				
				 TextBox paid = (TextBox)row.FindControl("txtgvpaid");
                //TextBox TDS = (TextBox)row.FindControl("txtgvTDS");
                TextBox Adjust = (TextBox)row.FindControl("txtgvadjust");
                TextBox Excess = (TextBox)row.FindControl("txtgvExcess");
                TextBox Pending = (TextBox)row.FindControl("txtgvpending");
                TextBox Note = (TextBox)row.FindControl("txtgvNote");
                Label payable = (Label)row.FindControl("lblpayable");
                Label Totalamount = (Label)row.FindControl("lbltotal");
                Label totalfooter = (Label)Gvpayment.FooterRow.FindControl("footertotal");
                Label footerpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");
                CheckBox chk = (CheckBox)row.FindControl("chkRow");

                TextBox Reced = (TextBox)row.FindControl("lblrate");

                Label lblfooterpaid = (Label)Gvpayment.FooterRow.FindControl("footerpaid");

                var tdsval = TDS.Text == "" ? "0" : TDS.Text;

                var paidval = Convert.ToDouble(payable.Text) - Convert.ToDouble(Reced.Text) - Convert.ToDouble(tdsval);

                if (chk != null & chk.Checked)
                {

                    paid.Enabled = true;
                    TDS.Enabled = true;
                    Adjust.Enabled = true;
                    Excess.Enabled = true;
                    Pending.Enabled = true;
                    Note.Enabled = true;
                    paid.Text = paidval.ToString();
                    Totalamount.Text = (Convert.ToDecimal(payable.Text) - Convert.ToDecimal(TDS.Text == "" ? "0" : TDS.Text)).ToString();
                    Calculation(row);
                    //Pending.Text = finalpend.ToString();
                    SumOfTotalFooter += Convert.ToDouble(Totalamount.Text);
                    SumOfPaidFooter += Convert.ToDouble(paid.Text);

                }
                else
                {
                    paid.Enabled = false;
                    TDS.Enabled = false;
                    Adjust.Enabled = false;
                    Excess.Enabled = false;
                    Pending.Enabled = false;
                    Note.Enabled = false;
                    paid.Text = "0";
                    TDS.Text = "0";
                    Adjust.Text = "0";
                    Excess.Text = "0";
                    Pending.Text = "0";
                    Note.Text = string.Empty;
                    Totalamount.Text = "0";
                }
                totalfooter.Text = Math.Round(SumOfTotalFooter).ToString();
                footerpaid.Text = Math.Round(SumOfPaidFooter).ToString();
            }
            //GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            //Calculation(row);
        }
        catch (Exception)
        {

            throw;
        }
    }

}

#line default
#line hidden
