using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CustomerDetails : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CustomerDetails", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCustomerDetails.DataSource = dt;
            gvCustomerDetails.DataBind();
        }
    }



    protected void GvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "RowEdit")
        {
            btnadd.Text = "Update";
            ViewState["CustomerID"] = e.CommandArgument.ToString();
            gvCustomerDetails_SelectedIndexChanged(e.CommandArgument.ToString());
        }
        
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnadd.Text == "Update")
        {
            // Retrieve the CustomerID from ViewState
            int customerId = 0;
            if (ViewState["CustomerID"] != null)
            {
                customerId = Convert.ToInt32(ViewState["CustomerID"]);
            }
            else
            {
                lblErrorMessage.Text = "Customer ID is not available.";
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE CustomerDetails SET CustomerName = @CustomerName, BasicAmount = @BasicAmount, CGST = @CGST, SGST = @SGST, IGST = @IGST, GrandTotal = @GrandTotal WHERE CustomerID = @CustomerID", con);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);

                decimal basicAmount;
                if (!decimal.TryParse(txtBasicAmount.Text, out basicAmount))
                {
                    lblErrorMessage.Text = "Invalid Basic Amount. Please enter a valid number.";
                    return;
                }

                cmd.Parameters.AddWithValue("@BasicAmount", txtBasicAmount.Text);
                cmd.Parameters.AddWithValue("@CGST", txtCGST.Text);
                cmd.Parameters.AddWithValue("@SGST", txtSGST.Text);
                cmd.Parameters.AddWithValue("@IGST", txtIGST.Text);
                cmd.Parameters.AddWithValue("@GrandTotal", txtGrandTotal.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                lblMessage.Text = "Record Updated successfully!";
                lblMessage.Visible = true;
                btnadd.Text = "Add";
               
            }
        }
        else
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO CustomerDetails (CustomerName, HsnDate, BasicAmount, CGST, SGST, IGST, GrandTotal) VALUES (@CustomerName, GETDATE(), @BasicAmount, @CGST, @SGST, @IGST, @GrandTotal)", con);
                cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@BasicAmount", txtBasicAmount.Text);
                cmd.Parameters.AddWithValue("@CGST", txtCGST.Text);
                cmd.Parameters.AddWithValue("@SGST", txtSGST.Text);
                cmd.Parameters.AddWithValue("@IGST", txtIGST.Text);
                cmd.Parameters.AddWithValue("@GrandTotal", txtGrandTotal.Text);
                con.Open();
                cmd.ExecuteNonQuery();
            }

        }

        lblMessage.Text = "Record added successfully!";
        lblMessage.Visible = true;
        lblErrorMessage.Text = "";
        BindGrid();
        ClearFields();

    }


    protected void gvCustomerDetails_SelectedIndexChanged(string id)
    {

        string query1 = string.Empty;
        query1 = "SELECT *  from [CustomerDetails] where CustomerID='" + id + "'";
        SqlDataAdapter ad = new SqlDataAdapter(query1, connectionString);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtCustomerName.Text = dt.Rows[0]["CustomerName"].ToString();
            txtBasicAmount.Text = dt.Rows[0]["BasicAmount"].ToString();
            if (dt.Rows[0]["CGST"].ToString() == "&nbsp;" || dt.Rows[0]["CGST"].ToString() == "")
            {
                txtCGST.Text = "0";
                txtCGST.ReadOnly = true;
            }
            else
            {
                txtCGST.Text = dt.Rows[0]["CGST"].ToString();
            }
            if (dt.Rows[0]["SGST"].ToString() == "&nbsp;" || dt.Rows[0]["SGST"].ToString() == "")
            {
                txtSGST.Text = "0";
                txtSGST.ReadOnly = true;
            }
            else
            {
                txtSGST.Text = dt.Rows[0]["SGST"].ToString();
            }
            if (dt.Rows[0]["IGST"].ToString() == "&nbsp;" || dt.Rows[0]["IGST"].ToString() == "")
            {
                txtIGST.Text = "0";
                txtIGST.ReadOnly = true;
            }
            else
            {
                txtIGST.Text = dt.Rows[0]["IGST"].ToString();
            }

            txtGrandTotal.Text = dt.Rows[0]["GrandTotal"].ToString();

        }

    }


    protected void GvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userid = Convert.ToInt32(gvCustomerDetails.DataKeys[e.RowIndex].Value.ToString());
        if (userid == -1)
        {
            lblErrorMessage.Text = "Please select a record to delete.";
            return;
        }

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM CustomerDetails WHERE CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", userid);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        lblMessage.Text = "Record deleted successfully!";
        lblMessage.Visible = true;
        lblErrorMessage.Text = string.Empty;
        BindGrid();
        ClearFields();
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void ClearFields()
    {

        txtIGST.ReadOnly = false;
        txtSGST.ReadOnly = false;
        txtCGST.ReadOnly = false;
        txtCustomerName.Text = string.Empty;
        txtBasicAmount.Text = string.Empty;
        txtCGST.Text = string.Empty;
        txtSGST.Text = string.Empty;
        txtIGST.Text = string.Empty;
        txtGrandTotal.Text = string.Empty;
    }

    protected void txtCGST_TextChanged(object sender, EventArgs e)
    {
        if (txtCGST.Text != "0")
        {
            txtIGST.ReadOnly = true;
            var total = Convert.ToDecimal(txtBasicAmount.Text);

            decimal cgst_amt;
            if (string.IsNullOrEmpty(txtCGST.Text))
            {
                cgst_amt = 0;
            }
            else
            {
                cgst_amt = Convert.ToDecimal(total.ToString()) * Convert.ToDecimal(txtCGST.Text) / 100;
            }

            decimal tot = total + cgst_amt;
            txtGrandTotal.Text = tot.ToString();

        }

    }

    protected void txtSGST_TextChanged(object sender, EventArgs e)
    {
        if (txtSGST.Text != "0")
        {
            txtIGST.ReadOnly = true;
            var total = Convert.ToDecimal(txtBasicAmount.Text);
            decimal sgst_amt;
            if (string.IsNullOrEmpty(txtSGST.Text))
            {
                sgst_amt = 0;
            }
            else
            {
                sgst_amt = Convert.ToDecimal(total.ToString()) * Convert.ToDecimal(txtSGST.Text) / 100;
            }

            decimal tot = Convert.ToDecimal(txtGrandTotal.Text) + sgst_amt;

            txtGrandTotal.Text = tot.ToString();

        }
    }

    protected void txtIGST_TextChanged(object sender, EventArgs e)
    {
        if (txtIGST.Text != "0")
        {
            txtSGST.ReadOnly = true;
            txtCGST.ReadOnly = true;
            var total = Convert.ToDecimal(txtBasicAmount.Text);
            decimal igst_amt;
            if (string.IsNullOrEmpty(txtIGST.Text))
            {
                igst_amt = 0;
            }
            else
            {
                igst_amt = Convert.ToDecimal(total.ToString()) * Convert.ToDecimal(txtIGST.Text) / 100;
            }

            decimal tot = total + igst_amt;
            txtGrandTotal.Text = tot.ToString();

        }
    }

    protected void txtBasicAmount_TextChanged(object sender, EventArgs e)
    {
        var total = Convert.ToDecimal(txtBasicAmount.Text);
        txtGrandTotal.Text = total.ToString();
    }
}
