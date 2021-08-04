using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class CameraFollow : NetworkBehaviour
{
    GameObject follow;
    CinemachineVirtualCamera virtualCamera;

    PlayerHP playerHP;

    [SerializeField]
    float ShakeDecay = 7.5f;

    static CinemachineBasicMultiChannelPerlin perlin;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        if(perlin.m_AmplitudeGain > 0)
        {
            perlin.m_AmplitudeGain -= Time.deltaTime * ShakeDecay;
            perlin.m_AmplitudeGain = Mathf.Max(perlin.m_AmplitudeGain, 0);
        }
    }

    public static void CameraShake()
    {
        perlin.m_AmplitudeGain = 5;
    }

    IEnumerator Delay()
    {
        yield return new WaitForFixedUpdate();
        follow = NetworkClient.localPlayer.gameObject;
        virtualCamera.Follow = follow.transform;
        yield return null;
    }
}
