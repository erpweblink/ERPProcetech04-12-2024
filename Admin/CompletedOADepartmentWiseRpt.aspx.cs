using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CompletedOADepartmentWiseRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    CommonCls objClass = new CommonCls();
    private object divtxt;

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

            }
        }
    }

    protected void GetDepartmentWiseRptData()
    {
        try
        {
            DateTime date;
            using (SqlCommand Cmd = new SqlCommand("SP_GetCompletedOADepartmentWiseRpt", con))
            {
                //DateTime date = Convert.ToDateTime(txtDate.Text.Trim());
                // string dateString = date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //date = DateTime.ParseExact(txtDate.Text.Trim(), "MM/dd/yyyy", null);
                date = Convert.ToDateTime(txtDate.Text.Trim(), System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Cmd.Connection = con;
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@DepartmentName", ddlDepartment.SelectedValue == "--Select--" ? (object)DBNull.Value : ddlDepartment.SelectedValue);
                    Cmd.Parameters.AddWithValue("@Date", date.ToString("M/d/yyyy"));
                    Da.SelectCommand = Cmd;
                    using (DataTable Dt = new DataTable())
                    {
                        lblDeptNameHeader.Text = ddlDepartment.SelectedValue;
                        Da.Fill(Dt);
                        dgvDepartmentWiseRpt.EmptyDataText = "No Records Found";
                        dgvDepartmentWiseRpt.DataSource = Dt;
                        dgvDepartmentWiseRpt.DataBind();

                    }
                }
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvDepartmentWiseRpt.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select filters..!');", true);
            }
            else
            {

                GetDepartmentWiseRptData();
                divDrawing.Visible = true;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompletedOADepartmentWiseRpt.aspx");
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        //GetDepartmentWiseRptData();
    }

    protected void ddlDepartment_TextChanged(object sender, EventArgs e)
    {
        //GetDepartmentWiseRptData();
        txtDate.Visible = true;
    }
}