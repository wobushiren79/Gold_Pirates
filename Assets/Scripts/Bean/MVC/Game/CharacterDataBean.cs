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
    public int maxLife = 10;

    public CharacterDataBean(CharacterTypeEnum characterType)
    {
        this.characterType = characterType;
    }

    public int AddLife(int addLife)
    {
        life += addLife;
        if (life < 0)
        {
            life = 0;
        }
        else if (life> maxLife)
        {
            life = maxLife;
        }
        return life;
    }
}