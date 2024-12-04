#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\DrawingNew.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C93B4632A199FECFF5414EB543694FB9EFF00045"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\DrawingNew.aspx.cs"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DrawingNew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string InwardQty = "";
    string LaserID = "";
    DataTable dtdata = new DataTable();
    CommonCls objClass = new CommonCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            GetDrawingCreationData();
        }
    }

    protected void GetDrawingCreationData()
    {
        try
        {
            string query = string.Empty;

            query = @"SELECT [VWID],[id] as mainID, [OAno],[currentdate],[customername],[deliverydatereqbycust],[IsDrawingcomplete],[Description],[Qty],[Price],[Discount]
,[TotalAmount],[CGST],[SGST],[IGST],[SubOANumber] FROM vwOrderAccept where IsComplete is null order by deliverydatereqbycust asc";


            SqlDataAdapter ad = new SqlDataAdapter(query, con);

            ad.Fill(dtdata);
            if (dtdata.Rows.Count > 0)
            {
                dgvDrawing.DataSource = dtdata;
                dgvDrawing.DataBind();
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + dgvDrawing.ClientID + "', 400, 1020 , 40 ,true); </script>", false);
            }
            else
            {
                dgvDrawing.DataSource = null;
                dgvDrawing.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Pending Record Not Found..!');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtOutwardQty_TextChanged(object sender, EventArgs e)
    {
        ViewState["Iscomplete"] = "1";
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
    }

    private void calculationA(GridViewRow row)
    {
        TextBox txt_Inward = (TextBox)row.FindControl("txtInwardQty");
        TextBox txt_Outward = (TextBox)row.FindControl("txtOutwardQty");
        txt_Inward.Text = (Convert.ToDecimal(txt_Inward.Text.Trim()) - Convert.ToDecimal(txt_Outward.Text.Trim())).ToString();
    }
}

#line default
#line hidden
