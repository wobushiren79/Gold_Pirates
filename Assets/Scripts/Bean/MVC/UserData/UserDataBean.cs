using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class UserDataBean
{
    //金币数量
    public long gold;
    //海盗数量上限
    public int pirateNumber = 1000;
    //速度加成
    public float speed = 1;
    //生命值加成
    public int life = 1;
    //伤害
    public int damage = 1;

    public void AddGold(long addGold)
    {
        gold += addGold;
        if (gold < 0)
            gold = 0;
    }

    public void AddPirateNumber(int number)
    {
        pirateNumber += number;
        if (pirateNumber < 1)
            pirateNumber = 1;
    }

    public float AddSpeed(float addSpeed)
    {
        speed += addSpeed;
        if (speed < 1)
            speed = 1;
        return speed;
    }

    public int AddLife(int addLife)
    {
        life += addLife;
        if (life < 1)
            life = 1;
        return life;
    }

    public int AddDamage(int addDamge)
    {
        damage += addDamge;
        if (damage < 1)
            damage = 1;
        return damage;
    }
}