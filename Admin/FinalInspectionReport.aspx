<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalInspectionReport.aspx.cs" Inherits="Admin_FinalInspectionReport" MasterPageFile="~/Admin/AdminMasterPage.master" %>

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

    <style type="text/css">
        .cal_Theme1 .ajax__calendar_container {
            background-color: #DEF1F4;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #ffffff;
            margin-bottom: 4px;
        }

        .cal_Theme1 .ajax__calendar_title,
        .cal_Theme1 .ajax__calendar_next,
        .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .cal_Theme1 .ajax__calendar_body {
            background-color: #ffffff;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: #004080;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #004080;
            text-align: center;
        }

        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
        .cal_Theme1 .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #DEF1F4;
        }

        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
            font-size: 10px;
        }

        .cal_Theme1 .ajax__calendar_other,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
        }

        .ajax__calendar_body {
            height: 158px !important;
            width: 220px !important;
            position: relative;
            overflow: hidden;
            margin: 0 0 0 -5px !important;
        }

        .ajax__calendar_container {
            padding: 4px;
            cursor: default;
            width: 220px !important;
            font-size: 11px;
            text-align: center;
            font-family: tahoma,verdana,helvetica;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #004080;
            font-size: 14px;
            text-align: center;
        }

        .ajax__calendar_day {
            height: 22px !important;
            width: 27px !important;
            text-align: right;
            padding: 0 14px !important;
            cursor: pointer;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            margin-left: 12px !important;
            color: #004080;
        }

        .ajax__calendar_year {
            height: 50px !important;
            width: 51px !important;
            font-weight: bold;
            text-align: center;
            cursor: pointer;
            overflow: hidden;
            color: #004080;
        }

        .ajax__calendar_month {
            height: 50px !important;
            width: 51px !important;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            overflow: hidden;
            color: #004080;
        }

        .grid tr:hover {
            background-color: #d4f0fa;
        }

        .pcoded[theme-layout="vertical"][vertical-nav-type="expanded"] .pcoded-header .pcoded-left-header, .pcoded[theme-layout="vertical"][vertical-nav-type="expanded"] .pcoded-navbar {
            width: 210px;
        }
    </style>


    <style>
        .row {
            margin-top: 10px;
        }
    </style>
    <style>
        .bold-big {
            font-weight: bold;
            font-size: 24px; /* Adjust the size as needed */
        }
    </style>
    <script type='text/javascript'>
        function scrollToElement() {
            var target = document.getElementById("divdtls").offsetTop;
            window.scrollTo(0, target);
        }
    </script>
    <script type='text/javascript'>
        function scrollToElement() {
            var target = document.getElementById("divdtls1").offsetTop;
            window.scrollTo(0, target);
        }
    </script>

    <style>
        .small-image {
            width: 70px; /* or whatever size you prefer */
            height: 100px; /* maintain the aspect ratio */
        }
    </style>


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(document).ready(function () {
                $('.myDate').datepicker({
                    dateFormat: 'dd-mm-yy',
                    inline: true,
                    showOtherMonths: true,
                    changeMonth: true,
                    changeYear: true,
                    constrainInput: true,
                    firstDay: 1,
                    navigationAsDateFormat: true,
                    showAnim: "fold",
                    yearRange: "c-100:c+10",
                    dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
                });
                $('.myDate').datepicker('setDate', new Date());
                $('.ui-datepicker').hide();
            });
        }
    </script>

    <script type='text/javascript'>
        function openModal() {
            $('[id*=myModal]').modal('show');
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="col-md-12">
                        <div class="container-fluid">
                            <div class="card">
                                <div class="card-header bg-primary text-uppercase text-white">
                                    <%--<h5>Final Inspection Report</h5>--%>
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <img src="../img/ProcetechLogoclr.jpg" class="small-image" />
                                            </div>
                                            <div class="col-md-5">
                                                <h2 style="color: black">Procetech Automation Pvt.Ltd.</h2>
                                                <br />
                                                <h5 style="color: black;"margin-left: 150px;">Final Inspection Report</h5>
                                            </div>
                                            <div class="col-md-4">
                                                <h5>Fomat No.-</h5>
                                                <br />
                                                <h5>PAPL/QA/F/09</h5>
                                                <br />
                                                <h5>Rev No.-00</h5>
                                                <br />
                                                <h5>Rev Date-</h5>
                                                <br />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                       
                         <div class="card-body">
                                                                <div class="row">
                                            <div class="col-md-12">
                                                <asp:HiddenField ID="hiddeninvoiceno" runat="server" />
                                                <asp:HiddenField ID="hidden1" runat="server" />
                                                <div class="row">
                                              
                                                    <div class="col-md-2 spancls">Procetech Jo<i class="reqcls">&nbsp;*</i> :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtjo" CssClass="form-control" runat="server" Width="100%" AutoComplete="off"></asp:TextBox>
                                                    </div>

                                                       <div class="col-md-2 spancls">Customer Name<i class="reqcls">&nbsp;*</i> :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtcustomer" CssClass="form-control" runat="server" Width="100%" AutoComplete="off"></asp:TextBox>
                                               <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                            TargetControlID="txtcustomer">
                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>

                                                <br />
                                                      <div class="row">
                                              
                                                    <div class="col-md-2 spancls">Po Number<i class="reqcls">&nbsp;*</i> :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtpo" CssClass="form-control" runat="server" Width="100%" AutoComplete="off"></asp:TextBox>
                                                    </div>

                                                       <div class="col-md-2 spancls">Project<i class="reqcls">&nbsp;*</i> :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtproject" CssClass="form-control" runat="server" Width="100%" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                              
                                                    <div class="col-md-2 spancls">Item<i class="reqcls">&nbsp;*</i> :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtitem" CssClass="form-control" runat="server" Width="100%" AutoComplete="off"></asp:TextBox>
                                                    </div>

                                                       <div class="col-md-2 spancls">Qty<i class="reqcls">&nbsp;*</i> :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtQty" CssClass="form-control" runat="server" Width="100%" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <br />
                                                                   <div class="row">
                                              
                                                    <div class="col-md-2 spancls">Drag No.<i class="reqcls">&nbsp;*</i> :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtdrag" CssClass="form-control" runat="server" Width="100%" AutoComplete="off"></asp:TextBox>
                                                    </div>

                                                       <div class="col-md-2 spancls">Date<i class="reqcls">&nbsp;*</i> :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtdate" CssClass="form-control" runat="server" Width="100%"   TextMode="Date"></asp:TextBox>
                                                    </div>
                                                </div>


                                        </div>
                        </div>

                            <br />
                            <br />
                                  
                                           <div class="table-responsive" id="manuallytable" runat="server">

                                               <table class="table" id="tableData" runat="server">
  <thead>
    <tr>
      <th scope="col">Sr No.</th>
      <th scope="col">Test / Check</th>
      <th scope="col">Type of Check</th>
      <th scope="col">Specification</th>
      <th scope="col">Observstion</th>
      <th scope="col">Result</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">A</th>
      <td  id="BareEnc" runat="server">Bare Encloure</td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
    </tr>
    <tr>
      <th scope="row"></th>
      <td  id="overalldimensioncheck" runat="server">Overall Dimentional Check</td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
    </tr>
    <tr>
      <th scope="row"></th>
      <td id="BEHEIGHT" runat="server">Height</td>
      <td>Measure</td>
      <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="chekckHeightBE" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="txtheightobservationBE" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
           <td style="text-align:center">
          <asp:TextBox ID="txtheightresult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <th scope="row"></th>
      <td id="BEWIDTH" runat="server">Width</td>
      <td>Measure</td>
          <td style="text-align:center">
            <asp:CheckBox runat="server" ID="txtwidthcheck" CssClass="bold-big" />
      </td>
      <td style="text-align:center">
                 <asp:TextBox ID="txtwidthobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
         <td style="text-align:center">
                 <asp:TextBox ID="txwidthresult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
         <tr>
      <th scope="row"></th>
      <td id="BEDEPTH" runat="server">Depth</td>
      <td>Measure</td>
          <td style="text-align:center">
            <asp:CheckBox runat="server" ID="chkBEDEPTH" CssClass="bold-big" />
      </td>
       <td style="text-align:center">
                 <asp:TextBox ID="txtDepthobser" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
       <td style="text-align:center">
            <asp:TextBox ID="txtdepthresult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
               <tr>
      <th scope="row"></th>
      <td  id="BECUTOUT" runat="server">Cut Out</td>
      <td>Visual</td>
   <td style="text-align:center">
            <asp:CheckBox runat="server" ID="CheckBECUTOUT" CssClass="bold-big" />
      </td>
     <td style="text-align:center">
                 <asp:TextBox ID="txtcutoutobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
     <td style="text-align:center">
       <asp:TextBox ID="txtcutoutresult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>

                     <tr>
      <th scope="row"></th>
      <td id="PaintShade" runat="server">Paint Shade</td>
      <td>Visual</td>
           <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="checkPaintshade" CssClass="bold-big" />
            </td>
         <td style="text-align:center">
          <asp:TextBox ID="txtpainshadeobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="txtpaintresultpainshit" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>


      
      <tr>
      <th scope="row"></th>
      <td id="PaintThickness" runat="server">Paint Thickness</td>
      <td>MEASURE</td>
           <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="checkThickness" CssClass="bold-big" />
            </td>
     <td style="text-align:center">
          <asp:TextBox ID="txtobservationThickness" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
       <td style="text-align:center">
          <asp:TextBox ID="txtresultThickness" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>

<%--      B section--%>
         <tr>
      <th scope="row">B</th>
      <td  id="InernalAssembky" runat="server">Inernal Assembky</td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
    </tr>
          <tr>
      <th scope="row"></th>
      <td id="MountingPlate" runat="server">Mounting Plate</td>
      <td>VISUAL</td>
      <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="checkMountingplate" CssClass="bold-big" />
            </td>
     <td style="text-align:center">
          <asp:TextBox ID="txtmountionobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="txtresultmountiong" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>

          <tr>
      <th scope="row"></th>
      <td id ="WireSupport" runat="server">Wire Support</td>
      <td>VISUAL</td>
          <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="checkWireSupport" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="txtWireSupportobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="txtWireSupportresult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>



             <tr>
      <th scope="row"></th>
      <td id="DrawingPacket" runat="server">Drawing Packet</td>
      <td>VISUAL</td>
          <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="checkDrawingPacket" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="txtobservationDrawingPacket" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="txtresultDrawingPacket" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>


             <tr>
      <th scope="row"></th>
      <td id="WireSupport1" runat="server">Wire Support</td>
      <td>VISUAL</td>
            <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="checkWireSupport1" CssClass="bold-big"  runat="server"  />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBox19" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBox20" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>


                   <tr>
      <th scope="row"></th>
      <td id="SwitchBrackett" runat="server">Switch Bracket</td>
      <td>VISUAL</td>
            <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxSwitchBrackett" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="txtobservationSwitchBrackett" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="txtresultSwitchBrackett" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
                   <tr>   
      <th scope="row"></th>
      <td  id="AlignmentofDoors" >Alignment of Doors</td>
      <td>VISUAL</td>
          <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxAlignmentofDoors" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="txtobservationAlignmentofDoors" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="txtresulttxtobservationAlignmentofDoors" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>



                         <tr>
      <th scope="row"></th>
      <td id="EArthingBolt" runat="server">EArthing Bolt/Solt</td>
      <td>VISUAL</td>
     <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxEArthingBolt" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="txtobservationEArthingBolt" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="txtresultEArthingBolt" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>

      
                         <tr>
      <th scope="row"></th>
      <td id="Liftingarrangement" runat="server"> Lifting Arrangment</td>
      <td>VISUAL</td>
          <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="checkLiftingarrangement" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="txtobervationLiftingarrangement" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="txtresultLiftingarrangement" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>


                       <tr>
      <th scope="row"></th>
      <td id="Gasket" runat="server">Gasket</td>
      <td>VISUAL</td>
        <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxGasket" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxGasketobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxresultGasket" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
                       <tr>
      <th scope="row"></th>
      <td runat="server" id="Hardware">Hardware</td>
      <td>VISUAL</td>
         <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxHardware" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxHardwareobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxHardwareResult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>

       <tr>
      <th scope="row"></th>
      <td  id="Glandplate">Gland plate</td>
      <td>VISUAL</td>
      <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxGlandplate" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxGlandplateobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxGlandplateresult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>

<%--c  section--%>
        
      <tr>
      <th scope="row">C</th>
      <td  id="IngressProtectionclass"> Ingress  Protection  class </td>
      <td>IP</td>
      <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxIngressProtectionclass" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxIngressProtectionclassobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxIngressProtectionclassResult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
      <%--D  section--%>
              <tr>
      <th scope="row">D</th>
      <td  id="VisualInspection" runat="server"> Visual Inspection</td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
    </tr>

            <tr>
      <th scope="row"></th>
      <td id="Scratech" runat="server">Scratech/Burr</td>
      <td>VISUAL</td>
          <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxVisualInspection" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxVisualInspectionobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxVisualInspectionResult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
  
  
        <%--E  section--%>
              <tr>
      <th scope="row">E</th>
      <td  id="CleaningPacking" runat="server"> Cleaning & Packing</td>
      <td>Visual</td>
           <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxCleaningPacking" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxCleaningPackingobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxCleaningPackingResult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
  
              <%--F  section--%>
              <tr>
      <th scope="row">F</th>
      <td  id="Wiring" runat="server"> Wiring</td>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
    </tr>
              <tr>
      <th scope="row"></th>
      <td  id="WiringDresssing" runat="server"> Wiring Dresssing </td>
      <td></td>
           <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxWiringWiringDresssing" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxWiringDresssingobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxWiringDresssingResult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>
  
                    <tr>
      <th scope="row"></th>
      <td  id="AccessabilityofTBAndWires" runat="server"> Accessability of TB And Wires</td>
      <td></td>
         <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxAccessabilityofTBAndWires" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxAccessabilityofTBAndWiresobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxAccessabilityofTBAndWiresResult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>

    <tr>
      <th scope="row"></th>
      <td  id="SizesandColourofwires" runat="server"> Sizes and  Colour of wires</td>
      <td></td>
      <td style="text-align:center;">
                <asp:CheckBox runat="server" ID="CheckBoxSizesandColourofwires" CssClass="bold-big" />
            </td>
      <td style="text-align:center">
          <asp:TextBox ID="TextBoxSizesandColourofwiresobservation" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
          <td style="text-align:center">
          <asp:TextBox ID="TextBoxSizesandColourofwiresResult" runat="server" TextMode="MultiLine"></asp:TextBox>
      </td>
    </tr>

  
  </tbody>
</table>

     </div>
</div>

                                <div class="card-footer">
                        <div class="col-md-12"  style="text-align:center">
                        <asp:Button ID="txtsubmit" runat="server" CssClass="btn btn-success" Text="Submit" Width="120px"  OnClick="txtsubmit_Click" />
                        </div>
                                    </div>
                                      </div>
                            </div>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


