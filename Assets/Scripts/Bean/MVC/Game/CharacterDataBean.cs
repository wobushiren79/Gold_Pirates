using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CharacterDataBean 
{
    public CharacterTypeEnum characterType;
    public float moveSpeed = 1;
    public bool hasGold = false;
    public int life = 10;

    public CharacterDataBean(CharacterTypeEnum characterType)
    {
        this.characterType = characterType;
    }
}