using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreatSkillAsset")]
[System.Serializable]
public class SkillData : ScriptableObject
{
    [Header("Skill Basic Data")]
    // Skill basic data
    public string Skillname;
    public string SkillIntro;
    public Sprite Skillsprite;
    public string SkillID;

    public string[] Command;
}
