        ��  ��                  
      �����e                 ').value;

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

            //if (isNaN(txtdisc1) || txtdisc1 == "") { txtdisc1 = 0; }
            if (isNaN(txtAmt1) || txtAmt1 == "") { txtAmt1 = 0; }

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('').value = igstamt1.toFixed(2); }

            // var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            var total1 = result1 + totGST1;
            if (!isNaN(result1)) { document.getElementById('').value; //For Discount

            if (isNaN(txtDiscount) || txtDiscount == "") {
                txtDiscount = 0;
            }
            if (isNaN(txtqty2) || txtqty2 == "") { txtqty2 = 0; }
            if (isNaN(txtrate2) || txtrate2 == "") { txtrate2 = 0; }
            if (isNaN(cgst2) || cgst2 == "") { cgst2 = 0; }
            if (isNaN(sgst2) || sgst2 == "") { sgst2 = 0; }
            if (isNaN(igst2) || igst2 == "") { igst2 = 0; }
            //if (isNaN(txtdisc2) || txtdisc2 == "") { txtdisc2 = 0; }
            if (isNaN(txtAmt2) || txtAmt2 == "") { txtAmt2 = 0; }

            var result1 = parseInt(txtqty1) * parseFloat(txtrate1);
            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

            if (!isNaN(cgstamt1)) { document.getElementById('').value = igstamt1.toFixed(2); }


            //var discAmt = (result1 * txtdisc1) / 100;
            var totGST1 = cgstamt1 + sgstamt1 + igstamt1;
            //var total1 = result1 - (discAmt + totGST1);

            var total1 = result1 + totGST1;

            var discounted_price = total1 - (total1 * txtDiscount / 100)

            if (!isNaN(result1)) { document.getElementById('