<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ManualOrderAcceptance.aspx.cs" Inherits="Admin_ManualOrderAcceptance" EnableEventValidation="false" %>

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

        .reqcls {
            color: red;
            font-weight: 600;
            font-size: 14px;
        }

        .aspNetDisabled {
            cursor: not-allowed !important;
        }

        .srtxt {
            margin-top: 30px;
            width: 30px !important;
        }

        .disc {
            width: 30px !important;
        }

        .Desctxt {
            width: 250px !important;
            height: 40px;
        }

        .Hsntxt {
            width: 400px !important;
            height: 100px;
        }

        .Qtytxt {
            margin-top: 30px;
            width: 60px !important;
        }

        .UOMtxt {
            margin-top: 30px;
            width: 60px !important;
        }

        .ddlPerticuler {
            margin-top: 30px;
            width: 150px !important;
        }

        .Ratetxt {
            width: 100px !important;
            margin-top: 30px;
        }

        .Amttxt {
            width: 100px !important;
        }

        .btncss {
            width: 150px !important;
            font-weight: bolder;
            font-size: initial;
        }

        .table td {
            padding: 5px !important;
        }

        .table th {
            padding: 5px !important;
        }
    </style>
    <style>
        .modelprofile1 {
            background-color: rgba(0, 0, 0, 0.54);
            display: block;
            position: fixed;
            z-index: 1;
            left: 0;
            height: 100%;
            overflow: auto;
            width: 100%;
            margin-bottom: 25px;
        }

        .profilemodel2 {
            background-color: #fefefe;
            margin-top: 25px;
            margin-bottom: 25px;
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

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <!-- Boostrap DatePciker JS  -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <%-- <script type="text/javascript">
        function pageLoad() {
            $(document).ready(function () {
                $('.myDate').datepicker({
                    dateFormat: 'dd-mm-yy',
                    inline: true,
                    showOtherMonths: true,
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true,
                    firstDay: 1,
                    navigationAsDateFormat: true,
                    showAnim: "fold",
                    yearRange: "c-100:c+10",
                    dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
                });
                //$('.myDate').datepicker('setDate', new Date());
                $('.ui-datepicker').hide();
            });
        }
    </script>--%>
    <script type='text/javascript'>
        function scrollToElement() {
            var target = document.getElementById("divdtls").offsetTop;
            window.scrollTo(0, target);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelCat1" runat="server" Visible="true">
                <div class="page-wrapper">
                    <div class="page-body">
                        <asp:HiddenField ID="HFccode" runat="server" />
                        <asp:HiddenField ID="HFcname" runat="server" />
                        <asp:HiddenField ID="hfregby" runat="server" />
                        <asp:HiddenField ID="alltotal" runat="server" />

                        <asp:HiddenField ID="HFQuotationid" runat="server" />

                        <asp:HiddenField ID="HFfile1" runat="server" />
                        <asp:HiddenField ID="HFfile2" runat="server" />
                        <asp:HiddenField ID="HFfile3" runat="server" />
                        <asp:HiddenField ID="HFfile4" runat="server" />
                        <asp:HiddenField ID="HFfile5" runat="server" />


                        <div class="row">
                            <div class="col-md-7">
                            </div>

                            <div class="col-md-5">
                                <div class="page-header-breadcrumb">
                                    <div style="float: right; margin: 3px; margin-bottom: 5px;">
                                        <span><a href="OrderAcceptanceList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;OA List</a>&nbsp;&nbsp;
                                <a href="QuotationList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Quotation List</a>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid py-3">
                            <div class="card">
                                <div class="card-header bg-primary text-uppercase text-white">
                                    <h5>Create Order (OA)</h5>
                                </div>
                                <div class="row">
                                    <div class="col-xl-12 col-md-12">

                                        <div class="card-header">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <br />
                                                    <div class="row">
                                                       <%-- <div class="col-md-1 spancls">OA No<i class="reqcls">*&nbsp;</i> : </div>--%>
                                                        <div class="spancls" style="margin-left: 15px;">OA No<i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtOAno" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Company Name"
                                                                ControlToValidate="txtOAno" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <p style="font-size: 12px !important;">(May change at run time)</p>
                                                        </div>
                                                        <%-- New Field by Nikhil  --%>
                                                        <%--<div class="col-md-1 spancls">Job No<i class="reqcls">*&nbsp;</i> : </div>--%>
                                                        <div class="spancls" style="margin-left: 21px;">Job No<i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtJobNo" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                        <%-- End--%>

                                                        <div class="col-md-1 spancls">Date<i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txttodaysdate" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Customer Name<i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtcustomername" Width="100%" runat="server" AutoPostBack="true" OnTextChanged="txtcustomername_TextChanged"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Customer Name"
                                                                ControlToValidate="txtcustomername" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                                TargetControlID="txtcustomername">
                                                            </asp:AutoCompleteExtender>
                                                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                        </div>
                                                        <div class="col-md-2 spancls">Address <i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtaddress" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                                ControlToValidate="txtaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" id="divGstaddres">
                                                        <div class="col-md-2 spancls">GST NO<i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtGSTNO" Width="100%" runat="server" OnTextChanged="txtGSTNO_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Shiping Address <i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtShipingAddress" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <br />
                                                    <div class="row" style="display: none">
                                                        <div class="col-md-2 spancls">Quotation No : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtquotationno" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ErrorMessage="Please Enter Quotation No"
                                                        ControlToValidate="txtquotationno" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator--%>
                                                        </div>

                                                        <div class="col-md-2 spancls">Quotation Date : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtquotationdate" Width="100%" CssClass="myDate" runat="server" ReadOnly="true"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Date"
                                                        ControlToValidate="txtquotationdate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                            <%--<asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtquotationdate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>--%>
                                                        </div>
                                                    </div>

                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">PO No <i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtpono" Width="100%" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter PO No"
                                                                ControlToValidate="txtpono" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2 spancls">PO Date : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtpodate" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtpodate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="row" runat="server">
                                                        <div class="col-md-2 spancls">Contact Person (Purchase) : </div>
                                                        <div class="col-md-4">
                                                            <%-- <asp:DropDownList ID="ddlcontactperson" Width="100%" runat="server"></asp:DropDownList>--%>
                                                            <asp:TextBox ID="ddlcontactperson" Width="100%" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlcontactperson" InitialValue="Select" runat="server" ErrorMessage="Plaese Enter contact person" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2 spancls">Contact No : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtpurchasecontactno" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server">
                                                        <div class="col-md-2 spancls">Contact Person (Technical) : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtcontactpersontechnical" Width="100%" runat="server"></asp:TextBox>
                                                            <%--<asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtpodate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>--%>
                                                        </div>
                                                        <div class="col-md-2 spancls">Contact No : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txttechnicalcontactno" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server">
                                                        <div class="col-md-2 spancls">Taxation <i class="reqcls">*&nbsp;</i> : </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList CssClass="form-control" ID="ddltaxation" runat="server" AutoPostBack="true" OnTextChanged="ddltaxation_TextChanged"></asp:DropDownList>
                                                            <%--     <asp:DropDownList ID="ddltaxation" runat="server" Width="100%">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="inmah">ONLY MAHARASHTRA (9% SGST + CGST)</asp:ListItem>
                                                                <asp:ListItem Value="outmah">OUT OF MAHARASHTRA (18% IGST)</asp:ListItem>
                                                                <asp:ListItem Value="outind">OUT OF INDIA (NO GST)</asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="card-header bg-primary text-uppercase text-white">
                                                        <h5>Products</h5>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="table-responsive">
                                                                <table class="table" border="1" style="width: 100%; border: 1px solid #0c7d38;">
                                                                    <tr style="background-color: #7ad2d4; color: #000; font-weight: 600; text-align: center;">
                                                                        <td>SN</td>
                                                                        <td runat="server" id="tdddlPerticular1">Perticulars</td>
                                                                        <td>Description</td>
                                                                        <td>HSN</td>
                                                                        <td>Qty</td>
                                                                        <td>UOM</td>
                                                                        <td>Price</td>
                                                                        <td>Discount</td>
                                                                        <td>Total Amount</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <center>
                                                                                <asp:TextBox ID="txtsr1" CssClass="srtxt" runat="server" Text="1" ReadOnly="true"></asp:TextBox>
                                                                            </center>
                                                                        </td>
                                                                        <td runat="server" id="tdddlPerticular">
                                                                            <center>
                                                                                <asp:DropDownList ID="ddlPerticuler" CssClass="ddlPerticuler" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlPerticuler_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                    <%--                                                                                   <asp:ListItem Value="Enclosure For Control Panel.">Enclosure For Control Panel.</asp:ListItem>
                                                                                     <asp:ListItem Value="Part Of Enclosures.">Part Of Enclosures.</asp:ListItem>--%>
                                                                                    <asp:ListItem Value="MS fabricated Enclosure.">MS fabricated Enclosure.</asp:ListItem>
                                                                                    <asp:ListItem Value="Loose Parts for the Enclosure.">Loose Parts for the Enclosure.</asp:ListItem>

                                                                                </asp:DropDownList>
                                                                            </center>
                                                                        </td>
                                                                        <td>
                                                                            <center>
                                                                                <asp:TextBox ID="txtdescription" CssClass="Hsntxt" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                            </center>
                                                                        </td>
                                                                        <td>
                                                                            <center>
                                                                                <asp:TextBox ID="txtHSN" CssClass="Ratetxt" runat="server"></asp:TextBox>
                                                                            </center>
                                                                        </td>
                                                                        <td>
                                                                            <center>
                                                                                <asp:TextBox ID="txtQty" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox>
                                                                            </center>
                                                                        </td>
                                                                        <td>
                                                                            <center>
                                                                                <asp:TextBox ID="txtuom" Width="100px" runat="server" Text="Nos" CssClass="UOMtxt"></asp:TextBox>
                                                                            </center>
                                                                        </td>


                                                                        <td>
                                                                            <center>
                                                                                <asp:TextBox ID="txtprice" CssClass="Ratetxt" onkeyup="sum()" onfocus="select()" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <center>
                                                                                <asp:TextBox ID="txtdrgref" onkeyup="sum()" onfocus="select()" CssClass="Ratetxt" runat="server" placeholder="%" Text="0"></asp:TextBox>
                                                                            </center>
                                                                        </td>
                                                                        <td>
                                                                            <center>
                                                                                <asp:TextBox ID="txtTotalamt" CssClass="Ratetxt" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td style="width: 100px">
                                                                            <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-sm btncss" OnClick="Insert" runat="server" Text="Add" />
                                                                        </td>
                                                                        <td colspan="5">
                                                                            <div class="row" style="margin-top: 5px;">
                                                                                <div class="col-md-1 spancls" style="margin-left: 17px">GST<i class="reqcls">*&nbsp;</i> : </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:TextBox ID="txtcgstper" onkeyup="sum()" onfocus="select()" placeholder="CGST%" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:TextBox ID="txtcgstamt" runat="server" placeholder="Amt" ReadOnly="true" Width="130%" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:TextBox ID="txtsgstper" onkeyup="sum()" onfocus="select()" placeholder="SGST%" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:TextBox ID="txtsgstamt" runat="server" placeholder="Amt" ReadOnly="true" Width="130%" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:TextBox ID="txtigstper" onkeyup="sum()" onfocus="select()" placeholder="IGST%" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:TextBox ID="txtigstamt" runat="server" placeholder="Amt" ReadOnly="true" Width="130%" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>


                                                    </div>
                                                    <br />
                                                    <hr />
                                                    <div class="row">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="dgvProductDtl" runat="server" CssClass="table" HeaderStyle-BackColor="#009999" AutoGenerateColumns="false"
                                                                EmptyDataText="No records has been added." OnRowDataBound="dgvProductDtl_RowDataBound" OnRowUpdating="dgvProductDtl_RowUpdating" OnRowEditing="dgvProductDtl_RowEditing" OnRowDeleting="dgvProductDtl_RowDeleting" OnRowCancelingEdit="dgvProductDtl_RowCancelingEdit">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>' ReadOnly="true"></asp:Label>
                                                                            <asp:Label ID="lblid" runat="Server" Text='<%# Eval("id") %>' Visible="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Perticulars" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox Text='<%# Eval("Perticulars") %>' Width="40" ID="txtPerticular" runat="server" ReadOnly="true"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPerticular" runat="Server" Text='<%# Eval("Perticulars") %>' ReadOnly="true" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="HSN" HeaderText="HSN" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="UOM" HeaderText="UOM" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Price" HeaderText="Price" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="CGST" HeaderText="CGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                                                    <asp:BoundField DataField="SGST" HeaderText="SGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                                                    <asp:BoundField DataField="IGST" HeaderText="IGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                                                    <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                                                    <asp:CommandField ShowEditButton="true" ButtonType="Link" CancelText="<i class='fa fa-close' style='font-size:24px;color:red;'></i>" UpdateText="<i class='fa fa-refresh' style='font-size:24px;color:blue;'></i>" EditText="<i class='fa fa-edit' style='font-size:24px;color:blue;'></i>" />
                                                                    <asp:CommandField ShowDeleteButton="true" ButtonType="Link" DeleteText="<i class='fa fa-trash-o' style='font-size:24px;color:red;'></i>" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-3" style="background-color: aquamarine">
                                                            <strong>Sum of material Amount : </strong>
                                                        </div>
                                                        <div class="col-md-3" style="background-color: aquamarine">
                                                            <asp:Label runat="server" ID="lblsumaofmaterialAmt">0</asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                        </div>
                                                    </div>
                                                    <div class="row" id="divdtls">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="dgvOADetailsLoad" runat="server" CssClass="table" HeaderStyle-BackColor="#009999" AutoGenerateColumns="false"
                                                                EmptyDataText="No records has been added." OnRowCommand="dgvOADetailsLoad_RowCommand" OnRowDataBound="dgvOADetailsLoad_RowDataBound" OnRowEditing="dgvOADetailsLoad_RowEditing">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            <asp:Label ID="lblid" runat="Server" Text='<%# Eval("id") %>' Visible="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Perticulars" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox Text='<%# Eval("Description") %>' Width="300" ID="txtPerticular" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPerticular" runat="Server" Text='<%# Eval("Description") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="HSN" HeaderText="HSN" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />--%>
                                                                    <asp:TemplateField HeaderText="HSN" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHSN" runat="Server" Text='<%# Eval("HSN") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox Text='<%# Eval("Qty") %>' Width="120" ID="txtQty1" AutoPostBack="true" OnTextChanged="txtQty_TextChanged1" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQty" runat="Server" Text='<%# Eval("Qty") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="UOM" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox Text='<%# Eval("UOM") %>' Width="120" ID="txtuom" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnit" runat="Server" Text='<%# Eval("UOM") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <%--<asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />--%>
                                                                    <asp:TemplateField HeaderText="Price" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox Text='<%# Eval("Price") %>' Width="120" ID="txtPrice1" OnTextChanged="txtPrice_TextChanged1" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPrice" runat="Server" Text='<%# Eval("Price") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="Price" HeaderText="Price" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />--%>
                                                                    <asp:TemplateField HeaderText="Discount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox Text='<%# Eval("Discount") %>' Width="120" ID="txtDiscount1" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged1" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDiscount" runat="Server" Text='<%# Eval("Discount") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />--%>
                                                                    <%--<asp:BoundField DataField="CGST" HeaderText="CGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />--%>
                                                                    <asp:TemplateField HeaderText="CGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCGST" runat="Server" Text='<%# Eval("CGST") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="SGST" HeaderText="SGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />--%>
                                                                    <asp:TemplateField HeaderText="SGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSGST" runat="Server" Text='<%# Eval("SGST") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="IGST" HeaderText="IGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />--%>
                                                                    <asp:TemplateField HeaderText="IGST" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIGST" runat="Server" Text='<%# Eval("IGST") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />--%>
                                                                    <asp:TemplateField HeaderText="TotalAmount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox Text='<%# Eval("TotalAmount") %>' Width="120" ID="txtTotalAmount" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotalAmount" runat="Server" Text='<%# Eval("TotalAmount") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton Text="Update" runat="server" OnClick="OnUpdate" ToolTip="Update"><i class="fa fa-refresh" style="font-size:28px;color:green;"></i></asp:LinkButton>
                                                                            |
                                                                    <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" ToolTip="Cancel"><i class="fa fa-close" style="font-size:28px;color:red;"></i></asp:LinkButton>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" ToolTip="Edit"><i class="fa fa-edit" style="font-size:28px;color:blue;"></i></asp:LinkButton>
                                                                            | 
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="RowDelete" CommandArgument='<%# Eval("id") %>' ToolTip="Delete"><i class="fa fa-trash-o" style="font-size:28px;color:red"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" id="divamt">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-3" style="background-color: aquamarine">
                                                            <strong>Sum of material Amount : </strong>
                                                        </div>
                                                        <div class="col-md-3" style="background-color: aquamarine">
                                                            <asp:Label runat="server" ID="lblsumaofmaterialAmtLoad"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                        </div>
                                                    </div>

                                                    <div class="card-header bg-primary text-uppercase text-white">
                                                        <h5>Special Note</h5>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Note 1<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote1" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Note 2<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote2" Width="100%" runat="server"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Note 3<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote3" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Note 4<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote4" Width="100%" runat="server"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Note 5<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote5" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Note 6<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote6" Width="100%" runat="server"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Note 7<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote7" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Note 8<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote8" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Note 9<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtnote9" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Delivery Date Required by customer<i class="reqcls">*&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtdeliverydatebycustomer" Width="100%"  runat="server" ReadOnly="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtdeliverydatebycustomer" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-2 spancls">Delivery Date Committed by us<i class="reqcls">*&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtdeliverydatebyus" Width="100%"  runat="server" ReadOnly="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtdeliverydatebyus" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Packing<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlregular" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlregular_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Regular" Value="Regular"></asp:ListItem>
                                                                <asp:ListItem Text="Wooden" Value="Wooden"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlcostincluded" runat="server" Width="100%" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlcostincluded_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Cost included" Value="Cost included"></asp:ListItem>
                                                                <asp:ListItem Text="Extra Billing" Value="Extra Billing"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlAmount" runat="server" Width="100%" Visible="false">
                                                                <asp:ListItem Selected="True" Text="Amount" Value="Amount"></asp:ListItem>
                                                                <asp:ListItem Text="At Actual" Value="At Actual"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls" style="width: 100%">
                                                            Delivery / Transportation Charges<i class="reqcls">&nbsp;</i> :
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtdeliverytransportaioncharge" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Terms Of Payment<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txttermsofpayment" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls" style="width: 100%">
                                                            Billing Details<i class="reqcls">&nbsp;</i> :
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtbillingdetails" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Buyer<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtbuyer" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls" style="width: 100%">
                                                            Consignee<i class="reqcls">*&nbsp;</i> :
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtConsignee" Width="100%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-4">
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="card-header bg-primary text-uppercase text-white">
                                                        <h5>Any Special instruction / Note</h5>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Instruction 1<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="Chkinstaruction1" runat="server" />
                                                            <asp:TextBox ID="txtinstruction1" Width="90%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Instruction 2<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="Chkinstaruction2" runat="server" />
                                                            <asp:TextBox ID="txtinstruction2" Width="90%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Instruction 3<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="Chkinstaruction3" runat="server" />
                                                            <asp:TextBox ID="txtinstruction3" Width="90%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Instruction 4<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="Chkinstaruction4" runat="server" />
                                                            <asp:TextBox ID="txtinstruction4" Width="90%" runat="server"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Instruction 5<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="Chkinstaruction5" runat="server" />
                                                            <asp:TextBox ID="txtinstruction5" Width="90%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Instruction 6<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="Chkinstaruction6" runat="server" />
                                                            <asp:TextBox ID="txtinstruction6" Width="90%" runat="server"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">Instruction 7<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="Chkinstaruction7" runat="server" />
                                                            <asp:TextBox ID="txtinstruction7" Width="90%" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Instruction 8<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="Chkinstaruction8" runat="server" />
                                                            <asp:TextBox ID="txtinstruction8" Width="90%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="card-header bg-primary text-uppercase text-white">
                                                        <h5>Attachment</h5>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">File 1<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            <strong>
                                                                <asp:Label ID="lblfile1" runat="server" Text=""></asp:Label></strong>
                                                        </div>
                                                        <div class="col-md-2 spancls">File 2<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:FileUpload ID="FileUpload2" runat="server" />
                                                            <strong>
                                                                <asp:Label ID="lblfile2" runat="server" Text=""></asp:Label></strong>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">File 3<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:FileUpload ID="FileUpload3" runat="server" />
                                                            <strong>
                                                                <asp:Label ID="lblfile3" runat="server" Text=""></asp:Label></strong>
                                                        </div>
                                                        <div class="col-md-2 spancls">File 4<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:FileUpload ID="FileUpload4" runat="server" />
                                                            <strong>
                                                                <asp:Label ID="lblfile4" runat="server" Text=""></asp:Label></strong>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2 spancls">File 5<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:FileUpload ID="FileUpload5" runat="server" />
                                                            <strong>
                                                                <asp:Label ID="lblfile5" runat="server" Text=""></asp:Label></strong>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="card-header bg-primary text-uppercase text-white" runat="server" visible="false">
                                                        <h5>Send to</h5>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" visible="false">
                                                        <div class="col-md-2 spancls">Email 1<i class="reqcls">*&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail1" Width="100%" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtemail1" runat="server" ErrorMessage="Email required"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtemail1" ErrorMessage="Invalid Email" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="col-md-2 spancls">Email 2<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail2" Width="100%" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtemail2" ErrorMessage="Invalid Email" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" visible="false">
                                                        <div class="col-md-2 spancls">Email 3<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail3" Width="100%" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtemail3" ErrorMessage="Invalid Email" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="col-md-2 spancls">Email 4<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail4" Width="100%" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtemail4" ErrorMessage="Invalid Email" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" visible="false">
                                                        <div class="col-md-2 spancls">Email 5<i class="reqcls">&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail5" Width="100%" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtemail5" ErrorMessage="Invalid Email" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-4">
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" visible="false">
                                                        <div class="col-md-2 spancls">Send OA To Customer<i class="reqcls">*&nbsp;</i> :</div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddloatocustomer" runat="server" Width="100%">
                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2 spancls"></div>
                                                        <div class="col-md-4">
                                                            <%-- <asp:Label ID="lblcustemail" runat="server" Text=""></asp:Label>--%>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3"></div>
                                                        <div class="col-md-1">
                                                        </div>
                                                        <div class="col-md-2">
                                                            <center>
                                                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Send/Save" OnClick="btnsubmit_Click" />
                                                            </center>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <center>
                                                                <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Width="100%" Text="Reset" OnClick="btnreset_Click" />
                                                            </center>
                                                        </div>
                                                        <div class="col-md-6"></div>

                                                    </div>
                                                    <br />

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsubmit" />
            <asp:PostBackTrigger ControlID="dgvProductDtl" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:HiddenField ID="cgstsmthdn" runat="server" />
    <asp:HiddenField ID="sgstsmthdn" runat="server" />
    <asp:HiddenField ID="igstsmthdn" runat="server" />

    <script type="text/javascript">
        function sum() {
        //1st Row Calculation

            <%--var txtqty = document.getElementById('<%=txtQty.ClientID%>').value;
            var txtprice = document.getElementById('<%=txtprice.ClientID%>').value;

            var cgst1 = document.getElementById('<%=txtcgstper.ClientID%>').value;
            var sgst1 = document.getElementById('<%=txtsgstper.ClientID%>').value;
            var igst1 = document.getElementById('<%=txtigstper.ClientID%>').value;

            var cgstamt1;
            var sgstamt1;
            var igstamt1;

            if (cgst1 == "") {
                document.getElementById('<%=txtcgstper.ClientID%>').value = '9';
            }
            if (sgst1 == "") {
                document.getElementById('<%=txtsgstper.ClientID%>').value = '9';
            }
            if (igst1 == "") {
                document.getElementById('<%=txtigstper.ClientID%>').value = '0';
            }

            if (isNaN(txtqty) || txtqty == "") { txtqty1 = 0; }
            if (isNaN(txtprice) || txtprice == "") { txtprice = 0; }
            if (isNaN(cgst1) || cgst1 == "") { cgst1 = 0; }
            if (isNaN(sgst1) || sgst1 == "") { sgst1 = 0; }
            if (isNaN(igst1) || igst1 == "") { igst1 = 0; }

            var result1 = parseInt(txtqty) * parseFloat(txtprice);
            if (!isNaN(result1)) { document.getElementById('<%=txtTotalamt.ClientID%>').value = result1.toFixed(2); }



            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            document.getElementById('<%=cgstsmthdn.ClientID%>').value = cgstamt1;
            document.getElementById('<%=sgstsmthdn.ClientID%>').value = sgstamt1;
            document.getElementById('<%=igstsmthdn.ClientID%>').value = igstamt1;

            if (!isNaN(cgstamt1)) { document.getElementById('<%=txtcgstamt.ClientID%>').value = cgstamt1.toFixed(2); }
            if (!isNaN(sgstamt1)) { document.getElementById('<%=txtsgstamt.ClientID%>').value = sgstamt1.toFixed(2); }
            if (!isNaN(igstamt1)) { document.getElementById('<%=txtigstamt.ClientID%>').value = igstamt1.toFixed(2); }--%>

            debugger

            var txtqty1 = document.getElementById('<%=txtQty.ClientID%>').value;
            var txtrate1 = document.getElementById('<%=txtprice.ClientID%>').value;
            var cgst1 = document.getElementById('<%=txtcgstper.ClientID%>').value;
            var sgst1 = document.getElementById('<%=txtsgstper.ClientID%>').value;
            var igst1 = document.getElementById('<%=txtigstper.ClientID%>').value;

            var cgstamt1 = document.getElementById('<%=txtcgstamt.ClientID%>').value;
            var sgstamt1 = document.getElementById('<%=txtsgstamt.ClientID%>').value;
            var igstamt1 = document.getElementById('<%=txtigstamt.ClientID%>').value;

            var txtAmt1 = document.getElementById('<%=txtTotalamt.ClientID%>').value;

            //taxation



            if (isNaN(cgstamt1) || cgstamt1 == "") { cgstamt1 = 0; }
            if (isNaN(sgstamt1) || sgstamt1 == "") { sgstamt1 = 0; }
            if (isNaN(igstamt1) || igstamt1 == "") { igstamt1 = 0; }

            //if (isNaN(txtdisc1) || txtdisc1 == "") { txtdisc1 = 0; }
            if (isNaN(txtAmt1) || txtAmt1 == "") { txtAmt1 = 0; }

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('<%=txtcgstamt.ClientID%>').value = cgstamt1.toFixed(2); }
            if (!isNaN(sgstamt1)) { document.getElementById('<%=txtsgstamt.ClientID%>').value = sgstamt1.toFixed(2); }
            if (!isNaN(igstamt1)) { document.getElementById('<%=txtigstamt.ClientID%>').value = igstamt1.toFixed(2); }

            // var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            var total1 = result1 + totGST1;
            if (!isNaN(result1)) { document.getElementById('<%=txtTotalamt.ClientID%>').value = total1.toFixed(2); }

            //2nd Row Calculation 
            var txtqty2 = document.getElementById('<%=txtQty.ClientID%>').value;
            var txtrate2 = document.getElementById('<%=txtprice.ClientID%>').value;
            var cgst2 = document.getElementById('<%=txtcgstper.ClientID%>').value;
            var sgst2 = document.getElementById('<%=txtsgstper.ClientID%>').value;
            var igst2 = document.getElementById('<%=txtigstper.ClientID%>').value;

            var cgstamt2 = document.getElementById('<%=txtcgstamt.ClientID%>').value;
            var sgstamt2 = document.getElementById('<%=txtsgstamt.ClientID%>').value;
            var igstamt2 = document.getElementById('<%=txtigstamt.ClientID%>').value;

            var txtAmt2 = document.getElementById('<%=txtTotalamt.ClientID%>').value;
            var txtDiscount = document.getElementById('<%=txtdrgref.ClientID%>').value; //For Discount

            if (isNaN(txtDiscount) || txtDiscount == "") {
                txtDiscount = 0;
            }
            if (isNaN(txtqty2) || txtqty2 == "") { txtqty2 = 0; }
            if (isNaN(txtrate2) || txtrate2 == "") { txtrate2 = 0; }
            if (isNaN(cgst2) || cgst2 == "") { cgst2 = 0; }
            if (isNaN(sgst2) || sgst2 == "") { sgst2 = 0; }
            if (isNaN(igst2) || igst2 == "") { igst2 = 0; }
            //if (isNaN(txtdisc2) || txtdisc2 == "") { txtdisc2 = 0; }
            if (isNaN(txtAmt2) || txtAmt2 == "") { txtAmt2 = 0; }

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('<%=txtcgstamt.ClientID%>').value = cgstamt1.toFixed(2); }
            if (!isNaN(sgstamt1)) { document.getElementById('<%=txtsgstamt.ClientID%>').value = sgstamt1.toFixed(2); }
            if (!isNaN(igstamt1)) { document.getElementById('<%=txtigstamt.ClientID%>').value = igstamt1.toFixed(2); }


            //var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            //var total1 = result1 - (discAmt + totGST1);


            /*          var total1 = result1 + totGST1;*/
            var total1 = result1;

            var discounted_price = total1 - (total1 * txtDiscount / 100)

            if (!isNaN(result1)) { document.getElementById('<%=txtTotalamt.ClientID%>').value = discounted_price.toFixed(2); }
        }
    </script>

    <script type="text/javascript">
        function DisableButton() {
            var btn = document.getElementById("<%=btnsubmit.ClientID %>");
            btn.value = 'Please wait...';
            document.getElementById("<%=btnsubmit.ClientID %>").disabled = true;
            document.getElementById("<%=btnsubmit.ClientID %>").classList.add("dissablebtn");
        }
        window.onbeforeunload = DisableButton;
    </script>
</asp:Content>
