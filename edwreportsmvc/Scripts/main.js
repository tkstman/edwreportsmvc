$(function () {
    $('#perfrep').on('click', function () {
        $(this).addClass('active');
        $("#edwrep").removeClass('active');
        if ($(this).hasClass('collapsed')) {
            $('myCollapsible').removeClass("collapsed");
            $('.collapse').collapse('show');
            $('myCollapsible').attr("aria-expanded", "true");

            $.ajax({
                url: "/Home/Constituencies",
                method: "get",
                dataType:"json",
                success: function (data) {
                    console.log(Object.keys(data).length);
                    loadConstituencies(data, "constituencies")
                    console.log("Load Data Into Constituency Dropdown");
                },
                error: function (data,status,text)
                {
                    alert("an error occurred |" + status + "| " + text);
                    console.log(data);
                }
            });
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
    });

    function loadConstituencies(constituencies,attributes)
    {
        var result = "";
        console.log($("#constituencybtn").siblings(".dropdown-menu"));
        for (x = 0; x < Object.keys(constituencies).length; x++)
        {
            result += '<button type="button" class="dropdown-item btn-sm" data-attribute="'
                + Object.keys(constituencies)[x].toString() + '" id="' + attributes + 'checkbox'
                + Object.keys(constituencies)[x].toString() + '" name="' + attributes + 'checkbox' + Object.keys(constituencies)[x].toString()
                + '">' + constituencies[Object.keys(constituencies)[x]] + '</button>';

            if(x < Object.keys(constituencies).length-1)
            {
                result += '<div class="dropdown-divider"></div>';
            }
        }
        console.log(result);

        $("#constituencybtn").siblings(".dropdown-menu").append(result);
        
    }

    $('.dropdown-menu').on('click', 'button', function () {
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
        //$(this).parent().prev().attr('data-attribute',$(this).attr('data-attribute'));

        console.log($(this).parent().prev().text(($(this).text())));
        console.log("dropdown option selected");
    });

    //$('#droppy').on('hidden.bs.dropdown', function () {
    //    alert('test');
    //    // do something...
    //})


});