<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesQuotationRptPDF_OldDB.aspx.cs" Inherits="Admin_SalesQuotationRptPDF_OldDB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title runat="server" id="titlename"></title>
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

        .lbl1 {
            font-weight: bold;
            width: 40%;
            /*margin-left:12px;*/
        }

        .lbl {
            font-weight: bold;
            width: 40%;
            margin-left: -96px;
            margin-right: 81px;
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
           <%-- <button class="btn" onclick="printDiv()"><i class="fa fa-print"></i>&nbsp;Print Report or Save as PDF</button>--%>
            <asp:LinkButton runat="server" ID="btnPrint" CssClass="btn btn-primary" OnClick="btnPrint_Click"><i class="fa fa-print"></i>&nbsp;Print Report or Save as PDF</asp:LinkButton>
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
                        <h2 style="font-family: fantasy; color: darkblue;">Sales Quotation</h2>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <table class="nav-justified">
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px; ">Party Name :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblPartyName" CssClass="lbl"></asp:Label></td>
                                <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px; ">Quotation Number :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblQuotationNo" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style18"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px; ">Kindatt :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblKindatt" CssClass="lbl"></asp:Label></td>
                                <td class="auto-style19"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px; ">Quotation Date :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblQuotationDate" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style18"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none; margin-left: 20px; ">Address :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblAddress" CssClass="lbl"></asp:Label></td>
                                <td class="auto-style19">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="row" style="margin: 0 auto; width: 100%; margin-top: 20px;">
                            <asp:GridView ID="dgvSalesQuatationRpt" runat="server" CssClass="table table-striped table-bordered nowrap Grid"
                                AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" ShowFooter="true" OnRowDataBound="OnDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle CssClass="thick" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Particulars" ItemStyle-Width="700" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameOfParticulars" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HSN" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHSN" runat="server" Text='<%# Eval("hsncode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblRate" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                             <asp:Label ID="txtRate" runat="server" Text='<%# Eval("Currency").ToString()+" "+ Eval("rate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disc%" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("discount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
											 <asp:Label ID="lblcurrency" runat="server" Text='<%# Eval("Currency") %>'></asp:Label>
                                            <asp:Label ID="txtAmount" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Red" HorizontalAlign="Right" />
                            </asp:GridView>
                            <div style="text-align: center; margin-right: 1%;">
                                <div class="col-md-8">
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label1" runat="server">Total Amount with GST TAX :</asp:Label>
                                </div>
                                <div class="col-md-1">
                                    <span runat="server" id="lblWithGSTAmount"></span>
                                </div>
                            </div>
                        </div>
                        <hr />
                    </td>

                </tr>

                <tr>
                    <td colspan="2">
                        <table class="nav-justified">
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">Remarks :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblRemark" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">Payment Term :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblPaymentTerm" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">CGST :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblCGST" CssClass="lbl1"></asp:Label>
                                    | Amount <asp:Label runat="server" ID="lblCGSTamt" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">SGST :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblSGST" CssClass="lbl1"></asp:Label>
                                    | Amount <asp:Label runat="server" ID="lblSGSTamt" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">IGST :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblIGST" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">VALIDITY OF OFFER :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblValidityOfOffer" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">DELIVERY PERIOD :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblDeliveryPeriod" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">TRANSPORTATION :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblTransportation" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">STANDARD PACKING :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblStandardpacking" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">SPECIAL PACKING :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblSpecialpacking" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">INSPECTION :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblInspection" CssClass="lbl1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style20"><span style="color: rgb(51, 51, 51); font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important;  margin-left: 20px;">NOTE :</span></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNote" CssClass="lbl1"></asp:Label></td>
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
