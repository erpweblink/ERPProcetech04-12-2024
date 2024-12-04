#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\TaxInvoiceList.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8CD9B1E98D339CD637787F6730C81EDAE103621C"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\TaxInvoiceList.aspx.cs"
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

public partial class Admin_TaxInvoiceList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GVBinddata();
        }
    }

    protected void GVBinddata()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("select * from tblTaxInvoiceHdr where isdeleted='0' order by CreatedOn DESC", con);
            sad.Fill(dt);
            GvInvoiceList.DataSource = dt;
            GvInvoiceList.DataBind();
            GvInvoiceList.EmptyDataText = "Record Not Found";
			ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvInvoiceList.ClientID + "', 400, 1020 , 40 ,true); </script>", false);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
    {
        return AutoFillCustomerlist(prefixText);
    }

    public static List<string> AutoFillCustomerlist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT BillingCustomer from tblTaxInvoiceHdr where " + "BillingCustomer like @Search + '%' AND isdeleted='0'  ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> BillingCustomer = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        BillingCustomer.Add(sdr["BillingCustomer"].ToString());
                    }
                }
                con.Close();
                return BillingCustomer;
            }

        }
    }

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("select * from tblTaxInvoiceHdr where BillingCustomer='" + txtCustomerName.Text + "' AND isdeleted='0' order by CreatedOn DESC", con);
            sad.Fill(dtt);
            GvInvoiceList.DataSource = dtt;
            GvInvoiceList.DataBind();
            GvInvoiceList.EmptyDataText = "Record Not Found";
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaxInvoiceList.aspx");
    }

    protected void GvInvoiceList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowEdit")
        {
            Response.Redirect("TaxInvoice.aspx?Id=" + encrypt(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();
                // Response.Write("<script>window.open('PurchaseBillPDF.aspx','_blank');</script>");
                Response.Write("<script>window.open ('TaxInvoicePDF.aspx?Id=" + encrypt(e.CommandArgument.ToString()) + "','_blank');</script>");


            }
        }
        if (e.CommandName == "RowDelete")
        {
            con.Open();
			SqlCommand cmdget = new SqlCommand("select AgainstNumber from tblTaxInvoiceHdr WHERE Id=@Id", con);
            cmdget.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            string pono = cmdget.ExecuteScalar().ToString();

            SqlCommand CmduptDtl = new SqlCommand("update OrderAccept set status=null where pono=@pono", con);
            CmduptDtl.Parameters.AddWithValue("@pono", pono);
            CmduptDtl.ExecuteNonQuery();
			
            SqlCommand Cmd = new SqlCommand("delete from tblTaxInvoiceHdr WHERE Id=@Id", con);
            Cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmd.ExecuteNonQuery();

            SqlCommand CmddeleteDtl = new SqlCommand("delete from tblTaxInvoiceDtls where HeaderID=@Id", con);
            CmddeleteDtl.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            CmddeleteDtl.ExecuteNonQuery();

            
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Deleted Sucessfully');window.location.href='TaxInvoiceList.aspx';", true);
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

    protected void GvInvoiceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                con.Open();
                int id = Convert.ToInt32(GvInvoiceList.DataKeys[e.Row.RowIndex].Values[0]);
                SqlCommand cmd = new SqlCommand("select COUNT(HeaderID) from tblTaxInvoiceDtls where HeaderID='" + id + "'", con);
                Object Procnt = cmd.ExecuteScalar();
                Label grandtotal = (Label)e.Row.FindControl("lblProduct");
                grandtotal.Text = Procnt == null ? "0" : Procnt.ToString();
                con.Close();


                Label lblGrandTotal = (Label)e.Row.FindControl("lblGrandTotal");

                var gtot = Math.Round(Convert.ToDouble(lblGrandTotal.Text));

                lblGrandTotal.Text = gtot.ToString("#0.00");
				 Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
				Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
                Label lblFinalBasic = (Label)e.Row.FindControl("lblFinalBasic");
                if (lblStatus.Text == "True")
                {
                    lblStatus.Text = "Paid";
                    lnkEdit.Visible = false; 
                    lnkDelete.Visible = false;
                }
                else {
                    lblStatus.Text = "Pending";
                    lnkEdit.Visible = true;
                    lnkDelete.Visible = true;
                }
				 if (lblInvoiceNo.Text != "")
                {
                    lblInvoiceNo.Text = lblInvoiceNo.Text;
                }
                else
                {
                    lblInvoiceNo.Text = lblFinalBasic.Text;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}

#line default
#line hidden
