#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\PurchaseBill.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AF350847A7747F488C956CABA90B2EB425AA6226"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\PurchaseBill.aspx.cs"
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
using System.Activities.Expressions;
using System.Globalization;

public partial class Admin_PurchaseBill : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    DataTable vdt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillddlpaymentterm();
            //fillddlFooter();
            fillddlUnit();
            Session["Iscompleted"] = "true";
            txtBilldate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            UpdateHistorymsg = string.Empty; //regdate = string.Empty;
            if (Request.QueryString["ID"] != null)
            {
                ViewState["RowNo"] = 0;
                dt.Columns.AddRange(new DataColumn[14] { new DataColumn("id"),
                 new DataColumn("Particulars"), new DataColumn("HSN")
                , new DataColumn("Qty"), new DataColumn("Rate"), new DataColumn("Amount"),
                    new DataColumn("CGSTPer"),new DataColumn("CGSTAmt"),new DataColumn("SGSTPer")
                    ,new DataColumn("SGSTAmt"),new DataColumn("IGSTPer"),new DataColumn("IGSTAmt"),new DataColumn("TotalAmount"),new DataColumn("POId")


                });
                ViewState["ParticularDetails"] = dt;
                ViewState["UpdateRowId"] = Decrypt(Request.QueryString["ID"].ToString());


                //Verbal DT

                ViewState["RowNo"] = 0;
                vdt.Columns.AddRange(new DataColumn[16] { new DataColumn("id"),
                 new DataColumn("Particulars"), new DataColumn("HSN")
                , new DataColumn("Qty"), new DataColumn("Rate"),new DataColumn("Discount"), new DataColumn("Amount"),
                    new DataColumn("CGSTPer"),new DataColumn("CGSTAmt"),new DataColumn("SGSTPer")
                    ,new DataColumn("SGSTAmt"),new DataColumn("IGSTPer"),new DataColumn("IGSTAmt"),new DataColumn("TotalAmount"),new DataColumn("Description"),new DataColumn("UOM")
                });
                ViewState["VParticularDetails"] = vdt;
                GetPurchaseBillData(ViewState["UpdateRowId"].ToString());
            }
            else
            {
                ViewState["RowNo"] = 0;
                dt.Columns.AddRange(new DataColumn[14] { new DataColumn("id"),
                 new DataColumn("Particulars"), new DataColumn("HSN")
                , new DataColumn("Qty"), new DataColumn("Rate"), new DataColumn("Amount"),
                    new DataColumn("CGSTPer"),new DataColumn("CGSTAmt"),new DataColumn("SGSTPer")
                    ,new DataColumn("SGSTAmt"),new DataColumn("IGSTPer"),new DataColumn("IGSTAmt"),new DataColumn("TotalAmount"),new DataColumn("POId")
                });
                ViewState["ParticularDetails"] = dt;
                txtBillNo.Text = GenerateComCode();

                //verbal DT

                ViewState["RowNo"] = 0;
                vdt.Columns.AddRange(new DataColumn[16] { new DataColumn("id"),
                 new DataColumn("Particulars"), new DataColumn("HSN")
                , new DataColumn("Qty"), new DataColumn("Rate"),new DataColumn("Discount"), new DataColumn("Amount"),
                    new DataColumn("CGSTPer"),new DataColumn("CGSTAmt"),new DataColumn("SGSTPer")
                    ,new DataColumn("SGSTAmt"),new DataColumn("IGSTPer"),new DataColumn("IGSTAmt"),new DataColumn("TotalAmount"),new DataColumn("Description"),new DataColumn("UOM")
                });
                ViewState["VParticularDetails"] = vdt;
            }
        }
    }

    private void fillddlUnit()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select distinct Unit from tblUnit", con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);

        if (dtpt.Rows.Count > 0)
        {
            txtUOM.DataSource = dtpt;
            txtUOM.DataValueField = "Unit";
            txtUOM.DataTextField = "Unit";
            txtUOM.DataBind();
        }
        //txtUOM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Nill", "0"));
    }

    static string regdate = string.Empty;
    protected void GetPurchaseBillData(string id)
    {
        string query1 = string.Empty;
        query1 = @"select * from tblPurchaseBillHdr where Id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            btnadd.Text = "Update";
            txtSupplierName.Text = dt.Rows[0]["SupplierName"].ToString();

            BindPO();
            txtSupplierBillNo.Text = dt.Rows[0]["SupplierBillNo"].ToString();
            txtBillNo.Text = dt.Rows[0]["BillNo"].ToString();

            //string str = dt.Rows[0]["BillDate"].ToString();
            //str = str.Replace("00:00:00 AM", "");
            //var time = Convert.ToDateTime(str);
            txtBilldate.Text = dt.Rows[0]["BillDate"].ToString();

            ddlBillAgainst.Text = dt.Rows[0]["BillAgainst"].ToString();
            ddlAgainstNumber.SelectedItem.Text = dt.Rows[0]["AgainstNumber"].ToString();
            txtTransportMode.Text = dt.Rows[0]["TransportMode"].ToString();
            txtVehicleNumber.Text = dt.Rows[0]["VehicleNo"].ToString();

            //string strdue = dt.Rows[0]["PaymentDueDate"].ToString();
            //strdue = strdue.Replace("00:00:00 AM", "");
            //var timedue = Convert.ToDateTime(strdue);
            txtPaymentDueDate.Text = dt.Rows[0]["PaymentDueDate"].ToString();

            txtAccontHead.Text = dt.Rows[0]["AccountHead"].ToString();
            txtRemark.Text = dt.Rows[0]["Remarks"].ToString();
            txtEBillNumber.Text = dt.Rows[0]["EBillNumber"].ToString();
            txtDescription.Text = dt.Rows[0]["ChargesDescription"].ToString();
            txtHSN.Text = dt.Rows[0]["HSNSAC"].ToString();
            txtRate.Text = dt.Rows[0]["Rate"].ToString();
            txtBasic.Text = dt.Rows[0]["Basic"].ToString();
            CGSTPer.Text = dt.Rows[0]["CGSTPer"].ToString();
            SGSTPer.Text = dt.Rows[0]["SGSTPer"].ToString();
            IGSTPer.Text = dt.Rows[0]["IGSTPer"].ToString();
            txtCost.Text = dt.Rows[0]["Cost"].ToString();
            txtTCSPer.Text = dt.Rows[0]["TCSPer"].ToString();
            txtTCSAmt.Text = dt.Rows[0]["TCSAmount"].ToString();
            txtGrandTot.Text = dt.Rows[0]["GrandTotal"].ToString();
            hdnfileData.Value = dt.Rows[0]["RefDocument"].ToString();
            //17 march 2022
            txtTCharge.Text = dt.Rows[0]["TransportationCharges"].ToString();
            txtTCGSTPer.Text = dt.Rows[0]["TCGSTPer"].ToString();
            txtTCGSTamt.Text = dt.Rows[0]["TCGSTAmt"].ToString();
            txtTSGSTPer.Text = dt.Rows[0]["TSGSTPer"].ToString();
            txtTSGSTamt.Text = dt.Rows[0]["TSGSTAmt"].ToString();
            txtTIGSTPer.Text = dt.Rows[0]["TIGSTPer"].ToString();
            txtTIGSTamt.Text = dt.Rows[0]["TIGSTAmt"].ToString();
            txtTCost.Text = dt.Rows[0]["TotalCost"].ToString();

            txtDOR.Text = dt.Rows[0]["DateOfReceived"].ToString();

            if (dt.Rows[0]["RefDocument"].ToString() != "")
            {
                spnFileUploadData.InnerText = "File Already Exsist, if you can update then update it.";
            }
            else
            {
                spnFileUploadData.InnerText = "File Not Found";
            }

            if (dt.Rows[0]["BillAgainst"].ToString() == "Verbal")
            {
                getVParticularsdts(id);
                DivVerbal.Visible = true;
                divVerbaldtls.Visible = true;
            }
            else if (dt.Rows[0]["BillAgainst"].ToString() == "Order")
            {
                getParticularsdts(id);
            }
        }
    }

    protected void getVParticularsdts(string id)
    {

        DataTable Dtproduct = new DataTable();
        SqlDataAdapter daa = new SqlDataAdapter("select * from tblPurchaseBillDtls where HeaderID='" + id + "'", con);
        daa.Fill(Dtproduct);
        ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;

        DataTable dt = ViewState["VParticularDetails"] as DataTable;

        if (Dtproduct.Rows.Count > 0)
        {
            for (int i = 0; i < Dtproduct.Rows.Count; i++)
            {
                dt.Rows.Add(ViewState["RowNo"], Dtproduct.Rows[i]["Particulars"].ToString(), Dtproduct.Rows[i]["HSN"].ToString(), Dtproduct.Rows[i]["Qty"].ToString(),
                    Dtproduct.Rows[i]["Rate"].ToString(), Dtproduct.Rows[i]["Discount"].ToString(), Dtproduct.Rows[i]["Amount"].ToString(), Dtproduct.Rows[i]["CGSTPer"].ToString(), "0",
                    Dtproduct.Rows[i]["SGSTPer"].ToString(), "0", Dtproduct.Rows[i]["IGSTPer"].ToString(), "0",
                    Dtproduct.Rows[i]["GrandTotal"].ToString(), Dtproduct.Rows[i]["Description"].ToString(), Dtproduct.Rows[i]["UOM"].ToString());
                ViewState["VParticularDetails"] = dt;
            }
        }
        dgvParticularsDetails.DataSource = dt;
        dgvParticularsDetails.DataBind();
    }

    protected void getParticularsdts(string id)
    {
        SqlDataAdapter ad = new SqlDataAdapter("select * from tblPurchaseBillDtls where HeaderID='" + id + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dgvOrderDtl.DataSource = dt;
            dgvOrderDtl.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('PO Details Not Found !!');", true);
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

    protected string GenerateComCode()
    {

        string invoiceno;
        DateTime date = DateTime.Now;
        string currentyeaar = date.ToString();

        string FinYear = null;

        if (DateTime.Today.Month > 3)
        {
            //FinYear = DateTime.Today.Year.ToString();
            FinYear = DateTime.Today.AddYears(1).ToString("yy");
        }
        else
        {
            var finYear = DateTime.Today.AddYears(1).ToString("yy");
            FinYear = (Convert.ToInt32(finYear) - 1).ToString();
        }
        string previousyear = (Convert.ToDecimal(FinYear) - 1).ToString();

        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([Id]) as maxid FROM [tblPurchaseBillHdr]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
            invoiceno = previousyear.ToString() + "-" + FinYear + "/" + (maxid + 1).ToString();
            if (maxid < 9)
            {
                invoiceno = previousyear.ToString() + "-" + FinYear + "/" + "000" + (maxid + 1).ToString();
            }
            else if (maxid <= 100)
            {
                invoiceno = previousyear.ToString() + "-" + FinYear + "/" + "00" + (maxid + 1).ToString();
            }
        }
        else
        {
            invoiceno = string.Empty;
        }
        return invoiceno;
    }

    bool flg = false;
    protected void btnadd_Click(object sender, EventArgs e)
    {
        #region Insert
        if (btnadd.Text == "Submit")
        {
            SqlCommand cmd = new SqlCommand("SP_PurchaseBill", con);
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;

            byte[] bytes = null;
            if (UploadRefDocs.HasFile)
            {
                string filename = Path.GetFileName(UploadRefDocs.PostedFile.FileName);
                string contentType = UploadRefDocs.PostedFile.ContentType;
                using (Stream fs = UploadRefDocs.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((Int32)fs.Length);
                    }
                }
            }
            if (ddlBillAgainst.Text == "Order")
            {
                foreach (GridViewRow row in dgvOrderDtl.Rows)
                {
                    bool chk = (row.FindControl("chkSelect") as CheckBox).Checked;
                    if (chk == true)
                    {
                        flg = true;
                        break;
                    }
                    else
                    {
                        flg = false;
                    }
                }
            }
            else if (ddlBillAgainst.Text == "Verbal")
            {
                if (dgvParticularsDetails.Rows.Count > 0)
                {
                    flg = true;
                }
                else
                {
                    flg = false;
                }
            }

            if (flg == true)
            {
                cmd.Parameters.AddWithValue("@Action", "insert");
                cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text.Trim());
                cmd.Parameters.AddWithValue("@SupplierBillNo", txtSupplierBillNo.Text.Trim());
                cmd.Parameters.AddWithValue("@BillNo", txtBillNo.Text.Trim());
                cmd.Parameters.AddWithValue("@BillDate", txtBilldate.Text.Trim());
                cmd.Parameters.AddWithValue("@BillAgainst", ddlBillAgainst.Text.Trim());
                cmd.Parameters.AddWithValue("@AgainstNumber", ddlBillAgainst.Text == "Verbal" ? DBNull.Value.ToString() : ddlAgainstNumber.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@TransportMode", txtTransportMode.Text.Trim());
                cmd.Parameters.AddWithValue("@VehicleNo", txtVehicleNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@TransportDescription", txtTransportMode.Text.Trim());
                cmd.Parameters.AddWithValue("@PaymentDueDate", txtPaymentDueDate.Text.Trim());
                cmd.Parameters.AddWithValue("@RefDocument", bytes);
                cmd.Parameters.AddWithValue("@AccountHead", txtAccontHead.Text.Trim());
                cmd.Parameters.AddWithValue("@Remarks", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@EBillNumber", txtEBillNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@ChargesDescription", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@HSNSAC", txtHSN.Text.Trim());
                cmd.Parameters.AddWithValue("@Rate", txtRate.Text.Trim());
                cmd.Parameters.AddWithValue("@Basic", txtBasic.Text.Trim());
                cmd.Parameters.AddWithValue("@CGSTPer", CGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@SGSTPer", SGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@IGSTPer", IGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@Cost", txtCost.Text.Trim());
                cmd.Parameters.AddWithValue("@TCSPer", txtTCSPer.Text.Trim());
                cmd.Parameters.AddWithValue("@TCSAmount", txtTCSAmt.Text.Trim());
                cmd.Parameters.AddWithValue("GrandTotal", txtGrandTot.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());

                //17 March 2022
                cmd.Parameters.AddWithValue("@TransportationCharges", txtTCharge.Text.Trim());
                cmd.Parameters.AddWithValue("@TCGSTPer", txtTCGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@TCGSTAmt", txtTCGSTamt.Text.Trim());
                cmd.Parameters.AddWithValue("@TSGSTPer", txtTSGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@TSGSTAmt", txtTSGSTamt.Text.Trim());
                cmd.Parameters.AddWithValue("@TIGSTPer", txtTIGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@TIGSTAmt", txtTIGSTamt.Text.Trim());
                cmd.Parameters.AddWithValue("@TotalCost", txtTCost.Text.Trim());

                //25 march 2022
                cmd.Parameters.AddWithValue("@DateOfReceived", txtDOR.Text.Trim());
                int a = 0;
                con.Open();
                a = cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmdmax = new SqlCommand("select MAX(Id) as MAxID from tblPurchaseBillHdr", con);
                con.Open();
                Object mx = cmdmax.ExecuteScalar();
                con.Close();
                int MaxId = Convert.ToInt32(mx.ToString());
                int count = 0;
                if (ddlBillAgainst.Text == "Order")
                {
                    if (dgvOrderDtl.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in dgvOrderDtl.Rows)
                        {
                            bool chk = (row.FindControl("chkSelect") as CheckBox).Checked;
                            if (chk == true)
                            {
                                count++;
                                string Particulars = ((Label)row.FindControl("txtParticulars")).Text;
                                string Description = ((TextBox)row.FindControl("txtDescription")).Text;
                                string HSN = ((TextBox)row.FindControl("txtHSN")).Text;
                                string Qty = ((TextBox)row.FindControl("txtQty")).Text;
                                string UOM = ((TextBox)row.FindControl("txtUOM")).Text;
                                string Rate = ((TextBox)row.FindControl("txtRate")).Text;
                                string Amount = ((Label)row.FindControl("txtAmount")).Text;
                                string CGST = ((TextBox)row.FindControl("txtCGST")).Text;
                                string SGST = ((TextBox)row.FindControl("txtSGST")).Text;
                                string IGST = ((TextBox)row.FindControl("txtIGST")).Text;
                               string Discount = ((TextBox)row.FindControl("txtdiscount")).Text == "" ? "0" :((TextBox)row.FindControl("txtdiscount")).Text;
                                string GrandTotal = ((TextBox)row.FindControl("txtGrandTotal")).Text;
                                string POId = ((Label)row.FindControl("lblID")).Text;

                                SqlCommand cmdParticulardata = new SqlCommand(@"INSERT INTO tblPurchaseBillDtls ([HeaderID],[Particulars],[Description],[HSN],[Qty],[UOM],[Rate],[Discount],[Amount],[CGSTPer],[SGSTPer],[IGSTPer],[GrandTotal],[POId])
                                VALUES(" + MaxId + ",'" + Particulars + "','" + Description + "','" + HSN + "','" + Qty + "','" + UOM + "'," +
                                 "'" + Rate + "','" + Discount + "','" + Amount + "','" + CGST + "'," +
                                 "'" + SGST + "','" + IGST + "','" + GrandTotal + "','" + POId + "')", con);
                                con.Open();
                                cmdParticulardata.ExecuteNonQuery();
                                con.Close();

                                SqlCommand cmdgetqty = new SqlCommand("select OpeningStock from tblItemMaster where ItemName='" + Particulars + "'", con);
                                con.Open();
                                Object openeningstock = cmdgetqty.ExecuteScalar();
                                con.Close();

                                string Openningstk = openeningstock.ToString() == "" ? "0" : openeningstock.ToString();

                                var PlusQty = Convert.ToDouble(Openningstk) + Convert.ToDouble(Qty);

                                SqlCommand cmdupdateqty = new SqlCommand("update tblItemMaster set OpeningStock='" + PlusQty + "' where ItemName='" + Particulars + "'", con);
                                con.Open();
                                cmdupdateqty.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }

                    int numVisible = 0;
                    foreach (GridViewRow row in dgvOrderDtl.Rows)
                    {
                        if (row.Visible == true)
                        {
                            numVisible += 1;
                        }
                    }
                    if (Session["Iscompleted"].ToString() == "false" || numVisible != count)
                    {
                        SqlCommand cmdupdate = new SqlCommand("update tblPurchaseOrderHdr set IsClosed=null,Mode='Open' where PONo='" + ddlAgainstNumber.SelectedItem.Text + "'", con);
                        con.Open();
                        cmdupdate.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        SqlCommand cmdupdate = new SqlCommand("update tblPurchaseOrderHdr set IsClosed=1,Mode='Close' where PONo='" + ddlAgainstNumber.SelectedItem.Text + "'", con);
                        con.Open();
                        cmdupdate.ExecuteNonQuery();
                        con.Close();
                    }

                    //if (dgvOrderDtl.Rows.Count == count)
                    //{
                    //    SqlCommand cmdupdate = new SqlCommand("update tblPurchaseOrderHdr set IsClosed=1,Mode='Close' where PONo='" + ddlAgainstNumber.SelectedItem.Text + "'", con);
                    //    con.Open();
                    //    cmdupdate.ExecuteNonQuery();
                    //    con.Close();
                    //}
                    //else
                    //{
                    //    SqlCommand cmdupdate = new SqlCommand("update tblPurchaseOrderHdr set IsClosed=0,Mode='Open' where PONo='" + ddlAgainstNumber.SelectedItem.Text + "'", con);
                    //    con.Open();
                    //    cmdupdate.ExecuteNonQuery();
                    //    con.Close();
                    //}
                }
                else if (ddlBillAgainst.Text == "Verbal")
                {
                    if (dgvParticularsDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in dgvParticularsDetails.Rows)
                        {
                            string Particulars = ((Label)row.FindControl("lblParticulars")).Text;
                            string Description = ((Label)row.FindControl("lblDescription")).Text;
                            string HSN = ((Label)row.FindControl("lblHSN")).Text;
                            string Qty = ((Label)row.FindControl("lblQty")).Text;
                            string UOM = ((Label)row.FindControl("lblUOM")).Text;
                            string Rate = ((Label)row.FindControl("lblRate")).Text;
                            string Amount = ((Label)row.FindControl("lblAmount")).Text;
                            string CGST = ((Label)row.FindControl("lblCGSTPer")).Text;
                            string SGST = ((Label)row.FindControl("lblSGSTPer")).Text;
                            string IGST = ((Label)row.FindControl("lblIGSTPer")).Text;
                            string Discount = ((Label)row.FindControl("lblDiscount")).Text==""?"0": ((Label)row.FindControl("lblDiscount")).Text;
                            string GrandTotal = ((Label)row.FindControl("lblTotalAmount")).Text;
                            //string POId = ((Label)row.FindControl("lblID")).Text;

                            SqlCommand cmdParticulardata = new SqlCommand(@"INSERT INTO tblPurchaseBillDtls ([HeaderID],[Particulars],[Description],[HSN],[Qty],[UOM],[Rate],[Discount],[Amount],[CGSTPer],[SGSTPer],[IGSTPer],[GrandTotal],[POId])
                            VALUES(" + MaxId + ",'" + Particulars + "','" + Description + "','" + HSN + "','" + Qty + "','" + UOM + "'," +
                             "'" + Rate + "','" + Discount + "','" + Amount + "','" + CGST + "'," +
                             "'" + SGST + "','" + IGST + "','" + GrandTotal + "',NULL)", con);
                            con.Open();
                            cmdParticulardata.ExecuteNonQuery();
                            con.Close();

                            SqlCommand cmdgetqty = new SqlCommand("select OpeningStock from tblItemMaster where ItemName='" + Particulars + "'", con);
                            con.Open();
                            Object openeningstock = cmdgetqty.ExecuteScalar();
                            con.Close();

                            string Openningstk = openeningstock.ToString() == "" ? "0" : openeningstock.ToString();

                            var PlusQty = Convert.ToDouble(Openningstk) + Convert.ToDouble(Qty);

                            SqlCommand cmdupdateqty = new SqlCommand("update tblItemMaster set OpeningStock='" + PlusQty + "' where ItemName='" + Particulars + "'", con);
                            con.Open();
                            cmdupdateqty.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Sucessfully');window.location.href='PurchaseBillList.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add Particulars');", true);
            }
        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update")
        {
            byte[] bytes = null;
            if (hdnfileData.Value == "")
            {
                if (UploadRefDocs.HasFile)
                {
                    string filename = Path.GetFileName(UploadRefDocs.PostedFile.FileName);
                    string contentType = UploadRefDocs.PostedFile.ContentType;
                    using (Stream fs = UploadRefDocs.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            bytes = br.ReadBytes((Int32)fs.Length);
                        }
                    }
                }
            }
            else
            {
                if (UploadRefDocs.HasFile)
                {
                    string filename = Path.GetFileName(UploadRefDocs.PostedFile.FileName);
                    string contentType = UploadRefDocs.PostedFile.ContentType;
                    using (Stream fs = UploadRefDocs.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            bytes = br.ReadBytes((Int32)fs.Length);
                        }
                    }
                }
            }

            if (ddlBillAgainst.Text == "Order")
            {
                foreach (GridViewRow row in dgvOrderDtl.Rows)
                {
                    bool chk = (row.FindControl("chkSelect") as CheckBox).Checked;
                    if (chk == true)
                    {
                        flg = true;
                        break;
                    }
                    else
                    {
                        flg = false;
                    }
                }
            }
            else if (ddlBillAgainst.Text == "Verbal")
            {
                if (dgvParticularsDetails.Rows.Count > 0)
                {
                    flg = true;
                }
                else
                {
                    flg = false;
                }
            }

            if (flg == true)
            {
                SqlCommand cmd = new SqlCommand("SP_PurchaseBill", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "update");
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(ViewState["UpdateRowId"].ToString()));
                cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text.Trim());
                cmd.Parameters.AddWithValue("@SupplierBillNo", txtSupplierBillNo.Text.Trim());
                cmd.Parameters.AddWithValue("@BillNo", txtBillNo.Text.Trim());
                cmd.Parameters.AddWithValue("@BillDate", txtBilldate.Text.Trim());
                cmd.Parameters.AddWithValue("@BillAgainst", ddlBillAgainst.Text.Trim());
                cmd.Parameters.AddWithValue("@AgainstNumber", ddlAgainstNumber.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@TransportMode", txtTransportMode.Text.Trim());
                cmd.Parameters.AddWithValue("@VehicleNo", txtVehicleNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@TransportDescription", txtTransportMode.Text.Trim());
                cmd.Parameters.AddWithValue("@PaymentDueDate", txtPaymentDueDate.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountHead", txtAccontHead.Text.Trim());
                cmd.Parameters.AddWithValue("@Remarks", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@EBillNumber", txtEBillNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@ChargesDescription", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@HSNSAC", txtHSN.Text.Trim());
                cmd.Parameters.AddWithValue("@Rate", txtRate.Text.Trim());
                cmd.Parameters.AddWithValue("@Basic", txtBasic.Text.Trim());
                cmd.Parameters.AddWithValue("@CGSTPer", CGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@SGSTPer", SGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@IGSTPer", IGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@Cost", txtCost.Text.Trim());
                cmd.Parameters.AddWithValue("@TCSPer", txtTCSPer.Text.Trim());
                cmd.Parameters.AddWithValue("@TCSAmount", txtTCSAmt.Text.Trim());
                cmd.Parameters.AddWithValue("@GrandTotal", txtGrandTot.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());

                //17 March 2022
                cmd.Parameters.AddWithValue("@TransportationCharges", txtTCharge.Text.Trim());
                cmd.Parameters.AddWithValue("@TCGSTPer", txtTCGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@TCGSTAmt", txtTCGSTamt.Text.Trim());
                cmd.Parameters.AddWithValue("@TSGSTPer", txtTSGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@TSGSTAmt", txtTSGSTamt.Text.Trim());
                cmd.Parameters.AddWithValue("@TIGSTPer", txtTIGSTPer.Text.Trim());
                cmd.Parameters.AddWithValue("@TIGSTAmt", txtTIGSTamt.Text.Trim());
                cmd.Parameters.AddWithValue("@TotalCost", txtTCost.Text.Trim());

                //25 March 2022
                cmd.Parameters.AddWithValue("@DateOfReceived", txtDOR.Text.Trim());

                if (hdnfileData.Value == "")
                {
                    cmd.Parameters.AddWithValue("@RefDocument", bytes);
                }
                else
                {
                    if (UploadRefDocs.HasFile)
                    {
                        cmd.Parameters.AddWithValue("@RefDocument", hdnfileData.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RefDocument", bytes);
                    }
                }
                int a = 0;
                cmd.Connection.Open();
                a = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                SqlCommand cmddelete = new SqlCommand("delete from tblPurchaseBillDtls where HeaderID='" + Convert.ToInt32(ViewState["UpdateRowId"].ToString()) + "'", con);
                con.Open();
                cmddelete.ExecuteNonQuery();
                con.Close();

                if (ddlBillAgainst.Text == "Order")
                {
                    if (dgvOrderDtl.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in dgvOrderDtl.Rows)
                        {
                            bool chk = (row.FindControl("chkSelect") as CheckBox).Checked;
                            if (chk == true)
                            {
                                string Particulars = ((Label)row.FindControl("txtParticulars")).Text;
                                string Description = ((TextBox)row.FindControl("txtDescription")).Text;
                                string HSN = ((TextBox)row.FindControl("txtHSN")).Text;
                                string Qty = ((TextBox)row.FindControl("txtQty")).Text;
                                string UOM = ((TextBox)row.FindControl("txtUOM")).Text;
                                string Rate = ((TextBox)row.FindControl("txtRate")).Text;
                                string Amount = ((Label)row.FindControl("txtAmount")).Text;
                                string CGST = ((TextBox)row.FindControl("txtCGST")).Text;
                                string SGST = ((TextBox)row.FindControl("txtSGST")).Text;
                                string IGST = ((TextBox)row.FindControl("txtIGST")).Text;
                                string Discount = ((TextBox)row.FindControl("txtdiscount")).Text==""?"0": ((TextBox)row.FindControl("txtdiscount")).Text;
                                string GrandTotal = ((TextBox)row.FindControl("txtGrandTotal")).Text;
                                string POId = ((Label)row.FindControl("lblID")).Text;

                                SqlCommand cmdParticulardata = new SqlCommand(@"INSERT INTO tblPurchaseBillDtls ([HeaderID],[Particulars],[Description],[HSN],[Qty],[UOM],[Rate],[Discount],[Amount],[CGSTPer],[SGSTPer],[IGSTPer],[GrandTotal],[POId])
                                VALUES(" + ViewState["UpdateRowId"].ToString() + ",'" + Particulars + "','" + Description + "','" + HSN + "','" + Qty + "','" + UOM + "'," +
                                 "'" + Rate + "','" + Discount + "','" + Amount + "','" + CGST + "'," +
                                 "'" + SGST + "','" + IGST + "','" + GrandTotal + "','" + POId + "')", con);
                                con.Open();
                                cmdParticulardata.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    if (Session["Iscompleted"].ToString() == "false")
                    {
                        SqlCommand cmdupdate = new SqlCommand("update tblPurchaseOrderHdr set IsClosed=null where PONo='" + ddlAgainstNumber.SelectedItem.Text + "'", con);
                        con.Open();
                        cmdupdate.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        SqlCommand cmdupdate = new SqlCommand("update tblPurchaseOrderHdr set IsClosed=1 where PONo='" + ddlAgainstNumber.SelectedItem.Text + "'", con);
                        con.Open();
                        cmdupdate.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else if (ddlBillAgainst.Text == "Verbal")
                {
                    if (dgvParticularsDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in dgvParticularsDetails.Rows)
                        {
                            string Particulars = ((Label)row.FindControl("lblParticulars")).Text;
                            string Description = ((Label)row.FindControl("lblDescription")).Text;
                            string HSN = ((Label)row.FindControl("lblHSN")).Text;
                            string Qty = ((Label)row.FindControl("lblQty")).Text;
                            string UOM = ((Label)row.FindControl("lblUOM")).Text;
                            string Rate = ((Label)row.FindControl("lblRate")).Text;
                            string Amount = ((Label)row.FindControl("lblAmount")).Text;
                            string CGST = ((Label)row.FindControl("lblCGSTPer")).Text;
                            string SGST = ((Label)row.FindControl("lblSGSTPer")).Text;
                            string IGST = ((Label)row.FindControl("lblIGSTPer")).Text;
                             string Discount = ((Label)row.FindControl("lblDiscount")).Text == "" ? "0" : ((Label)row.FindControl("lblDiscount")).Text;
                            string GrandTotal = ((Label)row.FindControl("lblTotalAmount")).Text;
                            //string POId = ((Label)row.FindControl("lblID")).Text;

                            SqlCommand cmdParticulardata = new SqlCommand(@"INSERT INTO tblPurchaseBillDtls ([HeaderID],[Particulars],[Description],[HSN],[Qty],[UOM],[Rate],[Discount],[Amount],[CGSTPer],[SGSTPer],[IGSTPer],[GrandTotal],[POId])
                           VALUES(" + ViewState["UpdateRowId"].ToString() + ",'" + Particulars + "','" + Description + "','" + HSN + "','" + Qty + "','" + UOM + "'," +
                             "'" + Rate + "','" + Discount + "','" + Amount + "','" + CGST + "'," +
                             "'" + SGST + "','" + IGST + "','" + GrandTotal + "',NULL)", con);
                            con.Open();
                            cmdParticulardata.ExecuteNonQuery();
                            con.Close();

                            SqlCommand cmdgetqty = new SqlCommand("select OpeningStock from tblItemMaster where ItemName='" + Particulars + "'", con);
                            con.Open();
                            Object openeningstock = cmdgetqty.ExecuteScalar();
                            con.Close();

                            string Openningstk = openeningstock.ToString() == "" ? "0" : openeningstock.ToString();

                            var PlusQty = Convert.ToDecimal(Openningstk) + Convert.ToDecimal(Qty);

                            SqlCommand cmdupdateqty = new SqlCommand("update tblItemMaster set OpeningStock='" + PlusQty + "' where ItemName='" + Particulars + "'", con);
                            con.Open();
                            cmdupdateqty.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Updated Sucessfully');window.location.href='PurchaseBillList.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add Particulars');", true);
            }
        }
        #endregion Update
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseBill.aspx");
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
                com.CommandText = "Select DISTINCT [SupplierName] from tblSupplierMaster where " + "SupplierName like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> SupplierNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        SupplierNames.Add(sdr["SupplierName"].ToString());
                    }
                }
                con.Close();
                return SupplierNames;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetTransportList(string prefixText, int count)
    {
        return AutoFillTransport(prefixText);
    }

    public static List<string> AutoFillTransport(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [TransportMode] from tblPurchaseBillHdr where " + "TransportMode like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> TransportModes = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        TransportModes.Add(sdr["TransportMode"].ToString());
                    }
                }
                con.Close();
                return TransportModes;
            }
        }
    }

    protected void BindPO()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT Id,PONo FROM tblPurchaseOrderHdr where SupplierName='" + txtSupplierName.Text.Trim() + "' and IsClosed is null", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlAgainstNumber.DataSource = dt;
            ddlAgainstNumber.DataBind();
            ddlAgainstNumber.DataTextField = "PONo";
            ddlAgainstNumber.DataValueField = "Id";
            ddlAgainstNumber.DataBind();
        }
        else
        {

        }
        ddlAgainstNumber.Items.Insert(0, new ListItem("--Select Order--", "0"));
    }

    protected void ddlBillAgainst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillAgainst.SelectedItem.Text == "Order")
        {
            if (txtSupplierName.Text != "")
            {
                BindPO();
                ddlAgainstNumber.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select Supplier Name !!');", true);
            }
        }
        else if (ddlBillAgainst.SelectedItem.Text == "Verbal")
        {
            if (txtSupplierName.Text != "")
            {
                DivVerbal.Visible = true;
                divVerbaldtls.Visible = true;
                ddlAgainstNumber.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select Supplier Name !!');", true);
            }
        }
    }

    protected void ddlAgainstNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdnGrandtotal.Value = "0";
            txtGrandTot.Text = "0";
            txtTCost.Text = "0";
            getOrderDatailsdts();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void getOrderDatailsdts()
    {
        string ID = ddlAgainstNumber.SelectedValue;
        SqlDataAdapter ad = new SqlDataAdapter("select * from tblPurchaseOrderDtls where HeaderID='" + ID.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dgvOrderDtl.DataSource = dt;
            dgvOrderDtl.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('PO Details Not Found !!');", true);
        }

        SqlDataAdapter add = new SqlDataAdapter("select * from tblPurchaseOrderHdr where Id='" + ID.Trim() + "' ", con);
        DataTable dtt = new DataTable();
        add.Fill(dtt);
        if (dtt.Rows.Count > 0)
        {
            txtTCharge.Text = dtt.Rows[0]["TransportationCharges"].ToString();
            txtTCGSTPer.Text = dtt.Rows[0]["TCGSTPer"].ToString();
            txtTCGSTamt.Text = dtt.Rows[0]["TCGSTAmt"].ToString();
            txtTSGSTPer.Text = dtt.Rows[0]["TSGSTPer"].ToString();
            txtTSGSTamt.Text = dtt.Rows[0]["TSGSTAmt"].ToString();
            txtTIGSTPer.Text = dtt.Rows[0]["TIGSTPer"].ToString();
            txtTIGSTamt.Text = dtt.Rows[0]["TIGSTAmt"].ToString();
            txtTCost.Text = dtt.Rows[0]["TotalCost"].ToString();

            var tot = Convert.ToDouble(txtGrandTot.Text) + Convert.ToDouble(txtTCost.Text);
            txtGrandTot.Text = tot.ToString();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('PO Details Not Found !!');", true);
        }
    }

    decimal grandtotalt = 0;
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgvOrderDtl.Rows)
        {
            TextBox txtqty = (TextBox)row.FindControl("txtQty");
            TextBox UOM = (TextBox)row.FindControl("txtUOM");
            TextBox rate = (TextBox)row.FindControl("txtRate");
            Label Amount = (Label)row.FindControl("txtAmount");
            TextBox cgst = (TextBox)row.FindControl("txtCGST");
            TextBox sgst = (TextBox)row.FindControl("txtSGST");
            TextBox Igst = (TextBox)row.FindControl("txtIGST");
            TextBox discount = (TextBox)row.FindControl("txtdiscount");
            TextBox grandtotal = (TextBox)row.FindControl("txtGrandTotal");
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            //CheckBox chkheader = (CheckBox)dgvOrderDtl.HeaderRow.FindControl("chkHeader");
            if (chk != null & chk.Checked)
            {
                Double qty1 = Convert.ToDouble(txtqty.Text);

                if (qty1 == 0)
                {
                    txtqty.Enabled = false;
                    UOM.Enabled = false;
                    rate.Enabled = false;
                    Amount.Enabled = false;
                    cgst.Enabled = false;
                    sgst.Enabled = false;
                    Igst.Enabled = false;
                    discount.Enabled = false;
                    chk.Enabled = false;
                }
                else
                {
                    txtqty.Enabled = true;
                    UOM.Enabled = false;
                    rate.Enabled = false;
                    Amount.Enabled = false;
                    cgst.Enabled = false;
                    sgst.Enabled = false;
                    Igst.Enabled = false;
                    discount.Enabled = false;
                    chk.Enabled = true;
                    //Double price1 = Convert.ToDouble(rate.Text);
                    //total = (qty1 * price1);
                    //Amount.Text = total.ToString();


                    //Totalamt += Convert.ToDecimal((e.Row.FindControl("txtAmount") as Label).Text);
                    //GrandTotalamt += Convert.ToDecimal((e.Row.FindControl("txtGrandTotal") as TextBox).Text);
                    //hdnGrandtotal.Value = GrandTotalamt.ToString();
                    //sumofAmount.Text = Totalamt.ToString();

                    //var Total = Convert.ToDecimal(txtCost.Text) + GrandTotalamt + Convert.ToDecimal(txtTCSAmt.Text) + Convert.ToDecimal(txtTCost.Text);
                    //txtGrandTot.Text = Total.ToString("##.00");


                    Totalamt += Convert.ToDecimal(Amount.Text);
                    sumofAmount.Text = Totalamt.ToString();
                    grandtotalt += Convert.ToDecimal(grandtotal.Text);
                    //FinalGrandtotalTcs += grandtotalt;
                    ////txtGrandTot.Text = FinalGrandtotalTcs.ToString();
                    hdnGrandtotal.Value = grandtotalt.ToString();
                    var Total = Convert.ToDecimal(txtCost.Text) + grandtotalt + Convert.ToDecimal(txtTCSAmt.Text) + Convert.ToDecimal(txtTCost.Text);
                    txtGrandTot.Text = Total.ToString("##.00");
                }
            }
            else
            {
                txtqty.Enabled = false;
                UOM.Enabled = false;
                rate.Enabled = false;
                Amount.Enabled = false;
                cgst.Enabled = false;
                sgst.Enabled = false;
                Igst.Enabled = false;
                discount.Enabled = false;
            }
        }
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
        decimal Amount = 0;
        decimal GrandTotal = 0;
        foreach (GridViewRow rows in dgvOrderDtl.Rows)
        {
            bool chk = (rows.FindControl("chkSelect") as CheckBox).Checked;
            if (chk == true)
            {
                Amount += Convert.ToDecimal(((Label)rows.FindControl("txtAmount")).Text);
                GrandTotal += Convert.ToDecimal(((TextBox)rows.FindControl("txtGrandTotal")).Text);
            }
        }
        sumofAmount.Text = Amount.ToString();
        txtGrandTot.Text = GrandTotal.ToString();
        Session["Iscompleted"] = "false";
    }

    private void calculationA(GridViewRow row)
    {
        TextBox txt_Qty = (TextBox)row.FindControl("txtQty");
        TextBox txt_price = (TextBox)row.FindControl("txtRate");
        TextBox txt_CGST = (TextBox)row.FindControl("txtCGST");
        TextBox txt_SGST = (TextBox)row.FindControl("txtSGST");
        TextBox txt_IGST = (TextBox)row.FindControl("txtIGST");
        Label txtBasicAmount = (Label)row.FindControl("txtAmount");
        TextBox txt_amount = (TextBox)row.FindControl("txtGrandTotal");
        TextBox txt_discount = (TextBox)row.FindControl("txtdiscount");

        var totalamt = Convert.ToDecimal(txt_Qty.Text.Trim()) * Convert.ToDecimal(txt_price.Text.Trim());

        decimal AmtWithDiscount;
        if (txt_discount.Text != "")
        {
            var disc = totalamt * (Convert.ToDecimal(txt_discount.Text.Trim())) / 100;
            AmtWithDiscount = totalamt - disc;
        }
        else
        {
            AmtWithDiscount = totalamt + 0;
        }
        txtBasicAmount.Text = AmtWithDiscount.ToString();

        var CGSTamt = AmtWithDiscount * (Convert.ToDecimal(txt_CGST.Text.Trim())) / 100;
        var SGSTamt = AmtWithDiscount * (Convert.ToDecimal(txt_SGST.Text.Trim())) / 100;
        var IGSTamt = AmtWithDiscount * (Convert.ToDecimal(txt_IGST.Text.Trim())) / 100;
        decimal GSTtotal = 0;
        if (IGSTamt == 0)
        {
            GSTtotal = SGSTamt + CGSTamt;
        }
        else
        {
            GSTtotal = IGSTamt;
        }

        var NetAmt = AmtWithDiscount + GSTtotal;

        txt_amount.Text = Math.Round(NetAmt).ToString();
    }

    protected void txtCGST_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);

        decimal Amount = 0;
        decimal GrandTotal = 0;
        foreach (GridViewRow rows in dgvOrderDtl.Rows)
        {
            Amount += Convert.ToDecimal(((Label)rows.FindControl("txtAmount")).Text);
            GrandTotal += Convert.ToDecimal(((TextBox)rows.FindControl("txtGrandTotal")).Text);
        }
        sumofAmount.Text = Amount.ToString();
        txtGrandTot.Text = GrandTotal.ToString();
    }

    protected void txtSGST_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);

        decimal Amount = 0;
        decimal GrandTotal = 0;
        foreach (GridViewRow rows in dgvOrderDtl.Rows)
        {
            Amount += Convert.ToDecimal(((Label)rows.FindControl("txtAmount")).Text);
            GrandTotal += Convert.ToDecimal(((TextBox)rows.FindControl("txtGrandTotal")).Text);
        }
        sumofAmount.Text = Amount.ToString();
        txtGrandTot.Text = GrandTotal.ToString();
    }

    protected void txtIGST_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);

        decimal Amount = 0;
        decimal GrandTotal = 0;
        foreach (GridViewRow rows in dgvOrderDtl.Rows)
        {
            Amount += Convert.ToDecimal(((Label)rows.FindControl("txtAmount")).Text);
            GrandTotal += Convert.ToDecimal(((TextBox)rows.FindControl("txtGrandTotal")).Text);
        }
        sumofAmount.Text = Amount.ToString();
        txtGrandTot.Text = GrandTotal.ToString();
    }

    protected void txtdiscount_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);

        decimal Amount = 0;
        decimal GrandTotal = 0;
        foreach (GridViewRow rows in dgvOrderDtl.Rows)
        {
            Amount += Convert.ToDecimal(((Label)rows.FindControl("txtAmount")).Text);
            GrandTotal += Convert.ToDecimal(((TextBox)rows.FindControl("txtGrandTotal")).Text);
        }
        sumofAmount.Text = Amount.ToString();
        txtGrandTot.Text = GrandTotal.ToString();
    }

    decimal Totalamt = 0;
    decimal GrandTotalamt = 0;
    protected void dgvOrderDtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txt_Qty = (TextBox)e.Row.FindControl("txtQty");
            Label id = (Label)e.Row.FindControl("lblID");
            int Id = Convert.ToInt32(id.Text);

            TextBox txt_price = (TextBox)e.Row.FindControl("txtRate");
            TextBox txt_CGST = (TextBox)e.Row.FindControl("txtCGST");
            TextBox txt_SGST = (TextBox)e.Row.FindControl("txtSGST");
            TextBox txt_IGST = (TextBox)e.Row.FindControl("txtIGST");
            Label txtBasicAmount = (Label)e.Row.FindControl("txtAmount");
            TextBox txt_amount = (TextBox)e.Row.FindControl("txtGrandTotal");
            TextBox txt_discount = (TextBox)e.Row.FindControl("txtdiscount");

            if (btnadd.Text == "Submit")
            {



                SqlCommand cmdmax = new SqlCommand("SELECT Qty FROM tblPurchaseBillDtls where POId='" + id.Text + "'", con);
                con.Open();
                Object billQtyid = cmdmax.ExecuteScalar();
                con.Close();

                SqlCommand cmdsumQty = new SqlCommand("SELECT SUM(CAST(Qty as float)) FROM tblPurchaseOrderDtls where Id='" + Id + "'", con);
                con.Open();
                string smQty = cmdsumQty.ExecuteScalar().ToString();
                //sumofqty = Convert.ToInt32(smQty);
                con.Close();
                var PObillqty = billQtyid == null ? "0" : billQtyid.ToString();
                var smquantity = smQty == "" ? "0" : smQty.ToString();
                var minusQty = Convert.ToDecimal(smquantity) - Convert.ToDecimal(PObillqty);
                txt_Qty.Text = minusQty.ToString();
            }

            //Calculation
            var totalamt = Convert.ToDouble(txt_Qty.Text.Trim()) * Convert.ToDouble(txt_price.Text.Trim());
            //txtBasicAmount.Text = totalamt.ToString();

            double AmtWithDiscount;
            if (txt_discount.Text != "")
            {
                var disc = totalamt * (Convert.ToDouble(txt_discount.Text.Trim())) / 100;
                AmtWithDiscount = totalamt - disc;
            }
            else
            {
                AmtWithDiscount = totalamt + 0;
            }
            txtBasicAmount.Text = AmtWithDiscount.ToString();

            var CGSTamt = AmtWithDiscount * (Convert.ToDouble(txt_CGST.Text.Trim())) / 100;
            var SGSTamt = AmtWithDiscount * (Convert.ToDouble(txt_SGST.Text.Trim())) / 100;
            var IGSTamt = AmtWithDiscount * (Convert.ToDouble(txt_IGST.Text.Trim())) / 100;
            double GSTtotal = 0;
            if (IGSTamt == 0)
            {
                GSTtotal = SGSTamt + CGSTamt;
            }
            else
            {
                GSTtotal = IGSTamt;
            }

            var NetAmt = AmtWithDiscount + GSTtotal;

            txt_amount.Text = Math.Round(NetAmt).ToString();

            if (txt_Qty.Text == "0")
            {
                e.Row.Visible = false;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Totalamt += Convert.ToDecimal((e.Row.FindControl("txtAmount") as Label).Text);
            GrandTotalamt += Convert.ToDecimal((e.Row.FindControl("txtGrandTotal") as TextBox).Text);
            //hdnGrandtotal.Value = GrandTotalamt.ToString();
            //sumofAmount.Text = Totalamt.ToString();

            var Total = Convert.ToDecimal(txtCost.Text) + GrandTotalamt + Convert.ToDecimal(txtTCSAmt.Text) + Convert.ToDecimal(txtTCost.Text);
            // txtGrandTot.Text = Total.ToString("##.00");
        }
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        string Amt = sumofAmount.Text;
        string Rate = txtRate.Text;
        if (Rate == "0")
        {
            txtBasic.Text = "0";
            txtCost.Text = "0";
            CGSTPer.Text = "0";
            SGSTPer.Text = "0";
            IGSTPer.Text = "0";
        }
        else
        {
            var Basic = Convert.ToDecimal(Amt) * Convert.ToDecimal(Rate) / 100;
            txtBasic.Text = Basic.ToString("##.00");

            var grandtot = Convert.ToDecimal(Basic) + Convert.ToDecimal(hdnGrandtotal.Value) + Convert.ToDecimal(txtTCost.Text);
            txtGrandTot.Text = grandtot.ToString("##.00");
        }
    }

    protected void CGSTPer_TextChanged(object sender, EventArgs e)
    {
        GstCalculation();
        var grandtot = Convert.ToDouble(txtCost.Text) + Convert.ToDouble(hdnGrandtotal.Value) + Convert.ToDouble(txtTCost.Text);
        txtGrandTot.Text = grandtot.ToString("##.00");
    }

    protected void SGSTPer_TextChanged(object sender, EventArgs e)
    {
        GstCalculation();
        var grandtot = Convert.ToDouble(txtCost.Text) + Convert.ToDouble(hdnGrandtotal.Value) + Convert.ToDouble(txtTCost.Text);
        txtGrandTot.Text = grandtot.ToString("##.00");
    }

    protected void GstCalculation()
    {
        string Basic = txtBasic.Text;
        string CGST = CGSTPer.Text;
        string SGST = SGSTPer.Text;
        if (CGST == "0" || SGST == "0")
        {
            if (CGST == "0" && SGST == "0" && IGSTPer.Text == "0")
            {
                IGSTPer.Enabled = true;
                CGSTPer.Enabled = true;
                SGSTPer.Enabled = true;
                txtCost.Text = Basic.ToString();
            }
            else
            {
                if (IGSTPer.Text == "0")
                {
                    IGSTPer.Enabled = false;
                    CGSTPer.Enabled = true;
                    SGSTPer.Enabled = true;
                    var CGSTAmt = Convert.ToDecimal(Basic) * Convert.ToDecimal(CGST) / 100;
                    var SGSTAmt = Convert.ToDecimal(Basic) * Convert.ToDecimal(SGST) / 100;
                    var GSTTaxTotal = Convert.ToDecimal(Basic) + CGSTAmt + SGSTAmt;
                    txtCost.Text = GSTTaxTotal.ToString("##.00");

                    var grandtot = Convert.ToDecimal(GSTTaxTotal) + Convert.ToDecimal(hdnGrandtotal.Value);
                    txtGrandTot.Text = grandtot.ToString("##.00");
                }
                else
                {
                    IGSTPer.Enabled = true;
                    CGSTPer.Enabled = false;
                    SGSTPer.Enabled = false;
                    var IGSTAmt = Convert.ToDecimal(Basic) * Convert.ToDecimal(IGSTPer.Text) / 100;
                    var GSTTaxTotal = Convert.ToDecimal(Basic) + IGSTAmt;
                    txtCost.Text = GSTTaxTotal.ToString("##.00");

                    var grandtot = Convert.ToDecimal(GSTTaxTotal) + Convert.ToDecimal(hdnGrandtotal.Value);
                    txtGrandTot.Text = grandtot.ToString("##.00");
                }
            }
        }
        else
        {
            IGSTPer.Enabled = false;
            CGSTPer.Enabled = true;
            SGSTPer.Enabled = true;
            var CGSTAmt = Convert.ToDecimal(Basic) * Convert.ToDecimal(CGST) / 100;
            var SGSTAmt = Convert.ToDecimal(Basic) * Convert.ToDecimal(SGST) / 100;

            var GSTTaxTotal = Convert.ToDecimal(Basic) + CGSTAmt + SGSTAmt;
            txtCost.Text = GSTTaxTotal.ToString("##.00");

            var grandtot = Convert.ToDecimal(GSTTaxTotal) + Convert.ToDecimal(hdnGrandtotal.Value);
            txtGrandTot.Text = grandtot.ToString("##.00");
        }
    }

    protected void IGSTPer_TextChanged(object sender, EventArgs e)
    {
        GstCalculation();
    }

    //protected void txtTCSPer_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtTCSPer.Text == "0" || txtTCSPer.Text == "")
    //    {
    //        var tot = Convert.ToDecimal(sumofAmount.Text) + Convert.ToDecimal(txtCost.Text);
    //        var TcsAmt = Convert.ToDecimal(txtTCSPer.Text) * tot / 100;
    //        txtTCSAmt.Text = TcsAmt.ToString("##.00");

    //        var grandtot = Convert.ToDecimal(txtTCSAmt.Text) + Convert.ToDecimal(hdnGrandtotal.Value) + Convert.ToDecimal(txtCost.Text);
    //        txtGrandTot.Text = grandtot.ToString("##.00");
    //        txtTCSAmt.Text = "0";
    //    }
    //    else
    //    {
    //        var tot = Convert.ToDecimal(sumofAmount.Text) + Convert.ToDecimal(txtCost.Text);
    //        var TcsAmt = Convert.ToDecimal(txtTCSPer.Text) * tot / 100;
    //        txtTCSAmt.Text = TcsAmt.ToString("##.00");

    //        var grandtot = Convert.ToDecimal(txtTCSAmt.Text) + Convert.ToDecimal(hdnGrandtotal.Value) + Convert.ToDecimal(txtCost.Text);
    //        txtGrandTot.Text = grandtot.ToString("##.00");
    //    }
    //}

    protected void txtTCSPer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtTCSPer.Text == "0" || txtTCSPer.Text == "")
        {
             var tot = Convert.ToDecimal(txtGrandTot.Text) + Convert.ToDecimal(txtCost.Text);
            var TcsAmt = Convert.ToDecimal(txtTCSPer.Text) * tot / 100;
            txtTCSAmt.Text = TcsAmt.ToString("##.00");

            var grandtot = Convert.ToDecimal(txtTCSAmt.Text) + Convert.ToDecimal(txtGrandTot.Text) + Convert.ToDecimal(txtCost.Text) + Convert.ToDecimal(txtTCost.Text);
            txtGrandTot.Text = grandtot.ToString("##.00");
            txtTCSAmt.Text = "0";
        }
        else
        {
            var tot = Convert.ToDecimal(txtGrandTot.Text) + Convert.ToDecimal(txtCost.Text); //hdnGrandtotal.Value
            var TcsAmt = Convert.ToDecimal(txtTCSPer.Text) * tot / 100;
            txtTCSAmt.Text = TcsAmt.ToString("##.00");

            var grandtot = Convert.ToDecimal(txtTCSAmt.Text) + Convert.ToDecimal(txtGrandTot.Text) + Convert.ToDecimal(txtCost.Text) + Convert.ToDecimal(txtTCost.Text);
            txtGrandTot.Text = grandtot.ToString("##.00");
        }
    }

    protected void txtBasic_TextChanged(object sender, EventArgs e)
    {
        string Amt = sumofAmount.Text;
        string Basic = txtBasic.Text;
        if (Basic == "0")
        {
            txtBasic.Text = "0";
            txtCost.Text = "0";
            CGSTPer.Text = "0";
            SGSTPer.Text = "0";
            IGSTPer.Text = "0";
        }
        else
        {
            var Per = Convert.ToDouble(Basic) / Convert.ToDouble(Amt) * 100;
            txtRate.Text = Per.ToString("##.00");

            var grandtot = Convert.ToDouble(Basic) + Convert.ToDouble(hdnGrandtotal.Value) + Convert.ToDouble(txtTCost.Text);
            txtGrandTot.Text = grandtot.ToString("##.00");
        }
    }

    //17 march 2022
    private void Transportation_Calculation()
    {
        var TotalAmt = Convert.ToDecimal(txtTCharge.Text.Trim());

        decimal CGST;
        if (string.IsNullOrEmpty(txtTCGSTPer.Text))
        {
            CGST = 0;
        }
        else
        {
            decimal Val1 = Convert.ToDecimal(txtTCharge.Text.Trim());
            decimal Val2 = Convert.ToDecimal(txtTCGSTPer.Text);

            CGST = (Val1 * Val2 / 100);
        }
        txtTCGSTamt.Text = CGST.ToString("0.00", CultureInfo.InvariantCulture);

        decimal SGST;
        if (string.IsNullOrEmpty(txtTSGSTPer.Text))
        {
            SGST = 0;
        }
        else
        {
            decimal Val1 = Convert.ToDecimal(txtTCharge.Text);
            decimal Val2 = Convert.ToDecimal(txtTSGSTPer.Text);

            SGST = (Val1 * Val2 / 100);
        }
        txtTSGSTamt.Text = SGST.ToString("0.00", CultureInfo.InvariantCulture);


        decimal IGST;
        if (string.IsNullOrEmpty(txtTIGSTPer.Text))
        {
            IGST = 0;
        }
        else
        {
            decimal Val1 = Convert.ToDecimal(txtTCharge.Text);
            decimal Val2 = Convert.ToDecimal(txtTIGSTPer.Text);

            IGST = (Val1 * Val2 / 100);
        }
        txtTIGSTamt.Text = IGST.ToString("0.00", CultureInfo.InvariantCulture);

        var GSTTotal = CGST + SGST + IGST;

        var Finalresult = Convert.ToDecimal(txtTCharge.Text) + GSTTotal;

        txtTCost.Text = Finalresult.ToString("0.00", CultureInfo.InvariantCulture);


    }

    protected void txtTCharge_TextChanged(object sender, EventArgs e)
    {
        Transportation_Calculation();
    }

    protected void txtTCGSTPer_TextChanged(object sender, EventArgs e)
    {
        if (txtTCGSTPer.Text != "0")
        {
            txtTIGSTPer.Enabled = false;
            txtTSGSTPer.Text = txtTCGSTPer.Text;
        }
        else
        {
            txtTIGSTPer.Enabled = true;
            txtTSGSTPer.Text = "0";
        }
        Transportation_Calculation();

        var TotalGrand = Convert.ToDouble(txtGrandTot.Text) + Convert.ToDouble(txtTCost.Text);
        txtGrandTot.Text = TotalGrand.ToString("0.00", CultureInfo.InvariantCulture);
    }

    protected void txtTSGSTPer_TextChanged(object sender, EventArgs e)
    {
        if (txtTSGSTPer.Text != "0")
        {
            txtTIGSTPer.Enabled = false;
        }
        else
        {
            txtTIGSTPer.Enabled = true;
        }
        Transportation_Calculation();
    }

    protected void txtTIGSTPer_TextChanged(object sender, EventArgs e)
    {
        if (txtTIGSTPer.Text != "0")
        {
            txtTSGSTPer.Enabled = false;
            txtTCGSTPer.Enabled = false;
        }
        else
        {
            txtTSGSTPer.Enabled = true;
            txtTCGSTPer.Enabled = true;
        }
        Transportation_Calculation();

        var TotalGrand = Convert.ToDouble(txtGrandTot.Text) + Convert.ToDouble(txtTCost.Text);
        hdnGrandtotal.Value = TotalGrand.ToString("0.00", CultureInfo.InvariantCulture);
        txtGrandTot.Text = hdnGrandtotal.Value;
    }

    protected void txtDOR_TextChanged(object sender, EventArgs e)
    {
        DateTime fromdate = Convert.ToDateTime(txtBilldate.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
        DateTime todate = Convert.ToDateTime(txtDOR.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
        if (fromdate > todate)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bill Date is greater than Received Date...Please Choose Correct Date.');", true);
            btnadd.Enabled = false;
        }
        else
        {
            btnadd.Enabled = true;
        }
    }

    protected void Insert(object sender, EventArgs e)
    {
        if (txtVQty.Text == "" || txtParticulars.Text == "" || txtVTotalamt.Text == "")
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
        ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;
        DataTable dt = (DataTable)ViewState["VParticularDetails"];

        dt.Rows.Add(ViewState["RowNo"], txtParticulars.Text, txtVHSN.Text, txtVQty.Text, txtVRate.Text, txtVDisc.Text, txtVAmount.Text, txtVCGSTPer.Text, txtVCGSTAmt.Text, txtVSGSTPer.Text, txtVSGSTAmt.Text, txtVIGSTPer.Text, txtVIGSTAmt.Text, txtVTotalamt.Text, txtVDescription.Text, txtUOM.Text);
        ViewState["VParticularDetails"] = dt;

        dgvParticularsDetails.DataSource = (DataTable)ViewState["VParticularDetails"];
        dgvParticularsDetails.DataBind();

        txtParticulars.Text = string.Empty;
        txtVQty.Text = string.Empty;
        txtVHSN.Text = string.Empty;
        txtVRate.Text = string.Empty;
        txtVDisc.Text = "0";
        txtVAmount.Text = string.Empty;
        txtVCGSTPer.Text = string.Empty;
        txtVCGSTAmt.Text = string.Empty;
        txtVSGSTPer.Text = string.Empty;
        txtVSGSTAmt.Text = string.Empty;
        txtVIGSTPer.Text = string.Empty;
        txtVIGSTAmt.Text = string.Empty;
        txtVTotalamt.Text = string.Empty;
        txtVDescription.Text = string.Empty;
        //txtUOM.SelectedItem.Text = string.Empty;


        decimal Amount = 0;
        decimal GrandTotal = 0;
        foreach (GridViewRow rows in dgvParticularsDetails.Rows)
        {
            Amount += Convert.ToDecimal(((Label)rows.FindControl("lblAmount")).Text);
            GrandTotal += Convert.ToDecimal(((Label)rows.FindControl("lblTotalAmount")).Text);
        }
        sumofAmount.Text = Amount.ToString();
        txtGrandTot.Text = (GrandTotal + Convert.ToDecimal(txtCost.Text) + Convert.ToDecimal(txtTCost.Text)).ToString();
    }

    protected void dgvParticularsDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void dgvParticularsDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dgvParticularsDetails.EditIndex = e.NewEditIndex;
        dgvParticularsDetails.DataSource = (DataTable)ViewState["VParticularDetails"];
        dgvParticularsDetails.DataBind();
		
		 TextBox txtIgst = (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[12].Controls[1] as TextBox);
        var Igst = txtIgst.Text;
        if (Igst == "0")
        {
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[12].Controls[1] as TextBox).Enabled = false;
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[13].Controls[1] as TextBox).Enabled = false;

            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[8].Controls[1] as TextBox).Enabled = true;
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[9].Controls[1] as TextBox).Enabled = true;
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[10].Controls[1] as TextBox).Enabled = true;
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[11].Controls[1] as TextBox).Enabled = true;
        }
        else
        {
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[12].Controls[1] as TextBox).Enabled = true;
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[13].Controls[1] as TextBox).Enabled = true;

            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[8].Controls[1] as TextBox).Enabled = false;
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[9].Controls[1] as TextBox).Enabled = false;
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[10].Controls[1] as TextBox).Enabled = false;
            (dgvParticularsDetails.Rows[e.NewEditIndex].Cells[11].Controls[1] as TextBox).Enabled = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void lnkbtnUpdate_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        string Particulars = ((Label)row.FindControl("lblParticulars")).Text;
        string HSN = ((Label)row.FindControl("lblHSN")).Text;
        string Qty = ((TextBox)row.FindControl("txtQty")).Text;
        string Rate = ((Label)row.FindControl("lblRate")).Text;

        string Discount = ((TextBox)row.FindControl("txtPerDiscount")).Text;

        string Amount = ((Label)row.FindControl("lblAmount")).Text;
        string CGSTPer = ((TextBox)row.FindControl("txtCGSTPer")).Text;
        string CGSTAmt = ((TextBox)row.FindControl("txtCGSTAmt")).Text;
        string SGSTPer = ((TextBox)row.FindControl("txtSGSTPer")).Text;
        string SGSTAmt = ((TextBox)row.FindControl("txtSGSTAmt")).Text;
        string IGSTPer = ((TextBox)row.FindControl("txtIGSTPer")).Text;
        string IGSTAmt = ((TextBox)row.FindControl("txtIGSTAmt")).Text;
        string TotalAmount = ((TextBox)row.FindControl("txtTotalAmount")).Text;
        string Description = ((TextBox)row.FindControl("txttblDescription")).Text;
        string UOM = ((TextBox)row.FindControl("txtUOM")).Text;

        DataTable Dt = ViewState["VParticularDetails"] as DataTable;

        Dt.Rows[row.RowIndex]["Particulars"] = Particulars;
        Dt.Rows[row.RowIndex]["HSN"] = HSN;
        Dt.Rows[row.RowIndex]["Qty"] = Qty;
        Dt.Rows[row.RowIndex]["Rate"] = Rate;
        Dt.Rows[row.RowIndex]["Discount"] = Discount;
        Dt.Rows[row.RowIndex]["Amount"] = Amount;
        Dt.Rows[row.RowIndex]["CGSTPer"] = CGSTPer;
        Dt.Rows[row.RowIndex]["CGSTAmt"] = CGSTAmt;
        Dt.Rows[row.RowIndex]["SGSTPer"] = SGSTPer;
        Dt.Rows[row.RowIndex]["SGSTAmt"] = SGSTAmt;
        Dt.Rows[row.RowIndex]["IGSTPer"] = IGSTPer;
        Dt.Rows[row.RowIndex]["IGSTAmt"] = IGSTAmt;
        Dt.Rows[row.RowIndex]["TotalAmount"] = TotalAmount;
        Dt.Rows[row.RowIndex]["Description"] = Description;
        Dt.Rows[row.RowIndex]["UOM"] = UOM;

        Dt.AcceptChanges();

        ViewState["VParticularDetails"] = Dt;
        dgvParticularsDetails.EditIndex = -1;

        dgvParticularsDetails.DataSource = (DataTable)ViewState["VParticularDetails"];
        dgvParticularsDetails.DataBind();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        DataTable dt = ViewState["VParticularDetails"] as DataTable;
        dt.Rows.Remove(dt.Rows[row.RowIndex]);
        ViewState["VParticularDetails"] = dt;
        dgvParticularsDetails.DataSource = (DataTable)ViewState["VParticularDetails"];
        dgvParticularsDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Delete Succesfully !!!');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);

    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        DataTable Dt = ViewState["VParticularDetails"] as DataTable;
        dgvParticularsDetails.EditIndex = -1;

        ViewState["VParticularDetails"] = Dt;
        dgvParticularsDetails.EditIndex = -1;

        dgvParticularsDetails.DataSource = (DataTable)ViewState["VParticularDetails"];
        dgvParticularsDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);

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
                com.CommandText = "Select DISTINCT [ItemName] from tblItemMaster where " + "ItemName like '%'+ @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                //com.Parameters.AddWithValue("@SName", sName);
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetUOMList(string prefixText, int count)
    {
        return AutoFilUOM(prefixText);
    }

    public static List<string> AutoFilUOM(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [StorageUnit] from tblItemMaster where " + "StorageUnit like '%' + @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> StorageUnit = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        StorageUnit.Add(sdr["StorageUnit"].ToString());
                    }
                }
                con.Close();
                return StorageUnit;
            }
        }
    }

    protected void txtParticulars_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM tblItemMaster where ItemName='" + txtParticulars.Text.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtVHSN.Text = dt.Rows[0]["HSN"].ToString() == "" ? "0" : dt.Rows[0]["HSN"].ToString();
            txtVRate.Text = dt.Rows[0]["PurchaseRate"].ToString() == "" ? "0" : dt.Rows[0]["PurchaseRate"].ToString();
            txtVCGSTPer.Text = dt.Rows[0]["CGST"].ToString() == "" ? "0" : dt.Rows[0]["CGST"].ToString();
            txtVSGSTPer.Text = dt.Rows[0]["SGST"].ToString() == "" ? "0" : dt.Rows[0]["SGST"].ToString();
            txtVIGSTPer.Text = dt.Rows[0]["IGST"].ToString() == "" ? "0" : dt.Rows[0]["IGST"].ToString();
			txtUOM.SelectedValue = dt.Rows[0]["StorageUnit"].ToString() == "" ? "0" : dt.Rows[0]["StorageUnit"].ToString();
            txtVDescription.Text = dt.Rows[0]["ItemDescription"].ToString() == "" ? "0" : dt.Rows[0]["ItemDescription"].ToString();
        }
        else
        {

        }
    }

    private void GST_Calculation()
    {
        var TotalAmt = Convert.ToDecimal(txtVQty.Text.Trim()) * Convert.ToDecimal(txtVRate.Text.Trim());

        decimal disc;
        if (string.IsNullOrEmpty(txtVDisc.Text))
        {
            disc = 0;
            txtVAmount.Text = TotalAmt.ToString("0.00", CultureInfo.InvariantCulture);
        }
        else
        {
            decimal Val1 = Convert.ToDecimal(TotalAmt);
            decimal Val2 = Convert.ToDecimal(txtVDisc.Text);
            disc = (Val1 * Val2 / 100);
            var result = Val1 - disc;
            txtVAmount.Text = result.ToString("0.00", CultureInfo.InvariantCulture);
        }

        decimal CGST;
        if (string.IsNullOrEmpty(txtVCGSTPer.Text))
        {
            CGST = 0;
        }
        else
        {
            decimal Val1 = Convert.ToDecimal(txtVAmount.Text);
            decimal Val2 = Convert.ToDecimal(txtVCGSTPer.Text);
			txtVSGSTPer.Text = txtVCGSTPer.Text;

            CGST = (Val1 * Val2 / 100);

        }
        txtVCGSTAmt.Text = CGST.ToString("0.00", CultureInfo.InvariantCulture);

        decimal SGST;
        if (string.IsNullOrEmpty(txtVSGSTPer.Text))
        {
            SGST = 0;
        }
        else
        {
            decimal Val1 = Convert.ToDecimal(txtVAmount.Text);
            decimal Val2 = Convert.ToDecimal(txtVSGSTPer.Text);

            SGST = (Val1 * Val2 / 100);
        }
        txtVSGSTAmt.Text = SGST.ToString("0.00", CultureInfo.InvariantCulture);


        decimal IGST;
        if (txtVIGSTPer.Text=="0")
        {
            IGST = 0;
            txtVIGSTPer.Enabled = false;
            txtVIGSTAmt.Enabled = false;

            txtVCGSTPer.Enabled = true;
            txtVCGSTAmt.Enabled = true;

            txtVSGSTPer.Enabled = true;
            txtVSGSTAmt.Enabled = true;
        }
        else
        {
            decimal Val1 = Convert.ToDecimal(txtVAmount.Text);
            decimal Val2 = Convert.ToDecimal(txtVIGSTPer.Text);

            IGST = (Val1 * Val2 / 100);

            txtVIGSTPer.Enabled = true;
            txtVIGSTAmt.Enabled = true;

            txtVCGSTPer.Enabled = false;
            txtVCGSTAmt.Enabled = false;

            txtVSGSTPer.Enabled = false;
            txtVSGSTAmt.Enabled = false;


        }
        txtVIGSTAmt.Text = IGST.ToString("0.00", CultureInfo.InvariantCulture);

        var GSTTotal = CGST + SGST + IGST;

        var Finalresult = Convert.ToDecimal(txtVAmount.Text) + GSTTotal;

        txtVTotalamt.Text = Finalresult.ToString("0.00", CultureInfo.InvariantCulture);
    }

    private void GRID_GST_Calculation(GridViewRow row)
    {
        string Particulars = ((Label)row.FindControl("lblParticulars")).Text;
        string HSN = ((Label)row.FindControl("lblHSN")).Text;
        string Qty = ((TextBox)row.FindControl("txtQty")).Text;
        string Rate = ((Label)row.FindControl("lblRate")).Text;
        TextBox Discount = ((TextBox)row.FindControl("txtPerDiscount"));
        Label Amount = ((Label)row.FindControl("lblAmount"));
        string CGSTPer = ((TextBox)row.FindControl("txtCGSTPer")).Text;
        TextBox CGSTAmt = (TextBox)row.FindControl("txtCGSTAmt");
        string SGSTPer = ((TextBox)row.FindControl("txtSGSTPer")).Text;
        TextBox SGSTAmt = (TextBox)row.FindControl("txtSGSTAmt");
        string IGSTPer = ((TextBox)row.FindControl("txtIGSTPer")).Text;
        TextBox IGSTAmt = (TextBox)row.FindControl("txtIGSTAmt");
        TextBox TotalAmount = (TextBox)row.FindControl("txtTotalAmount");

        var totalamt = Convert.ToDecimal(Qty) * Convert.ToDecimal(Rate);
        string Tot = "";

        decimal disc;
        if (string.IsNullOrEmpty(Discount.Text))
        {
            disc = 0;
            Amount.Text = totalamt.ToString("0.00", CultureInfo.InvariantCulture);
        }
        else
        {
            decimal val1 = Convert.ToDecimal(totalamt);
            decimal val2 = Convert.ToDecimal(Discount.Text);

            disc = (val1 * val2 / 100);
            var result = val1 - disc;
            Amount.Text = result.ToString("0.00", CultureInfo.InvariantCulture);
        }


        decimal Vcgst;
        if (string.IsNullOrEmpty(CGSTAmt.Text))
        {
            Vcgst = 0;
        }
        else
        {
            decimal val1 = Convert.ToDecimal(Amount.Text);
            decimal val2 = Convert.ToDecimal(CGSTPer);

            Vcgst = (val1 * val2 / 100);
        }
        CGSTAmt.Text = Vcgst.ToString("0.00", CultureInfo.InvariantCulture);

        decimal Vsgst;
        if (string.IsNullOrEmpty(SGSTAmt.Text))
        {
            Vsgst = 0;
        }
        else
        {
            decimal val1 = Convert.ToDecimal(Amount.Text);
            decimal val2 = Convert.ToDecimal(CGSTPer);

            Vsgst = (val1 * val2 / 100);
        }
        SGSTAmt.Text = Vsgst.ToString("0.00", CultureInfo.InvariantCulture);

        decimal Vigst;
        if (string.IsNullOrEmpty(IGSTAmt.Text))
        {
            Vigst = 0;
        }
        else
        {
            decimal val1 = Convert.ToDecimal(Amount.Text);
            decimal val2 = Convert.ToDecimal(IGSTPer);

            Vigst = (val1 * val2 / 100);
        }
        IGSTAmt.Text = Vigst.ToString("0.00", CultureInfo.InvariantCulture);

        var GSTTotal = Vcgst + Vsgst + Vigst;

        var taxamt = Convert.ToDecimal(Amount.Text) + GSTTotal;

        TotalAmount.Text = taxamt.ToString("0.00", CultureInfo.InvariantCulture);
    }

    protected void txtQty_TextChanged1(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        GRID_GST_Calculation(row);
    }

    protected void txtCGSTPer_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        GRID_GST_Calculation(row);
    }

    protected void txtSGSTPer_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        GRID_GST_Calculation(row);
    }

    protected void txtIGSTPer_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        GRID_GST_Calculation(row);
    }

    protected void txtPerDiscount_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        GRID_GST_Calculation(row);
    }

    protected void txtVDisc_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();
    }

    protected void txtVQty_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();
    }

    protected void txtVRate_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();
    }

    protected void txtVCGSTPer_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();
    }

    protected void txtVSGSTPer_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();
    }

    protected void txtVIGSTPer_TextChanged(object sender, EventArgs e)
    {
        GST_Calculation();
    }

    decimal VTotalamt = 0;
    protected void dgvParticularsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (btnadd.Text == "Update")
            {

                if (ddlBillAgainst.Text == "Verbal")
                {
                    Label txtAmount = (Label)e.Row.FindControl("lblAmount");
                    Label lblCGSTPer = (e.Row.FindControl("lblCGSTPer") as Label);
                    Label lblSGSTPer = (e.Row.FindControl("lblSGSTPer") as Label);
                    Label lblIGSTPer = (e.Row.FindControl("lblIGSTPer") as Label);

                    Label lblCGSTAmt = (e.Row.FindControl("lblCGSTAmt") as Label);
                    Label lblSGSTAmt = (e.Row.FindControl("lblSGSTAmt") as Label);
                    Label lblIGSTAmt = (e.Row.FindControl("lblIGSTAmt") as Label);

                    var cgstamt = Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(lblCGSTPer.Text == "" ? "0" : lblCGSTPer.Text) / 100;
                    var sgstamt = Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(lblSGSTPer.Text == "" ? "0" : lblSGSTPer.Text) / 100;
                    var igstamt = Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(lblIGSTPer.Text == "" ? "0" : lblIGSTPer.Text) / 100;

                    lblCGSTAmt.Text = cgstamt.ToString("#0.00");
                    lblSGSTAmt.Text = sgstamt.ToString("#0.00");
                    lblIGSTAmt.Text = igstamt.ToString("#0.00");
                }
            }

            TextBox txts = (e.Row.FindControl("txtTotalAmount") as TextBox);

            if (txts == null)
            {
                Totalamt += Convert.ToDecimal((e.Row.FindControl("lblTotalAmount") as Label).Text);
                VTotalamt += Convert.ToDecimal((e.Row.FindControl("lblAmount") as Label).Text);
                hdnGrandtotal.Value = Totalamt.ToString();
                sumofAmount.Text = VTotalamt.ToString();
                txtGrandTot.Text = (Totalamt + Convert.ToDecimal(txtCost.Text) + Convert.ToDecimal(txtTCost.Text)).ToString();
            }
            else
            {
                Totalamt += Convert.ToDecimal((e.Row.FindControl("txtTotalAmount") as TextBox).Text);
                VTotalamt += Convert.ToDecimal((e.Row.FindControl("lblAmount") as Label).Text);
                hdnGrandtotal.Value = Totalamt.ToString();
                sumofAmount.Text = VTotalamt.ToString();
                txtGrandTot.Text = (Totalamt + Convert.ToDecimal(txtCost.Text) + Convert.ToDecimal(txtTCost.Text)).ToString();
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lbltotal") as Label).Text = Totalamt.ToString();
        }
    }
}

#line default
#line hidden
