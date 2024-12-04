<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="EnquiryList.aspx.cs" Inherits="Admin_EnquiryList" %>

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

    <div class="page-wrapper">
        <div class="page-body">

            <div class="row">
                <div class="col-md-7">
                    <%--<div class="page-header-breadcrumb">
                        <div style="float: left; font-size: 15px;">
                            <span><i class="feather icon-home"></i>&nbsp;Enquiry List</span>
                        </div>
                    </div>--%>
                </div>


                <div class="col-md-5">
                    <div class="page-header-breadcrumb">
                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                            <span><a href="AddCompany.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Company</a>&nbsp;&nbsp;
                                <a href="Addenquiry.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Enquiry</a>
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
                                <h5>Enquiry List</h5>
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
                                       
                                        <div class="col-xl-8 col-md-8">
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

                                                <asp:TemplateField HeaderText="Enquiry Code" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEnqCode" runat="server" Text='<%# Eval("EnqCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkcname" runat="server" CssClass="linkbtn" CommandName="companyname" Text='<%# Eval("cname") %>' CommandArgument='<%# Eval("id") %>' ToolTip="View Details"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reg. Date" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvisitdate" runat="server" Text='<%# Eval("regdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="File" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkbtnfile" runat="server" CssClass="linkbtn" OnClick="linkbtnfile_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File">Open File</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="Button4" CssClass="btn" runat="server" Text="Edit" Style="background-color: #09989a !important; color: #fff;" CommandName="RowEdit" CommandArgument='<%# Eval("id") %>' />--%>
                                                        <asp:Button ID="btnsendquot" CssClass="btn btn-success" runat="server" Text="Send Quot" CommandArgument='<%# Eval("ccode") %>' OnClick="btnsendquot_Click" />
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
                        <h4 class="modal-title">Enquiry Detail
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                    </div>

                    <br />
                    <div class="row" style="margin-right: 0px!important; margin-left: 10px; padding: 3px;">
                        <div class="col-md-2"><b>Enquiry Code :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblenqcode" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>Company Name :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblcname" runat="server" Text=""></asp:Label>
                        </div>
                        <%--<div class="col-md-2"><b>Contact Name :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lbloname" runat="server" Text=""></asp:Label>
                                </div>--%>
                    </div>
                    <br />
                    <div class="row" style="background-color: rgb(249, 247, 247); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>Remarks :</b></div>
                        <div class="col-md-8 lblpopup">
                            <asp:Label ID="lblremark" runat="server" Text=""></asp:Label>
                        </div>
                        <%--<div class="col-md-2"><b>Mobile :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lblmobile" runat="server" Text=""></asp:Label>
                                </div>--%>
                    </div>
                    <br />

                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>Status :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-2"><b>Reg. Date :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblRegdate" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />

                    <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>Reg. By :</b></div>
                        <div class="col-md-8 lblpopup">
                            <asp:Label ID="lblregBy" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />


                </div>
            </div>
        </div>

    </asp:Panel>
    <%--  Company Details --%>



    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

