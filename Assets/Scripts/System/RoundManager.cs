using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoundManager : NetworkBehaviour
{
    static List<GameObject> Players = new List<GameObject>();
    [SerializeField]
    Transform[] SpawnPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ClientRpc]
    public void CheckRoundEnd()
    {
        Debug.Log("?");
        for(int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<PlayerMove>().SetToPoint(SpawnPosition[i].position);
        }
    }
    [Server]
    public void AddPlayer(GameObject player)
    {
        Debug.Log("?");
        Players.Add(player);
        player.GetComponent<PlayerHP>().PlayerDeath += CheckRoundEnd;
    }
}
