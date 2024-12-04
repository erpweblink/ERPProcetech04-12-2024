<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="DepartmentWiseRpt.aspx.cs" Inherits="Admin_DepartmentWiseRpt" %>

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

    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="container-fluid">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Department Wise Report</h5>
                            </div>
                            <br />
                            <div class="row" style="padding: 10px;">
                                <div class="col-md-10">
                                    <div class="row">
                                        <div class="col-md-3 spancls">Department Name:</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server" Width="100%" AutoPostBack="true">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Drawing">Drawing</asp:ListItem>
                                                <asp:ListItem Value="Laser Programing">Laser Programing</asp:ListItem>
                                                <asp:ListItem Value="Laser Cutting">Laser Cutting</asp:ListItem>
                                                <asp:ListItem Value="CNC Bending">CNC Bending</asp:ListItem>
                                                <asp:ListItem Value="Welding">Welding</asp:ListItem>
                                                <asp:ListItem Value="Powder Coating">Powder Coating</asp:ListItem>
                                                <asp:ListItem Value="Final Assembly">Final Assembly</asp:ListItem>
                                                <asp:ListItem Value="Final Inspection">Final Inspection</asp:ListItem>
                                                <asp:ListItem Value="Stock">Stock</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button runat="server" ID="btnGetReport" Text="Get Report" OnClick="btnGetReport_Click" CssClass="btn btn-primary" />
                                        </div>
                                        <div class="col-md-1" style="margin-left: 20px;">
                                            <asp:Button runat="server" ID="Button2" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="padding: 30px;">
                                    <div>
                                        <div class="card" runat="server" id="divDrawing" visible="false">
                                            <div class="card-header bg-primary text-uppercase text-white">
                                                <h5>
                                                    <asp:Label runat="server" ID="lblDeptNameHeader"></asp:Label></h5>
                                            </div>
                                            <br />
                                            <div class="card-body" style="padding: 20px;width: 100%;">
                                                <div id="DivRoot" align="left">
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                        <asp:GridView ID="dgvDepartmentWiseRpt" runat="server" CssClass="table table-striped table-bordered nowrap"
                                                            AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="68" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SubOA Number" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOANumber" runat="server" Text='<%# Eval("SubOA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName").ToString().Replace(" ","<br><br><br>") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Size" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Size").ToString().Replace("Enclosure For Control Panel."," Size:") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtTotQty" runat="server" Text='<%# Eval("TotalQty") %>'></asp:Label>
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
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
