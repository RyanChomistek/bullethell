using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public interface INetworkRigidbody
{
    Vector2 Position { get; set; }
    Vector2 Velocity { get; set; }
    Vector2 Rotation { get; set; }
}

public class NetworkRigidbody : INetworkRigidbody
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 Rotation { get; set; }
    
    [JsonConstructor]
    public NetworkRigidbody(Vector2 position, Vector2 velocity, Vector2 rotation)
    {
        Position = position;
        Velocity = velocity;
        Rotation = rotation;
    }
}

public interface INetworkObject
{
    int Id { get; set; }
    int PrefabIndex { get; set; }
    INetworkRigidbody Rigidbody { get; set; }
    int OwningPlayer { get; set; }
    System.DateTime LastUpdated { get; set; }
}

public class NetworkObject : INetworkObject
{
    public int Id { get; set; }
    public int PrefabIndex { get; set; }
    public int OwningPlayer { get; set; }
    public INetworkRigidbody Rigidbody { get; set; }
    public DateTime LastUpdated { get; set; }
    
    protected NetworkObject(int id, int PrefabIndex, int owningPlayer, INetworkRigidbody rigidbody, DateTime lastUpdated)
    {
        Id = id;
        OwningPlayer = owningPlayer;
        Rigidbody = rigidbody;
        LastUpdated = lastUpdated;
    }
}
