#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Sales\AllCompanyList.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8E980A04C0DA0EE31F13A1B32D75D20F3D462F3B"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Sales\AllCompanyList.aspx.cs"
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
        //if (Session["adminname"] == null || Session["adminempcode"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //}
        //else
        //{
            if (!IsPostBack)
            {
            DdlSalesBind();
            Gvbind();
            }
        //}  
    }

    private void Gvbind()
    {
        string query = string.Empty;
        if (ddlsalesMainfilter.Text!="All")
        {
            query = @"SELECT [id],[ccode],[cname],[oname],[email],[mobile],Format([regdate],'dd-MMM-yyyy') as [regdate] FROM [Company] where [status]=0 and [isdeleted]=0 and sessionname='" + ddlsalesMainfilter.SelectedValue + "' order by id desc";
        }
        else
        {
            query = @"SELECT [id],[ccode],[cname],[oname],[email],[mobile],Format([regdate],'dd-MMM-yyyy') as [regdate] FROM [Company] where [status]=0 and [isdeleted]=0 order by id desc";
        }
       
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvCompany.DataSource = dt;
            GvCompany.DataBind();
            lblnodatafoundComp.Visible = false;
        }
        else
        {
            GvCompany.DataSource = null;
            GvCompany.DataBind();
            lblnodatafoundComp.Text = "No Data Found !! ";
            lblnodatafoundComp.Visible = true;
            lblnodatafoundComp.ForeColor = System.Drawing.Color.Red;
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
    }

    //// GetSessionName to prevent editing the company's info to other person
    //private string GetSessionName(string rowid)
    //{
    //    SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],[sessionname] FROM [Company] where id='" + rowid + "' ", con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    string sscode = string.Empty;
    //    if (dt.Rows.Count > 0)
    //    {
    //        sscode= dt.Rows[0]["sessionname"].ToString();
    //    }
    //    return sscode;
    //}

    protected void GvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ViewState["CompRowId"]= e.CommandArgument.ToString();
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
    }

    private void GetCompanyDataPopup(string id)
    {
        string query1 = string.Empty;
        query1 = "SELECT A.[ccode],A.[cname],A.[oname],A.[email],A.[mobile],A.[billingaddress],[shippingaddress],format(A.[regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],A.[sessionname],A.[gstno],B.name,B.email as Empemail FROM [Company] A join employees B on A.sessionname=B.empcode where A.id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblccode.Text = dt.Rows[0]["ccode"].ToString();
            lblcname.Text = dt.Rows[0]["cname"].ToString();
            lbloname.Text = dt.Rows[0]["oname"].ToString();
            lblemail.Text = dt.Rows[0]["email"].ToString();
            lblmobile.Text = dt.Rows[0]["mobile"].ToString();

            lblRegdate.Text = dt.Rows[0]["regdate"].ToString();
            lblregBy.Text = dt.Rows[0]["name"].ToString();
            lblgstno.Text = dt.Rows[0]["gstno"].ToString();
            lblbillingaddress.Text = dt.Rows[0]["billingaddress"].ToString();
            lblshipaddress.Text = dt.Rows[0]["shippingaddress"].ToString();
            //dt.Rows[0]["sessionname"].ToString();// Current Sales code
            ViewState["CurrentSalesEmail"]= dt.Rows[0]["Empemail"].ToString();// Current Sales Email
            //GetCompanyDataBDEPopup(dt.Rows[0]["BDE"].ToString());

            //if (dt.Rows[0]["sessionname"].ToString() != Session["adminempcode"].ToString())
            //{
            //    btnComment.Enabled = false;
            //    txtcomment.ReadOnly = true;
            //    txtaddemail.ReadOnly = true;
            //}
            //else
            //{
            //    btnComment.Enabled = true;
            //    txtcomment.ReadOnly = false;
            //    txtaddemail.ReadOnly = false;
            //}
        }
    }

    //private void GetCompanyDataBDEPopup(string id)
    //{
    //    string query1 = string.Empty;
    //    query1 = "SELECT name as BDE,email as TMEemail,empcode FROM employees  where empcode='" + id + "' ";
    //    SqlDataAdapter ad = new SqlDataAdapter(query1, con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblbde.Text = dt.Rows[0]["BDE"].ToString();
    //        ddlbdepopup.SelectedItem.Text = dt.Rows[0]["BDE"].ToString();
    //        BdeCode.Value= dt.Rows[0]["empcode"].ToString();
    //        ViewState["CurrentTMEEmail"] = dt.Rows[0]["TMEemail"].ToString();
    //    }
    //}

    //private void GetEmpEmail(string empCode)
    //{
    //    string query1 = string.Empty;
    //    query1 = "SELECT [email] FROM [employees] where [empcode]='" + empCode + "' ";
    //    SqlDataAdapter ad = new SqlDataAdapter(query1, con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        BdeMailId.Value = dt.Rows[0]["email"].ToString();
    //    }
    //}

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
            query = "SELECT [id],[ccode],[cname],[oname],[email],[mobile],Format([regdate],'dd-MMM-yyyy') as [regdate] FROM [Company] where [status]=0 and [isdeleted]=0 and cname like '" + txtcnamefilter.Text.Trim() + "%' order by id desc";
        }
        else
        {
            query = "SELECT [id],[ccode],[cname],[oname],[email],[mobile],Format([regdate],'dd-MMM-yyyy') as [regdate] FROM [Company] where [status]=0 and [isdeleted]=0 order by id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvCompany.DataSource = dt;
            GvCompany.DataBind();
            lblnodatafoundComp.Visible = false;
        }
        else
        {
            GvCompany.DataSource = null;
            GvCompany.DataBind();
            lblnodatafoundComp.Text = "No Data Found !! ";
            lblnodatafoundComp.Visible = true;
            lblnodatafoundComp.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void txtonamefilter_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        if (!string.IsNullOrEmpty(txtonamefilter.Text.Trim()))
        {
            query = "SELECT [id],[ccode],[cname],[oname],[email],[mobile],Format([regdate],'dd-MMM-yyyy') as [regdate] FROM [Company] where [status]=0 and [isdeleted]=0 and oname like '" + txtonamefilter.Text.Trim() + "%' order by id desc";
        }
        else
        {
            query = "SELECT [id],[ccode],[cname],[oname],[email],[mobile],Format([regdate],'dd-MMM-yyyy') as [regdate] FROM [Company] where [status]=0 and [isdeleted]=0 order by id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvCompany.DataSource = dt;
            GvCompany.DataBind();
            lblnodatafoundComp.Visible = false;
        }
        else
        {
            GvCompany.DataSource = null;
            GvCompany.DataBind();
            lblnodatafoundComp.Text = "No Data Found !! ";
            lblnodatafoundComp.Visible = true;
            lblnodatafoundComp.ForeColor = System.Drawing.Color.Red;
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
                com.CommandText = "Select DISTINCT [oname] from [Company] where " + "oname like @Search + '%' and status=0 and [isdeleted]=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["oname"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    

    private string GetMailIdOfEmpl(string Empcode)
    {
        string query1  = "SELECT [email] FROM [employees] where [empcode]='" + Empcode + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        string email = string.Empty;
        if (dt.Rows.Count > 0)
        {
             email= dt.Rows[0]["email"].ToString();
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
        Response.Redirect("Addenquiry.aspx?Cname="+ encrypt(Cname));
        //Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid=" + id + "','','width=700px,height=600px');", true);
    }
}

#line default
#line hidden
