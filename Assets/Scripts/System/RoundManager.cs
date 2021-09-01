using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class RoundManager : NetworkBehaviour
{
    static List<GameObject> Players = new List<GameObject>();
    [SerializeField]
    Transform[] SpawnPosition;

    [SyncVar]
    int Num;

    [SyncVar]
    public int Ready = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Server]
    void ReadyCheck(bool IsReady)
    {
        int c = 0;
        for(int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetComponent<PlayerData>().IsReady) c++;
        }
        Debug.Log(c);
        Debug.Log(Players.Count);
        if (c >= Players.Count)
        {
            LoadScene();
        }
    }

    [ClientRpc]
    public void CheckRoundEnd()
    {
        SceneManager.LoadScene(1);
        NetworkClient.localPlayer.GetComponent<PlayerMove>().CmdSetToPoint(SpawnPosition[NetworkClient.localPlayer.GetComponent<PlayerData>().ID].position);
    }
    [Server]
    public void AddPlayer(GameObject player)
    {
        Debug.Log("?");
        Players.Add(player);
        player.GetComponent<PlayerData>().ID = Num;
        Num++;
        player.GetComponent<PlayerHP>().PlayerDeath += CheckRoundEnd;

        player.GetComponent<PlayerData>().PlayerReadyChanged += ReadyCheck;
    }

    [ClientRpc]
    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator RoundEnd()
    {
        yield return null;
    }
}
