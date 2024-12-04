#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\testcount.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2D4B32D98E44B5CEBDE66A05285A769963E8CB3E"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\testcount.aspx.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testcount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string abc = TextBox1.Text;
        int i = 0;
        foreach (var item in abc)
        {
            i++;
        }
        Response.Write(i);
    }
}

#line default
#line hidden
