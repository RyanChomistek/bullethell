using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerRequest
{
    public int PlayerId;

    [JsonConstructor]
    public RemovePlayerRequest(int playerId)
    {
        PlayerId = playerId;
    }
}

public class RemovePlayerResponse
{
    public int PlayerId;

    [JsonConstructor]
    public RemovePlayerResponse(int playerId)
    {
        PlayerId = playerId;
    }
}
