<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AuditLogDashboard.aspx.cs" Inherits="Admin_AuditLogDashboard" %>

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
    <style>
        body {
            font-family: Arial;
        }

        /* Style the tab */
        .tab {
            overflow: hidden;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
                font-size: 17px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #ddd;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #ccc;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid #ccc;
            border-top: none;
        }

        .form-group {
            padding: 15px;
        }

        .refresh {
            float: right;
        }
    </style>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <!-- Boostrap DatePciker JS  -->
    <link href="../JS/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../JS/bootstrap-datepicker.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function pageLoad() {
            $(document).ready(function () {
                $('.myDate').datepicker({
                    dateFormat: 'yy-mm-dd',
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">

                    <div class="row">
                        <div class="col-md-12" style="margin-top: 20px;">
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnEnquiry" Text="Enquiry" OnClick="btnEnquiry_Click" />
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnQuatation" Text="Quatations" OnClick="btnQuatation_Click" />
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnOrderAccept" Text="Order Acceptance" OnClick="btnOrderAccept_Click" />
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnLoginList" Text="Login Log" OnClick="btnLoginList_Click"  visible="False"/>
                            <asp:Button runat="server" CssClass="btn btn-grd-inverse refresh" ID="btnRefresh" Text="Refresh" OnClick="btnRefresh_Click" />
                        </div>

                        <%--Login List--%>
                        <div class="col-sm-12" runat="server" id="DivLoginList" visible="false">
                            <!-- Basic Form Inputs card start -->
                            <div class="card">
                                <div class="card-header">
                                    <h4>Login List</h4>
                                </div>
                                <div class="card-block">
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtDate" CssClass="form-control myDate" placeholder="From Date" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnLoginSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnLoginSearch_Click" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row" runat="server" id="Divnotfountimg" visible="false">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-8">
                                            <center>
                                                <asp:Label runat="server" Text="No Record Found" style="font-size:26px;text-align:center;font-weight:600" cssClass="btn btn-info"></asp:Label>
                                                            </center>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <div id="divgv" runat="server">
                                                <asp:GridView ID="GvLoginlog" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                    DataKeyNames="id" AllowPaging="true" PageSize="10" OnPageIndexChanging="GvLoginlog_PageIndexChanging">
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Enquiry List--%>
                        <div class="col-sm-12" runat="server" id="DivEnquiry">
                            <!-- Basic Form Inputs card start -->
                            <div class="card">
                                <div class="card-header">
                                    <h4>Enquiry List</h4>
                                </div>
                                <div class="card-block">
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtFromdate" CssClass="form-control myDate" placeholder="From Date" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <strong style="margin-top: 6px;"><i class="fa fa-arrow-right" aria-hidden="true"></i></strong><strong style="margin-top: 6px;"><i class="fa fa-arrow-left" aria-hidden="true"></i></strong>
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtTodate" CssClass="form-control myDate" placeholder="To Date" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnEnquirySearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnEnquirySearch_Click" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row" runat="server" id="Div1" visible="false">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-8">
                                            <center>
                                                <asp:Label runat="server" Text="No Record Found" style="font-size:26px;text-align:center;font-weight:600" cssClass="btn btn-info"></asp:Label>
                                                            </center>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="dgvEnquiryList" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false" AllowPaging="false" PageSize="50">
                                                <PagerStyle CssClass="GridPager" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Sr.No</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblsno" Text="<%# Container.DataItemIndex +1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EnqCode" ShowHeader="true" HeaderText="Enquiry Code" />
                                                    <asp:BoundField DataField="cname" ShowHeader="true" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="sessionname" ShowHeader="true" HeaderText="Created By" />
                                                    <asp:BoundField DataField="regdate" ShowHeader="true" HeaderText="Created Date & Time" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Quatations List--%>
                        <div class="col-sm-12" runat="server" id="DivQuatation" visible="false">
                            <!-- Basic Form Inputs card start -->
                            <div class="card">
                                <div class="card-header">
                                    <h4>Quatation List</h4>
                                </div>
                                <div class="card-block">
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtFromDate1" CssClass="form-control myDate" placeholder="From Date" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <strong style="margin-top: 6px;"><i class="fa fa-arrow-right" aria-hidden="true"></i></strong><strong style="margin-top: 6px;"><i class="fa fa-arrow-left" aria-hidden="true"></i></strong>
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtToDate1" CssClass="form-control myDate" placeholder="To Date" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnQuatationSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnQuatationSearch_Click" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row" runat="server" id="Div2" visible="false">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-8">
                                            <center>
                                                <asp:Label runat="server" Text="No Record Found" style="font-size:26px;text-align:center;font-weight:600" cssClass="btn btn-info"></asp:Label>
                                                            </center>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="dgvQuatationList" runat="server" AutoGenerateColumns="false" AllowPaging="false" CssClass="table table-striped table-bordered nowrap" PageSize="50">
                                                <PagerStyle CssClass="GridPager" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Sr.No</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblsno" Text="<%# Container.DataItemIndex +1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="quotationno" ShowHeader="true" HeaderText="Quotation No" />
                                                    <asp:BoundField DataField="partyname" ShowHeader="true" HeaderText="Party Name" />
                                                    <asp:BoundField DataField="material" ShowHeader="true" HeaderText="Material" />
                                                    <asp:BoundField DataField="sessionname" ShowHeader="true" HeaderText="Created By" />
                                                    <asp:BoundField DataField="createddate" ShowHeader="true" HeaderText="Date & Time" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Order Acceptance List--%>
                        <div class="col-sm-12" runat="server" id="DivOrderAccept" visible="false">
                            <!-- Basic Form Inputs card start -->
                            <div class="card">
                                <div class="card-header">
                                    <h4>Order Acceptance List</h4>
                                </div>
                                <div class="card-block">
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtfromDate2" CssClass="form-control myDate" placeholder="From Date" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <strong style="margin-top: 6px;"><i class="fa fa-arrow-right" aria-hidden="true"></i></strong><strong style="margin-top: 6px;"><i class="fa fa-arrow-left" aria-hidden="true"></i></strong>
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" ID="txtToDate2" CssClass="form-control myDate" placeholder="To Date" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnOrderAcceptance" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnOrderAcceptance_Click" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row" runat="server" id="Div3" visible="false">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-8">
                                            <center>
                                                <asp:Label runat="server" Text="No Record Found" style="font-size:26px;text-align:center;font-weight:600" cssClass="btn btn-info"></asp:Label>
                                                            </center>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">

                                            <asp:GridView ID="dgvOrderAcceptList" runat="server" AutoGenerateColumns="false" AllowPaging="false" CssClass="table table-striped table-bordered nowrap" PageSize="50">
                                                <PagerStyle CssClass="GridPager" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>S No</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblsno" Text="<%# Container.DataItemIndex +1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="OAno" ShowHeader="true" HeaderText="OA Number" />
                                                    <asp:BoundField DataField="customername" ShowHeader="true" HeaderText="Customer Name" />
                                                    <asp:BoundField DataField="quotationno" ShowHeader="true" HeaderText="Quotation No" />
                                                    <asp:BoundField DataField="pono" ShowHeader="true" HeaderText="PO No" />
                                                    <asp:BoundField DataField="CreatedBy" ShowHeader="true" HeaderText="Created By" />
                                                    <asp:BoundField DataField="CreatedOn" ShowHeader="true" HeaderText="Created On" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

