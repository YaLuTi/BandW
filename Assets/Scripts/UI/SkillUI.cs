using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI SkillName;
    [SerializeField]
    Image SkillImage;
    [SerializeField]
    TextMeshProUGUI SkillIntro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI(SkillData skillData)
    {
        SkillName.text = skillData.Skillname;
        SkillImage.sprite = skillData.Skillsprite;
        SkillIntro.text = skillData.SkillIntro;
    }
}
