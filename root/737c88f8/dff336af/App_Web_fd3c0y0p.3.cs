#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\Default2.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B2B9B1188C4C2A2AC295DE260965CD8308E6AB9F"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\Default2.aspx.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void OpenWindow(object sender, EventArgs e)
    {
        string url = "Popup.aspx";
        string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}

#line default
#line hidden
