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
    [SerializeField]
    PlayerMove playerMove;
    [SerializeField]
    Basic_Weapon weapon;
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
        IsReady = true;
        PlayerReadyChanged?.Invoke(IsReady);
    }

    [Command]
    public void SetSkill(string[] command)
    {
        RpcSetSkill(command);
    }

    [ClientRpc]
    public void RpcSetSkill(string[] command)
    {
        for (int i = 0; i < command.Length; i++)
        {
            string[] split = command[i].Split('_');
            switch (split[0])
            {
                case "playerAttack":
                    playerAttack.SetValue(split);
                    break;
                case "playerMove":
                    playerMove.SetValue(split);
                    break;
                case "weapon":
                    weapon.SetValue(split);
                    break;
                case "addWeapon":
                    GameObject instance = Instantiate(Resources.Load(split[1], typeof(GameObject))) as GameObject;
                    instance.transform.parent = this.transform;
                    playerAttack.weapon = instance.GetComponent<Basic_Weapon>();
                    break;
                default:
                    break;
            }
        }
    }
}
