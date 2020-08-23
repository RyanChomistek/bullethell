using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBehavior : MonoBehaviour
{
    public INetworkObject m_NetworkObject { get; protected set; }

    public bool IsControlledByLocalPlayer { get { return NetworkManager.Instance.IsControlledByLocalPlayer(m_NetworkObject.Id); } }

    public virtual void Init(INetworkObject networkObject)
    {
        m_NetworkObject = networkObject;
        transform.position = networkObject.Rigidbody.Position;
    }

    /// <summary>
    /// this handles the updates from the server
    /// </summary>
    /// <param name="networkObject"></param>
    public virtual void Modify(INetworkObject networkObject)
    {
        m_NetworkObject = networkObject;
    }

    public virtual void DeserializeJson()
    {

    }

    private void OnDestroy()
    {
        if(IsControlledByLocalPlayer)
        {
            NetworkManager.Instance.DestroyNetworkObject(m_NetworkObject.Id);
        }
    }
}
