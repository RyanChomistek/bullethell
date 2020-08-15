"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Lobby_1 = require("./Lobby");
var lobby = new Lobby_1.Lobby();
var io = require('socket.io')({
    transports: ['websocket'],
});
io.attach(4567);
io.on('connection', function (socket) {
    socket.on('addPlayer', function () {
        // const id = lobby.GetNextPlayerId();
        // const player = new Player(id);
        // console.log(`new player ${player.Id}`);
        // socket.emit('playerAdded', player);
    });
});
// console.log('Hello world!')
