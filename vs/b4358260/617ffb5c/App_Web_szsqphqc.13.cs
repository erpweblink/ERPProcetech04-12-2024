#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\EnquiryFile.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DC4C7FFC70C072640217608D958F922963529219"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\EnquiryFile.aspx.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Admin_EnquiryFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Fileid"] !=null)
        {
            Display(Request.QueryString["Fileid"].ToString());
        }
        else
        {
            lblnotfound.Text = "File Not Found or Not Available !!";
        }   
    }
    public void Display(string id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string CmdText = "SELECT [Id],'../'+[filepath] as Path FROM [EnquiryData] where id='" + id + "'";

                SqlDataAdapter ad = new SqlDataAdapter(CmdText, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Response.Write(dt.Rows[0]["Path"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Path"].ToString()))
                    {
                        Response.Redirect(dt.Rows[0]["Path"].ToString());
                    }
                    else
                    {
                        lblnotfound.Text = "File Not Found or Not Available !!";
                    }
                }
                else
                {
                    lblnotfound.Text = "File Not Found or Not Available !!";
                }

            }
        }
    }

}

#line default
#line hidden
