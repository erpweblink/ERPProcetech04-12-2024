using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RegisterReportPurchase : System.Web.UI.Page
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
                DivRoot.Visible = true;
                btn.Visible = false;
                //bindOutstandingData();
            }
        }
    }


    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegisterReport.aspx");
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
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {

                com.CommandText = "select DISTINCT cname from Company where " + "cname like @Search + '%'  ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> cname = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        cname.Add(sdr["cname"].ToString());
                    }
                }
                con.Close();
                return cname;
            }

        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSupplierList(string prefixText, int count)
    {
        return AutoFillSupplierlist(prefixText);
    }

    public static List<string> AutoFillSupplierlist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {

                com.CommandText = "select DISTINCT SupplierName from tblSupplierMaster where " + "SupplierName like @Search + '%' and IsActive=1";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> sname = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        sname.Add(sdr["SupplierName"].ToString());
                    }
                }
                con.Close();
                return sname;
            }
        }
    }

    int count = 0;
    protected void ddltype_TextChanged(object sender, EventArgs e)
    {
        if (ddltype.Text == "SALE")
        {
            AutoCompleteExtender2.Enabled = false;
            AutoCompleteExtender1.Enabled = true;
            txtPartyName.Text = string.Empty;
            GetCustomerList(txtPartyName.Text, count);
        }
        else if (ddltype.Text == "PURCHASE")
        {
            AutoCompleteExtender1.Enabled = false;
            AutoCompleteExtender2.Enabled = true;
            txtPartyName.Text = string.Empty;
            GetSupplierList(txtPartyName.Text, count);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void bindRegisterReportData()
    {
        DataTable dt = GetData("SP_RegisterReport", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        DataTable dtHSN = GetHSNSummaryData("SP_HSNRegisterReport", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);

        if (dt.Rows.Count > 0)
        {
            btn.Visible = true;
        }
        else
        {
            btn.Visible = false;
        }
        dgvRegisterReport.DataSource = dt;
        dgvRegisterReport.DataBind();
        dgvRegisterReport.EmptyDataText = "Record Not Found";

        dgvHSNSummary.DataSource = dtHSN;
        dgvHSNSummary.DataBind();
        dgvHSNSummary.EmptyDataText = "Record Not Found";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvRegisterReport.ClientID + "',500, 1020 , 40 ,true); </script>", false);
    }

   

    decimal Balance = 0;
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ID = dgvRegisterReport.DataKeys[e.Row.RowIndex].Values[0].ToString();
            con.Open();

            string cname = e.Row.Cells[0].Text.Replace("&amp;", "&");
            //string basicamt = e.Row.Cells[8].Text;

            //BAsicamt += Convert.ToDecimal(basicamt);

            SqlCommand cmd = new SqlCommand("select gstno from company where cname='" + cname + "' and status=0", con);
            string gstno = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

            SqlCommand cmdbasic = new SqlCommand("SELECT SUM(CAST(TaxableAmt as float)) FROM tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string basicamt = cmdbasic.ExecuteScalar().ToString();

            SqlCommand cmdFrightbasic = new SqlCommand("SELECT Basic as FBasic FROM tblTaxInvoiceHdr where Id='" + ID + "'", con);
            string Frightbasicamt = cmdFrightbasic.ExecuteScalar().ToString();

            SqlCommand cmdcgst = new SqlCommand(" SELECT top 1 CGSTPer FROM tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string cgst = cmdcgst.ExecuteScalar().ToString();

            SqlCommand cmdsgst = new SqlCommand(" SELECT top 1 SGSTPer FROM tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string sgst = cmdsgst.ExecuteScalar().ToString();

            SqlCommand cmdigst = new SqlCommand(" SELECT top 1 IGSTPer FROM tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string igst = cmdigst.ExecuteScalar().ToString();

            SqlCommand cmdqty = new SqlCommand("SELECT SUM(CAST(Qty as bigint)) as Qty FROM tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string Qty = cmdqty.ExecuteScalar().ToString();

            //// New change 20/12/2023
            SqlCommand cmdgstamtbasic = new SqlCommand("select ((CAST (tblTaxInvoiceHdr.Cost as float))-(CAST (tblTaxInvoiceHdr.Basic as float))) from tblTaxInvoiceHdr where Id='" + ID + "'", con);
            string BasicGSTAmount = cmdgstamtbasic.ExecuteScalar().ToString();
            ///

            SqlCommand cmdgstamt = new SqlCommand("SELECT SUM(CAST(CGSTAmt as float))+SUM(CAST(SGSTAmt as float)) + SUM(CAST(IGSTAmt as float)) as GSTAmount FROM tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string GSTAmount = cmdgstamt.ExecuteScalar().ToString();

            con.Close();
            Label lblCGST = (Label)e.Row.FindControl("lblCGST");
            lblCGST.Text = cgst;

            Label lblSGST = (Label)e.Row.FindControl("lblSGST");
            lblSGST.Text = sgst;

            Label lblIGST = (Label)e.Row.FindControl("lblIGST");
            lblIGST.Text = igst;

            Label lblQty = (Label)e.Row.FindControl("lblQty");
            lblQty.Text = Qty;

            Label lblBasicTotal = (Label)e.Row.FindControl("lblBasicTotal");
            var basictot = Convert.ToDouble(basicamt) + Convert.ToDouble(Frightbasicamt);
            //lblBasicTotal.Text = Math.Round(Convert.ToDouble(basictot)).ToString();
            lblBasicTotal.Text = Convert.ToDouble(basictot).ToString();

            Label lblGSTAmount = (Label)e.Row.FindControl("lblGSTAmount");
            //lblGSTAmount.Text = Math.Round(Convert.ToDouble(GSTAmount)).ToString();

            /////////// New change 20/12/2023
            var basicGSTtot = Convert.ToDouble(GSTAmount) + Convert.ToDouble(BasicGSTAmount);
            //lblGSTAmount.Text = Math.Round(Convert.ToDouble(basicGSTtot)).ToString();
            lblGSTAmount.Text = Convert.ToDouble(basicGSTtot).ToString();
            //////

            Label lblGSTNumber = (Label)e.Row.FindControl("lblGSTNumber");
            lblGSTNumber.Text = gstno;

            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus.Text != "")
            {
                lblStatus.Text = "Paid";
            }
            else
            {
                lblStatus.Text = "Raised";
            }

            Label lblDocNo = (Label)e.Row.FindControl("lblDocNo");
            Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
            if (lblDocNo.Text != "")
            {
                lblInvoiceNo.Text = lblDocNo.Text;
            }
            else
            {

            }
        }

        //Session["BasicAmt"] = BAsicamt.ToString();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddltype.Text == "SALE")
            {
                bindRegisterReportData();
            }
            else if (ddltype.Text == "PURCHASE")
            {
                bindPurchaseRegisterReportData();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static DataTable GetData(string SP, string PartyName, string FromDate, string ToDate, string Type)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand(SP, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", Type);
                // if (PartyName == "")
                // cmd.Parameters.AddWithValue("@PartyName", DBNull.Value);
                // else
                cmd.Parameters.AddWithValue("@PartyName", PartyName);

                if (FromDate == "")
                    cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                if (ToDate == "")
                    cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        //sda.Fill(ds);
                        return dt;
                    }
                }
            }
        }
    }

    private static DataTable GetHSNSummaryData(string SP, string PartyName, string FromDate, string ToDate, string Type)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand(SP, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", Type);
                if (PartyName == "")
                    cmd.Parameters.AddWithValue("@PartyName", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@PartyName", PartyName);

                if (FromDate == "")
                    cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                if (ToDate == "")
                    cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        //sda.Fill(ds);
                        return dt;
                    }
                }
            }
        }
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        if (ddltype.Text == "SALE")
        {
            string fileneme = ddltype.Text + "_Register " + DateTime.Now.ToShortDateString();
            DataTable dt = new DataTable("GridView_Data");
            DataTable dthsnsummary = new DataTable("GridView_DataHSN");
            DataTable dtHSN = GetHSNSummaryData("SP_HSNRegisterReport", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
            double BasicAmount = 0;
            double GSTTotAmount = 0;
            double GrandAmount = 0;

            double HSNBasicAmt = 0;
            double HSNCGST = 0;
            double HSNSGST = 0;
            double HSNIGST = 0;
            double HSNGtot = 0;
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                foreach (TableCell cell in dgvRegisterReport.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }
                foreach (GridViewRow row in dgvRegisterReport.Rows)
                {
                    Label lblGSTNumber = (Label)row.FindControl("lblGSTNumber");
                    Label lblStatus = (Label)row.FindControl("lblStatus");

                    Label lblCGST = (Label)row.FindControl("lblCGST");
                    Label lblSGST = (Label)row.FindControl("lblSGST");
                    Label lblIGST = (Label)row.FindControl("lblIGST");
                    Label lblGSTAmount = (Label)row.FindControl("lblGSTAmount");
                    Label lblGrandTotal = (Label)row.FindControl("lblGrandTotal");
                    Label lblBasicTotal = (Label)row.FindControl("lblBasicTotal");

                    Label lblInvoiceNo = (Label)row.FindControl("lblInvoiceNo");
                    Label lblQty = (Label)row.FindControl("lblQty");

                    string Party = row.Cells[0].Text.Replace("&amp;", "&");
                    string Type = row.Cells[1].Text;
                    string GSTNumber = lblGSTNumber.Text;
                    string VoucherType = row.Cells[3].Text;
                    string Date = row.Cells[4].Text;
                    string RefNo = row.Cells[5].Text;
                    string DocNo = lblInvoiceNo.Text;   //row.Cells[6].Text;
                    string RefDate = row.Cells[7].Text;
                    string TCS = row.Cells[8].Text;
                    string Qty = lblQty.Text;   //row.Cells[9].Text;
                    string BasicTotal = lblBasicTotal.Text;
                    string CGST = lblCGST.Text;
                    string SGST = lblSGST.Text;
                    string IGST = lblIGST.Text;
                    string GSTAmount = lblGSTAmount.Text;
                    string GrandTotal = lblGrandTotal.Text;
                    string Status = lblStatus.Text;
                    dt.Rows.Add(Party, Type, GSTNumber, VoucherType, Date, RefNo, DocNo, RefDate, TCS, Qty, BasicTotal, CGST, SGST, IGST, GSTAmount, GrandTotal, Status);

                    BasicAmount += Convert.ToDouble(BasicTotal);
                    GSTTotAmount += Convert.ToDouble(GSTAmount);
                    GrandAmount += Convert.ToDouble(GrandTotal);
                }
                //dt.Rows.Add("", "", "", "", "", "", "", "", "", "TOTAL", Math.Round(BasicAmount).ToString(), "", "", "", GSTTotAmount, Math.Round(GrandAmount).ToString(), "");
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "TOTAL", BasicAmount.ToString(), "", "", "", GSTTotAmount, GrandAmount.ToString(), "");


                foreach (TableCell cell in dgvHSNSummary.HeaderRow.Cells)
                {
                    dthsnsummary.Columns.Add(cell.Text);
                }
                dthsnsummary.Columns.Add("Grand Total");

                foreach (DataRow row in dtHSN.Rows)
                {
                    var Gtot = Convert.ToDouble(row["BasicTotal"].ToString()) + Convert.ToDouble(row["CGST"].ToString()) + Convert.ToDouble(row["SGST"].ToString()) + Convert.ToDouble(row["IGST"].ToString());

                    dthsnsummary.Rows.Add(row["Qty"].ToString(), row["BasicTotal"].ToString(), row["HSN"].ToString(), row["UOM"].ToString(), row["CGST"].ToString(), row["SGST"].ToString(), row["IGST"].ToString(), Math.Round(Gtot).ToString());

                    HSNBasicAmt += Convert.ToDouble(row["BasicTotal"].ToString());
                    HSNCGST += Convert.ToDouble(row["CGST"].ToString());
                    HSNSGST += Convert.ToDouble(row["SGST"].ToString());
                    HSNIGST += Convert.ToDouble(row["IGST"].ToString());
                    HSNGtot += Gtot;
                }
                dthsnsummary.Rows.Add("TOTAL", Math.Round(HSNBasicAmt).ToString(), "", "", Math.Round(HSNCGST).ToString(), Math.Round(HSNSGST).ToString(), Math.Round(HSNIGST).ToString(), Math.Round(HSNGtot).ToString());
            }
            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(dt);
                ds.Tables.Add(dthsnsummary);

                ds.Tables[0].TableName = "Invoice_Summary";
                ds.Tables[1].TableName = "HSN_Summary";

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= " + fileneme + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        else
        {
            string fileneme = ddltype.Text + "_Register " + DateTime.Now.ToShortDateString();
            DataTable dt = new DataTable("GridView_Data");
            DataTable dthsnsummary = new DataTable("GridView_DataHSN");
            DataTable dtHSN = GetHSNSummaryData("SP_HSNRegisterReport", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
            double BasicAmount = 0;
            double GSTTotAmount = 0;
            double GrandAmount = 0;

            double HSNBasicAmt = 0;
            double HSNCGST = 0;
            double HSNSGST = 0;
            double HSNIGST = 0;
            double HSNGtot = 0;
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                foreach (TableCell cell in dgvPurchaseRegisterReport.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                }
                foreach (GridViewRow row in dgvPurchaseRegisterReport.Rows)
                {
                    Label lblGSTNumber = (Label)row.FindControl("lblGSTNumber");
                    Label lblStatus = (Label)row.FindControl("lblStatus");
                    Label lblType = (Label)row.FindControl("lblType");

                    Label lblCGST = (Label)row.FindControl("lblCGST");
                    Label lblSGST = (Label)row.FindControl("lblSGST");
                    Label lblIGST = (Label)row.FindControl("lblIGST");
                    Label lblGSTAmount = (Label)row.FindControl("lblGSTAmount");
                    Label lblGrandTotal = (Label)row.FindControl("lblGrandTotal");
                    Label lblBasicTotal = (Label)row.FindControl("lblBasicTotal");

                    Label lblInvoiceNo = (Label)row.FindControl("lblVchNo");
                    Label lblQty = (Label)row.FindControl("lblQty");

                    string Party = row.Cells[0].Text.Replace("&amp;", "&");
                    string Type = lblType.Text;
                    string GSTNumber = lblGSTNumber.Text;
                    string VoucherType = row.Cells[3].Text;
                    string Date = row.Cells[4].Text;
                    string RefNo = row.Cells[5].Text;
                    string DocNo = lblInvoiceNo.Text;//row.Cells[6].Text;
                    //string RefDate = row.Cells[7].Text;
                    string TCS = row.Cells[7].Text;
                    string Qty = lblQty.Text; //row.Cells[9].Text;
                    string BasicTotal = lblBasicTotal.Text;
                    string CGST = lblCGST.Text;
                    string SGST = lblSGST.Text;
                    string IGST = lblIGST.Text;
                    string GSTAmount = lblGSTAmount.Text;
                    string GrandTotal = lblGrandTotal.Text;
                    string Status = lblStatus.Text;
                    dt.Rows.Add(Party, Type, GSTNumber, VoucherType, Date, RefNo, DocNo, TCS, Qty, BasicTotal, CGST, SGST, IGST, GSTAmount, GrandTotal, Status);

                    BasicAmount += Convert.ToDouble(BasicTotal);
                    GSTTotAmount += Convert.ToDouble(GSTAmount);
                    GrandAmount += Convert.ToDouble(GrandTotal);
                }
                dt.Rows.Add("", "", "", "", "", "", "", "", "TOTAL", Math.Round(BasicAmount).ToString(), "", "", "", GSTTotAmount, Math.Round(GrandAmount).ToString(), "");


                foreach (TableCell cell in dgvHSNSummaryPurchase.HeaderRow.Cells)
                {
                    dthsnsummary.Columns.Add(cell.Text);
                }
                dthsnsummary.Columns.Add("Grand Total");

                foreach (GridViewRow row in dgvHSNSummaryPurchase.Rows)
                {
                    string qty = row.Cells[0].Text;
                    string basictotal = row.Cells[1].Text;
                    string hsn = row.Cells[2].Text;
                    string uom = row.Cells[3].Text;
                    Label lblCGSTAmt = (Label)row.FindControl("lblCGSTAmt");
                    Label lblSGSTAmt = (Label)row.FindControl("lblSGSTAmt");
                    Label lblIGSTAmt = (Label)row.FindControl("lblIGSTAmt");

                    var Gtot = Convert.ToDouble(basictotal) + Convert.ToDouble(lblCGSTAmt.Text) + Convert.ToDouble(lblSGSTAmt.Text) + Convert.ToDouble(lblIGSTAmt.Text);

                    dthsnsummary.Rows.Add(qty, basictotal, hsn, uom, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, Math.Round(Gtot).ToString());

                    HSNBasicAmt += Convert.ToDouble(basictotal);
                    HSNCGST += Convert.ToDouble(lblCGSTAmt.Text);
                    HSNSGST += Convert.ToDouble(lblSGSTAmt.Text);
                    HSNIGST += Convert.ToDouble(lblIGSTAmt.Text);
                    HSNGtot += Gtot;
                }
                dthsnsummary.Rows.Add("TOTAL", Math.Round(HSNBasicAmt).ToString(), "", "", Math.Round(HSNCGST).ToString(), Math.Round(HSNSGST).ToString(), Math.Round(HSNIGST).ToString(), Math.Round(HSNGtot).ToString());
            }
            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(dt);
                ds.Tables.Add(dthsnsummary);

                ds.Tables[0].TableName = "Purchase_Summary";
                ds.Tables[1].TableName = "HSN_Summary";

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= " + fileneme + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
    }

    #region Purchase 

    protected void bindPurchaseRegisterReportData()
    {
        DataTable dt = GetData("SP_RegisterReport", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        DataTable dtHSN = GetHSNSummaryData("SP_HSNRegisterReport", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);

        if (dt.Rows.Count > 0)
        {
            btn.Visible = true;
        }
        else
        {
            btn.Visible = false;
        }
        dgvPurchaseRegisterReport.DataSource = dt;
        dgvPurchaseRegisterReport.DataBind();
        dgvPurchaseRegisterReport.EmptyDataText = "Record Not Found";

        dgvHSNSummaryPurchase.DataSource = dtHSN;
        dgvHSNSummaryPurchase.DataBind();
        dgvHSNSummaryPurchase.EmptyDataText = "Record Not Found";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeaderPurchase('" + dgvPurchaseRegisterReport.ClientID + "',500, 1020 , 40 ,true); </script>", false);
    }

    protected void dgvPurchaseRegisterReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ID = dgvPurchaseRegisterReport.DataKeys[e.Row.RowIndex].Values[0].ToString();
            con.Open();

            string sname = e.Row.Cells[0].Text.Replace("&amp;", "&");

            SqlCommand cmd = new SqlCommand("select GSTNo from tblSupplierMaster where SupplierName='" + sname + "' and IsActive=1", con);
            string gstno = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

            SqlCommand cmdbasic = new SqlCommand("SELECT SUM(CAST(Amount as float)) FROM tblPurchaseBillDtls where HeaderID='" + ID + "'", con);
            string basicamt = cmdbasic.ExecuteScalar() == null ? "0" : cmdbasic.ExecuteScalar().ToString();

            SqlCommand cmdcgst = new SqlCommand(" SELECT top 1 CGSTPer FROM tblPurchaseBillDtls where HeaderID='" + ID + "'", con);
            string cgst = cmdcgst.ExecuteScalar() == null ? "0" : cmdcgst.ExecuteScalar().ToString();

            SqlCommand cmdsgst = new SqlCommand(" SELECT top 1 SGSTPer FROM tblPurchaseBillDtls where HeaderID='" + ID + "'", con);
            string sgst = cmdsgst.ExecuteScalar() == null ? "0" : cmdsgst.ExecuteScalar().ToString();

            SqlCommand cmdigst = new SqlCommand(" SELECT top 1 IGSTPer FROM tblPurchaseBillDtls where HeaderID='" + ID + "'", con);
            string igst = cmdigst.ExecuteScalar() == null ? "0" : cmdigst.ExecuteScalar().ToString();

            SqlCommand cmdqty = new SqlCommand("SELECT SUM(CAST(Qty as float)) as Qty FROM tblPurchaseBillDtls where HeaderID='" + ID + "'", con);
            string Qty = cmdqty.ExecuteScalar().ToString();

            var Cgstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(cgst == "" ? "0" : cgst) / 100;
            var Sgstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(sgst == "" ? "0" : sgst) / 100;
            var Igstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(igst == "" ? "0" : igst) / 100;

            string GSTAmount = (Cgstamt + Sgstamt + Igstamt).ToString();

            con.Close();
            Label lblCGST = (Label)e.Row.FindControl("lblCGST");
            lblCGST.Text = cgst;

            Label lblSGST = (Label)e.Row.FindControl("lblSGST");
            lblSGST.Text = sgst;

            Label lblIGST = (Label)e.Row.FindControl("lblIGST");
            lblIGST.Text = igst;

            Label lblQty = (Label)e.Row.FindControl("lblQty");
            lblQty.Text = Qty;

            Label lblBasicTotal = (Label)e.Row.FindControl("lblBasicTotal");
            lblBasicTotal.Text = Math.Round(Convert.ToDouble(basicamt)).ToString();

            Label lblGSTAmount = (Label)e.Row.FindControl("lblGSTAmount");
            lblGSTAmount.Text = Math.Round(Convert.ToDouble(GSTAmount)).ToString();

            Label lblGSTNumber = (Label)e.Row.FindControl("lblGSTNumber");
            lblGSTNumber.Text = gstno;

            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus.Text != "")
            {
                lblStatus.Text = "Paid";
            }
            else
            {
                lblStatus.Text = "Raised";
            }

            Label lblType = (Label)e.Row.FindControl("lblType");
            if (lblGSTNumber.Text != "")
            {
                lblType.Text = "Register";
            }
            else
            {
                lblType.Text = "Unregister";
            }

        }
    }

	decimal BasicTotalAmt;
    decimal CGSTTotalAmt;
    decimal SGSTTotalAmt;
    decimal IGSTTotalAmt;
    protected void dgvHSNSummaryPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            con.Open();
            //string basicamt = e.Row.Cells[1].Text;
            string hsn = e.Row.Cells[2].Text;
			
			
			SqlCommand cmdbasicamt = new SqlCommand("select top 1 Amount from tblPurchaseBillDtls where HSN='" + hsn + "'", con);
            string basicamt = cmdbasicamt.ExecuteScalar().ToString();

            SqlCommand cmdcgst = new SqlCommand("select top 1 CGSTPer from tblPurchaseBillDtls where HSN='" + hsn + "'", con);
            string cgst = cmdcgst.ExecuteScalar().ToString();

            SqlCommand cmdsgst = new SqlCommand("select top 1 SGSTPer from tblPurchaseBillDtls where HSN='" + hsn + "'", con);
            string sgst = cmdsgst.ExecuteScalar().ToString();

            SqlCommand cmdigst = new SqlCommand("select top 1 IGSTPer from tblPurchaseBillDtls where HSN='" + hsn + "'", con);
            string igst = cmdigst.ExecuteScalar().ToString();

            var Cgstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(cgst) / 100;
            var Sgstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(sgst) / 100;
            var Igstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(igst) / 100;
			var Basic = Convert.ToDouble(basicamt);

            string GSTAmount = (Cgstamt + Sgstamt + Igstamt).ToString();

            con.Close();
            Label lblCGST = (Label)e.Row.FindControl("lblCGSTAmt");
            lblCGST.Text = Math.Round(Cgstamt).ToString();

            Label lblSGST = (Label)e.Row.FindControl("lblSGSTAmt");
            lblSGST.Text = Math.Round(Sgstamt).ToString();

            Label lblIGST = (Label)e.Row.FindControl("lblIGSTAmt");
            lblIGST.Text = Math.Round(Igstamt).ToString();
			
            Label lblBasicTotal = (Label)e.Row.FindControl("lblBasicTotal");
            lblBasicTotal.Text = Math.Round(Basic).ToString();
        }
		
		if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BasicTotalAmt += Convert.ToDecimal((e.Row.FindControl("lblBasicTotal") as Label).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblBasicTotalamt") as Label).Text = BasicTotalAmt.ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CGSTTotalAmt += Convert.ToDecimal((e.Row.FindControl("lblCGSTAmt") as Label).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblCGSTTotalamt") as Label).Text = CGSTTotalAmt.ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SGSTTotalAmt += Convert.ToDecimal((e.Row.FindControl("lblSGSTAmt") as Label).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblSGSTTotalamt") as Label).Text = SGSTTotalAmt.ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            IGSTTotalAmt += Convert.ToDecimal((e.Row.FindControl("lblIGSTAmt") as Label).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblIGSTTotalamt") as Label).Text = IGSTTotalAmt.ToString();
        }
    }

    #endregion

    Decimal TOTQty;
    Decimal TOTBasic;
    Decimal TOTCgst;
    Decimal TOTSgst;
    Decimal TOTIgst;
    protected void dgvHSNSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblQty = (Label)e.Row.FindControl("lblQty");
            Label lblBasicTotal = (Label)e.Row.FindControl("lblBasicTotal");
            Label lblCGST = (Label)e.Row.FindControl("lblCGST");
            Label lblSGST = (Label)e.Row.FindControl("lblSGST");
            Label lblIGST = (Label)e.Row.FindControl("lblIGST");

            TOTQty += Convert.ToDecimal((e.Row.FindControl("lblQty") as Label).Text);
            TOTBasic += Convert.ToDecimal((e.Row.FindControl("lblBasicTotal") as Label).Text);
            TOTCgst += Convert.ToDecimal((e.Row.FindControl("lblCGST") as Label).Text);
            TOTSgst += Convert.ToDecimal((e.Row.FindControl("lblSGST") as Label).Text);
            TOTIgst += Convert.ToDecimal((e.Row.FindControl("lblIGST") as Label).Text);
        }
        //sum of footer
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("totalQty") as Label).Text = TOTQty.ToString();
            (e.Row.FindControl("totalBasicTotal") as Label).Text = Math.Round(TOTBasic).ToString("##.00");
            (e.Row.FindControl("totalCGST") as Label).Text = Math.Round(TOTCgst).ToString("##.00");
            (e.Row.FindControl("totalSGST") as Label).Text = Math.Round(TOTSgst).ToString("##.00");
            (e.Row.FindControl("totalIGST") as Label).Text = Math.Round(TOTIgst).ToString("##.00");
        }
    }
}