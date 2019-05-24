$(document).ready(function () {

    // Begin Region Customer   

    var ResetCustomerForm = function () {
        $("#customer_form")[0].reset();
    }

    var GetCustomerByPhone = function (phone) {

        if (phone.length < 4) {
            return;
        }        

        $.ajax({
            dataType: "json",
            type: "GET",
            url: ("/Customer/GetCustomerByPhone/"),
            data: { phone: phone },
            success: function (customer) {                
                SetCustomer(customer);
            },
            error: function (result) {
                ResetCustomerForm();
            }
        });
    };

    var SetCustomer = function (customer) {

        ResetCustomerForm();        

        var i;
        for (i in customer) {
            $(("#" + i)).val(customer[i]);
        }

        for (i in customer["CustomerAddress"]) {            

            if (customer["CustomerAddress"][i]["enabled"] == 1) {

                var j;
                for (j in customer["CustomerAddress"][i]) {                    
                    $(("#" + j)).val(customer["CustomerAddress"][i][j]);
                }                    
            }
        }        
    };    

    $("#phone").blur(function () {        
        GetCustomerByPhone($("#phone").val());
    });        
     
    // End Region Customer.
});