function deleteContent(me,ContentId) {

    if (confirm('مطلب حذف شود؟') == true) {
        $.ajax({
            url: "/Content/DeleteContent",
            data: { id: ContentId },
            type: "Post",
            error: function () { alert("خظا!"); }
        })
    }
}
function AddBookMark(me, ContentId) {
    var w = me.attributes['class'].value;
    var typ = true
    if (w == "fa-bookmark") {
        me.attributes['class'].value = "fa-bookmark-o"
        typ = false
    } else {
        me.attributes['class'].value = "fa-bookmark"
        typ = true
    }
    $.ajax({
        url: "/Bookmark/AddBookmark",
        data: { ContentId: ContentId, type: typ },
        type: "Post",
        datatype: "Json",
        error: function () { alert("خظا!") }
    })
}
function togNav() {
    if (document.getElementById("mySidenav").style.width == "0px") {
        document.getElementById("mySidenav").style.width = "250px"
        document.getElementById("main").style.paddingRight = "250px";

    } else {
        document.getElementById("mySidenav").style.width = "0"
        document.getElementById("main").style.paddingRight = "0px";

    }
}
