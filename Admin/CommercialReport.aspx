<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="CommercialReport.aspx.cs" Inherits="Admin_CommercialReport" EnableEventValidation="false" %>

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

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <!-- Boostrap DatePciker JS  -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />

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
                    <div class="container-fluid">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Commercial Report</h5>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="padding: 30px;">
                                    <div class="col-md-10">
                                        <div class="row">
                                            <div class="col-md-2 spancls">Date:</div>
                                            <div class="col-md-3">
                                                <asp:TextBox runat="server" ID="txtDate" CssClass="form-control myDate" placeholder="Select Date" autocomplete="off" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox runat="server" ID="txtfromdate" CssClass="form-control myDate" placeholder="Select From Date" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox runat="server" ID="txttodate" CssClass="form-control myDate" placeholder="Select To Date" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Button runat="server" ID="btnGetReport" Text="Get Report" OnClick="btnGetReport_Click" CssClass="btn btn-primary" />
                                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-primary" />
                                                <asp:Button runat="server" ID="btnexcell" Text="Export Excell"  OnClick="btnexcell_Click" CssClass="btn btn-primary" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <asp:GridView ID="dgvCommercialRpt" runat="server" CssClass="table table-striped table-bordered nowrap"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="68" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTotQty" runat="server" Text='<%# Eval("Total_Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processed Quantity" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcessedQuantity" runat="server" Text='<%# Eval("Processed_Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value Price" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtValuePrice" runat="server" Text='<%# Eval("TotalValue", "{0:C}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processed Value" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcessedValue" runat="server" Text='<%# Eval("TotalProcessValue", "{0:C}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Average Value" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcessedValue" runat="server" Text='<%# Eval("AVERAGE", "{0:C}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID ="btnexcell" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
