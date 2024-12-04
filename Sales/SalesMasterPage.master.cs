using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sales_SalesMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["salesname"] == null || Session["salesempcode"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            lblusername.Text = Session["salesname"].ToString();
        }
    }
}
