<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="CustomerDetails.aspx.cs" Inherits="Admin_CustomerDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .dissablebtn {
            cursor: not-allowed;
        }
    </style>
    <style>
        .spancls {
            color: #5d5656 !important;
            font-size: 13px !important;
            font-weight: 600;
            text-align: left;
        }

        .starcls {
            color: red;
            font-size: 18px;
            font-weight: 700;
        }

        .card .card-header span {
            color: #060606;
            display: block;
            font-size: 13px;
            margin-top: 5px;
        }

        .reqcls {
            color: red;
            font-weight: 600;
            font-size: 14px;
        }
    </style>

    <style>
        .modelprofile1 {
            background-color: rgba(0, 0, 0, 0.54);
            display: block;
            position: fixed;
            z-index: 1;
            left: 0;
            /*top: 10px;*/
            height: 100%;
            overflow: auto;
            width: 100%;
            margin-bottom: 25px;
        }

        .profilemodel2 {
            background-color: #fefefe;
            margin-top: 25px;
            /*padding: 17px 5px 18px 22px;*/
            padding: 0px 0px 15px 0px;
            width: 100%;
            top: 40px;
            color: #000;
            border-radius: 5px;
        }

        .lblpopup {
            text-align: left;
        }

        .wp-block-separator:not(.is-style-wide):not(.is-style-dots)::before, hr:not(.is-style-wide):not(.is-style-dots)::before {
            content: '';
            display: block;
            height: 1px;
            width: 100%;
            background: #cccccc;
        }

        .btnclose {
            background-color: #ef1e24;
            float: right;
            font-size: 18px !important;
            /* font-weight: 600; */
            color: #f7f6f6 !important;
            border: 0px groove !important;
            background-color: none !important;
            /*margin-right: 10px !important;*/
            cursor: pointer;
            font-weight: 600;
            border-radius: 4px;
            padding: 4px;
        }

        hr.new1 {
            border-top: 1px dashed green !important;
            border: 0;
            margin-top: 5px !important;
            margin-bottom: 5px !important;
            width: 100%;
        }

        .errspan {
            float: right;
            margin-right: 6px;
            margin-top: -25px;
            position: relative;
            z-index: 2;
            color: black;
        }

        .currentlbl {
            text-align: center !important;
        }

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

        .headingcls {
            background-color: #01a9ac;
            color: #fff;
            padding: 15px;
            border-radius: 5px 5px 0px 0px;
        }

        .test tr input {
            border: 1px solid red;
            margin-right: 10px;
            padding-right: 10px;
        }

        .text-green {
            color: green;
        }

        @media (min-width: 1200px) {
            .container {
                max-width: 1250px !important;
            }
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
    <script type="text/javascript">
        window.onload = function () {
            setTimeout(function () {
                document.getElementById('lblMessage').style.display = 'none';
            }, 3000);
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server"></asp:ToolkitScriptManager>

    <div class="page-wrapper">
        <div class="page-body">           
            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h4><i class="fa fa-user-plus"></i>&nbsp;&nbsp;Add Customer</h4>
                    </div>

                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-body">
                                    <asp:Label ID="lblMessage" runat="server" CssClass="text-green"></asp:Label>
                                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger"></asp:Label>

                                    <div class="row">

                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblInvoiceNo" runat="server" class="form-label">Invoice No.<i class="reqcls">*&nbsp;</i></asp:Label>
                                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Invoice No"
                                                ControlToValidate="txtInvoiceNo" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-6">
                                            <asp:Label ID="lblInvoiceDate" runat="server" class="form-label">Invoice Date<i class="reqcls">*&nbsp;</i></asp:Label>
                                            <asp:TextBox ID="txtInvoiceDate" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtInvoiceDate" Format="yyyy-dd-MM" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please choose invoice date"
                                                ControlToValidate="txtInvoiceDate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="form-group col-md-6">
                                            <asp:Label ID="Label1" runat="server" class="form-label">Customer Name<i class="reqcls">*&nbsp;</i></asp:Label>
                                            <asp:TextBox ID="txtCustomerName" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Customer Name"
                                                ControlToValidate="txtCustomerName" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                TargetControlID="txtCustomerName">
                                            </asp:AutoCompleteExtender>
                                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblbasic" runat="server" class="form-label">Basic Amount<i class="reqcls">*&nbsp;</i></asp:Label>
                                            <asp:TextBox ID="txtBasicAmount" runat="server" CssClass="form-control" OnTextChanged="txtBasicAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please enter basic amount"
                                                ControlToValidate="txtBasicAmount" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblcgst" runat="server" class="form-label">CGST<i class="reqcls">*&nbsp;</i></asp:Label>
                                            <asp:TextBox ID="txtCGST" runat="server" CssClass="form-control" OnTextChanged="txtCGST_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Please enter cgst percentage"
                                                ControlToValidate="txtCGST" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblsgst" runat="server" class="form-label">SGST<i class="reqcls">*&nbsp;</i></asp:Label>
                                            <asp:TextBox ID="txtSGST" runat="server" CssClass="form-control" OnTextChanged="txtSGST_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Please enter sgst percentage"
                                                ControlToValidate="txtSGST" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lbligst" runat="server" class="form-label">IGST<i class="reqcls">*&nbsp;</i></asp:Label>
                                            <asp:TextBox ID="txtIGST" runat="server" CssClass="form-control" OnTextChanged="txtIGST_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Please enter igst percentage"
                                                ControlToValidate="txtIGST" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblgrand" runat="server" class="form-label">Grand Total<i class="reqcls">*&nbsp;</i></asp:Label>
                                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <center>
                                                <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-success" Width="100%" Style="display: flex; align-items: center" Text="Add" OnClick="btnAdd_Click" /></center>
                                        </div>
                                        <div class="col-md-2">
                                            <center>
                                                <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Width="100%" Style="display: flex; align-items: center" Text="Cancel" OnClick="btnClear_Click" /></center>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:HiddenField ID="hiddenid" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
