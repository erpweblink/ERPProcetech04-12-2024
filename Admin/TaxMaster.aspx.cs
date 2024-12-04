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

public partial class Admin_TaxMaster : System.Web.UI.Page
{
    string id;
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
                if (Request.QueryString["Id"] != null)
                {
                    id = Decrypt(Request.QueryString["Id"].ToString());
                    LoadData(id);
                    hdnId.Value = id;
                }
                else
                {
                    btnadd.Visible = true;
                }
            }
        }
    }


    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("[SP_TaxMaster]", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Jurisdictions", ddljusidctions.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@TaxTypes", ddltaxtype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@TaxCategory", ddlgst.Text);
            cmd.Parameters.AddWithValue("@Effective", txtEffectiveDate.Text);
            cmd.Parameters.AddWithValue("@GstPercentage", "");  // Make sure this value is set correctly
            cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
            cmd.Parameters.AddWithValue("@CGST", txtcgst.Text);
            cmd.Parameters.AddWithValue("@SGST", txtSgst.Text);
            cmd.Parameters.AddWithValue("@IGST", txtigst.Text);
            cmd.Parameters.AddWithValue("@Action", "insertTax");

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected < 0)
            {
                // Successfully inserted
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Tax Added Sucessfully');window.location='TaxMasterLis.aspx';", true);
            }
            else
            {
                // No rows affected
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('failed to save');window.location='TaxMasterLis.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            Response.Write("Error: " + ex.Message);
        }
        finally
        {
            // Ensure the connection is always closed
            con.Close();
        }

    }

    protected void LoadData(string id)
    {
        btnupdate.Visible = true;
        ddlgst.Enabled = false;
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("[SP_TaxMaster]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "GetGstDeatilsbyID"));
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                adapter.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    ddlgst.Text = Dt.Rows[0]["TaxCategory"].ToString();
                    txtcgst.Text = Dt.Rows[0]["CGST"].ToString();
                    txtSgst.Text = Dt.Rows[0]["SGST"].ToString();
                    txtigst.Text = Dt.Rows[0]["IGST"].ToString();
                }
            }
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

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("[SP_TaxMaster]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Jurisdictions", ddljusidctions.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@TaxTypes", ddltaxtype.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@TaxCategory", ddlgst.Text);
                cmd.Parameters.AddWithValue("@Effective", txtEffectiveDate.Text);
                cmd.Parameters.AddWithValue("@GstPercentage", "");  // Make sure this value is set correctly
                cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                cmd.Parameters.AddWithValue("@CGST", txtcgst.Text);
                cmd.Parameters.AddWithValue("@SGST", txtSgst.Text);
                cmd.Parameters.AddWithValue("@IGST", txtigst.Text);
                cmd.Parameters.AddWithValue("@Action", "TaxTypeupdate");
                cmd.Parameters.AddWithValue("@ID", hdnId.Value);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected < 0)
                {
                    // Successfully inserted
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Tax Added Sucessfully');window.location='TaxMasterLis.aspx';", true);
                }
                else
                {
                    // No rows affected
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('failed to save');window.location='TaxMasterLis.aspx';", true);
                }
            }

            catch (Exception ex)
            {
                // Handle exception
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is always closed
                connection.Close();
            }
        }
    }

    protected void ddltaxtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltaxtype.SelectedValue == "CGSTSGST")
        {
            txtigst.Text = "0";
            txtigst.Enabled = false;
            txtcgst.Enabled = true;
            txtSgst.Enabled = true;
        }
        if (ddltaxtype.SelectedValue == "IGST")
        {
            txtcgst.Text = "0";
            txtSgst.Text = "0";
            txtcgst.Enabled = false;
            txtSgst.Enabled = false;
            txtigst.Enabled = true;
        }
    }
}
