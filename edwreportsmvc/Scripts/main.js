$(function () {
    $('#perfrep').on('click', function () {
        $(this).addClass('active');
        $("#edwrep").removeClass('active');
        if ($(this).hasClass('collapsed')) {
            clearReportIframe();
            //CLEAR SELECTED INPUTS IN collapseExample
            $('#collapseExample').find('input[type="checkbox"]').prop('checked', false);
            $('#collapseExample').find('input[type="date"]').prop('disabled', 'disabled');
            $('#collapseExample').find('input[type="date"]').val('');
            verifyDropDownSelection();
            removeGenerate();
            /*
                COLLAPSE EDW ENTRY OPTIONS
            */
            $('.myCollapsible.edwrepbtn').addClass("collapsed");
            $('.collapse.edwrepbtn').collapse('hide');
            $('.myCollapsible.edwrepbtn').attr("aria-expanded", "false");

            /*
                DISPLAY PRODUCTIVITY OPTIONS
            */
            $('.myCollapsible.perfbtn').removeClass("collapsed");
            $('.collapse.perf').collapse('show');
            $('.myCollapsible.perfbtn').attr("aria-expanded", "true");            
        }
        //else {
            
        //}
    });

    $('#edwrep').on('click', function () {
        $(this).addClass('active');
        $('#perfrep').removeClass('active');
        if ($(this).hasClass('collapsed')) {
            clearReportIframe();
            //CLEAR SELECTED INPUTS IN collapseExamplePerf
            $('#collapseExamplePerf').find('input[type="checkbox"]').prop('checked', false);
            $('#collapseExamplePerf').find('input[type="date"]').prop('disabled', 'disabled');
            $('#collapseExamplePerf').find('input[type="date"]').val('');
            removeGenerate();

            /*
                COLLAPSE PRODUCTIVITY OPTIONS
            */
            $('.myCollapsible.perfbtn').addClass("collapsed");
            $('.collapse.perf').collapse('hide');
            $('.myCollapsible.perfbtn').attr("aria-expanded", "false");


            /*
                DISPLAY EDW ENTRY OPTIONS
            */
            $('.myCollapsible.edwrepbtn').removeClass("collapsed");
            $('.collapse.edwrepbtn').collapse('show');
            $('.myCollapsible.edwrepbtn').attr("aria-expanded", "true");

            /**
            IF DROP DOWN IS EMPTY THEN POPULATE IT WITH CONSTITUENCIES
            **/
            if ($("#constituencybtn").siblings(".dropdown-menu").children('.dropdown-item').length === 0) {

                $.ajax({
                    url: "../edwreports/Home/Constituencies",
                    method: "get",
                    dataType: "json",
                    success: function (data) {
                        loadDropDown(data, $("#constituencybtn"), "constituencies");
                    },
                    error: function (data, status, text) {
                        alert("an error occurred |" + status + "| " + text);
                    }
                });
            }
        }
        //else {
        //    $('.myCollapsible').addClass("collapsed");
        //    $('.collapse').collapse('hide');
        //    $('.myCollapsible').attr("aria-expanded", "false");
        //}
    });

    /***
    ****
    **** DATE TIME PROCESSING
    ****
    ****/

    /**
    Enable and disable Date Elements For Entry Report
    */
    $('#entryspecifystartdate').on('click', function () {
        if ($('#entrystartdate').attr('disabled')) {
            $('#entrystartdate').removeAttr('disabled');
        }
        else { $('#entrystartdate').val(''); $('#entrystartdate').attr('disabled', 'disabled'); }

    });

    $('#entryspecifyenddate').on('click', function () {
        if ($('#entryenddate').attr('disabled')) {
            $('#entryenddate').removeAttr('disabled');
        }
        else { $('#entryenddate').val(''); $('#entryenddate').attr('disabled', 'disabled'); }
    });


    /**
    Enable and disable Date Elements For Productivity Report
    */
    $('#perfspecifystartdate').on('click', function () {
        if ($('#perfstartdate').attr('disabled')) {
            $('#perfstartdate').removeAttr('disabled');
            
        }
        else {
            $('#perfstartdate').val('');
            $('#perfstartdate').attr('disabled', 'disabled');
            removeGenerate();
            clearReportIframe();
        }

    });

    $('#perfspecifyenddate').on('click', function () {
        if ($('#perfenddate').attr('disabled')) {
            $('#perfenddate').removeAttr('disabled');
           
        }
        else {
            $('#perfenddate').val('');
            $('#perfenddate').attr('disabled', 'disabled');
            removeGenerate();
            clearReportIframe();
        }
    });

    /**
    CHANGED DATES TO DISPLAY GENERATE
    ***/

    /*
        Start Date
    */
    $('#perfstartdate').on('change', function () {
        if ($('#perfenddate').val().length > 0) {
            //Show Gnerate button   
            showGenerate();
        }
    });

    /*
        End Date
    */
    $('#perfenddate').on('change', function () {
        if ($('#perfstartdate').val().length > 0) {
            //Show Gnerate button   
            showGenerate();
        }
        //else { $('#perfenddate').attr('disabled', 'disabled'); }
    });

    function showGenerate() {
        if ($('#ifrmReportViewer').attr('src').indexOf('Reports')<0)
        { 
            var htmlDiv = '<div class="border-top-2 input-group " id="generatebtnCont"><button type="button" id="generatebtn" class="btn btn-outline-success form-control">Generate Report</button></div>';
            if ($('#generatebtnCont').length < 1)
            {
                if ($('#perfrep').hasClass('active'))
                {
                    $('#perfdateform').children().append(htmlDiv);
                }
                else if ($('#edwrep').hasClass('active'))
                {
                    $('#entrydateform').children().append(htmlDiv);
                }
            }
        }
    }

    function removeGenerate()
    {
        if ($('#generatebtnCont').length>0)
        {
            $('#generatebtnCont').remove();
        }        
    }

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
        //
        var result = attributes === 'constituencies' ?
            '<li class="dropdown-item justify-content-center" data-attribute="All"><span class="mx-auto"><input class="all" style="pointer-events: none;"  id="constituenciescheckboxAll" type="checkbox"  name="constituenciescheckboxAll"></span><label class="col-sm-2 col-form-label col-form-label-sm" for="constituenciescheckboxAll">ALL CONSTITUENCIES</label></li>'
                    +'<div class="dropdown-divider"></div>': '';
        var cnter=0;
        for (var sortedValue = 0; sortedValue < valssorted.length; sortedValue++) {
            result += '<li class="dropdown-item justify-content-center" data-attribute="'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '"><span><input  style="pointer-events: none;" type="checkbox" id="' + attributes + 'checkbox'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '" name="' + attributes + 'checkbox' + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString()
                + '">' + '</span><label class="col-sm-2 col-form-label col-form-label-sm" for="' + attributes + 'checkbox'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '">' + valssorted[sortedValue] + '</label></li>';

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
        $(this).parent().prev().text($(this).text());        
    });


    function loadAppType(dataObject, eleBtn, attributes) {

        var keys = Object.keys(dataObject);
        var vals = Object.values(dataObject);
        var valssorted = Object.values(dataObject).sort();

        var result = '<li class="dropdown-item justify-content-center" data-attribute="All"><span class="mx-auto"><input  style="pointer-events: none;"  class="all"  type="checkbox" id="' + attributes + 'checkboxAll" name="' + attributes + 'checkboxAll"></span><label class="col-sm-2 col-form-label col-form-label-sm" for="' + attributes + 'checkboxAll">ALL ' + attributes.toUpperCase() + '</label></li>'
                    + '<div class="dropdown-divider"></div>';
        var cnter = 0;
        for (var sortedValue = 0; sortedValue < valssorted.length; sortedValue++) {

            result += '<li class="dropdown-item justify-content-center" data-attribute="'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '"><span><input  style="pointer-events: none;"  type="checkbox" id="' + attributes + 'checkbox'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '" name="' + attributes + 'checkbox' + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString()
                + '">' + '</span><label class="col-sm-2 col-form-label col-form-label-sm" for="' + attributes + 'checkbox'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '">' + valssorted[sortedValue].APP_TYPE + '</label></li>';

            if (cnter < Object.keys(dataObject).length - 1) {
                result += '<div class="dropdown-divider"></div>';
            }
            cnter++;
        }
        eleBtn.siblings(".dropdown-menu").append(result);
    }


    function loadStatuses(dataObject, eleBtn, attributes) {
        var keys = Object.keys(dataObject);
        var vals = Object.values(dataObject);
        var valssorted = Object.values(dataObject).sort();

        var result = '<li class="dropdown-item justify-content-center" data-attribute="All"><span class="mx-auto"><input  style="pointer-events: none;"  class="all" type="checkbox" id="' + attributes + 'checkboxAll" name="' + attributes + 'checkboxAll"></span><label class="col-sm-2 col-form-label col-form-label-sm" for="' + attributes + 'checkboxAll">ALL ' + attributes.toUpperCase() + '</label></li>'
                     + '<div class="dropdown-divider"></div>';
        var cnter = 0;
        for (var sortedValue = 0; sortedValue < valssorted.length; sortedValue++) {
            result += '<li class="dropdown-item justify-content-center" data-attribute="'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '"><span><input style="pointer-events: none;"  type="checkbox" id="' + attributes + 'checkbox'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '" name="' + attributes + 'checkbox' + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString()
                + '">' + '</span><label class="col-sm-2 col-form-label col-form-label-sm" for="' + attributes + 'checkbox'
                + Object.keys(dataObject).find(key =>  dataObject[key] === valssorted[sortedValue]).toString() + '">' + valssorted[sortedValue].STATUS_CD + '</label></li>';

            if (cnter < Object.keys(dataObject).length - 1) {
                result += '<div class="dropdown-divider"></div>';
            }
            cnter++;
        }
        eleBtn.siblings(".dropdown-menu").append(result);
    }



    /*
    GET  THE CONSTITUENCY VALUES SELECTED
    */
    function getSelectedConstituencies()
    {
        var arrStr = '';
        var arrConsts = $('.dropdown-menu.constituency').find('input[type="checkbox"]:checked');
        if (arrConsts.length > 0)
        {
            var sendingConsts = [];
            for (var i = 0; i < arrConsts.length; i++)
            {
                sendingConsts.push($(arrConsts[i]).parent().parent().attr('data-attribute'));
            }
            arrStr = '?const=' + encodeURIComponent(JSON.stringify(sendingConsts));
        }
        return arrStr;
    }

    /*
   GET  THE APP TYPE VALUES SELECTED
   */
    function getSelectedAppTypes() {
        var arrStr = '';
        var arrConsts = $('.dropdown-menu.applicationType').find('input[type="checkbox"]:checked');
        if (arrConsts.length > 0) {
            var sendingConsts = [];
            for (var i = 0; i < arrConsts.length; i++) {
                sendingConsts.push($(arrConsts[i]).parent().next().text());
            }
            arrStr = '&appType=' + encodeURIComponent(JSON.stringify(sendingConsts));
        }
        return arrStr;
    }

    /*
   GET  THE WORKER TYPE VALUES SELECTED
   */
    function getSelecteWorkerTypes() {
        var arrStr = '';
        var arrConsts = $('.dropdown-menu.workerType').find('input[type="checkbox"]:checked');
        if (arrConsts.length > 0) {
            var sendingConsts = [];
            for (var i = 0; i < arrConsts.length; i++) {
                sendingConsts.push($(arrConsts[i]).parent().next().text());
            }
            arrStr = '&workerType=' + encodeURIComponent(JSON.stringify(sendingConsts));
        }
        return arrStr;
    }


    /*
   GET  THE STATUS VALUES SELECTED
   */
    function getSelecteStatuses() {
        var arrStr = '';
        var arrConsts = $('.dropdown-menu.status').find('input[type="checkbox"]:checked');
        if (arrConsts.length > 0) {
            var sendingConsts = [];
            for (var i = 0; i < arrConsts.length; i++) {
                sendingConsts.push($(arrConsts[i]).parent().next().text());
            }
            arrStr = '&status=' + encodeURIComponent(JSON.stringify(sendingConsts));
          
        }
        return arrStr;
    }

    /*
   GET  THE START AND END DATE VALUES SELECTED
   */
    function getEdwStartEndDate() {
        var arrStr = '';
        var arrConsts = $('#entrydateform').find('input[type="date"]');
        if ($('#entrydateform').find('input[type="checkbox"]:checked').length > 1 && arrConsts.length===2)
        {            
            if ($(arrConsts[0]).val() !== '' && $(arrConsts[1]).val() !== '')
            { 
                var sendingConsts = [];
                for (var i = 0; i < arrConsts.length; i++) {
                    sendingConsts.push($(arrConsts[i]).val());
                }
                arrStr = '&dates=' + encodeURIComponent(JSON.stringify(sendingConsts));
            }
        }
        return arrStr;
    }

    /*
    CLEAR REPORT IFRAME
    */
    function clearReportIframe()
    {
        if ($('#ifrmReportViewer').attr('src') !== undefined)
        {
            if ($('#ifrmReportViewer').attr('src').trim() !== '')
            { 
                $('#ifrmReportViewer').empty();
                $('#ifrmReportViewer').attr('src', '');
            }
        }
    }



    /*
    CHECK ALL DROPDOWN BOXES OR UNCHECK THEM
    */
    /*
     UNCHECK "ALL" OPTION IF ONE OF THE OTHER OPTIONS ARE DESELECTED
    */
     function checkUncheckAll(ele) {
        if (!$(ele).hasClass('all')) {
            if ($(ele).prop('checked') === false) {
                $(ele).parent().parent().parent().find('li').find('span').find('input.all').prop('checked', false);
            }
        }
        else if ($(ele).hasClass('all')) {
            if ($(ele).prop('checked') === true) {
                $(ele).parent().parent().parent().find("li").find("span").find("input").prop('checked', true);
            }
            else {
                $(ele).parent().parent().parent().find("li").find("span").find("input").prop('checked', false);
            }
        }
        verifyDropDownSelection();
    }

    function verifyDropDownSelection() {
        var state = true;
        if ($('.dropdown-menu.constituency').find('input[type="checkbox"]:checked').length < 1) {
            $('.dropdown-toggle.applicationType').addClass('disabled');
            $('.dropdown-menu.applicationType').find("input").prop('checked', false);
            state = false;
        }

        if ($('.dropdown-menu.applicationType').find('input[type="checkbox"]:checked').length < 1) {
            $('.dropdown-toggle.workerType').addClass('disabled');
            $('.dropdown-menu.workerType').find("input").prop('checked', false);
            state = false;
        }

        if ($('.dropdown-menu.workerType').find('input[type="checkbox"]:checked').length < 1) {
            $('.dropdown-toggle.status').addClass('disabled');
            $('.dropdown-menu.status').find("input").prop('checked', false);
            removeGenerate();
            clearReportIframe();
            state = false;

        }
        return state;
    }


    $('.dropdown-menu.constituency').on('click', 'li', function (event) {
        changeLiCheckedState($(this));
        if ($('.dropdown-menu.constituency').find('input[type="checkbox"]:checked').length >0)
        { 
            if ($('.dropdown-toggle.applicationType').hasClass('disabled')) {
                //Remove Disabled Feature and add application types
                $('.dropdown-toggle.applicationType').removeClass('disabled');

                if ($('.dropdown-menu.applicationType').html().trim().length === 0)
                {
                    $.ajax({
                        url: '../edwreports/Home/ApplicationTypes',
                        method: "get",
                        dataType: "json",
                        success: function (data) {
                            loadAppType(data, $(".dropdown-toggle.applicationType"), "applicationType");
                        },
                        error: function (xhr, status, error) {
                            alert("Failed To Load Worker Types");
                        }
                    });
                }
            }
        }             
        
        event.preventDefault();
        event.stopPropagation();
    });

    $('.dropdown-menu.applicationType').on('click', 'li', function () {
        changeLiCheckedState($(this));           
        if ($('.dropdown-menu.applicationType').find('input[type="checkbox"]:checked').length > 0) {
                if ($('.dropdown-toggle.workerType').hasClass('disabled')) {
                    //Remove Disabled Feature and add worker types
                    $('.dropdown-toggle.workerType').removeClass('disabled');
                    if ($('.dropdown-menu.workerType').html().trim().length === 0) {
                        $.ajax({
                            url: '../edwreports/Home/WorkerTypes',
                            method: "get",
                            dataType: "json",
                            success: function (data) {
                                loadAppType(data, $(".dropdown-toggle.workerType"), "workerType");
                            },
                            error: function (xhr, status, error) {
                                alert("Failed To Load Work Types");
                            }
                        });
                    }
                }
            }
       
    });

    $('.dropdown-menu.workerType').on('click', 'li', function () {
        changeLiCheckedState($(this));
            
        if ($('.dropdown-menu.workerType').find('input[type="checkbox"]:checked').length > 0) {
            if ($('.dropdown-toggle.status').hasClass('disabled')) {
                //Remove Disabled Feature and add STATUSES
                $('.dropdown-toggle.status').removeClass('disabled');
                if ($('.dropdown-menu.status').html().trim().length === 0) {
                    $.ajax({
                        url: '../edwreports/Home/Statuses',
                        method: "get",
                        dataType: "json",
                        success: function (data) {
                            loadStatuses(data, $(".dropdown-toggle.status"), "statuses");
                        },
                        error: function (xhr, status, error) {
                            alert("Failed To Load Status Types");
                        }
                    });
                }
            }
        }        
    });

    $('.dropdown-menu.status').on('click', 'li', function () {
            //Display Generate Button If Atleast 1 Status check box is checked
            changeLiCheckedState($(this));
            if ($('.dropdown-menu.status').find('input[type="checkbox"]:checked').length > 0) {
                showGenerate();
            }
            else {
                removeGenerate();
                clearReportIframe();
            }
        
    });

    /*
    CHANGE CHECK BOX STATE WHEN LIST ITEM OF DROPDOWN IS CLICKED
    */
    function changeLiCheckedState($ele)
    {
        if ($ele.find('input[type="checkbox"]')[0].checked === true)
        {
            $ele.find('input[type="checkbox"]')[0].checked = false;

            //IF THE INPUT HAS A CLASS OF ALL THEN UNCHECK ALL CHECK BOXES
            checkUncheckAll($ele.find('input[type="checkbox"]')[0]);

            //ELSE ONLY UNCHECK THE INPUT WITH ALL CLASS IF IT IS CHECKED
        }
        else {
            $ele.find('input[type="checkbox"]')[0].checked = true;

            //IF THE INPUT HAS A CLASS OF ALL THEN CHECK ALL CHECK BOXES
            checkUncheckAll($ele.find('input[type="checkbox"]')[0]);
            //ELSE ONLY UNCHECK THE INPUT WITH ALL CLASS IF IT IS CHECKED
        }
    }

    //SET CURSOR TO DEFAULT ON REPORT LOAD COMPLETION AND REENABLE BUTTON
    $('#ifrmReportViewer').on('load', function () {
        $("body").css("cursor", "default");
        $("button").css("cursor", "default");
        $("a").css("cursor", "default");
        $("input").css("cursor", "default");
        $('#generatebtn').removeAttr('disabled');
    });
    /*
    TRIGGER REPORT GENERATION WHEN GENERATE BUTTON IS CLICKED
    */
    $(document).on('click', '#generatebtn', function () {
        $('#generatebtn').attr('disabled', 'disabled');
        $("body").css("cursor", "progress");
        $("button").css("cursor", "progress");
        $("a").css("cursor", "progress");
        $("input").css("cursor", "progress");
        ReportValidationCheck();
       
    });


    function getProductivityValuesURL()
    {
        var resultStr = '';
        if ($('#perfstartdate').val().length > 0 && $('#perfenddate').val().length > 0)
        {
            resultStr = '../edwreports/Reports/PageReportViewer.aspx?st=' + $('#perfstartdate').val() + '&end=' + $('#perfenddate').val();
        }
        return resultStr;
    }

    function getEdwEntrValuesURL() {
        var resultStr = '';

        if ($('#entrystartdate').val().length > 0 && $('#entryenddate').val().length > 0) {
            resultStr = '../edwreports/Reports/PageReportViewer.aspx' + getSelectedConstituencies() +
            getSelectedAppTypes() +
            getSelecteWorkerTypes() +
            getSelecteStatuses() +
            getEdwStartEndDate();
        }
        else if ($('#entrydateform').find('input[type="checkbox"]:checked').length >0) {
            alert("Please Input Both Dates Or None!");
            removeDisabledGenerate();
        }
        else {
            resultStr = '../edwreports/Reports/PageReportViewer.aspx' + getSelectedConstituencies() +
            getSelectedAppTypes() +
            getSelecteWorkerTypes() +
            getSelecteStatuses();
        }
        return resultStr;
    }

    function removeDisabledGenerate()
    {
        $("body").css("cursor", "default");
        $("button").css("cursor", "default");
        $("a").css("cursor", "default");
        $("input").css("cursor", "default");
        $('#generatebtn').removeAttr('disabled');
    }

    function ReportValidationCheck() {
        var url = '';//'/Reports/PageReportViewer.aspx';
        
        if ($('#edwrep').hasClass('active') )
        {
            if ($('#entrydateform').find('input[type="checkbox"]:checked').length === 2 || $('#entrydateform').find('input[type="checkbox"]:checked').length === 0) {
                url = getEdwEntrValuesURL();
            }
            else {
                alert("Please Input Both Dates Or None!");
                removeDisabledGenerate();
                return;
            }
            
        }
        else if ($('#perfrep').hasClass('active') )
        {
            url = getProductivityValuesURL();
        }
        
        //?rptmode=local&reportname=Edw&parameters=dpSpMonthYr=''
        var ifrm = document.getElementById('ifrmReportViewer');
        if (ifrm !== null) {
            if (ifrm.src) {
                ifrm.src = url;
            }
            else if (ifrm.contentWindow !== null && ifrm.contentWindow.location !== null) {
                ifrm.contentWindow.location = url;
            }
            else {
                ifrm.setAttribute('src', url);
            }
        }
    }

});