<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductionPendingReports.aspx.cs" Inherits="Admin_ProductionPendingReports" MasterPageFile="~/Admin/AdminMasterPage.master" %>

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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
                                <h5>Production Pending Report</h5>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">

                            <div class="col-xl-3 col-md-3">
                                <asp:Label ID="lblinvoce" runat="server" Text="Customer Name."></asp:Label>
                                <asp:TextBox ID="txtcustomer" runat="server" CssClass="form-control" placeholder="Customer Name" Width="100%" AutoPostBack="true"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                    TargetControlID="txtcustomer">
                                </asp:AutoCompleteExtender>

                            </div>
                            <div class="col-xl-3 col-md-3">
                                <asp:Label ID="lblFromdate" runat="server" Text="From Date"></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" placeholder="From Date" Width="100%" AutoComplete="off" TextMode="Date"></asp:TextBox>

                            </div>
                            <div class="col-xl-3 col-md-3">
                                <asp:Label ID="lbltodate" runat="server" Text="To Date"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" placeholder="To Date" Width="100%" AutoComplete="off" TextMode="Date"></asp:TextBox>
                            </div>

                            <div class="col-xl-3 col-md-3" style="margin-top: 25px">
                                <asp:Button ID="btnsearch" runat="server" ValidationGroup="form1" CssClass="btn btn-success" Text="Search" OnClick="btnsearch_Click" />
                                <asp:Button ID="btnpdf" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Export Excel" OnClick="btnpdf_Click" />
                                <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" Visible="false" />
                            </div>
                        </div>

                        <br />
                        <br />


                        <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                            <asp:GridView ID="Gvreports" runat="server"
                                CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                AllowPaging="true" ShowHeader="true" ShowFooter="true" PageSize="10" HeaderStyle-BackColor="#009999" OnPageIndexChanging="Gvreports_PageIndexChanging">
                                <EmptyDataTemplate>
                                    <div>No records found.</div>
                                </EmptyDataTemplate>

                                <Columns>
                                    <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDep" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblmsgpaid" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcust" runat="server" Text='<%# Eval("customername") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblmsgpaid1" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Oa Number" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloano" runat="server" Text='<%# Eval("OAno") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblmsgpaid2" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="LblJob" runat="server" Text='<%# Eval("JobNo") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblmsLblJob" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Po No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoNo" runat="server" Text='<%# Eval("pono") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="footerGrandtotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsize" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="LblQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Po Date" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpodte" runat="server" Text='<%# Eval("podate") %>' AutoPostBack="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="Remarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:TextBox>--%>
                                            <asp:Label ID="lblTamount" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dispatch" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDispatch" runat="server" Text='<%# Eval("IsDispatch") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:GridView ID="GvEXCELL" runat="server"
                                CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                ShowHeader="true" ShowFooter="true" HeaderStyle-BackColor="#009999">
                                <Columns>
                                    <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDep" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblmsgpaid" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcust" runat="server" Text='<%# Eval("customername") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblmsgpaid1" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Oa Number" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloano" runat="server" Text='<%# Eval("OAno") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblmsgpaid2" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="LblJob" runat="server" Text='<%# Eval("JobNo") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblmsLblJob" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Po No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoNo" runat="server" Text='<%# Eval("pono") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="footerGrandtotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsize" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="LblQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Po Date" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpodte" runat="server" Text='<%# Eval("podate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="Remarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:TextBox>--%>
                                            <asp:Label ID="lblTamount" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dispatch" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDispatch" runat="server" Text='<%# Eval("IsDispatch") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
