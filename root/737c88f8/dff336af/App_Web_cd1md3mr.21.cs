#pragma checksum "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\AdminMasterPage.master.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "417EB3BEB06A9CBF28F53F006CA6A33F30709E12"

#line 1 "E:\Ashish\User Autorization Excel Enc 21-11-22\Admin\AdminMasterPage.master.cs"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminMasterPage : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            PageAuthorization();    
            lblusername.Text = Session["name"].ToString();
        }
    }

    protected void PageAuthorization()
    {
        string username = Session["name"].ToString();
        DataTable dt = new DataTable();
        SqlCommand cmd1 = new SqlCommand("SELECT * FROM [tblUserRoleAuthorization] where UserName='" + username + "'", con);
        SqlDataAdapter sad = new SqlDataAdapter(cmd1);
        sad.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            foreach (DataRow row in dt.Rows)
            {
                string MenuName = row["PageName"].ToString();
                if (MenuName == "RoleMaster.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    Roleid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "Addusers.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    UserListid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "AllCompanyList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    CompanyListid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "SupplierList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    SupplierListid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "ItemList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    ItemListid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "CategoryList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    CategoryListid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "SubCategoryList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    SubCategoryListid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "EnquiryList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    Addenquiryid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "QuotationList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    Quotationid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "OrderAcceptanceList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    OAid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "DeliveryChallanList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    DeliveryChallanid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "ProformaInvoiceList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    ProformaInvoiceid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "PurchaseOrderList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    PurchaseOrderid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "PurchaseBillList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    PurchaseBillid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "TaxInvoiceList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    TaxInvoiceid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "ReceiptList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    ReceiptVoucherid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "PaymentList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    PaymentVoucherid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "CreditDebitNoteList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    CreditDebitNoteid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "RegisterReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    AccountReportid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "Drawing.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    Drawingid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "LaserProgramming.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    LaserProgrammingid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "LaserCutting.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    LaserCuttingid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "CNCBending.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    CNCBendingid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "Welding.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    Weldingid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "PowderCoating.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    PowderCoatingid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "FinalAssembly.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    FinalAssemblyid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "Stock.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    Stockid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "DepartmentWiseReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    CustomerReportid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "CommercialReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    CommercialReportid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "DepartmentWiseRpt.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    DailyDepartmentReportid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "CompletedOADepartmentWiseRpt.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    CompletedOAReportid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "TestCertificate.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    TestCertificateReportid.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "ManualOrderAcceptance.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    ManuallyOACreation.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "AuditLogDashboard.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    AuditLog.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "UserAuthorization.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    UserAuthorization.Visible = page1 == "True" ? true : false;
                }

                //condition

                //if (UserListid.Visible == false && CompanyListid.Visible == false && Roleid.Visible == false && SupplierListid.Visible == false && ItemListid.Visible == false && CategoryListid.Visible == false && SubCategoryListid.Visible == false)
                //{
                //    Mastersid.Visible = false;
                //}
                //if (Addenquiryid.Visible == false && Quotationid.Visible == false && OAid.Visible == false && DeliveryChallanid.Visible == false && ProformaInvoiceid.Visible == false )
                //{
                //    Marketingid.Visible = false;
                //}
                //if (PurchaseOrderid.Visible == false && PurchaseBillid.Visible == false)
                //{
                //    Purchaseid.Visible = false;
                //}
                //if (TaxInvoiceid.Visible == false && ReceiptVoucherid.Visible == false && PaymentVoucherid.Visible == false && CreditDebitNoteid.Visible == false && AccountReportid.Visible == false)
                //{
                //    Accountid.Visible = false;
                //}
                //if (Drawingid.Visible == false && LaserProgrammingid.Visible == false && LaserCuttingid.Visible == false && CNCBendingid.Visible == false && Weldingid.Visible == false && PowderCoatingid.Visible == false && Assemblyid.Visible == false && FinalAssemblyid.Visible == false && Stockid.Visible==false)
                //{
                //    Productionid.Visible = false;
                //}
                //if (CustomerReportid.Visible == false && CommercialReportid.Visible == false && DailyDepartmentReportid.Visible == false && CompletedOAReportid.Visible == false && TestCertificateReportid.Visible == false )
                //{
                //    Reportid.Visible = false;
                //}


            }
        }
        else
        {

            //entry.Visible = false;
            //entryhr.Visible = false;
            //master.Visible = false;
            //masterhr.Visible = false;
            //saleshr.Visible = false;
            //sales.Visible = false;
            //evalhr.Visible = false;
            //evalutionlist.Visible = false;
            //popup.Visible = true;
            //reporthr.Visible = false;
            //report.Visible = false;
            
            Response.Redirect("~/Login.aspx");
        }
    }

}


#line default
#line hidden
