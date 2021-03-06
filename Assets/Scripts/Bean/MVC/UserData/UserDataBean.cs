﻿using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class UserDataBean
{
    //海盗数量上限
    public int pirateNumber = 1000;
    //速度加成
    public float speed = 0;
    //生命值加成
    public int life = 0;
    //伤害
    public int damage = 0;

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