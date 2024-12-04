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
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class Admin_EnquiryFile : System.Web.UI.Page
{
    string id1;
    string id2;
    string Fpath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (Request.QueryString["Fileid1"] != null)
            {
                id2 = Decrypt(Request.QueryString["Fileid1"].ToString());
                Fpath = "filepath1";
                Display(id2,Fpath);
            }
            else if (Request.QueryString["Fileid2"] != null)
            {
                id2 = Decrypt(Request.QueryString["Fileid2"].ToString());
                Fpath = "filepath2";
                Display(id2, Fpath);
            }
            else if (Request.QueryString["Fileid3"] != null)
            {
                id2 = Decrypt(Request.QueryString["Fileid3"].ToString());
                Fpath = "filepath3";
                Display(id2, Fpath);
            }
            else if (Request.QueryString["Fileid4"] != null)
            {
                id2 = Decrypt(Request.QueryString["Fileid4"].ToString());
                Fpath = "filepath4";
                Display(id2, Fpath);
            }
            else if (Request.QueryString["Fileid5"] != null)
            {
                id2 = Decrypt(Request.QueryString["Fileid5"].ToString());
                Fpath = "filepath5";
                Display(id2, Fpath);
            }
            else
            {
                lblnotfound.Text = "File Not Found or Not Available !!";
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public void Display(string id2, string Fpath)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string CmdText = "SELECT [Id],'../'+["+ Fpath + "] as Path FROM [EnquiryData] where id='" + id2 + "'";

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