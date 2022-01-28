$(document).ready(function(){   
    // Attach CSRF token to the header of all AJAX post/put/delete requests
    var e = $('input[name="__RequestVerificationToken"]').val();
    $(document).ajaxSend(function (t, a, i) {
        a && i && ("POST" === i.type || "PUT" === i.type || "DELETE" === i.type) && a.setRequestHeader("RequestVerificationToken", e);
    });
});