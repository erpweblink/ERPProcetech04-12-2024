using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_TestCertificateList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

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
                GVBinddata();
            }
        }
    }

    protected void GVBinddata()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("select * from [tblTestCertificateHdr] order by CreatedOn DESC", con);
            sad.Fill(dt);
            GvTestCertificatelist.DataSource = dt;
            GvTestCertificatelist.DataBind();
            GvTestCertificatelist.EmptyDataText = "Record Not Found";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvTestCertificatelist.ClientID + "', 400, 1020 , 40 ,true); </script>", false);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestCertificateList.aspx");
    }

    protected void GvTestCertificatelist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Response.Write("<script>window.open ('TestCertificate_PDF.aspx?Id=" + encrypt(e.CommandArgument.ToString()) + "','_blank');</script>");
            }
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
                com.CommandText = "select DISTINCT CustomerName from tblTestCertificateHdr where " + "CustomerName like @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> CustomerName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CustomerName.Add(sdr["CustomerName"].ToString());
                    }
                }
                con.Close();
                return CustomerName;
            }

        }
    }

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("select * from [tblTestCertificateHdr] where CustomerName='" + txtCustomerName.Text + "' order by CreatedOn DESC", con);
            sad.Fill(dtt);
            GvTestCertificatelist.DataSource = dtt;
            GvTestCertificatelist.DataBind();
            GvTestCertificatelist.EmptyDataText = "Record Not Found";
        }
        catch (Exception)
        {

            throw;
        }
    }
}