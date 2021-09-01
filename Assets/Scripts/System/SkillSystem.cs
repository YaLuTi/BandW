using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SkillSystem : NetworkBehaviour
{
    [SerializeField]
    SkillDataArray dataArray;
    List<SkillData> skillDatas = new List<SkillData>();
    int[] drawedSkill = new int[3];
    [SerializeField]
    SkillUI[] skillUIs = new SkillUI[3];
    // Start is called before the first frame update
    void Start()
    {
        DrawSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawSkill()
    {
        skillDatas.AddRange(dataArray.skillDatas);
        for (int i = 0; i < 3; i++)
        {
            int r = Random.Range(0, skillDatas.Count);
            drawedSkill[i] = r;
            skillUIs[i].UpdateUI(skillDatas[r]);
            skillDatas.RemoveAt(r);
        }
        skillDatas.Clear();
    }

    public void ChooseSkill()
    {
        NetworkClient.localPlayer.GetComponent<PlayerData>().SetSkill(dataArray.skillDatas[0].Command);
    }
}
