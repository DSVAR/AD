$(document).ready(function () {
    $(".hides").hide();
    console.log("122");
    $.ajax({
        data: {
          //  'group': 'swer',
            groups:["1","2","цоб-WEB"]
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