#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Sales\SalesMasterPage.master.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2A32AB8E7FD5103253D48CBBC21B5B51B35C74CF"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Sales\SalesMasterPage.master.cs"
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


#line default
#line hidden
