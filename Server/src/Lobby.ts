import { IPlayer } from './Player'
import { SSL_OP_CISCO_ANYCONNECT } from 'constants';

export interface ILobby
{
    PlayerMap: Map<number, IPlayer>;

    GetNextPlayerId: () => number;
    AddPlayer: (player: IPlayer) => void;
    ModifyPlayer: (player: IPlayer) => void;
    RemovePlayer: (playerId: number) => void;
    ToJson: () => any;
    ToString: () => string;
}

function MapToString(map: Map<number, IPlayer>): string {
    const json: any = {};
    map.forEach((v,k) => {
        json[k] = v;
    })

    return JSON.stringify(json);
}

export class Lobby implements ILobby
{
    public PlayerMap: Map<number, IPlayer>;
    private playerIdCnt: number;

    constructor()
    {
        this.PlayerMap = new Map<number, IPlayer>();
        this.playerIdCnt = 0;
    }

    GetNextPlayerId = () =>
    {
        const id = this.playerIdCnt;
        this.playerIdCnt++;
        return id;
    }

    AddPlayer = (player: IPlayer) =>
    {
        console.log(`add player ${player.Id}, ${JSON.stringify(player)}`);
        this.PlayerMap.set(player.Id, player);
        console.log(`${this.PlayerMap.size} ${JSON.stringify(this.PlayerMap)}`);
    }

    ModifyPlayer = (player: IPlayer) =>
    {
        this.PlayerMap.set(player.Id, player);
    }

    RemovePlayer = (playerId: number) =>
    {
        console.log(`remove player ${playerId}, ${JSON.stringify(playerId)}`);
        this.PlayerMap.delete(playerId);
        console.log(`${this.PlayerMap.size} ${JSON.stringify(this.PlayerMap)}`);
    }

    ToJson = () =>
    {
        const json: any = {};
        this.PlayerMap.forEach((v,k) => {
            json[k] = v;
        })

        return json;
    }

    ToString = () =>
    {
        // console.log("map " + MapToString(this.PlayerMap));
        return MapToString(this.PlayerMap);
    }
}

