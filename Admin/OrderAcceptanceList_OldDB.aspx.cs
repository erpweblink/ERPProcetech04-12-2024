﻿using System;
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
using System.Threading;
using System.Collections;

public partial class Admin_OrderAcceptanceList_OldDB : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["New_connectionString"].ConnectionString);
    SqlDataAdapter adp;
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
                FillOAgrid();
            }
        }
    }

    private void FillOAgrid()
    {

        string query = "";
        if (!string.IsNullOrEmpty(txtCustomerName.Text))
        {
            adp = new SqlDataAdapter(@"SELECT [id],[quotationid],[oano],[quotationno],[constructiontype],[partyname],[description],
                             [status],convert(varchar(20),[createddate],105) as createddate,status,emailstatus,deliverydatereqbycust FROM [ExcelEncLive].[vw_OAList] where IsDispatch is null and partyname like '" + txtCustomerName.Text.Trim() + "%' order by id asc", con);
        }
        if (ddlDispatchList.SelectedItem.Text == "Pending")
        {
            adp = new SqlDataAdapter(@"SELECT [id],[quotationid],[oano],[quotationno],[constructiontype],[partyname],[description],
                             [status],convert(varchar(20),[createddate],105) as createddate,status,emailstatus,deliverydatereqbycust FROM [ExcelEncLive].[vw_OAList] where IsDispatch is null order by id asc", con);
        }
        if (ddlDispatchList.SelectedItem.Text == "Dispatch")
        {
            adp = new SqlDataAdapter(@"SELECT [id],[quotationid],[oano],[quotationno],[constructiontype],[partyname],[description],
                             [status],convert(varchar(20),[createddate],105) as createddate,status,emailstatus,deliverydatereqbycust FROM [ExcelEncLive].[vw_OAList] where IsDispatch='1' order by id asc", con);
        }
        if (ddlDispatchList.SelectedItem.Text == "Dispatch" && !string.IsNullOrEmpty(txtCustomerName.Text))
        {
            adp = new SqlDataAdapter(@"SELECT [id],[quotationid],[oano],[quotationno],[constructiontype],[partyname],[description],
                             [status],convert(varchar(20),[createddate],105) as createddate,status,emailstatus,deliverydatereqbycust FROM [ExcelEncLive].[vw_OAList] where IsDispatch='1' and partyname like '" + txtCustomerName.Text.Trim() + "%' order by id asc", con);
        }
        if (ddlDispatchList.SelectedItem.Text == "Pending" && !string.IsNullOrEmpty(txtCustomerName.Text))
        {
            adp = new SqlDataAdapter(@"SELECT [id],[quotationid],[oano],[quotationno],[constructiontype],[partyname],[description],
                             [status],convert(varchar(20),[createddate],105) as createddate,status,emailstatus,deliverydatereqbycust FROM [ExcelEncLive].[vw_OAList] where IsDispatch is null and partyname like '" + txtCustomerName.Text.Trim() + "%' order by id asc", con);
        }
        //else
        //{
        //    adp = new SqlDataAdapter(@"SELECT [id],[quotationid],[oano],[quotationno],[constructiontype],[partyname],[description],
        //                     [status],convert(varchar(20),[createddate],105) as createddate,status,emailstatus,deliverydatereqbycust FROM [ExcelEncLive].[vw_OAList] order by id asc", con);
        //}
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            GvOA.DataSource = dt;
            GvOA.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GvOA.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
        }
        else
        {
            GvOA.DataSource = null;
            GvOA.DataBind();

        }
    }

    protected void GvOA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        if (e.CommandName == "RowCreateOA")
        {
            Response.Redirect("OrderAcceptance.aspx?cmd=" + encrypt(id.ToString()));
        }
        if (e.CommandName == "RowEditOA")
        {
            Control ctrl = e.CommandSource as Control;
            GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
            Label ConstType = (Label)row.FindControl("lblConstructiontype");
            Thread.Sleep(2000);
            if (ConstType.Text == "")
            {
                Response.Redirect("ManualOrderAcceptance.aspx?cmded=" + encrypt(id.ToString()));
            }
            else
            {
                Response.Redirect("OrderAcceptance.aspx?cmded=" + encrypt(id.ToString()));
            }

        }

        if (e.CommandName == "RowDeleteOA")
        {
            SqlCommand cmddelete2 = new SqlCommand("delete from OrderAccept where OAno='" + id + "'", con);
            con.Open();
            Thread.Sleep(1000);
            cmddelete2.ExecuteNonQuery();
            con.Close();

            SqlCommand cmddelete4 = new SqlCommand("delete from OrderAcceptDtls where OAno='" + id + "'", con);
            con.Open();
            Thread.Sleep(1000);
            cmddelete4.ExecuteNonQuery();
            con.Close();

            SqlCommand cmddelete3 = new SqlCommand("delete from OAList where oano='" + id + "'", con);
            con.Open();
            Thread.Sleep(1000);
            cmddelete3.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('OA has been Deleted Succesfully...!!!');window.location='OrderAcceptanceList.aspx';", true);
        }
        if (e.CommandName == "partyname")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                //GetCompanyDataPopup(e.CommandArgument.ToString());
                //GetCompanyDataBDEPopup(e.CommandArgument.ToString());
                GetOAStatusByCustName(e.CommandArgument.ToString());
                this.modelprofile.Show();
            }
        }
    }

    protected void GvOA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvOA.PageIndex = e.NewPageIndex;
        FillOAgrid();
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
    protected void BindDeprt()
    {
        try
        {
            ArrayList alstNames = new ArrayList();
            alstNames.Add("Drawing Creation");
            alstNames.Add("Laser Programing");
            alstNames.Add("Laser Cutting");
            alstNames.Add("CNC Bending");
            alstNames.Add("Welding");
            alstNames.Add("Powder Coating");
            alstNames.Add("Final Assembly");
            //alstNames.Add("Final Inspection");
            alstNames.Add("Stock");
            dgvDeprt.DataSource = alstNames;
            dgvDeprt.DataBind();
        }
        catch (Exception)
        {

            throw;
        }

    }

    private void GetOAStatusByCustName(string OAnum)
    {
        try
        {
            SqlDataAdapter adp = new SqlDataAdapter(@"select DISTINCT(OAno) from [ExcelEncLive].[vwOrderAccept] where OAno='" + OAnum + "'", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgvOANumber.DataSource = dt;
                dgvOANumber.DataBind();
            }

            DataTable dtorder = new DataTable();
            SqlDataAdapter sadorder = new SqlDataAdapter("select * from [ExcelEncLive].[vwOrderAccept] where OAno='" + OAnum + "'", con);
            sadorder.Fill(dtorder);
            ddlSubOaNo.DataValueField = "SubOANumber";
            ddlSubOaNo.DataTextField = "SubOANumber";
            lblcname.Text = dtorder.Rows[0]["customername"].ToString();
            ddlSubOaNo.DataSource = dtorder;
            ddlSubOaNo.DataBind();

            //SqlDataAdapter adp1 = new SqlDataAdapter(@"select SubOANumber,customername from [ExcelEncLive].[vwOrderAccept] where OAno='" + OAnum + "'", con);
            //DataTable dt1 = new DataTable();
            //adp1.Fill(dt1);
            //if (dt1.Rows.Count > 0)
            //{
            //    dgvSubOA.DataSource = dt1;
            //    dgvSubOA.DataBind();

            //    lblcname.Text = dt1.Rows[0]["customername"].ToString();
            //}
            SqlCommand cmdtot = new SqlCommand("select SUM(CAST(Qty as int)) as Tot from [ExcelEncLive].[vwOrderAccept] where OAno='" + OAnum + "'", con);
            con.Open();
            Object Tot = cmdtot.ExecuteScalar();
            //lblQty.Text = Tot.ToString();
            con.Close();

            string query = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ExcelEncLive].[SP_CustomerWise]";
            cmd.Parameters.AddWithValue("@OANumber", OAnum);
            cmd.Connection = con;
            con.Open();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //bind the 1st resultset
                    dgvCustomerWise.EmptyDataText = "No Records Found";
                    dgvCustomerWise.DataSource = reader;
                    dgvCustomerWise.DataBind();
                    dgvCustomerWise.ShowHeader = true;
                    divdgvCustomerWise.Visible = true;
                    BindDeprt();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        catch (Exception)
        {
            throw;
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
        FillOAgrid();
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        txtCustomerName.Text = string.Empty;
        FillOAgrid();
    }
    protected void ddlDispatchList_TextChanged(object sender, EventArgs e)
    {
        FillOAgrid();
    }

    protected void GvOA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Label1 = e.Row.FindControl("Label1") as Label;
            LinkButton btnCreate = e.Row.FindControl("btnCreate") as LinkButton;
            LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;

            string empcode = Session["empcode"].ToString();
            DataTable Dt = new DataTable();
            SqlDataAdapter Sd = new SqlDataAdapter("Select id from [employees] where [empcode]='" + empcode + "'", con);
            Sd.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                string id = Dt.Rows[0]["id"].ToString();
                DataTable Dtt = new DataTable();
                SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM [ExcelEncLive].[tblUserRoleAuthorization] where UserID = '" + id + "' AND PageName = 'OrderAcceptanceList.aspx' AND PagesView = '1'", con);
                Sdd.Fill(Dtt);
                if (Dtt.Rows.Count > 0)
                {
                    btnCreate.Visible = false;
                    //btnAddOpenOrder.Visible = false;
                    //btnQuoList.Visible = false;
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }
    }
        
    protected void Bind_SubOaDDl()
    {
        ddlSubOaNo.Items.Clear();
        SqlCommand cmdGetOa = new SqlCommand("select * from [ExcelEncLive].[vwOrderAccept] where SubOANumber='" + ddlSubOaNo.Text + "'", con);
        con.Open();
        Object Var_OaNumber = cmdGetOa.ExecuteScalar();
        DataTable dtorder = new DataTable();
        SqlDataAdapter sadorder = new SqlDataAdapter("select * from [ExcelEncLive].[vwOrderAccept] where OAno='" + Var_OaNumber + "'", con);
        sadorder.Fill(dtorder);
        //ddlSubOaNo.DataValueField = "SubOANumber";
        ddlSubOaNo.DataTextField = "SubOANumber";
        lblcname.Text = dtorder.Rows[0]["customername"].ToString();
        ddlSubOaNo.DataSource = dtorder;
        ddlSubOaNo.DataBind();
        ddlSubOaNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }

    protected void ddlSubOaNo_TextChanged(object sender, EventArgs e)
    {
        divSubOaDetails.Visible = true;
        divSuboaGrid.Visible = true;
        con.Close();
        this.modelprofile.Show();
        try
        {
            SqlCommand cmdGetOa = new SqlCommand("select * from [ExcelEncLive].[vwOrderAccept] where SubOANumber='" + ddlSubOaNo.Text + "'", con);
            con.Open();
            Object Var_OaNumber = cmdGetOa.ExecuteScalar();

            SqlDataAdapter adp = new SqlDataAdapter(@"select DISTINCT(OAno) from [ExcelEncLive].[vwOrderAccept] where OAno='" + Var_OaNumber + "'", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgvOANumber.DataSource = dt;
                dgvOANumber.DataBind();

            }

            if (!IsPostBack)
            {
                Bind_SubOaDDl();
            }

            SqlDataAdapter adp1 = new SqlDataAdapter(@"select * from [ExcelEncLive].[vwOrderAccept] where SubOANumber='" + ddlSubOaNo.SelectedItem.Text + "'", con);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                //if (ddlSubOaNo.Text != "--Select--")
                //{
                dgvSubOADetails.DataSource = dt1;
                dgvSubOADetails.DataBind();
                //}
                //else
                //{
                //    dgvSubOADetails.EmptyDataText = "No Records Found";
                //    dgvSubOADetails.DataBind();
                //}
            }

            SqlCommand cmdtot = new SqlCommand("select SUM(CAST(Qty as int)) as Tot from [ExcelEncLive].[vwOrderAccept] where OAno='" + Var_OaNumber + "'", con);
            //con.Open();
            Object Tot = cmdtot.ExecuteScalar();
            //lblQty.Text = Tot.ToString();
            con.Close();

            string query = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SP_SubOawiseDetailsNew]";
            cmd.Parameters.AddWithValue("@SubOANumber", ddlSubOaNo.SelectedItem.Text);
            cmd.Connection = con;
            con.Open();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //bind the 1st resultset
                    dgvCustomerWise.EmptyDataText = "No Records Found";
                    dgvCustomerWise.DataSource = reader;
                    dgvCustomerWise.DataBind();
                    dgvCustomerWise.ShowHeader = true;
                    divdgvCustomerWise.Visible = true;
                    BindDeprt();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}