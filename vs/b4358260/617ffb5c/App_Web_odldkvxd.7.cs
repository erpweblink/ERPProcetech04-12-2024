#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\userprofile.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "79676F4A07C5E29EE5A5D74CEA76005EB4FE177E"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\userprofile.aspx.cs"
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

public partial class Admin_userprofile : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["salesname"] == null || Session["salesempcode"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                GetUserData(Session["salesempcode"].ToString());
            }
        }
       
    }
    
    protected void btnadd_Click(object sender, EventArgs e)
    {
        #region Update
        if (btnadd.Text == "Update")
        {
            SqlCommand cmd = new SqlCommand("SP_employees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "UpdateUser");
            cmd.Parameters.AddWithValue("@empcode", Session["salesempcode"].ToString());
            cmd.Parameters.AddWithValue("@name", txtname.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtemail.Text.ToLower().Trim());
            cmd.Parameters.AddWithValue("@emailpsw", txtemailpsw.Text.Trim());
            cmd.Parameters.AddWithValue("@panelpsw", txtpanelpsw.Text.Trim());
            cmd.Parameters.AddWithValue("@mobile", txtmobile.Text.Trim());
            //cmd.Parameters.AddWithValue("@role", ddldept.Text);

            int a = 0;
            cmd.Connection.Open();
            a = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            if (a > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Updated Sucessfully');window.location='userprofile.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Updated !!');", true);
            }
        }
        #endregion Update
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesDashboard.aspx");
    }


    protected void GetUserData(string id)
    {
        string query1 = string.Empty;
        query1 = "SELECT [id],[name],[email],[emailpsw],[panelpsw],[mobile],[role],[status] FROM [employees] where [empcode]='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtname.Text = dt.Rows[0]["name"].ToString();
            txtemail.Text = dt.Rows[0]["email"].ToString();
            txtemailpsw.Attributes.Add("value", dt.Rows[0]["emailpsw"].ToString());
            txtpanelpsw.Attributes.Add("value", dt.Rows[0]["panelpsw"].ToString());
            txtmobile.Text = dt.Rows[0]["mobile"].ToString();
            ddldept.Text = dt.Rows[0]["role"].ToString();

            btnadd.Text = "Update";
        }
    }

    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],[email] FROM [employees] where [isdeleted]=0 and email='" + txtemail.Text.Trim() + "' ", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Email Already Existing !!";
            btnadd.Enabled = false;
        }
        else
        {
            btnadd.Enabled = true;
            lblmsg.Visible = false;
        }
    }
}

#line default
#line hidden
