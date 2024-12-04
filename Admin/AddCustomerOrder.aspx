<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AddCustomerOrder.aspx.cs" Inherits="Admin_AddCustomerOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .dissablebtn {
            cursor: not-allowed;
        }

        .srtxt {
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
            width: 100px !important;
        }

        .Qtytxt {
            width: 60px !important;
        }

        .Ratetxt {
            width: 100px !important;
        }

        .Amttxt {
            width: 100px !important;
        }

        .table td {
            padding: 5px !important;
        }

        .table th {
            padding: 5px !important;
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

        .rwotoppadding {
            padding-top: 10px;
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
    <div class="page-wrapper">
        <div class="page-body">

            <div class="row">
                <div class="col-md-7">
                </div>

                <div class="col-md-5">
                    <div class="page-header-breadcrumb">
                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                            <span><a href="AllCompanyList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Company List</a>&nbsp;&nbsp;
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5>Add Customer Order </h5>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card-header">
                                <div class="row">
                                    <div class="col-md-12">
                                        <br />

                                        <div class="row">
                                            <div class="col-md-2 spancls">Billing Customer<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtbillingcustomer" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                    TargetControlID="txtbillingcustomer">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblPendingPayment" CssClass="spancls" runat="server" Text="Pending Payment :"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Shipping Customer<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtshippingcustomer" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 spancls">Shipping Address<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtshippingaddress" TextMode="MultiLine" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Date-Time<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtdate" CssClass="form-control" ReadOnly="true" runat="server" Width="100%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtdate" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txttime" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" Mask="99:99" MaskType="Time" AcceptAMPM="true" MessageValidatorTip="true" TargetControlID="txttime" runat="server"></asp:MaskedEditExtender>
                                            </div>
                                            <div class="col-md-2 spancls">Mode<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlmode" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                                                    <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Customer PO No<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtcustomerpono" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 spancls">PO Date<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtpodate" ReadOnly="true" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtpodate" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Delivery Date<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtdeliverydate" ReadOnly="true" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtdeliverydate" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                            </div>
                                            <div class="col-md-2 spancls">Kindly attention<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlkindlyattention" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Refer Quotation<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlreferquotation" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 spancls">Remark<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtremark" TextMode="MultiLine" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Order Close Mode<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlorderclosemode" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 spancls">Ref Document<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:FileUpload ID="uploadrefdoc" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" id="refdocumentrow" runat="server">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblrefdoument" runat="server" Text=""></asp:Label>
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
                                                            <td>Name Of Particulars</td>
                                                            <td>HSN</td>
                                                            <td>Qty</td>
                                                            <td>Rate</td>
                                                            <td>CGST</td>
                                                            <td>SGST</td>
                                                            <td>IGST</td>
                                                            <td>Disc %</td>
                                                            <td>Amount</td>
                                                        </tr>

                                                        <%--  Row 1--%>
                                                        <tr>
                                                            <td>
                                                                <center><asp:TextBox ID="txtsr1" CssClass="srtxt" runat="server" Text="1"></asp:TextBox></center>
                                                                <asp:RequiredFieldValidator ID="RFieldV4" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                    ControlToValidate="txtsr1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <center><asp:Button ID="btnaddperticular" runat="server" Text="Add" /></center>
                                                            </td>
                                                            <td>
                                                                <center><asp:TextBox ID="txtHsn1" CssClass="Hsntxt" runat="server"></asp:TextBox></center>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                    ControlToValidate="txtHsn1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <center><asp:TextBox ID="txtQty1" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                    ControlToValidate="txtQty1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <center><asp:TextBox ID="txtRate1" onkeyup="sum()" onfocus="select()" CssClass="Ratetxt" runat="server"></asp:TextBox></center>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                    ControlToValidate="txtRate1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td>
                                                                <center><asp:TextBox ID="txtCGST" CssClass="srtxt" placeholder="%" onkeyup="sum()" onfocus="select()" runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCGSTamt" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                    ControlToValidate="txtCGST" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <center><asp:TextBox ID="txtSGST" CssClass="srtxt" placeholder="%" onkeyup="sum()" onfocus="select()" runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="txtSGSTamt" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                    ControlToValidate="txtSGST" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <center><asp:TextBox ID="txtIGST" CssClass="srtxt" placeholder="%" onkeyup="sum()" onfocus="select()" runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="txtIGSTamt" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                    ControlToValidate="txtIGST" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td>
                                                                <center><asp:TextBox ID="txtdisc1" onkeyup="sum()" onfocus="select()" CssClass="disc" runat="server"></asp:TextBox></center>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                    ControlToValidate="txtdisc1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td>
                                                                <center><asp:TextBox ID="txtAmt1" CssClass="Amttxt" runat="server"></asp:TextBox></center>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">PAYMENT TERMS <i class="reqcls">*&nbsp;</i> :</div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlpaymentterms" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 spancls">TRANSPORTATION<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddltransportation" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Do you want to send Email :</div>
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="Chkemail" runat="server" Text="Yes" />
                                            </div>
                                            <div class="col-md-2 spancls">
                                                Email Id :
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-3"></div>
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-2">
                                                <center> <asp:Button ID="btnsubmit" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Send/Save" OnClick="btnsubmit_Click" /></center>
                                            </div>
                                            <div class="col-md-2">
                                                <center> <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Width="100%" Text="Reset" OnClick="btnreset_Click" /></center>
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
    <%--  <script type="text/javascript">
        function DisableButton() {
            var btn = document.getElementById("<%=btnadd.ClientID %>");
            btn.value = 'Please wait...';
            document.getElementById("<%=btnadd.ClientID %>").disabled = true;
            document.getElementById("<%=btnadd.ClientID %>").classList.add("dissablebtn");
        }
        window.onbeforeunload = DisableButton;
    </script>--%>
</asp:Content>
