<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="EnquiryList.aspx.cs" Inherits="Admin_EnquiryList" %>

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

        .aspNetDisabled {
            cursor: not-allowed !important;
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
    </style>

    <!-- Bootstrap -->
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>

    <script type="text/javascript">
        function ShowPopup(title, body) {
            $("#MyPopup .modal-title").html(title);
            $("#MyPopup .modal-body").html(body);
            $("#MyPopup").modal("show");
        }
    </script>
    <script language="javascript" type="text/javascript">

        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {

            $('#btnshowhide').hide();

            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                var wid = 100;

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = wid + "%";
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = wid + "%";
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = wid + "%";
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + '%';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                //if (isFooter) {
                //    var tblfr = tbl.cloneNode(true);
                //    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                //    var tblBody = document.createElement('tbody');
                //    tblfr.style.width = '100%';
                //    tblfr.cellSpacing = "0";
                //    tblfr.border = "0px";
                //    tblfr.rules = "none";
                //    //*****In the case of Footer Row *******
                //    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                //    tblfr.appendChild(tblBody);
                //    DivFR.appendChild(tblfr);
                //}
                //****Copy Header in divHeaderRow****


                DivHR.appendChild(tbl.cloneNode(true));

            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }

        function RemoveLocalStorage() {
            
            localStorage.removeItem("quotationData");
            window.location.href = "../Admin/EnquiryList.aspx";
        }

        function OnPageLoad() {
            debugger;
            var data = localStorage.getItem("quotationData");
            if (data != null) {
                var parsedData = JSON.parse(data);

                // Retrieve the CustomerCode and CustomerName from the stored data
                var customerCode = parsedData.CustomerId;
                var customerName = parsedData.CustomerName;
            } else {
                var customerCode = null;
                var customerName = null;
            }
            $.ajax({
                type: "POST",
                url: "AdminDashboard.aspx/StoreCustomerDataInSession", // Adjust URL
                data: JSON.stringify({ customerCode: customerCode, customerName: customerName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                  
                },
                error: function (xhr, status, error) {
                   
                }
            });

        }
    </script>


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
                            <span><a id="btnAddCompany" runat="server" href="AddCompany.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add customer</a>&nbsp;&nbsp;
                                <a id="btnAddEnq" runat="server" href="Addenquiry.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Enquiry</a>
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
                            <div class="col-md-2" style="display: none">
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
                                            <asp:Label ID="Label1" runat="server" Text="Customer Name :"></asp:Label>
                                            <asp:TextBox ID="txtcnamefilter" runat="server" placeholder="Customer name" Width="100%" OnTextChanged="txtcnamefilter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                TargetControlID="txtcnamefilter">
                                            </asp:AutoCompleteExtender>
                                        </div>

                                        <%--          <div class="col-xl-2 col-md-2">
                                           <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="88%" AutoPostBack="true" OnTextChanged="ddlStatus_TextChanged">
                                                <asp:ListItem>Pending Enquiry</asp:ListItem>
                                                <asp:ListItem>Completed</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xl-6 col-md-6">
                                        </div>
                                        <div class="col-xl-1 col-md-1">
                                            <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" OnClick="btnresetfilter_Click" />
                                        </div>--%>

                                        <div class="col-xl-2 col-md-2">
                                            <asp:Label ID="lblEnquiry" runat="server" Text="Enquiry"></asp:Label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="50%" AutoPostBack="true" OnTextChanged="ddlStatus_TextChanged">
                                                <asp:ListItem>Pending Enquiry</asp:ListItem>
                                                <asp:ListItem>Completed</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <%--From date to date filter start--%>

                                        <div class="col-md-3">

                                            <asp:Label ID="lblfromdate" runat="server" Text="From Date :"></asp:Label>
                                            <div>
                                                <asp:TextBox ID="txtfromdate" Placeholder="Enter From Date" runat="server" TextMode="Date" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lbltodate" runat="server" Text="To Date :"></asp:Label>
                                            <div>
                                                <asp:TextBox ID="txttodate" Placeholder="Enter To Date" runat="server" TextMode="Date" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-1 col-md-1" style="margin-top: 14px;">
                                            <asp:Button ID="btnsearch" OnClick="btnsearch_Click" CssClass="btn btn-primary" runat="server" Text="Search" Style="padding: 8px;" />
                                        </div>
                                        <%--From date to date filter end--%>
                                    </div>
                                    <div class="row">

                                        <b>Note :</b>

                                        The Visible button indicates the pending quotation records.
                                                To make other rows button visible first&nbsp;<b>Create Quot</b>&nbsp; or &nbsp; <b> Delete</b>&nbsp; the row.
                                    </div>

                                <br />
                                <div class="col-md-12">
                                    <div id="DivRoot" align="left">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="GvCompany" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                DataKeyNames="id" OnRowDataBound="GvCompany_RowDataBound" OnRowCommand="GvCompany_RowCommand" AllowPaging="false" OnPageIndexChanging="GvCompany_PageIndexChanging" PageSize="25">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="lblfilepath1" runat="server" Visible="false" Text='<%# Eval("filepath1") %>'></asp:Label>
                                                            <asp:Label ID="lblfilepath2" runat="server" Visible="false" Text='<%# Eval("filepath2") %>'></asp:Label>
                                                            <asp:Label ID="lblfilepath3" runat="server" Visible="false" Text='<%# Eval("filepath3") %>'></asp:Label>
                                                            <asp:Label ID="lblfilepath4" runat="server" Visible="false" Text='<%# Eval("filepath4") %>'></asp:Label>
                                                            <asp:Label ID="lblfilepath5" runat="server" Visible="false" Text='<%# Eval("filepath5") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus1" runat="server" Text='<%# Eval("status") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblstatus2" runat="server" Text='' Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Enquiry Code" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEnqCode" runat="server" Text='<%# Eval("EnqCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Code" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkCustomerCode" runat="server" CssClass="linkbtn" CommandName="companyname" Text='<%# Eval("CustomerCode").ToString().Replace(" ", "<br /><br />") %>' CommandArgument='<%# Eval("id") %>' ToolTip="View Details"></asp:LinkButton>
                                                            <%--   <asp:Label ID="lblcustomercode" runat="server" Text='<%# Eval("CustomerCode") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkcname" runat="server" CssClass="linkbtn" CommandName="companyname" Text='<%# Eval("cname").ToString().Replace(" ", "<br /><br />") %>' CommandArgument='<%# Eval("id") %>' ToolTip="View Details"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reg. Date" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvisitdate" runat="server" Text='<%# Eval("regdate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="File1" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%--<asp:ImageButton ID="ImageButton1" ImageUrl="../img/Open-file.ico" runat="server" Width="30px" OnClick="linkbtnfile_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File" />--%>
                                                            <asp:ImageButton ID="ImageButtonfile1" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="linkbtnfile_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File" />
                                                            <%--<asp:LinkButton ID="linkbtnfile1" runat="server" CssClass="linkbtn" OnClick="linkbtnfile_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File">Open</asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="File2" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonfile2" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="linkbtnfile2_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File" />
                                                            <%--<asp:LinkButton ID="linkbtnfile2" runat="server" CssClass="linkbtn" OnClick="linkbtnfile2_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File">Open</asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="File3" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonfile3" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="linkbtnfile3_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File" />
                                                            <%--<asp:LinkButton ID="linkbtnfile3" runat="server" CssClass="linkbtn" OnClick="linkbtnfile3_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File">Open</asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="File4" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonfile4" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="linkbtnfile4_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File" />
                                                            <%--<asp:LinkButton ID="linkbtnfile4" runat="server" CssClass="linkbtn" OnClick="linkbtnfile4_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File">Open</asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="File5" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonfile5" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="linkbtnfile5_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File" />
                                                            <%--<asp:LinkButton ID="linkbtnfile5" runat="server" CssClass="linkbtn" OnClick="linkbtnfile5_Click" CommandArgument='<%# Eval("id") %>' ToolTip="Open File">Open</asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Button4" CssClass="btn" runat="server" Text="Edit" Style="background-color: #09989a !important; color: #fff;" CommandName="RowEdit" CommandArgument='<%# Eval("id") %>' />
                                                            <%--<asp:Button ID="btnsendquot" CssClass="btn btn-success" runat="server" Text="Create Quot" OnClientClick="SetTarget();" CommandArgument='<%# Eval("ccode") %>' OnClick="btnsendquot_Click" />--%>
                                                            <asp:Button ID="btnsendquot" CssClass="btn btn-success" runat="server" Text="Create Quot" CommandName="CreateQuaot" CommandArgument='<%# Eval("ccode") %>' />
                                                            <asp:LinkButton ID="Linkbtndelete" runat="server" CssClass="btn btn-warning" CommandName="DeleteData" OnClientClick="return confirm('Do you want to delete this record ?')" CommandArgument='<%# Eval("id") %>' ToolTip="Delete">Delete</asp:LinkButton>&nbsp;
                                                    <%--<asp:LinkButton ID="linkaccount" runat="server" CssClass="linkbtn" CommandName="status" OnClientClick="return confirm('Do you want to Activate/Deactivate this account ?')" CommandArgument='<%# Eval("id") %>' ToolTip="Activate/Deactivate" Text="Activated"></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <script type="text/javascript">
                                    function SetTarget() {
                                        document.forms[0].target = "_blank";
                                    }
                                </script>
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
            <div class="col-md-3"></div>
            <div class="col-md-8">
                <div class="profilemodel2">
                    <div class="headingcls">
                        <h4 class="modal-title">Enquiry Detail
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                    </div>
                    <br />

                    <%-- <br />
                    <div class="row" style="margin-right: 0px!important; margin-left: 10px; padding: 3px;">
                        <div class="col-md-2"><b>Enquiry Code :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblenqcode" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />--%>
                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-3"><b>Customer Name :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblcname" runat="server" Text=""></asp:Label>
                        </div>

                    </div>

                    <div class="row" style="background-color: rgb(249, 247, 247); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-3"><b>Remarks :</b></div>
                        <div class="col-md-8 lblpopup">
                            <asp:Label ID="lblremark" runat="server" Text=""></asp:Label>
                        </div>

                    </div>

                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-3"><b>Status :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-3"><b>Reg. By :</b></div>
                        <div class="col-md-8 lblpopup">
                            <asp:Label ID="lblregBy" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-3"><b>Reg. Date :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblRegdate" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <br />


                </div>
            </div>
            <div class="col-md-1"></div>
        </div>

    </asp:Panel>
    <%--  Company Details --%>

    <asp:Button ID="btnCreateQuat" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modalCreateQuat" runat="server" TargetControlID="btnCreateQuat"
        PopupControlID="PnlCreateQuot" OkControlID="CloseCreatedetail" />

    <asp:Panel ID="PnlCreateQuot" runat="server" CssClass="modelprofile1">
        <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 1px; padding-left: 1px; margin-top: 186px;">
            <div class="col-md-3"></div>
            <div class="col-md-8">
                <div class="profilemodel2">
                    <div class="headingcls">
                        <h4 class="modal-title">Create Quotation
                            <button type="button" id="CloseCreatedetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">CANCEL</button></h4>
                    </div>

                    <div class="col-md-12" style="margin-left: 16%">
                        <asp:LinkButton ID="btnHide" runat="server"></asp:LinkButton>
                        <asp:Label runat="server" ID="HdnID" Visible="false"></asp:Label>
                        <br />
                        <%-- <asp:Button ID="btnHide" runat="server" CssClass="" Text="" />--%>
                        <br />
                        <asp:Button ID="BtnEnclosure" runat="server" CssClass="btn btn-outline-primary btn-lg" Text="MS fabricated Enclosure" OnClick="BtnEnclosure_Click" Visible="false" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnPart" runat="server" CssClass="btn btn-outline-success btn-lg" Text="Loose Parts for the Enclosure" OnClick="btnPart_Click" /><br />
                        <br />
                    </div>



                </div>
            </div>
            <div class="col-md-1"></div>
        </div>

    </asp:Panel>




    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
