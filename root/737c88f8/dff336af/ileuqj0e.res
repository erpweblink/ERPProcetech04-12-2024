        ��  ��                  v      �����e                 ').value = '0';
            }

            if (isNaN(txtqty) || txtqty == "") { txtqty1 = 0; }
            if (isNaN(txtprice) || txtprice == "") { txtprice = 0; }
            if (isNaN(cgst1) || cgst1 == "") { cgst1 = 0; }
            if (isNaN(sgst1) || sgst1 == "") { sgst1 = 0; }
            if (isNaN(igst1) || igst1 == "") { igst1 = 0; }

            var result1 = parseInt(txtqty) * parseFloat(txtprice);
            if (!isNaN(result1)) { document.getElementById('").classList.add("dissablebtn");
        }
        window.onbeforeunload = DisableButton;
    </script>

    <script type="text/javascript">
        function isNumber(evt) {

            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;

            var qty = document.getElementById('').value;

            alert(qty);

            //var total1 = qty * billingrate;
            //var total2 = qty * salerate;

            //var amt1 = total1;
            //var amt2 = total2;

            //var SGSTamt = salerate * sgst / 100;
            //var CGSTamt = salerate * cgst / 100;

            //var IGSTamt = salerate * igst / 100;

            //var GSTtotal = SGSTamt + CGSTamt;
            //var NetAmt = salerate + GSTtotal;



            //var abc = amt2.toFixed(2);

            return true;
        };


    </script>

