<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ReportAccount.aspx.cs" Inherits="Admin_ReportAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .completionList {
            border: solid 1px Gray;
            border-radius: 5px;
            margin: 0px;
            padding: 3px;
            height: 120px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
        }
    </style>
    <style type="text/css">
        .cal_Theme1 .ajax__calendar_container {
            background-color: #DEF1F4;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #ffffff;
            margin-bottom: 4px;
        }

        .cal_Theme1 .ajax__calendar_title,
        .cal_Theme1 .ajax__calendar_next,
        .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .cal_Theme1 .ajax__calendar_body {
            background-color: #ffffff;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: #004080;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #004080;
            text-align: center;
        }

        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
        .cal_Theme1 .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #DEF1F4;
        }

        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
            font-size: 10px;
        }

        .cal_Theme1 .ajax__calendar_other,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
        }

        .ajax__calendar_body {
            height: 158px !important;
            width: 220px !important;
            position: relative;
            overflow: hidden;
            margin: 0 0 0 -5px !important;
        }

        .ajax__calendar_container {
            padding: 4px;
            cursor: default;
            width: 220px !important;
            font-size: 11px;
            text-align: center;
            font-family: tahoma,verdana,helvetica;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #004080;
            font-size: 14px;
            text-align: center;
        }

        .ajax__calendar_day {
            height: 22px !important;
            width: 27px !important;
            text-align: right;
            padding: 0 14px !important;
            cursor: pointer;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            margin-left: 12px !important;
            color: #004080;
        }

        .ajax__calendar_year {
            height: 50px !important;
            width: 51px !important;
            font-weight: bold;
            text-align: center;
            cursor: pointer;
            overflow: hidden;
            color: #004080;
        }

        .ajax__calendar_month {
            height: 50px !important;
            width: 51px !important;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            overflow: hidden;
            color: #004080;
        }

        .grid tr:hover {
            background-color: #d4f0fa;
        }

        .pcoded[theme-layout="vertical"][vertical-nav-type="expanded"] .pcoded-header .pcoded-left-header, .pcoded[theme-layout="vertical"][vertical-nav-type="expanded"] .pcoded-navbar {
            width: 210px;
        }

        <style type="text/css" >
        .divgrid {
            height: 200px;
            width: 370px;
        }

        .divgrid table {
            width: 350px;
        }

            .divgrid table th {
                background-color: Green;
                color: #fff;
            }
    </style>
    <style type="text/css">
        .cal_Theme1 .ajax__calendar_container {
            background-color: #DEF1F4;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #ffffff;
            margin-bottom: 4px;
        }

        .cal_Theme1 .ajax__calendar_title,
        .cal_Theme1 .ajax__calendar_next,
        .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .cal_Theme1 .ajax__calendar_body {
            background-color: #ffffff;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: #004080;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #004080;
            text-align: center;
        }

        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
        .cal_Theme1 .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #DEF1F4;
        }

        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
            font-size: 10px;
        }

        .cal_Theme1 .ajax__calendar_other,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
        }

        .ajax__calendar_body {
            height: 158px !important;
            width: 220px !important;
            position: relative;
            overflow: hidden;
            margin: 0 0 0 -5px !important;
        }

        .ajax__calendar_container {
            padding: 4px;
            cursor: default;
            width: 220px !important;
            font-size: 11px;
            text-align: center;
            font-family: tahoma,verdana,helvetica;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #004080;
            font-size: 14px;
            text-align: center;
        }

        .ajax__calendar_day {
            height: 22px !important;
            width: 27px !important;
            text-align: right;
            padding: 0 14px !important;
            cursor: pointer;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            margin-left: 12px !important;
            color: #004080;
        }

        .ajax__calendar_year {
            height: 50px !important;
            width: 51px !important;
            font-weight: bold;
            text-align: center;
            cursor: pointer;
            overflow: hidden;
            color: #004080;
        }

        .ajax__calendar_month {
            height: 50px !important;
            width: 51px !important;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            overflow: hidden;
            color: #004080;
        }

        .grid tr:hover {
            background-color: #d4f0fa;
        }

        .pcoded[theme-layout="vertical"][vertical-nav-type="expanded"] .pcoded-header .pcoded-left-header, .pcoded[theme-layout="vertical"][vertical-nav-type="expanded"] .pcoded-navbar {
            width: 210px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>--%>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="container py-3">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <div class="row">
                                    <div class="col-md-4">
                                        <h5>Report</h5>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xl-12 col-md-12">
                                    <div class="card">
                                        <div class="card-header">
                                            <div class="row" style="margin-left: -12px;">
                                                <div class="col-xl-1 col-md-1 spancls">Type<i class="reqcls">&nbsp;</i> :</div>
                                                <div class="col-xl-2 col-md-2 spancls" style="margin-left: 177px;">Party Name<i class="reqcls">&nbsp;</i> :</div>
                                                <div class="col-xl-2 col-md-2 spancls" style="margin-left: 87px;">From Date<i class="reqcls">&nbsp;</i> :</div>
                                                <div class="col-xl-2 col-md-2 spancls" style="margin-left: 92px;">To Date<i class="reqcls">&nbsp;</i> :</div>
                                            </div>
                                            <div class="row">


                                                <div class="col-xl-3 col-md-3">
                                                    <asp:TextBox ID="ddltype" runat="server" CssClass="form-control" Width="100%" Text="Sales" ReadOnly="true"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" OnTextChanged="ddltype_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Value="" Text="--Select Type--"></asp:ListItem>
                                                <asp:ListItem Text="Sales"></asp:ListItem>
                                                <asp:ListItem Text="Purchase"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                                </div>

                                                <div class="col-xl-3 col-md-3">

                                                    <asp:TextBox ID="txtPartyName" runat="server" CssClass="form-control" placeholder="Party Name" Width="100%" AutoPostBack="true"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                        TargetControlID="txtPartyName">
                                                    </asp:AutoCompleteExtender>
                                                    <%--   <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetSupplierList"
                                                TargetControlID="txtPartyName">
                                            </asp:AutoCompleteExtender>--%>
                                                </div>
                                                <div class="col-xl-3 col-md-3">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" placeholder="From Date" Width="100%" AutoPostBack="true"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                                </div>
                                                <div class="col-xl-3 col-md-3">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" placeholder="To Date" Width="100%" AutoPostBack="true"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                                </div>

                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-xl-3 col-md-3">
                                                    <asp:Button ID="btnsearch" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Search" OnClick="btnsearch_Click" />
                                                    <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" OnClick="btnresetfilter_Click" />

                                                </div>




                                            </div>
                                            <br />
                                            <div class="row" id="btn" runat="server">
                                                <div class="col-xl-4 col-md-4">
                                                    <asp:Button ID="btnexcel" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Export Excel" OnClick="btnexcel_Click" />
                                                    <asp:Button ID="btnpdf" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Export PDF" OnClick="btnpdf_Click" />
                                                </div>
                                                <div class="col-xl-4 col-md-4"></div>
                                                <div class="col-xl-4 col-md-4">
                                                    <asp:Button ID="btndatewise" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Daily" OnClick="btndatewise_Click" />
                                                    <asp:Button ID="btnmonthwise" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Monthly" OnClick="btnmonthwise_Click" />
                                                    <asp:Button ID="btnyearwise" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Yearly" OnClick="btnyearwise_Click" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-12" style="padding: 20px; margin-top: 10px;">
                                            <div id="DivRoot" align="left" runat="server">
                                                <div style="overflow: hidden;" id="DivHeaderRow">
                                                </div>
                                                <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                    <asp:GridView ID="GvMonthReport" runat="server"
                                                        CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                        AllowPaging="false" ShowHeader="true" PageSize="50" OnRowCommand="GvMonthReport_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice No." ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblinvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcustomername" runat="server" Text='<%# Eval("BillingCustomer") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoicedate" runat="server" Text='<%# Eval("Invoicedate","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice Against">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoiceAgainst" runat="server" Text='<%# Eval("InvoiceAgainst") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer PO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCustomerPO" runat="server" Text='<%# Eval("CustomerPONo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Against No." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAgainstNumber" runat="server" Text='<%# Eval("AgainstNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Grand Total" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrandTotal" runat="server" Text='<%# Eval("GrandTotalFinal") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Created On" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcreatedOn" runat="server" Text='<%# Eval("CreatedOn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkdownload" runat="server" CommandName="DownloadPDF" CommandArgument='<%# Eval("Id") %>' ToolTip="Download"><i class="fa fa-file-pdf-o" style="font-size:24px;color:red;"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                    <div id="DivFooterRow" style="overflow: hidden">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="padding: 0px; margin-top: 10px;">
                                            <div id="DivRoot1" align="left" runat="server" style="display: none;">
                                                <div style="overflow: hidden;" id="DivHeaderRow1">
                                                </div>
                                                <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent1">
                                                    <asp:GridView ID="GvPurchase" runat="server"
                                                        CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                        DataKeyNames="id" AllowPaging="false" ShowHeader="true" PageSize="50" OnRowCommand="GvPurchase_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bill No" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSupplierBillNo" runat="server" Text='<%# Eval("SupplierBillNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supplier Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSuppliername" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bill Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBillDate" runat="server" Text='<%# Eval("BillDate").ToString().TrimEnd("0:0".ToCharArray()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amt" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrandTotal" runat="server" Text='<%# Eval("GrandTotal") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Due Date" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentDueDate" runat="server" Text='<%# Eval("PaymentDueDate").ToString().TrimEnd("0:0".ToCharArray()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Created By" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("CreatedBy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandName="DownloadPDF" CommandArgument='<%# Eval("Id") %>' ToolTip="Download"><i class="fa fa-file-pdf-o" style="font-size:24px;color:red;"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <div id="DivFooterRow1" style="overflow: hidden">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsearch" />
            <asp:PostBackTrigger ControlID="btnresetfilter" />
            <asp:PostBackTrigger ControlID="btnexcel" />
            <asp:PostBackTrigger ControlID="btnpdf" />
            <asp:PostBackTrigger ControlID="btndatewise" />
            <asp:PostBackTrigger ControlID="btnmonthwise" />
            <asp:PostBackTrigger ControlID="btnyearwise" />
           <%-- <asp:AsyncPostBackTrigger ControlID="lnkdownload" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>

