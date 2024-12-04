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

public partial class Admin_PaymentList : System.Web.UI.Page
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

    protected void GvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowEdit")
        {
            Response.Redirect("Payment.aspx?Id=" + encrypt(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();
                Response.Write("<script>window.open ('PaymentPDF.aspx?Id=" + encrypt(e.CommandArgument.ToString()) + "','_blank');</script>");
            }
        }
        if (e.CommandName == "RowDelete")
        {
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter cmdget = new SqlDataAdapter("select BillNo from tblPaymentDtls WHERE HeaderId =" + Convert.ToInt32(e.CommandArgument.ToString()) + "", con);
            cmdget.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                string billno = row.Field<string>("BillNo");
                SqlCommand Cmdupdt = new SqlCommand("UPDATE tblPurchaseBillHdr SET IsPaid=NULL WHERE SupplierBillNo='" + billno + "'", con);
                Cmdupdt.ExecuteNonQuery();

            }

            SqlCommand Cmd = new SqlCommand("delete from tblPaymentHdrs WHERE Id=@Id", con);
            Cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmd.ExecuteNonQuery();

            SqlCommand Cmddtl = new SqlCommand("delete from tblPaymentDtls WHERE HeaderID=@Id", con);
            Cmddtl.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmddtl.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Deleted Sucessfully');window.location.href='PaymentList.aspx';", true);

        }
        if (e.CommandName == "Suppliername")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                GetSupplierDataPopup(e.CommandArgument.ToString());
                this.modelprofile.Show();
            }
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
            //lblemail.Text = dt.Rows[0]["EmailID"].ToString();
            // txtsupplierCode.Text = dt.Rows[0]["SupplierCode"].ToString();
            // lblbillingaddress.Text = dt.Rows[0]["BillToAddress"].ToString();
            // lblshipaddress.Text = dt.Rows[0]["ShipToAddress"].ToString();
            // lblcountry.Text = dt.Rows[0]["Country"].ToString();
            // lblState.Text = dt.Rows[0]["StateName"].ToString();
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
                com.CommandText = "select DISTINCT PartyName from tblPaymentHdrs where " + "PartyName like @Search + '%' AND isdeleted='0'  ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> PartyName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        PartyName.Add(sdr["PartyName"].ToString());
                    }
                }
                con.Close();
                return PartyName;
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

    protected void GvBindList()
    {
        try
        {
            DataTable dttt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter(@"select * from tblPaymentHdrs AS A
LEFT JOIN tblSupplierMaster AS SM ON SM.SupplierName = A.PartyName where A.isdeleted ='0' order by A.CreatedOn DESC", con);
            sad.Fill(dttt);
            GvPayment.DataSource = dttt;
            GvPayment.DataBind();
            GvPayment.EmptyDataText = "Record Not Found";
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void txtpartyname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sad1 = new SqlDataAdapter(@"select * from tblPaymentHdrs AS A
LEFT JOIN tblSupplierMaster AS SM ON SM.SupplierName = A.PartyName where A.PartyName ='" + txtpartyname.Text + "' AND A.isdeleted='0'", con);
            sad1.Fill(dt);
            GvPayment.DataSource = dt;
            GvPayment.DataBind();
            GvPayment.EmptyDataText = "Record Not Found";
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentList.aspx");
    }

    protected void GvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    con.Open();
            //    int id = Convert.ToInt32(GvPayment.DataKeys[e.Row.RowIndex].Values[0]);
            //    SqlCommand cmd = new SqlCommand("select sum(CAST(Total as float)) from tblPaymentDtls where HeaderID='" + id + "'", con);
            //    Object Procnt = cmd.ExecuteScalar();
            //    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            //    lblAmount.Text = Procnt == null ? "0" : Procnt.ToString();
            //    con.Close();
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                con.Open();
                int id = Convert.ToInt32(GvPayment.DataKeys[e.Row.RowIndex].Values[0]);
                Label lblAgainst = (Label)e.Row.FindControl("lblAgainst");

                LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
                LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
                LinkButton btnPDF = e.Row.FindControl("btnPDF") as LinkButton;

                string empcode = Session["empcode"].ToString();
                DataTable Dt = new DataTable();
                SqlDataAdapter Sd = new SqlDataAdapter("Select id from [employees] where [empcode]='" + empcode + "'", con);
                Sd.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    string idd = Dt.Rows[0]["id"].ToString();
                    DataTable Dtt = new DataTable();
                    SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + idd + "' AND PageName = 'PaymentList.aspx' AND PagesView = '1'", con);
                    Sdd.Fill(Dtt);
                    if (Dtt.Rows.Count > 0)
                    {
                        btnAddPayment.Visible = false;
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                        btnPDF.Visible = true;
                    }
                }


                if (lblAgainst.Text == "Advance")
                {
                    SqlCommand cmd = new SqlCommand("select Amount from tblPaymentHdrs where Id='" + id + "'", con);
                    Object Procnt = cmd.ExecuteScalar();
                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");



                    lblAmount.Text = Procnt == null ? "0" : Math.Round(Convert.ToDouble(Procnt.ToString())).ToString();

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
                    SqlCommand cmd = new SqlCommand("select sum(CAST(Paid as float)) from tblPaymentDtls where HeaderID='" + id + "'", con);
                    string Procnt = cmd.ExecuteScalar().ToString();
                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                    lblAmount.Text = Procnt == "" ? "0" : Math.Round(Convert.ToDouble(Procnt.ToString())).ToString();
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}