        ��  ��                  �B      �����e                 

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
                max-width: 1250px !important;
            }
        }

        .selected_row {
            background-color: #A1DCF2;
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

    <script type="text/javascript">
        function ShowPopup() {
            $("#myModal").modal("show");
        }
    </script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {
                        row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Approve?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }


        function CheckedCheckbox(ob) {
            var checkboxval = ob.checked;
            if (checkboxval == true) {
                $('#btnshowhide').show();
            }
            else {
                $('#btnshowhide').hide();
            }

            var grid = document.getElementById("");
            var checkBoxes = grid.getElementsByTagName("INPUT");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    $('#btnshowhide').show();
                }
            }
        }
        function Linkclicked(txt) {

            var grid = document.getElementById("");
            var row = txt.parentNode.parentNode;
            var rowIndex = row.rowIndex;
            //var Userid = row.cells[3].innerHTML;
            var suboanumber = grid.rows[rowIndex].cells[3].childNodes[1].childNodes[0].data;
            var InwardQty = grid.rows[rowIndex].cells[7].childNodes[1].value;

            $("#").val(suboanumber);

            $('#divReturn').show();

        }

        function Keydown(txt) {
            alert("Inward Quantity will change after sending quantity to Next Stage.");
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../img/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
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
      <style>
        .lblsuboa {
            display: none;
        }
    </style>
').value;

            if (isNaN(txtqty1) || txtqty1 == "") { txtqty1 = 0; }
            if (isNaN(txtrate1) || txtrate1 == "") { txtrate1 = 0; }
            if (isNaN(cgst1) || cgst1 == "") { cgst1 = 0; }
            if (isNaN(sgst1) || sgst1 == "") { sgst1 = 0; }
            if (isNaN(igst1) || igst1 == "") { igst1 = 0; }

            if (isNaN(cgstamt1) || cgstamt1 == "") { cgstamt1 = 0; }
            if (isNaN(sgstamt1) || sgstamt1 == "") { sgstamt1 = 0; }
            if (isNaN(igstamt1) || igstamt1 == "") { igstamt1 = 0; }

            if (isNaN(txtdisc1) || txtdisc1 == "") { txtdisc1 = 0; }
            if (isNaN(txtAmt1) || txtAmt1 == "") { txtAmt1 = 0; }

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('').value = igstamt1.toFixed(2); }

            var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            var total1 = result1 - (discAmt + totGST1);
            if (!isNaN(result1)) { document.getElementById('').value;

            if (isNaN(txtqty2) || txtqty2 == "") { txtqty2 = 0; }
            if (isNaN(txtrate2) || txtrate2 == "") { txtrate2 = 0; }
            if (isNaN(cgst2) || cgst2 == "") { cgst2 = 0; }
            if (isNaN(sgst2) || sgst2 == "") { sgst2 = 0; }
            if (isNaN(igst2) || igst2 == "") { igst2 = 0; }
            if (isNaN(txtdisc2) || txtdisc2 == "") { txtdisc2 = 0; }
            if (isNaN(txtAmt2) || txtAmt2 == "") { txtAmt2 = 0; }

            var result1 = parsefloat(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('').value = igstamt1.toFixed(2); }

            var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            var total1 = result1 - (discAmt + totGST1);
            if (!isNaN(result1)) { document.getElementById('').value = total1.toFixed(2); }
        }
        //    //2nd Row Calculation 
        //    var txtqty2 = document.getElementById('ContentPlaceHolder1_txtQty2').value;
        //    var txtrate2 = document.getElementById('ContentPlaceHolder1_txtRate2').value;
        //    var txtdisc2 = document.getElementById('ContentPlaceHolder1_txtdisc2').value;
        //    var txtAmt2 = document.getElementById('ContentPlaceHolder1_txtAmt2').value;

        //    if (isNaN(txtqty2) || txtqty2 == "") { txtqty2 = 0; }
        //    if (isNaN(txtrate2) || txtrate2 == "") { txtrate2 = 0; }
        //    if (isNaN(txtdisc2) || txtdisc2 == "") { txtdisc2 = 0; }
        //    if (isNaN(txtAmt2) || txtAmt2 == "") { txtAmt2 = 0; }

        //    var result2 = parseInt(txtqty2) * parseFloat(txtrate2);
        //    var discAmt = (result2 * txtdisc2) / 100;
        //    var total2 = result2 - discAmt;
        //    if (!isNaN(result2)) { document.getElementById('ContentPlaceHolder1_txtAmt2').value = total2.toFixed(2); }

        //    //3 Row Calculation 
        //    var txtqty3 = document.getElementById('ContentPlaceHolder1_txtQty3').value;
        //    var txtrate3 = document.getElementById('ContentPlaceHolder1_txtRate3').value;
        //    var txtdisc3 = document.getElementById('ContentPlaceHolder1_txtdisc3').value;
        //    var txtAmt3 = document.getElementById('ContentPlaceHolder1_txtAmt3').value;

        //    if (isNaN(txtqty3) || txtqty3 == "") { txtqty3 = 0; }
        //    if (isNaN(txtrate3) || txtrate3 == "") { txtrate3 = 0; }
        //    if (isNaN(txtdisc3) || txtdisc3 == "") { txtdisc3 = 0; }
        //    if (isNaN(txtAmt3) || txtAmt3 == "") { txtAmt3 = 0; }

        //    var result3 = parseInt(txtqty3) * parseFloat(txtrate3);
        //    var discAmt = (result3 * txtdisc3) / 100;
        //    var total3 = result3 - discAmt;
        //    if (!isNaN(result3)) { document.getElementById('ContentPlaceHolder1_txtAmt3').value = total3.toFixed(2); }

        //    //4 Row Calculation 
        //    var txtqty4 = document.getElementById('ContentPlaceHolder1_txtQty4').value;
        //    var txtrate4 = document.getElementById('ContentPlaceHolder1_txtRate4').value;
        //    var txtdisc4 = document.getElementById('ContentPlaceHolder1_txtdisc4').value;
        //    var txtAmt4 = document.getElementById('ContentPlaceHolder1_txtAmt4').value;

        //    if (isNaN(txtqty4) || txtqty4 == "") { txtqty4 = 0; }
        //    if (isNaN(txtrate4) || txtrate4 == "") { txtrate4 = 0; }
        //    if (isNaN(txtdisc4) || txtdisc4 == "") { txtdisc4 = 0; }
        //    if (isNaN(txtAmt4) || txtAmt4 == "") { txtAmt4 = 0; }

        //    var result4 = parseInt(txtqty4) * parseFloat(txtrate4);
        //    var discAmt = (result4 * txtdisc4) / 100;
        //    var total4 = result4 - discAmt;
        //    if (!isNaN(result4)) { document.getElementById('ContentPlaceHolder1_txtAmt4').value = total4.toFixed(2); }

        //    //5 Row Calculation 
        //    var txtqty5 = document.getElementById('ContentPlaceHolder1_txtQty5').value;
        //    var txtrate5 = document.getElementById('ContentPlaceHolder1_txtRate5').value;
        //    var txtdisc5 = document.getElementById('ContentPlaceHolder1_txtdisc5').value;
        //    var txtAmt5 = document.getElementById('ContentPlaceHolder1_txtAmt5').value;

        //    if (isNaN(txtqty5) || txtqty5 == "") { txtqty5 = 0; }
        //    if (isNaN(txtrate5) || txtrate5 == "") { txtrate5 = 0; }
        //    if (isNaN(txtdisc5) || txtdisc5 == "") { txtdisc5 = 0; }
        //    if (isNaN(txtAmt5) || txtAmt5 == "") { txtAmt5 = 0; }

        //    var result5 = parseInt(txtqty5) * parseFloat(txtrate5);
        //    var discAmt = (result5 * txtdisc5) / 100;
        //    var total5 = result5 - discAmt;
        //    if (!isNaN(result5)) { document.getElementById('ContentPlaceHolder1_txtAmt5').value = total5.toFixed(2); }
        //}
    </script>

    <script type="text/javascript">
        function DisableButton() {
            var btn = document.getElementById("