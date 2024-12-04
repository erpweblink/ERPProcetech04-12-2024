<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Admin_Invoice" %>

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

        .reqcls {
            color: red;
            font-weight: 600;
            font-size: 14px;
        }

        .form-control {
            border: 1px solid gray !important;
            height: 31px;
        }

        th {
            text-align: center;
        }

        ::-webkit-scrollbar {
            width: 0px; /* Remove scrollbar space */
            background: transparent; /* Optional: just make scrollbar invisible */
        }

        .card {
            margin-bottom: 0px !important;
        }

        .readonlytxt {
            background-color: #f1f5f7;
        }

        .card .card-header span {
            color: #0a0003 !important;
            font-size: 15px !important;
            font-weight: 800 !important;
        }

        .background1 {
            position: absolute;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
            z-index: -1;
            overflow: hidden;
        }
    </style>

    <%--  <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,600" rel="stylesheet" />
    <link href="../files/bower_components/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../files/assets/icon/feather/css/feather.css" rel="stylesheet" />
    <link href="../files/assets/css/style.css" rel="stylesheet" />
    <link href="../files/assets/css/jquery.mCustomScrollbar.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-wrapper">
        <div class="page-body">
            <div class="container py-3">
                <div class="card">
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card-header">
                                <div class="row">
                                    <div class="col-md-12" style="border: 2px solid #000;">
                                        <div class="row">
                                            <div class="col-md-12" style="background-color: #bcd6ee; border: 2px solid #000;">
                                                <span style="color: #000; font-size: 22px; font-weight: 900; text-align: center; margin-top: 0px;">Tax Invoice</span>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-2 spancls">Invoice No<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtname" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Name"
                                                    ControlToValidate="txtname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">Invoice Date<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtemail" TextMode="Date" placeholder="DD-MM-YYYY" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Email"
                                                    ControlToValidate="txtemail" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Reverse Charge(Y/N)<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Name"
                                                    ControlToValidate="txtname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">State<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Width="100%" Text="Maharashtra"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Email"
                                                    ControlToValidate="txtemail" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12" style="background-color: #bcd6ee; border: 2px solid #000;">
                                                <span style="color: #000; font-size: 15px; font-weight: 900; text-align: center; margin-top: 0px;">Bill to Party</span>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-2 spancls">Company Name<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Please Enter Name"
                                                    ControlToValidate="txtname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">Address<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox4" TextMode="MultiLine" Height="50px" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Please Enter Email"
                                                    ControlToValidate="txtemail" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-2 spancls">GSTIN<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Please Enter Name"
                                                    ControlToValidate="txtname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">State<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlpartystate" CssClass="form-control" runat="server" Width="100%" Height="34px">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Value="Andhra Pradesh">Andhra Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="Andaman and Nicobar Islands">Andaman and Nicobar Islands</asp:ListItem>
                                                    <asp:ListItem Value="Arunachal Pradesh">Arunachal Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="Assam">Assam</asp:ListItem>
                                                    <asp:ListItem Value="Bihar">Bihar</asp:ListItem>
                                                    <asp:ListItem Value="Chandigarh">Chandigarh</asp:ListItem>
                                                    <asp:ListItem Value="Chhattisgarh">Chhattisgarh</asp:ListItem>
                                                    <asp:ListItem Value="Dadar and Nagar Haveli">Dadar and Nagar Haveli</asp:ListItem>
                                                    <asp:ListItem Value="Daman and Diu">Daman and Diu</asp:ListItem>
                                                    <asp:ListItem Value="Delhi">Delhi</asp:ListItem>
                                                    <asp:ListItem Value="Lakshadweep">Lakshadweep</asp:ListItem>
                                                    <asp:ListItem Value="Puducherry">Puducherry</asp:ListItem>
                                                    <asp:ListItem Value="Goa">Goa</asp:ListItem>
                                                    <asp:ListItem Value="Gujarat">Gujarat</asp:ListItem>
                                                    <asp:ListItem Value="Haryana">Haryana</asp:ListItem>
                                                    <asp:ListItem Value="Himachal Pradesh">Himachal Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="Jammu and Kashmir">Jammu and Kashmir</asp:ListItem>
                                                    <asp:ListItem Value="Jharkhand">Jharkhand</asp:ListItem>
                                                    <asp:ListItem Value="Karnataka">Karnataka</asp:ListItem>
                                                    <asp:ListItem Value="Kerala">Kerala</asp:ListItem>
                                                    <asp:ListItem Value="Madhya Pradesh">Madhya Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="Maharashtra" Selected="True">Maharashtra</asp:ListItem>
                                                    <asp:ListItem Value="Manipur">Manipur</asp:ListItem>
                                                    <asp:ListItem Value="Meghalaya">Meghalaya</asp:ListItem>
                                                    <asp:ListItem Value="Mizoram">Mizoram</asp:ListItem>
                                                    <asp:ListItem Value="Nagaland">Nagaland</asp:ListItem>
                                                    <asp:ListItem Value="Odisha">Odisha</asp:ListItem>
                                                    <asp:ListItem Value="Punjab">Punjab</asp:ListItem>
                                                    <asp:ListItem Value="Rajasthan">Rajasthan</asp:ListItem>
                                                    <asp:ListItem Value="Sikkim">Sikkim</asp:ListItem>
                                                    <asp:ListItem Value="Tamil Nadu">Tamil Nadu</asp:ListItem>
                                                    <asp:ListItem Value="Telangana">Telangana</asp:ListItem>
                                                    <asp:ListItem Value="Tripura">Tripura</asp:ListItem>
                                                    <asp:ListItem Value="Uttar Pradesh">Uttar Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="Uttarakhand">Uttarakhand</asp:ListItem>
                                                    <asp:ListItem Value="West Bengal">West Bengal</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter Email"
                                                    ControlToValidate="txtemail" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="dt-responsive table-responsive">
                                                    <table border="1" style="width: 100%; text-align: center !important;">
                                                        <tr style="background-color: #bcd6ee;">
                                                            <th style='border-bottom: none; text-align: center !important;'>SN.</th>
                                                            <th style='border-bottom: none; text-align: center !important;'>Product Description</th>
                                                            <th style='border-bottom: none; text-align: center !important;'>SAC code</th>
                                                            <th style='border-bottom: none; text-align: center !important;'>Qty</th>
                                                            <th style='border-bottom: none;'>Rate</th>
                                                            <th style='border-bottom: none; text-align: center !important;'>Taxable Value</th>
                                                            <th style='border-bottom: 1px solid gray !important; text-align: center !important;'>CGST</th>
                                                            <th style='border-bottom: 1px solid gray !important; text-align: center !important;'>SGST</th>
                                                            <th style='border-bottom: 1px solid gray !important; text-align: center !important;'>IGST</th>
                                                            <th style='border-bottom: none; text-align: center !important;'>Total</th>
                                                        </tr>

                                                        <tr style="background-color: #bcd6ee;">
                                                            <td style='border-top: none;'></td>
                                                            <td style='border-top: none;'></td>
                                                            <td style='border-top: none;'></td>
                                                            <td style='border-top: none;'></td>
                                                            <td style='border-top: none;'></td>
                                                            <td style='border-top: none;'></td>
                                                            <td style='border: none;'>
                                                                <table border="0" style="width: 100%; font-size: 12px; border-right: 1px solid gray !important;">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray !important; width: 33px !important;'>%</td>
                                                                        <td style='border-right: 1px solid gray !important;'>Amount</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style='border: none;'>
                                                                <table border="0" style="width: 100%; font-size: 12px;">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray !important; width: 33px !important;'>%</td>
                                                                        <td style='border-right: 1px solid gray !important;'>Amount</td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border: none;'>
                                                                <table border="0" style="width: 100%; font-size: 12px;">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray !important; width: 33px !important;'>%</td>
                                                                        <td>Amount</td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'></td>
                                                        </tr>

                                                        <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
                                                        <script type="text/javascript">
                                                            function sum() {
                                                                //1st Row Calculation 
                                                                var txtqty1 = document.getElementById('txtqty1').value;
                                                                var txtrate1 = document.getElementById('txtrate1').value;
                                                                var txttaxable1 = document.getElementById('txttaxable1').value;
                                                                var txtcgstper1 = document.getElementById('txtcgstper1').value;
                                                                var txtcgstamt1 = document.getElementById('txtcgstamt1').value;
                                                                var txtsgstper1 = document.getElementById('txtsgstper1').value;
                                                                var txtsgstamt1 = document.getElementById('txtsgstamt1').value;
                                                                var txtigstper1 = document.getElementById('txtigstper1').value;
                                                                var txtigstamt1 = document.getElementById('txtigstamt1').value;
                                                                var txttaxable1 = document.getElementById('txttaxable1').value;

                                                                if (isNaN(txtqty1) || txtqty1 == "") { txtqty1 = 0; }
                                                                if (isNaN(txtrate1) || txtrate1 == "") { txtrate1 = 0; }
                                                                if (isNaN(txttaxable1) || txttaxable1 == "") { txttaxable1 = 0; }
                                                                if (isNaN(txtcgstper1) || txtcgstper1 == "") { txtcgstper1 = 0; }
                                                                if (isNaN(txtcgstamt1) || txtcgstamt1 == "") { txtcgstamt1 = 0; }
                                                                if (isNaN(txtsgstper1) || txtsgstper1 == "") { txtsgstper1 = 0; }
                                                                if (isNaN(txtsgstamt1) || txtsgstamt1 == "") { txtsgstamt1 = 0; }
                                                                if (isNaN(txtigstper1) || txtigstper1 == "") { txtigstper1 = 0; }
                                                                if (isNaN(txtigstamt1) || txtigstamt1 == "") { txtigstamt1 = 0; }

                                                                var result1 = parseInt(txtqty1) * parseInt(txtrate1);
                                                                if (!isNaN(result1)) { txttaxable1 = result1; document.getElementById('txttaxable1').value = result1.toFixed(2); }

                                                                var cgstAmt1 = (result1 * txtcgstper1) / 100;
                                                                if (!isNaN(cgstAmt1)) { document.getElementById('txtcgstamt1').value = cgstAmt1.toFixed(2); }

                                                                var sgstAmt1 = (result1 * txtsgstper1) / 100;
                                                                if (!isNaN(sgstAmt1)) { document.getElementById('txtsgstamt1').value = sgstAmt1.toFixed(2); }

                                                                var igstAmt1 = (result1 * txtigstper1) / 100;
                                                                if (!isNaN(igstAmt1)) { document.getElementById('txtigstamt1').value = igstAmt1.toFixed(2); }

                                                                var TotalAfterTax1 = result1 + cgstAmt1 + sgstAmt1 + igstAmt1;
                                                                if (!isNaN(TotalAfterTax1)) { document.getElementById('txttotal1').value = TotalAfterTax1.toFixed(2); }


                                                                //2nd Row Calculation 
                                                                var txtqty2 = document.getElementById('txtqty2').value;
                                                                var txtrate2 = document.getElementById('txtrate2').value;
                                                                var txttaxable2 = document.getElementById('txttaxable2').value;
                                                                var txtcgstper2 = document.getElementById('txtcgstper2').value;
                                                                var txtcgstamt2 = document.getElementById('txtcgstamt2').value;
                                                                var txtsgstper2 = document.getElementById('txtsgstper2').value;
                                                                var txtsgstamt2 = document.getElementById('txtsgstamt2').value;
                                                                var txtigstper2 = document.getElementById('txtigstper2').value;
                                                                var txtigstamt2 = document.getElementById('txtigstamt2').value;
                                                                var txttaxable2 = document.getElementById('txttaxable2').value;
                                                                if (isNaN(txtqty2) || txtqty2 == "") { txtqty2 = 0; }
                                                                if (isNaN(txtrate2) || txtrate2 == "") { txtrate2 = 0; }
                                                                if (isNaN(txttaxable2) || txttaxable2 == "") { txttaxable2 = 0; }
                                                                if (isNaN(txtcgstper2) || txtcgstper2 == "") { txtcgstper2 = 0; }
                                                                if (isNaN(txtcgstamt2) || txtcgstamt2 == "") { txtcgstamt2 = 0; }
                                                                if (isNaN(txtsgstper2) || txtsgstper2 == "") { txtsgstper2 = 0; }
                                                                if (isNaN(txtsgstamt2) || txtsgstamt2 == "") { txtsgstamt2 = 0; }
                                                                if (isNaN(txtigstper2) || txtigstper2 == "") { txtigstper2 = 0; }
                                                                if (isNaN(txtigstamt2) || txtigstamt2 == "") { txtigstamt2 = 0; }

                                                                var result2 = parseInt(txtqty2) * parseInt(txtrate2);
                                                                if (!isNaN(result2)) { txttaxable2 = result2; document.getElementById('txttaxable2').value = result2.toFixed(2); }

                                                                var cgstAmt2 = (result2 * txtcgstper2) / 100;
                                                                if (!isNaN(cgstAmt2)) { document.getElementById('txtcgstamt2').value = cgstAmt2.toFixed(2); }

                                                                var sgstAmt2 = (result2 * txtsgstper2) / 100;
                                                                if (!isNaN(sgstAmt2)) { document.getElementById('txtsgstamt2').value = sgstAmt2.toFixed(2); }

                                                                var igstAmt2 = (result2 * txtigstper2) / 100;
                                                                if (!isNaN(igstAmt2)) { document.getElementById('txtigstamt2').value = igstAmt2.toFixed(2); }

                                                                var TotalAfterTax2 = result2 + cgstAmt2 + sgstAmt2 + igstAmt2;
                                                                if (!isNaN(TotalAfterTax2)) { document.getElementById('txttotal2').value = TotalAfterTax2.toFixed(2); }

                                                                //3rd Row Calculation 
                                                                var txtqty3 = document.getElementById('txtqty3').value;
                                                                var txtrate3 = document.getElementById('txtrate3').value;
                                                                var txttaxable3 = document.getElementById('txttaxable3').value;
                                                                var txtcgstper3 = document.getElementById('txtcgstper3').value;
                                                                var txtcgstamt3 = document.getElementById('txtcgstamt3').value;
                                                                var txtsgstper3 = document.getElementById('txtsgstper3').value;
                                                                var txtsgstamt3 = document.getElementById('txtsgstamt3').value;
                                                                var txtigstper3 = document.getElementById('txtigstper3').value;
                                                                var txtigstamt3 = document.getElementById('txtigstamt3').value;
                                                                var txttaxable3 = document.getElementById('txttaxable3').value;
                                                                if (isNaN(txtqty3) || txtqty3 == "") { txtqty3 = 0; }
                                                                if (isNaN(txtrate3) || txtrate3 == "") { txtrate3 = 0; }
                                                                if (isNaN(txttaxable3) || txttaxable3 == "") { txttaxable3 = 0; }
                                                                if (isNaN(txtcgstper3) || txtcgstper3 == "") { txtcgstper3 = 0; }
                                                                if (isNaN(txtcgstamt3) || txtcgstamt3 == "") { txtcgstamt3 = 0; }
                                                                if (isNaN(txtsgstper3) || txtsgstper3 == "") { txtsgstper3 = 0; }
                                                                if (isNaN(txtsgstamt3) || txtsgstamt3 == "") { txtsgstamt3 = 0; }
                                                                if (isNaN(txtigstper3) || txtigstper3 == "") { txtigstper3 = 0; }
                                                                if (isNaN(txtigstamt3) || txtigstamt3 == "") { txtigstamt3 = 0; }

                                                                var result3 = parseInt(txtqty3) * parseInt(txtrate3);
                                                                if (!isNaN(result3)) { txttaxable3 = result3; document.getElementById('txttaxable3').value = result3.toFixed(2); }

                                                                var cgstAmt3 = (result3 * txtcgstper3) / 100;
                                                                if (!isNaN(cgstAmt3)) { document.getElementById('txtcgstamt3').value = cgstAmt3.toFixed(2); }

                                                                var sgstAmt3 = (result3 * txtsgstper3) / 100;
                                                                if (!isNaN(sgstAmt3)) { document.getElementById('txtsgstamt3').value = sgstAmt3.toFixed(2); }

                                                                var igstAmt3 = (result3 * txtigstper3) / 100;
                                                                if (!isNaN(igstAmt3)) { document.getElementById('txtigstamt3').value = igstAmt3.toFixed(2); }

                                                                var TotalAfterTax3 = result3 + cgstAmt3 + sgstAmt3 + igstAmt3;
                                                                if (!isNaN(TotalAfterTax3)) { document.getElementById('txttotal3').value = TotalAfterTax3.toFixed(2); }



                                                                //4th Row Calculation 
                                                                var txtqty4 = document.getElementById('txtqty4').value;
                                                                var txtrate4 = document.getElementById('txtrate4').value;
                                                                var txttaxable4 = document.getElementById('txttaxable4').value;
                                                                var txtcgstper4 = document.getElementById('txtcgstper4').value;
                                                                var txtcgstamt4 = document.getElementById('txtcgstamt4').value;
                                                                var txtsgstper4 = document.getElementById('txtsgstper4').value;
                                                                var txtsgstamt4 = document.getElementById('txtsgstamt4').value;
                                                                var txtigstper4 = document.getElementById('txtigstper4').value;
                                                                var txtigstamt4 = document.getElementById('txtigstamt4').value;
                                                                var txttaxable4 = document.getElementById('txttaxable4').value;
                                                                if (isNaN(txtqty4) || txtqty4 == "") { txtqty4 = 0; }
                                                                if (isNaN(txtrate4) || txtrate4 == "") { txtrate4 = 0; }
                                                                if (isNaN(txttaxable4) || txttaxable4 == "") { txttaxable4 = 0; }
                                                                if (isNaN(txtcgstper4) || txtcgstper4 == "") { txtcgstper4 = 0; }
                                                                if (isNaN(txtcgstamt4) || txtcgstamt4 == "") { txtcgstamt4 = 0; }
                                                                if (isNaN(txtsgstper4) || txtsgstper4 == "") { txtsgstper4 = 0; }
                                                                if (isNaN(txtsgstamt4) || txtsgstamt4 == "") { txtsgstamt4 = 0; }
                                                                if (isNaN(txtigstper4) || txtigstper4 == "") { txtigstper4 = 0; }
                                                                if (isNaN(txtigstamt4) || txtigstamt4 == "") { txtigstamt4 = 0; }

                                                                var result4 = parseInt(txtqty4) * parseInt(txtrate4);
                                                                if (!isNaN(result4)) { txttaxable4 = result4; document.getElementById('txttaxable4').value = result4.toFixed(2); }

                                                                var cgstAmt4 = (result4 * txtcgstper4) / 100;
                                                                if (!isNaN(cgstAmt4)) { document.getElementById('txtcgstamt4').value = cgstAmt4.toFixed(2); }

                                                                var sgstAmt4 = (result4 * txtsgstper4) / 100;
                                                                if (!isNaN(sgstAmt4)) { document.getElementById('txtsgstamt4').value = sgstAmt4.toFixed(2); }

                                                                var igstAmt4 = (result4 * txtigstper4) / 100;
                                                                if (!isNaN(igstAmt4)) { document.getElementById('txtigstamt4').value = igstAmt4.toFixed(2); }

                                                                var TotalAfterTax4 = result4 + cgstAmt4 + sgstAmt4 + igstAmt4;
                                                                if (!isNaN(TotalAfterTax4)) { document.getElementById('txttotal4').value = TotalAfterTax4.toFixed(2); }


                                                                //5th Row Calculation 
                                                                var txtqty5 = document.getElementById('txtqty5').value;
                                                                var txtrate5 = document.getElementById('txtrate5').value;
                                                                var txttaxable5 = document.getElementById('txttaxable5').value;
                                                                var txtcgstper5 = document.getElementById('txtcgstper5').value;
                                                                var txtcgstamt5 = document.getElementById('txtcgstamt5').value;
                                                                var txtsgstper5 = document.getElementById('txtsgstper5').value;
                                                                var txtsgstamt5 = document.getElementById('txtsgstamt5').value;
                                                                var txtigstper5 = document.getElementById('txtigstper5').value;
                                                                var txtigstamt5 = document.getElementById('txtigstamt5').value;
                                                                var txttaxable5 = document.getElementById('txttaxable5').value;
                                                                if (isNaN(txtqty5) || txtqty5 == "") { txtqty5 = 0; }
                                                                if (isNaN(txtrate5) || txtrate5 == "") { txtrate5 = 0; }
                                                                if (isNaN(txttaxable5) || txttaxable5 == "") { txttaxable5 = 0; }
                                                                if (isNaN(txtcgstper5) || txtcgstper5 == "") { txtcgstper5 = 0; }
                                                                if (isNaN(txtcgstamt5) || txtcgstamt5 == "") { txtcgstamt5 = 0; }
                                                                if (isNaN(txtsgstper5) || txtsgstper5 == "") { txtsgstper5 = 0; }
                                                                if (isNaN(txtsgstamt5) || txtsgstamt5 == "") { txtsgstamt5 = 0; }
                                                                if (isNaN(txtigstper5) || txtigstper5 == "") { txtigstper5 = 0; }
                                                                if (isNaN(txtigstamt5) || txtigstamt5 == "") { txtigstamt5 = 0; }

                                                                var result5 = parseInt(txtqty5) * parseInt(txtrate5);
                                                                if (!isNaN(result5)) { txttaxable5 = result5; document.getElementById('txttaxable5').value = result5.toFixed(2); }

                                                                var cgstAmt5 = (result5 * txtcgstper5) / 100;
                                                                if (!isNaN(cgstAmt5)) { document.getElementById('txtcgstamt5').value = cgstAmt5.toFixed(2); }

                                                                var sgstAmt5 = (result5 * txtsgstper5) / 100;
                                                                if (!isNaN(sgstAmt5)) { document.getElementById('txtsgstamt5').value = sgstAmt5.toFixed(2); }

                                                                var igstAmt5 = (result5 * txtigstper5) / 100;
                                                                if (!isNaN(igstAmt5)) { document.getElementById('txtigstamt5').value = igstAmt5.toFixed(2); }

                                                                var TotalAfterTax5 = result5 + cgstAmt5 + sgstAmt5 + igstAmt5;
                                                                if (!isNaN(TotalAfterTax5)) { document.getElementById('txttotal5').value = TotalAfterTax5.toFixed(2); }



                                                                //6th Row Calculation 
                                                                var txtqty6 = document.getElementById('txtqty6').value;
                                                                var txtrate6 = document.getElementById('txtrate6').value;
                                                                var txttaxable6 = document.getElementById('txttaxable6').value;
                                                                var txtcgstper6 = document.getElementById('txtcgstper6').value;
                                                                var txtcgstamt6 = document.getElementById('txtcgstamt6').value;
                                                                var txtsgstper6 = document.getElementById('txtsgstper6').value;
                                                                var txtsgstamt6 = document.getElementById('txtsgstamt6').value;
                                                                var txtigstper6 = document.getElementById('txtigstper6').value;
                                                                var txtigstamt6 = document.getElementById('txtigstamt6').value;
                                                                var txttaxable6 = document.getElementById('txttaxable6').value;
                                                                if (isNaN(txtqty6) || txtqty6 == "") { txtqty6 = 0; }
                                                                if (isNaN(txtrate6) || txtrate6 == "") { txtrate6 = 0; }
                                                                if (isNaN(txttaxable6) || txttaxable6 == "") { txttaxable6 = 0; }
                                                                if (isNaN(txtcgstper6) || txtcgstper6 == "") { txtcgstper6 = 0; }
                                                                if (isNaN(txtcgstamt6) || txtcgstamt6 == "") { txtcgstamt6 = 0; }
                                                                if (isNaN(txtsgstper6) || txtsgstper6 == "") { txtsgstper6 = 0; }
                                                                if (isNaN(txtsgstamt6) || txtsgstamt6 == "") { txtsgstamt6 = 0; }
                                                                if (isNaN(txtigstper6) || txtigstper6 == "") { txtigstper6 = 0; }
                                                                if (isNaN(txtigstamt6) || txtigstamt6 == "") { txtigstamt6 = 0; }

                                                                var result6 = parseInt(txtqty6) * parseInt(txtrate6);
                                                                if (!isNaN(result6)) { txttaxable6 = result6; document.getElementById('txttaxable6').value = result6.toFixed(2); }

                                                                var cgstAmt6 = (result6 * txtcgstper6) / 100;
                                                                if (!isNaN(cgstAmt6)) { document.getElementById('txtcgstamt6').value = cgstAmt6.toFixed(2); }

                                                                var sgstAmt6 = (result6 * txtsgstper6) / 100;
                                                                if (!isNaN(sgstAmt6)) { document.getElementById('txtsgstamt6').value = sgstAmt6.toFixed(2); }

                                                                var igstAmt6 = (result6 * txtigstper6) / 100;
                                                                if (!isNaN(igstAmt6)) { document.getElementById('txtigstamt6').value = igstAmt6.toFixed(2); }

                                                                var TotalAfterTax6 = result6 + cgstAmt6 + sgstAmt6 + igstAmt6;
                                                                if (!isNaN(TotalAfterTax6)) { document.getElementById('txttotal6').value = TotalAfterTax6.toFixed(2); }


                                                                //7th Row Calculation 
                                                                var txtqty7 = document.getElementById('txtqty7').value;
                                                                var txtrate7 = document.getElementById('txtrate7').value;
                                                                var txttaxable7 = document.getElementById('txttaxable7').value;
                                                                var txtcgstper7 = document.getElementById('txtcgstper7').value;
                                                                var txtcgstamt7 = document.getElementById('txtcgstamt7').value;
                                                                var txtsgstper7 = document.getElementById('txtsgstper7').value;
                                                                var txtsgstamt7 = document.getElementById('txtsgstamt7').value;
                                                                var txtigstper7 = document.getElementById('txtigstper7').value;
                                                                var txtigstamt7 = document.getElementById('txtigstamt7').value;
                                                                var txttaxable7 = document.getElementById('txttaxable7').value;
                                                                if (isNaN(txtqty7) || txtqty7 == "") { txtqty7 = 0; }
                                                                if (isNaN(txtrate7) || txtrate7 == "") { txtrate7 = 0; }
                                                                if (isNaN(txttaxable7) || txttaxable7 == "") { txttaxable7 = 0; }
                                                                if (isNaN(txtcgstper7) || txtcgstper7 == "") { txtcgstper7 = 0; }
                                                                if (isNaN(txtcgstamt7) || txtcgstamt7 == "") { txtcgstamt7 = 0; }
                                                                if (isNaN(txtsgstper7) || txtsgstper7 == "") { txtsgstper7 = 0; }
                                                                if (isNaN(txtsgstamt7) || txtsgstamt7 == "") { txtsgstamt7 = 0; }
                                                                if (isNaN(txtigstper7) || txtigstper7 == "") { txtigstper7 = 0; }
                                                                if (isNaN(txtigstamt7) || txtigstamt7 == "") { txtigstamt7 = 0; }

                                                                var result7 = parseInt(txtqty7) * parseInt(txtrate7);
                                                                if (!isNaN(result7)) { txttaxable7 = result7; document.getElementById('txttaxable7').value = result7.toFixed(2); }

                                                                var cgstAmt7 = (result7 * txtcgstper7) / 100;
                                                                if (!isNaN(cgstAmt7)) { document.getElementById('txtcgstamt7').value = cgstAmt7.toFixed(2); }

                                                                var sgstAmt7 = (result7 * txtsgstper7) / 100;
                                                                if (!isNaN(sgstAmt7)) { document.getElementById('txtsgstamt7').value = sgstAmt7.toFixed(2); }

                                                                var igstAmt7 = (result7 * txtigstper7) / 100;
                                                                if (!isNaN(igstAmt7)) { document.getElementById('txtigstamt7').value = igstAmt7.toFixed(2); }

                                                                var TotalAfterTax7 = result7 + cgstAmt7 + sgstAmt7 + igstAmt7;
                                                                if (!isNaN(TotalAfterTax7)) { document.getElementById('txttotal7').value = TotalAfterTax7.toFixed(2); }


                                                                //8th Row Calculation 
                                                                var txtqty8 = document.getElementById('txtqty8').value;
                                                                var txtrate8 = document.getElementById('txtrate8').value;
                                                                var txttaxable8 = document.getElementById('txttaxable8').value;
                                                                var txtcgstper8 = document.getElementById('txtcgstper8').value;
                                                                var txtcgstamt8 = document.getElementById('txtcgstamt8').value;
                                                                var txtsgstper8 = document.getElementById('txtsgstper8').value;
                                                                var txtsgstamt8 = document.getElementById('txtsgstamt8').value;
                                                                var txtigstper8 = document.getElementById('txtigstper8').value;
                                                                var txtigstamt8 = document.getElementById('txtigstamt8').value;
                                                                var txttaxable8 = document.getElementById('txttaxable8').value;
                                                                if (isNaN(txtqty8) || txtqty8 == "") { txtqty8 = 0; }
                                                                if (isNaN(txtrate8) || txtrate8 == "") { txtrate8 = 0; }
                                                                if (isNaN(txttaxable8) || txttaxable8 == "") { txttaxable8 = 0; }
                                                                if (isNaN(txtcgstper8) || txtcgstper8 == "") { txtcgstper8 = 0; }
                                                                if (isNaN(txtcgstamt8) || txtcgstamt8 == "") { txtcgstamt8 = 0; }
                                                                if (isNaN(txtsgstper8) || txtsgstper8 == "") { txtsgstper8 = 0; }
                                                                if (isNaN(txtsgstamt8) || txtsgstamt8 == "") { txtsgstamt8 = 0; }
                                                                if (isNaN(txtigstper8) || txtigstper8 == "") { txtigstper8 = 0; }
                                                                if (isNaN(txtigstamt8) || txtigstamt8 == "") { txtigstamt8 = 0; }

                                                                var result8 = parseInt(txtqty8) * parseInt(txtrate8);
                                                                if (!isNaN(result8)) { txttaxable8 = result8; document.getElementById('txttaxable8').value = result8.toFixed(2); }

                                                                var cgstAmt8 = (result8 * txtcgstper8) / 100;
                                                                if (!isNaN(cgstAmt8)) { document.getElementById('txtcgstamt8').value = cgstAmt8.toFixed(2); }

                                                                var sgstAmt8 = (result8 * txtsgstper8) / 100;
                                                                if (!isNaN(sgstAmt8)) { document.getElementById('txtsgstamt8').value = sgstAmt8.toFixed(2); }

                                                                var igstAmt8 = (result8 * txtigstper8) / 100;
                                                                if (!isNaN(igstAmt8)) { document.getElementById('txtigstamt8').value = igstAmt8.toFixed(2); }

                                                                var TotalAfterTax8 = result8 + cgstAmt8 + sgstAmt8 + igstAmt8;
                                                                if (!isNaN(TotalAfterTax8)) { document.getElementById('txttotal8').value = TotalAfterTax8.toFixed(2); }

                                                                ///// Total calculation

                                                                //TotalQty
                                                                var TotalQty = parseInt(txtqty1) + parseInt(txtqty2) + parseInt(txtqty3) + parseInt(txtqty4) + parseInt(txtqty5) + parseInt(txtqty6) + parseInt(txtqty7) + parseInt(txtqty8);
                                                                if (!isNaN(TotalQty)) { document.getElementById('txttotalqty').value = TotalQty; }

                                                                //TotalRate
                                                                var TotalRate = parseFloat(txtrate1) + parseFloat(txtrate2) + parseFloat(txtrate3) + parseFloat(txtrate4) + parseFloat(txtrate5) + parseFloat(txtrate6) + parseFloat(txtrate7) + parseFloat(txtrate8);
                                                                if (!isNaN(TotalRate)) { document.getElementById('txttotalrate').value = TotalRate.toFixed(2); }

                                                                //TotalTaxAbleValue  
                                                                var TotalTaxAbleValue = parseFloat(txttaxable1) + parseFloat(txttaxable2) + parseFloat(txttaxable3) + parseFloat(txttaxable4) + parseFloat(txttaxable5) + parseFloat(txttaxable6) + parseFloat(txttaxable7) + parseFloat(txttaxable8);
                                                                if (!isNaN(TotalTaxAbleValue)) { document.getElementById('txttotaltaxable').value = TotalTaxAbleValue.toFixed(2); }

                                                                //Total cgst %
                                                                document.getElementById('txttotalcgstper').value = document.getElementById('txtcgstper1').value;
                                                                //TotalcgstAmt  
                                                                var TotalcgstAmt = parseFloat(cgstAmt1) + parseFloat(cgstAmt2) + parseFloat(cgstAmt3) + parseFloat(cgstAmt4) + parseFloat(cgstAmt5) + parseFloat(cgstAmt6) + parseFloat(cgstAmt7) + parseFloat(cgstAmt8);
                                                                if (!isNaN(TotalcgstAmt)) { document.getElementById('txttotalcgstamt').value = TotalcgstAmt.toFixed(2); }

                                                                //Total sgst %
                                                                document.getElementById('txttotalsgstper').value = document.getElementById('txtsgstper1').value;
                                                                //TotalsgstAmt  
                                                                var TotalsgstAmt = parseFloat(sgstAmt1) + parseFloat(sgstAmt2) + parseFloat(sgstAmt3) + parseFloat(sgstAmt4) + parseFloat(sgstAmt5) + parseFloat(sgstAmt6) + parseFloat(sgstAmt7) + parseFloat(sgstAmt8);
                                                                if (!isNaN(TotalsgstAmt)) { document.getElementById('txttotalsgstamt').value = TotalsgstAmt.toFixed(2); }

                                                                //Total igst %
                                                                document.getElementById('txttotaligstper').value = document.getElementById('txtigstper1').value;
                                                                //TotaligstAmt  
                                                                var TotaligstAmt = parseFloat(igstAmt1) + parseFloat(igstAmt2) + parseFloat(igstAmt3) + parseFloat(igstAmt4) + parseFloat(igstAmt5) + parseFloat(igstAmt6) + parseFloat(igstAmt7) + parseFloat(igstAmt8);
                                                                if (!isNaN(TotaligstAmt)) { document.getElementById('txttotaligstamt').value = TotaligstAmt.toFixed(2); }

                                                                //Gtotal
                                                                var TotaltxtGrandtotalAmt = parseFloat(TotalAfterTax1) + parseFloat(TotalAfterTax2) + parseFloat(TotalAfterTax3) + parseFloat(TotalAfterTax4) + parseFloat(TotalAfterTax5) + parseFloat(TotalAfterTax6) + parseFloat(TotalAfterTax7) + parseFloat(TotalAfterTax8);
                                                                if (!isNaN(TotaltxtGrandtotalAmt)) { document.getElementById('txtGrandtotal').value = TotaltxtGrandtotalAmt.toFixed(2); }

                                                                //Total Amt before Tax %
                                                                document.getElementById('lbltotalbefortax').innerHTML = document.getElementById('txttotaltaxable').value;

                                                                //Total CGST Amt %
                                                                document.getElementById('lbltotalcgstamt').innerHTML = document.getElementById('txttotalcgstamt').value;

                                                                //Total SGST Amt %
                                                                document.getElementById('lbltotalsgstamt').innerHTML = document.getElementById('txttotalsgstamt').value;

                                                                //Total IGST Amt %
                                                                document.getElementById('lbltotaligstamt').innerHTML = document.getElementById('txttotaligstamt').value;

                                                                //Total Tax Amt
                                                                var TotaltaxAmt111 = parseFloat(TotalcgstAmt) + parseFloat(TotalsgstAmt) + parseFloat(TotaligstAmt);
                                                                if (!isNaN(TotaltaxAmt111)) { document.getElementById('lbltotaltaxamt').innerHTML = TotaltaxAmt111.toFixed(2); }

                                                                //Max Grand total
                                                                var MaxGrandTotalAmt = parseFloat(TotaltaxAmt111) + parseFloat(document.getElementById('lbltotalbefortax').innerHTML);
                                                                if (!isNaN(MaxGrandTotalAmt)) { document.getElementById('lblMaxGrandtotal').innerHTML = MaxGrandTotalAmt.toFixed(2); }

                                                            }
                                                        </script>
                                                        <%--   1st Row  --%>
                                                        <tr>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsno1" runat="server" Width="25px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtdesc1" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsac1" runat="server" Width="75px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtqty1" onkeyup="sum()" onfocus="select()" runat="server" Width="35px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtrate1" onkeyup="sum()" onfocus="select()" runat="server" Width="65px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttaxable1" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtcgstper1" onkeyup="sum()" Text="9" onfocus="select()" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtcgstamt1" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtsgstper1" onkeyup="sum()" Text="9" onfocus="select()" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtsgstamt1" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtigstper1" onkeyup="sum()" Text="0" onfocus="select()" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtigstamt1" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotal1" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>

                                                        <%--   2nd Row  --%>
                                                        <tr>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsno2" runat="server" Width="25px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtdesc2" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsac2" runat="server" Width="75px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtqty2" onkeyup="sum()" onfocus="select()" runat="server" Width="35px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtrate2" onkeyup="sum()" onfocus="select()" runat="server" Width="65px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttaxable2" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtcgstper2" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtcgstamt2" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtsgstper2" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtsgstamt2" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtigstper2" onkeyup="sum()" onfocus="select()" Text="0" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtigstamt2" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotal2" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>

                                                        <%--   3rd Row  --%>
                                                        <tr>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsno3" runat="server" Width="25px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtdesc3" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsac3" runat="server" Width="75px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtqty3" onkeyup="sum()" onfocus="select()" runat="server" Width="35px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtrate3" onkeyup="sum()" onfocus="select()" runat="server" Width="65px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttaxable3" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtcgstper3" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtcgstamt3" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtsgstper3" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtsgstamt3" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtigstper3" onkeyup="sum()" onfocus="select()" Text="0" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtigstamt3" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotal3" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>

                                                        <%--   4th Row  --%>
                                                        <tr>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsno4" runat="server" Width="25px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtdesc4" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsac4" runat="server" Width="75px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtqty4" onkeyup="sum()" onfocus="select()" runat="server" Width="35px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtrate4" onkeyup="sum()" onfocus="select()" runat="server" Width="65px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttaxable4" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtcgstper4" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtcgstamt4" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtsgstper4" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtsgstamt4" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtigstper4" onkeyup="sum()" onfocus="select()" Text="0" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtigstamt4" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotal4" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>

                                                        <%--   5th Row  --%>
                                                        <tr>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsno5" runat="server" Width="25px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtdesc5" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsac5" runat="server" Width="75px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtqty5" onkeyup="sum()" onfocus="select()" runat="server" Width="35px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtrate5" onkeyup="sum()" onfocus="select()" runat="server" Width="65px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttaxable5" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtcgstper5" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtcgstamt5" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtsgstper5" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtsgstamt5" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtigstper5" onkeyup="sum()" onfocus="select()" Text="0" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtigstamt5" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotal5" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>

                                                        <%--   6th Row  --%>
                                                        <tr>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsno6" runat="server" Width="25px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtdesc6" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsac6" runat="server" Width="75px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtqty6" onkeyup="sum()" onfocus="select()" runat="server" Width="35px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtrate6" onkeyup="sum()" onfocus="select()" runat="server" Width="65px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttaxable6" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtcgstper6" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtcgstamt6" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtsgstper6" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtsgstamt6" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtigstper6" onkeyup="sum()" onfocus="select()" Text="0" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtigstamt6" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotal6" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>

                                                        <%--   7th Row  --%>
                                                        <tr>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsno7" runat="server" Width="25px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtdesc7" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsac7" runat="server" Width="75px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtqty7" onkeyup="sum()" onfocus="select()" runat="server" Width="35px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtrate7" onkeyup="sum()" onfocus="select()" runat="server" Width="65px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttaxable7" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtcgstper7" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtcgstamt7" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtsgstper7" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtsgstamt7" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtigstper7" onkeyup="sum()" onfocus="select()" Text="0" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtigstamt7" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotal7" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>

                                                        <%--   8th Row  --%>
                                                        <tr>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsno8" runat="server" Width="25px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtdesc8" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtsac8" runat="server" Width="75px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtqty8" onkeyup="sum()" onfocus="select()" runat="server" Width="35px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtrate8" onkeyup="sum()" onfocus="select()" runat="server" Width="65px"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttaxable8" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtcgstper8" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtcgstamt8" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtsgstper8" onkeyup="sum()" onfocus="select()" Text="9" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtsgstamt8" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txtigstper8" onkeyup="sum()" onfocus="select()" Text="0" runat="server" Width="25px"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txtigstamt8" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotal8" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>





                                                        <%--    Total line  --%>
                                                        <tr style='border: 3px solid #000 !important;'>
                                                            <td style='border-top: none; background-color: #bcd6ee; border: 2px solid #000;' colspan="3">
                                                                <span style="color: #000; font-size: 22px; font-weight: 900; text-align: center; margin-top: 0px;">Total</span>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotalqty" runat="server" Width="35px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotalrate" runat="server" Width="65px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txttotaltaxable" runat="server" Width="75px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txttotalcgstper" runat="server" Width="25px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txttotalcgstamt" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txttotalsgstper" runat="server" Width="25px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txttotalsgstamt" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style='border-right: 1px solid gray;'>
                                                                            <asp:TextBox ID="txttotaligstper" runat="server" Width="25px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                        <td style='border-right: none;'>
                                                                            <asp:TextBox ID="txttotaligstamt" runat="server" Width="55px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtGrandtotal" runat="server" Width="85px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox></td>
                                                        </tr>

                                                        <%--    Total Invoice Amount in word  --%>
                                                        <tr style='border: 1px solid #000 !important;'>
                                                            <td style='border-top: none; background-color: #bcd6ee; border: 2px solid #000;' colspan="6">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Total Invoice amount in words </span>
                                                            </td>

                                                            <td style='border-top: none; background-color: #9ebfde;' colspan="3">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Total Amount before Tax</span>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:Label ID="lbltotalbefortax" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>


                                                        <%--    Total   --%>
                                                        <tr style='border: 1px solid #000 !important;'>
                                                            <td style='border-top: none; border: 2px solid #000;' colspan="6" rowspan="5">
                                                                <span style="color: #000; font-size: 12px; font-weight: 900; text-align: center; margin-top: 0px;">Total Invoice amount in words </span>
                                                            </td>

                                                            <td style='border-top: none;' colspan="3">
                                                                <span style="color: #000; font-size: 12px; font-weight: 900; text-align: center; margin-top: 0px;">Add : CGST</span>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:Label ID="lbltotalcgstamt" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr style='border: 1px solid #000 !important;'>
                                                            <td style='border-top: none;' colspan="3">
                                                                <span style="color: #000; font-size: 12px; font-weight: 900; text-align: center; margin-top: 0px;">Add : SGST</span>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:Label ID="lbltotalsgstamt" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr style='border: 1px solid #000 !important;'>
                                                            <td style='border-top: none;' colspan="3">
                                                                <span style="color: #000; font-size: 12px; font-weight: 900; text-align: center; margin-top: 0px;">Add : IGST</span>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:Label ID="lbltotaligstamt" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr style='border: 1px solid #000 !important;'>
                                                            <td style='border-top: none;' colspan="3">
                                                                <span style="color: #000; font-size: 12px; font-weight: 900; text-align: center; margin-top: 0px;">Total Tax Amount</span>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:Label ID="lbltotaltaxamt" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr style='border: 1px solid #000 !important;'>
                                                            <td style='border-top: none;' colspan="3">
                                                                <span style="color: #000; font-size: 12px; font-weight: 900; text-align: center; margin-top: 0px;">Total Amount after Tax: </span>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:Label ID="lblMaxGrandtotal" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>


                                                        <%--    GST on Reverse Charge  --%>
                                                        <tr>
                                                            <td style='border-top: none; background-color: #bcd6ee;' colspan="4">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Bank Details</span>
                                                            </td>

                                                            <td style='border-top: none; background-image: url("../img/WlsplSeal.jpg"); background-repeat: no-repeat; background-size: 168px 149px; background-position: center;' colspan="2" rowspan="6">
                                                                <span style="color: #000; font-size: 12px; text-align: center; margin-top: 0px;">Web Link Services Pvt Ltd </span>
                                                            </td>

                                                            <td style='border-top: none; background-color: #9ebfde;' colspan="3">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">GST on Reverse Charge</span>
                                                            </td>

                                                            <td style='border-top: none;'>
                                                                <asp:TextBox ID="txtreversegst" runat="server" Width="85px"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style='border-top: none;' colspan="4">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Bank A/C: - 916020085136854</span>
                                                            </td>

                                                            <td style='border-top: none;' colspan="4" rowspan="5">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">For,<br />
                                                                    Web Link Services Pvt Ltd</span>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td style='border-top: none;' colspan="4">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Bank IFSC: - UTIB0001641 </span>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style='border-top: none;' colspan="4">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Axis Bank Ltd- Rahatani Branch, Pune</span>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style='border-top: none;' colspan="4">&nbsp;
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style='border: none;' colspan="4">&nbsp;
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style='border: none;' colspan="4">&nbsp;
                                                            </td>

                                                            <td style='border-top: none;' colspan="2">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Common Seal</span>
                                                            </td>

                                                            <td style='border-top: 1px solid white !important;' colspan="4">
                                                                <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Authorised signatory</span>
                                                            </td>

                                                        </tr>


                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <span style="font-size: 13px;">NOTE :-  This is system generated invoice. If you find this is not a genuine then please report to info@weblinkservices.net immediately.</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <center><a href="AdminDashboard.aspx" class="btn btn-dark"><< Back To Dashboard</a></center>
        </div>
        <div class="col-md-4">
            <center><asp:Button ID="btngenerate" runat="server" Text="Generate Invoice" CssClass="btn btn-primary" /></center>
        </div>
        <div class="col-md-4">
            <center><a href="AdminDashboard.aspx" class="btn btn-dark">Invoice List >></a></center>
        </div>
    </div>
    <br />

</asp:Content>

