"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/simulator").build();

//Disable send button until connection is established
document.getElementById("status").innerText = "Connecting...";

connection.on("ReceiveEndpointHealthCheckData", function(data){
    console.log(data);

    var countNode = document.getElementById(data.Endpoint + "_healthCount");

    var div = document.createElement("div");
    div.textContent = data.StatusCode + " #" + countNode.innerText;
    div.classList.add("statusCheck");
    div.classList.add(data.Style);

    document.getElementById(data.Endpoint + "_label").innerText = data.EndpointLabel;


    countNode.innerText = parseInt(countNode.innerText) + 1;

    var node = document.getElementById(data.Endpoint + "_health");

    node.insertBefore(div, node.childNodes[0] || null)
    
});


connection.on("ReceiveTrafficData", function(data){
    console.log(data);

    var countNode = document.getElementById(data.Endpoint + "_trafficCount");

    var div = document.createElement("div");
    div.textContent = data.Source + " #" + countNode.innerText;
    div.classList.add("traffic");
    div.classList.add(data.Style);

    countNode.innerText = parseInt(countNode.innerText) + 1;

    document.getElementById(data.Endpoint + "_label").innerText = data.EndpointLabel;

    var node = document.getElementById(data.Endpoint + "_traffic");

    node.insertBefore(div, node.childNodes[0] || null)

});


connection.start().then(function(){
    document.getElementById("status").innerText = "Connected";
}).catch(function (err) {
    return console.error(err.toString());
});
