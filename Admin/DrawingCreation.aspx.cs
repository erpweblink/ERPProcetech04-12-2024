using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_DrawingCreation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string InwardQty = "";
    string LaserID = "";
    DataTable dtdata = new DataTable();
    CommonCls objClass = new CommonCls();
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
                GetLaserProgamData();
            }
        }
    }

    protected void GetLaserProgamData()
    {
        try
        {
            string query = string.Empty;

            query = @"SELECT [VWID],[id] as mainID, [OAno],[currentdate],[customername],[deliverydatereqbycust],[IsDrawingcomplete],[Description],[Qty],[Price],[Discount]
,[TotalAmount],[CGST],[SGST],[IGST],[SubOANumber] FROM vwOrderAccept where IsComplete is null order by deliverydatereqbycust asc";


            SqlDataAdapter ad = new SqlDataAdapter(query, con);

            ad.Fill(dtdata);
            if (dtdata.Rows.Count > 0)
            {
                dgvLaserprogram.DataSource = dtdata;
                dgvLaserprogram.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvLaserprogram.ClientID + "', 400, 1020 , 40 ,true); </script>", false);
            }
            else
            {
                dgvLaserprogram.DataSource = null;
                dgvLaserprogram.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Pending Record Not Found..!');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GetSelectedRecords(object sender, EventArgs e)
    {
        CheckBox chkRow;
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            DataTable dt = new DataTable();
            bool flag = false;
            dt.Columns.AddRange(new DataColumn[10]
            { new DataColumn("OAnumber"),
            new DataColumn("SubOA"),
                new DataColumn("customername"),
                new DataColumn("size"),
                new DataColumn("totalinward"),
                new DataColumn("inwarddatetime"),
                new DataColumn("inwardqty"),
                new DataColumn("outwarddatetime"),
                new DataColumn("outwardqty"),
                new DataColumn("deliverydate") });
            foreach (GridViewRow row in dgvLaserprogram.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                    int totalCount = dgvLaserprogram.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
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
                            LaserID = (row.Cells[1].FindControl("lblLaserID") as Label).Text;
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

                            //DateTime OutwardDtT = DateTime.Now;//DateTime.Parse(tbOutwardDt.Text);
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

                        //DateTime UpdatedDate = DateTime.Parse(UDate);
                        //DateTime CreatedDate = DateTime.Parse(CDate);

                        foreach (DataRow row in dt.Rows)
                        {
                            con.Open();
                            if (row["inwardqty"].ToString() == "0")
                            {
                                Iscomplete = true;
                            }
                            else
                            {
                                Iscomplete = false;
                            }

                            SqlCommand cmdexsist = new SqlCommand("select OANumber,InwardQty,OutwardQty,SubOA from tblLaserCutting WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
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

                            SqlCommand cmd2 = new SqlCommand("select OutwardQty from tblLaserPrograming WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
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
                                cmd.CommandText += "INSERT INTO [dbo].[tblLaserCutting]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[SubOA])" +
                            "VALUES('" + row["OAnumber"].ToString() + "','" + row["customername"].ToString() + "'," +
                            "'" + row["size"].ToString() + "','" + row["totalinward"].ToString() + "'," +
                            "'" + row["outwarddatetime"].ToString() + "','" + row["outwardqty"].ToString() + "'," +
                            "'" + row["outwarddatetime"].ToString() + "','0'," +
                            "'" + row["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                            "'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','','" + row["SubOA"].ToString() + "'); ";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                int totOutwardqnt = Convert.ToInt32(InwardQty) + Convert.ToInt32(row["outwardqty"].ToString());
                                SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserCutting] SET [InwardQty] = '" + totOutwardqnt.ToString() + "',[InwardDtTime]='" + row["outwarddatetime"].ToString() + "' WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
                                cmdupdate.ExecuteNonQuery();
                            }

                            if (ViewState["Iscomplete"] != null)
                            {
                                int totoutward = Convert.ToInt32(Outward2Qty) + Convert.ToInt32(row["outwardqty"].ToString());
                                SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserPrograming] SET [InwardQty] = '" + row["inwardqty"].ToString() + "', [OutwardQty] = '" + totoutward.ToString() + "',UpdatedDate='" + UpdatedDate + "' WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
                                cmdupdate.ExecuteNonQuery();

                                //Data Save for Report
                                SqlCommand cmdrpt = con.CreateCommand();
                                cmdrpt.CommandText += "INSERT INTO [dbo].[tblRptLaserPrograming]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[SubOA])" +
                            "VALUES('" + row["OAnumber"].ToString() + "','" + row["customername"].ToString() + "'," +
                            "'" + row["size"].ToString() + "','" + row["totalinward"].ToString() + "'," +
                            "'" + row["outwarddatetime"].ToString() + "','0'," +
                            "'" + row["outwarddatetime"].ToString() + "','" + row["outwardqty"].ToString() + "'," +
                            "'" + row["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                            "'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','" + UpdatedDate + "','" + row["SubOA"].ToString() + "'); ";
                                cmdrpt.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserPrograming] SET [OutwardQty] = '" + row["totalinward"].ToString() + "',[InwardQty]='0',[IsComplete]=1,UpdatedDate='" + UpdatedDate + "' WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
                                cmdupdate.ExecuteNonQuery();

                                //Data Save for Report
                                SqlCommand cmdrpt = con.CreateCommand();
                                cmdrpt.CommandText += "INSERT INTO [dbo].[tblRptLaserPrograming]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[SubOA])" +
                            "VALUES('" + row["OAnumber"].ToString() + "','" + row["customername"].ToString() + "'," +
                            "'" + row["size"].ToString() + "','" + row["totalinward"].ToString() + "'," +
                            "'" + row["outwarddatetime"].ToString() + "','0'," +
                            "'" + row["outwarddatetime"].ToString() + "','" + row["outwardqty"].ToString() + "'," +
                            "'" + row["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                            "'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','" + UpdatedDate + "','" + row["SubOA"].ToString() + "'); ";
                                cmdrpt.ExecuteNonQuery();
                            }
                            con.Close();
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Approved and send to Laser Cutting Department...!')", true);
                        Response.Redirect("LaserProgramming.aspx");
                    }
                }
            }
            else
            {
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cancelled Successfully..!')", true);
            Response.Redirect("LaserProgramming.aspx");
        }
    }

    protected void ddlONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLaserProgamData();
    }

    //protected void txtOANumber_TextChanged(object sender, EventArgs e)
    //{
    //    GetLaserProgamData();
    //}

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Resetdata();
    }

    protected void Resetdata()
    {
        //txtcustomerName.Text = ""; ddlONumber.Text = "--Select--"; ddlONumber.Enabled = true;
        //dgvLaserprogram.DataSource = null;
        //dgvLaserprogram.DataBind();
        GetLaserProgamData();
        btnGetSelected.Visible = false;
    }

    //Checkbox All checked
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgvLaserprogram.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                int totalCount = dgvLaserprogram.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
                TextBox Inwardtb = (TextBox)row.Cells[1].FindControl("txtInwardQty");
                int InwardQty = Convert.ToInt32(Inwardtb.Text);
                if (chkRow.Checked == true)
                {
                    if (totalCount > 0)
                    {
                        if (InwardQty == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Less Quantity- You have sent Quantity to Laser Cutting department...!')", true);
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
                else
                {
                    //btnGetSelected.Visible = false;
                }
            }
        }
    }

    protected void checkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgvLaserprogram.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                int totalCount = dgvLaserprogram.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
                TextBox Inwardtb = (TextBox)row.Cells[1].FindControl("txtInwardQty");
                int InwardQty = Convert.ToInt32(Inwardtb.Text);
                if (totalCount > 0)
                {
                    if (InwardQty == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Less Quantity- You have sent Quantity to Laser Cutting department...!')", true);
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

    public string Before(string value, string a)
    {
        int posA = value.IndexOf(a);
        if (posA == -1)
        {
            return "";
        }
        return value.Substring(0, posA);
    }

    private void Gvbind(string OAid)
    {
        string query = string.Empty;
        if (OAid != "")
        {
            query = @"SELECT [DocID],[OAid],[File1],[File2],[File3],[File4],[File5],[CeatedDate],[CreatedBy]FROM tblOAFiledata where OAid='" + OAid + "'";
        }
        else
        {
            lblnodatafoundComp.Text = "OA Id not found...!! ";
        }
        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvOAFileData.DataSource = dt;
            GvOAFileData.DataBind();
            lblnodatafoundComp.Visible = false;
            btnClose.Visible = true;
        }
        else
        {
            GvOAFileData.DataSource = null;
            GvOAFileData.DataBind();
            lblnodatafoundComp.Text = "Attachment Not Found...!! ";
            lblnodatafoundComp.Visible = true;
            lblnodatafoundComp.ForeColor = Color.Red;
            btnClose.Visible = false;
        }
    }

    protected void lnkbtnReturn_Click(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToInt32(txtReturnInward.Text) > Convert.ToInt32(txtReturnOutward.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Inward Qauntity Should be Smaller than or equal to Outward Quantity..!')", true);
                txtReturnInward.Focus();
            }
            else
            {
                con.Open();
                SqlCommand cmdInwardQty = new SqlCommand("select InwardQty from tblLaserPrograming WHERE SubOA='" + ViewState["OANumber"].ToString() + "'", con);
                string Inward_Qty = cmdInwardQty.ExecuteScalar().ToString();

                SqlCommand cmdOutwardQty1 = new SqlCommand("select OutwardQty from tblLaserPrograming WHERE SubOA='" + ViewState["OANumber"].ToString() + "'", con);
                string OutwardQty1 = cmdOutwardQty1.ExecuteScalar().ToString();

                SqlCommand cmd1InwardQty = new SqlCommand("select InwardQty from tblLaserCutting WHERE SubOA='" + ViewState["OANumber"].ToString() + "'", con);
                string Inward_Qty1 = cmd1InwardQty.ExecuteScalar().ToString();

                SqlCommand cmdOutwardQty = new SqlCommand("select top 1 OutwardQty from tblRptLaserPrograming WHERE SubOA='" + ViewState["OANumber"].ToString() + "' and LaserProgId=(select max(LaserProgId) from tblRptLaserPrograming where SubOA='" + ViewState["OANumber"].ToString() + "')", con);
                string Outward_Qty = cmdOutwardQty.ExecuteScalar().ToString();

                //Update tblLaserPrograming
                int TotalReturnInward = Convert.ToInt32(Inward_Qty) + Convert.ToInt32(txtReturnInward.Text);
                int TotalReturn_Outward1 = Convert.ToInt32(OutwardQty1) - Convert.ToInt32(txtReturnInward.Text);
                SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserPrograming] SET [InwardQty] = '" + TotalReturnInward + "',[OutwardQty] = '" + TotalReturn_Outward1 + "' WHERE SubOA='" + ViewState["OANumber"].ToString() + "'", con);
                cmdupdate.ExecuteNonQuery();

                //Update tblLaserCutting
                int TotalReturnOutward = Convert.ToInt32(Inward_Qty1) - Convert.ToInt32(txtReturnInward.Text);
                SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblLaserCutting] SET [InwardQty] = '" + TotalReturnOutward + "' WHERE SubOA='" + ViewState["OANumber"].ToString() + "'", con);
                cmdupdate1.ExecuteNonQuery();

                //Update tblRptLaserPrograming
                int TotalReturn_Outward = Convert.ToInt32(Outward_Qty) - Convert.ToInt32(txtReturnInward.Text);
                SqlCommand cmdupdate2 = new SqlCommand("UPDATE [dbo].[tblRptLaserPrograming] SET [OutwardQty] = '" + TotalReturn_Outward + "' WHERE LaserProgId=(select max(LaserProgId) from tblRptLaserPrograming where SubOA='" + ViewState["OANumber"].ToString() + "')", con);
                cmdupdate2.ExecuteNonQuery();
                con.Close();
                divReturn.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Outward Quantity has been Return Successfully..!')", true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void DisplayPop(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = dgvLaserprogram.Rows[rowIndex];

        lblCustNm.Text = (row.FindControl("lblCustName") as Label).Text;
        lblTotalQnty.Text = (row.FindControl("lblTotalQty") as Label).Text;
        lblOAno.Text = (row.FindControl("lblOANumber") as Label).Text;
        lblDescriptionSize.Text = (row.FindControl("lblSize") as TextBox).Text; ;
        lblDeliveryDate.Text = (row.FindControl("lblDeliveryDt") as Label).Text;
        lblInwardDtandTime.Text = (row.FindControl("lblInwardDtTime") as Label).Text;

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal();", true);
    }

    protected void dgvLaserprogram_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Id = dgvLaserprogram.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
            gvDetails.DataSource = GetData(string.Format("select * from vwOrderAccept where VWID='{0}'", Id));
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        GvOAFileData.DataSource = null;
        GvOAFileData.DataBind();
        btnClose.Visible = false;
        GetLaserProgamData();
    }

    public void Display(string id, string fileNo)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string CmdText = string.Empty;
                if (fileNo == "1")
                {
                    CmdText = "SELECT '../'+[File1] as Path FROM tblOAFiledata where OAid='" + id + "'";
                }
                if (fileNo == "2")
                {
                    CmdText = "SELECT '../'+[File2] as Path FROM tblOAFiledata where OAid='" + id + "'";
                }
                if (fileNo == "3")
                {
                    CmdText = "SELECT '../'+[File3] as Path FROM tblOAFiledata where OAid='" + id + "'";
                }
                if (fileNo == "4")
                {
                    CmdText = "SELECT '../'+[File4] as Path FROM tblOAFiledata where OAid='" + id + "'";
                }
                if (fileNo == "5")
                {
                    CmdText = "SELECT '../'+[File5] as Path FROM tblOAFiledata where OAid='" + id + "'";
                }

                SqlDataAdapter ad = new SqlDataAdapter(CmdText, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Response.Write(dt.Rows[0]["Path"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Path"].ToString()))
                    {
                        string Patha = dt.Rows[0]["Path"].ToString();

                        string[] msgfilename = Patha.Split('.');
                        string filenameExt = msgfilename[3];
                        if (filenameExt == "msg")
                        {
                        }
                        else
                        {
                            //redirect to New Tab
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + Patha + "','_blank')", true);
                            //Response.Redirect(Patha);
                        }
                    }
                    else
                    {
                        lblnodatafoundComp.Text = "File Not Found or Not Available !!";
                    }
                }
                else
                {
                    lblnodatafoundComp.Text = "File Not Found or Not Available !!";
                }
            }
        }
    }

    protected void ImageButtonfile1_Click(object sender, ImageClickEventArgs e)
    {
        string id = ((sender as ImageButton).CommandArgument).ToString();
        Display(id, "1");
    }

    protected void ImageButtonfile2_Click(object sender, ImageClickEventArgs e)
    {
        string id = ((sender as ImageButton).CommandArgument).ToString();
        Display(id, "2");
    }

    protected void ImageButtonfile3_Click(object sender, ImageClickEventArgs e)
    {
        string id = ((sender as ImageButton).CommandArgument).ToString();
        Display(id, "3");
    }

    protected void ImageButtonfile4_Click(object sender, ImageClickEventArgs e)
    {
        string id = ((sender as ImageButton).CommandArgument).ToString();
        Display(id, "4");
    }

    protected void ImageButtonfile5_Click(object sender, ImageClickEventArgs e)
    {
        string id = ((sender as ImageButton).CommandArgument).ToString();
        Display(id, "5");
    }

    protected void GvOAFileData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
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
        }
    }

    protected void dgvLaserprogram_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        string OAid = e.CommandArgument.ToString();
        if (e.CommandName == "rowDisplay")
        {
            Gvbind(OAid);
            GetLaserProgamData();
        }
        else
        {
            string url = "../Reports/RptDrawing.aspx?OANumber=" + objClass.encrypt(OAid);
            Page.ClientScript.RegisterStartupScript(GetType(), "", "window.open('" + url + "','','width=700px,height=600px');", true);
        }

        if (e.CommandName == "rowSend")
        {
            bool Iscomplete;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (ViewState["Iscomplete"] != null)
                {
                    Iscomplete = false;
                }
                else
                {
                    Iscomplete = true;
                }

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("OAnumber"), new DataColumn("SubOA"), new DataColumn("customername"), new DataColumn("size"), new DataColumn("totalinward"), new DataColumn("inwarddatetime"), new DataColumn("inwardqty"), new DataColumn("outwarddatetime"), new DataColumn("outwardqty"), new DataColumn("deliverydate"), new DataColumn("remark") });

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                string OANumber = (row.FindControl("lblOANumber") as Label).Text;
                string SubOA = (row.Cells[1].FindControl("lblSubOANumber") as Label).Text;
                string CustName = (row.Cells[1].FindControl("lblCustName") as TextBox).Text;
                string TotalQty = (row.Cells[1].FindControl("lblTotalQty") as Label).Text;
                string DeliveryDt = (row.Cells[1].FindControl("lblDeliveryDt") as Label).Text;
                string InwardDtTime = (row.Cells[1].FindControl("lblInwardDtTime") as Label).Text;

                TextBox tb = (TextBox)row.Cells[1].FindControl("txtInwardQty");
                string InwardQty = tb.Text;

                TextBox tbOutwardDt = (TextBox)row.Cells[1].FindControl("txtOutwardDtTime");
                string time = DateTime.Now.ToString("h:mm tt");

                string OutwardDtTime = tbOutwardDt.Text + " " + time;

                TextBox Outwardtb = (TextBox)row.Cells[1].FindControl("txtOutwardQty");
                string OutwardQty = Outwardtb.Text;

                Label Sizetb = (Label)row.Cells[1].FindControl("txtSize");
                string Size = Sizetb.Text;

                TextBox Remarktb = (TextBox)row.Cells[1].FindControl("txtRemark");
                string Remark = Remarktb.Text;

                dt.Rows.Add(OANumber, SubOA, CustName, Size, TotalQty, InwardDtTime, InwardQty, OutwardDtTime, OutwardQty, DeliveryDt, Remark);

                using (con)
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        bool IsApprove = true, IsPending = false, IsCancel = false, Isdrawing = true, IsComplete = true;
                        string CreatedBy = Session["name"].ToString(), UpdatedBy = "";
                        string UpdatedDate = DateTime.Now.ToShortDateString(), CreatedDate = DateTime.Now.ToShortDateString();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            con.Open();
                            SqlCommand cmdexsist = new SqlCommand("select OANumber,InwardQty,OutwardQty,SubOA from tblLaserPrograming WHERE SubOA='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                            string OanumberExsists = "", InwardQtyss = "", OutwardQtyss = "";
                            using (SqlDataReader dr = cmdexsist.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    OanumberExsists = dr["SubOA"].ToString();
                                    InwardQtyss = dr["InwardQty"].ToString();
                                    OutwardQtyss = dr["OutwardQty"].ToString();
                                }
                            }
                            con.Close();
                            if (OanumberExsists == "")
                            {
                                cmd.CommandText += "INSERT INTO [dbo].[tblDrawing]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[IsComplete],[Remark],[SubOA])" +
                            "VALUES('" + dt.Rows[i]["OAnumber"].ToString() + "','" + dt.Rows[i]["customername"].ToString().Replace("<br />", " ") + "'," +
                            "'" + dt.Rows[i]["size"].ToString() + "','" + dt.Rows[i]["totalinward"].ToString() + "'," +
                            "'" + dt.Rows[i]["inwarddatetime"].ToString() + "','" + dt.Rows[i]["inwardqty"].ToString() + "'," +
                            "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','" + dt.Rows[i]["outwardqty"].ToString() + "'," +
                            "'" + dt.Rows[i]["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                            "'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','" + UpdatedDate + "','" + IsComplete + "','" + dt.Rows[i]["remark"].ToString() + "','" + dt.Rows[i]["SubOA"].ToString() + "'); ";
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                SqlCommand cmd1 = con.CreateCommand();
                                cmd1.CommandText += "INSERT INTO [dbo].[tblLaserPrograming]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[SubOA])" +
                                "VALUES('" + dt.Rows[i]["OAnumber"].ToString() + "','" + dt.Rows[i]["customername"].ToString().Replace("<br />", " ") + "'," +
                                "'" + dt.Rows[i]["size"].ToString() + "','" + dt.Rows[i]["totalinward"].ToString() + "'," +
                                "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','" + dt.Rows[i]["outwardqty"].ToString() + "'," +
                                "'" + dt.Rows[i]["outwarddatetime"].ToString() + "','0'," +
                                "'" + dt.Rows[i]["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                                "'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','','" + dt.Rows[i]["SubOA"].ToString() + "'); ";
                                con.Open();
                                cmd1.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                int totOutwardqnt = Convert.ToInt32(InwardQtyss) + Convert.ToInt32(dt.Rows[i]["outwardqty"].ToString());
                                SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserPrograming] SET [InwardQty] = '" + totOutwardqnt.ToString() + "',[InwardDtTime]='GetDate()',IsComplete=NULL WHERE SubOA='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                                con.Open();
                                cmdupdate.ExecuteNonQuery();
                                con.Close();

                                SqlCommand cmdupdatea = new SqlCommand("UPDATE [dbo].[tblDrawing] SET [InwardQty] = '" + totOutwardqnt.ToString() + "',[InwardDtTime]='GetDate()' WHERE SubOA='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                                con.Open();
                                cmdupdatea.ExecuteNonQuery();
                                con.Close();
                            }

                            if (Iscomplete == false)
                            {
                                SqlCommand cmdupdate1 = new SqlCommand("UPDATE OrderAcceptDtls SET [Qty] ='" + InwardQty + "' WHERE SubOANumber='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                                con.Open();
                                cmdupdate1.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                if (InwardQtyss == "")
                                {
                                    InwardQtyss = "0";
                                }
                                int totOutwardqnt = Convert.ToInt32(InwardQtyss) + Convert.ToInt32(dt.Rows[i]["outwardqty"].ToString());
                                SqlCommand cmdupdate1 = new SqlCommand("UPDATE OrderAcceptDtls SET [Qty] ='" + totOutwardqnt + "', [IsComplete] ='" + Iscomplete + "' WHERE SubOANumber='" + dt.Rows[i]["SubOA"].ToString() + "'", con);
                                con.Open();
                                cmdupdate1.ExecuteNonQuery();
                                con.Close();
                            }

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Approved and send to Laser Programming Department...!')", true);
                        }

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Approved and send to Laser Programming Department...!')", true);
                        Response.Redirect("DrawingCreation.aspx");
                    }
                }
            }
        }
    }

    protected void txtOutwardQty_TextChanged1(object sender, EventArgs e)
    {
        ViewState["Iscomplete"] = "1";
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvLaserprogram.ClientID + "', 400, 1020 , 40 ,true); </script>", false);
    }
}