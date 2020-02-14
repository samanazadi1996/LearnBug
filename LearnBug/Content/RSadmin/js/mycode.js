$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip();

    $('.group-items').hover(function(){
        var class_name = $(this).attr('class').trim();
        if(class_name == 'group-items')
        {
            $(this).children('.sub-item').stop().slideDown();
        }
    },function(){
        var class_name = $(this).attr('class').trim();
        if(class_name == 'group-items')
        {
            $(this).children('.sub-item').stop().slideUp();
        }
    });
});