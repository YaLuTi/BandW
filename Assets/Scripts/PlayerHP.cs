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
        
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Hp = 5;
        PlayerHealthChanged?.Invoke(Hp);
    }

    public void Damaged(int damage)
    {
        CmdDamaged(damage);
        audioSource.PlayOneShot(DamageSFX);
        if (isLocalPlayer)
        {
            CameraFollow.CameraShake();
        }
    }

    [Command]
    void CmdDamaged(int damage)
    {
        Hp -= damage;
        PlayerHealthChanged?.Invoke(Hp);
        if(Hp <= 0)
        {
            PlayerDeath?.Invoke();
        }
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
}
