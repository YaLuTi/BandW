using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using DG.Tweening;

public class Ammo : NetworkBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SyncVar]
    bool IsUsed = false;

    float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsUsed) return;
        if (collision.tag == "Player")
        {
            audioSource.Play();
            collision.GetComponent<PlayerHP>().Heal(5);
            StartCoroutine(AmmoCooldown());
        }
    }

    IEnumerator AmmoCooldown()
    {
        IsUsed = true;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(5);
        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        yield return new WaitForSeconds(0.1f);
        IsUsed = false;
        yield return null;
    }
}
