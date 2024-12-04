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

public partial class Admin_OutstandingReport_List : System.Web.UI.Page
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

            }
        }
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuotationF_Report.aspx");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        DateTime now = DateTime.Today;
        string filename = "QuotationReport" + now.ToString("dd/MM/yyyy");
        Response.AddHeader("content-disposition", "attachment; filename = '" + filename + "'.xls");
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite =
        new HtmlTextWriter(stringWrite);
        GV_QuotationF_Report.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //bindOutstandingReportData();
        BindQuotRpt();
    }

    private void BindQuotRpt()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
            {
                if (ddltype.SelectedItem.Value == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Kindly Select Type First !!!');", true);
                }
                else if (ddltype.SelectedItem.Value == "1")
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM QuotationMain QM WHERE NOT EXISTS (SELECT 1 FROM OAList OA WHERE OA.quotationid = QM.id) and (convert(date,createddate) BETWEEN CONVERT(DATETIME,'" + txtfromdate.Text + "',103) AND CONVERT(DATETIME,'" + txttodate.Text + "',103))", con);
                    sad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        GV_QuotationF_Report.EmptyDataText = "Record Not Found";
                        GV_QuotationF_Report.DataSource = dt;
                        GV_QuotationF_Report.DataBind();
                        btnexcel.Visible = true;
                    }
                    else
                    {
                        GV_QuotationF_Report.EmptyDataText = "Record Not Found";
                    }
                }
                else if (ddltype.SelectedItem.Value == "2")
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM QuotationMain QM WHERE EXISTS (SELECT 1 FROM OAList OA WHERE OA.quotationid = QM.id) and (convert(date,createddate) BETWEEN CONVERT(DATETIME,'" + txtfromdate.Text + "',103) AND CONVERT(DATETIME,'" + txttodate.Text + "',103))", con);
                    sad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        GV_QuotationF_Report.EmptyDataText = "Record Not Found";
                        GV_QuotationF_Report.DataSource = dt;
                        GV_QuotationF_Report.DataBind();
                        btnexcel.Visible = true;
                    }
                    else
                    {
                        GV_QuotationF_Report.EmptyDataText = "Record Not Found";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GV_QuotationF_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (ddltype.SelectedValue.ToString() == "1")
            //{
            //    DataTable dt = new DataTable();
            //    SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM QuotationMain QM WHERE EXISTS (SELECT 1 FROM OAList OA WHERE OA.quotationid = QM.id) and (convert(date,createddate) BETWEEN CONVERT(DATETIME,'" + txtfromdate.Text + "',103) AND CONVERT(DATETIME,'" + txttodate.Text + "',103))", con);
            //    sad.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {
            //        List<string> quotationList = dt.AsEnumerable().Select(r => r.Field<string>("quotationno").TrimEnd('-').TrimEnd('0').TrimEnd('1').TrimEnd('2').TrimEnd('3').TrimEnd('4').TrimEnd('5')).ToList();
            //        ViewState["QuotationList"] = quotationList;
            //    }

            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        DataRowView rowView = (DataRowView)e.Row.DataItem;
            //        if (rowView != null)
            //        {
            //            string quotationNo = rowView["quotationno"].ToString().TrimEnd('-').TrimEnd('0').TrimEnd('1').TrimEnd('2').TrimEnd('3').TrimEnd('4').TrimEnd('5');
            //            if (ViewState["QuotationList"] != null)
            //            {
            //                List<string> quotationList = (List<string>)ViewState["QuotationList"];
            //                bool visible = true;
            //                if (quotationList.Contains(quotationNo))
            //                {
            //                    visible = false;
            //                }
            //                e.Row.Visible = visible;
            //            }
            //        }
            //    }
            //}     

            if (ddltype.SelectedValue.ToString() == "1")
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM QuotationMain QM WHERE EXISTS (SELECT 1 FROM OAList OA WHERE OA.quotationid = QM.id) and (convert(date,createddate) BETWEEN CONVERT(DATETIME,'" + txtfromdate.Text + "',103) AND CONVERT(DATETIME,'" + txttodate.Text + "',103))", con);
                sad.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    List<string> quotationList = dt.AsEnumerable().Select(r => r.Field<string>("quotationno").TrimEnd('-').TrimEnd('0').TrimEnd('1').TrimEnd('2').TrimEnd('3').TrimEnd('4').TrimEnd('5')).ToList();

                    ViewState["QuotationList"] = quotationList;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    if (rowView != null)
                    {
                        string quotationNo = rowView["quotationno"].ToString().TrimEnd('-').TrimEnd('0').TrimEnd('1').TrimEnd('2').TrimEnd('3').TrimEnd('4').TrimEnd('5');

                        if (ViewState["QuotationList"] != null)
                        {
                            List<string> quotationList = (List<string>)ViewState["QuotationList"];

                            bool visible = true;

                            if (quotationList.Contains(quotationNo))
                            {
                                visible = false;
                            }

                            e.Row.Visible = visible;
                        }
                    }
                }

                // The second condition
                DataTable dt2 = new DataTable();
                SqlDataAdapter sad2 = new SqlDataAdapter("SELECT QM.* FROM QuotationMain QM LEFT JOIN OAList OA ON QM.quotationno = OA.quotationno WHERE(OA.quotationid IS  NULL OR QM.quotationno IS NULL) AND(CONVERT(date, QM.createddate) BETWEEN CONVERT(DATETIME, '" + txtfromdate.Text + "', 103) AND CONVERT(DATETIME, '" + txttodate.Text + "', 103))", con);
                sad2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    List<string> quotationList2 = dt2.AsEnumerable().Select(r => r.Field<string>("quotationno")).ToList();
                    ViewState["QuotationList2"] = quotationList2;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    if (rowView != null)
                    {
                        string quotationNo = rowView["quotationno"].ToString();
                        string[] parts = quotationNo.Split('/');
                        string series = parts[0];
                        List<string> quotationList2 = (List<string>)ViewState["QuotationList2"];
                        if (quotationList2 != null)
                        {
                            foreach (string item in quotationList2)
                            {
                                string[] parts1 = item.Split('/');
                                if (parts1.Length == 2)
                                {
                                    string listSeries = parts1[0];
                                    string listSeries1 = parts1[1];

                                    if (series == listSeries)
                                    {
                                        string modifiedString = parts[1].Replace("-", "").TrimStart('0');
                                        int currentQuotationNumber = int.Parse(modifiedString);
                                        string modifiedString1 = listSeries1.Replace("-", "").TrimStart('0');
                                        int listQuotationNumber = int.Parse(modifiedString1);
                                        string ModifiedcurrentQuotationNumber = parts[1].ToString().TrimEnd('-').TrimEnd('0').TrimEnd('1').TrimEnd('2').TrimEnd('3').TrimEnd('4').TrimEnd('5');
                                        string ModifedlistQuotationNumber = listSeries1.ToString().TrimEnd('-').TrimEnd('0').TrimEnd('1').TrimEnd('2').TrimEnd('3').TrimEnd('4').TrimEnd('5');

                                        if (ModifiedcurrentQuotationNumber == ModifedlistQuotationNumber && currentQuotationNumber < listQuotationNumber)
                                        {
                                            e.Row.Visible = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }
}