using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehavior
{
    [SerializeField]
    public IPlayer Player { get { return base.m_NetworkObject as IPlayer; } protected set { base.m_NetworkObject = value; } }
    private Rigidbody2D m_RB;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
    }

    public override void Init(INetworkObject networkObject)
    {
        base.Init(new Player(networkObject));
        NetworkManager.Instance.PlayerMap.Add(networkObject.Id, this);
    }

    public override void Modify(INetworkObject networkObject)
    {
        Player = new Player(networkObject);
    }

    private void OnMove(InputValue value)
    {
        // Debug.Log($"horz {value.Get<Vector2>()}");
        m_RB.velocity += value.Get<Vector2>();
    }
}
