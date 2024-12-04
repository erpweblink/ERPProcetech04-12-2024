#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Reports\OARptPDF.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4F71AF5F3FB85BEF1DE228C122D1B4DB9E0F4396"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Reports\OARptPDF.aspx.cs"

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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;

public partial class Admin_OARptPDF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string OA = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!this.IsPostBack)
        {

            if (Request.QueryString["ID"] != null)
            {
                OA = Request.QueryString["ID"].ToString();
                GetOAData();
            }
            else
            {

            }
        }
    }

    protected void GetOAData()
    {
        try
        {
            SqlDataAdapter ad = new SqlDataAdapter(@"SELECT [OAno]
      ,[currentdate]
      ,[customername]
      ,[deliverydatereqbycust]
      ,[IsDrawingcomplete]
      ,[Description]
      ,[Qty]
      ,[Price]
      ,[Discount]
      ,[TotalAmount]
      ,[CGST]
      ,[SGST]
      ,[IGST]
      ,[VWID]
      ,[id]
      ,[IsComplete]
      ,[SubOANumber]
      ,[QuotationID]
      ,[quotationno]
      ,[quotationdate]
      ,[pono]
      ,[podate]
      ,[contactpersonpurchase]
      ,[contpersonpurcontact]
      ,[contactpersontechnical]
      ,[contpersontechcontact]
      ,[deliverydatecommbyus]
      ,[note1]
      ,[note2]
      ,[note3]
      ,[note4]
      ,[note5]
      ,[note6]
      ,[note7]
      ,[note8]
      ,[note9]
      ,[DeliveryTransportation]
      ,[termaofpayment]
      ,[billingdetails]
      ,[buyer]
      ,[consignee]
      ,[instructionchk1]
      ,[instructionchk2]
      ,[instructionchk3]
      ,[instructionchk4]
      ,[instructionchk5]
      ,[instructionchk6]
      ,[instructionchk7]
      ,[instructionchk8]
      ,[specialinstruction1]
      ,[specialinstruction2]
      ,[specialinstruction3]
      ,[specialinstruction4]
      ,[specialinstruction5]
      ,[specialinstruction6]
      ,[specialinstruction7]
      ,[specialinstruction8]
      ,[packing1]
      ,[packing2]
      ,[packing3]
      ,[address]
  FROM vwOrderAccept where OAno='" + OA + "'", con);

            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //Bind dgv
                dgvOARpt.DataSource = dt;
                dgvOARpt.DataBind();

                //Bind Lable
                lblOANumber.Text = dt.Rows[0]["OAno"].ToString();

                DateTime currentdate = Convert.ToDateTime(dt.Rows[0]["currentdate"].ToString());
                lblDate.Text = currentdate.ToString("yyyy-MM-dd");

                lblCustName.Text = dt.Rows[0]["customername"].ToString();
                lblQuotationNo.Text = dt.Rows[0]["quotationno"].ToString();
                lblAddress.Text = dt.Rows[0]["address"].ToString();

                DateTime quotationdate = dt.Rows[0]["quotationdate"].ToString() == "" ? DateTime.Now :  Convert.ToDateTime(dt.Rows[0]["quotationdate"].ToString());
                lblQuotationDate.Text = quotationdate.ToString("yyyy-MM-dd");

                lblContPerson.Text = dt.Rows[0]["contactpersonpurchase"].ToString();
                lblContactNo.Text = dt.Rows[0]["contpersonpurcontact"].ToString();
                lblPONo.Text = dt.Rows[0]["pono"].ToString();

                DateTime podate = dt.Rows[0]["podate"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["podate"].ToString());
                lblPODate.Text = podate.ToString("yyyy-MM-dd");

                lblNote1.Text = dt.Rows[0]["note1"].ToString();
                lblNote2.Text = dt.Rows[0]["note2"].ToString();
                lblNote3.Text = dt.Rows[0]["note3"].ToString();
                lblNote4.Text = dt.Rows[0]["note4"].ToString();
                lblNote5.Text = dt.Rows[0]["note5"].ToString();
                lblNote6.Text = dt.Rows[0]["note6"].ToString();
                lblNote7.Text = dt.Rows[0]["note7"].ToString();
                lblNote8.Text = dt.Rows[0]["note8"].ToString();
                lblNote9.Text = dt.Rows[0]["note9"].ToString();

                DateTime deldtbycust = Convert.ToDateTime(dt.Rows[0]["deliverydatereqbycust"].ToString());
                lbldeliverydatebycust.Text = deldtbycust.ToString("yyyy-MM-dd");
                DateTime deldtcommbyus = Convert.ToDateTime(dt.Rows[0]["deliverydatecommbyus"].ToString());
                lbldeliverydatebyus.Text = deldtcommbyus.ToString("yyyy-MM-dd");
                //lbldeliverydatebycust.Text = dt.Rows[0]["deliverydatereqbycust"].ToString();
                //lbldeliverydatebyus.Text = dt.Rows[0]["deliverydatecommbyus"].ToString();
                //lblbuyer.Text = dt.Rows[0]["buyer"].ToString();
                //lblconsignee.Text = dt.Rows[0]["consignee"].ToString();

                lblCGSTPer.Text = dt.Rows[0]["CGST"].ToString();
                //lblCGSTAmt.Text = dt.Rows[0]["cgstamt"].ToString();
                lblSGSTPer.Text = dt.Rows[0]["SGST"].ToString();
                //lblSGSTAmt.Text = dt.Rows[0]["sgstamt"].ToString();
                lblIGSTPer.Text = dt.Rows[0]["IGST"].ToString();
                //lblIGSTAmt.Text = dt.Rows[0]["igstamt"].ToString();

                StringBuilder sbpacking = new StringBuilder();
                if (!string.IsNullOrEmpty(dt.Rows[0]["packing1"].ToString()))
                {
                    sbpacking.Append(dt.Rows[0]["packing1"].ToString());
                    sbpacking.Append(" ");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["packing2"].ToString()))
                {
                    sbpacking.Append(dt.Rows[0]["packing2"].ToString());
                    sbpacking.Append(" ");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["packing3"].ToString()))
                {
                    sbpacking.Append(dt.Rows[0]["packing3"].ToString());
                }

                lblpacking.Text = sbpacking.ToString();
                lbldeliverytransportcharge.Text = dt.Rows[0]["DeliveryTransportation"].ToString();
                lbltermaofpayment.Text = dt.Rows[0]["termaofpayment"].ToString();
                lblbillingdetails.Text = dt.Rows[0]["billingdetails"].ToString();

                //Special Instructions
                StringBuilder sbspecialnotes = new StringBuilder();
                if (!string.IsNullOrEmpty(dt.Rows[0]["specialinstruction1"].ToString()))
                {
                    sbspecialnotes.Append(dt.Rows[0]["specialinstruction1"].ToString());
                    sbspecialnotes.Append("<br>");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["specialinstruction2"].ToString()))
                {
                    sbspecialnotes.Append(dt.Rows[0]["specialinstruction2"].ToString());
                    sbspecialnotes.Append("<br>");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["specialinstruction3"].ToString()))
                {
                    sbspecialnotes.Append(dt.Rows[0]["specialinstruction3"].ToString());
                    sbspecialnotes.Append("<br>");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["specialinstruction4"].ToString()))
                {
                    sbspecialnotes.Append(dt.Rows[0]["specialinstruction4"].ToString());
                    sbspecialnotes.Append("<br>");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["specialinstruction5"].ToString()))
                {
                    sbspecialnotes.Append(dt.Rows[0]["specialinstruction5"].ToString());
                    sbspecialnotes.Append("<br>");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["specialinstruction6"].ToString()))
                {
                    sbspecialnotes.Append(dt.Rows[0]["specialinstruction6"].ToString());
                    sbspecialnotes.Append("<br>");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["specialinstruction7"].ToString()))
                {
                    sbspecialnotes.Append(dt.Rows[0]["specialinstruction7"].ToString());
                    sbspecialnotes.Append("<br>");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["specialinstruction8"].ToString()))
                {
                    sbspecialnotes.Append(dt.Rows[0]["specialinstruction8"].ToString());
                }

                lblSpecialNote.Text = sbspecialnotes.ToString();

            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    decimal amount = 0;
    int Qty = 0;
    protected void OnDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            amount += Convert.ToDecimal((e.Row.FindControl("txtAmount") as Label).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblAmount") as Label).Text = amount.ToString();
        }
    }


}

#line default
#line hidden
