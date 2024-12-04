#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\DepartmentWiseReport.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "47416FC5A758C2819DE99EA1DC6C3FBA58599344"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\DepartmentWiseReport.aspx.cs"
using System;
using System.Collections;
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

public partial class Admin_DepartmentWiseReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    CommonCls objClass = new CommonCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            OANumberDDLbind();
        }
    }

    private void OANumberDDLbind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select DrawingId,SubOA from [dbo].[tblLaserPrograming] where CustomerName='" + txtcname.Text + "'  order by LaserProgId desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlONumber.DataSource = dt;
            ddlONumber.DataTextField = "SubOA";
            ddlONumber.DataValueField = "SubOA";
            ddlONumber.DataBind();
            //ddlONumber.Items.Insert(0, "All");
            ddlONumber.Items.Insert(0, "--Select--");
        }
    }

    protected void GetDrawingRptData()
    {
        try
        {
            SqlCommand cmand = new SqlCommand("select OANumber,CustomerName,TotalQty,Size from tblLaserPrograming WHERE (SubOA = '" + ddlONumber.SelectedValue + "' OR ISNULL('" + ddlONumber.SelectedValue + "', '') =null)", con);
            con.Open();
            SqlDataReader dr = cmand.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lblCustName.Text = (string)dr["CustomerName"];
                    lbldesciption.Text = (string)dr["Size"];
                    lbloaNumber.Text = (string)dr["OANumber"];
                }
            }
            dr.Close();
            con.Close();

            SqlCommand cmdtot = new SqlCommand("select SUM(CAST(TotalQty as int)) as Tot from tblLaserPrograming WHERE (SubOA = '" + ddlONumber.SelectedValue + "' OR ISNULL('" + ddlONumber.SelectedValue + "', '') =null)", con);
            con.Open();
            Object Tot = cmdtot.ExecuteScalar();
            lblqnty.Text = Tot.ToString();
            con.Close();


            if (ddlONumber.SelectedValue != "--Select--")
            {
                string query = string.Empty;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_OAorCustomerWiseReport";
                cmd.Parameters.AddWithValue("@SubOA", ddlONumber.SelectedValue == "--Select--" ? (object)DBNull.Value : ddlONumber.SelectedValue);
                cmd.Connection = con;
                con.Open();
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dgvDrawingRpt.EmptyDataText = "No Records Found";
                        dgvDrawingRpt.DataSource = reader;
                        dgvDrawingRpt.DataBind();
                        divDetails.Visible = true;
                        divdgvDrawingRpt.Visible = true;
                        divdgvCustomerWise.Visible = false;
                        //dgvDrawingRpt.ShowHeader = false;
                        BindDeprt();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select Sub OA..!');", true);
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
            if (ddlONumber.SelectedValue == "--Select--")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select atleast one filter..!');", true);
                divIndicate.Visible = false;
            }
            else
            {
                divIndicate.Visible = true;
                divDrawing.Visible = true;
                GetDrawingRptData();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void BindDeprt()
    {
        try
        {
            ArrayList alstNames = new ArrayList();
            alstNames.Add("Drawing Creation");
            alstNames.Add("Laser Programing");
            alstNames.Add("Laser Cutting");
            alstNames.Add("CNC Bending");
            alstNames.Add("Welding");
            alstNames.Add("Powder Coating");
            alstNames.Add("Final Assembly");
            alstNames.Add("Final Inspection");
            alstNames.Add("Stock");
            dgvDeprt.DataSource = alstNames;
            dgvDeprt.DataBind();
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

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (dgvDrawingRpt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('No Data Found..!');", true);
            }
            else
            {
                Response.Redirect("../Reports/ReportPDF.aspx?OANumber=" + objClass.encrypt(ddlONumber.SelectedValue) + "");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void dgvDrawingRpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string status = e.Row.Cells[10].Text;
            //foreach (TableCell cell in e.Row.Cells)
            //{
            //    if (status == "True")
            //    {
            //        cell.BackColor = Color.Green;
            //        cell.ForeColor = Color.White;
            //    }
            //    if (status == "&nbsp;")
            //    {
            //        cell.BackColor = Color.Yellow;
            //    }
            //}
            //TableCell statusCell = e.Row.Cells[10];
            //if (statusCell.Text == "True")
            //{
            //    statusCell.Text = "COMPLETED";
            //}
            //if (statusCell.Text == "&nbsp;")
            //{
            //    statusCell.Text = "PENDING";
            //}

        }
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
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
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select distinct(cname) from company where " + "cname like @Search + '%'";

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
    protected void txtcname_TextChanged(object sender, EventArgs e)
    {
        OANumberDDLbind();
        divlabl.Visible = true;
        divtxt.Visible = true;
    }
}

#line default
#line hidden
