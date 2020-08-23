import vec2 from 'vec2'

export interface INetworkRigidbody
{
    Position: vec2;
    Velocity: vec2;
    Rotation: vec2;
}

export class NetworkRigidbody
{
    public Position: vec2;
    public Velocity: vec2;
    public Rotation: vec2;

    constructor(pos: vec2= new vec2(0,0), vel: vec2= new vec2(0,0), rot: vec2 = new vec2(0,0))
    {
        this.Position = pos;
        this.Velocity = vel;
        this.Rotation = rot;
    }
}

export interface INetworkObject
{
    Id: number;
    PrefabIndex: number;
    LastUpdated: Date;
    OwningPlayer: number;
    IpAddress: string;
}

export interface INetworkGameObject extends INetworkObject
{
    Rigidbody: INetworkRigidbody;
}