        ��  ��                  c      �����e                 ').value;

            if (isNaN(txtqty) || txtqty == "") { txtqty1 = 0; }
            if (isNaN(txtprice) || txtprice == "") { txtprice = 0; }

            var result1 = parseInt(txtqty) * parseFloat(txtprice);
            if (!isNaN(result1)) { document.getElementById('').value = result1.toFixed(2); }

            cgstamt1 = (result1 * cgst1) / 100;
            sgstamt1 = (result1 * sgst1) / 100;
            igstamt1 = (result1 * igst1) / 100;

        }
    </script>

    <script type="text/javascript">
        function DisableButton() {
            var btn = document.getElementById("