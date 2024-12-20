using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_ProdPendingReports : System.Web.UI.Page
{
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
                string Name = Session["name"].ToString();
                if (Name == "Admin" || Name == "Akash Chinchkar")
                {
                    GetOAreportsData();

                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }

            }
        }
    }

    public void GetOAreportsData()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_PendingReportsDetails]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "GetData"));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                adapter.Fill(Dt);

                if (Dt.Rows.Count > 0)
                {
                    Gvreports.DataSource = Dt;
                    Gvreports.DataBind();
                }
                else
                {
                    Gvreports.DataSource = null;
                    Gvreports.DataBind();
                }
            }
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
    {
        return AutoFillCustomerName(prefixText);
    }


    public static List<string> AutoFillCustomerName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [cname] from [Company] where " + "cname like @Search + '%' and status=0 and [isdeleted]=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["cname"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }


    protected void Gvreports_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        if (ViewState["Reports"] != null)
        {
            if (ViewState["Reports"].ToString() == "Customerwise")
            {
                Gvreports.PageIndex = e.NewPageIndex;
                GetOAreportsDataCustomerwise();
            }

            if (ViewState["Reports"].ToString() == "Datewise")
            {
                Gvreports.PageIndex = e.NewPageIndex;
                GetOAreportsDataDatwise();
            }
        }
        else
        {
            Gvreports.PageIndex = e.NewPageIndex;
            GetOAreportsData();
        }
    }


    protected void btnsearch_Click(object sender, EventArgs e)
    {
        GvEXCELL.Visible = false;
        if (txtcustomer.Text != "")
        {
            ViewState["Reports"] = "Customerwise";
            GetOAreportsDataCustomerwise();
        }
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            ViewState["Reports"] = "Datewise";
            GetOAreportsDataDatwise();
        }
    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        GvEXCELL.Visible = true;
        if (ViewState["Reports"] != null)
        {           
            //if (ViewState["Reports"].ToString() == "Customerwise")
            if (txtcustomer.Text != "")
            {
                GetExcellDataCustomerWise();
            }
            //if (ViewState["Reports"].ToString() == "Datewise") 
            if (txtfromdate.Text != "" && txttodate.Text != "")
            {
                GetExcellDataDatewise();
            }
            else
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_PendingReportsDetails]", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@action", "GetData");
                            //cmd.Parameters.AddWithValue("@customer", txtinvoice.Text);
                            connection.Open();
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                GvEXCELL.DataSource = dt;
                                GvEXCELL.DataBind();
                                connection.Close();

                                Response.Clear();
                                DateTime now = DateTime.Today;
                                string filename = "AllPendingReport_ " + now.ToString("dd/MM/yyyy");
                                Response.AddHeader("content-disposition", "attachment; filename = " + filename + ".xls");
                                Response.ContentType = "application/vnd.xls";
                                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                                System.Web.UI.HtmlTextWriter htmlWrite =
                                new HtmlTextWriter(stringWrite);
                                GvEXCELL.RenderControl(htmlWrite);
                                Response.Write(stringWrite.ToString());
                                Response.End();
                            }
                        }
                    }
                }


                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        else
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_PendingReportsDetails]", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action", "GetData");
                        //cmd.Parameters.AddWithValue("@customer", txtinvoice.Text);
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            GvEXCELL.DataSource = dt;
                            GvEXCELL.DataBind();
                            connection.Close();

                            Response.Clear();
                            DateTime now = DateTime.Today;
                            string filename = "AllPendingReport_ " + now.ToString("dd/MM/yyyy");
                            Response.AddHeader("content-disposition", "attachment; filename = " + filename + ".xls");
                            Response.ContentType = "application/vnd.xls";
                            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                            System.Web.UI.HtmlTextWriter htmlWrite =
                            new HtmlTextWriter(stringWrite);
                            GvEXCELL.RenderControl(htmlWrite);
                            Response.Write(stringWrite.ToString());
                            Response.End();
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    public void GetOAreportsDataCustomerwise()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_PendingReportsDetails]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "GetDataCustomerwise"));
                cmd.Parameters.Add(new SqlParameter("@customer", txtcustomer.Text));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                adapter.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    Gvreports.DataSource = Dt;
                    Gvreports.DataBind();
                }
                else
                {
                    Gvreports.DataSource = null;
                    Gvreports.DataBind();
                }
            }
        }
    }

    public void GetOAreportsDataDatwise()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_PendingReportsDetails]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "GetDataDatewise"));
                cmd.Parameters.Add(new SqlParameter("@fromdate", txtfromdate.Text));
                cmd.Parameters.Add(new SqlParameter("@todate", txttodate.Text));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                adapter.Fill(Dt);

                if (Dt.Rows.Count > 0)
                {
                    Gvreports.DataSource = Dt;
                    Gvreports.DataBind();
                }
                else
                {
                    Gvreports.DataSource = null;
                    Gvreports.DataBind();
                }
            }
        }
    }

    public void GetExcellData()
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_PendingReportsDetails]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "GetData");
                    //cmd.Parameters.AddWithValue("@customer", txtinvoice.Text);
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        GvEXCELL.DataSource = dt;
                        GvEXCELL.DataBind();
                        connection.Close();

                        Response.Clear();
                        DateTime now = DateTime.Today;
                        string filename = txtcustomer.Text + " Report_" + now.ToString("dd/MM/yyyy");
                        Response.AddHeader("content-disposition", "attachment; filename = " + filename + ".xls");
                        Response.ContentType = "application/vnd.xls";
                        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter htmlWrite =
                        new HtmlTextWriter(stringWrite);
                        GvEXCELL.RenderControl(htmlWrite);
                        Response.Write(stringWrite.ToString());
                        Response.End();
                    }
                }
            }
        }


        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetExcellDataCustomerWise()
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_PendingReportsDetails]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetDataCustomerwise"));
                    cmd.Parameters.Add(new SqlParameter("@customer", txtcustomer.Text));
                    DataTable Dt = new DataTable();
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        GvEXCELL.DataSource = dt;
                        GvEXCELL.DataBind();
                        connection.Close();

                        Response.Clear();
                        DateTime now = DateTime.Today;
                        string filename = txtcustomer.Text + " PendingReport " + now.ToString("dd/MM/yyyy");
                        Response.AddHeader("content-disposition", "attachment; filename = '" + filename + "'.xls");
                        Response.ContentType = "application/vnd.xls";
                        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter htmlWrite =
                        new HtmlTextWriter(stringWrite);
                        GvEXCELL.RenderControl(htmlWrite);
                        Response.Write(stringWrite.ToString());
                        Response.End();
                    }
                }
            }
        }


        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetExcellDataDatewise()
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[DB_ProcetechERP].[SP_PendingReportsDetails]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Action", "GetDataDatewise"));
                    cmd.Parameters.Add(new SqlParameter("@fromdate", txtfromdate.Text));
                    cmd.Parameters.Add(new SqlParameter("@todate", txttodate.Text));
                    DataTable Dt = new DataTable();
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        GvEXCELL.DataSource = dt;
                        GvEXCELL.DataBind();
                        connection.Close();

                        Response.Clear();
                        DateTime now = DateTime.Today;
                        string filename = " DateWiseReport_" + now.ToString("dd/MM/yyyy");
                        Response.AddHeader("content-disposition", "attachment; filename = " + filename + ".xls");
                        Response.ContentType = "application/vnd.xls";
                        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter htmlWrite =
                        new HtmlTextWriter(stringWrite);
                        GvEXCELL.RenderControl(htmlWrite);
                        Response.Write(stringWrite.ToString());
                        Response.End();
                    }
                }
            }
        }


        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkrefresh_Click(object sender, EventArgs e)
    {
        txtcustomer.Text = "";
        txtfromdate.Text = "";
        txttodate.Text = "";
        GvEXCELL.Visible = false;
        GetOAreportsData();
    }
}