using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkBehavior)), RequireComponent(typeof(Rigidbody2D))]
public class NetworkRigidbodyController : MonoBehaviour
{
    protected NetworkBehavior m_NetworkBehavior;
    protected INetworkRigidbody m_NetworkRigidbody { get { return m_NetworkBehavior.m_NetworkObject.Rigidbody; } }

    private Rigidbody2D m_RB;

    // Start is called before the first frame update
    void Start()
    {
        m_NetworkBehavior = GetComponent<NetworkBehavior>();
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_NetworkBehavior != null && m_RB.velocity != m_NetworkBehavior.m_NetworkObject?.Rigidbody?.Velocity && m_NetworkBehavior.IsControlledByLocalPlayer)
        {
            m_NetworkBehavior.m_NetworkObject.Rigidbody.Velocity = m_RB.velocity;
            m_NetworkBehavior.m_NetworkObject.Rigidbody.Position = m_RB.position;
            m_NetworkBehavior.m_NetworkObject.LastUpdated = System.DateTime.Now;
            NetworkManager.Instance.UpdateObject(m_NetworkBehavior.m_NetworkObject);
            Debug.Log("need to update");
        }
        else if (!m_NetworkBehavior.IsControlledByLocalPlayer)
        {
            //m_RB.velocity = m_NetworkBehavior.m_NetworkObject.Rigidbody.Velocity;
            //m_RB.position = m_NetworkBehavior.m_NetworkObject.Rigidbody.Position;
            // interpolate position since the last time we were updated
            System.TimeSpan delta = System.DateTime.Now.ToUniversalTime() - m_NetworkBehavior.m_NetworkObject.LastUpdated;
            // Debug.Log($"{System.DateTime.Now.ToUniversalTime() } | { m_NetworkBehavior.m_NetworkObject.LastUpdated}");
            // Debug.Log($"{delta.TotalSeconds} | {m_NetworkRigidbody.Velocity} | {m_NetworkRigidbody.Position}");
            m_RB.position = ((float)delta.TotalSeconds) * m_NetworkRigidbody.Velocity + m_NetworkRigidbody.Position;
        }
    }
}
