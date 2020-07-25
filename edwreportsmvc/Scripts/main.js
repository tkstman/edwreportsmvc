$(function () {
    $('#perfrep').on('click', function () {
        $(this).addClass('active');
        $("#edwrep").removeClass('active');
        if ($(this).hasClass('collapsed')) {
            $('myCollapsible').removeClass("collapsed");
            $('.collapse').collapse('show');
            $('myCollapsible').attr("aria-expanded", "true");

            /**
            IF DROP DOWN IS EMPTY THEN POPULATE IT WITH CONSTITUENCIES
            **/
            if($("#constituencybtn").siblings(".dropdown-menu").children('.dropdown-item').length===0){

                $.ajax({
                    url: "/Home/Constituencies",
                    method: "get",
                    dataType:"json",
                    success: function (data) {                        
                        loadDropDown(data,$("#constituencybtn"), "constituencies");
                    },
                    error: function (data,status,text)
                    {
                        alert("an error occurred |" + status + "| " + text);
                        console.log(data);
                    }
                });
            }
        }
        else {
            $('myCollapsible').addClass("collapsed");            
            $('.collapse').collapse('hide');
            $('myCollapsible').attr("aria-expanded", "false");
        }
    });

    $('#edwrep').on('click', function () {
        $(this).addClass('active');
        $('#perfrep').removeClass('active');
        if ($(this).hasClass('collapsed')) {
            $('myCollapsible').removeClass("collapsed");
            $('.collapse').collapse('show');
            $('myCollapsible').attr("aria-expanded", "true");
        }
        else {
            $('myCollapsible').addClass("collapsed");
            $('.collapse').collapse('hide');
            $('myCollapsible').attr("aria-expanded", "false");
        }
    });

    /**
    *Load Returned Values into dropdown
    *@param {string} dataObject Json Object Returned From Database.
    *@param {object} eleBtn The Main Button Related To the Dropdown.
    *@param {string} attributes A Description Of the current Dropdown Context.    
    **/
    function loadDropDown(dataObject,eleBtn ,attributes) {

        var keys = Object.keys( dataObject);
        var vals = Object.values( dataObject);
        var valssorted = Object.values( dataObject).sort();

        var result = (attributes === 'constituencies') ? '<button type="button" class="dropdown-item btn-sm" data-attribute="All" id="constituenciescheckboxAll" name="constituenciescheckboxAll">ALL CONSTITUENCIES</button>'
                    +'<div class="dropdown-divider"></div>': '';
        var cnter=0;
        for (var sortedValue = 0; sortedValue < valssorted.length; sortedValue++) {

            result += '<button type="button" class="dropdown-item btn-sm" data-attribute="'
                + Object.keys( dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '" id="' + attributes + 'checkbox'
                + Object.keys( dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '" name="' + attributes + 'checkbox' + Object.keys( dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString()
                + '">' + valssorted[sortedValue] + '</button>';

            if (cnter < Object.keys( dataObject).length-1) {
                result += '<div class="dropdown-divider"></div>';
            }
            cnter++;
        }
        eleBtn.siblings(".dropdown-menu").append(result);
    }

    /**
    Put Selected Dropdown List Item at the top of the list
    **/
    $('.dropdown-menu').on('click', 'button', function () {
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
        $(this).parent().prev().attr('data-attribute',$(this).attr('data-attribute'));
        $(this).parent().prev().text(($(this).text()));        
    });


    function loadAppType(dataObject, eleBtn, attributes) {

        var keys = Object.keys(dataObject);
        var vals = Object.values(dataObject);
        console.log(vals);
        console.log(keys);
        console.log(Object.keys((dataObject[keys[0]])) );
        var valssorted = Object.values(dataObject).sort();

        var result = (attributes === 'constituencies') ? '<button type="button" class="dropdown-item btn-sm" data-attribute="All" id="constituenciescheckboxAll" name="constituenciescheckboxAll">ALL CONSTITUENCIES</button>'
                    + '<div class="dropdown-divider"></div>' : '';
        var cnter = 0;
        for (var sortedValue = 0; sortedValue < valssorted.length; sortedValue++) {

            result += '<button type="button" class="dropdown-item btn-sm" data-attribute="'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '" id="' + attributes + 'checkbox'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '" name="' + attributes + 'checkbox' + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString()
                + '">' + valssorted[sortedValue].APP_TYPE + '</button>';

            if (cnter < Object.keys(dataObject).length - 1) {
                result += '<div class="dropdown-divider"></div>';
            }
            cnter++;
        }
        eleBtn.siblings(".dropdown-menu").append(result);
    }

    $('.dropdown-menu.constituency').on('click', 'button', function () {
        if ($('.dropdown-toggle.applicationType').hasClass('disabled'))
        {
            //Remove Disabled Feature and add application types
            $('.dropdown-toggle.applicationType').removeClass('disabled');

            $.ajax({
                url: '/Home/ApplicationTypes',
                method: "get",
                dataType: "json",
                success: function (data) {
                    loadAppType(data, $(".dropdown-toggle.applicationType"), "applicationType");
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                    console.log(status);
                    console.log(error);
                    alert("Failed To Load Worker Types")
                }
            });
        }
    });

    $('.dropdown-menu.applicationType').on('click', 'button', function () {
        console.log("button clicked");
        if ($('.dropdown-toggle.workerType').hasClass('disabled')) {
            //Remove Disabled Feature and add worker types
            $('.dropdown-toggle.workerType').removeClass('disabled');

            $.ajax({
                url: '/Home/WorkerTypes',
                method: "get",
                dataType: "json",
                success: function (data) {
                    loadAppType(data, $(".dropdown-toggle.workerType"), "workerType");
                },
                error: function (xhr, status, error) {
                    console.log(xhr);
                    console.log(status);
                    console.log(error);
                    alert("Failed To Load Status Types")
                }
            });
        }
    });

    $('.dropdown-menu.workerType').on('click', 'button', function () {
        console.log("button clicked");
        if ($('.dropdown-toggle.status').hasClass('disabled')) {
            //Remove Disabled Feature and add worker types
            $('.dropdown-toggle.status').removeClass('disabled');
        }
    });

    $('.dropdown-menu.workerType').on('click', 'button', function () {
        console.log("button clicked");
        if ($('.dropdown-toggle.status').hasClass('disabled')) {
            //Remove Disabled Feature and add worker types
            $('.dropdown-toggle.status').removeClass('disabled');
        }
    });

    //$('#droppy').on('hidden.bs.dropdown', function () {
    //    alert('test');
    //    // do something...
    //})


});