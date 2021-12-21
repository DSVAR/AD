$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: '/Home/Register',
        success: function (data) {
            var json = JSON.parse(data);


            if (json["CodeStatus"] == 201) {
                //alert("Ea");
            }
            else {
                alert("Не удалось определить пользователя");
                console.log(json);
            }
        }
    })
});