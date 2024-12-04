<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportPDF.aspx.cs" Inherits="Admin_ReportPDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            border: 2px solid;
            padding: 20px;
        }

        .auto-style3 {
            width: 190px;
        }

        .auto-style15 {
            width: 566px;
        }

        .lblclass {
            font-weight: bold;
        }

        .trHegight {
            /*height: 50px;*/
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
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
    </style>

    <script>
        function printDiv() {
            var divContents = document.getElementById("GFG").innerHTML;
            var a = window.open('', '', 'height=500, width=500');
            a.document.write('<html>');
            a.document.write('<body style="border: 2px solid;width:1000px;"> <br>');
            a.document.write(divContents);
            a.document.write('</body></html>');
            a.document.close();
            a.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <button class="btn" onclick="printDiv();"><i class="fa fa-print"></i> Print Report or Save as PDF</button>
        <br />
         <br />
        <div id="GFG">
            <table class="auto-style1">

                <tr>
                    <%--<td rowspan="3" style="text-align: center; width: 103px;">
                        <img src="../img/ExcelEncLogo.png" /></td>--%>
                    <td colspan="5" style="text-align: center;">
                        <h1>Excel Enclosures</h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="text-align: center; border: 1px solid">
                        <asp:Label runat="server" Font-Bold="true" ID="spnDate"></asp:Label>
                    </td>
                    <td colspan="3" style="text-align: center; border: 1px solid">
                        <h2 style="color: blue; font-family: system-ui;">
                            <asp:Label runat="server" Font-Bold="true" ID="lblDeprtment"></asp:Label></h2>
                    </td>
                    <td colspan="1" style="text-align: center; border: 1px solid">
                        <asp:Label runat="server" Font-Bold="true" ID="spnTime"></asp:Label>
                    </td>
                </tr>

                <%-- <tr class="trHegight" style="height: 50px;">
                    <td>&nbsp; Customer Name:</td>
                    <td class="auto-style3">
                        <asp:Label ID="lblcustomerName" CssClass="lblclass" runat="server"></asp:Label></td>
                    <td class="auto-style15" style="text-align: center; font-weight: bold; border-right: 2px solid;" rowspan="4">&nbsp;</td>
                    <td>&nbsp; Delivery Date:</td>
                    <td class="auto-style3">
                        <asp:Label ID="lbldeliverydate" CssClass="lblclass" runat="server"></asp:Label></td>
                </tr>
                <tr class="trHegight" style="height: 50px;">
                    <td>&nbsp; Quotation No:</td>
                    <td class="auto-style3">
                        <asp:Label ID="lblQuotationNo" CssClass="lblclass" runat="server"></asp:Label></td>
                    <td>&nbsp; Total Quantity:</td>
                    <td class="auto-style3">
                        <asp:Label ID="lblTotalQuantity" CssClass="lblclass" runat="server"></asp:Label></td>
                </tr>
                <tr class="trHegight" style="height: 50px;">
                    <td>&nbsp; Quotation Date:</td>
                    <td class="auto-style3">
                        <asp:Label ID="lblQuotationDate" CssClass="lblclass" runat="server"></asp:Label></td>
                    <td>&nbsp; PO No:</td>
                    <td class="auto-style3">
                        <asp:Label ID="lblPONo" CssClass="lblclass" runat="server"></asp:Label></td>
                </tr>
                <tr class="trHegight" style="height: 50px;">
                    <td></td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp; PO Date:</td>
                    <td class="auto-style3">
                        <asp:Label ID="lblPODate" CssClass="lblclass" runat="server"></asp:Label></td>
                </tr>--%>

                <tr>
                    <td colspan="5">
                        <asp:GridView ID="dgvDeptWiseRpt" runat="server" CssClass="table table-striped table-bordered nowrap"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OA Number" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOANumber" runat="server" Text='<%# Eval("OANumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" ItemStyle-Width="600" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemTemplate>
                                         <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTotQty" runat="server" Text='<%# Eval("TotalQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inward Date and Time" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInwardDtTime" runat="server" Text='<%# Eval("InwardDtTime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inward Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtInwardQty" runat="server" Text='<%# Eval("TotalQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Outward Date and Time" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOutwardDtTime" runat="server" Text='<%# Eval("OutwardDtTime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Outward Quantity" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtOutwardQty" runat="server" Text='<%# Eval("OutwardQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: end">For Excel Enclosures</td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td rowspan="2" style="text-align: end">Authorised Signature</td>
                </tr>
            </table>

        </div>

    </form>
</body>
</html>
