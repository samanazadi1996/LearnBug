function bookmark(postId) {
    $.ajax({
        url: "/Bookmark/CreateOrDeleteBookmark",
        data: { postId: postId },
        type: "Post",
        error: function () {
            toastr.error("خظا!")
        }
    })
}
function deleteComment(id) {
    $.ajax({
        url: "/Comment/deleteComment",
        data: { id: id },
        type: "Post",
        success: function (result) {
            eval(result)
        },
        error: function () { toastr.error("خظا!") }

    });
}
function BuyPost(PostId) {
    debugger
    $.ajax({
        url: "/Factor/CreateFactor",
        data: { Id: PostId },
        type: "Post",
        datatype: "Json",
        success: function (result) {
            if (result.Success) {
                document.getElementById("div-body").innerHTML = result.Html
            }
            eval(result.Script)
        }
    })
}
function SendMessage(userid) {
    $.ajax({
        url: "/Message/SendMessage",
        data: { text: document.getElementById('TextMessage').value, to: userid },
        type: "Post",
        dataType: "Json",
        success: function (data) {
            document.getElementById("TextMessage").value = ""
            eval(data.msg)
        }
    })
}
function follow(id) {
    $.ajax({
        url: "/Follow/AddOrDeleteFollow",
        data: { id: id },
        type: "Post",
        success: function (data) {
            var namebtn = "follow_" + id
            document.getElementById(namebtn).innerHTML = data
        }
    })
}
function SendComment() {
    event.preventDefault()
    $.ajax({
        url: "/Comment/SendComment",
        data: $("#sendCommentForm").serialize(),
        type: "Post",
        datatype: "Json",
        success: function (data) {
            if (data.success) {
                document.getElementById('comments').innerHTML = data.html
            }
            eval(data.message)
        },
        error: function () {
            toastr.warning("خطا")
        }
    })
}
function ChangePassword() {
    event.preventDefault()
    $.ajax({
        url: "/User/ChangePassword",
        data: $("#FormChangePassword").serialize(),
        type: "Post",
        datatype: "Json",
        success: function (data) {
            debugger
            if (data.success) {
                document.getElementById("bodypartial").innerHTML = ""
            } else {
                document.getElementById("OldPassword").focus()
                document.getElementById("OldPassword").value = ""
            }
            eval(data.message)
        },
        error: function () {
            toastr.warning("خطا")
        }
    })
}
function renderpage(a) {
    $.ajax({
        url: a,
        success: function (data) {
            $('#bodypartial').html(data)
        }
    })
}
function changeProfilePicture(type) {
    var img = $("#upload-Preview").attr('src')
    $.ajax({
        url: "/User/changeProfilePicture",
        data: { newPicture: img, type: type },
        type: "Post"
    })
    $('#bodypartial').html('')
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

