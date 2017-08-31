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
});