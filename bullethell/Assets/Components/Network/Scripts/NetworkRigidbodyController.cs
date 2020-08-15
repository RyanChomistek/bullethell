using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkBehavior)), RequireComponent(typeof(Rigidbody2D))]
public class NetworkRigidbodyController : MonoBehaviour
{
    protected NetworkBehavior m_NetworkBehavior;
    private Rigidbody2D m_RB;

    // Start is called before the first frame update
    void Start()
    {
        m_NetworkBehavior = GetComponent<NetworkBehavior>();
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_NetworkBehavior != null && m_RB.velocity != m_NetworkBehavior.m_NetworkObject?.Rigidbody?.Velocity)
        {
            m_NetworkBehavior.m_NetworkObject.Rigidbody.Velocity = m_RB.velocity;
            m_NetworkBehavior.m_NetworkObject.Rigidbody.Position = m_RB.position;
            m_NetworkBehavior.m_NetworkObject.LastUpdated = System.DateTime.Now;
            NetworkManager.Instance.UpdateObject(m_NetworkBehavior.m_NetworkObject);
            Debug.Log("need to update");
        }
    }
}
