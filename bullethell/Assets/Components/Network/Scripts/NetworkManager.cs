using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Newtonsoft.Json;
using System;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager s_Instance;
    public static NetworkManager Instance { get { return s_Instance; } }

    private SocketIOComponent m_Socket;

    [SerializeField]
    private PlayerController m_PlayerControllerPrefab;

    [SerializeField]
    private NetworkBehavior[] m_NetworkBehaviorPrefabArray;

    public Dictionary<int, NetworkBehavior> networkBehaviorMap;

    public Action OnConnect = null;

    bool firstConnection = true;

    private void Awake()
    {
        s_Instance = this;
        networkBehaviorMap = new Dictionary<int, NetworkBehavior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        m_Socket = go.GetComponent<SocketIOComponent>();

        OnConnect += CreatePlayer;
        OnConnect += () => { m_Socket.Emit("refreshAll"); };

        m_Socket.On("open", (SocketIOEvent e) =>
        {
            if(m_Socket.sid != null && firstConnection)
            {
                firstConnection = false;
                Debug.Log(string.Format("[name: {0}, data: {1}, sid{2}]", e.name, e.data, m_Socket.sid));
                // OnConnect?.Invoke();
                CreatePlayer();
                m_Socket.Emit("refreshPlayers");
            }
        });

        m_Socket.On("createPlayer", (SocketIOEvent e) => {
            string data = e.data.ToString();
            Debug.Log(data);
            try
            {
                Debug.Log(data);
                IPlayer player = JsonConvert.DeserializeObject<Player>(data);
                player.IsControlledByThisClient = true;
                PlayerController playerController = Instantiate(m_PlayerControllerPrefab);
                playerController.Init(player);
                networkBehaviorMap[player.Id] = playerController;
                m_Socket.Emit("refreshAll");

            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message + " " + ex.StackTrace);
            }
        });

        m_Socket.On("modifyPlayer", (SocketIOEvent e) =>
        {
            Debug.Log(e.data);
        });

        m_Socket.On("refreshPlayers", (SocketIOEvent e) =>
        {
            Debug.Log(e.data);
            var playerMap = JsonConvert.DeserializeObject<Dictionary<int, Player>>(e.data.ToString());
            Debug.Log($" map size {playerMap.Count}");
            foreach(var kvp in playerMap)
            {
                Debug.Log($"{kvp.Key} {kvp.Value.ToString()}");
                ModifyNetworkObject(kvp.Value);
            }
        });

        m_Socket.On("addObject", (SocketIOEvent e) =>
        {
            string json = e.data.ToString();
            NetworkObject networkObj = JsonConvert.DeserializeObject<NetworkObject>(json);
            ModifyNetworkObject(networkObj);
        });

        // no more socket on's after this
        m_Socket.Connect();
        m_Socket.Emit("createPlayer");
    }

    private void ModifyNetworkObject (INetworkObject netObj)
    {
        if (networkBehaviorMap.ContainsKey(netObj.Id))
        {
            networkBehaviorMap[netObj.Id].m_NetworkObject = netObj;
        }
        else
        {
            NetworkBehavior netBehaivor = Instantiate(m_NetworkBehaviorPrefabArray[netObj.PrefabIndex]);
            netBehaivor.Init(netObj);
            networkBehaviorMap[netObj.Id] = netBehaivor;
        }
    }

    public void CreatePlayer()
    {
        m_Socket.Emit("createPlayer");
    }

    public void UpdateObject(INetworkObject networkObject)
    {
        string jsonStr = JsonConvert.SerializeObject(networkObject);
        JSONObject json = new JSONObject(jsonStr);

        m_Socket.Emit("modifyPlayer", json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
