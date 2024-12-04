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

public partial class Admin_PaymentModuleList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    List<string> Lstbillno = new List<string>();
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
            Gvbind();
        }
        }
    }

    private void Gvbind()
    {
        string query = string.Empty;
        query = @"select * from tblPaymentModule where IsComplete is null order by Id desc";
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvPaymentModuleList.DataSource = dt;
            GvPaymentModuleList.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvPaymentModuleList.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            GvPaymentModuleList.DataSource = null;
            GvPaymentModuleList.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvPaymentModuleList.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
    }

    #region Filter

    protected void txtcnamefilter_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtcnamefilter.Text.Trim()))
        {
            query = "SELECT * FROM tblPaymentModule where SupplierName like '" + txtcnamefilter.Text.Trim() + "%' and IsComplete is null order by Id desc";
        }
        else
        {
            query = "select * from tblPaymentModule where IsComplete is null order by Id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvPaymentModuleList.DataSource = dt;
            GvPaymentModuleList.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvPaymentModuleList.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            GvPaymentModuleList.DataSource = null;
            GvPaymentModuleList.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvPaymentModuleList.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
        }
    }

    protected void txtSupplierBill_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtSupplierBill.Text.Trim()))
        {
            query = "SELECT * FROM tblPaymentModule where SupplierBillNo='" + txtSupplierBill.Text.Trim() + "' and IsComplete is null order by Id desc";
        }
        else
        {
            query = "SELECT * FROM tblPaymentModule where IsComplete is null order by Id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvPaymentModuleList.DataSource = dt;
            GvPaymentModuleList.DataBind();
        }
        else
        {
            GvPaymentModuleList.DataSource = null;
            GvPaymentModuleList.DataBind();
        }
    }
    #endregion Filter

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentModule.aspx");
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

  

    

   
}