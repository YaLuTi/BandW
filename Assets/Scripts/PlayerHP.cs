using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerHP : NetworkBehaviour
{
    [SyncVar]
    public int Hp;

    [SerializeField]
    Slider HpSlider;
    [SerializeField]
    AudioClip DamageSFX;

    AudioSource audioSource;

    float recoveryTimer;
    [SerializeField]
    float recoveryTime;

    float recoveryTimer2;
    [SerializeField]
    float recoveryTime2;

    public delegate void PlayerHealthHandler(int HP);
    public event PlayerHealthHandler PlayerHealthChanged;

    public delegate void OnPlayerDeathHandler();
    public event OnPlayerDeathHandler PlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            if (Hp < 3)
            {
                recoveryTimer += Time.deltaTime;
                if (recoveryTimer > recoveryTime)
                {
                    recoveryTimer2 += Time.deltaTime;
                    if (recoveryTimer2 > recoveryTime2)
                    {
                        recoveryTimer2 = 0;
                        Heal (1);
                    }
                }
            }
        }
    }

    private void ResetRecoveryTimer()
    {
        recoveryTimer = 0;
        recoveryTimer2 = 0;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Hp = 5;
        PlayerHealthChanged?.Invoke(Hp);
    }

    public void Damaged(int damage)
    {
        ResetRecoveryTimer();
        ServerDamaged(damage);
        audioSource.PlayOneShot(DamageSFX);
        if (isLocalPlayer)
        {
            CameraFollow.CameraShake();
        }
    }
    [Server]
    void ServerDamaged(int damage)
    {
        Hp -= damage;
        PlayerHealthChanged?.Invoke(Hp);
        if (Hp <= 0)
        {
            PlayerDeath?.Invoke();
        }
    }

    public bool Spend(int cost)
    {
        if (Hp > cost)
        {
            ResetRecoveryTimer();
            Hp -= cost;
            PlayerHealthChanged?.Invoke(Hp);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int h)
    {
        CmdHeal(h);
    }

    [Command]
    public void CmdHeal(int h)
    {
        Hp += h;
        Hp = Mathf.Min(Hp, 5);
        PlayerHealthChanged?.Invoke(Hp);
    }

    [ClientRpc]
    void UpdateHPUI()
    {
        HpSlider.value = Hp;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Re");
        HpSlider.value = Hp;
    }

    [Command]
    public void SetRound()
    {
        Hp = 5;
        PlayerHealthChanged?.Invoke(Hp);
    }
}
