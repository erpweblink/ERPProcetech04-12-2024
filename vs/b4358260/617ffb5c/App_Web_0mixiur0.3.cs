#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\UserAuthorization.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AA9C167B4D79C4CBD65F2EEEE31FBE8E1084F4FA"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\UserAuthorization.aspx.cs"
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserAuthorization : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    string Pages = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRole();
            
            GridDiv.Visible = false;
        }
    }

    protected void BindUser()
    {
        try
        {
            ddluser.Items.Clear();
            ddluser.Items.Add(new ListItem("--Select User--", ""));
            DataTable Dt = new DataTable();

            SqlDataAdapter Da = new SqlDataAdapter("Select id,name From employees Where Role='" + ddlrole.SelectedItem.Text + "' ", con);
            Da.Fill(Dt);
            ddluser.DataTextField = "name";
            ddluser.DataValueField = "id";
            ddluser.DataSource = Dt;
            ddluser.DataBind();
            //ddluser.Items.Insert(0, "-- Select User --");
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void BindRole()
    {
        try
        {
            ddlrole.Items.Clear();
            ddlrole.Items.Add(new ListItem("--Select Role--", ""));
            DataTable Dt = new DataTable();

            SqlDataAdapter Da = new SqlDataAdapter("Select Id,Role From tbl_RoleMaster ", con);
            Da.Fill(Dt);
            ddlrole.DataTextField = "Role";
            ddlrole.DataValueField = "Id";
            ddlrole.DataSource = Dt;
            ddlrole.DataBind();
            //ddluser.Items.Insert(0, "-- Select User --");
            BindUser();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUser();
    }

    protected void ddluser_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //con.Open();
            GridDiv.Visible = true;
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("SELECT * FROM [tblUserRoleAuthorization] where [UserID]='" + ddluser.SelectedItem.Value + "'", con);
            if (Dt.Rows.Count > 0)
            {
               
                btnSubmit.Text = "Update";
                DataTable Dtt = new DataTable();
                SqlDataAdapter Daa = new SqlDataAdapter("SELECT * FROM [tblUserRoleAuthorization] where [UserID]='" + ddluser.SelectedItem.Value + "'", con);
                Daa.Fill(Dtt);
                gvUserAuthorization.EmptyDataText = "No Records Found";
                gvUserAuthorization.DataSource = Dtt;
                gvUserAuthorization.DataBind();
                //con.Close();
            }
            else
            {
                //con.Close();
                
                btnSubmit.Text = "Save";
                DataTable Dttt = new DataTable();
                SqlDataAdapter Daaa = new SqlDataAdapter("SELECT * FROM [tblAuthPages]", con);
                Daaa.Fill(Dttt);
                gvUserAuthorization.EmptyDataText = "No Records Found";
                gvUserAuthorization.DataSource = Dttt;
                //con.Open();
                gvUserAuthorization.DataBind();
                //con.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvUserAuthorization_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            con.Open();
            int id = Convert.ToInt32(gvUserAuthorization.DataKeys[e.Row.RowIndex].Values[0]);
            CheckBox chkpages = (CheckBox)e.Row.FindControl("chkPages");
            SqlCommand cmd = new SqlCommand("select Pages from tblUserRoleAuthorization where ID='" + id + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Pages = dr["Pages"].ToString();
                dr.Close();
            }
            chkpages.Checked = Pages == "True" ? true : false;
            con.Close();
        }
    }

    protected void gvUserAuthorization_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        int RowIndex = grv.RowIndex;
        CheckBox chkpages = (CheckBox)gvUserAuthorization.Rows[RowIndex].FindControl("chkPages");
        chkpages.Checked = chkpages.Checked == true ? false : true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSubmit.Text == "Save")
            {
                foreach (GridViewRow g1 in gvUserAuthorization.Rows)
                {
                    string menuname = (g1.FindControl("lblMenuName") as Label).Text;
                    string pagename = (g1.FindControl("lblPageName") as Label).Text;
                    string menu = (g1.FindControl("lblMenuId") as Label).Text;
                    int userId = Convert.ToInt32(ddluser.SelectedItem.Value);
                    bool pageChk = (g1.FindControl("chkPages") as CheckBox).Checked;
                    DateTime Date = DateTime.Now;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_UAuthorization", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", ddluser.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@UserName", ddluser.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@menuId", menu);
                    cmd.Parameters.AddWithValue("@MenuName", menuname);
                    cmd.Parameters.AddWithValue("@PageName", pagename);
                    cmd.Parameters.AddWithValue("@createdBy", Session["name"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedDate", Date);
                    cmd.Parameters.AddWithValue("@Pages", pageChk);
                    cmd.Parameters.AddWithValue("@Action", "Insert");
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Pages Authorized Successfully..!!'); window.location='UserAuthorization.aspx';", true);
            }
            else if (btnSubmit.Text == "Update")
            {
                foreach (GridViewRow g1 in gvUserAuthorization.Rows)
                {
                    string menuname = (g1.FindControl("lblMenuName") as Label).Text;
                    string pagename = (g1.FindControl("lblPageName") as Label).Text;
                    string menu = (g1.FindControl("lblMenuId") as Label).Text;
                    bool pageChk = (g1.FindControl("chkPages") as CheckBox).Checked;
                    int userId = Convert.ToInt32(ddluser.SelectedItem.Value);
                    DateTime Date = DateTime.Now;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_UAuthorization", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@UserName", ddluser.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@menuId", menu);
                    cmd.Parameters.AddWithValue("@MenuName", menuname);
                    cmd.Parameters.AddWithValue("@PageName", pagename);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["name"].ToString());
                    cmd.Parameters.AddWithValue("@updatedDate", Date);
                    cmd.Parameters.AddWithValue("@Pages", pageChk);
                    cmd.Parameters.AddWithValue("@Action", "Update");
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Pages Authorized Successfully..!!'); window.location='UserAuthorization.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAuthorization.aspx");
    }
}

#line default
#line hidden
