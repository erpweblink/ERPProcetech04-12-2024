#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\ItemList.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "726DE025E887DCAA9046AA43EBC7A600D799FAE9"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\ItemList.aspx.cs"
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

public partial class Admin_ItemList : System.Web.UI.Page
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
        query = @"select * from tblItemMaster where IsActive=1 order by id desc";
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvItem.DataSource = dt;
            GvItem.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvItem.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            GvItem.DataSource = null;
            GvItem.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvItem.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
    }

    protected void GvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ViewState["CompRowId"] = e.CommandArgument.ToString();
        if (e.CommandName == "RowEdit")
        {
            ViewState["id"] = e.CommandArgument.ToString();
            Response.Redirect("ItemMaster.aspx?ID=" + encrypt(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "RowDelete")
        {
            con.Open();
            SqlCommand Cmd = new SqlCommand("Delete From [tblItemMaster] WHERE Id=@Id", con);
            Cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Deleted Sucessfully');window.location.href='ItemList.aspx';", true);

        }
        //if (e.CommandName == "Itemname")
        //{
        //    if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        //    {
        //        GetItemDataPopup(e.CommandArgument.ToString());
        //        this.modelprofile.Show();
        //    }
        //}
    }

    //private void GetItemDataPopup(string id)
    //{
    //    string query1 = string.Empty;
    //    query1 = @"select * from tblItemMaster where Id='" + id + "' ";
    //    SqlDataAdapter ad = new SqlDataAdapter(query1, con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblSname.Text = dt.Rows[0]["ItemName"].ToString();
    //        lblemail.Text = dt.Rows[0]["EmailID"].ToString();
    //       // txtItemCode.Text = dt.Rows[0]["ItemCode"].ToString();
    //        lblbillingaddress.Text = dt.Rows[0]["BillToAddress"].ToString();
    //        lblshipaddress.Text = dt.Rows[0]["ShipToAddress"].ToString();
    //        lblcountry.Text = dt.Rows[0]["Country"].ToString();
    //        lblState.Text = dt.Rows[0]["StateName"].ToString();
    //        //l.Text = dt.Rows[0]["RegistrationType"].ToString();
    //        lblgstno.Text = dt.Rows[0]["GSTNo"].ToString();
    //        lblPan.Text = dt.Rows[0]["PANNo"].ToString();
    //        //txtPaymentValidity.Text = dt.Rows[0]["PaymentValidity"].ToString();
    //        //txtItemTaxtype.Text = dt.Rows[0]["ItemTaxType"].ToString();
    //        //txtItemCategory.Text = dt.Rows[0]["ItemCategory"].ToString();
    //        //txtTradeName.Text = dt.Rows[0]["TradeName"].ToString();
    //        //txtOutstandingLimit.Text = dt.Rows[0]["OutstandingLimit"].ToString();
    //        //ddlPaymentTerm.Text = dt.Rows[0]["PaymentTerm"].ToString();
    //        //txtCurrency.Text = dt.Rows[0]["Currency"].ToString();
    //        lblregBy.Text = dt.Rows[0]["CreatedBy"].ToString();

    //        //getConatctdts(id);

    //        //btnadd.Text = "Update Item";
    //    }

    //    string query = string.Empty;
    //    query = @"select * from tblItemContactDtls where HeaderID='" + id + "' ";
    //    SqlDataAdapter ad1 = new SqlDataAdapter(query, con);
    //    DataTable dt1 = new DataTable();
    //    ad1.Fill(dt1);
    //    if (dt1.Rows.Count > 0)
    //    {
    //        dgvContactDtls.DataSource = dt1;
    //        dgvContactDtls.DataBind();
    //    }
    //}

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

    //protected void btnShowComDetail_Click(object sender, EventArgs e)
    //{
    //    GetItemDataPopup(ViewState["CompRowId"].ToString());
    //    this.modelprofile.Show();
    //}

    #region Filter

    protected void txtcnamefilter_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtcnamefilter.Text.Trim()))
        {
            query = "SELECT * FROM tblItemMaster where ItemName like '" + txtcnamefilter.Text.Trim() + "%' order by Id desc";
        }
        else
        {
            query = "SELECT * FROM tblItemMaster where order by Id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvItem.DataSource = dt;
            GvItem.DataBind();
        }
        else
        {
            GvItem.DataSource = null;
            GvItem.DataBind();
        }
    }
    #endregion Filter

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemList.aspx");
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemList(string prefixText, int count)
    {
        return AutoFillItemName(prefixText);
    }

    public static List<string> AutoFillItemName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [ItemName] from [tblItemMaster] where " + "ItemName like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["ItemName"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemOwnerList(string prefixText, int count)
    {
        return AutoFillItemOwnerName(prefixText);
    }

    public static List<string> AutoFillItemOwnerName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [oname1] from [Item] where oname1 like @Search + '%' and status=0 and [isdeleted]=0";

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
