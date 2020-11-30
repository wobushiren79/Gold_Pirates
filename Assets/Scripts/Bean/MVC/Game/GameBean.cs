using System;
using UnityEditor;
using UnityEngine;


[Serializable]
public class GameBean 
{
    public GameStatusEnum gameStatus;
    public long playerGold;
    public long enemyGold;
    //初始海盗数量
    public int initPirateNumber = 1;
    public void AddPlayerGold(long gold)
    {
        playerGold += gold;
        if (playerGold < 0)
        {
            playerGold = 0;
        }
    }

    public void AddEnemyGold(long gold)
    {
        enemyGold += gold;
        if (enemyGold < 0)
        {
            enemyGold = 0;
        }
    }
}