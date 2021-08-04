using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Basic_Bullet : NetworkBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += speed * transform.up * Time.deltaTime;
    }

    [Server]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            collision.GetComponent<PlayerHP>().Damaged(damage);
            Destroy(this.gameObject);
        }
    }
}
