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
        SqlCommand cmd1 = new SqlCommand("SELECT * FROM [DB_ProcetechTesting].[DB_ProcetechERP].[tblUserRoleAuthorization] where UserName='" + username + "'", con);
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
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        CompanyListid.Visible = false;
                    }
                    else
                    {
                        CompanyListid.Visible = true;
                    }
                }
                if (MenuName == "SupplierList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        SupplierListid.Visible = false;
                    }
                    else
                    {
                        SupplierListid.Visible = true;
                    }
                   
                }
                if (MenuName == "ItemList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        ItemListid.Visible = false;
                    }
                    else
                    {
                        ItemListid.Visible = true;
                    }
                }
                if (MenuName == "CategoryList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        CategoryListid.Visible = false;
                    }
                    else
                    {
                        CategoryListid.Visible = true;
                    }
                }
                if (MenuName == "SubCategoryList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        SubCategoryListid.Visible = false;
                    }
                    else
                    {
                        SubCategoryListid.Visible = true;
                    }
                }
                if (MenuName == "UnitList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        UnitListid.Visible = false;
                    }
                    else
                    {
                        UnitListid.Visible = true;
                    }
                }
                if (MenuName == "EnquiryList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Addenquiryid.Visible = false;
                    }
                    else
                    {
                        Addenquiryid.Visible = true;
                    }
                }
                if (MenuName == "QuotationList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Quotationid.Visible = false;
                    }
                    else
                    {
                        Quotationid.Visible = true;
                    }
                }
                if (MenuName == "OrderAcceptanceList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        OAid.Visible = false;
                    }
                    else
                    {
                        OAid.Visible = true;
                    }
                }
                //if (MenuName == "DeliveryChallanList.aspx")
                //{
                //    string page1 = row["Pages"].ToString();
                //    string pageView = row["PagesView"].ToString();
                //    if (page1 == "False" && pageView == "False")
                //    {
                //        DeliveryChallanid.Visible = false;
                //    }
                //    else
                //    {
                //        DeliveryChallanid.Visible = true;
                //    }
                //}
                if (MenuName == "ProformaInvoiceList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        ProformaInvoiceid.Visible = false;
                    }
                    else
                    {
                        ProformaInvoiceid.Visible = true;
                    }
                }
                if (MenuName == "PurchaseOrderList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PurchaseOrderid.Visible = false;
                    }
                    else
                    {
                        PurchaseOrderid.Visible = true;
                    }
                }
                if (MenuName == "PurchaseBillList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PurchaseBillid.Visible = false;
                    }
                    else
                    {
                        PurchaseBillid.Visible = true;
                    }
                }

                if (MenuName == "CreditDebitNoteList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Li1.Visible = false;
                    }
                    else
                    {
                        Li1.Visible = true;
                    }
                }

                //if (MenuName == "PaymentModule.aspx")
                //{
                //    string page1 = row["Pages"].ToString();
                //    string pageView = row["PagesView"].ToString();
                //    if (page1 == "False" && pageView == "False")
                //    {
                //        PaymentModuleid.Visible = false;
                //    }
                //    else
                //    {
                //        PaymentModuleid.Visible = true;
                //    }
                //}
                //if (MenuName == "PaymentModuleList.aspx")
                //{
                //    string page1 = row["Pages"].ToString();
                //    string pageView = row["PagesView"].ToString();
                //    if (page1 == "False" && pageView == "False")
                //    {
                //        PaymentModuleListid.Visible = false;
                //    }
                //    else
                //    {
                //        PaymentModuleListid.Visible = true;
                //    }
                //}
                //if (MenuName == "PaymentRequestList.aspx")
                //{
                //    string page1 = row["Pages"].ToString();
                //    string pageView = row["PagesView"].ToString();
                //    if (page1 == "False" && pageView == "False")
                //    {
                //        PaymentRequestListid.Visible = false;
                //    }
                //    else
                //    {
                //        PaymentRequestListid.Visible = true;
                //    }
                //}
                if (MenuName == "TaxInvoiceList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        TaxInvoiceid.Visible = false;
                    }
                    else
                    {
                        TaxInvoiceid.Visible = true;
                    }
                }
                if (MenuName == "ReceiptList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        ReceiptVoucherid.Visible = false;
                    }
                    else
                    {
                        ReceiptVoucherid.Visible = true;
                    }
                }
                if (MenuName == "PaymentList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PaymentVoucherid.Visible = false;
                    }
                    else
                    {
                        PaymentVoucherid.Visible = true;
                    }
                }
                if (MenuName == "CreditDebitNoteList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        CreditDebitNoteid.Visible = false;
                    }
                    else
                    {
                        CreditDebitNoteid.Visible = true;
                    }
                }
                if (MenuName == "RegisterReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        AccountReportid.Visible = false;
                    }
                    else
                    {
                        AccountReportid.Visible = true;
                    }
                }
                
                if (MenuName == "PurchaseReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PurchaseReportid.Visible = false;
                    }
                    else
                    {
                        PurchaseReportid.Visible = true;
                    }
                }
                if (MenuName == "OutstandingReportPurchase.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        OutstandingReportPurchaseid.Visible = false;
                    }
                    else
                    {
                        OutstandingReportPurchaseid.Visible = true;
                    }
                }
                if (MenuName == "Drawing.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Drawingid.Visible = false;
                    }
                    else
                    {
                        Drawingid.Visible = true;
                    }
                }
                if (MenuName == "LaserProgramming.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        LaserProgrammingid.Visible = false;
                    }
                    else
                    {
                        LaserProgrammingid.Visible = true;
                    }

                }
                if (MenuName == "LaserCutting.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        LaserCuttingid.Visible = false;
                    }
                    else
                    {
                        LaserCuttingid.Visible = true;
                    }
                }
                if (MenuName == "CNCBending.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        CNCBendingid.Visible = false;
                    }
                    else
                    {
                        CNCBendingid.Visible = true;
                    }
                }
                if (MenuName == "Welding.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Weldingid.Visible = false;
                    }
                    else
                    {
                        Weldingid.Visible = true;
                    }
                }
                if (MenuName == "PowderCoating.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PowderCoatingid.Visible = false;
                    }
                    else
                    {
                        PowderCoatingid.Visible = true;
                    }
                }
                if (MenuName == "FinalAssembly.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        FinalAssemblyid.Visible = false;
                    }
                    else
                    {
                        FinalAssemblyid.Visible = true;
                    }
                }
                if (MenuName == "Stock.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Stockid.Visible = false;
                    }
                    else
                    {
                        Stockid.Visible = true;
                    }
                }
                if (MenuName == "DepartmentWiseReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        CustomerReportid.Visible = false;
                    }
                    else
                    {
                        CustomerReportid.Visible = true;
                    }
                }
                if (MenuName == "CommercialReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        CommercialReportid.Visible = false;
                    }
                    else
                    {
                        CommercialReportid.Visible = true;
                    }
                }
                if (MenuName == "DepartmentWiseRpt.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        DailyDepartmentReportid.Visible = false;
                    }
                    else
                    {
                        DailyDepartmentReportid.Visible = true;
                    }
                }
                //if (MenuName == "CompletedOADepartmentWiseRpt.aspx")
                //{
                //    string page1 = row["Pages"].ToString();
                //    string pageView = row["PagesView"].ToString();
                //    if (page1 == "False" && pageView == "False")
                //    {
                //        CompletedOAReportid.Visible = false;
                //    }
                //    else
                //    {
                //        CompletedOAReportid.Visible = true;
                //    }
                //}
                if (MenuName == "TestCertificate.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        TestCertificateReportid.Visible = false;
                    }
                    else
                    {
                        TestCertificateReportid.Visible = true;
                    }
                }
                if (MenuName == "OutstandingReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        OutstandingReportid.Visible = false;
                    }
                    else
                    {
                        OutstandingReportid.Visible = true;
                    }
                }
                if (MenuName == "PartyLedgerReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PartyLedgerReportid.Visible = false;
                    }
                    else
                    {
                        PartyLedgerReportid.Visible = true;
                    }
                }
                if (MenuName == "RegisterReport.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        AccountReportid.Visible = false;
                    }
                    else
                    {
                        AccountReportid.Visible = true;
                    }
                }
                if (MenuName == "ManualOrderAcceptance.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        ManuallyOACreation.Visible = false;
                    }
                    else
                    {
                        ManuallyOACreation.Visible = true;
                    }
                }
                if (MenuName == "AuditLogDashboard.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                       AuditLog.Visible = false;
                    }
                    else
                    {
                        AuditLog.Visible = true;
                    }
                }
                if (MenuName == "UserAuthorization.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        UserAuthorization.Visible = false;
                    }
                    else
                    {
                        UserAuthorization.Visible = true;
                    }
                }

                if (MenuName == "Termsandmasterlist.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Termsandmasterlistid.Visible = false;
                    }
                    else
                    {
                        Termsandmasterlistid.Visible = true;
                    }
                }

                if (MenuName == "PaymentTermsList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PTermsListid.Visible = false;
                    }
                    else
                    {
                        PTermsListid.Visible = true;
                    }
                }

                if (MenuName == "TaxMasterLis.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        TaxMasteraspxid.Visible = false;
                    }
                    else
                    {
                        TaxMasteraspxid.Visible = true;
                    }
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

            //string role = session["rolename"].tostring();
            //if (role=="admin")
            //{
            //    response.redirect("~/admin/admindashboard.aspx");
            //}
            //else
            //{
            //    response.redirect("~/login.aspx");
            //}
                        //Response.Redirect("~/Login.aspx");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", "alert('MyButton clicked!');", true);

        }
    }

    protected void TodaysCount()
    {

    }

}
