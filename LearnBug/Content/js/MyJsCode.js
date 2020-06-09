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
