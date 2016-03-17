$(document).ready(function () {

    // Define o checkbox enabled.
    var boolean = ((parseInt($("#enabled").val()) == 1) ? true : false);
    $("#enabled-ckb").prop('checked', boolean);

    // Evento acionado ao clicar na checkbox enabled.
    $("#enabled-ckb").change(function (e) {

        var enabled = ($("#enabled-ckb").is(':checked') ? 1 : 0);
        $("#enabled").val(enabled);
    });

    // Evento é acionado quando o CEP perde o FOCUS.
    $("#zip_code").blur(function (e) {

        // Obtém o CEP.
        var zip_code = $("#zip_code").val();

        $.ajax({
            dataType: "json",
            type: "GET",
            url: ("http://cep.republicavirtual.com.br/web_cep.php?cep=" + zip_code + "&formato=json"),
            success: function (result) {

                if (result["resultado"] == "1") {

                    // Define os valores de endereço.
                    $("#address").val((result["tipo_logradouro"] + " " + result["logradouro"]));
                    $("#city").val(result["cidade"]);
                    $("#neighborhood").val(result["bairro"]);
                    $("#acronym_city").val(result["uf"]);

                    // Monta o endereço.
                    var address = $("#address").val() + ', ' + $("#number").val() + ', ' + $("#neighborhood").val() + ', ' + $("#city").val() + ', ' + $("#acronym_city").val();

                    // Busca as coordenadas.
                    var geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'address': address }, function (results, status) {
                        if (status === google.maps.GeocoderStatus.OK) {
                            var coordinates = results[0].geometry.location;
                            $("#latitude").val(coordinates.lat());
                            $("#longitude").val(coordinates.lng());
                        }
                    });
                }
            }
        });
    });
});