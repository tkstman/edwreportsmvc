$(function () {
    $('#perfrep').on('click', function () {
        $(this).addClass('active');
        $("#edwrep").removeClass('active');
        if ($(this).hasClass('collapsed')) {
            $('myCollapsible').removeClass("collapsed");
            $('.collapse').collapse('show');
            $('myCollapsible').attr("aria-expanded", "true");

            console.log("show");
        }
        else {
            $('myCollapsible').addClass("collapsed");            
            $('.collapse').collapse('hide');
            $('myCollapsible').attr("aria-expanded", "false");
            console.log("hide");
        }
    });

    $('#edwrep').on('click', function () {
        $(this).addClass('active');
        $('#perfrep').removeClass('active');
        if ($(this).hasClass('collapsed')) {
            $('myCollapsible').removeClass("collapsed");
            $('.collapse').collapse('show');
            $('myCollapsible').attr("aria-expanded", "true");

            console.log("show");
        }
        else {
            $('myCollapsible').addClass("collapsed");            
            $('.collapse').collapse('hide');
            $('myCollapsible').attr("aria-expanded", "false");
            console.log("hide");
        }
    })

});