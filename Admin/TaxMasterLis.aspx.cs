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

public partial class Admin_TaxMasterLis : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Getorderlist();


        }

    }

    public void Getorderlist()

    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("[SP_TaxMaster]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetGstDeatils"));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        GvTCList.DataSource = Dt;
                        GvTCList.DataBind();

                    }
                    else
                    {
                        GvTCList.EmptyDataText = "<span class='empty-data-message'>No records found.</span>";
                        GvTCList.DataBind();
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

    protected void GvTCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowEdit")
        {
            Response.Redirect("TaxMaster.aspx?Id=" + encrypt(e.CommandArgument.ToString()));
        }

        if (e.CommandName == "RowDelete")
        {

            con.Open();
            SqlCommand CmduptDtl = new SqlCommand("update TblTaxMaster set  IsDeleted = 1  where Id=@Id", con);
            CmduptDtl.Parameters.AddWithValue("@Id", Convert.ToInt32(e.CommandArgument.ToString()));
            CmduptDtl.ExecuteNonQuery();
            con.Close();
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
}