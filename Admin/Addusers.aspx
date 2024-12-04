<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Addusers.aspx.cs" Inherits="Admin_Addusers" %>

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

        @media (min-width: 1200px) {
            .container {
                max-width: 1250px !important;
            }
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
                            <span><i class="feather icon-home"></i>&nbsp;Add User</span>
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
                        <h5><i class="fa fa-user-plus"></i>Add User</h5>
                    </div>

                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <br />
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
                                                    &nbsp;<input type="checkbox" onclick="ShowPsw1()">&nbsp;&nbsp;<p style="font-size: 12px; display: inline;">Show Password</p>
                                                </div>
                                                <div class="col-md-2"></div>
                                                <div class="col-md-4">
                                                    &nbsp;<input type="checkbox" onclick="ShowPsw2()">&nbsp;&nbsp;<p style="font-size: 12px; display: inline;">Show Password</p>
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
                                                    <asp:DropDownList ID="ddldept" CssClass="form-control" runat="server" Width="100%">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Admin</asp:ListItem>
                                                        <asp:ListItem>Sales</asp:ListItem>
                                                        <asp:ListItem>Account</asp:ListItem>
                                                        <asp:ListItem>Production</asp:ListItem>
														<asp:ListItem>Purchase</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Please Select Department"
                                                        ControlToValidate="ddldept" InitialValue="Select" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-md-2 spancls">Status<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlstatus" CssClass="form-control" runat="server" Width="100%">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Activate</asp:ListItem>
                                                        <asp:ListItem>Deactivate</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Please Select"
                                                        ControlToValidate="ddlstatus" InitialValue="Select" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                              <div class="col-md-2 spancls">Role</div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlRole" CssClass="form-control" runat="server" Width="100%">
                                                        
                                                    </asp:DropDownList>
                                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Please Select Role"
                                                        ControlToValidate="ddlRole" InitialValue="0" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
											<br />
                                         <%--   <div class="row" runat="server" id="divRole" visible="false">
                                                <div class="col-md-2 spancls">Role<i class="reqcls">*&nbsp;</i> :</div>
                                                <div class="col-md-10">
                                                    <asp:CheckBoxList
                                                        ID="chkRoleList"
                                                        runat="server"
                                                        AutoPostBack="true"
                                                        BorderWidth="1"
                                                        CellPadding="5"
                                                        CellSpacing="5"
                                                        RepeatColumns="7"
                                                        BorderColor="LightGray"
                                                        CssClass="test"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Drawing" Value="drawing">&nbsp;</asp:ListItem>
                                                        <asp:ListItem Text="Laser Programming" Value="laserprogramming"></asp:ListItem>
                                                        <asp:ListItem Text="Laser Cutting" Value="lasercutting"></asp:ListItem>
                                                        <asp:ListItem Text="CNC Bending" Value="bending"></asp:ListItem>
                                                        <asp:ListItem Text="Welding" Value="welding"></asp:ListItem>
                                                        <asp:ListItem Text="Powder Coating" Value="powdercoating"></asp:ListItem>
                                                        <asp:ListItem Text="Assembly" Value="assembly"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                    <span style="color:green">You can assign multiple roles for a single user</span>
                                                </div>
                                            </div>--%>
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
                            </div>
                        </div>
                    </div>

                    <h3 class="container"><span class="starcls"><i class="feather icon-list"></i>&nbsp;Users List</span></h3>

                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <br />
                                            <div class="dt-responsive table-responsive">
                                                <asp:GridView ID="GvUsers" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                    DataKeyNames="id" OnRowDeleting="GvUsers_RowDeleting" OnRowCommand="GvUsers_RowCommand"
                                                    OnRowDataBound="GvUsers_RowDataBound" AllowPaging="true" OnPageIndexChanging="GvUsers_PageIndexChanging" PageSize="20">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Emp. Code" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("empcode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkname" runat="server" CssClass="linkbtn" CommandName="empname" Text='<%# Eval("name") %>' CommandArgument='<%# Eval("id") %>' ToolTip="View Details"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Email">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Role" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrole" runat="server" Text='<%# Eval("role") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkbtnedit" runat="server" CssClass="linkbtn" CommandName="RowEdit" CommandArgument='<%# Eval("id") %>' ToolTip="Edit">Edit</asp:LinkButton>&nbsp;|&nbsp;
                                                                <asp:LinkButton ID="Linkbtndelete" runat="server" CssClass="linkbtn" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this record ?')" CommandArgument='<%# Eval("id") %>' ToolTip="Delete">Delete</asp:LinkButton>&nbsp;|&nbsp;
                                                    <asp:LinkButton ID="linkaccount" runat="server" CssClass="linkbtn" CommandName="status" OnClientClick="return confirm('Do you want to Activate/Deactivate this account ?')" CommandArgument='<%# Eval("id") %>' ToolTip="Activate/Deactivate" Text="Activated"></asp:LinkButton>
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



    <%--  Profile--%>
    <asp:Button ID="btnprof" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modelprofile" runat="server" TargetControlID="btnprof"
        PopupControlID="PopupViewDetail" OkControlID="Closepopdetail" />

    <asp:Panel ID="PopupViewDetail" runat="server" CssClass="modelprofile1">
        <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 1px; padding-left: 1px;">
            <div class="col-md-2"></div>
            <div class="col-md-10">
                <div class="profilemodel2">
                    <div class="headingcls">
                        <h4 class="modal-title">User Detail
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                    </div>

                    <br />
                    <div class="row" style="background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px; margin-left: 10px;">
                        <div class="col-md-1"></div>
                        <div class="col-md-2"><b>Name :</b></div>
                        <div class="col-md-8 lblpopup">
                            <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-1"></div>
                    </div>
                    <br />

                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-1"></div>
                        <div class="col-md-2"><b>Email :</b></div>
                        <div class="col-md-8 lblpopup">
                            <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-1"></div>
                    </div>
                    <br />

                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-1"></div>
                        <div class="col-md-2"><b>Email Pswd :</b></div>
                        <div class="col-md-3 lblpopup">
                            <asp:TextBox ID="txtemailpsw1" runat="server" onClick="this.select();"></asp:TextBox>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-2"><b>Panel Pswd :</b></div>
                        <div class="col-md-3 lblpopup">
                            <asp:TextBox ID="txtpanelpsw1" runat="server" onClick="this.select();"></asp:TextBox>
                        </div>
                    </div>
                    <br />

                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-1"></div>
                        <div class="col-md-2"><b>Contact No :</b></div>
                        <div class="col-md-3 lblpopup">
                            <asp:Label ID="lblmobile" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-2"><b>Department :</b></div>
                        <div class="col-md-3 lblpopup">
                            <asp:Label ID="lbldept" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />

                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-1"></div>
                        <div class="col-md-2"><b>Status :</b></div>
                        <div class="col-md-3 lblpopup">
                            <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-2"><b>Reg. Date :</b></div>
                        <div class="col-md-3 lblpopup">
                            <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />

                </div>
            </div>
        </div>

    </asp:Panel>
    <%--  Profile--%>

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

