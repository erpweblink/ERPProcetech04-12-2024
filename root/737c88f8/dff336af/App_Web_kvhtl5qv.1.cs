#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Reports\ReportPDF.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EF533F9CA6C00688386F6FC8E822335058C1E331"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Reports\ReportPDF.aspx.cs"

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Admin_ReportPDF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    CommonCls objClass = new CommonCls();
    string InwardQty = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string OANumber = "", OANumberWise = "";
            string Dept = objClass.Decrypt(Request.QueryString["Dept"]);
            if (Request.QueryString["OANumber"] != null)
            {
                OANumber = objClass.Decrypt(Request.QueryString["OANumber"]);
                GetDeptWiseRptData(Dept, OANumber);
            }
            if (Request.QueryString["OANumberWise"] != null)
            {
                OANumberWise = objClass.Decrypt(Request.QueryString["OANumberWise"]);
                GetOANumberWiseRptData(Dept, OANumberWise);
            }

            spnDate.Text = "Date of Report: " + DateTime.Now.ToLongDateString();
            spnTime.Text = "Time of Report: " + DateTime.Now.ToShortTimeString();

            if (Dept == "Drawing")
                lblDeprtment.Text = "Drawing Creation";
            else if (Dept == "Laser Programing")
                lblDeprtment.Text = "Laser Programing";
            else if (Dept == "Laser Cutting")
                lblDeprtment.Text = "Laser Cutting";
            else if (Dept == "CNC Bending")
                lblDeprtment.Text = "CNC Bending";
            else if (Dept == "Welding")
                lblDeprtment.Text = "Welding";
            else if (Dept == "Powder Coating")
                lblDeprtment.Text = "Powder Coating";
            else if (Dept == "Final Assembly")
                lblDeprtment.Text = "Final Assembly";
            else if (Dept == "Final Inspection")
                lblDeprtment.Text = "Final Inspection";
            else if (Dept == "Dispatch")
                lblDeprtment.Text = "Dispatch";
        }
    }

    public string Before(string value, string a)
    {
        int posA = value.IndexOf(a);
        if (posA == -1)
        {
            return "";
        }
        return value.Substring(0, posA);
    }

    public string Between(string STR, string FirstString, string LastString)
    {
        string FinalString;
        int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
        int Pos2 = STR.IndexOf(LastString);
        FinalString = STR.Substring(Pos1, Pos2 - Pos1);
        return FinalString;
    }

    protected void GetDeptWiseRptData(string Dept, string OANumber)
    {
        try
        {
            string query = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetDeptWiseReport";
            cmd.Parameters.AddWithValue("@DepartmentName", Dept);
            cmd.Parameters.AddWithValue("@OANumber", OANumber == "--Select--" ? (object)DBNull.Value : OANumber);
            cmd.Connection = con;
            try
            {
                con.Open();
                dgvDeptWiseRpt.EmptyDataText = "No Records Found";
                dgvDeptWiseRpt.DataSource = cmd.ExecuteReader();
                dgvDeptWiseRpt.DataBind();
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

    protected void GetOANumberWiseRptData(string Dept, string OANumber)
    {
        try
        {
            string query = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetOANumberWiseReport";
            cmd.Parameters.AddWithValue("@DepartmentName", Dept);
            cmd.Parameters.AddWithValue("@OANumber", OANumber);
            cmd.Connection = con;
            try
            {
                con.Open();
                dgvDeptWiseRpt.EmptyDataText = "No Records Found";
                dgvDeptWiseRpt.DataSource = cmd.ExecuteReader();
                dgvDeptWiseRpt.DataBind();
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
}

#line default
#line hidden
