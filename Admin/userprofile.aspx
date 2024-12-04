<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="userprofile.aspx.cs" Inherits="Admin_userprofile" %>


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
            margin-top: 45px;
            padding: 17px 5px 18px 22px;
            /*border: 2px solid #c5bebe;*/
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
            float: right;
            font-size: 15px !important;
            /*font-weight: 600;*/
            color: #ff5a5f !important;
            border: 0px groove !important;
            background-color: none !important;
            margin-right: 10px !important;
            cursor: pointer;
        }

        hr {
            margin-top: 5px !important;
            margin-bottom: 15px !important;
            border: 1px solid #eae6e6 !important;
            width: 100%;
        }
        .reqcls{
            color:red;
            font-weight:600;
            font-size:14px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="page-wrapper">
        <div class="page-body">
            <div class="row">
                <div class="col-md-7">
                    <div class="page-header-breadcrumb">
                        <div style="float: left; font-size: 15px;">
                            <span><i class="feather icon-home"></i>&nbsp;Profile</span>
                        </div>
                    </div>
                </div>


                <div class="col-md-5">
                    <%--<div class="page-header-breadcrumb">
                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                            <span><a href="BlogsEditor.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Blogs Page</a>&nbsp;&nbsp;
                                <a href="Commentslist.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Comments</a>
                            </span>
                        </div>
                    </div>--%>
                </div>
            </div>

            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5><i class="fa fa-user-plus"></i>My Profile</h5>
                    </div>

                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <%--<div class="card">--%>
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Name<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtname" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Name"
                                                        ControlToValidate="txtname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2 spancls">Email<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" Width="100%" OnTextChanged="txtemail_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Email"
                                                        ControlToValidate="txtemail" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-md-2 spancls">Email Password<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtemailpsw" TextMode="Password" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Email Password"
                                                        ControlToValidate="txtemailpsw" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2 spancls">Panel Password<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtpanelpsw" TextMode="Password" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Panel Password"
                                                        ControlToValidate="txtpanelpsw" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-4">
                                                    &nbsp;<input type="checkbox" onclick="ShowPsw1()">&nbsp;&nbsp;<p style="font-size:12px;display: inline;">Show Password</p>
                                                </div>
                                                <div class="col-md-2"></div>
                                                <div class="col-md-4">
                                                    &nbsp;<input type="checkbox" onclick="ShowPsw2()">&nbsp;&nbsp;<p style="font-size:12px;display: inline;">Show Password</p>
                                                </div>
                                            </div>
                                            <br />
                                            <script>
                                                function ShowPsw1() {
                                                    var x = document.getElementById("ContentPlaceHolder1_txtemailpsw");
                                                    if (x.type === "password") {
                                                        x.type = "text";
                                                    } else {
                                                        x.type = "password";
                                                    }
                                                }

                                                function ShowPsw2() {
                                                    var x = document.getElementById("ContentPlaceHolder1_txtpanelpsw");
                                                    if (x.type === "password") {
                                                        x.type = "text";
                                                    } else {
                                                        x.type = "password";
                                                    }
                                                }
                                            </script>
                                            <div class="row">
                                                <div class="col-md-2 spancls">Mobile : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtmobile" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 spancls">Department<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddldept" Enabled="false" CssClass="form-control" runat="server" Width="100%">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Admin</asp:ListItem>
                                                        <asp:ListItem>Sales</asp:ListItem>
                                                        <asp:ListItem>BDE</asp:ListItem>
                                                        <asp:ListItem>Designer</asp:ListItem>
                                                        <asp:ListItem>Developer</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Please Select Department"
                                                        ControlToValidate="ddldept" InitialValue="Select" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <br />

                                       
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-2">
                                                    <center> <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-success" Width="100%" Text="Add User" OnClick="btnadd_Click"/></center>
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

                </div>
            </div>
        </div>
    </div>



</asp:Content>

