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
    bool m_IsControlledByThisClient;
    bool IPlayer.IsControlledByThisClient { get { return m_IsControlledByThisClient; } set { m_IsControlledByThisClient = value; } }

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

    public Player(INetworkObject other)
        : this(other.Id, other.PrefabIndex, other.OwningPlayer, other.Rigidbody, other.LastUpdated)
    {
        m_IsControlledByThisClient = false;
    }

    public override string ToString()
    {
        return $" Id:{Id}, ControlledByThisClient:{OwningPlayer}, LastUpdated:{LastUpdated}, Position:{Rigidbody.Position}";
    }
}
