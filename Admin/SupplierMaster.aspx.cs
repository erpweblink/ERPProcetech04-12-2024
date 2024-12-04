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
using System.Net;


public partial class Admin_SupplierMaster : System.Web.UI.Page
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
                //getdata();
                GetCategourylist();
                //fillddlpaymentterm();
                UpdateHistorymsg = string.Empty; regdate = string.Empty;
                GetTaxType();
                if (Request.QueryString["ID"] != null)
                {
                    ViewState["RowNo"] = 0;
                    dt.Columns.AddRange(new DataColumn[6] { new DataColumn("id"),
                 new DataColumn("contactname"), new DataColumn("designation")
                , new DataColumn("contactno"), new DataColumn("notify"), new DataColumn("access")
            });

                    ViewState["ContactDetails"] = dt;

                    ViewState["UpdateRowId"] = Decrypt(Request.QueryString["ID"].ToString());
                    GetSupplierData(ViewState["UpdateRowId"].ToString());
                }
                else
                {
                    ViewState["RowNo"] = 0;
                    dt.Columns.AddRange(new DataColumn[6] { new DataColumn("id"),
                 new DataColumn("contactname"), new DataColumn("designation")
                , new DataColumn("contactno"), new DataColumn("notify"), new DataColumn("access")
            });
                    ViewState["ContactDetails"] = dt;
                    txtsupplierCode.Text = GenerateComCode();
                }
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
    //        ddlPaymentTerm.DataSource = dtpt;
    //        ddlPaymentTerm.DataValueField = "paymentterm";
    //        ddlPaymentTerm.DataTextField = "paymentterm";
    //        ddlPaymentTerm.DataBind();
    //    }
    //}

    static string regdate = string.Empty;
    protected void GetSupplierData(string id)
    {
        string query1 = string.Empty;
        query1 = @"select * from tblSupplierMaster where Id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtSupplierName.Text = dt.Rows[0]["SupplierName"].ToString();
            txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
            txtsupplierCode.Text = dt.Rows[0]["SupplierCode"].ToString();
            txtBillingaddress.Text = dt.Rows[0]["BillToAddress"].ToString();
            txtShippingaddress.Text = dt.Rows[0]["ShipToAddress"].ToString();
            ddlCountry.Text = dt.Rows[0]["Country"].ToString();
            ddlState.Text = dt.Rows[0]["StateName"].ToString();
            ddlRegistrationType.Text = dt.Rows[0]["RegistrationType"].ToString();
            txtGSTNo.Text = dt.Rows[0]["GSTNo"].ToString();
            txtPANNo.Text = dt.Rows[0]["PANNo"].ToString();
            txtPaymentValidity.Text = dt.Rows[0]["PaymentValidity"].ToString();
            //txtSupplierTaxtype.Text = dt.Rows[0]["SupplierTaxType"].ToString();
            ddltxttype.SelectedItem.Text = dt.Rows[0]["SupplierTaxType"].ToString();
            txtSupplierCategory.Text = dt.Rows[0]["SupplierCategory"].ToString();
            txtTradeName.Text = dt.Rows[0]["TradeName"].ToString();
            txtOutstandingLimit.Text = dt.Rows[0]["OutstandingLimit"].ToString();
            txtCreditDays.Text = dt.Rows[0]["PaymentTerm"].ToString();
            txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
            getConatctdts(id);

            btnadd.Text = "Update Supplier";
        }
    }

    protected void getConatctdts(string id)
    {

        DataTable Dtproduct = new DataTable();
        SqlDataAdapter daa = new SqlDataAdapter("select * from tblSupplierContactDtls where HeaderID='" + id + "'", con);
        daa.Fill(Dtproduct);
        ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;

        DataTable dt = ViewState["ContactDetails"] as DataTable;

        if (Dtproduct.Rows.Count > 0)
        {
            for (int i = 0; i < Dtproduct.Rows.Count; i++)
            {
                bool notify, Access;
                if (Dtproduct.Rows[i]["Notify"].ToString() == "True")
                    notify = true;
                else
                    notify = false;

                if (Dtproduct.Rows[i]["Access"].ToString() == "True")
                    Access = true;
                else
                    Access = false;

                dt.Rows.Add(ViewState["RowNo"], Dtproduct.Rows[i]["ContactName"].ToString(), Dtproduct.Rows[i]["Designation"].ToString(), Dtproduct.Rows[i]["ContactNo"].ToString(), notify, Access);
                ViewState["ContactDetails"] = dt;
            }
        }
        dgvContactDetails.DataSource = dt;
        dgvContactDetails.DataBind();
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
        string SupplierCode = string.Empty;
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([Id]) as maxid FROM [tblSupplierMaster]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
            //SupplierCode = "EESup/0" + (maxid + 1).ToString();
            SupplierCode = "S00000" + (maxid + 1).ToString();
        }
        else
        {
            SupplierCode = string.Empty;
        }
        return SupplierCode;
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        #region Insert
        if (btnadd.Text == "Add Supplier")
        {
            bool flg = false;
            if (ddlRegistrationType.Text == "Registered")
            {
                if (txtGSTNo.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Enter GST Number..!');", true);
                    txtGSTNo.Focus();
                }
                else
                {
                    flg = true;
                }
            }
            else
            {
                flg = true;
            }

            if (flg == true)
            {
                string Suppliercode = GenerateComCode();
                if (!string.IsNullOrEmpty(Suppliercode))
                {
                    SqlCommand cmd = new SqlCommand("SP_SupplierMaster", con);
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Action", "insert");
                    cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text.Trim());
                    cmd.Parameters.AddWithValue("@SupplierCode", Suppliercode);
                    cmd.Parameters.AddWithValue("@EmailID", txtEmailID.Text.Trim());
                    cmd.Parameters.AddWithValue("@BillToAddress", txtBillingaddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipToAddress", txtShippingaddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Country", ddlCountry.Text.Trim());
                    cmd.Parameters.AddWithValue("@Currency", txtCurrency.Text.Trim());
                    cmd.Parameters.AddWithValue("@StateName", ddlState.Text.Trim());
                    cmd.Parameters.AddWithValue("@RegistrationType", ddlRegistrationType.Text.Trim());
                    cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@PANNo", txtPANNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@PaymentValidity", txtPaymentValidity.Text.Trim());
                    //cmd.Parameters.AddWithValue("@SupplierTaxType", txtSupplierTaxtype.Text.Trim());
                    cmd.Parameters.AddWithValue("@SupplierTaxType", ddltxttype.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@SupplierCategory", txtSupplierCategory.Text.Trim());
                    cmd.Parameters.AddWithValue("@TradeName", txtTradeName.Text.Trim());
                    cmd.Parameters.AddWithValue("@OutstandingLimit", txtOutstandingLimit.Text.Trim());
                    cmd.Parameters.AddWithValue("@PaymentTerm", txtCreditDays.Text.Trim());
                    cmd.Parameters.AddWithValue("@IsActive", true);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                    int a = 0;
                    con.Open();
                    a = cmd.ExecuteNonQuery();
                    con.Close();

                    SqlCommand cmdmax = new SqlCommand("select MAX(Id) as MAxID from tblSupplierMaster", con);
                    con.Open();
                    Object mx = cmdmax.ExecuteScalar();
                    con.Close();
                    int MaxId = Convert.ToInt32(mx.ToString());

                    if (dgvContactDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow g1 in dgvContactDetails.Rows)
                        {
                            Label lblcontactname = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lblcontactname");
                            Label lbldesignation = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lbldesignation");
                            Label lblcontactno = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lblcontactno");
                            Label lblNotify = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lblNotify");
                            Label lblaccess = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lblaccess");

                            bool notify, access;
                            if (lblNotify.Text == "YES")
                                notify = true;
                            else
                                notify = false;

                            if (lblaccess.Text == "YES")
                                access = true;
                            else
                                access = false;

                            SqlCommand cmdcontactdata = new SqlCommand(@"INSERT INTO tblSupplierContactDtls ([HeaderID],[ContactName],[Designation],[ContactNo],[Notify],[Access]) 
                        VALUES(" + MaxId + ",'" + lblcontactname.Text + "','" + lbldesignation.Text + "','" + lblcontactno.Text + "'," +
                             "'" + notify + "','" + access + "')", con);
                            con.Open();
                            cmdcontactdata.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Sucessfully');window.location.href='SupplierMaster.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Supplier Code Generation Problem Please Try Again !!');", true);
                }
            }
        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update Supplier")
        {
            bool flg = false;
            if (ddlRegistrationType.Text == "Registered")
            {
                if (txtGSTNo.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Enter GST Number..!');", true);
                    txtGSTNo.Focus();
                }
                else
                {
                    flg = true;
                }
            }
            else
            {
                flg = true;
            }

            if (flg == true)
            {
                SqlCommand cmd = new SqlCommand("SP_SupplierMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "update");
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(ViewState["UpdateRowId"].ToString()));

                cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text.Trim());
                cmd.Parameters.AddWithValue("@SupplierCode", txtsupplierCode.Text.Trim());
                cmd.Parameters.AddWithValue("@EmailID", txtEmailID.Text.Trim());
                cmd.Parameters.AddWithValue("@BillToAddress", txtBillingaddress.Text.Trim());
                cmd.Parameters.AddWithValue("@ShipToAddress", txtShippingaddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Country", ddlCountry.Text.Trim());
                cmd.Parameters.AddWithValue("@Currency", txtCurrency.Text.Trim());
                cmd.Parameters.AddWithValue("@StateName", ddlState.Text.Trim());
                cmd.Parameters.AddWithValue("@RegistrationType", ddlRegistrationType.Text.Trim());
                cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text.Trim());
                cmd.Parameters.AddWithValue("@PANNo", txtPANNo.Text.Trim());
                cmd.Parameters.AddWithValue("@PaymentValidity", txtPaymentValidity.Text.Trim());
                cmd.Parameters.AddWithValue("@SupplierTaxType", ddltxttype.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@SupplierCategory", txtSupplierCategory.Text.Trim());
                cmd.Parameters.AddWithValue("@TradeName", txtTradeName.Text.Trim());
                cmd.Parameters.AddWithValue("@OutstandingLimit", txtOutstandingLimit.Text.Trim());
                cmd.Parameters.AddWithValue("@PaymentTerm", txtCreditDays.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", true);
                cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                int a = 0;
                cmd.Connection.Open();
                a = cmd.ExecuteNonQuery();
                cmd.Connection.Close();


                SqlCommand cmddelete = new SqlCommand("delete from tblSupplierContactDtls where HeaderID='" + Convert.ToInt32(ViewState["UpdateRowId"].ToString()) + "'", con);
                con.Open();
                cmddelete.ExecuteNonQuery();
                con.Close();


                if (dgvContactDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow g1 in dgvContactDetails.Rows)
                    {
                        Label lblcontactname = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lblcontactname");
                        Label lbldesignation = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lbldesignation");
                        Label lblcontactno = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lblcontactno");
                        Label lblNotify = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lblNotify");
                        Label lblaccess = (Label)dgvContactDetails.Rows[g1.RowIndex].FindControl("lblaccess");

                        bool notify, access;
                        if (lblNotify.Text == "YES")
                            notify = true;
                        else
                            notify = false;

                        if (lblaccess.Text == "YES")
                            access = true;
                        else
                            access = false;

                        SqlCommand cmdcontactdata = new SqlCommand(@"INSERT INTO tblSupplierContactDtls ([HeaderID],[ContactName],[Designation],[ContactNo],[Notify],[Access]) 
                        VALUES(" + Convert.ToInt32(ViewState["UpdateRowId"].ToString()) + ",'" + lblcontactname.Text + "','" + lbldesignation.Text + "','" + lblcontactno.Text + "'," +
                         "'" + notify + "','" + access + "')", con);
                        con.Open();
                        cmdcontactdata.ExecuteNonQuery();
                        con.Close();
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Updated Sucessfully');window.location.href='SupplierMaster.aspx';", true);
            }
        }
        #endregion Update
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierMaster.aspx");
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

    protected void txtSupplierName_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [SupplierName] FROM [tblSupplierMaster] where SupplierName='" + txtSupplierName.Text.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Supplier Already Existing !!";
            btnadd.Enabled = false;
        }
        else
        {
            btnadd.Enabled = true;
            lblmsg.Visible = false;
        }
    }

    protected void Insert(object sender, EventArgs e)
    {
        if (txtContactName.Text == "" || txtDesignation.Text == "" || txtContactnumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill quantity and price !!!');", true);
        }
        else
        {
            Show_Grid();
        }
    }

    private void Show_Grid()
    {
        ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;
        DataTable dt = (DataTable)ViewState["ContactDetails"];
        bool Notify, Access;
        if (chkNotify.Checked == true)
            Notify = true;
        else
            Notify = false;

        if (chkAccess.Checked == true)
            Access = true;
        else
            Access = false;

        dt.Rows.Add(ViewState["RowNo"], txtContactName.Text, txtDesignation.Text, txtContactnumber.Text, Notify, Access);
        ViewState["ContactDetails"] = dt;

        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();

        txtContactName.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        txtContactnumber.Text = string.Empty;
    }

    protected void dgvContactDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void dgvContactDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dgvContactDetails.EditIndex = e.NewEditIndex;
        //this.getQutationdts();
        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void lnkbtnUpdate_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        string Tax = ((TextBox)row.FindControl("txtcontactName")).Text;
        string Quntity = ((TextBox)row.FindControl("txtdesignation")).Text;
        string Rate = ((TextBox)row.FindControl("txtcontactno")).Text;
        CheckBox chknotify = (CheckBox)row.FindControl("ckhnotify");
        CheckBox chkaccess = (CheckBox)row.FindControl("ckhaccess");
        bool Notify, Access;
        if (chknotify.Checked == true)
            Notify = true;
        else
            Notify = false;

        if (chkaccess.Checked == true)
            Access = true;
        else
            Access = false;

        DataTable Dt = ViewState["ContactDetails"] as DataTable;

        Dt.Rows[row.RowIndex]["contactname"] = Tax;
        Dt.Rows[row.RowIndex]["designation"] = Quntity;
        Dt.Rows[row.RowIndex]["contactno"] = Rate;
        Dt.Rows[row.RowIndex]["notify"] = Notify;
        Dt.Rows[row.RowIndex]["access"] = Access;

        Dt.AcceptChanges();

        ViewState["ContactDetails"] = Dt;
        dgvContactDetails.EditIndex = -1;

        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);

    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        DataTable dt = ViewState["ContactDetails"] as DataTable;
        dt.Rows.Remove(dt.Rows[row.RowIndex]);
        ViewState["ContactDetails"] = dt;
        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Delete Succesfully !!!');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);

    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        DataTable Dt = ViewState["ContactDetails"] as DataTable;
        dgvContactDetails.EditIndex = -1;

        ViewState["ContactDetails"] = Dt;
        dgvContactDetails.EditIndex = -1;

        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);

    }

    //protected void GetCoinValues(string GSTIN)
    //{
    //    var json = new WebClient().DownloadString("http://sheet.gstincheck.co.in/check/c2b700abd1768adb88f8e18395d318ea/" + GSTIN);

    //    var Result = JsonConvert.DeserializeObject<dynamic>(json);
    //    var flag = Result.flag.Value;

    //    if (flag == true)
    //    {
    //        var lgnm = Result.data.lgnm.Value;
    //        var tradeNam = Result.data.tradeNam.Value;
    //        var gstin = Result.data.gstin.Value;
    //        var State = Result.data.stj.Value;

    //        var message = Result.message.Value;

    //        string[] str = gstin.Split("27", "");



    //        txtTradeName.Text = tradeNam;
    //    }

    //}

    protected void txtGSTNo_TextChanged(object sender, EventArgs e)
    {
        string MyString = txtGSTNo.Text;
        string res = MyString.Substring(0, 2);
        string word1 = res;
        string word2 = "1Z";
        string text = stringBetween(MyString, word1, word2);
        txtPANNo.Text = text;

        
        string GstNo = txtGSTNo.Text;

        string firstTwoDigits = string.Empty;

        if (!string.IsNullOrEmpty(GstNo))
        {
            if (GstNo.Length < 15)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Invalid GST number. Please enter a 15-digit GST number. !!');", true);
                txtGSTNo.Text = "";
        
                return;
            }

            firstTwoDigits = GstNo.Substring(0, 2);

            // Check if the first two digits are valid state codes
            string[] validStateCodes = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36" };
            if (!validStateCodes.Contains(firstTwoDigits))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Invalid GST number. Please enter a valid state code. !!');", true);
                txtGSTNo.Text = "";
                return;
            }

       
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please enter a GST number. !!');", true);
            txtGSTNo.Text = "";
            return;
        }



    }

    public static string stringBetween(string Source, string Start, string End)
    {
        string result = "";
        if (Source.Contains(Start) && Source.Contains(End))
        {
            int StartIndex = Source.IndexOf(Start, 0) + Start.Length;
            int EndIndex = Source.IndexOf(End, StartIndex);
            result = Source.Substring(StartIndex, EndIndex - StartIndex);
            return result;
        }
        return result;
    }

    protected void chkSame_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSame.Checked == true)
        {
            txtShippingaddress.Text = txtBillingaddress.Text;
        }
        else
        {
            txtShippingaddress.Text = string.Empty;
        }

    }



    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtCurrency.Text = ddlCountry.SelectedValue;
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
                        ddltxttype.DataSource = Dt;
                        ddltxttype.DataValueField = "TaxCategory";
                        ddltxttype.DataTextField = "TaxCategory";
                        ddltxttype.DataBind();
                        ddltxttype.Items.Insert(0, "--Select Type--");

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

    public void GetCategourylist()

    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("[SP_Categoury]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetCategoury"));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        txtSupplierCategory.AppendDataBoundItems = true;
                        txtSupplierCategory.Items.Add(new ListItem("Select", ""));
                        txtSupplierCategory.DataSource = Dt;
                        txtSupplierCategory.DataValueField = "Category";
                        txtSupplierCategory.DataTextField = "Category";
                        txtSupplierCategory.DataBind();

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

    protected void txtSupplierCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

   
}