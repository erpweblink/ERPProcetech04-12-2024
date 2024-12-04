<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Termasandmaster.aspx.cs" Inherits="Admin_Termasandmaster" MasterPageFile="~/Admin/AdminMasterPage.master" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .dissablebtn {
            cursor: not-allowed;
        }
    </style>
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

        .test tr input {
            border: 1px solid red;
            margin-right: 10px;
            padding-right: 10px;
        }

        @media (min-width: 1200px) {
            .container {
                max-width: 1250px !important;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <div class="page-wrapper">
        <div class="page-body">
            <div class="row">
                <div class="col-md-7">
                    <div class="page-header-breadcrumb">
                        <div style="float: left; font-size: 15px;">
                            <span><i class="fas fa-file-contract"></i>&nbsp;Terms & Master</span>
                        </div>
                    </div>
                </div>

                <div class="col-md-5">
                    <%--<div class="page-header-breadcrumb">
                        <div style="float: right; margin: 3px; margin-bottom: 5px;">
                            <span><a href="BlogsEditor.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Blogs Page</a>&nbsp;&nbsp;
                                <a href="Commentslist.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Comments</a>
                            </span>
                        </div>
                    </div>--%>
                </div>
            </div>

            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <h5><i class="fa fa-user-plus"></i>Terms & Masters</h5>
                    </div>

                    <div class="card-body">
                        <table class="table ; border">

                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label11" runat="server">Refrence No.:</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtrefno" CssClass="form-control" placeholder="Refrence No." ></asp:TextBox>
                                </td>
                            </tr>





                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label10" runat="server">freight & Insurance Charges </asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtfreighCharges" CssClass="form-control">To the buyers account</asp:TextBox>
                                </td>
                            </tr>

                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label1" runat="server">Packing Charge</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtpackingcharge" CssClass="form-control">For Wooden Box, Cost WILL BE EXTRA</asp:TextBox>
                                </td>
                            </tr>
                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label2" runat="server">Payment</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtPayment" CssClass="form-control">60% Advance payment on confirmation of order & balance 40 % before dispatch</asp:TextBox>
                                </td>
                            </tr>
                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label3" runat="server">Inspection</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtInspection" CssClass="form-control">You may depute your representative for inspection of the machine before dispatch at your cost,Local travel arrangements will be done by us</asp:TextBox>
                                </td>
                            </tr>

                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label4" runat="server">Damage</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtDamage" CssClass="form-control">Any damages in the Equipment's found during transportation, than it should be informed to MATEE immediately upon receipt of
the Equipment's along with Photographs showing Truck
Numberbefore unloading the machine from Truck. If this not intimated
within 2 days' time then insurance claim cannot be processedand we shall not be responsible for any damages</asp:TextBox>
                                </td>
                            </tr>


                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label5" runat="server">After Sales Services</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtAfterSalesServices" CssClass="form-control">
After sales Service will be provided through factory trained technician for 1 year from the date of dispatch at no extra cost. However Technician's TO and FRO travel expenses, boarding &lodging and other incidental expenses will be at your account.
And thereafter as per terms and conditions mutually agreed upon.
                                    </asp:TextBox>
                                </td>
                            </tr>


                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label6" runat="server">Warranty</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtWarranty" CssClass="form-control">
MATEE gives warranty on all machines for a period of 12 months from the date of supply against any mechanical manufacturing defects. This warranty does not cover any
Bought-out Items mainly electronics and electrical such as
Sensors, HMI, PLC, VFD, Starters, Fans, Switches, Power
Supply also Pneumatic components due to any handling defects, Rubber items and other consumables brought out items. Any defects found in machine need to be informed at
MATEE within1 Month from the date of Dispatch to take corrective action. 
                                    </asp:TextBox>
                                </td>
                            </tr>

                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label7" runat="server">Validity</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtValidity" CssClass="form-control">
Prices & Delivery listed above are valid for 60days & thereaftersubject to our confirmation. 
                                    </asp:TextBox>
                                </td>
                            </tr>



                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label8" runat="server">Jurisdiction</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtJurisdiction" CssClass="form-control">
Prices & Delivery listed above are valid for 60days & thereaftersubject to our confirmation Jurisdiction

                                    </asp:TextBox>
                                </td>
                            </tr>


                            <tr class="border">
                                <td class="col-md-3">
                                    <asp:Label ID="Label9" runat="server">Special Note:</asp:Label>
                                </td>
                                <td class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtSpecialNote" CssClass="form-control">
Advance Payment received towards the making of the
Machines/Equipment's shall be forfeited if the
Machines/Equipment's are not lifted by you within Fifteen (15)

                                    </asp:TextBox>
                                </td>
                            </tr>



                        </table>


                    </div>

                    <div class="card-footer">
                        <div class="row">
                            <div class="col-md-6"></div>
                            <div class="col-md-26" style="text-align: center">
                                <center>
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-success" Width="100%" Text="Submit" OnClick="btnsubmit_Click" /></center>
                            </div>
                            <div class="col-md-6"></div>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
