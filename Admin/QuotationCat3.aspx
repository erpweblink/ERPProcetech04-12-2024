<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="QuotationCat3.aspx.cs" Inherits="Admin_Quotation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .dissablebtn {
            cursor: not-allowed;
        }

        .hideGridColumn {
            display: none;
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

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <!-- Boostrap DatePciker JS  -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
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
                $('.myDate').datepicker('setDate', new Date());
                $('.ui-datepicker').hide();
            });
        }
    </script>

    <script type='text/javascript'>
        function scrollToElement() {
            var target = document.getElementById("divdtls").offsetTop;
            window.scrollTo(0, target);
        }
    </script>


    <script type="text/javascript">

        function captureFormData() {
            var customerId = document.getElementById('<%= HFccode.ClientID %>').value;
            var customerName = document.getElementById('<%= txtcname.ClientID %>').value;
            var quotationNo = document.getElementById('<%= txQutno.ClientID %>').value;
            var kindAtt = document.getElementById('<%= ddlkindatt.ClientID %>').value;
            var quotationDate = document.getElementById('<%= txtdate.ClientID %>').value;
            var shippingAddress = document.getElementById('<%= txtshippingaddress.ClientID %>').value;
            var remark = document.getElementById('<%= txtremark.ClientID %>').value;
            var paymentTerm = document.getElementById('<%= ddlpaymentterm.ClientID %>').value;
            var taxation = document.getElementById('<%= ddltaxation.ClientID %>').value;
            var currency = document.getElementById('<%= ddlCurrency.ClientID %>').value;

            var formData = {
                "CustomerId": customerId,
                "CustomerName": customerName,
                "QuotationNo": quotationNo,
                "KindAtt": kindAtt,
                "QuotationDate": quotationDate,
                "ShippingAddress": shippingAddress,
                "Remark": remark,
                "PaymentTerm": paymentTerm,
                "Taxation": taxation,
                "Currency": currency
            };


            var products = [];

            if (typeof quotationData !== 'undefined' && quotationData.length > 0) {
                for (var i = 0; i < quotationData.length; i++) {

                    if (i == 0) {

                        var existingData = JSON.parse(localStorage.getItem("quotationData"));

                        if (existingData && existingData.Products) {
                            existingData.Products = [];
                        } else {

                            existingData = { Products: [] };
                        }
                    }

                    var row = quotationData[i];

                    if (row && row.description && row.hsncode && row.qty && row.uom && row.rate) {
                        var product = {
                            "SrNo": row.id,
                            "Description": row.description,
                            "HSN": row.hsncode,
                            "Qty": row.qty,
                            "UOM": row.uom,
                            "Price": row.rate,
                            "CGST": {
                                "percent": row.CGST,
                                "amount": row.CGSTamt
                            },
                            "SGST": {
                                "percent": row.SGST,
                                "amount": row.SGSTamt
                            },
                            "IGST": {
                                "percent": row.IGST,
                                "amount": row.IGSTamt
                            },
                            "Discount": row.discount,
                            "TotalAmount": row.amount
                        };
                        products.push(product);
                    }
                }
            }

            formData["Products"] = products;

            localStorage.setItem("quotationData", JSON.stringify(formData));
            LoadProducts();
            sessionStorage.setItem("quotationData", JSON.stringify(formData));
        }
    </script>
    <script type="text/javascript">
        var enquiryData = localStorage.getItem("quotationData");
        var formData = JSON.parse(enquiryData);
        var custId = formData.CustomerId;
        function checkEnquiryDataAfterLogin() {
            var CustomerId = document.getElementById('<%= HFccode.ClientID %>').value;
            if (CustomerId === formData.CustomerId) {
                if (enquiryData) {
                    bindMainFormData(formData);
                }
            }
        }

        function LoadProducts() {
            var CustomerId = document.getElementById('<%= HFccode.ClientID %>').value;
            var enquiryData = localStorage.getItem("quotationData");
            var formData = JSON.parse(enquiryData);
            if (formData != null && CustomerId === custId) {
                var data = formData.Products;
            } else {
                data = "Empty";
            }
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "QuotationCat3.aspx/AddProductsToViewState", true);
            xhr.setRequestHeader("Content-Type", "application/json");

            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                }
            };
            xhr.send(JSON.stringify({ productsJson: JSON.stringify(data) }));

        }

        function bindMainFormData(formData) {
            var txtcname = document.getElementById('<%= txtcname.ClientID %>');
            if (txtcname) txtcname.value = formData.CustomerName;

            var txQutno = document.getElementById('<%= txQutno.ClientID %>');
            if (txQutno) txQutno.value = formData.QuotationNo;

            var ddlkindatt = document.getElementById('<%= ddlkindatt.ClientID %>');
            if (ddlkindatt) ddlkindatt.value = formData.KindAtt;

            var txtdate = document.getElementById('<%= txtdate.ClientID %>');
            if (txtdate) txtdate.value = formData.QuotationDate;

            var txtshippingaddress = document.getElementById('<%= txtshippingaddress.ClientID %>');
            if (txtshippingaddress) txtshippingaddress.value = formData.ShippingAddress;

            var txtremark = document.getElementById('<%= txtremark.ClientID %>');
            if (txtremark) txtremark.value = formData.Remark;

            var ddlpaymentterm = document.getElementById('<%= ddlpaymentterm.ClientID %>');
            if (ddlpaymentterm) ddlpaymentterm.value = formData.PaymentTerm;

            var ddltaxation = document.getElementById('<%= ddltaxation.ClientID %>');
            if (ddltaxation) {
                ddltaxation.value = formData.Taxation;
                var event = new Event('change');
                ddltaxation.dispatchEvent(event);
            }

            var ddlCurrency = document.getElementById('<%= ddlCurrency.ClientID %>');
            if (ddlCurrency) ddlCurrency.value = formData.Currency;

        }


        function RemoveLocalStorage() {
          
            localStorage.removeItem("quotationData");
            LoadProducts();
        }
    </script>


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
                                <asp:Button ID="btnclear" CssClass="btn-danger" runat="server" Text="Clear Record" OnClientClick="RemoveLocalStorage(); return false;" />
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
                            <h5>Quotation</h5>
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
                                                    <asp:TextBox ID="txtcname" Width="100%" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Company Name"
                                                        ControlToValidate="txtcname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-2 spancls">Quotation No <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txQutno" Width="100%" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Qoutation no can't be blank."
                                                        ControlToValidate="txQutno" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <p>(May change at run time)</p>
                                                </div>
                                            </div>

                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Kind Att. <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlkindatt" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddlkindatt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlkindatt" InitialValue="Select" runat="server" ErrorMessage="Plaese select Kind Att." ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="col-md-2 spancls">Quotation Date <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtdate" Width="100%" CssClass="form-control" runat="server" placeholder="Select Date"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Date"
                                                        ControlToValidate="txtdate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                                </div>
                                            </div>

                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Address <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtshippingaddress" Width="100%" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                        ControlToValidate="txtshippingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2 spancls">Remark : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtremark" Width="100%" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                        ControlToValidate="txtshippingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <br />

                                            <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>--%>
                                            <div class="row" runat="server">
                                                <div class="col-md-2 spancls">Payment Term <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlpaymentterm" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <%--  <div class="col-md-2 spancls">Taxation <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddltaxation" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="ddltaxation_TextChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="inmah">ONLY MAHARASHTRA (9% SGST + CGST)</asp:ListItem>
                                                        <asp:ListItem Value="outmah">OUT OF MAHARASHTRA (18% IGST)</asp:ListItem>
                                                        <asp:ListItem Value="outind">OUT OF INDIA (NO GST)</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>--%>

                                                <div class="col-md-2 spancls">Taxation <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList CssClass="form-control" ID="ddltaxation" runat="server" AutoPostBack="true" OnTextChanged="ddltaxation_TextChanged"></asp:DropDownList>

                                                    <%-- <asp:DropDownList ID="ddltaxation" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="ddltaxation_TextChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="inmah">ONLY MAHARASHTRA (9% SGST + CGST)</asp:ListItem>
                                                        <asp:ListItem Value="outmah">OUT OF MAHARASHTRA (18% IGST)</asp:ListItem>
                                                        <asp:ListItem Value="outind">OUT OF INDIA (NO GST)</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                </div>


                                            </div>
                                            <%--    22-03-2022--%>
                                            <br />
                                            <div class="row" runat="server">
                                                <div class="col-md-2 spancls" runat="server">
                                                    <asp:Label runat="server" ID="lblRevise" Text="Is This Revise ? :" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-4" runat="server">
                                                    <%-- <asp:TextBox runat="server" TextMode="MultiLine" ID="txtPTSpecify" CssClass="form-control" ></asp:TextBox--%>
                                                    <asp:CheckBox ID="chkrevise" runat="server" AutoPostBack="true" Visible="false" />
                                                </div>
                                                <div class="col-md-2 spancls">Currency <i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlCurrency" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--  <div class="row" runat="server" id="paym2" style="margin-top: 10px;" visible="false">
                                                        <div class="col-md-2 spancls">Payment Term 2 : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtpayment2" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">
                                                            <asp:Button ID="btnpayment3" runat="server" Text="+" OnClick="btnpayment3_Click" />
                                                        </div>
                                                        <div class="col-md-4"></div>
                                                    </div>
                                                    <div class="row" runat="server" id="paym3" style="margin-top: 10px;" visible="false">
                                                        <div class="col-md-2 spancls">Payment Term 3 : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtpayment3" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">
                                                            <asp:Button ID="btnpayment4" runat="server" Text="+" OnClick="btnpayment4_Click" />
                                                        </div>
                                                        <div class="col-md-4"></div>
                                                    </div>
                                                    <div class="row" runat="server" id="paym4" style="margin-top: 10px;" visible="false">
                                                        <div class="col-md-2 spancls">Payment Term 4 : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtpayment4" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">
                                                            <asp:Button ID="btnpayment5" runat="server" Text="+" OnClick="btnpayment5_Click" />
                                                        </div>
                                                        <div class="col-md-4"></div>
                                                    </div>
                                                    <div class="row" runat="server" id="paym5" style="margin-top: 10px;" visible="false">
                                                        <div class="col-md-2 spancls">Payment Term 5 : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtpayment5" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6"></div>
                                                    </div>--%>
                                            <%--      </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                            <br />
                                            <div class="col-md-12" runat="server" visible="false" id="dvParticular">

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
                                                                    <td>UOM</td>
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
                                                                        <center>
                                                                            <asp:TextBox ID="txtsr1" CssClass="srtxt" runat="server" Text="1"></asp:TextBox>
                                                                        </center>
                                                                        <asp:RequiredFieldValidator ID="RFieldV4" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                            ControlToValidate="txtsr1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtperticular" runat="server" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                                                        </center>
                                                                        <%--                                                                                                                <center><asp:Button ID="btnaddperticular" runat="server" Text="Add" OnClick="btnaddperticular_Click" /></center>--%>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtHsn1" CssClass="Hsntxt" runat="server"></asp:TextBox>
                                                                        </center>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="*"
                                                                            ControlToValidate="txtHsn1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtQty1" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox>
                                                                        </center>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtuom" Width="100px" runat="server" Text="Nos"></asp:TextBox>
                                                                        </center>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtRate1" onkeyup="sum()" onfocus="select()" CssClass="Ratetxt" runat="server"></asp:TextBox>
                                                                        </center>

                                                                    </td>

                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtCGST" CssClass="srtxt" placeholder="%" onkeyup="sum()" onfocus="select()" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                                            <asp:TextBox ID="txtCGSTamt" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox>
                                                                        </center>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtSGST" CssClass="srtxt" placeholder="%" onkeyup="sum()" onfocus="select()" runat="server" Readonly="true" Enabled="false"></asp:TextBox>
                                                                            <asp:TextBox ID="txtSGSTamt" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox>
                                                                        </center>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtIGST" CssClass="srtxt" placeholder="%" onkeyup="sum()" onfocus="select()" runat="server" Readonly="true" Enabled="false"></asp:TextBox>
                                                                            <asp:TextBox ID="txtIGSTamt" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox>
                                                                        </center>
                                                                    </td>

                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtdisc1" onkeyup="sum()" onfocus="select()" CssClass="disc" runat="server" Text="0"></asp:TextBox>
                                                                        </center>
                                                                    </td>

                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtAmt1" CssClass="Amttxt" runat="server"></asp:TextBox>
                                                                        </center>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-4">
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:Button ID="btnAdd" CssClass="btn btn-warning btn-sm btncss" OnClick="Insert" runat="server" Text="Add Item" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="divdtls">
                                                <div class="table-responsive">

                                                    <asp:GridView ID="dgvQuatationDtl" runat="server" CssClass="table" HeaderStyle-BackColor="#009999" AutoGenerateColumns="false"
                                                         OnRowCommand="dgvQuatationDtl_RowCommand" OnRowEditing="dgvQuatationDtl_RowEditing">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    <asp:Label ID="lblid" runat="Server" Text='<%# Eval("id") %>' Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Perticular" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <%--     <HTMLEditor:Editor runat="server" ID="txtDescription" ClientIDMode="AutoID" Content='<%# Eval("description") %>' Height="30px" AutoFocus="true" Width="393px" />--%>

                                                                    <asp:TextBox Text='<%# Eval("description").ToString().Replace("<br>", "\n") %>' Width="393" Rows="10" ID="txtDescription" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <%--<HTMLEditor:Editor runat="server" ID="lblsdescription" ClientIDMode="AutoID" Content='<%# Eval("description") %>' Height="30px" AutoFocus="true" Width="393px" />--%>
                                                                    <%--                                                                    <asp:TextBox Text='<%# Eval("description").ToString().Replace("<br>", "<br />") %>' Width="393" Rows="6" ReadOnly="true" ID="lbldescription" TextMode="MultiLine" runat="server"></asp:TextBox>--%>
                                                                    <asp:Label ID="lbldescription" runat="Server" Text='<%# Eval("description").ToString().Replace("<br>", "<br>") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="hsncode" HeaderText="HSN" ReadOnly="true" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="Qty" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox Text='<%# Eval("qty") %>' Width="40" ID="txtqty" runat="server" AutoPostBack="true" OnTextChanged="txtqty_TextChanged"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblqty" runat="Server" Text='<%# Eval("qty") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="UOM" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox Text='<%# Eval("uom") %>' Width="40" ID="txtuom" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblunit" runat="Server" Text='<%# Eval("uom") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Price" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox Text='<%# Eval("rate") %>' Width="60" ID="txtprice" runat="server" AutoPostBack="true" OnTextChanged="txtprice_TextChanged"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprice" runat="Server" Text='<%# Eval("rate") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CGST" HeaderText="CGST" ReadOnly="true" ItemStyle-Width="120" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="SGST" HeaderText="SGST" ReadOnly="true" ItemStyle-Width="120" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="IGST" HeaderText="IGST" ReadOnly="true" ItemStyle-Width="120" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="CGSTamt" HeaderText="CGSTamt" ReadOnly="true" ItemStyle-Width="120" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="SGSTamt" HeaderText="SGSTamt" ReadOnly="true" ItemStyle-Width="120" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="IGSTamt" HeaderText="IGSTamt" ReadOnly="true" ItemStyle-Width="120" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="totaltax" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltottax" runat="Server" Text='<%# Eval("totaltax") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox Text='<%# Eval("discount") %>' Width="30" ID="txtdiscount" runat="server" AutoPostBack="true" OnTextChanged="txtdiscount_TextChanged"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltxtdiscount" runat="Server" Text='<%# Eval("discount") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TotalAmount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltotalamt" runat="Server" Text='<%# Eval("amount") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <EditItemTemplate>

                                                                    <asp:LinkButton Text="Update" ID="lnkbtnUpdate" ClientIDMode="Static" runat="server" OnClick="OnUpdate" ToolTip="Update"><i class="fa fa-refresh" style="font-size:28px;color:green;"></i></asp:LinkButton>
                                                                    |
                                                                            <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" ToolTip="Cancel" Visible="False"><i class="fa fa-close" style="font-size:28px;color:red;"></i></asp:LinkButton>

                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" ToolTip="Edit"><i class="fa fa-edit" style="font-size:28px;color:blue;"></i></asp:LinkButton>
                                                                    | 
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Delete"><i class="fa fa-trash-o" style="font-size:28px;color:red"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                            </div>

                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Ref Document<i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-4">
                                                    <asp:FileUpload ID="FileUploadrefdoc" runat="server" />
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="Dynamic" ErrorMessage="Please upload documnet "
                                                        ControlToValidate="FileUploadrefdoc" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblimg" Font-Bold="true" ForeColor="Green" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Validity of Offer <i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-4">
                                                    <%--                       <asp:DropDownList ID="ddlvalidityofoffer" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtvalidityofoffer" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetValidityofofferList"
                                                        TargetControlID="txtvalidityofoffer">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="Dynamic" ErrorMessage="Please Enter Validity of Offer"
                                                        ControlToValidate="txtvalidityofoffer" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-md-2 spancls">Delivery Period<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">

                                                    <%-- <asp:DropDownList ID="ddldeliveryperiod" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtdeliveryperiod" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>

                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ErrorMessage="Please Enter Delivery Period"
                                                        ControlToValidate="txtdeliveryperiod" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Transportation<i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txttransportation" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <%-- <asp:DropDownList ID="ddltransportation" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                                    <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetTransportationList"
                                                        TargetControlID="txttransportation">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic" ErrorMessage="Please Enter Transportation"
                                                        ControlToValidate="txttransportation" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-md-2 spancls">Standard Packing<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <%--   <asp:DropDownList ID="ddlStandardPacking" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtstandardpkg" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetStandaradpkgList"
                                                        TargetControlID="txtstandardpkg">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic" ErrorMessage="Please Enter Standard Packaging"
                                                        ControlToValidate="txtstandardpkg" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Special Packing<i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-4">
                                                    <%--    <asp:DropDownList ID="ddlSpecialPacking" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtspecialpakg" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetSpecialpkgList"
                                                        TargetControlID="txtspecialpakg">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Display="Dynamic" ErrorMessage="Please Enter Special Packaging"
                                                        ControlToValidate="txtspecialpakg" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-md-2 spancls">Inspection<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <%--<asp:DropDownList ID="ddlinspection" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtinspection" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <%--<asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetInspectionList"
                                                        TargetControlID="txtinspection">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" Display="Dynamic" ErrorMessage="Please Enter Inspection"
                                                        ControlToValidate="txtinspection" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row" runat="server" visible="false">
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
                                                    <center>
                                                        <asp:Button ID="btnsubmit" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Save" OnClick="btnsubmit_Click" />
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
                                <%-- </div>--%>
                            </div>
                        </div>

                        <%-- <br />--%>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>



    <%--    Add Particulars --%>
    <asp:Button ID="btnprof" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modelprofile" runat="server" TargetControlID="btnprof"
        PopupControlID="PopupAddDetail" OkControlID="Closepopdetail" />
    <asp:Panel ID="PopupAddDetail" runat="server" CssClass="modelprofile1">
        <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 1px; padding-left: 1px;">
            <div class="col-md-2"></div>
            <div class="col-md-10">
                <div class="profilemodel2">
                    <div class="headingcls">
                        <h4 style="margin: 0px;">Add Particulars
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                    </div>

                    <br />
                    <div class="row" style="margin-right: 0px!important; margin-left: 10px; padding: 3px;">
                        <div class="col-md-2">
                            <h5 style="font-size: 15px; font-weight: 700;">Size :</h5>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtwidth" runat="server" CssClass="form-control" Width="90%" placeholder="Width"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtdepth" runat="server" CssClass="form-control" Width="90%" placeholder="Depth"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtheight" runat="server" CssClass="form-control" Width="90%" placeholder="Height"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtbase" runat="server" CssClass="form-control" Width="90%" placeholder="Base"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtcanopy" runat="server" CssClass="form-control" Width="90%" placeholder="Canopy"></asp:TextBox>
                        </div>
                    </div>

                    <hr />
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="row" style="margin-right: 0px!important; margin-left: 10px; padding: 3px;">

                                <div class="col-md-2">
                                    <h5 style="font-size: 15px; font-weight: 700;">Material :</h5>
                                </div>

                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlmaterial" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmaterial_SelectedIndexChanged" CssClass="form-control" Width="100%">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>CRCA MS</asp:ListItem>
                                        <asp:ListItem>SS304</asp:ListItem>
                                        <asp:ListItem>SS316</asp:ListItem>
                                        <asp:ListItem>Specify</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="Select" runat="server" Display="Dynamic" ErrorMessage="Please select material"
                                        ControlToValidate="ddlmaterial" ValidationGroup="form2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtSpecifymaterial" runat="server" Visible="false" CssClass="form-control" Width="90%" placeholder="Specify"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <h5 style="font-size: 15px; font-weight: 700;">Construction Type</h5>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlConstype" AutoPostBack="true" OnSelectedIndexChanged="ddlConstype_SelectedIndexChanged" runat="server" CssClass="form-control" Width="100%">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>JB Box</asp:ListItem>
                                        <asp:ListItem>WMM-22</asp:ListItem>
                                        <asp:ListItem>WMM-30 (MCC Box)</asp:ListItem>
                                        <asp:ListItem>MFS (Modular Floor Standing Enclosure)</asp:ListItem>
                                        <asp:ListItem>Eco MCC 30mm</asp:ListItem>
                                        <asp:ListItem>Modular W-Big 43 mm</asp:ListItem>
                                        <asp:ListItem>Eco Frame 43mm</asp:ListItem>
                                        <asp:ListItem>PC ENCLOSURE</asp:ListItem>
                                        <asp:ListItem>PC TABLE</asp:ListItem>
                                        <asp:ListItem>PRINTER TABLE</asp:ListItem>
                                        <asp:ListItem>Single Piece Desk</asp:ListItem>
                                        <asp:ListItem>Three Piece Desk</asp:ListItem>
                                        <asp:ListItem>Specify</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="Select" runat="server" Display="Dynamic" ErrorMessage="Please select material"
                                        ControlToValidate="ddlConstype" ValidationGroup="form2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8"></div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtconstructiontype" runat="server" Visible="false" CssClass="form-control" Width="90%" placeholder="Construction Type"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ddlConstype" runat="server" />
                            <%--     <asp:AsyncPostBackTrigger ControlID="ddlConstype" EventName="SelectedIndexChanged" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Panel ID="Panel1" runat="server">
                        <br />
                        <%--<asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>--%>
                        <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                            <div class="col-md-12">
                                <div class="row">
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound" DataMember="id">
                                        <ItemTemplate>
                                            <div class="col-md-3 col-lg-3">
                                                <asp:ImageButton class="example-image" ID="Image1" runat="server" Style="margin-bottom: 10px; margin-top: 10px;"
                                                    Width="60%" Height="70px" ImageUrl="../img/excelsheet.png" CommandArgument='<%#Eval("id")%>' CommandName="download" />
                                                <%--<center>--%><br />
                                                <asp:Label ID="lblsheetname" Font-Size="12px" runat="server" Style="float: left;" Text='<%#Eval("sheetname") %>' Font-Bold="true"></asp:Label>
                                                <%--</center>--%>
                                                <br />
                                                <asp:Label ID="lblpath" runat="server" Visible="false" Text='<%#Eval("path") %>'></asp:Label>
                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" Style="height: 25px; padding: 0px 0px 0px 0px; margin: 3px 0px 0px 0px" runat="server" />
                                                <asp:Button ID="btnupload" Enabled="false" CssClass="btn btn-secondary" Style="margin-top: 5px; margin-bottom: 5px; padding: 0px 0px 0px 0px; width: 90px; height: 25px; font-size: 12px; font-weight: 600;" runat="server" Text="Upload File" CommandArgument='<%#Eval("id")%>' CommandName="upload" />
                                                <br />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </asp:Panel>
                    <asp:AsyncFileUpload ID="AsyncFileUpload1" Visible="false" runat="server" />
                    <%--1 JB Box--%>
                    <asp:Panel ID="PanelType1" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <%--1--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                <asp:CheckBox ID="chkJbweldedmainbody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlJbweldedmainbodycat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>with Top and bottom</asp:ListItem>
                                                    <asp:ListItem>with Sides</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlJbweldedmainbodycat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--2--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                <asp:CheckBox ID="chkjbGlandplat" Text="Gland Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddljbGlandplatcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Bottom Side</asp:ListItem>
                                                    <asp:ListItem>Top Side</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddljbGlandplatcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--3--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                <asp:DropDownList ID="ddljbComponetmtgplt" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Component Mtg Plate</asp:ListItem>
                                                    <asp:ListItem>M4 Tapped Z welded </asp:ListItem>
                                                    <asp:ListItem>DIN Channel</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddljbComponetmtgpltcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddljbComponetmtgpltcat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--4--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                <asp:CheckBox ID="chkjbfrontscrewcover" Text="Front Screwed Cover" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddljbfrontscrewcovercat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--5--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                <asp:CheckBox ID="Chkjblock" Text="Lock" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddljbLockcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Thumbscrew</asp:ListItem>
                                                    <asp:ListItem>Coin Lock</asp:ListItem>
                                                    <asp:ListItem>Cam Lock</asp:ListItem>
                                                    <asp:ListItem>3-point Handle Lock</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--6--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                <asp:CheckBox ID="chkjbTransparentdoor" Text="Transperent Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddljbTransparentdoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Glass</asp:ListItem>
                                                    <asp:ListItem>Acrylic</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddljbTransparentdoorcat2" AutoPostBack="true" OnSelectedIndexChanged="ddljbTransparentdoorcat2_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Door</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtjbTransparentdoorcat4" placeholder="Specify" Visible="false" runat="server"></asp:TextBox>
                                                <asp:DropDownList ID="ddljbTransparentdoorcat3" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>With bidding</asp:ListItem>
                                                    <asp:ListItem>biddingless technique</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CheckBox ID="ddljbTransparentdoorcat" Text="Size & Thickness" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--7--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                <asp:CheckBox ID="chkjbwallmtgbracket" Text="Wall Mtg. Bracket" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--8--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkjbPowercoatingshade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddljbPowercoatingshadecat1" runat="server" OnSelectedIndexChanged="ddljbPowercoatingshadecat1_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>RAL-7032</asp:ListItem>
                                                    <asp:ListItem>RAL-7035</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtjbPowercoatingshadecat2" runat="server" Visible="false" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--9--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkjbFan" Text="Fan" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddljbfancat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtjbfanqtycat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--10--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkjbJointlesspolyurethane" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--11--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkjbAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtjbAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="btnSubmitjbbox" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitjbbox_Click" />
                                    &nbsp;<asp:Button ID="btnCanceljbbox" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCanceljbbox_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--2 WMM-23.5 (AE Box)--%>
                    <asp:Panel ID="PanelType2" runat="server" Visible="false">
                        <br />
                        <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                            <div class="col-md-12">
                                <%--1--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                        <asp:CheckBox ID="chkWMM23WeldedMainBody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23WeldedMainBodycat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>with Top and bottom</asp:ListItem>
                                            <asp:ListItem>with Sides</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlWMM23WeldedMainBodycat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.2mm</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--2--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23GlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23GlandPlatecat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Bottom Side</asp:ListItem>
                                            <asp:ListItem>Top Side</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlWMM23GlandPlatecat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.2mm</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--3--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23Canopy" Text="Canopy" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23Canopycat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>Full size</asp:ListItem>
                                            <asp:ListItem>Part size</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23Canopycat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.2mm</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--4--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23ComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23ComponentMtgPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23ComponentMtgPlatecat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23ComponentMtgPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--5--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23SideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23SideCPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23SideCPlatecat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23SideCPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--6--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23DoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23DoorCPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23DoorCPlatecat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23DoorCPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--7--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23WallMtgBracket" Text="Wall Mtg. Bracket" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23WallMtgBracketcat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>Small</asp:ListItem>
                                            <asp:ListItem>Big</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--8--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23FrontDoor" Text="Front Door" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23FrontDoorcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Single</asp:ListItem>
                                            <asp:ListItem>Split</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23FrontDoorcat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--9--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23RearDoor" Text="Rear Door" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23RearDoorcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Single</asp:ListItem>
                                            <asp:ListItem>Split</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlWMM23RearDoorcat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--10--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23DoorStiffener" Text="Door Stiffener" CssClass="myClass" runat="server" />
                                    </div>
                                </div>
                                <%--11--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23Lock" Text="Lock" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23Lockcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Thumbscrew</asp:ListItem>
                                            <asp:ListItem>Coin Lock</asp:ListItem>
                                            <asp:ListItem>Cam Lock</asp:ListItem>
                                            <asp:ListItem>3-point Handle Lock</asp:ListItem>
                                            <asp:ListItem>Mini Lock</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtWMM23Lockcat2" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%--12--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23CableSupportAngle" Text="Cable Support Angle" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtWMM23CableSupportAnglecat1" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 13--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23PowerCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23PowerCoatingShadecat1" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>RAL-7032</asp:ListItem>
                                            <asp:ListItem>RAL-7035</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtddlWMM23PowerCoatingShadecat2" runat="server" placeholder="Specify"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 14--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23LiftingIBolt" Text="Lifting I-Bolt" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtWMM23LiftingIBoltcat1" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 15--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23Base" Text="Base" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23Basecat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Standard</asp:ListItem>
                                            <asp:ListItem>50mm Angle frame</asp:ListItem>
                                            <asp:ListItem>ISMC</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlWMM23Basecat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtWMM23Basecat3" runat="server" placeholder="Specify"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 16--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23TransparentDoor" Text="Transparent Door" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23TransparentDoorcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Glass</asp:ListItem>
                                            <asp:ListItem>Acrylic</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlWMM23TransparentDoorcat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                            <asp:ListItem>4.00mm</asp:ListItem>
                                            <asp:ListItem>5.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlWMM23TransparentDoorcat3" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>With bidding</asp:ListItem>
                                            <asp:ListItem>biddingless technique</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <%-- 17--%>
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                        <asp:CheckBox ID="chkWMM23fan" Text="Fan" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlWMM23fancat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtWMM23fancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 18--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                        <asp:CheckBox ID="chkWMM23Jointlesspolyurethanefoamedinplacegasketing" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                    </div>
                                </div>
                                <%-- 19--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkWMM23Anyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtWMM23Anyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="btnSubmitWMM23" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitWMM23_Click" />
                                    &nbsp;<asp:Button ID="btnCancelWMM23" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelWMM23_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--3 WMM-30 (MCC Box)--%>
                    <asp:Panel ID="PanelType3" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <%--1--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30WeldedMainBody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlWMM30WeldedMainBodycat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>with Top and bottom</asp:ListItem>
                                                    <asp:ListItem>with Sides</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlWMM30WeldedMainBodycat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--2--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30GlandPlat" Text="Gland Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlWMM30GlandPlatcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Bottom Side</asp:ListItem>
                                                    <asp:ListItem>Top Side</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlWMM30GlandPlatcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--3--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30Canopy" Text="Canopy" CssClass="myClass" runat="server" />

                                                <asp:DropDownList ID="ddlWMM30Canopycat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>Full size</asp:ListItem>
                                                    <asp:ListItem>Part size</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30Canopycat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--4--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30ComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />

                                                <asp:DropDownList ID="ddlWMM30ComponentMtgPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30ComponentMtgPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30ComponentMtgPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--5--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30SideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />

                                                <asp:DropDownList ID="ddlWMM30SideCPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30SideCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30SideCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--6--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30DoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />

                                                <asp:DropDownList ID="ddlWMM30DoorCPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30DoorCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30DoorCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--7--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30WallMtgBracket" Text="Wall Mtg. Bracket" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlWMM30WallMtgBracketcat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>Small</asp:ListItem>
                                                    <asp:ListItem>Big</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--8--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30FrontDoor" Text="Front Door" CssClass="myClass" runat="server" />

                                                <asp:DropDownList ID="ddlWMM30FrontDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30FrontDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--9--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30RearDoor" Text="Rear Door" CssClass="myClass" runat="server" />

                                                <asp:DropDownList ID="ddlWMM30RearDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlWMM30RearDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--10--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30DoorStiffener" Text="Door Stiffener" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--11--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30Lock" Text="Lock" CssClass="myClass" runat="server" />

                                                <asp:DropDownList ID="ddlWMM30Lockcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Thumbscrew</asp:ListItem>
                                                    <asp:ListItem>Coin Lock</asp:ListItem>
                                                    <asp:ListItem>Cam Lock</asp:ListItem>
                                                    <asp:ListItem>3-point Handle Lock</asp:ListItem>
                                                    <asp:ListItem>Mini Lock</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtWMM30Lockcat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--12--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30CableSupportAngle" Text="Cable Support Angle" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtWMM30CableSupportAnglecat1" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 13--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30PowerCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlWMM30PowerCoatingShadecat1" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>RAL-7032</asp:ListItem>
                                                    <asp:ListItem>RAL-7035</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtWMM30PowerCoatingShadecat2" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 14--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30LiftingIBolt" Text="Lifting I-Bolt" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtWMM30LiftingIBoltcat1" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 15--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30Base" Text="Base" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlWMM30Basecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Standard</asp:ListItem>
                                                    <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                    <asp:ListItem>ISMC</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlWMM30Basecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem> 3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlWMM30Basecat3" runat="server">
                                                    <asp:ListItem>Select Height</asp:ListItem>
                                                    <asp:ListItem>75 mm</asp:ListItem>
                                                    <asp:ListItem>100mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 16--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30TransparentDoor" Text="Transparent Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlWMM30TransparentDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Glass</asp:ListItem>
                                                    <asp:ListItem>Acrylic</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlWMM30TransparentDoorcat2" OnSelectedIndexChanged="ddlWMM30TransparentDoorcat2_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Door</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtWMM30TransparentDoorcat5" Visible="false" placeholder="Specify" runat="server"></asp:TextBox>
                                                <asp:DropDownList ID="ddlWMM30TransparentDoorcat3" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>With bidding</asp:ListItem>
                                                    <asp:ListItem>biddingless technique</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtWMM30TransparentDoorcat4" runat="server" placeholder="Size & Thickness"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 17--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30fan" Text="Fan" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlWMM30fancat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtWMM30fancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 18--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30Jointlesspolyurethanefoamedinplacegasketing" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%-- 19--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkWMM30Anyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtWMM30Anyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="btnSumbitWMM30" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSumbitWMM30_Click" />
                                    &nbsp;<asp:Button ID="btnCancelWMM30" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelWMM30_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--4 MFS (Modular Floor Standing Enclosure)--%>
                    <asp:Panel ID="PanelType4" runat="server" Visible="false">
                        <br />
                        <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                            <div class="col-md-12">
                                <%--1--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSMainframeStructureWelded" Text="Main frame Structure Welded" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSMainframeStructureWeldedcat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--2--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSBottomCover" Text="Bottom Cover" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSBottomCovercat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>GRCA</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMFSBottomCovercat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--3--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSGlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSGlandPlatecat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>LH</asp:ListItem>
                                            <asp:ListItem>RH</asp:ListItem>
                                            <asp:ListItem>Top</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSGlandPlatecat2" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>GRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSGlandPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--4--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSComponentMtgPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSComponentMtgPlatecat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>Orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSComponentMtgPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtMFSComponentMtgPlatecat4" runat="server" placeholder="Qty"></asp:TextBox>

                                    </div>
                                </div>
                                <%--5--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSSideCPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSSideCPlatecat2" runat="server">
                                            <asp:ListItem>Select colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSSideCPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtMFSSideCPlatecat4" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%--6--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSDoorCPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSDoorCPlatecat2" runat="server">
                                            <asp:ListItem>Select colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSDoorCPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtMFSDoorCPlatecat4" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%--7--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSPartialMountingPlate" Text="Partial Mounting Plate" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSPartialMountingPlatecat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>GRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSPartialMountingPlatecat2" runat="server">
                                            <asp:ListItem>Select colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSPartialMountingPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtMFSPartialMountingPlatecat4" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%--8--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSFillerTray" Text="Filler Tray" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSFillerTraycat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>GRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSFillerTraycat2" runat="server">
                                            <asp:ListItem>Select colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSFillerTraycat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtMFSFillerTraycat4" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%--9--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSFrontDoor" Text="Front Door" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSFrontDoorcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Single</asp:ListItem>
                                            <asp:ListItem>Split</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSFrontDoorcat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--10--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSRearDoor" Text="Rear Door" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSRearDoorcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Single</asp:ListItem>
                                            <asp:ListItem>Split</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlMFSRearDoorcat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--11--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSLock" Text="Lock" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSLockcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Thumbscrew</asp:ListItem>
                                            <asp:ListItem>Coin Lock</asp:ListItem>
                                            <asp:ListItem>Cam Lock</asp:ListItem>
                                            <asp:ListItem>3-point Handle Lock</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--12--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSRearCover" Text="Rear Cover" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSRearCovercat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--13--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSSideCover" Text="Side Cover" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSSideCovercat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--14--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSTopCover" Text="Top Cover" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSTopCovercat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--15--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSPowerCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSPowerCoatingShadecat1" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>RAL-7032</asp:ListItem>
                                            <asp:ListItem>RAL-7035</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtMFSPowerCoatingShadecat2" runat="server" placeholder="Specify"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 16--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSLiftingArrangement" Text="Lifting Arrangement" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSLiftingArrangementcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Eye Bolt</asp:ListItem>
                                            <asp:ListItem>Lifting L</asp:ListItem>
                                            <asp:ListItem>Lifting full length L</asp:ListItem>
                                            <asp:ListItem>Lifting Depth L</asp:ListItem>
                                            <asp:ListItem>Both Lifting full length width and depth L</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMFSLiftingArrangementcat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                            <asp:ListItem>5.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 17--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSBase" Text="Base" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSBasecat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Standard</asp:ListItem>
                                            <asp:ListItem>50mm Angle frame</asp:ListItem>
                                            <asp:ListItem>ISMC</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMFSBasecat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMFSBasecat3" runat="server">
                                            <asp:ListItem>Select Height</asp:ListItem>
                                            <asp:ListItem>75mm</asp:ListItem>
                                            <asp:ListItem>100mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 18--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSAntivibrationpadcat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>12.00mm</asp:ListItem>
                                            <asp:ListItem>15.00mm</asp:ListItem>
                                            <asp:ListItem>25.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 19--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSDrawingPocket" Text="Drawing Pocket" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlMFSDrawingPocketcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Flat</asp:ListItem>
                                            <asp:ListItem>Standard</asp:ListItem>
                                            <asp:ListItem>Extra Deep</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMFSDrawingPocketcat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>Orange</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 20--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">20.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSMicroswitchbracket" Text="Micro switch bracket" CssClass="myClass" runat="server" />
                                    </div>
                                </div>
                                <%-- 21--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">21.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSTubelightBracket" Text="Tubelight Bracket" CssClass="myClass" runat="server" />
                                    </div>
                                </div>
                                <%-- 22--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">22.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSCanopy" Text="Canopy" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSCanopycat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Full Size</asp:ListItem>
                                            <asp:ListItem>Part Size</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMFSCanopycat2" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Perforation Hole</asp:ListItem>
                                            <asp:ListItem>1.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                            <asp:ListItem>6.00mm</asp:ListItem>
                                            <asp:ListItem>Slotted</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMFSCanopycat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.2mm</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 23--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">23.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSfan" Text="Fan" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlMFSfancat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtMFSfancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 24--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">24.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSJointlesspolyurethanefoamedinplacegasketing" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                    </div>
                                </div>
                                <%-- 25--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">25.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkMFSAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />

                                        <asp:TextBox ID="txtMFSAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="btnSubmitMFS" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitMFS_Click" />
                                    &nbsp;<asp:Button ID="BtnCancelMFS" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="BtnCancelMFS_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--5 Eco MCC 30mm--%>
                    <asp:Panel ID="PanelType5" runat="server" Visible="false">
                        <br />
                        <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                            <div class="col-md-12">
                                <%--1--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCMainframeStructureWelded" Text="Main frame Structure Welded" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlEcoMCCMainframeStructureWeldedcat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--2--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCGlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlEcoMCCGlandPlatecat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Bottom Side</asp:ListItem>
                                            <asp:ListItem>Top Side</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEcoMCCGlandPlatecat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <%--3--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCComponentMtgPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlEcoMCCComponentMtgPlatecat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>Orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlEcoMCCComponentMtgPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtEcoMCCComponentMtgPlatecat4" runat="server" placeholder="Qty"></asp:TextBox>


                                    </div>
                                </div>
                                <%--4--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCSideCPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlEcoMCCSideCPlatecat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlEcoMCCSideCPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtEcoMCCSideCPlatecat4" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%--5--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCDoorCPlatecat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>GPSP</asp:ListItem>
                                            <asp:ListItem>CRCA</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlEcoMCCDoorCPlatecat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>orange</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlEcoMCCDoorCPlatecat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                            <asp:ListItem>3.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtEcoMCCDoorCPlatecat4" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>

                                <%--6--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCFrontDoor" Text="Front Door" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCFrontDoorca1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Single</asp:ListItem>
                                            <asp:ListItem>Split</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlEcoMCCFrontDoorca2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--7--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCRearDoor" Text="Rear Door" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCRearDoorcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Single</asp:ListItem>
                                            <asp:ListItem>Split</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlEcoMCCRearDoorcat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5mm</asp:ListItem>
                                            <asp:ListItem>2.00mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--8--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCRearCover" Text="Rear Cover" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCRearCovercat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5 mm</asp:ListItem>
                                            <asp:ListItem>2.00 mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--9--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCSideCover" Text="Side Cover" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCSideCovercat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.5 mm</asp:ListItem>
                                            <asp:ListItem>2.00 mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--10--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCLock" Text="Lock" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlEcoMCCLockcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Thumbscrew</asp:ListItem>
                                            <asp:ListItem>Coin Lock</asp:ListItem>
                                            <asp:ListItem>Cam Lock</asp:ListItem>
                                            <asp:ListItem>3-point Handle Lock</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--11--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCPowerCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlEcoMCCPowerCoatingShadecat1" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>RAL-7032</asp:ListItem>
                                            <asp:ListItem>RAL-7035</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtEcoMCCPowerCoatingShadecat2" placeholder="Specify" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- 12--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCLiftingArrangement" Text="Lifting Arrangement" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlEcoMCCLiftingArrangementcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Eye Bolt</asp:ListItem>
                                            <asp:ListItem>Lifting L</asp:ListItem>
                                            <asp:ListItem>Lifting full length L</asp:ListItem>
                                            <asp:ListItem>Lifting Depth L</asp:ListItem>
                                            <asp:ListItem>Both Lifting full length width and depth L</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEcoMCCLiftingArrangementcat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>3.00 mm</asp:ListItem>
                                            <asp:ListItem>5.00 mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 13--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCBase" Text="Base" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCBasecat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Standard</asp:ListItem>
                                            <asp:ListItem>50mm Angle frame</asp:ListItem>
                                            <asp:ListItem>ISMC</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEcoMCCBasecat2" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>2.00 mm</asp:ListItem>
                                            <asp:ListItem>3.00 mm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEcoMCCBasecat3" runat="server">
                                            <asp:ListItem>Select Height</asp:ListItem>
                                            <asp:ListItem>75 mm</asp:ListItem>
                                            <asp:ListItem>100mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 14--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCAntivibrationpadcat1" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>12.00 mm</asp:ListItem>
                                            <asp:ListItem>15.00 mm</asp:ListItem>
                                            <asp:ListItem>25.00 mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 15--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCDrawingPocket" Text="Drawing Pocket" CssClass="myClass" runat="server" />

                                        <asp:DropDownList ID="ddlEcoMCCDrawingPocketcat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Flat</asp:ListItem>
                                            <asp:ListItem>Standard</asp:ListItem>
                                            <asp:ListItem>Extra Deep</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEcoMCCDrawingPocketcat2" runat="server">
                                            <asp:ListItem>Select Colour</asp:ListItem>
                                            <asp:ListItem>7032</asp:ListItem>
                                            <asp:ListItem>7035</asp:ListItem>
                                            <asp:ListItem>Orange</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 16--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCMicroswitchbracket" Text="Micro switch bracket" CssClass="myClass" runat="server" />

                                    </div>
                                </div>
                                <%-- 17--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCTubelightBracket" Text="Tubelight Bracket" CssClass="myClass" runat="server" />
                                    </div>
                                </div>
                                <%-- 18--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCCanopy" Text="Canopy" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlEcoMCCCanopycat1" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Full Size</asp:ListItem>
                                            <asp:ListItem>Part Size</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEcoMCCCanopycat2" runat="server">
                                            <asp:ListItem>Perforation Hole</asp:ListItem>
                                            <asp:ListItem>1.00 mm</asp:ListItem>
                                            <asp:ListItem>3.00 mm</asp:ListItem>
                                            <asp:ListItem>6.00 mm</asp:ListItem>
                                            <asp:ListItem>Slotted</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEcoMCCCanopycat3" runat="server">
                                            <asp:ListItem>Select Thickness</asp:ListItem>
                                            <asp:ListItem>1.2 mm</asp:ListItem>
                                            <asp:ListItem>1.5 mm</asp:ListItem>
                                            <asp:ListItem>2.00 mm</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- 19--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCfan" Text="Fan" CssClass="myClass" runat="server" />
                                        <asp:DropDownList ID="ddlEcoMCCfancat1" runat="server">
                                            <asp:ListItem>Select Size</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtEcoMCCfancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                    </div>
                                </div>
                                <%--20--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">20.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCJointlesspolyurethanefoamedinplacegasketing" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                    </div>
                                </div>
                                <%--21--%>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="display: inline;">
                                        <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">21.&nbsp;</h5>
                                        <asp:CheckBox ID="ChkEcoMCCAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                        <asp:TextBox ID="txtEcoMCCAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="btnSubmitEcoMCC" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitEcoMCC_Click" />
                                    &nbsp;<asp:Button ID="btnCancelEcoMCC" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelEcoMCC_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--6 Modular W-Big 43 mm--%>
                    <asp:Panel ID="PanelType6" runat="server" Visible="false">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <br />
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <%--1--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularWeldedMainBody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularWeldedMainBodycat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>With Top and Bottom</asp:ListItem>
                                                    <asp:ListItem>With Sides</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlModularWeldedMainBodycat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--2--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularGlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularGlandPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Bottom Side</asp:ListItem>
                                                    <asp:ListItem>Top Side</asp:ListItem>
                                                    <asp:ListItem>LH</asp:ListItem>
                                                    <asp:ListItem>RH</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlModularGlandPlatecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--3--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularComponentMtgPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlModularComponentMtgPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>Orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlModularComponentMtgPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--4--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularSideCPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlModularSideCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Color</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlModularSideCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--5--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />

                                                <asp:DropDownList ID="ddlModularDoorCPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlModularDoorCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Color</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlModularDoorCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--6--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularWallMtgBracket" Text="Wall Mtg. Bracket" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularWallMtgBracketcat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                    <asp:ListItem>5.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--7--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularFrontDoor" Text="Front Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularFrontDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlModularFrontDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--8--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularRearDoor" Text="Rear Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularRearDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--9--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularRearCover" Text="Rear Cover" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularRearCovercat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--10--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularCableSupportAngle" Text="Cable Support Angle" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtModularCableSupportAnglecat1" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--11--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularLock" Text="Lock" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularLockcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Thumbscrew</asp:ListItem>
                                                    <asp:ListItem>Coin Lock</asp:ListItem>
                                                    <asp:ListItem>Cam Lock</asp:ListItem>
                                                    <asp:ListItem>3-point Handle Lock</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--12--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularPowerCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularPowerCoatingShadecat1" AutoPostBack="true" OnSelectedIndexChanged="ddlModularPowerCoatingShadecat1_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>RAL-7032</asp:ListItem>
                                                    <asp:ListItem>RAL-7035</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtModularPowerCoatingShadecat3" Visible="false" placeholder="Specify" runat="server"></asp:TextBox>
                                                <asp:DropDownList ID="ddlModularPowerCoatingShadecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                    <asp:ListItem>5.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 13--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularLiftingArrangement" Text="Lifting Arrangement" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularLiftingArrangementcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Eye Bolt</asp:ListItem>
                                                    <asp:ListItem>Lifting L</asp:ListItem>
                                                    <asp:ListItem>Lifting full length L</asp:ListItem>
                                                    <asp:ListItem>Lifting Depth L</asp:ListItem>
                                                    <asp:ListItem>Both Lifting full length width and depth L</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlModularLiftingArrangementcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 14--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularBase" Text="Base" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularBasecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Standard</asp:ListItem>
                                                    <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                    <asp:ListItem>ISMC</asp:ListItem>
                                                    <asp:ListItem>5.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlModularBasecat2" runat="server">
                                                    <asp:ListItem>Select Height</asp:ListItem>
                                                    <asp:ListItem>75mm</asp:ListItem>
                                                    <asp:ListItem>100mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 15--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularAntivibrationpadcat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>12.00 mm</asp:ListItem>
                                                    <asp:ListItem>15.00 mm</asp:ListItem>
                                                    <asp:ListItem>25.00 mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 16--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularCanopy" Text="Canopy" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularCanopycat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Full Size</asp:ListItem>
                                                    <asp:ListItem>Part Size</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlModularCanopycat2" runat="server">
                                                    <asp:ListItem>Perforation Hole</asp:ListItem>
                                                    <asp:ListItem>1.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                    <asp:ListItem>6.00mm</asp:ListItem>
                                                    <asp:ListItem>Slotted</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlModularCanopycat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.0mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 17--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularfan" Text="Fan" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlModularfancat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtModularfancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 18--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularJointlesspolyurethanefoamedinplacegasketing" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%-- 19--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkModularAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtModularAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="btnSubmitModular" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitModular_Click" />
                                    &nbsp;<asp:Button ID="btnCancelModular" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelModular_Click" /></center>
                            </div>
                        </div>

                    </asp:Panel>

                    <%--7 Eco Frame 43mm--%>
                    <asp:Panel ID="PanelType7" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <%--1--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameMainFrameTopBottomWeldedStructure" Text="Main Frame Top Bottom Welded Structure" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameMainFrameTopBottomWeldedStructurecat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--2--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameTopBottomGlandPlate" Text="Top/Bottom Gland Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameTopBottomGlandPlatecat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--3--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameComponentMtgPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameComponentMtgPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>Orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameComponentMtgPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--4--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameSideCPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameSideCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Color</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameSideCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--5--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameDoorCPlatecat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameDoorCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Color</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameDoorCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--6--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameFrontDoor" Text="Front Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameFrontDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameFrontDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--7--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameRearDoor" Text="Rear Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameRearDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--8--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameRearCover" Text="Rear Cover" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameRearCovercat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--9--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameSideCover" Text="Side Cover" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameSideCovercat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--10--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameLock" Text="Lock" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameLockcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Thumbscrew</asp:ListItem>
                                                    <asp:ListItem>Coin Lock</asp:ListItem>
                                                    <asp:ListItem>Cam Lock</asp:ListItem>
                                                    <asp:ListItem>3-point Handle Lock</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--11--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFramePowerCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFramePowerCoatingShadecat1" AutoPostBack="true" OnSelectedIndexChanged="ddlEcoFramePowerCoatingShadecat1_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>RAL-7032</asp:ListItem>
                                                    <asp:ListItem>RAL-7035</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtddlEcoFramePowerCoatingShadecat2" Visible="false" placeholder="Specify" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 12--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameLiftingArrangement" Text="Lifting Arrangement" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameLiftingArrangementcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Eye Bolt</asp:ListItem>
                                                    <asp:ListItem>Lifting L</asp:ListItem>
                                                    <asp:ListItem>Lifting full length L</asp:ListItem>
                                                    <asp:ListItem>Lifting Depth L</asp:ListItem>
                                                    <asp:ListItem>Both Lifting full length width and depth L</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameLiftingArrangementcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 13--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameBase" Text="Base" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameBasecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Standard</asp:ListItem>
                                                    <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                    <asp:ListItem>ISMC</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameBasecat2" runat="server">
                                                    <asp:ListItem>Select Height</asp:ListItem>
                                                    <asp:ListItem>75mm</asp:ListItem>
                                                    <asp:ListItem>100mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 14--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameCanopy" Text="Canopy" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFrameCanopycat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Full Size</asp:ListItem>
                                                    <asp:ListItem>Part Size</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameCanopycat2" runat="server">
                                                    <asp:ListItem>Perforation Hole</asp:ListItem>
                                                    <asp:ListItem>1.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                    <asp:ListItem>6.00mm</asp:ListItem>
                                                    <asp:ListItem>Slotted</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEcoFrameCanopycat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 15--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFramefan" Text="Fan" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlEcoFramefancat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtEcoFramefancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 16--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameJointlesspolyurethanefoamedinplacegasketing" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%-- 17--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkEcoFrameAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtEcoFrameAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="btnEcoFrame" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnEcoFrame_Click" />
                                    &nbsp;<asp:Button ID="btnCancelEcoFrame" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelEcoFrame_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--8 PC ENCLOSURE--%>
                    <asp:Panel ID="PanelType8" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <asp:RadioButtonList ID="rdlpcenclosure" RepeatDirection="Horizontal" Font-Bold="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdlpcenclosure_SelectedIndexChanged">
                                                    <asp:ListItem Enabled="true" style="margin-right: 5px;" Text="Shop Floor PC Enclosure Standing" Value="Shop Floor PC Enclosure Standing"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" style="margin-right: 5px;" Text="PC Enclosure ECO-Standing" Value="PC Enclosure ECO-Standing"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" style="margin-right: 5px;" Text="PC Enclosure ECO-Sitting" Value="PC Enclosure ECO-Sitting"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>

                                        <%--Shop Floor PC Enclosure Standing--%>
                                        <asp:Panel ID="Panel81" runat="server" Visible="false">
                                            <br />
                                            <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                                <div class="col-md-12">
                                                    <%--1--%>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingMainframestructure" Text="Main Frame Structure Welded" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingMainframestructurecat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--2--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingBottomCover" Text="Bottom Cover" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingBottomCovercat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>GPSP</asp:ListItem>
                                                                <asp:ListItem>CRCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingBottomCovercat2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--3--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingComponentMtgPlate" Text="Component Mtg Plate" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingComponentMtgPlatecat1" placeholder="Height" runat="server"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingComponentMtgPlatecat2" placeholder="Qty" runat="server"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat3" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>GPSP</asp:ListItem>
                                                                <asp:ListItem>CRCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat4" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat5" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:Button ID="btnAddStandingComponentMtgPlate1" runat="server" Text="+Add" OnClick="btnAddStandingComponentMtgPlate1_Click" />
                                                        </div>
                                                    </div>

                                                    <%--32--%>
                                                    <div class="row" id="componetmtgplat2" runat="server" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="Label1" runat="server" Text="Component Mtg Plate"></asp:Label>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingComponentMtgPlatecat21" placeholder="Height" runat="server"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingComponentMtgPlatecat22" placeholder="Qty" runat="server"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat23" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>GPSP</asp:ListItem>
                                                                <asp:ListItem>CRCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat24" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat25" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:Button ID="AddStandingComponentMtgPlate2" runat="server" Text="+Add" OnClick="AddStandingComponentMtgPlate2_Click" />
                                                        </div>
                                                    </div>

                                                    <%--33--%>
                                                    <div class="row" id="componetmtgplat3" runat="server" style="margin-top: 5px;" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label2" runat="server" Text="Component Mtg Plate"></asp:Label>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingComponentMtgPlatecat31" placeholder="Height" runat="server"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingComponentMtgPlatecat32" placeholder="Qty" runat="server"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat33" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>GPSP</asp:ListItem>
                                                                <asp:ListItem>CRCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat34" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat35" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:Button ID="AddStandingComponentMtgPlate3" runat="server" Text="+Add" OnClick="AddStandingComponentMtgPlate3_Click" />
                                                        </div>
                                                    </div>

                                                    <%--34--%>
                                                    <div class="row" id="componetmtgplat4" runat="server" style="margin-top: 5px;" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label3" runat="server" Text="Component Mtg Plate"></asp:Label>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingComponentMtgPlatecat41" placeholder="Height" runat="server"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingComponentMtgPlatecat42" placeholder="Qty" runat="server"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat43" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>GPSP</asp:ListItem>
                                                                <asp:ListItem>CRCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat44" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingComponentMtgPlatecat45" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--4--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingSidecPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat2" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat3" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Button ID="btnPCEncShopFloorStandingSidecPlate1" runat="server" Text="+Add" OnClick="btnPCEncShopFloorStandingSidecPlate1_Click" />
                                                        </div>
                                                    </div>
                                                    <%-- 42--%>
                                                    <div class="row" id="SidecPlate2" runat="server" style="margin-top: 5px;" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label4" runat="server" Text="Side C Plate"></asp:Label>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat21" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat22" runat="server" AutoPostBack="true">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat23" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:Button ID="btnPCEncShopFloorStandingSidecPlate2" runat="server" Text="+Add" OnClick="btnPCEncShopFloorStandingSidecPlate2_Click" />
                                                        </div>
                                                    </div>

                                                    <%-- 43--%>
                                                    <div class="row" id="SidecPlate3" runat="server" style="margin-top: 5px;" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label5" runat="server" Text="Side C Plate"></asp:Label>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat31" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat32" runat="server" AutoPostBack="true">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat33" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:Button ID="btnPCEncShopFloorStandingSidecPlate3" runat="server" Text="+Add" OnClick="btnPCEncShopFloorStandingSidecPlate3_Click" />
                                                        </div>
                                                    </div>

                                                    <%-- 44--%>
                                                    <div class="row" id="SidecPlate4" runat="server" style="margin-top: 5px;" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label6" runat="server" Text="Side C Plate"></asp:Label>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat41" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat42" runat="server" AutoPostBack="true">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSidecPlatecat43" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>

                                                    <%--5--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat2" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat3" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Button ID="btnPCEncShopFloorStandingDoorCPlate1" runat="server" Text="+Add" OnClick="btnPCEncShopFloorStandingDoorCPlate1_Click" />
                                                        </div>
                                                    </div>

                                                    <%-- 52--%>
                                                    <div class="row" id="DoorcPlate2" runat="server" style="margin-top: 5px;" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label7" runat="server" Text="Side C Plate"></asp:Label>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat21" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat22" runat="server" AutoPostBack="true">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat23" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:Button ID="btnPCEncShopFloorStandingDoorCPlate2" runat="server" Text="+Add" OnClick="btnPCEncShopFloorStandingDoorCPlate2_Click" />
                                                        </div>
                                                    </div>

                                                    <%-- 53--%>
                                                    <div class="row" id="DoorcPlate3" runat="server" style="margin-top: 5px;" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label8" runat="server" Text="Side C Plate"></asp:Label>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat31" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat32" runat="server" AutoPostBack="true">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat33" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:Button ID="btnPCEncShopFloorStandingDoorCPlate3" runat="server" Text="+Add" OnClick="btnPCEncShopFloorStandingDoorCPlate3_Click" />
                                                        </div>
                                                    </div>

                                                    <%-- 54--%>
                                                    <div class="row" id="DoorcPlate4" runat="server" style="margin-top: 5px;" visible="false">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">&nbsp;</h5>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label9" runat="server" Text="Side C Plate"></asp:Label>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat41" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat42" runat="server" AutoPostBack="true">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingDoorCPlatecat43" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>

                                                    <%--6--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingFrontDoor" Text="Front Door" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingFrontDoorcat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Single</asp:ListItem>
                                                                <asp:ListItem>Split</asp:ListItem>
                                                                <asp:ListItem>Partial</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingFrontDoorcat2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--7--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingRearDoor" Text="Rear Door" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingRearDoorcat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Single</asp:ListItem>
                                                                <asp:ListItem>Split</asp:ListItem>
                                                                <asp:ListItem>Partial</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingRearDoorcat2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--8--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingLock" Text="Lock" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingLockcat1" runat="server">
                                                                <asp:ListItem>Cam Lock</asp:ListItem>
                                                                <asp:ListItem>Mini lock</asp:ListItem>
                                                                <asp:ListItem>3-point Handle Lock</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingLockcat2" Placeholder="Qty" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--9--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingRearCover" Text="Rear Cover" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingRearCovercat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--10--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingSideCover" Text="Side Cover" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSideCovercat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--11--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingTopCover" Text="Top Cover" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingTopCovercat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--12--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingHorizontalPartition" Text="Horizontal Partition" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingHorizontalPartitioncat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingHorizontalPartitioncat2" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--13--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingSlidingKeyboarddrawer" Text="Sliding Keyboard drawer with Telescopic Rails" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSlidingKeyboarddrawercat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingSlidingKeyboarddrawercat2" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--14--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingPowderCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingPowderCoatingShadecat1" runat="server" OnSelectedIndexChanged="ddlPCEncShopFloorStandingPowderCoatingShadecat1_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>RAL-7032</asp:ListItem>
                                                                <asp:ListItem>RAL-7035</asp:ListItem>
                                                                <asp:ListItem>Specify</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingPowderCoatingShadecat2" runat="server" Visible="false" placeholder="Specify"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--15--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingLiftingArrangement" Text="Lifting Arrangement" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingLiftingArrangementcat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Eye Bolt</asp:ListItem>
                                                                <asp:ListItem>Lifting L</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingLiftingArrangementcat2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--16--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingBase" Text="Base" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingBasecat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Standard</asp:ListItem>
                                                                <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingBasecat2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtddlPCEncShopFloorStandingBasecat3" runat="server" placeholder="Height"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--17--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingAntivibrationpadcat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>12.00mm</asp:ListItem>
                                                                <asp:ListItem>15.00mm</asp:ListItem>
                                                                <asp:ListItem>25.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--18--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingAntivibrationCasterWheel" Text="Caster Wheel" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingAntivibrationCasterWheelcat1" runat="server" placeholder="Size"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingAntivibrationCasterWheelcat2" runat="server" placeholder="Revolving Qty"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingAntivibrationCasterWheelcat3" runat="server" placeholder="Fixed Qty"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingAntivibrationCasterWheelcat4" runat="server" placeholder="Specify"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--19--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingDrawingPocket" Text="Drawing Pocket" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlChkPCEncShopFloorStandingDrawingPocketcat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Flat</asp:ListItem>
                                                                <asp:ListItem>Standard</asp:ListItem>
                                                                <asp:ListItem>Extra Deep</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlChkPCEncShopFloorStandingDrawingPocketcat2" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--20--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">20.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingMicroswitchbracket" Text="Micro switch bracket" CssClass="myClass" runat="server" />
                                                        </div>
                                                    </div>
                                                    <%--21--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">21.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingTubelightBracket" Text="Tubelight Bracket" CssClass="myClass" runat="server" />
                                                        </div>
                                                    </div>
                                                    <%--22--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">22.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingTransparentDoor" Text="Transparent Door" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingTransparentDoorcat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingTransparentDoorcat2" AutoPostBack="true" OnSelectedIndexChanged="ddlPCEncShopFloorStandingTransparentDoorcat2_SelectedIndexChanged" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Door</asp:ListItem>
                                                                <asp:ListItem>Specify</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingTransparentDoorcat3" Visible="false" runat="server" placeholder="Specify"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingTransparentDoorcat4" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>With bidding</asp:ListItem>
                                                                <asp:ListItem>Biddingless Technique</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--23--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">23.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingFilters" Text="Filters" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingFilterscat1" runat="server">
                                                                <asp:ListItem>Select Inches</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                                <asp:ListItem>8</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--24--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">24.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingGasspring" Text="Gas spring for Monitor door" CssClass="myClass" runat="server" />
                                                        </div>
                                                    </div>
                                                    <%--25--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">25.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingAluminiumExtrusion" Text="Aluminium Extrusion For Monitor door" CssClass="myClass" runat="server" />
                                                        </div>
                                                    </div>
                                                    <%--26--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">26.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingJointlesspolyurethane" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                                        </div>
                                                    </div>
                                                    <%--27--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">27.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingfan" Text="Fan" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEncShopFloorStandingfancat1" runat="server">
                                                                <asp:ListItem>Select Size</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtChkPCEncShopFloorStandingfancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--28--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">28.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEncShopFloorStandingAnyadditional" Text="Any additional component" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEncShopFloorStandingAnyadditionalcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <%--PC Enclosure ECO-Standing AND PC Enclosure ECO-Sitting--%>
                                        <asp:Panel ID="Panel82" runat="server" Visible="false">
                                            <br />
                                            <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                                <div class="col-md-12">
                                                    <%--1--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingWeldedMainBody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingWeldedMainBodycat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>with Top and bottom</asp:ListItem>
                                                                <asp:ListItem>with Sides</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingWeldedMainBodycat2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--2--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingGlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingGlandPlatecat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Bottom</asp:ListItem>
                                                                <asp:ListItem>LHS</asp:ListItem>
                                                                <asp:ListItem>RHS</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingGlandPlatecat2" runat="server" placeholder="size:"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingGlandPlatecat3" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--3--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingComponentMtgPlate" Text="Component Mtg Plate" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingComponentMtgPlatecat1" runat="server" placeholder="size:"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingComponentMtgPlatecat2" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>GPSP</asp:ListItem>
                                                                <asp:ListItem>CRCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingComponentMtgPlatecat3" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>No colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingComponentMtgPlatecat4" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--4--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingSideCPlatecat1" runat="server" placeholder="size:"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingSideCPlatecat2" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>GPSP</asp:ListItem>
                                                                <asp:ListItem>CRCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingSideCPlatecat3" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingSideCPlatecat4" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--5--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingDoorCPlatecat1" runat="server" placeholder="size:"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingDoorCPlatecat2" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>GPSP</asp:ListItem>
                                                                <asp:ListItem>CRCA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingDoorCPlatecat3" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                                <asp:ListItem>Orange</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingDoorCPlatecat4" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--6--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingFrontDoorwithstiffeners" Text="Front Door with stiffeners" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingFrontDoorwithstiffenerscat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Single</asp:ListItem>
                                                                <asp:ListItem>Split</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingFrontDoorwithstiffenerscat2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--7--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingCableSupportAngle" Text="Cable Support Angle" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingCableSupportAnglecat1" runat="server" placeholder="Qty"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--8--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingLock" Text="Lock" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingLockcat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Cam Lock</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingLockcat2" runat="server" placeholder="Qty"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--9--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingHorizontalPartition" Text="Horizontal Partition" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingHorizontalPartitioncat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingHorizontalPartitioncat2" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%--10--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingSlidingKeyboarddrawer" Text="Sliding Keyboard drawer with telescopic rails" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingSlidingKeyboarddrawercat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>1.2mm</asp:ListItem>
                                                                <asp:ListItem>1.5mm</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingSlidingKeyboarddrawercat2" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>7032</asp:ListItem>
                                                                <asp:ListItem>7035</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--11--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingLiftingIBolt" Text="Lifting I-Bolt" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingLiftingIBoltcat1" runat="server" placeholder="Qty"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <%--12--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingBase" Text="Base" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingBasecat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Standard</asp:ListItem>
                                                                <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                                <asp:ListItem>ISMC</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingBasecat2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>2.00mm</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingBasecat3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPCEnclosureECOStandingBasecat3_SelectedIndexChanged">
                                                                <asp:ListItem>Select Height</asp:ListItem>
                                                                <asp:ListItem>75mm</asp:ListItem>
                                                                <asp:ListItem>100mm</asp:ListItem>
                                                                <asp:ListItem>Specify</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingBasecat4" Visible="false" runat="server" placeholder="Specify Height"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--13--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingAntivibrationpadcat1" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>12mm</asp:ListItem>
                                                                <asp:ListItem>15mm</asp:ListItem>
                                                                <asp:ListItem>25mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--14--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingTransparentDoor" Text="Transparent Door" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingTransparentDoorcat1" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Glass</asp:ListItem>
                                                                <asp:ListItem>Acrylic</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingTransparentDoor2" runat="server">
                                                                <asp:ListItem>Select Thickness</asp:ListItem>
                                                                <asp:ListItem>3.00mm</asp:ListItem>
                                                                <asp:ListItem>4.00mm</asp:ListItem>
                                                                <asp:ListItem>5.00mm</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingTransparentDoor3" runat="server">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>With bidding</asp:ListItem>
                                                                <asp:ListItem>biddingless technique</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <%--15--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingCasterWheel" Text="Caster Wheel" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingCasterWheelcat1" runat="server" placeholder="Size:"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingCasterWheelcat2" runat="server" placeholder="Revolving Qty:"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingCasterWheelcat3" runat="server" placeholder="Fixed Qty:"></asp:TextBox>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingCasterWheelcat4" runat="server" placeholder="Specify"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <%--16--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingFilters" Text="Filters" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingFilterscat1" runat="server">
                                                                <asp:ListItem>Select Inch</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                                <asp:ListItem>8</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingFilterscat2" runat="server" placeholder="Qty"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <%--17--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingTelescopicRail" Text="Telescopic Rail" CssClass="myClass" runat="server" />
                                                        </div>
                                                    </div>

                                                    <%--18--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingJointlesspolyurethane" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                                        </div>
                                                    </div>

                                                    <%--19--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingPowderCoating" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingPowderCoatingcat1" AutoPostBack="true" OnSelectedIndexChanged="ddlPCEnclosureECOStandingPowderCoatingcat1_SelectedIndexChanged" runat="server">
                                                                <asp:ListItem>Select Colour</asp:ListItem>
                                                                <asp:ListItem>RAL-7032</asp:ListItem>
                                                                <asp:ListItem>RAL-7035</asp:ListItem>
                                                                <asp:ListItem>Specify</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingPowderCoatingcat2" runat="server" Visible="false" placeholder="Specify colour"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--20--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">20.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingfan" Text="Fan" CssClass="myClass" runat="server" />
                                                            <asp:DropDownList ID="ddlPCEnclosureECOStandingfancat1" runat="server">
                                                                <asp:ListItem>Select Size</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingfancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--21--%>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12" style="display: inline;">
                                                            <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">21.&nbsp;</h5>
                                                            <asp:CheckBox ID="ChkPCEnclosureECOStandingAnyadditional" Text="Any additional component" CssClass="myClass" runat="server" />
                                                            <asp:TextBox ID="txtPCEnclosureECOStandingAnyadditionalcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </asp:Panel>
                                    </div>
                                </div>
                                <br />

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="rdlpcenclosure" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="btnSubmitPCEncShopFloorStanding" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitPCEncShopFloorStanding_Click" />
                                    &nbsp;<asp:Button ID="btnCancelPCEncShopFloorStanding" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelPCEncShopFloorStanding_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--9 PC TABLE--%>
                    <asp:Panel ID="PanelType9" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <%--1--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableWeldedMainbody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableWeldedMainbodycat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>with Top and bottom</asp:ListItem>
                                                    <asp:ListItem>with Sides</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableWeldedMainbodycat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--2--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableGlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableGlandPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Bottom Side</asp:ListItem>
                                                    <asp:ListItem>GRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableGlandPlatecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--3--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableComponentMtgPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPcTableComponentMtgPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>No colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>Orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPcTableComponentMtgPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--4--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableSideCPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPcTableSideCPlatecat2" runat="server">
                                                    <asp:ListItem>Select colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPcTableSideCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--5--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableDoorCPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPcTableDoorCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPcTableDoorCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--6--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableFrontDoor" Text="Front Door with stiffeners" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableFrontDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableFrontDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--7--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableRearDoor" Text="Rear Door with stiffeners" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableRearDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableRearDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--8--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableLock" Text="Lock" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableLockcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Cam Lock</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPcTableLockcat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--9--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableCableSupportAngle" Text="Cable Support Angle" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtPcTableCableSupportAnglecat1" runat="server" placeholder="Qty"></asp:TextBox>

                                            </div>
                                        </div>
                                        <%--10--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableHorizontalPartition" Text="Horizontal Partition" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableHorizontalPartitioncat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableHorizontalPartitioncat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--11--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableSlidingKeyboarddrawer" Text="Sliding Keyboard drawer with telescopic rails" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableSlidingKeyboarddrawercat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableSlidingKeyboarddrawercat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--12--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableSlidingCPUdrawer" Text="Sliding CPU drawer with telescopic rails" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableSlidingCPUdrawercat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableSlidingCPUdrawercat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--13--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableMonitomountingbracket" Text="Monitor mounting bracket with tilt adjustment" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableMonitomountingbracketcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>double</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableMonitomountingbracketcat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--14--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableLiftingIBolt" Text="Lifting I-Bolt" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtPcTableLiftingIBoltcat1" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 15--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableBase" Text="Base" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableBasecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Standard</asp:ListItem>
                                                    <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                    <asp:ListItem>ISMC</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableBasecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableBasecat3" runat="server">
                                                    <asp:ListItem>Select Height</asp:ListItem>
                                                    <asp:ListItem>75mm</asp:ListItem>
                                                    <asp:ListItem>100mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 16--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableAntivibrationpadcat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>12mm</asp:ListItem>
                                                    <asp:ListItem>15mm</asp:ListItem>
                                                    <asp:ListItem>25mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 17--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableTransparentDoor" Text="Transparent Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableTransparentDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Glass</asp:ListItem>
                                                    <asp:ListItem>Acrylic</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableTransparentDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                    <asp:ListItem>4.00mm</asp:ListItem>
                                                    <asp:ListItem>5.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPcTableTransparentDoorcat3" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>With bidding</asp:ListItem>
                                                    <asp:ListItem>biddingless technique</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 18--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableCasterWheel" Text="Caster Wheel" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtPcTableCasterWheelcat1" runat="server" placeholder="Size:"></asp:TextBox>
                                                <asp:TextBox ID="txtPcTableCasterWheelcat2" runat="server" placeholder="Revolving Qty:"></asp:TextBox>
                                                <asp:TextBox ID="txtPcTableCasterWheelcat3" runat="server" placeholder="Fixed Qty:"></asp:TextBox>
                                                <asp:TextBox ID="txtPcTableCasterWheelcat4" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--19--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableFilters" Text="Filters" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTableFilterscat1" runat="server">
                                                    <asp:ListItem>Select Inch</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPcTableFilterscat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 20--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">20.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTablePowderCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTablePowderCoatingShadecat1" AutoPostBack="true" OnSelectedIndexChanged="ddlPcTablePowderCoatingShadecat1_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>RAL-7032</asp:ListItem>
                                                    <asp:ListItem>RAL-7035</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPcTablePowderCoatingShadecat2" runat="server" Visible="false" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 21--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">21.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTablefan" Text="Fan" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPcTablefancat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPcTablefancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 22--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">22.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableJointlesspolyurethanefoamed" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%-- 23--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">23.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPcTableAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtPcTableAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="Button1" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitMFS_Click" />
                                    &nbsp;<asp:Button ID="Button2" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="BtnCancelMFS_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--10 PRINTER TABLE--%>
                    <asp:Panel ID="PanelType10" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <%--1--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableWeldedMainBody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableWeldedMainBodycat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>with Top and bottom</asp:ListItem>
                                                    <asp:ListItem>with Sides</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableWeldedMainBodycat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--2--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterGlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterGlandPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Bottom Side</asp:ListItem>
                                                    <asp:ListItem>GRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterGlandPlatecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--3--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableComponentMtgPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPrinterTableComponentMtgPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>No colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>Orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPrinterTableComponentMtgPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--4--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableSideCPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPrinterTableSideCPlatecat2" runat="server">
                                                    <asp:ListItem>Select colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlPrinterTableSideCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--5--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableDoorCPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableDoorCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableDoorCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--6--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableFrontDoor" Text="Front Door with stiffeners" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableFrontDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableFrontDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--7--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableRearDoor" Text="Rear Door with stiffeners" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableRearDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableRearDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--8--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableDoorStiffener" Text="Door Stiffener" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--9--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableLock" Text="Lock" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableLockcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Cam Lock</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPrinterTableLockcat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--10--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableCableSupportAngle" Text="Cable Support Angle" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtPrinterTableCableSupportAnglecat1" runat="server" placeholder="Qty"></asp:TextBox>

                                            </div>
                                        </div>
                                        <%--11--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableHorizontalPartition" Text="Horizontal Partition" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableHorizontalPartitioncat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableHorizontalPartitioncat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--12--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableSlidingdrawer" Text="Sliding drawer with telescopic rails" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableSlidingdrawercat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableSlidingdrawercat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--13--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableLiftingIBolt" Text="Lifting I-Bolt" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtPrinterTableLiftingIBoltcat1" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 14--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableBase" Text="Base" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableBasecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Standard</asp:ListItem>
                                                    <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                    <asp:ListItem>ISMC</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableBasecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableBasecat3" runat="server">
                                                    <asp:ListItem>Select Height</asp:ListItem>
                                                    <asp:ListItem>75mm</asp:ListItem>
                                                    <asp:ListItem>100mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 15--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableAntivibrationpadcat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>12mm</asp:ListItem>
                                                    <asp:ListItem>15mm</asp:ListItem>
                                                    <asp:ListItem>25mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 16--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableTransparentDoor" Text="Transparent Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableTransparentDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Glass</asp:ListItem>
                                                    <asp:ListItem>Acrylic</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableTransparentDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                    <asp:ListItem>4.00mm</asp:ListItem>
                                                    <asp:ListItem>5.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPrinterTableTransparentDoorcat3" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>With bidding</asp:ListItem>
                                                    <asp:ListItem>biddingless technique</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 17--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableCasterWheel" Text="Caster Wheel" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtPrinterTableCasterWheelcat1" runat="server" placeholder="Size:"></asp:TextBox>
                                                <asp:TextBox ID="txtPrinterTableCasterWheelcat2" runat="server" placeholder="Revolving Qty:"></asp:TextBox>
                                                <asp:TextBox ID="txtPrinterTableCasterWheelcat3" runat="server" placeholder="Fixed Qty:"></asp:TextBox>
                                                <asp:TextBox ID="txtPrinterTableCasterWheelcat4" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 18--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableFilters" Text="Filters" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTableFilterscat1" runat="server">
                                                    <asp:ListItem>Select Inch</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPrinterTableFilterscat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--19--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTablePowderCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTablePowderCoatingShadecat1" AutoPostBack="true" OnSelectedIndexChanged="ddlPrinterTablePowderCoatingShadecat1_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>RAL-7032</asp:ListItem>
                                                    <asp:ListItem>RAL-7035</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPrinterTablePowderCoatingShadecat2" runat="server" Visible="false" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 20--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">23.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTablefan" Text="Fan" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlPrinterTablefancat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPrinterTablefancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 21--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">20.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableJointlesspolyurethanefoamed" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--22--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">21.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkPrinterTableAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtPrinterTableAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="Button3" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitMFS_Click" />
                                    &nbsp;<asp:Button ID="Button4" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="BtnCancelMFS_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--11 Single Piece Desk--%>
                    <asp:Panel ID="PanelType11" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <%--1--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceWeldedMainBody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceWeldedMainBodycat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--2--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceGlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceGlandPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Bottom Side</asp:ListItem>
                                                    <asp:ListItem>LH</asp:ListItem>
                                                    <asp:ListItem>RH</asp:ListItem>
                                                    <asp:ListItem>LH/RH</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceGlandPlatecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--3--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceComponentMtgPlate" Text="Component Mtg. Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceComponentMtgPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceComponentMtgPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>No colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>Orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceComponentMtgPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--4--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceSideCPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceSideCPlatecat2" runat="server">
                                                    <asp:ListItem>Select colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceSideCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--5--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceDoorCPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceDoorCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceDoorCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--6--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceFrontDoor" Text="Front Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceFrontDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceFrontDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--7--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceRearDoor" Text="Rear Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceRearDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceRearDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--8--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceDoorStiffener" Text="Door Stiffener" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--9--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceLock" Text="Lock" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceLockcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Cam Lock</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtSinglePieceLockcat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--10--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceCableSupportAngle" Text="Cable Support Angle" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtSinglePieceCableSupportAnglecat1" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--11--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceSlidingKeyboarddrawertelescopicrails" Text="Sliding Keyboard drawer with telescopic rails" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceSlidingKeyboarddrawertelescopicrailscat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceSlidingKeyboarddrawertelescopicrailscat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--12--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceSlidingdrawerwithtelescopicrails" Text="Sliding drawer with telescopic rails" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceSlidingdrawerwithtelescopicrailscat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceSlidingdrawerwithtelescopicrailscat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--13--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">13.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceMonitormountingarrangement" Text="Monitor mounting arrangement" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceMonitormountingarrangementcat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceMonitormountingarrangementcat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 14--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceBase" Text="Base" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceBasecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Standard</asp:ListItem>
                                                    <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                    <asp:ListItem>ISMC</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceBasecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceBasecat3" runat="server">
                                                    <asp:ListItem>Select Height</asp:ListItem>
                                                    <asp:ListItem>75mm</asp:ListItem>
                                                    <asp:ListItem>100mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 15--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceAntivibrationpadcat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>12mm</asp:ListItem>
                                                    <asp:ListItem>15mm</asp:ListItem>
                                                    <asp:ListItem>25mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 16--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceTransparentDoor" Text="Transparent Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceTransparentDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Glass</asp:ListItem>
                                                    <asp:ListItem>Acrylic</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceTransparentDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                    <asp:ListItem>4.00mm</asp:ListItem>
                                                    <asp:ListItem>5.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSinglePieceTransparentDoorcat3" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>With bidding</asp:ListItem>
                                                    <asp:ListItem>biddingless technique</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 17--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceCasterWheel" Text="Caster Wheel" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtSinglePieceCasterWheelcat1" runat="server" placeholder="Size:"></asp:TextBox>
                                                <asp:TextBox ID="txtSinglePieceCasterWheelcat2" runat="server" placeholder="Revolving Qty:"></asp:TextBox>
                                                <asp:TextBox ID="txtSinglePieceCasterWheelcat3" runat="server" placeholder="Fixed Qty:"></asp:TextBox>
                                                <asp:TextBox ID="txtSinglePieceCasterWheelcat4" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 18--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceFilters" Text="Filters" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePieceFilterscat1" runat="server">
                                                    <asp:ListItem>Select Inch</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtSinglePieceFilterscat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 19--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePiecefan" Text="Fan" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePiecefancat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtSinglePiecefancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--20--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">20.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePiecePowderCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlSinglePiecePowderCoatingShadecat1" AutoPostBack="true" OnSelectedIndexChanged="ddlSinglePiecePowderCoatingShadecat1_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>RAL-7032</asp:ListItem>
                                                    <asp:ListItem>RAL-7035</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtSinglePiecePowderCoatingShadecat2" runat="server" Visible="false" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 21--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">21.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceJointlesspolyurethanefoamed" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--22--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">22.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkSinglePieceAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtSinglePieceAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="Button5" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitMFS_Click" />
                                    &nbsp;<asp:Button ID="Button6" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="BtnCancelMFS_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--12 Three Piece Desk--%>
                    <asp:Panel ID="PanelType12" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <div class="row" style="margin-right: 10px!important; margin-left: 10px; padding: 3px; border: 1px solid #ccc; border-radius: 3px;">
                                    <div class="col-md-12">
                                        <%--1--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">1.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceWeldedMainBody" Text="Welded Main Body" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceWeldedMainBodycat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--2--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">2.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceGlandPlate" Text="Gland Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceGlandPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Bottom Side</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceGlandPlatecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--3--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">3.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceComponentMtgPlate" Text="Component Mtg.Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceComponentMtgPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceComponentMtgPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>No colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>Orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceComponentMtgPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--4--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">4.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceSideCPlate" Text="Side C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceSideCPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlThreePieceSideCPlatecat2" runat="server">
                                                    <asp:ListItem>Select colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlThreePieceSideCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--5--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">5.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceDoorCPlate" Text="Door C Plate" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceDoorCPlatecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>GPSP</asp:ListItem>
                                                    <asp:ListItem>CRCA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceDoorCPlatecat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                    <asp:ListItem>orange</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceDoorCPlatecat3" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--6--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">6.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceFrontDoor" Text="Front Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceFrontDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceFrontDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--7--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">7.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceRearDoor" Text="Rear Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceRearDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Split</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceRearDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.2mm</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--8--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">8.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceDoorStiffener" Text="Door Stiffener" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--9--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">9.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceLock" Text="Lock" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceLockcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Cam Lock</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtThreePieceLockcat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--10--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">10.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceCableSupportAngle" Text="Cable Support Angle" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtThreePieceCableSupportAnglecat1" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--11--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">11.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceHorizontalPartition" Text="Horizontal Partition" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceHorizontalPartitioncat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceHorizontalPartitioncat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--12--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceSlidingdrawerwithtelescopic" Text="Sliding drawer with telescopic rails" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceSlidingdrawerwithtelescopiccat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>1.5mm</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceSlidingdrawerwithtelescopiccat2" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>7032</asp:ListItem>
                                                    <asp:ListItem>7035</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--13--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">12.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceLiftingIBolt" Text="Lifting I-Bolt" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtThreePieceLiftingIBoltcat1" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 14--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">14.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceBase" Text="Base" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceBasecat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Standard</asp:ListItem>
                                                    <asp:ListItem>50mm Angle frame</asp:ListItem>
                                                    <asp:ListItem>ISMC</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceBasecat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>2.00mm</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceBasecat3" runat="server">
                                                    <asp:ListItem>Select Height</asp:ListItem>
                                                    <asp:ListItem>75mm</asp:ListItem>
                                                    <asp:ListItem>100mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 15--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">15.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceAntivibrationpad" Text="Anti-vibration pad" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceAntivibrationpadcat1" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>12mm</asp:ListItem>
                                                    <asp:ListItem>15mm</asp:ListItem>
                                                    <asp:ListItem>25mm</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 16--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">16.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceTransparentDoor" Text="Transparent Door" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceTransparentDoorcat1" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Glass</asp:ListItem>
                                                    <asp:ListItem>Acrylic</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceTransparentDoorcat2" runat="server">
                                                    <asp:ListItem>Select Thickness</asp:ListItem>
                                                    <asp:ListItem>3.00mm</asp:ListItem>
                                                    <asp:ListItem>4.00mm</asp:ListItem>
                                                    <asp:ListItem>5.00mm</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlThreePieceTransparentDoorcat3" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>With bidding</asp:ListItem>
                                                    <asp:ListItem>biddingless technique</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- 17--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">17.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceCasterWheel" Text="Caster Wheel" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtThreePieceCasterWheelcat1" runat="server" placeholder="Size:"></asp:TextBox>
                                                <asp:TextBox ID="txtThreePieceCasterWheelcat2" runat="server" placeholder="Revolving Qty:"></asp:TextBox>
                                                <asp:TextBox ID="txtThreePieceCasterWheelcat3" runat="server" placeholder="Fixed Qty:"></asp:TextBox>
                                                <asp:TextBox ID="txtThreePieceCasterWheelcat4" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 18--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">18.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceFilters" Text="Filters" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePieceFilterscat1" runat="server">
                                                    <asp:ListItem>Select Inch</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtThreePieceFilterscat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- 19--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">19.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePiecefan" Text="Fan" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePiecefancat1" runat="server">
                                                    <asp:ListItem>Select Size</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtThreePiecefancat2" runat="server" placeholder="Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--20--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">20.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePiecePowderCoatingShade" Text="Powder Coating Shade" CssClass="myClass" runat="server" />
                                                <asp:DropDownList ID="ddlThreePiecePowderCoatingShadecat1" AutoPostBack="true" OnSelectedIndexChanged="ddlThreePiecePowderCoatingShadecat1_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem>Select Colour</asp:ListItem>
                                                    <asp:ListItem>RAL-7032</asp:ListItem>
                                                    <asp:ListItem>RAL-7035</asp:ListItem>
                                                    <asp:ListItem>Specify</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtThreePiecePowderCoatingShadecat2" runat="server" Visible="false" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                        <%-- 21--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">21.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceJointlesspolyurethanefoamed" Text="Jointless polyurethane foamed in place gasketing" CssClass="myClass" runat="server" />
                                            </div>
                                        </div>
                                        <%--22--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="display: inline;">
                                                <h5 style="font-size: 14px; font-weight: 700; color: black; display: inline;">22.&nbsp;</h5>
                                                <asp:CheckBox ID="ChkThreePieceAnyadditionalcomponent" Text="Any additional component" CssClass="myClass" runat="server" />
                                                <asp:TextBox ID="txtThreePieceAnyadditionalcomponentcat1" runat="server" placeholder="Specify"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="display: inline;">
                                <center>
                                    <asp:Button ID="Button7" ValidationGroup="form2" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitMFS_Click" />
                                    &nbsp;<asp:Button ID="Button8" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="BtnCancelMFS_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--13 Specify--%>
                    <asp:Panel ID="PanelType13" runat="server" Visible="false">
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
                                <center>
                                    <asp:Button ID="btnSubmitSpecify" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnSubmitSpecify_Click" />
                                    &nbsp;<asp:Button ID="btnCancelSpecify" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelSpecify_Click" /></center>
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>

    </asp:Panel>
    <%--  Add Particulars --%>

    <script type="text/javascript">

       <%-- function sum() {
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

            //taxation
            var taxation = document.getElementById('<%=ddltaxation.ClientID%>').value;

            if (isNaN(txtqty1) || txtqty1 == "") { txtqty1 = 0; }
            if (isNaN(txtrate1) || txtrate1 == "") { txtrate1 = 0; }

            if (taxation == 'inmah') {
                if (isNaN(cgst1) || cgst1 == "") {
                    cgst1 = 9;
                    $("#<%=txtCGST.ClientID%>").val(cgst1);
                }
                if (isNaN(sgst1) || sgst1 == "") {
                    sgst1 = 9;
                    $("#<%=txtSGST.ClientID%>").val(sgst1);
                }
                if (isNaN(igst1) || igst1 == "" || igst1 == 0) {
                    igst1 = 0; cgst1 = 9; sgst1 = 9;
                    $("#<%=txtIGST.ClientID%>").val(igst1);

                    $("#<%=txtCGST.ClientID%>").val(cgst1);
                    $("#<%=txtSGST.ClientID%>").val(sgst1);
                }
                else {

                    cgst1 = 0; sgst1 = 0; cgstamt1 = 0; sgstamt1 = 0;
                    $("#<%=txtCGST.ClientID%>").val(cgst1);
                    $("#<%=txtSGST.ClientID%>").val(sgst1);
                }
            }
            else if (taxation == 'outmah') {
                if (isNaN(cgst1) || cgst1 == "") {
                    cgst1 = 0;
                    $("#<%=txtCGST.ClientID%>").val(cgst1);
                }
                if (isNaN(sgst1) || sgst1 == "") {
                    sgst1 = 0;
                    $("#<%=txtSGST.ClientID%>").val(sgst1);
                }
                if (isNaN(igst1) || igst1 == "" || igst1 == 0) {
                    igst1 = 18; cgst1 = 0; sgst1 = 0;
                    $("#<%=txtIGST.ClientID%>").val(igst1);

                    $("#<%=txtCGST.ClientID%>").val(cgst1);
                    $("#<%=txtSGST.ClientID%>").val(sgst1);
                }
                else {

                    cgst1 = 0; sgst1 = 0; cgstamt1 = 0; sgstamt1 = 0;
                    $("#<%=txtCGST.ClientID%>").val(cgst1);
                    $("#<%=txtSGST.ClientID%>").val(sgst1);
                }
            }
            else if (taxation == 'outind') {
                $("#<%=txtCGST.ClientID%>").val('0');
                $("#<%=txtSGST.ClientID%>").val('0');
                $("#<%=txtIGST.ClientID%>").val('0');
            }
            else {
                alert('Please Select Taxation');
            }

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

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('<%=txtCGSTamt.ClientID%>').value = cgstamt1.toFixed(2); }
            if (!isNaN(sgstamt1)) { document.getElementById('<%=txtSGSTamt.ClientID%>').value = sgstamt1.toFixed(2); }
            if (!isNaN(igstamt1)) { document.getElementById('<%=txtIGSTamt.ClientID%>').value = igstamt1.toFixed(2); }

            var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            //var total1 = result1 - (discAmt + totGST1);
            //var total1 = result1 + totGST1 - discAmt;
            var total1 = result1 - discAmt;
            if (!isNaN(result1)) { document.getElementById('<%=txtAmt1.ClientID%>').value = total1.toFixed(2); }

            $('.myDate').datepicker('setDate', new Date());
        }--%>



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

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('<%=txtCGSTamt.ClientID%>').value = cgstamt1.toFixed(2); }
            if (!isNaN(sgstamt1)) { document.getElementById('<%=txtSGSTamt.ClientID%>').value = sgstamt1.toFixed(2); }
            if (!isNaN(igstamt1)) { document.getElementById('<%=txtIGSTamt.ClientID%>').value = igstamt1.toFixed(2); }

            var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            //var total1 = (result1 + totGST1) - discAmt;
            var total1 = (result1) - discAmt;
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
