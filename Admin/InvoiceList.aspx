<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="InvoiceList.aspx.cs" Inherits="Admin_InvoiceList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
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

        /*::-webkit-scrollbar {
            width: 2px; 
            background: transparent; 
        }*/


        .card {
            margin-bottom: 0px !important;
        }

        .readonlytxt {
            background-color: #f1f5f7;
        }

        .lblstyle {
            color: #0a0003 !important;
            font-size: 15px !important;
            font-weight: 800 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:HiddenField ID="HFccode" runat="server" />
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
                                                    <asp:TextBox ID="txtinvoiceno" ReadOnly="true" CssClass="readonlytxt form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Invoice No. is Required"
                                                        ControlToValidate="txtinvoiceno" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2 spancls">Invoice Date<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtinvdate" ReadOnly="true" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Date is Required"
                                                        ControlToValidate="txtinvdate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtinvdate" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2 spancls">Reverse Charge(Y/N)<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtreversecharge" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Reverse Charge is Required"
                                                        ControlToValidate="txtreversecharge" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2 spancls">State<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtstate" CssClass="form-control" runat="server" Width="100%" Text="Maharashtra"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="State is Required"
                                                        ControlToValidate="txtstate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                                    <asp:TextBox ID="txtcompanyname" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Company Name is Required"
                                                        ControlToValidate="txtcompanyname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                        TargetControlID="txtcompanyname">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-2 spancls">Address<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtaddress" TextMode="MultiLine" Height="50px" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Address is Required"
                                                        ControlToValidate="txtaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-md-2 spancls">GSTIN<i class="reqcls">*&nbsp;</i> : </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtgstin" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Required"
                                                        ControlToValidate="txtgstin" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Please Select State"
                                                        ControlToValidate="ddlpartystate" ValidationGroup="form1" ForeColor="Red" InitialValue="Select" SetFocusOnError="true"></asp:RequiredFieldValidator>
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


                                                            <%--   1st Row  --%>
                                                            <tr>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsno1" Text="1" runat="server" Width="25px"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Required !"
                                                        ControlToValidate="txtsno1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtdesc1" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsac1" Text="00440013" runat="server" Width="75px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtsno2"  Text="2" runat="server" Width="25px"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtdesc2" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsac2" Text="00440013" runat="server" Width="75px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtsno3" Text="3" runat="server" Width="25px"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtdesc3" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsac3" Text="00440013" runat="server" Width="75px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtsno4" Text="4" runat="server" Width="25px"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtdesc4" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsac4" Text="00440013" runat="server" Width="75px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtsno5" Text="5" runat="server" Width="25px"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtdesc5" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsac5" Text="00440013" runat="server" Width="75px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtsno6" Text="6" runat="server" Width="25px"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtdesc6" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsac6" Text="00440013" runat="server" Width="75px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtsno7" Text="7" runat="server" Width="25px"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtdesc7" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsac7" Text="00440013" runat="server" Width="75px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtsno8" Text="8" runat="server" Width="25px"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtdesc8" runat="server" Width="250px" TextMode="MultiLine" Height="30px" Style="margin-top: 4px !important;"></asp:TextBox></td>
                                                                <td style='border-top: none;'>
                                                                    <asp:TextBox ID="txtsac8" Text="00440013" runat="server" Width="75px"></asp:TextBox></td>
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
                                                                    <%--<asp:Label ID="lbltotalbefortax" CssClass="lblstyle" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lbltotalbefortax" runat="server" Width="90px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox>
                                                                </td>
                                                            </tr>


                                                            <%--    Total   --%>
                                                            <tr style='border: 1px solid #000 !important;'>
                                                                <td style='border-top: none; border: 2px solid #000;' colspan="6" rowspan="5">
                                                                    <asp:Label ID="lblamtinwords" CssClass="lblstyle" runat="server"></asp:Label>
                                                                    <%--<asp:TextBox ID="TextBox1" runat="server" Width="90px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox>--%>
                                                                </td>

                                                                <td style='border-top: none;' colspan="3">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Add : CGST</span>
                                                                </td>

                                                                <td style='border-top: none;'>
                                                                    <%--<asp:Label ID="lbltotalcgstamt" CssClass="lblstyle" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lbltotalcgstamt" runat="server" Width="90px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr style='border: 1px solid #000 !important;'>
                                                                <td style='border-top: none;' colspan="3">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Add : SGST</span>
                                                                </td>

                                                                <td style='border-top: none;'>
                                                                    <%--<asp:Label ID="lbltotalsgstamt" CssClass="lblstyle" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lbltotalsgstamt" runat="server" Width="90px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr style='border: 1px solid #000 !important;'>
                                                                <td style='border-top: none;' colspan="3">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Add : IGST</span>
                                                                </td>

                                                                <td style='border-top: none;'>
                                                                    <%--<asp:Label ID="lbltotaligstamt" CssClass="lblstyle" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lbltotaligstamt" runat="server" Width="90px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr style='border: 1px solid #000 !important;'>
                                                                <td style='border-top: none;' colspan="3">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Total Tax Amount</span>
                                                                </td>

                                                                <td style='border-top: none;'>
                                                                    <%--<asp:Label ID="lbltotaltaxamt" CssClass="lblstyle" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lbltotaltaxamt" runat="server" Width="90px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr style='border: 1px solid #000 !important;'>
                                                                <td style='border-top: none;' colspan="3">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Total Amount after Tax: </span>
                                                                </td>

                                                                <td style='border-top: none;'>
                                                                  <%--  <asp:Label ID="lblMaxGrandtotal" CssClass="lblstyle" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lblMaxGrandtotal" runat="server" Width="90px" ReadOnly="true" CssClass="readonlytxt"></asp:TextBox>
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
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Bank A/C : 916020085136854</span>
                                                                </td>

                                                                <td style='border-top: none;' colspan="4" rowspan="5">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">For,<br />
                                                                        Web Link Services Pvt Ltd</span>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td style='border-top: none;' colspan="4">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Bank IFSC : UTIB0001641 </span>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style='border-top: none;' colspan="4">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Nank Name : Axis Bank Ltd</span>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style='border-top: none;' colspan="4">
                                                                    <span style="color: #000; font-size: 14px; font-weight: 900; text-align: center; margin-top: 0px;">Branch : Rahatani Branch, Pune</span>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style='border: none;' colspan="4">
                                                                    <span style="font-size: 12px; text-align: left; color: #000;">&nbsp;NOTE : This is system generated invoice.<br />
                                                                        &nbsp;If you find this is not a genuine then,<br />
                                                                        &nbsp;please report to <a href="mailto:info@weblinkservices.net" style="color: #1672ca; font-weight: 700">info@weblinkservices.net</a> immediately.</span>
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

                                    </div>

                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtservicedesc" Height="55px" placeholder="Service Description" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
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
                <center><asp:Button ID="btngenerate" runat="server" ValidationGroup="form1" Text="Generate Invoice" CssClass="btn btn-primary" /></center>
            </div>
            <div class="col-md-4">
                <center><a href="AdminDashboard.aspx" class="btn btn-dark">Invoice List >></a></center>
            </div>
        </div>
        <br />
    
</asp:Content>

