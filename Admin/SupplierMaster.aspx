<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SupplierMaster.aspx.cs" Inherits="Admin_SupplierMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="multiselect.min.js"></script>

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

        .rwotoppadding {
            padding-top: 10px;
        }
    </style>
    <script>
        function HideLabel(msg) {
            Swal.fire({
                icon: 'success',
                text: msg,
                timer: 3000,
                showCancelButton: false,
                showConfirmButton: false
            }).then(function () {
                window.location.href = "../Admin/AllSupplierList.aspx";
            })
        };
    </script>
    <style>
        .row {
            margin-top: 10px;
        }
    </style>
    <script type='text/javascript'>
        function scrollToElement() {
            var target = document.getElementById("divdtls").offsetTop;
            window.scrollTo(0, target);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-md-7">
                        </div>
                        <div class="col-md-5">
                            <div class="page-header-breadcrumb">
                                <div style="float: right; margin: 3px; margin-bottom: 5px;">
                                    <span><a href="SupplierList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Supplier List</a>&nbsp;&nbsp;
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container py-3">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Add Supplier</h5>
                            </div>
                            <div class="row">
                                <div class="col-xl-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Supplier Name<i class="reqcls">*&nbsp;</i> : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtSupplierName" CssClass="form-control" runat="server" Width="100%" OnTextChanged="txtSupplierName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Supplier Name"
                                                            ControlToValidate="txtSupplierName" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetSupplierList"
                                                            TargetControlID="txtSupplierName">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 spancls">Supplier Code:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtsupplierCode" CssClass="form-control" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row">

                                                    <div class="col-md-2 spancls"></div>
                                                    <div class="col-md-4">
                                                    </div>

                                                    <div class="col-md-4 spancls">
                                                        <asp:CheckBox runat="server" AutoPostBack="true" ID="chkSame" OnCheckedChanged="chkSame_CheckedChanged" />
                                                        <b>Is Same Address</b>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </div>

                                                <div class="row">

                                                    <div class="col-md-2 spancls">Address 1 <i class="reqcls">*&nbsp;</i>: </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtBillingaddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">Address 2: </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtShippingaddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-2 spancls">Email:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtEmailID" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic" ErrorMessage="Invalid Email" runat="server"
                                                            ControlToValidate="txtEmailID" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="col-md-2 spancls">Credit Days: </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtCreditDays" Placeholder="Credit Days" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Country : </div>
                                                    <div class="col-md-4">
                                                        <%--                                                 <asp:DropDownList ID="ddlCountry" runat="server" class="form-control">
                                                            <asp:ListItem Value="" Text="Select Country"></asp:ListItem>
                                                             <%--<asp:ListItem Text="INDIA"></asp:ListItem>--%>

                                                        <%-- <asp:ListItem Text="AFGHANISTAN"></asp:ListItem>
                                                            <asp:ListItem Text="ALBANIA"></asp:ListItem>
                                                            <asp:ListItem Text="ALGERIA"></asp:ListItem>
                                                            <asp:ListItem Text="ANDORRA"></asp:ListItem>
                                                            <asp:ListItem Text="ANGOLA"></asp:ListItem>
                                                            <asp:ListItem Text="ANTIGUA AND BARBUDA"></asp:ListItem>
                                                            <asp:ListItem Text="ARGENTINA"></asp:ListItem>
                                                            <asp:ListItem Text="ARMENIA"></asp:ListItem>
                                                            <asp:ListItem Text="AUSTRIA"></asp:ListItem>
                                                            <asp:ListItem Text="AZERBAIJAN"></asp:ListItem>
                                                            <asp:ListItem Text="BAHRAIN"></asp:ListItem>
                                                            <asp:ListItem Text="BANGLADESH"></asp:ListItem>
                                                            <asp:ListItem Text="BARBADOS"></asp:ListItem>
                                                            <asp:ListItem Text="BELARUS"></asp:ListItem>
                                                            <asp:ListItem Text="BELGIUM"></asp:ListItem>
                                                            <asp:ListItem Text="BELIZE"></asp:ListItem>
                                                            <asp:ListItem Text="BENIN"></asp:ListItem>
                                                            <asp:ListItem Text="BHUTAN"></asp:ListItem>
                                                            <asp:ListItem Text="BOLIVIA"></asp:ListItem>
                                                            <asp:ListItem Text="BOSNIA AND HERZEGOVINA"></asp:ListItem>
                                                            <asp:ListItem Text="BOTSWANA"></asp:ListItem>
                                                            <asp:ListItem Text="BRAZIL"></asp:ListItem>
                                                            <asp:ListItem Text="BRUNEI"></asp:ListItem>
                                                            <asp:ListItem Text="BULGARIA"></asp:ListItem>
                                                            <asp:ListItem Text="BURKINA FASO"></asp:ListItem>
                                                            <asp:ListItem Text="BURUNDI"></asp:ListItem>
                                                            <asp:ListItem Text="CABO VERDE"></asp:ListItem>
                                                            <asp:ListItem Text="CAMBODIA"></asp:ListItem>
                                                            <asp:ListItem Text="CAMEROON"></asp:ListItem>
                                                            <asp:ListItem Text="CANADA"></asp:ListItem>
                                                            <asp:ListItem Text="CENTRAL AFRICAN REPUBLIC"></asp:ListItem>
                                                            <asp:ListItem Text="CHAD"></asp:ListItem>
                                                            <asp:ListItem Text="CHANNEL ISLANDS"></asp:ListItem>
                                                            <asp:ListItem Text="CHILE"></asp:ListItem>
                                                            <asp:ListItem Text="CHINA"></asp:ListItem>
                                                            <asp:ListItem Text="COLOMBIA"></asp:ListItem>
                                                            <asp:ListItem Text="COMOROS"></asp:ListItem>
                                                            <asp:ListItem Text="CONGO"></asp:ListItem>
                                                            <asp:ListItem Text="COSTA RICA"></asp:ListItem>
                                                            <asp:ListItem Text="CÔTE D'IVOIRE"></asp:ListItem>
                                                            <asp:ListItem Text="CROATIA"></asp:ListItem>
                                                            <asp:ListItem Text="CUBA"></asp:ListItem>
                                                            <asp:ListItem Text="CYPRUS"></asp:ListItem>
                                                            <asp:ListItem Text="CZECH REPUBLIC"></asp:ListItem>
                                                            <asp:ListItem Text="DENMARK"></asp:ListItem>
                                                            <asp:ListItem Text="DJIBOUTI"></asp:ListItem>
                                                            <asp:ListItem Text="DOMINICA"></asp:ListItem>
                                                            <asp:ListItem Text="DOMINICAN REPUBLIC"></asp:ListItem>
                                                            <asp:ListItem Text="DR CONGO"></asp:ListItem>
                                                            <asp:ListItem Text="DUBAI"></asp:ListItem>
                                                            <asp:ListItem Text="ECUADOR"></asp:ListItem>
                                                            <asp:ListItem Text="EGYPT"></asp:ListItem>
                                                            <asp:ListItem Text="EL SALVADOR"></asp:ListItem>
                                                            <asp:ListItem Text="EQUATORIAL GUINEA"></asp:ListItem>
                                                            <asp:ListItem Text="ERITREA"></asp:ListItem>
                                                            <asp:ListItem Text="ESTONIA"></asp:ListItem>
                                                            <asp:ListItem Text="ESWATINI"></asp:ListItem>
                                                            <asp:ListItem Text="ETHIOPIA"></asp:ListItem>
                                                            <asp:ListItem Text="FAEROE ISLANDS"></asp:ListItem>
                                                            <asp:ListItem Text="FINLAND"></asp:ListItem>
                                                            <asp:ListItem Text="FRANCE"></asp:ListItem>
                                                            <asp:ListItem Text="FRENCH GUIANA"></asp:ListItem>
                                                            <asp:ListItem Text="GABON"></asp:ListItem>
                                                            <asp:ListItem Text="GAMBIA"></asp:ListItem>
                                                            <asp:ListItem Text="GEORGIA"></asp:ListItem>
                                                            <asp:ListItem Text="GERMANY"></asp:ListItem>
                                                            <asp:ListItem Text="GHANA"></asp:ListItem>
                                                            <asp:ListItem Text="GIBRALTAR"></asp:ListItem>
                                                            <asp:ListItem Text="GREECE"></asp:ListItem>
                                                            <asp:ListItem Text="GRENADA"></asp:ListItem>
                                                            <asp:ListItem Text="GUATEMALA"></asp:ListItem>
                                                            <asp:ListItem Text="GUINEA"></asp:ListItem>
                                                            <asp:ListItem Text="GUINEA-BISSAU"></asp:ListItem>
                                                            <asp:ListItem Text="GUYANA"></asp:ListItem>
                                                            <asp:ListItem Text="HAITI"></asp:ListItem>
                                                            <asp:ListItem Text="HOLY SEE"></asp:ListItem>
                                                            <asp:ListItem Text="HONDURAS"></asp:ListItem>
                                                            <asp:ListItem Text="HONG KONG"></asp:ListItem>
                                                            <asp:ListItem Text="HUNGARY"></asp:ListItem>
                                                            <asp:ListItem Text="ICELAND"></asp:ListItem>
                                                            <asp:ListItem Text="INDIA"></asp:ListItem>
                                                            <asp:ListItem Text="INDONESIA"></asp:ListItem>
                                                            <asp:ListItem Text="IRAN"></asp:ListItem>
                                                            <asp:ListItem Text="IRAQ"></asp:ListItem>
                                                            <asp:ListItem Text="IRELAND"></asp:ListItem>
                                                            <asp:ListItem Text="ISLE OF MAN"></asp:ListItem>
                                                            <asp:ListItem Text="ISRAEL"></asp:ListItem>
                                                            <asp:ListItem Text="ITALY"></asp:ListItem>
                                                            <asp:ListItem Text="JAMAICA"></asp:ListItem>
                                                            <asp:ListItem Text="JAPAN"></asp:ListItem>
                                                            <asp:ListItem Text="JORDAN"></asp:ListItem>
                                                            <asp:ListItem Text="KAZAKHSTAN"></asp:ListItem>
                                                            <asp:ListItem Text="KENYA"></asp:ListItem>
                                                            <asp:ListItem Text="KUWAIT"></asp:ListItem>
                                                            <asp:ListItem Text="KYRGYZSTAN"></asp:ListItem>
                                                            <asp:ListItem Text="LAOS"></asp:ListItem>
                                                            <asp:ListItem Text="LATVIA"></asp:ListItem>
                                                            <asp:ListItem Text="LEBANON"></asp:ListItem>
                                                            <asp:ListItem Text="LESOTHO"></asp:ListItem>
                                                            <asp:ListItem Text="LIBERIA"></asp:ListItem>
                                                            <asp:ListItem Text="LIBYA"></asp:ListItem>
                                                            <asp:ListItem Text="LIECHTENSTEIN"></asp:ListItem>
                                                            <asp:ListItem Text="LITHUANIA"></asp:ListItem>
                                                            <asp:ListItem Text="LUXEMBOURG"></asp:ListItem>
                                                            <asp:ListItem Text="MACAO"></asp:ListItem>
                                                            <asp:ListItem Text="MADAGASCAR"></asp:ListItem>
                                                            <asp:ListItem Text="MALAWI"></asp:ListItem>
                                                            <asp:ListItem Text="MALAYSIA"></asp:ListItem>
                                                            <asp:ListItem Text="MALDIVES"></asp:ListItem>
                                                            <asp:ListItem Text="MALI"></asp:ListItem>
                                                            <asp:ListItem Text="MALTA"></asp:ListItem>
                                                            <asp:ListItem Text="MAURITANIA"></asp:ListItem>
                                                            <asp:ListItem Text="MAURITIUS"></asp:ListItem>
                                                            <asp:ListItem Text="MAYOTTE"></asp:ListItem>
                                                            <asp:ListItem Text="MEXICO"></asp:ListItem>
                                                            <asp:ListItem Text="MOLDOVA"></asp:ListItem>
                                                            <asp:ListItem Text="MONACO"></asp:ListItem>
                                                            <asp:ListItem Text="MONGOLIA"></asp:ListItem>
                                                            <asp:ListItem Text="MONTENEGRO"></asp:ListItem>
                                                            <asp:ListItem Text="MOROCCO"></asp:ListItem>
                                                            <asp:ListItem Text="MOZAMBIQUE"></asp:ListItem>
                                                            <asp:ListItem Text="MYANMAR"></asp:ListItem>
                                                            <asp:ListItem Text="NAMIBIA"></asp:ListItem>
                                                            <asp:ListItem Text="NEPAL"></asp:ListItem>
                                                            <asp:ListItem Text="NETHERLANDS"></asp:ListItem>
                                                            <asp:ListItem Text="NICARAGUA"></asp:ListItem>
                                                            <asp:ListItem Text="NIGER"></asp:ListItem>
                                                            <asp:ListItem Text="NIGERIA"></asp:ListItem>
                                                            <asp:ListItem Text="NORTH KOREA"></asp:ListItem>
                                                            <asp:ListItem Text="NORTH MACEDONIA"></asp:ListItem>
                                                            <asp:ListItem Text="NORWAY"></asp:ListItem>
                                                            <asp:ListItem Text="OMAN"></asp:ListItem>
                                                            <asp:ListItem Text="PAKISTAN"></asp:ListItem>
                                                            <asp:ListItem Text="PANAMA"></asp:ListItem>
                                                            <asp:ListItem Text="PARAGUAY"></asp:ListItem>
                                                            <asp:ListItem Text="PERU"></asp:ListItem>
                                                            <asp:ListItem Text="PHILIPPINES"></asp:ListItem>
                                                            <asp:ListItem Text="POLAND"></asp:ListItem>
                                                            <asp:ListItem Text="PORTUGAL"></asp:ListItem>
                                                            <asp:ListItem Text="QATAR"></asp:ListItem>
                                                            <asp:ListItem Text="RÉUNION"></asp:ListItem>
                                                            <asp:ListItem Text="ROMANIA"></asp:ListItem>
                                                            <asp:ListItem Text="RUSSIA"></asp:ListItem>
                                                            <asp:ListItem Text="RWANDA"></asp:ListItem>
                                                            <asp:ListItem Text="SAINT HELENA"></asp:ListItem>
                                                            <asp:ListItem Text="SAINT KITTS AND NEVIS"></asp:ListItem>
                                                            <asp:ListItem Text="SAINT LUCIA"></asp:ListItem>
                                                            <asp:ListItem Text="SAINT VINCENT AND THE GRENADINES"></asp:ListItem>
                                                            <asp:ListItem Text="SAN MARINO"></asp:ListItem>
                                                            <asp:ListItem Text="SAO TOME & PRINCIPE"></asp:ListItem>
                                                            <asp:ListItem Text="SAUDI ARABIA"></asp:ListItem>
                                                            <asp:ListItem Text="SENEGAL"></asp:ListItem>
                                                            <asp:ListItem Text="SERBIA"></asp:ListItem>
                                                            <asp:ListItem Text="SEYCHELLES"></asp:ListItem>
                                                            <asp:ListItem Text="SIERRA LEONE"></asp:ListItem>
                                                            <asp:ListItem Text="SINGAPORE"></asp:ListItem>
                                                            <asp:ListItem Text="SLOVAKIA"></asp:ListItem>
                                                            <asp:ListItem Text="SLOVENIA"></asp:ListItem>
                                                            <asp:ListItem Text="SOMALIA"></asp:ListItem>
                                                            <asp:ListItem Text="SOUTH AFRICA"></asp:ListItem>
                                                            <asp:ListItem Text="SOUTH KOREA"></asp:ListItem>
                                                            <asp:ListItem Text="SOUTH SUDAN"></asp:ListItem>
                                                            <asp:ListItem Text="SPAIN"></asp:ListItem>
                                                            <asp:ListItem Text="SRI LANKA"></asp:ListItem>
                                                            <asp:ListItem Text="STATE OF PALESTINE"></asp:ListItem>
                                                            <asp:ListItem Text="SUDAN"></asp:ListItem>
                                                            <asp:ListItem Text="SURINAME"></asp:ListItem>
                                                            <asp:ListItem Text="SWEDEN"></asp:ListItem>
                                                            <asp:ListItem Text="SWITZERLAND"></asp:ListItem>
                                                            <asp:ListItem Text="SYRIA"></asp:ListItem>
                                                            <asp:ListItem Text="TAIWAN"></asp:ListItem>
                                                            <asp:ListItem Text="TAJIKISTAN"></asp:ListItem>
                                                            <asp:ListItem Text="TANZANIA"></asp:ListItem>
                                                            <asp:ListItem Text="THAILAND"></asp:ListItem>
                                                            <asp:ListItem Text="THE BAHAMAS"></asp:ListItem>
                                                            <asp:ListItem Text="TIMOR-LESTE"></asp:ListItem>
                                                            <asp:ListItem Text="TOGO"></asp:ListItem>
                                                            <asp:ListItem Text="TRINIDAD AND TOBAGO"></asp:ListItem>
                                                            <asp:ListItem Text="TUNISIA"></asp:ListItem>
                                                            <asp:ListItem Text="TURKEY"></asp:ListItem>
                                                            <asp:ListItem Text="TURKMENISTAN"></asp:ListItem>
                                                            <asp:ListItem Text="UGANDA"></asp:ListItem>
                                                            <asp:ListItem Text="UKRAINE"></asp:ListItem>
                                                            <asp:ListItem Text="UNITED ARAB EMIRATES"></asp:ListItem>
                                                            <asp:ListItem Text="UNITED KINGDOM"></asp:ListItem>
                                                            <asp:ListItem Text="UNITED STATES"></asp:ListItem>
                                                            <asp:ListItem Text="URUGUAY"></asp:ListItem>
                                                            <asp:ListItem Text="UZBEKISTAN"></asp:ListItem>
                                                            <asp:ListItem Text="VENEZUELA"></asp:ListItem>
                                                            <asp:ListItem Text="VIETNAM"></asp:ListItem>
                                                            <asp:ListItem Text="WESTERN SAHARA"></asp:ListItem>
                                                            <asp:ListItem Text="YEMEN"></asp:ListItem>
                                                            <asp:ListItem Text="ZAMBIA"></asp:ListItem>
                                                            <asp:ListItem Text="ZIMBABWE"></asp:ListItem>--%>
                                                        <%--    </asp:DropDownList>--%>


                                                        <asp:DropDownList ID="ddlCountry" runat="server" class="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="">Select Country</asp:ListItem>
                                                            <asp:ListItem Text="AFGHANISTAN" Value="AFN"></asp:ListItem>
                                                            <asp:ListItem Text="ALBANIA" Value="ALL"></asp:ListItem>
                                                            <asp:ListItem Text="ALGERIA" Value="DZD"></asp:ListItem>
                                                            <asp:ListItem Text="ANDORRA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="ANGOLA" Value="AOA"></asp:ListItem>
                                                            <asp:ListItem Text="ANTIGUA AND BARBUDA" Value="XCD"></asp:ListItem>
                                                            <asp:ListItem Text="ARGENTINA" Value="ARS"></asp:ListItem>
                                                            <asp:ListItem Text="ARMENIA" Value="AMD"></asp:ListItem>
                                                            <asp:ListItem Text="AUSTRALIA" Value="AUD"></asp:ListItem>
                                                            <asp:ListItem Text="AUSTRIA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="AZERBAIJAN" Value="AZN"></asp:ListItem>
                                                            <asp:ListItem Text="BAHAMAS" Value="BSD"></asp:ListItem>
                                                            <asp:ListItem Text="BAHRAIN" Value="BHD"></asp:ListItem>
                                                            <asp:ListItem Text="BANGLADESH" Value="BDT"></asp:ListItem>
                                                            <asp:ListItem Text="BARBADOS" Value="BBD"></asp:ListItem>
                                                            <asp:ListItem Text="BELARUS" Value="BYN"></asp:ListItem>
                                                            <asp:ListItem Text="BELGIUM" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="BELIZE" Value="BZD"></asp:ListItem>
                                                            <asp:ListItem Text="BENIN" Value="XOF"></asp:ListItem>
                                                            <asp:ListItem Text="BHUTAN" Value="BTN"></asp:ListItem>
                                                            <asp:ListItem Text="BOLIVIA" Value="BOB"></asp:ListItem>
                                                            <asp:ListItem Text="BOSNIA AND HERZEGOVINA" Value="BAM"></asp:ListItem>
                                                            <asp:ListItem Text="BOTSWANA" Value="BWP"></asp:ListItem>
                                                            <asp:ListItem Text="BRAZIL" Value="BRL"></asp:ListItem>
                                                            <asp:ListItem Text="BRUNEI" Value="BND"></asp:ListItem>
                                                            <asp:ListItem Text="BULGARIA" Value="BGN"></asp:ListItem>
                                                            <asp:ListItem Text="BURKINA FASO" Value="XOF"></asp:ListItem>
                                                            <asp:ListItem Text="BURUNDI" Value="BIF"></asp:ListItem>
                                                            <asp:ListItem Text="CABO VERDE" Value="CVE"></asp:ListItem>
                                                            <asp:ListItem Text="CAMBODIA" Value="KHR"></asp:ListItem>
                                                            <asp:ListItem Text="CAMEROON" Value="XAF"></asp:ListItem>
                                                            <asp:ListItem Text="CANADA" Value="CAD"></asp:ListItem>
                                                            <asp:ListItem Text="CENTRAL AFRICAN REPUBLIC" Value="XAF"></asp:ListItem>
                                                            <asp:ListItem Text="CHAD" Value="XAF"></asp:ListItem>
                                                            <asp:ListItem Text="CHILE" Value="CLP"></asp:ListItem>
                                                            <asp:ListItem Text="CHINA" Value="CNY"></asp:ListItem>
                                                            <asp:ListItem Text="COLOMBIA" Value="COP"></asp:ListItem>
                                                            <asp:ListItem Text="COMOROS" Value="KMF"></asp:ListItem>
                                                            <asp:ListItem Text="CONGO" Value="CDF"></asp:ListItem>
                                                            <asp:ListItem Text="COSTA RICA" Value="CRC"></asp:ListItem>
                                                            <asp:ListItem Text="CÔTE D'IVOIRE" Value="XOF"></asp:ListItem>
                                                            <asp:ListItem Text="CROATIA" Value="HRK"></asp:ListItem>
                                                            <asp:ListItem Text="CUBA" Value="CUP"></asp:ListItem>
                                                            <asp:ListItem Text="CYPRUS" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="CZECH REPUBLIC" Value="CZK"></asp:ListItem>
                                                            <asp:ListItem Text="DENMARK" Value="DKK"></asp:ListItem>
                                                            <asp:ListItem Text="DJIBOUTI" Value="DJF"></asp:ListItem>
                                                            <asp:ListItem Text="DOMINICA" Value="XCD"></asp:ListItem>
                                                            <asp:ListItem Text="DOMINICAN REPUBLIC" Value="DOP"></asp:ListItem>
                                                            <asp:ListItem Text="DR CONGO" Value="CDF"></asp:ListItem>
                                                            <asp:ListItem Text="ECUADOR" Value="USD"></asp:ListItem>
                                                            <asp:ListItem Text="EGYPT" Value="EGP"></asp:ListItem>
                                                            <asp:ListItem Text="EL SALVADOR" Value="USD"></asp:ListItem>
                                                            <asp:ListItem Text="EQUATORIAL GUINEA" Value="XAF"></asp:ListItem>
                                                            <asp:ListItem Text="ERITREA" Value="ERN"></asp:ListItem>
                                                            <asp:ListItem Text="ESTONIA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="ESWATINI" Value="SZL"></asp:ListItem>
                                                            <asp:ListItem Text="ETHIOPIA" Value="ETB"></asp:ListItem>
                                                            <asp:ListItem Text="FAEROE ISLANDS" Value="DKK"></asp:ListItem>
                                                            <asp:ListItem Text="FINLAND" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="FRANCE" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="FRENCH GUIANA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="GABON" Value="XAF"></asp:ListItem>
                                                            <asp:ListItem Text="GAMBIA" Value="GMD"></asp:ListItem>
                                                            <asp:ListItem Text="GEORGIA" Value="GEL"></asp:ListItem>
                                                            <asp:ListItem Text="GERMANY" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="GHANA" Value="GHS"></asp:ListItem>
                                                            <asp:ListItem Text="GIBRALTAR" Value="GIP"></asp:ListItem>
                                                            <asp:ListItem Text="GREECE" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="GRENADA" Value="XCD"></asp:ListItem>
                                                            <asp:ListItem Text="GUATEMALA" Value="GTQ"></asp:ListItem>
                                                            <asp:ListItem Text="GUINEA" Value="GNF"></asp:ListItem>
                                                            <asp:ListItem Text="GUINEA-BISSAU" Value="XOF"></asp:ListItem>
                                                            <asp:ListItem Text="GUYANA" Value="GYD"></asp:ListItem>
                                                            <asp:ListItem Text="HAITI" Value="HTG"></asp:ListItem>
                                                            <asp:ListItem Text="HOLY SEE" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="HONDURAS" Value="HNL"></asp:ListItem>
                                                            <asp:ListItem Text="HONG KONG" Value="HKD"></asp:ListItem>
                                                            <asp:ListItem Text="HUNGARY" Value="HUF"></asp:ListItem>
                                                            <asp:ListItem Text="ICELAND" Value="ISK"></asp:ListItem>
                                                            <asp:ListItem Text="INDIA" Value="INR"></asp:ListItem>
                                                            <asp:ListItem Text="INDONESIA" Value="IDR"></asp:ListItem>
                                                            <asp:ListItem Text="IRAN" Value="IRR"></asp:ListItem>
                                                            <asp:ListItem Text="IRAQ" Value="IQD"></asp:ListItem>
                                                            <asp:ListItem Text="IRELAND" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="ISLE OF MAN" Value="GBP"></asp:ListItem>
                                                            <asp:ListItem Text="ISRAEL" Value="ILS"></asp:ListItem>
                                                            <asp:ListItem Text="ITALY" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="JAMAICA" Value="JMD"></asp:ListItem>
                                                            <asp:ListItem Text="JAPAN" Value="JPY"></asp:ListItem>
                                                            <asp:ListItem Text="JORDAN" Value="JOD"></asp:ListItem>
                                                            <asp:ListItem Text="KAZAKHSTAN " Value="KZT"></asp:ListItem>
                                                            <asp:ListItem Text="KENYA" Value="KES"></asp:ListItem>
                                                            <asp:ListItem Text="KUWAIT" Value="KWD"></asp:ListItem>
                                                            <asp:ListItem Text="KYRGYZSTAN" Value="KGS"></asp:ListItem>
                                                            <asp:ListItem Text="LAOS" Value="LAK"></asp:ListItem>
                                                            <asp:ListItem Text="LATVIA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="LEBANON" Value="LBP"></asp:ListItem>
                                                            <asp:ListItem Text="LESOTHO" Value="LSL"></asp:ListItem>
                                                            <asp:ListItem Text="LIBERIA" Value="LRD"></asp:ListItem>
                                                            <asp:ListItem Text="LIBYA" Value="LYD"></asp:ListItem>
                                                            <asp:ListItem Text="LIECHTENSTEIN" Value="CHF"></asp:ListItem>
                                                            <asp:ListItem Text="LITHUANIA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="LUXEMBOURG" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="MACAO" Value="MOP"></asp:ListItem>
                                                            <asp:ListItem Text="MADAGASCAR" Value="MGA"></asp:ListItem>
                                                            <asp:ListItem Text="MALAWI" Value="MWK"></asp:ListItem>
                                                            <asp:ListItem Text="MALAYSIA" Value="MYR"></asp:ListItem>
                                                            <asp:ListItem Text="MALDIVES" Value="MVR"></asp:ListItem>
                                                            <asp:ListItem Text="MALI" Value="XOF"></asp:ListItem>
                                                            <asp:ListItem Text="MALTA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="MAURITANIA" Value="MRU"></asp:ListItem>
                                                            <asp:ListItem Text="MAURITIUS" Value="MUR"></asp:ListItem>
                                                            <asp:ListItem Text="MAYOTTE" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="MEXICO" Value="MXN"></asp:ListItem>
                                                            <asp:ListItem Text="MOLDOVA" Value="MDL"></asp:ListItem>
                                                            <asp:ListItem Text="MONACO" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="MONGOLIA" Value="MNT"></asp:ListItem>
                                                            <asp:ListItem Text="MONTENEGRO" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="MOROCCO" Value="MAD"></asp:ListItem>
                                                            <asp:ListItem Text="MOZAMBIQUE" Value="MZN"></asp:ListItem>
                                                            <asp:ListItem Text="MYANMAR" Value="MMK"></asp:ListItem>
                                                            <asp:ListItem Text="NAMIBIA" Value="NAD"></asp:ListItem>
                                                            <asp:ListItem Text="NEPAL" Value="NPR"></asp:ListItem>
                                                            <asp:ListItem Text="NETHERLANDS" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="NICARAGUA" Value="NIO"></asp:ListItem>
                                                            <asp:ListItem Text="NIGER" Value="XOF"></asp:ListItem>
                                                            <asp:ListItem Text="NIGERIA" Value="NGN"></asp:ListItem>
                                                            <asp:ListItem Text="NORTH KOREA" Value="KPW"></asp:ListItem>
                                                            <asp:ListItem Text="NORTH MACEDONIA (MKD)" Value="MKD"></asp:ListItem>
                                                            <asp:ListItem Text="NORWAY" Value="NOK"></asp:ListItem>
                                                            <asp:ListItem Text="OMAN" Value="OMR"></asp:ListItem>
                                                            <asp:ListItem Text="PAKISTAN" Value="PKR"></asp:ListItem>
                                                            <asp:ListItem Text="PANAMA" Value="PAB"></asp:ListItem>
                                                            <asp:ListItem Text="PARAGUAY" Value="PYG"></asp:ListItem>
                                                            <asp:ListItem Text="PERU" Value="PEN"></asp:ListItem>
                                                            <asp:ListItem Text="PHILIPPINES" Value="PHP"></asp:ListItem>
                                                            <asp:ListItem Text="POLAND" Value="PLN"></asp:ListItem>
                                                            <asp:ListItem Text="PORTUGA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="QATAR" Value="QAR"></asp:ListItem>
                                                            <asp:ListItem Text="RÉUNION" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="ROMANIA" Value="RON"></asp:ListItem>
                                                            <asp:ListItem Text="RUSSIA" Value="RUB"></asp:ListItem>
                                                            <asp:ListItem Text="RWANDA" Value="RWF"></asp:ListItem>
                                                            <asp:ListItem Text="SAINT HELENA" Value="SHP"></asp:ListItem>
                                                            <asp:ListItem Text="SAINT KITTS AND NEVIS" Value="XCD"></asp:ListItem>
                                                            <asp:ListItem Text="SAINT LUCIA" Value="XCD"></asp:ListItem>
                                                            <asp:ListItem Text="SAINT VINCENT AND THE GRENADINES" Value="XCD"></asp:ListItem>
                                                            <asp:ListItem Text="SAN MARINO" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="SAO TOME & PRINCIPE" Value="STN"></asp:ListItem>
                                                            <asp:ListItem Text="SAUDI ARABIA" Value="SAR"></asp:ListItem>
                                                            <asp:ListItem Text="SENEGAL" Value="XOF"></asp:ListItem>
                                                            <asp:ListItem Text="SERBIA" Value="RSD"></asp:ListItem>
                                                            <asp:ListItem Text="SEYCHELLES" Value="SCR"></asp:ListItem>
                                                            <asp:ListItem Text="SIERRA LEONE" Value="SLL"></asp:ListItem>
                                                            <asp:ListItem Text="SINGAPORE" Value="SGD"></asp:ListItem>
                                                            <asp:ListItem Text="SLOVAKIA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="SLOVENIA" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="SOMALIA" Value="SOS"></asp:ListItem>
                                                            <asp:ListItem Text="SOUTH AFRICA" Value="ZAR"></asp:ListItem>
                                                            <asp:ListItem Text="SOUTH KOREA" Value="KRW"></asp:ListItem>
                                                            <asp:ListItem Text="SOUTH SUDAN" Value="SSP"></asp:ListItem>
                                                            <asp:ListItem Text="SPAIN" Value="EUR"></asp:ListItem>
                                                            <asp:ListItem Text="SRI LANKA" Value="LKR"></asp:ListItem>
                                                            <asp:ListItem Text="STATE OF PALESTINE" Value="ILS"></asp:ListItem>
                                                            <asp:ListItem Text="SUDAN (SDG)" Value="SDG"></asp:ListItem>
                                                            <asp:ListItem Text="SURINAME (SRD)" Value="SRD"></asp:ListItem>
                                                            <asp:ListItem Text="SWEDEN (SEK)" Value="SEK"></asp:ListItem>
                                                            <asp:ListItem Text="SWITZERLAND (CHF)" Value="CHF"></asp:ListItem>
                                                            <asp:ListItem Text="SYRIA (SYP)" Value="SYP"></asp:ListItem>
                                                            <asp:ListItem Text="TAIWAN (TWD)" Value="TWD"></asp:ListItem>
                                                            <asp:ListItem Text="TAJIKISTAN (TJS)" Value="TJS"></asp:ListItem>
                                                            <asp:ListItem Text="TANZANIA (TZS)" Value="TZS"></asp:ListItem>
                                                            <asp:ListItem Text="THAILAND (THB)" Value="THB"></asp:ListItem>
                                                            <asp:ListItem Text="TIMOR-LESTE (USD)" Value="USD"></asp:ListItem>
                                                            <asp:ListItem Text="TOGO (XOF)" Value="XOF"></asp:ListItem>
                                                            <asp:ListItem Text="TONGA (TOP)" Value="TOP"></asp:ListItem>
                                                            <asp:ListItem Text="TRINIDAD AND TOBAGO" Value="TTD"></asp:ListItem>
                                                            <asp:ListItem Text="TUNISIA" Value="TND"></asp:ListItem>
                                                            <asp:ListItem Text="TURKEY" Value="TRY"></asp:ListItem>
                                                            <asp:ListItem Text="TURKMENISTAN" Value="TMT"></asp:ListItem>
                                                            <asp:ListItem Text="UGANDA" Value="UGX"></asp:ListItem>
                                                            <asp:ListItem Text="UKRAINE" Value="UAH"></asp:ListItem>
                                                            <asp:ListItem Text="UNITED ARAB EMIRATES" Value="AED"></asp:ListItem>
                                                            <asp:ListItem Text="UNITED KINGDOM" Value="GBP"></asp:ListItem>
                                                            <asp:ListItem Text="UNITED STATES" Value="USD"></asp:ListItem>
                                                            <asp:ListItem Text="URUGUAY" Value="UYU"></asp:ListItem>
                                                            <asp:ListItem Text="UZBEKISTAN" Value="UZS"></asp:ListItem>
                                                            <asp:ListItem Text="VANUATU" Value="VUV"></asp:ListItem>
                                                            <asp:ListItem Text="VENEZUELA" Value="VES"></asp:ListItem>
                                                            <asp:ListItem Text="VIETNAM" Value="VND"></asp:ListItem>
                                                            <asp:ListItem Text="YEMEN" Value="YER"></asp:ListItem>
                                                            <asp:ListItem Text="ZAMBIA" Value="ZMW"></asp:ListItem>
                                                            <asp:ListItem Text="ZIMBABWE" Value="ZWL"></asp:ListItem>
                                                        </asp:DropDownList>



                                                    </div>
                                                    <div class="col-md-2 spancls">State : </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control">
                                                            <asp:ListItem Value="" Text="Select State"></asp:ListItem>
                                                            <asp:ListItem Text="1 JAMMU AND KASHMIR"></asp:ListItem>
                                                            <asp:ListItem Text="2 HIMACHAL PRADESH"></asp:ListItem>
                                                            <asp:ListItem Text="3 PUNJAB"></asp:ListItem>
                                                            <asp:ListItem Text="4 CHANDIGARH"></asp:ListItem>
                                                            <asp:ListItem Text="5 UTTARAKHAND"></asp:ListItem>
                                                            <asp:ListItem Text="6 HARYANA"></asp:ListItem>
                                                            <asp:ListItem Text="7 DELHI"></asp:ListItem>
                                                            <asp:ListItem Text="8 RAJASTHAN"></asp:ListItem>
                                                            <asp:ListItem Text="9 UTTAR PRADESH"></asp:ListItem>
                                                            <asp:ListItem Text="10 BIHAR"></asp:ListItem>
                                                            <asp:ListItem Text="11 SIKKIM"></asp:ListItem>
                                                            <asp:ListItem Text="12 ARUNACHAL PRADESH"></asp:ListItem>
                                                            <asp:ListItem Text="13 NAGALAND"></asp:ListItem>
                                                            <asp:ListItem Text="14 MANIPUR"></asp:ListItem>
                                                            <asp:ListItem Text="15 MIZORAM"></asp:ListItem>
                                                            <asp:ListItem Text="16 TRIPURA"></asp:ListItem>
                                                            <asp:ListItem Text="17 MEGHLAYA"></asp:ListItem>
                                                            <asp:ListItem Text="18 ASSAM"></asp:ListItem>
                                                            <asp:ListItem Text="19 WEST BENGAL"></asp:ListItem>
                                                            <asp:ListItem Text="20 JHARKHAND"></asp:ListItem>
                                                            <asp:ListItem Text="21 ODISHA"></asp:ListItem>
                                                            <asp:ListItem Text="22 CHATTISGARH"></asp:ListItem>
                                                            <asp:ListItem Text="23 MADHYA PRADESH"></asp:ListItem>
                                                            <asp:ListItem Text="24 GUJARAT"></asp:ListItem>
                                                            <asp:ListItem Text="25 DAMAN AND DIU"></asp:ListItem>
                                                            <asp:ListItem Text="26 DADRA AND NAGAR HAVELI"></asp:ListItem>
                                                            <asp:ListItem Text="27 MAHARASHTRA"></asp:ListItem>
                                                            <asp:ListItem Text="28 ANDHRA PRADESH (OLD)"></asp:ListItem>
                                                            <asp:ListItem Text="29 KARNATAKA"></asp:ListItem>
                                                            <asp:ListItem Text="30 GOA"></asp:ListItem>
                                                            <asp:ListItem Text="31 LAKSHWADEEP"></asp:ListItem>
                                                            <asp:ListItem Text="32 KERALA"></asp:ListItem>
                                                            <asp:ListItem Text="33 TAMIL NADU"></asp:ListItem>
                                                            <asp:ListItem Text="34 PUDUCHERRY"></asp:ListItem>
                                                            <asp:ListItem Text="35 ANDAMAN AND NICOBAR ISLANDS"></asp:ListItem>
                                                            <asp:ListItem Text="36 TELANGANA"></asp:ListItem>
                                                            <asp:ListItem Text="37 ANDHRA PRADESH (NEW)"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row" runat="server">
                                                    <div class="col-md-2 spancls">Registration Type : </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlRegistrationType" CssClass="form-control" runat="server">
                                                            <asp:ListItem>Registered</asp:ListItem>
                                                            <asp:ListItem>Non Registered</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2 spancls">GST Number</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtGSTNo" Placeholder="GST No" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged= "txtGSTNo_TextChanged"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="revGSTNumber" runat="server"
                                                            ControlToValidate="txtGSTNo"
                                                            ValidationExpression="^\d{2}[A-Z]{5}\d{4}[A-Z]{1}\d[Z]{1}[A-Z\d]{1}$"
                                                            Display="Dynamic"
                                                            ErrorMessage="Invalid GST Number. GST number should be in the format 27ATFPS1959J1Z4"
                                                            ForeColor="Red" />

                                                    </div>
                                                </div>

                                                <div class="row" runat="server">
                                                    <div class="col-md-2 spancls">PAN Number: </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtPANNo" Placeholder="PAN No" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">Currency</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtCurrency" Placeholder="Currency" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row" runat="server">
                                                    <div class="col-md-2 spancls">Supplier Tax Type: </div>
                                                    <div class="col-md-4">
                                                        <%--           <asp:TextBox ID="txtSupplierTaxtype" Placeholder="Supplier Tax Type" CssClass="form-control" runat="server"></asp:TextBox>--%>

                                                        <asp:DropDownList ID="ddltxttype" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2 spancls">Supplier Category</div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="txtSupplierCategory" CssClass="form-control select2" runat="server" SelectionMode="Multiple" AutoPostBack="true" />
                                                        <br />
                                                    </div>
                                                </div>

                                                <div class="row" runat="server">
                                                    <div class="col-md-2 spancls">Trade Name: </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtTradeName" Placeholder="Trade Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-2 spancls" style="display: none;">Payment Validity (Days)</div>
                                                    <div class="col-md-4" style="display: none;">
                                                        <asp:TextBox ID="txtPaymentValidity" Placeholder="Payment Validity" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row" runat="server">

                                                    <div class="col-md-2 spancls" style="display: none">Outstanding Limit</div>
                                                    <div class="col-md-4" style="display: none">
                                                        <asp:TextBox ID="txtOutstandingLimit" Placeholder="Outstanding Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="card-header bg-primary text-uppercase text-white" style="margin-top: 10px;" runat="server">
                                                    <h5>Contact Details</h5>
                                                </div>
                                                <div class="row" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <table class="table" border="1" style="width: 100%; border: 1px solid #0c7d38;">
                                                                <tr style="background-color: #7ad2d4; color: #000; font-weight: 600; text-align: center;">
                                                                    <td>Contact Name</td>
                                                                    <td>Designation</td>
                                                                    <td runat="server">Conatct Number</td>
                                                                    <td runat="server" visible="false">Notify</td>
                                                                    <td runat="server" visible="false">Access</td>
                                                                    <td style="width: 10%" runat="server" visible="false">Action</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtContactName" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </center>
                                                                    </td>
                                                                    <td>
                                                                        <center>

                                                                            <asp:DropDownList runat="server" ID="txtDesignation" CssClass="form-control">
                                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="  Manager Research and development"></asp:ListItem>
                                                                                <asp:ListItem Value=" A"></asp:ListItem>
                                                                                <asp:ListItem Value=" Account"></asp:ListItem>
                                                                                <asp:ListItem Value=" Account head"></asp:ListItem>
                                                                                <asp:ListItem Value=" Account Manager"></asp:ListItem>
                                                                                <asp:ListItem Value=" COMPANY"></asp:ListItem>
                                                                                <asp:ListItem Value=" COMPANY OWNER"></asp:ListItem>
                                                                                <asp:ListItem Value=" consultant"></asp:ListItem>
                                                                                <asp:ListItem Value=" Coordinator"></asp:ListItem>
                                                                                <asp:ListItem Value=" DIRECTOR"></asp:ListItem>
                                                                                <asp:ListItem Value=" DM"></asp:ListItem>
                                                                                <asp:ListItem Value=" Engineer "></asp:ListItem>
                                                                                <asp:ListItem Value=" Engineer Mech Design"></asp:ListItem>
                                                                                <asp:ListItem Value=" Executive"></asp:ListItem>
                                                                                <asp:ListItem Value=" M D"></asp:ListItem>
                                                                                <asp:ListItem Value=" Manager"></asp:ListItem>
                                                                                <asp:ListItem Value=" Manager Instrumentation"></asp:ListItem>
                                                                                <asp:ListItem Value=" Manager marketing"></asp:ListItem>
                                                                                <asp:ListItem Value=" Manager Mechanical Testing Division"></asp:ListItem>
                                                                                <asp:ListItem Value=" Manager procurement"></asp:ListItem>
                                                                                <asp:ListItem Value=" Manager Sales"></asp:ListItem>
                                                                                <asp:ListItem Value=" Manager SCM"></asp:ListItem>
                                                                                <asp:ListItem Value=" MD"></asp:ListItem>
                                                                                <asp:ListItem Value=" NA"></asp:ListItem>
                                                                                <asp:ListItem Value=" NTPC LTD"></asp:ListItem>
                                                                                <asp:ListItem Value=" Office"></asp:ListItem>
                                                                                <asp:ListItem Value=" Officer"></asp:ListItem>
                                                                                <asp:ListItem Value=" OWNER"></asp:ListItem>
                                                                                <asp:ListItem Value=" Partner"></asp:ListItem>
                                                                                <asp:ListItem Value=" POWERLINE"></asp:ListItem>
                                                                                <asp:ListItem Value=" Professor  Head Reliance Chair Professor"></asp:ListItem>
                                                                                <asp:ListItem Value=" Project Consultant"></asp:ListItem>
                                                                                <asp:ListItem Value=" Project Department"></asp:ListItem>
                                                                                <asp:ListItem Value=" Project Manager"></asp:ListItem>
                                                                                <asp:ListItem Value=" PROPRIETOR"></asp:ListItem>
                                                                                <asp:ListItem Value=" PROPRITOR"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchase"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchase asst"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchase department"></asp:ListItem>
                                                                                <asp:ListItem Value=" Purchase Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchase EX"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchase executive"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchase HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchase Manager"></asp:ListItem>
                                                                                <asp:ListItem Value=" Purchase Officer"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchaseHEAD"></asp:ListItem>
                                                                                <asp:ListItem Value=" purchaser"></asp:ListItem>
                                                                                <asp:ListItem Value=" QA Executive"></asp:ListItem>
                                                                                <asp:ListItem Value=" resigned"></asp:ListItem>
                                                                                <asp:ListItem Value=" Sales"></asp:ListItem>
                                                                                <asp:ListItem Value=" Sales Manager"></asp:ListItem>
                                                                                <asp:ListItem Value=" Sales team"></asp:ListItem>
                                                                                <asp:ListItem Value=" self"></asp:ListItem>
                                                                                <asp:ListItem Value=" Senior Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value=" Senior Quality Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value=" Sr Engineer "></asp:ListItem>
                                                                                <asp:ListItem Value=" Sr Engineer Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value=" Supertech "></asp:ListItem>
                                                                                <asp:ListItem Value=" superwiser"></asp:ListItem>
                                                                                <asp:ListItem Value=" WORKER"></asp:ListItem>
                                                                                <asp:ListItem Value="A"></asp:ListItem>
                                                                                <asp:ListItem Value="A PLUS"></asp:ListItem>
                                                                                <asp:ListItem Value="aa"></asp:ListItem>
                                                                                <asp:ListItem Value="AAA"></asp:ListItem>
                                                                                <asp:ListItem Value="AADARSH"></asp:ListItem>
                                                                                <asp:ListItem Value="Aakash Gohel"></asp:ListItem>
                                                                                <asp:ListItem Value="AB"></asp:ListItem>
                                                                                <asp:ListItem Value="ABA PAWAR"></asp:ListItem>
                                                                                <asp:ListItem Value="ABC"></asp:ListItem>
                                                                                <asp:ListItem Value="ABCD"></asp:ListItem>
                                                                                <asp:ListItem Value="ABCDEF"></asp:ListItem>
                                                                                <asp:ListItem Value="ABHI"></asp:ListItem>
                                                                                <asp:ListItem Value="ABHITRANS ENGINEERS AND CONTRACTORS"></asp:ListItem>
                                                                                <asp:ListItem Value="ABI"></asp:ListItem>
                                                                                <asp:ListItem Value="ACC"></asp:ListItem>
                                                                                <asp:ListItem Value="ACC DEPT"></asp:ListItem>
                                                                                <asp:ListItem Value="ACC LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="ACCOUNT"></asp:ListItem>
                                                                                <asp:ListItem Value="ACCOUNT DEPT"></asp:ListItem>
                                                                                <asp:ListItem Value="Account Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Account section"></asp:ListItem>
                                                                                <asp:ListItem Value="Accountant"></asp:ListItem>
                                                                                <asp:ListItem Value="ACCOUNTS"></asp:ListItem>
                                                                                <asp:ListItem Value="Accounts Dept"></asp:ListItem>
                                                                                <asp:ListItem Value="ACCURATE"></asp:ListItem>
                                                                                <asp:ListItem Value="ACCURATE POWERTECH INDIA PVT LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="ACCUSHARP"></asp:ListItem>
                                                                                <asp:ListItem Value="ACOUNT HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="ACPL Sales Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="ADANI"></asp:ListItem>
                                                                                <asp:ListItem Value="ADANI WILMAR LI"></asp:ListItem>
                                                                                <asp:ListItem Value="ADANI WILMAR LIMITE"></asp:ListItem>
                                                                                <asp:ListItem Value="ADMIN"></asp:ListItem>
                                                                                <asp:ListItem Value="ADMIN HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="Administrator"></asp:ListItem>
                                                                                <asp:ListItem Value="Administrator OFFICER"></asp:ListItem>
                                                                                <asp:ListItem Value="ADOR"></asp:ListItem>
                                                                                <asp:ListItem Value="ADOR SERV"></asp:ListItem>
                                                                                <asp:ListItem Value="ADVANCE"></asp:ListItem>
                                                                                <asp:ListItem Value="ADVERTISING"></asp:ListItem>
                                                                                <asp:ListItem Value="ADVIK ELECTRICAL"></asp:ListItem>
                                                                                <asp:ListItem Value="advocate"></asp:ListItem>
                                                                                <asp:ListItem Value="AEGASUN"></asp:ListItem>
                                                                                <asp:ListItem Value="AFM"></asp:ListItem>
                                                                                <asp:ListItem Value="AGENT"></asp:ListItem>
                                                                                <asp:ListItem Value="AGM"></asp:ListItem>
                                                                                <asp:ListItem Value="AGM COMMERCIAL"></asp:ListItem>
                                                                                <asp:ListItem Value="AGM Design Mfg"></asp:ListItem>
                                                                                <asp:ListItem Value="AGM Instruments"></asp:ListItem>
                                                                                <asp:ListItem Value="AGM Marketing"></asp:ListItem>
                                                                                <asp:ListItem Value="AGM Operation"></asp:ListItem>
                                                                                <asp:ListItem Value="AGM PROCUREMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="AGM Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="AGNI SOLAR SYSTEMS PRIVATE LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="AGRAVARTI"></asp:ListItem>
                                                                                <asp:ListItem Value="AHMEDABAD"></asp:ListItem>
                                                                                <asp:ListItem Value="AJS"></asp:ListItem>
                                                                                <asp:ListItem Value="AKSHAY"></asp:ListItem>
                                                                                <asp:ListItem Value="AKSHAY SOLAR"></asp:ListItem>
                                                                                <asp:ListItem Value="AKSHY URJA"></asp:ListItem>
                                                                                <asp:ListItem Value="ALLIANCE"></asp:ListItem>
                                                                                <asp:ListItem Value="AM"></asp:ListItem>
                                                                                <asp:ListItem Value="AMAZON"></asp:ListItem>
                                                                                <asp:ListItem Value="Ambika"></asp:ListItem>
                                                                                <asp:ListItem Value="AMBIKA ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="AMBUJA CEMENT LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="AMIT AQUA ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="AMO"></asp:ListItem>
                                                                                <asp:ListItem Value="ANNANYA"></asp:ListItem>
                                                                                <asp:ListItem Value="ANU"></asp:ListItem>
                                                                                <asp:ListItem Value="ANUJ ELECTRICALS"></asp:ListItem>
                                                                                <asp:ListItem Value="APEX ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="AQ"></asp:ListItem>
                                                                                <asp:ListItem Value="AQUA"></asp:ListItem>
                                                                                <asp:ListItem Value="AQUATECH ENGINEERING SEVICES"></asp:ListItem>
                                                                                <asp:ListItem Value="ARCITATE"></asp:ListItem>
                                                                                <asp:ListItem Value="ARCTIACT"></asp:ListItem>
                                                                                <asp:ListItem Value="Area Incharge Sales"></asp:ListItem>
                                                                                <asp:ListItem Value="Area Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Area Sale Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Area Sales Manager Equipment WEST"></asp:ListItem>
                                                                                <asp:ListItem Value="ARIHANT"></asp:ListItem>
                                                                                <asp:ListItem Value="ARMOR"></asp:ListItem>
                                                                                <asp:ListItem Value="AROUND ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="as"></asp:ListItem>
                                                                                <asp:ListItem Value="asdfd"></asp:ListItem>
                                                                                <asp:ListItem Value="ASHAPURA"></asp:ListItem>
                                                                                <asp:ListItem Value="ASIAN"></asp:ListItem>
                                                                                <asp:ListItem Value="ASMITA "></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Buyer  Procurement"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant general manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant General Manager Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Instrumentation Design"></asp:ListItem>
                                                                                <asp:ListItem Value="ASSISTANT MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Manager  Procurement"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Manager  Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Manager  SCM"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Manager Maintenance"></asp:ListItem>
                                                                                <asp:ListItem Value="ASSISTANT MANAGER OPERATIONS"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Manager Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="ASSISTANT MANAGER SALES"></asp:ListItem>
                                                                                <asp:ListItem Value="ASSISTANT MANAGER SERVICE"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Manager Supplier Quality"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Manager Supply Chain"></asp:ListItem>
                                                                                <asp:ListItem Value="ASSISTANT MANAGERQULALITY MANAGEMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Manger "></asp:ListItem>
                                                                                <asp:ListItem Value="assistant project coordinator"></asp:ListItem>
                                                                                <asp:ListItem Value="Assistant Purchase F and B Systems"></asp:ListItem>
                                                                                <asp:ListItem Value="Associate Engineer Operations"></asp:ListItem>
                                                                                <asp:ListItem Value="Associate Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="ASSOCIATE MANAGER PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="ASSOCIATE MANAGER QA"></asp:ListItem>
                                                                                <asp:ListItem Value="ASSOCIATE OFFICER"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst  Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst director"></asp:ListItem>
                                                                                <asp:ListItem Value="ASST ENGINEER PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="Asst manager  Project and Solution Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Manager  SCM"></asp:ListItem>
                                                                                <asp:ListItem Value="ASST MANAGER  STRATEGIC SOURCING"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Manager Application  Sales"></asp:ListItem>
                                                                                <asp:ListItem Value="ASST MANAGER DESIGN DEVELOPMENTS"></asp:ListItem>
                                                                                <asp:ListItem Value="ASST MANAGER LOGISTIC QUALITY"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst MANAGER MATERIALS"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Manager Order Execution"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Manager Project"></asp:ListItem>
                                                                                <asp:ListItem Value="ASST MANAGER PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Manager QA"></asp:ListItem>
                                                                                <asp:ListItem Value="ASST MANAGER QUALITY"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Manager Sales and Application"></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Maneger Sales "></asp:ListItem>
                                                                                <asp:ListItem Value="Asst Sales Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Astt Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="ATONSYS"></asp:ListItem>
                                                                                <asp:ListItem Value="Aurangabad"></asp:ListItem>
                                                                                <asp:ListItem Value="AUTHORISED SIGNATORY"></asp:ListItem>
                                                                                <asp:ListItem Value="AUTO"></asp:ListItem>
                                                                                <asp:ListItem Value="Automation Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="AUTORISED SIGN"></asp:ListItem>
                                                                                <asp:ListItem Value="AVL"></asp:ListItem>
                                                                                <asp:ListItem Value="AVM"></asp:ListItem>
                                                                                <asp:ListItem Value="AVP E and I"></asp:ListItem>
                                                                                <asp:ListItem Value="AWL"></asp:ListItem>
                                                                                <asp:ListItem Value="AZ"></asp:ListItem>
                                                                                <asp:ListItem Value="AZAD"></asp:ListItem>
                                                                                <asp:ListItem Value="B"></asp:ListItem>
                                                                                <asp:ListItem Value="B AND H MATERIAL"></asp:ListItem>
                                                                                <asp:ListItem Value="b d manager "></asp:ListItem>
                                                                                <asp:ListItem Value="BAJAJ"></asp:ListItem>
                                                                                <asp:ListItem Value="BALAJEE ENGINEERS"></asp:ListItem>
                                                                                <asp:ListItem Value="BALAJI INDUSTRIAL "></asp:ListItem>
                                                                                <asp:ListItem Value="BALEWADI TECHPARK PRIVATE LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="Bangalore"></asp:ListItem>
                                                                                <asp:ListItem Value="BASAVA"></asp:ListItem>
                                                                                <asp:ListItem Value="BDM Energy"></asp:ListItem>
                                                                                <asp:ListItem Value="BE"></asp:ListItem>
                                                                                <asp:ListItem Value="BEENA ENGLISH MEDIUM SCHOOL"></asp:ListItem>
                                                                                <asp:ListItem Value="BENGALURU"></asp:ListItem>
                                                                                <asp:ListItem Value="BEP"></asp:ListItem>
                                                                                <asp:ListItem Value="BERGER"></asp:ListItem>
                                                                                <asp:ListItem Value="BERZELIUS CHEMICALS PRIVATE LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="BFV  Process owner"></asp:ListItem>
                                                                                <asp:ListItem Value="BGR"></asp:ListItem>
                                                                                <asp:ListItem Value="BGR ENERGY "></asp:ListItem>
                                                                                <asp:ListItem Value="BGR ENERGY SYSTEMS LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="BHALERAO"></asp:ListItem>
                                                                                <asp:ListItem Value="BHAMHA"></asp:ListItem>
                                                                                <asp:ListItem Value="BHISHEK"></asp:ListItem>
                                                                                <asp:ListItem Value="BHOSARI"></asp:ListItem>
                                                                                <asp:ListItem Value="BIRLA CARBON"></asp:ListItem>
                                                                                <asp:ListItem Value="BIRLA CENTURY"></asp:ListItem>
                                                                                <asp:ListItem Value="BOHT OUT Procurement Dept"></asp:ListItem>
                                                                                <asp:ListItem Value="Branch Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="BRIGHT "></asp:ListItem>
                                                                                <asp:ListItem Value="Broker"></asp:ListItem>
                                                                                <asp:ListItem Value="BUNTY ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="BUSINESS DEVELOPEMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="BUSINESS DEVELOPEMENT EXECUTIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="Business Development"></asp:ListItem>
                                                                                <asp:ListItem Value="Business Development Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Business Development Executve AMC"></asp:ListItem>
                                                                                <asp:ListItem Value="Business Development ManageR"></asp:ListItem>
                                                                                <asp:ListItem Value="BUSINESS EXECUTIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="Business Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Business Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Business Manager India"></asp:ListItem>
                                                                                <asp:ListItem Value="BUYER"></asp:ListItem>
                                                                                <asp:ListItem Value="C"></asp:ListItem>
                                                                                <asp:ListItem Value="C and M manager"></asp:ListItem>
                                                                                <asp:ListItem Value="C E O"></asp:ListItem>
                                                                                <asp:ListItem Value="CA"></asp:ListItem>
                                                                                <asp:ListItem Value="CARE LIFTIN"></asp:ListItem>
                                                                                <asp:ListItem Value="CARPENTER"></asp:ListItem>
                                                                                <asp:ListItem Value="ccc"></asp:ListItem>
                                                                                <asp:ListItem Value="CEO"></asp:ListItem>
                                                                                <asp:ListItem Value="CFO"></asp:ListItem>
                                                                                <asp:ListItem Value="chairmen "></asp:ListItem>
                                                                                <asp:ListItem Value="Chakan"></asp:ListItem>
                                                                                <asp:ListItem Value="CHALAK TRANSPORT"></asp:ListItem>
                                                                                <asp:ListItem Value="Chankan"></asp:ListItem>
                                                                                <asp:ListItem Value="cheif Bussiness"></asp:ListItem>
                                                                                <asp:ListItem Value="chemist"></asp:ListItem>
                                                                                <asp:ListItem Value="Chennai"></asp:ListItem>
                                                                                <asp:ListItem Value="CHETTINAD"></asp:ListItem>
                                                                                <asp:ListItem Value="Chief Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Chief Executive officer "></asp:ListItem>
                                                                                <asp:ListItem Value="Chief Marketing"></asp:ListItem>
                                                                                <asp:ListItem Value="chikhali"></asp:ListItem>
                                                                                <asp:ListItem Value="Chinchawad"></asp:ListItem>
                                                                                <asp:ListItem Value="CHINTAMANI ELECTRICALS AND WORKS"></asp:ListItem>
                                                                                <asp:ListItem Value="chintamani transport"></asp:ListItem>
                                                                                <asp:ListItem Value="CHUCK"></asp:ListItem>
                                                                                <asp:ListItem Value="civil manager"></asp:ListItem>
                                                                                <asp:ListItem Value="CLEAN MAX ENVIRO ENERGY SOLUTIONS "></asp:ListItem>
                                                                                <asp:ListItem Value="cm"></asp:ListItem>
                                                                                <asp:ListItem Value="CMD"></asp:ListItem>
                                                                                <asp:ListItem Value="CMIE"></asp:ListItem>
                                                                                <asp:ListItem Value="CNC OPRATOR"></asp:ListItem>
                                                                                <asp:ListItem Value="co"></asp:ListItem>
                                                                                <asp:ListItem Value="CO OWNER"></asp:ListItem>
                                                                                <asp:ListItem Value="CO PARTY"></asp:ListItem>
                                                                                <asp:ListItem Value="College"></asp:ListItem>
                                                                                <asp:ListItem Value="COMAPNY"></asp:ListItem>
                                                                                <asp:ListItem Value="Commercial Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Commodity Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="compan"></asp:ListItem>
                                                                                <asp:ListItem Value="COMPANY"></asp:ListItem>
                                                                                <asp:ListItem Value="Company Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="COMPANY OWNER"></asp:ListItem>
                                                                                <asp:ListItem Value="Company Secratorary"></asp:ListItem>
                                                                                <asp:ListItem Value="compony"></asp:ListItem>
                                                                                <asp:ListItem Value="CONCISE ENGINEERING SOLUTIONS PVT LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="Consultant"></asp:ListItem>
                                                                                <asp:ListItem Value="Consultant Admistration"></asp:ListItem>
                                                                                <asp:ListItem Value="CONTRACTOR"></asp:ListItem>
                                                                                <asp:ListItem Value="COO"></asp:ListItem>
                                                                                <asp:ListItem Value="COORDINATION  OFFICER"></asp:ListItem>
                                                                                <asp:ListItem Value="COROMANDEL"></asp:ListItem>
                                                                                <asp:ListItem Value="Costing and purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Country Manager India"></asp:ListItem>
                                                                                <asp:ListItem Value="courier"></asp:ListItem>
                                                                                <asp:ListItem Value="COUSTMER"></asp:ListItem>
                                                                                <asp:ListItem Value="CPPM"></asp:ListItem>
                                                                                <asp:ListItem Value="CTC MR"></asp:ListItem>
                                                                                <asp:ListItem Value="CUSTOMER"></asp:ListItem>
                                                                                <asp:ListItem Value="CUSTOMER CARE"></asp:ListItem>
                                                                                <asp:ListItem Value="customer support"></asp:ListItem>
                                                                                <asp:ListItem Value="Customer Support Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Customer Support Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="CV"></asp:ListItem>
                                                                                <asp:ListItem Value="d"></asp:ListItem>
                                                                                <asp:ListItem Value="DANGOTE"></asp:ListItem>
                                                                                <asp:ListItem Value="DANGOTE INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="DAPODI"></asp:ListItem>
                                                                                <asp:ListItem Value="DEALER"></asp:ListItem>
                                                                                <asp:ListItem Value="del"></asp:ListItem>
                                                                                <asp:ListItem Value="DELEAR"></asp:ListItem>
                                                                                <asp:ListItem Value="DELIVERY ACCURATE TOOLS AND FIXTURES"></asp:ListItem>
                                                                                <asp:ListItem Value="DELIVRY BOY"></asp:ListItem>
                                                                                <asp:ListItem Value="DELTA FLOW"></asp:ListItem>
                                                                                <asp:ListItem Value="Dept Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Dept Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="DEPUTI MANAGER DEVELOPMENT "></asp:ListItem>
                                                                                <asp:ListItem Value="DEPUTY DIRECTOR"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy G M"></asp:ListItem>
                                                                                <asp:ListItem Value="deputy general manager procurement"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager  Instrumentation"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager  Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager Design"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager Enterprise Sales "></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager QC"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager Quality Assurance"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager Sales"></asp:ListItem>
                                                                                <asp:ListItem Value="DEPUTY MANAGER SBU QUALITY"></asp:ListItem>
                                                                                <asp:ListItem Value="Deputy Manager Sourcing "></asp:ListItem>
                                                                                <asp:ListItem Value="DESIGN"></asp:ListItem>
                                                                                <asp:ListItem Value="Design  Development  Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="design and proposal"></asp:ListItem>
                                                                                <asp:ListItem Value="Design DEP"></asp:ListItem>
                                                                                <asp:ListItem Value="Design Deptt"></asp:ListItem>
                                                                                <asp:ListItem Value="Design Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="DESIGN ENGR"></asp:ListItem>
                                                                                <asp:ListItem Value="DESIGN HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="DESIGN LEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="Designation Partner"></asp:ListItem>
                                                                                <asp:ListItem Value="DESIGNER"></asp:ListItem>
                                                                                <asp:ListItem Value="DESINATION"></asp:ListItem>
                                                                                <asp:ListItem Value="Developement "></asp:ListItem>
                                                                                <asp:ListItem Value="Development Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Development Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Development Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Development Officer"></asp:ListItem>
                                                                                <asp:ListItem Value="DEVMANGAL"></asp:ListItem>
                                                                                <asp:ListItem Value="dfga"></asp:ListItem>
                                                                                <asp:ListItem Value="dfgd"></asp:ListItem>
                                                                                <asp:ListItem Value="dfgdzf"></asp:ListItem>
                                                                                <asp:ListItem Value="DFHDH"></asp:ListItem>
                                                                                <asp:ListItem Value="DG SOLAR"></asp:ListItem>
                                                                                <asp:ListItem Value="DGM"></asp:ListItem>
                                                                                <asp:ListItem Value="DGM Customer Service"></asp:ListItem>
                                                                                <asp:ListItem Value="DGM Production"></asp:ListItem>
                                                                                <asp:ListItem Value="DGM purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="DHRUV"></asp:ListItem>
                                                                                <asp:ListItem Value="DIPATCH"></asp:ListItem>
                                                                                <asp:ListItem Value="DIPESH BHOSALE"></asp:ListItem>
                                                                                <asp:ListItem Value="Dire"></asp:ListItem>
                                                                                <asp:ListItem Value="Director"></asp:ListItem>
                                                                                <asp:ListItem Value="Director C AND MM"></asp:ListItem>
                                                                                <asp:ListItem Value="Director International Business"></asp:ListItem>
                                                                                <asp:ListItem Value="Director Of CEMS"></asp:ListItem>
                                                                                <asp:ListItem Value="Director of Operations"></asp:ListItem>
                                                                                <asp:ListItem Value="Director Operations"></asp:ListItem>
                                                                                <asp:ListItem Value="Director Plant"></asp:ListItem>
                                                                                <asp:ListItem Value="DIRECTOR SALES AND MKT"></asp:ListItem>
                                                                                <asp:ListItem Value="Direv"></asp:ListItem>
                                                                                <asp:ListItem Value="DISHACHI ENERGY"></asp:ListItem>
                                                                                <asp:ListItem Value="Ditrector"></asp:ListItem>
                                                                                <asp:ListItem Value="DIVISION HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="Divyesh Vora"></asp:ListItem>
                                                                                <asp:ListItem Value="DMP PRECIPART"></asp:ListItem>
                                                                                <asp:ListItem Value="Doctor"></asp:ListItem>
                                                                                <asp:ListItem Value="DODSAL"></asp:ListItem>
                                                                                <asp:ListItem Value="Dr"></asp:ListItem>
                                                                                <asp:ListItem Value="DREAMERS WORLD SMART SOLUTIONS LLP"></asp:ListItem>
                                                                                <asp:ListItem Value="Driver"></asp:ListItem>
                                                                                <asp:ListItem Value="DURACOOL"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy  Manager  Maintenance and Control Panel"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy  Manager  Maintenance and Safety"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy  Manager  Production"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Director"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy General Manager Commercial"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy General Manager Instrumentation"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy General Manager Quality"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Manager Admin"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Manager HR"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Manager materials"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Manager Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Manager SCM"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Mgr Strategic Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Dy Sr Manager Purchase and development"></asp:ListItem>
                                                                                <asp:ListItem Value="DYNAMIC ENGINEERING AND SOLUTIONS"></asp:ListItem>
                                                                                <asp:ListItem Value="e"></asp:ListItem>
                                                                                <asp:ListItem Value="E and S Purchase"></asp:ListItem>

                                                                                <asp:ListItem Value="ECO"></asp:ListItem>
                                                                                <asp:ListItem Value="ECOSOURCE"></asp:ListItem>
                                                                                <asp:ListItem Value="ECOTECH "></asp:ListItem>
                                                                                <asp:ListItem Value="EFFICIENT"></asp:ListItem>
                                                                                <asp:ListItem Value="EFFICIIENT TECHNOLOGIES"></asp:ListItem>
                                                                                <asp:ListItem Value="Electrical Deptt Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Electrical Design"></asp:ListItem>
                                                                                <asp:ListItem Value="Electrical Design Dept"></asp:ListItem>
                                                                                <asp:ListItem Value="ELECTRICAL ENGG"></asp:ListItem>
                                                                                <asp:ListItem Value="ELECTRICAL ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="ELECTRICIAN"></asp:ListItem>
                                                                                <asp:ListItem Value="ELETRICIAN"></asp:ListItem>
                                                                                <asp:ListItem Value="ELEX INDIA"></asp:ListItem>
                                                                                <asp:ListItem Value="EMERSON"></asp:ListItem>
                                                                                <asp:ListItem Value="EMPIRE MARKETING SERVISES"></asp:ListItem>
                                                                                <asp:ListItem Value="EMPLOYEE"></asp:ListItem>
                                                                                <asp:ListItem Value="ENERGICA"></asp:ListItem>
                                                                                <asp:ListItem Value="ENEXIO"></asp:ListItem>
                                                                                <asp:ListItem Value="ENGG"></asp:ListItem>
                                                                                <asp:ListItem Value="ENGINEER "></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Assembly"></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Design"></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Design and Estimation"></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Development"></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Materials "></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Materials Department"></asp:ListItem>
                                                                                <asp:ListItem Value="ENGINEER no call"></asp:ListItem>
                                                                                <asp:ListItem Value="ENGINEER PROCUREMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Engineer Service Head"></asp:ListItem>
                                                                                <asp:ListItem Value="ENGINEER TRAINEE"></asp:ListItem>
                                                                                <asp:ListItem Value="ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="Engineering Dept"></asp:ListItem>
                                                                                <asp:ListItem Value="ENGINEERING MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="EngineerPurchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Enterprise Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="ENVICARE"></asp:ListItem>
                                                                                <asp:ListItem Value="Equipment Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="er Sm"></asp:ListItem>
                                                                                <asp:ListItem Value="ERA"></asp:ListItem>
                                                                                <asp:ListItem Value="ESOLUTION"></asp:ListItem>
                                                                                <asp:ListItem Value="estimation and proposal"></asp:ListItem>
                                                                                <asp:ListItem Value="EVENTIDE"></asp:ListItem>
                                                                                <asp:ListItem Value="Ex  Executive Quality"></asp:ListItem>
                                                                                <asp:ListItem Value="Excutive"></asp:ListItem>
                                                                                <asp:ListItem Value="EXECUTIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive  Director"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive  Electrical  Automation"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive Central Marketing and Tendering Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="EXECUTIVE CHAIRMAN"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive Director"></asp:ListItem>
                                                                                <asp:ListItem Value="executive engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive Manufacturing Engineering"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive Marketing "></asp:ListItem>
                                                                                <asp:ListItem Value="EXECUTIVE MATERIAL"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive Procurement"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive Production"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive Production Sourcing"></asp:ListItem>
                                                                                <asp:ListItem Value="EXECUTIVE PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="Executive SCM"></asp:ListItem>
                                                                                <asp:ListItem Value="EXTREME"></asp:ListItem>
                                                                                <asp:ListItem Value="EXTREME CONTROL SYSTEMS"></asp:ListItem>
                                                                                <asp:ListItem Value="f"></asp:ListItem>
                                                                                <asp:ListItem Value="FABRICATEX "></asp:ListItem>
                                                                                <asp:ListItem Value="fabrication"></asp:ListItem>
                                                                                <asp:ListItem Value="FACTORY CORDINATOR"></asp:ListItem>
                                                                                <asp:ListItem Value="Factory Executive "></asp:ListItem>
                                                                                <asp:ListItem Value="Factory Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="FAIZ"></asp:ListItem>
                                                                                <asp:ListItem Value="FALCON"></asp:ListItem>
                                                                                <asp:ListItem Value="fd"></asp:ListItem>
                                                                                <asp:ListItem Value="FERRO"></asp:ListItem>
                                                                                <asp:ListItem Value="fhsf"></asp:ListItem>
                                                                                <asp:ListItem Value="Field Application Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="FIITING"></asp:ListItem>
                                                                                <asp:ListItem Value="Finance and Administration"></asp:ListItem>
                                                                                <asp:ListItem Value="FINANCE MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="Finance Officer"></asp:ListItem>
                                                                                <asp:ListItem Value="FIRM"></asp:ListItem>
                                                                                <asp:ListItem Value="FLAIR"></asp:ListItem>
                                                                                <asp:ListItem Value="FOOD"></asp:ListItem>
                                                                                <asp:ListItem Value="Founder"></asp:ListItem>
                                                                                <asp:ListItem Value="fsd"></asp:ListItem>
                                                                                <asp:ListItem Value="G"></asp:ListItem>
                                                                                <asp:ListItem Value="GANESH "></asp:ListItem>
                                                                                <asp:ListItem Value="GANGO"></asp:ListItem>
                                                                                <asp:ListItem Value="GARAGE"></asp:ListItem>
                                                                                <asp:ListItem Value="GARSIM"></asp:ListItem>
                                                                                <asp:ListItem Value="gdr"></asp:ListItem>
                                                                                <asp:ListItem Value="Gen Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="GENERAL MANAAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="General Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="General Manager Process Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="General Manager Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Genral Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="GH"></asp:ListItem>
                                                                                <asp:ListItem Value="GLOBAL ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="global procurment "></asp:ListItem>
                                                                                <asp:ListItem Value="GM"></asp:ListItem>
                                                                                <asp:ListItem Value="GM  Commercial "></asp:ListItem>
                                                                                <asp:ListItem Value="GM Fabrication"></asp:ListItem>
                                                                                <asp:ListItem Value="GM Operations"></asp:ListItem>
                                                                                <asp:ListItem Value="GM Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="GM Projects Operations"></asp:ListItem>
                                                                                <asp:ListItem Value="GM Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="GPRL"></asp:ListItem>
                                                                                <asp:ListItem Value="GRAMPANCHAYT"></asp:ListItem>
                                                                                <asp:ListItem Value="GRASIM INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="GRIFFYN ROBOT"></asp:ListItem>
                                                                                <asp:ListItem Value="GROUP"></asp:ListItem>
                                                                                <asp:ListItem Value="GST VERIFIED"></asp:ListItem>
                                                                                <asp:ListItem Value="GUPTA ELECTRICALS"></asp:ListItem>
                                                                                <asp:ListItem Value="GURAV"></asp:ListItem>
                                                                                <asp:ListItem Value="h"></asp:ListItem>
                                                                                <asp:ListItem Value="HARDWARE"></asp:ListItem>
                                                                                <asp:ListItem Value="HARSH"></asp:ListItem>
                                                                                <asp:ListItem Value="HARSHADA ELECTRICALS"></asp:ListItem>
                                                                                <asp:ListItem Value="HAVELI"></asp:ListItem>
                                                                                <asp:ListItem Value="HCD"></asp:ListItem>
                                                                                <asp:ListItem Value="HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="Head Business Development"></asp:ListItem>
                                                                                <asp:ListItem Value="Head Costing and PPC"></asp:ListItem>
                                                                                <asp:ListItem Value="Head D AND I "></asp:ListItem>
                                                                                <asp:ListItem Value="Head Design"></asp:ListItem>
                                                                                <asp:ListItem Value="HEAD Manufacturing Engineering"></asp:ListItem>
                                                                                <asp:ListItem Value="Head Marketing"></asp:ListItem>
                                                                                <asp:ListItem Value="Head MMG"></asp:ListItem>
                                                                                <asp:ListItem Value="HEAD OF DEP"></asp:ListItem>
                                                                                <asp:ListItem Value="Head of Process Engg and QA "></asp:ListItem>
                                                                                <asp:ListItem Value="Head of Product Engineering and Execution"></asp:ListItem>
                                                                                <asp:ListItem Value="Head of Product Management"></asp:ListItem>
                                                                                <asp:ListItem Value="Head Operation"></asp:ListItem>
                                                                                <asp:ListItem Value="Head Product Management"></asp:ListItem>
                                                                                <asp:ListItem Value="Head Project sales HVAC Vertical"></asp:ListItem>
                                                                                <asp:ListItem Value="Head Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="Head Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="HEAD QUALITY ASSURANCE"></asp:ListItem>
                                                                                <asp:ListItem Value="HENKEL"></asp:ListItem>
                                                                                <asp:ListItem Value="HEW PRECISION WORKS PVT LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="HINDUSTAN AUTOMATION"></asp:ListItem>
                                                                                <asp:ListItem Value="HOD "></asp:ListItem>
                                                                                <asp:ListItem Value="HOD BU"></asp:ListItem>
                                                                                <asp:ListItem Value="HOD Instrumentation"></asp:ListItem>
                                                                                <asp:ListItem Value="Home"></asp:ListItem>
                                                                                <asp:ListItem Value="HORIBA INDIA"></asp:ListItem>
                                                                                <asp:ListItem Value="Hospital "></asp:ListItem>
                                                                                <asp:ListItem Value="HOTEL DJ"></asp:ListItem>
                                                                                <asp:ListItem Value="HOTEL MONIKA"></asp:ListItem>
                                                                                <asp:ListItem Value="HOTEL TRUPTI"></asp:ListItem>
                                                                                <asp:ListItem Value="HOTEL VIRAJ"></asp:ListItem>
                                                                                <asp:ListItem Value="HOTEL VIRAJ AND SNACKS CENTER"></asp:ListItem>
                                                                                <asp:ListItem Value="HOUSE OWNER"></asp:ListItem>
                                                                                <asp:ListItem Value="HR"></asp:ListItem>
                                                                                <asp:ListItem Value="HR and Admin Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="HRUSHIKESH"></asp:ListItem>
                                                                                <asp:ListItem Value="HSHREE ENGINEERS AND CONSULTANTS"></asp:ListItem>
                                                                                <asp:ListItem Value="HYPER POWERTRON"></asp:ListItem>
                                                                                <asp:ListItem Value="HYT"></asp:ListItem>
                                                                                <asp:ListItem Value="I"></asp:ListItem>
                                                                                <asp:ListItem Value="ICC REALTY"></asp:ListItem>
                                                                                <asp:ListItem Value="IFPL"></asp:ListItem>
                                                                                <asp:ListItem Value="import department "></asp:ListItem>
                                                                                <asp:ListItem Value="IMPORTS EXECUTIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="INCHARGE"></asp:ListItem>
                                                                                <asp:ListItem Value="Incharge Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="INDIA CABLES"></asp:ListItem>
                                                                                <asp:ListItem Value="Indirect Purchase Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="INDMARK"></asp:ListItem>
                                                                                <asp:ListItem Value="INDORE"></asp:ListItem>
                                                                                <asp:ListItem Value="INDSUN ECO SYSTEMS"></asp:ListItem>
                                                                                <asp:ListItem Value="industry"></asp:ListItem>
                                                                                <asp:ListItem Value="INFI"></asp:ListItem>
                                                                                <asp:ListItem Value="INFINITY"></asp:ListItem>
                                                                                <asp:ListItem Value="INOX AIR"></asp:ListItem>
                                                                                <asp:ListItem Value="INSIDE SALES"></asp:ListItem>
                                                                                <asp:ListItem Value="Inside Sales Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="INSPIRED "></asp:ListItem>
                                                                                <asp:ListItem Value="Institute "></asp:ListItem>
                                                                                <asp:ListItem Value="INSTRUMENT ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="Instrument Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Instrumentation"></asp:ListItem>
                                                                                <asp:ListItem Value="INTERIO DIVISION "></asp:ListItem>
                                                                                <asp:ListItem Value="INTERIOR DESIGNER"></asp:ListItem>
                                                                                <asp:ListItem Value="INTERNATIONA"></asp:ListItem>
                                                                                <asp:ListItem Value="INTORQ INDIA"></asp:ListItem>
                                                                                <asp:ListItem Value="INTOWELLNESS"></asp:ListItem>
                                                                                <asp:ListItem Value="ION"></asp:ListItem>
                                                                                <asp:ListItem Value="ISGEC"></asp:ListItem>
                                                                                <asp:ListItem Value="J K INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="JAI GANESH"></asp:ListItem>
                                                                                <asp:ListItem Value="JAI MATA BHAVANI ENGITECH PVT LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="Jaipur"></asp:ListItem>
                                                                                <asp:ListItem Value="JAY"></asp:ListItem>
                                                                                <asp:ListItem Value="JGM"></asp:ListItem>
                                                                                <asp:ListItem Value="Jiteen Engineering works"></asp:ListItem>
                                                                                <asp:ListItem Value="JOB INSPECTION"></asp:ListItem>
                                                                                <asp:ListItem Value="job work only"></asp:ListItem>
                                                                                <asp:ListItem Value="JOYTI"></asp:ListItem>
                                                                                <asp:ListItem Value="Jr Asst Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="JR ENGINEER PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="Jr EXECUTIVE PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="Jr Purchase Assistant"></asp:ListItem>
                                                                                <asp:ListItem Value="jrj"></asp:ListItem>
                                                                                <asp:ListItem Value="Junior Scientist"></asp:ListItem>
                                                                                <asp:ListItem Value="JYOTI LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="K"></asp:ListItem>
                                                                                <asp:ListItem Value="KAIVALYA ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="KALASHREE"></asp:ListItem>
                                                                                <asp:ListItem Value="KALPA"></asp:ListItem>
                                                                                <asp:ListItem Value="KALPAK"></asp:ListItem>
                                                                                <asp:ListItem Value="KANTI BIJLEE UTPADAN NIGAM LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="KARTU"></asp:ListItem>
                                                                                <asp:ListItem Value="Key Account Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="KGN"></asp:ListItem>
                                                                                <asp:ListItem Value="KHANDALA"></asp:ListItem>
                                                                                <asp:ListItem Value="KIMURA"></asp:ListItem>
                                                                                <asp:ListItem Value="KIRLOSKA"></asp:ListItem>
                                                                                <asp:ListItem Value="Kolhapur"></asp:ListItem>
                                                                                <asp:ListItem Value="KONSTELEC ENGINEERS"></asp:ListItem>
                                                                                <asp:ListItem Value="KRISAM"></asp:ListItem>
                                                                                <asp:ListItem Value="KRISAM AUTOMATION"></asp:ListItem>
                                                                                <asp:ListItem Value="KRISTL "></asp:ListItem>
                                                                                <asp:ListItem Value="KUKREJA"></asp:ListItem>
                                                                                <asp:ListItem Value="KWALITY"></asp:ListItem>
                                                                                <asp:ListItem Value="L"></asp:ListItem>
                                                                                <asp:ListItem Value="l and l"></asp:ListItem>
                                                                                <asp:ListItem Value="Lab Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Lab Incharge"></asp:ListItem>
                                                                                <asp:ListItem Value="LABOUR"></asp:ListItem>
                                                                                <asp:ListItem Value="LAPTOP"></asp:ListItem>
                                                                                <asp:ListItem Value="Laser Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Lead Engineer Indirect Purchasing"></asp:ListItem>
                                                                                <asp:ListItem Value="LEAD PURCHASER"></asp:ListItem>
                                                                                <asp:ListItem Value="LEADEC"></asp:ListItem>
                                                                                <asp:ListItem Value="Leader Manufacturing and QC"></asp:ListItem>
                                                                                <asp:ListItem Value="LEADER VENDOR"></asp:ListItem>
                                                                                <asp:ListItem Value="LECTRICAL"></asp:ListItem>
                                                                                <asp:ListItem Value="LINDE INDIA LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="LO"></asp:ListItem>
                                                                                <asp:ListItem Value="Logicstic Manger"></asp:ListItem>
                                                                                <asp:ListItem Value="Ludhiana"></asp:ListItem>
                                                                                <asp:ListItem Value="M"></asp:ListItem>
                                                                                <asp:ListItem Value="M D"></asp:ListItem>
                                                                                <asp:ListItem Value="M R ELECTRIC"></asp:ListItem>
                                                                                <asp:ListItem Value="Machine Maintance"></asp:ListItem>
                                                                                <asp:ListItem Value="MAESTROTECH "></asp:ListItem>
                                                                                <asp:ListItem Value="Maganer"></asp:ListItem>
                                                                                <asp:ListItem Value="MAGNUM"></asp:ListItem>
                                                                                <asp:ListItem Value="MAHALAXMI ELECTROMECH "></asp:ListItem>
                                                                                <asp:ListItem Value="Mahavidyalaya"></asp:ListItem>
                                                                                <asp:ListItem Value="Main Acountant"></asp:ListItem>
                                                                                <asp:ListItem Value="Maint Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Maint Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="MAINTANACE"></asp:ListItem>
                                                                                <asp:ListItem Value="MAINTANCE"></asp:ListItem>
                                                                                <asp:ListItem Value="MAINTENANCE"></asp:ListItem>
                                                                                <asp:ListItem Value="Major"></asp:ListItem>
                                                                                <asp:ListItem Value="MALAD"></asp:ListItem>
                                                                                <asp:ListItem Value="Malak "></asp:ListItem>
                                                                                <asp:ListItem Value="MALHOTRA ENGINEERS"></asp:ListItem>
                                                                                <asp:ListItem Value="mam"></asp:ListItem>
                                                                                <asp:ListItem Value="MANA"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGAE"></asp:ListItem>
                                                                                <asp:ListItem Value="managaer"></asp:ListItem>
                                                                                <asp:ListItem Value="Management Representitive"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager  Application EngineeR"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER  PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager  R AND D"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager  Strategic Procurement"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Accts"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Admin Procurement"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Administration"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Business Development"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER DESIGN"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Design  Engineering"></asp:ListItem>
                                                                                <asp:ListItem Value="manager design and tool room"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER DEVELOPMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER DIRECTOR"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Elect  Electronics "></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Electrical"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER INDUSTRIAL BUSINESS"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER M P C"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Manufacturing Engineering"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Marketing"></asp:ListItem>
                                                                                <asp:ListItem Value="manager materials"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Mtrls"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Operations"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager oxygen plant"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Paint Shop"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER PROCUREMENT LOGISTICS"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Production"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Production Engineering"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Proposal"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER PURCHASE AND STORE"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager QA"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER QA QC"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Quality"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Quality Assurance "></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Sales"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Sales  Marketing"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Sourcing Dept PSM"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER SUPPLY CHAIN"></asp:ListItem>
                                                                                <asp:ListItem Value="Manager Technical"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGER TOO LROOM"></asp:ListItem>
                                                                                <asp:ListItem Value="MANAGING  DIRECTOR"></asp:ListItem>
                                                                                <asp:ListItem Value="Managing Directo"></asp:ListItem>
                                                                                <asp:ListItem Value="Managing Director"></asp:ListItem>
                                                                                <asp:ListItem Value="Managing Partner"></asp:ListItem>
                                                                                <asp:ListItem Value="manegar"></asp:ListItem>
                                                                                <asp:ListItem Value="MANEGER "></asp:ListItem>
                                                                                <asp:ListItem Value="MANEGER QUALITY"></asp:ListItem>
                                                                                <asp:ListItem Value="MANGER"></asp:ListItem>
                                                                                <asp:ListItem Value="MANGER EXIM"></asp:ListItem>
                                                                                <asp:ListItem Value="Manger Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="MANISHA"></asp:ListItem>
                                                                                <asp:ListItem Value="manneger"></asp:ListItem>
                                                                                <asp:ListItem Value="MANU"></asp:ListItem>
                                                                                <asp:ListItem Value="MANUFACTURING"></asp:ListItem>
                                                                                <asp:ListItem Value="Manufacturing Engineering"></asp:ListItem>
                                                                                <asp:ListItem Value="Marketer"></asp:ListItem>
                                                                                <asp:ListItem Value="MARKETING"></asp:ListItem>
                                                                                <asp:ListItem Value="marketing and Application Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Marketing coordinator"></asp:ListItem>
                                                                                <asp:ListItem Value="Marketing Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Marketing Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Marketing Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="Marketing Manager Distributor Network"></asp:ListItem>
                                                                                <asp:ListItem Value="Marketing Sales  Customer Relationship Management "></asp:ListItem>
                                                                                <asp:ListItem Value="MARUTI"></asp:ListItem>
                                                                                <asp:ListItem Value="MASS DYE CHEM"></asp:ListItem>
                                                                                <asp:ListItem Value="MATERE"></asp:ListItem>
                                                                                <asp:ListItem Value="Material"></asp:ListItem>
                                                                                <asp:ListItem Value="Material and Validation"></asp:ListItem>
                                                                                <asp:ListItem Value="MATERIAL HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="material manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Materials"></asp:ListItem>
                                                                                <asp:ListItem Value="Materials Department"></asp:ListItem>
                                                                                <asp:ListItem Value="Materials Dept"></asp:ListItem>
                                                                                <asp:ListItem Value="Materials QA "></asp:ListItem>
                                                                                <asp:ListItem Value="MATHA ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="MAULI INDUSTRIAL SUPPLIERS"></asp:ListItem>
                                                                                <asp:ListItem Value="MCT Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="MD"></asp:ListItem>
                                                                                <asp:ListItem Value="MECGALE"></asp:ListItem>
                                                                                <asp:ListItem Value="MECH DEP"></asp:ListItem>
                                                                                <asp:ListItem Value="Mechanical Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Mechanical Engineer Design"></asp:ListItem>
                                                                                <asp:ListItem Value="MECHELEIN ENGINEERS"></asp:ListItem>
                                                                                <asp:ListItem Value="Medical "></asp:ListItem>
                                                                                <asp:ListItem Value="Medical Supritendent"></asp:ListItem>
                                                                                <asp:ListItem Value="MEINTANANCE"></asp:ListItem>
                                                                                <asp:ListItem Value="member"></asp:ListItem>
                                                                                <asp:ListItem Value="Met Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Metallurgist"></asp:ListItem>
                                                                                <asp:ListItem Value="Metallurgy  HT"></asp:ListItem>
                                                                                <asp:ListItem Value="Mfg"></asp:ListItem>
                                                                                <asp:ListItem Value="MG"></asp:ListItem>
                                                                                <asp:ListItem Value="MG ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="Mgr NPD"></asp:ListItem>
                                                                                <asp:ListItem Value="MICRO"></asp:ListItem>
                                                                                <asp:ListItem Value="MICRO SENSE EARTHING"></asp:ListItem>
                                                                                <asp:ListItem Value="MIDC BHOSARI"></asp:ListItem>
                                                                                <asp:ListItem Value="milind"></asp:ListItem>
                                                                                <asp:ListItem Value="MK"></asp:ListItem>
                                                                                <asp:ListItem Value="Mktg"></asp:ListItem>
                                                                                <asp:ListItem Value="MNC AUTOMATION"></asp:ListItem>
                                                                                <asp:ListItem Value="MOTION TECHNOLOGY"></asp:ListItem>
                                                                                <asp:ListItem Value="MQC HT"></asp:ListItem>
                                                                                <asp:ListItem Value="MR"></asp:ListItem>
                                                                                <asp:ListItem Value="Mr b"></asp:ListItem>
                                                                                <asp:ListItem Value="MS BHARAN"></asp:ListItem>
                                                                                <asp:ListItem Value="Mumbai"></asp:ListItem>
                                                                                <asp:ListItem Value="N"></asp:ListItem>
                                                                                <asp:ListItem Value="N A"></asp:ListItem>
                                                                                <asp:ListItem Value="NA"></asp:ListItem>
                                                                                <asp:ListItem Value="NAGPUR"></asp:ListItem>
                                                                                <asp:ListItem Value="NASHIK"></asp:ListItem>
                                                                                <asp:ListItem Value="NATH"></asp:ListItem>
                                                                                <asp:ListItem Value="NATIONAL "></asp:ListItem>
                                                                                <asp:ListItem Value="Navi Mumbai"></asp:ListItem>
                                                                                <asp:ListItem Value="NAVNATH"></asp:ListItem>
                                                                                <asp:ListItem Value="NEOS"></asp:ListItem>
                                                                                <asp:ListItem Value="NEPTUNE ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="NEW"></asp:ListItem>
                                                                                <asp:ListItem Value="New Development Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="NEXT"></asp:ListItem>
                                                                                <asp:ListItem Value="NG"></asp:ListItem>
                                                                                <asp:ListItem Value="Nil"></asp:ListItem>
                                                                                <asp:ListItem Value="NIRMAN"></asp:ListItem>
                                                                                <asp:ListItem Value="NITESH "></asp:ListItem>
                                                                                <asp:ListItem Value="NITROJAIN"></asp:ListItem>
                                                                                <asp:ListItem Value="NK ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="no"></asp:ListItem>
                                                                                <asp:ListItem Value="NON"></asp:ListItem>
                                                                                <asp:ListItem Value="none"></asp:ListItem>
                                                                                <asp:ListItem Value="NOVEL"></asp:ListItem>
                                                                                <asp:ListItem Value="NPD  Asst Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="NPD Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="NR INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="NUCLEAR POWER"></asp:ListItem>
                                                                                <asp:ListItem Value="O"></asp:ListItem>
                                                                                <asp:ListItem Value="OFC"></asp:ListItem>
                                                                                <asp:ListItem Value="OFF"></asp:ListItem>
                                                                                <asp:ListItem Value="OFFICE"></asp:ListItem>
                                                                                <asp:ListItem Value="OFFICER"></asp:ListItem>
                                                                                <asp:ListItem Value="Officer MIS"></asp:ListItem>
                                                                                <asp:ListItem Value="OFFICER PROCUREMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="Officer Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Officer Purchase Engineering"></asp:ListItem>
                                                                                <asp:ListItem Value="OFICE"></asp:ListItem>
                                                                                <asp:ListItem Value="Oil"></asp:ListItem>
                                                                                <asp:ListItem Value="OM"></asp:ListItem>
                                                                                <asp:ListItem Value="OM AUTOMATION AND SERVICES"></asp:ListItem>
                                                                                <asp:ListItem Value="OM KRANTI FABRICATION  ENGINEERS"></asp:ListItem>
                                                                                <asp:ListItem Value="OMEGA"></asp:ListItem>
                                                                                <asp:ListItem Value="ONE UP"></asp:ListItem>
                                                                                <asp:ListItem Value="ONEAR"></asp:ListItem>
                                                                                <asp:ListItem Value="ONER"></asp:ListItem>
                                                                                <asp:ListItem Value="ONLINE"></asp:ListItem>
                                                                                <asp:ListItem Value="ONLINE SHOPPING"></asp:ListItem>
                                                                                <asp:ListItem Value="ONWER "></asp:ListItem>
                                                                                <asp:ListItem Value="Operation In charge"></asp:ListItem>
                                                                                <asp:ListItem Value="Operation Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Operations Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Operations Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Operations Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="OPERATOR"></asp:ListItem>
                                                                                <asp:ListItem Value="OPERETOR"></asp:ListItem>
                                                                                <asp:ListItem Value="OT incharge"></asp:ListItem>
                                                                                <asp:ListItem Value="OTHER"></asp:ListItem>
                                                                                <asp:ListItem Value="Ourchase"></asp:ListItem>
                                                                                <asp:ListItem Value="OURSOURCING"></asp:ListItem>
                                                                                <asp:ListItem Value="OUTPUT ENERGY "></asp:ListItem>
                                                                                <asp:ListItem Value="OWENER"></asp:ListItem>
                                                                                <asp:ListItem Value="Ower"></asp:ListItem>
                                                                                <asp:ListItem Value="OWMER"></asp:ListItem>
                                                                                <asp:ListItem Value="OWN"></asp:ListItem>
                                                                                <asp:ListItem Value="owne"></asp:ListItem>
                                                                                <asp:ListItem Value="Owner"></asp:ListItem>
                                                                                <asp:ListItem Value="OWNNER"></asp:ListItem>
                                                                                <asp:ListItem Value="P"></asp:ListItem>
                                                                                <asp:ListItem Value="P R ELECTRICALS"></asp:ListItem>
                                                                                <asp:ListItem Value="P S CONTROL"></asp:ListItem>
                                                                                <asp:ListItem Value="PANCH"></asp:ListItem>
                                                                                <asp:ListItem Value="PANCHSHIL"></asp:ListItem>
                                                                                <asp:ListItem Value="PANCHSHIL INFRASTRUCTURE HOLDINGS"></asp:ListItem>
                                                                                <asp:ListItem Value="Partner"></asp:ListItem>
                                                                                <asp:ListItem Value="PATERNER"></asp:ListItem>
                                                                                <asp:ListItem Value="Patil"></asp:ListItem>
                                                                                <asp:ListItem Value="PEHCHAN KA "></asp:ListItem>
                                                                                <asp:ListItem Value="PHULE SOLAR SYSTEM"></asp:ListItem>
                                                                                <asp:ListItem Value="PIDILITE"></asp:ListItem>
                                                                                <asp:ListItem Value="PILON"></asp:ListItem>
                                                                                <asp:ListItem Value="PILON ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="Pimpri"></asp:ListItem>
                                                                                <asp:ListItem Value="PINNACLE"></asp:ListItem>
                                                                                <asp:ListItem Value="PIRAMAL"></asp:ListItem>
                                                                                <asp:ListItem Value="PLAND HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="PLANE"></asp:ListItem>
                                                                                <asp:ListItem Value="Planing Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="PLANNING"></asp:ListItem>
                                                                                <asp:ListItem Value="PLANNING DEPT"></asp:ListItem>
                                                                                <asp:ListItem Value="Planning Purchase "></asp:ListItem>
                                                                                <asp:ListItem Value="PLANT HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="PLANT INCHARGE"></asp:ListItem>
                                                                                <asp:ListItem Value="PLANT MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="PLEXWARE"></asp:ListItem>
                                                                                <asp:ListItem Value="PLEXWARE AU"></asp:ListItem>
                                                                                <asp:ListItem Value="PLUS"></asp:ListItem>
                                                                                <asp:ListItem Value="PLUSCON "></asp:ListItem>
                                                                                <asp:ListItem Value="POLESTAR REF"></asp:ListItem>
                                                                                <asp:ListItem Value="POOJA "></asp:ListItem>
                                                                                <asp:ListItem Value="POONA"></asp:ListItem>
                                                                                <asp:ListItem Value="POWER "></asp:ListItem>
                                                                                <asp:ListItem Value="POWERSUN"></asp:ListItem>
                                                                                <asp:ListItem Value="PPC"></asp:ListItem>
                                                                                <asp:ListItem Value="PPC HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="PRAJ ELECTRICALS"></asp:ListItem>
                                                                                <asp:ListItem Value="PRAKASH"></asp:ListItem>
                                                                                <asp:ListItem Value="PRAMOD YALYE"></asp:ListItem>
                                                                                <asp:ListItem Value="PRANALI"></asp:ListItem>
                                                                                <asp:ListItem Value="PRATIBHA"></asp:ListItem>
                                                                                <asp:ListItem Value="PRD"></asp:ListItem>
                                                                                <asp:ListItem Value="PREMIER LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="president"></asp:ListItem>
                                                                                <asp:ListItem Value="Principal Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="PRIVILEGE"></asp:ListItem>
                                                                                <asp:ListItem Value="PROACTIVE FACILITY SERVICES"></asp:ListItem>
                                                                                <asp:ListItem Value="PROCECA"></asp:ListItem>
                                                                                <asp:ListItem Value="PROCESS DESIGN"></asp:ListItem>
                                                                                <asp:ListItem Value="Process Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="PROCRUEMENT "></asp:ListItem>
                                                                                <asp:ListItem Value="PROCRUEMENT ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="procurement"></asp:ListItem>
                                                                                <asp:ListItem Value="Procurement  Officer"></asp:ListItem>
                                                                                <asp:ListItem Value="Procurement Deptt"></asp:ListItem>
                                                                                <asp:ListItem Value="Procurement Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="Procurement Specialist"></asp:ListItem>
                                                                                <asp:ListItem Value="PRODUCT DEVELOPMENT ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="Product Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Product Engineering"></asp:ListItem>
                                                                                <asp:ListItem Value="Product Manager Development"></asp:ListItem>
                                                                                <asp:ListItem Value="PRODUCTION"></asp:ListItem>
                                                                                <asp:ListItem Value="Production Engineer "></asp:ListItem>
                                                                                <asp:ListItem Value="PRODUCTION INCHARGE"></asp:ListItem>
                                                                                <asp:ListItem Value="Production Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Production Mech Jobwork"></asp:ListItem>
                                                                                <asp:ListItem Value="Production Supervisor"></asp:ListItem>
                                                                                <asp:ListItem Value="PRODUCTON DEPT"></asp:ListItem>
                                                                                <asp:ListItem Value="Professor"></asp:ListItem>
                                                                                <asp:ListItem Value="programmer"></asp:ListItem>
                                                                                <asp:ListItem Value="PROJECT  SERVICE"></asp:ListItem>
                                                                                <asp:ListItem Value="Project and Application Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="PROJECT ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="PROJECT EXECUTIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="PROJECT HEAD "></asp:ListItem>
                                                                                <asp:ListItem Value="PROJECT LEADER"></asp:ListItem>
                                                                                <asp:ListItem Value="Project Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="PROJECT SUPPORT EXECUTIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="PROJEXEL"></asp:ListItem>
                                                                                <asp:ListItem Value="PROMPT TOOLS "></asp:ListItem>
                                                                                <asp:ListItem Value="PROP"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPARITER"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPARTER"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPARTIOR"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPERITOR"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPIETOR"></asp:ListItem>
                                                                                <asp:ListItem Value="Proposal Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPPRIETOR"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPR"></asp:ListItem>
                                                                                <asp:ListItem Value="Propraitor"></asp:ListItem>
                                                                                <asp:ListItem Value="Proprater"></asp:ListItem>
                                                                                <asp:ListItem Value="Propriator"></asp:ListItem>
                                                                                <asp:ListItem Value="Propriatot"></asp:ListItem>
                                                                                <asp:ListItem Value="Proprieor"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPRIETOR"></asp:ListItem>
                                                                                <asp:ListItem Value="Proprietorship"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPRIOTOR"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPRITER"></asp:ListItem>
                                                                                <asp:ListItem Value="PROPRITOR"></asp:ListItem>
                                                                                <asp:ListItem Value="PROSES HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="PROTECTIVE SLEEVE"></asp:ListItem>
                                                                                <asp:ListItem Value="PROTOTYPE"></asp:ListItem>
                                                                                <asp:ListItem Value="PROXIMITY ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="PRUD"></asp:ListItem>
                                                                                <asp:ListItem Value="PS"></asp:ListItem>
                                                                                <asp:ListItem Value="PU"></asp:ListItem>
                                                                                <asp:ListItem Value="PUCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="PUCHASE MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="PUECHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="pune"></asp:ListItem>
                                                                                <asp:ListItem Value="PUNE LASER TECHNOLOGY"></asp:ListItem>
                                                                                <asp:ListItem Value="Pune Marketing Head"></asp:ListItem>
                                                                                <asp:ListItem Value="punr"></asp:ListItem>
                                                                                <asp:ListItem Value="Purachase  HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="Purachase Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="PURACHSE"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCAHSE"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCCA"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchace Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHAGE"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHASE AND ACCOUNTS"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase and Admin Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHASE AND ESTIMATION ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHASE AND SUPPLIER DEVELOPMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="purchase associate"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase CEPL"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase Department"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase Dept"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase Director"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHASE ENGG"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase Engineer Machine Intelligence"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHASE EXECUTIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase Head"></asp:ListItem>
                                                                                <asp:ListItem Value="purchase incharge"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase Officer"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchase STAFF"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHASER"></asp:ListItem>
                                                                                <asp:ListItem Value="purchasing"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchasing Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHASW"></asp:ListItem>
                                                                                <asp:ListItem Value="Purches executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Purchess Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHHASE ENGINEENER"></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHSE "></asp:ListItem>
                                                                                <asp:ListItem Value="PURCHSE ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="PYRAMID ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="Q"></asp:ListItem>
                                                                                <asp:ListItem Value="Q A Department"></asp:ListItem>
                                                                                <asp:ListItem Value="Q A Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Q C"></asp:ListItem>
                                                                                <asp:ListItem Value="Q C Head"></asp:ListItem>
                                                                                <asp:ListItem Value="QA"></asp:ListItem>
                                                                                <asp:ListItem Value="QA  Components  Engineer "></asp:ListItem>
                                                                                <asp:ListItem Value="QA  Metallurgiest"></asp:ListItem>
                                                                                <asp:ListItem Value="QA ASSISTANT"></asp:ListItem>
                                                                                <asp:ListItem Value="QA Components  Engine"></asp:ListItem>
                                                                                <asp:ListItem Value="QA ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="QA Head"></asp:ListItem>
                                                                                <asp:ListItem Value="QA MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="QA MetLab"></asp:ListItem>
                                                                                <asp:ListItem Value="QA QC Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="QAC"></asp:ListItem>
                                                                                <asp:ListItem Value="QC"></asp:ListItem>
                                                                                <asp:ListItem Value="QC Incharge"></asp:ListItem>
                                                                                <asp:ListItem Value="Quality"></asp:ListItem>
                                                                                <asp:ListItem Value="QUALITY ASSURANCE"></asp:ListItem>
                                                                                <asp:ListItem Value="Quality Department"></asp:ListItem>
                                                                                <asp:ListItem Value="quality dept"></asp:ListItem>
                                                                                <asp:ListItem Value="Quality Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="Quality engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="quality Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Quality Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="Quality Manegaer "></asp:ListItem>
                                                                                <asp:ListItem Value="QUALITY MANEGER"></asp:ListItem>
                                                                                <asp:ListItem Value="Quality MCD"></asp:ListItem>
                                                                                <asp:ListItem Value="QualityEngineer "></asp:ListItem>
                                                                                <asp:ListItem Value="QULITY DEP"></asp:ListItem>
                                                                                <asp:ListItem Value="QWNER"></asp:ListItem>
                                                                                <asp:ListItem Value="r"></asp:ListItem>
                                                                                <asp:ListItem Value="R  D"></asp:ListItem>
                                                                                <asp:ListItem Value="R AND D Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="R D Department"></asp:ListItem>
                                                                                <asp:ListItem Value="RAIGARH ISPAT"></asp:ListItem>
                                                                                <asp:ListItem Value="RAMESH "></asp:ListItem>
                                                                                <asp:ListItem Value="RAQAMI TECHNOLOGIES"></asp:ListItem>
                                                                                <asp:ListItem Value="RAVI"></asp:ListItem>
                                                                                <asp:ListItem Value="RAVIN"></asp:ListItem>
                                                                                <asp:ListItem Value="RAVINDRA S"></asp:ListItem>
                                                                                <asp:ListItem Value="RAVITEC"></asp:ListItem>
                                                                                <asp:ListItem Value="RBS"></asp:ListItem>
                                                                                <asp:ListItem Value="RCF"></asp:ListItem>
                                                                                <asp:ListItem Value="Reception"></asp:ListItem>
                                                                                <asp:ListItem Value="Regional Marketing Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Regional Sales Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="RELATIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="RELYON SOLAR"></asp:ListItem>
                                                                                <asp:ListItem Value="RENEWORBIT"></asp:ListItem>
                                                                                <asp:ListItem Value="RENUKA"></asp:ListItem>
                                                                                <asp:ListItem Value="resigned"></asp:ListItem>
                                                                                <asp:ListItem Value="RIJ ELECTRICALS"></asp:ListItem>
                                                                                <asp:ListItem Value="RMS INFRASTRUCTURE"></asp:ListItem>
                                                                                <asp:ListItem Value="RUBY HALL CLINIC"></asp:ListItem>
                                                                                <asp:ListItem Value="Rudra metal"></asp:ListItem>
                                                                                <asp:ListItem Value="RULKA"></asp:ListItem>
                                                                                <asp:ListItem Value="RUSHI"></asp:ListItem>
                                                                                <asp:ListItem Value="s"></asp:ListItem>
                                                                                <asp:ListItem Value="S KULKARNI"></asp:ListItem>
                                                                                <asp:ListItem Value="SADAFAL ENGINEERS"></asp:ListItem>
                                                                                <asp:ListItem Value="SAFE"></asp:ListItem>
                                                                                <asp:ListItem Value="SAFETY "></asp:ListItem>
                                                                                <asp:ListItem Value="SAGAR CEMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="SAGAR CONSTRUCTIONS"></asp:ListItem>
                                                                                <asp:ListItem Value="SAHEE"></asp:ListItem>
                                                                                <asp:ListItem Value="SAI "></asp:ListItem>
                                                                                <asp:ListItem Value="SAI ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="SAIPRO"></asp:ListItem>
                                                                                <asp:ListItem Value="SAL"></asp:ListItem>
                                                                                <asp:ListItem Value="SALE"></asp:ListItem>
                                                                                <asp:ListItem Value="Sale Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="SALES"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales  Engineer "></asp:ListItem>
                                                                                <asp:ListItem Value="Sales  Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Agent UEA"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales and Marketing"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales and Marketing Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Associate"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Co Ordinator"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Dept"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Engineer Oil and Gas"></asp:ListItem>
                                                                                <asp:ListItem Value="SALES EXCUTIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Head"></asp:ListItem>
                                                                                <asp:ListItem Value="sales In Charges"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Manage"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Person"></asp:ListItem>
                                                                                <asp:ListItem Value="Sales Representative"></asp:ListItem>
                                                                                <asp:ListItem Value="Salesman "></asp:ListItem>
                                                                                <asp:ListItem Value="SALSE"></asp:ListItem>
                                                                                <asp:ListItem Value="SAMADHAN"></asp:ListItem>
                                                                                <asp:ListItem Value="SAMARTH"></asp:ListItem>
                                                                                <asp:ListItem Value="SAMARTHSHREE"></asp:ListItem>
                                                                                <asp:ListItem Value="SAMIKSHA"></asp:ListItem>
                                                                                <asp:ListItem Value="SAMSUNG"></asp:ListItem>
                                                                                <asp:ListItem Value="sanjay"></asp:ListItem>
                                                                                <asp:ListItem Value="SANSERA"></asp:ListItem>
                                                                                <asp:ListItem Value="SARA SOLAR ENERGY"></asp:ListItem>
                                                                                <asp:ListItem Value="SARTHAK ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="SARVA"></asp:ListItem>
                                                                                <asp:ListItem Value="SAUR GURU"></asp:ListItem>
                                                                                <asp:ListItem Value="SAYYNAR INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="ScE"></asp:ListItem>
                                                                                <asp:ListItem Value="SCM"></asp:ListItem>
                                                                                <asp:ListItem Value="SCON"></asp:ListItem>
                                                                                <asp:ListItem Value="SCR"></asp:ListItem>
                                                                                <asp:ListItem Value="sd"></asp:ListItem>
                                                                                <asp:ListItem Value="sdfsdf"></asp:ListItem>
                                                                                <asp:ListItem Value="SDH"></asp:ListItem>
                                                                                <asp:ListItem Value="se"></asp:ListItem>
                                                                                <asp:ListItem Value="SEAWORTHY "></asp:ListItem>
                                                                                <asp:ListItem Value="SECRETORY"></asp:ListItem>
                                                                                <asp:ListItem Value="Segment Manager Assembly India"></asp:ListItem>
                                                                                <asp:ListItem Value="sel"></asp:ListItem>
                                                                                <asp:ListItem Value="Seld"></asp:ListItem>
                                                                                <asp:ListItem Value="SELF"></asp:ListItem>
                                                                                <asp:ListItem Value="SELF EMPLOYED"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Application Engineer  "></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Area Sales Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="SENIOR BUSINESS MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Buyer"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Engineer Electrical"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Engineer Instrument"></asp:ListItem>
                                                                                <asp:ListItem Value="SENIOR ENGINEER MATERIAL"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Manager  Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="SENIOR MANAGER COMMERCIAL"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Manager Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Manager Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Officer"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Procurement Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Project Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="senior project manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Purchase "></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Purchase engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Purchase Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="senior purchase manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Research Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Sales Engineer "></asp:ListItem>
                                                                                <asp:ListItem Value="Senior Specialist Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="SERVICE"></asp:ListItem>
                                                                                <asp:ListItem Value="SERVICE CENTER"></asp:ListItem>
                                                                                <asp:ListItem Value="SERVICE ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="SERVICE MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="SERVICES"></asp:ListItem>
                                                                                <asp:ListItem Value="SEVA"></asp:ListItem>
                                                                                <asp:ListItem Value="SHA JETHMAL SOPAJI"></asp:ListItem>
                                                                                <asp:ListItem Value="SHADANAN AUTO"></asp:ListItem>
                                                                                <asp:ListItem Value="SHEEPEY INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="SHIRAPUR"></asp:ListItem>
                                                                                <asp:ListItem Value="Shirwal"></asp:ListItem>
                                                                                <asp:ListItem Value="SHIV SWITCHGEARS"></asp:ListItem>
                                                                                <asp:ListItem Value="SHOP"></asp:ListItem>
                                                                                <asp:ListItem Value="Showroom"></asp:ListItem>
                                                                                <asp:ListItem Value="SHRAVANI"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREE AGENCIES"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREE ENERGY SOLUTIONS"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREE ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREE GIRI ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREE KRUPA"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREE LAXMI ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREE RA"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREE RENUKA"></asp:ListItem>
                                                                                <asp:ListItem Value="SHREEYASH "></asp:ListItem>
                                                                                <asp:ListItem Value="SHRI"></asp:ListItem>
                                                                                <asp:ListItem Value="SHRI MAHALAXMI STEEL INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="SHRUTI INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="SHWOROOM"></asp:ListItem>
                                                                                <asp:ListItem Value="SIR"></asp:ListItem>
                                                                                <asp:ListItem Value="SITE INCHARGE"></asp:ListItem>
                                                                                <asp:ListItem Value="SKE"></asp:ListItem>
                                                                                <asp:ListItem Value="SKS"></asp:ListItem>
                                                                                <asp:ListItem Value="Slef"></asp:ListItem>
                                                                                <asp:ListItem Value="slf "></asp:ListItem>
                                                                                <asp:ListItem Value="society"></asp:ListItem>
                                                                                <asp:ListItem Value="Solar department"></asp:ListItem>
                                                                                <asp:ListItem Value="Sotre manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Sources"></asp:ListItem>
                                                                                <asp:ListItem Value="Sourcing"></asp:ListItem>
                                                                                <asp:ListItem Value="Sourcing and Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="sourcing engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Sourcing Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Sourcing Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Sourcing Lighting BU    "></asp:ListItem>
                                                                                <asp:ListItem Value="Sourcing Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Sourcing Team "></asp:ListItem>
                                                                                <asp:ListItem Value="soursing"></asp:ListItem>
                                                                                <asp:ListItem Value="SP INNOVATIVE"></asp:ListItem>
                                                                                <asp:ListItem Value="SPARK ELECTRICAL ENGINEERING"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr  Manager  Accounts  Admin"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr  Manager  PROJECTS"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Application Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr CAE engineer "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Design"></asp:ListItem>
                                                                                <asp:ListItem Value="SR DEVELOPMENT MANAGER"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr DGM Marketing"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engg Machine Shop"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engg Production"></asp:ListItem>
                                                                                <asp:ListItem Value="SR ENGINEER"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engineer  Buyer "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engineer Business Development"></asp:ListItem>
                                                                                <asp:ListItem Value="SR ENGINEER ESTIMATION"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engineer Instrumentation"></asp:ListItem>
                                                                                <asp:ListItem Value="SR ENGINEER PROCUREMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="SR ENGINEER PROCUREMENT AND SCM"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engineer Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engineer Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="SR ENGINEER Q A AND MANYFACTURING"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engineer QC"></asp:ListItem>
                                                                                <asp:ListItem Value="SR ENGINEER SCM"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Engineer Sourcing"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Executive  Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Executive Accounts"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Executive Market Research "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Executive Marketing"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Executive Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Executive SCM "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Executive Supply chain"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr ext"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr General Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr General Manager Plant Head"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Maintenance Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="sr mana project planning"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Manager Business Development"></asp:ListItem>
                                                                                <asp:ListItem Value="SR MANAGER CUSTOMER SUPPORT"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Manager EHS"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Manager Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Manager Materials "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Manager Project Execution"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Manager Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Manager Quality"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Mgr "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Officer Purchase"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Officer Safety"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Procurement Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Project Engg"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Project Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Purchase Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="SR Purchase Executive"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Qc Engg "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Sales Coordinator"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Sales Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Sales Engineer Pune"></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Sourcing Bidding Executive "></asp:ListItem>
                                                                                <asp:ListItem Value="Sr Vice President Project"></asp:ListItem>
                                                                                <asp:ListItem Value="SrEngineer Sales"></asp:ListItem>
                                                                                <asp:ListItem Value="SS"></asp:ListItem>
                                                                                <asp:ListItem Value="Staff"></asp:ListItem>
                                                                                <asp:ListItem Value="STD VD"></asp:ListItem>
                                                                                <asp:ListItem Value="STORE"></asp:ListItem>
                                                                                <asp:ListItem Value="Store Incharge"></asp:ListItem>
                                                                                <asp:ListItem Value="Store Officer"></asp:ListItem>
                                                                                <asp:ListItem Value="STORES"></asp:ListItem>
                                                                                <asp:ListItem Value="Strategic Purchaser"></asp:ListItem>
                                                                                <asp:ListItem Value="STRATEGIC SOURCING"></asp:ListItem>
                                                                                <asp:ListItem Value="Strategic Sourcing HPS"></asp:ListItem>
                                                                                <asp:ListItem Value="STUDAND"></asp:ListItem>
                                                                                <asp:ListItem Value="STUDENT"></asp:ListItem>
                                                                                <asp:ListItem Value="SUBHASH"></asp:ListItem>
                                                                                <asp:ListItem Value="SUBHASHREE"></asp:ListItem>
                                                                                <asp:ListItem Value="Substation Tendering Department"></asp:ListItem>
                                                                                <asp:ListItem Value="SUCCESS"></asp:ListItem>
                                                                                <asp:ListItem Value="SUDEEP ENGINEERS"></asp:ListItem>
                                                                                <asp:ListItem Value="SUNBEAM"></asp:ListItem>
                                                                                <asp:ListItem Value="SUNFLAG"></asp:ListItem>
                                                                                <asp:ListItem Value="SUNLIGHT"></asp:ListItem>
                                                                                <asp:ListItem Value="SUNMITRA"></asp:ListItem>
                                                                                <asp:ListItem Value="SUNPOWER"></asp:ListItem>
                                                                                <asp:ListItem Value="SUNRISE SOLAR POWER COMPANY"></asp:ListItem>
                                                                                <asp:ListItem Value="SUNSTROAT "></asp:ListItem>
                                                                                <asp:ListItem Value="SUPER ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPERVIER"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPERVISE SBU PURCHASE"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPERVISER"></asp:ListItem>
                                                                                <asp:ListItem Value="Supervisor"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPERVISOUR"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPERVOSOR"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPERWISER"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPLLY"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPPILER"></asp:ListItem>
                                                                                <asp:ListItem Value="SUPPLIER"></asp:ListItem>
                                                                                <asp:ListItem Value="supplier job work"></asp:ListItem>
                                                                                <asp:ListItem Value="Supply Chain"></asp:ListItem>
                                                                                <asp:ListItem Value="Supply Chain Management"></asp:ListItem>
                                                                                <asp:ListItem Value="Supply Chain Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="Supply Chain Manager India "></asp:ListItem>
                                                                                <asp:ListItem Value="SUPREME "></asp:ListItem>
                                                                                <asp:ListItem Value="Supreme Enterprise"></asp:ListItem>
                                                                                <asp:ListItem Value="Supritendent"></asp:ListItem>
                                                                                <asp:ListItem Value="Suravi Solar Syst"></asp:ListItem>
                                                                                <asp:ListItem Value="SURFIN"></asp:ListItem>
                                                                                <asp:ListItem Value="SURVEYOR"></asp:ListItem>
                                                                                <asp:ListItem Value="SURYATEJ SOLAR"></asp:ListItem>
                                                                                <asp:ListItem Value="SUSHIL"></asp:ListItem>
                                                                                <asp:ListItem Value="SUVARNA"></asp:ListItem>
                                                                                <asp:ListItem Value="SUYASH ELECTRIC CO"></asp:ListItem>
                                                                                <asp:ListItem Value="SUYOG"></asp:ListItem>
                                                                                <asp:ListItem Value="SWATI"></asp:ListItem>
                                                                                <asp:ListItem Value="SWID"></asp:ListItem>
                                                                                <asp:ListItem Value="SYMTRONICS AUTOMATION "></asp:ListItem>
                                                                                <asp:ListItem Value="System manager"></asp:ListItem>
                                                                                <asp:ListItem Value="System Techucian "></asp:ListItem>
                                                                                <asp:ListItem Value="T"></asp:ListItem>
                                                                                <asp:ListItem Value="TALAWADE"></asp:ListItem>
                                                                                <asp:ListItem Value="TALWADE"></asp:ListItem>
                                                                                <asp:ListItem Value="TASTY"></asp:ListItem>
                                                                                <asp:ListItem Value="TEACHER"></asp:ListItem>
                                                                                <asp:ListItem Value="Team Lead"></asp:ListItem>
                                                                                <asp:ListItem Value="Team Lead SCM"></asp:ListItem>
                                                                                <asp:ListItem Value="Team Lead Sourcing"></asp:ListItem>
                                                                                <asp:ListItem Value="Team Member  "></asp:ListItem>
                                                                                <asp:ListItem Value="Team Member  Project Management and Tooling Sourci"></asp:ListItem>
                                                                                <asp:ListItem Value="TECH"></asp:ListItem>
                                                                                <asp:ListItem Value="Tech manager"></asp:ListItem>
                                                                                <asp:ListItem Value="TECHFORMATO"></asp:ListItem>
                                                                                <asp:ListItem Value="TECHMURISE ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="TECHNICAL ADVISOR"></asp:ListItem>
                                                                                <asp:ListItem Value="Technical Assistant"></asp:ListItem>
                                                                                <asp:ListItem Value="technical co ordinator"></asp:ListItem>
                                                                                <asp:ListItem Value="technical director"></asp:ListItem>
                                                                                <asp:ListItem Value="Technical E O"></asp:ListItem>
                                                                                <asp:ListItem Value="Technical head"></asp:ListItem>
                                                                                <asp:ListItem Value="Technical Lead"></asp:ListItem>
                                                                                <asp:ListItem Value="Technical Manager  Projects"></asp:ListItem>
                                                                                <asp:ListItem Value="Technical Officer"></asp:ListItem>
                                                                                <asp:ListItem Value="Technology and IT Procurement"></asp:ListItem>
                                                                                <asp:ListItem Value="TECHNOMECH"></asp:ListItem>
                                                                                <asp:ListItem Value="TECHNOVERA"></asp:ListItem>
                                                                                <asp:ListItem Value="TECNICAL DIRECTOR"></asp:ListItem>
                                                                                <asp:ListItem Value="TEJ"></asp:ListItem>
                                                                                <asp:ListItem Value="TEMBHU LIFT"></asp:ListItem>
                                                                                <asp:ListItem Value="TEMPO"></asp:ListItem>
                                                                                <asp:ListItem Value="TENOVA INDIA PRIVET LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="Territory Manager "></asp:ListItem>
                                                                                <asp:ListItem Value="TERRITORY SALES OFFICER"></asp:ListItem>
                                                                                <asp:ListItem Value="THANE"></asp:ListItem>
                                                                                <asp:ListItem Value="THE"></asp:ListItem>
                                                                                <asp:ListItem Value="THE RAMCO CEMENT LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="thergaon"></asp:ListItem>
                                                                                <asp:ListItem Value="THERMAL"></asp:ListItem>
                                                                                <asp:ListItem Value="THERMAL SY"></asp:ListItem>
                                                                                <asp:ListItem Value="THERMAX"></asp:ListItem>
                                                                                <asp:ListItem Value="THERMAX LIMITED "></asp:ListItem>
                                                                                <asp:ListItem Value="THERMAX LIMITED WWS DIV"></asp:ListItem>
                                                                                <asp:ListItem Value="THERMAX SHIPPER"></asp:ListItem>
                                                                                <asp:ListItem Value="TIRUPATI ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="Tool Room"></asp:ListItem>
                                                                                <asp:ListItem Value="TOOL ROOM INCHARGE"></asp:ListItem>
                                                                                <asp:ListItem Value="TOOLROOM"></asp:ListItem>
                                                                                <asp:ListItem Value="TOOLROOM INCHARGE"></asp:ListItem>
                                                                                <asp:ListItem Value="TOOLROOM Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="TOP B"></asp:ListItem>
                                                                                <asp:ListItem Value="TRADERS"></asp:ListItem>
                                                                                <asp:ListItem Value="TRANSPORT"></asp:ListItem>
                                                                                <asp:ListItem Value="TRANSPYRO "></asp:ListItem>
                                                                                <asp:ListItem Value="TRISHUL"></asp:ListItem>
                                                                                <asp:ListItem Value="TRISHUL ENGINEERS"></asp:ListItem>
                                                                                <asp:ListItem Value="TRUSTY"></asp:ListItem>
                                                                                <asp:ListItem Value="TURNWELL ENGINEERS P LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="UD POWER"></asp:ListItem>
                                                                                <asp:ListItem Value="UG"></asp:ListItem>
                                                                                <asp:ListItem Value="ULA"></asp:ListItem>
                                                                                <asp:ListItem Value="UNIQUE"></asp:ListItem>
                                                                                <asp:ListItem Value="UNIT HEAD"></asp:ListItem>
                                                                                <asp:ListItem Value="Unknown"></asp:ListItem>
                                                                                <asp:ListItem Value="UP OFFICE"></asp:ListItem>
                                                                                <asp:ListItem Value="UTTAM ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="V"></asp:ListItem>
                                                                                <asp:ListItem Value="V K PUMP INDUSTRIES PVT LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="V K PUMPS PVT LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="V SQUARE"></asp:ListItem>
                                                                                <asp:ListItem Value="VAPI"></asp:ListItem>
                                                                                <asp:ListItem Value="VARUN SUNCLEAN ENERGY LLP"></asp:ListItem>
                                                                                <asp:ListItem Value="VEDA INFRASTRUCTURE"></asp:ListItem>
                                                                                <asp:ListItem Value="VEDANSH"></asp:ListItem>
                                                                                <asp:ListItem Value="VEDANT"></asp:ListItem>
                                                                                <asp:ListItem Value="Velu"></asp:ListItem>
                                                                                <asp:ListItem Value="Vender development Engineer"></asp:ListItem>
                                                                                <asp:ListItem Value="VENDOR DEVELOPMENT"></asp:ListItem>
                                                                                <asp:ListItem Value="VERTICON"></asp:ListItem>
                                                                                <asp:ListItem Value="Vice President"></asp:ListItem>
                                                                                <asp:ListItem Value="VICKHARDTH AUTOMATION"></asp:ListItem>
                                                                                <asp:ListItem Value="VIJAYDEEP "></asp:ListItem>
                                                                                <asp:ListItem Value="VIMAL ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="VINI ENGINEERING INDUSTRIES"></asp:ListItem>
                                                                                <asp:ListItem Value="VIOCE PRESIDENT"></asp:ListItem>
                                                                                <asp:ListItem Value="Vipin Jain"></asp:ListItem>
                                                                                <asp:ListItem Value="VISHVACHAYA STEELS PVT LTD"></asp:ListItem>
                                                                                <asp:ListItem Value="VISION"></asp:ListItem>
                                                                                <asp:ListItem Value="VIVEK"></asp:ListItem>
                                                                                <asp:ListItem Value="VP Business Development"></asp:ListItem>
                                                                                <asp:ListItem Value="vvv"></asp:ListItem>
                                                                                <asp:ListItem Value="W"></asp:ListItem>
                                                                                <asp:ListItem Value="WATERTECH"></asp:ListItem>
                                                                                <asp:ListItem Value="WESTERN ENTERPRISES"></asp:ListItem>
                                                                                <asp:ListItem Value="WIREMAN"></asp:ListItem>
                                                                                <asp:ListItem Value="WONDER CEMENT LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="WONER"></asp:ListItem>
                                                                                <asp:ListItem Value="WORKER"></asp:ListItem>
                                                                                <asp:ListItem Value="Works Manager"></asp:ListItem>
                                                                                <asp:ListItem Value="WTE"></asp:ListItem>
                                                                                <asp:ListItem Value="X"></asp:ListItem>
                                                                                <asp:ListItem Value="xx"></asp:ListItem>
                                                                                <asp:ListItem Value="XXX"></asp:ListItem>
                                                                                <asp:ListItem Value="XYZ"></asp:ListItem>
                                                                                <asp:ListItem Value="y"></asp:ListItem>
                                                                                <asp:ListItem Value="YASH"></asp:ListItem>
                                                                                <asp:ListItem Value="YEMEN"></asp:ListItem>
                                                                                <asp:ListItem Value="YUKTA"></asp:ListItem>
                                                                                <asp:ListItem Value="YUMN LIMITED"></asp:ListItem>
                                                                                <asp:ListItem Value="z"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </center>
                                                                        </center>

                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:TextBox ID="txtContactnumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </center>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:CheckBox runat="server" ID="chkNotify" value="notify" name="chkNotify" runat="server" visible="false" />
                                                                        </center>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:CheckBox runat="server" ID="chkAccess" value="access" name="chkAccess" runat="server" visible="false" />
                                                                        </center>
                                                                    </td>
                                                                    <td>
                                                                        <center>
                                                                            <asp:Button ID="btnAddMore" CssClass="btn btn-success btn-sm btncss" OnClick="Insert" runat="server" Text="Add More" />
                                                                        </center>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div class="row" id="divdtls">
                                                            <div class="table-responsive" visible="false">

                                                                <asp:GridView ID="dgvContactDetails" runat="server" CssClass="table" HeaderStyle-BackColor="#009999" AutoGenerateColumns="false"
                                                                    EmptyDataText="No records has been added." OnRowCommand="dgvContactDetails_RowCommand" OnRowEditing="dgvContactDetails_RowEditing">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                <asp:Label ID="lblid" runat="Server" Text='<%# Eval("id") %>' Visible="false" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Conatct Name" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("contactname") %>' CssClass="form-control" ID="txtcontactName" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcontactname" runat="Server" Text='<%# Eval("contactname") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Designation" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("designation") %>' CssClass="form-control" ID="txtdesignation" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldesignation" runat="Server" Text='<%# Eval("designation") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Contact No" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("contactno") %>' CssClass="form-control" ID="txtcontactno" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcontactno" runat="Server" Text='<%# Eval("contactno") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Notify" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                            <EditItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="ckhnotify" Checked='<%#Convert.ToBoolean(Eval("notify"))%>' />
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNotify" runat="Server" Text='<%# Convert.ToBoolean(Eval("notify"))==true? "YES":"NO" %>' Visible="false" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Access" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                            <EditItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="ckhaccess" Checked='<%#Convert.ToBoolean(Eval("access"))%>' />
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblaccess" runat="Server" Text='<%# Convert.ToBoolean(Eval("access"))==true?"YES":"NO" %>' Visible="false" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="120">
                                                                            <EditItemTemplate>
                                                                                <asp:LinkButton Text="Update" ID="lnkbtnUpdate" ClientIDMode="Static" runat="server" OnClick="lnkbtnUpdate_Click" ToolTip="Update"><i class="fa fa-refresh" style="font-size:28px;color:green;"></i></asp:LinkButton>
                                                                                |
                                                                            <asp:LinkButton Text="Cancel" ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" ToolTip="Cancel"><i class="fa fa-close" style="font-size:28px;color:red;"></i></asp:LinkButton>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" ToolTip="Edit"><i class="fa fa-edit" style="font-size:28px;color:blue;"></i></asp:LinkButton>
                                                                                | 
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="lnkDelete_Click" ToolTip="Delete"><i class="fa fa-trash-o" style="font-size:28px;color:red"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>

                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-2"></div>
                                                            <div class="col-md-2">
                                                                <center>
                                                                    <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Add Supplier" OnClick="btnadd_Click" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        function DisableButton() {
            var btn = document.getElementById("<%=btnadd.ClientID %>");
            btn.value = 'Please wait...';
            document.getElementById("<%=btnadd.ClientID %>").disabled = true;
            document.getElementById("<%=btnadd.ClientID %>").classList.add("dissablebtn");
        }
        window.onbeforeunload = DisableButton;
    </script>




</asp:Content>
