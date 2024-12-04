<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentTermsMaster.aspx.cs" Inherits="Admin_PaymentTermsMaster" MasterPageFile="~/Admin/AdminMasterPage.master" %>

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

        .MultiLine {
            height: 52px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="page-wrapper">
        <div class="page-body">
            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5>Add Payment Terms</h5>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <%--  <div class="card">--%>
                            <div class="card-header">
                                <div class="row">
                                    <div class="col-md-12">
                                        <%--  <br />--%>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Add Terms<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtpaymentTerms" CssClass="form-control" runat="server" Width="100%" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Submit" OnClick="btnadd_Click" />
                                                <asp:Button ID="btnupdate" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Update"   OnClick="btnupdate_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-footer" style="text-align: center;">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
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
    s
</asp:Content>

