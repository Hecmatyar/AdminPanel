$(function () {
    $('.page-sidebar-menu li ul a').each(function () {
        var pathname = window.location.pathname.split('/'); //получаем необходимое свойство текущей ссылки       

        //console.log(pathname[pathname.length - 1]);

        var thislink = $(this).attr('href'); //получаем значение атрибута href для ссылок меню
        //если значения идентичный - присваиваем новый класс 
        if (thislink.toLowerCase() == pathname[pathname.length - 1].toLowerCase()) {
            $(this).parent().addClass('active open');
            $(this).parent().parent().parent().addClass('active open');
        } else {
            $(this).parent().removeClass('active open');
        }
    });

    $(".page-content").on('change', "#file-upload", function () {
        preview(this);
    });
    function preview(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#preview').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $(".page-content").on('click', ".check", function (e) {
        $(this).toggleClass("default");
        $(this).toggleClass("green");
    });
    $(".page-content").on('click', ".cancel", function (e) {
        $(".back-popup").removeClass("flex");
        console.log("hide");
    });

    function fillPopup(href, currid) {
        $.ajax({
            url: href,
            type: 'POST',
            data: { id: currid },
            dataType: 'HTML',
            success: function (data) {
                $(".back-popup").addClass("flex");
                $("#\\#popup").html(data);
            },
            error: function (data) {
                alert(data);
            }
        });
    }
    $(function () {
        $(".page-content").on("click", ".reset-popup", function () {
            let href = this.href;
            let currid = $(this).attr("iduser");
            fillPopup(href, currid);
            return false;
        });

        $(".page-content").on("click", ".role-popup", function () {
            let href = this.href;
            let currid = $(this).attr("iduser");
            fillPopup(href, currid);
            return false;
        });
        $(".page-content").on("click", ".edit-popup", function () {
            let href = this.href;
            let currid = $(this).attr("iduser");
            fillPopup(href, currid);
            return false;
        });
        function UpdateTable(href, currpage, search) {
            $.ajax({
                url: href,
                type: 'POST',
                data: {
                    page: currpage,
                    searchString: search
                },
                dataType: 'HTML',
                success: function (data) {
                    $("#usereTable").html(data);
                    console.log("update");
                },
                error: function (data) {
                    alert(data);
                }
            });
        }
        $(".page-content").on("click", ".page", function () {
            let href = this.href;
            let currpage = $(this).attr("page");
            sessvars.page = currpage;
            UpdateTable(href, currpage, "");
            return false;
        });
        $(".tools").on("click", ".reload", function () {
            let href = this.href;
            let currpage = sessvars.page;
            UpdateTable(href, currpage, "");
            return false;
        });
    });
    $(function () {        
        if ($("#usereTable").length != 0) {

            if (sessvars.page == 'undefined')
                sessvars.page = 1;
            console.log(sessvars.page);
            $.ajax({
                url: '/Admin/Users/UserTable',
                type: 'POST',
                data: {
                    page: sessvars.page,
                    searchString: ""
                },
                dataType: 'HTML',
                success: function (data) {                   
                    $("#usereTable").html(data);
                },
                error: function (data) {
                    alert(data);
                }
            });
            return false;
        }
        
    });

    //$(function () {
    //    $("#\\#NewUserPassword").hide();
    //});
    //$('#\\#reset').on("click", function (e) {
    //    e.preventDefault();
    //    ResetPassword();
    //    return false;
    //});
    //$('#\\#cancelreset').on("click", function (e) {
    //    e.preventDefault();
    //    CancelResetPassword();
    //    return false;
    //});
    //function ResetPassword() {
    //    console.log("reset pass");
    //    $("#\\#NewUserPassword").show();
    //    $("#\\#cancelreset").show();
    //    $("#\\#reset").hide();
    //}
    //function CancelResetPassword() {
    //    $("#\\#NewUserPassword").hide();
    //    $("#\\#cancelreset").hide();
    //    $("#\\#reset").show();
    //}
});