<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ProformaInvoiceList.aspx.cs" Inherits="Admin_ProformaInvoiceList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        .sortable-handler {
            touch-action: none;
        }

        .card .card-header span {
            color: #060606;
            display: block;
            font-size: 13px;
            margin-top: 5px;
        }

        .btn {
            padding: 5px 5px !important;
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

        /*hr {
            margin-top: 5px !important;
            margin-bottom: 15px !important;
            border: 1px solid #eae6e6 !important;
            width: 100%;
        }*/
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
                max-width: 100% !important;
            }
        }
    </style>

    <style type="text/css">
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
                DivHR.appendChild(tbl.cloneNode(true));

            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>
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
                            <span id="btnAddProfInv" runat="server"><a href="ProformaInvoice.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Proforma Invoice</a>&nbsp;&nbsp;
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <div class="row">
                            <div class="col-md-4">
                                <h5>Proforma Invoice List</h5>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">

                                    <div class="row">
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" placeholder="Customer Name" Width="100%" AutoPostBack="true" OnTextChanged="txtCustomerName_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                TargetControlID="txtCustomerName">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-xl-1 col-md-1">
                                            <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" OnClick="btnresetfilter_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="padding: 0px; margin-top: 10px;">
                                        <div id="DivRoot" align="left">
                                            <div style="overflow: hidden;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="GvInvoiceList" runat="server"
                                                    CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                    AllowPaging="false" ShowHeader="true" PageSize="50" OnRowCommand="GvInvoiceList_RowCommand" DataKeyNames="Id" OnRowDataBound="GvInvoiceList_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Code" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkCustomerCode" runat="server" CssClass="linkbtn" CommandName="CompanyData" Text='<%# Eval("CustomerCode").ToString().Replace(" ", "<br /><br />") %>' CommandArgument='<%# Eval("id") %>' ToolTip="View Details"></asp:LinkButton>
                                                                <%--   <asp:Label ID="lblcustomercode" runat="server" Text='<%# Eval("CustomerCode") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcustomername" runat="server" Text='<%# Eval("BillingCustomer") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoicedate" runat="server" Text='<%# Eval("Invoicedate","{0:d}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProduct" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Grand Total" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrandTotal" runat="server" Text='<%# Eval("GrandTotalFinal") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Prepared By" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrepared" runat="server" Text='<%# Eval("CreatedBy") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="RowEdit" CommandArgument='<%# Eval("Id") %>' ToolTip="Edit"><i class="fa fa-edit" style="font-size:24px;color:black;"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnPDF" runat="server" CommandName="DownloadPDF" CommandArgument='<%# Eval("Id") %>' ToolTip="Download"><i class="fa fa-file-pdf-o" style="font-size:24px;color:red;"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="RowDelete" CommandArgument='<%# Eval("Id") %>' ToolTip="Delete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')"><i class="fa fa-trash" aria-hidden="true" style="font-size:24px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div id="DivFooterRow" style="overflow: hidden">
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
    </div>
        <asp:Button ID="btnprof" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modelprofile" runat="server" TargetControlID="btnprof"
        PopupControlID="PopupViewDetail" OkControlID="Closepopdetail" />

    <asp:Panel ID="PopupViewDetail" runat="server" CssClass="modelprofile1">
            <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 2px; padding-left: 4px;">
                <div class="col-md-3"></div>
                <div class="col-md-6">
                    <div class="profilemodel2">
                        <div class="headingcls">
                            <h4 class="modal-title">Customer Detail
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                        </div>

                        <%--<div class="row" style="margin-right: 0px; margin-left: 10px;">--%>
                        <%--<div class="col-md-12" style="padding-right: 1px!important; background-color: rgb(249, 247, 247)!important;">--%>

                         <div class="row" style="margin-right: 0px!important;margin-left: 10px; padding: 3px;">
                                <div class="col-md-2"><b>Customer Code :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lblcusomercode" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                        <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px; margin-top: 5px;">
                            <div class="col-md-2"><b>Customer Name :</b></div>
                            <div class="col-md-8 lblpopup">
                                <asp:Label ID="lblcname" runat="server" Text=""></asp:Label>
                            </div>
                       
                        </div>

                 <%--       <div class="row" style="background-color: rgb(249, 247, 247); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-2"><b>Email :</b></div>
                            <div class="col-md-4 lblpopup">
                                <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
                            </div>
                      
                        </div>

                        <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-2"><b>Billing Address :</b></div>
                            <div class="col-md-8 lblpopup">
                                <asp:Label ID="lblbillingaddress" runat="server" Text=""></asp:Label>
                            </div>
                        </div>


                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-2"><b>Shipping Address :</b></div>
                            <div class="col-md-8 lblpopup">
                                <asp:Label ID="lblshipaddress" runat="server" Text=""></asp:Label>
                            </div>
                        </div>--%>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-2"><b>Reg. date :</b></div>
                            <div class="col-md-4 lblpopup">
                                <asp:Label ID="lblRegdate" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-2"><b>GST No :</b></div>
                            <div class="col-md-4 lblpopup">
                                <asp:Label ID="lblgstno" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Reg. By :</b></div>
                            <div class="col-md-4 lblpopup">
                                <asp:Label ID="lblregBy" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                      <%--  <hr />--%>
                   <%--     <div class="row" style="margin-right: 0px; margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-4"><b><u>Contact Details :</u></b></div>
                            <div class="col-md-8 lblpopup">
                            </div>
                        </div>
                        <br />
                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name1:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup1" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup1" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup1" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name2:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup2" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup2" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup2" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name3:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup3" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup3" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup3" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name4:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup4" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup4" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup4" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name5:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup5" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup5" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup5" runat="server" Text=""></asp:Label>
                            </div>
                        </div>--%>
                       <%-- <br />--%>

                    </div>
                </div>
            </div>

        </asp:Panel>
</asp:Content>

