<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentWiseOAReport.aspx.cs" Inherits="Admin_DepartmentWiseOAReport" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERP Excel Enclosure</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="#" />
    <meta name="keywords" content="Admin" />
    <meta name="author" content="#" />
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

        .divblock {
            text-align: end;
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
                max-width: 1250px !important;
            }
        }

        .selected_row {
            background-color: #A1DCF2;
        }

        .divStatus1 {
            width: 1px;
            height: 1px;
            padding: 10px;
            border: 1px solid gray;
            margin: 0;
        }

        .clsMargin {
            margin-left: -4%;
        }
    </style>
    <style>
        /* Existing styles remain unchanged */

        /*body { background-image: url('http://localhost:55079/img/excelenclosures.jpg'); /* Replace with the actual path to your image */ background-position: center center; background-repeat: no-repeat; background-attachment: fixed; /* This will make the background fixed when scrolling */ background-color: #060606; /* Adjust the last value (0.5) for opacity (0 to 1) */ background-size: cover;
        }
        */
    </style>
    <style>
         /* Responsive styles for mobile */
        @media only screen and (max-width: 600px) {
            .col-md-3 {
                width: 100% !important;
                margin-bottom: 10px;
            }

            .col-md-12 {
                width: 100% !important;
            }
        }
    </style>








    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,600" rel="stylesheet" />
    <link href="../files/bower_components/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../files/assets/icon/feather/css/feather.css" rel="stylesheet" />
    <link href="../files/assets/css/style.css" rel="stylesheet" />
    <link href="../files/assets/css/jquery.mCustomScrollbar.css" rel="stylesheet" />
    <script>
        // Your JavaScript code goes here
        function pageLoad() {
            $(document).ready(function () {
                $('.myDate').datepicker({
                    // Datepicker configuration
                });
                $('.myDate').datepicker('setDate', new Date());
                $('.ui-datepicker').hide();
            });
        }
    </script>
</head>


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
            DivHR.appendChild(tbl.cloneNode(true));

        }
    }

    function OnScrollDiv(Scrollablediv) {
        document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
        document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
    }
</script>


<form id="form1" runat="server">
    <div class="page-wrapper">
        <div class="page-body">
            <div class="container py-3">
                <div class="card">
                    <div class="card-header bg-primary text-uppercase text-white">
                        <div class="row">
                            <div class="col-md-4">
                                <h5>Track Your Order Report</h5>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12 col-md-12">
                            <div class="card">
                                <div class="card-header">

                                    <%--<div class="row my-md-4 ">
                                        <div class="spancls " style="margin-left: 15px; font-weight: bold;">Customer Name:</div>
                                        <div class="col-md-4" >
                                            <asp:label id="txtcname" runat="server" text="label"></asp:label>
                                        </div>
                                        <div class="spancls mt-2" style="margin-left: 15px; font-weight: bold;">Customer Name:</div>
                                        <div class="col-md-4">
                                            <asp:label id="lbloaNumber" runat="server" text="label"></asp:label>
                                        </div>
                                    </div>--%>



                                    <div class=" row">
                                        <div class="" style="font-weight: bold; margin-left: 10px;">
                                            <strong>Customer Name:</strong>
                                        </div>
                                        <div class="col-md-2 col-lg-3">
                                            <asp:label id="txtcname" runat="server" text="label" style="color: black; margin-top: 2px;"></asp:label>
                                        </div>

                                        <div class="mt-2 mt-md-0" font-weight: bold style=" margin-left: 10px;">
                                            <strong>OA Number:</strong>
                                        </div>
                                        <div class="col-md-2 col-lg-3">
                                            <asp:label id="lbloaNumber" runat="server" text="label" style="color: black; margin-top: 2px;"></asp:label>
                                        </div>
                                        <div class="mt-2 mt-md-0" font-weight: bold style=" margin-left: 10px;">
                                            <strong>PO Number:</strong>
                                        </div>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:label id="lblpo" runat="server" text="label" style="color: black; margin-top: 2px;"> </asp:label>
                                        </div>




                                    </div>
                                    <br />
                                    <div class=" row">
                                    </div>


                                    <div class="col-md-12" style="padding: 0px; margin-top: 10px;">
                                        <div id="DivRoot" align="left">
                                            <div style="overflow: hidden;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:gridview id="GvReports" runat="server" cssclass="table" headerstyle-backcolor="#009999" autogeneratecolumns="false" style="max-height: 300px; overflow-y: auto; text-align: centerl">
                                                    <Columns>
                                                          <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="68" ItemStyle-HorizontalAlign="Center" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                        <asp:BoundField DataField="Size" HeaderText="Size" />
                                                        <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" />
                                                            <asp:BoundField DataField="Department" HeaderText="Current Status" />
                                                        <%--<asp:BoundField DataField="InwardQty" HeaderText="Inward Quantity" />
                                                        <asp:BoundField DataField="OutwardQty" HeaderText="Outward Quantity" />--%>
                                                    </Columns>
                                                </asp:gridview>
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
</form>
</html>
