<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="PurchaseReport.aspx.cs" Inherits="Admin_PurchaseReport" %>

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

                //if (isFooter) {
                //    var tblfr = tbl.cloneNode(true);
                //    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                //    var tblBody = document.createElement('tbody');
                //    tblfr.style.width = '100%';
                //    tblfr.cellSpacing = "0";
                //    tblfr.border = "0px";
                //    tblfr.rules = "none";
                //    //*****In the case of Footer Row *******
                //    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                //    tblfr.appendChild(tblBody);
                //    DivFR.appendChild(tblfr);
                //}
                //****Copy Header in divHeaderRow****


                DivHR.appendChild(tbl.cloneNode(true));

            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>

    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
		if (typeof jQuery.fn.live == 'undefined' || !(jQuery.isFunction(jQuery.fn.live))) {
  jQuery.fn.extend({
      live: function (event, callback) {
         if (this.selector) {
              jQuery(document).on(event, this.selector, callback);
          }
      }
  });
}
		
		
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
    <%--    <asp:UpdatePanel ID="updatepnl" runat="server">
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
                                     <div class="row">
                                        <div class="col-xl-1 col-md-1 spancls">Type<i class="reqcls">&nbsp;</i> :</div>
                                        <div class="col-xl-3 col-md-3">
                                            <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" OnTextChanged="ddltype_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="--Select Type--"></asp:ListItem>
                                                <asp:ListItem Value="PO">Purchase Order</asp:ListItem>
                                                <asp:ListItem Value="PurchaseBill">Purchase Bill</asp:ListItem>
												<asp:ListItem Value="CreditDebitNote">Credit/Debit Note</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xl-1 col-md-1 spancls">Party<i class="reqcls">&nbsp;</i> :</div>
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtPartyName" runat="server" CssClass="form-control" placeholder="Party Name" Width="100%" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetSupplierList"
                                                TargetControlID="txtPartyName">
                                            </asp:AutoCompleteExtender>
                                        </div>

                                        <div class="col-xl-1 col-md-1 spancls">By Item<i class="reqcls">&nbsp;</i>:</div>
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtByItem" runat="server" CssClass="form-control" placeholder="By Item" Width="100%" AutoComplete="Off"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetItemList"
                                                TargetControlID="txtByItem">
                                            </asp:AutoCompleteExtender>
                                        </div>


                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xl-1 col-md-1 spancls">From<i class="reqcls">&nbsp;</i> :</div>
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" placeholder="From Date" Width="100%" AutoComplete="Off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" Format="yyyy-MM-dd" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                        </div>
                                        <div class="col-xl-1 col-md-1 spancls">To<i class="reqcls">&nbsp;</i> :</div>
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" placeholder="To Date" Width="100%" AutoComplete="Off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" Format="yyyy-MM-dd" CssClass="cal_Theme1" runat="server"></asp:CalendarExtender>
                                        </div>

                                        <div class="col-xl-1 col-md-1 spancls" id="divbn1" runat="server" visible="false">Bill No<i class="reqcls">&nbsp;</i> :</div>
                                        <div class="col-xl-3 col-md-3" runat="server" id="divbn2" visible="false">
                                            <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control" placeholder="Bill No" Width="100%" AutoComplete="Off"></asp:TextBox>
                                        </div>

                                        <div class="col-xl-1 col-md-1 spancls" id="divPO1" runat="server" visible="false">PO No<i class="reqcls">&nbsp;</i> :</div>
                                        <div class="col-xl-3 col-md-3" runat="server" id="divPO2" visible="false">
                                            <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control" placeholder="PO No" Width="100%" AutoComplete="Off"></asp:TextBox>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xl-1 col-md-1 spancls" runat="server" id="divsbn1" visible="false">Supplier Bill No<i class="reqcls">&nbsp;</i> :</div>
                                        <div class="col-xl-3 col-md-3" runat="server" id="divsbn2" visible="false">
                                            <asp:TextBox ID="txtSupplierBill" runat="server" CssClass="form-control" placeholder="Supplier Bill No" Width="100%"></asp:TextBox>
                                        </div>
										
										  <div class="col-xl-1 col-md-1 spancls" runat="server" id="divCreDebNo1" visible="false">Credit/Debit No<i class="reqcls">&nbsp;</i> :</div>
                                        <div class="col-xl-3 col-md-3" runat="server" id="divCreDebNo2" visible="false">
                                            <asp:TextBox ID="txtCreditDebitNo" runat="server" CssClass="form-control" placeholder="Credit/Debit No" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xl-5 col-md-5"></div>
                                        <div class="col-xl-1 col-md-1">
                                            <asp:Button ID="btnSearch" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-xl-1 col-md-1">
                                            <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" OnClick="btnresetfilter_Click" />
                                        </div>
                                        <div class="col-xl-5 col-md-5"></div>
                                    </div>
                                    <br />
                                    <div class="row" runat="server" id="exportbtn" visible="false">
                                        <div class="col-xl-4 col-md-4">
                                            <asp:Button ID="btnexcel" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Export Excel" OnClick="btnexcel_Click" />
                                            <asp:Button ID="btnpdf" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Export PDF" OnClick="btnpdf_Click" Visible="false" />
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

                                            <asp:GridView ID="dgvPurchaseOrder" runat="server"
                                                CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                AllowPaging="false" DataKeyNames="id" ShowHeader="true" PageSize="50" OnRowCommand="dgvPurchaseOrder_RowCommand" OnRowDataBound="dgvPurchaseOrder_RowDataBound" ShowFooter="true" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <img alt="" style="cursor: pointer" src="../img/plus.png" />
                                                            <asp:Panel ID="pnlDetails" runat="server" Style="display: none">
                                                                <asp:GridView ID="gvPODetails" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                                                    <Columns>
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Particulars" HeaderText="Particulars" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Description" HeaderText="Description" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="HSN" HeaderText="HSN" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Qty" HeaderText="Qty" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="UOM" HeaderText="UOM" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Rate" HeaderText="Rate" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Discount" HeaderText="Discount" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Amount" HeaderText="Amount" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CGSTPer" HeaderText="CGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="SGSTPer" HeaderText="SGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="IGSTPer" HeaderText="IGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="GrandTotal" HeaderText="GrandTotal" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="PODate" HeaderText="PO Date" DataFormatString="{0:dd-M-yyyy}" />
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="PONo" HeaderText="PO No" />
                                                      <asp:TemplateField HeaderText="Supplier Name" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSuppliername" Text='<%#Eval("SupplierName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfootgrandtotal" runat="server"><b>Grand Total</b></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGrandAmount"></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lblTotalamt" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" CommandName="DownloadPDF" CommandArgument='<%# Eval("Id") %>' ToolTip="Download"><i class="fa fa-file-pdf-o" style="font-size:24px;color:red;"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                            <asp:GridView ID="dgvPurchaseBill" runat="server"
                                                CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                DataKeyNames="id" AllowPaging="false" ShowHeader="true" ShowFooter="true" PageSize="50" OnRowCommand="dgvPurchaseBill_RowCommand" OnRowDataBound="dgvPurchaseBill_RowDataBound"    Visible="false">
                                                <Columns>
                                                   
                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <img alt="" style="cursor: pointer" src="../img/plus.png" />
                                                            <asp:Panel ID="pnlDetails" runat="server" Style="display: none">
                                                                <asp:GridView ID="gvPBillDetails" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                                                    <Columns>
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Particulars" HeaderText="Particulars" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Description" HeaderText="Description" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="HSN" HeaderText="HSN" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Qty" HeaderText="Qty" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="UOM" HeaderText="UOM" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Rate" HeaderText="Rate" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Discount" HeaderText="Discount" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Amount" HeaderText="Amount" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CGSTPer" HeaderText="CGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="SGSTPer" HeaderText="SGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="IGSTPer" HeaderText="IGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="GrandTotal" HeaderText="GrandTotal" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="BillDate" HeaderText="Bill Date" DataFormatString="{0:dd-M-yyyy}" />
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="SupplierBillNo" HeaderText="Supplier BillNo" />
                                                      <asp:TemplateField HeaderText="Supplier Name" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSuppliername" Text='<%#Eval("SupplierName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfootgrandtotal" runat="server"><b>Grand Total</b></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGrandAmountpurchaseBill" Text='<%#Eval("GrandTotal")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lblTotalamt" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" CommandName="DownloadPDF" CommandArgument='<%# Eval("Id") %>' ToolTip="Download"><i class="fa fa-file-pdf-o" style="font-size:24px;color:red;"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
											
											     <asp:GridView ID="dgvCreditDebit" runat="server"
                                                CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                DataKeyNames="id" AllowPaging="false" ShowHeader="true" ShowFooter="true" PageSize="50" OnRowCommand="dgvCreditDebit_RowCommand" OnRowDataBound="dgvCreditDebit_RowDataBound" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <img alt="" style="cursor: pointer" src="../img/plus.png" />
                                                            <asp:Panel ID="pnlDetails" runat="server" Style="display: none">
                                                                <asp:GridView ID="gvCreditDebitDetails" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                                                    <Columns>
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Particulars" HeaderText="Particulars" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Description" HeaderText="Description" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="HSN" HeaderText="HSN" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Qty" HeaderText="Qty" />
                                                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="UOM" HeaderText="UOM" />--%>
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Rate" HeaderText="Rate" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Discount" HeaderText="Discount" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Amount" HeaderText="Amount" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CGSTPer" HeaderText="CGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="SGSTPer" HeaderText="SGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="IGSTPer" HeaderText="IGSTPer" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Total" HeaderText="GrandTotal" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="NoteType" HeaderText="Note Type" />
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="DocNo" HeaderText="Doc No" />
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="DocDate" HeaderText="Doc Date" />
                                                       <asp:TemplateField HeaderText="Supplier Name" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSuppliername" Text='<%#Eval("SupplierName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfootgrandtotal" runat="server"><b>Grand Total</b></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGrandAmount"></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lblTotalamt" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" CommandName="DownloadPDF" CommandArgument='<%# Eval("Id") %>' ToolTip="Download"><i class="fa fa-file-pdf-o" style="font-size:24px;color:red;"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                        </div>
                                    </div>
                                    <%--</div>--%>
                                </div>

                                <%--  <div class="col-md-12" style="padding: 20px; margin-top: 10px;">
                                    <div id="DivRoot1" align="left" runat="server">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent1">
                                            
                                            <div id="DivFooterRow1" style="overflow: hidden">
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

