#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\QuotationList.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "863C0C8514F146764418DF6457DC57000A7D1D5A"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\QuotationList.aspx.cs"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class Admin_QuotationList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
        }
    }

    private void GvBind()
    {
        string query = "";
        if (!string.IsNullOrEmpty(txtquotationno.Text) && string.IsNullOrEmpty(txtconstructiontype.Text) && string.IsNullOrEmpty(txtCustomerName.Text) && ddlStatus.SelectedItem.Text == "All")
        {
            query = @"select id,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise from QuotationMain where quotationno like '" + txtquotationno.Text.Trim() + "%' order by id desc";
        }
        else if (string.IsNullOrEmpty(txtquotationno.Text) && !string.IsNullOrEmpty(txtconstructiontype.Text) && string.IsNullOrEmpty(txtCustomerName.Text) && string.IsNullOrEmpty(ddlStatus.SelectedItem.Text))
        {
            query = @"select id,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise from QuotationMain where Constructiontype like '" + txtconstructiontype.Text.Trim() + "%' order by id desc";
        }
        else if (!string.IsNullOrEmpty(txtCustomerName.Text) && string.IsNullOrEmpty(txtconstructiontype.Text) && string.IsNullOrEmpty(txtquotationno.Text) && ddlStatus.SelectedItem.Text == "All")
        {
            query = @"select id,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise from QuotationMain where partyname like '" + txtCustomerName.Text.Trim() + "%' order by id desc";

        }
        else if (!string.IsNullOrEmpty(txtquotationno.Text) && !string.IsNullOrEmpty(txtconstructiontype.Text))
        {
            query = @"select id,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise from QuotationMain where Constructiontype like '" + txtconstructiontype.Text.Trim() + "%' and quotationno like '" + txtquotationno.Text.Trim() + "%' order by id desc";
        }
        else if (string.IsNullOrEmpty(txtquotationno.Text) && string.IsNullOrEmpty(txtconstructiontype.Text) && string.IsNullOrEmpty(txtCustomerName.Text) && ddlStatus.SelectedItem.Text == "All")
        {
            query = @"select id,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise from QuotationMain order by id desc";
        }
        else if (ddlStatus.SelectedItem.Text == "Completed")
        {
            query = @"select id,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise from QuotationMain  order by id desc";
        }

        SqlDataAdapter adp = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Constructiontype"].ToString() == "JBboxdata")
                {
                    dt.Rows[i]["Constructiontype"] = "JB Box";
                }
                if (dt.Rows[i]["Constructiontype"].ToString() == "EcoMcc")
                {
                    dt.Rows[i]["Constructiontype"] = "Eco MCC 30mm";
                }
                if (dt.Rows[i]["Constructiontype"].ToString() == "Modular")
                {
                    dt.Rows[i]["Constructiontype"] = "Modular W-Big 43mm";
                }
                if (dt.Rows[i]["Constructiontype"].ToString() == "EcoFrame")
                {
                    dt.Rows[i]["Constructiontype"] = "Eco Frame 43mm";
                }
                if (dt.Rows[i]["Constructiontype"].ToString() == "PCEnclosureECOStanding")
                {
                    dt.Rows[i]["Constructiontype"] = "PC Enclosure ECO-Standing";
                }
                if (dt.Rows[i]["Constructiontype"].ToString() == "ShopFloorPCEnclosureStanding")
                {
                    dt.Rows[i]["Constructiontype"] = "SHOP FLOOR PC ENCLOSURE STANDING";
                }
                if (dt.Rows[i]["Constructiontype"].ToString() == "PCEnclosureECOSitting")
                {
                    dt.Rows[i]["Constructiontype"] = "PC Enclosure ECO-Sitting";
                }
            }

            GvQuotation.DataSource = dt;
            GvQuotation.DataBind();
            GvQuotation.EmptyDataText = "No data found !!!";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvQuotation.ClientID + "', 900, 1020 , 40 ,true); </script>", false);

        }
		 else
        {
            GvQuotation.DataSource = null;
            GvQuotation.DataBind();
            GvQuotation.EmptyDataText = "No data found !!!";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvQuotation.ClientID + "', 400, 1020 , 40 ,true); </script>", false);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetQuotationList(string prefixText, int count)
    {
        return AutoFillQuotationlist(prefixText);
    }

    public static List<string> AutoFillQuotationlist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT quotationno from QuotationMain where " + "quotationno like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> Qno = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Qno.Add(sdr["quotationno"].ToString());
                    }
                }
                con.Close();
                return Qno;
            }
        }
    }

    protected void txtquotationno_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        GvBind();
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuotationList.aspx");
    }

    protected void GvQuotation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvQuotation.PageIndex = e.NewPageIndex;
        GvBind();
    }

    protected void GvQuotation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id;
        id = Convert.ToInt32(e.CommandArgument.ToString());

        //GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        //int RowIndex = gvr.RowIndex;
        //Label lblConstructiontype = (Label)GvQuotation.Rows[RowIndex].FindControl("lblConstructiontype");

        if (e.CommandName == "RowEdit")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;
            Label lblConstructiontype = (Label)GvQuotation.Rows[RowIndex].FindControl("lblConstructiontype");
            if (lblConstructiontype.Text == "PartofControlPanel")
            {
                Response.Redirect("QuotationCat2.aspx?cdd=" + encrypt(id.ToString()));
            }
            else
            {
                SqlCommand cmdexsi = new SqlCommand("select count(id) as count from OAList where quotationid='" + id + "'", con);
                con.Open();
                int cnt = Convert.ToInt32(cmdexsi.ExecuteScalar().ToString());
                con.Close();

                if (cnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Quotation Already Existed in OA !!!');", true);
                }
                else
                {
                    Response.Redirect("QuotationCat1.aspx?cdd=" + encrypt(id.ToString()));
                }

            }
        }
        if (e.CommandName == "RowDelete")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;
            Label lblConstructiontype = (Label)GvQuotation.Rows[RowIndex].FindControl("lblConstructiontype");
            SqlCommand cmddelete = new SqlCommand("SP_Deletequotation", con);
            cmddelete.CommandType = CommandType.StoredProcedure;

            cmddelete.Parameters.AddWithValue("@id", id);

            con.Open();
            cmddelete.ExecuteNonQuery();
            con.Close();
            GvBind();
        }
        if (e.CommandName == "RowOA")
        {
            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;
            Label lblConstructiontype = (Label)GvQuotation.Rows[RowIndex].FindControl("lblConstructiontype");
            string message = "";
            SqlCommand cmd = new SqlCommand("SP_SendForOA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@message", SqlDbType.VarChar, 500);
            cmd.Parameters.AddWithValue("@quotationid", id);
            con.Open();
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery().ToString();
            con.Close();
            message = (string)cmd.Parameters["@message"].Value;
            if (message == "inserted")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Quotation has been send for OA !!!');", true);

                //Response.Redirect("OrderAcceptance.aspx?cmd=" + encrypt(id.ToString()));
            }
            if (message == "exist")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Quotation Already Existed in OA !!!');", true);
            }

        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetConstructiontypeList(string prefixText, int count)
    {
        return AutoFillConstructiontypelist(prefixText);
    }

    public static List<string> AutoFillConstructiontypelist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT Constructiontype from QuotationMain where " + "Constructiontype like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> Qno = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Qno.Add(sdr["Constructiontype"].ToString());
                    }
                }
                con.Close();
                return Qno;
            }
        }
    }


    protected void txtconstructiontype_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        GvBind();
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

    protected void GvQuotation_PreRender(object sender, EventArgs e)
    {
        if (GvQuotation.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            GvQuotation.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements
            GvQuotation.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. 
            //Remove if you don't have a footer row
            //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
    {
        return AutoFillCustomerlist(prefixText);
    }

    public static List<string> AutoFillCustomerlist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT cname from Company where " + "cname like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> Qno = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Qno.Add(sdr["cname"].ToString());
                    }
                }
                con.Close();
                return Qno;
            }
        }
    }

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        string query = string.Empty;
        GvBind();
    }

    protected void GvQuotation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label revise = e.Row.FindControl("lblRevise") as Label;
            LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
            Button btnSendOA = e.Row.FindControl("btnOA") as Button;
            Label lblRevised = e.Row.FindControl("lblRevised") as Label;
            Label lblcreatedby = e.Row.FindControl("lblcreatedby") as Label;

            //Edit Access Only Login User
            string sessinname = Session["adminname"].ToString();
            if (lblcreatedby.Text == sessinname)
            {
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = true;
            }

            if (revise.Text == "True")
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnSendOA.Visible = false;
                lblRevised.Visible = true;
            }
            else
            {
                //btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSendOA.Enabled = true;
                lblRevised.Visible = false;
            }

            //Label lblIscompletd = e.Row.FindControl("lblIscompletd") as Label;
            //LinkButton btnEditgv = e.Row.FindControl("btnEdit") as LinkButton;
            //LinkButton btnDeletegv = e.Row.FindControl("btnDelete") as LinkButton;
            //Button btnOAgv = e.Row.FindControl("btnOA") as Button;
            //if (lblIscompletd.Text == "True")
            //{
            //    btnEditgv.Visible = false;
            //    btnDeletegv.Visible = false;
            //    btnOAgv.Visible = false;
            //}
        }
    }

    protected void ddlStatus_TextChanged(object sender, EventArgs e)
    {
        GvBind();
    }
}

#line default
#line hidden
