using AjaxControlToolkit;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnPageLoad", "OnPageLoad();", true);
                int currentYear = DateTime.Now.Year;

                // Loop to add years to the dropdown
                for (int i = 0; i <= 10; i++)
                {
                    int year = currentYear - i; // Get current year and the previous 10 years
                    DropDownList1.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }
                DropDownList1.SelectedValue = currentYear.ToString();
                var Role = Session["RoleName"].ToString();
                if (Role == "Admin")
                {
                    Bindchart();
                    DropDownList1_TextChanged();
                    RptSalesDetailsbind(); GvLoginLogBind();
                    lbltotalpaid = string.Empty; lbltotalunpaid = string.Empty; lbltotlaclients = string.Empty;
                    GvActiveUsersBind();
                    CountData();
                    TodayEnquiryList();
                    TodayQuotationList();
                }
                else
                {
                    cardCustomers.Visible = false;
                    cardQuotation.Visible = false;
                    cardOA.Visible = false;
                    //cardUsers.Visible = false;
                    cardTodayEnquiries.Visible = false;
                    cardTodayQuotation.Visible = false;
                    carddateprocessedvaluesection.Visible = false;
                }


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

    private void BothSalesDDLbind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],substring([name],1,charindex(' ',[name]+' ')-1) as [name],[empcode] FROM [employees] where [role]='Sales' and [status]=1 and [isdeleted]=0 order by id Asc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlTbrofilter.DataSource = dt;
            ddlTbrofilter.DataTextField = "name";
            ddlTbrofilter.DataValueField = "empcode";
            ddlTbrofilter.DataBind();
            ddlTbrofilter.Items.Insert(0, "All");

            //ddlLoginfilter.DataSource = dt;
            //ddlLoginfilter.DataTextField = "name";
            //ddlLoginfilter.DataValueField = "empcode";
            //ddlLoginfilter.DataBind();
            //ddlLoginfilter.Items.Insert(0, "All");
        }
    }

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
            //query = @"select * from tblLoginLogs where LoginDate=CONVERT(date, getdate()) order by id desc";
            query = @"select * from [DB_ProcetechTesting].[DB_ProcetechERP].[tblLoginLogs] where LoginDate=CONVERT(date, getdate()) order by id desc";
        }
        else
        {
            DateTime temp = DateTime.ParseExact(txtLoginDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string str = temp.ToString("yyyy-MM-dd");
            //query = @"select * from tblLoginLogs where LoginDate='" + str + "' order by id desc";
            query = @"select * from [DB_ProcetechTesting].[DB_ProcetechERP].[tblLoginLogs] where LoginDate='" + str + "' order by id desc";
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
        string dateString = "";
        string dateStringDrawing = "";

        if (txtDate.Text != "")
        {
            DateTime date = DateTime.ParseExact(txtDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime date1 = DateTime.ParseExact(txtDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            dateString = date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            dateStringDrawing = date1.ToString("MMM dd yyyy", CultureInfo.InvariantCulture);
        }

        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[DB_ProcetechERP].[SP_DashBoardBarCount]";

        if (txtDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@Date", dateString);
            cmd.Parameters.AddWithValue("@Date1", dateStringDrawing);
        }
        else
        {
            cmd.Parameters.AddWithValue("@Date", "All");
            cmd.Parameters.AddWithValue("@Date1", "All");
        }

        cmd.Connection = con;
        con.Open();

        DataSet ds = new DataSet();
        using (var da = new SqlDataAdapter(cmd))
        {
            da.Fill(ds);
        }

        DataTable ChartData = ds.Tables[0];

        // Serialize the ChartData to JSON
        string jsonData = JsonConvert.SerializeObject(ChartData, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

        // Close the connection
        con.Close();
        if (ChartData.Rows.Count > 0)
        {
           // ClientScript.RegisterStartupScript(this.GetType(), "loadBarChartData", "loadBarChartData(" + jsonData + ");", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "loadBarChartData", "loadBarChartData(" + jsonData + ");", true);
        }





        ////storing total rows count to loop on each Record    
        //string[] XPointMember = new string[ChartData.Rows.Count];
        //int[] YPointMember = new int[ChartData.Rows.Count];
        //for (int count = 0; count < ChartData.Rows.Count; count++)
        //{
        //    //storing Values for X axis   
        //    XPointMember[count] = ChartData.Rows[count]["Department"].ToString();

        //    //storing values for Y Axis    
        //    YPointMember[count] = ChartData.Rows[count]["Processed Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(ChartData.Rows[count]["Processed Quantity"]);

        //}


        ////binding chart control    
        //Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);
        ////Setting width of line    
        //Chart1.Series[0].BorderWidth = 2;
        ////setting Chart type     
        //// Chart1.Series[0].ChartType = SeriesChartType.Column;
        //Chart1.Series[0].ChartType = SeriesChartType.StackedColumn;

        //Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        //Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Department";
        //Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Quantity in numbers";

        //Chart1.Width = 500;
        //Chart1.Height = 500;
        //Chart1.Style.Add("margin-left", "-4%");

        //Chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);
        //Chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);

        //Chart1.Series[0].IsValueShownAsLabel = true;
        //// Chart1.Series[0].IsValueShownAsLabel= new Font("Arial", 14);

        ////Color color = ColorTranslator.FromHtml("#01a9ac");

        //Color color = ColorTranslator.FromHtml("#ff4500");
        //Chart1.Series[0].Color = color;

        //con.Close();
        DropDownList1_TextChanged();
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
        //cmd.CommandText = "SP_GetDateBetweenRpt";
        cmd.CommandText = "SP_GetDateBetweenRptDashBoardChart";
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

            // Old Code

            //string[] words = datedata.Split('/');

            //int YYYY = Convert.ToInt32(words[2]);
            //int MM = Convert.ToInt32(words[1]);
            //int DD = Convert.ToInt32(words[0]);


            //string dayname = getDayname(YYYY, DD, MM);

            // End Code


            datedata = datedata.Trim();
            datedata = datedata.Replace("  ", " ");
            string format = "MMM d yyyy h:mmtt";
            DateTime parsedDate;
            bool success = DateTime.TryParseExact(datedata, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate);
            int YYYY = parsedDate.Year;
            int MM = parsedDate.Month;
            int DD = parsedDate.Day;
            string dayname = parsedDate.ToString("dddd");

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

        SqlCommand cmdOrderaccept = new SqlCommand("SELECT COUNT(DISTINCT OAno) AS OrderAccept from OAlist Where IsDispatch IS NULL", con);
        lblOrderAcceptancecount.Text = Convert.ToInt32(cmdOrderaccept.ExecuteScalar()).ToString();

        //SqlCommand cmdUser = new SqlCommand("SELECT COUNT(empcode) AS Usercount from employees where isdeleted = 0 ", con);
        //lblUsercount.Text = Convert.ToInt32(cmdUser.ExecuteScalar()).ToString();
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

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        txtDate.Text = "";
        Bindchart();
    }

    protected void DropDownList1_TextChanged()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetSalesDetailsMonthWise";
        cmd.Parameters.AddWithValue("@year", DropDownList1.Text);
        cmd.Connection = con;

        // Open the connection and fill the DataSet
        con.Open();
        DataSet ds = new DataSet();
        using (var da = new SqlDataAdapter(cmd))
        {
            da.Fill(ds);
        }

        // Serialize the DataTable to JSON using JsonConvert (to handle circular references)
        DataTable chartData = ds.Tables[0];  // Assuming you want the first table in the DataSet
        string jsonData = JsonConvert.SerializeObject(chartData,
                           Formatting.None,
                           new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

        // Close the connection
        con.Close();

        // Pass the JSON data to JavaScript (via a ClientScript registration)
        //ClientScript.RegisterStartupScript(this.GetType(), "loadChartData", "loadChartData(" + jsonData + ");", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "loadChartData", "loadChartData(" + jsonData + ");", true);
    }

    protected void DropDownList1_TextChanged1(object sender, EventArgs e)
    {
        DropDownList1_TextChanged();
        Bindchart();
    }

    [System.Web.Services.WebMethod]
    public static void StoreCustomerDataInSession(string customerCode, string customerName)
    {
        HttpContext.Current.Session["CustomerCode"] = customerCode;
        HttpContext.Current.Session["CustomerName"] = customerName;
    }


}