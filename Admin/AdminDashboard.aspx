<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminDashboard.aspx.cs" Inherits="Admin_AdminDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .card-bg1 {
            background-color: dodgerblue;
        }

        .card-bg2 {
            background-color: crimson;
        }

        .card-bg3 {
            background-color: green;
        }

        .card-bg4 {
            background-color: slateblue;
        }

        .lblcount {
            color: white !important;
        }

        .mar {
            margin-left: 10px;
        }
    </style>

    <style type="text/css">
        .paging {
        }

            .paging a {
                background-color: #0755A1;
                padding: 1px 7px;
                text-decoration: none;
                border: 1px solid #0755A1;
            }

                .paging a:hover {
                    background-color: #E1FFEF;
                    color: white;
                    border: 1px solid #47417c;
                }

            .paging span {
                background-color: #0755A1;
                padding: 1px 7px;
                color: white;
                border: 1px solid #0755A1;
            }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
            }

        .card1 {
            /*margin-top: 78px;*/
            border-radius: 15px;
        }

        .listdash {
            width: 50%;
            padding: 33px;
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

        .box {
            border: 3px solid #795548;
            padding: 5px;
            border-radius: 4px;
            box-shadow: 4px 2px 3px 3px #888888;
            height: 100px;
            color: #101010;
            /*background: #b2ccec;*/
        }

        .clstotal {
            color: #000;
            font-weight: 600;
        }

        .clspaid {
            color: green;
            font-weight: 600;
        }

        .clsunpaid {
            color: red;
            font-weight: 600;
        }

        .Box-align {
            text-align: center;
        }

        .fa {
            display: initial !important;
        }
    </style>

    <style>
        .lblbold {
            font-size: 14px;
            font-weight: 600;
        }

        hr.new1 {
            border-top: 1px dashed green !important;
            border: 0;
            margin-top: 5px !important;
            margin-bottom: 5px !important;
            width: 100%;
        }
    </style>



    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <!-- Boostrap DatePciker JS  -->
    <link href="../JS/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../JS/bootstrap-datepicker.js"></script>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
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
                //$('.myDate').datepicker('setDate', new Date());
                $('.ui-datepicker').hide();
            });
        }
    </script>

    <%--       <script>
        $(document).ready(
            function () {
                $("#arrowclick").click(function () {
                    $("#musicinfo").toggle();

                   
                });
            });
    </script>--%>

    <%-- <script>
        $(document).ready(function () {

            $("#arrowclick").click(function () {
                $("#musicinfo").toggle();
                $(this).find("i").toggleClass("fa fa-arrow-right fa fa-arrow-down");
            });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div id="cardCustomers" runat="server" class="col-xl-4 col-md-6 mb-4">
                                <div class="card1 border-left-info shadow h-90 py-2 card-bg1">
                                    <a href="AllCompanyList.aspx" style="text-decoration: none; display: block;">
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-info text-uppercase mb-1 lblcount">
                                                        Customers
                                                    </div>
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col-auto">
                                                            <div class="h4 mb-0 mr-3 font-weight-bolder text-white">
                                                                <asp:Label ID="lblcustomercount" runat="server" Text="Label" Font-Size="Larger" Font-Bold="true"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-auto">
                                                    <i class="fa fa-users fa-2x text-info" aria-hidden="true"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <%-- </div>--%>
                            </div>
                            <div id="cardQuotation" runat="server" class="col-xl-4 col-md-6 mb-4">
                                <div class="card1 border-left-warning shadow h-90 py-2 card-bg2">
                                    <a href="QuotationList.aspx" style="text-decoration: none; display: block;">
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-info text-uppercase mb-1 lblcount">
                                                        Quotation
                                           
                                                    </div>
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col-auto">
                                                            <div class="h4 mb-0 mr-3 font-weight-bolder text-white">
                                                                <asp:Label ID="lblQuotationcount" runat="server" Text="Label" Font-Size="Larger" Font-Bold="true"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-auto">
                                                    <i class="fa fa-file-pdf-o fa-2x text-warning" aria-hidden="true"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>

                            <div id="cardOA" runat="server" class="col-xl-4 col-md-6 mb-4">
                                <div class="card1 border-left-success shadow h-90 py-2 card-bg3">
                                    <a href="OrderAcceptanceList.aspx" style="text-decoration: none; display: block;">
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-info text-uppercase mb-1 lblcount">
                                                        Order Acceptance
                                           
                                                    </div>
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col-auto">
                                                            <div class="h4 mb-0 mr-3 font-weight-bolder text-white">
                                                                <asp:Label ID="lblOrderAcceptancecount" runat="server" Text="Label" Font-Size="Larger" Font-Bold="true"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-auto">
                                                    <i class="fa fa-check fa-2x text-success"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <%--<div id="cardUsers" runat="server" class="col-xl-3 col-md-6 mb-4">
                                <div class="card1 border-left-danger shadow h-90 py-2 card-bg4">
                                    <a href="Addusers.aspx" style="text-decoration: none; display: block;">
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-info text-uppercase mb-1 lblcount">
                                                        Total Users
                                                    </div>
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col-auto">
                                                            <div class="h4 mb-0 mr-3 font-weight-bolder text-white">
                                                                <asp:Label ID="lblUsercount" runat="server" Text="Label" Font-Size="Larger" Font-Bold="true"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-auto">
                                                    <i class="fa fa-user fa-2x text-danger" aria-hidden="true"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>--%>
                        </div>

                        <div class="row">
                            <div id="cardTodayEnquiries" runat="server" class="col-md-6">
                                <div class="card-header bg-primary text-uppercase text-white">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <h5>Today's Enquiries</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-12 col-md-12">
                                        <div class="card">
                                            <div class="card-header">
                                                <div class="dt-responsive table-responsive">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="dgvEnquiry" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                            AllowPaging="true" PageSize="5" OnPageIndexChanging="dgvEnquiry_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Comapny Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblComapnyName" runat="server" Text='<%# Eval("cname") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Created By" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("sessionname") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="cardTodayQuotation" runat="server" class="col-md-6">
                                <div class="card-header bg-primary text-uppercase text-white">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <h5>Today's Quotation</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-12 col-md-12">
                                        <div class="card">
                                            <div class="card-header">
                                                <div class="dt-responsive table-responsive">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="dgvQuoation" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                            AllowPaging="true" PageSize="5" OnPageIndexChanging="dgvQuoation_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quotation No" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuotationNo" runat="server" Text='<%# Eval("quotationno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Comapny Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblComapnyName" runat="server" Text='<%# Eval("partyname") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quotation Date" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date").ToString().TrimEnd("0:0".ToCharArray()) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Created By" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("sessionname") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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

                        <div class="row" style="display: none">
                            <div class="col-md-12">
                                <div class="card-header bg-primary text-uppercase text-white">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h5 style="text-align: left">Today's Login Log</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-12 col-md-12">
                                        <div class="card">
                                            <div class="card-header">
                                                <div class="row">
                                                    <div class="col-xl-2 col-md-2" style="margin-left: 15px;">
                                                        <asp:TextBox runat="server" ID="txtLoginDate" TextMode="Date" CssClass="form-control" placeholder="Select Date" autocomplete="off" AutoPostBack="true" OnTextChanged="txtLoginDate_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-xl-10 col-md-10"></div>

                                                </div>
                                                <div class="dt-responsive table-responsive">

                                                    <div class="col-md-12">
                                                        <asp:GridView ID="GvLoginlog" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                            DataKeyNames="id" AllowPaging="true" OnPageIndexChanging="GvLoginlog_PageIndexChanging" PageSize="20">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblempname" runat="server" Text='<%# Eval("LoginId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Last Login Date" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblccode" runat="server" Text='<%# Eval("LoginDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Last Login Time" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblccode" runat="server" Text='<%# Eval("LoginTime") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                    <h2>
                                                        <center>
                                                            <asp:Label ID="lblnoLogdatafound" runat="server" Text="" Visible="false" CssClass="lblboldred"></asp:Label>
                                                        </center>
                                                    </h2>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>

                        <div class="card" id="carddateprocessedvaluesection" runat="server">
                            <div class="card-header bg-primary text-uppercase text-white" id="music">
                                <div class="row">
                                    <div class="col-md-10">
                                        <h5>Date wise Processed Value</h5>
                                    </div>
                                    <div class="col-md-2" style="text-align: end; display: none;">
                                        <a href="#" id="arrowclick" style="color: white;"><i class="fa fa-arrow-down"></i></a>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="card-body" id="musicinfo">
                                <div class="row container">
                                    <asp:Repeater ID="RptSalesDetails" runat="server" OnItemDataBound="RptSalesDetails_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="col-md-3">
                                                <div class="box">
                                                    <asp:Label ID="lblname" runat="server" CssClass="text-capitalize text-black text-underline" Text='<%# Eval("name") %>' Style="font-size: 14px; font-weight: 600"></asp:Label>&nbsp;(<asp:Label ID="lblempcode" runat="server" Text='<%# Eval("empcode") %>' Style="font-size: 12px;"></asp:Label>)<br />
                                                    <div class="clstotal">
                                                        Total : 
                                                 <asp:Label ID="lbltotalclientRP" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <br />

                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2 spancls">Date:</div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtDate" CssClass="form-control myDate" placeholder="Select Date" autocomplete="off" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <%-- <asp:Button runat="server" OnClick="Unnamed_Click" Text="Refresh.." style="font-size: small; background-color: #01a9ac; color: black;"/> --%>
                                            <asp:Button ID="btnRefresh" runat="server" OnClick="Unnamed_Click" Text="Refresh.." Style="display: none;" />
                                            <i class="fa fa-undo" style="font-size: 25px!important; cursor: pointer;" onclick="document.getElementById('<%= btnRefresh.ClientID %>').click();"></i>

                                        </div>
                                        <br />
                                        <hr />
                                        <div class="col-md-10">
                                            <asp:Chart ID="Chart1" runat="server"
                                                BackGradientStyle="LeftRight" Height="350px" Palette="None"
                                                PaletteCustomColors="192, 0, 0" Width="1000px">
                                                <Series>
                                                    <asp:Series Name="Series1">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <BorderSkin BackColor="" PageColor="192, 64, 0" />
                                            </asp:Chart>

                                        </div>
                                    </div>
                                    <br />
                                    <hr />
                                    <div class="col-md-12 d-none">
                                        <div class="row">
                                            <div class="col-md-2 spancls">From Date:</div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control myDate" placeholder="Select From Date" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1 spancls">To Date:</div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control myDate" placeholder="Select To Date" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 spancls">Department:</div>
                                            <div class="col-md-2">
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
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" ID="btnGetData" Text="GET REPORT" CssClass="btn btn-primary" OnClick="btnGetData_Click" />
                                            </div>
                                            <div class="col-md-4"></div>
                                        </div>
                                        <br />
                                        <hr />
                                        <div class="col-md-10">
                                            <asp:Chart ID="Chart2" runat="server"
                                                BackGradientStyle="LeftRight" Height="350px" Palette="None"
                                                PaletteCustomColors="192, 0, 0" Width="1000px">
                                                <Series>
                                                    <asp:Series Name="Series1">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <BorderSkin BackColor="" PageColor="192, 64, 0" />
                                            </asp:Chart>

                                        </div>
                                    </div>


                                    <div class="row" style="display: none">
                                        <div class="col-md-12">
                                            <div class="card-header bg-primary text-uppercase text-white">
                                                <h5>Upcoming TBRO Details</h5>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-12 col-md-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row">
                                                                <div class="col-xl-10 col-md-10"></div>
                                                                <div class="col-xl-2 col-md-2">
                                                                    <asp:DropDownList ID="ddlTbrofilter" runat="server" Width="100%" AutoPostBack="true" OnTextChanged="ddlTbrofilter_TextChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="dt-responsive table-responsive">

                                                                <asp:GridView ID="GvTBRO" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                                    DataKeyNames="id" AllowPaging="true" OnPageIndexChanging="GvTBRO_PageIndexChanging" PageSize="10">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Added By">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblempname" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldateofreminder" runat="server" Text='<%# Eval("dateofreminder") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Company Code" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblccode" runat="server" Text='<%# Eval("ccode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Company Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcompname" runat="server" Text='<%# Eval("cname") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Title">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblownnamegv" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Remark">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                                <h2>
                                                                    <center>
                                                                        <asp:Label ID="lblnoTbrodatafound" runat="server" Text="" Visible="false" CssClass="lblboldred"></asp:Label>
                                                                    </center>
                                                                </h2>
                                                            </div>
                                                            <br />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                    </div>

                                    <br />



                                    <hr class="new1 d-none" />
                                    <div class="row">
                                        <div class="col-md-6"></div>
                                        <div class="col-md-6" style="text-align: right; padding: 5px 20px 5px 0px;">
                                            <asp:Label ID="lbllastlogin" CssClass="lblbold" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row" style="display: none">
                                <div class="col-md-12">
                                    <div class="card-header bg-primary text-uppercase text-white">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <h5>Date wise Processed Value</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xl-12 col-md-12">
                                        <div class="card">
                                            <div class="card-header">
                                                <div class="row">

                                                    <div class="col-xl-4 col-md-4" style="margin-left: 15px;">
                                                        <asp:DropDownList ID="ddlModule" CssClass="form-control" runat="server" Width="100%" AutoPostBack="true">
                                                            <asp:ListItem Value="0">--Select Module--</asp:ListItem>
                                                            <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                                            <asp:ListItem Value="Production">Production</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-xl-4 col-md-4" style="margin-left: 15px;">
                                                        <asp:DropDownList ID="ddlRole" CssClass="form-control" runat="server" Width="100%" AutoPostBack="true">
                                                            <asp:ListItem Value="0">--Select Department--</asp:ListItem>
                                                            <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                                            <asp:ListItem Value="drawing">Drawing Creation</asp:ListItem>
                                                            <asp:ListItem Value="laserprogramming">Laser Programing</asp:ListItem>
                                                            <asp:ListItem Value="lasercutting">Laser Cutting</asp:ListItem>
                                                            <asp:ListItem Value="bending">CNC Bending</asp:ListItem>
                                                            <asp:ListItem Value="welding">Welding</asp:ListItem>
                                                            <asp:ListItem Value="powdercoating">Powder Coating</asp:ListItem>
                                                            <asp:ListItem Value="assembly">Assembly</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-xl-4 col-md-4"></div>
                                                </div>
                                                <br />
                                                <div class="dt-responsive table-responsive">

                                                    <div class="col-md-12">
                                                        <asp:GridView ID="dgvActiveUser" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                            DataKeyNames="id" AllowPaging="true" PageSize="20">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblempname" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Email" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblccode" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Module" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblccode" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblccode" runat="server" Text='<%# Eval("role") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                    <h2>
                                                        <center>
                                                            <asp:Label ID="Label1" runat="server" Text="" Visible="false" CssClass="lblboldred"></asp:Label>
                                                        </center>
                                                    </h2>
                                                </div>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

