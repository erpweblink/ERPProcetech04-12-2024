#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\RoleMaster.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8878A3533D566884B3619470C4B461395D499AEF"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\RoleMaster.aspx.cs"
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

public partial class Admin_RoleMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Grid();
            if (Request.QueryString["id"] != null)
            {
                string id = Decrypt(Request.QueryString["id"].ToString());
                Load_Record(id);
                btnadd.Text = "Update";
                hhd.Value = id;
            }
        }
    }

    private void Bind_Grid()
    {
        try
        {
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("SELECT * FROM tbl_RoleMaster", con);
            Da.Fill(Dt);

            if (Dt.Rows.Count>0)
            {
                GvRole.DataSource = Dt;
                GvRole.DataBind();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void Load_Record(string id)
    {
        try
        {
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("SELECT * FROM tbl_RoleMaster WHERE id='" + id + "' ", con);
            Da.Fill(Dt);

            if (Dt.Rows.Count>0)
            {
                txtrolename.Text = Dt.Rows[0]["Role"].ToString();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void Save_Record()
    {
        try
        {
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("SELECT * FROM tbl_RoleMaster WHERE Role='" + txtrolename.Text + "'", con);
            Da.Fill(Dt);

            if (btnadd.Text == "Update")
            {
                SqlCommand Cmd = new SqlCommand("UPDATE tbl_RoleMaster  SET  Role=@Role,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate WHERE id='"+id+"'", con);

                Cmd.Parameters.AddWithValue("@Role", txtrolename.Text);
                Cmd.Parameters.AddWithValue("@UpdatedBy", Session["name"].ToString());
                Cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);

                con.Open();
                Cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Updated Sucessfully  !!');", true);
            }
            else
            {
                if (Dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Alredy Save !!');", true);
                }
                else
                {
                    SqlCommand Cmd = new SqlCommand("INSERT INTO tbl_RoleMaster (Role,CreatedBy,CreatedDate) VALUES (@Role,@CreatedBy,@CreatedDate)", con);

                    Cmd.Parameters.AddWithValue("@Role", txtrolename.Text);
                    Cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                    Cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    con.Open();
                    Cmd.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Save Sucessfully  !!');", true);

                }
            }

            
        }
        catch (Exception)
        {

            throw;
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

    protected void btnadd_Click(object sender, EventArgs e)
    {
        Save_Record();
    }

    static int id;
    protected void GvRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "RowEdit")
        {
            Response.Redirect("RoleMaster.aspx?id=" + encrypt(e.CommandArgument.ToString()) + "");
        }
    }
}

#line default
#line hidden
