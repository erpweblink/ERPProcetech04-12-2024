using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddCustomerOrder : System.Web.UI.Page
{
    static string oldfile1, oldfile2, date;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                fillddlpaymentterm();
            }
        }       
    }

    private void fillddlpaymentterm()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select distinct paymentterm,transportation from QuotationMainFooter", con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);

        if (dtpt.Rows.Count > 0)
        {
            ////////1
            List<string> lstpaytm = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["paymentterm"].ToString() != "")
                {
                    lstpaytm.Add(dtpt.Rows[i]["paymentterm"].ToString());
                }
            }
            ddlpaymentterms.DataSource = lstpaytm;
            ddlpaymentterms.DataBind();

            ////2
            List<string> lsttrans = new List<string>();
            for (int i = 0; i < dtpt.Rows.Count; i++)
            {
                if (dtpt.Rows[i]["transportation"].ToString() != "")
                {
                    lsttrans.Add(dtpt.Rows[i]["transportation"].ToString());
                }
            }
            ddltransportation.DataSource = lsttrans;
            ddltransportation.DataBind();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string filename1 = "", filename2 = "";
        if (uploadrefdoc.HasFile)
        {
            string filePath = uploadrefdoc.PostedFile.FileName;
            filename1 = Path.GetFileName(filePath);
            string[] avc = filename1.Split('.');
            string ext = Path.GetExtension(filename1);
            string contenttype = String.Empty;
            string timest = DateTime.Now.ToString("ddMMyyyyhhmmssfff");
            filename2 = avc[0] + timest + ext;
            uploadrefdoc.SaveAs(Server.MapPath("~/RefDocument/") + filename2);
        }

        if (uploadrefdoc.HasFile == false && oldfile1 != "" && oldfile2 != "")
        {
            filename1 = oldfile1; filename2 = oldfile2;
        }

        SqlCommand cmd = new SqlCommand("SP_customerorder", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@billingcustomer", txtbillingcustomer.Text);
        cmd.Parameters.AddWithValue("@shippingcustomer", txtshippingcustomer.Text);
        cmd.Parameters.AddWithValue("@shippingaddress", txtshippingaddress.Text);
        cmd.Parameters.AddWithValue("@pendingpayment", lblPendingPayment.Text);
        cmd.Parameters.AddWithValue("@date", Request.Form[txtdate.UniqueID].ToString());
        cmd.Parameters.AddWithValue("@time", txttime.Text);
        cmd.Parameters.AddWithValue("@Mode", ddlmode.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@customerpono", txtcustomerpono.Text);
        cmd.Parameters.AddWithValue("@podate", Request.Form[txtpodate.UniqueID].ToString());
        cmd.Parameters.AddWithValue("@deliverydate", Request.Form[txtdeliverydate.UniqueID].ToString());
        cmd.Parameters.AddWithValue("@refdocumentname", filename1);
        cmd.Parameters.AddWithValue("@refdocumentpath", "../RefDocument/" + filename2);
        //cmd.Parameters.AddWithValue("@referquotation", ddlreferquotation.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@remark", txtremark.Text);
        //cmd.Parameters.AddWithValue("@orderclosemode", ddlorderclosemode.SelectedItem.Text);
        //cmd.Parameters.AddWithValue("@kindatt", ddlkindlyattention.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@paymentterms", ddlpaymentterms.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@transportation", ddltransportation.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@email", lblemail.Text);
        cmd.Parameters.AddWithValue("@action", "insert");

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCustomerorder.aspx");
    }
}
