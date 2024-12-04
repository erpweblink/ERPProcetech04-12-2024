<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="QuotationCat2.aspx.cs" Inherits="Admin_QuotationCat2" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:Panel ID="PanelCat1" runat="server" Visible="true">
        <div class="page-wrapper">
            <div class="page-body">
                <asp:HiddenField ID="HFccode" runat="server" />
                <asp:HiddenField ID="HFcname" runat="server" />
                <asp:HiddenField ID="hfregby" runat="server" />

                <div class="row">
                    <div class="col-md-7">
                    </div>

                    <div class="col-md-5">
                        <div class="page-header-breadcrumb">
                            <div style="float: right; margin: 3px; margin-bottom: 5px;">
                                <span><a href="AllCompanyList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Company List</a>&nbsp;&nbsp;
                                <a href="EnquiryList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Enquiry List</a>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="container py-3">
                    <div class="card">
                        <div class="card-header bg-primary text-uppercase text-white">
                            <h5>Quotation (Part of Control Panel)</h5>
                        </div>
                        <div class="row">
                            <div class="col-xl-12 col-md-12">
                                <%--  <div class="card">--%>
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Party Name<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtcname" Width="100%" runat="server" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Company Name"
                                                        ControlToValidate="txtcname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                        TargetControlID="txtcname">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-2 spancls">Quotation No <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txQutno" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Contact Name"
                                                        ControlToValidate="txQutno" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <p>(May change at run time)</p>
                                                </div>
                                            </div>

                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Kind Att. <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlkindatt" runat="server" Width="100%" OnSelectedIndexChanged="ddlkindatt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlkindatt" InitialValue="Select" runat="server" ErrorMessage="Plaese select Kind Att." ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-2 spancls">Date <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtdate" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Date"
                                                        ControlToValidate="txtdate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                                </div>
                                            </div>

                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Address <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtshippingaddress" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                        ControlToValidate="txtshippingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2 spancls">Remark : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtremark" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                        ControlToValidate="txtshippingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row" runat="server">
                                                <div class="col-md-2 spancls">Payment Term <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlpaymentterm" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-4"></div>
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
                                                                    <center><asp:Button ID="btnaddperticular" runat="server" Text="Add" OnClick="btnaddperticular_Click" /></center>
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
                                                <div class="col-md-2 spancls">Ref Document<i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-4">
                                                    <asp:FileUpload ID="FileUploadrefdoc" runat="server" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblimg" Font-Bold="true" ForeColor="Green" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Validity of Offer <i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlvalidityofoffer" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2 spancls">Delivery Period<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">

                                                    <asp:DropDownList ID="ddldeliveryperiod" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Transportation<i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddltransportation" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2 spancls">Standard Packing<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlStandardPacking" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Special Packing<i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlSpecialPacking" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2 spancls">Inspection<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlinspection" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    <%-- <asp:TextBox ID="txtinspection" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetInspectionList"
                                                        TargetControlID="txtinspection">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" Display="Dynamic" ErrorMessage="Please Enter Inspection"
                                                        ControlToValidate="txtinspection" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
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
                                                    <center> <asp:Button ID="btnsubmit" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Send/Save" OnClick="btnsubmit_Click"/></center>
                                                </div>
                                                <div class="col-md-2">
                                                    <center> <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Width="100%" Text="Reset" OnClick="btnreset_Click"/></center>
                                                </div>
                                                <div class="col-md-6"></div>

                                            </div>
                                            <br />

                                        </div>
                                    </div>

                                </div>
                                <%-- </div>--%>
                            </div>
                        </div>

                        <%-- <br />--%>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Button ID="btnprof" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modelprofile" runat="server" TargetControlID="btnprof"
        PopupControlID="PopupAddDetail" OkControlID="Closepopdetail" />
    <asp:Panel ID="PopupAddDetail" runat="server" CssClass="modelprofile1">
        <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 1px; padding-left: 1px;">
            <div class="col-md-3"></div>
            <div class="col-md-7">
                <div class="profilemodel2">
                    <div class="headingcls">
                        <h4 style="margin: 0px;">Add Particulars
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                    </div>

                    <br />

                    <%--Specify Matrial Details--%>
                    <asp:Panel ID="PanelType11" runat="server">
                        <br />
                        <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                            <div class="col-md-12">
                                <%--1--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify1" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify1cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify1cat2" placeholder="Specify" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify1cat3" placeholder="Specify" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--2--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify2" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify2cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify2cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify2cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--3--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify3" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify3cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify3cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify3cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--4--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify4" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify4cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify4cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify4cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--5--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify5" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify5cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify5cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify5cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--6--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify6" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify6cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify6cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify6cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--7--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify7" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify7cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify7cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify7cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--8--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify8" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify8cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify8cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify8cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--9--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify9" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify9cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify9cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify9cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--10--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify10" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify10cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify10cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify10cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--11--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify11" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify11cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify11cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify11cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 12--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify12" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify12cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify12cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify12cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 13--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify13" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify13cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify13cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify13cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 14--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify14" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify14cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify14cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify14cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 15--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkSpecify15" Text="" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtspecify15cat1" placeholder="Category" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify15cat2" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtspecify15cat3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center> <asp:Button ID="btnSubmitmaterial" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitmaterial_Click"/>&nbsp;<asp:Button ID="btnCancelMaterial" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelMaterial_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </div>
            <div class="col-md-2"></div>
        </div>

    </asp:Panel>

    <script type="text/javascript">
        function sum() {
            //1st Row Calculation 


            var txtqty1 = document.getElementById('<%=txtQty1.ClientID%>').value;
            var txtrate1 = document.getElementById('<%=txtRate1.ClientID%>').value;
            var cgst1 = document.getElementById('<%=txtCGST.ClientID%>').value;
            var sgst1 = document.getElementById('<%=txtSGST.ClientID%>').value;
            var igst1 = document.getElementById('<%=txtIGST.ClientID%>').value;

            var cgstamt1 = document.getElementById('<%=txtCGSTamt.ClientID%>').value;
            var sgstamt1 = document.getElementById('<%=txtSGSTamt.ClientID%>').value;
            var igstamt1 = document.getElementById('<%=txtIGSTamt.ClientID%>').value;
            var txtdisc1 = document.getElementById('<%=txtdisc1.ClientID%>').value;
            var txtAmt1 = document.getElementById('<%=txtAmt1.ClientID%>').value;

            if (isNaN(txtqty1) || txtqty1 == "") { txtqty1 = 0; }
            if (isNaN(txtrate1) || txtrate1 == "") { txtrate1 = 0; }
            if (isNaN(cgst1) || cgst1 == "") { cgst1 = 0; }
            if (isNaN(sgst1) || sgst1 == "") { sgst1 = 0; }
            if (isNaN(igst1) || igst1 == "") { igst1 = 0; }

            if (isNaN(cgstamt1) || cgstamt1 == "") { cgstamt1 = 0; }
            if (isNaN(sgstamt1) || sgstamt1 == "") { sgstamt1 = 0; }
            if (isNaN(igstamt1) || igstamt1 == "") { igstamt1 = 0; }

            if (isNaN(txtdisc1) || txtdisc1 == "") { txtdisc1 = 0; }
            if (isNaN(txtAmt1) || txtAmt1 == "") { txtAmt1 = 0; }

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('<%=txtCGSTamt.ClientID%>').value = cgstamt1.toFixed(2); }
            if (!isNaN(sgstamt1)) { document.getElementById('<%=txtSGSTamt.ClientID%>').value = sgstamt1.toFixed(2); }
            if (!isNaN(igstamt1)) { document.getElementById('<%=txtIGSTamt.ClientID%>').value = igstamt1.toFixed(2); }

            var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            var total1 = result1 - (discAmt + totGST1);
            if (!isNaN(result1)) { document.getElementById('<%=txtAmt1.ClientID%>').value = total1.toFixed(2); }

            //2nd Row Calculation 
            var txtqty2 = document.getElementById('<%=txtQty1.ClientID%>').value;
            var txtrate2 = document.getElementById('<%=txtRate1.ClientID%>').value;
            var cgst2 = document.getElementById('<%=txtCGST.ClientID%>').value;
            var sgst2 = document.getElementById('<%=txtSGST.ClientID%>').value;
            var igst2 = document.getElementById('<%=txtIGST.ClientID%>').value;

            var cgstamt2 = document.getElementById('<%=txtCGSTamt.ClientID%>').value;
            var sgstamt2 = document.getElementById('<%=txtSGSTamt.ClientID%>').value;
            var igstamt2 = document.getElementById('<%=txtIGSTamt.ClientID%>').value;
            var txtdisc2 = document.getElementById('<%=txtdisc1.ClientID%>').value;
            var txtAmt2 = document.getElementById('<%=txtAmt1.ClientID%>').value;

            if (isNaN(txtqty2) || txtqty2 == "") { txtqty2 = 0; }
            if (isNaN(txtrate2) || txtrate2 == "") { txtrate2 = 0; }
            if (isNaN(cgst2) || cgst2 == "") { cgst2 = 0; }
            if (isNaN(sgst2) || sgst2 == "") { sgst2 = 0; }
            if (isNaN(igst2) || igst2 == "") { igst2 = 0; }
            if (isNaN(txtdisc2) || txtdisc2 == "") { txtdisc2 = 0; }
            if (isNaN(txtAmt2) || txtAmt2 == "") { txtAmt2 = 0; }

            var result1 = parsefloat(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('<%=txtCGSTamt.ClientID%>').value = cgstamt1.toFixed(2); }
            if (!isNaN(sgstamt1)) { document.getElementById('<%=txtSGSTamt.ClientID%>').value = sgstamt1.toFixed(2); }
            if (!isNaN(igstamt1)) { document.getElementById('<%=txtIGSTamt.ClientID%>').value = igstamt1.toFixed(2); }

            var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            var total1 = result1 - (discAmt + totGST1);
            if (!isNaN(result1)) { document.getElementById('<%=txtAmt1.ClientID%>').value = total1.toFixed(2); }
        }
        //    //2nd Row Calculation 
        //    var txtqty2 = document.getElementById('ContentPlaceHolder1_txtQty2').value;
        //    var txtrate2 = document.getElementById('ContentPlaceHolder1_txtRate2').value;
        //    var txtdisc2 = document.getElementById('ContentPlaceHolder1_txtdisc2').value;
        //    var txtAmt2 = document.getElementById('ContentPlaceHolder1_txtAmt2').value;

        //    if (isNaN(txtqty2) || txtqty2 == "") { txtqty2 = 0; }
        //    if (isNaN(txtrate2) || txtrate2 == "") { txtrate2 = 0; }
        //    if (isNaN(txtdisc2) || txtdisc2 == "") { txtdisc2 = 0; }
        //    if (isNaN(txtAmt2) || txtAmt2 == "") { txtAmt2 = 0; }

        //    var result2 = parseInt(txtqty2) * parseFloat(txtrate2);
        //    var discAmt = (result2 * txtdisc2) / 100;
        //    var total2 = result2 - discAmt;
        //    if (!isNaN(result2)) { document.getElementById('ContentPlaceHolder1_txtAmt2').value = total2.toFixed(2); }

        //    //3 Row Calculation 
        //    var txtqty3 = document.getElementById('ContentPlaceHolder1_txtQty3').value;
        //    var txtrate3 = document.getElementById('ContentPlaceHolder1_txtRate3').value;
        //    var txtdisc3 = document.getElementById('ContentPlaceHolder1_txtdisc3').value;
        //    var txtAmt3 = document.getElementById('ContentPlaceHolder1_txtAmt3').value;

        //    if (isNaN(txtqty3) || txtqty3 == "") { txtqty3 = 0; }
        //    if (isNaN(txtrate3) || txtrate3 == "") { txtrate3 = 0; }
        //    if (isNaN(txtdisc3) || txtdisc3 == "") { txtdisc3 = 0; }
        //    if (isNaN(txtAmt3) || txtAmt3 == "") { txtAmt3 = 0; }

        //    var result3 = parseInt(txtqty3) * parseFloat(txtrate3);
        //    var discAmt = (result3 * txtdisc3) / 100;
        //    var total3 = result3 - discAmt;
        //    if (!isNaN(result3)) { document.getElementById('ContentPlaceHolder1_txtAmt3').value = total3.toFixed(2); }

        //    //4 Row Calculation 
        //    var txtqty4 = document.getElementById('ContentPlaceHolder1_txtQty4').value;
        //    var txtrate4 = document.getElementById('ContentPlaceHolder1_txtRate4').value;
        //    var txtdisc4 = document.getElementById('ContentPlaceHolder1_txtdisc4').value;
        //    var txtAmt4 = document.getElementById('ContentPlaceHolder1_txtAmt4').value;

        //    if (isNaN(txtqty4) || txtqty4 == "") { txtqty4 = 0; }
        //    if (isNaN(txtrate4) || txtrate4 == "") { txtrate4 = 0; }
        //    if (isNaN(txtdisc4) || txtdisc4 == "") { txtdisc4 = 0; }
        //    if (isNaN(txtAmt4) || txtAmt4 == "") { txtAmt4 = 0; }

        //    var result4 = parseInt(txtqty4) * parseFloat(txtrate4);
        //    var discAmt = (result4 * txtdisc4) / 100;
        //    var total4 = result4 - discAmt;
        //    if (!isNaN(result4)) { document.getElementById('ContentPlaceHolder1_txtAmt4').value = total4.toFixed(2); }

        //    //5 Row Calculation 
        //    var txtqty5 = document.getElementById('ContentPlaceHolder1_txtQty5').value;
        //    var txtrate5 = document.getElementById('ContentPlaceHolder1_txtRate5').value;
        //    var txtdisc5 = document.getElementById('ContentPlaceHolder1_txtdisc5').value;
        //    var txtAmt5 = document.getElementById('ContentPlaceHolder1_txtAmt5').value;

        //    if (isNaN(txtqty5) || txtqty5 == "") { txtqty5 = 0; }
        //    if (isNaN(txtrate5) || txtrate5 == "") { txtrate5 = 0; }
        //    if (isNaN(txtdisc5) || txtdisc5 == "") { txtdisc5 = 0; }
        //    if (isNaN(txtAmt5) || txtAmt5 == "") { txtAmt5 = 0; }

        //    var result5 = parseInt(txtqty5) * parseFloat(txtrate5);
        //    var discAmt = (result5 * txtdisc5) / 100;
        //    var total5 = result5 - discAmt;
        //    if (!isNaN(result5)) { document.getElementById('ContentPlaceHolder1_txtAmt5').value = total5.toFixed(2); }
        //}
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
