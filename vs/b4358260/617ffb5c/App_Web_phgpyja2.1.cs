#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\UnitMaster.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A2B807E137DCC50671FB33F8B8FC7E58466BDEFA"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\UnitMaster.aspx.cs"
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
public partial class Admin_UnitMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateHistorymsg = string.Empty; //regdate = string.Empty;
            if (Request.QueryString["ID"] != null)
            {
                ViewState["UpdateRowId"] = Decrypt(Request.QueryString["ID"].ToString());
                GetUnitData(ViewState["UpdateRowId"].ToString());
            }
            else
            {
                ViewState["ContactDetails"] = dt;
            }
        }
    }

    static string regdate = string.Empty;
    protected void GetUnitData(string id)
    {
        string query1 = string.Empty;
        query1 = @"select * from tblUnit where Id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtUnitName.Text = dt.Rows[0]["Unit"].ToString();
            btnadd.Text = "Update Unit";
        }
    }

    static string UpdateHistorymsg = string.Empty;

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
        #region Insert
        if (btnadd.Text == "Add Unit")
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tblUnit([Unit],[IsActive],[CreatedBy],[CreatedOn])VALUES(@Unit,@IsActive,@CreatedBy,@CreatedOn)", con);
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Unit", txtUnitName.Text.Trim());
            bool flg = true;
            if (chkIsactive.Checked == true)
                flg = true;
            else
                flg = false;
            cmd.Parameters.AddWithValue("@IsActive", flg);
            cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
            int a = 0;
            con.Open();
            a = cmd.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Sucessfully');window.location.href='UnitMaster.aspx';", true);

        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update Unit")
        {
            SqlCommand cmd = new SqlCommand("UPDATE tblUnit SET [Unit] = @Unit,[IsActive] = @IsActive,[CreatedBy] = @CreatedBy,[CreatedOn] = @CreatedOn WHERE Id=@ID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(ViewState["UpdateRowId"].ToString()));
            cmd.Parameters.AddWithValue("@Unit", txtUnitName.Text.Trim());
            bool flg = true;
            if (chkIsactive.Checked == true)
                flg = true;
            else
                flg = false;
            cmd.Parameters.AddWithValue("@IsActive", flg);
            cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
            int a = 0;
            cmd.Connection.Open();
            a = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Updated Sucessfully');window.location.href='UnitMaster.aspx';", true);
        }
        #endregion Update
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnitMaster.aspx");
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetUnitList(string prefixText, int count)
    {
        return AutoFillUnitName(prefixText);
    }

    public static List<string> AutoFillUnitName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [Unit] from tblUnit where " + "Unit like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> UnitNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        UnitNames.Add(sdr["Unit"].ToString());
                    }
                }
                con.Close();
                return UnitNames;
            }
        }
    }

    protected void txtUnitName_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [Unit] FROM [tblUnit] where Unit='" + txtUnitName.Text.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Unit Already Existing !!";
            btnadd.Enabled = false;
        }
        else
        {
            btnadd.Enabled = true;
            lblmsg.Visible = false;
        }
    }

}

#line default
#line hidden
