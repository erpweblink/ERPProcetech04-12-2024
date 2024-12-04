<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="QuotationList.aspx.cs" Inherits="Admin_QuotationList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>

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


        .aspNetDisabled {
            cursor: not-allowed !important;
        }

        @media (min-width: 1200px) {
            .container {
                max-width: 100% !important;
            }
        }
    </style>

    <style type="text/css">
        .divgrid {
            height: 200px;
            width: 370px;
        }

            .divgrid table {
                width: 350px;
            }

                .divgrid table th {
                    background-color: Green;
                    color: #fff;
                }
    </style>
    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
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
    </script>

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
                            <span><a id="btnAddCompany" runat="server" href="AddCompany.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Customer</a>&nbsp;&nbsp;
                                <a id="btnAddOA" runat="server" href="OrderAcceptanceList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;OA List</a>&nbsp;&nbsp;
								<a id="btnQuotRpt" runat="server" href="QuotationF_Report.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;" visible="False">&nbsp;Quotation Report</a>
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
                                <h5>Quotation List</h5>
                            </div>
                            <div class="col-md-6">
                            </div>
                            <%--<div class="col-md-2">
                                <asp:DropDownList ID="ddlsalesMainfilter" runat="server" AutoPostBack="true" OnTextChanged="ddlsalesMainfilter_TextChanged" Style="margin-bottom: 5px;"></asp:DropDownList>
                            </div>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">

                                    <div class="row">
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtCustomerName" runat="server" placeholder="Customer Name" Width="100%" OnTextChanged="txtCustomerName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                TargetControlID="txtCustomerName">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtconstructiontype" runat="server" placeholder="Construction Type" Width="100%" OnTextChanged="txtconstructiontype_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetConstructiontypeList"
                                                TargetControlID="txtconstructiontype">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtquotationno" runat="server" placeholder="Quotation No" Width="100%" OnTextChanged="txtquotationno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetQuotationList"
                                                TargetControlID="txtquotationno">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-xl-2 col-md-2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="88%" AutoPostBack="true" OnTextChanged="ddlStatus_TextChanged">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Completed</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <%-- <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtmobilefilter" runat="server" placeholder="Mobile" Width="100%" OnTextChanged="txtmobilefilter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>--%>
                                        <%--<div class="col-xl-2 col-md-2">
                                            <%-- <asp:DropDownList ID="ddltypefilter" runat="server" Width="100%" AutoPostBack="true" OnTextChanged="ddltypefilter_TextChanged">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Paid</asp:ListItem>
                                                <asp:ListItem>Unpaid</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                                        <div class="col-xl-1 col-md-1">
                                            <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" OnClick="btnresetfilter_Click" />
                                        </div>
                                    </div>

                                    <div id="DivRoot" align="left">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="GvQuotation" runat="server" GridLines="Both" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                DataKeyNames="id" OnRowCommand="GvQuotation_RowCommand" AllowPaging="false" OnPageIndexChanging="GvQuotation_PageIndexChanging" PageSize="50" OnPreRender="GvQuotation_PreRender" OnRowDataBound="GvQuotation_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="lblRevise" runat="server" Text='<%# Eval("IsRevise") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblfile" runat="server" Text='<%# Eval("filename1") %>' Visible="false"></asp:Label>
                                                            <%-- <asp:Label ID="lblIscompletd" runat="server" Text='<%# Eval("IsComplete") %>' Visible="false"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quotation No" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblquotationno" runat="server" Text='<%# Eval("quotationno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kindatt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkindatt" runat="server" Text='<%# Eval("kindatt").ToString().Replace(" ", "<br /><br />") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Code" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkCustomerCode" runat="server" CssClass="linkbtn" CommandName="CompanyData" Text='<%# Eval("CustomerCode").ToString().Replace(" ", "<br /><br />") %>' CommandArgument='<%# Eval("id") %>' ToolTip="View Details"></asp:LinkButton>
                                                            <%--   <asp:Label ID="lblcustomercode" runat="server" Text='<%# Eval("CustomerCode") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="40">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpartyname" runat="server" Text='<%# Eval("partyname").ToString().Replace(" ", "<br /><br />") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Material" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmaterial" runat="server" Text='<%# Eval("material") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Construction Type" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblConstructiontype" runat="server" Text='<%# Eval("Constructiontype").ToString().Replace(" ", "<br /><br />") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Created Date" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created By" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcreatedby" runat="server" Text='<%# Eval("sessionname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton ID="linkbtnedit" runat="server" CssClass="linkbtn" CommandName="RowEdit" CommandArgument='<%# Eval("id") %>' ToolTip="Edit">Edit</asp:LinkButton>&nbsp;--%>
                                                            <asp:LinkButton ID="btnEdit" CssClass="btn" runat="server" Text="Edit" Style="background-color: #09989a !important; color: #fff;" CommandName="RowEdit" CommandArgument='<%# Eval("id") %>' ToolTip="Edit"><i class="fa fa-edit" style="font-size:24px"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnDelete" CssClass="btn btn-success" runat="server" Text="Delete" CommandName="RowDelete" OnClientClick="return confirm('Do you want to delete this Quotation ?')" CommandArgument='<%# Eval("id") %>' ToolTip="Delete"><i class="fa fa-trash-o" style="font-size:24px"></i></asp:LinkButton>
                                                            <a href="../Admin/SalesQuotationRptPDF.aspx?ID=<%#Eval("quotationno")%>" target="_blank">
                                                                <asp:Label ID="Label1" CssClass="btn btn-info" Style="padding: 5px 3px !important; margin-top: 0px; color: white;" Height="35px" runat="server" Text="Print" Font-Size="15px"><i class="fa fa-print" style="font-size:24px"></i></asp:Label></span></a>

                                                            <asp:LinkButton ID="btnDownloadAttachment" CssClass="btn" runat="server" Text="Download Attachment" Style="background-color: #4caf50 !important; color: #fff;" CommandName="RowDownloadAttachment" CommandArgument='<%# Eval("id") %>' ToolTip="Download Attachment"><i class="fa fa-download" style="font-size:24px"></i></asp:LinkButton>

                                                            <asp:Button ID="btnOA" runat="server" CssClass="btn btn-primary" CommandName="RowOA" CommandArgument='<%# Eval("id") %>' Text="Send For OA" ToolTip="Send For OA" />
                                                            <%--<asp:LinkButton ID="linkaccount" runat="server" CssClass="linkbtn" CommandName="status" OnClientClick="return confirm('Do you want to Activate/Deactivate this account ?')" CommandArgument='<%# Eval("id") %>' ToolTip="Activate/Deactivate" Text="Activated"></asp:LinkButton>--%>
                                                            <br />
                                                            <asp:Label ID="lblRevised" runat="server" Text="Revised" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <h2>
                                                <center>
                                                    <asp:Label ID="lblnodatafoundComp" runat="server" Text="" Visible="false" CssClass="lblboldred"></asp:Label></center>
                                            </h2>
                                            <div id="DivFooterRow" style="overflow: hidden">
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

    </div>

    <%--    Company Details --%>
    <asp:Button ID="btnprof" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modelprofile" runat="server" TargetControlID="btnprof"
        PopupControlID="PopupViewDetail" OkControlID="Closepopdetail" />

    <asp:Panel ID="PopupViewDetail" runat="server" CssClass="modelprofile1">
        <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 2px; padding-left: 4px;">
            <div class="col-md-3"></div>
            <div class="col-md-6">
                <div class="profilemodel2">
                    <div class="headingcls">
                        <h4 class="modal-title">Customer Detail
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                    </div>

                    <%--<div class="row" style="margin-right: 0px; margin-left: 10px;">--%>
                    <%--<div class="col-md-12" style="padding-right: 1px!important; background-color: rgb(249, 247, 247)!important;">--%>

                    <div class="row" style="margin-right: 0px!important; margin-left: 10px; padding: 3px;">
                        <div class="col-md-2"><b>Customer Code :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblcusomercode" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px; margin-top: 5px;">
                        <div class="col-md-2"><b>Customer Name :</b></div>
                        <div class="col-md-8 lblpopup">
                            <asp:Label ID="lblcname" runat="server" Text=""></asp:Label>
                        </div>

                    </div>

                    <%--       <div class="row" style="background-color: rgb(249, 247, 247); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-2"><b>Email :</b></div>
                            <div class="col-md-4 lblpopup">
                                <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
                            </div>
                      
                        </div>

                        <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-2"><b>Billing Address :</b></div>
                            <div class="col-md-8 lblpopup">
                                <asp:Label ID="lblbillingaddress" runat="server" Text=""></asp:Label>
                            </div>
                        </div>


                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-2"><b>Shipping Address :</b></div>
                            <div class="col-md-8 lblpopup">
                                <asp:Label ID="lblshipaddress" runat="server" Text=""></asp:Label>
                            </div>
                        </div>--%>

                    <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>Reg. date :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblRegdate" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                        <div class="col-md-2"><b>GST No :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblgstno" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-2"><b>Reg. By :</b></div>
                        <div class="col-md-4 lblpopup">
                            <asp:Label ID="lblregBy" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <%--  <hr />--%>
                    <%--     <div class="row" style="margin-right: 0px; margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-4"><b><u>Contact Details :</u></b></div>
                            <div class="col-md-8 lblpopup">
                            </div>
                        </div>
                        <br />
                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name1:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup1" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup1" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup1" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name2:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup2" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup2" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup2" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name3:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup3" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup3" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup3" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name4:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup4" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup4" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup4" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(238, 238, 238); margin-right: 0px!important; padding: 3px;">
                            <div class="col-md-1"><b>Name5:</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblnamePopup5" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-1"><b>Mobile:</b></div>
                            <div class="col-md-2 lblpopup">
                                <asp:Label ID="lblmobilePopup5" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2"><b>Designation :</b></div>
                            <div class="col-md-3 lblpopup">
                                <asp:Label ID="lblDesgPopup5" runat="server" Text=""></asp:Label>
                            </div>
                        </div>--%>
                    <%-- <br />--%>
                </div>
            </div>
        </div>

    </asp:Panel>
    <%--  Company Details --%>



    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
