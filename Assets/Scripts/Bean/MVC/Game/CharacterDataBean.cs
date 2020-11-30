using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CharacterDataBean
{
    public CharacterTypeEnum characterType;
    public float moveSpeed = 1;
    public bool hasGold = false;
    public int life = 1;
    public int maxLife = 1;

    public CharacterDataBean(CharacterTypeEnum characterType)
    {
        this.characterType = characterType;
    }

    public void SetLife(int maxLife)
    {
        this.maxLife = maxLife;
        life = maxLife;
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