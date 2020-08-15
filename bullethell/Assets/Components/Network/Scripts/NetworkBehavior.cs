using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBehavior : MonoBehaviour
{
    public INetworkObject m_NetworkObject { get; set; }

    public virtual void Init(INetworkObject networkObject)
    {
        m_NetworkObject = networkObject;
        transform.position = networkObject.Rigidbody.Position;
    }

    public virtual void DeserializeJson()
    {

    }
}
