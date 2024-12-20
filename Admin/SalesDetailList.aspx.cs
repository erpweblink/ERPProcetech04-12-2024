using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Record Deleted Successfully...!');;window.location.href='SalesDetailList.aspx';", true);
        }
        string query = string.Empty;
        if (txtDateSearchfrom.Text != "" && txtDateSearchto.Text != "")
        {
            DateTime date1 = DateTime.ParseExact(txtDateSearchfrom.Text, "yyyy-MM-dd", null);
            DateTime date2 = DateTime.ParseExact(txtDateSearchto.Text, "yyyy-MM-dd", null);

            string formDate = date1.ToString("yyyy-dd-MM");
            string ToDate = date2.ToString("yyyy-dd-MM");

            query = @"SELECT * FROM [CustomerDetails] WHERE InvoiceDate >='" + formDate + "' AND InvoiceDate <='" + ToDate + "' order by Customerid desc";
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
                Response.Redirect("SalesDetails.aspx?cdd=" + encrypt(DocType.ToString()));
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
        Response.Redirect("SalesDetailList.aspx");
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

    protected void gvCustomerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Find the label control in the current row
            Label lblInvoiceDate = (Label)e.Row.FindControl("lblInvoiceDate");

            // Get the InvoiceDate value from the DataItem
            if (lblInvoiceDate != null)
            {
                string invoiceDate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceDate"));
                if (invoiceDate != "")
                {
                    DateTime date = DateTime.ParseExact(invoiceDate, "yyyy-dd-MM", CultureInfo.InvariantCulture);
                    lblInvoiceDate.Text = date.ToString("dd-MM-yyyy");
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            decimal totalBasic = 0;
            decimal grandTotal = 0;
            decimal TotalQuantity = 0;



            // Loop through the data rows to calculate the totals
            foreach (GridViewRow row in gvCustomerDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Calculate the total for each column
                    totalBasic += Convert.ToDecimal((row.FindControl("lblBasicAmount") as Label).Text);
                    string val = (row.FindControl("lblGSTAmount") as Label).Text;
                    if (val != "")
                    {
                        grandTotal += Convert.ToDecimal((row.FindControl("lblGSTAmount") as Label).Text);
                    }
                    TotalQuantity += Convert.ToDecimal((row.FindControl("lblGrandTotal") as Label).Text);
                }
            }

        // Display the totals in the footer labels
        (e.Row.FindControl("lblTotalBasicAmount") as Label).Text = "Basic Amt:<br /><br /><br /> ₹ " + totalBasic.ToString();
            (e.Row.FindControl("lblTotalGSTAmount") as Label).Text = "GST Amt:<br /><br /><br /> ₹ " + grandTotal.ToString();
            (e.Row.FindControl("lblTotalGrandTotal") as Label).Text = "Grand Amt:  <br /><br /><br /> ₹ " + TotalQuantity.ToString();
        }
    }

    protected void btnexportexcel_Click(object sender, EventArgs e)
    {
        try
        {                     
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[10]
            {
            new DataColumn("S No."),
            new DataColumn("Invoice No"),
            new DataColumn("Invoice Date"),
            new DataColumn("Customer Name"),
            new DataColumn("Basic Amount"),
            new DataColumn("CGST"),
            new DataColumn("SGST"),
            new DataColumn("IGST"),
            new DataColumn("GST Amount"),
            new DataColumn("Grand Total")
            });

           
            string totalBasicAmount = (gvCustomerDetails.FooterRow.FindControl("lblTotalBasicAmount") as Label).Text
                 .Replace("<br />", "").Trim() ?? "0";

            string totalGSTAmount = (gvCustomerDetails.FooterRow.FindControl("lblTotalGSTAmount") as Label).Text
                .Replace("<br />", "").Trim() ?? "0";

            string totalGrandTotal = (gvCustomerDetails.FooterRow.FindControl("lblTotalGrandTotal") as Label).Text
                .Replace("<br />", "").Trim() ?? "0";


            foreach (GridViewRow row in gvCustomerDetails.Rows)
            {
              
                string lblSno = (row.FindControl("lblsno") as Label).Text;
                string lblInvoiceNo = (row.FindControl("lblInvoiceNo") as Label).Text;
                string lblInvoiceDate = (row.FindControl("lblInvoiceDate") as Label).Text;
                string lblCustomerName = (row.FindControl("lblcustnamr") as Label).Text;
                string lblBasicAmount = (row.FindControl("lblBasicAmount") as Label).Text;
                string lblCGST = (row.FindControl("Label1") as Label).Text;
                string lblSGST = (row.FindControl("Label2") as Label).Text;
                string lblIGST = (row.FindControl("Label3") as Label).Text;
                string lblGSTAmount = (row.FindControl("lblGSTAmount") as Label).Text;
                string lblGrandTotal = (row.FindControl("lblGrandTotal") as Label).Text;

                dt.Rows.Add(lblSno, lblInvoiceNo, lblInvoiceDate, lblCustomerName, lblBasicAmount, lblCGST, lblSGST, lblIGST, lblGSTAmount, lblGrandTotal);
            }
            dt.Rows.Add("Total", "", "", "", totalBasicAmount, "", "", "", totalGSTAmount, totalGrandTotal);

            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            string todayDate = DateTime.Now.ToString("dd-MM-yyyy");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=SalesDetails_" + todayDate + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
           
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }

            GridView1.RenderControl(hw);
           
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {           
            throw ex;
        }
    }


}