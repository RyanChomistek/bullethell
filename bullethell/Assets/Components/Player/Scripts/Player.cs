using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public interface IPlayer : INetworkObject
{
    bool IsControlledByThisClient { get; set; }
}

public class Player : NetworkObject, IPlayer
{
    bool IPlayer.IsControlledByThisClient { get; set; }

    public Player(int id, int PrefabIndex, int owningPlayer, INetworkRigidbody rigidbody, DateTime lastUpdated) 
        : base(id, PrefabIndex, owningPlayer, rigidbody, lastUpdated)
    {
    }

    [JsonConstructor]
    public Player(int id, int PrefabIndex, int owningPlayer, NetworkRigidbody rigidbody, string lastUpdated)
        :this(id, PrefabIndex, owningPlayer, rigidbody, DateTime.Parse(lastUpdated))
    {
    }

    public Player(Player other) 
        : this(other.Id, other.PrefabIndex, other.OwningPlayer, other.Rigidbody, other.LastUpdated)
    {
    }

    public override string ToString()
    {
        return $" Id:{Id}, ControlledByThisClient:{OwningPlayer}, LastUpdated:{LastUpdated}, Position:{Rigidbody.Position}";
    }
}
