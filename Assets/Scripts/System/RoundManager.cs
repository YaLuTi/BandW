using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class RoundManager : NetworkBehaviour
{
    List<GameObject> Players = new List<GameObject>();
    [SerializeField]
    GameObject[] SpawnPosition;

    [SyncVar]
    int Num;

    [SyncVar]
    public int Ready = 0;

    [SerializeField]
    NewNetworkManager networkManager;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        networkManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<NewNetworkManager>();
        networkManager.PlayerJoin += AddPlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Server]
    void ReadyCheck(bool IsReady)
    {
        Ready++;
        if (Ready >= Players.Count)
        {
            LoadScene("SampleScene");
            Ready = 0;
        }
    }

    [ClientRpc]
    public void CheckRoundEnd()
    {
        Debug.Log("QWFR");
        networkManager.ServerChangeScene("SkillScene");
        foreach(GameObject p in Players)
        {
            p.GetComponent<PlayerHP>().SetRound();
        }
        // NetworkClient.localPlayer.GetComponent<PlayerMove>().CmdSetToPoint(SpawnPosition[NetworkClient.localPlayer.GetComponent<PlayerData>().ID].position);
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
    public void LoadScene(string scene)
    {
        StartCoroutine(RondStart(scene));
        
    }

    IEnumerator RondStart(string scene)
    {
        AsyncOperation asyncLoad = networkManager.ServerChangeScene(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        if(scene != "SkillScene")
        {
            yield return new WaitForSeconds(0.5f);
            SpawnPosition = GameObject.FindGameObjectsWithTag("Respawn");
            for(int i = 0; i < Players.Count; i++)
            {
                Players[i].GetComponent<PlayerMove>().CmdSetToPoint(SpawnPosition[i].transform.position);
                Players[i].GetComponent<PlayerHP>().SetRound();
            }
        }
        yield return null;
    }

    IEnumerator RoundEnd()
    {
        yield return null;
    }
}
