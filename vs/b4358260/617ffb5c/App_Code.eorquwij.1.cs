#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\App_Code\clsScheduleMail.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4395BE04B77A16CB7FF3A57F914DE3277C0B0FC6"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\App_Code\clsScheduleMail.cs"
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
using System.Net;
using System.Reflection;

/// <summary>
/// Summary description for clsScheduleMail
/// </summary>
public class clsScheduleMail
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    public clsScheduleMail()
    {

    }
    /// <summary>
    ///  Remainder set for Exact Date and Time
    /// </summary>
    public void SendScheduleMail()
    {
        // Get data of TBRO  // Send mail at 8 AM  // Update TBRO tbl data after mail send

        string date1 = DateTime.Now.ToString("HH:mm");

        if (date1 == "08:00" || date1 == "08:01" || date1 == "08:02" || date1 == "08:03" || date1 == "08:04" || date1 == "08:05" || date1 == "08:06")
        {
            GetdataofTBRO();
        }
    }



    protected void GetdataofTBRO()
    {
        string todaydate = DateTime.Now.ToString("yyyy-MM-dd");
        //string td= todaydate.ToString("yyyy-MM-dd");
        string q = @"SELECT r.[id],r.[cname],r.[title],r.[remark],e.name,e.email FROM [RemainderData] r
        join employees e on r.[sessionname]=e.empcode where [dateofreminder]='" + todaydate + "' and r.[mailsentstatus]=0 order by r.id desc ";
        SqlDataAdapter ad = new SqlDataAdapter(q, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                mailsendforTBRO(dt.Rows[i]["cname"].ToString(), dt.Rows[i]["email"].ToString(), dt.Rows[i]["title"].ToString(), dt.Rows[i]["remark"].ToString());
                UpdateDataforTBRO(dt.Rows[i]["id"].ToString());
            }
        }
    }

    protected void mailsendforTBRO(string cname, string email, string title, string remark)
    {
        //string readFile = @"<!DOCTYPE html> <html> <head> <title></title> </head> <body> <div width='100 % ' style='min - width:100 % !important; margin: 0!important; padding: 0!important'> <table align='center' width='100 % ' border='0' cellpadding='0' cellspacing='0' style='display: block; min - width:100 % !important' bgcolor='#ffffff'> <tbody> <tr> <td>&nbsp;</td> <td width='100%' align='center' style='text-align:center;padding:10px 0px'> <a href='http://www.weblinkservices.net' target='_blank'><img src='https://www.weblinkservices.net/images/logo.png' width='50%'></a> </td> <td>&nbsp;</td> </tr> </tbody> </table> <div style='overflow:hidden;display:none;font-size:1px;color:#ffffff;line-height:1px;max-height:0px;max-width:0px;opacity:0'>&nbsp;</div> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='display:block;min-width:100%;background-color:#edece6'> <tbody> <tr style='background-color:#1a263a'> <td>&nbsp;</td> <td width='660' style='padding:20px 0px 20px 0px;text-align:center'><a href='#' style='text-decoration:none;font-size:20px;color:#ffffff'>TBRO / Remainder</a></td> <td>&nbsp;</td> </tr> <tr> <td>&nbsp;</td> <td valign='middle' align='left' width='100%' style='padding:0px 21px 0px 21px'>&nbsp;</td> <td>&nbsp;</td> </tr> <tr> <td>&nbsp;</td> <td width='100%'> <table cellpadding='0' cellspacing='0' border='0' align='center' style='border-bottom:2px solid #b8b8b8; width:90%' bgcolor='#ffffff'> <tbody> <tr> <td width='100%' align='left' valign='top' style='padding:10px;'> <table border='0' cellspacing='0' cellpadding='0' width='100%'> <tbody> <tr height='40'> <td style='text-align:center;padding:0px 1px 1px 15px;font-size:14px;color:#333333;line-height:1.4!important;word-wrap:break-word' valign='top'> <p class='pdata' align='center' style='font-size: 16px; text-align: left;'> Company Name :&nbsp; $Companyname$<br /> TBRO Title :&nbsp; $TBROtitle$<br /> Remark :&nbsp; $Comment$ </p> <br /><br /> <hr style='border-top: 1px dashed #333333;' /> <p class='pdata' align='center' style='font-size: 16px; text-align: left;'> NOTE :-  This is system generated mail.<br /> If you find this is not a genuine update then please report to <br /><a href='mailto:info@weblinkservices.net'>info@weblinkservices.net</a> immediately. </p> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <br /> </td> <td>&nbsp;</td> </tr> </tbody> </table> <br /> <table width='100%'> <tbody> <tr> <td width='100%' bgcolor='#1A263A' style='padding:0px 30px!important;background-color:#1a263a'> <table width='100%' border='0' cellpadding='0' cellspacing='0' style='color:#ffffff;text-align:center;font-size:14px'> <tbody> <tr><td height='24px'>&nbsp;</td></tr> <tr> <td style='padding:0px 10px 0px 10px;font-size:14px'> <a href='https://www.weblinkservices.net/about-us.html' style='text-decoration:underline;color:#ffffff' target='_blank'>About Us</a> | <a href='https://www.weblinkservices.net/services.html' style='text-decoration:underline;color:#ffffff' target='_blank'>Services</a>&nbsp;| <a href='https://www.weblinkservices.net/contact-us.html' style='text-decoration:underline;color:#ffffff' target='_blank'>Contact Us</a> | <a href='https://www.weblinkservices.net/career.php' style='text-decoration:underline;color:#ffffff' target='_blank'><span style='white-space:nowrap'>Career</span></a> </td> </tr> <tr> <td style='padding:5px 10px 24px 10px;text-decoration:none!important;color:#ffffff!important;font-size:14px'> <span>Surya Plaza, Office No. 06, Thergaon, Pune, Maharashtra - 411033</span> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </div> </body> </html>";
        //readFile = readFile.Replace("$Companyname$", cname);
        //readFile = readFile.Replace("$TBROtitle$", title);
        //readFile = readFile.Replace("$Comment$", remark);
        ////MailMessage o = new MailMessage(email.ToLower(), email.ToLower(), title + "- TBRO / Remainder", readFile);
        //MailMessage o = new MailMessage("95pushpendra@gmail.com", "95pushpendra@gmail.com", title + "- TBRO / Remainder", readFile);
        //NetworkCredential netCred = new NetworkCredential("95pushpendra@gmail.com", "Pkk#2303&Pkk");
        //o.Bcc.Add("pushpendrakushwaha07@gmail.com");//Admin Mail
        //SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
        //o.IsBodyHtml = true;
        //smtpobj.EnableSsl = true;
        //smtpobj.UseDefaultCredentials = false;
        //smtpobj.Credentials = netCred;
        //smtpobj.Send(o);

        //string fromMailID = "95pushpendra@gmail.com";
        string fromMailID = "pushpendra@weblinkservices.net";
        string mailTo = "95pushpendra@gmail.com";
        MailMessage mm = new MailMessage();
        mm.From = new MailAddress(fromMailID);

        mm.Subject = title + "- TBRO / Remainder";
        mm.To.Add(mailTo);

        //mm.To.Add(Session["salesemail"].ToString().Trim().ToLower());
        //mm.Bcc.Add(ConfigurationManager.AppSettings["AdminMailBcc"]);
        string readFile = @"<!DOCTYPE html> <html> <head> <title></title> </head> <body> <div width='100 % ' style='min - width:100 % !important; margin: 0!important; padding: 0!important'> <table align='center' width='100 % ' border='0' cellpadding='0' cellspacing='0' style='display: block; min - width:100 % !important' bgcolor='#ffffff'> <tbody> <tr> <td>&nbsp;</td> <td width='100%' align='center' style='text-align:center;padding:10px 0px'> <a href='http://www.weblinkservices.net' target='_blank'><img src='https://www.weblinkservices.net/images/logo.png' width='50%'></a> </td> <td>&nbsp;</td> </tr> </tbody> </table> <div style='overflow:hidden;display:none;font-size:1px;color:#ffffff;line-height:1px;max-height:0px;max-width:0px;opacity:0'>&nbsp;</div> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='display:block;min-width:100%;background-color:#edece6'> <tbody> <tr style='background-color:#1a263a'> <td>&nbsp;</td> <td width='660' style='padding:20px 0px 20px 0px;text-align:center'><a href='#' style='text-decoration:none;font-size:20px;color:#ffffff'>TBRO / Remainder</a></td> <td>&nbsp;</td> </tr> <tr> <td>&nbsp;</td> <td valign='middle' align='left' width='100%' style='padding:0px 21px 0px 21px'>&nbsp;</td> <td>&nbsp;</td> </tr> <tr> <td>&nbsp;</td> <td width='100%'> <table cellpadding='0' cellspacing='0' border='0' align='center' style='border-bottom:2px solid #b8b8b8; width:90%' bgcolor='#ffffff'> <tbody> <tr> <td width='100%' align='left' valign='top' style='padding:10px;'> <table border='0' cellspacing='0' cellpadding='0' width='100%'> <tbody> <tr height='40'> <td style='text-align:center;padding:0px 1px 1px 15px;font-size:14px;color:#333333;line-height:1.4!important;word-wrap:break-word' valign='top'> <p class='pdata' align='center' style='font-size: 16px; text-align: left;'> Company Name :&nbsp; $Companyname$<br /> TBRO Title :&nbsp; $TBROtitle$<br /> Remark :&nbsp; $Comment$ </p> <br /><br /> <hr style='border-top: 1px dashed #333333;' /> <p class='pdata' align='center' style='font-size: 16px; text-align: left;'> NOTE :-  This is system generated mail.<br /> If you find this is not a genuine update then please report to <br /><a href='mailto:info@weblinkservices.net'>info@weblinkservices.net</a> immediately. </p> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <br /> </td> <td>&nbsp;</td> </tr> </tbody> </table> <br /> <table width='100%'> <tbody> <tr> <td width='100%' bgcolor='#1A263A' style='padding:0px 30px!important;background-color:#1a263a'> <table width='100%' border='0' cellpadding='0' cellspacing='0' style='color:#ffffff;text-align:center;font-size:14px'> <tbody> <tr><td height='24px'>&nbsp;</td></tr> <tr> <td style='padding:0px 10px 0px 10px;font-size:14px'> <a href='https://www.weblinkservices.net/about-us.html' style='text-decoration:underline;color:#ffffff' target='_blank'>About Us</a> | <a href='https://www.weblinkservices.net/services.html' style='text-decoration:underline;color:#ffffff' target='_blank'>Services</a>&nbsp;| <a href='https://www.weblinkservices.net/contact-us.html' style='text-decoration:underline;color:#ffffff' target='_blank'>Contact Us</a> | <a href='https://www.weblinkservices.net/career.php' style='text-decoration:underline;color:#ffffff' target='_blank'><span style='white-space:nowrap'>Career</span></a> </td> </tr> <tr> <td style='padding:5px 10px 24px 10px;text-decoration:none!important;color:#ffffff!important;font-size:14px'> <span>Surya Plaza, Office No. 06, Thergaon, Pune, Maharashtra - 411033</span> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </div> </body> </html>";
        readFile = readFile.Replace("$Companyname$", cname);
        readFile = readFile.Replace("$TBROtitle$", title);
        readFile = readFile.Replace("$Comment$", remark);

        mm.Body = readFile.ToString();

        mm.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "ssmtp.net4india.com";
        //smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = false;
        //smtp.EnableSsl = true;
        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
        NetworkCred.UserName = "enquiry@factsheetinc.com";
        NetworkCred.Password = "factsheetinc@123";
        //NetworkCred.UserName = "95pushpendra@gmail.com";
        //NetworkCred.Password = "Pkk#2303&Pkk";

        smtp.UseDefaultCredentials = false;
        smtp.Credentials = NetworkCred;
        smtp.Port = 587;
        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };
        smtp.Send(mm);
    }


    protected void UpdateDataforTBRO(string rowid)
    {
        using (SqlCommand cmm = new SqlCommand())
        {
            int value = 1;
            cmm.Connection = con;
            cmm.CommandType = CommandType.Text;
            cmm.CommandText = "Update [RemainderData] set [mailsentstatus]='" + value + "'  where id='" + rowid + "'";
            cmm.Connection.Open();
            int a = 0;
            a = cmm.ExecuteNonQuery();
            cmm.Connection.Close();
            if (a > 0)
            {

            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Not Updated !!');", true);
            }
        }
    }

    }


#line default
#line hidden
