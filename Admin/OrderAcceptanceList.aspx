<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="OrderAcceptanceList.aspx.cs" Inherits="Admin_OrderAcceptanceList" %>

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
                max-width: 100% !important;
            }
        }

        .aspNetDisabled {
            cursor: not-allowed !important;
        }

        .nowrap {
            margin-left: 31px;
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

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //***In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //**Copy Header in divHeaderRow****


                DivHR.appendChild(tbl.cloneNode(true));

            }
        }
        //function OnScrollDiv(Scrollablediv) {
        //    document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
        //    document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>

    <script type="text/javascript" src='https://aja-x.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script language="javascript">
        $(document).ready(function () {
            debugger;
            var gridHeader = $('#<%=GvOA.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
            $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
            $('#<%=GvOA.ClientID%> tr th').each(function (i) {
                // Here Set Width of each th from gridview to new table(clone table) th 
                $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
            });
            $("#GHead").append(gridHeader);
            $('#GHead').css('position', 'absolute');
            $('#GHead').css('top', '30px');
            $('#GHead').css('width', '983px');

        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                    <%-- <span><a id="btnAddOpenOrder" runat="server" href="AddOpenOrder.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Open Order</a>&nbsp;&nbsp; --%>
                                    <a id="btnQuoList" runat="server" href="QuotationList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Quotation List</a>
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
                                        <h5>Order Acceptance List</h5>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <%--<div class="col-md-2">
                                <asp:DropDownList ID="ddlsalesMainfilter" runat="server" AutoPostBack="true" OnTextChanged="ddlsalesMainfilter_TextChanged" Style="margin-bottom: 5px;"></asp:DropDownList>
                            </div>--%>
                                </div>
                            </div>
                            <br />
                            <div class="row" style="margin-left: 35px;">
                                <div class="col-xl-3 col-md-3">
                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" placeholder="Customer Name" Width="100%" OnTextChanged="txtCustomerName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                        TargetControlID="txtCustomerName">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <div class="col-xl-2 col-md-2">
                                    <asp:DropDownList ID="ddlDispatchList" CssClass="form-control" runat="server" AutoPostBack="true" Width="100%" Height="100%" OnTextChanged="ddlDispatchList_TextChanged">
                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                        <asp:ListItem Value="Dispatch">Dispatch</asp:ListItem>
                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xl-1 col-md-1">
                                    <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" OnClick="btnresetfilter_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xl-12 col-md-12">
                                    <div class="card">
                                        <div class="card-header">

                                            <%-- <div class="row">
                                        <div class="col-xl-3 col-md-3">
                                            <asp:TextBox ID="txtquotationno" runat="server" placeholder="Quotation No" Width="100%" OnTextChanged="txtquotationno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetQuotationList"
                                                TargetControlID="txtquotationno">
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

                                        <div class="col-xl-1 col-md-1">
                                            <asp:Button ID="btnresetfilter" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" OnClick="btnresetfilter_Click" />
                                        </div>
                                    </div>--%>

                                            <br />
                                            <div class="col-md-12">
                                                <div id="DivRoot" align="left">
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                        <asp:GridView ID="GvOA" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                            DataKeyNames="id" OnRowCommand="GvOA_RowCommand" OnRowDataBound="GvOA_RowDataBound" AllowPaging="false" OnPageIndexChanging="GvOA_PageIndexChanging" PageSize="50" AutoPostBack="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%-- New Field added for Job No by Nikhil 06-1-2024 --%>
                                                                <asp:TemplateField HeaderText="Job No" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JobNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- End  --%>

                                                                <asp:TemplateField HeaderText="OA No" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbloano" runat="server" Text='<%# Eval("oano") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Quotation No" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblquotationno" runat="server" Text='<%# Eval("quotationno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%-- <asp:TemplateField HeaderText="Kindatt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblkindatt" runat="server" Text='<%# Eval("kindatt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Customer Code" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkCustomerCode" runat="server" CssClass="linkbtn" CommandName="CompanyData" Text='<%# Eval("CustomerCode").ToString().Replace(" ", "<br /><br />") %>' CommandArgument='<%# Eval("id") %>' ToolTip="View Details"></asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Party Name" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%--    <asp:LinkButton ID="linkcname" runat="server" CssClass="linkbtn" CommandName="partyname" Text='<%# Eval("partyname").ToString().Replace(" ", "<br /><br />") %>' CommandArgument='<%# Eval("oano") %>' ToolTip="View Details"></asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="linkcname" runat="server" CssClass="linkbtn" CommandName="partyname" Text='<%# Eval("partyname").ToString().Replace(" ", "<br /><br />") %>' CommandArgument='<%# Eval("oano") %>' ToolTip="View Details"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%-- <asp:TemplateField HeaderText="Material" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmaterial" runat="server" Text='<%# Eval("material") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="ConstructionType" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConstructiontype" runat="server" Text='<%# Eval("Constructiontype") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Created Date" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delivery Date" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("deliverydatereqbycust","{0:MM-dd-yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Email Status" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblemailstatus" runat="server" Text='<%# Eval("emailstatus") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="left">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnCreate" CssClass="btn btn-info" runat="server" Text="Create OA" ToolTip="Create OA" CommandName="RowCreateOA" CommandArgument='<%# Eval("id") %>' Enabled='<%# Eval("Status").ToString() == "new" ? true : false %>'><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="btnEdit" CssClass="btn btn-facebook" runat="server" Text="Edit OA" ToolTip="Edit OA" CommandName="RowEditOA" CommandArgument='<%# Eval("oano") %>' Enabled='<%# Eval("Status").ToString() == "edit" ? true : false %>'><i class="fa fa-edit" aria-hidden="true"></i></asp:LinkButton>
                                                                        <a href="../Admin/OARptPDF.aspx?ID=<%#Eval("oano")%>" target="_blank">
                                                                            <asp:Label ID="Label1" CssClass="btn btn-info" Style="padding: 5px 3px !important; margin-top: 0px; color: white;" Height="35px" runat="server" Text="Print" Font-Size="15px"><i class="fa fa-print" style="font-size:24px"></i></asp:Label></span></a>
                                                                        <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete OA" ToolTip="Delete OA" CommandName="RowDeleteOA" OnClientClick="return confirm('Do you want to delete this OA ?')" CommandArgument='<%# Eval("ID") %>'><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>

                                                                        <%--<asp:LinkButton ID="lnkmailsend" CssClass="btn btn-primary " runat="server" Text="Send Mail" ToolTip="Send Mail" CommandName="Sendamil" OnClientClick="return confirm('Do you want to Send Maill ?')" CommandArgument='<%# Eval("oano") %>'  Visible="false"><i class="fa fa-envelope" aria-hidden="true"></i></asp:LinkButton>--%>


                                                                        <%--<asp:LinkButton ID="btnCloseOa" CssClass="btn btn-danger" runat="server" Text="Close OA" ToolTip="Close OA" CommandName="RowCloseOA" OnClientClick="return confirm('Do you want to Close this OA ?')" CommandArgument='<%# Eval("oano") %>'><i class="fa fa-close" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                        <%--<asp:Button ID="btnDelete" CssClass="btn btn-success" runat="server" Text="Delete" CommandName="RowDelete" OnClientClick="return confirm('Do you want to delete this Quotation ?')" CommandArgument='<%# Eval("id") %>' />
                                                        <a href="../Salesquotationreport.aspx?abc=<%#Eval("id")%>" target="_blank">
                                                            <asp:Label ID="Label1" CssClass="btn btn-info" Style="padding: 5px 3px !important; margin-top: 0px; color: white;" Height="29px" runat="server" Text="Print" Font-Size="15px"></asp:Label></span></a>
                                                        <asp:Button ID="btnOA" runat="server" CssClass="btn btn-primary" CommandName="RowOA" CommandArgument='<%# Eval("id") %>' Text="Send For OA" ToolTip="Send For OA" />--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <div id="DivFooterRow" style="overflow: hidden">
                                                        </div>
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
                <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 1px; padding-left: 1px;">
                    <div class="col-md-2"></div>
                    <div class="col-md-10">
                        <div class="profilemodel2">
                            <div class="headingcls">
                                <h4 class="modal-title">OA Detail
                          
                            <button type="button" id="Closepopdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button>
                                </h4>
                            </div>

                            <div class="row" style="background-color: rgb(238, 238, 238); margin-left: 10px; margin-right: 0px!important; padding: 3px; margin-top: 5px;">
                                <div class="col-md-2"><b>Customer Name :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                                </div>

                                <%--<div class="col-md-2"><b>Total Qty :</b></div>--%>
                                <div class="col-md-4 lblpopup">
                                    <%--<asp:Label ID="lblQty" runat="server" Text=""></asp:Label>--%>
                                </div>

                            </div>
                            <br />
                            <div class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                                <div class="col-md-2"><b>OA Number :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:GridView ID="dgvOANumber" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="OAno" HeaderText="OA Number" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-2"><b>SUBOA Number :</b></div>
                                <div class="col-md-4 lblpopup">
                                    <asp:DropDownList runat="server" Width="80%" ID="ddlSubOaNo" AppendDataBoundItems="true" CssClass="form-control" OnTextChanged="ddlSubOaNo_TextChanged" AutoPostBack="true" CausesValidation="false">
                                        <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:GridView ID="dgvSubOA" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="SubOANumber" HeaderText="SubOA Number" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <br />
                            <div id="divSuboaGrid" runat="server" visible="false" class="row" style="margin-right: 0px; margin-left: 10px; background-color: rgb(249, 247, 247); margin-right: 0px!important; padding: 3px;">
                                <div class="col-md-2"><b>SUBOA Details :</b></div>
                                <div class="col-md-10 lblpopup">
                                    <asp:GridView ID="dgvSubOADetails" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="SubOANumber" HeaderText="SubOA Number" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="Description" HeaderText="Description" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="Qty" HeaderText="Qty" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <hr />
                            <div id="divSubOaDetails" runat="server" visible="false">
                                <div class="row" style="margin-right: 0px; margin-left: 10px; margin-right: 0px!important; padding: 3px;">
                                    <div class="col-md-4"><b><u>OA Status :</u></b></div>
                                    <div class="col-md-8 lblpopup">
                                    </div>
                                </div>
                                <div class="row" style="padding: 20px; width: 100%;">
                                    <div class="col-md-4 col-sm-4 col-xs-4">

                                        <asp:GridView ID="dgvDeprt" runat="server" CssClass="table table-striped table-bordered nowrap"
                                            AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="68" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <%# Container.DataItem %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-8 col-sm-8 col-xs-8" runat="server" visible="false" id="divdgvCustomerWise">
                                        <asp:GridView ID="dgvCustomerWise" runat="server" CssClass="table table-striped table-bordered clsMargin"
                                            AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Inward Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtInwardQty" runat="server" Text='<%# Eval("total_InQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Outward Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOutwardQty" runat="server" Text='<%# Eval("total_OutQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inward Datetime" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOutwardDt" runat="server" Text='<%# Eval("InwardDttime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="150px" DataField="IsComplete" HeaderText="Status" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <%--</div>--%>
                            <br />
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <%--    Company Details --%>
            <asp:Button ID="btnprof1" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="profilemodel2" runat="server" TargetControlID="btnprof1"
                PopupControlID="Panel1" OkControlID="Closepopupdetail" />

            <asp:Panel ID="Panel1" runat="server" CssClass="modelprofile1">
                <div class="row container" style="margin-right: 0px; margin-left: 0px; padding-right: 2px; padding-left: 4px;">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <div class="profilemodel2">
                            <div class="headingcls">
                                <h4 class="modal-title">Customer Detail
                            <button type="button" id="Closepopupdetail" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
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
                                <div class="col-md-4 lblpopup">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                </div>

                            </div>

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

                        </div>
                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <%--            <asp:PostBackTrigger ControlID="ddlSubOaNo" />--%>
            <asp:AsyncPostBackTrigger ControlID="ddlSubOaNo" />
            <asp:PostBackTrigger ControlID="GvOA" />
            <%--           <asp:AsyncPostBackTrigger ControlID="lnkmailsend" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
       //<%-- function DisableButtons() {
            var inputs = document.getElementsByTagName("INPUT");
            for (var i in inputs) {
                if (inputs[i].type == "button" || inputs[i].type == "submit") {
                    var a = inputs[i];
                    if (true) {

                    }
                <%--    inputs[i].disabled = true;
                    inputs[i].classList.add("dissablebtn");
                    document.getElementById("<%=btnComment.ClientID %>").value = 'Please wait...';
                    document.getElementById("<%=btnsaveRemainder.ClientID %>").value = 'Please wait...';
                }
            }
        }--%>
        // window.onbeforeunload = DisableButtons;
    </script>
</asp:Content>

