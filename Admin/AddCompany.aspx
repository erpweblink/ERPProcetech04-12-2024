<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AddCompany.aspx.cs" Inherits="Admin_AddCompany" %>

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
                            <span><a href="AllCompanyList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Customer List</a>&nbsp;&nbsp;
                                <%--<a href="UploadCompanyData.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Upload Excel</a>--%>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5>Add Customer</h5>
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
                                            <div class="col-md-2 spancls">Customer Name<i class="reqcls">*&nbsp;</i> : </div>
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
                                            <div class="col-md-2 spancls">Type of Supply For &nbsp;&nbsp;&nbsp;&nbsp; E-Invoice : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlTypeofSupply" OnTextChanged="ddlTypeofSupply_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="-- Select Type of Supply --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="B2B"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="SEZWOP"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="EXPWOP"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <br />

                                        <div class="row" id="DivCountryCode" runat="server" visible="false">
                                            <div class="col-md-2 spancls">Country Name : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlCountryCode" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div id="Brtag" runat="server" visible="false">
                                            <br />
                                        </div>

                                        <div class="row">
                                            <div class="col-md-2 spancls">GST Number : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtgstno" MaxLength="15" CssClass="form-control" runat="server" Width="100%" OnTextChanged="txtgstno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <%--                                                <asp:RegularExpressionValidator ID="revGSTNumber" runat="server"
                                                    ControlToValidate="txtgstno"
                                                    ValidationExpression="^\d{2}[A-Z]{5}\d{4}[A-Z]{1}\d[Z]{1}[A-Z\d]{1}$"
                                                    Display="Dynamic"
                                                    ErrorMessage="Invalid GST Number. GST number should be in the format 27ATFPS1959J1Z4"
                                                    ForeColor="Red" />--%>
                                            </div>
                                            <div class="col-md-2 spancls">Credit Limit : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtcredit" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>

                                        <br />

                                        <div class="row">
                                            <div class="col-md-2 spancls">Details Billing Address : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="DetailsBilling" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ErrorMessage="Please Enter Billing Address"
                                                    ControlToValidate="txtbillingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">Details Shipping Address : </div>
                                            <div class="col-md-4">

                                                <asp:TextBox ID="DetailsShipping" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ErrorMessage="Please Enter Shipping Address"
                                                    ControlToValidate="txtshippingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <br />
                                        <%--                                        <div class="col-md-4">
                                          

                                        </div>--%>

                                        <div class="row">
                                            <div class="col-md-2 spancls">Billing Address : </div>
                                            <div class="col-md-4">

                                                <asp:TextBox ID="txtbillingaddress" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Billing Address"
                                                    ControlToValidate="txtbillingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">Shipping Address : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtshippingaddress" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Shipping Address"
                                                    ControlToValidate="txtshippingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>




                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Billing Location : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtbillinglocation" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Billing Location"
                                                    ControlToValidate="txtbillinglocation" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">Shipping Location : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtshippinglocation" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Please Enter Shipping Location"
                                                    ControlToValidate="txtshippinglocation" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Billing Pincode : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtbillingpin" MaxLength="6" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Please Enter Billing Pincode"
                                                    ControlToValidate="txtbillingpin" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revPincode" runat="server"
                                                    ControlToValidate="txtbillingpin"
                                                    ValidationExpression="^\d{6}$"
                                                    Display="Dynamic"
                                                    ErrorMessage="Invalid PIN Code. PIN code should be a 6-digit number."
                                                    ForeColor="Red" />
                                            </div>
                                            <div class="col-md-2 spancls">Shipping Pincode : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtshippingpin" MaxLength="6" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Please Enter Shipping Pincode"
                                                    ControlToValidate="txtshippingpin" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ControlToValidate="txtshippingpin"
                                                    ValidationExpression="^\d{6}$"
                                                    Display="Dynamic"
                                                    ErrorMessage="Invalid PIN Code. PIN code should be a 6-digit number."
                                                    ForeColor="Red" />
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Billing State Code : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtbillingState" MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter Billing State Code"
                                                    ControlToValidate="txtbillingState" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revStateCode" runat="server"
                                                    ControlToValidate="txtbillingState"
                                                    ValidationExpression="^\d{2}$"
                                                    Display="Dynamic"
                                                    ErrorMessage="Invalid State Code. State code should be a 2-digit number."
                                                    ForeColor="Red" />
                                            </div>
                                            <div class="col-md-2 spancls">Shipping State Code : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtshippingstate" MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ErrorMessage="Please Enter Shipping State Code"
                                                    ControlToValidate="txtshippingstate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                    ControlToValidate="txtshippingstate"
                                                    ValidationExpression="^\d{2}$"
                                                    Display="Dynamic"
                                                    ErrorMessage="Invalid State Code. State code should be a 2-digit number."
                                                    ForeColor="Red" />
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row" runat="server" id="paym1">
                                            <div class="col-md-2 spancls">Payment Term : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlpaymentterm" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpaymentterm_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 spancls"></div>
                                            <%-- </div>--%>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtpaymentterm" Placeholder="Specify Payment Term" Visible="false" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="row" runat="server" id="Div1">
                                            <div class="col-md-2 spancls"></div>
                                            <%-- </div>--%>
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="checkforcopy_CheckedChanged" AutoPostBack="true" />
                                                <label for="checkforcopy" class="ml-2">Copy Billing & shipping Details</label>
                                            </div>

                                        </div>



                                        <%--  Contact Detail --%>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <br />
                                                <h5 style="color: #000 !important;"><u>Contact Detail : </u></h5>

                                                <br />
                                                <%-- 1st --%>
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Name 1:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtownname1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">Mobile:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtmobile1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <br />

                                                <div class="row">
                                                    <div class="col-md-2 spancls">Email:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtemail1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic" ErrorMessage="Invalid Email" runat="server"
                                                            ControlToValidate="txtemail1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="col-md-2 spancls">Designation:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtdesig1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <hr />

                                                <%-- 2nd --%>
                                                <asp:Panel ID="row2" runat="server" Visible="false">
                                                    <div class="row rwotoppadding">
                                                        <div class="col-md-2 spancls">Name 2 :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtownname2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Mobile :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtmobile2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationExpression="^([0-9]{10})" Display="Dynamic" ErrorMessage="Invalid Mobile No" runat="server"
                                                                ControlToValidate="txtmobile2" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <br />
                                                    </div>

                                                    <div class="row rwotoppadding">
                                                        <div class="col-md-2 spancls">Email : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic" ErrorMessage="Invalid Email" runat="server"
                                                                ControlToValidate="txtemail2" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="col-md-2 spancls">Designation :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtdesig2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <br />
                                                    </div>

                                                    <hr />
                                                </asp:Panel>
                                                <%-- 3rd --%>
                                                <asp:Panel ID="row3" runat="server" Visible="false">
                                                    <div class="row rwotoppadding">
                                                        <br />
                                                        <div class="col-md-2 spancls">Name 3 :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtownname3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Mobile :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtmobile3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="^([0-9]{10})" Display="Dynamic" ErrorMessage="Invalid Mobile No" runat="server"
                                                                ControlToValidate="txtmobile3" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row rwotoppadding">
                                                        <br />
                                                        <div class="col-md-2 spancls">Email : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic" ErrorMessage="Invalid Email" runat="server"
                                                                ControlToValidate="txtemail3" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="col-md-2 spancls">Designation :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtdesig3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <hr />
                                                </asp:Panel>
                                                <%-- 4th --%>
                                                <asp:Panel ID="row4" runat="server" Visible="false">
                                                    <div class="row rwotoppadding">
                                                        <br />
                                                        <div class="col-md-2 spancls">Name 4 :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtownname4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Mobile : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtmobile4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationExpression="^([0-9]{10})" Display="Dynamic" ErrorMessage="Invalid Mobile No" runat="server"
                                                                ControlToValidate="txtmobile4" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row rwotoppadding">
                                                        <br />
                                                        <div class="col-md-2 spancls">Email : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic" ErrorMessage="Invalid Email" runat="server"
                                                                ControlToValidate="txtemail4" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="col-md-2 spancls">Designation : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtdesig4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </asp:Panel>
                                                <%-- 5th --%>
                                                <asp:Panel ID="row5" runat="server" Visible="false">
                                                    <div class="row rwotoppadding">
                                                        <br />
                                                        <div class="col-md-2 spancls">Name 5 :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtownname5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 spancls">Mobile :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtmobile5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationExpression="^([0-9]{10})" Display="Dynamic" ErrorMessage="Invalid Mobile No" runat="server"
                                                                ControlToValidate="txtmobile5" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row rwotoppadding">
                                                        <br />
                                                        <div class="col-md-2 spancls">Email : </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtemail5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic" ErrorMessage="Invalid Email" runat="server"
                                                                ControlToValidate="txtemail5" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="col-md-2 spancls">Designation :</div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtdesig5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </asp:Panel>
                                                <div class="row rwotoppadding">
                                                    <div class="col-md-11"></div>
                                                    <div class="col-md-1">
                                                        <asp:Button ID="btnadd2" runat="server" Text="+ Add" OnClick="btnadd2_Click" />
                                                        <asp:Button ID="btnadd3" Visible="false" runat="server" Text="+ Add" OnClick="btnadd3_Click" />
                                                        <asp:Button ID="btnadd4" Visible="false" runat="server" Text="+ Add" OnClick="btnadd4_Click" />
                                                        <asp:Button ID="btnadd5" Visible="false" runat="server" Text="+ Add" OnClick="btnadd5_Click" />
                                                    </div>
                                                </div>
                                                <br />
                                                <asp:HiddenField ID="hiddenid" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <br />
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-2">
                                                <center>
                                                    <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Add Customer" OnClick="btnadd_Click" /></center>
                                            </div>
                                            <div class="col-md-2">
                                                <center>
                                                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Width="100%" Text="Reset" OnClick="btnreset_Click" /></center>
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
