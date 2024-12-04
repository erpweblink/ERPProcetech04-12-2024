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

public partial class Admin_PaymentTermsMaster : System.Web.UI.Page
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
                if (Request.QueryString["ID"] != null )
                {

                    string ID = Decrypt(Request.QueryString["ID"].ToString());
                    GetTermslIstbyID( ID);
                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                    ViewState["ID"] = ID;
                   

                }
                else
                {
                    btnadd.Visible = true;
                    btnupdate.Visible = false;
                }
              
            }
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SP_PaymnetTerms", con);
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Action", "Insert");
        cmd.Parameters.AddWithValue("@Terms",  txtpaymentTerms.Text);
        cmd.Parameters.AddWithValue("@Isdeleted", 0);
        cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
        int a = 0;
        con.Open();
        a = cmd.ExecuteNonQuery();
        con.Close();
        if (a > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Saved Sucessfully');window.location='PaymentTermsList.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Saved !!');", true);
        }
    }
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

    public void GetTermslIstbyID(string ID)

    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("[SP_PaymnetTerms]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetListbyid"));
                    cmd.Parameters.Add(new SqlParameter("@ID",  ID));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {

                        txtpaymentTerms.Text = Dt.Rows[0]["Terms"].ToString();
                    }
                    else
                    {
                    
                    }

                }
            }
        }
        catch (Exception ex)
        {

            //throw;
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SP_PaymnetTerms", con);
        cmd.Parameters.Clear();
        string ID = ViewState["ID"].ToString();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Action", "UpdateRecords");
        cmd.Parameters.AddWithValue("@Terms", txtpaymentTerms.Text);
        cmd.Parameters.AddWithValue("@Id", ID);

        int a = 0;
        con.Open();
        a = cmd.ExecuteNonQuery();
        con.Close();
        if (a > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Saved Sucessfully');window.location='PaymentTermsList.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Saved !!');", true);
        }
    }
}