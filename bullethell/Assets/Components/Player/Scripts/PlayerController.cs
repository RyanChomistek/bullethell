using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehavior
{
    [SerializeField]
    public IPlayer m_Player { get { return base.m_NetworkObject as IPlayer; } protected set { base.m_NetworkObject = value; } }
    private Rigidbody2D m_RB;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
    }

    public override void Init(INetworkObject player)
    {
        base.Init(player);
    }

    private void OnMove(InputValue value)
    {
        // Debug.Log($"horz {value.Get<Vector2>()}");
        m_RB.velocity += value.Get<Vector2>();
    }
}
