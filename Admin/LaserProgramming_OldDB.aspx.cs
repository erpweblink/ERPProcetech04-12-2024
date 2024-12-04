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


public partial class Admin_LaserProgramming_OldDB : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string InwardQty = "";
    string LaserID = "";
    DataTable dtdata = new DataTable();
    DataTable tempdt = new DataTable();

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
                //LaserProgrammDDLbind();
                GetLaserProgamData();
            }
        }
    }

    private void LaserProgrammDDLbind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select LaserProgId, OANumber as OANumber from tblLaserProgramingOld where IsApprove=1 and IsComplete is null order by LaserProgId desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlONumber.DataSource = dt;
            ddlONumber.DataTextField = "OANumber";
            ddlONumber.DataValueField = "LaserProgId";
            ddlONumber.DataBind();
            ddlONumber.Items.Insert(0, "All");
            ddlONumber.Items.Insert(0, "--Select--");
        }
    }

    protected void GetLaserProgamData()
    {
        try
        {
            string query = string.Empty;

            query = @"SELECT [LaserProgId],[OANumber],[SubOA],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],
                [DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate] 
                FROM tblLaserProgramingOld
                where IsApprove=1 and IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";


            SqlDataAdapter ad = new SqlDataAdapter(query, con);

            ad.Fill(dtdata);
            if (dtdata.Rows.Count > 0)
            {
                dgvLaserprogram.DataSource = dtdata;
                dgvLaserprogram.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvLaserprogram.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
            }
            else
            {
                dgvLaserprogram.DataSource = null;
                dgvLaserprogram.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvLaserprogram.ClientID + "', 900, 1020 , 40 ,true); </script>", false);

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
            dt.Columns.AddRange(new DataColumn[11]
            { new DataColumn("OAnumber"),
            new DataColumn("SubOA"),
                new DataColumn("customername"),
                new DataColumn("size"),
                new DataColumn("totalinward"),
                new DataColumn("inwarddatetime"),
                new DataColumn("inwardqty"),
                new DataColumn("outwarddatetime"),
                new DataColumn("outwardqty"),
                new DataColumn("deliverydate"),
                new DataColumn("Isapprove") });

            tempdt.Columns.AddRange(new DataColumn[11] { new DataColumn("OAnumber"),
                                new DataColumn("SubOA"),
                                new DataColumn("customername"),
                                new DataColumn("size"),
                                new DataColumn("totalinward"),
                                new DataColumn("inwarddatetime"),
                                new DataColumn("inwardqty"),
                                new DataColumn("outwarddatetime"),
                                new DataColumn("outwardqty"),
                                new DataColumn("deliverydate"),
                                new DataColumn("Isapprove") });
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
                            //string time = DateTime.Now.ToString();
                            string OutwardDtTime = DateTime.Now.ToString();

                            TextBox Outwardtb = (TextBox)row.Cells[1].FindControl("txtOutwardQty");
                            string[] strarr = Outwardtb.Text.Split(',');

                            string OutwardQty = strarr[1].ToString();



                            TextBox Sizetb = (TextBox)row.Cells[1].FindControl("lblSize");
                            string Size = Sizetb.Text;

                            dt.Rows.Add(OANumber, SubOA, CustName, Size, TotalQty, InwardDtTime, InwardQty, OutwardDtTime, OutwardQty, DeliveryDt);
                        }
                    }
                }

            }
            using (con)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    bool IsApprove = true, IsPending = false, IsCancel = false, Iscomplete;
                    string CreatedBy = Session["name"].ToString(), UpdatedBy = "";
                    string UpdatedDate = DateTime.Now.ToShortDateString(), CreatedDate = DateTime.Now.ToShortDateString();

                    //DateTime UpdatedDate = DateTime.Parse(UDate);
                    //DateTime CreatedDate = DateTime.Parse(CDate);
                    bool Insertdata = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        con.Open();
                        if (row["inwardqty"].ToString() == row["outwardqty"].ToString())
                        {
                            Iscomplete = true;
                        }
                        else
                        {
                            Iscomplete = false;
                        }

                        SqlCommand cmdexsist = new SqlCommand("select OANumber,InwardQty,OutwardQty,SubOA from tblLaserCuttingOld WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
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

                        SqlCommand cmd2 = new SqlCommand("select OutwardQty from tblLaserProgramingOld WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
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

                            tempdt.Rows.Add(row["OAnumber"].ToString(),
                                row["SubOA"].ToString(),
                                row["customername"].ToString(),
                                row["size"].ToString(),
                                row["totalinward"].ToString(),
                                DateTime.Now,
                                row["outwardqty"].ToString(),
                                DateTime.Now,
                                row["outwardqty"].ToString(),
                                row["deliverydate"].ToString(),
                                 true);

                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name
                                sqlBulkCopy.DestinationTableName = "dbo.tblLaserCuttingOld";
                                sqlBulkCopy.ColumnMappings.Add("OAnumber", "OANumber");
                                sqlBulkCopy.ColumnMappings.Add("SubOA", "SubOA");
                                sqlBulkCopy.ColumnMappings.Add("customername", "CustomerName");
                                sqlBulkCopy.ColumnMappings.Add("size", "Size");
                                sqlBulkCopy.ColumnMappings.Add("totalinward", "TotalQty");
                                sqlBulkCopy.ColumnMappings.Add("inwarddatetime", "InwardDtTime");
                                sqlBulkCopy.ColumnMappings.Add("inwardqty", "InwardQty");
                                sqlBulkCopy.ColumnMappings.Add("outwarddatetime", "OutwardDtTime");
                                sqlBulkCopy.ColumnMappings.Add("outwardqty", "OutwardQty");
                                sqlBulkCopy.ColumnMappings.Add("deliverydate", "DeliveryDate");
                                sqlBulkCopy.ColumnMappings.Add("Isapprove", "IsApprove");
                                sqlBulkCopy.WriteToServer(tempdt);

                                tempdt.Clear();
                            }



                            //    cmd.CommandText += "INSERT INTO [dbo].[tblLaserCuttingOld]([OANumber],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],[DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[SubOA])" +
                            //"VALUES('" + row["OAnumber"].ToString() + "','" + row["customername"].ToString() + "'," +
                            //"'" + row["size"].ToString() + "','" + row["totalinward"].ToString() + "'," +
                            //"'" + row["outwarddatetime"].ToString() + "','" + row["outwardqty"].ToString() + "'," +
                            //"'" + row["outwarddatetime"].ToString() + "','0'," +
                            //"'" + row["deliverydate"].ToString() + "','" + IsApprove + "','" + IsPending + "','" + IsCancel + "'," +
                            //"'" + CreatedBy + "','" + CreatedDate + "','" + UpdatedBy + "','','" + row["SubOA"].ToString() + "'); ";
                            //    cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            int totOutwardqnt = Convert.ToInt32(InwardQty) + Convert.ToInt32(row["outwardqty"].ToString());
                            SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserCuttingOld] SET [InwardQty] = '" + totOutwardqnt.ToString() + "',[IsComplete]=NULL,[InwardDtTime]='" + row["outwarddatetime"].ToString() + "' WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
                            cmdupdate.ExecuteNonQuery();
                        }

                        if (row["inwardqty"].ToString() == row["outwardqty"].ToString())
                        {
                            SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserProgramingOld] SET [OutwardQty] = '" + row["totalinward"].ToString() + "',[InwardQty]='0',[IsComplete]=1,UpdatedDate='" + UpdatedDate + "' WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
                            cmdupdate.ExecuteNonQuery();
                        }
                        else
                        {
                            int totoutward;
                            int inwardqy;
                            if (Outward2Qty == "")
                            {
                                Outward2Qty = "0";
                                inwardqy = Convert.ToInt32(row["inwardqty"].ToString()) - Convert.ToInt32(row["outwardqty"].ToString());
                                totoutward = Convert.ToInt32(Outward2Qty) + Convert.ToInt32(row["outwardqty"].ToString());
                            }
                            else
                            {
                                inwardqy = Convert.ToInt32(row["inwardqty"].ToString()) - Convert.ToInt32(row["outwardqty"].ToString());
                                totoutward = Convert.ToInt32(Outward2Qty) + Convert.ToInt32(row["outwardqty"].ToString());
                            }

                            SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserProgramingOld] SET [InwardQty] = '" + inwardqy.ToString() + "', [OutwardQty] = '" + totoutward.ToString() + "',UpdatedDate='" + UpdatedDate + "' WHERE SubOA='" + row["SubOA"].ToString() + "'", con);
                            cmdupdate.ExecuteNonQuery();
                        }
                        con.Close();
                    }
                    if (Insertdata == true)
                    {

                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Approved and send to Laser Cutting Department...!');window.location.href='LaserProgramming_OldDB.aspx';", true);
                    //Response.Redirect("LaserProgramming_OldDB.aspx");
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cancelled Successfully..!')", true);
            Response.Redirect("LaserProgramming_OldDB.aspx");
        }
    }

    protected void ddlONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLaserProgamData();
    }

    protected void txtOANumber_TextChanged(object sender, EventArgs e)
    {
        GetLaserProgamData();
    }

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
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvLaserprogram.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
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
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvLaserprogram.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
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
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvLaserprogram.ClientID + "', 400, 1020 , 40 ,true); </script>", false);
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

    protected void dgvLaserprogram_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "selectOAnumber")
        {
            string oaNumber = Convert.ToString(e.CommandArgument.ToString());
            ViewState["OANumber"] = oaNumber;
            if (oaNumber != "")
            {
                SqlCommand cmd = new SqlCommand("select OutwardQty from tblRptLaserPrograming WHERE SubOA='" + oaNumber + "'", con);
                string InwardQty = "";
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //txtReturnOutward.Text = dr["OutwardQty"].ToString();
                            //divReturn.Visible = true;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Outward Quantity Not Found..!')", true);
                    }
                }
                con.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OA number Does not exsist..!')", true);
            }
        }
    }

    protected void lnkbtnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtReturnInward.Text) > Convert.ToInt32(hdnInwardQty.Value))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Inward Qauntity Should be Smaller than or equal to Outward Quantity..!')", true);
                txtReturnInward.Focus();
            }
            else
            {
                con.Open();

                //Get Exsiting Record
                SqlCommand cmdselect = new SqlCommand("select InwardQty from tblDrawingOld WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                Object Inwardqty = cmdselect.ExecuteScalar();

                if (Convert.ToInt32(txtReturnInward.Text) == Convert.ToInt32(hdnInwardQty.Value))
                {
                    // If all record return
                    SqlCommand cmdDelete = new SqlCommand("Delete from tblLaserProgramingOld WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                    cmdDelete.ExecuteNonQuery();

                    int TotalReturnInward = Convert.ToInt32(Inwardqty.ToString()) + Convert.ToInt32(txtReturnInward.Text);
                    SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblDrawingOld] SET [InwardQty] = '" + TotalReturnInward + "',[IsComplete] = NULL WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                    cmdupdate1.ExecuteNonQuery();
                }
                else
                {


                    int TotalReturn_Outward = Convert.ToInt32(hdnInwardQty.Value) - Convert.ToInt32(txtReturnInward.Text);
                    int TotalReturnInward = Convert.ToInt32(Inwardqty.ToString()) + Convert.ToInt32(txtReturnInward.Text);

                    //Updated current stage
                    SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserProgramingOld] SET [InwardQty] = '" + TotalReturn_Outward + "',[OutwardQty] = '" + TotalReturn_Outward + "' WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                    cmdupdate.ExecuteNonQuery();

                    //Updated Prev stage 
                    SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblDrawingOld] SET [InwardQty] = '" + TotalReturnInward + "' ,[IsComplete] = NULL  WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                    cmdupdate1.ExecuteNonQuery();
                }
                con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Quantity has been Return Successfully..!');window.location.href='LaserProgramming_OldDB.aspx';", true);
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
            TextBox txtOutwardQty = e.Row.FindControl("txtOutwardQty") as TextBox;

            string empcode = Session["empcode"].ToString();
            DataTable Dt = new DataTable();
            SqlDataAdapter Sd = new SqlDataAdapter("Select id from [employees] where [empcode]='" + empcode + "'", con);
            Sd.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                string idd = Dt.Rows[0]["id"].ToString();
                DataTable Dtt = new DataTable();
                SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + idd + "' AND PageName = 'LaserProgramming_OldDB.aspx' AND PagesView = '1'", con);
                Sdd.Fill(Dtt);
                if (Dtt.Rows.Count > 0)
                {
                    dgvLaserprogram.Columns[1].Visible = false;
                    dgvLaserprogram.Columns[13].Visible = false;
                    txtOutwardQty.ReadOnly = true;
                    btnPrintData.Visible = false;
                }
            }

            string Id = dgvLaserprogram.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
            gvDetails.DataSource = GetData(string.Format("select * from vw_LaserProgrammingOld where LaserProgId='{0}'", Id));
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
			 Response.Redirect("PDFShow.aspx?Name=Laser Programming");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}