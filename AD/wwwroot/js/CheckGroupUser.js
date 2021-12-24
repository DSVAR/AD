$(document).ready(function () {
    $(".hides").hide();
    $.ajax({
        data: {
          //  'group': 'swer',
            groups:["web","swe","цоб-WEB"]
        },
        url: "/Home/GetGroup",
        method: "POST",
        success: function (data) {
            let json = JSON.parse(data);
            console.log(data);
            if (json["CodeStatus"] == 200) {
                $(".hides").show();
            }

            if (json["CodeStatus"] == 404) {
                location.reload();
            }
        }

    });

})