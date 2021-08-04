using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HealthUI : NetworkBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<PlayerHP>().PlayerHealthChanged += Change;
        slider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ClientRpc]
    private void Change(int hp)
    {
        slider.value = hp;
    }
}
