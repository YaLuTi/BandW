using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NewNetworkManager : NetworkManager
{
    public delegate void OnPlayerJoinHandler(GameObject player);
    public event OnPlayerJoinHandler PlayerJoin;
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
        PlayerJoin?.Invoke(player);
        // roundManager.AddPlayer(player);
    }
}
