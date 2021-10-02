using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Space_Dash : Basic_Weapon
{
    PlayerMove playerMove;
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fire(Transform muzzle)
    {
        StartCoroutine(speedUp());
        /*
        Ray castPoint = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(hit.point.y - transform.position.y, hit.point.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            playerMove.rb.AddForce(speed * new Vector3(Mathf.Sin(AngleDeg), Mathf.Cos(AngleDeg), 0));
        }*/
    }

    IEnumerator speedUp()
    {
        float u = speed;
        float p;

        float t = 0.5f;

        playerMove.speedMultiplier += u;
        while(t > 0)
        {
            p = u;
            u *= 0.9f;
            playerMove.speedMultiplier -= p - u;
            t -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        playerMove.speedMultiplier -= u;
        yield return null;
    }
}
