<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AddCompany.aspx.cs" Inherits="Admin_AddCompany" %>

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
         .aspNetDisabled{
             cursor:not-allowed !important;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="page-wrapper">
        <div class="page-body">
            <asp:HiddenField ID="HFcname" runat="server" />
            <asp:HiddenField ID="HFoname" runat="server" />
            <asp:HiddenField ID="HFemail" runat="server" />
            <asp:HiddenField ID="HFmobile" runat="server" />
            <asp:HiddenField ID="HFvisitdate" runat="server" />
            <asp:HiddenField ID="HFclienttype" runat="server" />
            <asp:HiddenField ID="HFbde" runat="server" />
            <asp:HiddenField ID="HFaddress" runat="server" />
            <asp:HiddenField ID="HFvisitingcard" runat="server" />
            <asp:HiddenField ID="hfwebsite" runat="server" />
            <asp:HiddenField ID="hfregby" runat="server" />


            <div class="row">
                <div class="col-md-7">
                    <%--<div class="page-header-breadcrumb">
                        <div style="float: left; font-size: 15px;">
                            <span><i class="feather icon-home"></i>&nbsp;Add Company</span>
                        </div>
                    </div>--%>
                </div>


                <div class="col-md-5">
                    <div class="page-header-breadcrumb">
                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                            <span><a href="AllCompanyList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Company List</a>&nbsp;&nbsp;
                                <%--<a href="UploadCompanyData.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Upload Excel</a>--%>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5>Add Company</h5>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <%--  <div class="card">--%>
                            <div class="card-header">
                                <div class="row">
                                    <div class="col-md-12">
                                        <%--  <br />--%>
                                        <br />
                                       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>--%>
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Company Name<i class="reqcls">*&nbsp;</i> : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtcname" CssClass="form-control" runat="server" Width="100%" OnTextChanged="txtcname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Company Name"
                                                            ControlToValidate="txtcname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                            TargetControlID="txtcname">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 spancls">Contact Name<i class="reqcls">*&nbsp;</i> : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtownname" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Contact Name"
                                                            ControlToValidate="txtownname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                          <%--  </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-2 spancls">Email<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Email"
                                                    ControlToValidate="txtemail" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">Contact Number<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtmobile" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Mobile No."
                                                    ControlToValidate="txtmobile" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="col-md-2 spancls">GST Number<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtgstno" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Please Enter GST No."
                                                    ControlToValidate="txtgstno" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        

                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Billing Address : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtbillingaddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                    ControlToValidate="txtbillingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="col-md-2 spancls">Shipping Address : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtshippingaddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                    ControlToValidate="txtshippingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                        

                                        <br />
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-2">
                                                <center> <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Add Company" OnClick="btnadd_Click"/></center>
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
        <%-- <br />--%>
    </div>




</asp:Content>
