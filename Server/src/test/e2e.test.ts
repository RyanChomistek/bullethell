import { StartServer } from "../SocketManager";
import { Player } from "../Player";

var io = require('socket.io-client');

let socket: any;


beforeEach((done) => {
	socket = io.connect('ws://127.0.0.1:4567', {
		'reconnection delay': 0,
		'reopen delay': 0,
		'force new connection': true,
		transports: ['websocket'],
	});
	socket.on('connect', function() {
		done();
	});
});

afterAll((done) => {
	socket.close();
	done();
});

test('Create Player', (done) => {
	socket.on('createPlayer', (message: any) => {
		expect(message.Id).toEqual(expect.any(Number));
		socket.emit('removePlayer', message.Id);
		done();
	});

	socket.emit('createPlayer');
});

test('Move Player', (done) => {
	
	let id = -1;

	socket.on('createPlayer', (player: any) => {
		expect(player.Id).toEqual(expect.any(Number));
		id = player.Id;
		player.Rigidbody.Velocity.x = 1;
		socket.emit('modifyPlayer', player);
	});

	socket.on('modifyPlayer', (player: any) => {
		expect(player.Id).toEqual(id);
		expect(player.Rigidbody.Velocity.x).toEqual(1);
		socket.emit('removePlayer', player.Id);
		done();
	});

	socket.emit('createPlayer');
});

test('refreshPlayers', (done) => {
	
	let id = -1;

	socket.on('createPlayer', (player: any) => {
		expect(player.Id).toEqual(expect.any(Number));
		id = player.Id;
		player.Rigidbody.Velocity.x = 1;
	});

	socket.on('refreshPlayers', (playerMapJson: any) => {
		// console.log(JSON.stringify(playerMapJson));
		const playerMap = new Map<number, Player>();
		for (var value in playerMapJson) {  
			playerMap.set(parseInt(value),playerMapJson[value])  
		}

		expect(playerMap.size).toEqual(3);

		done();
	});

	socket.emit('createPlayer');
	socket.emit('createPlayer');
	socket.emit('createPlayer');

	socket.emit('refreshPlayers');
});