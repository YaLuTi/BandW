using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ReadyButton : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Ready()
    {
        Debug.Log("WTF");
        NetworkClient.localPlayer.GetComponent<PlayerData>().CmdReady();
    }
}
