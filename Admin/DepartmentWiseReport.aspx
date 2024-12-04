<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="DepartmentWiseReport.aspx.cs" Inherits="Admin_DepartmentWiseReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        .divblock {
            text-align: end;
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

        .selected_row {
            background-color: #A1DCF2;
        }

        .divStatus1 {
            width: 1px;
            height: 1px;
            padding: 10px;
            border: 1px solid gray;
            margin: 0;
        }

        .clsMargin {
            margin-left: -4%;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(document).ready(function () {
                $('.myDate').datepicker({
                    dateFormat: 'dd-mm-yy',
                    inline: true,
                    showOtherMonths: true,
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true,
                    firstDay: 1,
                    navigationAsDateFormat: true,
                    showAnim: "fold",
                    yearRange: "c-100:c+10",
                    dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
                });
                $('.myDate').datepicker('setDate', new Date());
                $('.ui-datepicker').hide();
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="container py-3">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>OA Number / Customer Wise Report</h5>
                            </div>
                            <br />
                            <div class="row" style="padding: 10px;">
                                <div class="col-md-10">
                                    <div class="row">
                                        <div class="col-md-2 spancls">Customer Name:</div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtcname" CssClass="form-control" runat="server" Width="100%" OnTextChanged="txtcname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                TargetControlID="txtcname">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-md-2 spancls" runat="server" id="divlabl" visible="false">SUB OA Number:</div>
                                        <div class="col-md-3" runat="server" id="divtxt" visible="false">
                                            <asp:DropDownList ID="ddlONumber" CssClass="form-control" runat="server" Width="100%" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <asp:Button runat="server" ID="btnGetReport" Text="Get Report" OnClick="btnGetReport_Click" CssClass="btn btn-primary" />
                                </div>
                                <div class="col-md-1">
                                    <asp:Button runat="server" ID="Button2" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-primary" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="padding: 30px;">
                                    <div>
                                        <div runat="server" id="divDetails" visible="false" style="padding: 30px;">
                                            <div class="row">
                                                <div class="col-md-2 spancls">Customer Name:</div>
                                                <div class="col-md-5">
                                                    <asp:Label runat="server" Font-Bold="true" ID="lblCustName"></asp:Label>
                                                </div>
                                                <div class="col-md-2 spancls">OA Number:</div>
                                                <div class="col-md-3">
                                                    <asp:Label runat="server" Font-Bold="true" ID="lbloaNumber"></asp:Label>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Description:</div>
                                                <div class="col-md-5">
                                                    <asp:Label runat="server" Font-Bold="true" ID="lbldesciption"></asp:Label>
                                                </div>
                                                <div class="col-md-2 spancls">Total Quantity:</div>
                                                <div class="col-md-3">
                                                    <asp:Label runat="server" Font-Bold="true" ID="lblqnty"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">

                                            <div class="col-md-2">
                                                <asp:LinkButton ID="btnGenerateReport" OnClick="btnGenerateReport_Click" runat="server" CssClass="btn btn-small btn-primary" Visible="false"><i class="fa fa-print"></i>&nbsp;Generate Report</asp:LinkButton>
                                            </div>

                                            <div class="row" runat="server" id="divIndicate" visible="false">
                                                <%-- <div class="divStatus1" style="background-color: #008000!important; margin-left: 800px;"></div>
                                                &nbsp;COMPLETED &nbsp;
                                                <div class="divStatus1" style="background-color: #ffff00!important;margin-left: 800px;"></div>
                                                &nbsp;PENDING--%>
<%--                       </div>
                                        </div>
                                        <br />
                                        <%--Drawing--%>
<%--     <div class="card" runat="server" id="divDrawing" visible="false">
                                            <div class="card-header bg-primary text-uppercase text-white">
                                                <h5>Process Data</h5>
                                            </div>
                                            <br />
                                            <div class="card-body" style="padding: 20px; overflow-x: scroll; width: 100%;">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                                        <asp:GridView ID="dgvDeprt" runat="server" CssClass="table table-striped table-bordered nowrap"
                                                            AutoGenerateColumns="false" OnRowDataBound="dgvDrawingRpt_RowDataBound">
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

                                                    <div class="col-md-8 col-sm-8 col-xs-8" runat="server" id="divdgvDrawingRpt">
                                                        <asp:GridView ID="dgvDrawingRpt" runat="server" CssClass="table table-striped table-bordered clsMargin"
                                                            AutoGenerateColumns="false" OnRowDataBound="dgvDrawingRpt_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="68" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="OA Number" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOANumber" runat="server" Text='<%# Eval("OANumber") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="Department Name" ItemStyle-Width="600" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDept" runat="server" Text=''></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

<%-- <asp:BoundField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center"  HeaderText="Department Name" />--%> <%-- DataField="Department"--%>
<%-- <asp:TemplateField HeaderText="Description" ItemStyle-Width="600" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtTotQty" runat="server" Text='<%# Eval("TotalQty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Inward Date and Time" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInwardDtTime" runat="server" Text='<%# Eval("InwardDtTime") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Inward Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtInwardQty" runat="server" Text='<%# Eval("InwardQty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Outward Date and Time" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOutwardDtTime" runat="server" Text='<%# Eval("OutwardDtTime") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Outward Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtOutwardQty" runat="server" Text='<%# Eval("OutwardQty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField ItemStyle-Width="150px" DataField="IsComplete" HeaderText="Status" Visible="false" />

                                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <a href="<%#string.Format("../Reports/ReportPDF.aspx?Dept="+encrypt(Eval("Department").ToString())+"&OANumberWise={0}", encrypt(Eval("OANumber").ToString())) %>" target="_blank">
                                                                            <asp:Image ID="ImgPrint" runat="server" CssClass="img1" ImageUrl="../img/Print.png" Height="29px" ToolTip="Print" /></a>
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
                                                                <asp:BoundField ItemStyle-Width="150px" DataField="IsComplete" HeaderText="Status" Visible="false" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
<%--                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
<%--</asp:Content>--%>


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

        .sortable-handler {
            touch-action: none;
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


    <script src="../JS/jquery.min.js"></script>
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

    <div class="page-wrapper">
        <div class="page-body">
            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <div class="row">
                            <div class="col-md-4">
                                <h5>Pending Customer Reports</h5>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">

                                    <div class="row my-md-4 gap-">
                                        <div class=" spancls mt-2 " style="margin-left: 15px;">Customer Name:</div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtcname" CssClass="form-control" runat="server" Width="100%" AutoPostBack="true" OnTextChanged="txtcname_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                TargetControlID="txtcname">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="BtnExcell" Text="Export Excel" OnClick="BtnExcell_Click" CssClass="btn btn-primary" />
                                        </div>

                                    </div>
                                    <div class="col-md-12" style="padding: 0px; margin-top: 10px;">
                                        <div id="DivRoot" align="left">
                                            <div style="overflow: hidden;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="GvReports" runat="server" CssClass="table" HeaderStyle-BackColor="#009999" AutoGenerateColumns="false" Style="max-height: 300px; overflow-y: auto;">
                                                    <Columns>
														
														<asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="68" ItemStyle-HorizontalAlign="Center" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                        <asp:BoundField DataField="customername" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="OANumber" HeaderText="OA Number" />
                                                        <asp:BoundField DataField="pono" HeaderText="PO Number" />
                                                        <asp:BoundField DataField="Size" HeaderText="Size" />
                                                        <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" />
                                                        <asp:BoundField DataField="Department" HeaderText="Stages" />
                                                        <%--<asp:BoundField DataField="InwardQty" HeaderText="Inward Quantity" />
                                                        <asp:BoundField DataField="OutwardQty" HeaderText="Outward Quantity" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
