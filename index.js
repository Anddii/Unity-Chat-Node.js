var net = require('net'); 

var sockets = [];

var tcp_server = net.createServer(function(socket) 
{ 
    sockets.push(socket);
    socket.write("Connected");
    console.log('connected');

    socket.on('data', function (msg_sent) {

        console.log('Message: ' + msg_sent);

        for (var i = 0; i < sockets.length; i++) {
            // Write the msg sent by chat client
            sockets[i].write("<< " + msg_sent);
        }
    });

    socket.on('end', function () {
        var i = sockets.indexOf(socket);
        sockets.splice(i, 1);
    });
}); 
tcp_server.listen(3000);