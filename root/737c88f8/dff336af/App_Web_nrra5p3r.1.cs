#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\Addusers.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0E2A49038250939FEF3E9FA6AF9BE88694160051"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\Addusers.aspx.cs"
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

public partial class Admin_Addusers : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EmpCode = string.Empty;
            Gvbind();
            Bind_Role();
        }
    }

    private void Gvbind()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT [id],[empcode],[name],[email],[emailpsw],[panelpsw],[mobile],[role],[status],[regdate] FROM [employees] where [isdeleted]=0 order by id desc", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvUsers.DataSource = dt;
            GvUsers.DataBind();
        }
    }

    static string EmpCode = string.Empty;
	string Rolesdata = "";
    protected void GenerateEmpCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([id]) as maxid FROM [employees]", con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            EmpCode = "ExcelEnc/0" + dt.Rows[0]["maxid"].ToString() + 1;
        }
        else
        {
            EmpCode = string.Empty;
        }
    }

    //01/10/2021
    protected void btnadd_Click(object sender, EventArgs e)
    {
        #region Insert
        if (btnadd.Text == "Add User")
        {
            if (ddldept.SelectedItem.Text == "Production")
            {
                if (chkRoleList.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select at leaset one Role...!!');", true);
                    chkRoleList.Focus();
                }
                else
                {
                    insertData();
                }
            }
            else
            {
                insertData();
            }
        }
        #endregion Insert

        #region Update
        if (btnadd.Text == "Update")
        {
            SqlCommand cmd = new SqlCommand("SP_employees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Update");
            cmd.Parameters.AddWithValue("@id", ViewState["id"].ToString());
            cmd.Parameters.AddWithValue("@name", txtname.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtemail.Text.ToLower().Trim());
            cmd.Parameters.AddWithValue("@emailpsw", txtemailpsw.Text.Trim());
            cmd.Parameters.AddWithValue("@panelpsw", txtpanelpsw.Text.Trim());
            cmd.Parameters.AddWithValue("@mobile", txtmobile.Text.Trim());
            cmd.Parameters.AddWithValue("@role", ddlRole.SelectedItem.Text.Trim());

            if (ddldept.SelectedItem.Text == "Admin")
            {
                Rolesdata = "Admin";
            }
            else
            {
                Rolesdata = GetCheckBoxListSelections();
            }
            cmd.Parameters.AddWithValue("@role", Rolesdata);
            cmd.Parameters.AddWithValue("@department", ddldept.Text);
            int Statusvalue;
            if (ddlstatus.Text == "Activate")
            {
                Statusvalue = 1;
                cmd.Parameters.AddWithValue("@status", Statusvalue);
            }
            else
            {
                Statusvalue = 0;
                cmd.Parameters.AddWithValue("@status", Statusvalue);
            }

            int a = 0;
            cmd.Connection.Open();
            a = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            if (a > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('Data Updated Sucessfully');window.location='Addusers.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Updated !!');", true);
            }

        }
        #endregion Update
    }

    //01/10/2021
    public void insertData()
    {
        GenerateEmpCode();
        if (!string.IsNullOrEmpty(EmpCode))
        {
            SqlCommand cmd = new SqlCommand("SP_employees", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "Insert");
            cmd.Parameters.AddWithValue("@empcode", EmpCode);
            cmd.Parameters.AddWithValue("@name", txtname.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtemail.Text.ToLower().Trim());
            cmd.Parameters.AddWithValue("@emailpsw", txtemailpsw.Text.Trim());
            cmd.Parameters.AddWithValue("@panelpsw", txtpanelpsw.Text.Trim());
            cmd.Parameters.AddWithValue("@mobile", txtmobile.Text.Trim());
            cmd.Parameters.AddWithValue("@role", ddlRole.SelectedItem.Text.Trim());

            if (ddldept.SelectedItem.Text == "Admin")
            {
                Rolesdata = "Admin";
            }
            else
            {
                Rolesdata = GetCheckBoxListSelections();
            }

            //cmd.Parameters.AddWithValue("@role", Rolesdata);
            cmd.Parameters.AddWithValue("@department", ddldept.Text);
            int Statusvalue;
            if (ddlstatus.Text == "Activate")
            {
                Statusvalue = 1;
                cmd.Parameters.AddWithValue("@status", Statusvalue);
            }
            else
            {
                Statusvalue = 0;
                cmd.Parameters.AddWithValue("@status", Statusvalue);
            }

            int a = 0;
            cmd.Connection.Open();
            a = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            if (a > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "alert('User Added Sucessfully');window.location='Addusers.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Data Not Saved !!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Employee Code Generation Problem Please Try Again !!');", true);
        }
    }

	private string GetCheckBoxListSelections()
    {
        List<string> Roles = new List<string>();
        for (int i = 0; i < chkRoleList.Items.Count; i++)
        {
            if (chkRoleList.Items[i].Selected)
            {
                Roles.Add(chkRoleList.Items[i].Value);
                //if you want values instead of text then items.Add(cblCourses.Items[i].Value);
            }
        }
        return String.Join(", ", Roles.ToArray());
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addusers.aspx");
    }

    protected void GvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int userid = Convert.ToInt32(GvUsers.DataKeys[e.RowIndex].Value.ToString());
            using (SqlCommand cmm = new SqlCommand())
            {
                cmm.Connection = con;
                cmm.CommandType = CommandType.Text;
                cmm.CommandText = "Update [employees] set isdeleted=1 where id='" + userid + "'";
                cmm.Connection.Open();
                int a = 0;
                a = cmm.ExecuteNonQuery();
                cmm.Connection.Close();
                if (a > 0)
                {
                    Gvbind();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('Delete Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Not Deleted !!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

        }
    }

    protected void GvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        #region Account Status
        if (e.CommandName == "status")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;
            LinkButton linkaccount = (LinkButton)GvUsers.Rows[RowIndex].FindControl("linkaccount");
            try
            {
                using (SqlCommand cmm = new SqlCommand())
                {
                    int value = 0;
                    if (linkaccount.Text == "Deactivated")
                    {
                        value = 1;
                    }
                    if (linkaccount.Text == "Activated")
                    {
                        value = 0;
                    }
                    cmm.Connection = con;
                    cmm.CommandType = CommandType.Text;
                    cmm.CommandText = "Update [employees] set [status]='" + value + "'  where id='" + id + "'";
                    cmm.Connection.Open();
                    int a = 0;
                    a = cmm.ExecuteNonQuery();
                    cmm.Connection.Close();
                    if (a > 0)
                    {
                        //if (linkaccount.Text == "Activated")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Account is Deactivated Now !!');", true);
                        //}
                        //if (linkaccount.Text == "Deactivated")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Account is Activated Now !!');", true);
                        //}

                        Gvbind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Not Updated !!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #endregion Account Status

        if (e.CommandName == "RowEdit")
        {
            ViewState["id"] = e.CommandArgument.ToString();
            GetUserData(e.CommandArgument.ToString());
        }

        if (e.CommandName == "empname")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                GetUserDataPopup(e.CommandArgument.ToString());
                this.modelprofile.Show();
            }
        }
    }

    //01/10/2021
    protected void GetUserData(string id)
    {
        chkRoleList.ClearSelection();
        string query1 = string.Empty;
        query1 = "SELECT [id],[name],[email],[emailpsw],[panelpsw],[mobile],[Department],[role],[status] FROM [employees] where id='" + id + "'";
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
            ddldept.Text = dt.Rows[0]["Department"].ToString();
            ddlRole.Text = dt.Rows[0]["role"].ToString();

            //01/10/2021
            string roledata = dt.Rows[0]["role"].ToString();
            if (roledata == "")
            {
                if (roledata != "Admin")
                {
                    divRole.Visible = true;
                }
                else
                {
                    divRole.Visible = false;
                }
            }
            else
            {
                if (roledata != "Admin")
                {
                    divRole.Visible = true;
                    foreach (string item in roledata.Split(new char[] { ',' }))
                    {

                        chkRoleList.Items.FindByValue(item.Trim()).Selected = true;
                    }
                }
                else
                {
                    divRole.Visible = false;
                }
            }

            if (dt.Rows[0]["status"].ToString() == "True")
            {
                ddlstatus.Text = "Activate";
            }
            if (dt.Rows[0]["status"].ToString() == "False")
            {
                ddlstatus.Text = "Deactivate";
            }

            btnadd.Text = "Update";
        }
    }

    protected void GetUserDataPopup(string id)
    {
        string query1 = string.Empty;
        query1 = "SELECT [id],[name],[email],[emailpsw],[panelpsw],[mobile],[role],[status],[regdate] FROM [employees] where id='" + id + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(query1, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblname.Text = dt.Rows[0]["name"].ToString();
            lblemail.Text = dt.Rows[0]["email"].ToString();
            txtemailpsw1.Text = dt.Rows[0]["emailpsw"].ToString();
            txtpanelpsw1.Text = dt.Rows[0]["panelpsw"].ToString();
            lblmobile.Text = dt.Rows[0]["mobile"].ToString();
            lbldept.Text = dt.Rows[0]["role"].ToString();
            if (dt.Rows[0]["status"].ToString() == "True")
            {
                lblstatus.Text = "Activated";
                lblstatus.ForeColor = System.Drawing.Color.Green;
            }
            if (dt.Rows[0]["status"].ToString() == "False")
            {
                lblstatus.Text = "Deactivated";
                lblstatus.ForeColor = System.Drawing.Color.Red;
            }
            lbldate.Text = dt.Rows[0]["regdate"].ToString();
        }
    }

    protected void GvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvUsers.PageIndex = e.NewPageIndex;
        Gvbind();
    }

    protected void GvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            LinkButton linkaccount = (LinkButton)e.Row.FindControl("linkaccount");

            if (lblstatus.Text == "False")
            {
                linkaccount.Text = "Deactivated";
                linkaccount.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                linkaccount.Text = "Activated";
                linkaccount.ForeColor = System.Drawing.Color.Green;
            }

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
	//01/10/2021
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldept.SelectedItem.Text == "Production")
        {
            divRole.Visible = true;
        }
        else
        {
            divRole.Visible = false;
        }
    }

    //18/10/2022
    private void Bind_Role()
    {
        try
        {
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("SELECT * FROM tbl_RoleMaster", con);
            Da.Fill(Dt);

            ddlRole.DataTextField = "Role";
            ddlRole.DataValueField = "Id";
            ddlRole.DataSource = Dt;
            ddlRole.DataBind();

            ddlRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", "0"));

        }
        catch (Exception)
        {

            throw;
        }
    }
}

#line default
#line hidden
