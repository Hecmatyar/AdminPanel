$(function () {
    $(".page-content").on('click', ".cancel", function (e) {
        $(".back-popup").removeClass("flex");
    });
        
    //заполнение модальных окон
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
    $(".page-content").on("click", ".edit-category-popup", function () {
        let href = this.href;
        let currid = $(this).attr("idCategory");
        fillPopup(href, currid);
        return false;
    });
    $(".page-content").on("click", ".create-category-popup", function () {        
        let href = this.href;
        let currid = 0;
        fillPopup(href, currid);
        return false;
    });
});