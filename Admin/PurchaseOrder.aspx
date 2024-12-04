<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="Admin_PurchaseOrder" %>

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
                                    <span><a href="PurchaseOrderList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Purchase Order List</a>&nbsp;&nbsp;
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container py-3">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Create Purchase Order</h5>
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
                                                    </div>
                                                    <div class="col-md-2 spancls">Kind. att:</div>
                                                    <div class="col-md-4">

                                                        <asp:HiddenField runat="server" ID="hdnfileData" />
                                                        <asp:HiddenField runat="server" ID="hdnGrandtotal" />
                                                        <asp:DropDownList runat="server" ID="ddlKindAtt" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 spancls">PO No:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtPONo" CssClass="form-control" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">PO Date:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtPodate" CssClass="form-control" runat="server" Width="100%" AutoComplete="off" TextMode="Date"></asp:TextBox>
           <%--                                             <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtPodate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>--%>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-2 spancls">Mode : </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlMode" runat="server" class="form-control">
                                                            <asp:ListItem Text="Open"></asp:ListItem>
                                                            <asp:ListItem Text="Close"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2 spancls">Delivery Date <i class="reqcls">*&nbsp;</i>:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtDeliverydate" CssClass="form-control" runat="server" Width="100%" AutoComplete="off" OnTextChanged="txtDeliverydate_TextChanged" AutoPostBack="true" TextMode="Date"></asp:TextBox>
                            <%--                            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDeliverydate" Format="dd-MM-yyyy" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>--%>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Select Delivery Date"
                                                            ControlToValidate="txtDeliverydate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Refer Quotation:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtReferQuotation" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">Remarks:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row" runat="server">
                                                    <div class="col-md-2 spancls">Order Close Mode : </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlOrderCloseMode" CssClass="form-control" runat="server">
                                                            <asp:ListItem>Quantity</asp:ListItem>
                                                            <asp:ListItem>Amount</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
													     <div class="col-md-2 spancls">Taxation <i class="reqcls">*&nbsp;</i> : </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList CssClass="form-control" ID="ddltaxation" runat="server" AutoPostBack="true"  OnTextChanged="ddltaxation_TextChanged"></asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-2 spancls"  runat="server" visible="false" >Send Mail:</div>
                                                    <div class="col-md-4"  runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="col-md-2">
                                                                <asp:CheckBox runat="server" ID="IsSedndMail" />
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:Label runat="server" Font-Bold="true" ID="lblEmailID"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                               
                                                </div>



                                                <div class="card-header bg-primary text-uppercase text-white" style="margin-top: 10px;">
                                                    <h5>Particulars Details</h5>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <table class="table" border="1" style="width: 100%; border: 1px solid #0c7d38;">
                                                                <tr style="background-color: #7ad2d4; color: #000; font-weight: 600; text-align: center;">
                                                                    <td style="width: 50%;">Particulars<i class="reqcls">*&nbsp;</i></td>
                                                                    <td>HSN</td>
                                                                    <td>Qty<i class="reqcls">*&nbsp;</i></td>
                                                                    <td>UOM</td>
                                                                    <td>Rate<i class="reqcls">*&nbsp;</i></td>
                                                                    <td>Disc(%)</td>
                                                                    <td>Amount</td>
                                                                    <td>CGST</td>
                                                                    <td>SGST</td>
                                                                    <td>IGST</td>
                                                                    <td>Total Amount</td>
                                                                    <td style="width: 50%;">Description</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <%--  <asp:DropDownList runat="server" ID="ddlparticular" Width="300px" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtParticulars_TextChanged"></asp:DropDownList>--%>
                                                                        <asp:TextBox ID="txtParticulars" Width="300px" runat="server" AutoPostBack="true" OnTextChanged="txtParticulars_TextChanged"></asp:TextBox>
                                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetItemList"
                                                                            TargetControlID="txtParticulars">
                                                                        </asp:AutoCompleteExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHSN" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQty" Width="50px" runat="server" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" ID="txtUOM">
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:TextBox ID="txtUOM" Width="50px" runat="server"></asp:TextBox>
                                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetUOMList"
                                                                            TargetControlID="txtUOM">
                                                                        </asp:AutoCompleteExtender>--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRate" Width="100px" runat="server" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDisc" Width="100px" runat="server" AutoPostBack="true" Text="0" OnTextChanged="txtDisc_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAmountt" Width="100px" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="CGSTPer" Width="50px" runat="server" AutoPostBack="true" OnTextChanged="CGSTPer_TextChanged" placeholder="%"></asp:TextBox>
                                                                        <asp:TextBox ID="CGSTAmt" Width="100px" runat="server" ReadOnly="true" placeholder="CGSTAmt"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="SGSTPer" Width="50px" runat="server" AutoPostBack="true" OnTextChanged="SGSTPer_TextChanged" placeholder="%"></asp:TextBox>
                                                                        <asp:TextBox ID="SGSTAmt" Width="100px" runat="server" ReadOnly="true" placeholder="SGSTAmt"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="IGSTPer" Width="50px" runat="server" AutoPostBack="true" OnTextChanged="IGSTPer_TextChanged" placeholder="%"></asp:TextBox>
                                                                        <asp:TextBox ID="IGSTAmt" Width="100px" runat="server" ReadOnly="true" placeholder="IGSTAmt"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotalamt" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtDescription" Width="300px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnAddMore" CssClass="btn btn-success btn-sm btncss" OnClick="Insert" runat="server" Text="+ Add" />
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                            </table>
                                                        </div>

                                                        <div class="row" id="divdtls">
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="dgvParticularsDetails" runat="server" CssClass="table" HeaderStyle-BackColor="#009999" AutoGenerateColumns="false"
                                                                    EmptyDataText="No records has been added." OnRowCommand="dgvParticularsDetails_RowCommand" OnRowEditing="dgvParticularsDetails_RowEditing" OnRowDataBound="dgvParticularsDetails_RowDataBound" ShowFooter="true">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                <asp:Label ID="lblid" runat="Server" Text='<%# Eval("id") %>' Visible="false" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <%-- <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("Particulars") %>' ID="txtParticulars" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>--%>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblParticulars" runat="Server" Text='<%# Eval("Particulars") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="HSN" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <%--   <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("HSN") %>' ID="txtHSN" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>--%>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblHSN" runat="Server" Text='<%# Eval("HSN") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Qty" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("Qty") %>' Width="50px" ID="txtQty" runat="server" AutoPostBack="true" OnTextChanged="txtQty_TextChanged1"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblQty" runat="Server" Text='<%# Eval("Qty") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="UOM" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("UOM") %>' Width="50px" ID="txtUOM" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUOM" runat="Server" Text='<%# Eval("UOM") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Rate" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("Rate") %>' ID="txtRate" runat="server" AutoPostBack="true" OnTextChanged="txtRate_TextChanged1"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRate" runat="Server" Text='<%# Eval("Rate") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Discount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("Discount") %>' ID="txtDiscount" runat="server" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDiscount" runat="Server" Text='<%# Eval("Discount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <%-- <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("Amount") %>' ID="txtAmount" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>--%>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmount" runat="Server" Text='<%# Eval("Amount") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CGSTPer" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("CGSTPer") %>' Width="50px" ID="txtCGSTPer" runat="server" OnTextChanged="txtCGSTPer_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCGSTPer" runat="Server" Text='<%# Eval("CGSTPer") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CGSTAmt" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("CGSTAmt") %>' Width="100px" ID="txtCGSTAmt" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCGSTAmt" runat="Server" Text='<%# Eval("CGSTAmt") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="SGSTPer" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("SGSTPer") %>' Width="50px" ID="txtSGSTPer" runat="server" OnTextChanged="txtSGSTPer_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSGSTPer" runat="Server" Text='<%# Eval("SGSTPer") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SGSTAmt" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("SGSTAmt") %>' Width="100px" ID="txtSGSTAmt" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSGSTAmt" runat="Server" Text='<%# Eval("SGSTAmt") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IGSTPer" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("IGSTPer") %>' Width="50px" ID="txtIGSTPer" runat="server" OnTextChanged="txtIGSTPer_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIGSTPer" runat="Server" Text='<%# Eval("IGSTPer") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="IGSTAmt" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("IGSTAmt") %>' Width="100px" ID="txtIGSTAmt" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIGSTAmt" runat="Server" Text='<%# Eval("IGSTAmt") %>' />
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblGrand" Font-Bold="true" runat="server" Text="Grand Total"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Left">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("TotalAmount") %>' ID="txtTotalAmount" runat="server" ReadOnly="true"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalAmount" runat="Server" Text='<%# Eval("TotalAmount") %>' />
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbltotal" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Left">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox Text='<%# Eval("Description") %>' ID="txttblDescription" runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDescription" runat="Server" Text='<%# Eval("Description") %>' />
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
                                                        <div class="table-responsive">
                                                            <table class="table" border="1" style="width: 100%; border: 1px solid #0c7d38;">
                                                                <tr style="background-color: #7ad2d4; color: #000; font-weight: 600; text-align: center;">
                                                                    <td style="width: 15%;">Transportation Charges</td>
                                                                    <td style="width: 15%;">Description</td>
                                                                    <td>CGST(%)</td>
                                                                    <td>SGST(%)</td>
                                                                    <td>IGST(%)</td>
                                                                    <td>Total Cost</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTCharge" Width="250px" runat="server" Text="0" OnTextChanged="txtTCharge_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTDescription" Width="250px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTCGSTPer" Width="50px" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtTCGSTPer_TextChanged"></asp:TextBox>
                                                                        <asp:TextBox ID="txtTCGSTamt" Width="100px" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTSGSTPer" Width="50px" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtTSGSTPer_TextChanged"></asp:TextBox>
                                                                        <asp:TextBox ID="txtTSGSTamt" Width="100px" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTIGSTPer" Width="50px" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtTIGSTPer_TextChanged"></asp:TextBox>
                                                                        <asp:TextBox ID="txtTIGSTamt" Width="100px" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTCost" Width="100px" runat="server" Enabled="false" Text="0"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <%--  footer--%>
                                                        <div class="row">
                                                            <div class="col-md-2 spancls">PAYMENT TERMS <i class="reqcls">*&nbsp;</i>:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlPaymentTerm" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Select Payment Term"
                                                                    ControlToValidate="ddlPaymentTerm" InitialValue="0" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-2 spancls">PACKING & FORWARDING:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlPackingAndForwarding" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-2 spancls">TRANSPORTATION:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlTransportation" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-2 spancls">VARIATION:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlVariation" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-2 spancls">DELIVERY:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlDelivery" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-2 spancls">TEST CERTIFICATE:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlTestCertificate" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-2 spancls">WEEKLY OFF.:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlWeeklyOff" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-2 spancls">TIME:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlTime" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-2 spancls">II:</div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="ddlII" CssClass="form-control"></asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-2 spancls">Ref. Documents</div>
                                                            <div class="col-md-4">
                                                                <asp:FileUpload runat="server" ID="UploadRefDocs" CssClass="form-control" />
                                                                <span runat="server" id="spnFileUploadData" style="color: red;"></span>
                                                            </div>


                                                        </div>
                                                        <%--  footer End--%>

                                                        <div class="row">
                                                            <div class="col-md-2"></div>
                                                            <div class="col-md-2">
                                                                <center>
                                                                    <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Add PO" OnClick="btnadd_Click" />
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnadd" />
            <asp:PostBackTrigger ControlID="txtParticulars" />
        </Triggers>
    </asp:UpdatePanel>
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
