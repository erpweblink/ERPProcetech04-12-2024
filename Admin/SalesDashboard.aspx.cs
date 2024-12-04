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
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;

public partial class Admin_SalesDashboard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["salesname"] == null || Session["salesempcode"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //}
        //else
        //{
        //    if (!IsPostBack)
        //    {
        //        Gvbind(); ddlhistorytype.Text = "All"; GvTBROBind(); getlastlogindetail();
        //        getClientsCountdetail(); dtfullhistory.Clear();
        //    }
        //}
    }

   
}