using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System.Reflection;

public class PlayerAttack : NetworkBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform muzzle;

    [SerializeField]
    public float bulletSize = 1;

    void OnFire()
    {
        CmdSpawnBullet();
    }

    [Command]
    void CmdSpawnBullet()
    {
        GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
        b.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
        NetworkServer.Spawn(b);
    }

    [Command]
    public void CmdSetValue(string[] command)
    {
        FieldInfo[] rProps = this.GetType().GetFields();

        foreach (FieldInfo rp in rProps)
            Debug.Log(rp.Name);

        FieldInfo field = this.GetType().GetField(command[2]);

        if (field != null)
        {
            Debug.Log("1");
            field.SetValue(
                this, // So we specify who owns the object
                10
              );
        }
    }
}
