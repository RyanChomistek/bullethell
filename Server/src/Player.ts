import { INetworkGameObject, INetworkRigidbody, NetworkRigidbody } from './NetworkObject';

export interface IPlayer extends INetworkGameObject
{

}

export class Player implements IPlayer
{
    public Id: number;
    public PrefabIndex: number;
    public Rigidbody: INetworkRigidbody;
    public LastUpdated: Date;
    public OwningPlayer: number;
    public IpAddress: string;

    constructor(
        id: number, 
        ipAddress:string,
        rigidbody: INetworkRigidbody = new NetworkRigidbody(),
        lastUpdated: Date = new Date(Date.now()))
    {
        this.Id = id;
        this.Rigidbody = rigidbody;
        this.LastUpdated = lastUpdated;
        this.OwningPlayer = this.Id;
        this.PrefabIndex = 0;
        this.IpAddress = ipAddress;
    }
}