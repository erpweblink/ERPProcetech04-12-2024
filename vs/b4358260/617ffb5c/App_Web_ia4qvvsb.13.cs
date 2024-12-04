#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\SupplierList.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71436773948BC00B80F62B42C91727E94E0E7917"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\SupplierList.aspx.cs"
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

public partial class Admin_SupplierList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
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
        query = @"select * from tblSupplierMaster where IsActive=1 order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvSupplier.DataSource = dt;
            GvSupplier.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvSupplier.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            GvSupplier.DataSource = null;
            GvSupplier.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvSupplier.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
    }

    protected void GvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ViewState["CompRowId"] = e.CommandArgument.ToString();
        if (e.CommandName == "RowEdit")
        {
            ViewState["id"] = e.CommandArgument.ToString();
            Response.Redirect("SupplierMaster.aspx?ID=" + encrypt(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "Suppliername")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                GetSupplierDataPopup(e.CommandArgument.ToString());
                this.modelprofile.Show();
            }
        }
		 if (e.CommandName == "RowDelete")
        {
            con.Open();
            SqlCommand Cmd = new SqlCommand("Delete From tblSupplierMaster WHERE Id=@Id", con);
            Cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmd.ExecuteNonQuery();
			 
			 SqlCommand Cmddtl = new SqlCommand("Delete From tblSupplierContactDtls WHERE HeaderID=@Id", con);
            Cmddtl.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmddtl.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Deleted Sucessfully');window.location.href='SupplierList.aspx';", true);

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
            query = "SELECT * FROM tblSupplierMaster where SupplierName like '" + txtcnamefilter.Text.Trim() + "%' order by Id desc";
        }
        else
        {
            query = "SELECT * FROM tblSupplierMaster where order by Id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvSupplier.DataSource = dt;
            GvSupplier.DataBind();
        }
        else
        {
            GvSupplier.DataSource = null;
            GvSupplier.DataBind();
        }
    }
    #endregion Filter

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierList.aspx");
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


}

#line default
#line hidden
