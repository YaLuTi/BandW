using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Reflection;

public class Basic_Weapon : NetworkBehaviour
{

    [SerializeField]
    protected GameObject bullet;

    [SerializeField]
    public int cost;

    public float cooldown = 1;

    [SerializeField]
    public float addProjectile = 0;
    [SerializeField]
    public float ProjectileSize = 1;
    [SerializeField]
    public float ProjectileSpeed = 1;

    // Start is called before the first frame update
    protected void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ClientRpc]
    public virtual void Attack(Transform muzzle)
    {
        // if (!isLocalPlayer) return;
    }


    public virtual void Fire(Transform muzzle)
    {

    }

    [Command]
    public virtual void CmdSpawnBullet(Transform muzzle)
    {
    }
}
