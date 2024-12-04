<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Termsandmasterlist.aspx.cs" Inherits="Admin_Termsandmasterlist" MasterPageFile="~/Admin/AdminMasterPage.master" %>

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
                max-width: 100% !important;
            }
        }
    </style>
    <script src="../JS/jquery.min.js"></script>
    <script language="javascript" type="text/javascript">

        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {

            $('#btnshowhide').hide();

            var tbl = document.getElementById(gridId);
            if (tbl) {
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
    <div class="page-wrapper">
        <div class="page-body">

            <div class="row">
                <div class="col-md-7">
                </div>


                <div class="container py-3">
                    <div class="card">
                        <div class="col-md-12">
                            <div class="page-header-breadcrumb">
                                <div style="float: right; margin: 3px; margin-bottom: 5px;">
                                    <span id="btnAddCompany" runat="server"><a href="Termasandmaster.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Add Terms</a>&nbsp;&nbsp;
                                <%--<a href="UploadCompanyData.aspx" style="font-size: 16px; border: 1px dashed gray; padding: 4px;">&nbsp;Upload Excel</a>--%>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="card-header bg-primary text-uppercase text-white">
                            <div class="row">
                                <div class="col-md-4">
                                    <h5>Terms & Master Lists</h5>
                                </div>
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-12 col-md-12">
                                <div class="card">
                                    <div class="card-header">
                                        <div class="col-md-12" style="padding: 30px;">
                                            <div id="DivRoot" align="left">
                                                <div style="overflow: hidden;" id="DivHeaderRow">
                                                </div>
                                                <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                    <asp:GridView ID="GvTCList" runat="server"
                                                        CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                        DataKeyNames="id" AllowPaging="false" ShowHeader="true" PageSize="50">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ref. No." ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrefno" runat="server" Text='<%# Eval("RefNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Freight & Insurance charges" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFreight" runat="server" Text='<%# Eval("Frightinsurance") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Packing Charge">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPackungCharge" runat="server" Text='<%# Eval("PackungCharge") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inspection">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInspection" runat="server" Text='<%# Eval("Inspection") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Damages">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDamages" runat="server" Text='<%# Eval("Damages") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="After sales Service">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblafterSalesServicess" runat="server" Text='<%# Eval("afterSalesServices") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Warranty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblafterSalesServicess" runat="server" Text='<%# Eval("Warranty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Validity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblafterSalesServicess" runat="server" Text='<%# Eval("Validity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Jurisdiction">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblJurisdiction" runat="server" Text='<%# Eval("Jurisdiction") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Special Note:">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblJurisdiction" runat="server" Text='<%# Eval("[Special Note]") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-CssClass="gvhead" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" Height="27px" CausesValidation="false" CommandName="RowEdit">Edit</asp:LinkButton>
                                                                    &nbsp;
                                                                   <asp:LinkButton ID="btnDelete" runat="server" Height="27px" ToolTip="Delete" CausesValidation="false" CommandName="RowDelete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CommandArgument='<%#Eval("ID")%>'>Delete</asp:LinkButton>
                                                                    &nbsp;
                                            <%--<asp:LinkButton runat="server" ID="btnpdfview" ToolTip="View Quotation PDF" CommandName="RowView" CommandArgument='<%# Eval("Quotationno") %>'><i class="fas fa-file-pdf"  style="font-size: 26px; color:red; "></i></i></asp:LinkButton>--%>

                                                                    <%--<a href="../Admin/Pdf_Quotation.aspx?Quotationno=<%#Eval("Quotationno")%>" target="_blank"><asp:Label ID="Label3" Style="padding: 5px 3px !important; margin-top: 0px; color: white;" Height="35px" runat="server" Text="Print" Font-Size="15px"><i class="fas fa-file-pdf"  style="font-size: 26px; color:red; "></i></asp:Label></span></a>  --%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                    <div id="DivFooterRow" style="overflow: hidden">
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>


                    </div>
                </div>
            </div>
        </div>
</asp:Content>


