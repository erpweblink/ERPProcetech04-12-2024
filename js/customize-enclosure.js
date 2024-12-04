/* Author: Mattias Bengtsson, Insytec AB (2015) */

$(document).ready(function() {
	// Validate that distributor-location is provided before enabling the continue-button
	var validateDistribLoc = function() {
		var valid = !!$("#custenc-distributorlocation select").val();
		if (valid)
			$("#custenc-distributorlocation .graphbutton").removeClass("disabled");
	};
	validateDistribLoc();
	$("#custenc-distributorlocation select").change(function() {
		validateDistribLoc();
	});
	$("#custenc-distributorlocation .graphbutton a").click(function() {
		return !$(this).parent().hasClass("disabled");
	});

	// Display throbber when selecting enclosure
	$("#custenc-enclosure select").change(function(e) {
		$("#custenc-enclosure .throbber").show();
		$("#custenc-features, #custenc-specification, #custenc-continue").hide();
	});

	// Add/remove feature
	$("#custenc-features select").change(function(e) {
		$("#custenc-features .graphbutton").removeClass("disabled");
	});
	$("#custenc-features .graphbutton a").click(function(e) {
		var sku = $("#addfeature-id").val();
		if (!sku)
			return false;
		var name = $("#addfeature-id option:selected").data("name");
		var price = $("#addfeature-id option:selected").data("price");
		var qty = $("#addfeature-qty").val();
		if (!$.isNumeric(qty))
			return false;
		$.get("index.html", { add: sku, qty: qty }, function(data) {
			$("#custenc-features table").removeClass("empty");
			var existingRows = $.grep($("#custenc-features table tr"), function(x) {
				return $(x).data("sku") == sku;
			});
			if (existingRows.length) {
				var qtyCell = $(existingRows[0]).find(".qty");
				qtyCell.html(parseInt(qtyCell.text()) + parseInt(qty));
			} else {
				var newRow = $("<tr><td class=\"name\"></td><td class=\"qty\"></td><td class=\"price\"></td><td class=\"remove\"><span>Remove</span></td></tr>");
				newRow.data("sku", sku);
				newRow.find(".name").html(sku + (name.length == 0 ? "" : ": " + name));
				newRow.find(".qty").html(qty);
				newRow.find(".price").html(price);
				$("#custenc-features table").append(newRow);
			}
		})
		.error(function(event, request, settings) {
			console.log("Ajax request error");
		});
		return false;
	});
	$("#custenc-features").on("click", ".remove", function(e) {
		var row = $(this).closest("tr");
		$.get("index.html", { remove: row.data("sku") }, function(data) {
			row.remove();
			if ($("#custenc-features table tr").length == 1)
				$("#custenc-features table").addClass("empty");
			})
		.error(function(event, request, settings) {
			console.log("Ajax request error");
		});
	});

	// Validate that specification is provided before enabling the continue-button
	var validateSpecification = function() {
		var valid = $("#custenc-specification input:checked").length == 1;
		if (valid)
			$("#custenc-continue .graphbutton").removeClass("disabled");
	};
	validateSpecification();
	$("#custenc-specification input").click(function() {
		validateSpecification();
	});
	$("#custenc-continue .graphbutton a").click(function() {
		return !$(this).parent().hasClass("disabled");
	});

	// Display throbber when clicking continue-button
	$("#custenc-continue a").click(function(e) {
		if (!$(this).parent().hasClass("disabled"))
			$("#custenc-continue .throbber").show();
	});

	// Validate form
	$("#custenc-final a").click(function() {
		var isValid = validateForm($("#custenc-form"));
		if (isValid) {
			$("#custenc-final .throbber").show();

			// Google Analytics event: customization sent
			sendGaEvent('Request', 'Customization');
		}
		return isValid;
	});
});
