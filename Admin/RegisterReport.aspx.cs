using ClosedXML.Excel;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Font = iTextSharp.text.Font;
using System.Globalization;

public partial class Admin_RegisterReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
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

                com.CommandText = "select DISTINCT SupplierName from [DB_ProcetechERP].tblSupplierMaster where " + "SupplierName like @Search + '%' and IsActive=1";

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
        //DataTable dt = GetData("[ExcelEncLive].[SP_RegisterReport]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        DataTable dt = GetData("[DB_ProcetechERP].[SP_RegsiterReportWithouRowBound]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        DataTable dtHSN = GetHSNSummaryData("[DB_ProcetechERP].[SP_HSNRegisterReport]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);

        if (dt.Rows.Count > 0)
        {
            btn.Visible = true;
        }
        else
        {
            btn.Visible = false;
        }
        //dgvRegisterReport.DataSource = dt;
        //dgvRegisterReport.DataBind();
        //dgvRegisterReport.EmptyDataText = "Record Not Found";

        GVReports.DataSource = dt;
        GVReports.DataBind();
        GVReports.EmptyDataText = "Record Not Found";



        dgvHSNSummary.DataSource = dtHSN;
        dgvHSNSummary.DataBind();
        dgvHSNSummary.EmptyDataText = "Record Not Found";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvRegisterReport.ClientID + "',500, 1020 , 40 ,true); </script>", false);
    }

    //private static DataTable GetData(string SP)
    //{
    //    string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(strConnString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(SP, con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Type", "SALE");
    //            cmd.Parameters.AddWithValue("@PartyName", "Radix Electrosystems Pvt Ltd");
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataSet ds = new DataSet())
    //                {
    //                    DataTable dt = new DataTable();
    //                    sda.Fill(dt);
    //                    return dt;
    //                }
    //            }
    //        }
    //    }
    //}

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

            SqlCommand cmdbasic = new SqlCommand("SELECT SUM(CAST(TaxableAmt as float)) FROM [DB_ProcetechERP].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string basicamt = cmdbasic.ExecuteScalar().ToString();

            SqlCommand cmdFrightbasic = new SqlCommand("SELECT Basic as FBasic FROM [DB_ProcetechERP].tblTaxInvoiceHdr where Id='" + ID + "'", con);
            string Frightbasicamt = cmdFrightbasic.ExecuteScalar().ToString();

            SqlCommand cmdcgst = new SqlCommand(" SELECT top 1 CGSTPer FROM [DB_ProcetechERP].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string cgst = cmdcgst.ExecuteScalar().ToString();

            // New Changes 18-7-23
            SqlCommand cmdcgstAmt = new SqlCommand("select SUM(cast(CGSTAmt as float))from [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string cgstAmt = cmdcgstAmt.ExecuteScalar().ToString();

            SqlCommand cmdsgst = new SqlCommand(" SELECT top 1 SGSTPer FROM [DB_ProcetechERP].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string sgst = cmdsgst.ExecuteScalar().ToString();

            // New Changes 18-7-23
            SqlCommand cmdsgstAmt = new SqlCommand("select SUM(cast(SGSTAmt as float))from [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string sgstAmt = cmdsgstAmt.ExecuteScalar().ToString();

            SqlCommand cmdigst = new SqlCommand(" SELECT top 1 IGSTPer FROM [DB_ProcetechERP].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string igst = cmdigst.ExecuteScalar().ToString();

            // New Changes 18-7-23
            SqlCommand cmdigstAmt = new SqlCommand("select SUM(cast(IGSTAmt as float))from [DB_ProcetechERP]. tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string igstAmt = cmdigstAmt.ExecuteScalar().ToString();

            SqlCommand cmdqty = new SqlCommand("SELECT SUM(CAST(Qty as bigint)) as Qty FROM [DB_ProcetechERP] tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string Qty = cmdqty.ExecuteScalar().ToString();

            //// New change 20/12/2023
            SqlCommand cmdgstamtbasic = new SqlCommand("select ((CAST (tblTaxInvoiceHdr.Cost as float))-(CAST (tblTaxInvoiceHdr.Basic as float))) from [DB_ProcetechERP].tblTaxInvoiceHdr where Id='" + ID + "'", con);
            string BasicGSTAmount = cmdgstamtbasic.ExecuteScalar().ToString();
            ///

            SqlCommand cmdgstamt = new SqlCommand("SELECT SUM(CAST(CGSTAmt as float))+SUM(CAST(SGSTAmt as float)) + SUM(CAST(IGSTAmt as float)) as GSTAmount FROM [DB_ProcetechERP].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
            string GSTAmount = cmdgstamt.ExecuteScalar().ToString();

            con.Close();
            Label lblCGST = (Label)e.Row.FindControl("lblCGST");
            lblCGST.Text = cgst;

            // New Changes 18-7-23
            Label lblCGSTAmt = (Label)e.Row.FindControl("lblCGSTAmt");
            lblCGSTAmt.Text = cgstAmt;

            Label lblSGST = (Label)e.Row.FindControl("lblSGST");
            lblSGST.Text = sgst;

            // New Changes 18-7-23
            Label lblSGSTAmt = (Label)e.Row.FindControl("lblSGSTAmt");
            lblSGSTAmt.Text = sgstAmt;

            Label lblIGST = (Label)e.Row.FindControl("lblIGST");
            lblIGST.Text = igst;

            // New Changes 18-7-23
            Label lblIGSTAmt = (Label)e.Row.FindControl("lblIGSTAmt");
            lblIGSTAmt.Text = igstAmt;

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
            //var basicGSTtot = Convert.ToDouble(GSTAmount) + Convert.ToDouble(BasicGSTAmount);
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private static DataTable GetData(string SP, string PartyName, string FromDate, string ToDate, string Type)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
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
        string strConnString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
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
        ExportExcell();
        //if (ddltype.Text == "SALE")
        //{
        //    string fileneme = ddltype.Text + "_Register " + DateTime.Now.ToShortDateString();
        //    DataTable dt = new DataTable("GridView_Data");
        //    DataTable dthsnsummary = new DataTable("GridView_DataHSN");
        //    DataTable dtHSN = GetHSNSummaryData("[ExcelEncLive].SP_HSNRegisterReport", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        //    double BasicAmount = 0;
        //    double GSTTotAmount = 0;
        //    double GrandAmount = 0;
        //    double CGSTAmount = 0;
        //    double SGSTAmount = 0;
        //    double IGSTAmount = 0;
        //    double TCSAmount = 0;

        //    double HSNBasicAmt = 0;
        //    double HSNCGST = 0;
        //    double HSNSGST = 0;
        //    double HSNIGST = 0;
        //    double HSNGtot = 0;
        //    using (SqlDataAdapter sda = new SqlDataAdapter())
        //    {
        //        foreach (TableCell cell in dgvRegisterReport.HeaderRow.Cells)
        //        {
        //            dt.Columns.Add(cell.Text);
        //        }
        //        foreach (GridViewRow row in dgvRegisterReport.Rows)
        //        {
        //            Label lblGSTNumber = (Label)row.FindControl("lblGSTNumber");
        //            Label lblStatus = (Label)row.FindControl("lblStatus");

        //            Label lblCGST = (Label)row.FindControl("lblCGST");
        //            Label lblCGSTAmt = (Label)row.FindControl("lblCGSTAmt");
        //            Label lblSGST = (Label)row.FindControl("lblSGST");
        //            Label lblSGSTAmt = (Label)row.FindControl("lblSGSTAmt");
        //            Label lblIGST = (Label)row.FindControl("lblIGST");
        //            Label lblIGSTAmt = (Label)row.FindControl("lblIGSTAmt");
        //            Label lblGSTAmount = (Label)row.FindControl("lblGSTAmount");
        //            Label lblGrandTotal = (Label)row.FindControl("lblGrandTotal");
        //            Label lblBasicTotal = (Label)row.FindControl("lblBasicTotal");

        //            Label lblInvoiceNo = (Label)row.FindControl("lblInvoiceNo");
        //            Label lblQty = (Label)row.FindControl("lblQty");

        //            string Party = row.Cells[0].Text.Replace("&amp;", "&");
        //            string Type = row.Cells[1].Text;
        //            string GSTNumber = lblGSTNumber.Text;
        //            string VoucherType = row.Cells[3].Text;
        //            string Date = row.Cells[4].Text;
        //            string RefNo = row.Cells[5].Text;
        //            string DocNo = lblInvoiceNo.Text;   //row.Cells[6].Text;
        //            string RefDate = row.Cells[7].Text;
        //            string TCS = row.Cells[8].Text;
        //            string Qty = lblQty.Text;   //row.Cells[9].Text;
        //            string BasicTotal = lblBasicTotal.Text;
        //            string CGST = lblCGST.Text;
        //            string CGSTAmt = lblCGSTAmt.Text;
        //            string SGST = lblSGST.Text;
        //            string SGSTAmt = lblSGSTAmt.Text;
        //            string IGST = lblIGST.Text;
        //            string IGSTAmt = lblIGSTAmt.Text;
        //            string GSTAmount = lblGSTAmount.Text;
        //            string GrandTotal = lblGrandTotal.Text;
        //            string Status = lblStatus.Text;
        //            dt.Rows.Add(Party, Type, GSTNumber, VoucherType, Date, RefNo, DocNo, RefDate, TCS, Qty, BasicTotal, CGST, CGSTAmt, SGST, SGSTAmt, IGST, IGSTAmt, GSTAmount, GrandTotal, Status);

        //            BasicAmount += Convert.ToDouble(BasicTotal);
        //            GSTTotAmount += Convert.ToDouble(GSTAmount);
        //            GrandAmount += Convert.ToDouble(GrandTotal);
        //            CGSTAmount += Convert.ToDouble(CGSTAmt);
        //            SGSTAmount += Convert.ToDouble(SGSTAmt);
        //            IGSTAmount += Convert.ToDouble(IGSTAmt);
        //            TCSAmount += Convert.ToDouble(TCS);
        //        }
        //        //dt.Rows.Add("", "", "", "", "", "", "", "", "", "TOTAL", Math.Round(BasicAmount).ToString(), "", "", "", GSTTotAmount, Math.Round(GrandAmount).ToString(), "");
        //        dt.Rows.Add("", "", "", "", "", "", "", "TOTAL", TCSAmount.ToString(), "", BasicAmount.ToString(), "", CGSTAmount.ToString(), "", SGSTAmount.ToString(), "", IGSTAmount.ToString(), GSTTotAmount, GrandAmount.ToString(), "");


        //        foreach (TableCell cell in dgvHSNSummary.HeaderRow.Cells)
        //        {
        //            dthsnsummary.Columns.Add(cell.Text);
        //        }
        //        dthsnsummary.Columns.Add("Grand Total");

        //        foreach (DataRow row in dtHSN.Rows)
        //        {
        //            var Gtot = Convert.ToDouble(row["BasicTotal"].ToString()) + Convert.ToDouble(row["CGST"].ToString()) + Convert.ToDouble(row["SGST"].ToString()) + Convert.ToDouble(row["IGST"].ToString());

        //            //dthsnsummary.Rows.Add(row["Qty"].ToString(), row["BasicTotal"].ToString(), row["HSN"].ToString(), row["UOM"].ToString(), row["CGST"].ToString(), row["SGST"].ToString(), row["IGST"].ToString(), Math.Round(Gtot).ToString());

        //            dthsnsummary.Rows.Add(row["HSN"].ToString(), row["Qty"].ToString(), row["UOM"].ToString(), row["BasicTotal"].ToString(), row["CGST"].ToString(), row["SGST"].ToString(), row["IGST"].ToString(), Math.Round(Gtot).ToString());


        //            HSNBasicAmt += Convert.ToDouble(row["BasicTotal"].ToString());
        //            HSNCGST += Convert.ToDouble(row["CGST"].ToString());
        //            HSNSGST += Convert.ToDouble(row["SGST"].ToString());
        //            HSNIGST += Convert.ToDouble(row["IGST"].ToString());
        //            HSNGtot += Gtot;
        //        }
        //        dthsnsummary.Rows.Add("TOTAL", "", "", Math.Round(HSNBasicAmt).ToString(), Math.Round(HSNCGST).ToString(), Math.Round(HSNSGST).ToString(), Math.Round(HSNIGST).ToString(), Math.Round(HSNGtot).ToString());
        //    }
        //    using (DataSet ds = new DataSet())
        //    {
        //        ds.Tables.Add(dt);
        //        ds.Tables.Add(dthsnsummary);

        //        ds.Tables[0].TableName = "Invoice_Summary";
        //        ds.Tables[1].TableName = "HSN_Summary";

        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            wb.Worksheets.Add(ds);
        //            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //            wb.Style.Font.Bold = true;
        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.Charset = "";
        //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            Response.AddHeader("content-disposition", "attachment;filename= " + fileneme + ".xlsx");
        //            using (MemoryStream MyMemoryStream = new MemoryStream())
        //            {
        //                wb.SaveAs(MyMemoryStream);
        //                MyMemoryStream.WriteTo(Response.OutputStream);
        //                Response.Flush();
        //                Response.End();
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    string fileneme = ddltype.Text + "_Register " + DateTime.Now.ToShortDateString();
        //    DataTable dt = new DataTable("GridView_Data");
        //    DataTable dthsnsummary = new DataTable("GridView_DataHSN");
        //    DataTable dtHSN = GetHSNSummaryData("[ExcelEncLive].SP_HSNRegisterReport", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        //    double BasicAmount = 0;
        //    double GSTTotAmount = 0;
        //    double GrandAmount = 0;

        //    double HSNBasicAmt = 0;
        //    double HSNCGST = 0;
        //    double HSNSGST = 0;
        //    double HSNIGST = 0;
        //    double HSNGtot = 0;
        //    using (SqlDataAdapter sda = new SqlDataAdapter())
        //    {
        //        foreach (TableCell cell in dgvPurchaseRegisterReport.HeaderRow.Cells)
        //        {
        //            dt.Columns.Add(cell.Text);
        //        }
        //        foreach (GridViewRow row in dgvPurchaseRegisterReport.Rows)
        //        {
        //            Label lblGSTNumber = (Label)row.FindControl("lblGSTNumber");
        //            Label lblStatus = (Label)row.FindControl("lblStatus");
        //            Label lblType = (Label)row.FindControl("lblType");

        //            Label lblCGST = (Label)row.FindControl("lblCGST");
        //            Label lblSGST = (Label)row.FindControl("lblSGST");
        //            Label lblIGST = (Label)row.FindControl("lblIGST");
        //            Label lblGSTAmount = (Label)row.FindControl("lblGSTAmount");
        //            Label lblGrandTotal = (Label)row.FindControl("lblGrandTotal");
        //            Label lblBasicTotal = (Label)row.FindControl("lblBasicTotal");

        //            Label lblInvoiceNo = (Label)row.FindControl("lblVchNo");
        //            Label lblQty = (Label)row.FindControl("lblQty");

        //            string Party = row.Cells[0].Text.Replace("&amp;", "&");
        //            string Type = lblType.Text;
        //            string GSTNumber = lblGSTNumber.Text;
        //            string VoucherType = row.Cells[3].Text;
        //            string Date = row.Cells[4].Text;
        //            string RefNo = row.Cells[5].Text;
        //            string DocNo = lblInvoiceNo.Text;//row.Cells[6].Text;
        //            //string RefDate = row.Cells[7].Text;
        //            string TCS = row.Cells[7].Text;
        //            string Qty = lblQty.Text; //row.Cells[9].Text;
        //            string BasicTotal = lblBasicTotal.Text;
        //            string CGST = lblCGST.Text;
        //            string SGST = lblSGST.Text;
        //            string IGST = lblIGST.Text;
        //            string GSTAmount = lblGSTAmount.Text;
        //            string GrandTotal = lblGrandTotal.Text;
        //            string Status = lblStatus.Text;
        //            dt.Rows.Add(Party, Type, GSTNumber, VoucherType, Date, RefNo, DocNo, TCS, Qty, BasicTotal, CGST, SGST, IGST, GSTAmount, GrandTotal, Status);

        //            BasicAmount += Convert.ToDouble(BasicTotal);
        //            GSTTotAmount += Convert.ToDouble(GSTAmount);
        //            GrandAmount += Convert.ToDouble(GrandTotal);
        //        }
        //        dt.Rows.Add("", "", "", "", "", "", "", "", "TOTAL", Math.Round(BasicAmount).ToString(), "", "", "", GSTTotAmount, Math.Round(GrandAmount).ToString(), "");


        //        foreach (TableCell cell in dgvHSNSummaryPurchase.HeaderRow.Cells)
        //        {
        //            dthsnsummary.Columns.Add(cell.Text);
        //        }
        //        dthsnsummary.Columns.Add("Grand Total");

        //        foreach (GridViewRow row in dgvHSNSummaryPurchase.Rows)
        //        {
        //            string qty = row.Cells[0].Text;
        //            string basictotal = row.Cells[1].Text;
        //            string hsn = row.Cells[2].Text;
        //            string uom = row.Cells[3].Text;
        //            Label lblCGSTAmt = (Label)row.FindControl("lblCGSTAmt");
        //            Label lblSGSTAmt = (Label)row.FindControl("lblSGSTAmt");
        //            Label lblIGSTAmt = (Label)row.FindControl("lblIGSTAmt");

        //            var Gtot = Convert.ToDouble(basictotal) + Convert.ToDouble(lblCGSTAmt.Text) + Convert.ToDouble(lblSGSTAmt.Text) + Convert.ToDouble(lblIGSTAmt.Text);

        //            dthsnsummary.Rows.Add(qty, basictotal, hsn, uom, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, Math.Round(Gtot).ToString());

        //            HSNBasicAmt += Convert.ToDouble(basictotal);
        //            HSNCGST += Convert.ToDouble(lblCGSTAmt.Text);
        //            HSNSGST += Convert.ToDouble(lblSGSTAmt.Text);
        //            HSNIGST += Convert.ToDouble(lblIGSTAmt.Text);
        //            HSNGtot += Gtot;
        //        }
        //        dthsnsummary.Rows.Add("TOTAL", Math.Round(HSNBasicAmt).ToString(), "", "", Math.Round(HSNCGST).ToString(), Math.Round(HSNSGST).ToString(), Math.Round(HSNIGST).ToString(), Math.Round(HSNGtot).ToString());
        //    }
        //    using (DataSet ds = new DataSet())
        //    {
        //        ds.Tables.Add(dt);
        //        ds.Tables.Add(dthsnsummary);

        //        ds.Tables[0].TableName = "Purchase_Summary";
        //        ds.Tables[1].TableName = "HSN_Summary";

        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            wb.Worksheets.Add(ds);
        //            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //            wb.Style.Font.Bold = true;
        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.Charset = "";
        //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            Response.AddHeader("content-disposition", "attachment;filename= " + fileneme + ".xlsx");
        //            using (MemoryStream MyMemoryStream = new MemoryStream())
        //            {
        //                wb.SaveAs(MyMemoryStream);
        //                MyMemoryStream.WriteTo(Response.OutputStream);
        //                Response.Flush();
        //                Response.End();
        //            }
        //        }
        //    }
        //}
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

    protected void dgvHSNSummaryPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            con.Open();
            string basicamt = e.Row.Cells[1].Text;
            string hsn = e.Row.Cells[2].Text;

            SqlCommand cmdcgst = new SqlCommand("select top 1 CGSTPer from tblPurchaseBillDtls where HSN='" + hsn + "'", con);
            string cgst = cmdcgst.ExecuteScalar().ToString();

            SqlCommand cmdsgst = new SqlCommand("select top 1 SGSTPer from tblPurchaseBillDtls where HSN='" + hsn + "'", con);
            string sgst = cmdsgst.ExecuteScalar().ToString();

            SqlCommand cmdigst = new SqlCommand("select top 1 IGSTPer from tblPurchaseBillDtls where HSN='" + hsn + "'", con);
            string igst = cmdigst.ExecuteScalar().ToString();

            var Cgstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(cgst) / 100;
            var Sgstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(sgst) / 100;
            var Igstamt = Convert.ToDouble(basicamt) * Convert.ToDouble(igst) / 100;

            string GSTAmount = (Cgstamt + Sgstamt + Igstamt).ToString();

            con.Close();
            Label lblCGST = (Label)e.Row.FindControl("lblCGSTAmt");
            lblCGST.Text = Math.Round(Cgstamt).ToString();

            Label lblSGST = (Label)e.Row.FindControl("lblSGSTAmt");
            lblSGST.Text = Math.Round(Sgstamt).ToString();

            Label lblIGST = (Label)e.Row.FindControl("lblIGSTAmt");
            lblIGST.Text = Math.Round(Igstamt).ToString();
        }
    }

    #endregion

    Decimal TOTQty;
    Decimal TOTBasic;
    Decimal TOTCgst;
    Decimal TOTSgst;
    Decimal TOTIgst;
    decimal GrandTotalFinal;
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

            GrandTotalFinal = Convert.ToDecimal(TOTBasic) + Convert.ToDecimal(TOTCgst) + Convert.ToDecimal(TOTSgst) + Convert.ToDecimal(TOTIgst);
        }
        lbltotalamount.Text = "Grand Total : ₹ " + GrandTotalFinal.ToString("##.00");
    }
	string formattedDate;
   DateTime dateValue;
    public void ExportExcell()
    {
        // Set the filename for the Excel download
        string filename = ddltype.Text + "_Register " + DateTime.Now.ToShortDateString();
        DataTable dt = GetData("[DB_ProcetechERP].[SP_RegsiterReportWithouRowBound]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        //DataTable dt = GetData("[ExcelEncLive].[SP_RegsiterReportsNew]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        DataTable dtHSN = GetHSNSummaryData("[DB_ProcetechERP].[SP_HSNRegisterReport]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);

        if (dt.Rows.Count > 0)
        {
            //GvExcell.DataSource = dt;
            //GvExcell.DataBind();
            //GvExcell.EmptyDataText = "Record Not Found";
            GVReports.DataSource = dt;
            GVReports.DataBind();
            GVReports.EmptyDataText = "Record Not Found";
        }
        // Initialize total variables
        double BasicAmountTotal = 0;
        double CGSTAmtTotal = 0;
        double SGSTAmtTotal = 0;
        double IGSTAmtTotal = 0;
        double GSTAmountTotal = 0;
        double GrandTotalTotal = 0;

        double HSNBasicAmt = 0;
        double HSNCGST = 0;
        double HSNSGST = 0;
        double HSNIGST = 0;
        double HSNGtot = 0;

        // Create a new Excel workbook
        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("Register Data");
            // Add a name/header for the upper GridView
            ws.Range("A1:G1").Merge().Value = "Procetech Coaters Pvt.Ltd."; // Adjust the range as needed
            ws.Range("A1:G1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.CenterContinuous;
            ws.Range("A1:G1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Range("A1:G1").Style.Font.Bold = true;
            ws.Range("A1:G1").Style.Font.FontSize = 20;
            ws.Range("A2:G2").Merge().Value = "Sales Register"; // Adjust the range as needed
            ws.Range("A2:G2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("A2:G2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Range("A2:G2").Style.Font.Bold = true;
            ws.Range("A2:G2").Style.Font.FontSize = 15;
            ws.Range("A2:G2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


            // Add column headers for dgvRegisterReport
            ws.Cell(3, 1).Value = "Party";
            ws.Cell(3, 2).Value = "Type";
            ws.Cell(3, 3).Value = "GSTNumber";
            ws.Cell(3, 4).Value = "VoucherType";
            ws.Cell(3, 5).Value = "Doc No";
            ws.Cell(3, 6).Value = "Date";
            ws.Cell(3, 7).Value = "RefNo";
            ws.Cell(3, 8).Value = "TCS";
            ws.Cell(3, 9).Value = "RefDate";
            ws.Cell(3, 10).Value = "Qty";
            ws.Cell(3, 11).Value = "BasicTotal";
            ws.Cell(3, 12).Value = "CGST";
            ws.Cell(3, 13).Value = "CGST Amt";
            ws.Cell(3, 14).Value = "SGST";
            ws.Cell(3, 15).Value = "SGST Amt";
            ws.Cell(3, 16).Value = "IGST";
            ws.Cell(3, 17).Value = "IGST Amt";
            ws.Cell(3, 18).Value = "GSTAmount";
            ws.Cell(3, 19).Value = "GrandTotal";
            ws.Cell(3, 20).Value = "Status";

            for (int i = 1; i <= 20; i++)
            {
                ws.Cell(3, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(3, i).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(3, i).Style.Font.Bold = true;
                ws.Cell(3, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(3, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }

            //New aded
            // Add other column headers here...

            ws.Cell(3, 1).Style.Font.Bold = true;
            ws.Cell(3, 2).Style.Font.Bold = true;
            ws.Cell(3, 3).Style.Font.Bold = true;
            ws.Cell(3, 4).Style.Font.Bold = true;
            ws.Cell(3, 5).Style.Font.Bold = true;
            ws.Cell(3, 6).Style.Font.Bold = true;
			ws.Cell(3, 6).Style.DateFormat.Format = "dd-MM-yyyy";
            ws.Cell(3, 7).Style.Font.Bold = true;
            ws.Cell(3, 8).Style.Font.Bold = true;
            ws.Cell(3, 9).Style.Font.Bold = true;
            ws.Cell(3, 10).Style.Font.Bold = true;
            ws.Cell(3, 11).Style.Font.Bold = true;
            ws.Cell(3, 12).Style.Font.Bold = true;
            ws.Cell(3, 13).Style.Font.Bold = true;
            ws.Cell(3, 14).Style.Font.Bold = true;
            ws.Cell(3, 15).Style.Font.Bold = true;
            ws.Cell(3, 16).Style.Font.Bold = true;
            ws.Cell(3, 17).Style.Font.Bold = true;
            ws.Cell(3, 18).Style.Font.Bold = true;
            ws.Cell(3, 19).Style.Font.Bold = true;
            ws.Cell(3, 20).Style.Font.Bold = true;

            int row = 4; // Start from row 2
            if (dt.Rows.Count > 0)
            {
                  DateTime dateValue;
                foreach (GridViewRow row1 in GVReports.Rows)
                {
                    Label lblparty = (Label)row1.FindControl("lblparty");
                    string Party = lblparty.Text;
                    //string Party = row1.Cells[0].Text.Replace("&amp;", "&");
                    Label lblType = (Label)row1.FindControl("lblType1");
                    string Type = lblType.Text;
                    Label lblGSTNumber = (Label)row1.FindControl("lblGSTNumber1");
                    string GSTNumber = lblGSTNumber.Text;
                    Label lblVoucherType = (Label)row1.FindControl("lblVType1");
                    string VoucherType = lblVoucherType.Text;
                    Label lblDocNO = (Label)row1.FindControl("lblVhno1");
                    string VhNo = lblDocNO.Text;
                    Label lblDate = (Label)row1.FindControl("lblnoiceDate1");
                    string Date = lblDate.Text;
					  /// Added on 10-05-2024 by shubham wankhade
                  //  ws.Cell(row, 6).Value = lblDate.Text;
                   // ws.Cell(row, 6).Style.DateFormat.Format = "dd-MM-yyyy";
					  ///End
                    Label lblRefNo = (Label)row1.FindControl("lblrefno1");
                    string RefNo = lblRefNo.Text;
                    Label lblInvoiceNo = (Label)row1.FindControl("lblrefno1");
                    string DocNo = lblInvoiceNo.Text;
                    Label lblRefdate = (Label)row1.FindControl("lblRefdate1");
                    string Refdate = lblRefdate.Text;
                    Label lblTCS = (Label)row1.FindControl("lblTCS1");
                    string TCS = lblTCS.Text;
                    Label lblQty = (Label)row1.FindControl("lblQty1");
                    string Qty = lblQty.Text;
                    Label lblBasicTotal = (Label)row1.FindControl("lblBasicTotal1");
                    string BasicTotal = lblBasicTotal.Text;
                    Label lblCGST = (Label)row1.FindControl("lblCGST1");
                    string CGST = lblCGST.Text;
                    Label lblCGSTAmt = (Label)row1.FindControl("lblCGSTAmt1");
                    string CGSTAmt = lblCGSTAmt.Text;
                    Label lblSGST = (Label)row1.FindControl("lblSGST1");
                    string SGST = lblSGST.Text;
                    Label lblSGSTamt = (Label)row1.FindControl("lblSGSTamt1");
                    string SGSTAmt = lblSGSTamt.Text;
                    Label lblIGST = (Label)row1.FindControl("lblIGST1");
                    string IGST = lblIGST.Text;
                    Label lblIGSTAmt = (Label)row1.FindControl("lblIGSTAmt1");
                    string IGSTAmt = lblIGSTAmt.Text;
                    Label lblGSTAmount = (Label)row1.FindControl("lblGSTAmount1");
                    string GSTAmount = lblGSTAmount.Text;
                    Label lblGrandTotal = (Label)row1.FindControl("lblGrandTotal1");
                    string GrandTotal = lblGrandTotal.Text;
                    Label lblStatus = (Label)row1.FindControl("lblStatus1");
                    string Status = lblStatus.Text;

                    ws.Cell(row, 1).Value = Party;
                    ws.Cell(row, 2).Value = Type;
                    ws.Cell(row, 3).Value = GSTNumber;
                    ws.Cell(row, 4).Value = VoucherType;
                    ws.Cell(row, 5).Value = VhNo;
                   // ws.Cell(row, 6).Value = Date;
					ws.Cell(row, 6).Value = lblDate.Text;
                    ws.Cell(row, 6).Style.NumberFormat.Format = "dd-mm-yyyy";
			     
                    ws.Cell(row, 7).Value = RefNo;
                    ws.Cell(row, 8).Value = TCS;
                    ws.Cell(row, 9).Value = Refdate;
                    ws.Cell(row, 10).Value = Qty;
                    ws.Cell(row, 11).Value = BasicTotal;
                    ws.Cell(row, 12).Value = CGST;
                    ws.Cell(row, 13).Value = CGSTAmt;
                    ws.Cell(row, 14).Value = SGST;
                    ws.Cell(row, 15).Value = SGSTAmt;
                    ws.Cell(row, 16).Value = IGST;
                    ws.Cell(row, 17).Value = IGSTAmt;
                    ws.Cell(row, 18).Value = GSTAmount;
                    ws.Cell(row, 19).Value = GrandTotal;
                    ws.Cell(row, 20).Value = Status;
                    //ws.Cell(row, 18).Value = Qty;
                    // Add other data columns here...
                    BasicAmountTotal += Convert.ToDouble(BasicTotal);
                    CGSTAmtTotal += Convert.ToDouble(CGSTAmt);
                    SGSTAmtTotal += Convert.ToDouble(SGSTAmt);
                    IGSTAmtTotal += Convert.ToDouble(IGSTAmt);
                    GSTAmountTotal += Convert.ToDouble(GSTAmount);
                    GrandTotalTotal += Convert.ToDouble(GrandTotal);
                    // Apply borders and formatting to the current row
                    ws.Range(row, 1, row, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Range(row, 1, row, 20).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    ws.Range(row, 1, row, 20).Style.Font.Bold = false;
                    ws.Range(row, 1, row, 20).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range(row, 1, row, 20).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    row++;
                }

                ws.Cell(row, 1).Value = "";
                ws.Cell(row, 2).Value = "";
                ws.Cell(row, 3).Value = "";
                ws.Cell(row, 4).Value = "";
                ws.Cell(row, 5).Value = "";
                ws.Cell(row, 6).Value = "";
                ws.Cell(row, 7).Value = "";
                ws.Cell(row, 8).Value = "";
                ws.Cell(row, 9).Value = "Total";
                ws.Cell(row, 10).Value = "";
                ws.Cell(row, 11).Value = BasicAmountTotal;
                ws.Cell(row, 12).Value = "";
                ws.Cell(row, 13).Value = CGSTAmtTotal;
                ws.Cell(row, 14).Value = "";
                ws.Cell(row, 15).Value = SGSTAmtTotal;
                ws.Cell(row, 16).Value = "";
                ws.Cell(row, 17).Value = IGSTAmtTotal;
                ws.Cell(row, 18).Value = GSTAmountTotal;
                ws.Cell(row, 19).Value = GrandTotalTotal;
                ws.Cell(row, 20).Value = "";

                // Apply formatting to the "Total" row
                ws.Range(row, 1, row, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Range(row, 1, row, 20).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws.Range(row, 1, row, 20).Style.Font.Bold = true;
                ws.Range(row, 1, row, 20).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(row, 1, row, 20).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                // Add a few empty rows as spacing between sections
                int spacingRows = 2; // You can adjust the number of empty rows as needed

                for (int i = 0; i < spacingRows; i++)
                {
                    row++;
                    ws.Row(row).Height = 15; // Set the height of the empty row for spacing
                }



                // Add column headers for dgvHSNSummary
                ws.Cell(row, 1).Value = "HSN";
                ws.Cell(row, 2).Value = "Qty";
                ws.Cell(row, 3).Value = "UOM";
                ws.Cell(row, 4).Value = "BasicTotal";
                ws.Cell(row, 5).Value = "CGST";
                ws.Cell(row, 6).Value = "SGST";
                ws.Cell(row, 7).Value = "IGST";
                //ws.Cell(row, 8).Value = "SGST";
                ws.Cell(row, 8).Value = "Grand Total";
                //Add other column headers for HSN data here...

                ws.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws.Range(row, 1, row, 8).Style.Font.Bold = true;
                ws.Range(row, 1, row, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(row, 1, row, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                row++; // Start from row 2 for HSN data


                // Add data for dgvHSNSummary
                foreach (DataRow dataRow in dtHSN.Rows)
                {
                    ws.Cell(row, 1).Value = dataRow["HSN"];
                    ws.Cell(row, 2).Value = dataRow["Qty"];
                    ws.Cell(row, 3).Value = dataRow["UOM"];
                    ws.Cell(row, 4).Value = dataRow["BasicTotal"];
                    ws.Cell(row, 5).Value = dataRow["CGST"];
                    ws.Cell(row, 6).Value = dataRow["SGST"];
                    ws.Cell(row, 7).Value = dataRow["IGST"];
                    //ws.Cell(row, 8).Value = dataRow["SGST"];
                    var Gtot = Convert.ToDouble(dataRow["BasicTotal"].ToString()) + Convert.ToDouble(dataRow["CGST"].ToString()) + Convert.ToDouble(dataRow["SGST"].ToString()) + Convert.ToDouble(dataRow["IGST"].ToString());
                    ws.Cell(row, 8).Value = Gtot;
                    // Add other data columns for HSN data here...

                    // Apply borders and formatting to the current row
                    ws.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    ws.Range(row, 1, row, 8).Style.Font.Bold = false;
                    ws.Range(row, 1, row, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range(row, 1, row, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                    // Update HSN totals
                    HSNBasicAmt += Convert.ToDouble(dataRow["BasicTotal"]);
                    HSNCGST += Convert.ToDouble(dataRow["CGST"]);
                    HSNSGST += Convert.ToDouble(dataRow["SGST"]);
                    HSNIGST += Convert.ToDouble(dataRow["IGST"]);
                    HSNGtot += Gtot;
                    row++;
                }

                // Add a total row for HSN data
                ws.Cell(row, 1).Value = "Total";
                ws.Cell(row, 2).Value = "";
                ws.Cell(row, 3).Value = "";
                ws.Cell(row, 4).Value = HSNBasicAmt;
                ws.Cell(row, 5).Value = HSNCGST;
                ws.Cell(row, 6).Value = HSNSGST;
                //ws.Cell(row, 7).Value = "";
                ws.Cell(row, 7).Value = HSNIGST;
                ws.Cell(row, 8).Value = HSNGtot;
                // Add other HSN column totals here...
                ws.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws.Range(row, 1, row, 8).Style.Font.Bold = true;
                ws.Range(row, 1, row, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(row, 1, row, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                for (int i = 0; i < 1; i++)
                {
                    row++;
                }
                // Add the Grand Total row
                //double grandTotal = BasicAmount + GSTTotAmount + GrandAmount + CGSTAmount + SGSTAmount + IGSTAmount + TCSAmount;

                // Add a blank row for separation between the HSN table and the Grand Total
                //row++;

                // Add the Grand Total row
                // Prepare the response for download
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xlsx");

                // Write the Excel workbook to the response
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }


        //public void ExportExcell()
        //{
        //    // Set the filename for the Excel download
        //    string filename = ddltype.Text + "_Register " + DateTime.Now.ToShortDateString();
        //    DataTable dt = GetData("[ExcelEncLive].[SP_RegisterReport]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        //    DataTable dtHSN = GetHSNSummaryData("[ExcelEncLive].[SP_HSNRegisterReport]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);

        //    if (dt.Rows.Count > 0)
        //    {
        //        btn.Visible = true;
        //    }
        //    else
        //    {
        //        btn.Visible = false;
        //    }
        //    dgvRegisterReport.DataSource = dt;
        //    dgvRegisterReport.DataBind();
        //    dgvRegisterReport.EmptyDataText = "Record Not Found";





        //    // Initialize total variables
        //    double BasicAmount = 0;
        //    double GSTTotAmount = 0;
        //    double GrandAmount = 0;
        //    double CGSTAmount = 0;
        //    double SGSTAmount = 0;
        //    double IGSTAmount = 0;
        //    double TCSAmount = 0;

        //    double HSNBasicAmt = 0;
        //    double HSNCGST = 0;
        //    double HSNSGST = 0;
        //    double HSNIGST = 0;
        //    double HSNGtot = 0;

        //    // Create a new Excel workbook
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        // Add a worksheet for Register Data
        //        var ws = wb.Worksheets.Add("Register Data");

        //        var registerHeadings = new string[]
        //{
        //    "Party", "Type", "GSTNumber", "VoucherType", "Date", "RefNo", "RefDate",
        //    "TCS", "Qty", "BasicTotal", "CGST", "CGSTAmt", "SGST", "IGST", "IGSTAmt", "GSTAmount", "Status"
        //};

        //        // Set the column headers
        //        for (int col = 1; col <= registerHeadings.Length; col++)
        //        {
        //            ws.Cell(1, col).Value = registerHeadings[col - 1];
        //            ws.Cell(1, col).Style.Font.SetBold();
        //        }

        //        int row = 2; // Start from row 2

        //        // Iterate through each row in the GridView
        //        foreach (GridViewRow gridViewRow in dgvRegisterReport.Rows)
        //        {
        //            // Set the cell values from the GridView
        //            for (int col = 1; col <= gridViewRow.Cells.Count; col++)
        //            {
        //                // Extract data from bound fields
        //                if (dgvRegisterReport.Columns[col - 1] is BoundField)
        //                {
        //                    string cellValue = gridViewRow.Cells[col - 1].Text;
        //                    ws.Cell(row, col).Value = cellValue;
        //                }
        //            }

        //            // Update total amounts if necessary
        //            // (you can add this part as per your requirements)

        //            ws.Range(row, 1, row, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            ws.Range(row, 1, row, 17).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //            ws.Range(row, 1, row, 17).Style.Font.Bold = true;

        //            row++;
        //        }

        //        // Add a total row
        //        row++;
        //        ws.Cell(row, 1).Value = "TOTAL";
        //        ws.Cell(row, 8).Value = BasicAmount;
        //        ws.Cell(row, 10).Value = GSTTotAmount;
        //        ws.Cell(row, 12).Value = CGSTAmount;
        //        ws.Cell(row, 14).Value = IGSTAmount;
        //        ws.Cell(row, 16).Value = GrandAmount;

        //        ws.Range(row, 1, row, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range(row, 1, row, 17).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range(row, 1, row, 17).Style.Font.Bold = true;

        //        // Add a separator row
        //        row += 2;

        //        // Add column headers for HSN summary
        //        var hsnHeadings = new string[]
        //        {
        //            "HSN", "Qty", "UOM", "BasicTotal", "CGST", "SGST", "IGST", "SGST"
        //        };

        //        for (int col = 1; col <= hsnHeadings.Length; col++)
        //        {
        //            ws.Cell(row, col).Value = hsnHeadings[col - 1];
        //            ws.Cell(row, col).Style.Font.SetBold();
        //        }

        //        row++;

        //        foreach (DataRow dataRow in dtHSN.Rows)
        //        {
        //            ws.Cell(row, 1).Value = dataRow["HSN"];
        //            ws.Cell(row, 2).Value = dataRow["Qty"];
        //            ws.Cell(row, 3).Value = dataRow["UOM"];
        //            ws.Cell(row, 4).Value = dataRow["BasicTotal"];
        //            ws.Cell(row, 5).Value = dataRow["CGST"];
        //            ws.Cell(row, 6).Value = dataRow["SGST"];
        //            ws.Cell(row, 7).Value = dataRow["IGST"];
        //            ws.Cell(row, 8).Value = dataRow["SGST"];

        //            ws.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            ws.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //            ws.Range(row, 1, row, 8).Style.Font.Bold = true;

        //            // Update HSN totals
        //            HSNBasicAmt += Convert.ToDouble(dataRow["BasicTotal"]);
        //            HSNCGST += Convert.ToDouble(dataRow["CGST"]);
        //            HSNSGST += Convert.ToDouble(dataRow["SGST"]);
        //            HSNIGST += Convert.ToDouble(dataRow["IGST"]);
        //            var Gtot = Convert.ToDouble(dataRow["BasicTotal"].ToString()) + Convert.ToDouble(dataRow["CGST"].ToString()) + Convert.ToDouble(dataRow["SGST"].ToString()) + Convert.ToDouble(dataRow["IGST"].ToString());
        //            HSNGtot += Convert.ToDouble(Gtot);

        //            row++;
        //        }

        //        // Add a total row for HSN summary
        //        row++;
        //        ws.Cell(row, 1).Value = "TOTAL";
        //        ws.Cell(row, 4).Value = HSNBasicAmt;
        //        ws.Cell(row, 6).Value = HSNCGST;
        //        ws.Cell(row, 8).Value = HSNIGST;

        //        ws.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range(row, 1, row, 8).Style.Font.Bold = true;

        //        // Add a blank row for separation
        //        row += 2;

        //        // Add the Grand Total row
        //        row++;
        //        ws.Cell(row, 1).Value = "GRAND TOTAL";
        //        ws.Cell(row, 2).Value = HSNGtot;

        //        ws.Cell(row, 1).Style.Font.FontColor = XLColor.Red;
        //        ws.Cell(row, 2).Style.Font.FontColor = XLColor.Red;

        //        // Prepare the response for download
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xlsx");

        //        // Write the Excel workbook to the response
        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //}

        //private string GetColumnName(int col)
        //{
        //    switch (col)
        //    {
        //        case 1: return "BillingCustomer";
        //        case 2: return "Type";
        //        case 3: return "GSTNumber";
        //        case 4: return "VoucherType";
        //        case 5: return "RefDate";
        //        case 6: return "RefNo";
        //        case 7: return "RefDate";
        //        case 8: return "TCSAmt";
        //        case 9: return "Qty";
        //        case 10: return "BasicTotal";
        //        case 11: return "CGST";
        //        case 12: return "CGSTAmt";
        //        case 13: return "SGST";
        //        case 14: return "IGST";
        //        case 15: return "IGSTAmt";
        //        case 16: return "GSTAmount";
        //        case 17: return "Status";
        //        default: return "";
        //    }
        //}

        //private string GetHSNColumnName(int col)
        //{
        //    switch (col)
        //    {
        //        case 1: return "HSN";
        //        case 2: return "Qty";
        //        case 3: return "UOM";
        //        case 4: return "BasicTotal";
        //        case 5: return "CGST";
        //        case 6: return "SGST";
        //        case 7: return "IGST";
        //        case 8: return "SGST";
        //        default: return "";
        //    }
        //}



        //public void ExportExcell()
        //{
        //    // Set the filename for the Excel download
        //    string filename = ddltype.Text + "_Register " + DateTime.Now.ToShortDateString();
        //    DataTable dt = GetData("[ExcelEncLive].[SP_RegisterReport]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);
        //    DataTable dtHSN = GetHSNSummaryData("[ExcelEncLive].[SP_HSNRegisterReport]", txtPartyName.Text, txtfromdate.Text, txttodate.Text, ddltype.Text);

        //    // Initialize total variables
        //    double BasicAmount = 0;
        //    double GSTTotAmount = 0;
        //    double GrandAmount = 0;
        //    double CGSTAmount = 0;
        //    double SGSTAmount = 0;
        //    double IGSTAmount = 0;
        //    double TCSAmount = 0;

        //    double HSNBasicAmt = 0;
        //    double HSNCGST = 0;
        //    double HSNSGST = 0;
        //    double HSNIGST = 0;
        //    double HSNGtot = 0;

        //    // Create a new Excel workbook
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        // Add a worksheet for Register Data
        //        var ws = wb.Worksheets.Add("Register Data");
        //        // Add a name/header for the upper GridView
        //        ws.Range("A1:Q1").Merge().Value = "Excel Enclosures";
        //        ws.Range("A1:Q1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Range("A1:Q1").Style.Font.Bold = true;
        //        ws.Range("A1:Q1").Style.Font.FontSize = 20;

        //        ws.Range("A2:Q2").Merge().Value = "Sales Register";
        //        ws.Range("A2:Q2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Range("A2:Q2").Style.Font.Bold = true;
        //        ws.Range("A2:Q2").Style.Font.FontSize = 15;

        //        // Add column headers for dgvRegisterReport
        //        ws.Cell(3, 1).Value = "Party";
        //        ws.Cell(3, 2).Value = "Type";
        //        // Add other column headers here...
        //        ws.Cell(3, 17).Value = "Status";

        //        ws.Cell(3, 1).Style.Font.Bold = true;
        //        //ws.Cell(3, 2).Style Font.Bold = true;
        //        // Add other column header styles...

        //        int row = 4; // Start from row 4

        //        // Initialize the DataTable columns
        //        dt.Columns.Add("Party");
        //        dt.Columns.Add("Type");
        //        // Add other column names...

        //        // Loop through the GridView rows
        //        foreach (GridViewRow gridViewRow in dgvPurchaseRegisterReport.Rows)
        //        {
        //            // Extract data from GridView cells
        //            string Party = gridViewRow.Cells[0].Text.Replace("&amp;", "&");
        //            Label lblType = (Label)gridViewRow.FindControl("lblType");
        //            string Type = lblType.Text;
        //            // Extract other data...

        //            // Add data to the DataTable
        //            dt.Rows.Add(Party, Type, /* Add other data here... */);

        //            // Update total amounts
        //            BasicAmount += Convert.ToDouble(BasicTotal);
        //            GSTTotAmount += Convert.ToDouble(GSTAmount);
        //            GrandAmount += Convert.ToDouble(GrandTotal);

        //            // Set borders and styles for the row
        //            ws.Range(row, 1, row, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            ws.Range(row, 1, row, 17).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //            ws.Range(row, 1, row, 17).Style.Font.Bold = true;

        //            row++;
        //        }

        //        // Add the total row to the DataTable
        //        dt.Rows.Add("", "", "", "", "", "", "", "", "TOTAL", "", "", "", "", GSTTotAmount, "", "");

        //        // Set the row values for TOTAL
        //        ws.Cell(row, 1).Value = "TOTAL";
        //        ws.Cell(row, 2).Value = "";
        //        // Set other values for TOTAL...
        //        ws.Cell(row, 16).Value = GrandAmount;
        //        ws.Cell(row, 17).Value = "";

        //        // Set borders and styles for the TOTAL row
        //        ws.Range(row, 1, row, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range(row, 1, row, 17).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range(row, 1, row, 17).Style.Font.Bold = true;

        //        for (int i = 0; i < 4; i++)
        //        {
        //            row++;
        //        }

        //        // Add column headers for dgvHSNSummary
        //        ws.Cell(row, 1).Value = "HSN";
        //        ws.Cell(row, 2).Value = "Qty";
        //        // Add other column headers for HSN data...

        //        // Set borders and styles for HSN column headers
        //        ws.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //        ws.Range(row, 1, row, 8).Style.Font.Bold = true;

        //        row++; // Start from row 2 for HSN data

        //        // Loop through the HSN DataTable
        //        foreach (DataRow dataRow in dtHSN.Rows)
        //        {
        //            // Extract data from the HSN DataTable
        //            ws.Cell(row, 1).Value = dataRow["HSN"];
        //            ws.Cell(row, 2).Value = dataRow["Qty"];
        //            // Extract other data...

        //            // Set borders and styles for the HSN row
        //            ws.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            ws.Range(row, 1, row, 8).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //            ws.Range(row, 1, row, 8).Style.Font.Bold = true;

        //            // Update HSN totals
        //            HSNBasicAmt += Convert.ToDouble(dataRow["BasicTotal"]);
        //            HSNCGST += Convert.ToDouble(dataRow["CGST"]);
        //            HSNSGST += Convert.ToDouble(dataRow["SGST"]);
        //            HSNIGST += Convert.ToDouble(dataRow["IGST"]);
        //            var Gtot = Convert.ToDouble(dataRow["BasicTotal"].ToString()) + Convert.ToDouble(dataRow["CGST"].ToString()) + Convert.ToDouble(dataRow["SGST"].ToString()) + Convert.ToDouble(dataRow["IGST"].ToString());
        //            HSNGtot += Convert.ToDouble(Gtot);

        //            row++;
        //        }

        //        // Add a total row for HSN data
        //        ws.Cell(row, 1).Value = "TOTAL";
        //        ws.Cell(row, 2).Value = "";
        //        // Set other values for TOTAL in HSN data...
        //        ws.Cell(row, 8).Value = HSNIGST;

        //        // Set borders and styles for the TOTAL row in HSN data
        //        ws.Range(row, 1, row, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws




    }

    protected void GVReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ////if (e.Row.RowType == DataControlRowType.DataRow)
        ////{
        //string ID = GVReports.DataKeys[e.Row.RowIndex].Values[0].ToString();
        //con.Open();

        //string cname = e.Row.Cells[0].Text.Replace("&amp;", "&");
        ////string basicamt = e.Row.Cells[8].Text;

        ////BAsicamt += Convert.ToDecimal(basicamt);

        //SqlCommand cmd = new SqlCommand("select gstno from company where cname='" + cname + "' and status=0", con);
        //string gstno = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

        //SqlCommand cmdbasic = new SqlCommand("SELECT SUM(CAST(TaxableAmt as float)) FROM [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string basicamt = cmdbasic.ExecuteScalar().ToString();

        //SqlCommand cmdFrightbasic = new SqlCommand("SELECT Basic as FBasic FROM [ExcelEncLive].tblTaxInvoiceHdr where Id='" + ID + "'", con);
        //string Frightbasicamt = cmdFrightbasic.ExecuteScalar().ToString();

        //SqlCommand cmdcgst = new SqlCommand(" SELECT top 1 CGSTPer FROM [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string cgst = cmdcgst.ExecuteScalar().ToString();

        //// New Changes 18-7-23
        //SqlCommand cmdcgstAmt = new SqlCommand("select SUM(cast(CGSTAmt as float))from [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string cgstAmt = cmdcgstAmt.ExecuteScalar().ToString();

        //SqlCommand cmdsgst = new SqlCommand(" SELECT top 1 SGSTPer FROM [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string sgst = cmdsgst.ExecuteScalar().ToString();

        //// New Changes 18-7-23
        //SqlCommand cmdsgstAmt = new SqlCommand("select SUM(cast(SGSTAmt as float))from [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string sgstAmt = cmdsgstAmt.ExecuteScalar().ToString();

        //SqlCommand cmdigst = new SqlCommand(" SELECT top 1 IGSTPer FROM [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string igst = cmdigst.ExecuteScalar().ToString();

        //// New Changes 18-7-23
        //SqlCommand cmdigstAmt = new SqlCommand("select SUM(cast(IGSTAmt as float))from [ExcelEncLive]. tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string igstAmt = cmdigstAmt.ExecuteScalar().ToString();

        //SqlCommand cmdqty = new SqlCommand("SELECT SUM(CAST(Qty as bigint)) as Qty FROM [ExcelEncLive]. tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string Qty = cmdqty.ExecuteScalar().ToString();

        ////// New change 20/12/2023
        //SqlCommand cmdgstamtbasic = new SqlCommand("select ((CAST (tblTaxInvoiceHdr.Cost as float))-(CAST (tblTaxInvoiceHdr.Basic as float))) from [ExcelEncLive].tblTaxInvoiceHdr where Id='" + ID + "'", con);
        //string BasicGSTAmount = cmdgstamtbasic.ExecuteScalar().ToString();
        /////

        //SqlCommand cmdgstamt = new SqlCommand("SELECT SUM(CAST(CGSTAmt as float))+SUM(CAST(SGSTAmt as float)) + SUM(CAST(IGSTAmt as float)) as GSTAmount FROM [ExcelEncLive].tblTaxInvoiceDtls where HeaderID='" + ID + "'", con);
        //string GSTAmount = cmdgstamt.ExecuteScalar().ToString();

        //con.Close();
        //Label lblCGST = (Label)e.Row.FindControl("lblCGST");
        //lblCGST.Text = cgst;

        //// New Changes 18-7-23
        //Label lblCGSTAmt = (Label)e.Row.FindControl("lblCGSTAmt");
        //lblCGSTAmt.Text = cgstAmt;

        //Label lblSGST = (Label)e.Row.FindControl("lblSGST");
        //lblSGST.Text = sgst;

        //// New Changes 18-7-23
        //Label lblSGSTAmt = (Label)e.Row.FindControl("lblSGSTAmt");
        //lblSGSTAmt.Text = sgstAmt;

        //Label lblIGST = (Label)e.Row.FindControl("lblIGST");
        //lblIGST.Text = igst;

        //// New Changes 18-7-23
        //Label lblIGSTAmt = (Label)e.Row.FindControl("lblIGSTAmt");
        //lblIGSTAmt.Text = igstAmt;

        //Label lblQty = (Label)e.Row.FindControl("lblQty");
        //lblQty.Text = Qty;

        //Label lblBasicTotal = (Label)e.Row.FindControl("lblBasicTotal");
        //var basictot = Convert.ToDouble(basicamt) + Convert.ToDouble(Frightbasicamt);
        ////lblBasicTotal.Text = Math.Round(Convert.ToDouble(basictot)).ToString();
        //lblBasicTotal.Text = Convert.ToDouble(basictot).ToString();

        //Label lblGSTAmount = (Label)e.Row.FindControl("lblGSTAmount");
        ////lblGSTAmount.Text = Math.Round(Convert.ToDouble(GSTAmount)).ToString();

        ///////////// New change 20/12/2023
        //var basicGSTtot = Convert.ToDouble(GSTAmount) + Convert.ToDouble(BasicGSTAmount);
        ////var basicGSTtot = Convert.ToDouble(GSTAmount) + Convert.ToDouble(BasicGSTAmount);
        ////lblGSTAmount.Text = Math.Round(Convert.ToDouble(basicGSTtot)).ToString();
        //lblGSTAmount.Text = Convert.ToDouble(basicGSTtot).ToString();
        ////////

        //Label lblGSTNumber = (Label)e.Row.FindControl("lblGSTNumber");
        //lblGSTNumber.Text = gstno;

        //Label lblStatus = (Label)e.Row.FindControl("lblStatus");
        //if (lblStatus.Text != "")
        //{
        //    lblStatus.Text = "Paid";
        //}
        //else
        //{
        //    lblStatus.Text = "Raised";
        //}

        //Label lblDocNo = (Label)e.Row.FindControl("lblDocNo");
        //Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
        //if (lblDocNo.Text != "")
        //{
        //    lblInvoiceNo.Text = lblDocNo.Text;
        //}
        //else
        //{

        //}
		
    }

}



