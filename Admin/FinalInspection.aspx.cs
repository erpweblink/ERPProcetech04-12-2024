using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_FinalInspection : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string InwardQty = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!this.IsPostBack)
        {
            GetFinalInspectionData();
            //GetLaserProgamData();
        }
        }
    }

    private void FinalInspectionDDLbind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select FinalInspectionId, OANumber as OANumber from tblFinalInspection where IsApprove=1 and IsComplete is null order by FinalInspectionId desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlONumber.DataSource = dt;
            ddlONumber.DataTextField = "OANumber";
            ddlONumber.DataValueField = "FinalInspectionId";
            ddlONumber.DataBind();
            ddlONumber.Items.Insert(0, "All");
            ddlONumber.Items.Insert(0, "--Select--");
        }
    }

    protected void GetFinalInspectionData()
    {
        try
        {
            string query = string.Empty;
            
                query = @"SELECT [FinalInspectionId],[OANumber],[SubOA],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],
                [DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate] 
                FROM [ExcelEncLive].[dbo].[tblFinalInspection]
                where IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";
            

            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //btnExport.Visible = true;
                dgvFinalInspection.DataSource = dt;
                dgvFinalInspection.DataBind();
            }
            else
            {
                dgvFinalInspection.DataSource = null;
                dgvFinalInspection.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Not Found..!');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Save Data
    protected void GetSelectedRecords(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("OAnumber"), new DataColumn("SubOA"), new DataColumn("customername"), new DataColumn("size"), new DataColumn("totalinward"), new DataColumn("inwarddatetime"), new DataColumn("inwardqty"), new DataColumn("outwarddatetime"), new DataColumn("outwardqty"), new DataColumn("deliverydate") });
            foreach (GridViewRow row in dgvFinalInspection.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                    int totalCount = dgvFinalInspection.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
                    if (totalCount <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select Atleast One Row..!!');", true);
                        flag = true;
                    }
                    else
                    {
                        if (chkRow.Checked)
                        {
                            string OANumber = (row.Cells[1].FindControl("lblOANumber") as Label).Text;
                            string SubOA = (row.Cells[1].FindControl("lblSubOANumber") as Label).Text;
                            //string CustName = (row.Cells[1].FindControl("lblCustName") as Label).Text;
							TextBox Custtb = (TextBox)row.Cells[1].FindControl("lblCustName");
                            string CustName = Custtb.Text;
                            string TotalQty = (row.Cells[1].FindControl("lblTotalQty") as Label).Text;
                            string DeliveryDt = (row.Cells[1].FindControl("lblDeliveryDt") as Label).Text;
                            string InwardDtTime = (row.Cells[1].FindControl("lblInwardDtTime") as Label).Text;
                            TextBox tb = (TextBox)row.Cells[1].FindControl("txtInwardQty");
                            string InwardQty = tb.Text;
                            //Get Date and time gridview row
                            TextBox tbOutwardDt = (TextBox)row.Cells[1].FindControl("txtOutwardDtTime");
                           // DateTime OutwardDtT = DateTime.Parse(tbOutwardDt.Text);
                            string time = DateTime.Now.ToString("h:mm tt");
                            string OutwardDtTime = tbOutwardDt.Text + " " + time;

                            TextBox Outwardtb = (TextBox)row.Cells[1].FindControl("txtOutwardQty");
                            string OutwardQty = Outwardtb.Text;

                            TextBox Sizetb = (TextBox)row.Cells[1].FindControl("lblSize");
                            string Size = Sizetb.Text;

                            dt.Rows.Add(OANumber, SubOA, CustName, Size, TotalQty, InwardDtTime, InwardQty, OutwardDtTime, OutwardQty, DeliveryDt);
                        }
                    }
                }
            }
            if (flag == false)
            {
                using (con)
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        bool IsApprove = true, IsPending = false, IsCancel = false, Iscomplete;
                        if (ViewState["Iscomplete"] != null)
                        {
                            Iscomplete = false;
                        }
                        else
                        {
                            Iscomplete = true;
                        }
                        string CreatedBy = Session["name"].ToString(), UpdatedBy = "";
                        string UpdatedDate = DateTime.Now.ToShortDateString(), CreatedDate = DateTime.Now.ToShortDateString();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            con.Open();
                            if (dt.Rows[i]["inwardqty"].ToString() == "0")
                            {
                                Iscomplete = true;
                            }
                            else
                            {
                                Iscomplete = false;
                            }

                            SqlCommand cmdexsist = new SqlCommand("select OANumber,InwardQty,OutwardQty,SubOA from tblStock WHERE SubOA='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                            string OanumberExsists = "", InwardQty = "", OutwardQty = "";
                            using (SqlDataReader dr = cmdexsist.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    OanumberExsists = dr["SubOA"].ToString();
                                    InwardQty = dr["InwardQty"].ToString();
                                    OutwardQty = dr["OutwardQty"].ToString();
                                }
                            }

                            SqlCommand cmd2 = new SqlCommand("select OutwardQty from tblFinalInspection WHERE SubOA='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                            string Outward2Qty = "";
                            using (SqlDataReader dr = cmd2.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Outward2Qty = dr["OutwardQty"].ToString();
                                }
                            }

                            if (OanumberExsists == "")
                            {
                                cmd.CommandText += "INSERT INTO [dbo].[tblStock]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[SubOA])" +
                            "VALUES('" + dt.Rows[i]["OAnumber"].ToString() + "','" + dt.Rows[i]["customername"].ToString() + "'," +
                            "'" + dt.Rows[i]["size"].ToString() + "','" + dt.Rows[i]["totalinward"].ToString() + "'," +
                            "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','" + dt.Rows[i]["outwardqty"].ToString() + "'," +
                            "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','0'," +
                            "'" + dt.Rows[i]["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                            "'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','','"+ dt.Rows[i]["SubOA"].ToString() + "'); ";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                int totOutwardqnt = Convert.ToInt32(InwardQty) + Convert.ToInt32(dt.Rows[i]["outwardqty"].ToString());
                                SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblStock] SET [InwardQty] = '" + totOutwardqnt.ToString() + "',[InwardDtTime]='" + dt.Rows[i]["outwarddatetime"].ToString() + "' WHERE SubOA='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                                cmdupdate.ExecuteNonQuery();
                            }

                            if (ViewState["Iscomplete"] != null)
                            {
                                int totoutward = Convert.ToInt32(Outward2Qty) + Convert.ToInt32(dt.Rows[i]["outwardqty"].ToString());
                                SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblFinalInspection] SET [InwardQty] = '" + dt.Rows[i]["inwardqty"].ToString() + "', [OutwardQty] = '" + totoutward.ToString() + "',UpdatedDate='" + UpdatedDate + "' WHERE SubOA='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                                cmdupdate.ExecuteNonQuery();

                                //Data Save for Report
                                SqlCommand cmdrpt = con.CreateCommand();
                                cmdrpt.CommandText += "INSERT INTO [dbo].[tblRptFinalInspection]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[SubOA])" +
                            "VALUES('" + dt.Rows[i]["OAnumber"].ToString() + "','" + dt.Rows[i]["customername"].ToString() + "'," +
                            "'" + dt.Rows[i]["size"].ToString() + "','" + dt.Rows[i]["totalinward"].ToString() + "'," +
                            "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','0'," +
                            "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','" + dt.Rows[i]["outwardqty"].ToString() + "'," +
                            "'" + dt.Rows[i]["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                            "'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','" + UpdatedDate + "','"+ dt.Rows[i]["SubOA"].ToString() + "'); ";
                                cmdrpt.ExecuteNonQuery();
                                Resetdata();
                            }
                            else
                            {
                                SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblFinalInspection] SET [OutwardQty] = '" + dt.Rows[i]["totalinward"].ToString() + "',[InwardQty]='0',[IsComplete]=1,UpdatedDate='" + UpdatedDate + "' WHERE SubOA='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                                cmdupdate.ExecuteNonQuery();

                                //Data Save for Report
                                SqlCommand cmdrpt = con.CreateCommand();
                                cmdrpt.CommandText += "INSERT INTO [dbo].[tblRptFinalInspection]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[SubOA])" +
                            "VALUES('" + dt.Rows[i]["OAnumber"].ToString() + "','" + dt.Rows[i]["customername"].ToString() + "'," +
                            "'" + dt.Rows[i]["size"].ToString() + "','" + dt.Rows[i]["totalinward"].ToString() + "'," +
                            "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','0'," +
                            "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','" + dt.Rows[i]["outwardqty"].ToString() + "'," +
                            "'" + dt.Rows[i]["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                            "'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','" + UpdatedDate + "','"+ dt.Rows[i]["SubOA"].ToString() + "'); ";
                                cmdrpt.ExecuteNonQuery();
                                Resetdata();
                            }
                            con.Close();
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Approved and send to Stock Department...!')", true);
                        Response.Redirect("FinalInspection.aspx");
                    }
                }
            }
            else { }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cancelled Successfully..!')", true);
        }
    }
    #endregion

    protected void ddlONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFinalInspectionData();
        txtcustomerName.Enabled = false;
    }

    protected void txtOANumber_TextChanged(object sender, EventArgs e)
    {
        GetFinalInspectionData();
        ddlONumber.Enabled = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Resetdata();
    }

    protected void Resetdata()
    {
        //txtcustomerName.Text = ""; ddlONumber.Text = "--Select--"; ddlONumber.Enabled = true;
        //dgvFinalInspection.DataSource = null;
        //dgvFinalInspection.DataBind();
        btnGetSelected.Visible = false;
        //txtcustomerName.Enabled = true;
        //Response.Redirect("LaserProgramming.aspx");
        GetFinalInspectionData();
    }

    //Checkbox All checked
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgvFinalInspection.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                int totalCount = dgvFinalInspection.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
                TextBox Inwardtb = (TextBox)row.Cells[1].FindControl("txtInwardQty");
                int InwardQty = Convert.ToInt32(Inwardtb.Text);
                if (chkRow.Checked == true)
                {
                    if (totalCount > 0)
                    {
                        if (InwardQty == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Less Quantity- You have sent Quantity to Stock department...!')", true);
                        }
                        else
                        {
                            btnGetSelected.Visible = true;
                        }
                    }
                    else
                    {
                        btnGetSelected.Visible = false;
                    }
                }
            }
        }
    }

    protected void checkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgvFinalInspection.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                int totalCount = dgvFinalInspection.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
                TextBox Inwardtb = (TextBox)row.Cells[1].FindControl("txtInwardQty");
                int InwardQty = Convert.ToInt32(Inwardtb.Text);
                if (totalCount > 0)
                {
                    if (InwardQty == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Less Quantity- You have sent Quantity to Stock department...!')", true);
                    }
                    else
                    {
                        btnGetSelected.Visible = true;
                    }
                }
                else
                {
                    btnGetSelected.Visible = false;
                }
            }
        }
    }

    private void calculationA(GridViewRow row)
    {
        TextBox txt_Inward = (TextBox)row.FindControl("txtInwardQty");
        TextBox txt_Outward = (TextBox)row.FindControl("txtOutwardQty");
        txt_Inward.Text = (Convert.ToDecimal(txt_Inward.Text.Trim()) - Convert.ToDecimal(txt_Outward.Text.Trim())).ToString();
    }

    protected void txtOutwardQty_TextChanged(object sender, EventArgs e)
    {
        ViewState["Iscomplete"] = "1";
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
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

    protected void dgvFinalInspection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Id = dgvFinalInspection.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
            gvDetails.DataSource = GetData(string.Format("select * from tblFinalAssembly where SubOA='{0}'", Id));
            gvDetails.DataBind();
        }
    }
    private static DataTable GetData(string query)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
	protected void btnPrintData_Click(object sender, EventArgs e)
    {
        try
        {
            //string URL = "PDFShow.aspx?Name=Drawing";
            //string modified_URL = "window.open('" + URL + "', '_blank');";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", modified_URL, true);
			 Response.Redirect("PDFShow.aspx?Name=Final Inspection");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}