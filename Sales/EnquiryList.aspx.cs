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

public partial class Admin_EnquiryList : System.Web.UI.Page
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
        if (ddlsalesMainfilter.Text != "All")
        {
            query = @"SELECT [id],[EnqCode],[ccode],[cname],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] where sessionname='" + ddlsalesMainfilter.SelectedValue + "' order by id desc";
        }
        else
        {
            query = @"SELECT [id],[EnqCode],[ccode],[cname],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] order by id desc";
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
    }

    private void GetCompanyDataPopup(string id)
    {
        string query1 = string.Empty;
        query1 = "SELECT A.[EnqCode],A.[cname],A.[status],A.[remark],format(A.[regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],A.[sessionname],B.name FROM [EnquiryData] A join employees B on A.sessionname=B.empcode where A.id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblenqcode.Text = dt.Rows[0]["EnqCode"].ToString();
            lblcname.Text = dt.Rows[0]["cname"].ToString();
            lblremark.Text = dt.Rows[0]["remark"].ToString();
            if (dt.Rows[0]["status"].ToString()=="False" || dt.Rows[0]["status"].ToString() == "false")
            {
                lblstatus.Text = "Open";
            }
            else
            {
                lblstatus.Text = "Close";
            }
             
            
            lblRegdate.Text = dt.Rows[0]["regdate"].ToString();
            lblregBy.Text = dt.Rows[0]["name"].ToString();
           
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
            query = "SELECT [id],[EnqCode],[ccode],[cname],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] where cname like '" + txtcnamefilter.Text.Trim() + "%' order by id desc";
        }
        else
        {
            query = "SELECT [id],[EnqCode],[ccode],[cname],Format([regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],[sessionname] FROM [EnquiryData] order by id desc";
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
        string id = ((sender as LinkButton).CommandArgument).ToString();
        Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('EnquiryFile.aspx?Fileid=" + id + "','','width=700px,height=600px');", true);
    }

    protected void btnsendquot_Click(object sender, EventArgs e)
    {
        string Ccode = ((sender as Button).CommandArgument).ToString();
        Response.Redirect("Quotation.aspx?Ccode="+ encrypt(Ccode));
    }

   
}