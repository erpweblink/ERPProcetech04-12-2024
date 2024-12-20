<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="Admin_Stock" EnableEventValidation="false" %>

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

    <%-- New SweetAlert Script by nikhil  --%>
    <script>
        function HideLabel(msg) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: msg,
                timer: 3000,
                showCancelButton: false,
                showConfirmButton: false
            }).then(function () {
                //window.location.href = "../Admin/TaxInvoiceList.aspx";
            })
        };
    </script>
    <%-- ENd  --%>

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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js">
        
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

        function CheckedCheckbox(ob) {
            //debugger;
            var checkboxval = ob.checked;
            if (checkboxval == true) {
                $('#btnshowhide').show();
                $.ajax({
                    type: 'POST',
                    url: 'Drawing.aspx/MakeSessionNull',
                    data: '{}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {

                    },
                    error: function (xhr, status, error) {

                    }
                });
            }
            else {
                $('#btnshowhide').hide();
            }

            var grid = document.getElementById("<%=dgvStock.ClientID%>");
            var checkBoxes = grid.getElementsByTagName("INPUT");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    $('#btnshowhide').show();
                }
            }
        }
        function Linkclicked(txt) {

            var grid = document.getElementById("<%= dgvStock.ClientID%>");
            var row = txt.parentNode.parentNode;
            var rowIndex = row.rowIndex;
            //var Userid = row.cells[3].innerHTML;
            //var suboanumber = grid.rows[rowIndex].cells[3].childNodes[1].childNodes[0].data;
            var suboanumber = grid.rows[rowIndex].cells[4].childNodes[1].childNodes[0].data;
            var InwardQty = grid.rows[rowIndex].cells[7].childNodes[1].value;

            $("#<%=txtReturnInward.ClientID%>").val(InwardQty);
            $("#<%=hdnInwardQty.ClientID%>").val(InwardQty);
            $("#<%=hdnSubOANo.ClientID%>").val(suboanumber);

            $('#divReturn').show();
            $.ajax({
                type: 'POST',
                url: 'Drawing.aspx/MakeSessionNull',
                data: '{}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {

                },
                error: function (xhr, status, error) {

                }
            });

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
    </script>
    <style>
        .lblsuboa {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="pnlContainer">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="page-wrapper">
                    <div class="page-body">
                        <div class="container-fluid">
                            <div class="card">
                                <div class="card-header bg-primary text-uppercase text-white">
                                    <h5>Finish Goods Inventory </h5>
                                    <asp:HiddenField runat="server" ID="hdnInwardQty" />
                                    <asp:HiddenField runat="server" ID="hdnSubOANo" />
                                </div>
                                <div class="row" style="padding: 10px;">
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCustomerNameNew" runat="server" CssClass="form-control" placeholder="Customer Name" Width="100%" OnTextChanged="txtCustomerNameNew_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                            TargetControlID="txtCustomerNameNew">
                                        </asp:AutoCompleteExtender>

                                    </div>


                                    <div class="col-md-4">
                                        <asp:LinkButton ID="btnPrintData" runat="server" CssClass="btn btn-small btn-danger" OnClick="btnPrintData_Click"><i class="fa fa-print"></i>&nbsp;Print</asp:LinkButton>
                                        <asp:LinkButton ID="btnexcel" runat="server" CssClass="btn btn-small   btn-success " OnClick="btnexcel_Click" OnClientClick="aspnetForm.target ='_blank';"><i class="fa fa-download" aria-hidden="true"></i>&nbsp;Export Excel</asp:LinkButton>
                                    </div>

                                    <div class="page-header-breadcrumb" runat="Server" visible="false">
                                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                                            <span id="btnstockoldlist" runat="server"><a href="Stock_OldDB.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;STOCK OLD DATA</a>&nbsp;&nbsp;
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
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
                                    <div class="col-md-2" id="btnshowhide">
                                        <asp:LinkButton ID="btnGetSelected" runat="server" CssClass="btn btn-small btn-primary" OnClick="GetSelectedRecords"><i class="fa fa-truck"></i>&nbsp;Dispatch</asp:LinkButton>
                                        <%--fa fa-send-o--%>
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                </div>
                                <%--Return div--%>
                                <div class="row" style="padding: 20px; display: none;" id="divReturn">
                                    <div class="col-md-6">
                                        <div class="row" runat="server">
                                            <div class="col-md-3 spancls">Inward Qty:</div>
                                            <div class="col-md-3">
                                                <asp:TextBox runat="server" ID="txtReturnInward" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 spancls">Stages:</div>
                                            <div class="col-md-3">

                                                <asp:DropDownList runat="server" ID="ddlstages" CssClass="form-control">
                                                    <asp:ListItem Text="Select" Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Text="Drawing Creation" Value="tblDrawing">Drawing Creation</asp:ListItem>
                                                    <asp:ListItem Text="Laser Programing" Value="tblLaserPrograming">TPP Programing</asp:ListItem>
                                                    <asp:ListItem Text="Laser Cutting" Value="tblLaserCutting"> TPP Cutting</asp:ListItem>
                                                    <asp:ListItem Text="CNC Bending" Value="tblCNCBending">CNC Bending</asp:ListItem>
                                                    <asp:ListItem Text="Welding" Value="tblWelding">Welding</asp:ListItem>
                                                    <asp:ListItem Text="Powder coating" Value="tblPowderCoating">Powder coating</asp:ListItem>
                                                    <asp:ListItem Text="Final assembly" Value="tblFinalAssembly">Final assembly</asp:ListItem>
                                                    <asp:ListItem Text="Quality Assurance" Value="tblQualityAsurance">Quality assurance</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>


                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lnkbtnReturn" runat="server" CssClass="btn btn-small btn-primary" OnClick="lnkbtnReturn_Click"><i class="fa fa-undo"></i>&nbsp;Return Quantity</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Close Return Div--%>

                                <div class="row">
                                    <div class="col-md-12" style="padding: 30px;">
                                        <div id="DivRoot" align="left">
                                            <div style="overflow: hidden;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="dgvStock" runat="server" CssClass="table table-striped table-bordered nowrap"
                                                    AutoGenerateColumns="false" DataKeyNames="SubOA" OnRowDataBound="dgvStock_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="20">
                                                            <ItemTemplate>
                                                                <img alt="" style="cursor: pointer" src="../img/plus.png" />
                                                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                    <div class="col-md-12">
                                                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Customer Name :"></asp:Label>
                                                                        <asp:Label ID="lblcustomersname" runat="server" Text='<%# Eval("customername") %>'></asp:Label>

                                                                    </div>
                                                                    <div>
                                                                        <hr />
                                                                    </div>
                                                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                                                        <Columns>
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="OANumber" HeaderText="OA Number" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="SubOA" HeaderText="Sub OA No" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="pono" HeaderText="PO No" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="TotalQty" HeaderText="Total Qty" />

                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="InwardDtTime" HeaderText="Inward DateTime" />
                                                                            <%--<asp:BoundField ItemStyle-Width="150px" DataField="OutwardDtTime" HeaderText="Outward DateTime" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="20" HeaderText="Select" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="false" onclick="CheckedCheckbox(this)" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%-- New Filed added by Nikhil 10-12-2024 --%>
                                                        <asp:TemplateField HeaderText="Job No" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JobNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- End  --%>

                                                        <asp:TemplateField HeaderText="OA Number" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOANumber" runat="server" Text='<%# Eval("OANumber") %>'></asp:Label>
                                                                <asp:Label ID="lblSubOANumberr" runat="server" Text='<%# Eval("SubOA") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Code" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblCustName" ReadOnly="true" TextMode="MultiLine" Visible="false" Rows="4" Width="130" CssClass="form-control" Text='<%# Eval("customername") %>'></asp:TextBox>
                                                                <asp:Label ID="lblSubOANumber" CssClass="lblsuboa" runat="server" Text='<%# Eval("SubOA") %>' Visible="true"></asp:Label>
                                                                <asp:Label ID="lblCustCode" runat="server" Text='<%# Eval("CustomerCode").ToString().Replace(" ", "<br /><br />") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--        <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubOANumber" CssClass="lblsuboa" runat="server" Text='<%# Eval("SubOA") %>' Visible="true"></asp:Label>
                                                                <asp:TextBox runat="server" ID="lblCustName" ReadOnly="true" TextMode="MultiLine" Rows="4" Width="100%" CssClass="form-control" Text='<%# Eval("CustomerName") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Size" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="lblSize" ReadOnly="true" Width="100%" TextMode="MultiLine" Rows="5" CssClass="form-control" Text='<%# Eval("Size") %>'></asp:TextBox>

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
                                                                <asp:TextBox runat="server" ID="txtInwardQty" Width="100%" ReadOnly="true" CssClass="form-control" Text='<%# Eval("InwardQty") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Outward DateTime" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOutwardDtTime" runat="server" Width="100" ReadOnly="false" CssClass="form-control myDate"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Out Qty" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Width="100%" ID="txtOutwardQty" onkeypress="return ValidNumeric()"  AutoPostBack="false" CssClass="form-control" Text='<%# Eval("InwardQty") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <a href="<%#string.Format("../Reports/ReportPDF.aspx?Dept="+encrypt("Dispatch")+"&OANumberWise={0}", encrypt(Eval("OANumber").ToString())) %>" target="_blank">
                                                                    <asp:Image ID="ImgPrint" runat="server" CssClass="img1" ImageUrl="../img/Print.png" Height="29px" ToolTip="Print" /></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Return Qty" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkdetail" OnClick="Linkclicked(this)"><i class="fa fa-undo" style="font-size: 25px!important" title="Return Quantity"></i></asp:LinkButton>
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
    </asp:Panel>
</asp:Content>

