using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class UserDataBean
{
    //金币数量
    public long gold;
    //海盗数量上限
    public int pirateNumber = 10;
    //速度加成
    public float speed = 0;
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

    public void AddSpeed(float addSpeed)
    {
        speed += addSpeed;
        if (speed < 0)
            speed = 0;
    }
}