using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Reflection;

public class Basic_Bullet : NetworkBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    int damage;

    [SyncVar]
    public float bulletSize = 1;
    [SyncVar]
    public float bulletSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += speed * bulletSpeed * transform.up * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            collision.GetComponent<PlayerHP>().Damaged(damage);
            NetworkServer.Destroy(this.gameObject);
        }
        else if(collision.tag == "Wall")
        {
            NetworkServer.Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float s)
    {
        speed *= s;
    }


}
