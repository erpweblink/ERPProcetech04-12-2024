<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="CustomerDetails.aspx.cs" Inherits="Admin_CustomerDetails" %>

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
            <div class="row">
                <div class="col-md-7">
                    <div class="page-header-breadcrumb">
                        <div style="float: left; font-size: 15px;">
                            <span><i class="feather icon-home"></i>&nbsp;Add Customer</span>
                        </div>
                    </div>
                </div>

                <div class="col-md-5">
                </div>
            </div>

            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5><i class="fa fa-user-plus"></i>Add Customer</h5>
                    </div>

                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-body">
                                    <asp:Label ID="lblMessage" runat="server" CssClass="text-green"></asp:Label>
                                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger"></asp:Label>


                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblCustomerName" runat="server" class="form-label">Customer Name</asp:Label>
                                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblbasic" runat="server" class="form-label">Basic Amount</asp:Label>
                                            <asp:TextBox ID="txtBasicAmount" runat="server" CssClass="form-control" OnTextChanged="txtBasicAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblcgst" runat="server" class="form-label">CGST</asp:Label>
                                            <asp:TextBox ID="txtCGST" runat="server" CssClass="form-control" OnTextChanged="txtCGST_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblsgst" runat="server" class="form-label">SGST</asp:Label>
                                            <asp:TextBox ID="txtSGST" runat="server" CssClass="form-control" OnTextChanged="txtSGST_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lbligst" runat="server" class="form-label">IGST</asp:Label>
                                            <asp:TextBox ID="txtIGST" runat="server" CssClass="form-control" OnTextChanged="txtIGST_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblgrand" runat="server" class="form-label">Grand Total</asp:Label>
                                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <center>
                                                <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-success" Width="100%" Text="Add" OnClick="btnAdd_Click" /></center>
                                        </div>
                                        <div class="col-md-2">
                                            <center>
                                                <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Width="100%" Text="Reset" OnClick="btnClear_Click" /></center>
                                        </div>
                                        <div class="col-md-4"></div>

                                    </div>
                                    <br />

                                </div>
                            </div>
                        </div>
                    </div>

                    <h3 class="container"><span class="starcls"><i class="feather icon-list"></i>&nbsp;Customer List</span></h3>

                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <br />
                                            <div class="dt-responsive table-responsive">

                                                <asp:GridView ID="gvCustomerDetails" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                    DataKeyNames="CustomerID" OnRowCommand="GvUsers_RowCommand" OnRowDeleting="GvUsers_RowDeleting" AllowPaging="true" PageSize="20">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcustnamr" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Basic Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBasicAmount" runat="server" Text='<%# Eval("BasicAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCGST" runat="server" Text='<%# Eval("CGST") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSGST" runat="server" Text='<%# Eval("SGST") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("IGST") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GrandTotal" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrandTotal" runat="server" Text='<%# Eval("GrandTotal") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkbtnedit" runat="server" CssClass="linkbtn" CommandName="RowEdit" CommandArgument='<%# Eval("CustomerID") %>' ToolTip="Edit">Edit</asp:LinkButton>&nbsp;|&nbsp;
                                                           <asp:LinkButton ID="Linkbtndelete" runat="server" CssClass="linkbtn" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this record ?')" CommandArgument='<%# Eval("CustomerID") %>' ToolTip="Delete">Delete</asp:LinkButton>&nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>










