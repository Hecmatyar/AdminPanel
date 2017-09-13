$(function () {
    function fillComment() {
        let idPost = $('#Id').attr("data-id");
        console.log("id: " + idPost);
        var form = $('#__AjaxAntiForgeryForm');
        var token = $('input[name="__RequestVerificationToken"]', form).val();
        $.ajax({
            url: '/Home/GetCommentFromPost',
            type: 'POST',
            data: {
                __RequestVerificationToken: token,
                idPost: idPost
            },
            dataType: 'HTML',
            success: function (data) {
                $("#comments").html(data);
            },
            error: function (data) {
                console.log("big error" + data);
            }
        });
    }
    $(function () {
        if ($("#comments").length !== 0) {
            fillComment();
        }
        return false;
    });
    $("#comments").on('click', ".title-right", function () {
        let parentId = $(this).parent().parent().children(".parent-comment").attr("data-id");
        let parentAuthor = $(this).parent().parent().children(".parent-author").attr("data-name");
        //console.log(parentId);
        $("#ParentId").attr("data-id", parentId);
        $(".comment-body").val("@" + parentAuthor + "#" + parentId + ", ").focus();
    });

    //$("#sendCommentForm").on("submit", function (event) {
    //    console.log("отправляет");

    //});
    $(".send-comment").on('click', function () {
        if ($("#comment-body").val().length > 0) {
            var form = $('#__AjaxAntiForgeryForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            //var form = $('#sendCommentForm').serialize();
            //console.log(JSON.stringify(form));
            let frmValues = {
                parentId: $("#ParentId").attr("data-id"),
                body: $("#comment-body").val(),
                postId: $("#PostId").attr("data-id")
            };
            console.log(frmValues);
            $.ajax({
                url: '/Home/SendComment',
                type: 'POST',
                data: {
                    __RequestVerificationToken: token,
                    comment: frmValues
                },
                success: function (data) {
                    console.log(data);
                    fillComment();
                    $("#comment-body").val("");
                },
                error: function (data) {
                    console.log("big error" + data);
                }
            });
        }
    });
});