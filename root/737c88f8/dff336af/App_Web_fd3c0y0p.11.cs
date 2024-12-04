#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\DepartmentWiseRpt.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4E9E06CD1EE2F47814E5D285CE95CD92498C06FE"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\DepartmentWiseRpt.aspx.cs"
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

public partial class Admin_DepartmentWiseRpt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    CommonCls objClass = new CommonCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            
        }
    }
   
    protected void GetDepartmentWiseRptData()
    {
        try
        {
            lblDeptNameHeader.Text = ddlDepartment.SelectedItem.ToString();
            string query = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetDepartmentWiseRpt";
            cmd.Parameters.AddWithValue("@DepartmentName", ddlDepartment.SelectedValue == "--Select--" ? (object)DBNull.Value : ddlDepartment.SelectedValue);
            cmd.Connection = con;
            con.Open();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //bind the 1st resultset
                    dgvDepartmentWiseRpt.EmptyDataText = "No Records Found";
                    dgvDepartmentWiseRpt.DataSource = reader;
                    dgvDepartmentWiseRpt.DataBind();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvDepartmentWiseRpt.ClientID + "', 900, 1020 , 40 ,true); </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            if (ddlDepartment.SelectedValue == "--Select--" && ddlDepartment.SelectedValue == "--Select--")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select Department..!');", true);
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
        Response.Redirect("DepartmentWiseReport.aspx");
    }
    
}

#line default
#line hidden
