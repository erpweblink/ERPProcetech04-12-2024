<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="DrawingNew.aspx.cs" Inherits="Admin_DrawingNew" %>

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

    <script type='text/javascript'>
        function openModal() {
            $('[id*=myModal]').modal('show');
        }
    </script>
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

    <script type="text/javascript">   
        function CheckSingleCheckbox(ob) {
            var grid = ob.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (ob.checked && inputs[i] != ob && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
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

    <script type='text/javascript'>
        function openModal() {
            $('[id*=myModal]').modal('show');
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="container">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Drawing Creation</h5>
                            </div>
                            <br />
                            <div class="row">
                                <div id="DivRoot" style="margin-left: 3%;">
                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                    </div>
                                    <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">

                                        <asp:GridView ID="dgvDrawing" runat="server" CssClass="table table-striped table-bordered nowrap"
                                            AutoGenerateColumns="false" DataKeyNames="VWID">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="20" HeaderText="Select" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVWID" runat="server" Text='<%# Eval("VWID") %>' Visible="false"></asp:Label>
                                                        <asp:CheckBox ID="chkRow" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OA Number" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOANumber" runat="server" Text='<%# Eval("OAno") %>'></asp:Label>
                                                        <asp:Label ID="lblSubOANumber" runat="server" Text='<%# Eval("SubOANumber") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="lblCustName" ReadOnly="true" TextMode="MultiLine" Rows="4" Width="130" CssClass="form-control" Text='<%# Eval("CustomerName") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size" ItemStyle-Width="400" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="txtSize" Text='<%# Eval("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Quantity" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deliv Date" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDt" runat="server" Text='<%# Eval("deliverydatereqbycust","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inward Date and Time" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInwardDtTime" runat="server" Text='<%# Eval("currentdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="InQty" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtInwardQty" Width="60" ReadOnly="true" CssClass="form-control" Text='<%# Eval("Qty") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="OutDtTime" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOutwardDtTime" runat="server" Width="100" ReadOnly="false" CssClass="form-control myDate"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OutQty" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtOutwardQty" Width="60" CssClass="form-control" Text='<%# Eval("Qty") %>' OnTextChanged="txtOutwardQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="500" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtRemark" Width="130" Height="100" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBtnview" runat="server" CommandArgument='<%# Eval("mainID")%>' CommandName="rowDisplay" ToolTip="View Attachment"><i class="fa fa-folder-open" style="font-size: x-large;" aria-hidden="true"></i></asp:LinkButton>
                                                        |
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" CommandName="rowSend" OnClientClick="Confirm()" CommandArgument='<%# Eval("mainID")%>' ToolTip="Approve and Send"><i class="fa fa-paper-plane" style="font-size: 15px;" aria-hidden="true"></i>&nbsp;Send</asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

