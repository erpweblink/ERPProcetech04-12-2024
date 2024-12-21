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


public partial class Admin_CNCBending : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string InwardQty = "";
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
                GetCNCBendingData();
                //GetLaserProgamData();
            }
        }
    }

    private void CNCBendingDDLbind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select CNCBendingId, OANumber as OANumber from tblCNCBending where IsApprove=1 and IsComplete is null order by CNCBendingId desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlONumber.DataSource = dt;
            ddlONumber.DataTextField = "OANumber";
            ddlONumber.DataValueField = "CNCBendingId";
            ddlONumber.DataBind();
            ddlONumber.Items.Insert(0, "All");
            ddlONumber.Items.Insert(0, "--Select--");
        }
    }

    protected void GetCNCBendingData()
    {
        try
        {
            string query = string.Empty;

            // New Filed Added JobNo  by Nikhil 10-12-2024
            query = @"SELECT [JobNo],[CNCBendingId],[OANumber],[SubOA],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],
                [DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],A.[UpdatedBy],[UpdatedDate] 
              ,CustomerCode  FROM tblCNCBending AS A                
			   LEFT JOIN Company AS C ON C.cname=A.customername
                where IsComplete is null order by CONVERT(DateTime, DeliveryDate,103) asc";

            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgvCNCBending.DataSource = dt;
                dgvCNCBending.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvCNCBending.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
            }
            else
            {
                dgvCNCBending.DataSource = null;
                dgvCNCBending.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvCNCBending.ClientID + "', 900, 1020 , 40 ,true); </script>", false);

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Pending Record Not Found..!');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel('Record Not Found..!')", true);
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
        if (Session["OneTimeFlag"] == null || Session["OneTimeFlag"].ToString() == "")
        {
            Session["OneTimeFlag"] = "Inserted";
            string vwID = "";
            //string confirmValue = Request.Form["confirm_value"];
            string confirmValue = "Yes";
            if (confirmValue == "Yes")
            {
                DataTable dt = new DataTable();
                bool flag = false;

                //Changes made to Add JObNo by Nikhil 10-12-2024
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("OAnumber"),
                new DataColumn("SubOA"), new DataColumn("customername"),
                new DataColumn("size"), new DataColumn("totalinward"),
                new DataColumn("inwarddatetime"), new DataColumn("inwardqty"),
                new DataColumn("outwarddatetime"), new DataColumn("outwardqty"),
                new DataColumn("deliverydate"),new DataColumn("JobNo")
            });

                tempdt.Columns.AddRange(new DataColumn[12] { new DataColumn("OAnumber"),
                                new DataColumn("SubOA"),
                                new DataColumn("customername"),
                                new DataColumn("size"),
                                new DataColumn("totalinward"),
                                new DataColumn("inwarddatetime"),
                                new DataColumn("inwardqty"),
                                 //Uncommented two fileds to fetch exact data by Nikhil  10-12-2024
                                new DataColumn("outwarddatetime"),
                                new DataColumn("outwardqty"),

                                new DataColumn("deliverydate"),
                                new DataColumn("JobNo"),
                                new DataColumn("Isapprove") });

                foreach (GridViewRow row in dgvCNCBending.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                        int totalCount = dgvCNCBending.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
                        if (totalCount <= 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select Atleast One Row..!!');", true);
                            flag = true;
                        }
                        else
                        {
                            // new code start

                            if (chkRow.Checked)
                            {

                                TextBox txtQty1 = (TextBox)row.Cells[1].FindControl("txtInwardQty"); // Assuming txtInwardQty is qty1
                                TextBox txtQty2 = (TextBox)row.Cells[1].FindControl("txtOutwardQty"); // Assuming txtOutwardQty is qty2

                                // Get the value from txtQty2 (Outward Quantity)
                                string outwardQtyText = txtQty2.Text;

                                // Check if there are comma-separated values
                                string[] qty2Values = outwardQtyText.Split(',');

                                // Get the last value in the array (which is the last number)
                                string lastQty2Value = qty2Values[qty2Values.Length - 1]; // Last value after splitting by comma

                                // Try parsing the quantities
                                int qty1 = Convert.ToInt32(txtQty1.Text); // Inward Quantity
                                int qty2 = Convert.ToInt32(lastQty2Value); // Last value of Outward Quantity

                                //  if (qty1 < qty2)  original
                                if (qty1 >= qty2)
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
                                        //DateTime OutwardDtT = DateTime.Parse(tbOutwardDt.Text);
                                        string time = DateTime.Now.ToString("h:mm tt");
                                        //string OutwardDtTime = tbOutwardDt.Text + " " + time;
                                        string OutwardDtTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                                        TextBox Outwardtb = (TextBox)row.Cells[1].FindControl("txtOutwardQty");
                                        string[] strarr = Outwardtb.Text.Split(',');

                                        string OutwardQty = strarr[1].ToString();

                                        TextBox Sizetb = (TextBox)row.Cells[1].FindControl("lblSize");
                                        string Size = Sizetb.Text;

                                        //dt.Rows.Add(OANumber, SubOA, CustName, Size, TotalQty, InwardDtTime, InwardQty, OutwardDtTime, OutwardQty, DeliveryDt);

                                        // Added by Nikhil 10-12-2024
                                        string JobNo = (row.Cells[1].FindControl("lblJobNo") as Label).Text;

                                        dt.Rows.Add(OANumber, SubOA, CustName, Size, TotalQty, InwardDtTime, InwardQty, OutwardDtTime, OutwardQty, DeliveryDt, JobNo);
                                    }


                                    using (con)
                                    {
                                        using (SqlCommand cmd = con.CreateCommand())
                                        {
                                            cmd.CommandType = CommandType.Text;
                                            bool IsApprove = true, IsPending = false, IsCancel = false, Iscomplete;
                                            string CreatedBy = Session["name"].ToString(), UpdatedBy = "";
                                            //string UpdatedDate = DateTime.Now.ToShortDateString(), 
                                            //    CreatedDate = DateTime.Now.ToShortDateString();

                                            foreach (DataRow roww in dt.Rows)
                                            {
                                                con.Open();
                                                if (roww["inwardqty"].ToString() == roww["outwardqty"].ToString())
                                                {
                                                    Iscomplete = true;
                                                }
                                                else
                                                {
                                                    Iscomplete = false;
                                                }

                                                SqlCommand cmdexsist = new SqlCommand("select OANumber,InwardQty,OutwardQty,SubOA from tblWelding WHERE SubOA='" + roww["SubOA"].ToString() + "'", con);
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


                                                SqlCommand cmd2 = new SqlCommand("select OutwardQty from tblCNCBending WHERE SubOA='" + roww["SubOA"].ToString() + "'", con);
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
                                                    tempdt.Rows.Add(roww["OAnumber"].ToString(),
                                                        roww["SubOA"].ToString(),
                                                        roww["customername"].ToString(),
                                                        roww["size"].ToString(),
                                                        roww["totalinward"].ToString(),
                                                        //Below Two fileds added by Nikhil  and JobNo as well 
                                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                        roww["InwardQty"].ToString(),

                                                        DateTime.Now,
                                                        roww["outwardqty"].ToString(),
                                                        //DateTime.Now,
                                                        //row["outwardqty"].ToString(),
                                                        roww["deliverydate"].ToString(),
                                                        roww["JobNo"].ToString(),
                                                         true);

                                                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                                                    {
                                                        //Set the database table name
                                                        sqlBulkCopy.DestinationTableName = "dbo.tblWelding";
                                                        sqlBulkCopy.ColumnMappings.Add("OAnumber", "OANumber");
                                                        sqlBulkCopy.ColumnMappings.Add("SubOA", "SubOA");
                                                        sqlBulkCopy.ColumnMappings.Add("customername", "CustomerName");
                                                        sqlBulkCopy.ColumnMappings.Add("size", "Size");
                                                        sqlBulkCopy.ColumnMappings.Add("totalinward", "TotalQty");
                                                        sqlBulkCopy.ColumnMappings.Add("inwarddatetime", "InwardDtTime");
                                                        sqlBulkCopy.ColumnMappings.Add("outwardqty", "InwardQty");
                                                        //sqlBulkCopy.ColumnMappings.Add("inwardqty", "InwardQty");
                                                        //sqlBulkCopy.ColumnMappings.Add("outwarddatetime", "OutwardDtTime");
                                                        //sqlBulkCopy.ColumnMappings.Add("outwardqty", "OutwardQty");
                                                        sqlBulkCopy.ColumnMappings.Add("deliverydate", "DeliveryDate");
                                                        sqlBulkCopy.ColumnMappings.Add("JobNo", "JobNo");
                                                        sqlBulkCopy.ColumnMappings.Add("Isapprove", "IsApprove");
                                                        sqlBulkCopy.WriteToServer(tempdt);

                                                        tempdt.Clear();
                                                    }
                                                }
                                                else
                                                {
                                                    int totOutwardqnt = Convert.ToInt32(InwardQty) + Convert.ToInt32(roww["outwardqty"].ToString());
                                                    SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblWelding] SET [InwardQty] = '" + totOutwardqnt.ToString() + "',[IsComplete]=NULL,[InwardDtTime]='" + roww["outwarddatetime"].ToString() + "' WHERE SubOA='" + roww["SubOA"].ToString() + "'", con);
                                                    cmdupdate.ExecuteNonQuery();
                                                }

                                                if (roww["inwardqty"].ToString() == roww["outwardqty"].ToString())
                                                {
                                                    string OutwardDtTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                                                    SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblCNCBending] SET [OutwardQty] = '" + roww["totalinward"].ToString() + "',[InwardQty]='0',[IsComplete]=1,OutwardDtTime= '" + OutwardDtTime.ToString() + "',UpdatedDate='" + OutwardDtTime + "' WHERE SubOA='" + roww["SubOA"].ToString() + "'", con);
                                                    cmdupdate.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    string OutwardDtTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                                                    int totoutward;
                                                    int inwardqy;
                                                    if (Outward2Qty == "")
                                                    {
                                                        Outward2Qty = "0";
                                                        inwardqy = Convert.ToInt32(roww["inwardqty"].ToString()) - Convert.ToInt32(roww["outwardqty"].ToString());
                                                        totoutward = Convert.ToInt32(Outward2Qty) + Convert.ToInt32(roww["outwardqty"].ToString());
                                                    }
                                                    else
                                                    {
                                                        //Added by Nikhil  to get new outward qty every time 13-12-2024
                                                        Outward2Qty = "0";

                                                        inwardqy = Convert.ToInt32(roww["inwardqty"].ToString()) - Convert.ToInt32(roww["outwardqty"].ToString());
                                                        totoutward = Convert.ToInt32(Outward2Qty) + Convert.ToInt32(roww["outwardqty"].ToString());
                                                    }

                                                    SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblCNCBending] SET [InwardQty] = '" + inwardqy.ToString() + "', [OutwardQty] = '" + totoutward.ToString() + "',OutwardDtTime= '" + OutwardDtTime.ToString() + "',UpdatedDate='" + OutwardDtTime + "' WHERE SubOA='" + roww["SubOA"].ToString() + "'", con);
                                                    cmdupdate.ExecuteNonQuery();
                                                }
                                                con.Close();
                                            }
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Approved and send to Welding Department...!');window.location.href='CNCBending.aspx';", true);
                                            //Response.Redirect("CNCBending.aspx");
                                        }
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Outward Quantity Should be Smaller than or equal to Inward Quantity..!'); location.reload();", true);

                                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Outward Quantity Should be Smaller than or equal to Inward Quantity..!')", true);
                                    txtQty2.Focus(); // Focus on txtQty2

                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cancelled Successfully..!')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully- Approved and send to Welding Department...!');window.location.href='CNCBending.aspx';", true);


        }
    }

    #endregion


    protected void ddlONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCNCBendingData();
        txtcustomerName.Enabled = false;
    }

    protected void txtOANumber_TextChanged(object sender, EventArgs e)
    {
        GetCNCBendingData();
        ddlONumber.Enabled = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Resetdata();
    }

    protected void Resetdata()
    {
        //txtcustomerName.Text = ""; ddlONumber.Text = "--Select--"; ddlONumber.Enabled = true;
        //dgvCNCBending.DataSource = null;
        //dgvCNCBending.DataBind();
        btnGetSelected.Visible = false;
        // txtcustomerName.Enabled = true;
        //Response.Redirect("LaserProgramming.aspx");
        GetCNCBendingData();
    }

    //Checkbox All checked
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgvCNCBending.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                int totalCount = dgvCNCBending.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
                TextBox Inwardtb = (TextBox)row.Cells[1].FindControl("txtInwardQty");
                int InwardQty = Convert.ToInt32(Inwardtb.Text);
                if (chkRow.Checked == true)
                {
                    if (totalCount > 0)
                    {
                        if (InwardQty == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Less Quantity- You have sent Quantity to Welding department...!')", true);
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
        foreach (GridViewRow row in dgvCNCBending.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                int totalCount = dgvCNCBending.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkRow")).Checked);
                TextBox Inwardtb = (TextBox)row.Cells[1].FindControl("txtInwardQty");
                int InwardQty = Convert.ToInt32(Inwardtb.Text);
                if (totalCount > 0)
                {
                    if (InwardQty == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Less Quantity- You have sent Quantity to Welding department...!')", true);
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

    public string Between(string STR, string FirstString, string LastString)
    {
        string FinalString;
        int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
        int Pos2 = STR.IndexOf(LastString);
        FinalString = STR.Substring(Pos1, Pos2 - Pos1);
        return FinalString;
    }

    protected void lnkbtnReturn_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (Convert.ToInt32(txtReturnInward.Text) > Convert.ToInt32(hdnInwardQty.Value))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Inward Qauntity Should be Smaller than or equal to Outward Quantity..!')", true);
        //        txtReturnInward.Focus();
        //    }
        //    else
        //    {
        //        con.Open();

        //        //Get Exsiting Record
        //        //SqlCommand cmdselect = new SqlCommand("select InwardQty from tblLaserCutting WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        //        //Object Inwardqty = cmdselect.ExecuteScalar();

        //        string Tbale = ddlstages.SelectedValue;
        //        SqlCommand cmdselect = new SqlCommand("select InwardQty from  " + Tbale + "  WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        //        Object Inwardqty = cmdselect.ExecuteScalar();


        //        if (Convert.ToInt32(txtReturnInward.Text) == Convert.ToInt32(hdnInwardQty.Value))
        //        {
        //            // If all record return
        //            //SqlCommand cmdDelete = new SqlCommand("Delete from [tblCNCBending] WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        //            //cmdDelete.ExecuteNonQuery();

        //            int TotalReturnInward = Convert.ToInt32(Inwardqty.ToString()) + Convert.ToInt32(txtReturnInward.Text);
        //            //SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblLaserCutting] SET [InwardQty] = '" + TotalReturnInward + "',[IsComplete] = NULL WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        //            //cmdupdate1.ExecuteNonQuery();
        //            SetFullReturnquntity(TotalReturnInward);

        //        }
        //        else
        //        {


        //            int TotalReturn_Outward = Convert.ToInt32(hdnInwardQty.Value) - Convert.ToInt32(txtReturnInward.Text);
        //            int TotalReturnInward = Convert.ToInt32(Inwardqty.ToString()) + Convert.ToInt32(txtReturnInward.Text);

        //            ////Updated current stage
        //            //SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblCNCBending] SET [InwardQty] = '" + TotalReturn_Outward + "',[OutwardQty] = '" + TotalReturn_Outward + "' WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        //            //cmdupdate.ExecuteNonQuery();

        //            ////Updated Prev stage 
        //            //SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblLaserCutting] SET [InwardQty] = '" + TotalReturnInward + "' ,[IsComplete] = NULL  WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        //            //cmdupdate1.ExecuteNonQuery();

        //            Setreturnquantity(TotalReturn_Outward, TotalReturnInward);

        //        }
        //        con.Close();
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Quantity has been Return Successfully..!');window.location.href='CNCBending.aspx';", true);
        //    }
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        if (Session["OneTimeFlag"] == null || Session["OneTimeFlag"].ToString() == "")
        {

            if (ddlstages.SelectedValue != "0")
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
                        //SqlCommand cmdselect = new SqlCommand("select InwardQty from tblLaserPrograming WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                        //Object Inwardqty = cmdselect.ExecuteScalar();

                        string Tbale = ddlstages.SelectedValue;
                        SqlCommand cmdselect = new SqlCommand("select InwardQty from  " + Tbale + "  WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                        Object Inwardqty = cmdselect.ExecuteScalar();


                        if (Convert.ToInt32(txtReturnInward.Text) == Convert.ToInt32(hdnInwardQty.Value))
                        {
                            if (Inwardqty != null)
                            {
                                //Inwardqty = 0;
                                // If all record return
                                //SqlCommand cmdDelete = new SqlCommand("Delete from tblLaserCutting WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                                //cmdDelete.ExecuteNonQuery();

                                int TotalReturnInward = Convert.ToInt32(Inwardqty.ToString()) + Convert.ToInt32(txtReturnInward.Text);
                                //SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblLaserPrograming] SET [InwardQty] = '" + TotalReturnInward + "',[IsComplete] = NULL WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                                //cmdupdate1.ExecuteNonQuery();

                                SetFullReturnquntity(TotalReturnInward);
                            }
                            else
                            {
                                //Uncommented the InwardQty = 0 and changed the method name from [SetFullReturnquntity] to new one below
                                //By Nikhil if there is No data presnet in returing table 11-12-2024

                                Inwardqty = 0;
                                // If all record return
                                //SqlCommand cmdDelete = new SqlCommand("Delete from tblLaserCutting WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                                //cmdDelete.ExecuteNonQuery();

                                int TotalReturnInward = Convert.ToInt32(Inwardqty.ToString()) + Convert.ToInt32(txtReturnInward.Text);
                                //SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblLaserPrograming] SET [InwardQty] = '" + TotalReturnInward + "',[IsComplete] = NULL WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                                //cmdupdate1.ExecuteNonQuery();

                                InsertFullReturnQuantity(TotalReturnInward);
                            }
                        }

                        else
                        {

                            if (Inwardqty != null)
                            {
                                int TotalReturn_Outward = Convert.ToInt32(hdnInwardQty.Value) - Convert.ToInt32(txtReturnInward.Text);
                                int TotalReturnInward = Convert.ToInt32(Inwardqty.ToString()) + Convert.ToInt32(txtReturnInward.Text);

                                ////Updated current stage
                                //SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserCutting] SET [InwardQty] = '" + TotalReturn_Outward + "',[OutwardQty] = '" + TotalReturn_Outward + "' WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                                //cmdupdate.ExecuteNonQuery();

                                ////Updated Prev stage 
                                //SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblLaserPrograming] SET [InwardQty] = '" + TotalReturnInward + "' ,[IsComplete] = NULL  WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                                //cmdupdate1.ExecuteNonQuery();
                                Setreturnquantity(TotalReturn_Outward, TotalReturnInward);
                            }
                            else
                            {
                                Inwardqty = 0;
                                //int TotalReturn_Outward = Convert.ToInt32(hdnInwardQty.Value) - Convert.ToInt32(txtReturnInward.Text);
                                int TotalReturnInward = Convert.ToInt32(Inwardqty.ToString()) + Convert.ToInt32(txtReturnInward.Text);

                                ////Updated current stage
                                //SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblLaserCutting] SET [InwardQty] = '" + TotalReturn_Outward + "',[OutwardQty] = '" + TotalReturn_Outward + "' WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                                //cmdupdate.ExecuteNonQuery();

                                ////Updated Prev stage 
                                //SqlCommand cmdupdate1 = new SqlCommand("UPDATE [dbo].[tblLaserPrograming] SET [InwardQty] = '" + TotalReturnInward + "' ,[IsComplete] = NULL  WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                                //cmdupdate1.ExecuteNonQuery();
                                //Setreturnquantity(TotalReturn_Outward, TotalReturnInward);
                                InsertFullReturnQuantity(TotalReturnInward);
                            }



                        }
                        con.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Quantity has been Return Successfully..!');window.location.href='LaserCutting.aspx';", true);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select stage..!');window.location.href='Stock.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Quantity has been Return Successfully..!');window.location.href='LaserCutting.aspx';", true);

        }


    }

    protected void dgvCNCBending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "selectOAnumber")
        {
            string oaNumber = Convert.ToString(e.CommandArgument.ToString());
            ViewState["OANumber"] = oaNumber;
            if (oaNumber != "")
            {
                SqlCommand cmd = new SqlCommand("select OutwardQty from tblRptCNCBending WHERE SubOA='" + oaNumber + "'", con);
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

    protected void dgvCNCBending_RowDataBound(object sender, GridViewRowEventArgs e)
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
                SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM [DB_ProcetechERP].[tblUserRoleAuthorization] where UserID = '" + idd + "' AND PageName = 'CNCBending.aspx' AND PagesView = '1'", con);
                Sdd.Fill(Dtt);
                if (Dtt.Rows.Count > 0)
                {
                    dgvCNCBending.Columns[1].Visible = false;
                    dgvCNCBending.Columns[13].Visible = false;
                    txtOutwardQty.ReadOnly = true;
                    btnPrintData.Visible = false;
                }
            }



            string Id = dgvCNCBending.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
            gvDetails.DataSource = GetData(string.Format("select * from [DB_ProcetechERP].[vwCNCBending] where SubOA='{0}'", Id));
            gvDetails.DataBind();
        }
    }
    private static DataTable GetData(string query)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
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
            Response.Redirect("PDFShow.aspx?Name=CNC Bending");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        //string Report = "CNCBEN";
        //string url = "ProductionExcel.aspx?Dep=" + Server.UrlEncode(Report);
        //Response.Redirect(url);



        if (txtCustomerNameNew.Text == "")
        {
            string Report = "CNCBEN";
            string url = "ProductionExcel.aspx?Dep=" + Server.UrlEncode(Report);
            Response.Redirect(url);
        }

        else
        {
            string Report = "CNCBEN";
            string Customer = Server.UrlEncode(txtCustomerNameNew.Text);
            string url = "ProductionExcel.aspx?Dep=" + Server.UrlEncode(Report) + "&Customer=" + Customer;
            Response.Redirect(url);
        }
    }


    protected void txtCustomerNameNew_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }



    public void FillGrid()
    {

        try
        {
            string query = string.Empty;
            query = @"SELECT [JobNo],[CNCBendingId],[OANumber],[SubOA],[CustomerName],[Size],[TotalQty],[InwardDtTime],[InwardQty],[OutwardDtTime],[OutwardQty],
                [DeliveryDate],[IsApprove],[IsPending],[IsCancel],[CreatedBy],[CreatedDate],A.[UpdatedBy],[UpdatedDate] 
              ,CustomerCode  FROM tblCNCBending AS A                
			   LEFT JOIN Company AS C ON C.cname=A.customername
                WHERE IsComplete IS NULL 
                AND CustomerName LIKE '" + txtCustomerNameNew.Text.Trim() + @"%'
                ORDER BY CONVERT(DateTime, DeliveryDate, 103) ASC";


            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgvCNCBending.DataSource = dt;
                dgvCNCBending.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvCNCBending.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
            }
            else
            {
                dgvCNCBending.DataSource = null;
                dgvCNCBending.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvCNCBending.ClientID + "', 900, 1020 , 40 ,true); </script>", false);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Pending Record Not Found..!');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
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

    public void SetFullReturnquntity(int TotalReturnInward)
    {
        string Table = ddlstages.SelectedValue;
        SqlCommand cmdupdate1 = new SqlCommand("UPDATE " + Table + " SET [InwardQty] = '" + TotalReturnInward + "',[IsComplete] = NULL WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        int Success = cmdupdate1.ExecuteNonQuery();
        if (Success >= 0)
        {
            SqlCommand cmdDelete = new SqlCommand("Delete from [tblCNCBending] WHERE SubOA='" + hdnSubOANo.Value + "'", con);
            cmdDelete.ExecuteNonQuery();
        }


    }

    //Added By Nikhil if there is No data presnet in returing table 11-12-2024
    public void InsertFullReturnQuantity(int TotalReturnInward)
    {

        string Table = ddlstages.SelectedValue;
        SqlCommand cmdupdate1 = new SqlCommand("SELECT * FROM [tblCNCBending] WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        SqlDataReader reader = cmdupdate1.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            string OAnumber = reader["OAnumber"].ToString();
            string SubOA = reader["SubOA"].ToString();
            string customername = reader["customername"].ToString();
            string size = reader["size"].ToString();
            int totalinward;
            DateTime? inwarddatetime;
            int inwardqty;
            DateTime? deliverydate;
            if (reader["TotalQty"] != "")
            {
                totalinward = Convert.ToInt32(reader["TotalQty"]);
            }
            else
            {
                totalinward = 0;
            }
            if (reader["InwardDtTime"] != "")
            {
                inwarddatetime = Convert.ToDateTime(reader["InwardDtTime"]);
            }
            else
            {
                inwarddatetime = null;
            }
            if (reader["InwardQty"] != "")
            {
                inwardqty = Convert.ToInt32(reader["InwardQty"]);
            }
            else
            {
                inwardqty = 0;
            }
            if (reader["DeliveryDate"] != "")
            {
                deliverydate = Convert.ToDateTime(reader["DeliveryDate"]);
            }
            else
            {
                deliverydate = null;
            }
            string JobNo = reader["JobNo"].ToString();

            reader.Close();

            SqlCommand cmdupdate = new SqlCommand(
                "INSERT INTO " + Table + " (OAnumber, SubOA, customername, size, TotalQty, InwardDtTime, InwardQty, DeliveryDate, JobNo, IsComplete,IsApprove) " +
                "VALUES (@OAnumber, @SubOA, @customername, @size, @totalinward, @inwarddatetime, @inwardqty, @deliverydate, @JobNo, NULL,1)", con);


            cmdupdate.Parameters.AddWithValue("@OAnumber", OAnumber);
            cmdupdate.Parameters.AddWithValue("@SubOA", SubOA);
            cmdupdate.Parameters.AddWithValue("@customername", customername);
            cmdupdate.Parameters.AddWithValue("@size", size);
            cmdupdate.Parameters.AddWithValue("@totalinward", totalinward);
            if (inwarddatetime == null)
            {
                cmdupdate.Parameters.AddWithValue("@inwarddatetime", DBNull.Value);
            }
            else
            {
                cmdupdate.Parameters.AddWithValue("@inwarddatetime", inwarddatetime);
            }
            cmdupdate.Parameters.AddWithValue("@inwardqty", TotalReturnInward);
            if (deliverydate == null)
            {
                cmdupdate.Parameters.AddWithValue("@deliverydate", DBNull.Value);
            }
            else
            {
                cmdupdate.Parameters.AddWithValue("@deliverydate", deliverydate);
            }
            cmdupdate.Parameters.AddWithValue("@JobNo", JobNo);

            int Success = cmdupdate.ExecuteNonQuery();

            if (Success >= 0)
            {
                SqlCommand cmdselect = new SqlCommand("select InwardQty from  tblCNCBending  WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                Object Inwardqty = cmdselect.ExecuteScalar();
                if (Inwardqty == null)
                {
                    SqlCommand cmdDelete = new SqlCommand("DELETE FROM [tblCNCBending] WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                    cmdDelete.ExecuteNonQuery();
                }
                else
                {
                    int inquity = Convert.ToInt32(Inwardqty) - Convert.ToInt32(TotalReturnInward);

                    SqlCommand cmdsupdate = new SqlCommand("UPDATE [tblCNCBending] SET [InwardQty] = '" + inquity + "' WHERE SubOA='" + hdnSubOANo.Value + "'", con);
                    cmdsupdate.ExecuteNonQuery();
                }
            }
        }
        else
        {
            Console.WriteLine("No records found for SubOA: " + hdnSubOANo.Value);
        }
    }

    public void Setreturnquantity(int TotalReturn_Outward, int TotalReturnInward)
    {
        string Table = ddlstages.SelectedValue;
        //Updated current stage
        SqlCommand cmdupdate = new SqlCommand("UPDATE [dbo].[tblCNCBending] SET [InwardQty] = '" + TotalReturn_Outward + "',[OutwardQty] = '" + TotalReturn_Outward + "' WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        cmdupdate.ExecuteNonQuery();

        //Updated Prev stage 
        SqlCommand cmdupdate1 = new SqlCommand("UPDATE " + Table + " SET [InwardQty] = '" + TotalReturnInward + "' ,[IsComplete] = NULL  WHERE SubOA='" + hdnSubOANo.Value + "'", con);
        cmdupdate1.ExecuteNonQuery();
    }

}