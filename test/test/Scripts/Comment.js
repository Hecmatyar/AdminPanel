$(function () {
    if ($("#comments").length !== 0) {        
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
                console.log(data);
                $("#comments").html(data);
            },
            error: function (data) {
                console.log("big error" + data);  
            }
        });
        return false;
    }   
});