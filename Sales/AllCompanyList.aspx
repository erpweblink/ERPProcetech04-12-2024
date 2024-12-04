<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AllCompanyList.aspx.cs" Inherits="Admin_AllCompanyList" %>

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
                max-width: 1250px !important;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <asp:HiddenField ID="BdeCode" runat="server" />
    <asp:HiddenField ID="BdeMailId" runat="server" />
    <div class="page-wrapper">
        <div class="page-body">

            <div class="row">
                <div class="col-md-7">
                    <%--<div class="page-header-breadcrumb">
                        <div style="float: left; font-size: 15px;">
                            <span><i class="feather icon-home"></i>&nbsp;Company List</span>
                        </div>
                    </div>--%>
                </div>


                <div class="col-md-5">
                    <div class="page-header-breadcrumb">
                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                            <span><a href="AddCompany.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Company</a>&nbsp;&nbsp;
                                <%--<a href="UploadCompanyData.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Upload Excel</a>--%>
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
                                <h5>Company List</h5>
                            </div>
                            <div class="col-md-6">
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlsalesMainfilter" runat="server" AutoPostBack="true" OnTextChanged="ddlsalesMainfilter_TextChanged" Style="margin-bottom: 5px;"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">

                                    <div class="row">
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtcnamefilter" runat="server" placeholder="Company name" Width="100%" OnTextChanged="txtcnamefilter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                TargetControlID="txtcnamefilter">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtonamefilter" runat="server" placeholder="Contact name" Width="100%" OnTextChanged="txtonamefilter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyOwnerList"
                                                TargetControlID="txtonamefilter">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-xl-3 col-md-3">
                                            <%--<asp:TextBox ID="txtmobilefilter" runat="server" placeholder="Mobile" Width="100%" OnTextChanged="txtmobilefilter_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                        </div>
                                        <div class="col-xl-2 col-md-2">
                                           <%-- <asp:DropDownList ID="ddltypefilter" runat="server" Width="100%" AutoPostBack="true" OnTextChanged="ddltypefilter_TextChanged">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Paid</asp:ListItem>
                                                <asp:ListItem>Unpaid</asp:ListItem>
                                            </asp:DropDownList>--%>
                                        </div>
                                        <div class="col-xl-1 col-md-1">
                                            <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" OnClick="btnresetfilter_Click" />
                                        </div>
                                    </div>

                                    <br />
                                    <div class="dt-responsive table-responsive">
                                        <asp:GridView ID="GvCompany" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                            DataKeyNames="id" OnRowCommand="GvCompany_RowCommand" AllowPaging="true" OnPageIndexChanging="GvCompany_PageIndexChanging" PageSize="25">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Code" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblccode" runat="server" Text='<%# Eval("ccode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkcname" runat="server" CssClass="linkbtn" CommandName="companyname" Text='<%# Eval("cname") %>' CommandArgument='<%# Eval("id") %>' ToolTip="View Details"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Owner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblownnamegv" runat="server" Text='<%# Eval("oname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("mobile") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Reg. Date" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvisitdate" runat="server" Text='<%# Eval("regdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="linkbtnedit" runat="server" CssClass="linkbtn" CommandName="RowEdit" CommandArgument='<%# Eval("id") %>' ToolTip="Edit">Edit</asp:LinkButton>&nbsp;--%>
                                                        <asp:Button ID="Button4" CssClass="btn" runat="server" Text="Edit" Style="background-color: #09989a !important; color: #fff;" CommandName="RowEdit" CommandArgument='<%# Eval("id") %>' />
                                                        <asp:Button ID="btnAddEnq" CssClass="btn btn-success" runat="server" Text="Enquiry" CommandArgument='<%# Eval("cname") %>' OnClick="btnAddEnq_Click"/>
                                                        <%--<asp:LinkButton ID="Linkbtndelete" runat="server" CssClass="linkbtn" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this record ?')" CommandArgument='<%# Eval("id") %>' ToolTip="Delete">Delete</asp:LinkButton>&nbsp;|&nbsp;
                                                    <asp:LinkButton ID="linkaccount" runat="server" CssClass="linkbtn" CommandName="status" OnClientClick="return confirm('Do you want to Activate/Deactivate this account ?')" CommandArgument='<%# Eval("id") %>' ToolTip="Activate/Deactivate" Text="Activated"></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <h2>
                                            <center><asp:Label ID="lblnodatafoundComp" runat="server" Text="" Visible="false" CssClass="lblboldred"></asp:Label></center>
                                        </h2>
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


    <%--    Company Details --%>
    <asp:Button ID="btnprof" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modelprofile" runat="server" TargetControlID="btnprof"
        PopupControlID="PopupViewDetail" OkControlID="Closepopdetail" />

    <asp:Panel ID="PopupViewDetail" runat="server" CssClass="modelprofile1">
        <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 1px; padding-left: 1px;">
            <div class="col-md-2"></div>
            <div class="col-md-10">
                <div class="profilemodel2">
                    <div class="headingcls">
                        <h4 class="modal-title">Company Detail
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                    </div>

                    <br />
                    <%--<div class="row" style="margin-right: 0px; margin-left: 10px;">--%>
                        <%--<div class="col-md-12" style="padding-right: 1px!important; background-color: rgb(249, 247, 247)!important;">--%>

                            <div class="row" style="margin-right: 0px!important;margin-left: 10px; padding: 3px;">
                                <div class="col-md-2"><b>Company Code :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lblccode" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row" style="background-color: rgb(238, 238, 238);margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                                <div class="col-md-2"><b>Company Name :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lblcname" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2"><b>Contact Name :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lbloname" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row" style="background-color: rgb(249, 247, 247);margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                                <div class="col-md-2"><b>Email :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2"><b>Mobile :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lblmobile" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <br />

                            <div class="row" style="background-color: rgb(238, 238, 238);margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                                <div class="col-md-2"><b>Billing Address :</b></div>
                                <div class="col-md-8 lblpopup">
                                    <asp:Label ID="lblbillingaddress" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <br />
                       <%-- </div>--%>
                    <%--</div>--%>

                    <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>Shipping Address :</b></div>
                        <div class="col-md-8 lblpopup">
                            <asp:Label ID="lblshipaddress" runat="server" Text=""></asp:Label>
                        </div>
                    </div><br />

                    <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>Reg. date :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblRegdate" runat="server" Text=""></asp:Label>
                        </div>
                    </div><br />

                    <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>Sales Manager :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblregBy" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-2"><b>GST No :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblgstno" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <br />

                    <%--<div class="row" style="margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" placeholder="Write Something..." CssClass="form-control" Width="99%" Height="70px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Write Something..."
                                ControlToValidate="txtcomment" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row" style="margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-8">
                            <asp:TextBox ID="txtaddemail" runat="server" placeholder="Add additional Mail Id's separated with comma (,)" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                    <br />--%>

                    <%--<div class="row" style="margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2">
                            <asp:Button ID="btnComment" ValidationGroup="form1" runat="server" CssClass="btn btn-success" Text="Save" Width="100%" OnClick="btnComment_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger" Text="Cancel" Width="100%" />
                        </div>
                        <div class="col-md-4"></div>
                        <div class="col-md-2">
                            <asp:Button ID="btnreminder" runat="server" CssClass="btn btn-primary" Text="TBRO" Width="100%" OnClick="btnreminder_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnhistory" runat="server" CssClass="btn btn-secondary" Text="History" Width="100%" OnClick="btnhistory_Click" />
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>

    </asp:Panel>
    <%--  Company Details --%>



    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

