$(function () {
    if ($('#tag-area').length > 0) {
        $.ajax({
            url: '/Moderator/Posts/GetTagEditor',
            type: 'POST',
            data: {
                edit: false,
                idPost: 0
            },
            success: function (data) {               
                $('#tag-area').tagEditor({
                    autocomplete: { delay: 0, position: { collision: 'flip' }, source: data },
                    forceLowercase: false,
                    placeholder: 'Тэги поста ...'
                });
            },
            error: function (data) {
                alert(data);
            }
        });
        return false;   
    }
    if ($('#tag-area-edit').length > 0) {
        let idpost = $("#Id").attr("value");       
        $.ajax({
            url: '/Moderator/Posts/GetTagEditor',
            type: 'POST',
            data: {
                edit: true,
                idpost: idpost
            },
            success: function (data) {                
                $('#tag-area-edit').tagEditor({
                    initialTags: data[0],
                    autocomplete: { delay: 0, position: { collision: 'flip' }, source: data[1] },
                    forceLowercase: false,
                    placeholder: 'Тэги поста ...'
                });
            },
            error: function (data) {
                alert(data);
            }
        });
        return false;
    }
   
});