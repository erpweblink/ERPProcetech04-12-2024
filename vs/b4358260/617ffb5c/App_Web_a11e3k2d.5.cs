#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\AdminDashboard.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F6AB7E317086E4B1A8360DB9717049B8D3D05CA5"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\AdminDashboard.aspx.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Web.UI.DataVisualization.Charting;
using System.Globalization;

public partial class Admin_AdminDashboard : System.Web.UI.Page
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
                RptSalesDetailsbind(); GvLoginLogBind();
                lbltotalpaid = string.Empty; lbltotalunpaid = string.Empty; lbltotlaclients = string.Empty;
                GvActiveUsersBind();
                CountData();
                TodayEnquiryList();
                TodayQuotationList();
                //Bindchart();
            }
        }
    }

    private void RptSalesDetailsbind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],substring([name],1,charindex(' ',[name]+' ')-1) as [name],[empcode] FROM [employees] where [role]='Sales' and [status]=1 and [isdeleted]=0 order by id Asc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            RptSalesDetails.DataSource = dt;
            RptSalesDetails.DataBind();
        }
    }

    //private void BothSalesDDLbind()
    //{
    //    SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],substring([name],1,charindex(' ',[name]+' ')-1) as [name],[empcode] FROM [employees] where [role]='Sales' and [status]=1 and [isdeleted]=0 order by id Asc", con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlTbrofilter.DataSource = dt;
    //        ddlTbrofilter.DataTextField = "name";
    //        ddlTbrofilter.DataValueField = "empcode";
    //        ddlTbrofilter.DataBind();
    //        ddlTbrofilter.Items.Insert(0, "All");

    //        ddlLoginfilter.DataSource = dt;
    //        ddlLoginfilter.DataTextField = "name";
    //        ddlLoginfilter.DataValueField = "empcode";
    //        ddlLoginfilter.DataBind();
    //        ddlLoginfilter.Items.Insert(0, "All");
    //    }
    //}

    protected void RptSalesDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    //Reference the Repeater Item.
        //    RepeaterItem item = e.Item;

        //    string empcode = (item.FindControl("lblempcode") as Label).Text;
        //    //CheckClients(empcode);

        //   Label a = item.FindControl("lbltotalclientRP") as Label;
        //    a.Text = lbltotlaclients;

        //    Label a1 = item.FindControl("lbltotalPaidclientRP") as Label;
        //    a1.Text = lbltotalpaid;

        //    Label a2 = item.FindControl("lbltotalUnpaidclientRP") as Label;
        //    a2.Text = lbltotalunpaid;
        //}
    }

    static string lbltotalpaid = string.Empty; static string lbltotalunpaid = string.Empty; static string lbltotlaclients = string.Empty;
    //protected void CheckClients(string empcode)
    //{
    //    lbltotalpaid = string.Empty; lbltotalunpaid = string.Empty; lbltotlaclients = string.Empty;
    //    string q = @" SELECT (select COUNT(*) from [Company] where [sessionname]='"+ empcode + "' and (type = 'Paid' or type = 'paid') and status=0) as Paidclient,(select COUNT(*) from [Company] where [sessionname]= '"+ empcode + "' and (type = 'Unpaid' or type = 'unpaid') and status=0) as Unpaidclient,(select COUNT(*) from [Company] where [sessionname]= '"+ empcode + "' and status=0) as Totalclient";
    //    SqlDataAdapter ad = new SqlDataAdapter(q, con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);

    //    if (dt.Rows.Count > 0)
    //    {
    //        if (!string.IsNullOrEmpty(dt.Rows[0]["Paidclient"].ToString()))
    //        {
    //            lbltotalpaid = dt.Rows[0]["Paidclient"].ToString();
    //        }
    //        else
    //        {
    //            lbltotalpaid ="0";
    //        }
    //        if (!string.IsNullOrEmpty(dt.Rows[0]["Unpaidclient"].ToString()))
    //        {
    //            lbltotalunpaid = dt.Rows[0]["Unpaidclient"].ToString();
    //        }
    //        else
    //        {
    //            lbltotalunpaid = "0";
    //        }

    //        if (!string.IsNullOrEmpty(dt.Rows[0]["Totalclient"].ToString()))
    //        {
    //            lbltotlaclients = dt.Rows[0]["Totalclient"].ToString();
    //        }
    //        else
    //        {
    //            lbltotlaclients = "0";
    //        }

    //    }
    //    else
    //    {
    //        lbltotalunpaid = "Unpaid : Not Available!";
    //        lbltotalpaid = "Paid : Not Available!";
    //        lbltotlaclients = "Total : Not Available!";
    //    }
    //}


    //private void GvTBROBind()
    //{
    //    string query = string.Empty;
    //    if (ddlTbrofilter.SelectedItem.Text != "All")
    //    {
    //        query = @"SELECT r.[id],r.[ccode],r.[cname],r.[title],r.[remark],r.[sessionname],substring(e.[name],1,charindex(' ',e.[name]+' ')-1) as [name],format(r.[dateofreminder],'dd-MMM-yyyy') as [dateofreminder] FROM [RemainderData] r join [employees] e on r.[sessionname]=e.[empcode] where format([dateofreminder],'yyyy-MM-dd')>=format(getdate(),'yyyy-MM-dd') and r.[sessionname]='" + ddlTbrofilter.SelectedValue + "' order by id desc";
    //    }
    //    else
    //    {
    //        query = @"SELECT r.[id],r.[ccode],r.[cname],r.[title],r.[remark],r.[sessionname],substring(e.[name],1,charindex(' ',e.[name]+' ')-1) as [name],format(r.[dateofreminder],'dd-MMM-yyyy') as [dateofreminder] FROM [RemainderData] r join [employees] e on r.[sessionname]=e.[empcode] where format([dateofreminder],'yyyy-MM-dd')>=format(getdate(),'yyyy-MM-dd') order by id desc";
    //    }
    //    SqlDataAdapter ad = new SqlDataAdapter(query, con);
    //    DataTable dt = new DataTable();
    //    ad.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        GvTBRO.DataSource = dt;
    //        GvTBRO.DataBind();
    //        lblnoTbrodatafound.Visible = false;
    //    }
    //    else
    //    {
    //        GvTBRO.DataSource = null;
    //        GvTBRO.DataBind();
    //        lblnoTbrodatafound.Text = "No TBRO/Remainder Data Found !! ";
    //        lblnoTbrodatafound.Visible = true;
    //        lblnoTbrodatafound.ForeColor = System.Drawing.Color.Red;
    //    }
    //}

    protected void GvTBRO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvTBRO.PageIndex = e.NewPageIndex;
        //GvTBROBind();
    }

    protected void GvLoginlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvLoginlog.PageIndex = e.NewPageIndex;
        //GvLoginLogBind();
    }

    private void GvLoginLogBind()
    {

        string query = string.Empty;
        if (txtLoginDate.Text == "")
        {
            query = @"select * from tblLoginLogs where LoginDate=CONVERT(date, getdate()) order by id desc";
        }
        else
        {
            DateTime temp = DateTime.ParseExact(txtLoginDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string str = temp.ToString("yyyy-MM-dd");
            query = @"select * from tblLoginLogs where LoginDate='" + str + "' order by id desc";
        }

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvLoginlog.DataSource = dt;
            GvLoginlog.DataBind();
            lblnoLogdatafound.Visible = false;
        }
        else
        {
            GvLoginlog.DataSource = null;
            GvLoginlog.DataBind();
            lblnoLogdatafound.Text = "No Login Data Found !! ";
            lblnoLogdatafound.Visible = true;
            lblnoLogdatafound.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void GvActiveUsersBind()
    {

        string query = string.Empty;
        if (ddlModule.SelectedValue != null)
        {
            query = @"select * from employees where status=1";
        }
        else if (ddlRole.SelectedValue != null)
        {

        }

        query = @"select * from employees where status=1";

        SqlDataAdapter ad = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dgvActiveUser.DataSource = dt;
            dgvActiveUser.DataBind();
            lblnoLogdatafound.Visible = false;
        }
        else
        {
            dgvActiveUser.DataSource = null;
            dgvActiveUser.DataBind();
            lblnoLogdatafound.Text = "No Active Users Data Found !! ";
            lblnoLogdatafound.Visible = true;
            lblnoLogdatafound.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void ddlLoginfilter_TextChanged(object sender, EventArgs e)
    {
        //GvLoginLogBind();
    }

    protected void ddlTbrofilter_TextChanged(object sender, EventArgs e)
    {
        //GvTBROBind();
    }

    protected void getlastlogindetail()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT top 2 [id],Format([logindate],'dd-MMM-yyyy hh:mm tt') as [logindate] FROM [userslogindetails] where [empcode]='" + Session["adminempcode"].ToString() + "' order by id desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 1)
        {
            lbllastlogin.Text = "Last Login : " + dt.Rows[1]["logindate"].ToString();
        }
    }

    private void Bindchart()
    {

        //DateTime date = Convert.ToDateTime(txtDate.Text.Trim());
        //string dateString = date.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
		DateTime date = DateTime.ParseExact(txtDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
        string dateString = date.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
		
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_CommercialReport";
        cmd.Parameters.AddWithValue("@Date", dateString);
        cmd.Connection = con;
        con.Open();
        DataSet ds = new DataSet();
        using (var da = new SqlDataAdapter(cmd))
        {
            da.Fill(ds);
        }

        DataTable ChartData = ds.Tables[0];
        //storing total rows count to loop on each Record    
        string[] XPointMember = new string[ChartData.Rows.Count];
        int[] YPointMember = new int[ChartData.Rows.Count];
        for (int count = 0; count < ChartData.Rows.Count; count++)
        {
            //storing Values for X axis   
            XPointMember[count] = ChartData.Rows[count]["Department"].ToString();
            //storing values for Y Axis    
            YPointMember[count] = ChartData.Rows[count]["Processed Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(ChartData.Rows[count]["Processed Quantity"]);
        }
        //binding chart control    
        Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);
        //Setting width of line    
        Chart1.Series[0].BorderWidth = 1;
        //setting Chart type     
        Chart1.Series[0].ChartType = SeriesChartType.Column;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;

        Chart1.Series[0].IsValueShownAsLabel = true;
        // Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;    
        //  Chart1.Series[0].ChartType = SeriesChartType.Spline;    
        //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;   
        con.Close();
    }

    private void BindWeekDatachart()
    {
        //DateTime datefrom = Convert.ToDateTime(txtFromDate.Text.Trim());
        //string FromDate = datefrom.ToString("M/d/yyyy", CultureInfo.InvariantCulture);

        //DateTime dateto = Convert.ToDateTime(txtToDate.Text.Trim());
        //string ToDate = dateto.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
		
		DateTime datefrom = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
        string FromDate = datefrom.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
		
		DateTime dateto = DateTime.ParseExact(txtToDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
        string ToDate = dateto.ToString("M/d/yyyy", CultureInfo.InvariantCulture);

        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_GetDateBetweenRpt";
        cmd.Parameters.AddWithValue("@FromDate", FromDate);
        cmd.Parameters.AddWithValue("@ToDate", ToDate);
        cmd.Parameters.AddWithValue("@DepartmentName", ddlDepartment.Text);
        cmd.Connection = con;
        con.Open();
        DataSet ds = new DataSet();
        using (var da = new SqlDataAdapter(cmd))
        {
            da.Fill(ds);
        }

        DataTable ChartData = ds.Tables[0];
        //storing total rows count to loop on each Record    
        string[] XPointMember = new string[ChartData.Rows.Count];
        int[] YPointMember = new int[ChartData.Rows.Count];
        for (int count = 0; count < ChartData.Rows.Count; count++)
        {
            //storing Values for X axis   

            string datedata = ChartData.Rows[count]["UpdatedDate"].ToString();

            string[] words = datedata.Split('/');

            int YYYY = Convert.ToInt32(words[2]);
            int MM = Convert.ToInt32(words[1]);
            int DD = Convert.ToInt32(words[0]);


            string dayname = getDayname(YYYY, DD, MM);

            XPointMember[count] = dayname;
            //storing values for Y Axis    
            YPointMember[count] = ChartData.Rows[count]["Processed Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(ChartData.Rows[count]["Processed Quantity"]);
        }
        //binding chart control    
        Chart2.Series[0].Points.DataBindXY(XPointMember, YPointMember);
        //Setting width of line    
        Chart2.Series[0].BorderWidth = 1;
        //setting Chart type     
        Chart2.Series[0].ChartType = SeriesChartType.Line;
        Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;

        Chart2.Series[0].IsValueShownAsLabel = true;
        // Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;    
        //  Chart1.Series[0].ChartType = SeriesChartType.Spline;    
        //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;   
        con.Close();
    }

    public string getDayname(int year, int Month, int day)
    {

        DateTime dateValue = new DateTime(year, Month, day);   //(YYYY,MM,DD)
        string DayName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)dateValue.DayOfWeek];
        return DayName;
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        Bindchart();
    }

    protected void btnGetData_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "")
        {

        }
        else if (txtToDate.Text == "")
        {

        }
        else if (ddlDepartment.SelectedValue == "0")
        {

        }
        else
        {
            BindWeekDatachart();
        }
    }

    protected void txtLoginDate_TextChanged(object sender, EventArgs e)
    {
        GvLoginLogBind();
    }

    protected void CountData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT COUNT(DISTINCT cname) AS 'Number of company' from Company", con);
        lblcustomercount.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString();

        SqlCommand cmdquotation = new SqlCommand("SELECT COUNT(DISTINCT quotationno) AS Quotation from QuotationMain", con);
        lblQuotationcount.Text = Convert.ToInt32(cmdquotation.ExecuteScalar()).ToString();

        SqlCommand cmdOrderaccept = new SqlCommand("SELECT COUNT(DISTINCT OAno) AS OrderAccept from OrderAccept", con);
        lblOrderAcceptancecount.Text = Convert.ToInt32(cmdOrderaccept.ExecuteScalar()).ToString();

        SqlCommand cmdUser = new SqlCommand("SELECT COUNT(empcode) AS Usercount from employees", con);
        lblUsercount.Text = Convert.ToInt32(cmdUser.ExecuteScalar()).ToString();
        con.Close();
    }

    protected void TodayEnquiryList()
    {
        try
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT [cname],[remark],[sessionname] FROM EnquiryData where Convert(char(10), regdate, 120) = Convert(date, getdate())", con);
            sad.Fill(dt);
            dgvEnquiry.EmptyDataText = "Not Records Found";
            dgvEnquiry.DataSource = dt;
            dgvEnquiry.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void TodayQuotationList()
    {
        try
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT [id],[quotationno],[partyname],[date],[sessionname],[createddate] FROM QuotationMain where Convert(char(10), createddate, 120) = Convert(date, getdate())", con);
            sad.Fill(dt);
            dgvQuoation.EmptyDataText = "Not Records Found";
            dgvQuoation.DataSource = dt;
            dgvQuoation.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    
    protected void dgvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvEnquiry.PageIndex = e.NewPageIndex;
        TodayEnquiryList();
    }

    protected void dgvQuoation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvQuoation.PageIndex = e.NewPageIndex;
        TodayQuotationList();
    }
}

#line default
#line hidden
