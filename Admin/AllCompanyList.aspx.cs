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

public partial class Admin_AllCompanyList : System.Web.UI.Page
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
            SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM [DB_ProcetechTesting].[DB_ProcetechERP].[tblUserRoleAuthorization] where UserID = '" + id + "' AND PageName = 'AllCompanyList.aspx' AND PagesView = '1'", con);
            Sdd.Fill(Dtt);
            if (Dtt.Rows.Count > 0)
            {
                GvCompany.Columns[8].Visible = false;
                btnAddCompany.Visible = false;
            }
        }
    }
    private void Gvbind()
    {
        string query = string.Empty;
        if (ddlsalesMainfilter.Text != "All")
        {
            query = @"SELECT [id],[ccode],[cname],[oname1],[oname2],[oname3],[oname4],[oname5],[email1],[email2],[email3],[email4],[email5],
            [mobile1],[mobile2],[mobile3],[mobile4],[mobile5]
            ,[desig1],[desig2],[desig3],[desig4],[desig5],[paymentterm1],[paymentterm2],[paymentterm3],[paymentterm4],[paymentterm5]
                ,Format([regdate],'dd-MMM-yyyy') as [regdate],[creditlimit],CustomerCode FROM [Company] where [status]=0 and [isdeleted]=0 and sessionname='" + ddlsalesMainfilter.SelectedValue + "' order by id desc";
        }
        else
        {
            query = @"SELECT [id],[ccode],[cname],[oname1],[oname2],[oname3],[oname4],[oname5],[email1],[email2],[email3],[email4],[email5],
            [mobile1],[mobile2],[mobile3],[mobile4],[mobile5]
            ,[desig1],[desig2],[desig3],[desig4],[desig5],[paymentterm1],[paymentterm2],[paymentterm3],[paymentterm4],[paymentterm5],
            Format([regdate],'dd-MMM-yyyy') as [regdate],[creditlimit],CustomerCode FROM [Company] where [status]=0 and [isdeleted]=0 order by id desc";
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

    //private void DdlEmpBind()
    //{
    //    SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],[empcode],[name] FROM [employees] where [status]=1 and [isdeleted]=0 order by id desc", con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlbdepopup.DataSource = dt;
    //        ddlbdepopup.DataValueField = "empcode";
    //        ddlbdepopup.DataTextField = "name";
    //        ddlbdepopup.DataBind();
    //        ddlbdepopup.Items.Insert(0, "Select");

    //    }
    //}

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
            //string empcode= GetSessionName(e.CommandArgument.ToString());
            // if (empcode == Session["adminempcode"].ToString())
            // {
            ViewState["id"] = e.CommandArgument.ToString();
            Response.Redirect("AddCompany.aspx?code=" + encrypt(e.CommandArgument.ToString()));
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('You do not have permission to edit this !!');", true);
            //}  
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
        if(e.CommandName == "CompanyData")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                GetCompanyDataPopup(e.CommandArgument.ToString());
                //GetCompanyDataBDEPopup(e.CommandArgument.ToString());
                this.modelprofile.Show();
            }
        }
    }

    private void GetCompanyDataPopup(string id)
    {
        string query1 = string.Empty;
        query1 = @"SELECT A.[ccode],A.[cname],A.[oname1],A.[oname2],A.[oname3],A.[oname4],A.[oname5],A.[email1],A.[mobile1],A.[mobile2],
      A.[mobile3],A.[mobile4],A.[mobile5],A.[billingaddress],[shippingaddress],format(A.[regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],
A.[sessionname],A.[gstno],B.name,B.email as Empemail,A.[desig1],A.[desig2],A.[desig3],A.[desig4],A.[desig5], A.CustomerCode FROM [Company]
A join employees B on A.sessionname=B.name where A.id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //lblccode.Text = dt.Rows[0]["ccode"].ToString();
            lblcname.Text = dt.Rows[0]["cname"].ToString();
            //lbloname.Text = dt.Rows[0]["oname1"].ToString();
           // lblemail.Text = dt.Rows[0]["email1"].ToString();
            //lblmobile.Text = dt.Rows[0]["mobile1"].ToString();
            lblcusomercode.Text = dt.Rows[0]["CustomerCode"].ToString();

            lblRegdate.Text = dt.Rows[0]["regdate"].ToString();
            lblregBy.Text = dt.Rows[0]["name"].ToString();
            lblgstno.Text = dt.Rows[0]["gstno"].ToString();
           // lblbillingaddress.Text = dt.Rows[0]["billingaddress"].ToString();
           // lblshipaddress.Text = dt.Rows[0]["shippingaddress"].ToString();
            //dt.Rows[0]["sessionname"].ToString();// Current Sales code
            ViewState["CurrentSalesEmail"] = dt.Rows[0]["Empemail"].ToString();// Current Sales Email

            //lblmobilePopup1.Text = dt.Rows[0]["mobile1"].ToString();
            //lblmobilePopup2.Text = dt.Rows[0]["mobile2"].ToString();
            //lblmobilePopup3.Text = dt.Rows[0]["mobile3"].ToString();
            //lblmobilePopup4.Text = dt.Rows[0]["mobile4"].ToString();
            //lblmobilePopup5.Text = dt.Rows[0]["mobile5"].ToString();

            //lblnamePopup1.Text = dt.Rows[0]["oname1"].ToString();
            //lblnamePopup2.Text = dt.Rows[0]["oname2"].ToString();
            //lblnamePopup3.Text = dt.Rows[0]["oname3"].ToString();
            //lblnamePopup4.Text = dt.Rows[0]["oname4"].ToString();
            //lblnamePopup5.Text = dt.Rows[0]["oname5"].ToString();

            //lblDesgPopup1.Text = dt.Rows[0]["desig1"].ToString();
            //lblDesgPopup2.Text = dt.Rows[0]["desig2"].ToString();
            //lblDesgPopup3.Text = dt.Rows[0]["desig3"].ToString();
            //lblDesgPopup4.Text = dt.Rows[0]["desig4"].ToString();
            //lblDesgPopup5.Text = dt.Rows[0]["desig5"].ToString();

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

    protected void btnShowComDetail_Click(object sender, EventArgs e)
    {
        GetCompanyDataPopup(ViewState["CompRowId"].ToString());// GetCompanyDataBDEPopup(ViewState["CompRowId"].ToString());
        this.modelprofile.Show();
    }

    #region Filter

    protected void txtcnamefilter_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtcnamefilter.Text.Trim()))
        {
            query = "SELECT [id],[ccode],[cname],[oname1],[email1],[mobile1],Format([regdate],'dd-MMM-yyyy') as [regdate],CustomerCode FROM [Company] where [status]=0 and [isdeleted]=0 and cname like '" + txtcnamefilter.Text.Trim() + "%' order by id desc";
        }
        else
        {
            query = "SELECT [id],[ccode],[cname],[oname1],[email1],[mobile1],Format([regdate],'dd-MMM-yyyy') as [regdate],CustomerCode FROM [Company] where [status]=0 and [isdeleted]=0 order by id desc";
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

    protected void txtonamefilter_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtonamefilter.Text.Trim()))
        {
            query = "SELECT [id],[ccode],[cname],[oname1],[email1],[mobile1],Format([regdate],'dd-MMM-yyyy') as [regdate],CustomerCode FROM [Company] where [status]=0 and [isdeleted]=0 and oname1 like '" + txtonamefilter.Text.Trim() + "%' order by id desc";
        }
        else
        {
            query = "SELECT [id],[ccode],[cname],[oname1],[email1],[mobile1],Format([regdate],'dd-MMM-yyyy') as [regdate],CustomerCode FROM [Company] where [status]=0 and [isdeleted]=0 order by id desc";
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

    //protected void txtmobilefilter_TextChanged(object sender, EventArgs e)
    //{
    //    string query = string.Empty;
    //    if (!string.IsNullOrEmpty(txtmobilefilter.Text.Trim()))
    //    {
    //        query = "SELECT [id],[ccode],[cname],[oname],[email],[mobile],Format([regdate],'dd-MMM-yyyy') as [regdate] FROM [Company] where [status]=0 and [isdeleted]=0 and mobile like '" + txtmobilefilter.Text.Trim() + "%' order by id desc";
    //    }
    //    else
    //    {
    //        query = "SELECT [id],[ccode],[cname],[oname],[email],[mobile],Format([regdate],'dd-MMM-yyyy') as [regdate] FROM [Company] where [status]=0 and [isdeleted]=0 order by id desc";
    //    }

    //    SqlDataAdapter ad = new SqlDataAdapter(query, con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        GvCompany.DataSource = dt;
    //        GvCompany.DataBind();
    //        lblnodatafoundComp.Visible = false;
    //    }
    //    else
    //    {
    //        GvCompany.DataSource = null;
    //        GvCompany.DataBind();
    //        lblnodatafoundComp.Text = "No Data Found !! ";
    //        lblnodatafoundComp.Visible = true;
    //        lblnodatafoundComp.ForeColor = System.Drawing.Color.Red;
    //    }
    //}
    #endregion Filter

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("AllCompanylist.aspx");
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompanyOwnerList(string prefixText, int count)
    {
        return AutoFillCompanyOwnerName(prefixText);
    }

    public static List<string> AutoFillCompanyOwnerName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [oname1] from [Company] where oname1 like @Search + '%' and status=0 and [isdeleted]=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["oname1"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    private string GetMailIdOfEmpl(string Empcode)
    {
        string query1 = "SELECT [email] FROM [employees] where [empcode]='" + Empcode + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        string email = string.Empty;
        if (dt.Rows.Count > 0)
        {
            email = dt.Rows[0]["email"].ToString();
        }
        return email;
    }

    protected void ddlsalesMainfilter_TextChanged(object sender, EventArgs e)
    {
        Gvbind();
    }

    protected void btnAddEnq_Click(object sender, EventArgs e)
    {
        string Cname = ((sender as Button).CommandArgument).ToString();
        Response.Redirect("Addenquiry.aspx?Cname=" + encrypt(Cname));
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid=" + id + "','','width=700px,height=600px');", true);
    }
}