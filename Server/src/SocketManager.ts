import { ILobby, Lobby } from './Lobby';
import { IRemovePlayerRequest } from './Messages/RemovePlayer';
import { IPlayer, Player } from './Player';

export function StartServer()
{
	const lobby: ILobby = new Lobby();
	var io = require('socket.io')({
		transports: ['websocket'],
	});
	
	io.attach(4567);
	
	io.on('connection', function(socket: SocketIO.Socket){
		console.log("----------------------------------------------------------------------------------");
		socket.on('createPlayer', function(){
			const id = lobby.GetNextPlayerId();
			const player: IPlayer = new Player(id, socket.handshake.address);

			// var address = socket.handshake.address;
			console.log(JSON.stringify(socket.handshake));
			console.log(JSON.stringify(socket.handshake.address));
			lobby.AddPlayer(player);
			console.log(`add player ${player.Id}`);

			socket.emit('createPlayer', player);
			socket.broadcast.emit('addObject', player);
		});
	
		socket.on('modifyPlayer', function(message: any){
            const player: IPlayer = <IPlayer> message;
			console.log(`modify player ${player.Id} ${player.LastUpdated}`);
			lobby.ModifyPlayer(player);
			
            socket.emit('modifyPlayer', player);
			socket.broadcast.emit('modifyPlayer', player);
		});
	
		socket.on('refreshPlayers', function(){
			console.log("refresh " + lobby.ToString())
			socket.emit('refreshPlayers', lobby.ToJson());
		});

		socket.on('removePlayer', function(req: IRemovePlayerRequest){
			console.log(`remove player ${req.PlayerId}`);
			lobby.PlayerMap.delete(req.PlayerId)
			socket.emit('removePlayer', req);
			socket.broadcast.emit('removePlayer', req);
		});
	})
}
