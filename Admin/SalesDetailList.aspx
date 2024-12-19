<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SalesDetailList.aspx.cs" Inherits="Admin_CustomerDetailList" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">   
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>       
        <asp:HiddenField ID="BdeCode" runat="server" />
        <asp:HiddenField ID="BdeMailId" runat="server" />
        <div class="page-wrapper">
            <div class="page-body">
                <div class="container py-3">
                    <div class="card">
                        <div class="card-header bg-primary text-uppercase text-white" style="width: 96% !important">
                            <div class="row">
                                <div class="col-md-6">
                                    <h4><i class="fa fa-universal-access"></i>&nbsp;&nbsp;Sales List</h4>
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2" style="float: right; margin-bottom: 11px;">
                                    <%-- Old color #01a9ac --%>
                                    <span id="btnAddCompany" runat="server"><a href="SalesDetails.aspx" style="font-size: 15px; border: 3px solid #ed8f03; padding: 5px 19px; background: #ed8f03; color: aliceblue;">&nbsp;<b>Add Sales</b></a>&nbsp;&nbsp;                               
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-12 col-md-12">
                                <div class="card">
                                    <div class="card-header">

                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:Label ID="lblCustName" runat="server" class="control-label"><b>Customer Name</b></asp:Label>
                                                <asp:TextBox ID="txtcnamefilter" class="form-control" runat="server" placeholder="Customer name" Width="100%" OnTextChanged="txtcnamefilter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                    TargetControlID="txtcnamefilter">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblfromdate" runat="server" class="control-label"><b>From Date</b></asp:Label>
                                                <asp:TextBox runat="server" class="form-control " ID="txtDateSearchfrom" name="Search" TextMode="Date" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lbltodate" runat="server" class="control-label"><b>To Date</b></asp:Label>
                                                <asp:TextBox runat="server" class="form-control " ID="txtDateSearchto" name="Search" TextMode="Date" />
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6" style="display: flex; align-items: end;margin-top: 23px;">
                                                    <asp:LinkButton ID="lnkBtnsearch" runat="server" CssClass="btn btn-primary txtsear" Style="margin-right: 5px !important; padding: 6px 3px 6px 9px !important" OnClick="ddlsalesMainfilter_TextChanged"><i class="fa fa-search" style="font-size:24px"></i></asp:LinkButton>
                                                </div>
                                                <div class="col-md-6" style="display: flex; align-items: end; margin-left: 51px; margin-top: -39px;">
                                                    <asp:LinkButton ID="lnkrefresh" runat="server" CssClass="btn btn-primary txtsear mt-top" Style="margin-right: 5px !important; padding: 6px 3px 6px 9px !important" OnClick="btnresetfilter_Click"><i class="fa fa-refresh" style="font-size:24px"></i></asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class="col-md-2" style="display: flex; align-items: end;">
                                                <button id="btnexportexcel" runat="server" class="btn btn-primary " style="padding: 8px 5px 9px 11px !important; margin-left:79px" onserverclick="btnexportexcel_Click">
                                                    Export To Excel <i class="fa fa-file-excel"></i>
                                                </button>
                                            </div>
                                            <div class="d-none">
                                                <asp:DropDownList ID="ddlsalesMainfilter" runat="server" AutoPostBack="true" Style="margin-bottom: 5px; width: 100%;"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="padding: 0px; margin-top: 24px">
                                            <div id="DivRoot" align="left">
                                                <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">

                                                    <asp:GridView ID="gvCustomerDetails" runat="server" ShowFooter="true" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                        DataKeyNames="CustomerID" OnRowCommand="GvUsers_RowCommand" OnRowDeleting="GvUsers_RowDeleting"
                                                        OnRowDataBound="gvCustomerDetails_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Invoice No" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcustnamr" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Basic Amount" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBasicAmount" runat="server" Text='<%# Eval("BasicAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalBasicAmount" runat="server" Font-Bold="True"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CGST") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("SGST") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("IGST") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="GST Amount" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGSTAmount" runat="server" Text='<%# Eval("GSTTotAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalGSTAmount" runat="server" Font-Bold="True"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Grand Total" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrandTotal" runat="server" Text='<%# Eval("GrandTotal") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalGrandTotal" runat="server" Font-Bold="True"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="linkbtnedit" runat="server" CssClass="linkbtn" CommandName="RowEdit" Style="background-color: #09989a !important; color: #fff; padding: 3px 5px 3px 5px" CommandArgument='<%# Eval("CustomerID") %>' ToolTip="Edit">Edit</asp:LinkButton>
                                                                    <asp:LinkButton ID="Linkbtndelete" runat="server" CssClass="linkbtn" CommandName="Delete" Style="background-color: #ff0000 !important; color: #fff; padding: 3px 5px 3px 5px" OnClientClick="return confirm('Do you want to delete this record ?')" CommandArgument='<%# Eval("CustomerID") %>' ToolTip="Delete">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Center" BackColor="#ed8f03" />
                                                    </asp:GridView>
                                                    <div id="DivFooterRow" style="overflow: hidden">
                                                    </div>

                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
