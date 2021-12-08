$(document).ready(function () {
    debugger;
    $.ajax({
        type: "GET",
        url: '/Home/Register',
        success: function () {
            debugger;
            alert("yea boy");
        }
    })
});