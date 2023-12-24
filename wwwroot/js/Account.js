
$(document).ready(function () {

    $("#deposit").click(function () {
        debugger;
        var amount = $("#amount").val();
        var data = e.currentTarget.id.split("myBtn_");
        value = data[1];
        var id = document.getElementById("id_" + value + "");

        $.ajax({
            type: 'POST',
            url: '/Home/Deposit',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: { id: id, amount : amount },
            success: function (data) {
                location.reload();
                $('#result-container').html('POST request successful. Response: ' + JSON.stringify(data));
            },
            error: function (error) {
                $('#result-container').html('POST request failed. Error: ' + JSON.stringify(error));
            }
        });
    });
})


$("#withdraw").click(function () {
    debugger;
    var amount = $("#amount").val();

});