import { INetworkObject, INetworkGameObject, NetworkRigidbody, INetworkRigidbody} from './NetworkObject';
import {Vec3} from 'vec3'

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

    constructor(
        id: number, 
        rigidbody: INetworkRigidbody = new NetworkRigidbody(),
        lastUpdated: Date = new Date(Date.now()))
    {
        this.Id = id;
        this.Rigidbody = rigidbody;
        this.LastUpdated = lastUpdated;
        this.OwningPlayer = this.Id;
        this.PrefabIndex = 0;
    }
}