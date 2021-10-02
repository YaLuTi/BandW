using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System.Reflection;

public class PlayerMove : NetworkBehaviour
{
    [SerializeField]
    public float a = 0.5f;
    public Vector2 speed;
    float MaxSpeed;
    public float MaxSpeedMultiplier = 1;

    [SyncVar]
    public float speedMultiplier = 1;

    public Rigidbody2D rb;

    public float h, v;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (!isLocalPlayer) return;
        MaxSpeed = (8f / 2) * 1.414f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        speed += new Vector2(h, v) * a * (1 + Mathf.Min((speed.magnitude * 0.1f), 0.2f));
        if (Mathf.Abs(speed.x) + Mathf.Abs(speed.y) > MaxSpeed * MaxSpeedMultiplier)
        {
            float m = MaxSpeed / (Mathf.Abs(speed.x) + Mathf.Abs(speed.y));
            speed.x = m * speed.x;
            speed.y = m * speed.y;
        }

        // Vector2.Min(speed, new Vector2(2.5f, 2.5f));

        speed *= 0.95f;

        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Physics.Raycast(ray, out raycastHit, Mathf.Infinity);

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(raycastHit.point.y - transform.position.y, raycastHit.point.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        Move(speed);
        Rotate(AngleDeg);
    }

    void Move(Vector2 s)
    {
        rb.velocity = (new Vector3(s.x, s.y, 0) * 2 * speedMultiplier);
    }

    void Rotate(float r)
    {
        this.transform.rotation = Quaternion.Euler(0, 0, r - 90);
    }

    void OnMove(InputValue value)
    {
        if (!isLocalPlayer) return;
        h = value.Get<Vector2>().x;
        v = value.Get<Vector2>().y;
    }

    public void CmdSetToPoint(Vector3 point)
    {
        Debug.Log(transform.position);
        transform.position = point;
        Debug.Log(transform.position);
    }

    public void SetValue(string[] command)
    {
        switch (command[1])
        {
            case "add":
                {
                    FieldInfo field = this.GetType().GetField(command[2]);

                    if (field != null)
                    {
                        Debug.Log("1");
                        field.SetValue(
                            this, // So we specify who owns the object
                            (float)field.GetValue(this) + float.Parse(command[3])
                          );
                    }
                }
                break;
            default:
                break;
        }
    }
}
