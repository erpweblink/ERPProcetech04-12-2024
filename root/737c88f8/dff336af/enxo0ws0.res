        ��  ��                  �      �����e                 ').value;

            if (isNaN(txtqty1) || txtqty1 == "") { txtqty1 = 0; }
            if (isNaN(txtrate1) || txtrate1 == "") { txtrate1 = 0; }

            if (taxation == 'inmah') {
                if (isNaN(cgst1) || cgst1 == "") {
                    cgst1 = 9;
                    $("#").val('0');
            }
            else {
                alert('Please Select Taxation');
            }

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

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('').value = igstamt1.toFixed(2); }

            var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            //var total1 = result1 - (discAmt + totGST1);
            //var total1 = result1 + totGST1 - discAmt;
            var total1 = result1 - discAmt;
            if (!isNaN(result1)) { document.getElementById('').value = total1.toFixed(2); }

            $('.myDate').datepicker('setDate', new Date());
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