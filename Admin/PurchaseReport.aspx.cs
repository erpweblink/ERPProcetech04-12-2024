using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;

public partial class Admin_PurchaseReport : System.Web.UI.Page
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
                DivRoot.Visible = false;

            }
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseReport.aspx");
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

                com.CommandText = "select DISTINCT SupplierName from tblSupplierMaster where " + "SupplierName like @Search + '%'  ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> SupplierName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        SupplierName.Add(sdr["SupplierName"].ToString());
                    }
                }
                con.Close();
                return SupplierName;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemList(string prefixText, int count)
    {
        return AutoFilItem(prefixText);
    }

    public static List<string> AutoFilItem(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [ItemName] from tblItemMaster where " + "ItemName like '%'+ @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                //com.Parameters.AddWithValue("@SName", sName);
                com.Connection = con;
                con.Open();
                List<string> Items = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Items.Add(sdr["ItemName"].ToString());
                    }
                }
                con.Close();
                return Items;
            }
        }
    }
    private void BindData()
    {
        using (SqlCommand Cmd = new SqlCommand("SP_SalesPurchaseReport", con))
        {
            using (SqlDataAdapter Da = new SqlDataAdapter())
            {

                if (ddltype.SelectedValue == "0" && txtPartyName.Text == "" && txtfromdate.Text == "" && txttodate.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select atleast one filter.');", true);
                }
                else
                {
                    Cmd.Connection = con;
                    Cmd.CommandType = CommandType.StoredProcedure;
                    string fromdateString, todateString;
                    if (txtfromdate.Text != "" && txttodate.Text != "")
                    {
                        //DateTime fromdate = Convert.ToDateTime(txtfromdate.Text.Trim());
                        //fromdateString = fromdate.ToString("M/d/yyyy", CultureInfo.InvariantCulture);

                        // DateTime todate = Convert.ToDateTime(txttodate.Text.Trim());
                        //todateString = todate.ToString("M/d/yyyy", CultureInfo.InvariantCulture);

                        // string strDate = txtfromdate.Text.Trim();
                        // string[] dateString = strDate.Split('-');
                        // DateTime enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);

                        // fromdateString = enter_date.ToString("M/d/yyyy", CultureInfo.InvariantCulture);

                        //var fromtime = Convert.ToDateTime(txtfromdate.Text.Trim());
                        //fromdateString = fromtime.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);

                        //var tttime = Convert.ToDateTime(txttodate.Text.Trim());
                        //DateTime date = Convert.ToDateTime(txttodate.Text.Trim(),                          System.Globalization.CultureInfo.GetCultureInfo("urPK").DateTimeFormat);
                        //todateString = date.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);//tttime.ToString("MM/dd/yyyy");

						string Fromdate = txtfromdate.Text;
                        IFormatProvider culture = new CultureInfo("en-US", true);
                        //DateTime FromdateVal = DateTime.ParseExact(Fromdate, "dd-MM-yyyy", culture);

                        string Todate = txttodate.Text.Trim();
                        IFormatProvider culture1 = new CultureInfo("en-US", true);
                        //DateTime TodateVal = DateTime.ParseExact(Todate, "dd-MM-yyyy", culture1);
						
						
                        //Cmd.Parameters.AddWithValue("@Fromdate", fromdateString);
                        //Cmd.Parameters.AddWithValue("@ToDate", todateString);
						
						 Cmd.Parameters.AddWithValue("@Fromdate", Fromdate);
                        Cmd.Parameters.AddWithValue("@ToDate", Todate);
                    }
                    else
                    {
                        Cmd.Parameters.AddWithValue("@Fromdate", DBNull.Value);
                        Cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                    }

                    Cmd.Parameters.AddWithValue("@SupplierName", txtPartyName.Text);
                    if (txtPONo.Text == "")
                        Cmd.Parameters.AddWithValue("@PONo", DBNull.Value);
                    else
                        Cmd.Parameters.AddWithValue("@PONo", txtPONo.Text.Trim());

                    if (txtSupplierBill.Text == "")
                        Cmd.Parameters.AddWithValue("@SupplierBill", DBNull.Value);
                    else
                        Cmd.Parameters.AddWithValue("@SupplierBill", txtSupplierBill.Text.Trim());

                    if (txtBillNo.Text == "")
                        Cmd.Parameters.AddWithValue("@BillNo", DBNull.Value);
                    else
                        Cmd.Parameters.AddWithValue("@BillNo", txtBillNo.Text.Trim());

                    if (txtByItem.Text == "")
                        Cmd.Parameters.AddWithValue("@Item", DBNull.Value);
                    else
                        Cmd.Parameters.AddWithValue("@Item", txtByItem.Text.Trim());

                    if (txtCreditDebitNo.Text == "")
                        Cmd.Parameters.AddWithValue("@CreditDebitNo", DBNull.Value);
                    else
                        Cmd.Parameters.AddWithValue("@CreditDebitNo", txtCreditDebitNo.Text.Trim());

                    if (ddltype.Text == "PO")
                    {

                        DivRoot.Visible = true;
                        dgvPurchaseOrder.Visible = true;
                        dgvPurchaseBill.Visible = false;
                        Cmd.Parameters.AddWithValue("@Type", ddltype.Text);
                        Da.SelectCommand = Cmd;
                        using (DataTable Dt = new DataTable())
                        {
                            Da.Fill(Dt);

                            dgvPurchaseOrder.DataSource = Dt;
                            dgvPurchaseOrder.EmptyDataText = "Record Not Found";
                            dgvPurchaseOrder.DataBind();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvPurchaseOrder.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
                            exportbtn.Visible = true;
                        }

                    }
                    else if (ddltype.Text == "CreditDebitNote")
                    {
                        DivRoot.Visible = true;
                        dgvPurchaseOrder.Visible = false;
                        dgvPurchaseBill.Visible = false;
                        dgvCreditDebit.Visible = true;
                        Cmd.Parameters.AddWithValue("@Type", ddltype.Text);
                        Da.SelectCommand = Cmd;
                        using (DataTable Dt = new DataTable())
                        {
                            Da.Fill(Dt);

                            dgvCreditDebit.DataSource = Dt;
                            dgvCreditDebit.EmptyDataText = "Record Not Found";
                            dgvCreditDebit.DataBind();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvCreditDebit.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
                            exportbtn.Visible = true;
                        }
                    }
                    else
                    {

                        DivRoot.Visible = true;
                        dgvPurchaseBill.Visible = true;
                        dgvPurchaseOrder.Visible = false;
                        Cmd.Parameters.AddWithValue("@Type", ddltype.Text);
                        Da.SelectCommand = Cmd;
                        using (DataTable Dt = new DataTable())
                        {
                            Da.Fill(Dt);
                            dgvPurchaseBill.DataSource = Dt;
                            dgvPurchaseBill.EmptyDataText = "Record Not Found";
                            dgvPurchaseBill.DataBind();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvPurchaseBill.ClientID + "', 500, 1020 , 40 ,true); </script>", false);
                            exportbtn.Visible = true;
                        }

                    }
                }
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    //Export To Excel
    private void PurchaseOrderExport_Excel()
    {
        //bool exists = false;
        DataTable dt = new DataTable("PurchaseOrder");
        GridView gvOrders = (GridView)dgvPurchaseOrder.Rows[1].FindControl("gvPODetails");
        foreach (TableCell cell in dgvPurchaseOrder.HeaderRow.Cells)
        {
            if (cell.Text == "Action")
            {
            }
            else
            {
                dt.Columns.Add(cell.Text);
            }
        }
        foreach (TableCell cell in gvOrders.HeaderRow.Cells)
        {
            dt.Columns.Add(cell.Text);
        }
        dt.Columns.RemoveAt(0);

        int SN = 0;
        double BasicAmount = 0;
        double Qty = 0;
        double Rate = 0;
        double GrandTotal = 0;
        foreach (GridViewRow row in dgvPurchaseOrder.Rows)
        {

            string BillDate = row.Cells[2].Text == "&nbsp;" ? "" : row.Cells[2].Text;
            string Supplierbill = row.Cells[3].Text == "&nbsp;" ? "" : row.Cells[3].Text;
            string SupplierName = row.Cells[4].Text == "&nbsp;" ? "" : row.Cells[4].Text;
            Label TotalAmt = row.FindControl("lblGrandAmount") as Label;
            string TotalAmount = TotalAmt.Text;

            GridView gvOrderscell = (row.FindControl("gvPODetails") as GridView);
            for (int j = 0; j < gvOrderscell.Rows.Count; j++)
            {
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = dt.Columns["SNo"];
                dt.PrimaryKey = keyColumns;

                bool exists = dt.Select().ToList().Exists(rows => rows["PO No"].ToString().ToUpper() == Supplierbill);
                if (exists)
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }

                if (exists == true)
                {
                    dt.Rows.Add(SN++, "", "", "", "", gvOrderscell.Rows[j].Cells[0].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[0].Text, gvOrderscell.Rows[j].Cells[1].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[1].Text, gvOrderscell.Rows[j].Cells[2].Text, gvOrderscell.Rows[j].Cells[3].Text, gvOrderscell.Rows[j].Cells[4].Text, gvOrderscell.Rows[j].Cells[5].Text, gvOrderscell.Rows[j].Cells[6].Text == "&nbsp;" ? "0" : gvOrderscell.Rows[j].Cells[6].Text, gvOrderscell.Rows[j].Cells[7].Text, gvOrderscell.Rows[j].Cells[8].Text, gvOrderscell.Rows[j].Cells[9].Text, gvOrderscell.Rows[j].Cells[10].Text, gvOrderscell.Rows[j].Cells[11].Text);
                }
                else
                {
                    dt.Rows.Add(SN++, BillDate, Supplierbill, SupplierName, TotalAmount, gvOrderscell.Rows[j].Cells[0].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[0].Text, gvOrderscell.Rows[j].Cells[1].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[1].Text, gvOrderscell.Rows[j].Cells[2].Text, gvOrderscell.Rows[j].Cells[3].Text, gvOrderscell.Rows[j].Cells[4].Text, gvOrderscell.Rows[j].Cells[5].Text, gvOrderscell.Rows[j].Cells[6].Text == "&nbsp;" ? "0" : gvOrderscell.Rows[j].Cells[6].Text, gvOrderscell.Rows[j].Cells[7].Text, gvOrderscell.Rows[j].Cells[8].Text, gvOrderscell.Rows[j].Cells[9].Text, gvOrderscell.Rows[j].Cells[10].Text, gvOrderscell.Rows[j].Cells[11].Text);
                }
                BasicAmount += Convert.ToDouble(gvOrderscell.Rows[j].Cells[7].Text);
                Qty += Convert.ToDouble(gvOrderscell.Rows[j].Cells[3].Text);
                Rate += Convert.ToDouble(gvOrderscell.Rows[j].Cells[5].Text);
                GrandTotal += Convert.ToDouble(gvOrderscell.Rows[j].Cells[11].Text);
            }
        }

        dt.Rows.Add(SN++, "", "", "", "", "", "", "TOTAL", Qty, "", Rate, "", BasicAmount, "", "", "", GrandTotal);

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PurchaseOrder.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                string path = Server.MapPath("~") + "/files/PurchaseOrder.xlsx";
                wb.SaveAs(MyMemoryStream);
                wb.SaveAs(path);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    private void PurchaseBillExport_Excel()
    {
        //bool exists = false;
        DataTable dt = new DataTable("PurchaseBill");
        GridView gvOrders = (GridView)dgvPurchaseBill.Rows[1].FindControl("gvPBillDetails");
        foreach (TableCell cell in dgvPurchaseBill.HeaderRow.Cells)
        {
            if (cell.Text == "Action")
            {
            }
            else
            {
                dt.Columns.Add(cell.Text);
            }
        }
        foreach (TableCell cell in gvOrders.HeaderRow.Cells)
        {
            dt.Columns.Add(cell.Text);
        }
        dt.Columns.RemoveAt(0);

        int SN = 0;
        double BasicAmount = 0;
        double Qty = 0;
        double Rate = 0;
        double GrandTotal = 0;
        foreach (GridViewRow row in dgvPurchaseBill.Rows)
        {

            string BillDate = row.Cells[2].Text == "&nbsp;" ? "" : row.Cells[2].Text;
            string Supplierbill = row.Cells[3].Text == "&nbsp;" ? "" : row.Cells[3].Text;
            string SupplierName = row.Cells[4].Text == "&nbsp;" ? "" : row.Cells[4].Text;
            Label TotalAmt = row.FindControl("lblGrandAmountpurchaseBill") as Label;
            string TotalAmount = TotalAmt.Text;

            GridView gvOrderscell = (row.FindControl("gvPBillDetails") as GridView);
            for (int j = 0; j < gvOrderscell.Rows.Count; j++)
            {
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = dt.Columns["SNo"];
                dt.PrimaryKey = keyColumns;

                bool exists = dt.Select().ToList().Exists(rows => rows["Supplier BillNo"].ToString().ToUpper() == Supplierbill);
                if (exists)
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }

                if (exists == true)
                {
                    dt.Rows.Add(SN++, "", "", "", "", gvOrderscell.Rows[j].Cells[0].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[0].Text, gvOrderscell.Rows[j].Cells[1].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[1].Text, gvOrderscell.Rows[j].Cells[2].Text, gvOrderscell.Rows[j].Cells[3].Text, gvOrderscell.Rows[j].Cells[4].Text, gvOrderscell.Rows[j].Cells[5].Text, gvOrderscell.Rows[j].Cells[6].Text == "&nbsp;" ? "0" : gvOrderscell.Rows[j].Cells[6].Text, gvOrderscell.Rows[j].Cells[7].Text, gvOrderscell.Rows[j].Cells[8].Text, gvOrderscell.Rows[j].Cells[9].Text, gvOrderscell.Rows[j].Cells[10].Text, gvOrderscell.Rows[j].Cells[11].Text);
                }
                else
                {
                    dt.Rows.Add(SN++, BillDate, Supplierbill, SupplierName, TotalAmount, gvOrderscell.Rows[j].Cells[0].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[0].Text, gvOrderscell.Rows[j].Cells[1].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[1].Text, gvOrderscell.Rows[j].Cells[2].Text, gvOrderscell.Rows[j].Cells[3].Text, gvOrderscell.Rows[j].Cells[4].Text, gvOrderscell.Rows[j].Cells[5].Text, gvOrderscell.Rows[j].Cells[6].Text == "&nbsp;" ? "0" : gvOrderscell.Rows[j].Cells[6].Text, gvOrderscell.Rows[j].Cells[7].Text, gvOrderscell.Rows[j].Cells[8].Text, gvOrderscell.Rows[j].Cells[9].Text, gvOrderscell.Rows[j].Cells[10].Text, gvOrderscell.Rows[j].Cells[11].Text);
                }

                BasicAmount += Convert.ToDouble(gvOrderscell.Rows[j].Cells[7].Text);
                Qty += Convert.ToDouble(gvOrderscell.Rows[j].Cells[3].Text);
                Rate += Convert.ToDouble(gvOrderscell.Rows[j].Cells[5].Text);
                GrandTotal += Convert.ToDouble(gvOrderscell.Rows[j].Cells[11].Text);
            }
        }
        dt.Rows.Add(SN++, "", "", "", "", "", "", "TOTAL", Qty, "", Rate, "", BasicAmount, "", "", "", GrandTotal);

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PurchaseBill.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                string path = Server.MapPath("~") + "/files/PurchaseBill.xlsx";
                wb.SaveAs(MyMemoryStream);
                wb.SaveAs(path);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    private void CreditDebitExport_Excel()
    {
        //bool exists = false;
        DataTable dt = new DataTable("CreditDebit");
        GridView gvOrders = (GridView)dgvCreditDebit.Rows[0].FindControl("gvCreditDebitDetails");
        foreach (TableCell cell in dgvCreditDebit.HeaderRow.Cells)
        {
            if (cell.Text == "Action")
            {
            }
            else
            {
                dt.Columns.Add(cell.Text);
            }
        }
        foreach (TableCell cell in gvOrders.HeaderRow.Cells)
        {
            dt.Columns.Add(cell.Text);
        }
        dt.Columns.RemoveAt(0);

        int SN = 0;
        double BasicAmount = 0;
        double Qty = 0;
        double Rate = 0;
        double GrandTotal = 0;
        foreach (GridViewRow row in dgvCreditDebit.Rows)
        {
            string NoteType = row.Cells[2].Text == "&nbsp;" ? "" : row.Cells[2].Text;
            string docNo = row.Cells[3].Text == "&nbsp;" ? "" : row.Cells[3].Text;
            string DocDate = row.Cells[4].Text == "&nbsp;" ? "" : row.Cells[4].Text;
            string SupplierName = row.Cells[5].Text == "&nbsp;" ? "" : row.Cells[5].Text;
            Label TotalAmt = row.FindControl("lblGrandAmount") as Label;
            string TotalAmount = TotalAmt.Text;

            GridView gvOrderscell = (row.FindControl("gvCreditDebitDetails") as GridView);
            for (int j = 0; j < gvOrderscell.Rows.Count; j++)
            {
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = dt.Columns["SNo"];
                dt.PrimaryKey = keyColumns;

                bool exists = dt.Select().ToList().Exists(rows => rows["Doc No"].ToString().ToUpper() == docNo);
                if (exists)
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }

                if (exists == true)
                {
                    dt.Rows.Add(SN++, "", "", "", "", "", gvOrderscell.Rows[j].Cells[0].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[0].Text, gvOrderscell.Rows[j].Cells[1].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[1].Text, gvOrderscell.Rows[j].Cells[2].Text, gvOrderscell.Rows[j].Cells[3].Text, gvOrderscell.Rows[j].Cells[4].Text, gvOrderscell.Rows[j].Cells[5].Text, gvOrderscell.Rows[j].Cells[6].Text == "&nbsp;" ? "0" : gvOrderscell.Rows[j].Cells[6].Text, gvOrderscell.Rows[j].Cells[7].Text, gvOrderscell.Rows[j].Cells[8].Text, gvOrderscell.Rows[j].Cells[9].Text, gvOrderscell.Rows[j].Cells[10].Text);
                }
                else
                {
                    dt.Rows.Add(SN++, NoteType, docNo, DocDate, SupplierName, TotalAmount, gvOrderscell.Rows[j].Cells[0].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[0].Text, gvOrderscell.Rows[j].Cells[1].Text == "&nbsp;" ? "" : gvOrderscell.Rows[j].Cells[1].Text, gvOrderscell.Rows[j].Cells[2].Text, gvOrderscell.Rows[j].Cells[3].Text, gvOrderscell.Rows[j].Cells[4].Text, gvOrderscell.Rows[j].Cells[5].Text, gvOrderscell.Rows[j].Cells[6].Text == "&nbsp;" ? "0" : gvOrderscell.Rows[j].Cells[6].Text, gvOrderscell.Rows[j].Cells[7].Text, gvOrderscell.Rows[j].Cells[8].Text, gvOrderscell.Rows[j].Cells[9].Text, gvOrderscell.Rows[j].Cells[10].Text);  /* gvOrderscell.Rows[j].Cells[11].Text*/
                }

                BasicAmount += Convert.ToDouble(gvOrderscell.Rows[j].Cells[6].Text);
                Qty += Convert.ToDouble(gvOrderscell.Rows[j].Cells[3].Text);
                Rate += Convert.ToDouble(gvOrderscell.Rows[j].Cells[4].Text);
                GrandTotal += Convert.ToDouble(gvOrderscell.Rows[j].Cells[10].Text);
            }
        }
        dt.Rows.Add(SN++, "", "", "", "", "", "", "", "TOTAL", Qty, Rate, "", BasicAmount, "", "", "", GrandTotal);

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=CreditDebitNote.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                string path = Server.MapPath("~") + "/files/CreditDebitNote.xlsx";
                wb.SaveAs(MyMemoryStream);
                wb.SaveAs(path);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    //---------------- 

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        if (ddltype.Text == "PO")
        {
            PurchaseOrderExport_Excel();
        }
        else if (ddltype.Text == "CreditDebitNote")
        {
            CreditDebitExport_Excel();
        }
        else
        {
            PurchaseBillExport_Excel();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void dgvPurchaseOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();
                Response.Write("<script>window.open('PurchaseOrderPDF.aspx','_blank');</script>");

            }
        }
    }

    protected void dgvPurchaseBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();
                Response.Write("<script>window.open('PurchaseBillPDF.aspx','_blank');</script>");

            }
        }
    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        if (ddltype.SelectedItem.Text == "Purchase Order")
        {
            PurchaseOrderPDF();
        }
        else
        {
            PurchaseBillPDF();
        }

    }

    protected void PurchaseBillPDF()
    {
        string pathsave = Server.MapPath("~") + "/files/PurchaseBill.xlsx";
        string savepathsave = Server.MapPath("~") + "/files/PurchaseBill.pdf";

        //// Instantiate the Workbook object with the Excel file
        //Workbook workbook = new Workbook("SampleExcel.xls");

        //// Save the document in PDF format
        //workbook.Save("outputPDF.pdf", SaveFormat.Pdf);

        //DataTable dtexcelpurchase = new DataTable();
        //dtexcelpurchase.Columns.AddRange(new DataColumn[7]
        //{ new DataColumn("Id"),
        //       new DataColumn("SupplierBillNo"),
        //        new DataColumn("SupplierName"),
        //         new DataColumn("BillDate"),
        //          new DataColumn("GrandTotal"),
        //           new DataColumn("PaymentDueDate"),
        //           new DataColumn("CreatedBy")


        //});
        //foreach (GridViewRow row in dgvPurchaseBill.Rows)
        //{
        //    string SN = (row.Cells[1].FindControl("lblsno") as Label).Text;
        //    string BillNo = (row.Cells[1].FindControl("lblSupplierBillNo") as Label).Text;
        //    string SupplierName = (row.Cells[1].FindControl("lblSupplierName") as Label).Text;
        //    string BillDate = (row.Cells[1].FindControl("lblBillDate") as Label).Text;
        //    string GrandTotal = (row.Cells[1].FindControl("lblGrandTotal") as Label).Text;
        //    //string AgainstNumber = (row.Cells[1].FindControl("lblAgainstNumber") as Label).Text;
        //    string PaymentDate = (row.Cells[1].FindControl("lblPaymentDueDate") as Label).Text;
        //    string CreatedBy = (row.Cells[1].FindControl("lblCreatedBy") as Label).Text;
        //    dtexcelpurchase.Rows.Add(SN, BillNo, SupplierName, BillDate, GrandTotal, PaymentDate, CreatedBy);

        //}
        //System.IO.StringWriter sw = new StringWriter();
        //StringReader sr = new StringReader(sw.ToString());

        //Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);

        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "Report.pdf", FileMode.Create));

        //doc.Open();


        //PdfContentByte cb = writer.DirectContent;
        //cb.Rectangle(15f, 800f, 561f, 35f);
        //cb.Stroke();
        //// Header 
        //cb.BeginText();
        //cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 20);
        //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tax Invoice Purchase Report", 200, 812, 0);
        //cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 145, 755, 0);
        //cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
        //cb.EndText();
        //if (dtexcelpurchase.Rows.Count > 0)
        //{
        //    string BillNo = dtexcelpurchase.Rows[0]["SupplierBillNo"].ToString();
        //    string SupplierName = dtexcelpurchase.Rows[0]["SupplierName"].ToString();
        //    string Billdate = dtexcelpurchase.Rows[0]["BillDate"].ToString().TrimEnd("0:0".ToCharArray());
        //    string GrandTotal = dtexcelpurchase.Rows[0]["GrandTotal"].ToString();
        //    string PaymentDate = dtexcelpurchase.Rows[0]["PaymentDueDate"].ToString();
        //    string CreatedBy = dtexcelpurchase.Rows[0]["CreatedBy"].ToString();
        //    //string IGST = Dt.Rows[0]["IGST"].ToString();

        //    Paragraph paragraphTable1 = new Paragraph();
        //    paragraphTable1.SpacingBefore = 120f;
        //    paragraphTable1.SpacingAfter = 10f;

        //    PdfPTable table = new PdfPTable(1);

        //    float[] widths2 = new float[] { 560f };
        //    table.SetWidths(widths2);
        //    table.TotalWidth = 560f;
        //    table.LockedWidth = true;


        //    paragraphTable1.Add(table);
        //    doc.Add(paragraphTable1);

        //    Paragraph paragraphTable2 = new Paragraph();
        //    paragraphTable2.SpacingAfter = 0f;
        //    paragraphTable2.SpacingBefore = 120f;

        //    table = new PdfPTable(7);
        //    float[] widths3 = new float[] { 4f, 10f, 19f, 10f, 10f, 10f, 10f };
        //    table.SetWidths(widths3);
        //    if (dgvPurchaseBill.Rows.Count > 0)
        //    {
        //        table.TotalWidth = 560f;
        //        table.LockedWidth = true;
        //        table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //        table.AddCell(new Phrase("Bill Number", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //        table.AddCell(new Phrase("Supplier Name", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //        table.AddCell(new Phrase("Bill Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //        table.AddCell(new Phrase("Grand Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //        table.AddCell(new Phrase("Payment Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //        table.AddCell(new Phrase("Created By", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //        //table.AddCell(new Phrase("IGST", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //        int rowid = 1;
        //        foreach (DataRow dr in dtexcelpurchase.Rows)
        //        {
        //            table.TotalWidth = 560f;
        //            table.LockedWidth = true;
        //            table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 13)));
        //            table.AddCell(new Phrase(dr["SupplierBillNo"].ToString(), FontFactory.GetFont("Arial", 10)));
        //            table.AddCell(new Phrase(dr["SupplierName"].ToString(), FontFactory.GetFont("Arial", 10)));
        //            table.AddCell(new Phrase(dr["BillDate"].ToString(), FontFactory.GetFont("Arial", 10)));
        //            table.AddCell(new Phrase(dr["GrandTotal"].ToString(), FontFactory.GetFont("Arial", 10)));
        //            table.AddCell(new Phrase(dr["PaymentDueDate"].ToString(), FontFactory.GetFont("Arial", 10)));
        //            table.AddCell(new Phrase(dr["CreatedBy"].ToString(), FontFactory.GetFont("Arial", 10)));
        //            //table.AddCell(new Phrase(dr["IGST"].ToString(), FontFactory.GetFont("Arial", 10)));
        //            rowid++;
        //        }
        //    }
        //    paragraphTable2.Add(table);
        //    doc.Add(paragraphTable2);

        //    //Space
        //    Paragraph paragraphTable3 = new Paragraph();

        //    string[] items = { "Goods once sold will not be taken back or exchange. \b",
        //                "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
        //                "Our risk and responsibility ceases the moment goods leaves out godown. \n",
        //                };

        //    Font font12 = FontFactory.GetFont("Arial", 12, Font.BOLD);
        //    Font font10 = FontFactory.GetFont("Arial", 10, Font.BOLD);
        //    Paragraph paragraph = new Paragraph("", font12);

        //    for (int i = 0; i < items.Length; i++)
        //    {
        //        paragraph.Add(new Phrase("", font10));
        //    }

        //    table = new PdfPTable(7);
        //    table.TotalWidth = 560f;
        //    table.LockedWidth = true;
        //    table.SetWidths(new float[] { 4f, 10f, 19f, 10f, 10f, 10f, 10f });
        //    table.AddCell(paragraph);
        //    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //    table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //    //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
        //    table.AddCell(new Phrase("  \n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont("Arial", 10, Font.BOLD)));

        //    doc.Add(table);


        //    Paragraph paragraphTable4 = new Paragraph();

        //    paragraphTable4.SpacingBefore = 10f;

        //    table = new PdfPTable(2);
        //    table.TotalWidth = 560f;

        //    float[] widths = new float[] { 160f, 400f };
        //    table.SetWidths(widths);
        //    table.LockedWidth = true;

        //    doc.Close();
        //    Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + "Report.pdf");
        //    string empFilename = "Report" + DateTime.Now.ToShortDateString() + ".pdf";

        //    if (FileBuffer != null)
        //    {
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-length", FileBuffer.Length.ToString());
        //        Response.BinaryWrite(FileBuffer);
        //        Response.AddHeader("Content-Disposition", "attachment;filename=" + empFilename);
        //    }
        //}
        //doc.Close();
    }

    protected void PurchaseOrderPDF()
    {
        DataTable dtexcelpurchase = new DataTable();
        dtexcelpurchase.Columns.AddRange(new DataColumn[7]
        { new DataColumn("Id"),
               new DataColumn("SupplierName"),
                new DataColumn("PONo"),
                 new DataColumn("PODate"),
                  new DataColumn("DeliveryDate"),
                   new DataColumn("ReferQuotation"),
                   new DataColumn("GrandTotat")


        });
        foreach (GridViewRow row in dgvPurchaseOrder.Rows)
        {
            string SN = (row.Cells[1].FindControl("lblsno") as Label).Text;
            // string BillNo = (row.Cells[1].FindControl("lblSupplierBillNo") as Label).Text;
            string SupplierName = (row.Cells[1].FindControl("lblSupplierName") as Label).Text;
            string PONo = (row.Cells[1].FindControl("lblPONo") as Label).Text;
            string PODate = (row.Cells[1].FindControl("lblPODate") as Label).Text;
            //string GrandTotal = (row.Cells[1].FindControl("lblGrandTotal") as Label).Text;
            string deliverydate = (row.Cells[1].FindControl("lblDeliveryDate") as Label).Text;
            string ReferQuatation = (row.Cells[1].FindControl("lblReferQuotation") as Label).Text;
            string GrandTotal = (row.Cells[1].FindControl("lblGrandTotal") as Label).Text;

            dtexcelpurchase.Rows.Add(SN, SupplierName, PONo, PODate, deliverydate, ReferQuatation, GrandTotal);

        }
        System.IO.StringWriter sw = new StringWriter();
        StringReader sr = new StringReader(sw.ToString());

        Document doc = new Document(PageSize.A4, 10f, 10f, 55f, 0f);

        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/files/") + "Report.pdf", FileMode.Create));

        doc.Open();


        PdfContentByte cb = writer.DirectContent;
        cb.Rectangle(15f, 800f, 561f, 35f);
        cb.Stroke();
        // Header 
        cb.BeginText();
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 20);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Purchase Order Report", 200, 812, 0);
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 145, 755, 0);
        cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED), 11);
        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 227, 740, 0);
        cb.EndText();
        if (dtexcelpurchase.Rows.Count > 0)
        {
            string Suppliername = dtexcelpurchase.Rows[0]["SupplierName"].ToString();
            string PONO = dtexcelpurchase.Rows[0]["PONo"].ToString();
            string POdate = dtexcelpurchase.Rows[0]["PODate"].ToString().TrimEnd("0:0".ToCharArray());
            string DeliveryDate = dtexcelpurchase.Rows[0]["DeliveryDate"].ToString();
            string Referquatation = dtexcelpurchase.Rows[0]["ReferQuotation"].ToString();
            string Grandtotal = dtexcelpurchase.Rows[0]["GrandTotat"].ToString();
            //string IGST = Dt.Rows[0]["IGST"].ToString();

            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingBefore = 120f;
            paragraphTable1.SpacingAfter = 10f;

            PdfPTable table = new PdfPTable(1);

            float[] widths2 = new float[] { 560f };
            table.SetWidths(widths2);
            table.TotalWidth = 560f;
            table.LockedWidth = true;


            paragraphTable1.Add(table);
            doc.Add(paragraphTable1);

            Paragraph paragraphTable2 = new Paragraph();
            paragraphTable2.SpacingAfter = 0f;
            paragraphTable2.SpacingBefore = 120f;

            table = new PdfPTable(7);
            float[] widths3 = new float[] { 4f, 19f, 10f, 10f, 10f, 10f, 10f };
            table.SetWidths(widths3);
            if (dgvPurchaseOrder.Rows.Count > 0)
            {
                table.TotalWidth = 560f;
                table.LockedWidth = true;
                table.AddCell(new Phrase("SN.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Supplier Name", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("PO No.", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("PO Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Delivery Date", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Refer Quatation", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                table.AddCell(new Phrase("Grand Total", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                //table.AddCell(new Phrase("IGST", FontFactory.GetFont("Arial", 10, Font.BOLD)));
                int rowid = 1;
                foreach (DataRow dr in dtexcelpurchase.Rows)
                {
                    table.TotalWidth = 560f;
                    table.LockedWidth = true;
                    table.AddCell(new Phrase(rowid.ToString(), FontFactory.GetFont("Arial", 13)));
                    table.AddCell(new Phrase(dr["SupplierName"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["PONo"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["PODate"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["DeliveryDate"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["ReferQuotation"].ToString(), FontFactory.GetFont("Arial", 10)));
                    table.AddCell(new Phrase(dr["GrandTotat"].ToString(), FontFactory.GetFont("Arial", 10)));
                    //table.AddCell(new Phrase(dr["IGST"].ToString(), FontFactory.GetFont("Arial", 10)));
                    rowid++;
                }
            }
            paragraphTable2.Add(table);
            doc.Add(paragraphTable2);

            //Space
            Paragraph paragraphTable3 = new Paragraph();

            string[] items = { "Goods once sold will not be taken back or exchange. \b",
                        "Interest at the rate of 18% will be charged if bill is'nt paid within 30 days.\b",
                        "Our risk and responsibility ceases the moment goods leaves out godown. \n",
                        };

            Font font12 = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font font10 = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Paragraph paragraph = new Paragraph("", font12);

            for (int i = 0; i < items.Length; i++)
            {
                paragraph.Add(new Phrase("", font10));
            }

            table = new PdfPTable(7);
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 4f, 19f, 10f, 10f, 10f, 10f, 10f });
            table.AddCell(paragraph);
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            //table.AddCell(new Phrase("", FontFactory.GetFont("Arial", 10, Font.BOLD)));
            table.AddCell(new Phrase("  \n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont("Arial", 10, Font.BOLD)));

            doc.Add(table);


            Paragraph paragraphTable4 = new Paragraph();

            paragraphTable4.SpacingBefore = 10f;

            table = new PdfPTable(2);
            table.TotalWidth = 560f;

            float[] widths = new float[] { 160f, 400f };
            table.SetWidths(widths);
            table.LockedWidth = true;

            doc.Close();
            Byte[] FileBuffer = File.ReadAllBytes(Server.MapPath("~/files/") + "Report.pdf");
            string empFilename = "Report" + DateTime.Now.ToShortDateString() + ".pdf";

            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
                Response.AddHeader("Content-Disposition", "attachment;filename=" + empFilename);
            }
        }
        doc.Close();
    }

    protected void btndatewise_Click(object sender, EventArgs e)
    {
        Daily();
    }

    protected void Daily()
    {
        try
        {
            using (SqlCommand Cmd = new SqlCommand("SP_Report", con))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    var date = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    Cmd.Connection = con;
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@BillingCustomer", txtPartyName.Text);
                    Cmd.Parameters.AddWithValue("@Type", ddltype.SelectedItem.Text);
                    Cmd.Parameters.AddWithValue("@Date", date);
                    DivRoot.Visible = true;
                    if (ddltype.SelectedItem.Text == "Purchase Bill")
                    {
                        dgvPurchaseBill.Visible = true;
                        dgvPurchaseOrder.Visible = false;
                        Cmd.Parameters.AddWithValue("@Action", "DailyPurchaseBill");
                        Da.SelectCommand = Cmd;
                        using (DataTable Dt = new DataTable())
                        {
                            Da.Fill(Dt);


                            dgvPurchaseBill.DataSource = Dt;
                            dgvPurchaseBill.EmptyDataText = "Record Not Found";
                            dgvPurchaseBill.DataBind();
                        }
                    }
                    else
                    {
                        dgvPurchaseBill.Visible = false;
                        dgvPurchaseOrder.Visible = true;
                        Cmd.Parameters.AddWithValue("@Action", "DailyPurchaseOrder");
                        Da.SelectCommand = Cmd;
                        using (DataTable Dt = new DataTable())
                        {
                            Da.Fill(Dt);

                            dgvPurchaseOrder.DataSource = Dt;
                            dgvPurchaseOrder.EmptyDataText = "Record Not Found";
                            dgvPurchaseOrder.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnmonthwise_Click(object sender, EventArgs e)
    {
        Monthly();
    }

    private void Monthly()
    {
        try
        {
            using (SqlCommand Cmd = new SqlCommand("SP_Report", con))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {

                    Cmd.Connection = con;
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@BillingCustomer", txtPartyName.Text);
                    Cmd.Parameters.AddWithValue("@Type", ddltype.SelectedItem.Text);
                    DivRoot.Visible = true;
                    if (ddltype.SelectedItem.Text == "Purchase Bill")
                    {
                        dgvPurchaseBill.Visible = true;
                        dgvPurchaseOrder.Visible = false;
                        Cmd.Parameters.AddWithValue("@Action", "MonthlyPurchaseBill");
                        Da.SelectCommand = Cmd;
                        using (DataTable Dt = new DataTable())
                        {
                            Da.Fill(Dt);

                            dgvPurchaseBill.DataSource = Dt;
                            dgvPurchaseBill.EmptyDataText = "Record Not Found";
                            dgvPurchaseBill.DataBind();
                        }
                    }
                    else
                    {
                        dgvPurchaseBill.Visible = false;
                        dgvPurchaseOrder.Visible = true;
                        Cmd.Parameters.AddWithValue("@Action", "MonthlyPurchaseOrder");
                        Da.SelectCommand = Cmd;
                        using (DataTable Dt = new DataTable())
                        {
                            Da.Fill(Dt);

                            dgvPurchaseOrder.DataSource = Dt;
                            dgvPurchaseOrder.EmptyDataText = "Record Not Found";
                            dgvPurchaseOrder.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnyearwise_Click(object sender, EventArgs e)
    {
        Yearly();
    }

    protected void Yearly()
    {
        using (SqlCommand Cmd = new SqlCommand("SP_Report", con))
        {
            using (SqlDataAdapter Da = new SqlDataAdapter())
            {

                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@BillingCustomer", txtPartyName.Text);
                Cmd.Parameters.AddWithValue("@Type", ddltype.SelectedItem.Text);

                if (ddltype.SelectedItem.Text == "Purchase Bill")
                {
                    dgvPurchaseBill.Visible = true;
                    dgvPurchaseOrder.Visible = false;
                    DivRoot.Visible = true;
                    Cmd.Parameters.AddWithValue("@Action", "YearlyPurchaseBill");
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);
                        dgvPurchaseBill.DataSource = Dt;
                        dgvPurchaseBill.EmptyDataText = "Record Not Found";
                        dgvPurchaseBill.DataBind();
                    }
                }
                else
                {
                    dgvPurchaseBill.Visible = false;
                    dgvPurchaseOrder.Visible = true;
                    Cmd.Parameters.AddWithValue("@Action", "YearlyPurchaseOrder");
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);
                        dgvPurchaseOrder.DataSource = Dt;
                        dgvPurchaseOrder.EmptyDataText = "Record Not Found";
                        dgvPurchaseOrder.DataBind();
                    }
                }
            }
        }
    }

    //decimal totamt_purchaseBill;
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (txtByItem.Text != "")
        //    {
        //        string Id = dgvPurchaseBill.DataKeys[e.Row.RowIndex].Value.ToString();
        //        GridView gvPBillDetails = e.Row.FindControl("gvPBillDetails") as GridView;
        //        gvPBillDetails.DataSource = GetData(string.Format("select * from tblPurchaseBillDtls where HeaderID='" + Id + "' and Particulars='" + txtByItem.Text.Trim() + "'"));
        //        gvPBillDetails.DataBind();
        //    }
        //    else
        //    {
        //        string Id = dgvPurchaseBill.DataKeys[e.Row.RowIndex].Value.ToString();
        //        GridView gvPBillDetails = e.Row.FindControl("gvPBillDetails") as GridView;
        //        gvPBillDetails.DataSource = GetData(string.Format("select * from tblPurchaseBillDtls where HeaderID='" + Id + "'"));
        //        gvPBillDetails.DataBind();
        //    }
        //    con.Open();
        //    string id = dgvPurchaseBill.DataKeys[e.Row.RowIndex].Value.ToString();
        //    SqlCommand cmd = new SqlCommand("select SUM(CAST(GrandTotal as float)) as GrossAmt from tblPurchaseBillDtls where HeaderID='" + id + "'", con);
        //    Object GAmtcnt = cmd.ExecuteScalar();
        //    Label grandtotal = (Label)e.Row.FindControl("lblGrandAmountpurchaseBill");
        //    grandtotal.Text = GAmtcnt == null ? "0" : GAmtcnt.ToString();
        //    con.Close();
        //}

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    totamt_purchaseBill += Convert.ToDecimal((e.Row.FindControl("lblGrandAmountpurchaseBill") as Label).Text);
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    (e.Row.FindControl("lblTotalamt") as Label).Text = totamt_purchaseBill.ToString();
        //}
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
	
	decimal totamt_purchase;
    protected void dgvPurchaseOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (txtByItem.Text != "")
            {
                string Id = dgvPurchaseOrder.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvPODetails = e.Row.FindControl("gvPODetails") as GridView;
                gvPODetails.DataSource = GetData(string.Format("select * from tblPurchaseOrderDtls where HeaderID='" + Id + "' and Particulars='" + txtByItem.Text.Trim() + "'"));
                gvPODetails.DataBind();
            }
            else
            {
                string Id = dgvPurchaseOrder.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvPODetails = e.Row.FindControl("gvPODetails") as GridView;
                gvPODetails.DataSource = GetData(string.Format("select * from tblPurchaseOrderDtls where HeaderID='{0}'", Id));
                gvPODetails.DataBind();
            }
            con.Open();
            string id = dgvPurchaseOrder.DataKeys[e.Row.RowIndex].Value.ToString();
            SqlCommand cmd = new SqlCommand("select SUM(CAST(GrandTotal as float)) as GrossAmt from tblPurchaseOrderDtls where HeaderID='" + id + "'", con);
            Object GAmtcnt = cmd.ExecuteScalar();
            Label grandtotal = (Label)e.Row.FindControl("lblGrandAmount");
            grandtotal.Text = GAmtcnt == null ? "0" : GAmtcnt.ToString();
            con.Close();
        }
		
		if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totamt_purchase += Convert.ToDecimal((e.Row.FindControl("lblGrandAmount") as Label).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblTotalamt") as Label).Text = totamt_purchase.ToString();
        }
    }
	
    protected void ddltype_TextChanged(object sender, EventArgs e)
    {
        if (ddltype.Text == "PO")
        {
            divbn1.Visible = false;
            divbn2.Visible = false;

            divsbn1.Visible = false;
            divsbn2.Visible = false;

            divPO1.Visible = true;
            divPO2.Visible = true;

            divCreDebNo1.Visible = false;
            divCreDebNo2.Visible = false;
        }
        else if (ddltype.Text == "PurchaseBill")
        {
            divbn1.Visible = true;
            divbn2.Visible = true;

            divsbn1.Visible = true;
            divsbn2.Visible = true;

            divPO1.Visible = false;
            divPO2.Visible = false;

            divCreDebNo1.Visible = false;
            divCreDebNo2.Visible = false;
        }
        else if (ddltype.Text == "CreditDebitNote")
        {
            divbn1.Visible = true;
            divbn2.Visible = true;

            divsbn1.Visible = false;
            divsbn2.Visible = false;

            divPO1.Visible = false;
            divPO2.Visible = false;

            divCreDebNo1.Visible = true;
            divCreDebNo2.Visible = true;
        }
        else
        {
            divbn1.Visible = false;
            divbn2.Visible = false;

            divsbn1.Visible = false;
            divsbn2.Visible = false;

            divPO1.Visible = false;
            divPO2.Visible = false;

            divCreDebNo1.Visible = false;
            divCreDebNo2.Visible = false;
        }
    }

    protected void dgvCreditDebit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownloadPDF")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                Session["PDFID"] = e.CommandArgument.ToString();
                Response.Write("<script>window.open('CreditDebitNotePDF.aspx','_blank');</script>");

            }
        }
    }

	 decimal totamt_Creditedebit;
    protected void dgvCreditDebit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (txtByItem.Text != "")
            {
                string Id = dgvCreditDebit.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvCreditDebitDetails = e.Row.FindControl("gvCreditDebitDetails") as GridView;
                gvCreditDebitDetails.DataSource = GetData(string.Format("select * from tblCreditDebitNoteDtls where HeaderID='" + Id + "' and Particulars='" + txtByItem.Text.Trim() + "'"));
                gvCreditDebitDetails.DataBind();
            }
            else
            {
                string Id = dgvCreditDebit.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvCreditDebitDetails = e.Row.FindControl("gvCreditDebitDetails") as GridView;
                gvCreditDebitDetails.DataSource = GetData(string.Format("select * from tblCreditDebitNoteDtls where HeaderID='{0}'", Id));
                gvCreditDebitDetails.DataBind();
            }
            con.Open();
            string id = dgvCreditDebit.DataKeys[e.Row.RowIndex].Value.ToString();
            SqlCommand cmd = new SqlCommand("select SUM(CAST(Total as float)) as GrossAmt from tblCreditDebitNoteDtls where HeaderID='" + id + "'", con);
            Object GAmtcnt = cmd.ExecuteScalar();
            Label grandtotal = (Label)e.Row.FindControl("lblGrandAmount");
            grandtotal.Text = GAmtcnt == null ? "0" : GAmtcnt.ToString();
            con.Close();
        }
		
		
		if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totamt_Creditedebit += Convert.ToDecimal((e.Row.FindControl("lblGrandAmount") as Label).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblTotalamt") as Label).Text = totamt_Creditedebit.ToString();
        }
    }

    decimal totamt_purchaseBill;
    protected void dgvPurchaseBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (txtByItem.Text != "")
            {
                string Id = dgvPurchaseBill.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvPBillDetails = e.Row.FindControl("gvPBillDetails") as GridView;
                gvPBillDetails.DataSource = GetData(string.Format("select * from tblPurchaseBillDtls where HeaderID='" + Id + "' and Particulars='" + txtByItem.Text.Trim() + "'"));
                gvPBillDetails.DataBind();
            }
            else
            {
                string Id = dgvPurchaseBill.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvPBillDetails = e.Row.FindControl("gvPBillDetails") as GridView;
                gvPBillDetails.DataSource = GetData(string.Format("select * from tblPurchaseBillDtls where HeaderID='" + Id + "'"));
                gvPBillDetails.DataBind();
            }
            //con.Open();
            //string id = dgvPurchaseBill.DataKeys[e.Row.RowIndex].Value.ToString();
            //SqlCommand cmd = new SqlCommand("select SUM(CAST(GrandTotal as float)) as GrossAmt from tblPurchaseBillDtls where HeaderID='" + id + "'", con);
            //Object GAmtcnt = cmd.ExecuteScalar();
            //Label grandtotal = (Label)e.Row.FindControl("lblGrandAmountpurchaseBill");
            //grandtotal.Text = GAmtcnt == null ? "0" : GAmtcnt.ToString();
            //con.Close();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            totamt_purchaseBill += Convert.ToDecimal((e.Row.FindControl("lblGrandAmountpurchaseBill") as Label).Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblTotalamt") as Label).Text = totamt_purchaseBill.ToString();
        }
    }
}