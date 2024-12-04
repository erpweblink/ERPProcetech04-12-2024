<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="RegisterReport.aspx.cs" Inherits="Admin_RegisterReport" %>

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
    <%--   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../img/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
    --%>

    <script src="../JS/jquery.min.js"></script>
    <script language="javascript" type="text/javascript">

        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {

            $('#btnshowhide').hide();

            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                var wid = 100;

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = wid + "%";
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = wid + "%";
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = wid + "%";
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + '%';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';
                DivHR.appendChild(tbl.cloneNode(true));

            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }

        //Purchase
        function MakeStaticHeaderPurchase(gridId, height, width, headerHeight, isFooter) {

            $('#btnshowhide').hide();

            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowPurchase');
                var DivMC = document.getElementById('DivMainContentPurchase');
                var DivFR = document.getElementById('DivFooterRowPurchase');

                var wid = 100;

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = wid + "%";
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = wid + "%";
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = wid + "%";
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + '%';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';
                DivHR.appendChild(tbl.cloneNode(true));

            }
        }

        function OnScrollDivPurchase(Scrollablediv) {
            document.getElementById('DivHeaderRowPurchase').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowPurchase').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>--%>
    <div class="page-wrapper">
        <div class="page-body">
            <div class="container-fluid py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <div class="row">
                            <div class="col-md-4">
                                <h5>Register Report - Sales</h5>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-xl-3 col-md-3">
                                            <div class="spancls">Type<i class="reqcls">&nbsp;</i> :</div>
                                            <asp:TextBox ID="ddltype" Text="SALE" runat="server" CssClass="form-control" Width="100%" Enabled="false" OnTextChanged="ddltype_TextChanged" AutoPostBack="true"></asp:TextBox>

                                            <%--        <asp:DropDownList runat="server" CssClass="form-control" ID="ddltype" Enabled="false" OnTextChanged="ddltype_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                <asp:ListItem>SALE</asp:ListItem>
                                                <asp:ListItem>PURCHASE</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Select Type"
                                                ControlToValidate="ddltype" ValidationGroup="form1" InitialValue="0" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xl-3 col-md-3">
                                            <div class="spancls">Party Name<i class="reqcls">&nbsp;</i> :</div>
                                            <asp:TextBox ID="txtPartyName" runat="server" CssClass="form-control" placeholder="Party Name" Width="100%" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                TargetControlID="txtPartyName">
                                            </asp:AutoCompleteExtender>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetSupplierList"
                                                TargetControlID="txtPartyName">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-xl-3 col-md-3">
                                            <div class="spancls">From Date<i class="reqcls">&nbsp;</i> :</div>
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" placeholder="From Date" Width="100%" AutoComplete="off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                        </div>
                                        <div class="col-xl-3 col-md-3">
                                            <div class="spancls">To Date<i class="reqcls">&nbsp;</i> :</div>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" placeholder="To Date" Width="100%" AutoComplete="off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xl-3 col-md-3">
                                            <asp:Button ID="btnsearch" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Search" OnClick="btnsearch_Click" />
                                        </div>
                                    </div>

                                    <div class="row" id="btn" runat="server" style="margin-top: 10px;">
                                        <div class="col-xl-6 col-md-6">
                                            <asp:Button ID="btnexcel" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Export Excel" OnClick="ExportToExcel" />
                                            <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" OnClick="btnresetfilter_Click" />
                                        </div>
                                        <div class="col-xl-4 col-md-4"></div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="padding: 20px; margin-top: 0px;">
                                    <div id="DivRoot" align="left" runat="server">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="dgvRegisterReport" runat="server"
                                                CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                AllowPaging="false" ShowHeader="true" PageSize="50" DataKeyNames="Id" OnRowDataBound="OnRowDataBound">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="Party" DataField="BillingCustomer" />

                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="Type" DataField="Type" />
                                                    <asp:TemplateField HeaderText="GSTNumber" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTNumber" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="VoucherType" DataField="VoucherType" />
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="Date" DataField="Invoicedate" />
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="RefNo" DataField="RefNo" />

                                                    <asp:TemplateField HeaderText="InvoiceNo" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="RefDate" DataField="RefDate" />
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="TCS" DataField="TCSAmt" />
                                                    <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BasicTotal" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBasicTotal" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGST" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CGST Amt" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGSTAmt" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGST" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SGST Amt" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGSTAmt" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIGST" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IGST Amt" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIGSTAmt" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GSTAmount" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTAmount" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GrandTotal" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrandTotal" runat="server" Text='<%# Math.Round(Convert.ToDouble(Eval("GrandTotal"))).ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("IsPaid") %>'></asp:Label>
                                                            <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DocNo") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                        </div>
                                    </div>
                                <%--    Start --%>
                                <%--    <asp:GridView ID="dgvHSNSummary" ShowFooter="true" runat="server" OnRowDataBound="dgvHSNSummary_RowDataBound"
                                        CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                        AllowPaging="false" ShowHeader="true" PageSize="50">
                                        <Columns>
                                            <asp:TemplateField HeaderText="HSN Code" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("HSN") %>' ID="lblHSN" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Qty") %>' ID="lblQty" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalQty" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("UOM") %>' ID="lblUOM" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BasicTotal" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("BasicTotal") %>' ID="lblBasicTotal" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalBasicTotal" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("CGST") %>' ID="lblCGST" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalCGST" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SGST" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("SGST") %>' ID="lblSGST" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalSGST" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("IGST") %>' ID="lblIGST" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalIGST" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField ItemStyle-Width="100px" HeaderText="Qty" DataField="Qty" />--%>
                                            <%--  <asp:BoundField ItemStyle-Width="100px" HeaderText="BasicTotal" DataField="BasicTotal" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="HSN Code" DataField="HSN" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="UOM" DataField="UOM" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="CGST" DataField="CGST" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="SGST" DataField="SGST" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="IGST" DataField="IGST" />--%>
                                  <%--      </Columns>
                                    </asp:GridView>
                                    <div class="row">
                                        <div class="col-4"></div>
                                        <div class="col-4"></div>
                                        <div class="col-4" style="text-align: end;">
                                            <asp:Label runat="server" ID="lbltotalamount" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>--%>
                        <%--        End--%>

                                <div class="col-md-12" style="padding: 20px; margin-top: 0px;">
                                    <div id="DivRootPurchase" align="left" runat="server">
                                        <div style="overflow: hidden;" id="DivHeaderRowPurchase">
                                        </div>
                                        <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDivPurchase(this)" id="DivMainContentPurchase">
                                            <asp:GridView ID="dgvPurchaseRegisterReport" runat="server"
                                                CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                AllowPaging="false" ShowHeader="true" PageSize="50" DataKeyNames="Id" OnRowDataBound="dgvPurchaseRegisterReport_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="Party" DataField="SupplierName" />
                                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblType" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField ItemStyle-Width="100px" HeaderText="Type" DataField="Type" />--%>
                                                    <asp:TemplateField HeaderText="GSTNumber" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTNumber" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="VoucherType" DataField="VoucherType" />
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="Date" DataField="BillDate" />
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="RefNo" DataField="RefNo" />

                                                    <asp:TemplateField HeaderText="VchNo" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVchNo" Text='<%#Eval("SupplierBillNo") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- <asp:BoundField ItemStyle-Width="100px" HeaderText="RefDate" DataField="RefDate" />--%>
                                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="TCS" DataField="TCSAmount" />
                                                    <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BasicTotal" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBasicTotal" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGST" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGST" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIGST" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GSTAmount" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTAmount" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GrandTotal" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrandTotal" runat="server" Text='<%# Eval("GrandTotal") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("IsPaid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div id="DivFooterRowPurchase" style="overflow: hidden">
                                            </div>
                                        </div>
                                    </div>
                                    <%--    <asp:GridView ID="dgvHSNSummaryPurchase" runat="server"
                                        CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                        AllowPaging="false" ShowHeader="true" PageSize="50" OnRowDataBound="dgvHSNSummaryPurchase_RowDataBound">
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="Qty" DataField="Qty" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="BasicTotal" DataField="BasicTotal" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="HSN Code" DataField="HSN" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="UOM" DataField="UOM" />
                                            <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCGSTAmt" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSGSTAmt" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIGSTAmt" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>--%>
                                </div>

                                <%-- Added by shubham for Sales reports excell downloadng--%>

                                <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent2">
                                    <asp:GridView ID="GVReports" runat="server"
                                        CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                        AllowPaging="false" ShowHeader="true" PageSize="50" OnRowDataBound="GVReports_RowDataBound" DataKeyNames="Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Party">
                                                <ItemTemplate>
                                                    <%--<asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="true" />--%>
                                                    <asp:Label ID="lblparty" runat="server" Text='<%# Eval("BillingCustomer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:BoundField DataField="BillingCustomer" HeaderText="Party" ReadOnly="true" />--%>

                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <%--<asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="true" />--%>
                                                    <asp:Label ID="LBLType1" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GSTNumber">
                                                <ItemTemplate>
                                                    <%-- <asp:BoundField DataField="GSTNumber" HeaderText="GSTNumber" />--%>
                                                    <asp:Label ID="lblGSTNumber1" runat="server" Text='<%# Eval("GSTNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="VoucherType">
                                                <ItemTemplate>
                                                    <%-- <asp:BoundField DataField="VoucherType" HeaderText="VoucherType" />--%>
                                                    <asp:Label ID="lblVType1" runat="server" Text='<%# Eval("VoucherType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vhno">
                                                <ItemTemplate>
                                                    <%--<asp:BoundField DataField="Vhno" HeaderText=" Vhno" />--%>
                                                    <asp:Label ID="lblVhno1" runat="server" Text='<%# Eval("Vhno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <%--  <asp:BoundField DataField="Date" HeaderText="Date" />--%>
                                          <%--          <asp:Label ID="lblnoiceDate1" runat="server" Text='<%# Eval("Date") %>  '></asp:Label>--%>
                                 <asp:Label ID="lblnoiceDate1" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RefNo">
                                                <ItemTemplate>
                                                    <%--  <asp:BoundField DataField="RefNo" HeaderText="RefNo" />--%>
                                                    <asp:Label ID="lblrefno1" runat="server" Text='<%# Eval("RefNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RefDate">
                                                <ItemTemplate>
                                                    <%-- <asp:BoundField DataField="RefDate" HeaderText="RefDate" />--%>
                                                    <asp:Label ID="lblRefdate1" runat="server" Text='<%# Eval("RefDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TCS">
                                                <ItemTemplate>
                                                    <%--   <asp:BoundField DataField="TCS" HeaderText="TCS" />--%>
                                                    <asp:Label ID="lblTCS1" runat="server" Text='<%# Eval("TCS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty1" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BasicTotal">
                                                <ItemTemplate>
                                                    <%--  <asp:BoundField DataField="BasicTotal" HeaderText="BasicTotal" />--%>
                                                    <asp:Label ID="lblBasicTotal1" runat="server" Text='<%# Eval("BasicTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CGST">
                                                <ItemTemplate>
                                                    <%--<asp:BoundField DataField="CGSTPer" HeaderText="CGSTPer" />--%>
                                                    <asp:Label ID="lblCGST1" runat="server" Text='<%# Eval("CGSTPer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CGSTAMT">
                                                <ItemTemplate>
                                                    <%--<asp:BoundField DataField="CGSTAMT" HeaderText="CGSTAMT" />--%>
                                                    <asp:Label ID="lblCGSTAmt1" runat="server" Text='<%# Eval("CGSTAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SGST">
                                                <ItemTemplate>
                                                    <%-- <asp:BoundField DataField="SGSTPer" HeaderText="SGSTPer" />--%>
                                                    <asp:Label ID="lblSGST1" runat="server" Text='<%# Eval("SGSTPer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGSTAmt">
                                                <ItemTemplate>
                                                    <%-- <asp:BoundField DataField="SGSTAmt" HeaderText="SGSTAmt" />--%>
                                                    <asp:Label ID="lblSGSTamt1" runat="server" Text='<%# Eval("SGSTAmt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST">
                                                <ItemTemplate>
                                                    <%--  <asp:BoundField DataField="IGSTPer" HeaderText="IGSTPer" />--%>
                                                    <asp:Label ID="lblIGST1" runat="server" Text='<%# Eval("IGSTPer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="IGSTAmt">
                                                <ItemTemplate>
                                                    <%--    <asp:BoundField DataField="IGSTAmt" HeaderText="IGSTAmt" />--%>
                                                    <asp:Label ID="lblIGSTAmt1" runat="server" Text='<%# Eval("IGSTAmt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GSTAmt">
                                                <ItemTemplate>
                                                    <%--             <asp:BoundField DataField="GSTAmt" HeaderText="GSTAmt" />--%>
                                                    <asp:Label ID="lblGSTAmount1" runat="server" Text='<%# Eval("GSTAmt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GrandTotal">
                                                <ItemTemplate>

                                                    <%--    <asp:BoundField DataField="GrandTotal" HeaderText="GrandTotal" />--%>
                                                    <asp:Label ID="lblGrandTotal1" runat="server" Text='<%# Eval("GrandTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <%--<asp:BoundField DataField="PaymentStatus" HeaderText="Status" />--%>
                                                    <asp:Label ID="lblStatus1" runat="server" Text='<%# Eval("PaymentStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <asp:GridView ID="dgvHSNSummaryPurchase" runat="server"
                                    CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                    AllowPaging="false" ShowHeader="true" PageSize="50" OnRowDataBound="dgvHSNSummaryPurchase_RowDataBound">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="100px" HeaderText="Qty" DataField="Qty" />
                                        <asp:BoundField ItemStyle-Width="100px" HeaderText="BasicTotal" DataField="BasicTotal" />
                                        <asp:BoundField ItemStyle-Width="100px" HeaderText="HSN Code" DataField="HSN" />
                                        <asp:BoundField ItemStyle-Width="100px" HeaderText="UOM" DataField="UOM" />
                                        <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGSTAmt" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGSTAmt" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGSTAmt" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                                    <asp:GridView ID="dgvHSNSummary" ShowFooter="true" runat="server" OnRowDataBound="dgvHSNSummary_RowDataBound"
                                        CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                        AllowPaging="false" ShowHeader="true" PageSize="50">
                                        <Columns>
                                            <asp:TemplateField HeaderText="HSN Code" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("HSN") %>' ID="lblHSN" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Qty") %>' ID="lblQty" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalQty" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("UOM") %>' ID="lblUOM" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BasicTotal" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("BasicTotal") %>' ID="lblBasicTotal" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalBasicTotal" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("CGST") %>' ID="lblCGST" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalCGST" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SGST" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("SGST") %>' ID="lblSGST" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalSGST" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("IGST") %>' ID="lblIGST" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="totalIGST" ForeColor="Green" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField ItemStyle-Width="100px" HeaderText="Qty" DataField="Qty" />--%>
                                            <%--  <asp:BoundField ItemStyle-Width="100px" HeaderText="BasicTotal" DataField="BasicTotal" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="HSN Code" DataField="HSN" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="UOM" DataField="UOM" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="CGST" DataField="CGST" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="SGST" DataField="SGST" />
                                            <asp:BoundField ItemStyle-Width="100px" HeaderText="IGST" DataField="IGST" />--%>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="row">
                                        <div class="col-4"></div>
                                        <div class="col-4"></div>
                                        <div class="col-4" style="text-align: end;">
                                            <asp:Label runat="server" ID="lbltotalamount" ForeColor="Red" Font-Bold="true"></asp:Label>
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
</asp:Content>

