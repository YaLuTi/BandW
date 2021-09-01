using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Reflection;

public class PlayerData : NetworkBehaviour
{
    [SyncVar]
    public int ID;
    [SyncVar]
    public bool IsReady;
    [SyncVar]
    public float Scale;

    public delegate void PlayerReadyHandler(bool ready);
    public event PlayerReadyHandler PlayerReadyChanged;

    [SerializeField]
    PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void CmdReady()
    {
        IsReady = !IsReady;
        PlayerReadyChanged?.Invoke(IsReady);
    }

    [Command]
    public void SetSkill(string[] command)
    {
        for (int i = 0; i < command.Length; i++)
        {
            string[] split = command[i].Split('_');
            switch (split[0])
            {
                case "playerAttack":
                    playerAttack.CmdSetValue(split);
                    break;
                default:
                    break;
            }
        }

    }
}
