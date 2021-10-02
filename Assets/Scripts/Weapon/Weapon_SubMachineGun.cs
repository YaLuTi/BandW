using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Weapon_SubMachineGun : Basic_Weapon
{

    [SerializeField]
    float deflection = 0;
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fire(Transform muzzle)
    {
        base.Fire(muzzle);
        StartCoroutine(Attacking(muzzle));
    }

    public override void CmdSpawnBullet(Transform muzzle)
    {
        float rf = Random.Range(-deflection, deflection);
        Quaternion r = Quaternion.Euler(new Vector3( 0, 0, rf) + muzzle.eulerAngles);
        GameObject b = Instantiate(bullet, muzzle.position, r);
        SetBullet(b);
        deflection = Mathf.Min(deflection + 5, 45);
        if(rf > deflection * 0.5f)
        {
            deflection *= 0.5f;
        }
        NetworkServer.Spawn(b);
    }

    void SetBullet(GameObject b)
    {
        Basic_Bullet bullet = b.GetComponent<Basic_Bullet>();
        bullet.bulletSpeed = ProjectileSpeed;
        bullet.transform.localScale = new Vector3(ProjectileSize, ProjectileSize, ProjectileSize);
    }

    IEnumerator Attacking(Transform muzzle)
    {
        int count = 0;
        while (count < addProjectile + 8)
        {
            count++;
            CmdSpawnBullet(muzzle);
            yield return new WaitForSeconds(0.5f / (addProjectile + 8));
        }
        yield return null;
    }
}
