using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EnquiryList : System.Web.UI.Page
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnPageLoad", "OnPageLoad();", true);
                DdlSalesBind();
                Gvbind();
                ViewAuthorization();
            }
        }
    }
    private void ViewAuthorization()
    {
        string empcode = Session["empcode"].ToString();
        DataTable Dt = new DataTable();
        SqlDataAdapter Sd = new SqlDataAdapter("Select id from [employees] where [empcode]='" + empcode + "'", con);
        Sd.Fill(Dt);
        if (Dt.Rows.Count > 0)
        {
            string id = Dt.Rows[0]["id"].ToString();
            DataTable Dtt = new DataTable();
            SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM [DB_ProcetechTesting].[DB_ProcetechERP].[tblUserRoleAuthorization] where UserID = '" + id + "' AND PageName = 'EnquiryList.aspx' AND PagesView = '1'", con);
            Sdd.Fill(Dtt);
            if (Dtt.Rows.Count > 0)
            {
                GvCompany.Columns[10].Visible = false;
                btnAddCompany.Visible = false;
                btnAddEnq.Visible = false;
            }
        }
    }
    private void Gvbind()
    {
        string query = string.Empty;
        if (ddlsalesMainfilter.Text != "All")
        {
            //query = @"SELECT [id],[EnqCode],[ccode],[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],[status],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] where sessionname='" + ddlsalesMainfilter.SelectedValue + "' order by id desc";
            query = @"SELECT E.[id],[EnqCode],E.[ccode],E.[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],E.[status],convert(varchar(20),E.[regdate],105) as [regdate],E.[sessionname],IsActive,C.CustomerCode FROM [EnquiryData] AS E INNER JOIN Company AS C ON C.ccode=E.ccode where E.sessionname='" + ddlsalesMainfilter.SelectedValue + "' and E.IsActive=1 order by id desc";
        }
        if (!string.IsNullOrEmpty(txtcnamefilter.Text.Trim()))
        {
            //query = "SELECT [id],[EnqCode],[ccode],[cname],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] where cname like '" + txtcnamefilter.Text.Trim() + "%' order by id desc";
            query = @"SELECT E.[id],[EnqCode],E.[ccode],E.[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],E.[status],convert(varchar(20),E.[regdate],105) as [regdate],E.[sessionname],IsActive,C.CustomerCode FROM [EnquiryData] AS E INNER JOIN Company AS C ON C.ccode=E.ccode where E.cname like '" + txtcnamefilter.Text.Trim() + "%' and E.IsActive=1 order by id desc";
        }
        if (ddlsalesMainfilter.Text == "All" && string.IsNullOrEmpty(txtcnamefilter.Text.Trim()) && ddlStatus.SelectedItem.Text == "Pending Enquiry")
        {
            //query = @"SELECT [id],[EnqCode],[ccode],[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],[status],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] order by id desc";
            query = @"SELECT E.[id],[EnqCode],E.[ccode],E.[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],E.[status],convert(varchar(20),E.[regdate],105) as [regdate],E.[sessionname],IsActive,C.CustomerCode FROM [EnquiryData] AS E INNER JOIN Company AS C ON C.ccode=E.ccode where E.IsActive=1 order by id desc";
        }
        if (ddlStatus.SelectedItem.Text == "Completed")
        {
            query = @"SELECT E.[id],[EnqCode],E.[ccode],E.[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],E.[status],convert(varchar(20),E.[regdate],105) as [regdate],E.[sessionname],IsActive,C.CustomerCode FROM [EnquiryData] AS E INNER JOIN Company AS C ON C.ccode=E.ccode where E.IsActive=0 order by id desc";
        }
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvCompany.DataSource = dt;
            GvCompany.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvCompany.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            GvCompany.DataSource = null;
            GvCompany.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvCompany.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
    }

    private void DdlSalesBind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],[empcode],[name] FROM [employees] where [status]=1 and [isdeleted]=0 and [role]='Sales' order by id desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlsalesMainfilter.DataSource = dt;
            ddlsalesMainfilter.DataValueField = "empcode";
            ddlsalesMainfilter.DataTextField = "name";
            ddlsalesMainfilter.DataBind();
            ddlsalesMainfilter.Items.Insert(0, "All");
        }
        else
        {
            ddlsalesMainfilter.DataSource = null;
            ddlsalesMainfilter.DataBind();
            ddlsalesMainfilter.Items.Insert(0, "All");
        }
    }

    protected void GvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ViewState["CompRowId"] = e.CommandArgument.ToString();
        if (e.CommandName == "RowEdit")
        {
            ViewState["id"] = e.CommandArgument.ToString();
            Response.Redirect("Addenquiry.aspx?code=" + encrypt(e.CommandArgument.ToString()));

        }
        if (e.CommandName == "companyname")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                GetCompanyDataPopup(e.CommandArgument.ToString());
                //GetCompanyDataBDEPopup(e.CommandArgument.ToString());
                this.modelprofile.Show();
            }
        }

        if (e.CommandName == "CreateQuaot")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                HdnID.Text = e.CommandArgument.ToString();
                Response.Redirect("QuotationCat3.aspx?Ccode=" + encrypt(HdnID.Text));
                
                //this.modalCreateQuat.Show();
            }
        }

        if (e.CommandName == "DeleteData")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RemoveLocalStorage", "RemoveLocalStorage();", true);

                HttpContext.Current.Session["CustomerCode"] = null;
                HttpContext.Current.Session["CustomerName"] = null;

                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[EnquiryData] SET [IsActive] = 0 WHERE id='" + e.CommandArgument.ToString() + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Enquiry Deleted Sucessfully');window.location='EnquiryList.aspx';", true);
            }
        }
    }

    private void GetCompanyDataPopup(string id)
    {
        string query1 = string.Empty;
        //query1 = "SELECT A.[EnqCode],A.[cname],A.[status],A.[remark],format(A.[regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],A.[sessionname],B.name FROM [EnquiryData] A join employees B on A.sessionname=B.empcode where A.id='" + id + "' ";
        query1 = @"select A.[EnqCode],A.[cname],A.[status],A.[remark],
format(A.[regdate], 'dd-MMM-yyyy hh:mm tt') as [regdate]
,A.[sessionname],CustomerCode from EnquiryData AS A LEFT JOIN Company AS C
ON C.ccode = A.ccode where A.id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //lblenqcode.Text = dt.Rows[0]["EnqCode"].ToString();
            lblcname.Text = dt.Rows[0]["cname"].ToString();
            lblremark.Text = dt.Rows[0]["remark"].ToString();
            if (dt.Rows[0]["status"].ToString() == "False" || dt.Rows[0]["status"].ToString() == "false")
            {
                lblstatus.Text = "Open";
            }
            else
            {
                lblstatus.Text = "Close";
            }


            lblRegdate.Text = dt.Rows[0]["regdate"].ToString();
            lblregBy.Text = dt.Rows[0]["sessionname"].ToString();

        }
    }

    protected void GvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvCompany.PageIndex = e.NewPageIndex;
        Gvbind();
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

    protected void btnShowComDetail_Click(object sender, EventArgs e)
    {
        GetCompanyDataPopup(ViewState["CompRowId"].ToString());// GetCompanyDataBDEPopup(ViewState["CompRowId"].ToString());
        this.modelprofile.Show();
    }

    #region Filter

    protected void txtcnamefilter_TextChanged(object sender, EventArgs e)
    {
        Gvbind();
        //string query = string.Empty;
        //if (!string.IsNullOrEmpty(txtcnamefilter.Text.Trim()))
        //{
        //    //query = "SELECT [id],[EnqCode],[ccode],[cname],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] where cname like '" + txtcnamefilter.Text.Trim() + "%' order by id desc";
        //    query = @"SELECT [id],[EnqCode],[ccode],[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],[status],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] where cname like '" + txtcnamefilter.Text.Trim() + "%' order by id desc";
        //}
        //else
        //{
        //    //query = "SELECT [id],[EnqCode],[ccode],[cname],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] order by id desc";
        //    query = @"SELECT [id],[EnqCode],[ccode],[cname],[filepath1],[filepath2],[filepath3],[filepath4],[filepath5],[status],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] order by id desc";
        //}

        //SqlDataAdapter ad = new SqlDataAdapter(query, con);
        //DataTable dt = new DataTable();
        //ad.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    GvCompany.DataSource = dt;
        //    GvCompany.DataBind();
        //    lblnodatafoundComp.Visible = false;
        //}
        //else
        //{
        //    GvCompany.DataSource = null;
        //    GvCompany.DataBind();
        //    lblnodatafoundComp.Text = "No Data Found !! ";
        //    lblnodatafoundComp.Visible = true;
        //    lblnodatafoundComp.ForeColor = Color.Red;
        //}
    }

    #endregion Filter

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("EnquiryList.aspx");
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
                com.CommandText = "Select DISTINCT [cname] from [EnquiryData] where " + "cname like @Search + '%'";

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

    protected void ddlsalesMainfilter_TextChanged(object sender, EventArgs e)
    {
        Gvbind();
    }

    protected void linkbtnfile_Click(object sender, EventArgs e)
    {
        string id1 = ((sender as ImageButton).CommandArgument).ToString();
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid1=" + id1 + "&SN=1','','width=700px,height=600px');", true);
        DataTable Dt = new DataTable();
        SqlDataAdapter Sd = new SqlDataAdapter("Select * from [dbo].[EnquiryData] where id='" + id1 + "'", con);
        Sd.Fill(Dt);
        if (Dt.Rows.Count > 0)
        {
            string filepath1 = Dt.Rows[0]["filepath1"].ToString();
            if (filepath1 == "" || filepath1 == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Attachment Not Found..!!');", true);
            }
            else
            {
                //string URL= "erp.excelenclosures.net/RefDocument/";
                string URL = "~/EnquiryFiles/";
                string URL1 = filepath1;
                Response.Redirect("~/" + URL1);
            }
        }
    }

    protected void linkbtnfile2_Click(object sender, EventArgs e)
    {
        //string id2 = encrypt(((sender as ImageButton).CommandArgument).ToString());
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid2=" + id2 + "&SN=2','','width=700px,height=600px');", true);

        string id1 = ((sender as ImageButton).CommandArgument).ToString();
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid1=" + id1 + "&SN=1','','width=700px,height=600px');", true);
        DataTable Dt = new DataTable();
        SqlDataAdapter Sd = new SqlDataAdapter("Select * from [ExcelEncLive].[dbo].[EnquiryData] where id='" + id1 + "'", con);
        Sd.Fill(Dt);
        if (Dt.Rows.Count > 0)
        {
            string filepath1 = Dt.Rows[0]["filepath2"].ToString();
            if (filepath1 == "" || filepath1 == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Attachment Not Found..!!');", true);
            }
            else
            {
                //string URL= "erp.excelenclosures.net/RefDocument/";
                string URL = "~/EnquiryFiles/";
                string URL1 = filepath1;
                Response.Redirect("~/" + URL1);
            }
        }
    }

    protected void linkbtnfile3_Click(object sender, EventArgs e)
    {
        //string id = encrypt(((sender as ImageButton).CommandArgument).ToString());
        // Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid3=" + id + "&SN=3','','width=700px,height=600px');", true);

        string id1 = ((sender as ImageButton).CommandArgument).ToString();
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid1=" + id1 + "&SN=1','','width=700px,height=600px');", true);
        DataTable Dt = new DataTable();
        SqlDataAdapter Sd = new SqlDataAdapter("Select * from [ExcelEncLive].[dbo].[EnquiryData] where id='" + id1 + "'", con);
        Sd.Fill(Dt);
        if (Dt.Rows.Count > 0)
        {
            string filepath1 = Dt.Rows[0]["filepath3"].ToString();
            if (filepath1 == "" || filepath1 == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Attachment Not Found..!!');", true);
            }
            else
            {
                //string URL= "erp.excelenclosures.net/RefDocument/";
                string URL = "~/EnquiryFiles/";
                string URL1 = filepath1;
                Response.Redirect("~/" + URL1);
            }
        }
    }

    protected void linkbtnfile4_Click(object sender, EventArgs e)
    {
        //string id = encrypt(((sender as ImageButton).CommandArgument).ToString());
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid4=" + id + "&SN=4','','width=700px,height=600px');", true);

        string id1 = ((sender as ImageButton).CommandArgument).ToString();
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid1=" + id1 + "&SN=1','','width=700px,height=600px');", true);
        DataTable Dt = new DataTable();
        SqlDataAdapter Sd = new SqlDataAdapter("Select * from [ExcelEncLive].[dbo].[EnquiryData] where id='" + id1 + "'", con);
        Sd.Fill(Dt);
        if (Dt.Rows.Count > 0)
        {
            string filepath1 = Dt.Rows[0]["filepath4"].ToString();
            if (filepath1 == "" || filepath1 == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Attachment Not Found..!!');", true);
            }
            else
            {
                //string URL= "erp.excelenclosures.net/RefDocument/";
                string URL = "~/EnquiryFiles/";
                string URL1 = filepath1;
                Response.Redirect("~/" + URL1);
            }
        }
    }

    protected void linkbtnfile5_Click(object sender, EventArgs e)
    {
        //string id = encrypt(((sender as ImageButton).CommandArgument).ToString());
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid5=" + id + "&SN=5','','width=700px,height=600px');", true);

        string id1 = ((sender as ImageButton).CommandArgument).ToString();
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid1=" + id1 + "&SN=1','','width=700px,height=600px');", true);
        DataTable Dt = new DataTable();
        SqlDataAdapter Sd = new SqlDataAdapter("Select * from [ExcelEncLive].[dbo].[EnquiryData] where id='" + id1 + "'", con);
        Sd.Fill(Dt);
        if (Dt.Rows.Count > 0)
        {
            string filepath1 = Dt.Rows[0]["filepath5"].ToString();
            if (filepath1 == "" || filepath1 == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Attachment Not Found..!!');", true);
            }
            else
            {
                //string URL= "erp.excelenclosures.net/RefDocument/";
                string URL = "~/EnquiryFiles/";
                string URL1 = filepath1;
                Response.Redirect("~/" + URL1);
            }
        }
    }

    protected void GvCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the session values
            string sessionCustomerCode = null;
            string sessionCustomerName = null;

            if (Session["CustomerCode"] != null)
            {
                sessionCustomerCode = Session["CustomerCode"].ToString();
            }

            if (Session["CustomerName"] != null)
            {
                sessionCustomerName = Session["CustomerName"].ToString();
            }

            if (!string.IsNullOrEmpty(sessionCustomerCode) || !string.IsNullOrEmpty(sessionCustomerName))
            {
                // Retrieve values from the current row
                string rowCustomerCode = DataBinder.Eval(e.Row.DataItem, "Ccode").ToString();
                string rowCustomerName = DataBinder.Eval(e.Row.DataItem, "Cname").ToString();

                // Get the controls for file paths and image buttons
                Label file1 = e.Row.FindControl("lblfilepath1") as Label;
                Label file2 = e.Row.FindControl("lblfilepath2") as Label;
                Label file3 = e.Row.FindControl("lblfilepath3") as Label;
                Label file4 = e.Row.FindControl("lblfilepath4") as Label;
                Label file5 = e.Row.FindControl("lblfilepath5") as Label;

                ImageButton ImageButtonfile1 = e.Row.FindControl("ImageButtonfile1") as ImageButton;
                ImageButton ImageButtonfile2 = e.Row.FindControl("ImageButtonfile2") as ImageButton;
                ImageButton ImageButtonfile3 = e.Row.FindControl("ImageButtonfile3") as ImageButton;
                ImageButton ImageButtonfile4 = e.Row.FindControl("ImageButtonfile4") as ImageButton;
                ImageButton ImageButtonfile5 = e.Row.FindControl("ImageButtonfile5") as ImageButton;

                // Disable Image Buttons if no file exists
                if (string.IsNullOrEmpty(file1.Text))
                {
                    ImageButtonfile1.Enabled = false;
                    ImageButtonfile1.ToolTip = "File Not Available";
                }
                if (string.IsNullOrEmpty(file2.Text))
                {
                    ImageButtonfile2.Enabled = false;
                    ImageButtonfile2.ToolTip = "File Not Available";
                }
                if (string.IsNullOrEmpty(file3.Text))
                {
                    ImageButtonfile3.Enabled = false;
                    ImageButtonfile3.ToolTip = "File Not Available";
                }
                if (string.IsNullOrEmpty(file4.Text))
                {
                    ImageButtonfile4.Enabled = false;
                    ImageButtonfile4.ToolTip = "File Not Available";
                }
                if (string.IsNullOrEmpty(file5.Text))
                {
                    ImageButtonfile5.Enabled = false;
                    ImageButtonfile5.ToolTip = "File Not Available";
                }

                // Check the status label and set text and color
                Label lblstatus1 = e.Row.FindControl("lblstatus1") as Label;
                Label lblstatus2 = e.Row.FindControl("lblstatus2") as Label;
                if (lblstatus1.Text == "False" || lblstatus1.Text == "false")
                {
                    lblstatus2.Text = "Open";
                    lblstatus2.ForeColor = Color.Green;
                }
                if (lblstatus1.Text == "True" || lblstatus1.Text == "true")
                {
                    lblstatus2.Text = "Close";
                    lblstatus2.ForeColor = Color.Red;
                }

                // Get the "IsActive" label and buttons
                Label lblIsActive = e.Row.FindControl("lblIsActive") as Label;
                Button btnEdit = e.Row.FindControl("Button4") as Button;
                Button btnsendquot = e.Row.FindControl("btnsendquot") as Button;
                LinkButton Linkbtndelete = e.Row.FindControl("Linkbtndelete") as LinkButton;

                // Check if the row corresponds to the active customer and is the correct customer
                if (lblIsActive.Text == "False")
                {
                    btnEdit.Visible = false;
                    btnsendquot.Visible = false;
                    Linkbtndelete.Visible = false;
                }
                else
                {
                    // Hide the buttons for rows where customer code/name do not match session data
                    if (sessionCustomerCode != rowCustomerCode || sessionCustomerName != rowCustomerName)
                    {
                        btnEdit.Visible = false;
                        btnsendquot.Visible = false;
                        Linkbtndelete.Visible = false;
                    }
                    else
                    {
                        btnEdit.Visible = true;
                        btnsendquot.Visible = true;
                        Linkbtndelete.Visible = true;
                    }
                }
            }
            else
            {
                Label file1 = e.Row.FindControl("lblfilepath1") as Label;
                Label file2 = e.Row.FindControl("lblfilepath2") as Label;
                Label file3 = e.Row.FindControl("lblfilepath3") as Label;
                Label file4 = e.Row.FindControl("lblfilepath4") as Label;
                Label file5 = e.Row.FindControl("lblfilepath5") as Label;

                ImageButton ImageButtonfile1 = e.Row.FindControl("ImageButtonfile1") as ImageButton;
                ImageButton ImageButtonfile2 = e.Row.FindControl("ImageButtonfile2") as ImageButton;
                ImageButton ImageButtonfile3 = e.Row.FindControl("ImageButtonfile3") as ImageButton;
                ImageButton ImageButtonfile4 = e.Row.FindControl("ImageButtonfile4") as ImageButton;
                ImageButton ImageButtonfile5 = e.Row.FindControl("ImageButtonfile5") as ImageButton;

                if (string.IsNullOrEmpty(file1.Text))
                {
                    ImageButtonfile1.Enabled = false;
                    ImageButtonfile1.ToolTip = "File Not Available";
                }
                if (string.IsNullOrEmpty(file2.Text))
                {
                    ImageButtonfile2.Enabled = false;
                    ImageButtonfile2.ToolTip = "File Not Available";
                }
                if (string.IsNullOrEmpty(file3.Text))
                {
                    ImageButtonfile3.Enabled = false;
                    ImageButtonfile3.ToolTip = "File Not Available";
                }
                if (string.IsNullOrEmpty(file4.Text))
                {
                    ImageButtonfile4.Enabled = false;
                    ImageButtonfile4.ToolTip = "File Not Available";
                }
                if (string.IsNullOrEmpty(file5.Text))
                {
                    ImageButtonfile5.Enabled = false;
                    ImageButtonfile5.ToolTip = "File Not Available";
                }

                Label lblstatus1 = e.Row.FindControl("lblstatus1") as Label;
                Label lblstatus2 = e.Row.FindControl("lblstatus2") as Label;
                if (lblstatus1.Text == "False" || lblstatus1.Text == "false")
                {
                    lblstatus2.Text = "Open";
                    lblstatus2.ForeColor = Color.Green;
                }
                if (lblstatus1.Text == "True" || lblstatus1.Text == "true")
                {
                    lblstatus2.Text = "Close";
                    lblstatus2.ForeColor = Color.Red;
                }
                Label lblIsActive = e.Row.FindControl("lblIsActive") as Label;
                Button btnEdit = e.Row.FindControl("Button4") as Button;
                Button btnsendquot = e.Row.FindControl("btnsendquot") as Button;
                LinkButton Linkbtndelete = e.Row.FindControl("Linkbtndelete") as LinkButton;
                if (lblIsActive.Text == "False")
                {
                    btnEdit.Visible = false;
                    btnsendquot.Visible = false;
                    Linkbtndelete.Visible = false;
                }
            }
        }
    }

    protected void BtnEnclosure_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuotationCat1.aspx?Ccode=" + encrypt(HdnID.Text));
    }

    protected void btnPart_Click(object sender, EventArgs e)
    {
        //Response.Redirect("QuotationCat2.aspx?Ccode=" + encrypt(HdnID.Text));
        Response.Redirect("QuotationCat3.aspx?Ccode=" + encrypt(HdnID.Text));
    }
    protected void ddlStatus_TextChanged(object sender, EventArgs e)
    {
        Gvbind();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtfromdate.Text) && string.IsNullOrEmpty(txttodate.Text))
            {
                Gvbind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Search Record');", true);
            }
            else
            {
                if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter sad = new SqlDataAdapter(" SELECT * FROM [EnquiryData] WHERE regdate between'" + txtfromdate.Text + "' AND '" + txttodate.Text + "' ", con);
                    sad.Fill(dt);

                    GvCompany.EmptyDataText = "Not Records Found";
                    GvCompany.DataSource = dt;
                    GvCompany.DataBind();
                }
            }
        }
        catch (System.Exception ex)
        {
            //throw ex;
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }
}