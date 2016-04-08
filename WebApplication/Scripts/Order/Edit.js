$(document).ready(function () {

    var CalculateFinalPrice = function () {
        try {
            var price = (parseFloat(String($("#price").val()).replace(",", ".")).toFixed(2) * 100);
            var discount = (parseFloat(String($("#discount").val()).replace(",", ".")).toFixed(2) * 100);
            var delivery_price = (parseFloat(String($("#delivery_price").val()).replace(",", ".")).toFixed(2) * 100);
            var final_price = String(((price - discount + delivery_price) * 0.01).toFixed(2));
            $("#final_price").val(final_price);
        } catch (e) {
            $("#final_price").val("0.00");
        }
    };

    var CalculateChange = function () {
        try {
            var final_price = (parseFloat(String($("#final_price").val()).replace(",", ".")).toFixed(2) * 100);
            var payment = (parseFloat(String($("#payment").val()).replace(",", ".")).toFixed(2) * 100);
            var change = (((payment - final_price) * 0.01)).toFixed(2);
            change = ((change < 0) ? "0.00" : String(change));            
            $("#change").val(change);            

        } catch (e) {
            $("#change").val("0.00");
        }
    };

    $("#price").change(function (e) {
        CalculateFinalPrice();
        CalculateChange();
    });

    $("#discount").change(function (e) {
        CalculateFinalPrice();
        CalculateChange();
    });

    $("#delivery_price").change(function (e) {
        CalculateFinalPrice();
        CalculateChange();
    });

    $("#payment").change(function (e) {
        CalculateChange();
    });
});