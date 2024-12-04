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
        if (Session["name"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                GvBind();

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
            SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + id + "' AND PageName = 'QuotationList.aspx' AND PagesView = '1'", con);
            Sdd.Fill(Dtt);
            if (Dtt.Rows.Count > 0)
            {
                Session["PagesView"] = Dtt.Rows[0]["PagesView"].ToString();
                //GvQuotation.Columns[8].Visible = false;
                btnAddCompany.Visible = false;
                btnAddOA.Visible = false;
            }
        }
    }
    private void GvBind()
    {
        string query = "";
        if (!string.IsNullOrEmpty(txtquotationno.Text) && string.IsNullOrEmpty(txtconstructiontype.Text) && string.IsNullOrEmpty(txtCustomerName.Text) && ddlStatus.SelectedItem.Text == "All")
        {
            query = @"select TOP 100 A.id,filename1,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,A.sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise,C.CustomerCode from QuotationMain AS A INNER JOIN Company AS C ON C.ccode=A.ccode where quotationno like '" + txtquotationno.Text.Trim() + "%' order by id desc";
        }
        else if (string.IsNullOrEmpty(txtquotationno.Text) && !string.IsNullOrEmpty(txtconstructiontype.Text) && string.IsNullOrEmpty(txtCustomerName.Text) && string.IsNullOrEmpty(ddlStatus.SelectedItem.Text))
        {
            query = @"select TOP 100 A.id,filename1,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,A.sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise,C.CustomerCode from QuotationMain AS A INNER JOIN Company AS C ON C.ccode=A.ccode where Constructiontype like '" + txtconstructiontype.Text.Trim() + "%' order by id desc";
        }
        else if (!string.IsNullOrEmpty(txtCustomerName.Text) && string.IsNullOrEmpty(txtconstructiontype.Text) && string.IsNullOrEmpty(txtquotationno.Text) && ddlStatus.SelectedItem.Text == "All")
        {
            query = @"select TOP 100 A.id,filename1,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,A.sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise,C.CustomerCode from QuotationMain AS A INNER JOIN Company AS C ON C.ccode=A.ccode where partyname like '" + txtCustomerName.Text.Trim() + "%' order by id desc";

        }
        else if (!string.IsNullOrEmpty(txtquotationno.Text) && !string.IsNullOrEmpty(txtconstructiontype.Text))
        {
            query = @"select TOP 100 A.id,filename1,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,A.sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise,C.CustomerCode from QuotationMain AS A INNER JOIN Company AS C ON C.ccode=A.ccode where Constructiontype like '" + txtconstructiontype.Text.Trim() + "%' and quotationno like '" + txtquotationno.Text.Trim() + "%' order by id desc";
        }
        else if (string.IsNullOrEmpty(txtquotationno.Text) && string.IsNullOrEmpty(txtconstructiontype.Text) && string.IsNullOrEmpty(txtCustomerName.Text) && ddlStatus.SelectedItem.Text == "All")
        {
            query = @"select TOP 100 A.id,filename1,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,A.sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise,C.CustomerCode from QuotationMain AS A INNER JOIN Company AS C ON C.ccode=A.ccode order by id desc";
        }
        else if (ddlStatus.SelectedItem.Text == "Completed")
        {
            query = @"select TOP 100 A.id,filename1,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,A.sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise,C.CustomerCode from QuotationMain AS A INNER JOIN Company AS C ON C.ccode=A.ccode order by id desc";
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
        if (e.CommandName == "CompanyData")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;
            Label lblCode = (Label)GvQuotation.Rows[RowIndex].FindControl("lblquotationno");

            GetCompanyDataPopup(lblCode.Text);
            //GetCompanyDataBDEPopup(e.CommandArgument.ToString());
            this.modelprofile.Show();

        }
        if (e.CommandName == "RowEdit")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;
            Label lblConstructiontype = (Label)GvQuotation.Rows[RowIndex].FindControl("lblConstructiontype");
            //changes By shubham for seprations
            DataTable Dt = new DataTable();
            SqlDataAdapter Sd = new SqlDataAdapter("Select DocumentType from [QuotationMain] where [id]='" + id + "'", con);
            Sd.Fill(Dt);

            string DocType = Dt.Rows[0]["DocumentType"].ToString();
            if (DocType == "Part Of Enclouser")
            {
                Response.Redirect("QuotationCat3.aspx?cdd=" + encrypt(id.ToString()));
            }

            //if (lblConstructiontype.Text == "PartofControlPanel")
            //{
            //    Response.Redirect("QuotationCat2.aspx?cdd=" + encrypt(id.ToString()));
            //}
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

        //Old before 27-10-23
        //if (e.CommandName == "RowEdit")
        //{
        //    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //    int RowIndex = gvr.RowIndex;
        //    Label lblConstructiontype = (Label)GvQuotation.Rows[RowIndex].FindControl("lblConstructiontype");
        //    if (lblConstructiontype.Text == "PartofControlPanel")
        //    {
        //        Response.Redirect("QuotationCat2.aspx?cdd=" + encrypt(id.ToString()));
        //    }
        //    else
        //    {
        //        SqlCommand cmdexsi = new SqlCommand("select count(id) as count from OAList where quotationid='" + id + "'", con);
        //        con.Open();
        //        int cnt = Convert.ToInt32(cmdexsi.ExecuteScalar().ToString());
        //        con.Close();

        //        if (cnt > 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Quotation Already Existed in OA !!!');", true);
        //        }
        //        else
        //        {
        //            Response.Redirect("QuotationCat1.aspx?cdd=" + encrypt(id.ToString()));
        //        }

        //    }
        //}

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
            //message = (string)cmd.Parameters["@message"].Value;
            object messageObj = cmd.Parameters["@message"].Value;
             message = (messageObj == DBNull.Value) ? null : (string)messageObj;

            if (message == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Quotation has been send for OA !!!');", true);

                Response.Redirect("OrderAcceptance.aspx?cmd=" + encrypt(id.ToString()));
            }

            if (message == "inserted")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Quotation has been send for OA !!!');", true);

                Response.Redirect("OrderAcceptance.aspx?cmd=" + encrypt(id.ToString()));
            }
            if (message == "exist")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Quotation Already Existed in OA !!!');", true);
            }

        }
        if (e.CommandName == "RowDownloadAttachment")
        {
            DataTable Dt = new DataTable();
            SqlDataAdapter Sd = new SqlDataAdapter("Select * from [DB_ProcetechERP].[dbo].[QuotationMain] where id='" + id + "'", con);
            Sd.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                string filename1 = Dt.Rows[0]["filename1"].ToString();
                string filename2 = Dt.Rows[0]["filename2"].ToString();
                if (filename1 == "" || filename1 == null && filename2 == "" || filename2 == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Attachment Not Found..!!');", true);
                }
                else
                {
                    //string URL= "erp.excelenclosures.net/RefDocument/";
                    string URL = "~/RefDocument/";
                    string URL1 = URL + filename2;
                    Response.Redirect(URL1);
                }
            }
        }
    }
    private void GetCompanyDataPopup(string id)
    {
        string query1 = string.Empty;
        query1 = @"SELECT A.[ccode],A.[cname],A.[oname1],A.[oname2],A.[oname3],A.[oname4],A.[oname5],A.[email1],A.[mobile1],A.[mobile2],
      A.[mobile3],A.[mobile4],A.[mobile5],A.[billingaddress],[shippingaddress],format(A.[regdate],'dd-MMM-yyyy hh:mm tt') as [regdate],
A.[sessionname],A.[gstno],B.name,B.email as Empemail,A.[desig1],A.[desig2],A.[desig3],A.[desig4],A.[desig5], A.CustomerCode FROM [Company]
A join employees B on A.sessionname=B.name 
INNER JOIN QuotationMain AS Q ON Q.ccode=A.ccode WHERE Q.quotationno='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblcname.Text = dt.Rows[0]["cname"].ToString();
            lblcusomercode.Text = dt.Rows[0]["CustomerCode"].ToString();
            lblRegdate.Text = dt.Rows[0]["regdate"].ToString();
            lblregBy.Text = dt.Rows[0]["name"].ToString();
            lblgstno.Text = dt.Rows[0]["gstno"].ToString();

            ViewState["CurrentSalesEmail"] = dt.Rows[0]["Empemail"].ToString();// Current Sales Email


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
        try
        {
            string query = string.Empty;
            //GvBind();

            query = @"select id,filename1,quotationno,Format(createddate,'dd-MM-yyyy') as CreatedDate,sessionname,kindatt,partyname,specifymaterial,
(case when material='Specify' THEN specifymaterial ELSE material END) as material,
(case when Constructiontype='Specify' THEN Specifyconstruction ELSE Constructiontype END) as Constructiontype,IsRevise from QuotationMain where Constructiontype like '" + txtconstructiontype.Text.Trim() + "%' order by id desc";

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
        catch (Exception ex)
        {

            throw ex;
        }

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
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblfile = e.Row.FindControl("lblfile") as Label;
                Label revise = e.Row.FindControl("lblRevise") as Label;
                LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
                LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
                LinkButton btnDownloadAttachment = e.Row.FindControl("btnDownloadAttachment") as LinkButton;
                Button btnSendOA = e.Row.FindControl("btnOA") as Button;
                Label lblRevised = e.Row.FindControl("lblRevised") as Label;
                Label lblcreatedby = e.Row.FindControl("lblcreatedby") as Label;

                if (string.IsNullOrEmpty(lblfile.Text))
                {
                    btnDownloadAttachment.Enabled = false;
                    btnDownloadAttachment.ToolTip = "File Not Available";
                }

                //Edit Access Only Login User
                string sessinname = Session["name"].ToString();
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

                string empcode = Session["empcode"].ToString();
                DataTable Dt = new DataTable();
                SqlDataAdapter Sd = new SqlDataAdapter("Select id from [employees] where [empcode]='" + empcode + "'", con);
                Sd.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    string id = Dt.Rows[0]["id"].ToString();
                    DataTable Dtt = new DataTable();
                    SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM [DB_ProcetechTesting].[DB_ProcetechERP].[tblUserRoleAuthorization] where UserID = '" + id + "' AND PageName = 'QuotationList.aspx' AND PagesView = '1'", con);
                    Sdd.Fill(Dtt);
                    if (Dtt.Rows.Count > 0)
                    {
                        btnAddCompany.Visible = false;
                        btnAddOA.Visible = false;
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                        btnSendOA.Visible = false;
                        lblRevised.Visible = false;
                    }
                }

                //var PagesView = Session["PagesView"].ToString();
                //if (PagesView == "1")
                //{
                //    btnEdit.Visible = false;
                //    btnDelete.Visible = false;
                //    btnSendOA.Visible = false;
                //    lblRevised.Visible = false;                
                //}

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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlStatus_TextChanged(object sender, EventArgs e)
    {
        GvBind();
    }

    


}