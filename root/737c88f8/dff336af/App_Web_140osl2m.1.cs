#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\Addenquiry.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C921EA24DAED07ACFDCDE7CFCB79C43D78F4F26A"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\Addenquiry.aspx.cs"
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

public partial class Admin_Addenquiry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComCode = string.Empty; ComCodeUpdate = string.Empty; regdate = string.Empty;
            if (Request.QueryString["Cname"] != null)
            {
                //ViewState["UpdateRowId"] = Decrypt(Request.QueryString["code"].ToString());
                GetCompanyDataByName(Decrypt(Request.QueryString["Cname"].ToString()));
            }
        }
    }


    static string regdate = string.Empty;
    //protected void GetCompanyData(string id)
    //{
    //    string query1 = string.Empty;
    //    query1 = "SELECT [id],[ccode],[cname],[oname],[email],[mobile],[regdate],[sessionname],[billingaddress],[shippingaddress],[gstno] FROM [Company] where id='" + id + "' ";
    //    SqlDataAdapter ad = new SqlDataAdapter(query1, con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ComCodeUpdate = dt.Rows[0]["ccode"].ToString();
    //        txtcname.Text = dt.Rows[0]["cname"].ToString();
    //        //txtownname.Text = dt.Rows[0]["oname"].ToString();
    //        //txtemail.Text = dt.Rows[0]["email"].ToString();
    //        //txtmobile.Text = dt.Rows[0]["mobile"].ToString();

    //        //txtbillingaddress.Text = dt.Rows[0]["billingaddress"].ToString();
    //        //txtshippingaddress.Text = dt.Rows[0]["shippingaddress"].ToString();
    //        //txtgstno.Text = dt.Rows[0]["gstno"].ToString();

    //        btnadd.Text = "Update";

    //        /// Fill hidden fields for maintaining history
    //        //ComCodeUpdate = dt.Rows[0]["ccode"].ToString();
    //        HFcname.Value = dt.Rows[0]["cname"].ToString();
    //        HFoname.Value = dt.Rows[0]["oname"].ToString();
    //        HFemail.Value = dt.Rows[0]["email"].ToString();
    //        HFmobile.Value = dt.Rows[0]["mobile"].ToString();
    //        //HFvisitingcard.Value = dt.Rows[0]["visitingcard"].ToString();
    //        //HFaddress.Value = dt.Rows[0]["address"].ToString();
    //        //HFvisitdate.Value = dt.Rows[0]["visitdate"].ToString();
    //        //HFbde.Value = dt.Rows[0]["BDE"].ToString();
    //        //hfwebsite.Value = dt.Rows[0]["website"].ToString();
    //        //HFclienttype.Value = dt.Rows[0]["type"].ToString();
    //        hfregby.Value = dt.Rows[0]["sessionname"].ToString();
    //    }
    //}

    protected void GetCompanyDataByName(string cname)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT top 1 [id],[ccode],[cname] FROM [Company] where [isdeleted]=0 and cname='" + cname.Trim() + "' order by id desc ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtcname.Text = cname;
            btnadd.Enabled = true;
            lblmsg.Visible = false;
            HFccode.Value = dt.Rows[0]["ccode"].ToString();
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Company Not found in our data base, Please add company first !!";
            btnadd.Enabled = false;
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
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [EnquiryData]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ComCode = "Excel/0" + (Convert.ToInt32(dt.Rows[0]["maxid"].ToString()) + 1).ToString();
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
        if (btnadd.Text == "Add Enquiry")
        {
            GenerateComCode();
            if (!string.IsNullOrEmpty(ComCode))
            {
                SqlCommand cmd = new SqlCommand("SP_EnquiryData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@EnqCode", ComCode);
                cmd.Parameters.AddWithValue("@ccode", HFccode.Value);
                cmd.Parameters.AddWithValue("@cname", txtcname.Text.Trim());
                cmd.Parameters.AddWithValue("@remark", txtremark.Text.Trim());
                cmd.Parameters.AddWithValue("@sessionname", Session["name"].ToString());
                if (FileUpload1.HasFile)
                {
                    foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                    {
                        string filename = Path.GetFileName(postedFile.FileName);
                        string[] pdffilename = filename.Split('.');
                        string pdffilename1 = pdffilename[0];
                        string filenameExt = pdffilename[1];
                        //if (filenameExt == "pdf" || filenameExt == "PDF")
                        //{
                            string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                            postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") + pdffilename1 + time1 + "." + filenameExt);
                            cmd.Parameters.AddWithValue("@filepath", "EnquiryFiles/" + pdffilename1 + time1 + "." + filenameExt);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                        //}
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@filepath", DBNull.Value);
                }
                
                int a = 0;
                cmd.Connection.Open();
                a = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Enquiry Added Sucessfully');window.location='Addenquiry.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Saved !!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Enquiry Code Generation Problem Please Try Again !!');", true);
            }
        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update")
        {
            //SqlCommand cmd = new SqlCommand("SP_EnquiryData", con);
            //cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@Action", "Update");
            //cmd.Parameters.AddWithValue("@id", ViewState["UpdateRowId"].ToString());
            //cmd.Parameters.AddWithValue("@ccode", ComCodeUpdate);
            //cmd.Parameters.AddWithValue("@cname", txtcname.Text.Trim());
            //cmd.Parameters.AddWithValue("@oname", txtownname.Text.Trim());
            ////cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
            ////cmd.Parameters.AddWithValue("@mobile", txtmobile.Text.Trim());

            ////cmd.Parameters.AddWithValue("@billingaddress", txtbillingaddress.Text);
            ////cmd.Parameters.AddWithValue("@shippingaddress", txtshippingaddress.Text);

            ////cmd.Parameters.AddWithValue("@sessionname", hfregby.Value);
            ////cmd.Parameters.AddWithValue("@gstno", txtgstno.Text);
            //cmd.Parameters.AddWithValue("@updatedby", Session["name"].ToString());
            //int a = 0;
            //cmd.Connection.Open();
            //a = cmd.ExecuteNonQuery();
            //cmd.Connection.Close();
            //if (a > 0)
            //{
            //    //CreateHistory();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Updated Sucessfully');window.location='AddCompany.aspx';", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Updated !!');", true);
            //}
        }
        #endregion Update
    }

    
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addenquiry.aspx");
    }

    protected void txtcname_TextChanged(object sender, EventArgs e)
    {
        GetCompanyDataByName(txtcname.Text);
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
