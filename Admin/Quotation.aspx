<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Quotation.aspx.cs" Inherits="Admin_Quotation" %>


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

        .aspNetDisabled {
            cursor: not-allowed !important;
        }

        .srtxt {
            width: 50px !important;
        }

        .Desctxt {
            width: 250px !important;
            height: 40px;
        }

        .Hsntxt {
            width: 100px !important;
        }

        .Qtytxt {
            width: 60px !important;
        }

        .Ratetxt {
            width: 100px !important;
        }

        .Amttxt {
            width: 100px !important;
        }

        .table td {
            padding: 5px !important;
        }

        .table th {
            padding: 5px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="page-wrapper">
        <div class="page-body">
            <asp:HiddenField ID="HFccode" runat="server" />
            <asp:HiddenField ID="HFcname" runat="server" />

            <asp:HiddenField ID="hfregby" runat="server" />

            <div class="row">
                <div class="col-md-7">
                    <%--<div class="page-header-breadcrumb">
                        <div style="float: left; font-size: 15px;">
                            <span><i class="feather icon-home"></i>&nbsp;Register an Enquiry</span>
                        </div>
                    </div>--%>
                </div>


                <div class="col-md-5">
                    <div class="page-header-breadcrumb">
                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                            <span><a href="AllCompanyList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Company List</a>&nbsp;&nbsp;
                                <a href="EnquiryList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Enquiry List</a>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5>Quotation</h5>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <%--  <div class="card">--%>
                            <div class="card-header">
                                <div class="row">
                                    <div class="col-md-12">
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Party Name<i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtcname" CssClass="form-control" runat="server" Width="100%" OnTextChanged="txtcname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Company Name"
                                                    ControlToValidate="txtcname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                                    TargetControlID="txtcname">
                                                </asp:AutoCompleteExtender>
                                                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-2 spancls">Quotation No <i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txQutno" CssClass="form-control" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Contact Name"
                                                    ControlToValidate="txQutno" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <p>(May change at run time)</p>
                                            </div>
                                        </div>


                                        <br />
                                        <div class="row">
                                            <div class="col-md-2 spancls">Address <i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtshippingaddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                    ControlToValidate="txtshippingaddress" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2 spancls">Date <i class="reqcls">*&nbsp;</i> : </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtdate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Address"
                                                    ControlToValidate="txtdate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="card-header bg-primary text-uppercase text-white">
                                            <h5>Products</h5>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <table class="table" border="1" style="width: 100%; border: 1px solid #0c7d38;">
                                                    <tr style="background-color: #7ad2d4; color: #000; font-weight: 600; text-align: center;">
                                                        <td>SN</td>
                                                        <td>Name Of Particulars</td>
                                                        <td>HSN</td>
                                                        <td>Qty</td>
                                                        <td>Rate</td>
                                                        <td>Disc %</td>
                                                        <td>Amount</td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <center><asp:TextBox ID="txtsr1" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                            <asp:RequiredFieldValidator ID="RFieldV4" runat="server" Display="Dynamic" ErrorMessage="Please Enter"
                                                                ControlToValidate="txtsr1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtDesc1" CssClass="Desctxt" TextMode="MultiLine" runat="server"></asp:TextBox></center>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter"
                                                                ControlToValidate="txtDesc1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtHsn1" CssClass="Hsntxt" runat="server"></asp:TextBox></center>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Please Enter"
                                                                ControlToValidate="txtHsn1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtQty1" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Please Enter"
                                                                ControlToValidate="txtQty1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtRate1" onkeyup="sum()" onfocus="select()" CssClass="Ratetxt" runat="server"></asp:TextBox></center>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Please Enter"
                                                                ControlToValidate="txtRate1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>

                                                        <td>
                                                            <center><asp:TextBox ID="txtdisc1" onkeyup="sum()" onfocus="select()" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ErrorMessage="Please Enter"
                                                                ControlToValidate="txtdisc1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>

                                                        <td>
                                                            <center><asp:TextBox ID="txtAmt1" CssClass="Amttxt" runat="server" ReadOnly="true"></asp:TextBox></center>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ErrorMessage="Please Enter"
                                                                ControlToValidate="txtAmt1" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <center><asp:TextBox ID="txtsr2" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtDesc2" CssClass="Desctxt" TextMode="MultiLine" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtHsn2" CssClass="Hsntxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtQty2" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtRate2" onkeyup="sum()" onfocus="select()" CssClass="Ratetxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtdisc2" onkeyup="sum()" onfocus="select()" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                        </td>

                                                        <td>
                                                            <center><asp:TextBox ID="txtAmt2" ReadOnly="true" CssClass="Amttxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <center><asp:TextBox ID="txtsr3" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtDesc3" CssClass="Desctxt" TextMode="MultiLine" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtHsn3" CssClass="Hsntxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtQty3" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtRate3" onkeyup="sum()" onfocus="select()" CssClass="Ratetxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtdisc3" onkeyup="sum()" onfocus="select()" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtAmt3" ReadOnly="true" CssClass="Amttxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <center><asp:TextBox ID="txtsr4" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtDesc4" CssClass="Desctxt" TextMode="MultiLine" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtHsn4" CssClass="Hsntxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtQty4" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtRate4" onkeyup="sum()" onfocus="select()" CssClass="Ratetxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtdisc4" onkeyup="sum()" onfocus="select()" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtAmt4" ReadOnly="true" CssClass="Amttxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <center><asp:TextBox ID="txtsr5" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtDesc5" CssClass="Desctxt" TextMode="MultiLine" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtHsn5" CssClass="Hsntxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtQty5" onkeyup="sum()" onfocus="select()" CssClass="Qtytxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtRate5" onkeyup="sum()" onfocus="select()" CssClass="Ratetxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtdisc5" onkeyup="sum()" onfocus="select()" CssClass="srtxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                        <td>
                                                            <center><asp:TextBox ID="txtAmt5" ReadOnly="true" CssClass="Amttxt" runat="server"></asp:TextBox></center>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lbltotalqty" runat="server" Text="" Style="font-size: 14px; font-weight: 600; text-align: center;"></asp:Label></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lbltotalamount" runat="server" Text="" Style="font-size: 14px; font-weight: 600; text-align: center;"></asp:Label></td>
                                                    </tr>

                                                </table>
                                            </div>
                                        </div>


                                        <br />
                                        <div class="row">
                                            <div class="col-md-4"></div>
                                            <div class="col-md-2">
                                                <center> <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Send/Save" OnClick="btnadd_Click"/></center>
                                            </div>
                                            <div class="col-md-2">
                                                <center> <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Width="100%" Text="Reset" OnClick="btnreset_Click"/></center>
                                            </div>
                                            <div class="col-md-6"></div>

                                        </div>
                                        <br />

                                    </div>
                                </div>


                            </div>
                            <%-- </div>--%>
                        </div>
                    </div>

                    <%-- <br />--%>
                </div>
            </div>
        </div>
        <%-- <br />--%>
    </div>

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript">
        function sum() {
            
            //1st Row Calculation 
            var txtqty1 = document.getElementById('ContentPlaceHolder1_txtQty1').value;
            var txtrate1 = document.getElementById('ContentPlaceHolder1_txtRate1').value;
            var txtdisc1 = document.getElementById('ContentPlaceHolder1_txtdisc1').value;
            var txtAmt1 = document.getElementById('ContentPlaceHolder1_txtAmt1').value;
            
            if (isNaN(txtqty1) || txtqty1 == "") { txtqty1 = 0; }
            if (isNaN(txtrate1) || txtrate1 == "") { txtrate1 = 0; }
            if (isNaN(txtdisc1) || txtdisc1 == "") { txtdisc1 = 0; }
            if (isNaN(txtAmt1) || txtAmt1 == "") { txtAmt1 = 0; }

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            var discAmt = (result1 * txtdisc1) / 100;
            var total1 = result1 - discAmt;
            if (!isNaN(result1)) { document.getElementById('ContentPlaceHolder1_txtAmt1').value = total1.toFixed(2); }
            
            //2nd Row Calculation 
            var txtqty2 = document.getElementById('ContentPlaceHolder1_txtQty2').value;
            var txtrate2 = document.getElementById('ContentPlaceHolder1_txtRate2').value;
            var txtdisc2 = document.getElementById('ContentPlaceHolder1_txtdisc2').value;
            var txtAmt2 = document.getElementById('ContentPlaceHolder1_txtAmt2').value;

            if (isNaN(txtqty2) || txtqty2 == "") { txtqty2 = 0; }
            if (isNaN(txtrate2) || txtrate2 == "") { txtrate2 = 0; }
            if (isNaN(txtdisc2) || txtdisc2 == "") { txtdisc2 = 0; }
            if (isNaN(txtAmt2) || txtAmt2 == "") { txtAmt2 = 0; }

            var result2 = parseInt(txtqty2) * parseFloat(txtrate2);
            var discAmt = (result2 * txtdisc2) / 100;
            var total2 = result2 - discAmt;
            if (!isNaN(result2)) { document.getElementById('ContentPlaceHolder1_txtAmt2').value = total2.toFixed(2); }

            //3 Row Calculation 
            var txtqty3 = document.getElementById('ContentPlaceHolder1_txtQty3').value;
            var txtrate3 = document.getElementById('ContentPlaceHolder1_txtRate3').value;
            var txtdisc3 = document.getElementById('ContentPlaceHolder1_txtdisc3').value;
            var txtAmt3 = document.getElementById('ContentPlaceHolder1_txtAmt3').value;

            if (isNaN(txtqty3) || txtqty3 == "") { txtqty3 = 0; }
            if (isNaN(txtrate3) || txtrate3 == "") { txtrate3 = 0; }
            if (isNaN(txtdisc3) || txtdisc3 == "") { txtdisc3 = 0; }
            if (isNaN(txtAmt3) || txtAmt3 == "") { txtAmt3 = 0; }

            var result3 = parseInt(txtqty3) * parseFloat(txtrate3);
            var discAmt = (result3 * txtdisc3) / 100;
            var total3 = result3 - discAmt;
            if (!isNaN(result3)) { document.getElementById('ContentPlaceHolder1_txtAmt3').value = total3.toFixed(2); }

            //4 Row Calculation 
            var txtqty4 = document.getElementById('ContentPlaceHolder1_txtQty4').value;
            var txtrate4 = document.getElementById('ContentPlaceHolder1_txtRate4').value;
            var txtdisc4 = document.getElementById('ContentPlaceHolder1_txtdisc4').value;
            var txtAmt4 = document.getElementById('ContentPlaceHolder1_txtAmt4').value;

            if (isNaN(txtqty4) || txtqty4 == "") { txtqty4 = 0; }
            if (isNaN(txtrate4) || txtrate4 == "") { txtrate4 = 0; }
            if (isNaN(txtdisc4) || txtdisc4 == "") { txtdisc4 = 0; }
            if (isNaN(txtAmt4) || txtAmt4 == "") { txtAmt4 = 0; }

            var result4 = parseInt(txtqty4) * parseFloat(txtrate4);
            var discAmt = (result4 * txtdisc4) / 100;
            var total4 = result4 - discAmt;
            if (!isNaN(result4)) { document.getElementById('ContentPlaceHolder1_txtAmt4').value = total4.toFixed(2); }

            //5 Row Calculation 
            var txtqty5 = document.getElementById('ContentPlaceHolder1_txtQty5').value;
            var txtrate5 = document.getElementById('ContentPlaceHolder1_txtRate5').value;
            var txtdisc5 = document.getElementById('ContentPlaceHolder1_txtdisc5').value;
            var txtAmt5 = document.getElementById('ContentPlaceHolder1_txtAmt5').value;

            if (isNaN(txtqty5) || txtqty5 == "") { txtqty5 = 0; }
            if (isNaN(txtrate5) || txtrate5 == "") { txtrate5 = 0; }
            if (isNaN(txtdisc5) || txtdisc5 == "") { txtdisc5 = 0; }
            if (isNaN(txtAmt5) || txtAmt5 == "") { txtAmt5 = 0; }

            var result5 = parseInt(txtqty5) * parseFloat(txtrate5);
            var discAmt = (result5 * txtdisc5) / 100;
            var total5 = result5 - discAmt;
            if (!isNaN(result5)) { document.getElementById('ContentPlaceHolder1_txtAmt5').value = total5.toFixed(2); }
        }
    </script>

</asp:Content>

