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

public partial class Admin_Addenquiry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComCode = string.Empty; ComCodeUpdate = string.Empty; regdate = string.Empty;
            GetUser();
            if (Request.QueryString["Cname"] != null)
            {
                GetCompanyDataByName(Decrypt(Request.QueryString["Cname"].ToString()));
                
            }

            if (Request.QueryString["code"] != null)
            {
                ViewState["UpdateRowId"] = Decrypt(Request.QueryString["code"].ToString());
                if (!string.IsNullOrEmpty(ViewState["UpdateRowId"].ToString()))
                {
                    GetCompanyData(ViewState["UpdateRowId"].ToString());
                }

            }

        }
    }


    static string regdate = string.Empty;
    protected void GetCompanyData(string id)
    {
        string query1 = string.Empty;
        query1 = "SELECT [id],[ccode],[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],[remark],[SalesPerson] FROM [EnquiryData] where id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtcname.Text = dt.Rows[0]["cname"].ToString();
            HFccode.Value = dt.Rows[0]["ccode"].ToString();
            txtremark.Text = dt.Rows[0]["remark"].ToString();
            HFfile1.Value = dt.Rows[0]["filepath1"].ToString();
            ddluser.SelectedItem.Text = dt.Rows[0]["SalesPerson"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["filepath1"].ToString()))
            {
                string a1 = dt.Rows[0]["filepath1"].ToString().Remove(0, 13);// "Has file";
                lblfile1.Text = a1.Remove(a1.Length - 18, 18) + "...";
                ImageButtonfile1.Visible = true;
            }
            else
            {
                ImageButtonfile1.Visible = false;
                lblfile1.Text = "file not available";
            }


            HFfile2.Value = dt.Rows[0]["filepath2"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["filepath2"].ToString()))
            {
                ImageButtonfile2.Visible = true;
                string a1 = dt.Rows[0]["filepath2"].ToString().Remove(0, 13);// "Has file";
                lblfile2.Text = a1.Remove(a1.Length - 18, 18) + "...";
            }
            else
            {
                ImageButtonfile2.Visible = false;
                lblfile2.Text = "file not available";
            }
            HFfile3.Value = dt.Rows[0]["filepath3"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["filepath3"].ToString()))
            {
                ImageButtonfile3.Visible = true;
                string a1 = dt.Rows[0]["filepath3"].ToString().Remove(0, 13);// "Has file";
                lblfile3.Text = a1.Remove(a1.Length - 18, 18) + "...";
            }
            else
            {
                ImageButtonfile3.Visible = false;
                lblfile3.Text = "file not available";
            }
            HFfile4.Value = dt.Rows[0]["filepath4"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["filepath4"].ToString()))
            {
                ImageButtonfile4.Visible = true;
                string a1 = dt.Rows[0]["filepath4"].ToString().Remove(0, 13);// "Has file";
                lblfile4.Text = a1.Remove(a1.Length - 18, 18) + "...";
            }
            else
            {
                ImageButtonfile4.Visible = false;
                lblfile4.Text = "file not available";
            }
            HFfile5.Value = dt.Rows[0]["filepath5"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["filepath5"].ToString()))
            {
                ImageButtonfile5.Visible = true;
                string a1 = dt.Rows[0]["filepath5"].ToString().Remove(0, 13);// "Has file";
                lblfile5.Text = a1.Remove(a1.Length - 18, 18) + "...";
            }
            else
            {
                ImageButtonfile5.Visible = false;
                lblfile5.Text = "file not available";
            }
            btnadd.Text = "Update";
        }
    }

    protected void GetCompanyDataByName(string cname)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT top 1 [id],[ccode],[cname] FROM [Company] where [isdeleted]=0 and cname='" + cname.Trim() + "' order by id desc ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtcname.Text = cname;
            btnadd.Enabled = true;
            lblmsg.Visible = false;
            HFccode.Value = dt.Rows[0]["ccode"].ToString();
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Company Not found in our data base, Please add company first !!";
            btnadd.Enabled = false;
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

    static string ComCode = string.Empty;
    protected void GenerateComCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [EnquiryData]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
            ComCode = "Excel/0" + (maxid + 1).ToString();
        }
        else
        {
            ComCode = string.Empty;
        }
    }

    static string ComCodeUpdate = string.Empty; static string visitingcardPath = string.Empty;
    protected void btnadd_Click(object sender, EventArgs e)
    {
        #region Insert
        if (btnadd.Text == "Add Enquiry")
        {
            GenerateComCode();
            if (!string.IsNullOrEmpty(ComCode))
            {
                SqlCommand cmd = new SqlCommand("SP_EnquiryData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@EnqCode", ComCode);
                cmd.Parameters.AddWithValue("@ccode", HFccode.Value);
                cmd.Parameters.AddWithValue("@cname", txtcname.Text.Trim());
                cmd.Parameters.AddWithValue("@remark", txtremark.Text.Trim());
                cmd.Parameters.AddWithValue("@sessionname", Session["name"].ToString());
                cmd.Parameters.AddWithValue("@SalesPerson", ddluser.SelectedItem.Text);
                if (FileUpload1.HasFile)
                {
                    foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                    {
                        string filename1 = Path.GetFileName(postedFile.FileName);
                        string filename = filename1.Replace(" ", "_");
                        string[] pdffilename = filename.Split('.');
                        string pdffilename1 = pdffilename[0];
                        string filenameExt = pdffilename[1];
                        //if (filenameExt == "pdf" || filenameExt == "PDF")
                        //{
                        string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                        postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") + time1 + filename);
                        cmd.Parameters.AddWithValue("@filepath1", "EnquiryFiles/" + time1 + filename);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                        //}
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@filepath1", DBNull.Value);
                }

                if (FileUpload2.HasFile)
                {
                    foreach (HttpPostedFile postedFile in FileUpload2.PostedFiles)
                    {
                        string filename1 = Path.GetFileName(postedFile.FileName);
                        string filename = filename1.Replace(" ", "_");
                        string[] pdffilename = filename.Split('.');
                        string pdffilename1 = pdffilename[0];
                        string filenameExt = pdffilename[1];
                        //if (filenameExt == "pdf" || filenameExt == "PDF")
                        //{
                        string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                        postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") +  time1 + filename);
                        cmd.Parameters.AddWithValue("@filepath2", "EnquiryFiles/" +  time1 + filename);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                        //}
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@filepath2", DBNull.Value);
                }

                if (FileUpload3.HasFile)
                {
                    foreach (HttpPostedFile postedFile in FileUpload3.PostedFiles)
                    {
                        string filename1 = Path.GetFileName(postedFile.FileName);
                        string filename = filename1.Replace(" ", "_");
                        string[] pdffilename = filename.Split('.');
                        string pdffilename1 = pdffilename[0];
                        string filenameExt = pdffilename[1];
                        //if (filenameExt == "pdf" || filenameExt == "PDF")
                        //{
                        string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                        postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") + time1 + filename);
                        cmd.Parameters.AddWithValue("@filepath3", "EnquiryFiles/" + time1 + filename);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                        //}
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@filepath3", DBNull.Value);
                }

                if (FileUpload4.HasFile)
                {
                    foreach (HttpPostedFile postedFile in FileUpload4.PostedFiles)
                    {
                        string filename1 = Path.GetFileName(postedFile.FileName);
                        string filename = filename1.Replace(" ", "_");
                        string[] pdffilename = filename.Split('.');
                        string pdffilename1 = pdffilename[0];
                        string filenameExt = pdffilename[1];
                        //if (filenameExt == "pdf" || filenameExt == "PDF")
                        //{
                        string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                        postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") + time1 + filename);
                        cmd.Parameters.AddWithValue("@filepath4", "EnquiryFiles/" + time1 + filename);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                        //}
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@filepath4", DBNull.Value);
                }

                if (FileUpload5.HasFile)
                {
                    foreach (HttpPostedFile postedFile in FileUpload5.PostedFiles)
                    {
                        string filename1 = Path.GetFileName(postedFile.FileName);
                        string filename = filename1.Replace(" ", "_");
                        string[] pdffilename = filename.Split('.');
                        string pdffilename1 = pdffilename[0];
                        string filenameExt = pdffilename[1];
                        //if (filenameExt == "pdf" || filenameExt == "PDF")
                        //{
                        string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                        postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") + time1 + filename);
                        cmd.Parameters.AddWithValue("@filepath5", "EnquiryFiles/" + time1 + filename);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                        //}
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@filepath5", DBNull.Value);
                }

                int a = 0;
                cmd.Connection.Open();
                a = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                if (a > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Enquiry Added Sucessfully');window.location='EnquiryList.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Saved !!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Enquiry Code Generation Problem Please Try Again !!');", true);
            }
        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update")
        {
            //GenerateComCode();
            //if (!string.IsNullOrEmpty(ComCode))
            //{
            SqlCommand cmd = new SqlCommand("SP_EnquiryData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Update");
            cmd.Parameters.AddWithValue("@id", ViewState["UpdateRowId"].ToString());
            //cmd.Parameters.AddWithValue("@EnqCode", ComCode);
            cmd.Parameters.AddWithValue("@ccode", HFccode.Value);
            cmd.Parameters.AddWithValue("@cname", txtcname.Text.Trim());
            cmd.Parameters.AddWithValue("@remark", txtremark.Text.Trim());
            cmd.Parameters.AddWithValue("@SalesPerson", ddluser.SelectedItem.Text);
            //cmd.Parameters.AddWithValue("@sessionname", Session["empcode"].ToString());
            if (FileUpload1.HasFile)
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    string filename1 = Path.GetFileName(postedFile.FileName).Trim();
                    string filename = filename1.Replace(" ", "_");
                    //string filename = filename1.Replace("+", "_").Replace("-", "_");

                    string[] pdffilename = filename.Split('.');
                    string MaxNo = pdffilename.Max();
                    string pdffilename1 = pdffilename[0];
                    //string filenameExt = pdffilename[MaxNo];
                    string filenameExt = MaxNo;
                    //if (filenameExt == "pdf" || filenameExt == "PDF")
                    //{
                    string time1 = DateTime.Now.ToString("ddmmyyyyttmmss"+"_");
                    //postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") + pdffilename1 + time1 + "." + filenameExt);
                    //cmd.Parameters.AddWithValue("@filepath1", "EnquiryFiles/" + pdffilename1 + time1 + "." + filenameExt);

                    postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") + time1 + filename);
                    cmd.Parameters.AddWithValue("@filepath1", "EnquiryFiles/" + time1 + filename);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                    //}
                } 
            }
            else
            {
                cmd.Parameters.AddWithValue("@filepath1", HFfile1.Value);
            }

            if (FileUpload2.HasFile)
            {
                foreach (HttpPostedFile postedFile in FileUpload2.PostedFiles)
                {
                    string filename1 = Path.GetFileName(postedFile.FileName);
                    string filename = filename1.Replace(" ", "_");
                    string[] pdffilename = filename.Split('.');
                    string pdffilename1 = pdffilename[0];
                    string filenameExt = pdffilename[1];
                    //if (filenameExt == "pdf" || filenameExt == "PDF")
                    //{
                    string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                    postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") +  time1 + filename);
                    cmd.Parameters.AddWithValue("@filepath2", "EnquiryFiles/" + time1 + filename);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                    //}
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@filepath2", HFfile2.Value);
            }

            if (FileUpload3.HasFile)
            {
                foreach (HttpPostedFile postedFile in FileUpload3.PostedFiles)
                {
                    string filename1 = Path.GetFileName(postedFile.FileName);
                    string filename = filename1.Replace(" ", "_");
                    string[] pdffilename = filename.Split('.');
                    string pdffilename1 = pdffilename[0];
                    string filenameExt = pdffilename[1];
                    //if (filenameExt == "pdf" || filenameExt == "PDF")
                    //{
                    string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                    postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") + time1 + filename);
                    cmd.Parameters.AddWithValue("@filepath3", "EnquiryFiles/" + time1 + filename);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                    //}
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@filepath3", HFfile3.Value);
            }

            if (FileUpload4.HasFile)
            {
                foreach (HttpPostedFile postedFile in FileUpload4.PostedFiles)
                {
                    string filename1 = Path.GetFileName(postedFile.FileName);
                    string filename = filename1.Replace(" ", "_");
                    string[] pdffilename = filename.Split('.');
                    string pdffilename1 = pdffilename[0];
                    string filenameExt = pdffilename[1];
                    //if (filenameExt == "pdf" || filenameExt == "PDF")
                    //{
                    string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                    postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") +  time1 + filename);
                    cmd.Parameters.AddWithValue("@filepath4", "EnquiryFiles/" +  time1 + filename);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                    //}
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@filepath4", HFfile4.Value);
            }

            if (FileUpload5.HasFile)
            {
                foreach (HttpPostedFile postedFile in FileUpload5.PostedFiles)
                {
                    string filename1 = Path.GetFileName(postedFile.FileName);
                    string filename = filename1.Replace(" ", "_");
                    string[] pdffilename = filename.Split('.');
                    string pdffilename1 = pdffilename[0];
                    string filenameExt = pdffilename[1];
                    //if (filenameExt == "pdf" || filenameExt == "PDF")
                    //{
                    string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                    postedFile.SaveAs(Server.MapPath("~/EnquiryFiles/") +  time1 + filename);
                    cmd.Parameters.AddWithValue("@filepath5", "EnquiryFiles/" + time1 + filename);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                    //}
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@filepath5", HFfile5.Value);
            }

            int a = 0;
            cmd.Connection.Open();
            a = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            if (a > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Enquiry Updated Sucessfully');window.location='EnquiryList.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Updated !!');", true);
            }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Enquiry Code Generation Problem Please Try Again !!');", true);
            //}
        }
        #endregion Update
    }


    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addenquiry.aspx");
    }

    protected void txtcname_TextChanged(object sender, EventArgs e)
    {
        GetCompanyDataByName(txtcname.Text);
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


    protected void Deletefile(string id, string fileno)
    {
        SqlCommand cmd = new SqlCommand("SP_EnquiryData", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Action", "UpdateFile");
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@fileno", fileno);
        int a = 0;
        cmd.Connection.Open();
        a = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        if (a > 0)
        {
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Enquiry Updated Sucessfully');window.location='Addenquiry.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('File Not Deleted !!');", true);
        }

    }

    protected void ImageButtonfile1_Click(object sender, ImageClickEventArgs e)
    {
        Deletefile(ViewState["UpdateRowId"].ToString(), "1");
    }

    protected void ImageButtonfile2_Click(object sender, ImageClickEventArgs e)
    {
        Deletefile(ViewState["UpdateRowId"].ToString(), "2");
    }

    protected void ImageButtonfile3_Click(object sender, ImageClickEventArgs e)
    {
        Deletefile(ViewState["UpdateRowId"].ToString(), "3");
    }

    protected void ImageButtonfile4_Click(object sender, ImageClickEventArgs e)
    {
        Deletefile(ViewState["UpdateRowId"].ToString(), "4");
    }

    protected void ImageButtonfile5_Click(object sender, ImageClickEventArgs e)
    {
        Deletefile(ViewState["UpdateRowId"].ToString(), "5");
    }

     public void GetUser()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("[SP_EnquiryData]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "Getuser"));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        ddluser.DataSource = Dt;
                        ddluser.DataValueField = "id";
                        ddluser.DataTextField = "name";
                        ddluser.DataBind();
                        ddluser.Items.Insert(0, "--Select Sales Person --");

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

}