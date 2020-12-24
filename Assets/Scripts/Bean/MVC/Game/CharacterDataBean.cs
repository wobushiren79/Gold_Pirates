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
    public CharacterDataBean Clone()
    {
        return this.MemberwiseClone() as CharacterDataBean;
    }
    public void SetLife(int maxLife)
    {
        this.maxLife = maxLife;
        life = maxLife;
    }

    public void AddMaxLife(int addMaxLife)
    {
        SetLife(maxLife + addMaxLife);
    }

    public float SetSpeed(float speed)
    {
        this.moveSpeed = speed;
        return this.moveSpeed;
    }

    public float AddSpeed(float addSpeed)
    {
        moveSpeed += addSpeed;
        if (moveSpeed < 0)
        {
            moveSpeed = 0;
        }
        return moveSpeed;
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