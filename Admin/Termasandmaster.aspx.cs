using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Termasandmaster : System.Web.UI.Page
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SP_TermsMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Action", "InsertDetails");
        cmd.Parameters.AddWithValue("@Frightinsurance", txtfreighCharges.Text);
        cmd.Parameters.AddWithValue("@PackungCharge", txtpackingcharge.Text);
        cmd.Parameters.AddWithValue("@Inspection", txtInspection.Text);
        cmd.Parameters.AddWithValue("@Damages", txtDamage.Text);
        cmd.Parameters.AddWithValue("@afterSalesServices", txtAfterSalesServices.Text);
        cmd.Parameters.AddWithValue("@Warranty", txtWarranty.Text);
        cmd.Parameters.AddWithValue("@Validity", txtValidity.Text);
        cmd.Parameters.AddWithValue("@Jurisdiction", txtJurisdiction.Text);
        cmd.Parameters.AddWithValue("@SpecialNote", txtSpecialNote.Text);
        cmd.Parameters.AddWithValue("@RefNo", txtrefno.Text);
        cmd.Parameters.AddWithValue("@CreatedBy", Session["name"].ToString());
        int a = 0;
        cmd.Connection.Open();
        a = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        if (a < 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('User Added Sucessfully');window.location='Addusers.aspx';", true);
        }
    }


 
}