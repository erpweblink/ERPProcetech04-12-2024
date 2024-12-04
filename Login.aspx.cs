using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;
public partial class Login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            if (Request.Cookies["Excuserid"] != null)
                txtemail.Text = Request.Cookies["Excuserid"].Value;
            if (Request.Cookies["Excpwd"] != null)
                txtpassword.Attributes.Add("value", Request.Cookies["Excpwd"].Value);
            if (Request.Cookies["Excuserid"] != null && Request.Cookies["Excpwd"] != null)
                chkremember.Checked = true;
        }
    }

    //01/10/2021
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        //////////////////
        ///Cls_Main.Conn_Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM employees WHERE name='" + txtemail.Text + "' AND panelpsw='" + txtpassword.Text + "'", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@name", txtemail.Text.Trim());
        cmd.Parameters.AddWithValue("@panelpsw", txtpassword.Text.Trim());cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                string Username = dr["name"].ToString();
                string Role = dr["Role"].ToString();
                string status = dr["status"].ToString();
                if (status == "True")
                {
                    if (!string.IsNullOrEmpty(Username))
                    {
                        if (chkremember.Checked == true)
                        {
                            Response.Cookies["Excuserid"].Value = txtemail.Text.ToLower().Trim();
                            Response.Cookies["Excpwd"].Value = txtpassword.Text.Trim();
                            Response.Cookies["Excuserid"].Expires = DateTime.Now.AddDays(30);
                            Response.Cookies["Excpwd"].Expires = DateTime.Now.AddDays(30);
                        }
                        else
                        {
                            Response.Cookies["Excuserid"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["Excpwd"].Expires = DateTime.Now.AddDays(-1);
                        }
                        Session["empcode"] = dr["empcode"].ToString();
                        string roleName = dr["role"].ToString();
                        Session["RoleName"] = roleName;
                        Session["name"] = dr["name"].ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel('Login Successfully..!!')", true);


                      //  Response.Redirect("~/Admin/Dashboard.aspx");

                        //if (Role == "Admin")
                        //{
                        //    Session["adminname"] = dr["name"].ToString();
                        //    Session["adminemail"] = dr["email"].ToString();
                        //    Session["adminemailpass"] = dr["emailpass"].ToString();
                        //    Response.Redirect("~/Admin/Dashboard.aspx");
                        //}
                        //if (Role == "User")
                        //{
                        //    // Session["Sessionuser"] = dr["name"].ToString();
                        //    // Response.Redirect("~/User/User_Dashboard.aspx");

                        //    // for testing
                        //    Session["username"] = dr["name"].ToString();
                        //    Session["useremail"] = dr["email"].ToString();
                        //    Session["useremailpass"] = dr["emailpass"].ToString();
                        //    Response.Redirect("~/Admin/Dashboard.aspx");
                        //}
                        //if (Role == "Super User")
                        //{
                        //    // Session["Sessionuser"] = dr["name"].ToString();
                        //    // Response.Redirect("~/User/User_Dashboard.aspx");

                        //    // for testing
                        //    Session["username"] = dr["name"].ToString();
                        //    Session["useremail"] = dr["email"].ToString();
                        //    Session["useremailpass"] = dr["emailpass"].ToString();
                        //    Response.Redirect("~/Admin/Dashboard.aspx");
                        //}
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabelerror('Login Failed, Activate Your Account First..!!')", true);
                    txtemail.Text = ""; txtpassword.Text = "";
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabelerror('Login Failed, Incorrect Username or Password..!!')", true);
            txtemail.Text = ""; txtpassword.Text = "";
        }
        cmd.Connection.Close();
        //////////////////



        //SqlCommand cmd = new SqlCommand("Select [name],[panelpsw],[Department],[role],[empcode] FROM [employees] where name=@uname COLLATE SQL_Latin1_General_CP1_CS_AS and panelpsw=@pass COLLATE SQL_Latin1_General_CP1_CS_AS", con);
        //cmd.CommandType = CommandType.Text;
        //cmd.Parameters.AddWithValue("@uname", txtemail.Text.Trim());
        //cmd.Parameters.AddWithValue("@pass", txtpassword.Text.Trim());
        //cmd.Connection.Open();
        //SqlDataReader dr = cmd.ExecuteReader();
        //if (dr.HasRows)
        //{
        //    while (dr.Read())
        //    {
        //        if (chkremember.Checked == true)
        //        {
        //            Response.Cookies["Excuserid"].Value = txtemail.Text.ToLower().Trim();
        //            Response.Cookies["Excpwd"].Value = txtpassword.Text.Trim();
        //            Response.Cookies["Excuserid"].Expires = DateTime.Now.AddDays(30);
        //            Response.Cookies["Excpwd"].Expires = DateTime.Now.AddDays(30);
        //        }
        //        else
        //        {
        //            Response.Cookies["Excuserid"].Expires = DateTime.Now.AddDays(-1);
        //            Response.Cookies["Excpwd"].Expires = DateTime.Now.AddDays(-1);
        //        }
        //        if (dr["role"].ToString() == "Admin")
        //        {
        //            string[] name = dr["name"].ToString().Split(' ');
        //            Session["ProductionName"] = name[0];
        //            Session["adminname"] = name[0];
        //            Session["empcode"] = dr["empcode"].ToString();
        //            string roleName = dr["role"].ToString();
        //            Session["RoleName"] = roleName;
        //            con.Close();
        //            SaveLog(name[0]);
        //            if (Session["adminname"] != null)
        //            {
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel('Login Successfully..!!')", true);                         //if (Role == "Admin")
        //                //Response.Redirect("~/Admin/AdminDashboard.aspx");
        //            }
        //        }
        //        else if (dr["Department"].ToString() == "Purchase")
        //        {
        //            string[] name = dr["name"].ToString().Split(' ');
        //            Session["ProductionName"] = name[0];
        //            Session["adminname"] = name[0];
        //            Session["empcode"] = dr["empcode"].ToString();
        //            string roleName = dr["role"].ToString();
        //            Session["RoleName"] = roleName;
        //            con.Close();
        //            SaveLog(name[0]);
        //            // CheckUserSession(txtemail.Text.Trim(), txtpassword.Text.Trim(), "Insert");
        //            if (Session["ProductionName"] != null)
        //            {
        //                Response.Redirect("~/Purchase/PurchaseDashboard.aspx");
        //            }
        //        }
        //        else if (dr["Department"].ToString() == "Account")
        //        {
        //            string[] name = dr["name"].ToString().Split(' ');
        //            Session["ProductionName"] = name[0];
        //            Session["adminname"] = name[0];
        //            Session["empcode"] = dr["empcode"].ToString();
        //            string roleName = dr["role"].ToString();
        //            Session["RoleName"] = roleName;
        //            con.Close();
        //            SaveLog(name[0]);
        //            // CheckUserSession(txtemail.Text.Trim(), txtpassword.Text.Trim(), "Insert");
        //            if (Session["ProductionName"] != null)
        //            {

        //                Response.Redirect("~/Account/AccountDashboard.aspx");
        //            }
        //        }

        //        //if (dr["role"].ToString() == "Sales")
        //        //{
        //        //    string[] name = dr["name"].ToString().Split(' ');
        //        //    Session["salesname"] = name[0];
        //        //    Session["salesempcode"] = dr["empcode"].ToString();
        //        //    con.Close();
        //        //    SaveLog(name[0]);
        //        //    if (Session["salesname"] != null)
        //        //    {

        //        //        Response.Redirect("~/Sales/SalesDashboard.aspx");
        //        //    }
        //        //}
        //        //if (dr["Department"].ToString() == "Production")
        //        //{
        //        //    string[] name = dr["name"].ToString().Split(' ');
        //        //    Session["ProductionName"] = name[0];
        //        //    Session["drawingempcode"] = dr["empcode"].ToString();
        //        //    string roleName = dr["role"].ToString();
        //        //    Session["RoleName"] = roleName;
        //        //    con.Close();
        //        //    SaveLog(name[0]);

        //        //    foreach (string item in roleName.Split(new char[] { ',' }))
        //        //    {
        //        //        if (item.Trim() == "drawing")
        //        //        {

        //        //            Response.Redirect("~/Production/Drawing.aspx");
        //        //        }
        //        //        else if (item.Trim() == "laserprogramming")
        //        //        {

        //        //            Response.Redirect("~/Production/LaserProgramming.aspx");
        //        //        }
        //        //        else if (item.Trim() == "lasercutting")
        //        //        {

        //        //            Response.Redirect("~/Production/LaserCutting.aspx");
        //        //        }
        //        //        else if (item.Trim() == "bending")
        //        //        {

        //        //            Response.Redirect("~/Production/CNCBending.aspx");
        //        //        }
        //        //        else if (item.Trim() == "welding")
        //        //        {

        //        //            Response.Redirect("~/Production/Welding.aspx");
        //        //        }
        //        //        else if (item.Trim() == "powdercoating")
        //        //        {

        //        //            Response.Redirect("~/Production/PowderCoating.aspx");
        //        //        }
        //        //        else if (item.Trim() == "assembly")
        //        //        {

        //        //            Response.Redirect("~/Production/FinalAssembly.aspx");
        //        //        }
        //        //    }
        //        //}
        //    }
        //}
        //else
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Invalid Login !!";
        //}
        //cmd.Connection.Close();
    }

    protected void SaveLog(string LoginID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SP_AllLogs", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoginID", LoginID);
            cmd.Parameters.AddWithValue("@Action", "Login");
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception)
        {

            throw;
        }

    }

}

