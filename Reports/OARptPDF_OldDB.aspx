<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OARptPDF_OldDB.aspx.cs" Inherits="Admin_OARptPDF_OldDB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Acceptance Report</title>
    <style type="text/css">
        .lblclass {
            font-weight: bold;
        }

        .trHegight {
            /*height: 50px;*/
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style>
        .btn {
            background-color: DodgerBlue;
            border: none;
            color: white;
            padding: 12px 16px;
            font-size: 16px;
            cursor: pointer;
        }

            /* Darker background on mouse-over */
            .btn:hover {
                background-color: RoyalBlue;
            }

        .auto-style16 {
            width: 100%;
            border-style: solid;
            border-width: 2px;
        }

        .auto-style17 {
            width: 190px;
        }

        td {
            /*text-align: center;*/
            /*border: 1px solid;*/
        }
        th {
            text-align: center!important;
        }

        .lbl {
            font-weight: bold;
            width: 40%;
        }

        .ss {
            text-align: left;
        }

        .auto-style18 {
            width: 235px;
        }

        @media print {
            /*assing myPagesClass to every div you want to print on single separate A4 page*/

            body .myPagesClass {
                z-index: 100 !important;
                visibility: visible !important;
                position: relative !important;
                display: block !important;
                background-color: lightgray !important;
                height: 297mm !important;
                width: 211mm !important;
                position: relative !important;
                padding: 0px;
                top: 0 !important;
                left: 0 !important;
                margin: 0 !important;
                orphans: 0 !important;
                widows: 0 !important;
                overflow: visible !important;
                page-break-after: always;
            }

            @page {
                size: A3;
                margin: 0mm;
                orphans: 0 !important;
                widows: 0 !important;
            }
        }

        .auto-style19 {
            width: 287px
        }

        .auto-style20 {
            width: 225px;
        }

        .auto-style201 {
            width: 300px;
        }

        .auto-style202 {
            width: 300px;
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.8.1/html2pdf.bundle.min.js"></script>
    <script>
        function printDiv() {
            var printContents = document.getElementById('DivSaleQuatation').innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <div style="margin-top: 20px; margin-left: 148px;">
            <button class="btn" onclick="printDiv()"><i class="fa fa-print"></i>&nbsp;Print Report or Save as PDF</button>
        </div>
        <div id="DivSaleQuatation">

            <table class="auto-style16" style="width: 80%; margin: 0 auto; margin-top: 20px; margin-bottom: 20px; font-weight: bold;">
                <tr>
                    <td class="auto-style17" style="border: 1px solid; text-align: center; padding: 10px;">
                        <img src="../img/ExcelEncLogo.png" /></td>
                    <td style="border: 1px solid;">
                        <h1 style="text-align: center; font-family: fantasy;">Excel Enclosures</h1>
                        <p style="text-align: center">Gat No. 1567, Shelar Vasti, Dehu-Alandi Road, Chikhali, Pune - 411062</p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; border: 1px solid;">GSTIN : 27ATFPS1959J1Z4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PAN NO : ATFPS1959J&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; EMAIL : <a href="mailto:mktg@excelenclosures.com">mktg@excelenclosures.com</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CONTACT : 9225658662</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; border: 1px solid;">
                        <h3 style="font-family: fantasy; color: darkblue;">Order Acceptance Report</h3>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <table class="nav-justified" style="margin-top: 20px;">
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">OA No :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblOANumber" CssClass="lbl"></asp:Label></td>
                                <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Date :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDate" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style18"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Customer Name :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblCustName" CssClass="lbl"></asp:Label></td>
                                <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Quotation No :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblQuotationNo" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style18"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Address :</span></td>
                                <td class="auto-style18">
                                    <asp:Label runat="server" ID="lblAddress" CssClass="lbl"></asp:Label></td>
                                <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Date :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblQuotationDate" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style18"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Contact Person Purchase :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblContPerson" CssClass="lbl"></asp:Label></td>
                                <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">PO No :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblPONo" CssClass="lbl"></asp:Label></td>
                            </tr>

                            <tr>
                                <td class="auto-style18"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Contact No :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblContactNo" CssClass="lbl"></asp:Label></td>
                                <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">PO Date :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblPODate" CssClass="lbl"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <div class="row" style="margin: 0 auto; width: 100%; margin-top: 20px;">
                            <asp:GridView ID="dgvOARpt" runat="server" CssClass="table table-striped table-bordered nowrap Grid"
                                AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" ShowFooter="true" OnRowDataBound="OnDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle CssClass="thick" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Particulars" ItemStyle-Width="700" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameOfParticulars" runat="server" Text='<%# Eval("Description").ToString().Replace("\n", "<br />") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="txtDiscount" runat="server" Text='<%# Eval("Discount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
            
                                            <asp:Label ID="txtAmount" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>

                <%-- Special notes--%>
                <tr>
                    <td colspan="2">
                        <table class="nav-justified">
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Special Note :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote1" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote2" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote3" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote4" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote5" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote6" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote7" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote8" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote9" CssClass="lbl"></asp:Label></td>
                            </tr>
                        </table>
                        <hr style="border-top: 1px solid" />
                    </td>
                </tr>
                <%-- End Special notes--%>

                <tr>
                    <td colspan="2">
                        <table class="nav-justified" style="margin-top: 10px;">
                            <tr>
                                <td class="auto-style201"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Delivery Date Required by customer :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lbldeliverydatebycust" CssClass="lbl"></asp:Label></td>
                              <%--  <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Buyer :</span></td>
                                <td></td>--%>
                            </tr>
                            <tr>
                                <td class="auto-style18"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Delivery Date Committed by us :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lbldeliverydatebyus" CssClass="lbl"></asp:Label></td>
                               <%-- <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Consignee :</span></td>
                                <td>
                                   <asp:Label runat="server" ID="lblconsignee" CssClass="lbl"></asp:Label></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>

                 <%-- GST--%>
                <tr>
                    <td colspan="2">
                        <table class="nav-justified" style="margin-top:20px;">
                            <tr>
                                <td class="auto-style202"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">GST :</span></td>
                                <td>
                                    CGST(<asp:Label runat="server" ID="lblCGSTPer" CssClass="lbl"></asp:Label> %)&nbsp; <%--Amount: <asp:Label runat="server" ID="lblCGSTAmt" CssClass="lbl"></asp:Label>--%></td>
                            </tr>
                            <tr>
                                <td class="auto-style202"></td>
                                <td>
                                    SGST(<asp:Label runat="server" ID="lblSGSTPer" CssClass="lbl"></asp:Label> %)&nbsp; <%--Amount: <asp:Label runat="server" ID="lblSGSTAmt" CssClass="lbl"></asp:Label>--%></td>
                            </tr>
                            <tr>
                                <td class="auto-style202"></td>
                               <td>
                                    IGST(<asp:Label runat="server" ID="lblIGSTPer" CssClass="lbl"></asp:Label> %)&nbsp; <%--Amount: <asp:Label runat="server" ID="lblIGSTAmt" CssClass="lbl"></asp:Label>--%></td>
                            </tr>
                           
                        </table>
                        <hr style="border-top: 1px solid" />
                    </td>
                </tr>
                <%-- End GST--%>

                <tr>
                    <td colspan="2">
                        <table class="nav-justified">
                            <tr>
                                <td class="auto-style202"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Packing :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblpacking" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style202"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Delivery/Transportation Charges :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lbldeliverytransportcharge" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style202"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Terms of payment :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lbltermaofpayment" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style202"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Billing details :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblbillingdetails" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style202"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px;">Any Special instruction / Note :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblSpecialNote" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr style="border-top: 1px solid" />
                                    <div class="row" style="text-align: end; margin-top: 20px; margin-right: 10px;">
                                        <div class="col-md-4"></div>
                                        <div class="col-md-4"></div>
                                        <div class="col-md-4">
                                            <asp:Label runat="server">For Excel Enclosures</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="text-align: end; margin-right: 10px;">
                                        <div class="col-md-4"></div>
                                        <div class="col-md-4"></div>
                                        <div class="col-md-4" style="margin-top: 80px;">
                                            <asp:Label runat="server">Authorised Signature</asp:Label>
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
