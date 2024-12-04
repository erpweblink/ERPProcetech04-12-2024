<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="Admin_ItemMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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

        .rwotoppadding {
            padding-top: 10px;
        }
    </style>
    <script>
        function HideLabel(msg) {
            Swal.fire({
                icon: 'success',
                text: msg,
                timer: 3000,
                showCancelButton: false,
                showConfirmButton: false
            }).then(function () {
                window.location.href = "../Admin/AllItemList.aspx";
            })
        };
    </script>
    <style>
        .row {
            margin-top: 10px;
        }
    </style>
    <script type='text/javascript'>
        function scrollToElement() {
            var target = document.getElementById("divdtls").offsetTop;
            window.scrollTo(0, target);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-md-7">
                        </div>
                        <div class="col-md-5">
                            <div class="page-header-breadcrumb">
                                <div style="float: right; margin: 3px; margin-bottom: 5px;">
                                    <span><a href="ItemList.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Item List</a>&nbsp;&nbsp;
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container py-3">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Add Item</h5>
                            </div>
                            <div class="row">
                                <div class="col-xl-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Supplier Name<i class="reqcls">*&nbsp;</i> : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtSupplierName" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Please Enter Supplier Name"
                                                            ControlToValidate="txtSupplierName" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetSupplierList"
                                                            TargetControlID="txtSupplierName">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <asp:HiddenField runat="server" ID="hdnFile" />
                                                    <div class="col-md-2 spancls">Item Name<i class="reqcls">*&nbsp;</i> : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtItemName" CssClass="form-control" runat="server" Width="100%" OnTextChanged="txtItemName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Item Name"
                                                            ControlToValidate="txtItemName" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetItemList"
                                                            TargetControlID="txtItemName">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 spancls">Item Description : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Item Code : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtItemcode" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">HSN <i class="reqcls">*&nbsp;</i>: </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtHSNNo" CssClass="form-control" runat="server" MaxLength="8" OnTextChanged="txtHSNNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter HSN"
                                                            ControlToValidate="txtHSNNo" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetHSNList"
                                                            TargetControlID="txtHSNNo">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Category:</div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2 spancls">Sub Category:</div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlSubcategory" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Purchase Rate:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtPurchaseRate" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">Storage Unit <i class="reqcls">*&nbsp;</i>:</div>
                                                    <div class="col-md-4">

                                                        <asp:DropDownList runat="server" ID="txtStorageUnit" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Select Unit"
                                                            ControlToValidate="txtStorageUnit" InitialValue="0" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <%--<asp:TextBox ID="txtStorageUnit" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>--%>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-2 spancls">GST Taxation <i class="reqcls">*&nbsp;</i>:</div>
                                                    <div class="col-md-4">
                                                        <asp:Label runat="server">Type</asp:Label>
                                                        <asp:DropDownList CssClass="form-control" ID="txtGSTType" runat="server" AutoPostBack="true" OnTextChanged="txtGSTType_TextChanged">
                                                            <%--        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem>CGST_SGST</asp:ListItem>
                                                            <asp:ListItem>IGST</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Select Taxation"
                                                            ControlToValidate="txtGSTType" InitialValue="0" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label runat="server">CGST(%)</asp:Label>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlCgst" runat="server">
                                                            <asp:ListItem>0</asp:ListItem>
                                                            <asp:ListItem>0.125</asp:ListItem>
                                                            <asp:ListItem>1.5</asp:ListItem>
                                                            <asp:ListItem>2.5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label runat="server">SGST(%)</asp:Label>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlSgst" runat="server">
                                                            <asp:ListItem>0</asp:ListItem>
                                                            <asp:ListItem>0.125</asp:ListItem>
                                                            <asp:ListItem>1.5</asp:ListItem>
                                                            <asp:ListItem>2.5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label runat="server">IGST(%)</asp:Label>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlIgst" runat="server">
                                                            <asp:ListItem>0</asp:ListItem>
                                                            <asp:ListItem>0.25</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                            <asp:ListItem>28</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>


                                                <div class="row" runat="server">
                                                    <div class="col-md-2 spancls">Opening Stock: </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtOpeningStock" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">Safety Stock</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtSafetyStock" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-2 spancls">Min Order Qty:</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtminOrderqty" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">Type:</div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server">
                                                            <asp:ListItem>Goods</asp:ListItem>
                                                            <asp:ListItem>Services</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row" runat="server">
                                                    <div class="col-md-2 spancls">Stock Location: </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtStockLocation" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">Shelf Life(Days)</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtShelfLife" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row" runat="server">
                                                    <div class="col-md-2 spancls">Salable:</div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlSalable" CssClass="form-control" runat="server">
                                                            <asp:ListItem>YES</asp:ListItem>
                                                            <asp:ListItem>NO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>


                                                    <div class="col-md-2 spancls">Drawing File: </div>
                                                    <div class="col-md-4">
                                                        <asp:FileUpload runat="server" ID="fileuploadDrawing" CssClass="form-control" />
                                                        <asp:Label ID="lblFilemsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-2 spancls">Is Active: </div>
                                                    <div class="col-md-4">
                                                        <asp:CheckBox runat="server" ID="chkIsactive" Checked="true" />
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-2 spancls">Part code </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtpartcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-2"></div>
                                                            <div class="col-md-2">
                                                                <asp:Button ID="btnadd" runat="server" ValidationGroup="form1" CssClass="btn btn-primary" Width="100%" Text="Add Item" OnClick="btnadd_Click" />
                                                            </div>
                                                            <div class="col-md-2">
                                                                <center>
                                                                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Width="100%" Text="Reset" OnClick="btnreset_Click" />
                                                                </center>
                                                            </div>
                                                            <div class="col-md-6"></div>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnadd" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function DisableButton() {
            var btn = document.getElementById("<%=btnadd.ClientID %>");
            btn.value = 'Please wait...';
            document.getElementById("<%=btnadd.ClientID %>").disabled = true;
            document.getElementById("<%=btnadd.ClientID %>").classList.add("dissablebtn");
        }
        window.onbeforeunload = DisableButton;
    </script>
</asp:Content>
