$(document).ready(function () {

    $.ajax({
        type: 'GET',
        url: '/Main/Index")',
        success: function (result) {
            var r = result;
            console.log(result);
        }
    })
});