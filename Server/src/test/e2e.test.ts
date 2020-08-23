import { StartServer } from "../SocketManager";
import { Player } from "../Player";
import { RemovePlayerRequest, IRemovePlayerResponse } from "../Messages/RemovePlayer";
import Vec2 from "vec2";

var io = require('socket.io-client');

let socket: SocketIO.Socket;

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

function sleep(ms: number) {
	return new Promise(resolve => setTimeout(resolve, ms));
}

test('Move Player', (done) => {
	
	let id = -1;

	socket.on('createPlayer', async (player: any) => {
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

test('Move Player2', async (done) => {
	jest.setTimeout(10000);
	let id = -1;



	socket.on('createPlayer', (player: any) => {
		expect(player.Id).toEqual(expect.any(Number));
		id = player.Id;
		player.Rigidbody.Velocity.x = 1;
		socket.emit('modifyPlayer', player);

		socket.on('modifyPlayer', async (player: Player) => {
			const time: number = 2.5;
			await sleep(time * 1000);
			player.Rigidbody.Position = new Vec2(time, 0);
			player.Rigidbody.Velocity = new Vec2(0, 1);
			player.LastUpdated = new Date(Date.now());
			socket.emit('modifyPlayer', player);

			socket.on('modifyPlayer', async (player: any) => {
				await sleep(1000);
				socket.emit('removePlayer', new RemovePlayerRequest(id));
			});
		});
	});

	socket.on('removePlayer', async (player: any) => {
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