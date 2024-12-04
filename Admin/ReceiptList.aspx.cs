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
using System.Drawing;

public partial class Admin_ReceiptList : System.Web.UI.Page
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
                GvBindList();
            }
        }
    }

    protected void GvBindList()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter sad = new SqlDataAdapter("select * from tblReceiptHdr AS A LEFT JOIN Company AS C ON A.Partyname=C.cname where A.isdeleted='0' order by A.Createddate DESC", con);
        sad.Fill(dt);
        GvRecipt.DataSource = dt;
        GvRecipt.DataBind();
        GvRecipt.EmptyDataText = "Record Not Found";
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetPartyList(string prefixText, int count)
    {
        return AutoFillPartylist(prefixText);
    }

    public static List<string> AutoFillPartylist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT Partyname from tblReceiptHdr where " + "Partyname like @Search + '%' AND isdeleted='0'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> Partyname = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Partyname.Add(sdr["Partyname"].ToString());
                    }
                }
                con.Close();
                return Partyname;
            }

        }
    }

    protected void txtpartyname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("select * from tblReceiptHdr AS A LEFT JOIN Company AS C ON A.Partyname=C.cname where Partyname='" + txtpartyname.Text + "' AND A.isdeleted='0'", con);
            sad.Fill(dt);
            GvRecipt.DataSource = dt;
            GvRecipt.DataBind();
            GvRecipt.EmptyDataText = "Record Not Found";

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiptList.aspx");
    }

    protected void GvRecipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowEdit")
        {
            Response.Redirect("Receipt.aspx?Id=" + encrypt(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();
                Response.Write("<script>window.open ('ReceiptPDF.aspx?Id=" + encrypt(e.CommandArgument.ToString()) + "','_blank');</script>");


            }
        }
        if (e.CommandName == "RowDelete")
        {
            con.Open();

            SqlCommand cmdget = new SqlCommand("select InvoiceNo from tblReceiptDtls WHERE HeaderID=@Id", con);
            cmdget.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            string invoiceNo = cmdget.ExecuteScalar() == null ? "" : cmdget.ExecuteScalar().ToString();

            SqlCommand cmdupdate = new SqlCommand("update tblTaxInvoiceHdr set IsPaid=null where InvoiceNo='" + invoiceNo + "'", con);
            cmdupdate.ExecuteNonQuery();

            SqlCommand Cmd = new SqlCommand("delete from tblReceiptHdr WHERE Id=@Id", con);
            Cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmd.ExecuteNonQuery();

            SqlCommand Cmddtl = new SqlCommand("delete from tblReceiptDtls WHERE HeaderID=@Id", con);
            Cmddtl.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmddtl.ExecuteNonQuery();

            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Deleted Sucessfully');window.location.href='ReceiptList.aspx';", true);

        }
        if (e.CommandName == "CompanyData")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                GetCompanyDataPopup(e.CommandArgument.ToString());
                //GetCompanyDataBDEPopup(e.CommandArgument.ToString());
                this.modelprofile.Show();
            }
        }
    }
    private void GetCompanyDataPopup(string id)
    {
        string query1 = string.Empty;
        query1 = @"SELECT A.[ccode],A.[cname],A.[oname1],A.[oname2],A.[oname3],A.[oname4],A.[oname5],A.[email1],A.[mobile1],A.[mobile2],
      A.[mobile3],A.[mobile4],A.[mobile5],A.[billingaddress],[shippingaddress],format(A.[regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],
A.[sessionname],A.[gstno],B.name,B.email as Empemail,A.[desig1],A.[desig2],A.[desig3],A.[desig4],A.[desig5], A.CustomerCode FROM [Company]
A join employees B on A.sessionname=B.name LEFT JOIN tblReceiptHdr AS RH ON RH.Partyname=A.cname
where RH.Id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            lblcname.Text = dt.Rows[0]["cname"].ToString();
            lblcusomercode.Text = dt.Rows[0]["CustomerCode"].ToString();
            lblRegdate.Text = dt.Rows[0]["regdate"].ToString();
            lblregBy.Text = dt.Rows[0]["name"].ToString();
            lblgstno.Text = dt.Rows[0]["gstno"].ToString();
            ViewState["CurrentSalesEmail"] = dt.Rows[0]["Empemail"].ToString();// Current Sales Email


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

    protected void GvRecipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                con.Open();
                int id = Convert.ToInt32(GvRecipt.DataKeys[e.Row.RowIndex].Values[0]);
                Label lblAgainst = (Label)e.Row.FindControl("lblAgainst");
                LinkButton btnedit = e.Row.FindControl("btnedit") as LinkButton;
                LinkButton btndelete = e.Row.FindControl("btndelete") as LinkButton;
                LinkButton btnpdf = e.Row.FindControl("btnpdf") as LinkButton;
                Label lblGvstatus = (Label)e.Row.FindControl("lblGvstatus");

                if (lblAgainst.Text == "Advance")
                {
                    SqlCommand cmd = new SqlCommand("select Amount from tblReceiptHdr where Id='" + id + "'", con);
                    Object Procnt = cmd.ExecuteScalar();
                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                    lblAmount.Text = Procnt == null ? "0" : Procnt.ToString();

                    if (lblAmount.Text == "0")
                    {
                        e.Row.Visible = false;
                    }
                    else
                    {
                        e.Row.Visible = true;
                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select sum(CAST(Paid as float)) from tblReceiptDtls where HeaderID='" + id + "'", con);
                    Object Procnt = cmd.ExecuteScalar();
                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                    lblAmount.Text = Procnt == null ? "0" : Procnt.ToString();


                    //btnedit.Visible = false;
                    //btndelete.Visible = false;

                    lblGvstatus.Text = "Paid";
                    lblGvstatus.ForeColor = Color.Green;
                }
                con.Close();

                string empcode = Session["empcode"].ToString();
                DataTable Dt = new DataTable();
                SqlDataAdapter Sd = new SqlDataAdapter("Select id from [employees] where [empcode]='" + empcode + "'", con);
                Sd.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    string idd = Dt.Rows[0]["id"].ToString();
                    DataTable Dtt = new DataTable();
                    SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + idd + "' AND PageName = 'ReceiptList.aspx' AND PagesView = '1'", con);
                    Sdd.Fill(Dtt);
                    if (Dtt.Rows.Count > 0)
                    {
                        btnAddReceipt.Visible = false;
                        btnedit.Visible = false;
                        btndelete.Visible = false;
                        btnpdf.Visible = true;
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}