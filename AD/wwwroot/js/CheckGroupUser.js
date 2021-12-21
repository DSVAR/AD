$(document).ready(function () {
    $(".hides").hide();

    $.ajax({
        url: "/Home/GetGroup",
        method: "POST",
        success: function (data) {
            let json = JSON.parse(data);

            if (json["CodeStatus"] == 200) {
                $(".hides").show();
            }

            if (json["CodeStatus"] == 404) {
                location.reload();
            }
        }

    });

})