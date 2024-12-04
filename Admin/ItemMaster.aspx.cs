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
using System.Drawing;

public partial class Admin_ItemMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
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
                fillddlUnit();
                UpdateHistorymsg = string.Empty; //regdate = string.Empty;
                GetTaxType();
                if (Request.QueryString["ID"] != null)
                {
                    Bindcategory();
                    ViewState["UpdateRowId"] = Decrypt(Request.QueryString["ID"].ToString());
                    GetItemData(ViewState["UpdateRowId"].ToString());

                }
                else
                {
                    ViewState["ContactDetails"] = dt;
                    txtItemcode.Text = GenerateComCode();
                    Bindcategory();
                }
            }
        }
    }
    private void fillddlUnit()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select distinct Unit from tblUnit", con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);

        if (dtpt.Rows.Count > 0)
        {
            txtStorageUnit.DataSource = dtpt;
            txtStorageUnit.DataValueField = "Unit";
            txtStorageUnit.DataTextField = "Unit";
            txtStorageUnit.DataBind();
        }
        txtStorageUnit.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }
    static string regdate = string.Empty;

    protected void GetItemData(string id)
    {
        string query1 = string.Empty;
        query1 = @"select * from tblItemMaster where Id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtItemName.Text = dt.Rows[0]["ItemName"].ToString();
            txtDescription.Text = dt.Rows[0]["ItemDescription"].ToString();
            txtItemcode.Text = dt.Rows[0]["Itemcode"].ToString();
            txtHSNNo.Text = dt.Rows[0]["HSN"].ToString();
            txtPurchaseRate.Text = dt.Rows[0]["PurchaseRate"].ToString();
            txtStorageUnit.Text = dt.Rows[0]["StorageUnit"].ToString();
            txtGSTType.Text = dt.Rows[0]["GST_Type"].ToString();
            ddlCgst.Text = dt.Rows[0]["CGST"].ToString();
            ddlSgst.Text = dt.Rows[0]["SGST"].ToString();
            ddlIgst.Text = dt.Rows[0]["IGST"].ToString();
            if (txtGSTType.Text == "IGST")
            {
                ddlIgst.Enabled = true;
                ddlCgst.Enabled = false;
                ddlSgst.Enabled = false;
            }
            else if (txtGSTType.Text == "CGST_SGST")
            {
                ddlIgst.Enabled = false;
                ddlCgst.Enabled = true;
                ddlSgst.Enabled = true;
            }
            else
            {
                ddlIgst.Enabled = true;
                ddlCgst.Enabled = true;
                ddlSgst.Enabled = true;
            }

            ddlSalable.Text = dt.Rows[0]["Salable"].ToString();
            txtOpeningStock.Text = dt.Rows[0]["OpeningStock"].ToString();
            txtSafetyStock.Text = dt.Rows[0]["SafetyStock"].ToString();
            txtminOrderqty.Text = dt.Rows[0]["MinOrderQty"].ToString();
            ddlType.Text = dt.Rows[0]["Type"].ToString();
            txtStockLocation.Text = dt.Rows[0]["StockLocation"].ToString();
            txtShelfLife.Text = dt.Rows[0]["ShelfLife"].ToString();

            txtSupplierName.Text = dt.Rows[0]["SupplierName"].ToString();

            string Category = dt.Rows[0]["Category"].ToString();
            string SubCategory = dt.Rows[0]["SubCategory"].ToString();

            hdnFile.Value = dt.Rows[0]["DrawingFile"].ToString();


            if (hdnFile.Value != "")
            {
                lblFilemsg.Text = "File is already exists";
                lblFilemsg.Visible = true;
                lblFilemsg.ForeColor = Color.Green;
            }
            else
            {
                lblFilemsg.Text = "File is not found";
                lblFilemsg.Visible = true;
                lblFilemsg.ForeColor = Color.Red;
            }

            if (Category == "0" || Category == "")
            {
            }
            else
            {
                ddlCategory.Items.FindByText(Category).Selected = true;
                BindSubcategory();
            }

            if (SubCategory == "0" || SubCategory == "")
            {

            }
            else
            {
                ddlSubcategory.Items.FindByText(SubCategory).Selected = true;
            }


            btnadd.Text = "Update Item";
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

    protected void BindSubcategory()
    {
        string Category = ddlCategory.SelectedItem.ToString();

        string com = "Select * from tblSubCategory where Category='" + Category.Trim() + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddlSubcategory.DataSource = dt;
        ddlSubcategory.DataBind();
        ddlSubcategory.DataTextField = "SubCategory";
        ddlSubcategory.DataValueField = "SubCategory";
        ddlSubcategory.DataBind();

        ddlSubcategory.Items.Insert(0, new ListItem("--Select Sub Category--", "0"));
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

    protected string GenerateComCode()
    {
        string ItemCode = string.Empty;
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([Id]) as maxid FROM [tblItemMaster]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
            ItemCode = "PASup/0" + (maxid + 1).ToString();
        }
        else
        {
            ItemCode = string.Empty;
        }
        return ItemCode;
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        #region Insert
        if (btnadd.Text == "Add Item")
        {
            string Itemcode = GenerateComCode();
            if (!string.IsNullOrEmpty(Itemcode))
            {
                SqlCommand cmd = new SqlCommand("SP_ItemMaster", con);
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;

                if (fileuploadDrawing.HasFile)
                {
                    HttpPostedFile file = fileuploadDrawing.PostedFile;
                    foreach (HttpPostedFile postedFile in fileuploadDrawing.PostedFiles)
                    {
                        string filename = Path.GetFileName(postedFile.FileName);
                        string[] pdffilename = filename.Split('.');
                        string pdffilename1 = pdffilename[0];
                        string filenameExt = pdffilename[1];
                        if (filenameExt == "jpg" || filenameExt == "pdf" || filenameExt == "PDF" || filenameExt == "xlsx" || filenameExt == "xls" || filenameExt == "xlsm" || filenameExt == "xltx" || filenameExt == "xltm" || filenameExt == "doc" || filenameExt == "docm" || filenameExt == "docx" || filenameExt == "ppt" || filenameExt == "pptx" || filenameExt == "pptm")
                        {
                            postedFile.SaveAs(Server.MapPath("~/RefDocument/") + filename);
                            cmd.Parameters.AddWithValue("@DrawingFile", "../RefDocument/" + postedFile.FileName);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select PDF, Excel, PPT, Word files only !!');", true);
                        }
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DrawingFile", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@Action", "insert");
                cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim());
                cmd.Parameters.AddWithValue("@ItemCode", Itemcode);
                cmd.Parameters.AddWithValue("@Category", ddlCategory.Text.Trim());
                cmd.Parameters.AddWithValue("@SubCategory", ddlSubcategory.Text.Trim());
                cmd.Parameters.AddWithValue("@PurchaseRate", txtPurchaseRate.Text.Trim());
                cmd.Parameters.AddWithValue("@HSN", txtHSNNo.Text.Trim());
                cmd.Parameters.AddWithValue("@GST_Type", txtGSTType.Text.Trim());
                cmd.Parameters.AddWithValue("@CGST", ddlCgst.Text.Trim());
                cmd.Parameters.AddWithValue("@SGST", ddlSgst.Text.Trim());
                cmd.Parameters.AddWithValue("@IGST", ddlIgst.Text.Trim());
                cmd.Parameters.AddWithValue("@StorageUnit", txtStorageUnit.Text.Trim());
                cmd.Parameters.AddWithValue("@Salable", ddlSalable.Text.Trim());
                cmd.Parameters.AddWithValue("@ItemDescription", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@OpeningStock", txtOpeningStock.Text.Trim());
                cmd.Parameters.AddWithValue("@SafetyStock", txtSafetyStock.Text.Trim());
                cmd.Parameters.AddWithValue("@MinOrderQty", txtminOrderqty.Text.Trim());
                cmd.Parameters.AddWithValue("@Type", ddlType.Text.Trim());
                cmd.Parameters.AddWithValue("@StockLocation", txtStockLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@ShelfLife", txtShelfLife.Text.Trim());
                cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
                int a = 0;
                con.Open();
                a = cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Sucessfully');window.location.href='ItemMaster.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Item Code Generation Problem Please Try Again !!');", true);
            }
        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update Item")
        {
            bool chk = true;
            if (chkIsactive.Checked == true)
                chk = true;
            else
                chk = false;


            SqlCommand cmd = new SqlCommand("SP_ItemMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (fileuploadDrawing.HasFile)
            {
                HttpPostedFile file = fileuploadDrawing.PostedFile;
                foreach (HttpPostedFile postedFile in fileuploadDrawing.PostedFiles)
                {
                    string filename = Path.GetFileName(postedFile.FileName);
                    string[] pdffilename = filename.Split('.');
                    string pdffilename1 = pdffilename[0];
                    string filenameExt = pdffilename[1];
                    if (filenameExt == "jpg" || filenameExt == "png" || filenameExt == "pdf" || filenameExt == "PDF" || filenameExt == "xlsx" || filenameExt == "xls" || filenameExt == "xlsm" || filenameExt == "xltx" || filenameExt == "xltm" || filenameExt == "doc" || filenameExt == "docm" || filenameExt == "docx" || filenameExt == "ppt" || filenameExt == "pptx" || filenameExt == "pptm")
                    {
                        postedFile.SaveAs(Server.MapPath("~/RefDocument/") + filename);
                        cmd.Parameters.AddWithValue("@DrawingFile", "../RefDocument/" + postedFile.FileName);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select PDF, Excel, PPT, Word files only !!');", true);
                    }
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@DrawingFile", hdnFile.Value);
            }

            cmd.Parameters.AddWithValue("@Action", "update");
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(ViewState["UpdateRowId"].ToString()));

            cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim());
            cmd.Parameters.AddWithValue("@ItemCode", txtItemcode.Text.Trim());
            cmd.Parameters.AddWithValue("@Category", ddlCategory.Text.Trim());
            cmd.Parameters.AddWithValue("@SubCategory", ddlSubcategory.Text.Trim());
            cmd.Parameters.AddWithValue("@PurchaseRate", txtPurchaseRate.Text.Trim());
            cmd.Parameters.AddWithValue("@HSN", txtHSNNo.Text.Trim());
            cmd.Parameters.AddWithValue("@GST_Type", txtGSTType.Text.Trim());
            cmd.Parameters.AddWithValue("@CGST", ddlCgst.Text.Trim());
            cmd.Parameters.AddWithValue("@SGST", ddlSgst.Text.Trim());
            cmd.Parameters.AddWithValue("@IGST", ddlIgst.Text.Trim());
            cmd.Parameters.AddWithValue("@StorageUnit", txtStorageUnit.Text.Trim());
            cmd.Parameters.AddWithValue("@Salable", ddlSalable.Text.Trim());
            cmd.Parameters.AddWithValue("@ItemDescription", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@OpeningStock", txtOpeningStock.Text.Trim());
            cmd.Parameters.AddWithValue("@SafetyStock", txtSafetyStock.Text.Trim());
            cmd.Parameters.AddWithValue("@MinOrderQty", txtminOrderqty.Text.Trim());
            cmd.Parameters.AddWithValue("@Type", ddlType.Text.Trim());
            cmd.Parameters.AddWithValue("@StockLocation", txtStockLocation.Text.Trim());
            cmd.Parameters.AddWithValue("@ShelfLife", txtShelfLife.Text.Trim());
            cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", chk);
            cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
            int a = 0;
            cmd.Connection.Open();
            a = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Updated Sucessfully');window.location.href='ItemMaster.aspx';", true);
        }
        #endregion Update
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemMaster.aspx");
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemList(string prefixText, int count)
    {
        return AutoFillItemName(prefixText);
    }

    public static List<string> AutoFillItemName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [ItemName] from tblItemMaster where " + "ItemName like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> ItemNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ItemNames.Add(sdr["ItemName"].ToString());
                    }
                }
                con.Close();
                return ItemNames;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetHSNList(string prefixText, int count)
    {
        return AutoFillHSNno(prefixText);
    }

    public static List<string> AutoFillHSNno(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [HSN] from tblItemMaster where " + "HSN like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> hsnno = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        hsnno.Add(sdr["HSN"].ToString());
                    }
                }
                con.Close();
                return hsnno;
            }
        }
    }

    protected void txtItemName_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [ItemName] FROM [tblItemMaster] where ItemName='" + txtItemName.Text.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Item Already Existing !!";
            btnadd.Enabled = false;
        }
        else
        {
            btnadd.Enabled = true;
            lblmsg.Visible = false;
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubcategory();
    }

    protected void txtGSTType_TextChanged(object sender, EventArgs e)
    {
        //if (txtGSTType.Text == "IGST")
        //{
        //    ddlIgst.Enabled = true;
        //    ddlIgst.SelectedItem.Text = "18";
        //    ddlCgst.SelectedItem.Text = "0";
        //    ddlCgst.SelectedItem.Text = "0";
        //    ddlCgst.Enabled = false;
        //    ddlSgst.Enabled = false;
        //}
        //else if (txtGSTType.Text == "CGST_SGST")
        //{
        //    ddlIgst.Enabled = false;
        //    ddlCgst.Enabled = true;
        //    ddlSgst.Enabled = true;
        //    ddlIgst.SelectedItem.Text = "0";
        //    ddlSgst.SelectedItem.Text = "9";
        //    ddlCgst.SelectedItem.Text = "9";
        //}
        //else
        //{
        //    ddlIgst.Enabled = true;
        //    ddlCgst.Enabled = true;
        //    ddlSgst.Enabled = true;
        //}

        //ddlIgst.Text = "";
        //ddlCgst.Text = "";
        //ddlSgst.Text = "";
        string com = "Select * from TblTaxMaster where ID='" + txtGSTType.SelectedValue + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlIgst.SelectedItem.Text = dt.Rows[0]["IGST"].ToString();
            ddlCgst.SelectedItem.Text = dt.Rows[0]["CGST"].ToString();
            ddlSgst.SelectedItem.Text = dt.Rows[0]["SGST"].ToString();


        }

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
                com.CommandText = "Select DISTINCT [SupplierName] from tblSupplierMaster where " + "SupplierName like '%'+ @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> SupplierNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        SupplierNames.Add(sdr["SupplierName"].ToString());
                    }
                }
                con.Close();
                return SupplierNames;
            }
        }
    }

    public void GetTaxType()
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
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetTaxType"));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        txtGSTType.DataSource = Dt;
                        txtGSTType.DataValueField = "ID";
                        txtGSTType.DataTextField = "TaxCategory";
                        txtGSTType.DataBind();
                        txtGSTType.Items.Insert(0, "--Select Type--");

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

    protected void txtHSNNo_TextChanged(object sender, EventArgs e)
    {
        string text = txtHSNNo.Text;
        int length = text.Length;

        if (length != 8)
        {
            string errorMsg = "Hsn no too short";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }
}