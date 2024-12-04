#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Sales\AddCompany.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "46BCDBE79F872B23639B72A99251BE9A10C97D30"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Sales\AddCompany.aspx.cs"
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

public partial class Admin_AddCompany : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateHistorymsg = string.Empty; ComCode = string.Empty; ComCodeUpdate = string.Empty; visitingcardPath = string.Empty; regdate = string.Empty;
            if (Request.QueryString["code"]!=null)
            {
                ViewState["UpdateRowId"]= Decrypt(Request.QueryString["code"].ToString());
                GetCompanyData(ViewState["UpdateRowId"].ToString());
            }
        }
    }


    static string regdate = string.Empty; 
    protected void GetCompanyData(string id)
    {
        string query1 = string.Empty;
        query1 = "SELECT [id],[ccode],[cname],[oname],[email],[mobile],[regdate],[sessionname],[billingaddress],[shippingaddress],[gstno] FROM [Company] where id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ComCodeUpdate = dt.Rows[0]["ccode"].ToString();
            txtcname.Text = dt.Rows[0]["cname"].ToString();
            txtownname.Text = dt.Rows[0]["oname"].ToString();
            txtemail.Text = dt.Rows[0]["email"].ToString();
            txtmobile.Text = dt.Rows[0]["mobile"].ToString();

            txtbillingaddress.Text = dt.Rows[0]["billingaddress"].ToString();
            txtshippingaddress.Text = dt.Rows[0]["shippingaddress"].ToString();
            txtgstno.Text = dt.Rows[0]["gstno"].ToString();

            btnadd.Text = "Update";

            /// Fill hidden fields for maintaining history
            //ComCodeUpdate = dt.Rows[0]["ccode"].ToString();
            HFcname.Value = dt.Rows[0]["cname"].ToString();
            HFoname.Value = dt.Rows[0]["oname"].ToString();
            HFemail.Value = dt.Rows[0]["email"].ToString();
            HFmobile.Value = dt.Rows[0]["mobile"].ToString();
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
    //protected void CreateHistory()
    //{
    //    if (HFcname.Value.Trim().ToLower() != txtcname.Text.Trim().ToLower())
    //    {
    //        UpdateHistorymsg = "Company name has been changed from '"+ HFcname.Value + "' to '"+ txtcname.Text + "', ";
    //    }
    //    if (HFoname.Value.Trim().ToLower() != txtownname.Text.Trim().ToLower())
    //    {
    //        UpdateHistorymsg = UpdateHistorymsg + "Company's owner name has been changed from '" + HFoname.Value + "' to '" + txtownname.Text + "', ";
    //    }
    //    if (HFemail.Value.Trim().ToLower() != txtemail.Text.Trim().ToLower())
    //    {
    //        UpdateHistorymsg = UpdateHistorymsg + "Company's email address has been changed from '" + HFemail.Value + "' to '" + txtemail.Text + "', ";
    //    }
    //    if (HFmobile.Value.Trim().ToLower() != txtmobile.Text.Trim().ToLower())
    //    {
    //        UpdateHistorymsg = UpdateHistorymsg + "Company's mobile number has been changed from '" + HFmobile.Value + "' to '" + txtmobile.Text + "', ";
    //    }
    //    if (HFvisitdate.Value.Trim().ToLower() != txtvisiteddate.Text.Trim().ToLower())
    //    {
    //        UpdateHistorymsg = UpdateHistorymsg + "Company visited date has been changed from '" + HFvisitdate.Value + "' to '" + txtvisiteddate.Text + "', ";
    //    }
    //    if (HFclienttype.Value.ToLower() != ddltype.Text.ToLower())
    //    {
    //        UpdateHistorymsg = UpdateHistorymsg + "Client type has been changed from '" + HFclienttype.Value + "' to '" + ddltype.Text + "', ";
    //    }
    //    if (HFbde.Value.Trim().ToLower() != ddlbde.Text.Trim().ToLower())
    //    {
    //        UpdateHistorymsg = UpdateHistorymsg + "TME/BDE has been changed from '" + HFbde.Value + "' to '" + ddlbde.Text + "', ";
    //    }
    //    if (HFaddress.Value.Trim().ToLower() != txtaddress.Text.Trim().ToLower())
    //    {
    //        UpdateHistorymsg = UpdateHistorymsg + "Company's address has been changed from '" + HFaddress.Value + "' to '" + txtaddress.Text + "', ";
    //    }
    //    if (hfwebsite.Value.Trim().ToLower() != txtwebsitelink.Text.Trim().ToLower())
    //    {
    //        UpdateHistorymsg = UpdateHistorymsg + "Company's website link has been changed from '" + hfwebsite.Value + "' to '" + txtwebsitelink.Text + "', ";
    //    }
    //    if (!string.IsNullOrEmpty(UpdateHistorymsg))
    //    {
    //        SqlCommand cmd = new SqlCommand("SP_CompanyHistory", con);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@Action", "Insert");
    //        cmd.Parameters.AddWithValue("@sessionname", Session["adminempcode"].ToString());
    //        cmd.Parameters.AddWithValue("@ccode", ComCodeUpdate);
    //        cmd.Parameters.AddWithValue("@message", UpdateHistorymsg);
    //        cmd.Connection.Open();
    //        cmd.ExecuteNonQuery();
    //        cmd.Connection.Close();
    //    }
        
    //}

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
            ComCode = "ECL/0" + (Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1).ToString();
        }
        else
        {
            ComCode = string.Empty;
        }
    }

    static string ComCodeUpdate = string.Empty; static string visitingcardPath = string.Empty;
    protected void btnadd_Click(object sender, EventArgs e)
    {
        #region Insert
        if (btnadd.Text == "Add Company")
        {
            GenerateComCode();
            if (!string.IsNullOrEmpty(ComCode))
            {
                SqlCommand cmd = new SqlCommand("SP_Company", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@ccode", ComCode);
                cmd.Parameters.AddWithValue("@cname", txtcname.Text.Trim());
                cmd.Parameters.AddWithValue("@oname", txtownname.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
                cmd.Parameters.AddWithValue("@mobile", txtmobile.Text.Trim());

                cmd.Parameters.AddWithValue("@billingaddress", txtbillingaddress.Text);
                cmd.Parameters.AddWithValue("@shippingaddress", txtshippingaddress.Text);

                cmd.Parameters.AddWithValue("@sessionname", Session["salesempcode"].ToString());
                cmd.Parameters.AddWithValue("@gstno", txtgstno.Text);
                cmd.Parameters.AddWithValue("@updatedby", Session["salesempcode"].ToString());
                int a = 0;
                cmd.Connection.Open();
                a = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Saved Sucessfully');window.location='AddCompany.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Saved !!');", true);
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
                SqlCommand cmd = new SqlCommand("SP_Company", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Update");
                cmd.Parameters.AddWithValue("@id", ViewState["UpdateRowId"].ToString());
            cmd.Parameters.AddWithValue("@ccode", ComCodeUpdate);
            cmd.Parameters.AddWithValue("@cname", txtcname.Text.Trim());
            cmd.Parameters.AddWithValue("@oname", txtownname.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
            cmd.Parameters.AddWithValue("@mobile", txtmobile.Text.Trim());

            cmd.Parameters.AddWithValue("@billingaddress", txtbillingaddress.Text);
            cmd.Parameters.AddWithValue("@shippingaddress", txtshippingaddress.Text);

            cmd.Parameters.AddWithValue("@sessionname", hfregby.Value);
            cmd.Parameters.AddWithValue("@gstno", txtgstno.Text);
            cmd.Parameters.AddWithValue("@updatedby", Session["salesempcode"].ToString());
            int a = 0;
                cmd.Connection.Open();
                a = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                if (a > 0)
                {
                    //CreateHistory();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Updated Sucessfully');window.location='AddCompany.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Updated !!');", true);
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
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],[cname] FROM [Company] where [isdeleted]=0 and cname='" + txtcname.Text.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Company Already Existing !!";
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

}

#line default
#line hidden
