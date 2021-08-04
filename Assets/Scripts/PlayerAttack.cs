using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class PlayerAttack : NetworkBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform muzzle;
    void OnFire()
    {
        CmdSpawnBullet();
    }

    [Command]
    void CmdSpawnBullet()
    {
        GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
        NetworkServer.Spawn(b);
    }
}
