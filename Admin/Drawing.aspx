<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Drawing.aspx.cs" Inherits="Admin_Drawing" %>

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
                max-width: 90% !important;
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


    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        function SetTarget() {
            document.forms[0].target = "_blank";
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
    <script type="text/javascript">   
        function CheckSingleCheckbox(ob) {
            var grid = ob.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (ob.checked && inputs[i] != ob && inputs[i].checked) {

                        //MakeStaticHeader('dgvLaserprogram', 400, 1020, 40, false)

                    }
                }
            }
        }

        function CheckedCheckbox(ob) {
            var checkboxval = ob.checked;
            if (checkboxval == true) {
                $('#btnshowhide').show();
            }
            else {
                $('#btnshowhide').hide();
            }

            var grid = document.getElementById("<%=dgvDrawing.ClientID%>");
            var checkBoxes = grid.getElementsByTagName("INPUT");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    $('#btnshowhide').show();
                }
            }
        }


        function Keydown(txt) {           
            var grid = document.getElementById("<%= dgvDrawing.ClientID%>");

            var row = txt.parentNode.parentNode;
            var rowIndex = row.rowIndex;           
            var dataValue = { "RowIndex": rowIndex, "InwardQty": inwardval, "OutwardQty": txtval };

            var inwardval = grid.rows[rowIndex].cells[7].childNodes[1].value;
            var txtval = txt.value;

            if (parseFloat(txtval) <= parseFloat(inwardval)) {   
                
                alert("Inward Quantity will change after sending quantity to Next Stage.");
                var checkbox = row.querySelector('input[type="checkbox"]');
                if (checkbox) {
                    checkbox.disabled = false;
                }
            } else {
               
                alert("The value should be less than or equal to the Inward Quantity.");
                var checkbox = row.querySelector('input[type="checkbox"]');
                if (checkbox) {
                    checkbox.disabled = true;
                }
            }


            //alert("Inward Quantity will change after sending quantity to Next Stage.");

            //$.ajax({
            //    type: "POST",
            //    url: "Drawing.aspx/OnSubmit",
            //    data: JSON.stringify(dataValue),
            //    contentType: 'application/json; charset=utf-8',
            //    dataType: 'json',
            //    error: function (XMLHttpRequest, textStatus, errorThrown) {
            //        alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            //    },
            //    success: function (result) {
            //    }
            //});



            <%-- var grid = document.getElementById("<%= dgvDrawing.ClientID%>");
            var row = txt.parentNode.parentNode;
            var rowIndex = row.rowIndex;

            var inwardval = grid.rows[rowIndex].cells[6].childNodes[1].value;

            var OutwardQty = $("input[id*=txtOutwardQty]")

            if (OutwardQty[rowIndex].value != '') {
                var Minusqty = parseInt(inwardval) - parseInt(OutwardQty[rowIndex].value);


                console.log(Minusqty);
                //var txtbx = grid.rows[rowIndex].cells[6].childNodes[1];
                //txtbx.val = Minusqty;
                //alert("Inward Quantity will change after sending quantity to Next Stage.");
            }--%>      

        }


    </script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../img/plus.png");
            $(this).closest("tr").next().remove();


        });

        document.querySelectorAll("[src*=minus]").forEach(function (element) {
            element.addEventListener("click", function () {
                element.src = "../img/plus.png";
                var tr = element.closest("tr");
                tr.nextSibling.remove();
            });

        });
    </script>

    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            debugger;
            $('#btnshowhide').hide();

            var tbl = document.getElementById(gridId);
            if (tbl) {
                debugger;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="container-fluid">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Drawing Creation</h5>
                            </div>

                            <div class="row" style="padding: 10px;">
                                <div class="col-md-4">

                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" placeholder="Customer Name" Width="100%" OnTextChanged="txtCustomerName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                        TargetControlID="txtCustomerName">
                                    </asp:AutoCompleteExtender>


                                    <%--                                    <asp:LinkButton ID="OAREPORTSNEW" runat="server" CssClass="btn btn-sm btn-google-plus">
                                     <a href="OAReportsForDays.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;"> OA_Reports</a>
                                    </asp:LinkButton>--%>
                                    <%-- <asp:Button ID="btnexcel" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Text="Export Excel" OnClick="btnexcel_Click" />--%>
                                </div>

                                <div class="col-md-4">

                                    <asp:LinkButton ID="btnPrintData" runat="server" CssClass="btn btn-small btn-danger" OnClick="btnPrintData_Click" OnClientClick="aspnetForm.target ='_blank';"><i class="fa fa-print"></i>&nbsp;Print</asp:LinkButton>
                                    <asp:LinkButton ID="btnexcel" runat="server" CssClass="btn btn-small   btn-success " OnClick="btnexcel_Click" OnClientClick="aspnetForm.target ='_blank';"><i class="fa fa-download" aria-hidden="true"></i>&nbsp;Export Excel</asp:LinkButton>
                                </div>

                                <div class="page-header-breadcrumb" runat="server" visible="false">
                                    <div style="float: right; margin: 3px; margin-bottom: 5px;">
                                        <span id="btndrawinglist" runat="server"><a href="Drawing_OldDB.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;DRAWING CREATION OLD DATA</a>&nbsp;&nbsp;
                                        </span>
                                    </div>
                                </div>



                                <div class="col-md-2" id="btnshowhide">
                                    <asp:LinkButton ID="btnGetSelected" runat="server" CssClass="btn btn-small btn-primary" OnClick="GetSelectedRecords"><i class="fa fa-angle-double-right"></i>&nbsp;Approve & Send</asp:LinkButton>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-2 spancls">
                                            <asp:Button runat="server" ID="btnReset" Visible="false" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-12" style="padding: 30px;">
                                    <div id="DivRoot" align="left">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="dgvDrawing" runat="server" CssClass="table table-striped table-bordered nowrap"
                                                AutoGenerateColumns="false" DataKeyNames="SubOA" OnRowCommand="dgvDrawing_RowCommand" OnRowDataBound="dgvDrawing_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="20" HeaderText="View">
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
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="OAno" HeaderText="OA Number" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="SubOANumber" HeaderText="SUB OA No" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Qty" HeaderText="Total Qty" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="pono" HeaderText="PO No" />
                                                                        <%-- Commented because used multitimes by Nikhil --%>
                                                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="deliverydatereqbycust" HeaderText="Delivery Date" />--%>
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="currentdate" HeaderText="Inward DateTime" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="OutwardDtTime" HeaderText="OutWard DateTime" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <br />
                                                                <asp:GridView ID="GvOAFileData" runat="server" CssClass="ChildGrid" AutoGenerateColumns="false"
                                                                    OnRowDataBound="GvOAFileData_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                <asp:Label ID="lblfilepath1" runat="server" Visible="false" Text='<%# Eval("File1") %>'></asp:Label>
                                                                                <asp:Label ID="lblfilepath2" runat="server" Visible="false" Text='<%# Eval("File2") %>'></asp:Label>
                                                                                <asp:Label ID="lblfilepath3" runat="server" Visible="false" Text='<%# Eval("File3") %>'></asp:Label>
                                                                                <asp:Label ID="lblfilepath4" runat="server" Visible="false" Text='<%# Eval("File4") %>'></asp:Label>
                                                                                <asp:Label ID="lblfilepath5" runat="server" Visible="false" Text='<%# Eval("File5") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="File1" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButtonfile1" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile1_Click" OnClientClick="aspnetForm.target ='_blank';" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="File2" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButtonfile2" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile2_Click" OnClientClick="SetTarget();" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="File3" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButtonfile3" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile3_Click" OnClientClick="aspnetForm.target ='_blank';" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="File4" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButtonfile4" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile4_Click" OnClientClick="aspnetForm.target ='_blank';" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="File5" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButtonfile5" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile5_Click" OnClientClick="aspnetForm.target ='_blank';" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="20" HeaderText="Select" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%-- <asp:Label ID="lblVWID" runat="server" Text='<%# Eval("VWID") %>' Visible="false"></asp:Label>--%>
                                                            <%--<asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" onclick="CheckSingleCheckbox(this)" />--%>
                                                            <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="false" onclick="CheckedCheckbox(this)" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- New Filed added by Nikhil 09-12-2024 --%>
                                                    <asp:TemplateField HeaderText="Job No" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JobNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- End  --%>

                                                    <asp:TemplateField HeaderText="OA Number" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOANumber" runat="server" Text='<%# Eval("OANumber") %>'></asp:Label>
                                                            <asp:Label ID="lblSubOANumber" runat="server" Text='<%# Eval("SubOA") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Code" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="lblCustName" ReadOnly="true" TextMode="MultiLine" Visible="false" Rows="4" Width="130" CssClass="form-control" Text='<%# Eval("customername") %>'></asp:TextBox>

                                                            <asp:Label ID="lblCustCode" runat="server" Text='<%# Eval("CustomerCode").ToString().Replace(" ", "<br /><br />") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" ItemStyle-Width="400" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemTemplate>
                                                            <%--<asp:TextBox runat="server" ID="txtSize" ReadOnly="true" Width="500" CssClass="form-control" Text='<%# Between(Eval("description").ToString(),"2. ","3. ") %>'></asp:TextBox>--%>
                                                            <asp:TextBox runat="server" ID="txtSize" TextMode="MultiLine" Rows="5" ReadOnly="true" Width="100%" CssClass="form-control" Text='<%# Eval("Size").ToString().Replace("<br>","") %>'></asp:TextBox>
                                                            <%--<asp:Label runat="server" ID="txtSize" Text='<%# Eval("Description") %>'></asp:Label>--%>
                                                            <%--<asp:Label ID="lblSize" runat="server" Text='<%# Eval("description").ToString().Replace("\n", "<br><br><br>") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Quantity" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("TotalQty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deliv Date" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryDt" runat="server" Text='<%# Eval("deliverydatereqbycust","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- Visible removed Visible="false" by Nikhil --%>
                                                    <asp:TemplateField HeaderText="Inward Date and Time" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInwardDtTime" runat="server" Text='<%# Eval("InwardDtTime") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- End --%>

                                                    <asp:TemplateField HeaderText="InQty" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtInwardQty" Width="60" ReadOnly="true" CssClass="form-control" Text='<%# Eval("InwardQty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- Visible removed Visible="false" by Nikhil --%>
                                                    <asp:TemplateField HeaderText="OutDtTime" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <%-- Old Code --%>
                                                            <%--<asp:TextBox ID="txtOutwardDtTime" runat="server" Width="100" ReadOnly="false" CssClass="form-control myDate"></asp:TextBox>--%>
                                                            <%--<asp:Label ID="lblOutwardDtTime" runat="server" Text='<%# Eval("currentdate") %>'></asp:Label>--%>

                                                            <%-- New Code  --%>
                                                            <asp:Label ID="lblOutwardDtTime" runat="server" Text='<%# Eval("OutwardDtTime") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- Visible removed Visible="false" by Nikhil --%>

                                                    <asp:TemplateField HeaderText="OutQty" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtOutwardQty" Width="60" onblur="Keydown(this);" AutoPostBack="false" CssClass="form-control" Text='<%# Eval("InwardQty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="500" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtRemark" Width="130" Height="100" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="150" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnview" runat="server" CommandArgument='<%# Eval("mainID")%>' CommandName="rowDisplay" ToolTip="View Attachment"><i class="fa fa-folder-open" style="font-size: x-large;" aria-hidden="true"></i></asp:LinkButton>

                                                            <a href="<%#string.Format("../Reports/ReportPDF.aspx?Dept="+encrypt("Drawing")+"&OANumberWise={0}", encrypt(Eval("OANumber").ToString())) %>" target="_blank" style="display: none">
                                                                <asp:Image ID="ImgPrint" runat="server" CssClass="img1" ImageUrl="../img/Print.png" Height="29px" ToolTip="Print" /></a>

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
                            <br />
                            <div class="dt-responsive table-responsive" style="padding: 10px;">
                                <div class="divblock">
                                    <asp:LinkButton runat="server" ID="btnClose" OnClick="btnClose_Click" Visible="false" CssClass="btn btn-primary btn-sm"><i class="fa fa-times"></i> CLOSE</asp:LinkButton>
                                </div>
                                <asp:GridView ID="GvOAFileData" runat="server" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                    OnRowDataBound="GvOAFileData_RowDataBound" AllowPaging="true" PageSize="25">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                <asp:Label ID="lblfilepath1" runat="server" Visible="false" Text='<%# Eval("File1") %>'></asp:Label>
                                                <asp:Label ID="lblfilepath2" runat="server" Visible="false" Text='<%# Eval("File2") %>'></asp:Label>
                                                <asp:Label ID="lblfilepath3" runat="server" Visible="false" Text='<%# Eval("File3") %>'></asp:Label>
                                                <asp:Label ID="lblfilepath4" runat="server" Visible="false" Text='<%# Eval("File4") %>'></asp:Label>
                                                <asp:Label ID="lblfilepath5" runat="server" Visible="false" Text='<%# Eval("File5") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="File1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButtonfile1" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile1_Click" OnClientClick="aspnetForm.target ='_blank';" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="File2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButtonfile2" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile2_Click" OnClientClick="SetTarget();" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="File3" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButtonfile3" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile3_Click" OnClientClick="aspnetForm.target ='_blank';" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="File4" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButtonfile4" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile4_Click" OnClientClick="aspnetForm.target ='_blank';" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="File5" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButtonfile5" ImageUrl="../img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile5_Click" OnClientClick="aspnetForm.target ='_blank';" CommandArgument='<%# Eval("OAid") %>' ToolTip="Open File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <center>
                                    <asp:Label ID="lblnodatafoundComp" runat="server" Text="" Visible="false" CssClass="lblboldred"></asp:Label>
                                </center>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade bd-example-modal-lg" id="myModal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content ">
                        <div class="modal-header">
                            <p class="modal-title">OA Details</p>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body" style="background-color: aliceblue;">
                            <div class="row">
                                <div class="col-md-3"><strong>Customer Name:</strong></div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblCustNm" runat="server" Text=""></asp:Label>
                                </div>

                                <div class="col-md-3"><strong>Total Quantity:</strong></div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblTotalQnty" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3"><strong>OA Number:</strong></div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblOAno" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-3"><strong>Size:</strong></div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblDescriptionSize" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3"><strong>Delivery Date:</strong></div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblDeliveryDate" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-3"><strong>Inward Date & Time:</strong></div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblInwardDtandTime" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />

                        </div>
                        <%-- <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                            <i class="fa fa-close" style="font-size:18px"></i></button>
                    </div>--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

