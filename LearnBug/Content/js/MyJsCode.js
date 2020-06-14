function bookmark(postId) {
    $.ajax({
        url: "/Bookmark/CreateOrDeleteBookmark",
        data: { postId: postId },
        type: "Post",
        error: function () {
            alert("خظا!")
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
        error: function () { alert("خظا!") }

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
        success: function () {
            document.getElementById("SendMessage").value = ""
        }
    })
}
