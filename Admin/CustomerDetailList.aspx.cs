using DocumentFormat.OpenXml.ExtendedProperties;
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

public partial class Admin_CustomerDetailList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    string value = "";
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
                DdlSalesBind();
                Gvbind();
                ViewAuthorization();
            }
        }
    }
    private void ViewAuthorization()
    {
        string empcode = Session["empcode"].ToString();
        DataTable Dt = new DataTable();
        SqlDataAdapter Sd = new SqlDataAdapter("Select id from [employees] where [empcode]='" + empcode + "'", con);
        Sd.Fill(Dt);
        if (Dt.Rows.Count > 0)
        {
            string id = Dt.Rows[0]["id"].ToString();
            DataTable Dtt = new DataTable();
            SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM [DB_ProcetechTesting].[DB_ProcetechERP].[tblUserRoleAuthorization] where UserID = '" + id + "' AND PageName = 'AllCompanyList.aspx' AND PagesView = '1'", con);
            Sdd.Fill(Dtt);
            if (Dtt.Rows.Count > 0)
            {
                gvCustomerDetails.Columns[8].Visible = false;
                btnAddCompany.Visible = false;
            }
        }
    }
    private void Gvbind()
    {
        if (value == "Deleted")
        {
            value = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Record Deleted Successfully...!');;window.location.href='CustomerDetailList.aspx';", true);
        }
        string query = string.Empty;
        if (ddlsalesMainfilter.Text != "All")
        {
            query = @"SELECT * FROM [CustomerDetails] Where CustomerId = '" + ddlsalesMainfilter.Text + "'  order by Customerid desc";
        }
        else
        {
            query = @"SELECT * from [CustomerDetails] order by  UpdatedDate desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvCustomerDetails.DataSource = dt;
            gvCustomerDetails.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvCustomerDetails.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            gvCustomerDetails.DataSource = null;
            gvCustomerDetails.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvCustomerDetails.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
    }


    private void DdlSalesBind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [CustomerId],[CustomerName] FROM [CustomerDetails]  order by Customerid desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlsalesMainfilter.DataSource = dt;
            ddlsalesMainfilter.DataValueField = "CustomerId";
            ddlsalesMainfilter.DataTextField = "CustomerName";
            ddlsalesMainfilter.DataBind();
            ddlsalesMainfilter.Items.Insert(0, "All");
        }
        else
        {
            ddlsalesMainfilter.DataSource = null;
            ddlsalesMainfilter.DataBind();
            ddlsalesMainfilter.Items.Insert(0, "All");
        }
    }


    protected void GvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowEdit")
        {
            string DocType = e.CommandArgument.ToString();
            if (DocType != "")
            {
                Response.Redirect("CustomerDetails.aspx?cdd=" + encrypt(DocType.ToString()));
            }
        }
    }

    protected void GvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userid = Convert.ToInt32(gvCustomerDetails.DataKeys[e.RowIndex].Value.ToString());
        if (userid == -1)
        {

            return;
        }

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM CustomerDetails WHERE CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", userid);
            con.Open();
            value = "Deleted";
            cmd.ExecuteNonQuery();
        }
        Gvbind();
    }


    protected void GvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomerDetails.PageIndex = e.NewPageIndex;
        Gvbind();
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



    protected void txtcnamefilter_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtcnamefilter.Text.Trim()))
        {
            query = "SELECT * FROM [CustomerDetails] where  CustomerName like '" + txtcnamefilter.Text.Trim() + "%' order by Customerid desc";
        }
        else
        {
            query = "SELECT * FROM [CustomerDetails] order by Customerid desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvCustomerDetails.DataSource = dt;
            gvCustomerDetails.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvCustomerDetails.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            gvCustomerDetails.DataSource = null;
            gvCustomerDetails.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvCustomerDetails.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
    }

    protected void txtonamefilter_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;

        query = "SELECT * FROM [CustomerDetails]  order by Customerid desc";
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvCustomerDetails.DataSource = dt;
            gvCustomerDetails.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvCustomerDetails.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            gvCustomerDetails.DataSource = null;
            gvCustomerDetails.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvCustomerDetails.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
    }


    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerDetailList.aspx");
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



    protected void ddlsalesMainfilter_TextChanged(object sender, EventArgs e)
    {
        Gvbind();
    }
}