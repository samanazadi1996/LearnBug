function UpdateBlockUser(id) {
    $.ajax({
        url: "/Admin/User/UpdateBlock",
        data: { id: id },
        type: "Post",
        datatype:"Json",
        success: function (data) {
            var namebtn = "Block_" + id
            document.getElementById(namebtn).innerHTML = data.str
            eval(data.script)
        }
    })
}

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "3000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}