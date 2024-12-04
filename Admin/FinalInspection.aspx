<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="FinalInspection.aspx.cs" Inherits="Admin_FinalInspection" %>

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

        .selected_row {
            background-color: #A1DCF2;
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
    <script type="text/javascript">
        function ShowPopup() {
            $("#myModal").modal("show");
        }
    </script>
    <%-- <script>
        function ValidNumeric() {
            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57) { return true; }
            else { return false; }
        }
    </script>--%>

    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {
                        row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Approve?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../img/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="container">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Final Inspection</h5>
                            </div>
                            <br />
                            <div class="row" style="padding: 10px;">
                                <div class="col-md-8">
                                    <div class="row" runat="server" visible="false">
                                        <div class="col-md-2 spancls">OA Number:</div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlONumber" CssClass="form-control" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlONumber_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <b>-OR-</b>
                                        </div>
                                        <div class="col-md-2 spancls">Customer Name:</div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtcustomerName" CssClass="form-control" OnTextChanged="txtOANumber_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row" runat="server" visible="false">
                                        <div class="col-md-2 spancls">
                                            <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="btnGetSelected" runat="server" CssClass="btn btn-small btn-primary" OnClick="GetSelectedRecords" OnClientClick="Confirm()" Visible="false"><i class="fa fa-angle-double-right"></i>&nbsp;Approve & Send</asp:LinkButton>
                                </div>
                                <br />
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="padding: 30px;">
                                    <div style="overflow-x: scroll; width: 100%;">
                                        <asp:GridView ID="dgvFinalInspection" runat="server" CssClass="table table-striped table-bordered nowrap"
                                            AutoGenerateColumns="false" DataKeyNames="SubOA" OnRowDataBound="dgvFinalInspection_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="20">
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer" src="../img/plus.png" />
                                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="OANumber" HeaderText="OA Number" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="SubOA" HeaderText="Sub OA No" />
                                                                    
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="TotalQty" HeaderText="Total Qty" />
                                                                    
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CreatedDate" HeaderText="Inward DateTime" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="20" HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                    <%--<HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true" OnCheckedChanged="checkAll_CheckedChanged" />
                                                    </HeaderTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" onclick="CheckSingleCheckbox(this)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OA Number" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOANumber" runat="server" Text='<%# Eval("OANumber") %>'></asp:Label>
                                                        <asp:Label ID="lblSubOANumber" runat="server" Text='<%# Eval("SubOA") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
														<asp:TextBox runat="server" ID="lblCustName" ReadOnly="true" TextMode="MultiLine" Rows="4" Width="150" CssClass="form-control" Text='<%# Eval("CustomerName") %>'></asp:TextBox>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="lblSize" ReadOnly="true" Width="250" TextMode="MultiLine" Rows="5" CssClass="form-control" Text='<%# Eval("Size") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Quantity" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("TotalQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Date" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDt" runat="server" Text='<%# Eval("DeliveryDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inward Date and Time" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInwardDtTime" runat="server" Text='<%# Eval("InwardDtTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="In Qty" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtInwardQty" Width="60" ReadOnly="true" CssClass="form-control" Text='<%# Eval("InwardQty") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Outward DateTime" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOutwardDtTime" runat="server" Width="100" ReadOnly="false" CssClass="form-control myDate"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Out Qty" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" Width="60" ID="txtOutwardQty" OnTextChanged="txtOutwardQty_TextChanged" onkeypress="return ValidNumeric()" AutoPostBack="true" CssClass="form-control" Text='<%# Eval("InwardQty") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="<%#string.Format("../Reports/ReportPDF.aspx?Dept="+encrypt("Final Inspection")+"&OANumberWise={0}", encrypt(Eval("OANumber").ToString())) %>" target="_blank">
                                                            <asp:Image ID="ImgPrint" runat="server" CssClass="img1" ImageUrl="../img/Print.png" Height="29px" ToolTip="Print" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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

