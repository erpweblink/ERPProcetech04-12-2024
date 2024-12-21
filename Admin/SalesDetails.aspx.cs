using DocumentFormat.OpenXml.Office.Word;
using Spire.Pdf.General.Paper.Uof;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CustomerDetails : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;      
        if (!IsPostBack)
        {
            if (Request.QueryString["cdd"] != null)
            {
                btnadd.Text = "Update";
                string id = Decrypt(Request.QueryString["cdd"].ToString());
                hiddenid.Value = id;
                gvCustomerDetails_SelectedIndexChanged(id);
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


    protected void GvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "RowEdit")
        {
            btnadd.Text = "Update";
            ViewState["CustomerID"] = e.CommandArgument.ToString();
            gvCustomerDetails_SelectedIndexChanged(e.CommandArgument.ToString());
        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnadd.Text == "Update")
        {
            // Retrieve the CustomerID from ViewState
            int customerId = Convert.ToInt32(hiddenid.Value);
            if (customerId != 0)
            {
                customerId = Convert.ToInt32(hiddenid.Value);
            }
            else
            {
                lblErrorMessage.Text = "Customer ID is not available.";
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE SalesDetails SET CustomerName = @CustomerName, BasicAmount = @BasicAmount, " +
                    "CGST = @CGST, SGST = @SGST, IGST = @IGST, GrandTotal = @GrandTotal, UpdatedBy = @UpdatedBy,UpdatedDate=@UpdatedDate" +
                    ",InvoiceNo = @InvoiceNo,InvoiceDate =@InvoiceDate, GSTTotAmount = @GSTTotAmount WHERE CustomerID = @CustomerID", con);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);

                decimal basicAmount;
                if (!decimal.TryParse(txtBasicAmount.Text, out basicAmount))
                {
                    lblErrorMessage.Text = "Invalid Basic Amount. Please enter a valid number.";
                    return;
                }

                cmd.Parameters.AddWithValue("@BasicAmount", txtBasicAmount.Text);
                cmd.Parameters.AddWithValue("@CGST", txtCGST.Text);
                cmd.Parameters.AddWithValue("@SGST", txtSGST.Text);
                cmd.Parameters.AddWithValue("@IGST", txtIGST.Text);
                cmd.Parameters.AddWithValue("@GrandTotal", txtGrandTotal.Text);
                cmd.Parameters.AddWithValue("@UpdatedBy", Session["name"].ToString());
                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now.ToString("yyyy/dd/MM hh:mm:ss"));
                cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text);
                if (Request.Form[txtInvoiceDate.UniqueID].ToString() != "")
                {
                    string invoiceDateStr = Request.Form[txtInvoiceDate.UniqueID].ToString();
                    cmd.Parameters.AddWithValue("@InvoiceDate", invoiceDateStr);
                }
                cmd.Parameters.AddWithValue("@GSTTotAmount", txtGstTot.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                lblMessage.Text = "Record Updated successfully!";
                lblMessage.Visible = true;
                btnadd.Text = "Add";
                ClearFields();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Record Updated Successfully...!');window.location.href='CustomerDetailList.aspx';", true);

            }
        }
        else
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO SalesDetails" +
                    " (CustomerName, HsnDate, BasicAmount, CGST, SGST, IGST, GrandTotal,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,InvoiceNo,InvoiceDate,GSTTotAmount) " +
                    "VALUES (@CustomerName, GETDATE(), @BasicAmount, @CGST, @SGST, @IGST, @GrandTotal,@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@InvoiceNo,@InvoiceDate,@GSTTotAmount)", con);
                cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@BasicAmount", txtBasicAmount.Text);
                cmd.Parameters.AddWithValue("@CGST", txtCGST.Text);
                cmd.Parameters.AddWithValue("@SGST", txtSGST.Text);
                cmd.Parameters.AddWithValue("@IGST", txtIGST.Text);
                cmd.Parameters.AddWithValue("@GrandTotal", txtGrandTotal.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy/dd/MM hh:mm:ss"));
                cmd.Parameters.AddWithValue("@UpdatedBy", Session["name"].ToString());
                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now.ToString("yyyy/dd/MM hh:mm:ss"));
                cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text);
                if (Request.Form[txtInvoiceDate.UniqueID].ToString() != "")
                {
                    string invoiceDateStr = Request.Form[txtInvoiceDate.UniqueID].ToString();
                    cmd.Parameters.AddWithValue("@InvoiceDate", invoiceDateStr);
                }
                cmd.Parameters.AddWithValue("@GSTTotAmount", txtGstTot.Text);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            lblMessage.Text = "Record added successfully!";
            lblMessage.Visible = true;
            lblErrorMessage.Text = "";
            ClearFields();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Record Updated Successfully...!');window.location.href='CustomerDetailList.aspx';", true);
        }


    }


    protected void gvCustomerDetails_SelectedIndexChanged(string id)
    {

        string query1 = string.Empty;
        query1 = "SELECT *  from [SalesDetails] where CustomerID='" + id + "'";
        SqlDataAdapter ad = new SqlDataAdapter(query1, connectionString);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
            txtInvoiceDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
            txtCustomerName.Text = dt.Rows[0]["CustomerName"].ToString();
            txtGstTot.Text = dt.Rows[0]["GSTTotAmount"].ToString();
            txtBasicAmount.Text = dt.Rows[0]["BasicAmount"].ToString();
            if (dt.Rows[0]["CGST"].ToString() == "&nbsp;" || dt.Rows[0]["CGST"].ToString() == "0" || dt.Rows[0]["CGST"].ToString() == "")
            {
                txtCGST.Text = "0";
                txtCGST.ReadOnly = true;
            }
            else
            {
                txtCGST.Text = dt.Rows[0]["CGST"].ToString();
            }
            if (dt.Rows[0]["SGST"].ToString() == "&nbsp;" || dt.Rows[0]["SGST"].ToString() == "0" || dt.Rows[0]["SGST"].ToString() == "")
            {
                txtSGST.Text = "0";
                txtSGST.ReadOnly = true;
            }
            else
            {
                txtSGST.Text = dt.Rows[0]["SGST"].ToString();
            }
            if (dt.Rows[0]["IGST"].ToString() == "&nbsp;" || dt.Rows[0]["IGST"].ToString() == "0" || dt.Rows[0]["IGST"].ToString() == "")
            {
                txtIGST.Text = "0";
                txtIGST.ReadOnly = true;
            }
            else
            {
                txtIGST.Text = dt.Rows[0]["IGST"].ToString();
            }

            txtGrandTotal.Text = dt.Rows[0]["GrandTotal"].ToString();

        }

    }



    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void ClearFields()
    {

        txtIGST.ReadOnly = false;
        txtSGST.ReadOnly = false;
        txtCGST.ReadOnly = false;
        txtCustomerName.Text = string.Empty;
        txtBasicAmount.Text = string.Empty;
        txtCGST.Text = string.Empty;
        txtSGST.Text = string.Empty;
        txtIGST.Text = string.Empty;
        txtGrandTotal.Text = string.Empty;

        Response.Redirect("SalesDetailList.aspx");
    }

    protected void txtCGST_TextChanged(object sender, EventArgs e)
    {
        if (txtCGST.Text != "0")
        {
            if (txtBasicAmount.Text != "")
            {
                if (Request.Form[txtInvoiceDate.UniqueID].ToString() != "")
                {
                    string invoiceDateStr = Request.Form[txtInvoiceDate.UniqueID].ToString();
                    txtInvoiceDate.Text = invoiceDateStr;
                }
                txtIGST.Text = "0";
                txtIGST.ReadOnly = true;
                var total = Convert.ToDecimal(txtBasicAmount.Text);
                txtSGST.Text = txtCGST.Text;
                txtSGST.ReadOnly = true;

                decimal cgst_amt;
                if (string.IsNullOrEmpty(txtCGST.Text))
                {
                    cgst_amt = 0;
                }
                else
                {
                    cgst_amt = Convert.ToDecimal(total.ToString()) * Convert.ToDecimal(txtCGST.Text) / 100;
                }


                decimal sgst_amt;
                if (string.IsNullOrEmpty(txtSGST.Text))
                {
                    sgst_amt = 0;
                }
                else
                {
                    sgst_amt = Convert.ToDecimal(total.ToString()) * Convert.ToDecimal(txtSGST.Text) / 100;
                }

                decimal tots = cgst_amt + sgst_amt;
                txtGstTot.Text = tots.ToString();
                decimal tot = total + tots;
                txtGrandTotal.Text = tot.ToString();
            }
        }

    }

    protected void txtSGST_TextChanged(object sender, EventArgs e)
    {
        if (txtSGST.Text != "0")
        {
            if (txtBasicAmount.Text != "")
            {
                if (Request.Form[txtInvoiceDate.UniqueID].ToString() != "")
                {
                    string invoiceDateStr = Request.Form[txtInvoiceDate.UniqueID].ToString();
                    txtInvoiceDate.Text = invoiceDateStr;
                }
                txtIGST.Text = "0";
                txtIGST.ReadOnly = true;
                var total = Convert.ToDecimal(txtBasicAmount.Text);
                txtCGST.Text = txtSGST.Text;
                txtCGST.ReadOnly = true;

                decimal sgst_amt;
                if (string.IsNullOrEmpty(txtSGST.Text))
                {
                    sgst_amt = 0;
                }
                else
                {
                    sgst_amt = Convert.ToDecimal(total.ToString()) * Convert.ToDecimal(txtSGST.Text) / 100;
                }

                decimal cgst_amt;
                if (string.IsNullOrEmpty(txtCGST.Text))
                {
                    cgst_amt = 0;
                }
                else
                {
                    cgst_amt = Convert.ToDecimal(total.ToString()) * Convert.ToDecimal(txtCGST.Text) / 100;
                }

                decimal tots = cgst_amt + sgst_amt;
                txtGstTot.Text = tots.ToString();
                decimal tot = total + tots;

                txtGrandTotal.Text = tot.ToString();
            }
        }
    }

    protected void txtIGST_TextChanged(object sender, EventArgs e)
    {
        if (txtIGST.Text != "0")
        {
            if (txtBasicAmount.Text != "")
            {
                if (Request.Form[txtInvoiceDate.UniqueID].ToString() != "")
                {
                    string invoiceDateStr = Request.Form[txtInvoiceDate.UniqueID].ToString();
                    txtInvoiceDate.Text = invoiceDateStr;
                }
                txtCGST.Text = "0";
                txtSGST.Text = "0";
                txtSGST.ReadOnly = true;
                txtCGST.ReadOnly = true;
                var total = Convert.ToDecimal(txtBasicAmount.Text);
                decimal igst_amt;
                if (string.IsNullOrEmpty(txtIGST.Text))
                {
                    igst_amt = 0;
                }
                else
                {
                    igst_amt = Convert.ToDecimal(total.ToString()) * Convert.ToDecimal(txtIGST.Text) / 100;
                }
                txtGstTot.Text = igst_amt.ToString();

                decimal tot = total + igst_amt;
                txtGrandTotal.Text = tot.ToString();
            }

        }
    }

    protected void txtBasicAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtBasicAmount.Text != "")
        {
            if (Request.Form[txtInvoiceDate.UniqueID].ToString() != "")
            {
                string invoiceDateStr = Request.Form[txtInvoiceDate.UniqueID].ToString();
                txtInvoiceDate.Text = invoiceDateStr;
            }
            var total = Convert.ToDecimal(txtBasicAmount.Text);
            txtGrandTotal.Text = total.ToString();
            if (btnadd.Text == "Update")
            {
                if (txtCGST.Text != "0")
                {
                    txtCGST_TextChanged(null, null);
                }
                else
                {
                    txtIGST_TextChanged(null, null);
                }
            }
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


}