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
public partial class Admin_SubCategoryMaster : System.Web.UI.Page
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
                GetSubCategoryData(ViewState["UpdateRowId"].ToString());
            }
            else
            {
                Bindcategory();
                ViewState["ContactDetails"] = dt;
            }
        }
    }

    protected void Bindcategory()
    {

        string com = "Select * from tblCategory";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddlCategory.DataSource = dt;
        ddlCategory.DataBind();
        ddlCategory.DataTextField = "Category";
        ddlCategory.DataValueField = "Category";
        ddlCategory.DataBind();

        ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
    }

    static string regdate = string.Empty;
    protected void GetSubCategoryData(string id)
    {
        string query1 = string.Empty;
        query1 = @"select * from tblSubCategory where Id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Bindcategory();
            txtSubCategoryName.Text = dt.Rows[0]["SubCategory"].ToString();
            ddlCategory.Text = dt.Rows[0]["Category"].ToString();
            btnadd.Text = "Update SubCategory";
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
        if (btnadd.Text == "Add SubCategory")
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tblSubCategory([Category],[SubCategory],[IsActive],[CreatedBy],[CreatedOn])VALUES(@Category,@SubCategory,@IsActive,@CreatedBy,@CreatedOn)", con);
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@SubCategory", txtSubCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("@Category", ddlCategory.Text);
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

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Sucessfully');window.location.href='SubCategoryMaster.aspx';", true);

        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update SubCategory")
        {
            SqlCommand cmd = new SqlCommand("UPDATE tblCategory SET [Category] = @Category,[SubCategory] = @SubCategory,[IsActive] = @IsActive,[CreatedBy] = @CreatedBy,[CreatedOn] = @CreatedOn WHERE Id=@ID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(ViewState["UpdateRowId"].ToString()));
            cmd.Parameters.AddWithValue("@Category", txtSubCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("@SubCategory", ddlCategory.Text);
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

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Updated Sucessfully');window.location.href='SubCategoryMaster.aspx';", true);
        }
        #endregion Update
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubCategoryMaster.aspx");
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCategoryList(string prefixText, int count)
    {
        return AutoFillCategoryName(prefixText);
    }

    public static List<string> AutoFillCategoryName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [SubCategory] from tblSubCategory where " + "SubCategory like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> CategoryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CategoryNames.Add(sdr["SubCategory"].ToString());
                    }
                }
                con.Close();
                return CategoryNames;
            }
        }
    }

    protected void txtSubCategoryName_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [SubCategory] FROM [tblSubCategory] where SubCategory='" + txtSubCategoryName.Text.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Category Already Existing !!";
            btnadd.Enabled = false;
        }
        else
        {
            btnadd.Enabled = true;
            lblmsg.Visible = false;
        }
    }

}