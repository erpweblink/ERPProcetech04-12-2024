<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaxMaster.aspx.cs" Inherits="Admin_TaxMaster" MasterPageFile="~/Admin/AdminMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
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

        @media (min-width: 1200px) {
            .container {
                max-width: 1250px !important;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="page-wrapper">
        <div class="page-body">
            <div class="row">
                <div class="col-md-7">
                    <div class="page-header-breadcrumb">
                        <div style="float: left; font-size: 15px;">
                            <span><i class="feather icon-home"></i>&nbsp;Tax Master</span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <h5>Add Tax</h5>

                </div>

                <div class="card-body">
                    <div class="col-md-12">
                        <div class="row">
                            <!-- Tax Jurisdictions -->
                            <div class="col-md-2 spancls">Tax Jurisdictions<i class="reqcls">*&nbsp;</i> : </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddljusidctions" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="federal">Federal</asp:ListItem>
                                    <asp:ListItem Value="state">State</asp:ListItem>
                                    <asp:ListItem Value="local">Local</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <!-- Tax Types -->
                            <div class="col-md-2 spancls">Tax Types<i class="reqcls">*&nbsp;</i> : </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddltaxtype" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddltaxtype_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="CGSTSGST">CGST/SGST</asp:ListItem>
                                    <asp:ListItem Value="IGST">IGST</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <br />
                        <div class="row">
                            <!-- Tax Rate -->
                            <div class="col-md-2 spancls">Tax Category <i class="reqcls">*&nbsp;</i> : </div>
                            <div class="col-md-4">
                  <%--              <asp:DropDownList ID="ddlgst" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="NoGST">No GST</asp:ListItem>
                                    <asp:ListItem Value="CSGST18">CGST-SGST-18</asp:ListItem>
                                    <asp:ListItem Value="IGST18">IGST 18</asp:ListItem>
                                    <asp:ListItem Value="CSGST12">CGST-SGST-12</asp:ListItem>
                                    <asp:ListItem Value="IGST12">IGST 12</asp:ListItem>
                                </asp:DropDownList>--%>

                                <asp:TextBox ID="ddlgst" runat="server"  CssClass="form-control"></asp:TextBox>


                            </div>



                            <!-- Effective Date -->
                            <div class="col-md-2 spancls">Effective Date<i class="reqcls">*&nbsp;</i> : </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtEffectiveDate" CssClass="form-control" runat="server" placeholder="yyyy-mm-dd" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <br />

                        <div class="row">
                            <!-- Tax Rate -->
                            <div class="col-md-2 spancls">CGST <i class="reqcls">*&nbsp;</i> : </div>
                            <div class="col-md-4">
   

                                <asp:TextBox ID="txtcgst" runat="server"  CssClass="form-control"></asp:TextBox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="* Required" ControlToValidate="txtcgst" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid number. Only numbers and decimals are allowed." ControlToValidate="txtcgst" ValidationExpression="^\d+(\.\d+)?$" ValidationGroup="form1" ForeColor="Red" Display="Dynamic" />


                            </div>

                            <!-- Effective Date -->
                            <div class="col-md-2 spancls">SGST<i class="reqcls">*&nbsp;</i> : </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSgst" CssClass="form-control" runat="server"  ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="* Required" ControlToValidate="txtSgst" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a valid number. Only numbers and decimals are allowed." ControlToValidate="txtSgst" ValidationExpression="^\d+(\.\d+)?$" ValidationGroup="form1" ForeColor="Red" Display="Dynamic" />
                            </div>
                        </div>

                        <br />
                        <div class="row">
                               <div class="col-md-2 spancls">IGST<i class="reqcls">*&nbsp;</i> : </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtigst" CssClass="form-control" runat="server" ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="* Required" ControlToValidate="txtigst" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter a valid number. Only numbers and decimals are allowed." ControlToValidate="txtigst" ValidationExpression="^\d+(\.\d+)?$" ValidationGroup="form1" ForeColor="Red" Display="Dynamic" />
                            </div>
                        </div>



                    </div>
                </div>
                <div class="card-footer">
                    <div class="row">
                        <div class="col-md-6"></div>
                        <div class="col-md-26" style="text-align:center">
                            <center>
                                <asp:Button ID="btnadd" runat="server" CssClass="btn btn-success" Width="100%" Text="Submit"  OnClick="btnadd_Click"   Visible="false"/>
                             <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success" Width="100%"  Text="Update"   OnClick="btnupdate_Click"  Visible="false" /></center>
                        </div>
                        <div class="col-md-6"></div>

                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>
