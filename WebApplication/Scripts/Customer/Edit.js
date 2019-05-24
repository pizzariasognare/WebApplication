$(document).ready(function () {

    // Define o checkbox enabled.
    var boolean = ((parseInt($("#enabled").val()) == 1) ? true : false);
    $("#enabled-ckb").prop('checked', boolean);

    // Evento acionado ao clicar na checkbox enabled.
    $("#enabled-ckb").change(function (e) {

        var enabled = ($("#enabled-ckb").is(':checked') ? 1 : 0);
        $("#enabled").val(enabled);
    });

});