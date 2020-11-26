using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CharacterDataBean 
{
    public CharacterTypeEnum characterType;
    public float moveSpeed;
    public bool hasGold = false;
    public int life = 10;

}