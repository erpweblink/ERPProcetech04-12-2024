<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="TestCertificate.aspx.cs" Inherits="Admin_TestCertificate" %>

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

        .row {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="updatepnl" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-md-7">
                        </div>
                        <div class="col-md-5">
                            <div class="page-header-breadcrumb">
                                <div style="float: right; margin: 3px; margin-bottom: 5px; display: none;">
                                    <span>
                                        <a href="#" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Certificate List</a>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container-fluid">
                        <div class="card">
                            <div class="card-header bg-primary text-uppercase text-white">
                                <h5>Create Test Certificate</h5>
                            </div>
                            <div class="row">
                                <div class="col-xl-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-4 spancls">Company Name<i class="reqcls">*&nbsp;</i> : </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCustomerName" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 spancls">OA Numbers : </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOANumbers" OnSelectedIndexChanged="ddlOANumbers_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-2 spancls">Kind Att : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtkindatt" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 spancls">PO : </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtPoNo" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 spancls">Category : </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategory">
                                                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                            <asp:ListItem Value="IP-42">IP-42</asp:ListItem>
                                                            <asp:ListItem Value="IP-44">IP-44</asp:ListItem>
															<asp:ListItem Value="IP-54">IP-54</asp:ListItem>
                                                            <asp:ListItem Value="IP-55">IP-55</asp:ListItem>
                                                            <asp:ListItem Value="IP-65">IP-65</asp:ListItem>
															<asp:ListItem Value="IP-66">IP-66</asp:ListItem>
                                                            <asp:ListItem Value="IP-67">IP-67</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-4 spancls">Shade : </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtShade"></asp:TextBox>
                                                        <%-- <asp:DropDownList runat="server" CssClass="form-control" ID="ddlShade">
                                                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                            <asp:ListItem Value="RAL7032">RAL7032</asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-4 spancls">Coating Thickness : </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlcoatingThickness" AutoPostBack="true" OnTextChanged="ddlcoatingThickness_TextChanged">
                                                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                            <asp:ListItem Value="60-80">60-80</asp:ListItem>
                                                            <asp:ListItem Value="80-110">80-110</asp:ListItem>
                                                            <asp:ListItem Value="110-150">110-150</asp:ListItem>
                                                            <asp:ListItem Value="Specify">Specify</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                </div>
                                            </div>

                                            <div class="col-md-6" runat="server">
                                                <div class="row">
                                                    <div class="col-md-4 spancls">Is Stainless Steel: </div>
                                                    <div class="col-md-8">
                                                        <asp:CheckBox runat="server" ID="chkIsStainlesssteel" AutoPostBack="true" OnCheckedChanged="chkIsStainlesssteel_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                </div>
                                            </div>
                                            <div class="col-md-6" runat="server">
                                                <div class="row">
                                                </div>
                                                <div class="row" id="divSpecificThickness" runat="server" visible="false">
                                                    <div class="col-md-4 spancls">Specific Thickness: </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSpecificthickness"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6" runat="server" id="SS1" visible="false">
                                                <div class="row">
                                                    <div class="col-md-4 spancls">Stainless Steel Shade : </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtStainlessSteelShade"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                </div>
                                            </div>

                                            <div class="col-md-6" runat="server" id="SS2" visible="false">
                                                <div class="row">
                                                    <div class="col-md-4 spancls">Surface Finish : </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtBuffingFinish"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                </div>
                                            </div>

                                            <div class="col-md-6" runat="server">
                                                <div class="row">
                                                    <div class="col-md-4 spancls">Remarks : </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRemarks" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <table class="table" border="1" style="width: 100%; border: 1px solid #0c7d38;">
                                                                <tr style="background-color: #7ad2d4; color: #000; font-weight: 600; text-align: center;">
                                                                    <td>Sub OA Number</td>
                                                                    <td>Enclosure Size</td>
                                                                    <td style="width: 50px;">Quantity</td>
                                                                    <td style="width: 50px;">Action</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSubOanumber" OnSelectedIndexChanged="ddlSubOanumber_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSize" CssClass="form-control" Width="100%" runat="server" TextMode="MultiLine" ReadOnly="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQty" Width="100%" CssClass="form-control" runat="server" ReadOnly="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnAdd" CssClass="btn btn-warning btn-sm btncss" OnClick="Insert" runat="server" Text="Add More" />
                                                                        <asp:Button ID="btnreset" CssClass="btn btn-danger btn-sm btncss" OnClick="Reset" runat="server" Text="Reset" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-8 text-center">

                                                    <div class="table">
                                                        <asp:GridView ID="dgvSubOADtl" runat="server" CssClass="table " HeaderStyle-BackColor="#009999" ClientIDMode="Static" AutoGenerateColumns="false"
                                                            EmptyDataText="No records has been added.">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sub OA Number" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsubOanumber" runat="Server" Text='<%# Eval("SubOANumber") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Size" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSize" runat="Server" Text='<%# Eval("Size") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuantity" runat="Server" Text='<%# Eval("Qty") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" ToolTip="Delete" CausesValidation="false"><i class="fa fa-trash-o" style="font-size:28px;color:red"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-md-2"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4"></div>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-primary" OnClick="btnGenerate_Click" Text="Generate Certificate" />
                                            </div>
                                            <div class="col-md-4"></div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

