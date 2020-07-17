$.connection.hub.start()
    .done(
        function ()
        {
            console.log("it worked");
            $.connection.myHub.server.announce("Connected");
        }
     )
    .fail(function () { alert("failed"); });

$.connection.myHub.client.announce = function (message) { alert(message); }