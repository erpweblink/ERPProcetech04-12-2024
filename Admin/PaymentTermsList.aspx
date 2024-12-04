<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentTermsList.aspx.cs" Inherits="Admin_PaymentTermsList" MasterPageFile="~/Admin/AdminMasterPage.master" %>

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


        .action-btn {
            padding: 5px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

            .action-btn:hover {
                background-color: #ddd;
            }

        .edit-btn {
            background-color: #4CAF50;
            color: #fff;
        }

            .edit-btn:hover {
                background-color: #3e8e41;
            }

        .delete-btn {
            background-color: #f44336;
            color: #fff;
        }

            .delete-btn:hover {
                background-color: #da190b;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="page-wrapper">
        <div class="page-body">
            <div class="container py-3">
                <div class="card">
                   <div class="col-md-12">
                    <div class="page-header-breadcrumb">
                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                            <span id="btnAddCompany" runat="server"><a href="PaymentTermsMaster.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Payment Terms</a>&nbsp;&nbsp;
                                <%--<a href="UploadCompanyData.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Upload Excel</a>--%>
                            </span>
                        </div>
                    </div>
                </div>
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5>payment Terms List</h5>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <%--  <div class="card">--%>
                            <div class="card-header">
                                <div class="row">
                                </div>

                                <div class="card-body">
                                    <asp:GridView ID="GVTerms" runat="server" CellPadding="4" DataKeyNames="ID" PageSize="10" AllowPaging="true" Width="100%"
                                        CssClass="grivdiv pagination-ys" AutoGenerateColumns="false" OnRowCommand="GVTerms_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Payment Terms" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="terms" runat="server" Text='<%#Eval("Terms")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Created  Date" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsodate" runat="server" Text='<%# Eval("CreatedON", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                    <%--<asp:Label ID="Quotationdate" runat="server" Text='<%#Eval("Quotationdate")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" Height="27px" CausesValidation="false" CommandName="RowEdit" CommandArgument='<%#Eval("ID")%>' CssClass="action-btn edit-btn">Edit</asp:LinkButton>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnDelete" runat="server" Height="27px" ToolTip="Delete" CausesValidation="false" CommandName="RowDelete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CommandArgument='<%#Eval("ID")%>' CssClass="action-btn delete-btn">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <%-- </div>--%>
                            </div>
                        </div>

                        <%-- <br />--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
