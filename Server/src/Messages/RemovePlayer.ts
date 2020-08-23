export interface IRemovePlayerRequest {
    PlayerId: number
}

export class RemovePlayerRequest {
    public PlayerId: number;
    constructor(playerId: number) {
        this.PlayerId = playerId;
    }
}

export interface IRemovePlayerResponse {
    PlayerId: number
}