using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System.Reflection;

public class PlayerAttack : NetworkBehaviour
{
    [SerializeField]
    Transform muzzle;

    float attackCooldown;
    float dashCooldown;

    [SerializeField]
    public Basic_Weapon weapon;
    [SerializeField]
    public Basic_Weapon dash;

    PlayerHP playerHP;

    [SerializeField]
    GameObject bullet;

    void Start()
    {
        playerHP = GetComponent<PlayerHP>();
    }

    private void Update()
    {
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void OnFire()
    {
        if (weapon == null) return;
        if (attackCooldown > 0) return;
        if (!isLocalPlayer) return;
        CmdFire();
        attackCooldown = weapon.cooldown;
    }

    [Command]
    void CmdFire()
    {
        if (playerHP.Spend(weapon.cost))
        {
            weapon.Fire(muzzle);
        }
    }

    void OnDash()
    {
        if (dashCooldown > 0) return;
        if (!isLocalPlayer) return;
        CmdDash();
    }

    [Command]
    void CmdDash()
    {
        dash.Fire(muzzle);
    }
}
